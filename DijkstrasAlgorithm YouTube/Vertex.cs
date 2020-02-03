using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstrasAlgorithm_YouTube
{
    class Vertex
    {
        public string name;
        public int status;
        public int predecessor;
        public int pathLength;

        public Vertex(string name)
        {
            this.name = name;
        }
    }
}
