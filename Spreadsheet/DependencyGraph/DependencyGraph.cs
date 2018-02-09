// Skeleton implementation written by Joe Zachary for CS 3500, January 2018.
//Completed by Nathan Herrmann for CS 3500, January 2018 final commit.
using System;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    public class DependencyGraph
    {
        //Object variables
        private Dictionary<string, DependencyNode> dependentBackingDictionary;
        private Dictionary<string, DependencyNode> dependeeBackingDictionary;
        private int dependencySize = 0;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            //Two dictionaries are used to store dependent and dependee nodes.  Each node uses a HashSet to store dependents and dependees node.
            this.dependentBackingDictionary = new Dictionary<string, DependencyNode>();
            this.dependeeBackingDictionary = new Dictionary<string, DependencyNode>();
        }

        /// <summary>
        /// Second constructor that takes a dependency graph.  The resulting dependency graph will be a dependency graph that
        /// is identical to the graph that is passed to the constructor, thouhg it is a different, independent object.
        /// </summary>
        /// <param name="graph"></param>
        public DependencyGraph(DependencyGraph graph)
        {
            if(graph == null)
            {
                throw new ArgumentNullException("Graph cannot be null");
            }

            //Initialize the variables
            this.dependentBackingDictionary = new Dictionary<string, DependencyNode>();
            this.dependeeBackingDictionary = new Dictionary<string, DependencyNode>();

            //For each key in the backing dictionary of the graph that is passed
            foreach (string dependentKey in graph.dependeeBackingDictionary.Keys)
            {
                //Get each dependent of that key.
                graph.dependeeBackingDictionary.TryGetValue(dependentKey, out DependencyNode node);

                //Create a base string to make the stored string
                string firstNew = "";
                foreach (string dependentValue in node.getDependencies())
                {
                    //Adds a new dependency equal to the depenency of the other graph.
                    AddDependency(firstNew + dependentValue, dependentKey);
                }
            }
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return this.dependencySize; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.  Throws ArgumentNullException
        /// in the case that s == null;
        /// </summary>
        public bool HasDependents(string s)
        {
            if(s == null)
            {
                throw new ArgumentNullException("String cannot be null");
            }

            //If the key is contained in the dependent backing array, check to see if it has values in the HashTable.
            if (dependentBackingDictionary.TryGetValue(s, out DependencyNode dependee))
            {
                //If there is a dependency stored
                if (dependee.getDependencies().Length > 0) {
                    return true;
                }
                else
                {
                    //Otherwise return false
                    return false;
                }
            }
            else
            {
                //Otherwise return false
                return false;
            }
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null. throws ArgumentNullException
        /// in the case that s == null
        /// </summary>
        public bool HasDependees(string s)
        {
            //If s is null throw an exception
            if (s == null)
            {
                throw new ArgumentNullException("Dependee must not be null");
            }

            //If the key is contained in the dependent backing array, check to see if it has values in the HashTable.
            if (dependeeBackingDictionary.TryGetValue(s, out DependencyNode dependent))
            {
                //If there is a dependency stored
                if (dependent.getDependencies().Length > 0)
                {
                    return true;
                }
                else
                {
                    //Otherwise return false
                    return false;
                }
            }
            else
            {
                //otherwise return false
                return false;
            }
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.  throws ArgumentNullException
        /// in the case that s == null
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            //If s is null throw exception
            if (s == null)
            {
                throw new ArgumentNullException("Dependent must not be null");
            }
            //Check if there is a value stored
            if (dependentBackingDictionary.TryGetValue(s, out DependencyNode dependee))
            {
                //If there is return the hashset as IEnumerable<string>
                return dependee.getDependencies();
            }
            else
            {
                //Or return and empty list
                return new List<string>();
            }
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.  throws ArgumentNullException
        /// in the case that s == null
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //If s is null throw an exception
            if (s == null)
            {
                throw new ArgumentNullException("Dependee must not be null");
            }
            //Check if the value is stored
            if (dependeeBackingDictionary.TryGetValue(s, out DependencyNode dependee))
            {
                //If there is then return the hashset as an IEnumerable<string>
                return dependee.getDependencies();
            }
            else
            { 
                //Or return and empty list
                return new List<string>();
            }
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.  throws ArgumentNullException in the
        /// case that s == null or t == null
        /// </summary>
        public void AddDependency(string s, string t)
        {
            if(s == null || t == null)
            {
                throw new ArgumentNullException("Parameters cannot be null");
            }
            //check if the dependency node is stored
            if (dependentBackingDictionary.TryGetValue(s, out DependencyNode dependency))
            {
                //If the dependyncy exists
                if (dependency.hasDependency(t))
                {
                    //Do nothing
                }
                else
                {
                    //If the node is already in the list and the dependency does not exist
                    dependency.addDependency(t);
                    this.dependencySize++;
                }
            }
            //If not, make a new dependency
            else
            {
                DependencyNode newNode = new DependencyNode(s);
                newNode.addDependency(t);
                dependentBackingDictionary.Add(s, newNode);
                this.dependencySize++;
            }

            //check if the dependency node is stored
            if (dependeeBackingDictionary.TryGetValue(t, out DependencyNode dependee))
            {
                //If the dependyncy exists
                if (dependee.hasDependency(s))
                {
                    //Do nothing
                }
                else
                {
                    //If the node is already in the list and the dependency does not exist
                    dependee.addDependency(s);
                }
            }
            //If not, make a new dependency, add the dependency, and store it
            else
            {
                DependencyNode newNode = new DependencyNode(t);
                newNode.addDependency(s);
                dependeeBackingDictionary.Add(t, newNode);
            }
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.  throws ArgumentNullException in the case
        /// that s == null or t == null
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            //If s or t is null the throw an exception
            if (s == null || t == null)
            {
                throw new ArgumentNullException("Removed dependecy cannot be null");
            }

            //Check for dependent value
            if (dependentBackingDictionary.TryGetValue(s, out DependencyNode dependent))
            {
                //If it exists, remove the dependency and update the dependency count
                if (dependent.hasDependency(t))
                {
                    dependent.removeDependency(t);
                    this.dependencySize--;
                }
                else
                {
                    //Do nothing
                }
            }
            else
            {
                //Do nothing
            }

            //If the value exists in the backing dictionary
            if (dependeeBackingDictionary.TryGetValue(t, out DependencyNode dependee))
            {
                //If the dependee exists.
                if (dependee.hasDependency(s))
                {
                    //Remove the dependency
                    dependee.removeDependency(s);
                }
                else
                {
                    //Do nothing
                }
            }
            else
            {
                //Do nothing
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and newDependents != null.  throws ArgumentNullException in the
        /// case that s == null or newDependents == null or a string in newDependent == null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            //If s is null. throw an exception
            if (s == null || newDependents == null)
            {
                throw new ArgumentNullException("Dependent cannot be null");
            }

            //Check if key exists.
            if (dependentBackingDictionary.TryGetValue(s, out DependencyNode dependee))
            {
                //If it does, dependency must be removed first.
                foreach(string toRemove in dependee.getDependencies())
                {
                    RemoveDependency(s,toRemove);
                }
            }
            //Add the new dependencies
            foreach (string addDep in newDependents)
            {
                //If the new dependency is null, throw an exception
                if(addDep == null)
                {
                    throw new ArgumentNullException("Dependee cannot be null");
                }
                AddDependency(s, addDep);
            }
        }


        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.  throws ArgumentNullException in the 
        /// case that t == null or newdependees == null or a string in newDependees == null
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees)
        {
            //if t is null throw an exception
            if (t == null || newDependees == null)
            {
                throw new ArgumentNullException("Dependecy cannot be null");
            }

            //Check if key exists.
            if (dependeeBackingDictionary.TryGetValue(t, out DependencyNode dependent))
            {
                //For each dependee, check for a value
                foreach(string toRemove in dependent.getDependencies())
                {
                    RemoveDependency(toRemove, t);
                }
            }
            //Add the new dependencies
            foreach (string addDep in newDependees)
            {
                //If the new dependency is null, throw an exception
                if (addDep == null)
                {
                    throw new ArgumentNullException("Dependee cannot be null");
                }
                AddDependency(addDep, t);
            }
        }

        ///<summary>
        ///This oject represents either a dependent or a dependee relationship.  Each object stores a hashset which
        ///epresents the different dependents or dependees that are associated with the particular string.
        ///Wether the string is a dependent or dependee is shows by which dictionary it is stored in in the dependency graph.
        ///</summary> 
        private class DependencyNode
        {
            //Object variables
            private HashSet<string> dependers = new HashSet<string>();
            private string dependencyString;
            private int size;

            //Constructor sets the dependencyString to the given value.  Value is used as a key in the dependency graph.
            public DependencyNode(string dependentString)
            {
                this.dependencyString = dependentString;
            }

            ///<summary>
            ///Adds a depender to the DependencyNode list.
            ///</summary>
            public void addDependency(string dependee)
            {
                this.dependers.Add(dependee);
                this.size++;
            }

            ///<summary>
            ///Removes a depender from the Dependency list.
            ///</summary>
            public void removeDependency(string dependee)
            {
                this.dependers.Remove(dependee);
                this.size--;
            }

            ///<summary>
            ///Check if the dependency has a specific depender
            ///</summary>
            public bool hasDependency(string depender)
            {
                return this.dependers.Contains(depender);
            }

            ///<summary>
            ///Returns the number of dependers in the stored list. 
            ///</summary>
            public int getSize()
            {
                return size;
            }
            ///<summary>
            ///Returns the list of dependers to the caller as a list.
            ///</summary>
            public string[] getDependencies()
            {
                string[] toReturn = new string[dependers.Count];
                dependers.CopyTo(toReturn, 0);
                return toReturn;

            }
        }
    }
}
