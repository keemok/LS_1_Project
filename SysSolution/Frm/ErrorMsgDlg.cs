using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.Frm
{
    public partial class ErrorMsgDlg : Form
    {
        public ErrorMsgDlg()
        {
            InitializeComponent();
        }
        public string m_strErrTitle = "";

        public void onErrTitle(string strmsg, List<string>errlist)
        {
            //Invoke(new MethodInvoker(delegate ()
            // {

            listBox1.Items.Clear();
            m_strErrTitle = strmsg;
            lblErrTitle.Text = m_strErrTitle;

            for (int i = 0; i < errlist.Count(); i++)
            {
                listBox1.Items.Add(errlist[i]);
            }

            timer1.Interval = 250;
            timer1.Enabled = true;

            // }));

        }

        private void ErrorMsgDlg_Load(object sender, EventArgs e)
        {

        }
        bool _b = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_b)
            {
                lblErrTitle.ForeColor = Color.Red;

            }
            else lblErrTitle.ForeColor = Color.White;

            _b = !_b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
