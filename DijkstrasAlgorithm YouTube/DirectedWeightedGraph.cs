using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstrasAlgorithm_YouTube
{
    class DirectedWeightedGraph
    {
        public readonly int MAX_VERTICES = 30;

        int n;

        internal void InsertVertex(string vertexName)
        {
            for (int i = 0; i < vertexList.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(vertexList[i]?.name))
                {
                    vertexList[i] = new Vertex(vertexName);
                    n++;
                    break;
                }
            }
        }

        internal void InsertEdge(string vertex1, string vertex2, int weight)
        {
            var indexOfV1 = GetIndex(vertex1);
            var indexOfV2 = GetIndex(vertex2);

            adj[indexOfV1, indexOfV2] = weight;
        }

        int e;
        int[,] adj;
        Vertex[] vertexList;

        private readonly int TEMPORARY = 1;
        private readonly int PERMANENT = 2;
        private readonly int NIL = -1;
        private readonly int INFINITY = 99999;

        public DirectedWeightedGraph()
        {
            adj = new int[MAX_VERTICES, MAX_VERTICES];

            vertexList = new Vertex[MAX_VERTICES];
        }

        private void Dijkstra(int s)
        {
            int v, c;

            for (v = 0; v < n; v++)
            {
                vertexList[v].status = TEMPORARY;
                vertexList[v].pathLength = INFINITY;
                vertexList[v].predecessor = NIL;
            }

            vertexList[s].pathLength = 0;

            while (true)
            {
                c = TempVertexMinPL();

                if (c == NIL)
                {
                    return;
                }

                vertexList[c].status = PERMANENT;

                for (v = 0; v < n; v++)
                {
                    if (IsAdjacent(c,v) && vertexList[v].status == TEMPORARY)
                    {
                        if (vertexList[c].pathLength + adj[c,v] < vertexList[v].pathLength)
                        {
                            vertexList[v].predecessor = c;
                            vertexList[v].pathLength = vertexList[c].pathLength + adj[c, v];
                        }
                    }
                }
            }
        }

        private bool IsAdjacent(int c, int v)
        {
            if (adj[c,v] != 0 )
            {
                return true;
            }

            return false;
        }

        private int TempVertexMinPL()
        {
            int min = INFINITY;
            int x = NIL;

            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == TEMPORARY && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x;
        }

        public void FindPaths(string source)
        {
            int s = GetIndex(source);

            Dijkstra(s);

            Console.WriteLine("Source Vertex : " + source + "\n");

            for (int v = 0; v < n; v++)
            {
                Console.WriteLine("Destination Vertex : " + vertexList[v].name);
                if (vertexList[v].pathLength == INFINITY)
                {
                    Console.WriteLine("There is no path from " + source + " to vertex " + vertexList[v].name + "\n");
                }
                else
                {
                    FindPath(s, v);
                }
            }
        }

        private void FindPath(int s, int v)
        {
            int i, u;
            int[] path = new int[n];
            int sd = 0;
            int count = 0;

            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].predecessor;
                sd += adj[u, v];
                v = u;
            }
            count++;
            path[count] = s;

            Console.Write("Shortest Path is : ");
            for (i = count; i>= 1 ; i--)
            {
                Console.Write(path[i] + " ");
            }
            Console.WriteLine("\n Shortest distance is : " + sd + "\n");
        }

        private int GetIndex(string s)
        {
            for (int i = 0; i < n; i++)
            {
                if (s.Equals(vertexList[i].name))
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Invalid Vertex!");
        }
    }
}
