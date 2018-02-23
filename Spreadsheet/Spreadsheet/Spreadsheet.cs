//Created by Nathan Herrmann for CS 3500
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Dependencies;
using Formulas;

namespace SS
{
    /// <summary>
    /// A spreadsheet consists of a regular expression (called IsValid below) and an infinite 
    /// number of named cells.
    /// 
    /// A string is a valid cell name if and only if (1) s consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits AND (2) the C#
    /// expression IsValid.IsMatch(s.ToUpper()) is true.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are valid cell names, so long as they also
    /// are accepted by IsValid.  On the other hand, "Z", "X07", and "hello" are not valid cell 
    /// names, regardless of IsValid.
    /// 
    /// Any valid incoming cell name, whether passed as a parameter or embedded in a formula,
    /// must be normalized by converting all letters to upper case before it is used by this 
    /// this spreadsheet.  For example, the Formula "x3+a5" should be normalize to "X3+A5" before 
    /// use.  Similarly, all cell names and Formulas that are returned or written to a file must also
    /// be normalized.
    /// 
    /// A spreadsheet contains a unique cell corresponding to every possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important, and it is important that you understand the distinction and use
    /// the right term when writing code, writing comments, and asking questions.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In an empty spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError.
    /// The value of a Formula, of course, can depend on the values of variables.  The value 
    /// of a Formula variable is the value of the spreadsheet cell it names (if that cell's 
    /// value is a double) or is undefined (otherwise).  If a Formula depends on an undefined
    /// variable or on a division by zero, its value is a FormulaError.  Otherwise, its value
    /// is a double, as specified in Formula.Evaluate.
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        const string namePattern = @"^([a-zA-Z]+)([1-9])(\d+)?$";
        private Dictionary<string, Cell> nonEmptyCells;
        private DependencyGraph dependencyGraph;
        private Regex IsValid;
        private bool hasChanges = false;

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression accepts every string.
        /// </summary>
        public Spreadsheet()
        {
            nonEmptyCells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
            IsValid = new Regex(@"(.+)?");
        }

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression is provided as the parameter
        /// </summary>
        public Spreadsheet(Regex isValid)
        {
            nonEmptyCells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
            this.IsValid = isValid;
        }


