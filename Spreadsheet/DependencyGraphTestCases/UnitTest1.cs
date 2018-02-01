using System;
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

    }
}
