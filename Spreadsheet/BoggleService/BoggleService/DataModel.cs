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
        public int TimeLimit { get; set; }
    }
    public class UserCancel
    {
        public string UserToken { get; set; }
    }

    public class WordToPlay
    {
        public string UserToken { get; set; }
        public string Word { get; set; }
    }

    public class GameInfo
    {
        public string GameState { get; set; }
        public string Board { get; set; }
        public BoggleBoard BoardObject { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string GameID { get; set; }
        public int TimeLimit { get; set; }
        public Dictionary<string, int> wordsPlayedP1 = new Dictionary<string, int>();
        public Dictionary<string, int> wordsPlayedP2 = new Dictionary<string, int>();

        // is this right?

    }

    public class storedUserInfo
    {
        public string Nickname { get; set; }
        public string GameID { get; set; }
        public string UserToken { get; set; }
    }

}