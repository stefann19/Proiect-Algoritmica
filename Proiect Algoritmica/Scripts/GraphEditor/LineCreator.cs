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
                road.StartingNode = Graph.Nodes[road.StaringNodeName];
                road.EndingNode = Graph.Nodes[road.EndingNodeName];
                road.Line = CreateLine(road.StartingNode, road.EndingNode);
                road.CostHeader = CreateCostHeader((int)road.Cost, road.StartingNode, road.EndingNode);
                road.CostHeader.ParentRoad = road;
                Graph.Nodes[road.StaringNodeName].Roads.Add(road.EndingNode,road);
                Graph.Nodes[road.EndingNodeName].Roads.Add(road.StartingNode, road);
            });
        }

        public Node StartingNode { get; set; }
        public Node EndingNode
        {
            get => throw new NotImplementedException();
            set
            {
                if(value==null)return; 
                CreateRoad(StartingNode, value);
            }
        }
        

        public Line CreateLine(Node startingNode,Node endingNode)
        {
            if (Graph.Nodes[startingNode.NodeName].Roads.ContainsKey(endingNode)) return null;
            Line line = new Line
            {
                X1 = startingNode.Position.X,
                Y1 = startingNode.Position.Y,
                X2 = endingNode.Position.X,
                Y2 = endingNode.Position.Y,
                Stroke = Brushes.DarkCyan,
                StrokeThickness = 4
            };

            Panel.SetZIndex(line,-2);
            WorkSpace.Children.Add(line);



            StartingNode = null;
            EndingNode = null;
            return line;
        }

        public void CreateRoad(Node startingNode, Node endingNode)
        {
            Line line = CreateLine(startingNode, endingNode);
            if(line==null)return;
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
            Graph.Nodes[road.StaringNodeName].Roads.Add(road.EndingNode, road);
            Graph.Nodes[road.EndingNodeName].Roads.Add(road.StartingNode, road);
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
