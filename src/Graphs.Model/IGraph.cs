namespace Graphs.Model
{
    public interface IGraph : IGraphBuilder, IGraphSearch
    {
        /// <summary>
        /// Ориентация графа
        /// </summary>
        EdgeDirectionType DirectionType { get; }
    }
}
