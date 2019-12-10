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

namespace Syscon_Solution.FleetManager.UI.Edit
{
    public partial class MapEdit_Ctrl : UserControl
    {
        Fleet_Main mainform;

        public MapEdit_Ctrl()
        {
            InitializeComponent();
        }

        public MapEdit_Ctrl(Fleet_Main frm)
        {
            mainform = frm;
            InitializeComponent();
        }

        public void onInitSet()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("MapEdit_Ctrl ..onInitSet err" + ex.Message.ToString());
            }
        }

       

        private void MapEdit_Ctrl_Load(object sender, EventArgs e)
        {

        }

        private void btnMaploading_Click(object sender, EventArgs e)
        {

        }
    }
}
