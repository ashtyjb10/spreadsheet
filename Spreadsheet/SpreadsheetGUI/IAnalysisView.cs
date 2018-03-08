using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGUI
{
    public interface IAnalysisView
    {
        event Action<string> NewFileChoses;

        event Action CloseEvent;

        event Action<string> GetCellInfo;

        event Action<string> ContentsChanged;


    }
}
