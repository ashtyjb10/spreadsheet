using System;
using System.Collections;
using System.Collections.Generic;
using Dependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyGraphTestCases
{
    [TestClass]
    public class UnitTest1
    {
        /*This test case will cover the simple addition of a single dependency, removal, and just creating a graph.
         */
        [TestMethod]
        public void TestSize1()
        {
            DependencyGraph graph = new DependencyGraph();
            Assert.AreEqual(0, graph.Size);

            graph.AddDependency("Stand","Tall");
            Assert.AreEqual(1, graph.Size);

            graph.RemoveDependency("Stand", "Tall");
            Assert.AreEqual(0, graph.Size);
        }

        /*This method tests the size of a graph after two identical depencecies are added, testing to see
         * if there is nothing that changes.
         */
        [TestMethod]
        public void TestSize2()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("food", "water");
            Assert.AreEqual(1, graph.Size);
        }

        /*Tests size after two dependencies are added with the same dependent.
         */
        [TestMethod]
        public void TestSize3()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("foods", "water");
            Assert.AreEqual(2, graph.Size);
        }

        //Tests size after replaceDepenents is called
        [TestMethod]
        public void TestSize4()
        {
            List<string> senses = new List<string>();
            senses.Add("taste");
            senses.Add("touch");
            senses.Add("smell");
            senses.Add("feel");
            senses.Add("see");

            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("food", "waters");
            graph.AddDependency("Spice", "waters");
            graph.ReplaceDependents("food", senses);
            Assert.AreEqual(6, graph.Size);
        }
        //Tests size after replaceDependees is called
        [TestMethod]
        public void TestSize5()
        {
            List<string> senses = new List<string>();
            senses.Add("taste");
            senses.Add("touch");
            senses.Add("smell");
            senses.Add("feel");
            senses.Add("see");

            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("catfish", "water");
            graph.AddDependency("food", "waters");
            graph.ReplaceDependees("water", senses);
            Assert.AreEqual(6, graph.Size);
        }

        /*Tests to ensure that the replace dependents method correctly removes the old and adds the new dependents.
         */
        [TestMethod]
        public void TestReplaceDependency1()
        {
            List<string> senses = new List<string>();
            List<string> storedDependents = new List<string>();

            senses.Add("taste");
            senses.Add("touch");
            senses.Add("smell");
            senses.Add("feel");
            senses.Add("see");

            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("food", "waters");
            graph.ReplaceDependents("food", senses);

            foreach (string dependent in graph.GetDependents("food"))
            {
                storedDependents.Add(dependent);
            }

            for (int index = 0; index < senses.Count; index++)
            {
                if (!(senses[index]).Equals(storedDependents[index]))
                {
                    Assert.Fail();
                }
            }
        }

        /*Tests to see if the replace depenees method removes the old and adds the new depenencies correclty
         * 
         */
        [TestMethod]
        public void TestReplaceDependency2()
        {
            List<string> senses = new List<string>();
            List<string> storedDependees = new List<string>();

            senses.Add("taste");
            senses.Add("touch");
            senses.Add("smell");
            senses.Add("feel");
            senses.Add("see");

            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("food", "water");
            graph.ReplaceDependees("water", senses);

            foreach (string dependee in graph.GetDependees("water"))
            {
                storedDependees.Add(dependee);
            }

            for (int index = 0; index < senses.Count; index++)
            {
                if (!(senses[index]).Equals(storedDependees[index]))
                {
                    Assert.Fail();
                }
            }
        }

        /*Tests to see if adding and removing a large list of dependencies is efficient and possible.
         */
        [TestMethod]
        public void StressTestAddRemove1()
        {
            List<string> correctDependent = new List<string>();
            List<string> correctDependee = new List<string>();
            List<string> storedDependent = new List<string>();
            List<string> storedDependee = new List<string>();


            DependencyGraph graph = new DependencyGraph();

            for (int index = 0; index <= 100_000; index++)
            {
                string random1 = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);
                string random2 = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);

                graph.AddDependency(random1, random2);

                correctDependent.Add(random2);
                correctDependee.Add(random1);
            }

            for(int index = 0; index <= 100_000; index++)
            {
                if(index == 100_000 % 50)
                {
                    graph.RemoveDependency("not a real", "dependency");
                }

                graph.RemoveDependency(correctDependee[index],correctDependent[index]);
            }

            Assert.AreEqual(0, graph.Size);
        }

        /*Tests adding a large number of dependencies using one single depenedee for efficiency
         */
        [TestMethod]
        public void StressTestAddRemove2()
        {
            List<string> correctDependent = new List<string>();
            List<string> correctDependee = new List<string>();
            List<string> storedDependent = new List<string>();
            List<string> storedDependee = new List<string>();


            DependencyGraph graph = new DependencyGraph();

            for (int index = 0; index <= 100_000; index++)
            {
                string random1 = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);
                string random2 = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);

                graph.AddDependency("fish", random2);

                correctDependent.Add(random2);
                correctDependee.Add(random1);
            }

            Assert.AreEqual(100_001, graph.Size);
        }

        //Tests the has dependency methods after addition and removal
        [TestMethod]
        public void TestHasDependence1()
        {

            DependencyGraph graph = new DependencyGraph();

            if(graph.HasDependents("Teeth"))
            {
                Assert.Fail();
            }
            if (graph.HasDependees("Space"))
            {
                Assert.Fail();
            }

            //addition
            graph.AddDependency("Teeth", "Space");

            if (graph.HasDependents("gerr"))
            {
                Assert.Fail();
            }

            if (!graph.HasDependents("Teeth"))
            {
                Assert.Fail();
            }
            if (!graph.HasDependees("Space"))
            {
                Assert.Fail();
            }

            //removal
            graph.RemoveDependency("Teeth", "Space");

            if (graph.HasDependents("Teeth"))
            {
                Assert.Fail();
            }
            if (graph.HasDependees("Space"))
            {
                Assert.Fail();
            }
        }
    }
}
