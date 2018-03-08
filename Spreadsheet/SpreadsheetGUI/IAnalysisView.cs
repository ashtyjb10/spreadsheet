using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    public interface IAnalysisView
    {
        event Action<string> NewFileChosen;

        event Action CloseEvent;

        event Action<string> GetCellInfo;

        event Action<string> ContentsChanged;

        event Action Save;

        bool isChanged { get; }

        string Title { set; get; }

        void DoClose();
    }
}
