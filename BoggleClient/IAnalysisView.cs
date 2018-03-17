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
        bool timerEnabled { get; set; }
        void setUserNames(string player1, string player2);
        void setScores(string player1, string player2);
        void setTime(string timeRemaining);
        event Action<string, string> RegisterUser;
        event Action<string> DesiredGameDuration;
        event Action<string> ScoreWord;
        event Action TickingTimer;
        event Action CancelJoinGame;
        event Action QuitGameClicked;

        void RegisteredUser();
        void GameJoined();
        void JoinGameCanceled();
        void ViewPendingBox(bool visable);
        void SetBoard(char[] boardArray);
        void ViewActiveBox(bool visible);
        void ViewCompletedBox(bool visible);
        void setPlayer1WordsPlayed(HashSet<string> wordsPlayed);
        void setPlayer2WordsPlayed(HashSet<string> wordsPlayed);
        

    }
}
