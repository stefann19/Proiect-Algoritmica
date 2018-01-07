using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Proiect_Algoritmica.Scripts.Graphs;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class AlgorithmHighlighter
    {
        private Node _selectedNode;
        private Node _currentProcessedNode;
        private Road _currentProcessedRoad;

        public Node SelectedNode
        {
            get => _selectedNode;
            set
            {
                if (_selectedNode != null)
                {
                    _selectedNode.NodeUi.Ellipse.Fill = Brushes.Cyan;
                }
                _selectedNode = value;
                if (_selectedNode != null)
                {
                    _selectedNode.NodeUi.Ellipse.Fill = Brushes.Red;
                }
            }
        }

        public Node CurrentProcessedNode
        {
            get => _currentProcessedNode;
            set
            {
                if (_currentProcessedNode != null)
                {
                    _currentProcessedNode.NodeUi.Ellipse.Fill = Brushes.Cyan;
                }
                _currentProcessedNode = value;
                if (_currentProcessedNode != null)
                {
                    _currentProcessedNode.NodeUi.Ellipse.Fill = Brushes.Green;
                }
            }
        }

        public Road CurrentProcessedRoad
        {
            get => _currentProcessedRoad;
            set
            {
                if (_currentProcessedRoad != null)
                {
                    _currentProcessedRoad.Line.Fill = Brushes.DarkCyan;
                }
                _currentProcessedRoad = value;
                if (_currentProcessedRoad != null)
                {
                    _currentProcessedRoad.Line.Fill = Brushes.Green;
                }
            }
        }
    }
}
