using System.Collections.Generic;

namespace Graphs.Model
{
    /// <summary>
    /// Примитива поиска в графе
    /// </summary>
    public interface IGraphSearch
    {
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <param name="start">Стартовая вершина</param>
        /// <returns>Путь следования от стартовой вершины</returns>
        IEnumerable<Vertex> BreadthFirstSearch(Vertex start);

        /// <summary>
        /// Поиск в глубину
        /// </summary>
        /// <param name="start">Стартовая вершина</param>
        /// <returns>Путь следования от стартовой вершины</returns>
        IEnumerable<Vertex> DeepthFirstSearch(Vertex start);
    }
}
