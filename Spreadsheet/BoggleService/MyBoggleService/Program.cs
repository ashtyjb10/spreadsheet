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


        //private SSCallback ConnectionMade;
        static void Main(string[] args)
        {
            //BoggleService serv = new BoggleService();
            SSListener server = new SSListener(60000, Encoding.UTF8); 
            server.Start();
            server.BeginAcceptSS(ConnectionMade, server);
            Console.ReadLine();


             // fix the dictionary

            
           HttpStatusCode status;
           /* UserInfo name = new UserInfo { Nickname = "Joe" };
            BoggleService service = new BoggleService();
            UserToke user = service.Register(name, out status);
            Console.WriteLine(user.UserToken);
            Console.WriteLine(status.ToString());*/

            // This is our way of preventing the main thread from
            // exiting while the server is in use
            Console.ReadLine();
        }

        private static void ConnectionMade(SS ss, object payload)
        {
            SSListener server = (SSListener)payload;
            server.BeginAcceptSS(ConnectionMade, server);
            new RequestHandler(ss);
        }

        private class RequestHandler
        {
            private SS ss;
            private string firstLn;
            private int length;
            private static readonly Regex RegisterPatt = new Regex(@"^POST /BoggleService.svc/users HTTP");
            private static readonly Regex JoinPattern = new Regex(@"^POST /BoggleService.svc/games HTTP");
            private static readonly Regex CancelJoinPattern = new Regex(@"^PUT /BoggleService.svc/games HTTP");
            private static readonly Regex PlayWordPattern = new Regex(@"^PUT /BoggleService.svc/games/(\d+) HTTP");
            private static readonly Regex GameStatusBrfYPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+)/(Brief=yes) HTTP");
            private static readonly Regex GameStatusBrfNPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+)/Brief=no HTTP");
            private static readonly Regex GameStatusPattern = new Regex(@"^GET /BoggleService.svc/games/(\d+) HTTP");
            private static readonly Regex contentLengthPattern = new Regex(@"^content-length: (\d+)", RegexOptions.IgnoreCase);
            ///
            /// </summary>
            /// <param name="ss"></param>

            public RequestHandler(SS ss)
            {
                this.ss = ss;
                length = 0;
                ss.BeginReceive(ReadLines, null);
            }


            private void ReadLines(String lines, object pay)
            {
                if (lines.Trim().Length == 0 && length > 0)
                {
                    ss.BeginReceive(ProcessRequest, null, length);
                }
                else if (lines.Trim().Length == 0)
                {
                    ProcessRequest(null);
                }
                else if (firstLn != null)
                {
                    Match m = contentLengthPattern.Match(lines);
                    if (m.Success)
                    {
                        length = int.Parse(m.Groups[1].ToString());
                    }
                    ss.BeginReceive(ReadLines, null);
                }
                else
                {
                    firstLn = lines;
                    ss.BeginReceive(ReadLines, null);
                }
            }

            private void ProcessRequest(string line, object pay = null)
            {
                if (RegisterPatt.IsMatch(firstLn))
                {

                    UserInfo nickName = JsonConvert.DeserializeObject<UserInfo>(line);
                    UserToke token = new BoggleService().Register(nickName, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status / 100 == 2)
                    {
                        string res = JsonConvert.SerializeObject(token);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                else if (JoinPattern.IsMatch(firstLn))
                {
                    JoinGameInfo newInfo = JsonConvert.DeserializeObject<JoinGameInfo>(line);
                    UserGame game = new BoggleService().joinGame(newInfo, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status == 201 || (int)status == 202)
                    {
                        string res = JsonConvert.SerializeObject(game);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                else if (CancelJoinPattern.IsMatch(firstLn))
                {
                    UserCancel token = JsonConvert.DeserializeObject<UserCancel>(line);
                    new BoggleService().cancelGame(token, out HttpStatusCode status);
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);

                }
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
                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(score);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
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

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);

                }
                else if (GameStatusBrfNPattern.IsMatch(firstLn))
                {
                    Match match = GameStatusBrfNPattern.Match(firstLn);
                    string gameid = "";
                    if(match.Success)
                    {
                        gameid = match.Groups[1].Value;
                    }
                    FullGameInfo gameInfo = new BoggleService().getGameStats(gameid, out HttpStatusCode status);

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);
                }
                else
                {
                    //it is just a regular game status.
                    Match match = GameStatusPattern.Match(firstLn);
                    string gameid = "";
                    if (match.Success)
                    {
                        gameid = match.Groups[1].Value;
                    }

                    FullGameInfo gameInfo = new BoggleService().getGameStats(gameid, out HttpStatusCode status);

                    string result = "HTTP/1.1 " + (int)status + " " + status + "\r\n\r\n";

                    if ((int)status == 200)
                    {
                        string res = JsonConvert.SerializeObject(gameInfo);
                        result += "Content-Length: " + Encoding.UTF8.GetByteCount(res) + "\r\n";
                        result += res;
                    }
                    ss.BeginSend(result, (x, y) => { ss.Shutdown(System.Net.Sockets.SocketShutdown.Both); }, null);


                }

            }
        }
    }
}
