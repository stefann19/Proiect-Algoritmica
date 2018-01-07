using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Proiect_Algoritmica.Views;
using Node = Proiect_Algoritmica.Scripts.Graphs.Node;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class LineCreator
    {
        public LineCreator(Graph graph, Canvas workSpace)
        {
            Graph = graph;
            WorkSpace = workSpace;
            ReloadOldRoads();
        }

        private void ReloadOldRoads()
        {
            Graph.Roads.ToList().ForEach(road =>
            {
                road.StartingNode = Graph.Nodes[road.StaringNodeIndex];
                road.EndingNode = Graph.Nodes[road.EndingNodeIndex];
                road.Line = CreateLine(road.StartingNode, road.EndingNode);
                WorkSpace.Children.Add(road.Line);
                road.CostHeader = CreateCostHeader((int)road.Cost, road.StartingNode, road.EndingNode);
                road.CostHeader.ParentRoad = road;
                Graph.Nodes[road.StaringNodeIndex].Roads.Add(road.EndingNode,road);
                Graph.Nodes[road.EndingNodeIndex].Roads.Add(road.StartingNode, road);
            });
        }

        public Node StartingNode { get; set; }
        public Node EndingNode
        {
            get => throw new NotImplementedException();
            set
            {
                if(StartingNode==null)return;
                
                if(value==null)return; 
                CreateRoad(StartingNode, value);
            }
        }
        public static Shape DrawLinkArrow(Point p1, Point p2)
        {
            Vector v = Point.Subtract(p2, p1);
            v.Normalize();
            p1 = p1 + v * MyConstants.NodeSize / 2f;
            p2 = p2 - v * MyConstants.NodeSize / 2f;

            GeometryGroup lineGroup = new GeometryGroup();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Point p = new Point(p1.X  +((p2.X - p1.X) / 1), p1.Y +((p2.Y - p1.Y) / 1));
            pathFigure.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);
            LineSegment seg1 = new LineSegment();
            seg1.Point = lpoint;
            pathFigure.Segments.Add(seg1);

            LineSegment seg2 = new LineSegment();
            seg2.Point = rpoint;
            pathFigure.Segments.Add(seg2);

            LineSegment seg3 = new LineSegment();
            seg3.Point = p;
            pathFigure.Segments.Add(seg3);

            pathGeometry.Figures.Add(pathFigure);
            RotateTransform transform = new RotateTransform();
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pathGeometry.Transform = transform;
            lineGroup.Children.Add(pathGeometry);

            LineGeometry connectorGeometry = new LineGeometry();
            connectorGeometry.StartPoint = p1;
            connectorGeometry.EndPoint = p2;
            lineGroup.Children.Add(connectorGeometry);
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Data = lineGroup;
            path.StrokeThickness = 2;
            path.Stroke = path.Fill = Brushes.Black;

            return path;
        }

        public static Shape CreateLine(Node startingNode,Node endingNode)
        {
            if (startingNode == null) return null;
            if (endingNode == null) return null;

            //Shape s = DrawLinkArrow(startingNode.Position, endingNode.Position);
            //WorkSpace.Children.Add(s);
            Shape line = DrawLinkArrow(startingNode.Position, endingNode.Position);

            line.Stroke = Brushes.DarkCyan;
            line.StrokeThickness = 4;
            

            Panel.SetZIndex(line,-2);




          
            return line;
        }

        public void CreateRoad(Node startingNode, Node endingNode)
        {
            if(startingNode == endingNode)return;
            Shape line = CreateLine(startingNode, endingNode);
            if (Graph.Nodes[startingNode.NodeIndex].Roads.ContainsKey(endingNode)) return;
            WorkSpace.Children.Add(line);
            StartingNode = null;
            EndingNode = null;



            Random rnd = new Random();
            int rand = rnd.Next(0, 100);
            Road road = new Road
            {
                Line = line,
                StartingNode = startingNode,
                EndingNode = endingNode,
                Cost =  rand,
                CostHeader = CreateCostHeader(rand,startingNode,endingNode),
                
            };
            road.CostHeader.ParentRoad = road;
            Graph.Roads.Add(road);
            Graph.Nodes[road.StaringNodeIndex].Roads.Add(road.EndingNode, road);
            Graph.Nodes[road.EndingNodeIndex].Roads.Add(road.StartingNode, road);
        }

        private TextBoxx CreateCostHeader(int Cost,Node startingNode, Node endingNode)
        {
            Point p = new Point((startingNode.Position.X + endingNode.Position.X) / 2f, (startingNode.Position.Y + endingNode.Position.Y) / 2f);
            TextBoxx textBox = new TextBoxx
            {
                TextBox = {Text = Cost.ToString()},
                Margin = new Thickness(p.X, p.Y, 0, 0)
            };
            textBox.TextBox.TextChanged += TextBox_TextChanged;
            textBox.TextBox.MouseDoubleClick += TextBox_MouseDoubleClick;
            WorkSpace.Children.Add(textBox);
            
            return textBox;
        }

        private void TextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!(((sender as TextBox)?.Parent as Grid).Parent  is TextBoxx textBox))return;

            Graph.Roads.Remove(textBox.ParentRoad);
            WorkSpace.Children.Remove(textBox);
            WorkSpace.Children.Remove(textBox.ParentRoad.Line);
            Graph.Nodes.Values.ToList().ForEach(node =>
            {
                node.Roads.ToList().ForEach(road =>
                {
                    if (road.Value == textBox.ParentRoad)
                    {
                        node.Roads.Remove(road.Key);
                    }
                });
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxx r = ((TextBoxx) ((sender as TextBox)?.Parent as Grid)?.Parent);
            if(r==null)return;
            if (!int.TryParse(r.TextBox.Text, out int result)) return;
            if(Math.Abs(result - r.ParentRoad.Cost) > 0.1f)
                r.ParentRoad.Cost = int.Parse(r.TextBox.Text);
        }

        public Graph Graph { get; set; }
        public Canvas WorkSpace { get; set; }

    }
}
