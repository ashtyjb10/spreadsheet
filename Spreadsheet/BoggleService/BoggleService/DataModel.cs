using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Boggle
{

    public class UserInfo
    {
        public String Nickname { get; set; }
    }
    [DataContract]
    public class UserToke
    {
       
        [DataMember]
        public String UserToken { get; set; }
    }

    public class JoinGameInfo
    {
        public string UserToken { get; set; }
        public int TimeLimit { get; set; }
    }
    [DataContract]
    public class UserGame
    {
        [DataMember]
        public String GameID { get; set; }
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
    [DataContract]
    public class WordScore
    {
        [DataMember]
        public int Score { get; set; }
    }

    public class GameInfo
    {
        public string GameState { get; set; }
        public string Board { get; set; }
        public BoggleBoard BoardObject { get; set; }
        public string Player1 { get; set; }//userToken
        public int p1Score { get; set; }
        public string Player2 { get; set; }//userToken
        public int p2Score { get; set; }
        public string GameID { get; set; }
        public int TimeGameStarted { get; set; }
        public int TimeLimit { get; set; }
        public Dictionary<string, int> wordsPlayedP1 = new Dictionary<string, int>();
        public Dictionary<string, int> wordsPlayedP2 = new Dictionary<string, int>();

    }
    [DataContract]
    public class FullGameInfo
    {
        [DataMember]
        public string GameState { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Board { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int? TimeLeft { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Player1 Player1 { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Player2 Player2 { get; set; }
    }

    [DataContract]
    public class Player1
    {
        [DataMember(EmitDefaultValue = false)]
        public string Nickname { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public List<WordInfo> WordsPlayed { get; set; }

    }
    [DataContract]
    public class Player2
    {
        [DataMember(EmitDefaultValue = false)]
        public string Nickname { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public List<WordInfo> WordsPlayed { get; set; }

    }

    public class WordInfo
    {
        public string Word { get; set; }
        public string Score { get; set; }
    }


    public class storedUserInfo
    {
        public string Nickname { get; set; }
        public string GameID { get; set; }
        public string UserToken { get; set; }
    }

}