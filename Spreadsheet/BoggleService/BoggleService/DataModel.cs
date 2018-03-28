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
        public string BogBoard { get; set; }
        // is this right?

    }

}