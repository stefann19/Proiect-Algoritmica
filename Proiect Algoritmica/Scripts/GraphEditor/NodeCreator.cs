using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using Proiect_Algoritmica.Scripts.Graphs;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class NodeCreator
    {
        public NodeCreator(Graph graph,Canvas workSpace)
        {
            Graph = graph;
            _currentNodeCount = 0;
            WorkSpace = workSpace;
            MaxPoint = new Point(0,0);
            MinPoint = new Point(WorkSpace.ActualWidth, WorkSpace.ActualHeight);
            ReloadOldNodes();
        }

        private void ReloadOldNodes()
        {

            Graph?.Nodes?.Values.ToList().ForEach(node =>
            {
                node.NodeUi = CreateNodeUi(node.Position);
                node.NodeUi.NodeParent = node;
                node.GraphParent = Graph;
            });
        }

        public Views.Node CreateNodeUi(Point position)
        {
            _currentNodeCount++;
            Views.Node node = new Views.Node
            {
                Margin = new Thickness(position.X - MyConstants.NodeSize / 2f,
                    position.Y - MyConstants.NodeSize / 2f, 0, 0),
                Width = MyConstants.NodeSize,
                Height = MyConstants.NodeSize,
                Label = {Content = _currentNodeCount.ToString()}
            };
            WorkSpace.Children.Add(node);

            //calculate min/max
            if (position.X + MyConstants.NodeSize / 2f > MaxPoint.X) MaxPoint = new Point(position.X + MyConstants.NodeSize / 2f, MaxPoint.Y);
            if (position.X - MyConstants.NodeSize / 2f < MinPoint.X) MinPoint = new Point(position.X - MyConstants.NodeSize / 2f, MinPoint.Y);
            if (position.Y + MyConstants.NodeSize / 2f > MaxPoint.Y) MaxPoint = new Point(MaxPoint.X, position.Y + MyConstants.NodeSize / 2f);
            if (position.Y - MyConstants.NodeSize / 2f < MinPoint.Y) MinPoint = new Point(MinPoint.X, position.Y - MyConstants.NodeSize / 2f);

            node.MouseDoubleClick += Node_MouseDoubleClick;

            return node;
        }

        private void Node_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //delete node;
            //Views.Node n = ((Views.Node) ((sender as Ellipse)?.Parent as Grid)?.Parent);
            if(!(sender is Views.Node n))return;
            
            n.NodeParent.Roads.Values.ToList().ForEach(road =>
            {
                WorkSpace.Children.Remove(road.CostHeader);
                WorkSpace.Children.Remove(road.Line);
                Graph.Roads.Remove(road);
            });
           
            n.NodeParent.Roads.Clear();
            WorkSpace.Children.Remove(n);
            Graph.Nodes.Remove(n.NodeParent.NodeName);
        }

        public void CreateNode(Point position)
        {
            Views.Node nodeUi = CreateNodeUi(position);
           
            Node node = new Node(nodeUi, _currentNodeCount.ToString());
            nodeUi.NodeParent = node;

            Graph.GraphSize = Point.Subtract(MaxPoint,MinPoint);
            Graph.MinPoint = MinPoint;
            node.GraphParent = Graph;
            Graph.Nodes.Add(node.NodeName,node);
        }
        public void CreateNode(Views.Node nodeUi)
        {
            Node node = new Node(nodeUi, _currentNodeCount.ToString());
            nodeUi.NodeParent = node;

            Graph.GraphSize = Point.Subtract(MaxPoint, MinPoint);
            Graph.MinPoint = MinPoint;

            node.GraphParent = Graph;
            Graph.Nodes.Add(node.NodeName, node);
        }



        public Graph Graph { get; set; }
        public Canvas WorkSpace { get; set; }
        private int _currentNodeCount;

        public Point MinPoint { get; set; }
        public Point MaxPoint { get; set; }
    }
}
