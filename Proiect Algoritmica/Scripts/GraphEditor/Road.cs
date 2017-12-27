using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Proiect_Algoritmica.Views;
using Node = Proiect_Algoritmica.Scripts.Graphs.Node;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class Road
    {
        [JsonIgnore]
        private Node _startingNode;
        [JsonIgnore]
        private Node _endingNode;

        private double _cost;

        [JsonIgnore]
        public Line Line { get; set; }

        public double Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                if(CostHeader==null)return;
                CostHeader.TextBox.Text = value.ToString(CultureInfo.CurrentCulture);
            }
        }

        [JsonIgnore]
        public TextBoxx CostHeader { get; set; }

        [JsonIgnore]
        public Node StartingNode
        {
            get => _startingNode;
            set
            {
                _startingNode = value;
                StaringNodeName = _startingNode.NodeName;
                
            }
        }

        [JsonIgnore]
        public Node EndingNode
        {
            get => _endingNode;
            set
            {
                _endingNode = value;
                EndingNodeName = _endingNode.NodeName;
            }
        }

        public string StaringNodeName { get; set; }
        public string EndingNodeName { get; set; }
    }
}
