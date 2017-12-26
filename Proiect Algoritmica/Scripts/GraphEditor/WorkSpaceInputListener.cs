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

            WorkSpace.PreviewMouseLeftButtonDown += WorkSpace_MouseLeftButtonDown;
            WorkSpace.MouseRightButtonDown += WorkSpace_MouseRightButtonDown;
            WorkSpace.MouseRightButtonUp += WorkSpace_MouseRightButtonUp;

        }

        private void WorkSpace_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WorkSpace_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WorkSpace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GraphEditorEngine.NodeCreator.CreateNode(Mouse.GetPosition(WorkSpace));
            //create node
        }

        private Node currentNode

        public GraphEditorEngine GraphEditorEngine { get; set; }
        public Canvas WorkSpace { get; set; }

        private Point startingPosition;
        private Point endingPosition;

    }
}
