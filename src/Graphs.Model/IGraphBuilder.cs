using System.Collections.Generic;

namespace Graphs.Model
{
    /// <summary>
    /// Построитель графа
    /// </summary>
    public interface IGraphBuilder
    {
        /// <summary>
        /// Вершины
        /// </summary>
        IEnumerable<Vertex> Vertices { get; }

        /// <summary>
        /// Ребра
        /// </summary>
        IEnumerable<Edge> Edges { get; }

        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertex">Вершина</param>
        void AddVertex(Vertex vertex);

        /// <summary>
        /// Удаление вершины
        /// </summary>
        /// <param name="vertex"></param>
        void RemoveVertex(Vertex vertex);

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="from">Начальная вершина ребра</param>
        /// <param name="to">Конечная вершина ребра</param>
        /// <param name="edge">Ребро</param>
        /// <remarks>Неориентированное ребро добавляет один раз, при этом не важно в каком порядке указывать From и To</remarks>
        void AddEdge(Vertex from, Vertex to, Edge edge);

        /// <summary>
        /// Удаление ребра
        /// </summary>
        /// <param name="edge">Ребро</param>
        void RemoveEdge(Edge edge);
    }
}
