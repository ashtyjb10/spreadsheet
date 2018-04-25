using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CustomNetworking;
using Newtonsoft.Json;

namespace Boggle
{
    class Program
    {


        /// <summary>
        /// The main method to get everything going. creates a new server and opens a socket, and begins to listen
        /// for incoming calls.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //BoggleService serv = new BoggleService();
            SSListener server = new SSListener(60000, Encoding.UTF8); 
            server.Start();
            server.BeginAcceptSS(ConnectionMade, server);
            Console.ReadLine();

           HttpStatusCode status;
            Console.ReadLine();
        }
        /// <summary>
        /// called when a connection is made and sets the listener to the payload and begins accepting input from the server.
        /// And then creates a request handler.
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="payload"></param>
        private static void ConnectionMade(SS ss, object payload)
        {
            SSListener server = (SSListener)payload;
            server.BeginAcceptSS(ConnectionMade, server);
            new RequestHandler(ss);
        }


        /// <summary>
        /// The Request Handler deals with the incoming lines of code and takes it appart so that we cam get all of the info that
        /// we need that was included in the lines we recieve from the string socket/caller.
        /// </summary>
        private class RequestHandler
        {
            //initialize variables.
            private SS ss;
            private string firstLn;
            private int length;
            //regex for registering a user.
            private static readonly Regex RegisterPatt = new Regex(@"^POST /BoggleService.svc/users HTTP");
            //regex for joining a game.
            private static readonly Regex JoinPattern = new Regex(@"^POST /BoggleService.svc/games HTTP");
            //regex for cancel a game.
            private static readonly Regex CancelJoinPattern = new Regex(@"^PUT /BoggleService.svc/games HTTP");
            //regex for play word.
            private static readonly Regex PlayWordPattern = new Regex(@"^PUT /BoggleService.svc/games/(\d+) HTTP");
            //regex for game stats brief=yes
            private static readonly Regex GameStatusBrfYPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+)\?(brief=yes) HTTP");
            //regex for game stats brief=no
            private static readonly Regex GameStatusBrfNPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+)\?brief=no HTTP");
            //regex for game stats if brief was not initialized in the call.
            private static readonly Regex GameStatusPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+) HTTP");
            //regex for the lengeth of the incoming information from the caller/ss
            private static readonly Regex contentLengthPattern = new Regex(@"^content-length: (\d+)", RegexOptions.IgnoreCase);
           

            /// <summary>
            /// sets a new ss from the parameter, initialized the variables and beings to recieve lines for 
            /// computing by calling readlines.
            /// </summary>
            /// <param name="ss"></param>
            public RequestHandler(SS ss)
            {
                this.ss = ss;
                length = 0;
                ss.BeginReceive(ReadLines, null);
            }

            /// <summary>
            /// gets the lines/ input that is passed in from the ss, along with the payload. It then begins to seperate the data
            /// from the lines and get the first line, which tells us which rest call to make, and then we want the length of the content,
            /// lastly we want the content so that we can process the rest call. All other info can be disreagarded.
            /// </summary>
            /// <param name="lines"></param>
            /// <param name="pay"></param>
            private void ReadLines(String lines, object pay)
            {
                //if we have nothing we want more and with the new info we want to pass that into the process request.
                if (lines.Trim().Length == 0 && length > 0)
                {
                    ss.BeginReceive(ProcessRequest, null, length);
                }
                //if there is nothing else just process nothing.
                else if (lines.Trim().Length == 0)
                {
                    ProcessRequest(null);
                }
                //if we have the first line then we want to try and extract the length of the info so that we know how
                //much data to ask for when calling the ProcessRequest.
                else if (firstLn != null)
                {
                    Match m = contentLengthPattern.Match(lines);
                    if (m.Success)
                    {
                        length = int.Parse(m.Groups[1].ToString());
                    }
                    ss.BeginReceive(ReadLines, null);
                }
                //set the first line so we know what rest call is being done.
                else
                {
                    firstLn = lines;
                    ss.BeginReceive(ReadLines, null);
                }
            }


            /// <summary>
            /// we want to compare the firstLn to all of the regexes and see which rest call that we are going to be using.
            /// Depening on the rest call we will extract the data from the Json object, call the method from the BoggleService,
            /// recieve the status codes and then create the headers containing the status codes,the length of the serialized object
            /// we are sending, and the serialized object from the BoggleService. Then send back all the info and shut down that socket.
            /// </summary>
            /// <param name="line"></param>
            /// <param name="pay"></param>
            private void ProcessRequest(string line, object pay = null)
            {
                //register
                if (RegisterPatt.IsMatch(firstLn))
                {
                    //get the object from the serialized Json line.
                    UserInfo nickName = JsonConvert.DeserializeObject<UserInfo>(line);
                    //call the method from boggleservice, and initialize the correct object.
                    UserToke token = new BoggleService().Register(nickName, out HttpStatusCode status);

                    //create header
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    //if we have a successful status set that.
                    if ((int)status / 100 == 2)
                    {
                        string res = JsonConvert.SerializeObject(token);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }
                    //close socket/
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                //joingame ****** see register for implemenation.
                else if (JoinPattern.IsMatch(firstLn))
                {
                    JoinGameInfo newInfo = JsonConvert.DeserializeObject<JoinGameInfo>(line);
                    UserGame game = new BoggleService().joinGame(newInfo, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    if ((int)status == 201 || (int)status == 202)
                    {
                        string res = JsonConvert.SerializeObject(game);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                //cancel join game ****** see register for implemenation.
                else if (CancelJoinPattern.IsMatch(firstLn))
                {
                    UserCancel token = JsonConvert.DeserializeObject<UserCancel>(line);
                    new BoggleService().cancelGame(token, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);

                }
                //play word ****** see register for implemenation.
                else if (PlayWordPattern.IsMatch(firstLn))
                {
                    Match match = PlayWordPattern.Match(firstLn);
                    string gameid = "";
                    if (match.Success)
                    {
                        gameid = match.Groups[1].Value;
                    }
                    WordToPlay wordInfo = JsonConvert.DeserializeObject<WordToPlay>(line);
                    WordScore score = new BoggleService().playWord(wordInfo, gameid, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(score);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                //game status Brief = y ****** see register for implemenation.
                else if (GameStatusBrfYPattern.IsMatch(firstLn))
                {
                    Match match = GameStatusBrfYPattern.Match(firstLn);
                    string gameid = "";
                    string brief = "";
                    if (match.Success)
                    {
                        gameid = match.Groups[1].Value;
                        brief = match.Groups[2].Value; //brief should only be yes!

                    }

                    FullGameInfo gameInfo = new BoggleService().getGameStatsBrief(gameid, out HttpStatusCode status);

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }

                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);

                }
                // ****** see register for implemenation.
                else if (GameStatusBrfNPattern.IsMatch(firstLn))
                {
                    Match match = GameStatusBrfNPattern.Match(firstLn);
                    string gameid = "";
                    if(match.Success)
                    {
                        gameid = match.Groups[1].Value;
                    }
                    FullGameInfo gameInfo = new BoggleService().getGameStats(gameid, out HttpStatusCode status);

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                else
                {
                    //it is just a regular game status. ****** see register for implemenation.
                    Match match = GameStatusPattern.Match(firstLn);
                    string gameid = "";
                    if (match.Success)
                    {
                        gameid = match.Groups[1].Value;
                    }

                    FullGameInfo gameInfo = new BoggleService().getGameStats(gameid, out HttpStatusCode status);

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    else
                    {
                        result += "\r\n";
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);


                }

            }
        }
    }
}
