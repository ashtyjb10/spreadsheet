using System;
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

        // is this right?
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
        [DataMember]
        public string Nickname { get; set; }
        [DataMember]
        public int Score { get; set; }

        //        [DataMember(EmitDefaultValue = false)] for the array list I have to do.


    }
    [DataContract]
    public class Player2
    {
        [DataMember]
        public string Nickname { get; set; }
        [DataMember]
        public int Score { get; set; }
        //[DataMember(EmitDefaultValue = false)] for the array list I have to do.

    }

    public class storedUserInfo
    {
        public string Nickname { get; set; }
        public string GameID { get; set; }
        public string UserToken { get; set; }
    }

}