using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class UserInfo
    {
        public string Nickname { get; set; }
    }

    public class JoinGameInfo
    {
        public string userToken { get; set; }
        public string desiredTime { get; set; }
    }

    public class GameInfo
    {
        public string GameState { get; set; }
        public string BogBoard { get; set; }
        // is this right?

    }

}