using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.Frm
{
    public partial class Dashboard_RobotStatusCtrl : UserControl
    {
        public Dashboard_RobotStatusCtrl()
        {
            InitializeComponent();
        }

        DashboardForm mainfrm;
        public Dashboard_RobotStatusCtrl(DashboardForm mfrm)
        {
            mainfrm = mfrm;
            InitializeComponent();
        }

        private void Dashboard_RobotStatusCtrl_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                string strrobotid = string.Format("R_00{0}", i + 4);
                string strrobotname = string.Format("로봇50{0}", i + 1);
                string[] strRobotstatus = { strrobotid, strrobotname };
                dataGridView_runingrobot.Rows.Add(strRobotstatus);
            }


            for (int i = 0; i < 3; i++)
            {
                string strrobotid = string.Format("R_00{0}", i + 7);
                string strrobotname = string.Format("로봇10{0}", i + 1);
                string[] strRobotstatus = { strrobotid, strrobotname };
                dataGridView_idlerobot.Rows.Add(strRobotstatus);
            }

            for (int i = 0; i < 1; i++)
            {
                string strrobotid = string.Format("R_00{0}", i + 17);
                string strrobotname = string.Format("로봇30{0}", i + 1);
                string[] strRobotstatus = { strrobotid, strrobotname };
                dataGridView_errorrobot.Rows.Add(strRobotstatus);
            }

            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                int cnt = mainfrm.G_robots_live_status.Count;
                int workcnt =0;
                int idlecnt = 0;
                int deadcnt = 0;

                dataGridView_idlerobot.Rows.Clear();
                dataGridView_runingrobot.Rows.Clear();
                dataGridView_errorrobot.Rows.Clear();

                List<string> strrobot=new List<string>();
                for (int i = 0; i < cnt; i++)
                {
                    KeyValuePair<string, RobotLive_Status> info = mainfrm.G_robots_live_status.ElementAt(i);

                    if(info.Value.strRobotLiveStatus == "wait" || info.Value.strRobotLiveStatus == "live")
                    {
                        string strrobotid = info.Key;
                        string strrobotname = "";
                       if (strrobotid == "R_005") strrobotname = "로봇1";
                       else if (strrobotid == "R_006") strrobotname = "로봇2";
                        else if (strrobotid == "R_008") strrobotname = "로봇3";
                        else if (strrobotid == "R_007") strrobotname = "로봇4";
                        string[] strRobotstatus = { strrobotid, strrobotname };
                        dataGridView_idlerobot.Rows.Add(strRobotstatus);

                        idlecnt += 1;

                        dataGridView_runingrobot.Rows.Add(strRobotstatus);

                        workcnt += 1;
                    }
                    else if (info.Value.strRobotLiveStatus == "jobing")
                    {
                        string strrobotid = info.Key;
                        string strrobotname = "";
                        if (strrobotid == "R_005") strrobotname = "로봇1";
                        else if (strrobotid == "R_006") strrobotname = "로봇2";
                        else if (strrobotid == "R_008") strrobotname = "로봇3";
                        else if (strrobotid == "R_007") strrobotname = "로봇4";
                        string[] strRobotstatus = { strrobotid, strrobotname };
                    //    dataGridView_runingrobot.Rows.Add(strRobotstatus);

                     //   workcnt += 1;
                    }
                    else if (info.Value.strRobotLiveStatus == "dead")
                    {
                        string strrobotid = info.Key;
                        string strrobotname = "";
                        if (strrobotid == "R_005") strrobotname = "로봇1";
                        else if (strrobotid == "R_006") strrobotname = "로봇2";
                        else if (strrobotid == "R_008") strrobotname = "로봇3";
                        else if (strrobotid == "R_007") strrobotname = "로봇4";
                        string[] strRobotstatus = { strrobotid, strrobotname };
                        dataGridView_errorrobot.Rows.Add(strRobotstatus);
                        deadcnt += 1;
                    }
                   
                }

                Invoke(new MethodInvoker(delegate ()
                {
                    lblWorkcnt.Text = string.Format("{0}", workcnt);
                    lblidlecnt.Text = string.Format("{0}", idlecnt);
                    lbldeadcnt.Text = string.Format("{0}", deadcnt);

                }));

                //Data.Instance
            }
        }
    }
}
