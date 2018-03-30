//created by Ashton Schmidt and Nathan Herrmann

using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Boggle
{
    [ServiceContract]
    public interface IBoggleService
    { 
        
        /// <summary>
        /// Sends back index.html as the response body.
        /// </summary>
        [WebGet(UriTemplate = "/api")]
        Stream API();
        
        //******************our start *************************
        /// <summary>
        /// post method to Register a user, with Nickname as a parameter.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "/users")]
        UserToke Register(UserInfo user);
        
        /// <summary>
        /// post method to join a game with the parameters having UserToken and TimeLimit.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        UserGame joinGame(JoinGameInfo item);
        
        /// <summary>
        /// put method in order to cancel joining a game. Can only be done when
        /// GameState is pending. Parameters are UserToken.
        /// </summary>
        /// <param name="cancelInfo"></param>
        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void cancelGame(UserCancel cancelInfo);

        /// <summary>
        /// put method in order to play a word for a certain game, only when the game is active.
        /// Parameters are the word played, and the UserToken.
        /// </summary>
        /// <param name="wordInfo"></param>
        /// <param name="GameID"></param>
        /// <returns></returns>
        [WebInvoke(Method = "PUT", UriTemplate = "/games/{GameID}")]
        WordScore playWord(WordToPlay wordInfo, string GameID);

        /// <summary>
        /// Gets the game status no parameters required. Will provide the correct information, 
        /// no matter the game state. as long as Brief=yes is not a parameter.
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}")]
        FullGameInfo getGameStats(string GameID);

        /// <summary>
        /// Gets the game statue with Brief=yes is a parameter, will returned a shortened
        /// version of the game requested.
        /// </summary>
        /// <param name="GameID"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}?Brief=yes")]
        FullGameInfo getGameStatsBrief(string GameID);
       

    }
}
