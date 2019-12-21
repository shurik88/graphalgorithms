using Graphs.Model;
using System.Collections.Generic;

namespace Graphs.Algorithms.Extensions
{
    /// <summary>
    /// Компонент связности (кластер)
    /// </summary>
    public class ConnectiveComponent
    {
        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<Vertex> Vertices { get; set; }
    }
}
