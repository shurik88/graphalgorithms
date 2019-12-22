namespace Graphs.Model
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Создание экземпляра класса <see cref="Edge"/>
        /// </summary>
        public Edge()
        {
            Weight = 1;
        }
        /// <summary>
        /// Номер
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ориентация
        /// </summary>
        public EdgeDirectionType DirectionType { get; set; }

        /// <summary>
        /// Вес
        /// </summary>
        public int Weight { get; set; }
    }
}
