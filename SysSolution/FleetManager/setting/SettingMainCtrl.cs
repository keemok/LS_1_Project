using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//add using info
using Rosbridge.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Numerics;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using MySql.Data.MySqlClient;

using System.Drawing.Imaging;using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace SysSolution.FleetManager
{
    public partial class SettingMainCtrl : UserControl
    {
        FleetManager_MainForm mainform;
        setting.RobotReg_Ctrl robotregctrl;
        setting.JobReg_Ctrl jobregctrl;

        public string m_strDBConnectionString = "";


        public SettingMainCtrl()
        {
            InitializeComponent();
        }

        public SettingMainCtrl(FleetManager_MainForm frm)
        {
            mainform = frm;
            InitializeComponent();
        }


        private void SettingMainCtrl_Load(object sender, EventArgs e)
        {
            if (panel_setting.Controls.Count == 1) panel_setting.Controls.RemoveAt(0);

            robotregctrl = new setting.RobotReg_Ctrl(mainform, this);
            jobregctrl = new setting.JobReg_Ctrl(mainform, this);
        }


        public void onInitSet()
        {
        }


        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button  ownbtn = (Button)sender;
            ownbtn.ForeColor = Color.White;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Button ownbtn = (Button)sender;
            ownbtn.ForeColor = System.Drawing.Color.Black;
        }

        private void btnRobotReg_Click(object sender, EventArgs e)
        {
            panel_setting.Size = new Size(917, 600);

            if (panel_setting.Controls.Count == 1) panel_setting.Controls.RemoveAt(0);

            panel_setting.Controls.Add(robotregctrl);
            robotregctrl.onInitSet();
        }

        private void btnXisReg_Click(object sender, EventArgs e)
        {
            if (panel_setting.Controls.Count == 1) panel_setting.Controls.RemoveAt(0);
        }

        private void btnJobReg_Click(object sender, EventArgs e)
        {
            panel_setting.Size = new Size(1100, 760);

            if (panel_setting.Controls.Count == 1) panel_setting.Controls.RemoveAt(0);
            panel_setting.Controls.Add(jobregctrl);

            jobregctrl.onInitSet();
        }
    }
}
