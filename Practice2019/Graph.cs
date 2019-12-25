using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice2019
{
    public static class ListExtensions
    {
        public static GraphFacade.Graph Dequeue(this LinkedList<GraphFacade.Graph> queue)
        {
            GraphFacade.Graph retVal = queue.First();
            queue.RemoveFirst();
            return retVal;
        }
    }

    public class GraphFacade
    {
        Dictionary<int, Graph> nodeDictionary = new Dictionary<int, Graph>();

        public Graph Root = null;

        public class Graph
        {
            int id;
            LinkedList<Graph> adjacent = new LinkedList<Graph>();

            public Graph(int id)
            {
                this.id = id;
            }

            public void AddItemToAdjacentList(Graph child)
            {
                adjacent.AddLast(child);
            }

            public LinkedList<Graph> GetChildren()
            {
                return adjacent;
            }

            public int GetValue()
            {
                return this.id;
            }

            public Graph Clone()
            {
                Graph clone = new Graph(this.id);
                return clone;
            }
        }

        public void PrintGraph()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Cannot clone a graph with root node as null.");
            }

            HashSet<int> visited = new HashSet<int>();
            LinkedList<Graph> queue = new LinkedList<Graph>();
            queue.AddLast(Root);
            while (queue.Any())
            {
                Graph nodeToVisit = queue.Dequeue();
                if (visited.Contains(nodeToVisit.GetValue()))
                {
                    continue;
                }

                visited.Add(nodeToVisit.GetValue());

                Console.Write($"Node: {nodeToVisit.GetValue()} -> [");
                foreach (Graph child in nodeToVisit.GetChildren())
                {
                    Console.Write($"{child.GetValue()},");
                    queue.AddLast(child);
                }

                Console.WriteLine($"]");
            }
        }

        public Graph GetOrCreateGraphNode(int id)
        {
            if (nodeDictionary.ContainsKey(id))
            {
                return GetNode(id);
            }

            return CreateGraphNode(id);
        }

        public Graph CreateGraphNode(int id)
        {
            Graph node = new Graph(id);
            if (Root == null)
            {
                Root = node;
            }

            AddToDictionary(id, node);
            return node;
        }

        // Facebook phone round.
        public GraphFacade Clone()
        {
            GraphFacade newGraph = new GraphFacade();
            this.CloneUsingBFSOnePass(newGraph);
            return newGraph;
        }

        private void CloneUsingBFS(GraphFacade destination)
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Cannot clone a graph with root node as null.");
            }

            HashSet<int> visited = new HashSet<int>();
            LinkedList<Graph> queue = new LinkedList<Graph>();
            queue.AddLast(Root);
            while(queue.Any())
            {
                Graph nodeToVisit = queue.Dequeue();
                if (visited.Contains(nodeToVisit.GetValue()))
                {
                    continue;
                }

                visited.Add(nodeToVisit.GetValue());

                destination.CreateGraphNode(nodeToVisit.GetValue());
                foreach (Graph child in nodeToVisit.GetChildren())
                {
                    queue.AddLast(child);
                }
            }

            // build adjacent list
            visited.Clear();
            queue.AddLast(Root);
            while (queue.Any())
            {
                Graph nodeToVisit = queue.Dequeue();
                if (visited.Contains(nodeToVisit.GetValue()))
                {
                    continue;
                }

                visited.Add(nodeToVisit.GetValue());
                Graph clonedNode = destination.GetNode(nodeToVisit.GetValue());

                foreach (Graph child in nodeToVisit.GetChildren())
                {
                    clonedNode.AddItemToAdjacentList(destination.GetNode(child.GetValue()));
                    queue.AddLast(child);
                }
            }
        }

        private void CloneUsingBFSOnePass(GraphFacade destination)
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Cannot clone a graph with root node as null.");
            }

            HashSet<int> visited = new HashSet<int>();
            LinkedList<Graph> queue = new LinkedList<Graph>();
            queue.AddLast(Root);
            while (queue.Any())
            {
                Graph nodeToVisit = queue.Dequeue();
                if (visited.Contains(nodeToVisit.GetValue()))
                {
                    continue;
                }

                visited.Add(nodeToVisit.GetValue());

                Graph node = destination.GetOrCreateGraphNode(nodeToVisit.GetValue());
                foreach (Graph child in nodeToVisit.GetChildren())
                {
                    queue.AddLast(child);
                    node.AddItemToAdjacentList(destination.GetOrCreateGraphNode(child.GetValue()));
                }
            }
        }

        public void AddToDictionary(int id, Graph node)
        {
            if (nodeDictionary.ContainsKey(id))
            {
                if (nodeDictionary[id] == node)
                {
                    return;
                }

                nodeDictionary.Remove(id);
            }

            nodeDictionary.Add(id, node);
        }

        public void AddAdjacentNode(int source, int child)
        {
            Graph sourceNode = GetNode(source);
            Graph childNode = GetNode(child);
            AddAdjacentNode(sourceNode, childNode);
        }

        public static void AddAdjacentNode(Graph source, Graph child)
        {
            if (source == null || child == null)
            {
                throw new InvalidOperationException("source or child items not found");
            }

            source.AddItemToAdjacentList(child);
        }

        private Graph GetNode(int id)
        {
            if (nodeDictionary.ContainsKey(id))
            {
                return nodeDictionary[id];
            }

            return null;
        }

        public bool HasPathDFS(int source, int destination)
        {
            HashSet<int> visited = new HashSet<int>();
            return hasPathDFS(GetNode(source), GetNode(destination), visited);
        }

        public bool HasPathBFS(int source, int destination)
        {
            HashSet<int> visited = new HashSet<int>();
            LinkedList<Graph> queue = new LinkedList<Graph>();
            return hasPathBFS(GetNode(source), GetNode(destination), visited, queue);
        }

        private bool hasPathDFS(Graph source, Graph destination, HashSet<int> visited)
        {
            // To handle cycles
            if (visited.Contains(source.GetValue()))
            {
                return false;
            }

            visited.Add(source.GetValue());

            if (source == destination)
            {
                return true;
            }

            foreach (Graph child in source.GetChildren())
            {
                // Donot return false, in that way we can exhaust 
                // all the opportunities to search across all children.
                if (hasPathDFS(child, destination, visited))
                {
                    return true;
                }
            }

            return false;
        }

        private bool hasPathBFS(Graph source, Graph destination, HashSet<int> visited, LinkedList<Graph> queue)
        {
            queue.AddLast(source);
            while (queue.Any())
            {
                Graph nextGraphNode = queue.Dequeue();

                if (nextGraphNode == destination)
                {
                    return true;
                }

                // To handle cycles.
                if (visited.Contains(nextGraphNode.GetValue()))
                {
                    continue;
                }
                visited.Add(nextGraphNode.GetValue());

                foreach (Graph child in nextGraphNode.GetChildren())
                {
                    if (child == null) continue;
                    queue.AddLast(child);
                }
            }

            return false;
        }
    } 
}
