using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoggleClient
{
    class Controller
    {
        /// <summary>
        /// The token of the most recently registered user, or "0" if no user
        /// has ever registered
        /// </summary>
        private string userToken;

        /// <summary>
        /// for canceling the current opperation.
        /// </summary>
        private CancellationTokenSource tokenSource;


        /// <summary>
        /// Cancels the current operation (currently unimplemented)
        /// </summary>
        private void Cancel()
        {
            tokenSource.Cancel();
        }

        public async void Register(String domain, String nickname)
        {
            using (HttpClient client = CreateClient())
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
                    object temp = JsonConvert.DeserializeObject(result);
                  
                     userToken = (string)JsonConvert.DeserializeObject(result);
                    //userToken = (String) JsonConvert.DeserializeObject(result);
                    Console.WriteLine(userToken);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    //MessageBox.Show("Error registering: " + response.StatusCode + "\n" + response.ReasonPhrase);
                }

            }

        }


        private static HttpClient CreateClient()
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://ice.eng.utah.edu/BoggleService.svc/users");

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // There is more client configuration to do, depending on the request.
            return client;
        }
    }
}
