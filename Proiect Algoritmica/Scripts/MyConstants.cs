using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Algoritmica.Scripts
{
    public class MyConstants
    {
        public static string ExePath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}\\Graphs".Substring(6);
        public static int NodeSize = 50;
    }
}
