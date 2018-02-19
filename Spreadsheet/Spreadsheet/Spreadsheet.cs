//Created by Nathan Herrmann for CS 3500
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dependencies;
using Formulas;

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        const string namePattern = @"^([a-zA-Z]+)([1-9])(\d+)?$";
        private Dictionary<string, Cell> nonEmptyCells;
        private DependencyGraph dependencyGraph;

        /// <summary>
        /// Creates and empty spreadsheet
        /// </summary>
        public Spreadsheet()
        {
            nonEmptyCells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (name == null || !Regex.IsMatch(name, namePattern))
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
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, double number)
        {
            //Null name check and pattern matching
            if (name == null || !Regex.IsMatch(name, namePattern))
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
        public override ISet<string> SetCellContents(string name, string text)
        {
            //Null check
            if(text == null)
            {
                throw new ArgumentNullException();
            }

            //Null check and regex check
            if (name == null || !Regex.IsMatch(name, namePattern))
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
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            //Null check.
            if (formula.Equals(null))
            {
                throw new ArgumentNullException();
            }

            //Null and regex check on name.
            if (name == null || !Regex.IsMatch(name, namePattern))
            {
                throw new InvalidNameException();
            }

            foreach(string token in formula.GetVariables())
            {
                CheckCircularDependencies(name, token);
            }
            

            //If the cell exits, it along with all its dependencies must be removed
            if (nonEmptyCells.ContainsKey(name))
            {
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                nonEmptyCells.Remove(name);
            }

            //Create a new cell and add it to the cell list.
            Cell newCell = new Cell(name, formula);
            nonEmptyCells.Add(name, newCell);

            //HashSet to return to the caller created.
            HashSet<string> toReturn = new HashSet<string>();
            toReturn.Add(name);

            //Set up dependencies.
            foreach (string token in formula.GetVariables())
            {
                if(Regex.IsMatch(token, namePattern))
                {
                    dependencyGraph.AddDependency(token, name);
                }
            }

            //Get all the direct and indirect dependencies.
            GetAllDependents(name, name, toReturn);

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
            if (!Regex.IsMatch(name, namePattern))
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

       private void CheckCircularDependencies(string start, string name)
        {
            foreach(string dependency in dependencyGraph.GetDependents(name))
            {
                if (dependency.Equals(start))
                {
                    throw new CircularException();
                }

                CheckCircularDependencies(start, dependency);
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

            //String constructor
            public Cell(string name, string content)
            {
                this.name = name;
                this.content = content;
            }


            //Double constructor
            public Cell(string name, double content)
            {
                this.name = name;
                this.content = content;
            }


            //Formula constructor
            public Cell(string name, Formula content)
            {
                this.name = name;
                this.content = content;
            }

            public object GetContent()
            {
                return this.content;
            }

            public string GetName()
            {
                return this.name;
            }

        }
    }
}