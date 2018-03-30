//created by Ashton Schmidt and Nathan Herrmann
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

        /// <summary>
        /// If UserToken or time limit is bad setStatue to Forbidden and return nothing. If the user is 
        /// already in a pending game then SetStatus to Conflict. If there is no players in the game the 
        /// user is set to player one and TimeLimit is stored and return game id. If thers is already a player in the game then
        /// We make that game active and set all the information for the now active game.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public UserGame joinGame(JoinGameInfo item)
        {
            lock (sync)
            {
                //the new GameID
                UserGame returnGID = new UserGame();

                if (!users.ContainsKey(item.UserToken) || item.TimeLimit < 5
                    || item.TimeLimit > 120)
                {
                    SetStatus(Forbidden);
                    return null;
                }

                if(games[CurrentPendingGame].Player1 == null)
                {
                    //add the information for game information.
                    games[CurrentPendingGame].Player1 = item.UserToken;
                    games[CurrentPendingGame].TimeLimit = item.TimeLimit;
                    SetStatus(Accepted);

                    //add the game id to the users data.
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

                    //average the two TimeLimits.
                    games[CurrentPendingGame].TimeLimit = ((games[CurrentPendingGame].TimeLimit + item.TimeLimit) / 2);
                    
                    //Create a new board and activate the current game, and set the time in seconds when the game started.
                    BoggleBoard newBoard = new BoggleBoard();
                    games[CurrentPendingGame].GameState = "active";
                    games[CurrentPendingGame].TimeGameStarted = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                    games[CurrentPendingGame].BoardObject = newBoard;
                    games[CurrentPendingGame].Board = newBoard.ToString();
                    SetStatus(Created);

                    //add info to data stored here.
                    users[item.UserToken].GameID = gameToReturn;
                    returnGID.GameID = gameToReturn;

                    //create a new pending game for users to join.
                    CurrentPendingGame = CreateNewGameID();


                    return returnGID;
                }
            }
        }

        /// <summary>
        /// play word returns a WordScore Object. if the word is not within the valid constrains of the
        /// API then SetStaus is Forbidden. If the Game is anything other than active SetStaus is set to
        /// Conflict. Otherwise the word is scored and the score and word is stored in WordScore, and
        /// SetStatus is OK.
        /// </summary>
        /// <param name="wordInfo"></param>
        /// <param name="gameID"></param>
        /// <returns></returns>
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

                    //is it player1's move or player2's?
                    if (games[gameID].Player1 == wordInfo.UserToken)
                    {
                        int wordPoints = 0;

                        //it is player one's

                        //is it an actual word?
                        if (words.ContainsKey(wordInfo.Word))
                        {
                            //can it be created on the current board?
                            if (games[gameID].BoardObject.CanBeFormed(wordInfo.Word))
                            {
                                //get the score of the word!
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
                            else
                            {
                                //cant be dubplicated on board.
                                wordPoints = -1;
                            }

                        }
                        else
                        {
                            //not a valid word in the dictionary.
                            wordPoints = -1;
                        }
                        //if the word has not already been played then add it and the score.
                        if(!games[gameID].wordsPlayedP1.ContainsKey(wordInfo.Word))
                        {
                            games[gameID].wordsPlayedP1.Add(wordInfo.Word, wordPoints);
                            games[gameID].p1Score += wordPoints;
                            score.Score = wordPoints;

                        }
                        SetStatus(OK);
                        return score;
                    }
                    else
                    {
                        int wordPoints = 0;

                        //its is player two's move!

                        //is it an actual word in the dictuinary?
                        if (words.ContainsKey(wordInfo.Word))
                        {
                            //can it be duplicated on the board?
                            if (games[gameID].BoardObject.CanBeFormed(wordInfo.Word))
                            {
                                //score the valid word.
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
                            else
                            {
                                //cant be duplicated on board.
                                wordPoints = -1;
                            }

                        }
                        else
                        {
                            //not a vlid word in the dictionary.
                            wordPoints = -1;
                        }
                        //if the word has not already been played add it and the score.
                        if (!games[gameID].wordsPlayedP2.ContainsKey(wordInfo.Word))
                        {
                            games[gameID].wordsPlayedP2.Add(wordInfo.Word, wordPoints);
                            games[gameID].p2Score += wordPoints;
                            score.Score = wordPoints;

                        }
                        SetStatus(OK);
                        return score;
                    }
                }
            }
        }

        /// <summary>
        /// Registers a new User. If the Nickname doesnt fit the API standards that SetStaus is Forbidden and nothing
        /// is returned. Otherwise we create a new token add a new user to the Dictionary of users, and 
        /// sent back a new UserToke.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

                    //get new token.
                    string userToken = Guid.NewGuid().ToString();

                    //create new user
                    storedUserInfo newUser = new storedUserInfo();

                    //add nickname and token to user.
                    newUser.Nickname = user.Nickname;
                    newUser.UserToken = userToken;

                    //add user to the dicionary of users.
                    users.Add(userToken, newUser);
                    SetStatus(Created);

                    //send back a new userToken.
                    UserToke token = new UserToke();
                    token.UserToken = userToken;
                    return token;
               
                }
            }
        }

        /// <summary>
        /// Creates the new GameID string that is used for the games, and creates a new pending game.
        /// </summary>
        /// <returns></returns>
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
