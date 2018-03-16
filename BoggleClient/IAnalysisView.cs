using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    public interface IAnalysisView
    {
        bool IsRegisteredUser { get; set; }
        string NickName { get;  set; }
        string baseAddress { get; set; }
        string wordToSubmit { get; set; }
        string timeRemaining { get; set; }
        string score { get; set; }
        string board { get; set; }
        string statsBoard { get; set; }
        string GameDuraton { get; set; }

        void setUserNames(string player1, string player2);
        void EnableControls(bool enabled);
        void setScores(string player1, string player2);
        void setTime(string timeRemaining);

        event Action<string, string> RegisterUser;
        event Action<string> DesiredGameDuration;
        event Action<string> ScoreWord;
        void RegisteredUser();
        void GameJoined();
        void ViewPendingBox(bool visable);
        void setBoard(char[] boardArray);
        void ViewActiveBox(bool visible);
        void ViewCompletedBox(bool visible);

    }
}
