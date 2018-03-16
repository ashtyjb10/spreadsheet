using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
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
                        Console.WriteLine(userToken);
                        view.RegisteredUser();///*************************************
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
                using (HttpClient client = CreateClient( baseAddress, "games"))
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
                        Console.WriteLine(gameID);
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

        public async void GetGameStatus()
        {
            try
            {
                using (HttpClient client = CreateClient(baseAddress, "games/" + gameID))
                {
                    // tokenSource = new CancellationTokenSource();
                    StringContent content = new StringContent("application/json");
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

                        Console.WriteLine(gameState);


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
