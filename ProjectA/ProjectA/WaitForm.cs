using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            progressBar.Step = 1;
            /*while (true)
            {
                progressBar.PerformStep();
            }*/
        }
    }
}
