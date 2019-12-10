using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using System.Drawing.Drawing2D;

using System.Drawing.Imaging;
using System.IO;

namespace SysSolution
{
    public partial class XISMonitoringCtrl : UserControl
    {
        public XISMonitoringCtrl()
        {
            InitializeComponent();
        }

        public XISMonitoringCtrl(Frm.RobotStatusFrm mfrm)
        {
            mainForm2 = mfrm;
            InitializeComponent();
        }

        Frm.RobotStatusFrm mainForm2;
        public string m_strRobotName = "";
        string m_strCurrentRobotDetailInfo_ID = "";
        public bool m_bLive = false;

        public string m_strLog_File = "";

        private void XISMonitoringCtrl_Load(object sender, EventArgs e)
        {
            pictureBox_goods.Image = Properties.Resource_jo.drum;
            pictureBox_pallet.Image = Properties.Resource_jo.palletstation;
            pictureBox_Lamp.BackColor = Color.Green;
        }


        public void onInitSet()
        {
            // this.DoubleBuffered = true;

            if (Data.Instance.isConnected)
            {

                m_strCurrentRobotDetailInfo_ID = m_strRobotName;

                //pictureBox_goods.BackColor = Color.Blue;
                

                UI_Updatetimer.Interval = 200;
                UI_Updatetimer.Enabled = true;

            }
        }
        
        public  void onPallet_DP(bool bexist)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if(bexist) pictureBox_pallet.Image = Properties.Resource_jo.palletstation_goods;
                else pictureBox_pallet.Image = Properties.Resource_jo.palletstation;

                pictureBox_pallet.Invalidate();
            }));
       }
        string[] strgoodsname = { "물", "황산", "불산" };
        public void onGoodsCode_DP(int ncode)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (ncode == 0)
                {
                    pictureBox_goodscode.Image = Properties.Resource_jo.MarkerData_0;
                    lblgoodsname.Text = strgoodsname[ncode];
                    pictureBox_goodscode.Invalidate();
                }
                else if (ncode == 1)
                {
                    pictureBox_goodscode.Image = Properties.Resource_jo.MarkerData_1;
                    lblgoodsname.Text = strgoodsname[ncode]; ;
                    pictureBox_goodscode.Invalidate();
                }
                else if (ncode == 2)
                {
                    pictureBox_goodscode.Image = Properties.Resource_jo.MarkerData_2;
                    lblgoodsname.Text = strgoodsname[ncode];

                    pictureBox_goodscode.Invalidate();
                }
            }));
        }

        public void onPalletLamp_DP(bool bexist)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (bexist)
                    pictureBox_Lamp.BackColor = Color.Blue;
                else pictureBox_Lamp.BackColor = Color.Green;

                pictureBox_Lamp.Invalidate();

            }));
        }

        public void onTimerOff()
        {
            UI_Updatetimer.Enabled = false;
        }

        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {
            onXISStatusDP_Update();
        }

        public bool m_bPalleton = false;
        public bool m_bGoodson = false;
        public int m_nGoodsKinds = 0;

        public void onXISStatusDP_Update()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    try {
                        foreach (KeyValuePair<string, XISState> info in Data.Instance.XIS_Status_Info)
                        {
                            string key = info.Key;
                            XISState value = info.Value;

                            if (value.status == null) continue;
                            if (value.status.data.Count < 1) continue;

                            int npalletstatus = value.status.data[1];

                            if (npalletstatus == 1)
                            {
                                onPalletLamp_DP(true);
                                onPallet_DP(true);
                            }
                            else if (npalletstatus == 2)
                            {
                                onPalletLamp_DP(false);
                                onPallet_DP(false);
                            }
                        }
                     }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("onXISStatusDP_Update - 1 pallet err :={0}", ex.Message.ToString());
                    }

                    try
                    {
                        foreach (KeyValuePair<string, Robot_WorkKInfo> info2 in Data.Instance.Robot_work_info)
                        {
                            string key = info2.Key;
                            Robot_WorkKInfo value = info2.Value;

                            if (value.robot_status_info.markers.msg == null) continue;
                            if (value.robot_status_info.markers.msg.markers.Count < 1) continue;

                            int cn = value.robot_status_info.markers.msg.markers.Count;

                            int nmarker = value.robot_status_info.markers.msg.markers[cn - 1].id;
                            onGoodsCode_DP(nmarker);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("onXISStatusDP_Update - 2 marker err :={0}", ex.Message.ToString());
                    }

                    #region test 


                    if (m_bGoodson)
                    {
                //        onGoodsCode_DP(m_nGoodsKinds);
                    }
                    #endregion


                    if (Data.Instance.Robot_work_info.ContainsKey(m_strRobotName))
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onXISStatusDP_Update err :={0}", ex.Message.ToString());
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
