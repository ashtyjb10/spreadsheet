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
        [TestMethod]
        public void TestSize1()
        {
            DependencyGraph graph = new DependencyGraph();

            Assert.AreEqual(graph.Size, 0);
        }

        [TestMethod]
        public void TestSize2()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            Assert.AreEqual(1,  graph.Size);
        }

        [TestMethod]
        public void TestSize3()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("food", "water");
            Assert.AreEqual(1, graph.Size);
        }
        [TestMethod]
        public void TestSize4()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("foods", "water");
            Assert.AreEqual(2, graph.Size);
        }

        [TestMethod]
        public void TestSize5()
        {
            DependencyGraph graph = new DependencyGraph();
            graph.AddDependency("food", "water");
            graph.AddDependency("foods", "water");
            graph.RemoveDependency("foods", "water");
            Assert.AreEqual(1, graph.Size);
        }

        [TestMethod]
        public void TestSize6()
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

        [TestMethod]
        public void TestSize7()
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
            graph.ReplaceDependees("water", senses);
            Assert.AreEqual(6, graph.Size);
        }


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
    }
}
