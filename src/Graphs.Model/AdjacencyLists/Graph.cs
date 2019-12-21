using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Model.AdjacencyLists
{

    public class Graph : IGraph
    {
        private readonly Dictionary<int, AdjacencyVertex> _vertices;
        private readonly Dictionary<int, AdjacencyEdge> _edges;
        public Graph(EdgeDirectionType directionType = EdgeDirectionType.Undirected)
        {
            DirectionType = directionType;
            _vertices = new Dictionary<int, AdjacencyVertex>();
            _edges = new Dictionary<int, AdjacencyEdge>();
        }

        /// <inheritdoc/>
        public EdgeDirectionType DirectionType { get; }

        public IEnumerable<KeyValuePair<int, int>> GetClosestPaths(Vertex start)
        {
            if (!_vertices.ContainsKey(start.Id))
                throw new ArgumentException("Vertex not exists");

            var visitedVertices = new HashSet<int>();
            var paths = _vertices.ToDictionary(x => x.Key, x => 0);
            var queue = new Queue<Vertex>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                if (visitedVertices.Contains(current.Id))
                    continue;
                visitedVertices.Add(current.Id);
                yield return new KeyValuePair<int, int>(current.Id, paths[current.Id]);
                foreach (var outEdge in _vertices[current.Id].OutComming)
                {
                    queue.Enqueue(outEdge.To.Vertex);
                    if(paths[outEdge.To.Vertex.Id] == 0)
                        paths[outEdge.To.Vertex.Id] = paths[current.Id] + 1;
                }

            }

        }

        /// <inheritdoc/>
        public IEnumerable<Vertex> BreadthFirstSearch(Vertex start)
        {
            if(!_vertices.ContainsKey(start.Id))
                throw new ArgumentException("Vertex not exists");

            var visitedVertices = new HashSet<int>();
            var queue = new Queue<Vertex>();
            queue.Enqueue(start);
            while(queue.Any())
            {
                var current = queue.Dequeue();
                if (visitedVertices.Contains(current.Id))
                    continue;
                visitedVertices.Add(current.Id);
                yield return current;
                foreach (var outEdge in _vertices[current.Id].OutComming)
                    queue.Enqueue(outEdge.To.Vertex);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Vertex> DeepthFirstSearch(Vertex start)
        {
            if (!_vertices.ContainsKey(start.Id))
                throw new ArgumentException("Vertex not exists");

            var visitedVertices = new HashSet<int>();
            var stack = new Stack<Vertex>();
            stack.Push(start);
            while (stack.Any())
            {
                var current = stack.Pop();
                if (visitedVertices.Contains(current.Id))
                    continue;
                visitedVertices.Add(current.Id);
                yield return current;
                foreach (var outEdge in _vertices[current.Id].OutComming.Reverse())
                    stack.Push(outEdge.To.Vertex);
            }
        }

        #region IGraphBuilder Implementation

        /// <inheritdoc/>
        public IEnumerable<Vertex> Vertices => _vertices.Values.Select(x => x.Vertex);

        /// <inheritdoc/>
        public IEnumerable<Edge> Edges => _edges.Values.Select(x => x.Edge);

        /// <inheritdoc/>
        public void AddEdge(Vertex from, Vertex to, Edge edge)
        {
            if (_edges.ContainsKey(edge.Id))
                throw new ArgumentException("Edge already exists");

            if (!_vertices.ContainsKey(from.Id))
                throw new ArgumentException("From vertex not exists");

            if (!_vertices.ContainsKey(to.Id))
                throw new ArgumentException("To vertex not exists");

            if (DirectionType == EdgeDirectionType.Undirected && edge.DirectionType == EdgeDirectionType.Directed)
                throw new InvalidOperationException("Current undirected graph doesn't support directed edges");

            _edges.Add(edge.Id, ConnectVerticesInternal(from, to, edge));
            if (edge.DirectionType == EdgeDirectionType.Undirected)
                ConnectVerticesInternal(to, from, edge);
        }

        private AdjacencyEdge ConnectVerticesInternal(Vertex from, Vertex to, Edge edge)
        {
            var adjacencyFrom = _vertices[from.Id];
            var adjacencyTo = _vertices[to.Id];
            var adjacencyEdge = new AdjacencyEdge { Edge = edge, From = adjacencyFrom, To = adjacencyTo };
            adjacencyEdge.From.OutComming.Add(adjacencyEdge);
            adjacencyEdge.To.InComming.Add(adjacencyEdge);
            return adjacencyEdge;
        }

        /// <inheritdoc/>
        public void AddVertex(Vertex vertex)
        {
            if (_vertices.ContainsKey(vertex.Id))
                throw new ArgumentException("Vertex already exists");

            _vertices.Add(vertex.Id, new AdjacencyVertex { Vertex = vertex });
        }

        /// <inheritdoc/>
        public void RemoveEdge(Edge edge)
        {
            if(!_edges.ContainsKey(edge.Id))
                throw new ArgumentException("Vertex not exists");

            RemoveEdgeInternal(edge);
        }

        private class EdgeEqualityComparer : IEqualityComparer<Edge>
        {
            public bool Equals(Edge x, Edge y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Edge obj)
            {
                return obj.Id;
            }
        }

        /// <inheritdoc/>
        public void RemoveVertex(Vertex vertex)
        {
            if (!_vertices.ContainsKey(vertex.Id))
                throw new ArgumentException("Vertex not exists");

            var adjacencyVertex = _vertices[vertex.Id];
            var outEdges = adjacencyVertex.OutComming.ToList();

            var edges = adjacencyVertex.OutComming.Select(x => x.Edge).Union(adjacencyVertex.InComming.Select(x => x.Edge), new EdgeEqualityComparer()).ToList();
            foreach(var edge in edges)
                RemoveEdgeInternal(edge);

            _vertices.Remove(vertex.Id);
        }

        private void RemoveEdgeInternal(Edge edge)
        {
            var adjacencyEdge = _edges[edge.Id];
            adjacencyEdge.From.OutComming.Remove(adjacencyEdge);
            adjacencyEdge.To.InComming.Remove(adjacencyEdge);
            if (adjacencyEdge.Edge.DirectionType == EdgeDirectionType.Undirected)
            {
                var copyEdge = adjacencyEdge.To.OutComming.First(x => x.Edge.Id == edge.Id);
                adjacencyEdge.To.OutComming.Remove(copyEdge);
                adjacencyEdge.From.InComming.Remove(copyEdge);
            }
            _edges.Remove(edge.Id);
        }

        #endregion
    }
}
