using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Proiect_Algoritmica.Views;

namespace Proiect_Algoritmica.Scripts.Graphs
{
    public class Node
    {
        public Node()
        {
            
        }

        public Node(Views.Node nodeUi, string nodeName)
        {
            NodeUi = nodeUi;
            NodeName = nodeName;

            Position = new Point(NodeUi.Margin.Left+NodeUi.Width/2f,NodeUi.Margin.Top+NodeUi.Height/2f);
        }
        [JsonIgnore]
        public Views.Node NodeUi { get; set; }
        /// <summary>
        /// JsonSerializerInfo
        /// </summary>
        public Point Position { get; set; }
        public string NodeName { get; set; }
    }
}
