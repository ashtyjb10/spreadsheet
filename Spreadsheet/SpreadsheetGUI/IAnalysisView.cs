using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    /// <summary>
    /// Controller is allowed to use these to modify the view.
    /// </summary>
    public interface IAnalysisView
    {
        event Action<string> NewFileChosen;

        event Action<string> GetCellInfo;

        event Action<string> ContentsChanged;

        event Action<string> SelectionChanged;

        event Action CloseEvent;

        event Action Save;

        bool isChanged { get; }

        string Title { set; get; }

        string Content { set; }

        string Value { set; }

        void DoClose();
    }
}
