using Graphs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Graphs.Algorithms.Extensions
{
    /// <summary>
    /// Методы расширения для работы с <seealso cref="IGraph"/>
    /// </summary>
    public static class IGraphExtensions
    {
        /// <summary>
        /// Получение компонент связности для неориентированного графа
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <returns>список компонент</returns>
        public static IEnumerable<ConnectiveComponent> GetConnectiveComponents(this IGraph graph)
        {
            if (graph.DirectionType == EdgeDirectionType.Directed)
                throw new InvalidOperationException("Current graph is directed");

            var visited = new HashSet<int>();
            
            foreach(var vertex in graph.Vertices)
            {
                if (visited.Contains(vertex.Id))
                    continue;
                var vertices = graph.BreadthFirstSearch(vertex).ToList();
                var cluster = new Collection<Vertex>();
                foreach(var found in vertices)
                {
                    if (visited.Contains(found.Id))
                        continue;

                    cluster.Add(found);
                    visited.Add(found.Id);
                }
                if (cluster.Any())
                    yield return new ConnectiveComponent { Vertices = cluster };
            }
        }
    }
}
