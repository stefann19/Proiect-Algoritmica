using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Proiect_Algoritmica.Views;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class WorkSpaceInputListener
    {
        public WorkSpaceInputListener(GraphEditorEngine graphEditorEngine)
        {
            GraphEditorEngine = graphEditorEngine;
            WorkSpace = graphEditorEngine.GraphEditor.WorkSpace;

            WorkSpace.MouseLeftButtonDown += WorkSpace_MouseLeftButtonDown;
            WorkSpace.MouseMove += WorkSpace_PreviewMouseMove;
            WorkSpace.MouseLeftButtonUp += WorkSpace_PreviewMouseLeftButtonUp;
            WorkSpace.MouseLeave += WorkSpace_MouseLeave;
           
            //WorkSpace.MouseRightButtonDown += WorkSpace_MouseRightButtonDown;
            //WorkSpace.MouseRightButtonUp += WorkSpace_MouseRightButtonUp;

        }

      
        //private void WorkSpace_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void WorkSpace_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    throw new NotImplementedException();
            
        //}

        private void WorkSpace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //GraphEditorEngine.NodeCreator.CreateNode(Mouse.GetPosition(WorkSpace));
            //if(GraphEditorEngine.LineCreator.StartingNode ==null)
            currentNode = GraphEditorEngine.NodeCreator.CreateNodeUi(Mouse.GetPosition(WorkSpace));
            //create node
        }
        private void WorkSpace_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (currentNode == null) return;
            Point p = Mouse.GetPosition(WorkSpace);
            p = new Point(p.X -MyConstants.NodeSize/2f,p.Y-MyConstants.NodeSize/2f);

            p = new Point(Math.Min(Math.Max(p.X, 0), GraphEditorEngine.GraphEditor.ActualWidth), Math.Min(Math.Max(p.Y, 0), GraphEditorEngine.GraphEditor.ActualHeight));

            currentNode.Margin = new Thickness(p.X,p.Y,0,0);
        }
        private void WorkSpace_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateNodeFromUi();
        }
        private void WorkSpace_MouseLeave(object sender, MouseEventArgs e)
        {
            CreateNodeFromUi();
        }

        private void CreateNodeFromUi()
        {
            if(currentNode==null)return;
            GraphEditorEngine.NodeCreator.CreateNode(currentNode);
            currentNode = null;
        }

        private Node currentNode;

        public GraphEditorEngine GraphEditorEngine { get; set; }
        public Canvas WorkSpace { get; set; }

        private Point startingPosition;
        private Point endingPosition;

    }
}
