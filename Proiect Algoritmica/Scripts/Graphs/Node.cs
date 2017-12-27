using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Proiect_Algoritmica.Scripts.GraphEditor;
using Proiect_Algoritmica.Views;

namespace Proiect_Algoritmica.Scripts.Graphs
{
    public class Node
    {
        private Point _position;

        public Node()
        {
            Roads = new Dictionary<Node, Road>();

        }

        public Node(Views.Node nodeUi, string nodeName)
        {
            NodeUi = nodeUi;
            NodeName = nodeName;
            Position = new Point(NodeUi.Margin.Left+NodeUi.Width/2f,NodeUi.Margin.Top+NodeUi.Height/2f);
            Roads = new Dictionary<Node, Road>();
        }
        [JsonIgnore]
        public Views.Node NodeUi { get; set; }

        /// <summary>
        /// JsonSerializerInfo
        /// </summary>
        public Point Position
        {
            get => _position;
            set
            {
                _position = value; 
                if(NodeUi==null)return;
                NodeUi.Margin = new Thickness(value.X-MyConstants.NodeSize/2f,value.Y-MyConstants.NodeSize/2f,0,0);
                Roads?.Values.ToList().ForEach(road =>
                {
                    Point xy1 = new Point(road.Line.X1,road.Line.Y1);
                    Point xy2 = new Point(road.Line.X2,road.Line.Y2);
                    if (Point.Subtract(xy1, value).LengthSquared < Point.Subtract(xy2, value).LengthSquared)
                    {
                        road.Line.X1 = value.X;
                        road.Line.Y1 = value.Y;
                    }
                    else
                    {
                        road.Line.X2 = value.X;
                        road.Line.Y2 = value.Y;
                    }
                    Point p = new Point((road.Line.X1 + road.Line.X2) / 2f, (road.Line.Y1 + road.Line.Y2) / 2f);
                    road.CostHeader.Margin = new Thickness(p.X,p.Y,0,0);
                });
            }
        }

        [JsonIgnore]
        public Graph GraphParent { get; set; }


        public string NodeName { get; set; }
        [JsonIgnore]
        public Dictionary<Node,Road> Roads { get; set; }
    }
}
