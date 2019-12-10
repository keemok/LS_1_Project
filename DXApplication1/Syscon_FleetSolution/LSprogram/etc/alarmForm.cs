using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram.etc
{
    public partial class alarmForm : Form
    {
        public alarmForm()
        {
            InitializeComponent();
        }
        string alarm_name;
        LSprogram.mainForm mainform;
        public alarmForm(mainForm frm)
        {
            InitializeComponent();
            mainform = frm;
            alarmName.Text = "-";
        }
        public void alarmOccur(string message)
        {
            alarmName.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alarmName.Text = "-";
            this.Close();
        }
    }
}
