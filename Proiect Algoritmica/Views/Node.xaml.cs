using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proiect_Algoritmica.Views
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Scripts.Graphs.Node NodeParent;
        private bool move;
        public Node()
        {
            InitializeComponent();

            move = false;
            Ellipse.MouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;
            Ellipse.MouseLeftButtonUp += Ellipse_PreviewMouseLeftButtonUp;
            Ellipse.MouseMove += Ellipse_MouseMove;
           
            Ellipse.MouseRightButtonDown += Ellipse_PreviewMouseRightButtonDown;
            Ellipse.MouseRightButtonUp += Ellipse_PreviewMouseRightButtonUp;

        }

      

        private void Ellipse_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NodeParent == null) return;
            NodeParent.GraphParent.GraphEditorEngine.LineCreator.EndingNode = GetNodeFromObj(sender);
            e.Handled = true;
        }

        private void Ellipse_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(NodeParent==null)return;
            Scripts.Graphs.Node node = GetNodeFromObj(sender);
            NodeParent.GraphParent.GraphEditorEngine.LineCreator.StartingNode = node;
            NodeParent.GraphParent.GraphEditorEngine.NodeCreator.SelectedNode2 = node;
            e.Handled = true;

        }

        private void Ellipse_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            move = false;
            if(NodeParent==null)return;
            
            NodeParent.GraphParent.GraphEditorEngine.NodeCreator.SelectedNode = GetNodeFromObj(sender);
        }

        private void Ellipse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Scripts.Graphs.Node node = GetNodeFromObj(sender);
            move = true;
            e.Handled = true;
        }
        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            //e.Handled = true;
            if (move)
            {
                NodeParent.Position = Mouse.GetPosition(NodeParent.GraphParent.GraphEditorEngine.GraphEditor.WorkSpace);
            }
        }


        private Scripts.Graphs.Node GetNodeFromObj(object sender)
        {
            Node node = ((sender as Ellipse)?.Parent as Grid)?.Parent as Node;
            return node?.NodeParent;
        }
    }
}
