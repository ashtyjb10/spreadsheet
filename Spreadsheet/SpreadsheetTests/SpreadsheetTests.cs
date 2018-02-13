using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        public void ATestMethod()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("AAcd31");
        }
    }
}
