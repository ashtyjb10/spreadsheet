using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        private readonly static Dictionary<String, storedUserInfo> users = new Dictionary<String, storedUserInfo>();
        private readonly static Dictionary<String, GameInfo> games = new Dictionary<String, GameInfo>();
        private static readonly object sync = new object();
        private static int countingID;
        private static string CurrentPendingGame;


        static BoggleService()
        {
            countingID = 0;
            CurrentPendingGame = CreateNewGameID();
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

        public void cancelGame(UserCancel cancelInfo)
        {
            string gameID = users[cancelInfo.UserToken].GameID;
            if (!users.ContainsKey(cancelInfo.UserToken) || gameID == null ||
                games[gameID].GameState != "Pending")
            {
                SetStatus(Forbidden);
            }
            else
            {
                //remove user from pending game.
                if (games[gameID].Player1 == cancelInfo.UserToken)
                {
                    games[gameID].Player1.Equals("");
                    users[cancelInfo.UserToken].GameID.Equals("");

                }
                else
                {
                    games[gameID].Player2.Equals("");
                    users[cancelInfo.UserToken].GameID.Equals("");
                }
                SetStatus(OK);
            }
        }

        public string getGameStats(string GameID)
        {
            //setup the boogle board
            throw new NotImplementedException();
        }

        public string getGameStatsBrief(string GameID)
        {
            throw new NotImplementedException();
        }

        public string joinGame(JoinGameInfo item)
        {
            lock (sync)
            {
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
                    return CurrentPendingGame;

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
                    CreateNewGameID();

                    games[CurrentPendingGame].TimeLimit = ((games[CurrentPendingGame].TimeLimit + item.TimeLimit) / 2);
                    games[CurrentPendingGame].GameState = "Active";
                    BoggleBoard newBoard = new BoggleBoard();
                    games[CurrentPendingGame].BoardObject = newBoard;
                    games[CurrentPendingGame].Board = newBoard.ToString();
                    SetStatus(Created);
                    users[item.UserToken].GameID = gameToReturn;

                    return gameToReturn;
                }
            }
        }

        public int playWord(WordToPlay wordInfo, string gameID)
        {
            if (wordInfo.Word == "" || wordInfo.Word.Trim().Length > 30 || !games.ContainsKey(gameID)
                    || !users.ContainsKey(wordInfo.UserToken))
            {
                SetStatus(Forbidden);
                return 0;
            }
            else if (games[gameID].Player1 != wordInfo.UserToken &&
                games[gameID].Player2 != wordInfo.UserToken)
            {
                SetStatus(Forbidden);
                return 0;
            }
            else if (games[gameID].GameState != "active")
            {
                SetStatus(Conflict);
                return 0;
            }
            else
            {
                int wordPoints = 0;
                if (games[gameID].Player1 == wordInfo.UserToken)
                {
                    //it is player one's

                    if ("a" == "a")//is it an actual word? 
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
                    games[gameID].wordsPlayedP1.Add(wordInfo.Word, 0);
                }
                else
                {
                    //its is player two's word
                    if ("a" == "a")//is it an actual word? 
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
                    games[gameID].wordsPlayedP2.Add(wordInfo.Word, 0);

                }
                //records the trimed word as being played by that UserToken in that game/GameID,
                //returns the scored word in the context of the game(if word played score = 0)
                return 0;
            }
        }

        public string Register(UserInfo user)
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
                    return userToken;
               
                }
            }
        }

        /// <summary>
        /// Demo.  You can delete this.
        /// </summary>
        public string WordAtIndex(int n)
        {
            if (n < 0)
            {
                SetStatus(Forbidden);
                return null;
            }

            string line;
            using (StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (n == 0) break;
                    n--;
                }
            }

            if (n == 0)
            {
                SetStatus(OK);
                return line;
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
        }

        static string CreateNewGameID()
        {
            GameInfo newGame = new GameInfo();
            newGame.GameID = "G" + countingID;
            games.Add(newGame.GameID, newGame);

            countingID++;

            newGame.GameState = "Pending";
            return newGame.GameID;
        }
    }
}
