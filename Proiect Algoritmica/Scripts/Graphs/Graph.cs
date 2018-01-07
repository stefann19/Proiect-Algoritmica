using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Proiect_Algoritmica.Scripts.Graphs;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class Graph
    {
        /// <summary>
        /// Json deserialzation
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// New Graph
        /// </summary>
        /// <param name="name"></param>
        public Graph(string name)
        {
            Name = name;
            Nodes = new Dictionary<int, Node>();
            Roads = new HashSet<Road>();
        }

        public string Name { get; set; }

        public Dictionary<int,Node> Nodes { get; set; }

        public HashSet<Road> Roads { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Vector GraphSize { get; set; }
        public Point MinPoint { get; set; }
        [JsonIgnore]
        public GraphEditorEngine GraphEditorEngine { get; set; }
    }
}
