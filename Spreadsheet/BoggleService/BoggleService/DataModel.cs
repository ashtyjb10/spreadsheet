//created by Ashton Schmidt and Nathan Herrmann

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Boggle
{
    /// <summary>
    ///Used to store the Nickname of the user.
    /// </summary>
    public class UserInfo
    {
        public String Nickname { get; set; }
    }

    /// <summary>
    /// Used to serialize and send back the user token to the client.
    /// </summary>
    [DataContract]
    public class UserToke
    {
        [DataMember]
        public String UserToken { get; set; }
    }
    /// <summary>
    /// Stores the userToken, as well as the time limit allowing access in the
    /// BoggleService file.
    /// </summary>
    public class JoinGameInfo
    {
        public string UserToken { get; set; }
        public int TimeLimit { get; set; }
    }

    /// <summary>
    /// Serializes and sends the GameID back to the client.
    /// </summary>
    [DataContract]
    public class UserGame
    {
        [DataMember]
        public String GameID { get; set; }
    }
    /// <summary>
    /// stores the user token so that we can find the game to cancel.
    /// </summary>
    public class UserCancel
    {
        public string UserToken { get; set; }
    }
    /// <summary>
    /// stores the word and the user token so we can find the game to play the 
    /// word and add the word and score to the correct person in BoggleService file.
    /// </summary>
    public class WordToPlay
    {
        public string UserToken { get; set; }
        public string Word { get; set; }
    }
    /// <summary>
    /// serialize and send the score of the word played to the client.
    /// </summary>
    [DataContract]
    public class WordScore
    {
        [DataMember]
        public int Score { get; set; }
    }

    /// <summary>
    /// Stores ALL game information and used in the dictionary of the BoggleService. 
    /// Used because there are things that are set and we dont want to return to the client.
    /// </summary>
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

    /// <summary>
    /// Serializes and sends back the game information that matches the information
    /// provided by the client.
    /// </summary>
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

    /// <summary>
    /// Serializes and is added to the FullGameInfo object.
    /// </summary>
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
    /// <summary>
    /// Serializes and is added to the FullGameInfo object.
    /// </summary>
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


    /// <summary>
    /// Stores the word and the score of the word so that we can add that to the
    /// lists of words played for Player1 and Player2 objects
    /// </summary>
    public class WordInfo
    {
        public string Word { get; set; }
        public string Score { get; set; }
    }

    /// <summary>
    /// Used for the dictionary in BoggleService file, so that we can access players names
    /// and the game that they are currently in.
    /// </summary>
    public class storedUserInfo
    {
        public string Nickname { get; set; }
        public string GameID { get; set; }
        public string UserToken { get; set; }
    }

}