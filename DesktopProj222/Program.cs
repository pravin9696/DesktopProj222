using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopProj222
{    
  
    internal class Program
    {
        public static string Constr = "Data Source=.;Initial Catalog=DB_desktop_demo;Integrated Security=True;TrustServerCertificate=True";
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Registration());
        }
    }
}
