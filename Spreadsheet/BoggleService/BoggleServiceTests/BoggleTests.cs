using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.HttpStatusCode;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using System.Dynamic;
using System.Text;

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
        /// <summary>
        /// This is automatically run prior to all the tests to start the server
        /// </summary>
        [ClassInitialize()]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start(@"/site:""BoggleService"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""");
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

        /// <summary>
        /// Note that DoGetAsync (and the other similar methods) returns a Response object, which contains
        /// the response Stats and the deserialized JSON response (if any).  See RestTestClient.cs
        /// for details.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            Response r = client.DoGetAsync("word?index={0}", "-5").Result;
            Assert.AreEqual(Forbidden, r.Status);

            r = client.DoGetAsync("word?index={0}", "5").Result;
            Assert.AreEqual(OK, r.Status);

            string word = (string) r.Data;
            Assert.AreEqual("AAL", word);
        }

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

            Response rOK = client.DoPutAsync(playerOne, "games").Result;
            Assert.AreEqual(OK, rOK.Status);

            r = client.DoPostAsync("games", playerOne).Result;
            Assert.AreEqual(Accepted, r.Status);






        }
    }
}