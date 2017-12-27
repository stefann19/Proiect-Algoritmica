using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using Proiect_Algoritmica.Scripts.GraphEditor;

namespace Proiect_Algoritmica.Scripts.MainWindow
{
    public class ListViewManager
    {
        public ListViewManager(ListView listView)
        {
            Graphs = new List<Graph>();
            DirectoryInfo directoryInfo = new DirectoryInfo(MyConstants.ExePath);
            directoryInfo.GetFiles("*.json").ToList().ForEach(file =>
            {
                using (StreamReader fi = File.OpenText(file.FullName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Graph graph = (Graph)serializer.Deserialize(fi, typeof(Graph));
                    Graphs.Add(graph);
                }
            });

            listView.ItemsSource = Graphs;
            listView.SelectionChanged += ListView_SelectionChanged;
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Proiect_Algoritmica.MainWindow.GraphListS.SelectedItem==null)return;
            Proiect_Algoritmica.MainWindow.PreviewCanvasS.Children.Clear();

            Proiect_Algoritmica.MainWindow.FlyoutS.IsOpen = true;
            
            Proiect_Algoritmica.MainWindow.FlyoutS.Header = Proiect_Algoritmica.MainWindow.GraphListS.SelectedItem.ToString();

            string jsonInfo = System.IO.File.ReadAllText($"{MyConstants.ExePath}\\{Proiect_Algoritmica.MainWindow.GraphListS.SelectedItem.ToString()}.json");

           
            ScaleTransform scaletransform = new ScaleTransform();
            Proiect_Algoritmica.MainWindow.PreviewCanvasS.RenderTransform = scaletransform;
            Graph graph = JsonConvert.DeserializeObject<Graph>(jsonInfo);
            NodeCreator nodeCreator = new NodeCreator(graph, Proiect_Algoritmica.MainWindow.PreviewCanvasS);
            LineCreator line = new LineCreator(graph, Proiect_Algoritmica.MainWindow.PreviewCanvasS);

            double width =  Proiect_Algoritmica.MainWindow.FlyoutS.ActualWidth / (nodeCreator.MaxPoint.X - nodeCreator.MinPoint.X)  ;
            double height =  Proiect_Algoritmica.MainWindow.FlyoutS.ActualHeight / (nodeCreator.MaxPoint.Y - nodeCreator.MinPoint.Y)  ;
            double greater = Math.Min(Math.Min(width, height)* 0.7f,1.2f);
            scaletransform.ScaleX = greater;
            scaletransform.ScaleY = greater;

            //scaletransform.CenterX = -nodeCreator.MinPoint.X ;
            //scaletransform.CenterY = -nodeCreator.MinPoint.Y ;
            //scaletransform.CenterY = (nodeCreator.MinPoint.Y + nodeCreator.MaxPoint.Y) / 2f;
            //scaletransform.CenterX = (nodeCreator.MinPoint.X + nodeCreator.MaxPoint.X) / 2f;
            Proiect_Algoritmica.MainWindow.PreviewCanvasS.Margin = new Thickness(-nodeCreator.MinPoint.X * greater ,-nodeCreator.MinPoint.Y * greater ,0,0);
        }

        public List<Graph> Graphs { get; set; }
    }
}
