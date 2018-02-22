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
        [TestMethod]
        public void AAASetCellContent1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "0.0");

            Assert.AreEqual(0.0, s.GetCellValue("A1"));
        }

        [TestMethod]
        public void AAASetCellContent2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Food");

            Assert.AreEqual("Food", s.GetCellValue("A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void AAASetCellContent3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1A", "Food");

            Assert.AreEqual("Food", s.GetCellValue("A1"));
        }

        [TestMethod]
        public void AAASetCellContent5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2 + A2");
            s.SetContentsOfCell("a2", "2");

            Assert.AreEqual(4.0, s.GetCellValue("A1"));
        }


        [TestMethod]
        public void AAASetCellContent6()
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

        [TestMethod]
        public void AAASetCellContent7()
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

            Assert.AreEqual(30.0, s.GetCellValue("A1"));
        }

        [TestMethod]
        public void AAASetCellContent8()
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

            StreamWriter writer1 = new StreamWriter("Test string Spreadsheet");
            s.Save(writer1);

            s.SetContentsOfCell("A9", "1");

            Assert.AreEqual(6.0, s.GetCellValue("A1"));
            Assert.AreEqual(4.0, s.GetCellValue("A3"));
            Assert.AreEqual(4.0, s.GetCellValue("A4"));
            Assert.AreEqual(1.0, s.GetCellValue("A9"));
            Assert.AreEqual(1.0, s.GetCellValue("A10"));
            Assert.AreEqual(2.0, s.GetCellValue("A11"));

            StreamWriter writer2 = new StreamWriter("Test Spreadsheet");
            s.Save(writer2);
        }
    }

}
