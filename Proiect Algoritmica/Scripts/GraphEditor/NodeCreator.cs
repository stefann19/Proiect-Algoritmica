using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

            Graph?.Nodes?.ToList().ForEach(node=> CreateNodeUi(node.Position));
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

            return node;
        }

        public void CreateNode(Point position)
        {
            Node node = new Node(CreateNodeUi(position), _currentNodeCount.ToString());
            Graph.GraphSize = Point.Subtract(MaxPoint,MinPoint);
            Graph.MinPoint = MinPoint;
            Graph.Nodes.Add(node);
        }

        public Graph Graph { get; set; }
        public Canvas WorkSpace { get; set; }
        private int _currentNodeCount;

        public Point MinPoint { get; set; }
        public Point MaxPoint { get; set; }
    }
}
