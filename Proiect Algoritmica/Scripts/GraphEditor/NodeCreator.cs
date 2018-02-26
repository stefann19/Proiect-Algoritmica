using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using Proiect_Algoritmica.Scripts.Graphs;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class NodeCreator
    {
        private int _currentNodeCount1;
        private Node _selectedNode;
        private Node _selectedNode2;

        public NodeCreator(Graph graph,Canvas workSpace)
        {
            Graph = graph;
            _currentNodeCount1 = 0;
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
                node.NodeUi.Label.Content = node.NodeIndex.ToString();
                node.NodeUi.NodeParent = node;
                node.GraphParent = Graph;
            });
        }

        public Views.Node CreateNodeUi(Point position)
        {
            Views.Node node = new Views.Node
            {
                Margin = new Thickness(position.X - MyConstants.NodeSize / 2f,
                    position.Y - MyConstants.NodeSize / 2f, 0, 0),
                Width = MyConstants.NodeSize,
                Height = MyConstants.NodeSize,
                Label = {Content = CurrentNodeCount.ToString()}
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

                Node endingNode = n.NodeParent.Roads.First(v => v.Value.Equals(road)).Key;

                endingNode.Roads.Remove(n.NodeParent);
            });
           
            n.NodeParent.Roads.Clear();
            WorkSpace.Children.Remove(n);
            Graph.Nodes.Remove(n.NodeParent.NodeIndex);
        }

        public void CreateNode(Point position)
        {
            Views.Node nodeUi = CreateNodeUi(position);
             Node node = new Node(nodeUi, CurrentNodeCount);
            nodeUi.NodeParent = node;

            Graph.GraphSize = Point.Subtract(MaxPoint,MinPoint);
            Graph.MinPoint = MinPoint;
            node.GraphParent = Graph;
            Graph.Nodes.Add(node.NodeIndex,node);
        }
        public void CreateNode(Views.Node nodeUi)
        {
            Node node = new Node(nodeUi, CurrentNodeCount);
            nodeUi.NodeParent = node;
            Graph.GraphSize = Point.Subtract(MaxPoint, MinPoint);
            Graph.MinPoint = MinPoint;

            node.GraphParent = Graph;
            Graph.Nodes.Add(node.NodeIndex, node);
        }

 

        public Graph Graph { get; set; }
        public Canvas WorkSpace { get; set; }

        private int CurrentNodeCount
        {
            get
            {
                while (Graph.Nodes.ContainsKey(_currentNodeCount1))
                {
                    _currentNodeCount1++;
                }
                return _currentNodeCount1;
            }
        }

        public Node SelectedNode2
        {
            get => _selectedNode2;
            set
            {
                Graph.GraphEditorEngine.Cleaup(Graph,null);
                if (_selectedNode2 != null)
                {
                    _selectedNode2.NodeUi.Ellipse.Fill = Brushes.Cyan;
                }
                _selectedNode2 = value;
                if (_selectedNode2 != null)
                {
                    _selectedNode2.NodeUi.Ellipse.Fill = Brushes.DarkRed;
                }
            }
        }
        public Node SelectedNode
        {
            get => _selectedNode;
            set
            {
                Graph.GraphEditorEngine.Cleaup(Graph,null);
                if (_selectedNode != null)
                {
                    _selectedNode.NodeUi.Ellipse.Fill = Brushes.Cyan;
                }
                _selectedNode = value;
                if (_selectedNode != null)
                {
                    _selectedNode.NodeUi.Ellipse.Fill = Brushes.Green;
                }
            }
        }

        public Point MinPoint { get; set; }
        public Point MaxPoint { get; set; }
    }
}
