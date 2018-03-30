using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        //store the users.
        private readonly static Dictionary<String, storedUserInfo> users = new Dictionary<String, storedUserInfo>();
        //store all of the games.
        private readonly static Dictionary<String, GameInfo> games = new Dictionary<String, GameInfo>();
        //allow for thread manipulation.
        private static readonly object sync = new object();
        //What game number / id # we are on
        private static int countingID;
        //the new game Id generated
        private static string CurrentPendingGame;
        //The Dictionary that stores the valid words from dictionary.txt.
        private readonly static Dictionary<String, String> words = new Dictionary<String, String>();


        static BoggleService()
        {

            countingID = 0;
            CurrentPendingGame = CreateNewGameID();
            
            //Reads in the dictionary for all games to use.
            string path = AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() > -1)
            {
                string wordToAdd = reader.ReadLine();
                words.Add(wordToAdd, wordToAdd);
            }

            reader.Close();
        }
        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API()
        {
            SetStatus(OK);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        /// <summary>
        /// Returns nothing but will set the status to Forbidden if the GameState is not pending,
        /// otherwise will set the status to OK and remove player1 from the game.
        /// </summary>
        /// <param name="cancelInfo"></param>
        public void cancelGame(UserCancel cancelInfo)
        {
            lock (sync)
            {
                string gameID = users[cancelInfo.UserToken].GameID;
                if (!users.ContainsKey(cancelInfo.UserToken) || gameID == null ||
                    games[gameID].GameState != "pending")
                {
                    SetStatus(Forbidden);
                }
                else
                {
                    //remove user from pending game.
                    if (games[gameID].Player1 == cancelInfo.UserToken)
                    {
                        games[gameID].Player1 = null;
                        users[cancelInfo.UserToken].GameID = null;
                        SetStatus(OK);
                        return;

                    }
                }
            }
        }
       
        /// <summary>
        /// Returns a FullGameInfo object that will be serialized.  If the gameID is not valid nothing is returned and 
        /// the status is set to Forbidden. Otherwise the game will be set to ok and have the correct things added specified
        /// via the API.
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>

        public FullGameInfo getGameStats(string GameID)
        {
            lock (sync)
            {
                
                if (!games.ContainsKey(GameID))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {
                    //current game object
                    GameInfo current = games[GameID];
                    //new object to return.
                    FullGameInfo infoToReturn = new FullGameInfo();

                    if (games[GameID].GameState == "pending")
                    {

                        SetStatus(OK);
                        infoToReturn.GameState = "pending";
                        return infoToReturn;

                    }
                    else if (current.GameState == "active")
                    {
                        infoToReturn.GameState = "active";
                        infoToReturn.Board = current.Board;
                        infoToReturn.TimeLimit = current.TimeLimit;
                        //get the current time in seconds subtract it from the time the game started, and add that to the TimeLimit
                        int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                        int timeLeft = infoToReturn.TimeLimit + (current.TimeGameStarted - currentTime);
                        if (timeLeft <= 0)
                        {
                            current.GameState = "completed";
                        }
                        infoToReturn.TimeLeft = timeLeft;

                        //set the player (1 and 2) information in the object to return.
                        infoToReturn.Player1 = new Player1();
                        infoToReturn.Player1.Nickname = users[current.Player1].Nickname;
                        infoToReturn.Player1.Score = current.p1Score;
                        infoToReturn.Player2 = new Player2();
                        infoToReturn.Player2.Nickname = users[current.Player2].Nickname;
                        infoToReturn.Player2.Score = current.p2Score;
                        SetStatus(OK);
                        return infoToReturn;
                    }
                    else
                    {
                        //game is completed!
                        infoToReturn.GameState = "completed";
                        infoToReturn.Board = current.Board;
                        infoToReturn.TimeLimit = current.TimeLimit;
                        infoToReturn.TimeLeft = 0;

                        //set player1 info.
                        infoToReturn.Player1 = new Player1();
                        infoToReturn.Player1.Nickname = users[current.Player1].Nickname;
                        infoToReturn.Player1.Score = current.p1Score;

                        //list of words played by player 1 added to the infoToReturn.
                        List<WordInfo> playedListP1 = new List<WordInfo>();
                        foreach (KeyValuePair<string, int> entry in current.wordsPlayedP1)
                        {
                            WordInfo word = new WordInfo();
                            word.Word = entry.Key;
                            word.Score = "" + entry.Value;

                            if (!playedListP1.Contains(word))
                            {
                                playedListP1.Add(word);
                            }
                        }

                        infoToReturn.Player1.WordsPlayed = playedListP1;

                        //set player2 info.
                        infoToReturn.Player2 = new Player2();
                        infoToReturn.Player2.Nickname = users[current.Player2].Nickname;
                        infoToReturn.Player2.Score = current.p2Score;

                        //list of words played by player 2 added to the infoToReturn.
                        List<WordInfo> playedListP2 = new List<WordInfo>();
                        foreach (KeyValuePair<string, int> entry in current.wordsPlayedP2)
                        {
                            WordInfo word = new WordInfo();
                            word.Word = entry.Key;
                            word.Score = ""+entry.Value;

                            if (!playedListP2.Contains(word))
                            {
                                playedListP2.Add(word);
                            }
                        }
                        infoToReturn.Player2.WordsPlayed = playedListP2;
                        SetStatus(OK);
                        return infoToReturn;
                    }
                }
            }
        }

        /// <summary>
        /// See above for outline. All that is different is that infoToReturn only sets the gameStatus, TimeLeft,
        /// and player 1, and 2's score.
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        public FullGameInfo getGameStatsBrief(string GameID)
        {

            lock (sync)
            {

                if (!games.ContainsKey(GameID))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {
                    //get current game requested
                    GameInfo current = games[GameID];
                    //new object to return.
                    FullGameInfo infoToReturn = new FullGameInfo();


                    infoToReturn.GameState = current.GameState;
                    if (infoToReturn.GameState.Equals("pending"))
                    {
                        SetStatus(OK);
                        return infoToReturn;
                    }

                    //take current time subtract it from the time the game started and add that to the time Limit. 
                    int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                    int timeLeft = infoToReturn.TimeLimit + (current.TimeGameStarted - currentTime);
                    if (timeLeft <= 0)
                    {
                        current.GameState = "completed";
                        timeLeft = 0;
                    }
                    infoToReturn.TimeLeft = timeLeft;

                    //set Player1 info.
                    infoToReturn.Player1 = new Player1();
                    infoToReturn.Player1.Score = current.p1Score;
                    //set Playe2 info.
                    infoToReturn.Player2 = new Player2();
                    infoToReturn.Player2.Score = current.p2Score;
                    SetStatus(OK);

                    return infoToReturn;
                }
            }
        }

        public UserGame joinGame(JoinGameInfo item)
        {
            lock (sync)
            {
                UserGame returnGID = new UserGame();
                if (!users.ContainsKey(item.UserToken) || item.TimeLimit < 5
                    || item.TimeLimit > 120)
                {
                    SetStatus(Forbidden);
                    return null;
                }

                if(games[CurrentPendingGame].Player1 == null)
                {
                    games[CurrentPendingGame].Player1 = item.UserToken;
                    games[CurrentPendingGame].TimeLimit = item.TimeLimit;
                    SetStatus(Accepted);

                    users[item.UserToken].GameID = CurrentPendingGame;
                    returnGID.GameID = CurrentPendingGame;
                    return returnGID;

                }
                else if (games[CurrentPendingGame].Player1 == item.UserToken)
                {

                    SetStatus(Conflict);
                    return null;
                }
                else
                {
                    games[CurrentPendingGame].Player2 = item.UserToken;

                    string gameToReturn = CurrentPendingGame;

                    games[CurrentPendingGame].TimeLimit = ((games[CurrentPendingGame].TimeLimit + item.TimeLimit) / 2);
                    BoggleBoard newBoard = new BoggleBoard();
                    games[CurrentPendingGame].GameState = "active";
                    games[CurrentPendingGame].TimeGameStarted = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                    games[CurrentPendingGame].BoardObject = newBoard;
                    games[CurrentPendingGame].Board = newBoard.ToString();
                    SetStatus(Created);
                    users[item.UserToken].GameID = gameToReturn;
                    returnGID.GameID = gameToReturn;
                    CurrentPendingGame = CreateNewGameID();


                    return returnGID;
                }
            }
        }

        public WordScore playWord(WordToPlay wordInfo, string gameID)
        {
            lock (sync)
            {
                WordScore score = new WordScore();
                score.Score = 0;

                if (wordInfo.Word == "" || wordInfo.Word == null || wordInfo.Word.Trim().Length > 30 || !games.ContainsKey(gameID)
                        || !users.ContainsKey(wordInfo.UserToken))
                {
                    SetStatus(Forbidden);
                    return score;
                }
                else if (games[gameID].Player1 != wordInfo.UserToken &&
                    games[gameID].Player2 != wordInfo.UserToken)
                {
                    SetStatus(Forbidden);
                    return score;
                }
                else if (games[gameID].GameState != "active")
                {
                    SetStatus(Conflict);
                    return score;
                }
                else
                {
                    if (games[gameID].Player1 == wordInfo.UserToken)
                    {
                        int wordPoints = 0;

                        //it is player one's

                        if (words.ContainsKey(wordInfo.Word))//is it an actual word? 
                        {
                            if (games[gameID].BoardObject.CanBeFormed(wordInfo.Word))
                            {
                                if (wordInfo.Word.Length < 3)
                                {
                                    wordPoints = 0;
                                }
                                else if (wordInfo.Word.Length >= 3 && wordInfo.Word.Length <= 4)
                                {
                                    wordPoints = 1;
                                }
                                else if (wordInfo.Word.Length == 5)
                                {
                                    wordPoints = 2;
                                }
                                else if (wordInfo.Word.Length == 6)
                                {
                                    wordPoints = 3;
                                }
                                else if (wordInfo.Word.Length == 7)
                                {
                                    wordPoints = 5;
                                }
                                else
                                {
                                    //it is longer than 7
                                    wordPoints = 11;
                                }

                            }

                        }
                        if(!games[gameID].wordsPlayedP1.ContainsKey(wordInfo.Word))
                        {
                            games[gameID].wordsPlayedP1.Add(wordInfo.Word, wordPoints);
                            games[gameID].p1Score += wordPoints;
                            score.Score = wordPoints;

                        }
                        return score;
                    }
                    else
                    {
                        int wordPoints = 0;

                        //its is player two's word
                        if (words.ContainsKey(wordInfo.Word))//is it an actual word? 
                        {
                            if (games[gameID].BoardObject.CanBeFormed(wordInfo.Word))
                            {
                                if (wordInfo.Word.Length < 3)
                                {
                                    wordPoints = 0;
                                }
                                else if (wordInfo.Word.Length >= 3 && wordInfo.Word.Length <= 4)
                                {
                                    wordPoints = 1;
                                }
                                else if (wordInfo.Word.Length == 5)
                                {
                                    wordPoints = 2;
                                }
                                else if (wordInfo.Word.Length == 6)
                                {
                                    wordPoints = 3;
                                }
                                else if (wordInfo.Word.Length == 7)
                                {
                                    wordPoints = 5;
                                }
                                else
                                {
                                    //it is longer than 7
                                    wordPoints = 11;
                                }

                            }

                        }
                        if (!games[gameID].wordsPlayedP2.ContainsKey(wordInfo.Word))
                        {
                            games[gameID].wordsPlayedP2.Add(wordInfo.Word, wordPoints);
                            games[gameID].p2Score += wordPoints;
                            score.Score = wordPoints;

                        }
                        return score;
                        //todo do I need to set a status for OK?
                    }
                }
            }
        }

        public UserToke Register(UserInfo user)
        {
            lock (sync)
            {
                if (user.Nickname == null || user.Nickname.Trim().Length == 0 || user.Nickname.Trim().Length > 50)
                    {
                    SetStatus(Forbidden);
                    return null;
                }
                else
                {

                    string userToken = Guid.NewGuid().ToString();
                    storedUserInfo newUser = new storedUserInfo();
                    newUser.Nickname = user.Nickname;
                    newUser.UserToken = userToken;
                    users.Add(userToken, newUser);
                    SetStatus(Created);
                    UserToke token = new UserToke();
                    token.UserToken = userToken;
                    return token;
               
                }
            }
        }

        static string CreateNewGameID()
        {
            GameInfo newGame = new GameInfo();
            newGame.GameID = "G" + countingID;
            games.Add(newGame.GameID, newGame);

            countingID++;

            newGame.GameState = "pending";
            return newGame.GameID;
        }


    }
}
