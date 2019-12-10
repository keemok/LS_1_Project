using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class tempDlg : Form
    {
        public tempDlg()
        {
            InitializeComponent();
        }
        public string atc_name;
        private void button1_Click(object sender, EventArgs e)
        {
            atc_name = textBox1.Text.ToString();

            this.DialogResult = DialogResult.OK;
        }
    }
}
