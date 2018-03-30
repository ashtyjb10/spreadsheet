using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.HttpStatusCode;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using System.Dynamic;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Boggle
{
    /// <summary>
    /// Provides a way to start and stop the IIS web server from within the test
    /// cases.  If something prevents the test cases from stopping the web server,
    /// subsequent tests may not work properly until the stray process is killed
    /// manually.
    /// </summary>
    public static class IISAgent
    {
        // Reference to the running process
        private static Process process = null;
        

        /// <summary>
        /// Starts IIS
        /// </summary>
        public static void Start(string arguments)
        {
            if (process == null)
            {
                ProcessStartInfo info = new ProcessStartInfo(Properties.Resources.IIS_EXECUTABLE, arguments);
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = false;
                process = Process.Start(info);

                


            }
        }

        /// <summary>
        ///  Stops IIS
        /// </summary>
        public static void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }
    }
    [TestClass]
    public class BoggleTests
    {

        private static Dictionary<String, String> dictionary = new Dictionary<string, string>();
        /// <summary>
        /// This is automatically run prior to all the tests to start the server
        /// </summary>
        [ClassInitialize()]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start(@"/site:""BoggleService"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""");

            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Remove(path.Length - 10, 10);
            StreamReader reader = new StreamReader(path + "/dictionary.txt");
            while (reader.Peek() > -1)
            {
                string wordToAdd = reader.ReadLine();

                if (wordToAdd.Length < 4)
                { 
                    dictionary.Add(wordToAdd, wordToAdd);
                }
            }
            reader.Close();
        }

        /// <summary>
        /// This is automatically run when all tests have completed to stop the server
        /// </summary>
        [ClassCleanup()]
        public static void StopIIS()
        {
            IISAgent.Stop();
        }

        private RestTestClient client = new RestTestClient("http://localhost:60000/BoggleService.svc/");

        [TestMethod]
        public void TestRegisterValidUser()
        {
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor";
            
            Response r = client.DoPostAsync("users", users).Result;
            Assert.AreEqual(Created, r.Status);

            users.Nickname = null;
            r = client.DoPostAsync("users", users).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestJoinGame()
        {
            //Create first user.
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor1";
            Response r = client.DoPostAsync("users", users).Result;
            dynamic userOne = new ExpandoObject();
            userOne.UserToken = r.Data.UserToken;
            userOne.TimeLimit = "25";

            //Create second user.
            users.Nickname = "Nathor2";
            Response r2 = client.DoPostAsync("users", users).Result;
            dynamic userTwo = new ExpandoObject();
            userTwo.UserToken = r2.Data.UserToken;
            userTwo.TimeLimit = "0";
            
            //User two with wrong time limit.
            r = client.DoPostAsync("games", userTwo).Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Set time limit properly.
            userTwo.TimeLimit = "25";

            //First user enters game.
            r = client.DoPostAsync("games", userOne).Result;
            Assert.AreEqual(Accepted, r.Status);
            //PlayerOne enters the game again.
            r = client.DoPostAsync("games", userOne).Result;
            Assert.AreEqual(Conflict, r.Status);
            //Second player joins the game.
            r = client.DoPostAsync("games", userTwo).Result;
            Assert.AreEqual(Created, r.Status);
        }

        [TestMethod]
        public void TestCancelJoinRequest()
        {
            //Create and register playerOne
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor3";
            Response r = client.DoPostAsync("users", users).Result;
            dynamic playerOne = new ExpandoObject();
            playerOne.UserToken = r.Data.UserToken;
            playerOne.TimeLimit = "25";

            //Create second user.
            users.Nickname = "Nathor4";
            Response r2 = client.DoPostAsync("users", users).Result;
            dynamic userTwo = new ExpandoObject();
            userTwo.UserToken = r2.Data.UserToken;
            userTwo.TimeLimit = "0";


            //First user enters game.
            r = client.DoPostAsync("games", playerOne).Result;
            Assert.AreEqual(Accepted, r.Status);

            //Second player cancels the game
            r = client.DoPutAsync(userTwo, "games").Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Player One cancels the game.
            Response rOK = client.DoPutAsync(playerOne, "games").Result;
            Assert.AreEqual(OK, rOK.Status);
        }

        [TestMethod]
        public void TestPlayWord()
        {
            //Create and register playerOne
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor5";
            Response r = client.DoPostAsync("users", users).Result;
            dynamic playerOne = new ExpandoObject();
            playerOne.UserToken = r.Data.UserToken;
            playerOne.TimeLimit = "120";

            //Create and register playerTwo
            dynamic users2 = new ExpandoObject();
            users.Nickname = "Nathor6";
            Response r2 = client.DoPostAsync("users", users).Result;
            dynamic playerTwo = new ExpandoObject();
            playerTwo.UserToken = r2.Data.UserToken;
            playerTwo.TimeLimit = "120";

            //First user enters game.
            Response rGameID = client.DoPostAsync("games", playerOne).Result;

            //Player one plays when game is not active
            playerOne.Word = "A";
            r = client.DoPutAsync(playerOne, "games/" + rGameID.Data.GameID).Result;
            Assert.AreEqual(Conflict, r.Status);

            //Player two plays when not in the game.
            playerTwo.Word = "A";
            r = client.DoPutAsync(playerTwo, "games/" + rGameID.Data.GameID).Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Second user enters game.
            r = client.DoPostAsync("games", playerTwo).Result;

            //word played is too long.
            playerOne.Word = "abcdefghijklmnopqrstuvwxyznowiknowmyabcnexttimechooseashorterstring";
            r = client.DoPutAsync(playerOne,"games/" + rGameID.Data.GameID).Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Player one plays a single word.
            playerOne.Word = "a";
            r = client.DoPutAsync(playerOne, "games/" + rGameID.Data.GameID).Result;
            Assert.AreEqual(OK, r.Status);

            //Trys to play a series of words.
            foreach(string word in dictionary.Keys)
            {
                playerOne.Word = word;
                playerTwo.Word = word;

                r = client.DoPutAsync(playerOne, "games/" + rGameID.Data.GameID).Result;
                Assert.AreEqual(OK, r.Status);
                r2 = client.DoPutAsync(playerTwo, "games/" + rGameID.Data.GameID).Result;
                Assert.AreEqual(OK, r2.Status);

            }
        }

        [TestMethod]
        public void TestGameStatus()
        {
            //Create and register playerOne
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor7";
            Response r = client.DoPostAsync("users", users).Result;
            dynamic playerOne = new ExpandoObject();
            playerOne.UserToken = r.Data.UserToken;
            playerOne.TimeLimit = "5";

            //Create and register playerTwo
            dynamic users2 = new ExpandoObject();
            users.Nickname = "Nathor8";
            Response r2 = client.DoPostAsync("users", users).Result;
            dynamic playerTwo = new ExpandoObject();
            playerTwo.UserToken = r2.Data.UserToken;
            playerTwo.TimeLimit = "5";

            //First user enters game.
            Response rGameID = client.DoPostAsync("games", playerOne).Result;

            //Fake GameID is made
            r2.Data.GameID = "54";

            //Fake ID is used against the service
            r = client.DoGetAsync("games/" + r2.Data.GameID, r2.Data.GameID.ToString()).Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Real GameID is used against the service in pending status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID, rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);

            //Player2 enters the game
            r2 = client.DoPostAsync("games",playerTwo).Result;

            System.Threading.Thread.Sleep(6000);

            //Player one plays a single word.
            playerOne.Word = "a";
            r = client.DoPutAsync(playerOne, "games/" + rGameID.Data.GameID).Result;

            //Player two plays a single word.
            playerTwo.Word = "a";
            r = client.DoPutAsync(playerTwo, "games/" + rGameID.Data.GameID).Result;
            

            //Real GameID is used against the service in active status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID, rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);
            
            //Real GameID is used against the service in complete status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID, rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);

        }

        [TestMethod]
        public void TestGameStatusBrief()
        {
            //Create and register playerOne
            dynamic users = new ExpandoObject();
            users.Nickname = "Nathor9";
            Response r = client.DoPostAsync("users", users).Result;
            dynamic playerOne = new ExpandoObject();
            playerOne.UserToken = r.Data.UserToken;
            playerOne.TimeLimit = "5";

            //Create and register playerTwo
            dynamic users2 = new ExpandoObject();
            users.Nickname = "Nathor10";
            Response r2 = client.DoPostAsync("users", users).Result;
            dynamic playerTwo = new ExpandoObject();
            playerTwo.UserToken = r2.Data.UserToken;
            playerTwo.TimeLimit = "5";

            //First user enters game.
            Response rGameID = client.DoPostAsync("games", playerOne).Result;

            //Fake GameID is made
            r2.Data.GameID = "5566";

            //Fake ID is used against the service
            r = client.DoGetAsync("games/" + r2.Data.GameID + "?Brief=yes").Result;
            Assert.AreEqual(Forbidden, r.Status);

            //Real GameID is used against the service in pending status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID + "?Brief=yes", rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);

            //Player2 enters the game
            r2 = client.DoPostAsync("games", playerTwo).Result;

            //Player one plays a single word.
            playerOne.Word = "a";
            r = client.DoPutAsync(playerOne, "games/" + rGameID.Data.GameID).Result;

            //Player two plays a single word.
            playerTwo.Word = "a";
            r = client.DoPutAsync(playerTwo, "games/" + rGameID.Data.GameID).Result;

            System.Threading.Thread.Sleep(6000);

            //Real GameID is used against the service in active status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID + "?Brief=yes", rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);

            //Real GameID is used against the service in complete status
            r = client.DoGetAsync("games/" + rGameID.Data.GameID + "?Brief=yes", rGameID.Data.GameID.ToString()).Result;
            Assert.AreEqual(OK, r.Status);
        }
    }
}