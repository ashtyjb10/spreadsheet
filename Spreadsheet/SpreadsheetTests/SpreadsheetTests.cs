using System;
using System.Collections.Generic;
using Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Determins if the name is acceptable
        /// </summary>
        [TestMethod]
        public void nameTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("AA31");
        }

        /// <summary>
        /// Verifies  that the return content method works on an empty cell
        /// </summary>
        [TestMethod]
        public void GetCellContent()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 12);
            string returned = sheet.GetCellContents("A2").ToString();

            Assert.AreEqual(returned, "");
        }

        /// <summary>
        /// Determines if the get cell content works on a nonempty cell
        /// </summary>
        [TestMethod]
        public void GetCellContent2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 12);
            string returned = sheet.GetCellContents("A1").ToString();

            Assert.AreEqual(returned, "12");
        }

        /// <summary>
        /// Ensures that an inccorect cell name is detected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContent3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A;1", 12);
            
        }
        
        ///<summary>
        ///Adds a cell and changes the content, no exception thrown
        ///<summary>
        [TestMethod]
        public void DoubleAdd1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 5);
            sheet.SetCellContents("A2", 2);
            sheet.SetCellContents("A1", 3);

        }
        ///<summary>
        ///Changes a double to a string.
        ///<summary>
        [TestMethod]
        public void DoubleAdd2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 3);
            sheet.SetCellContents("A2", 2);
            sheet.SetCellContents("A1", "grow");

        }
        /// <summary>
        /// Changes a double to a formula 
        /// </summary>
        [TestMethod]
        public void DoubleAdd3()
        {
            Spreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("2");
            sheet.SetCellContents("A1", 5);
            sheet.SetCellContents("A2", 2);
            sheet.SetCellContents("A1", f);

        }
        /// <summary>
        /// Returns the names of a single nonempty cell
        /// </summary>
        [TestMethod]
        public void GetNames1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 3);
            string[] trueNames = new string[] { "A1" };


            IEnumerable<string> names = sheet.GetNamesOfAllNonemptyCells();

            foreach (string checkName in names)
            {
                if (!(trueNames[0].Equals(checkName)))
                {
                    Assert.AreEqual(true, false);
                }
            }
        }

        /// <summary>
        /// Returns the names of the nonempty cells, and does not treat "D1" as nonempty
        /// </summary>
        [TestMethod]
        public void GetNames2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", "forest");
            sheet.SetCellContents("B1", "for");
            sheet.SetCellContents("C1", "the");
            sheet.SetCellContents("A1", "trees");
            sheet.SetCellContents("D1", "");
            string[] trueNames = new string[] { "A1", "B1", "C1" };
            int index = 0;
            IEnumerable<string> names = sheet.GetNamesOfAllNonemptyCells();

            foreach (string checkName in names)
            {
                if (!(trueNames[index].Equals(checkName)))
                {
                    Assert.AreEqual(true, false);
                }
                index++;
            }
        }
        ///<summary>
        ///For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        ///<summary>
        [TestMethod]
        public void CheckIndirectDependencies1()
        {
            ISet<string> check;
            string[] trueValue = new string[3];
            trueValue[0] = "A1";
            trueValue[1] = "B1";
            trueValue[2] = "C1";
            
            Spreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("A1*2");
            Formula f2 = new Formula("B1+A1");
            
            sheet.SetCellContents("B1", f);
            sheet.SetCellContents("C1", f2);
            check = sheet.SetCellContents("A1", 3);
            foreach (string dependent in trueValue)
            {
                if (!check.Contains(dependent))
                {
                    Assert.AreEqual(false, true);
                }
            }
        }

        /// <summary>
        /// WHen a formula is changed the depenedncies that existed are removed.
        /// </summary>
        [TestMethod]

        public void ClearDependencies1()
        {
            ISet<string> check;
            string[] trueValue = new string[3];
            trueValue[0] = "A1";
            trueValue[1] = "B1";
            trueValue[2] = "C1";
            
            Spreadsheet sheet = new Spreadsheet();
            Formula f = new Formula("A1 *2");
            Formula f2 = new Formula("B1 + A1");
            Formula f3 = new Formula("A4");
            sheet.SetCellContents("A1", f3);
            sheet.SetCellContents("B1", f);
            sheet.SetCellContents("C1", f2);
            check = sheet.SetCellContents("A1", 3);

            foreach(string dependency in check)
            {
                if (!check.Contains(dependency))
                {
                    Assert.AreEqual(true, false);
                }
            }
        }
        /// <summary>
        /// Checks for circular ecxetions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependencies1()
        {
            
            Spreadsheet sheet = new Spreadsheet();
            Formula f1 = new Formula("1 + A2");
            Formula f2 = new Formula("1 + A3");
            Formula f3 = new Formula("1 + A1");
            sheet.SetCellContents("A1", f1);
            sheet.SetCellContents("A2", f2);
            sheet.SetCellContents("A3", f3);
            
        }

        /// <summary>
        /// checks for circular ecxeptions indirect
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependencies2()
        {
            Spreadsheet sheet = new Spreadsheet();
            Formula f1 = new Formula("1 + A2");
            Formula f2 = new Formula("1 + A3");
            Formula f3 = new Formula("A3 + A3");
            sheet.SetCellContents("A1", f1);
            sheet.SetCellContents("A2", f2);
            sheet.SetCellContents("A3", f3);

        }
    }
}
