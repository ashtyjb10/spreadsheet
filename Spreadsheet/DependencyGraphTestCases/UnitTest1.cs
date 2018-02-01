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
            graph.ReplaceDependents("food", senses);
            Assert.AreEqual(5, graph.Size);
        }

    }
}