        /// <summary>
        /// Creates a Spreadsheet that is a duplicate of the spreadsheet saved in source.
        ///
        /// See the AbstractSpreadsheet.Save method and Spreadsheet.xsd for the file format 
        /// specification.  
        ///
        /// If there's a problem reading source, throws an IOException.
        ///
        /// Else if the contents of source are not consistent with the schema in Spreadsheet.xsd, 
        /// throws a SpreadsheetReadException.  
        ///
        /// Else if the IsValid string contained in source is not a valid C# regular expression, throws
        /// a SpreadsheetReadException.  (If the exception is not thrown, this regex is referred to
        /// below as oldIsValid.)
        ///
        /// Else if there is a duplicate cell name in the source, throws a SpreadsheetReadException.
        /// (Two cell names are duplicates if they are identical after being converted to upper case.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a 
        /// SpreadsheetReadException.  (Use oldIsValid in place of IsValid in the definition of 
        /// cell name validity.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a
        /// SpreadsheetVersionException.  (Use newIsValid in place of IsValid in the definition of
        /// cell name validity.)
        ///
        /// Else if there's a formula that causes a circular dependency, throws a SpreadsheetReadException. 
        ///
        /// Else, create a Spreadsheet that is a duplicate of the one encoded in source except that
        /// the new Spreadsheet's IsValid regular expression should be newIsValid.
        /// </summary>
        public Spreadsheet (TextReader source, Regex newIsValid)
        {
            //Create schema.
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add(null, "Spreadsheet.xsd");

            //Create settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schema;
            settings.ValidationEventHandler += ValidationCallback;

            //Object variables
            nonEmptyCells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
            this.IsValid = newIsValid;
            Regex oldIsValid = null;

            //Open reader for reading the file
            using (XmlReader reader = XmlReader.Create(source, settings))
            {
                //As long as there is more to read.
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            //If the StartElement is spreadsheet.
                            case "spreadsheet":

                                //Copies oldIsValid to a string.
                                string oldIsValidString = reader["IsValid"];

                                try
                                {
                                    //If the string is a valid regex, no exception thrown.
                                    oldIsValid = new Regex(oldIsValidString);
                                }
                                catch (Exception)
                                {
                                    //If regex is not valid, throw spreadsheetReadException
                                    throw new SpreadsheetReadException("oldIsValid is not a valid Regex");
                                }

                                //Bail out.
                                break;

                            //If the startElement is cell
                            case "cell":

                                //make the cell from the stored info
                                string cellName = reader["name"].ToUpper();
                                string cellContent = reader["contents"];

                                //If the cell is already in named cells, throw a spreadhseetreadexception
                                if (nonEmptyCells.ContainsKey(cellName))
                                {
                                    throw new SpreadsheetReadException("Duplicate cell names exist");
                                }

                                //If the name is not valid on the old regex.
                                if (!oldIsValid.IsMatch(cellName))
                                {
                                    throw new SpreadsheetReadException("Name is not valid to oldIsValid expression");
                                }

                                //If the name is not valid throw a spreadsheetversionexception
                                if (!newIsValid.IsMatch(cellName))
                                {
                                    throw new SpreadsheetVersionException("Name is not valid to newIsValid expression");
                                }

                                //Try to set the content.  If there is a circulat exception, throw a spreasheetreadexceotion.
                                try
                                {
                                    SetContentsOfCell(cellName, cellContent);
                                }
                                catch (CircularException)
                                {
                                    throw new SpreadsheetReadException("Circular Dependency exists in source");
                                }
                                //If there is a formulaformatexception, throw a spreadsheetread exception.
                                catch (FormulaFormatException)
                                {
                                    throw new SpreadsheetReadException("Formula in source is invalid");
                                }
                                
                                //bail out
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Throws an exception in the case that the schematic is not followed by the source.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationCallback(object sender, ValidationEventArgs e)
        {
            throw new SpreadsheetReadException("Could not validate spreasdheet");
        }

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed { get => this.hasChanges; protected set => hasChanges = true; }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            
            //Try to find the cell that has the content.
            this.nonEmptyCells.TryGetValue(name, out Cell returnedCell);

            //If the content in the cell is null, then it is an empty cell and an empty string is returned.
            if (returnedCell.GetContent() == null)
            {
                return "";
            }
            //Otherwise return the content.
            else
            {
                return returnedCell.GetContent();
            }        
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            if (name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }

            nonEmptyCells.TryGetValue(name, out Cell returnedCell);

            if (returnedCell.Equals(null))
            {
                return 0;
            }
            else
            {
                return returnedCell.GetValue();
            }
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            //The set of all names to be returned.
            HashSet<string> cellNames = new HashSet<string>();

            //Populate the set.
            foreach (Cell cell in this.nonEmptyCells.Values)
            {
                cellNames.Add(cell.GetName());
            }

            return cellNames;
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to dest using an XML format.
        /// The XML elements should be structured as follows:
        ///
        /// <spreadsheet IsValid="IsValid regex goes here">
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        /// </spreadsheet>
        ///
        /// The value of the IsValid attribute should be IsValid.ToString()
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.
        /// If the cell contains a string, the string (without surrounding double quotes) should be written as the contents.
        /// If the cell contains a double d, d.ToString() should be written as the contents.
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        ///
        /// If there are any problems writing to dest, the method should throw an IOException.
        /// </summary>
        public override void Save(TextWriter dest)
        {
            

            using(XmlWriter writer = XmlWriter.Create(dest))
            {
                writer.WriteStartDocument();
                char[] toTrim = { '"' };
                
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("IsValid", this.IsValid.ToString());
                foreach(Cell cell in nonEmptyCells.Values)
                {
                    
                    writer.WriteStartElement("cell");
                    writer.WriteAttributeString("name", cell.GetName());

                    if (cell.GetContent() is string)
                    {
                        writer.WriteAttributeString("contents", cell.GetContent().ToString());
                    }

                    if(cell.GetContent() is Double)
                    {
                        writer.WriteAttributeString("contents", cell.GetContent().ToString());
                    }
                    

                    if (cell.GetContent() is Formula)
                    {     
                        writer.WriteAttributeString("contents", "=" + cell.GetContent().ToString());
                    }
                    
                    writer.WriteFullEndElement();
                }
                writer.WriteFullEndElement();
                writer.WriteEndDocument();
            }

            this.hasChanges = false;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, double number)
        {
            //Null name check and pattern matching
            if (name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }

            //If the cell exists in the nonEmpty list then it must be removed and replacesd along
            //with the dependencies
            if (nonEmptyCells.ContainsKey(name))
            {
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                nonEmptyCells.Remove(name);
            }
            
            //Create the new cell
            Cell newCell = new Cell(name, number);
            nonEmptyCells.Add(name, newCell);
            
            //Create the list to be returned
            HashSet<string> toReturn = new HashSet<string>();
            toReturn.Add(name);

            GetAllDependents(name, name, toReturn);

            return toReturn;
    }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            //Null check
            if(text == null)
            {
                throw new ArgumentNullException();
            }

            //Null check and regex check
            if (name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }

            //If the content is an empty string, the cell is empty and does not have any dependency other than itself.
            if (text.Equals(""))
            {
                if (nonEmptyCells.ContainsKey(name))
                {
                    nonEmptyCells.Remove(name);
                }

                HashSet<string> toReturnEmpty = new HashSet<string>();
                toReturnEmpty.Add(name);
                return toReturnEmpty;
            }

            //If the nonempty cell contains the name, the the original cell and dependencies must
            //be removed.
            if (nonEmptyCells.ContainsKey(name))
            {
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                nonEmptyCells.Remove(name);
            }

            //If cell is not empty, make a new, non-empty cell
            Cell newCell = new Cell(name, text);
            nonEmptyCells.Add(name, newCell);

            //Create the list to be returned
            HashSet<string> toReturn = new HashSet<string>();
            toReturn.Add(name);

            GetAllDependents(name, name, toReturn);
            return toReturn;
        }

        /// <summary>
        /// Requires that all of the variables in formula are valid cell names.
        /// 
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            //Null check.
            if (formula.Equals(null))
            {
                throw new ArgumentNullException();
            }

            //Null and regex check on name.
            if (name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            
            
            //Set up dependencies.
            foreach (string token in formula.GetVariables())
            {
                if(Regex.IsMatch(token, namePattern))
                {
                    dependencyGraph.AddDependency(token, name);
                }
            }

            //HashSet to return to the caller created.
            HashSet<string> toReturn = new HashSet<string>();
            toReturn.Add(name);

            //Get all the direct and indirect dependencies.
            GetAllDependents(name, name, toReturn);
            

            //If the cell exits, it along with all its dependencies must be removed
            if (nonEmptyCells.ContainsKey(name))
            {
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                nonEmptyCells.Remove(name);
            }

            //Create a new cell and add it to the cell list.
            Cell newCell = new Cell(name, formula);
            nonEmptyCells.Add(name, newCell);

            return toReturn;
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        ///
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        ///
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor with s => s.ToUpper() as the normalizer and a validator that
        /// checks that s is a valid cell name as defined in the AbstractSpreadsheet
        /// class comment.  There are then three possibilities:
        ///
        ///   (1) If the remainder of content cannot be parsed into a Formula, a
        ///       Formulas.FormulaFormatException is thrown.
        ///
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///
        ///   (3) Otherwise, the contents of the named cell becomes f.
        ///
        /// Otherwise, the contents of the named cell becomes content.
        ///
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        ///
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            //Set changes to be true.
            this.hasChanges = true;

            //To return value.
            ISet<String> toReturn;
            name = name.ToUpper();
            //Null check.
            if(content == null)
            {
                throw new ArgumentNullException();
            }

            //Null or invalid name check.
            if(name == null || !Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }

            //If a double,  set the value of the cell to content and return the set.
            try
            {
                toReturn = SetCellContents(name, double.Parse(content));
                
                //Update required cells.
                foreach (String cellName in GetCellsToRecalculate(toReturn))
                {
                    //If a FormulaEvaluationException occurs
                    try
                    {
                        nonEmptyCells.TryGetValue(cellName, out Cell actualCell);
                        EvaluateByType(cellName, actualCell);
                    }
                    //The Cell that was unable to evaluate is given a FormulaError as a value
                    catch (FormulaEvaluationException)
                    {
                        nonEmptyCells.TryGetValue(cellName, out Cell toError);
                        toError.SetValue(new FormulaError());
                        nonEmptyCells[cellName] = toError;
                        
                    }
                }
                //Return dependents
                return toReturn;

            }
            catch (Exception)
            {
                //Not a double.
            }

            //If the content starts with "=" then make the cotnent a formula.
            if (content.StartsWith("="))
            {
                toReturn = SetCellContents(name, new Formula(content.Substring(1), s => s.ToUpper(), s => Regex.IsMatch(s, namePattern)));
                
                 //Update required cells.
                foreach (String cellName in GetCellsToRecalculate(toReturn))
                {
                    //If a FormulaEvaluationException happens.
                    try
                    {
                        nonEmptyCells.TryGetValue(cellName, out Cell actualCell);
                        EvaluateByType(cellName, actualCell);
                    }
                    //The cells value that could not be evaluated is given FormulaError as its value.
                    catch (FormulaEvaluationException)
                    {
                        nonEmptyCells.TryGetValue(cellName, out Cell toError);
                        toError.SetValue(new FormulaError());
                        nonEmptyCells[cellName] = toError;
                        
                    }
                }
                //return the dependents.
                return toReturn;
            }

            //Otherise make the content = content.
            toReturn = SetCellContents(name, content);
            
            //Update required cells.
            foreach (String cellName in GetCellsToRecalculate(toReturn))
            {
                //If a FormulaEvaluationException happens.
                try
                {
                    nonEmptyCells.TryGetValue(cellName, out Cell actualCell);
                    EvaluateByType(cellName, actualCell);
                }
                //The cell that could not be evaluatr
                catch (FormulaEvaluationException)
                {
                    nonEmptyCells.TryGetValue(cellName, out Cell toError);
                    toError.SetValue(new FormulaError());
                    nonEmptyCells[cellName] = toError;
                    
                }
            }
            //return dependents.
            return toReturn;
         }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //Null check
            if(name == null)
            {
                throw new ArgumentNullException();
            }

            //regex check
            if (!Regex.IsMatch(name, namePattern) || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            
            //create the list to be returned
            List<string> directDependencies = new List<string>();

            //Populate list with dependencies
            foreach (string dependent in this.dependencyGraph.GetDependents(name))
            {
                directDependencies.Add(dependent);
            }

            return directDependencies;
        }

        /// <summary>
        /// Helper method that will add all dependencies, direct and indirect, to an ISet<string>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="set"></param>
        private void GetAllDependents(string start, string name, ISet<string> set)
        {
            //Get all the dependencies of the named cell.
            foreach (string dependency in dependencyGraph.GetDependents(name))
            {
                //If the top cell is dependenct on itself, throw an exception
                if (dependency.Equals(start))
                {
                    throw new CircularException();
                }

                //If the set does not already have the depenedncy listed, add it
                if(!set.Contains(dependency))
                {
                    set.Add(dependency);
                    GetAllDependents(start, dependency, set);
                }
            }
        }

        
        /// <summary>
        /// Private method that helps evaluate each cell following the formula or double that it contains.
        /// If the cell contains only a string then the exception is caught and the value is set to the string.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toEvaluate"></param>
        private void EvaluateByType(string name, Cell toEvaluate)
        {
            //Try to evaluate
            try
            {
                toEvaluate.Calculate(new Formula(toEvaluate.GetContent().ToString(), s => s.ToUpper(), s => Regex.IsMatch(s, namePattern)), nonEmptyCells);
                nonEmptyCells[name] = toEvaluate;
                
            }
            //If exception is caught then set the value to the string of the cell.
            catch (FormulaFormatException)
            {
                toEvaluate.SetValue(toEvaluate.GetContent());
            }
            

        }

        
        /// <summary>
        /// Private struct that is used to represent a cell in a spreadhseet, contains the name of the cell
        /// and the content of the cell as either a string, double or Formula.
        /// </summary>
        private struct Cell
        {
            private string name;
            private object content;
            private object value;

            /// <summary>
            /// String constructor
            /// </summary>
            /// <param name="name"></param>
            /// <param name="content"></param>
            public Cell(string name, string content)
            {
                this.name = name;
                this.content = content;
                this.value = content;
            }

            ///<summary>
            ///Double constructor
            ///<summary>
            public Cell(string name, double content)
            {
                this.name = name;
                this.content = content;
                this.value = content;
            }

            /// <summary>
            /// Formula constructor
            /// </summary>
            /// <param name="name"></param>
            /// <param name="content"></param>
            public Cell(string name, Formula content)
            {
                this.name = name;
                this.content = content;
                this.value = 0;
            }

            /// <summary>
            /// Returns the content of the cell
            /// </summary>
            /// <returns></returns>
            public object GetContent()
            {
                return this.content;
            }

            /// <summary>
            /// Returns the name of the cell
            /// </summary>
            /// <returns></returns>
            public string GetName()
            {
                return this.name;
            }

            /// <summary>
            /// Returns the value of the cell
            /// </summary>
            /// <returns></returns>
            public object GetValue()
            {
                return this.value;
            }

            /// <summary>
            /// Uses the formula privided to set the value of the cell
            /// </summary>
            /// <param name="content"></param>
            /// <param name="dictionary"></param>
            public void Calculate(Formula content, Dictionary<String, Cell> dictionary)
            {
                value = content.Evaluate(s => {if (dictionary.TryGetValue(s, out Cell cell))
                        return (double)cell.GetValue();
                    else throw new FormulaFormatException(""); });
            }

            /// <summary>
            /// Sets the value of the cell.  Used in the case that there is a FormulaEvaluationError error,
            /// or the value is a string.
            /// </summary>
            /// <param name="val"></param>
            public void SetValue(object val)
            {
                this.value = val;
            }
        }
    }
}