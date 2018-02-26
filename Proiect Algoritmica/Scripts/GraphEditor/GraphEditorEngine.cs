using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using Proiect_Algoritmica.Scripts.Graphs;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class GraphEditorEngine
    {
        public Proiect_Algoritmica.GraphEditor GraphEditor { get; set; }
        public Graph Graph { get; set; }
        public NodeCreator NodeCreator { get; set; }
        public LineCreator LineCreator { get; set; }
        public WorkSpaceInputListener WorkSpaceInputListener { get; set; }
        /// <summary>
        /// Json serialization
        /// </summary>
        public GraphEditorEngine()
        {
            Init();
        }
        /// <summary>
        /// New Graph
        /// </summary>
        /// <param name="graphName"></param>
        public GraphEditorEngine(string graphName)
        {
            if (System.IO.File.Exists($"{MyConstants.ExePath}\\{graphName}.json"))
            {

                string jsonInfo = System.IO.File.ReadAllText($"{MyConstants.ExePath}\\{graphName}.json");
                Graph = JsonConvert.DeserializeObject<Graph>(jsonInfo);
            }
            else
            {
                Graph = new Graph(graphName);
            }
            Init();

        }

        private void Init()
        {
            Graph.GraphEditorEngine = this;
            GraphEditor = new Proiect_Algoritmica.GraphEditor {Title = $"{Graph.Name} graph editor"};
            GraphEditor.Show();
            GraphEditor.Closed += GraphEditor_Closed;
            NodeCreator = new NodeCreator(Graph,GraphEditor.WorkSpace);
            LineCreator = new LineCreator(Graph,GraphEditor.WorkSpace);
            WorkSpaceInputListener = new WorkSpaceInputListener(this);

            if (GraphEditor != null)
            {
                GraphEditor.BT_GenericParsing.PreviewMouseDown += BT_GenericParsing_MouseDown;
                GraphEditor.BT_BFParsing.PreviewMouseDown += BT_BFParsing_PreviewMouseDown;
                GraphEditor.BT_DFParsing.PreviewMouseDown += BT_DFParsing_PreviewMouseDown;
                GraphEditor.BT_GenericTree.PreviewMouseDown += BT_GenericTree_PreviewMouseDown;
                GraphEditor.BT_KruskalTree.PreviewMouseDown += BT_KruskalTree_PreviewMouseDown;
                GraphEditor.BT_PrimeTree.PreviewMouseDown += BT_PrimeTree_PreviewMouseDown;
                GraphEditor.BT_Dijkstra.PreviewMouseDown += BT_Dijkstra_PreviewMouseDown;
                GraphEditor.BT_BellmanFord.PreviewMouseDown += BT_BellmanFord_PreviewMouseDown;
                GraphEditor.BT_FLOYDWARSHALL.PreviewMouseDown += BT_FLOYDWARSHALL_PreviewMouseDown;
                GraphEditor.BT_EulerianCycle.PreviewMouseDown += BT_EulerianCycle_PreviewMouseDown;
            }

        }

        private void BT_EulerianCycle_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph, GraphEditor);
            List<Road> Aa = TreeParsings.EulerianCicleAlgorithm(Graph,NodeCreator.SelectedNode);
            if (Aa == null)
            {
                GraphEditor.TB_Results.Text = "No starting node selected";
            }
            else
            {
                Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            }
        }

        private void BT_FLOYDWARSHALL_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph, GraphEditor);
            List<Road> Aa = TreeParsings.FloydWarshallAlgorithm(Graph, NodeCreator.SelectedNode, NodeCreator.SelectedNode2);
            if (Aa == null)
            {
                GraphEditor.TB_Results.Text = "No path found...";
            }
            else
            {
                Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            }
        }

        private void BT_BellmanFord_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph,GraphEditor);
            List<Road> Aa = TreeParsings.BellmanFordAlgorithm(Graph, NodeCreator.SelectedNode, NodeCreator.SelectedNode2);
            if (Aa == null)
            {
                GraphEditor.TB_Results.Text = "No path found...";
            }
            else
            {
                Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            }
        }

        private void BT_Dijkstra_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph,GraphEditor);
            List<Road> Aa = TreeParsings.DijkstraAlgorithm(Graph,NodeCreator.SelectedNode,NodeCreator.SelectedNode2);
            if (Aa == null)
            {
                GraphEditor.TB_Results.Text = "No path found...";
            }
            else
            {
                Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            }
        }

        private void BT_PrimeTree_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph,GraphEditor);
            List<Road> Aa = TreeParsings.PrimeTree(Graph);
            Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            GraphEditor.TB_Results.Text = Aa.Select(road => road.ToString()).Aggregate((a, b) => a + $"\n{b}");

        }



        private void BT_KruskalTree_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph,GraphEditor);
            List<Road> Aa = TreeParsings.KruskalTree(Graph);
            Aa.ForEach(road => road.Line.Stroke = Brushes.Green);
            GraphEditor.TB_Results.Text = Aa.Select(road => road.ToString()).Aggregate((a, b) => a + $"\n{b}");

        }
        private void BT_GenericTree_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Cleaup(Graph,GraphEditor);
            List<Road> Aa = TreeParsings.GenericTree(Graph);
            Aa.ForEach(road=> road.Line.Stroke = Brushes.Green);
            GraphEditor.TB_Results.Text = Aa.Select(road=>road.ToString()).Aggregate((a,b)=> a+$"\n{b}");
        }

        private void BT_DFParsing_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NodeCreator.SelectedNode==null)return;    
            Cleaup(Graph,GraphEditor);
            GraphEditor.TB_Results.Text = TreeParsings.DepthFirstParsing(Graph, NodeCreator.SelectedNode);
        }

        private void BT_BFParsing_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NodeCreator.SelectedNode == null) return;
            Cleaup(Graph,GraphEditor);
            GraphEditor.TB_Results.Text = TreeParsings.BreadthFirstParsing(Graph, NodeCreator.SelectedNode);
        }

        private void BT_GenericParsing_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NodeCreator.SelectedNode == null) return;
            Cleaup(Graph,GraphEditor);
            GraphEditor.TB_Results.Text = TreeParsings.GenericParsing(Graph,NodeCreator.SelectedNode);
        }
        public void Cleaup(Graph graph,Proiect_Algoritmica.GraphEditor graphEditor)
        {
            graph.Roads.ToList().ForEach(road => road.Line.Stroke = Brushes.DarkCyan);
            if(graphEditor==null)return;
            GraphEditor.Flyout.IsOpen = true;
            graphEditor.TB_Results.Text = "";
        }

        private void GraphEditor_Closed(object sender, EventArgs e)
        {
            string serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(Graph);
            File.WriteAllText($"{MyConstants.ExePath}//{Graph.Name}.json",serializedObj);
            Proiect_Algoritmica.MainWindow.UpdateList();
        }
    }
}
