using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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
              /*  if (Graph.Nodes[road.StaringNodeIndex].Roads.ContainsKey(road.EndingNode)) return;*/
                Graph.Nodes[road.StaringNodeIndex].Roads.Add(road.EndingNode,road);

                Graph.Nodes[road.StaringNodeIndex].RoadsList.Add(road);
                Graph.Nodes[road.EndingNodeIndex].RoadsList.Add(road);

                /*Graph.Nodes[road.EndingNodeIndex].Roads.Add(road.StartingNode, road);*/
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
            if(p1 == p2)
                v = new Vector(0,0);
            else
            v.Normalize();
            p1 = p1 + v * MyConstants.NodeSize / 2f;
            p2 = p2 - v * MyConstants.NodeSize / 2f;

            GeometryGroup lineGroup = new GeometryGroup();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Point p = p2;
            
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


            /* LineGeometry connectorGeometry = new LineGeometry();
             connectorGeometry.StartPoint = p1;
             connectorGeometry.EndPoint = p2;
             lineGroup.Children.Add(connectorGeometry);
 */
            int drift = 40;
            Point p3 = (Point)(((Vector)p1 + (Vector)p2) / 2f + new Vector(0, drift).Rotate(theta));
            Point p4 = (Point)(((Vector)p1 + (Vector)p2) / 2f + new Vector(drift, 0).Rotate(theta));
            if (p1 == p2)
            {
                drift = 120;
                p3 = (Point)(((Vector)p1 + new Vector(drift, drift).Rotate(theta)));
                p4 = (Point)(((Vector)p1 + new Vector(-drift, drift).Rotate(theta)));
            }
            

            PathGeometry pathGeometryy = MakePathGeometry(p1, p2,p3,p4);

            lineGroup.Children.Add(pathGeometryy);


            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Data = lineGroup;
            path.StrokeThickness = 2;
            path.Stroke = Brushes.Transparent;
            path.Fill = Brushes.Transparent;


            return path;
        }

        private static PathGeometry MakePathGeometry(Point p1,Point p2,Point p3,Point p4)
        {
            PathGeometry pathGeometry = new PathGeometry();

            pathGeometry.FillRule = FillRule.Nonzero;
            

            PathFigure pathFigure = new PathFigure();

            pathFigure.StartPoint = p1;

            pathFigure.IsClosed = false;

            pathGeometry.Figures.Add(pathFigure);

            


         /*   LineSegment lineSegment1 = new LineSegment();

            lineSegment1.Point = new Point(198, 48.6667);

            pathFigure.Segments.Add(lineSegment1);



            LineSegment lineSegment2 = new LineSegment();

            lineSegment2.Point = new Point(198, 102);

            pathFigure.Segments.Add(lineSegment2);
*/


            BezierSegment bezierSegment1 = new BezierSegment();

            bezierSegment1.Point1 = p3;

            bezierSegment1.Point2 = p4;

            bezierSegment1.Point3 = p2;
            

            pathFigure.Segments.Add(bezierSegment1);



           /* BezierSegment bezierSegment2 = new BezierSegment();

            bezierSegment2.Point1 = new Point(64.667, 149.111);

            bezierSegment2.Point2 = new Point(58.4444, 130.444);

            bezierSegment2.Point3 = new Point(47.7778, 118.889);

            pathFigure.Segments.Add(bezierSegment2);*/

            return pathGeometry;
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
            //if(startingNode == endingNode)return;

            Shape line = CreateLine(startingNode, endingNode);
            if (Graph.Nodes[startingNode.NodeIndex].Roads.ContainsKey(endingNode)/* && Graph.Nodes[startingNode.NodeIndex].Roads[endingNode].StartingNode == startingNode*/) return;
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
            /*if(Graph.Nodes[road.StaringNodeIndex].Roads.ContainsKey(road.EndingNode)) return;*/
            
            startingNode.Roads.Add(road.EndingNode, road);
            endingNode.RoadsList.Add(road);
            startingNode.RoadsList.Add(road);
            /*Graph.Nodes[road.EndingNodeIndex].Roads.Add(road.StartingNode, road);*/
        }

        public static Thickness GetCostHeaderPos(Node startingNode, Node endingNode)
        {
            Point p = new Point((startingNode.Position.X + endingNode.Position.X) / 2f, (startingNode.Position.Y + endingNode.Position.Y) / 2f);

            double theta = Math.Atan2((startingNode.Position.Y - endingNode.Position.Y), (startingNode.Position.X - endingNode.Position.X)) * 180 / Math.PI;

            Vector v = new Vector(0, 80).Rotate(theta);

            return new Thickness(p.X + v.X, p.Y + v.Y, 0, 0);
        }

        private TextBoxx CreateCostHeader(int Cost,Node startingNode, Node endingNode)
        {
            /*Point p = new Point((startingNode.Position.X + endingNode.Position.X) / 2f, (startingNode.Position.Y + endingNode.Position.Y) / 2f);
            
            double theta = Math.Atan2((startingNode.Position.Y - endingNode.Position.Y), (startingNode.Position.X - endingNode.Position.X)) * 180 / Math.PI;

            Vector v = new Vector(0,50).Rotate(theta);*/
            TextBoxx textBox = new TextBoxx
            {
                TextBox = {Text = Cost.ToString()},
                Margin = GetCostHeaderPos(startingNode,endingNode)
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

    public static class VectorExt
    {
        private const double DegToRad = Math.PI / 180;

        public static Vector Rotate(this Vector v, double degrees)
        {
            return v.RotateRadians(degrees * DegToRad);
        }

        public static Vector RotateRadians(this Vector v, double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new Vector(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
