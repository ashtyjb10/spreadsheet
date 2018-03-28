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

        /// <summary>
        /// Returns the nth word from dictionary.txt.  If there is
        /// no nth word, responds with code 403. This is a demo;
        /// you can delete it.
        /// </summary>
        [WebGet(UriTemplate = "/word?index={n}")]
        string WordAtIndex(int n);
        
        
        //******************our start *************************

        [WebInvoke(Method = "POST", UriTemplate = "/users")]
        string Register(UserInfo user);
        
        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        string joinGame(JoinGameInfo item);
        
        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void cancelGame(string UserToken);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{GameID}")]
        string playWord(string GameID);

        [WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}")]
        string getGameStats(string GameID);
        [WebInvoke(Method = "GET", UriTemplate = "/games/{GameID}?Brief=yes")]
        string getGameStatsBrief(string GameID);
       

    }
}
