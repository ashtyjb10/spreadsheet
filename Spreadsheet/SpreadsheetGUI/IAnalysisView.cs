﻿using System;
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

        event Action SelectionChanged;

        event Action<int> ColChanged;

        event Action<int> RowChanged;

        event Action CloseEvent;

        void CellNameText(string CellName);

        void ContentsBox(object contents);

        void ValueBox(object value);
        void UpdatedValue(int col, int row, object value);


        event Action Save;

        bool isChanged { get; }

        string Title { set; get; }

        string Content { set; }

        string Value {   set; }

        void DoClose();
    }
}
