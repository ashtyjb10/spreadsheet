using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public Controller(IAnalysisView view)
        {
            this.view = view;
            userToken = "0";
            view.RegisterUser += Register;
            view.DesiredGameDuration += JoinGame;
        }

        /// <summary>
        /// Cancels the current operation (currently unimplemented)
        /// </summary>
        private void Cancel()
        {
            tokenSource.Cancel();
        }

        public async void Register(String domain, String nickname)
        {
            baseAddress = domain + "/BoggleService.svc/";
            try
            {
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
                        view.RegisteredUser();///******************
                    }
                    else
                    {
                        MessageBox.Show("Error registering: " + response.StatusCode + "\n" + response.ReasonPhrase);
                    }
                }
            }
            finally
            {
               // view.EnableControls(true);
            }
        }

        public async void JoinGame(String gameDuration)
        {
            try
            {
                using (HttpClient client = CreateClient( baseAddress, ""))
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
                    }
                    else
                    {
                        MessageBox.Show("Error Joining Game " + response.StatusCode + "\n" + response.ReasonPhrase);
                    }


                }

            }
            finally
            {

            }
        }

        public async void PlayWord(String word)
        {
            try
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
                        Console.WriteLine(WordScore);
                    }
                    else
                    {
                        MessageBox.Show("Error playing word " + response.StatusCode + "\n" + response.ReasonPhrase);
                    }
                }
            }
            finally
            {

            }
        }

        public async void GetGameStatus()
        {
            try
            {
                using (HttpClient client = CreateClient(baseAddress, ""))
                {

                    tokenSource = new CancellationTokenSource();

                    //StringContent content = new StringContent(JsonConvert.SerializeObject(joinGameInfo), Encoding.UTF8, "application/json");
                    //HttpResponseMessage response = await client.GetAsync("games/" + gameID);
                    HttpResponseMessage response = await client.GetAsync("games/G2067"); //*****************************************************  add + gameID



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


                                if (gameState == "active")
                                {
                                    dynamic player2 = deserialized.Player2;
                                    p2Nickname = player2.Nickname;
                                    p2Score = player2.Score;

                                    //change the game to active!
                                }
                                else if (gameState == "completed")
                                {
                                    var wordsPlayedP1 =  player1.WordsPlayed;
                                    foreach (var obj in wordsPlayedP1)
                                    {
                                        string wordScore = obj.Word + "... ";
                                        wordScore += obj.Score;
                                        wordsFromP1.Add(wordScore);
                                    }
                                    dynamic player2 = deserialized.Player2;
                                    p2Nickname = player2.Nickname;
                                    p2Score = player2.score;
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

                        updateBoardLong();
                
                    }
                    else
                    {
                        MessageBox.Show("Error getting game info " + response.StatusCode + "\n" + response.ReasonPhrase);

                    }

                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Updates all the information in the window with what was taken from the server.
        /// </summary>
        private void updateBoardLong()
        {
            //Update Board 
            view.setBoard(gameBoard.ToArray());
            
            //Update player Names
            
            //Update Player Scores
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
