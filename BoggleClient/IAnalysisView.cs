using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    public interface IAnalysisView
    {
        string NickName { get;  set; }
        string baseAddress { get; set; }
        string wordToSubmit { get; set; }
        string timeRemaining { get; set; }
        string score { get; set; }
        string board { get; set; }
        string statsBoard { get; set; }
        void EnableControls(bool enabled);
        event Action<string, string> RegisterUser;
    }
}
