using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.VisualBasic;
using Proiect_Algoritmica.Scripts;
using Proiect_Algoritmica.Scripts.GraphEditor;
using Proiect_Algoritmica.Scripts.MainWindow;

namespace Proiect_Algoritmica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static ListView GraphListS;
        public static Flyout FlyoutS;
        public static Canvas PreviewCanvasS;
        public MainWindow()
        {
            InitializeComponent();
            System.IO.Directory.CreateDirectory(MyConstants.ExePath);
            GraphListS = GraphList;
            FlyoutS = Flyout;
            PreviewCanvasS = PreviewCanvas;
            UpdateList();
            //GraphList.ItemsSource = new List<string>{"Jon","Snow","Is","not","Dead","Merry","Christmas","YOu","Filthy","Animals"};
        }

        public static void UpdateList()
        {
            ListViewManager listViewManager = new ListViewManager(GraphListS);            
        }


        private async void BT_NewGraph_Click(object sender, RoutedEventArgs e)
        {
            string graphName = await this.ShowInputAsync("Creating a new graph","Please name the graph...");
            if(graphName==null)return;
            GraphEditorEngine graphEditorEngine = new GraphEditorEngine(graphName);
        }

        private async void BT_DeleteGraph_Click(object sender, RoutedEventArgs e)
        {
            if(GraphList.SelectedItems.Count==0)return;
            
            MessageDialogResult result = await this.ShowMessageAsync("You are going to delete some graphs...", "Are you sure?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative) return;
            foreach (object graphListSelectedItem in GraphList.SelectedItems)
            {
                File.Delete($"{MyConstants.ExePath}\\{graphListSelectedItem.ToString()}.json");
            }
            UpdateList();
           
        }



    }
}
