using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace SysSolution.UR_Sample
{
    public partial class URControl_TestFrm : Form
    {
        Worker worker;

        Frm.ingdlg ingdlg = new Frm.ingdlg();
        string m_strRobotName = "R_007";

        public URControl_TestFrm()
        {
            InitializeComponent();
        }

        private void URControl_TestFrm_Load(object sender, EventArgs e)
        {
#if _urtest
            Data.Instance.MAINFORM = this;

#endif
            worker = new Worker(this, 1);

            groupBox_UR.Enabled = false;
            
            string strrobotid = "R_007";
         
            Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));

            timer1.Interval = 300;
            

            numericUpDown_UR_RepeatCnt.Value = 10;

            

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown_UR_RepeatCnt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox_UR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region 기본 ros 연결/해제
        /// <summary>
        /// Form강제종료시 처리할 내용들
        /// </summary>
        public void onCompulsion_Close() //강제종료시 처리할 부분들
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    worker.onDeleteAllSubscribe_Compulsion();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onCompulsion_Close err" + ex.Message.ToString());
                }
            }
        }

        private async void ROSConnection(string strAddr)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    onCompulsion_Close();

                    ROSDisconnect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ROSConnection err =" + ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    if (btnConnect.Text == "connect")
                    {
                        ingdlg.onLblMsg("서버에 연결중입니다.");
                        ingdlg.Show();
                        string uri = strAddr;
                        Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                        Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                        await Data.Instance.md.StartAsync();

                        Data.Instance.isConnected = true;

                        if (Data.Instance.isConnected == true)
                        {


                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";
                            groupBox_UR.Enabled = true;
                        
                            ingdlg.Hide();
                            worker.onURStatus_subscribe("R_007");

                            timer1.Enabled = true;
                        }
                    }
                    else
                    {
                        ROSDisconnect();

                        btnConnect.Enabled = true;
                        ingdlg.Hide();
                        timer1.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    //ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
                        ingdlg.Hide();
                        btnConnect.Enabled = true;
                        MessageBox.Show("연결에 실패하였습니다.");
                        timer1.Enabled = false;
                    }
                    return;
                }
            }
        }

        public void ROSDisconnect()
        {
            try
            {
                if (Data.Instance.md != null)
                {
                    Data.Instance.md.Dispose();
                    Data.Instance.md = null;
                }
                Data.Instance.isConnected = false;

                btnConnect.Text = "connect";
                btnConnect.Enabled = true;
                Data.Instance.socket = null;
                
                groupBox_UR.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        #endregion

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

        public void onURListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox_UR.Items.Add(msg);
                // listBox1.SelectedIndex = listBox1.Items.Count - 1;

                if (listBox_UR.Items.Count > 100)
                    listBox_UR.Items.Clear();
            }));
        }
        public void updateURListDP(string strtopic, string msg)
        {
            onURListmsg(string.Format("topic={0}..data={1}", strtopic, msg));
        }

        private void btnAssembly_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {

                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_assembly";
                    string strworkid = "UR_assembly1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:assembly";
                    //strSelectworkdata_Worker[0] = "UR assembly test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnAssembly, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnAssembly_Click err :={0}", ex.Message.ToString());
            }
        }
        private async void btnFolding_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {

                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_folding";
                    string strworkid = "UR_folding1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:folding";
                    //strSelectworkdata_Worker[0] = "UR folding test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnFolding,500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnFolding_Click err :={0}", ex.Message.ToString());
            }

        }

        private async void btnHoming_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_homing";
                    string strworkid = "UR_homing1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:homing";
                    //strSelectworkdata_Worker[0] = "UR homing test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnHoming, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnHoming_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnObstacle_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_obstacle";
                    string strworkid = "UR_obstacle1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    string strmsg = "type:URMISSION/mode:1/countnumber:1/ur_command:obstacle";
                    //strSelectworkdata_Worker[0] = "UR obstacle test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnObstacle, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnObstacle_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnWaving_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_waving";
                    string strworkid = "UR_waving1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                   int nrepeatcnt =  (int)numericUpDown_UR_RepeatCnt.Value;

                    string strmsg = string.Format("type:URMISSION/mode:1/countnumber:{0}/ur_command:waving", nrepeatcnt);
                    //strSelectworkdata_Worker[0] = "UR waving test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnWaving, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnWaving_Click err :={0}", ex.Message.ToString());
            }
        }

        private void btnWriting_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_writing";
                    string strworkid = "UR_writing1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    int nrepeatcnt = (int)numericUpDown_UR_RepeatCnt.Value;

                    string strmsg = string.Format("type:URMISSION/mode:1/countnumber:{0}/ur_command:writing", nrepeatcnt);
                   // strSelectworkdata_Worker[0] = "UR writing test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnWriting, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnWriting_Click err :={0}", ex.Message.ToString());
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                    string[] strSelectworkdata_Worker;
                    string strworkname = "UR_stop";
                    string strworkid = "UR_stop1";
                    int nworkcnt = 1;

                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[1];
                    strSelectRobot_Worker[0] = m_strRobotName;

                    string strmsg = "type:URMISSION/mode:0/countnumber:0/ur_switch_to:stop";
                    //strSelectworkdata_Worker[0] = "UR stop test";
                    strSelectworkdata_Worker[0] = strmsg;

                    var task = Task.Run(() => worker.onWorkOrder_publish_new(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));

                    onbtnDelay(btnStop, 500);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnStop_Click err :={0}", ex.Message.ToString());
            }
        }

        private void onbtnDelay(Button btn, int ndelaytime)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                btn.Enabled = false;
                Thread.Sleep(ndelaytime);
                btn.Enabled = true;
            }));

        }
        private void onUrStatus_DP()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(m_strRobotName))
                    {
                        string strtopic = "";
                        string strstatus = "";
                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status == null) return;
                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg == null) return;


                        if (Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg != null)
                        {
                            strtopic = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.topic;
                            strstatus= Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg.status;

                           List<string> strname= Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg.arm_status.name;
                           List<float> position = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg.arm_status.position;
                           List<float> velocity = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.ur_status.msg.arm_status.velocity;

                            string strparam = "";

                            for(int i=0; i<strname.Count; i++)
                            {
                                strparam += string.Format("name:{0},position:{1:f2},velocity:{2:f2}", strname[i], position[i], velocity[i]);
                            }


                            string strmsg = string.Format("topic:{0},status:{1},param={2}", strtopic, strstatus, strparam);

                            updateURListDP(strtopic, strmsg);
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("timer1_Tick err :={0}", ex.Message.ToString());
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                // updateURListDP(strtopic, msg);
                onUrStatus_DP();
            }
        }

        
    }
}
