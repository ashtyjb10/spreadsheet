using Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using System.IO;
using System.Text.RegularExpressions;

namespace PS6UnitTest
{
    [TestClass]
    public class PS6UnitTests
    {
        /// <summary>
        /// Simple set double content
        /// </summary>
        [TestMethod]
        public void SetCellContent1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "0.0");

            Assert.AreEqual(0.0, s.GetCellValue("A1"));
        }
        /// <summary>
        /// Simple set string content
        /// </summary>
        [TestMethod]
        public void SetCellContent2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Food");

            Assert.AreEqual("Food", s.GetCellValue("A1"));
        }
        /// <summary>
        /// Simple error check
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContent3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1A", "Food");

            Assert.AreEqual("Food", s.GetCellValue("A1"));
        }

        /// <summary>
        /// Simple set multiple content with dependencies
        /// </summary>
        [TestMethod]
        public void SetCellContent5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2 + A2");
            s.SetContentsOfCell("a2", "2");

            Assert.AreEqual(4.0, s.GetCellValue("A1"));
        }

        /// <summary>
        /// Set lots of content with chain of depenencies, checking for FormulaError
        /// </summary>
        [TestMethod]
        public void SetCellContent6()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2 + A3 + A4 +A5 + A6+A7+A8+A9+A10+a11");
            s.SetContentsOfCell("a2", "2");
            s.SetContentsOfCell("a3", "2");
            s.SetContentsOfCell("a4", "guffy");
            s.SetContentsOfCell("a5", "2");
            s.SetContentsOfCell("a6", "2");
            s.SetContentsOfCell("a7", "2");
            s.SetContentsOfCell("a8", "2");
            s.SetContentsOfCell("a9", "2");
            s.SetContentsOfCell("a10", "2");
            s.SetContentsOfCell("a11", "2");

            Assert.AreEqual(new FormulaError(), s.GetCellValue("A1"));
        }

        /// <summary>
        /// New spreadsheet with regex constructor.  If the cell is empty, and another cell depends on it, that 
        /// evaluates to a FormulaError
        /// </summary>
        [TestMethod]
        public void SetCellContent7()
        {
            Spreadsheet s = new Spreadsheet(new Regex(@"^([a-zA-Z]+)([1-9])(\d+)?$"));
            s.SetContentsOfCell("A1", "=a2 + a3 + A4 +A5 + A6+A7+A8+A9+A10+a11");
            s.SetContentsOfCell("a2", "2");
            s.SetContentsOfCell("a3", "2");
            s.SetContentsOfCell("a4", "5");
            s.SetContentsOfCell("a5", "2");
            s.SetContentsOfCell("a6", "3");
            s.SetContentsOfCell("a7", "2");
            s.SetContentsOfCell("a8", "1");
            s.SetContentsOfCell("a8", "2");
            s.SetContentsOfCell("a10", "3");
            s.SetContentsOfCell("a11", "9");

            Assert.AreEqual(new FormulaError(), s.GetCellValue("A1"));
        }

        /// <summary>
        /// new spreadsheet with lots of content, checking for FormulaErrors and changing content.  Write final spreadseet to file.
        /// </summary>
        [TestMethod]
        public void SetCellContent8()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=a2 + a4");
            s.SetContentsOfCell("a2", "2");
            s.SetContentsOfCell("a3", "=A7 + A11");
            s.SetContentsOfCell("a4", "=A3");
            s.SetContentsOfCell("a5", "2");
            s.SetContentsOfCell("a6", "3");
            s.SetContentsOfCell("a7", "2");
            s.SetContentsOfCell("a8", "1");
            s.SetContentsOfCell("a9", "Gwap");
            s.SetContentsOfCell("a10", "=A9");
            s.SetContentsOfCell("a11", "=A8 + A10");

            Assert.AreEqual(new FormulaError(), s.GetCellValue("A1"));
            Assert.AreEqual(2.0, s.GetCellValue("A2"));
            Assert.AreEqual(new FormulaError(), s.GetCellValue("A3"));
            Assert.AreEqual(new FormulaError(), s.GetCellValue("A4"));
            Assert.AreEqual(2.0, s.GetCellValue("A5"));
            Assert.AreEqual(3.0, s.GetCellValue("A6"));
            Assert.AreEqual(2.0, s.GetCellValue("A7"));
            Assert.AreEqual(1.0, s.GetCellValue("A8"));
            Assert.AreEqual("Gwap", s.GetCellValue("A9"));
            Assert.AreEqual(new FormulaError(), s.GetCellValue("A10"));
            Assert.AreEqual(new FormulaError(), s.GetCellValue("A11"));

            StreamWriter writer1 = new StreamWriter("TestStringSpreadsheet.xml");
            s.Save(writer1);

            s.SetContentsOfCell("A9", "1");

            Assert.AreEqual(6.0, s.GetCellValue("A1"));
            Assert.AreEqual(4.0, s.GetCellValue("A3"));
            Assert.AreEqual(4.0, s.GetCellValue("A4"));
            Assert.AreEqual(1.0, s.GetCellValue("A9"));
            Assert.AreEqual(1.0, s.GetCellValue("A10"));
            Assert.AreEqual(2.0, s.GetCellValue("A11"));

            StreamWriter writer2 = new StreamWriter("TestSpreadsheet.xml");
            s.Save(writer2);
            
        }

        /// <summary>
        /// Check for formula format exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void SetCellContent9()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=j;");

            Assert.AreEqual("Food", s.GetCellValue("A1"));
        }

        /// <summary>
        /// Crating a copy of a spreadsheet to verify equality of data.
        /// </summary>
        [TestMethod]
        public void CheckSaveCellContent1()
        {
            Spreadsheet s = new Spreadsheet(new Regex(@"^([a-zA-Z]+)([1-9])(\d+)?$"));
            s.SetContentsOfCell("A1", "=a2 + a3 + A4 +A5 + A6+A7+A8+A9+A10+a11");
            s.SetContentsOfCell("a2", "2");
            s.SetContentsOfCell("a3", "2");
            s.SetContentsOfCell("a4", "5");
            s.SetContentsOfCell("a5", "2");
            s.SetContentsOfCell("a6", "3");
            s.SetContentsOfCell("a7", "2");
            s.SetContentsOfCell("a8", "1");
            s.SetContentsOfCell("a9", "=A8");
            s.SetContentsOfCell("a10", "3");
            s.SetContentsOfCell("a11", "9");

            Assert.AreEqual(30.0, s.GetCellValue("A1"));
            
            Spreadsheet copy = new Spreadsheet(new StreamReader("spreadsheetSave.xml"), new Regex(@"^([a-zA-Z]+)([1-9])(\d+)?$"));

            Assert.AreEqual(6.0, copy.GetCellValue("A1"));
        }

        /// <summary>
        /// Check for spreadsheet read exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ValidateCellContent1()
        {
            Spreadsheet copy = new Spreadsheet(new StreamReader("TestSpreadsheetFail.xml"), new Regex(@"^([a-zA-Z]+)([1-9])(\d+)?$"));
        }

        /// <summary>
        /// Ensures that the change method returns the proper bool
        /// </summary>
        [TestMethod]
        public void Changed1()
        {
            Spreadsheet s = new Spreadsheet();
            if(s.Changed == true)
            {
                throw new System.Exception();
            }

            s.SetContentsOfCell("A1", "2.0");

            if (s.Changed == false)
            {
                throw new System.Exception();
            }

            s.Save(new StreamWriter( "HasChangedTest.txt"));

            if (s.Changed == true)
            {
                throw new System.Exception();
            }
        }

    }
}
