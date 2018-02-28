using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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
            RoadsList = new List<Road>();
        }

        public Node(Views.Node nodeUi, int nodeName)
        {
            NodeUi = nodeUi;
            NodeIndex = nodeName;
            Position = new Point(NodeUi.Margin.Left+NodeUi.Width/2f,NodeUi.Margin.Top+NodeUi.Height/2f);
            Roads = new Dictionary<Node, Road>();
            RoadsList = new List<Road>();
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
/*
                List<Road> roads = new List<Road>();
                if(Roads?.Values ==null)return;
                
                roads.AddRange(Roads.Values);

                Roads.Values.ToList().ForEach(road=> roads.AddRange(road.EndingNode.Roads.Values.Where(r=> r.EndingNode == this)) );*/

                RoadsList?.ToList().ForEach(road =>
                {
                    road.StartingNode.GraphParent.GraphEditorEngine.GraphEditor.WorkSpace.Children.Remove(road.Line);
                    road.Line = LineCreator.CreateLine(road.StartingNode, road.EndingNode);
                    road.StartingNode.GraphParent.GraphEditorEngine.GraphEditor.WorkSpace.Children.Add(road.Line);
                    //Point xy1 = new Point(road.Line.X1,road.Line.Y1);
                    //Point xy2 = new Point(road.Line.X2,road.Line.Y2);
                    //if (Point.Subtract(xy1, value).LengthSquared < Point.Subtract(xy2, value).LengthSquared)
                    //{
                    //    road.Line.X1 = value.X;
                    //    road.Line.Y1 = value.Y;
                    //}
                    //else
                    //{
                    //    road.Line.X2 = value.X;
                    //    road.Line.Y2 = value.Y;
                    //}
                    Point p = new Point(road.StartingNode.Position.X + road.EndingNode.Position.X, road.StartingNode.Position.Y + road.EndingNode.Position.Y);
                    road.CostHeader.Margin = LineCreator.GetCostHeaderPos(road.StartingNode,road.EndingNode);
                });

                

            }
        }

        [JsonIgnore]
        public Graph GraphParent { get; set; }


        public int NodeIndex { get; set; }
        [JsonIgnore]
        public Dictionary<Node,Road> Roads { get; set; }
        [JsonIgnore]
        public List<Road> RoadsList { get; set; }
    }
}
