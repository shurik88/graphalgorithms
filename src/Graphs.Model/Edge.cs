namespace Graphs.Model
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Номер
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ориентация
        /// </summary>
        public EdgeDirectionType DirectionType { get; set; }
    }
}
