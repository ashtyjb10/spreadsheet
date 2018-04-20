//created by Ashton Schmidt and Nathan Herrmann
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService
    {
        /* //store the users.
         private readonly static Dictionary<String, storedUserInfo> users = new Dictionary<String, storedUserInfo>();
         //store all of the games.
         private readonly static Dictionary<String, GameInfo> games = new Dictionary<String, GameInfo>();

         //What game number / id # we are on
         private static int countingID;
         //the new game Id generated
         private static string CurrentPendingGame;
         */
        //The Dictionary that stores the valid words from dictionary.txt.
        private readonly static Dictionary<String, String> words = new Dictionary<String, String>();
        //allow for thread manipulation.
        private static readonly object sync = new object();

        private static string BoggleDB;

        static BoggleService()
        {
            //the connection string is stored in the Web.config file where it can easily be found and changed.
            BoggleDB = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDBFilename = |DataDirectory|\\BoggleDB.mdf; Integrated Security = True";

            //countingID = 0;
            //CurrentPendingGame = CreateNewGameID();

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
                        //check to see if the userToken is in the users
                        string query = "SELECT UserID  FROM dbo.Users WHERE UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UserID", cancelInfo.UserToken);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                //if the userToken is not a valid token then close everything set status and return.
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    trans.Commit();
                                    return;
                                }
                            }
                        }

                        //get the most recent game which is the pending game
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
                                    //if our player1 token is not null then set it to Player1Tok
                                    if (reader["Player1"] != System.DBNull.Value)
                                    {
                                        player1Toke = (string)reader["Player1"];
                                    }
                                    //as long as the game status is not null we set the gameStatus to ensure
                                    //that a game actually contains values.
                                    if (reader["GameStatus"] != System.DBNull.Value)
                                    {
                                        gameStatus = (string)reader["GameStatus"];
                                    }
                                    //if the game we are in is not pending but active or completed we need set to forbidden
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
                                throw new Exception("Query failed unexpectedly");
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
                //variables set when we use the reader thrroughout the method.
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
                        //get the row of the correct game id.
                        string query = "Select * From dbo.Games Where GameID = @GameID";

                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@GameID", GameID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                //invalid gameID 
                                if (!reader.HasRows)
                                {
                                    SetStatus(Forbidden);
                                    reader.Close();
                                    return null;
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        gameStatus = (string)reader["GameStatus"];

                                        //if pending we just return the game status and set status to OK
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
                                            // extract all of the information we will use outside of the reader.
                                            player1 = (string)reader["Player1"];
                                            player2 = (string)reader["Player2"];
                                            board = (string)reader["Board"];
                                            timeLimit = (int)reader["TimeLimit"];

                                            //calculations to get the start time into an int, so that we can get the
                                            //current game time left later.
                                            DateTime startTime = reader.GetDateTime(5);
                                            int startInInt = (int)(startTime - new DateTime(1970, 1, 1)).TotalSeconds;
                                            startGame = startInInt;

                                        }
                                    }
                                    reader.Close();
                                }
                            }

                            //set the return object(gameInfo) 
                            gameInfo.GameState = gameStatus;
                            gameInfo.Board = board;
                            gameInfo.TimeLimit = (int)timeLimit;

                            // we will get the players information  below to add to the player object depending 
                            //on the status of the game.
                            gameInfo.Player1 = new Player1();
                            gameInfo.Player2 = new Player2();

                            //get the current time Left in the game.
                            int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                            timeLeft = gameInfo.TimeLimit + ((int)startGame - currentTime);
                        }


                        //get the correct players Tokens.
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

                        //get the nickname of the player1Token
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
                        //get the nickname of the player2Token.
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

                        //get all the words played by the player token and the game id to get the score,
                        // and the list of words played if the game is complete.
                        query = "SELECT * FROM words WHERE GameID = @GameID AND Player = @Player";
                        int player1Score = 0;
                        List<WordInfo> player1Words = new List<WordInfo>();
                        int player2Score = 0;
                        List<WordInfo> player2Words = new List<WordInfo>();


                        if (timeLeft <= 0)
                        {
                            //game completed so get the lists of words.

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
                                        //create and set a word object containg the word played,
                                        //and the score of that word, so that we can add the object to the
                                        //list of words played.
                                        WordInfo wordStats = new WordInfo();
                                        wordStats.Word = (string)reader["Word"];
                                        int score = (int)reader["Score"];
                                        wordStats.Score = score.ToString();
                                        //increment the total score.
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
                                        //create and set a word object containg the word played,
                                        //and the score of that word, so that we can add the object to the
                                        //list of words played.
                                        WordInfo wordStats = new WordInfo();
                                        wordStats.Word = (string)reader["Word"];
                                        int score = (int)reader["Score"];
                                        wordStats.Score = score.ToString();
                                        //increment the players total score.
                                        player2Score += score;
                                        player2Words.Add(wordStats);
                                    }
                                    reader.Close();

                                }
                            }

                            //remember game status is completed. so set all of the correct information.
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

                            //use the same query as above just with different values.
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player1);

                                //extract the total score from player 1.
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        player1Score += (int)reader["Score"];
                                    }
                                    reader.Close();
                                }
                            }
                            //use same query for player2
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@GameID", GameID);
                                cmd.Parameters.AddWithValue("@Player", player2);

                                //extract the total score for player2.
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        player2Score += (int)reader["Score"];
                                    }
                                    reader.Close();
                                }

                            }
                            //set the scores in the return objects.
                            gameInfo.Player1.Score = player1Score;
                            gameInfo.Player2.Score = player2Score;
                            gameInfo.TimeLeft = timeLeft;
                        }

                        SetStatus(OK);
                        trans.Commit();
                        return gameInfo;

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
                //look at getGameStats above on how this works. Only difference is what
                //is passed back.

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


                if (item.UserToken == null || item.TimeLimit < 5
                    || item.TimeLimit > 120)
                {
                    SetStatus(Forbidden);
                    return null;
                }
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
                                //if the DataBase doent have th userID.
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


                        //now we need to go to the latest Pending game.... and see which player we need to get.
                        string query = "SELECT TOP 1 * FROM dbo.Games ORDER BY GameID DESC";
                        //variables initialed below.
                        string player1Tok = null;
                        string player2Tok = null;
                        int gameID;


                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //as long as the values are not set to null set them!
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

                        //if both of the tokens are null then we are adding the player who desires to 
                        //play to be Player1.
                        if (player1Tok != null && player2Tok != null)
                        {

                            query = "INSERT INTO dbo.Games  (Player1, TimeLimit, GameStatus) VALUES(@Player1, @TimeLimit, @GameStatus)";
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                //set the time limit, Player1, and set the game status to be pending.
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
                                        //get the game id of the game!
                                        gameID = (int)reader["GameID"];
                                        returnGID.GameID = gameID.ToString();
                                    }
                                }
                                // finish everything and set to accepted.
                                SetStatus(Accepted);
                                trans.Commit();
                                return returnGID;
                            }
                        }
                        else
                        {
                            //We want to set player to and get the average of the two desired times.

                            //variables that we will use!
                            int player1Time = 0;
                            gameID = 0;
                            player1Tok = null;

                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    //get the time, gameid, and player1 token.
                                    while (reader.Read())
                                    {
                                        player1Time = (int)reader["TimeLimit"];
                                        gameID = (int)reader["GameID"];
                                        player1Tok = (string)reader["Player1"];

                                        //if player1s token is the same as the desired players token,
                                        //set to forbidden and return.
                                        if (item.UserToken == player1Tok)
                                        {
                                            SetStatus(Conflict);
                                            reader.Close();
                                            trans.Commit();
                                            return null;
                                        }
                                    }
                                }
                            }

                            //update the row information!
                            query = "UPDATE dbo.Games SET Player2 = @Player2, TimeLimit = @TimeLimit, StartTime = @StartTime," +
                                "Board = @Board, GameStatus = @GameStatus  WHERE GameID = @GameID";
                            using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                            {
                                BoggleBoard newBoard = new BoggleBoard();
                                //set values.
                                cmd.Parameters.AddWithValue("@GameID", gameID);
                                cmd.Parameters.AddWithValue("@Player2", item.UserToken);
                                //calculate the average time.
                                player1Time = (int)(player1Time + item.TimeLimit) / 2;
                                cmd.Parameters.AddWithValue("@TimeLimit", player1Time);
                                //current time game starts.
                                cmd.Parameters.AddWithValue("@StartTime", (DateTime.UtcNow));
                                //get a new string of letters on a boggle board.
                                cmd.Parameters.AddWithValue("@Board", newBoard.ToString());
                                //game is now active!
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
                                    //get gameID and set it.
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

                                //extract the information needed!
                                while (GameIdReader.Read())
                                {
                                    Player1 = GameIdReader["Player1"].ToString();
                                    Player2 = GameIdReader["Player2"].ToString();
                                    Board = GameIdReader["Board"].ToString();
                                    GameStatus = GameIdReader["GameStatus"].ToString();
                                    //word can only be played when it is active
                                    if (GameStatus != "active")
                                    {
                                        SetStatus(Conflict);
                                        GameIdReader.Close();
                                        trans.Commit();
                                        return score;
                                    }

                                    //get the time info!
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
                                        //if the same player has already played the same word in the same game
                                        if (readWord.HasRows)
                                        {
                                            wordPoints = 0;
                                        }
                                        readWord.Close();
                                    }

                                    //add the word and all of the information to the database.
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
                                //Player two played the word.

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
                                        //has the word been played?
                                        if (readWord.HasRows)
                                        {
                                            wordPoints = 0;
                                        }
                                        readWord.Close();
                                    }
                                    // add the word and word info into the Database.
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
                }
            }
        }
    }
}

