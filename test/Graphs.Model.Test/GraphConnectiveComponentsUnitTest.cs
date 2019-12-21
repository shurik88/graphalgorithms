using Graphs.Algorithms.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Graphs.Model.Test
{
    [TestClass]
    public class GraphConnectiveComponentsUnitTest
    {
        [TestMethod]
        public void AdjacencyGraphUndirectedConnectiveComponentsTest()
        {
            var graph = new AdjacencyLists.Graph(EdgeDirectionType.Undirected);
            UndirectedConnectiveComponentsTest(graph);
        }


        private static void Build3ComponentsUndirectedGraph(IGraphBuilder graph)
        {
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
            graph.AddVertex(12);

            var edgeId = new IntIdGenerator();

            graph.AddEdge(1, 2, edgeId.Next);
            graph.AddEdge(2, 4, edgeId.Next);
            graph.AddEdge(4, 5, edgeId.Next);
            graph.AddEdge(5, 1, edgeId.Next);
            graph.AddEdge(3, 1, edgeId.Next);

            graph.AddEdge(6, 7, edgeId.Next);
            graph.AddEdge(7, 8, edgeId.Next);
            graph.AddEdge(8, 6, edgeId.Next);

            graph.AddEdge(9, 10, edgeId.Next);
            graph.AddEdge(10, 11, edgeId.Next);
            graph.AddEdge(11, 12, edgeId.Next);
            graph.AddEdge(12, 9, edgeId.Next);
        }

        private static void UndirectedConnectiveComponentsTest(IGraph graph)
        {
            Build3ComponentsUndirectedGraph(graph);
            var components = graph.GetConnectiveComponents().ToList();
            Assert.AreEqual(3, components.Count, "components count");
            Assert.AreEqual(graph.Vertices.Count(), components.SelectMany(x => x.Vertices).Select(x => x.Id).Distinct().Count(), "all vertices found in components");

            var componentWithVertex1 = components.First(x => x.Vertices.Any(y => y.Id == 1));
            Assert.AreEqual(5, componentWithVertex1.Vertices.Count(), "vertices from component 1");

            var componentWithVertex6 = components.First(x => x.Vertices.Any(y => y.Id == 6));
            Assert.AreEqual(3, componentWithVertex6.Vertices.Count(), "vertices from component 2");

            var componentWithVertex9 = components.First(x => x.Vertices.Any(y => y.Id == 9));
            Assert.AreEqual(4, componentWithVertex9.Vertices.Count(), "vertices from component 3");
        }
    }
}
