//created by Ashton Schmidt and Nathan Herrmann
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        private static string BoggleDB;

        static BoggleService()
        {
            //the connection string is stored in the Web.config file where it can easily be found and changed.
            BoggleDB = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;

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
                if (cancelInfo.UserToken == null)
                {
                    SetStatus(Forbidden);
                    return;
                }
                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        //check to see if it is in the users
                        string query = "SELECT UserID  FROM dbo.Users WHERE UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", cancelInfo.UserToken);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    trans.Commit();
                                    return;
                                }
                            }
                        }

                        //check to see if it is in the pending game
                        query = "SELECT TOP 1 * FROM dbo.Games ORDER BY GameID DESC";
                        string gameStatus = null;
                        string player1Toke = null;
                        int? gameID = null;
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    gameID = (int)reader["GameID"];
                                    if (reader["Player1"] != System.DBNull.Value)
                                    {
                                        player1Toke = (string)reader["Player1"];
                                    }
                                    if (reader["GameStatus"] != System.DBNull.Value)
                                    {
                                        gameStatus = (string)reader["GameStatus"];
                                    }
                                    if (gameStatus != "pending" || player1Toke != cancelInfo.UserToken)
                                    {
                                        SetStatus(Forbidden);
                                        reader.Close();
                                        trans.Commit();
                                        return;

                                    }

                                }
                            }

                        }
                        // it is okay to remove the game.
                        query = "DELETE FROM Games WHERE GameID = @GameID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", gameID);
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                //something went wrong.
                            }
                            else
                            {
                                SetStatus(OK);
                                trans.Commit();
                                return;
                            }

                        }
                    }
                }
                /*
                    //string gameID = users[cancelInfo.UserToken].GameID;
                    if (!users.ContainsKey(cancelInfo.UserToken) || users[cancelInfo.UserToken].GameID == null)
                    {
                        SetStatus(Forbidden);
                    }
                    else
                    {
                        string gameID = users[cancelInfo.UserToken].GameID;
                        if (games[gameID].GameState != "pending")
                        {
                            SetStatus(Forbidden);
                        }
                        //remove user from pending game.
                        else if (games[gameID].Player1 == cancelInfo.UserToken)
                        {
                            games[gameID].Player1 = null;
                            users[cancelInfo.UserToken].GameID = null;
                            SetStatus(OK);
                            return;

                        }
                    }
                    */
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
                FullGameInfo gameInfo = new FullGameInfo();
                string gameStatus = null;
                string player1 = null;
                string player2 = null;
                string board = null;
                int? timeLimit = null;
                int? startGame = null;
                int? timeLeft = null;

                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        string query = "Select * From dbo.Games Where GameID = @GameID";

                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    trans.Commit();
                                    return null;
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        //string b = (string)reader["Board"];
                                        gameStatus = (string)reader["GameStatus"];
                                        if (gameStatus == "pending")
                                        {
                                            gameInfo.GameState = gameStatus;
                                            SetStatus(OK);
                                            reader.Close();
                                            trans.Commit();
                                            return gameInfo;
                                        }
                                        else
                                        {
                                            player1 = (string)reader["Player1"];
                                            player2 = (string)reader["Player2"];
                                            board = (string)reader["Board"];
                                            timeLimit = (int)reader["TimeLimit"];
                                            DateTime startTime = reader.GetDateTime(5);
                                            int startInInt = (int)(startTime - new DateTime(1970, 1, 1)).TotalSeconds;
                                            startGame = startInInt;

                                        }
                                    }
                                    reader.Close();
                                }
                            }

                            gameInfo.GameState = gameStatus;
                            gameInfo.Board = board;
                            gameInfo.TimeLimit = (int)timeLimit;
                            gameInfo.Player1 = new Player1();
                            gameInfo.Player2 = new Player2();
                            int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                            timeLeft = gameInfo.TimeLimit + ((int)startGame - currentTime);
                        }
                        //get names.
                        query = "SELECT Player1,Player2 FROM Games WHERE GameID = @GameID";
                        string player1Tok = null;
                        string player2Tok = null;
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    player1Tok = (string)reader["Player1"];
                                    player2Tok = (string)reader["Player2"];
                                }
                                reader.Close();
                            }
                        }

                        query = "SELECT Nickname FROM Users WHERE UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", player1Tok);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    gameInfo.Player1.Nickname = (string)reader["Nickname"];
                                }
                            }
                        }
                        query = "SELECT Nickname FROM Users WHERE UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", player2Tok);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    gameInfo.Player2.Nickname = (string)reader["Nickname"];
                                }
                            }
                        }

                        //query to get the nicknames of the users.

                        query = "SELECT * FROM words WHERE GameID = @GameID AND Player = @Player";
                        int player1Score = 0;
                        List<WordInfo> player1Words = new List<WordInfo>();
                        int player2Score = 0;
                        List<WordInfo> player2Words = new List<WordInfo>();

                        //make a list for each and then if it is contained dont count the value.
                        if (timeLeft <= 0)
                        {
                            //game complete

                            //get player1 info
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player1);

                                //get player one info.
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WordInfo wordStats = new WordInfo();
                                        wordStats.Word = (string)reader["Word"];
                                        int score = (int)reader["Score"];
                                        wordStats.Score = score.ToString();
                                        player1Score += score;
                                        player1Words.Add(wordStats);
                                    }
                                    reader.Close();
                                }
                            }

                            //get Player2 info.
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player2);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WordInfo wordStats = new WordInfo();
                                        wordStats.Word = (string)reader["Word"];
                                        int score = (int)reader["Score"];
                                        wordStats.Score = score.ToString();
                                        player2Score += score;
                                        player2Words.Add(wordStats);
                                    }
                                    reader.Close();

                                }
                            }

                            SetStatus(OK);
                            trans.Commit();
                            gameInfo.Player1.Score = player1Score;
                            gameInfo.Player1.WordsPlayed = player1Words;
                            gameInfo.Player2.Score = player2Score;
                            gameInfo.Player2.WordsPlayed = player2Words;
                            gameInfo.TimeLeft = 0;
                            gameInfo.GameState = "completed";
                            return gameInfo;

                        }
                        else
                        {
                            //game active.
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player1);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        player1Score += (int)reader["Score"];
                                    }
                                    reader.Close();
                                }
                            }
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player2);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        player2Score += (int)reader["Score"];
                                    }
                                    reader.Close();
                                }

                            }
                            gameInfo.Player1.Score = player1Score;
                            gameInfo.Player2.Score = player2Score;
                            gameInfo.TimeLeft = timeLeft;
                        }

                        SetStatus(OK);
                        trans.Commit();
                        return gameInfo;


                        /* else if (gameInfo.GameState == "active")
                         {
                             gameInfo.Board = (string)reader["Board"];
                             gameInfo.TimeLimit = (int)reader["TimeLimit"];

                             //do the time calculations.
                             DateTime startTime = reader.GetDateTime(5);
                             int startInInt = (int)(startTime - new DateTime(1970, 1, 1)).TotalSeconds;
                             int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                             int timeLeft = gameInfo.TimeLimit + (startInInt - currentTime);
                             // get the rest of the info for the reader.
                             string player1 = (string)reader["Player1"];
                             string player2 = (String)reader["Player2"];


                             if (timeLeft <= 0)
                             {
                                 gameInfo.GameState = "completed";
                                 gameInfo.TimeLeft = 0;

                                 //add the player information
                                 gameInfo.Player1 = new Player1();
                                 gameInfo.Player2 = new Player2();
                                 //get the player nickname, and the score of the words played

                             }
                             else
                             {
                                 gameInfo.TimeLeft = timeLeft;
                                 // get the player nickname, and the score and words played with score.

                             }



                         }
                         */

                        // the id is valid.

                        //now we need to know if the status is pending. / get all user data. 


                    }
                }

                /*
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
                                infoToReturn.TimeLeft = 0;

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
                                    word.Score = "" + entry.Value;

                                    if (!playedListP2.Contains(word))
                                    {
                                        playedListP2.Add(word);
                                    }
                                }
                                infoToReturn.Player2.WordsPlayed = playedListP2;
                                SetStatus(OK);
                                return infoToReturn;
                            }
                            else
                            {
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
                                word.Score = "" + entry.Value;

                                if (!playedListP2.Contains(word))
                                {
                                    playedListP2.Add(word);
                                }
                            }
                            infoToReturn.Player2.WordsPlayed = playedListP2;
                            SetStatus(OK);
                            return infoToReturn;
                        }
                    }*/
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

                FullGameInfo gameInfo = new FullGameInfo();
                string player1Tok = null;
                string player2Tok = null;
                string gameState = null;
                int? timeStarted = null;
                int timeLimit = 0;
                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        string query = "Select * From dbo.Games Where GameID = @GameID";

                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    trans.Commit();
                                    return null;
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        player1Tok = (string)reader["Player1"];
                                        player2Tok = (string)reader["Player2"];
                                        gameState = (string)reader["GameState"];
                                        timeLimit = (int)reader["TimeLimit"];

                                        if (gameState == "pending")
                                        {
                                            SetStatus(Forbidden);
                                            reader.Close();
                                            trans.Commit();
                                            return null;
                                        }

                                        DateTime startTime = reader.GetDateTime(5);
                                        int startInInt = (int)(startTime - new DateTime(1970, 1, 1)).TotalSeconds;
                                        timeStarted = startInInt;
                                        int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                                        int timeLeft = (int)(gameInfo.TimeLimit + (timeStarted - currentTime));


                                        if (timeLeft <= 0)
                                        {
                                            gameInfo.GameState = "completed";
                                            gameInfo.TimeLeft = 0;
                                        }
                                        else
                                        {
                                            gameInfo.GameState = gameState;
                                            gameInfo.TimeLeft = timeLeft;
                                        }


                                    }
                                }
                                reader.Close();

                            }
                        }

                        query = "SELECT * FROM words WHERE GameID = @GameID AND Player = @Player";
                        int player1Score = 0;
                        int player2Score = 0;


                        //get player1 info
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);
                            cmd.Parameters.AddWithValue("@Player", player1Tok);

                            //get player one info.
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    WordInfo wordStats = new WordInfo();
                                    wordStats.Word = (string)reader["Word"];
                                    int score = (int)reader["Score"];
                                    wordStats.Score = score.ToString();
                                    player1Score += score;
                                }
                                reader.Close();
                            }
                        }

                        //get Player2 info.
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);
                            cmd.Parameters.AddWithValue("@Player", player2Tok);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    WordInfo wordStats = new WordInfo();
                                    wordStats.Word = (string)reader["Word"];
                                    int score = (int)reader["Score"];
                                    wordStats.Score = score.ToString();
                                    player2Score += score;
                                }
                                reader.Close();

                            }
                        }

                        SetStatus(OK);
                        trans.Commit();
                        gameInfo.Player1.Score = player1Score;
                        gameInfo.Player2.Score = player2Score;
                        return gameInfo;

                    }
                }


                /* if (!games.ContainsKey(GameID))
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
                 */
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


                if (item.UserToken == null || item.TimeLimit < 5 ////?????? should it be null??
                    || item.TimeLimit > 120)
                {
                    SetStatus(Forbidden);
                    return null;
                }

                //need to check if the DB contains the user token. if not then we set status to forbidden.)
                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        string query1 = "select UserID from Users where UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query1, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", item.UserToken);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    //the userID doesnt exist!
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    trans.Commit();
                                    return null;

                                }

                            }
                        }

                        //now we need to go to the game.... and see which player we need to get.
                        string query = "SELECT TOP 1 * FROM dbo.Games ORDER BY GameID DESC";
                        string player1Tok = null;
                        string player2Tok = null;
                        int gameID;

                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    gameID = (int)reader["GameID"];
                                    if (reader["Player1"] != System.DBNull.Value)
                                    {
                                        player1Tok = (string)reader["Player1"];
                                    }
                                    if (reader["Player2"] != System.DBNull.Value)
                                    {
                                        player2Tok = (string)reader["Player2"];
                                    }

                                }
                            }
                        }

                        if (player1Tok != null && player2Tok != null)
                        {
                            query = "INSERT INTO dbo.Games  (Player1, TimeLimit, GameStatus) VALUES(@Player1, @TimeLimit, @GameStatus)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@Player1", item.UserToken);
                                cmd.Parameters.AddWithValue("@TimeLimit", item.TimeLimit);
                                cmd.Parameters.AddWithValue("@GameStatus", "pending");
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    //was a row returned?
                                    if (!reader.HasRows)
                                    {

                                    }
                                }
                                cmd.CommandText = "SELECT TOP 1 * FROM dbo.Games ORDER BY GameID DESC";
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        gameID = (int)reader["GameID"];
                                        returnGID.GameID = gameID.ToString();
                                    }
                                }
                                SetStatus(Accepted);
                                trans.Commit();
                                return returnGID;


                            }

                        }
                        else
                        {
                            //


                            int player1Time = 0;
                            gameID = 0;

                            ;
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        player1Time = (int)reader["TimeLimit"];
                                        gameID = (int)reader["GameID"];
                                    }
                                }
                            }

                            query = "UPDATE dbo.Games SET Player2 = @Player2, TimeLimit = @TimeLimit, StartTime = @StartTime," +
                                "Board = @Board, GameStatus = @GameStatus  WHERE GameID = @GameID";
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {

                                BoggleBoard newBoard = new BoggleBoard();
                                cmd.Parameters.AddWithValue("@GameID", gameID);
                                cmd.Parameters.AddWithValue("@Player2", item.UserToken);
                                player1Time = (int)(player1Time + item.TimeLimit) / 2;
                                cmd.Parameters.AddWithValue("@TimeLimit", player1Time);
                                cmd.Parameters.AddWithValue("@StartTime", (DateTime.UtcNow));
                                cmd.Parameters.AddWithValue("@Board", newBoard.ToString());
                                cmd.Parameters.AddWithValue("@GameStatus", "active");

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    //was a row returned?
                                    if (!reader.HasRows)
                                    {

                                    }
                                }
                                cmd.CommandText = "SELECT TOP 1 * FROM dbo.Games ORDER BY GameID DESC";
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        gameID = (int)reader["GameID"];
                                        returnGID.GameID = gameID.ToString();
                                    }
                                }
                                SetStatus(Created);
                                trans.Commit();
                                return returnGID;
                            }

                        }
                    }
                }



                /*
                    if (games[CurrentPendingGame].Player1 == null)
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
                    */
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
            /*lock (sync)
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
                int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                int timeLeft = games[gameID].TimeLimit + (games[gameID].TimeGameStarted - currentTime);
                 if (timeLeft <= 0)
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
                                if (wordInfo.Word.Length < 3)
                                {
                                    wordPoints = 0;
                                }
                                else
                                {
                                    wordPoints = -1;
                                }
                            }

                        }
                        else
                        {
                            //not a valid word in the dictionary.
                            if (wordInfo.Word.Length < 3)
                            {
                                wordPoints = 0;
                            }
                            else
                            {
                                wordPoints = -1;
                            }
                            
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
                                if (wordInfo.Word.Length < 3)
                                {
                                    wordPoints = 0;
                                }
                                else
                                {
                                    wordPoints = -1;
                                }
                            }

                        }
                        else
                        {
                            //not a vlid word in the dictionary.
                            if (wordInfo.Word.Length < 3)
                            {
                                wordPoints = 0;
                            }
                            else
                            {
                                wordPoints = -1;
                            }
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
            */

            lock (sync)
            {

                WordScore score = new WordScore();
                score.Score = 0;

                //Check the word for valid input.  If not valid, fobidden set and score 0 returned.
                if (wordInfo.Word == "" || wordInfo.Word == null || wordInfo.Word.Trim().Length > 30)
                {
                    SetStatus(Forbidden);
                    return score;
                }

                //Connect to the Database
                using (SqlConnection conn = new SqlConnection(BoggleDB))
                {

                    conn.Open();

                    //Set up transaction.
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        string Player1 = null;
                        string Player2 = null;
                        string Board = null;
                        int? TimeLimit = null;
                        DateTime StartTime = DateTime.UtcNow;
                        string GameStatus = null;

                        //Set command to find gameID.
                        using (SqlCommand findGameid =
                            new SqlCommand("Select * from Games where GameID = @GameID",
                                            conn,
                                            trans))
                        {
                            //Set parameter.
                            findGameid.Parameters.AddWithValue("@GameID", gameID);

                            //Use command in reader.
                            using (SqlDataReader GameIdReader = findGameid.ExecuteReader())
                            {

                                //If the reader does not return a row, set forbidden.
                                if (!GameIdReader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    GameIdReader.Close();
                                    trans.Commit();
                                    return score;
                                }

                                while (GameIdReader.Read())
                                {
                                    Player1 = GameIdReader["Player1"].ToString();
                                    Player2 = GameIdReader["Player2"].ToString();
                                    Board = GameIdReader["Board"].ToString();
                                    GameStatus = GameIdReader["GameStatus"].ToString();
                                    if(GameStatus != "active")
                                    {
                                        SetStatus(Forbidden);
                                        GameIdReader.Close();
                                        trans.Commit();
                                        return score;
                                    }

                                    TimeLimit = (int)GameIdReader["TimeLimit"];
                                    StartTime = GameIdReader.GetDateTime(5);
                                    
                                }
                                GameIdReader.Close();


                            }
                            //Set command to find if UserID exists.
                            using (SqlCommand findUserid =
                                new SqlCommand("Select UserID from Users where UserID = @UserID",
                                                conn,
                                                trans))
                            {
                                //Set the parameter.
                                findUserid.Parameters.AddWithValue("@UserID", wordInfo.UserToken);

                                //User command reader.
                                using (SqlDataReader UserIdReader = findUserid.ExecuteReader())
                                {
                                    //If the reader does not return a row, set forbidden
                                    if (!UserIdReader.HasRows)
                                    {
                                        SetStatus(Forbidden);
                                        UserIdReader.Close();
                                        trans.Commit();
                                        return score;
                                    }
                                    UserIdReader.Close();

                                }
                            }
                            
                            //If neither of the PlayerIds match the playerId playing, set forbidden and return zero score.
                            if (Player1 != (wordInfo.UserToken) &&
                                (Player2 != (wordInfo.UserToken)))
                            {
                                SetStatus(Forbidden);
                                trans.Commit();
                                return score;
                            }

                            //If the time limit is zero, then set conflict and return zero score.
                            int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                            int time = (int)(StartTime - new DateTime(1970, 1, 1)).TotalSeconds;
                            int timeLeft = (int)(TimeLimit + (time - currentTime));
                            if (timeLeft <= 0)
                            {
                                SetStatus(Conflict);
                                trans.Commit();
                                return score;
                            }

                            //If it is Player1 who moves.
                            if ((Player1) == (wordInfo.UserToken))
                            {
                                int wordPoints = 0;

                                //Check the word.
                                if (words.ContainsKey(wordInfo.Word))
                                {
                                    //Create the board to check if it can be formed.
                                    BoggleBoard board = new BoggleBoard(Board);

                                    //Can it be created on the board?
                                    if (board.CanBeFormed(wordInfo.Word))
                                    {
                                        //Get the score of the word.
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
                                        if (wordInfo.Word.Length < 3)
                                        {
                                            wordPoints = 0;
                                        }
                                        else
                                        {
                                            wordPoints = -1;
                                        }
                                    }
                                }
                                else
                                {
                                    //not a valid word in the dictionary.
                                    if (wordInfo.Word.Length < 3)
                                    {
                                        wordPoints = 0;
                                    }
                                    else
                                    {
                                        wordPoints = -1;
                                    }
                                }
                                //If the word has not already been played the add it and the score.

                                using (SqlCommand findWordsPlayed =
                                        new SqlCommand("Select * from Words where GameID = @GameID AND Player = @Player AND Word = @Word",
                                        conn,
                                        trans))
                                {
                                    findWordsPlayed.Parameters.AddWithValue("@GameID", gameID);
                                    findWordsPlayed.Parameters.AddWithValue("@Player", wordInfo.UserToken);
                                    findWordsPlayed.Parameters.AddWithValue("@Word", wordInfo.Word);

                                    using (SqlDataReader readWord = findWordsPlayed.ExecuteReader())
                                    {
                                        if (readWord.HasRows)
                                        {
                                            wordPoints = 0;
                                        }
                                        readWord.Close();
                                    }
                                    using (SqlCommand setWordInPlayed =
                                        new SqlCommand("insert into Words (Word, GameID, Player, Score) values(@Word, @GameID, @Player, @Score)",
                                                       conn,
                                                       trans))
                                    {
                                        setWordInPlayed.Parameters.AddWithValue("@Word", wordInfo.Word);
                                        setWordInPlayed.Parameters.AddWithValue("@GameID", gameID);
                                        setWordInPlayed.Parameters.AddWithValue("@Player", wordInfo.UserToken);
                                        setWordInPlayed.Parameters.AddWithValue("@Score", wordPoints);

                                        if (setWordInPlayed.ExecuteNonQuery() != 1)
                                        {
                                            throw new Exception("Query failed unexpectedly");
                                        }

                                        SetStatus(OK);
                                        score.Score = wordPoints;
                                        trans.Commit();
                                        return score;
                                    }
                                }
                            }
                            else
                            {
                                int wordPoints = 0;

                                //Check the word.
                                if (words.ContainsKey(wordInfo.Word))
                                {
                                    //Create the board to check if it can be formed.
                                    BoggleBoard board = new BoggleBoard(Board);

                                    //Can it be created on the board?
                                    if (board.CanBeFormed(wordInfo.Word))
                                    {
                                        //Get the score of the word.
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
                                        if (wordInfo.Word.Length < 3)
                                        {
                                            wordPoints = 0;
                                        }
                                        else
                                        {
                                            wordPoints = -1;
                                        }
                                    }
                                }
                                else
                                {
                                    //not a valid word in the dictionary.
                                    if (wordInfo.Word.Length < 3)
                                    {
                                        wordPoints = 0;
                                    }
                                    else
                                    {
                                        wordPoints = -1;
                                    }
                                }
                                //If the word has not already been played the add it and the score.
                                bool isInList = true;
                                using (SqlCommand findWordsPlayed =
                                        new SqlCommand("Select * from Words where GameID = @GameID AND Player = @Player AND Word = @Word",
                                        conn,
                                        trans))
                                {
                                    findWordsPlayed.Parameters.AddWithValue("@GameID", gameID);
                                    findWordsPlayed.Parameters.AddWithValue("@Player", wordInfo.UserToken);
                                    findWordsPlayed.Parameters.AddWithValue("@Word", wordInfo.Word);

                                    using (SqlDataReader readWord = findWordsPlayed.ExecuteReader())
                                    {
                                        if (readWord.HasRows)
                                        {
                                            wordPoints = 0;
                                        }
                                        isInList = false;
                                        readWord.Close();
                                    }
                                    using (SqlCommand setWordInPlayed =
                                        new SqlCommand("insert into Words (Word, GameID, Player, Score) values(@Word, @GameID, @Player, @Score)",
                                                       conn,
                                                       trans))
                                    {
                                        setWordInPlayed.Parameters.AddWithValue("@Word", wordInfo.Word);
                                        setWordInPlayed.Parameters.AddWithValue("@GameID", gameID);
                                        setWordInPlayed.Parameters.AddWithValue("@Player", wordInfo.UserToken);
                                        setWordInPlayed.Parameters.AddWithValue("@Score", wordPoints);

                                        if (setWordInPlayed.ExecuteNonQuery() != 1)
                                        {
                                            throw new Exception("Query failed unexpectedly");
                                        }

                                        SetStatus(OK);
                                        score.Score = wordPoints;
                                        trans.Commit();
                                        return score;
                                    }

                                }
                            }

                        }
     
                        

                        

                        

                    }


                }

                //Stub
                return null;
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
                    //database connection the DB is open while connected. When we get to the end
                    //The DB closes.
                    using (SqlConnection conn = new SqlConnection(BoggleDB))
                    {
                        //open the connection.
                        conn.Open();

                        //either all commands will succeed or all will be canceled.
                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            using (SqlCommand command =
                                new SqlCommand("insert into Users(UserID, Nickname) values (@UserID, @Nickname)",
                                conn, trans))
                            {
                                String userID = Guid.NewGuid().ToString();

                                //replace the placeholders
                                command.Parameters.AddWithValue("@UserID", userID);
                                command.Parameters.AddWithValue("@Nickname", user.Nickname.Trim());

                                //executes the command within the transaction over the connection. # rows modified is returned.
                                if (command.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("Query failed unexpectedly");
                                }
                                SetStatus(Created);
                                trans.Commit();
                                UserToke token = new UserToke();
                                token.UserToken = userID;
                                return token;
                            }
                        }
                    }


                        /////////*************************************************************???Propagate????

                    /*
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
                    return token;*/
               
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
