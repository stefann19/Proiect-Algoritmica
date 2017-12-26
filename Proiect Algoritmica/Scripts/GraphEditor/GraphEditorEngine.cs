using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Proiect_Algoritmica.Scripts.GraphEditor
{
    public class GraphEditorEngine
    {
        public Proiect_Algoritmica.GraphEditor GraphEditor { get; set; }
        public Graph Graph { get; set; }
        public NodeCreator NodeCreator { get; set; }
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
            Graph = new Graph(graphName);
            Init();
        }

        private void Init()
        {
            GraphEditor = new Proiect_Algoritmica.GraphEditor {Title = $"{Graph.Name} graph editor"};
            GraphEditor.Show();
            GraphEditor.Closed += GraphEditor_Closed;
            NodeCreator = new NodeCreator(Graph,GraphEditor.WorkSpace);
            WorkSpaceInputListener = new WorkSpaceInputListener(this);
        }

        private void GraphEditor_Closed(object sender, EventArgs e)
        {
            string serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(Graph);
            File.WriteAllText($"{MyConstants.ExePath}//{Graph.Name}.json",serializedObj);
            Proiect_Algoritmica.MainWindow.UpdateList();
        }
    }
}
