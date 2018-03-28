using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class UserInfo
    {
        public  String Nickname { get; set; }
    }

    public class JoinGameInfo
    {
        public string UserToken { get; set; }
        public string TimeLimit { get; set; }
    }

    public class GameInfo
    {
        public string GameState { get; set; }
        public string Board { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string GameID { get; set; }
        public string TimeLimit { get; set; }
        // is this right?

    }

}