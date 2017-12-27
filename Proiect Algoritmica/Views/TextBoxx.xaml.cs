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
using Proiect_Algoritmica.Scripts.GraphEditor;

namespace Proiect_Algoritmica.Views
{
    /// <summary>
    /// Interaction logic for TextBoxx.xaml
    /// </summary>
    public partial class TextBoxx : UserControl
    {
        public Road ParentRoad { get; set; }
        public TextBoxx()
        {
            InitializeComponent();
        }
    }
}
