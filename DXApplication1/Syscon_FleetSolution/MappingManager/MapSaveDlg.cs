using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.MappingManager
{
    public partial class MapSaveDlg : Form
    {
        public MapSaveDlg()
        {
            InitializeComponent();
        }

        public string strRobotID { get; set; }
        public string strMapID { get; set; }

        public void onInitSet(string strrobotid)
        {
            txtRobotID.Text = strrobotid;
        }

        private void MapSaveDlg_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
            txtMapID_2.Text = string.Format("mapid_{0}", strtime);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            strRobotID = txtRobotID.Text.ToString();
            strMapID = txtMapID_1.Text.ToString() + "_" + txtMapID_2.Text.ToString();

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
