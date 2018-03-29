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

        [WebInvoke(Method = "POST", UriTemplate = "/users")]
        UserToke Register(UserInfo user);
        
        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        UserGame joinGame(JoinGameInfo item);
        
        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void cancelGame(UserCancel cancelInfo);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{GameID}")]
        WordScore playWord(WordToPlay wordInfo, string GameID);

        [WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}")]
        FullGameInfo getGameStats(string GameID);

       //[WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}?Brief=yes")]
        //string getGameStatsBrief(string GameID);
       

    }
}
