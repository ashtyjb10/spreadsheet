using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BoggleClient
{
    class Controller
    {
        private IAnalysisView view;
        private string baseAddress;
       
        /// <summary>
        /// The token of the most recently registered user, or "0" if no user
        /// has ever registered
        /// </summary>
        private string userToken;
        private string gameID;
        private string clientNickname;
        private string gameState;
        private string gameBoard;
        private bool brief = false;
        private string timeLimit;
        private string timeLeft;
        private string p1Nickname;
        private string p1Score;
        private string p2Nickname;
        private string p2Score;
        private HashSet<string> wordsFromP1 = new HashSet<string>();
        private HashSet<string> wordsFromP2 = new HashSet<string>();

        /// <summary>
        /// for canceling the current opperation.
        /// </summary>
        private CancellationTokenSource tokenSource;

        /// <summary>
        /// Constructor creates the handles for events.
        /// </summary>
        /// <param name="view"></param>
        public Controller(IAnalysisView view)
        {
            this.view = view;
            userToken = "0";
            view.RegisterUser += Register;
            view.DesiredGameDuration += JoinGame;
            view.ScoreWord += PlayWord;
            view.TickingTimer += HandleTickingTimer;
            view.CancelJoinGame += HandleCancelJoin;
            view.QuitGameClicked += HandleQuitGameClicked;
        }

        private void HandleQuitGameClicked()
        {
            NewGameReset();
        }

        private void NewGameReset()
        {
            p1Nickname = "Player 1";
            p2Nickname = "Player 2";
            p1Score = "0";
            p2Score = "0";
            timeLeft = "000";
            gameState = null;
            gameBoard = "----------------";
           
        }

        /// <summary>
        /// Handles the timer ticking to check the server.
        /// </summary>
        private void HandleTickingTimer()
        {
            GetGameStatus();
        }

        /// <summary>
        /// Cancels the current operation (currently unimplemented)
        /// </summary>
        private void Cancel()
        {
            tokenSource.Cancel();
        }

        /// <summary>
        /// Registers a user with the server given a domain name and a valid nickname.  A user token is returned
        /// and used to identify the user.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="nickname"></param>
        public async void Register(String domain, String nickname)
        {
            clientNickname = nickname;
            baseAddress = domain + "/BoggleService.svc/";
            using (HttpClient client = CreateClient(baseAddress, "users"))
            {
                dynamic users = new ExpandoObject();
                users.Nickname = nickname;

                //cancel token
                tokenSource = new CancellationTokenSource();
                StringContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("users", content, tokenSource.Token);

                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic deserialized = JsonConvert.DeserializeObject<object>(result);
                    userToken = deserialized.UserToken;
                    view.IsRegisteredUser = true;
                    view.RegisteredUser();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Error: 404 not found try another domain name.", "Not Found", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Error registering Username either null or longer than 50 characters: ", "Registraion Error",
                            MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        /// Joins a pending game on the server using information passed in by the user of nickname, and game duration.
        /// Clears all words from both players.  A game ID is given to be used to update the game status.
        /// </summary>
        /// <param name="gameDuration"></param>
        public async void JoinGame(String gameDuration)
        {

            using (HttpClient client = CreateClient(baseAddress, ""))
            {
                dynamic joinGameInfo = new ExpandoObject();
                joinGameInfo.UserToken = userToken;
                joinGameInfo.TimeLimit = gameDuration;

                //cancel token
                tokenSource = new CancellationTokenSource();
                StringContent content = new StringContent(JsonConvert.SerializeObject(joinGameInfo), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("games", content, tokenSource.Token);

                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic deserialized = JsonConvert.DeserializeObject<object>(result);
                    gameID = deserialized.GameID;
                    //Console.WriteLine(gameID);
                    GetGameStatus();
                    view.GameJoined();
                    wordsFromP1 = new HashSet<string>();
                    wordsFromP2 = new HashSet<string>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        view.timerEnabled = false;
                        MessageBox.Show("Error: please provide a time within the parameters 4< parameter > 121",
                            "Invalid Time", MessageBoxButtons.OK);

                    }
                    else
                    {
                        view.timerEnabled = false;
                        view.InvalidUserToken();
                        MessageBox.Show("Error: User Token Invalid please sign in again.", "Invalid User Token",
                            MessageBoxButtons.OK);
                    }
                    //403 forbidden.  Time limit is bad.  409 conflict, usertoken is already a player in a pending game.
                    //view.timerEnabled = false;

                    //if 403 show bad time
                    // MessageBox.Show("Error Joining Game " + response.StatusCode + "\n" + response.ReasonPhrase);
                    //if 409 send back to reg window.

                }
            }
        }

        public async void HandleCancelJoin()
        {
            using (HttpClient client = CreateClient(baseAddress, ""))
            {
                dynamic characteristics = new ExpandoObject();
                characteristics.UserToken = userToken;

                StringContent content = new StringContent(JsonConvert.SerializeObject(characteristics), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("games", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cancel Game Succeeded!", "Cancel Game", MessageBoxButtons.OK);
                    view.JoinGameCanceled();
                }
            }

        }

        /// <summary>
        /// Submits a word from the user to the server.  When the word is submitted, a score for the word is passed back and
        /// the score of the player is updated.
        /// </summary>
        /// <param name="word"></param>
        public async void PlayWord(String word)
        {

            using (HttpClient client = CreateClient(baseAddress, ""))
            {
                dynamic characteristics = new ExpandoObject();
                characteristics.UserToken = userToken;
                characteristics.Word = word;

                //tokenSource = new CancellationTokenSource();

                StringContent content = new StringContent(JsonConvert.SerializeObject(characteristics), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("games/" + gameID, content);


                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic deserialized = JsonConvert.DeserializeObject<object>(result);
                    string WordScore = deserialized.Word;
                    GetGameStatus();
                    UpdateBoardLong();
                    Console.WriteLine(WordScore);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        GetGameStatus();
                    }
                    //MessageBox.Show("Error playing word " + response.StatusCode + "\n" + response.ReasonPhrase);
                }
            }

        }

        /// <summary>
        /// Used to update the status of the game.  Status inclides time remaining, player scores, and state of the game.
        /// Calles the update method for the board after all the information is received from the server.
        /// </summary>
        public async void GetGameStatus()
        {

            using (HttpClient client = CreateClient(baseAddress, ""))
            {
                tokenSource = new CancellationTokenSource();


                HttpResponseMessage response = await client.GetAsync("games/" + gameID);

                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic deserialized = JsonConvert.DeserializeObject<object>(result);

                    gameState = deserialized.GameState;
                    if (gameState == "pending")
                    {
                        view.ViewPendingBox(true);

                    }
                    else
                    {
                        //game board is not pending, either active or completed.
                        if (!brief)
                        {
                            gameBoard = deserialized.Board;
                            timeLimit = deserialized.TimeLimit;
                            timeLeft = deserialized.TimeLeft;


                            dynamic player1 = deserialized.Player1;
                            p1Nickname = player1.Nickname;
                            p1Score = player1.Score;

                            //If the game is active, the scores of the players is updates.
                            if (gameState == "active")
                            {
                                view.ViewPendingBox(false);
                                view.ViewActiveBox(true);
                                dynamic player2 = deserialized.Player2;
                                p2Nickname = player2.Nickname;
                                p2Score = player2.Score;


                                //change the game to active!
                            }
                            //If the game is complete, the words of both players is displayed.
                            else if (gameState == "completed")
                            {
                                Console.WriteLine("Complete");
                                view.ViewActiveBox(false);
                                view.ViewCompletedBox(true);
                                var wordsPlayedP1 = player1.WordsPlayed;
                                foreach (var obj in wordsPlayedP1)
                                {
                                    string wordScore = obj.Word + "... ";
                                    wordScore += obj.Score;
                                    wordsFromP1.Add(wordScore);


                                }
                                dynamic player2 = deserialized.Player2;
                                p2Nickname = player2.Nickname;
                                p2Score = player2.Score;
                                var wordsPlayedP2 = player2.WordsPlayed;
                                foreach (var obj in wordsPlayedP2)
                                {
                                    string wordScore = obj.Word + "... ";
                                    wordScore += obj.Score;
                                    wordsFromP2.Add(wordScore);
                                }
                            }
                        }
                        else
                        {
                            //time left, player1 (score), player2(score)
                            timeLeft = deserialized.TimeLeft;
                            p1Score = deserialized.Score;
                            p2Score = deserialized.Score;
                        }
                    }
                    //Ensures that the board is updated with each call.
                    UpdateBoardLong();
                }
                else
                {
                    view.timerEnabled = false;
                    MessageBox.Show("Game Id Invalid please quit and join a new game!", "Game Id Invalid", MessageBoxButtons.OK);
                    view.GameIdInvalid();

                }
            }

        }

        /// <summary>
        /// Updates all the information in the window with what was taken from the server.
        /// </summary>
        private void UpdateBoardLong()
        {
            //Update Board if not just pending.
            if(gameBoard != null)
            {
                view.SetBoard(gameBoard.ToArray());
            }

            //Update player Names
            view.setUserNames(p1Nickname, p2Nickname);

            //Update Player Scores
            view.setScores(p1Score, p2Score);

            //Update time left
            view.setTime(timeLeft);

            //Update words played
            view.setPlayer2WordsPlayed(wordsFromP2);
            view.setPlayer1WordsPlayed(wordsFromP1);
        }

        private static HttpClient CreateClient(string baseAddress, string end)
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            //string baseAddress = "http://ice.eng.utah.edu/BoggleService.svc/";
            client.BaseAddress = new Uri(baseAddress + end);

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // There is more client configuration to do, depending on the request.
            return client;
        }
    }
}
