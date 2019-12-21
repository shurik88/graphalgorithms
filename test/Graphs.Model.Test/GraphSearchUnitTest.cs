using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Graphs.Model.Test
{
    [TestClass]
    public class GraphSearchUnitTest
    {
        [TestMethod]
        public void AdjacencyGraphBreadthFirstSearchTest()
        {
            var graph = new AdjacencyLists.Graph();
            BreadthFirstSearch(graph);
        }

        [TestMethod]
        public void AdjacencyGraphDeepthFirstSearchTest()
        {
            var graph = new AdjacencyLists.Graph();
            DeepthFirstSearch(graph);
        }

        private static void BuildGraph1(IGraph graph)
        {
            //var vertextId = new IntIdGenerator();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddVertex(7);
            graph.AddVertex(8);
            graph.AddVertex(9);
            graph.AddVertex(10);
            graph.AddVertex(11);

            var edgeId = new IntIdGenerator();
            graph.AddEdge(1, 2, edgeId.Next);
            graph.AddEdge(1, 3, edgeId.Next, EdgeDirectionType.Directed);
            graph.AddEdge(1, 4, edgeId.Next, EdgeDirectionType.Directed);
            graph.AddEdge(1, 5, edgeId.Next);
            graph.AddEdge(5, 8, edgeId.Next);
            graph.AddEdge(5, 9, edgeId.Next);
            graph.AddEdge(8, 10, edgeId.Next);
            graph.AddEdge(8, 11, edgeId.Next, EdgeDirectionType.Directed);
            graph.AddEdge(8, 9, edgeId.Next, EdgeDirectionType.Directed);
            graph.AddEdge(2, 6, edgeId.Next);
            graph.AddEdge(4, 8, edgeId.Next);
            graph.AddEdge(3, 7, edgeId.Next, EdgeDirectionType.Directed);
            graph.AddEdge(7, 8, edgeId.Next, EdgeDirectionType.Directed);
        }

        private static void BreadthFirstSearch(IGraph graph)
        {
            BuildGraph1(graph);

            Assert.ThrowsException<ArgumentException>(() => graph.BreadthFirstSearch(new Vertex { Id = -1 }).ToList(), "unknown vertex");

            var pathFrom1 = graph.BreadthFirstSearch(new Vertex { Id = 1 }).ToList();
            Assert.IsTrue(pathFrom1.Select(x => x.Id).Distinct().Count() == pathFrom1.Count(), "unique vertex ids from 1");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom1.Count(), "all vertices found from 1");

            var i = 0;
            Assert.AreEqual(1, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(2, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(3, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(4, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(5, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(6, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(7, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(8, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(9, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(10, pathFrom1[i].Id, $"path elem number {i++}");
            Assert.AreEqual(11, pathFrom1[i].Id, $"path elem number {i++}");


            var pathFrom5 = graph.BreadthFirstSearch(new Vertex { Id = 5 }).ToList();
            Assert.IsTrue(pathFrom5.Select(x => x.Id).Distinct().Count() == pathFrom5.Count(), "unique vertex ids from 5");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom5.Count(), "all vertices found from 5");

            i = 0;
            Assert.AreEqual(5, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(1, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(8, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(9, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(2, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(3, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(4, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(10, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(11, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(6, pathFrom5[i].Id, $"path elem number {i++}");
            Assert.AreEqual(7, pathFrom5[i].Id, $"path elem number {i++}");

            var pathFrom10 = graph.BreadthFirstSearch(new Vertex { Id = 10 }).ToList();
            Assert.IsTrue(pathFrom10.Select(x => x.Id).Distinct().Count() == pathFrom10.Count(), "unique vertex ids from 10");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom10.Count(), "all vertices found from 10");

            i = 0;
            Assert.AreEqual(10, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(8, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(5, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(11, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(9, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(4, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(1, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(2, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(3, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(6, pathFrom10[i].Id, $"path elem number {i++}");
            Assert.AreEqual(7, pathFrom10[i].Id, $"path elem number {i++}");

            var pathFrom11 = graph.BreadthFirstSearch(new Vertex { Id = 11 }).ToList();
            Assert.AreEqual(1, pathFrom11.Count(), "one vertex found from 11");

        }

        private static void DeepthFirstSearch(IGraph graph)
        {
            BuildGraph1(graph);

            Assert.ThrowsException<ArgumentException>(() => graph.DeepthFirstSearch(new Vertex { Id = -1 }).ToList(), "unknown vertex");

            var pathFrom1 = graph.DeepthFirstSearch(new Vertex { Id = 1 }).ToList();
            Assert.IsTrue(pathFrom1.Select(x => x.Id).Distinct().Count() == pathFrom1.Count(), "unique vertex ids from 1");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom1.Count(), "all vertices found from 1");

            var i = 0;
            Assert.AreEqual(1, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(2, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(6, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(3, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(7, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(8, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(5, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(9, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(10, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(11, pathFrom1[i].Id, $"path elem number {i++} from 1");
            Assert.AreEqual(4, pathFrom1[i].Id, $"path elem number {i++} from 1");


            var pathFrom5 = graph.DeepthFirstSearch(new Vertex { Id = 5 }).ToList();
            Assert.IsTrue(pathFrom5.Select(x => x.Id).Distinct().Count() == pathFrom5.Count(), "unique vertex ids from 5");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom5.Count(), "all vertices found from 5");

            i = 0;
            Assert.AreEqual(5, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(1, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(2, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(6, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(3, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(7, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(8, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(10, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(11, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(9, pathFrom5[i].Id, $"path elem number {i++} from 5");
            Assert.AreEqual(4, pathFrom5[i].Id, $"path elem number {i++} from 5");

            var pathFrom10 = graph.BreadthFirstSearch(new Vertex { Id = 10 }).ToList();
            Assert.IsTrue(pathFrom10.Select(x => x.Id).Distinct().Count() == pathFrom10.Count(), "unique vertex ids from 10");
            Assert.AreEqual(graph.Vertices.Count(), pathFrom10.Count(), "all vertices found from 10");

            var pathFrom11 = graph.BreadthFirstSearch(new Vertex { Id = 11 }).ToList();
            Assert.AreEqual(1, pathFrom11.Count(), "one vertex found from 11");

        }
    }
}
