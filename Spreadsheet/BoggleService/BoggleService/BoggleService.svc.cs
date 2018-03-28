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
        private readonly static Dictionary<String, UserInfo> users = new Dictionary<String, UserInfo>();
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

        public void cancelGame(string UserToken)
        {
            throw new NotImplementedException();
        }

        public string getGameStats(string GameID)
        {
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
                    SetStatus(Created);
                    return gameToReturn;
                }
            }
        }

        public string playWord(string GameID)
        {
            throw new NotImplementedException();
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
                    users.Add(userToken, user);
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
