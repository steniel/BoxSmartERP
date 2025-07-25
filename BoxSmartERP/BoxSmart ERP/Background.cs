using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSmart_ERP
{
    public partial class Background : Form
    {
        public Background()
        {
            InitializeComponent();
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/WaitBackground.html");
            webView21.Source = new Uri(htmlFilePath);
        }
    }
}
