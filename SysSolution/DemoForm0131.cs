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
using System.Windows.Forms.DataVisualization.Charting;

namespace SysSolution
{
    public partial class DemoForm0131 : Form
    {
        Worker worker;
        string m_strWorklist_File = "";
        string m_strRobot_Status_File = "";
        string m_strCurrentSelectWork = "";
        string m_strCurrentSelectWorkFile = "";

        /// <summary>
        /// 현재 작업중이로봇중 세부정보를 보고싶은 로봇id 
        /// </summary>
        string m_strCurrentRobotDetailInfo_ID = "";

        /// <summary>
        /// 리스트에서 선택된 작업들을 표시 및 update용으로 사용됨.. 
        /// </summary>
        TreeNode treenode_CurrentSelectWork = new TreeNode();

        List<string> m_strWorkTitle_list = new List<string>(); //워크 작업명을 저장
        Graphics g;
        public DemoForm0131()
        {
            InitializeComponent();
        }

        private void DemoForm0131_Load(object sender, EventArgs e)
        {
#if _demo
            Data.Instance.MAINFORM = this;

#endif
            g = panel2.CreateGraphics();

            m_strWorklist_File = Application.StartupPath + "\\worklist.txt";
            m_strRobot_Status_File = Application.StartupPath + "\\Robot.txt";

            worker = new Worker(this, 1);

            onWorklist_Open();
            onRobotFile_Open();

            onEditControlClear();
            //    groupBox_goalpoint.Visible = false;
            //    groupBox_basicmove.Visible = false;
            //    groupBox_lift.Visible = false;
            //   groupBox_plc.Visible = false;
            //  label1.Visible = false;

            label_Workname.Text = "";


            //tabControl1.Visible = false;

            UI_Updatetimer.Interval = 250;
            UI_Updatetimer.Enabled = true;

            myTimer.Enabled = true;
            myTimer.Interval = 10;

            txtWorkCnt.Text = "1";
            txtCurrWorkCnt.Text = "1";
            numericUpDown_WorkCnt.Value = 1;

            worklooptimer1.Interval = 50;
            worklooptimer1.Enabled = true;

            numericUpDown1.Value = 100;
            numericUpDown3.Value = 1;


            timer_initpos.Interval = 50;
            timer_initpos.Enabled = true;

            worker.initpos_Evt += new Worker.InitPosComplete(this.InitPosComplete);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            //btnConnect.Enabled = false;
            ROSConnection(strAddr);
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


                    //   await Data.Instance.md.StopAsync();
                    //   Data.Instance.md = null;

                    //   Data.Instance.isConnected = false;

                    ROSDisconnect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ROSConnection err" + ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    if (btnConnect.Text == "connect")
                    {

                        string uri = strAddr;
                        Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                        Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                        await Data.Instance.md.StartAsync();

                        Data.Instance.isConnected = true;

                        if (Data.Instance.isConnected == true)
                        {
                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";
                        }
                    }
                    else
                    {
                        ROSDisconnect();

                        btnConnect.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    //ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
                        MessageBox.Show("연결에 실패하였습니다.");
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
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err"+ex.Message.ToString());
            }
        }

        #endregion

        private void listBox_Work_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Work.SelectedIndex > -1)
            {

                listBox_Worksub.Items.Clear();

                //      label1.Visible = false;
                //      groupBox_goalpoint.Visible = false;
                //       groupBox_basicmove.Visible = false;
                //       groupBox_lift.Visible = false;
                //      groupBox_plc.Visible = false;

                Invoke(new MethodInvoker(delegate ()
                {
                    label_Workname.Text = "";

                    if (listBox_Work.SelectedIndex < 0)
                        return;

                    m_strCurrentSelectWork = listBox_Work.SelectedItem.ToString();

                    string strtemp = "";
                    strtemp = m_strCurrentSelectWork;
                    int idx = strtemp.IndexOf('<');

                    strtemp = strtemp.Substring(idx + 1, strtemp.Length - 2);

                    m_strCurrentSelectWorkFile = Application.StartupPath + "\\" + strtemp + ".xml";
                }));

                onWorklist_SubFileOpen(m_strCurrentSelectWork);
            }
        }
        private void listBox_Worksub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Worksub.SelectedIndex < 0)
                return;

            Invoke(new MethodInvoker(delegate ()
            {
                onEditControlClear();

                //     label1.Visible = true;
                //     groupBox_goalpoint.Visible = true;
                //     groupBox_basicmove.Visible = true;
                //     groupBox_lift.Visible = true;
                //      groupBox_plc.Visible = true;


                //label_Workname.Text = "";
                //name이 있는 노드는 읽어오지않는다. 
                string strnodename = treenode_CurrentSelectWork.Nodes[0].Nodes[listBox_Worksub.SelectedIndex + 1].Text.ToString();
                string strnodedata = treenode_CurrentSelectWork.Nodes[0].Nodes[listBox_Worksub.SelectedIndex + 1].FirstNode.Text.ToString();
                //label_Workname.Text = strnodedata;

                string[] strgoal_sub = strnodedata.Split('/');

                if (strnodename == "Goal-Point")
                {
                    float x, y, th;
                    string[] strsub_act_param = strgoal_sub[1].Split(':');
                    txtGoalpoint_X.Text = strsub_act_param[1];

                    strsub_act_param = strgoal_sub[2].Split(':');
                    txtGoalpoint_Y.Text = strsub_act_param[1];

                    strsub_act_param = strgoal_sub[3].Split(':');
                    txtGoalpoint_theta.Text = strsub_act_param[1];

                }
                else if (strnodename == "Basic-Move")
                {
                    cboBasicmove_mode.Items.Add("Go");
                    cboBasicmove_mode.Items.Add("Rotate");

                    string[] strsub_act_param = strgoal_sub[1].Split(':');
                    if (strsub_act_param[1] == "Go")
                        cboBasicmove_mode.SelectedIndex = 0;
                    else
                        cboBasicmove_mode.SelectedIndex = 1;

                    strsub_act_param = strgoal_sub[2].Split(':');

                    txtBasicmove_action.Text = strsub_act_param[1];


                }
                else if (strnodename == "Lift-Conveyor-Control")
                {
                    cboLiftConveyor_mode.Items.Add("Lift(Top_Bottom)");
                    cboLiftConveyor_mode.Items.Add("Lift(Set height)");
                    cboLiftConveyor_mode.Items.Add("Conveyor(Transfer)");
                    cboLiftConveyor_mode.Items.Add("Conveyor(Recevier)");

                    cboLiftConveyor_action.Items.Clear();
                    string[] strsub_act_param = strgoal_sub[1].Split(':');
                    if (strsub_act_param[1] == "Top-Bottom")
                    {
                        cboLiftConveyor_action.Visible = true;

                        cboLiftConveyor_mode.SelectedIndex = 0;
                        cboLiftConveyor_action.Items.Add("Top");
                        cboLiftConveyor_action.Items.Add("Bottom");

                        strsub_act_param = strgoal_sub[2].Split(':');

                        char cCR = (char)0x0d;
                        char cLF = (char)0x0a;
                        char[] cCarrige = { (char)0x0d, (char)0x0a };
                        string strpara = strsub_act_param[1];
                        if (strpara.IndexOf(cCR) > -1)
                        {
                            strpara = strpara.Trim(cCarrige);
                        }


                        if (strpara == "Top")
                            cboLiftConveyor_action.SelectedIndex = 0;
                        else cboLiftConveyor_action.SelectedIndex = 1;

                    }
                    else if (strsub_act_param[1] == "Set-Height")
                    {
                        txtLiftConveyor_action.Visible = true;

                        cboLiftConveyor_mode.SelectedIndex = 1;

                        strsub_act_param = strgoal_sub[2].Split(':');

                        txtLiftConveyor_action.Text = strsub_act_param[1];

                    }
                    else if (strsub_act_param[1] == "Transfer")
                    {
                        cboLiftConveyor_action.Visible = true;

                        cboLiftConveyor_mode.SelectedIndex = 2;

                        cboLiftConveyor_action.Items.Add("Forward");
                        cboLiftConveyor_action.Items.Add("Backward");

                        strsub_act_param = strgoal_sub[2].Split(':');

                        char cCR = (char)0x0d;
                        char cLF = (char)0x0a;
                        char[] cCarrige = { (char)0x0d, (char)0x0a };
                        string strpara = strsub_act_param[1];
                        if (strpara.IndexOf(cCR) > -1)
                        {
                            strpara = strpara.Trim(cCarrige);
                        }

                        if (strpara == "Forward")
                            cboLiftConveyor_action.SelectedIndex = 0;
                        else cboLiftConveyor_action.SelectedIndex = 1;
                    }
                    else if (strsub_act_param[1] == "Receiver")
                    {
                        cboLiftConveyor_action.Visible = true;

                        cboLiftConveyor_mode.SelectedIndex = 3;


                        cboLiftConveyor_action.Items.Add("Forward");
                        cboLiftConveyor_action.Items.Add("Backward");

                        strsub_act_param = strgoal_sub[2].Split(':');

                        char cCR = (char)0x0d;
                        char cLF = (char)0x0a;
                        char[] cCarrige = { (char)0x0d, (char)0x0a };
                        string strpara = strsub_act_param[1];
                        if (strpara.IndexOf(cCR) > -1)
                        {
                            strpara = strpara.Trim(cCarrige);
                        }

                        if (strpara == "Forward")
                            cboLiftConveyor_action.SelectedIndex = 0;
                        else cboLiftConveyor_action.SelectedIndex = 1;
                    }
                }
                else if (strnodename == "PLC")
                { }
            }));
        }

        public void onWorklist_SubFileOpen(string strSelectWorkFile)
        {
            try
            {
                int idx = strSelectWorkFile.IndexOf('<');

                strSelectWorkFile = strSelectWorkFile.Substring(idx + 1, strSelectWorkFile.Length - 2);

                strSelectWorkFile = Application.StartupPath + "\\" + strSelectWorkFile + ".xml";

                if (!File.Exists(strSelectWorkFile))
                {
                    return;
                }

                treenode_CurrentSelectWork = new TreeNode();

                treenode_CurrentSelectWork.Nodes.Clear();
                TreeViewSerializer serializer = new TreeViewSerializer();
                TreeNode treenode = serializer.LoadXmlFileInTreeNode_sub(strSelectWorkFile);

                treenode_CurrentSelectWork = treenode;

                foreach (TreeNode childNode in treenode.Nodes[0].Nodes)
                {
                    string tnode = childNode.Text.ToString();
                    if (tnode == "name")
                    {
                        string strworkname = childNode.FirstNode.Text.ToString();

                        Invoke(new MethodInvoker(delegate ()
                        {
                            char cCR = (char)0x0d;
                            char cLF = (char)0x0a;
                            char[] cCarrige = { (char)0x0d, (char)0x0a };

                            if (strworkname.IndexOf(cCR) > -1)
                            {
                                strworkname = strworkname.Trim(cCarrige);
                            }
                            if (strworkname == "Run_Lift")
                                strworkname = "일반 주행 + 리프트 동작";
                            label_Workname.Text = "미션명: " + strworkname;
                        }));
                    }
                    else
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                            listBox_Worksub.Items.Add(tnode);
                        }));
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorklist_SubFileOpen err :={0}", ex.Message.ToString());
            }
        }

        private void btnGoalPoint_update_Click(object sender, EventArgs e)
        {
            //type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3



            string strX = txtGoalpoint_X.Text.ToString();
            string strY = txtGoalpoint_Y.Text.ToString();
            string strTh = txtGoalpoint_theta.Text.ToString();

            if (strX == "" || strY == "" || strTh == "")
            {
                MessageBox.Show("정보 입력 오류");
                return;
            }

            string strupdateData = string.Format("type:Goal-Point/x:{0}/y:{1}/theta:{2}/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3", strX, strY, strTh);

            treenode_CurrentSelectWork.Nodes[0].Nodes[listBox_Worksub.SelectedIndex].FirstNode.Text = strupdateData;


            try
            {
                string strSelectWorkFile = m_strCurrentSelectWork;
                int idx = strSelectWorkFile.IndexOf('<');

                strSelectWorkFile = strSelectWorkFile.Substring(idx + 1, strSelectWorkFile.Length - 2);

                strSelectWorkFile = Application.StartupPath + "\\" + strSelectWorkFile + ".xml";

                int cnt = treenode_CurrentSelectWork.Nodes.Count;
                int nextcnt = treenode_CurrentSelectWork.Nodes[0].Nodes.Count;


                using (StreamWriter sw = new System.IO.StreamWriter(strSelectWorkFile, false, Encoding.Default))
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        char[] chr = new char[2];
                        chr[0] = (char)0x0d;
                        chr[1] = (char)0x0a;

                        string strdata = "";

                        strdata = "<" + treenode_CurrentSelectWork.FirstNode.Text.ToString() + ">";
                        strdata = strdata.Trim(chr);
                        sw.WriteLine(strdata);
                        for (int j = 0; j < nextcnt; j++)
                        {
                            strdata = "<" + treenode_CurrentSelectWork.Nodes[0].Nodes[j].Text.ToString() + ">";


                            strdata = strdata.Trim(chr);
                            sw.WriteLine(strdata);

                            strdata = treenode_CurrentSelectWork.Nodes[0].Nodes[j].FirstNode.Text.ToString();
                            strdata = strdata.Trim(chr);
                            sw.WriteLine(strdata);

                            strdata = "</" + treenode_CurrentSelectWork.Nodes[0].Nodes[j].Text.ToString() + ">";
                            strdata = strdata.Trim(chr);
                            sw.WriteLine(strdata);
                        }
                        strdata = "</" + treenode_CurrentSelectWork.FirstNode.Text.ToString() + ">";
                        strdata = strdata.Trim(chr);
                        sw.WriteLine(strdata);
                    }


                    sw.Close();
                }
                MessageBox.Show("수정 완료");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("btnGoalPoint_update err :={0}", ex.Message.ToString());
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int nrow = dataGridView1.SelectedCells[0].RowIndex;

            if (nrow < 0 || nrow > dataGridView1.RowCount - 2) return;


            x1 = 0;
            y1 = 0;
            x2 = 0;
            y2 = 0;

            string strrobot = dataGridView1["robotid", nrow].Value.ToString();
            m_strCurrentRobotDetailInfo_ID = strrobot;

            txtRobotModel.Text = "";

            txtRobotpos_X.Text = "";
            txtRobotpos_Y.Text = "";
            txtRobotpos_Theta.Text = "";

            txtRobotMotorspeed_setting_Right.Text = "";
            txtRobotMotorspeed_setting_Left.Text = "";
            txtRobotMotorspeed_return_Right.Text = "";
            txtRobotMotorspeed_return_Left.Text = "";

            txtRobotspeed_setting_linear_x.Text = "";
            txtRobotspeed_setting_linear_y.Text = "";
            txtRobotspeed_setting_linear_z.Text = "";

            txtRobotspeed_setting_angular_x.Text = "";
            txtRobotspeed_setting_angular_y.Text = "";
            txtRobotspeed_setting_angular_z.Text = "";

            txtRobotspeed_return_linear_x.Text = "";
            txtRobotspeed_return_linear_y.Text = "";
            txtRobotspeed_return_linear_z.Text = "";

            txtRobotspeed_return_angular_x.Text = "";
            txtRobotspeed_return_angular_y.Text = "";
            txtRobotspeed_return_angular_z.Text = "";

            txtUltrasonic_1.Text = "";
            txtUltrasonic_2.Text = "";
            txtUltrasonic_3.Text = "";
            txtUltrasonic_4.Text = "";
            txtUltrasonic_5.Text = "";
            txtUltrasonic_6.Text = "";
            txtUltrasonic_7.Text = "";
            txtUltrasonic_8.Text = "";


            //   tabControl1.Visible = true;

            txtActionStep.Text = "";


            //선속도, 각속도를 임시적으로 ui에서 저장후 표시 jo temp
            robot_linear_speed = new double[1000];
            robot_anglua_speed = new double[1000];
            nlinearspeed_idx = 0;
            nangularspeed_idx = 0;

            txtangle_min.Text = "";
            txtangle_max.Text = "";
            txtangle_incre.Text = "";
            txttime_incre.Text = "";
            txtscantime.Text = "";
            txtrange_min.Text = "";
            txtrange_max.Text = "";

            

            BMS_Amp = new double[1000];
            BMS_Volt = new double[1000];
            BMS_Temper = new double[1000];
            nbms_amp_idx = 0;
            nbms_volt_idx = 0;
            nbms_temper_idx = 0;

        }

        public void onWorklist_Open()
        {
            try
            {
                m_strWorkTitle_list.Clear();

                if (!File.Exists(m_strWorklist_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strWorklist_File, false, Encoding.Default))
                    {
                        sw.WriteLine("WID" + DateTime.Now.ToString("hhMMss"));
                        sw.Close();
                    }
                    //  return;
                }


                using (StreamReader sr1 = new System.IO.StreamReader(m_strWorklist_File, Encoding.Default))
                {
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        m_strWorkTitle_list.Add(strTemp);
                    }
                }                for (int i = 0; i < m_strWorkTitle_list.Count; i++)
                {
                    listBox_Work.Items.Add(m_strWorkTitle_list[i]);
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorklist_Open err :={0}", ex.Message.ToString());
            }
        }

        public void onRobotFile_Open()
        {
            try
            {
                if (!File.Exists(m_strRobot_Status_File))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_Status_File, false, Encoding.Default))
                    {
                        sw.WriteLine("robot_id,ip,work id,work status,work cnt,curr work cnt");
                        sw.Close();
                    }
                    //  return;
                }

                dataGridView1.Rows.Clear();


                using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_Status_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    Data.Instance.Robot_status_info.Clear();

                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strRobotstatus = strTemp.Split(',');
                            dataGridView1.Rows.Add(strRobotstatus);



                            Data.Instance.Robot_status_info.Add(strRobotstatus[0], strTemp);
                        }
                        ncnt++;
                    }
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotFile_Open err :={0}", ex.Message.ToString());
            }
        }



        public void onEditControlClear()
        {
            txtGoalpoint_X.Text = "";
            txtGoalpoint_Y.Text = "";
            txtGoalpoint_theta.Text = "";

            cboBasicmove_mode.Items.Clear();
            cboBasicmove_mode.Text = "";
            txtBasicmove_action.Text = "";
            cboLiftConveyor_mode.Items.Clear();
            cboLiftConveyor_mode.Text = "";
            cboLiftConveyor_action.Items.Clear();
            cboLiftConveyor_action.Text = "";
            txtLiftConveyor_action.Text = "";

            cboLiftConveyor_action.Visible = false;
            txtLiftConveyor_action.Visible = false;


            txtRobotModel.Text = "";

            txtRobotpos_X.Text = "";
            txtRobotpos_Y.Text = "";
            txtRobotpos_Theta.Text = "";
        }


        public void onUItimer_enable()
        {
            UI_Updatetimer.Enabled = true;
        }
        async void btnWorkStart_Click(object sender, EventArgs e)
        {
            Robot_Xpos.Clear();
            Robot_Ypos.Clear();
            panel2.BackColor = Color.Black;
            panel2.BackColor = Color.Linen;

            //panel2.Dispose();
            // Invoke(new MethodInvoker(delegate ()
            //   {
            if (listBox_Work.SelectedIndex < 0 || m_strCurrentSelectWorkFile == "")
            {
                MessageBox.Show("작업을 선택하세요");
                return;
            }

            if (numericUpDown_WorkCnt.Value == 0)
            {
                MessageBox.Show("작업횟수를 입력하세요");
                return;
            }
            //   }));


            RobotSelectForm robotselectform = new RobotSelectForm();
            robotselectform.onInit(m_strRobot_Status_File);

            // UI_Updatetimer.Enabled = false;
            Robotstatus_Updatetimer.Enabled = false;

            if (robotselectform.ShowDialog() == DialogResult.OK)
            {

                Robotstatus_Updatetimer.Enabled = true;


                List<string> strSelectRobot = new List<string>();
                List<string> strworkdata = new List<string>();

                strSelectRobot = robotselectform.selectRobot;

                string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
                string[] strSelectworkdata_Worker;

                try
                {
                    using (StreamReader sr1 = new System.IO.StreamReader(m_strCurrentSelectWorkFile, Encoding.Default))
                    {
                        while (sr1.Peek() >= 0)
                        {
                            string strTemp = sr1.ReadLine();



                            if (strTemp.IndexOf('<') < 0 && strTemp != "")
                            {
                                strworkdata.Add(strTemp);
                            }
                        }
                        sr1.Close();
                    }

                    Invoke(new MethodInvoker(delegate ()
                    {
                        if (strworkdata.Count < 0)
                        {
                            MessageBox.Show("작업 내용이 존재하지 않습니다.");
                            return;
                        }
                    }));

                    strSelectRobot_Worker = new string[strSelectRobot.Count];
                    strSelectworkdata_Worker = new string[strworkdata.Count - 1];

                    for (int i = 0; i < strSelectRobot.Count; i++)
                    {
                        strSelectRobot_Worker[i] = strSelectRobot[i];
                    }


                    string strworkname = strworkdata[0];
                    for (int i = 1; i < strworkdata.Count; i++)
                    {
                        strSelectworkdata_Worker[i - 1] = strworkdata[i];
                    }
                    int nworkcnt = (int)(numericUpDown_WorkCnt.Value);
                    //var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, m_strCurrentSelectWork, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));
                    //await task;
                    Thread.Sleep(500);


                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("btnWorkStart err :={0}", ex.Message.ToString());
                }

            }
        }

        private void numericUpDown_WorkCnt_ValueChanged(object sender, EventArgs e)
        {

        }


        private void btnSelectRobot_WorkCancel_Click(object sender, EventArgs e)
        {
            if (m_strCurrentRobotDetailInfo_ID == "")
                return;

            btnSelectRobot_WorkCancel.Enabled = false;
            // btnSelectRobot_WorkCancel.Text = "작업 취소중..";
            if (Data.Instance.isConnected)
            {
                try
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(m_strCurrentRobotDetailInfo_ID))
                    {
                        string strgoal_id = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].strGoalid;
                        worker.onWorkCancel_publish(m_strCurrentRobotDetailInfo_ID, strgoal_id);
                        Thread.Sleep(Data.Instance.nPulishDelayTime);
                        worker.onDeleteSelectRobotSubscribe(m_strCurrentRobotDetailInfo_ID);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnSelectRobot_WorkCancel_Click err ==" + ex.Message.ToString());
                }
            }
            btnSelectRobot_WorkCancel.Enabled = true;
            // btnSelectRobot_WorkCancel.Text = "작업취소";

        }



        private void UI_Updatetimer_Tick(object sender, EventArgs e)
        {
            Robotstatus_Updatetimer.Enabled = false;
            onRobotStatusDP_Update();
            Robotstatus_Updatetimer.Enabled = true;
        }

        private void Robotstatus_Updatetimer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new System.IO.StreamWriter(m_strRobot_Status_File, false, Encoding.Default))
                {
                    sw.WriteLine("robot_id,ip,work id,work status,work cnt,curr work cnt");

                    foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                    {
                        string key = info.Key;
                        string value = info.Value;

                        sw.WriteLine(value);
                    }

                    sw.Close();
                }



            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Robotstatus_Updatetimer_Tick err :={0}", ex.Message.ToString());
            }
        }


        int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

        List<int> Robot_Xpos = new List<int>();
        List<int> Robot_Ypos = new List<int>();

        bool bworkfinish = false;
        public void onRobotStatusDP_Update()
        {
            try
            {

                for (int i = 0; i < Data.Instance.Robot_status_info.Count; i++)
                {
                    string strrobot = dataGridView1["robotid", i].Value.ToString();
                    if (Data.Instance.Robot_status_info.ContainsKey(strrobot))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strrobot];
                        string[] strRobotstatus = strtemp.Split(',');

                        dataGridView1["workid", i].Value = strRobotstatus[2];

                        dataGridView1["Workcnt", i].Value = strRobotstatus[4];
                        dataGridView1["CurrWorkcnt", i].Value = strRobotstatus[5];

                        if (strRobotstatus[2] == "")
                        {
                            bworkfinish = true;
                        }
                        else
                            bworkfinish = false;

                    }

                    //  if (bworkfinish && b_initpos) b_initpos = false;
                    //  else b_initpos = true;



                }

                if (Data.Instance.isConnected)
                {
                    if (m_strCurrentRobotDetailInfo_ID == "")
                    { }
                    else
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(m_strCurrentRobotDetailInfo_ID))
                        {
                            string strrobotmodel = "";
                            double x = 0, y = 0, theta = 0;
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg != null)
                            {
                                strrobotmodel = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.type.ToString();
                                if (m_strCurrentRobotDetailInfo_ID == "R_005") strrobotmodel = "500_1";
                                else if (m_strCurrentRobotDetailInfo_ID == "R_006") strrobotmodel = "500_2";
                                else if (m_strCurrentRobotDetailInfo_ID == "R_007") strrobotmodel = "100A";
                                else if (m_strCurrentRobotDetailInfo_ID == "R_008") strrobotmodel = "300C";
                            }

                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg != null)
                            {
                                x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.x;

                                y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.y;
                                theta = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.robotstate.msg.pose.theta;

                                int tx = 0, ty = 0;

                                tx = (int)(x * 100) / 4; /// 2;
                                ty = (int)(y * 100) / 3;// / 4;

                                if (tx < 0) tx *= -1;
                                if (ty < 0) ty *= -1;

                                // if (tx > 400) tx += 400;
                                // else tx = 400 - tx;

                                // if (ty > 200) ty += 200;
                                // else ty = 200 - ty;

                                tx += 50;
                                ty += 50;

                                Robot_Xpos.Add(tx);
                                Robot_Ypos.Add(ty);


                                Graphics g = panel2.CreateGraphics();

                                panel2.Refresh();
                                if (Robot_Xpos.Count > 1 && Robot_Ypos.Count > 1)
                                {
                                    Pen pen = new Pen(Color.Blue, 2); //pen 객체생성및 속성정의(선을 그리기위해 pen이 필요)
                                    Pen pen2 = new Pen(Color.Black);
                                    for (int ng = 0; ng < Robot_Xpos.Count - 1; ng++)
                                    {

                                        g.DrawLine(pen, Robot_Ypos[ng], Robot_Xpos[ng], Robot_Ypos[ng + 1], Robot_Xpos[ng + 1]); //선그리기
                                                                                                                                 // g.DrawImage(image, Robot_Ypos[ng + 1], Robot_Xpos[ng + 1]);
                                    }
                                    //g.DrawRectangle(pen2, new Rectangle(Robot_Ypos[Robot_Ypos.Count-1] - 2, Robot_Xpos[Robot_Xpos.Count-1] - 2, 4, 4));
                                    g.FillRectangle(Brushes.Green, new Rectangle(Robot_Ypos[Robot_Ypos.Count - 1] - 3, Robot_Xpos[Robot_Xpos.Count - 1] - 3, 6, 6));

                                    g.Dispose();
                                }


                            }

                            string action_type = "";
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.workfeedback.msg != null)
                            {
                                int action_idx = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.workfeedback.msg.feedback.action_indx;
                                //action_type = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_workdata[action_idx].strActionType;
                            }
                            onMotorstate_DP();
                            onUltrasonic_DP();
                            onLidar_DP();

                            onRobotSpeed_DP();
                            onBMS_DP();

                            Invoke(new MethodInvoker(delegate ()
                            {
                                txtRobotModel.Text = strrobotmodel;
                                txtRobotpos_X.Text = string.Format("{0:F2}", x);
                                txtRobotpos_Y.Text = string.Format("{0:F2}", y);
                                txtRobotpos_Theta.Text = string.Format("{0:F2}", theta);
                                txtActionStep.Text = action_type;
                            }));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotStatusDP_Update err :={0}", ex.Message.ToString());
            }
        }

        private void onMotorstate_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg != null)
                {
                    float motorspeedsetting_right = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_rpm.data[0];
                    float motorspeedsetting_left = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_rpm.data[1];
                    float motorspeedreturn_right = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.rpm.data[0];
                    float motorspeedreturn_left = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.rpm.data[1];

                    double motor_Robotspeed_setting_linear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.x;
                    double motor_Robotspeed_setting_linear_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.y;
                    double motor_Robotspeed_setting_linear_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.linear.z;
                    double motor_Robotspeed_setting_angular_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.x;
                    double motor_Robotspeed_setting_angular_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.y;
                    double motor_Robotspeed_setting_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.cmd_vel.angular.z;

                    double motor_Robotspeed_returnlinear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.x;
                    double motor_Robotspeed_return_linear_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.y;
                    double motor_Robotspeed_return_linear_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.z;
                    double motor_Robotspeed_return_angular_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.x;
                    double motor_Robotspeed_return_angular_y = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.y;
                    double motor_Robotspeed_return_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.z;

                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtRobotMotorspeed_setting_Right.Text = string.Format("{0:F3}", motorspeedsetting_right);
                        txtRobotMotorspeed_setting_Left.Text = string.Format("{0:F3}", motorspeedsetting_left);
                        txtRobotMotorspeed_return_Right.Text = string.Format("{0:F3}", motorspeedreturn_right);
                        txtRobotMotorspeed_return_Left.Text = string.Format("{0:F3}", motorspeedreturn_left);

                        txtRobotspeed_setting_linear_x.Text = string.Format("{0:F3}", motor_Robotspeed_setting_linear_x);
                        txtRobotspeed_setting_linear_y.Text = string.Format("{0:F3}", motor_Robotspeed_setting_linear_y);
                        txtRobotspeed_setting_linear_z.Text = string.Format("{0:F3}", motor_Robotspeed_setting_linear_z);

                        txtRobotspeed_setting_angular_x.Text = string.Format("{0:F3}", motor_Robotspeed_setting_angular_x);
                        txtRobotspeed_setting_angular_y.Text = string.Format("{0:F3}", motor_Robotspeed_setting_angular_y);
                        txtRobotspeed_setting_angular_z.Text = string.Format("{0:F3}", motor_Robotspeed_setting_angular_z);

                        txtRobotspeed_return_linear_x.Text = string.Format("{0:F3}", motor_Robotspeed_returnlinear_x);
                        txtRobotspeed_return_linear_y.Text = string.Format("{0:F3}", motor_Robotspeed_return_linear_y);
                        txtRobotspeed_return_linear_z.Text = string.Format("{0:F3}", motor_Robotspeed_return_linear_z);

                        txtRobotspeed_return_angular_x.Text = string.Format("{0:F3}", motor_Robotspeed_return_angular_x);
                        txtRobotspeed_return_angular_y.Text = string.Format("{0:F3}", motor_Robotspeed_return_angular_y);
                        txtRobotspeed_return_angular_z.Text = string.Format("{0:F3}", motor_Robotspeed_return_angular_z);

                    }));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMotorstate_DP err :={0}", ex.Message.ToString());
            }
        }
        private void onUltrasonic_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg != null)
                {
                    if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data.Count < 1) return;

                    float ultrasonicraw_1 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[0];
                    float ultrasonicraw_2 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[1];
                    float ultrasonicraw_3 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[2];
                    float ultrasonicraw_4 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[3];
                    float ultrasonicraw_5 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[4];
                    float ultrasonicraw_6 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[5];
                    float ultrasonicraw_7 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[6];
                    float ultrasonicraw_8 = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.ultrasonic.msg.data[7];

                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtUltrasonic_1.Text = string.Format("{0:F3}", ultrasonicraw_1);
                        txtUltrasonic_2.Text = string.Format("{0:F3}", ultrasonicraw_2);
                        txtUltrasonic_3.Text = string.Format("{0:F3}", ultrasonicraw_3);
                        txtUltrasonic_4.Text = string.Format("{0:F3}", ultrasonicraw_4);
                        txtUltrasonic_5.Text = string.Format("{0:F3}", ultrasonicraw_5);
                        txtUltrasonic_6.Text = string.Format("{0:F3}", ultrasonicraw_6);
                        txtUltrasonic_7.Text = string.Format("{0:F3}", ultrasonicraw_7);
                        txtUltrasonic_8.Text = string.Format("{0:F3}", ultrasonicraw_8);

                    }));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onUltrasonic_DP err :={0}", ex.Message.ToString());
            }
        }

        private void onRobotSpeed_DP()
        {
            ChartSetting();
            ViewAll();
        }

        private void onBMS_DP()
        {
            ChartBMSSetting();
            ViewBMSAll();

        }

        private void onLidar_DP()
        {
            try
            {
                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg != null)
                {
                    float angle_min = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_min;
                    float angle_max = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_max;
                    float angle_incre = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.angle_increment;
                    float time_incre = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.time_increment;
                    float scan_time = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.scan_time;
                    float range_min = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_min;
                    float range_max = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.lidar.msg.range_max;



                    Invoke(new MethodInvoker(delegate ()
                    {
                        txtangle_min.Text = string.Format("{0:F3}", angle_min);
                        txtangle_max.Text = string.Format("{0:F3}", angle_max);
                        txtangle_incre.Text = string.Format("{0:F3}", angle_incre);
                        txttime_incre.Text = string.Format("{0:F3}", time_incre);
                        txtscantime.Text = string.Format("{0:F3}", scan_time);
                        txtrange_min.Text = string.Format("{0:F3}", range_min);
                        txtrange_max.Text = string.Format("{0:F3}", range_max);
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLidar_DP err :={0}", ex.Message.ToString());
            }
        }
        private void ChartSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제

            robotspeed_chart.ChartAreas.Clear();
            robotspeed_chart.Series.Clear();
            //1.ChartAreas
            robotspeed_chart.ChartAreas.Add("Draw"); ;
            robotspeed_chart.ChartAreas["Draw"].BackColor = Color.Black;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Minimum = 0;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Maximum = 120;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Interval = 10;
            robotspeed_chart.ChartAreas["Draw"].AxisX.Title = "시간";
            robotspeed_chart.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            robotspeed_chart.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            robotspeed_chart.ChartAreas["Draw"].AxisY.Minimum = -1.2;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Maximum = 1.2;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Interval = 0.1;
            robotspeed_chart.ChartAreas["Draw"].AxisY.Title = "속도";
            robotspeed_chart.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            robotspeed_chart.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            robotspeed_chart.Series.Add("Liner");
            robotspeed_chart.Series["Liner"].ChartType = SeriesChartType.Line;
            robotspeed_chart.Series["Liner"].Color = Color.LightGreen;
            robotspeed_chart.Series["Liner"].BorderWidth = 2; ;
            robotspeed_chart.Series["Liner"].LegendText = "선속도(m/s)"; //레전드는 범례
            robotspeed_chart.Series.Add("Angular");
            robotspeed_chart.Series["Angular"].ChartType = SeriesChartType.Line;
            robotspeed_chart.Series["Angular"].Color = Color.Orange;
            robotspeed_chart.Series["Angular"].BorderWidth = 2; ;
            robotspeed_chart.Series["Angular"].LegendText = "각속도(rad/s)"; //레전드는 범례
            //줌기능을 넣어보자!
            robotspeed_chart.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            robotspeed_chart.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }

        private void ChartBMSSetting()
        {
            #region BMS_ampare_chart
            //0.디폴트 ChartAreas와 Series 삭제
            chart_Bms_Ampare.ChartAreas.Clear();
            chart_Bms_Ampare.Series.Clear();
            //1.ChartAreas
            chart_Bms_Ampare.ChartAreas.Add("Draw"); ;
            chart_Bms_Ampare.ChartAreas["Draw"].BackColor = Color.Black;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.Interval = 10;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.Minimum = -5;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.Maximum = 20;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.Interval = 5;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.Title = "(A)";
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Ampare.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_Bms_Ampare.Series.Add("Ampare");
            chart_Bms_Ampare.Series["Ampare"].ChartType = SeriesChartType.Line;
            chart_Bms_Ampare.Series["Ampare"].Color = Color.LightGreen;
            chart_Bms_Ampare.Series["Ampare"].BorderWidth = 2; ;
            chart_Bms_Ampare.Series["Ampare"].LegendText = "전류(A)"; //레전드는 범례

            //줌기능을 넣어보자!
            chart_Bms_Ampare.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_Bms_Ampare.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;

            #endregion

            #region BMS_Volt_chart
            //0.디폴트 ChartAreas와 Series 삭제
            chart_Bms_Volt.ChartAreas.Clear();
            chart_Bms_Volt.Series.Clear();
            //1.ChartAreas
            chart_Bms_Volt.ChartAreas.Add("Draw"); ;
            chart_Bms_Volt.ChartAreas["Draw"].BackColor = Color.Black;
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.Interval = 10;
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.Minimum = -5;
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.Maximum = 60;
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.Interval = 10;
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.Title = "(V)";
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Volt.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_Bms_Volt.Series.Add("Volt");
            chart_Bms_Volt.Series["Volt"].ChartType = SeriesChartType.Line;
            chart_Bms_Volt.Series["Volt"].Color = Color.LightGreen;
            chart_Bms_Volt.Series["Volt"].BorderWidth = 2; ;
            chart_Bms_Volt.Series["Volt"].LegendText = "전압(V)"; //레전드는 범례

            //줌기능을 넣어보자!
            chart_Bms_Volt.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_Bms_Volt.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;

            #endregion

            #region BMS_Temperature_chart
            //0.디폴트 ChartAreas와 Series 삭제
            chart_Bms_Temper.ChartAreas.Clear();
            chart_Bms_Temper.Series.Clear();
            //1.ChartAreas
            chart_Bms_Temper.ChartAreas.Add("Draw"); ;
            chart_Bms_Temper.ChartAreas["Draw"].BackColor = Color.Black;
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.Minimum = 0;
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.Maximum = 120;
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.Interval = 10;
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.Title = "시간";
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.Minimum = -5;
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.Maximum = 200;
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.Interval = 20;
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.Title = "(T)";
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart_Bms_Temper.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            chart_Bms_Temper.Series.Add("Temper");
            chart_Bms_Temper.Series["Temper"].ChartType = SeriesChartType.Line;
            chart_Bms_Temper.Series["Temper"].Color = Color.LightGreen;
            chart_Bms_Temper.Series["Temper"].BorderWidth = 2; ;
            chart_Bms_Temper.Series["Temper"].LegendText = "온도(T)"; //레전드는 범례

            //줌기능을 넣어보자!
            chart_Bms_Temper.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            chart_Bms_Temper.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;

            #endregion
        }
        double[] ecg = new double[1000];
        double[] ppg = new double[1000];

        double[] robot_linear_speed = new double[1000];
        double[] robot_anglua_speed = new double[1000];
        int nlinearspeed_idx = 0;
        int nangularspeed_idx = 0;


        double[] BMS_Amp = new double[1000];
        double[] BMS_Volt = new double[1000];
        double[] BMS_Temper = new double[1000];
        int nbms_amp_idx = 0;
        int nbms_volt_idx = 0;
        int nbms_temper_idx = 0;


        private void myTimer_Tick(object sender, EventArgs e)
        {
            //       ChartSetting();
            //       ViewAll();
            try
            {
                if (Data.Instance.isConnected)
                {
                    if (m_strCurrentRobotDetailInfo_ID == "")
                    {
                    }
                    else
                    {
                        if (Data.Instance.Robot_work_info.ContainsKey(m_strCurrentRobotDetailInfo_ID))
                        {
                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg != null)
                            {
                                double motor_Robotspeed_returnlinear_x = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.linear.x;
                                if (nlinearspeed_idx < 120)
                                {
                                    //선속도
                                    robot_linear_speed[nlinearspeed_idx] = motor_Robotspeed_returnlinear_x;
                                    nlinearspeed_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_linear_speed[i] = robot_linear_speed[i + 1];
                                        robot_linear_speed[119] = motor_Robotspeed_returnlinear_x;
                                    }
                                }

                                //각속도
                                double motor_Robotspeed_return_angular_z = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.motorstate.msg.feed_vel.angular.z;

                                if (nangularspeed_idx < 120)
                                {
                                    //선속도
                                    robot_anglua_speed[nangularspeed_idx] = motor_Robotspeed_return_angular_z;
                                    nangularspeed_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        robot_anglua_speed[i] = robot_anglua_speed[i + 1];
                                        robot_anglua_speed[119] = motor_Robotspeed_return_angular_z;
                                    }
                                }
                            }


                            if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo != null)
                            {
                                if (Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data.Count < 1)
                                    return;
                                float bms_ampare = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[1];
                                float bms_volt = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[2];
                                float bms_temper = Data.Instance.Robot_work_info[m_strCurrentRobotDetailInfo_ID].robot_status_info.bmsinfo.msg.data[3];

                                if (nbms_amp_idx < 120) //bms ampare 그래프로 표시하기위해 .. 배열에 저장
                                {
                                    BMS_Amp[nbms_amp_idx] = bms_ampare;
                                    nbms_amp_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        BMS_Amp[i] = BMS_Amp[i + 1];
                                        BMS_Amp[119] = bms_ampare;
                                    }
                                }

                                if (nbms_volt_idx < 120) // bms volt 그래프로 표시하기위해..배열에 저장
                                {
                                    BMS_Volt[nbms_volt_idx] = bms_volt;
                                    nbms_volt_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        BMS_Volt[i] = BMS_Volt[i + 1];
                                        BMS_Volt[119] = bms_volt;
                                    }
                                }

                                if (nbms_temper_idx < 120)  // bms volt 그래프로 표시하기위해..배열에 저장
                                {
                                    BMS_Temper[nbms_temper_idx] = bms_temper;
                                    nbms_temper_idx++;
                                }
                                else
                                {
                                    for (int i = 0; i < 119; i++)
                                    {
                                        BMS_Temper[i] = BMS_Temper[i + 1];
                                        BMS_Temper[119] = bms_temper;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("myTimer_Tick err :={0}", ex.Message.ToString());
            }

        }
        private void ViewAll()
        {
            Random ra = new Random();

            /* Queue<int> q = new Queue<int>();
             q.Enqueue(120);
             q.Enqueue(130);
             q.Enqueue(150);



             int next = q.Dequeue(); // 120
             next = q.Dequeue(); // 130
             */

            for (int i = 0; i < 120; i++)
            {
                int nrandom = ra.Next(-3, 3);
                ecg[i] = (double)(nrandom);

                nrandom = ra.Next(-3, 3);
                ppg[i] = (double)(nrandom);
            }

            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in robot_linear_speed)
            {
                robotspeed_chart.Series["Liner"].Points.Add(x);
            }
            foreach (double x in robot_anglua_speed)
            {
                robotspeed_chart.Series["Angular"].Points.Add(x);
            }
        }

        private void ViewBMSAll()
        {


            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in BMS_Amp)
            {
                chart_Bms_Ampare.Series["Ampare"].Points.Add(x);
            }

            foreach (double x in BMS_Volt)
            {
                chart_Bms_Volt.Series["Volt"].Points.Add(x);
            }

            foreach (double x in BMS_Temper)
            {
                chart_Bms_Temper.Series["Temper"].Points.Add(x);
            }

        }

        //chart DP


        private async void worklooptimer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Data.Instance.Robot_work_info.Count > 0)
                {
                    for (int i = 0; i < Data.Instance.Robot_work_info.Count; i++)
                    {
                        KeyValuePair<string, Robot_WorkKInfo> robot_workinfo = Data.Instance.Robot_work_info.ElementAt(i);
                        string strrobotid = robot_workinfo.Value.strRobotID;

                        if (Data.Instance.Robot_work_info[strrobotid].strLoop_Flag == "loop")
                        {
                            Robot_Xpos.Clear();
                            Robot_Ypos.Clear();

                            Data.Instance.Robot_work_info[strrobotid].strLoop_Flag = "wait";
                            int nworkcnt = 0;
                            nworkcnt = Data.Instance.Robot_work_info[strrobotid].nCurrWork_cnt;
                            nworkcnt++;

                            Thread.Sleep(Data.Instance.nWorkDelayTime);
                            listBox1.Items.Clear();
                            var task = Task.Run(() => worker.onLoopWork_Publish(Data.Instance.Robot_work_info[strrobotid].strRobotID, nworkcnt));
                            await task;

                            // Thread.Sleep(500);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("worklooptimer1_Tick err :={0}", ex.Message.ToString());
            }
        }


        public void onListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox1.Items.Add(msg);
                // listBox1.SelectedIndex = listBox1.Items.Count - 1;

                if (listBox1.Items.Count > 1000)
                    listBox1.Items.Clear();
            }));

        }
        public void updateDP(string strtopic, string msg, string strcnt)
        {
            onListmsg(string.Format("topic={0}..data={1}", strtopic, msg));
        }



        private void DemoForm0131_FormClosing(object sender, FormClosingEventArgs e)
        {
            Robotstatus_Updatetimer.Enabled = false;
            UI_Updatetimer.Enabled = false;
            myTimer.Enabled = false;
            worklooptimer1.Enabled = false;
            g.Dispose();
        }

        private void txtRobotspeed_setting_y_TextChanged(object sender, EventArgs e)
        {

        }





        private void chart1_Paint(object sender, PaintEventArgs e)
        { }
        protected override void OnPaint(PaintEventArgs e)
        {
            /*      base.OnPaint(e);

                  chart1.ChartAreas.Clear();  // 이미 있는 디폴트 차트를 깨끗하게 지워줍니다 ChartAreas는 배경
                  chart1.Series.Clear();     // Series는 선이라고 이해하면 되겠습니다.

                  //1.ChartArea
                  chart1.ChartAreas.Add("Draw");  // Draw라는 이름의 배경을 추가합니다
                  chart1.ChartAreas["Draw"].BackColor = Color.Black;  // 백그라운드는 검정색!

                  chart1.ChartAreas["Draw"].AxisX.Minimum = -20;  //x축의 최소값은 -20
                  chart1.ChartAreas["Draw"].AxisX.Maximum = 20;  //x축의 최대값은 +20
                  chart1.ChartAreas["Draw"].AxisX.Interval = 2;  //x축의 간격은 2
                  chart1.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray; // 간격선은 회색! 스타일은 대쉬
                  chart1.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                  chart1.ChartAreas["Draw"].AxisY.Minimum = -2;  //y축설정입니다
                  chart1.ChartAreas["Draw"].AxisY.Maximum = 2;
                  chart1.ChartAreas["Draw"].AxisY.Interval = 0.5;
                  chart1.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
                  chart1.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                  //2.Series
                  chart1.Series.Add("SIN");  //SIN이라는 선을 추가합니다
                  chart1.Series["SIN"].ChartType = SeriesChartType.Line;  //타입은 선으로그려야죠?
                  chart1.Series["SIN"].Color = Color.LightGreen;  //밝은초록색으로 그립시다
                  chart1.Series["SIN"].BorderWidth = 2;  // 너비는 2
                  chart1.Series["SIN"].LegendText = "sin(x)/x";  //Legend는 범례를 가르킵니다

                  chart1.Series.Add("COS");  //SIN과 동일해요
                  chart1.Series["COS"].ChartType = SeriesChartType.Line;
                  chart1.Series["COS"].Color = Color.Orange;
                  chart1.Series["COS"].BorderWidth = 2;
                  chart1.Series["COS"].LegendText = "cos(x)/x";

                  for (double x = -20; x < 20; x += 0.1)// X를 반복문을 통해 나타냅시다 0.1씩 키우죠
                  {
                      double y = Math.Sin(x) / x;  //double형을 쓰고 Math.를 이용해서 계산해요
                      chart1.Series["SIN"].Points.AddXY(x, y);  //각각의 점들을 그려줍니다. Points.Add(x,y)

                      y = Math.Cos(x) / x;
                      chart1.Series["COS"].Points.AddXY(x, y);
                  }

                  */

        }



        Queue<double> q = new Queue<double>();
        Queue<double> q2 = new Queue<double>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (q.Count > 50000)
            {
                for (int j = 0; j < 10000; j++)
                    q.Dequeue();
            }

            for (int i = 0; i < 1000; i++)
            {
                Random ra = new Random();
                int nrandom = ra.Next(-3, 3);
                double ed = (double)(nrandom);
                q.Enqueue(ed);
            }

            if (q2.Count > 50000)
            {
                for (int j = 0; j < 10000; j++)
                    q2.Dequeue();
            }

            for (int i = 0; i < 1000; i++)
            {
                Random ra = new Random();
                int nrandom = ra.Next(-3, 3);
                double ed = (double)(nrandom);
                q2.Enqueue(ed);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


        #region 20190115 시연

        private void button4_Click(object sender, EventArgs e) //시연1
        {
            b_initpos = false;
            Dictionary<string, string> workinfo = new Dictionary<string, string>();
            workinfo.Add("R_004", "WID021403");
            workinfo.Add("R_002", "WID021401");
            workinfo.Add("R_001", "WID021402");



            button4.Enabled = false;

            onDemoRun(workinfo, (int)(numericUpDown1.Value));

            //button4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e) //중지
        {
            b_initpos = false;

            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            workinfo.Add("R_002", "WID021401");
            workinfo.Add("R_001", "WID021402");
            workinfo.Add("R_004", "WID021403");

            button5.Enabled = false;
            onDemoStop(workinfo);
            button5.Enabled = true;

            button4.Enabled = true;
            button6.Enabled = true;
            button9.Enabled = true;
            button7.Enabled = true;
        }


        bool b_initpos = false;
        int ninitprocesscnt = 3;
        int ninitcurrprocesscnt = 1;
        
        private async void button7_Click(object sender, EventArgs e) //초기이동
        {
            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            workinfo.Add("R_002", "WID021404");
            workinfo.Add("R_001", "WID021405");
            workinfo.Add("R_004", "WID021406");

            button7.Enabled = false;

            oninitpos_run();


        }

        public void oninitpos_run()
        {
            b_initpos = false;
            bworkfinish = false;

            ninitcurrprocesscnt = 1;

            onDemoInitRun(1);
        }

        public void InitPosComplete()
        {
            if (b_initpos)
            {
                ninitcurrprocesscnt++;
                Thread.Sleep(500);
                onDemoInitRun(1);
            }
            //MessageBox.Show("d");
        }


        private async void onDemoInitRun(int workcnt)
        {


            string strworkfile = "";
            string strworkrobot = "";

            string strworkid = "";



            List<string> strSelectRobot = new List<string>();
            List<string> strworkdata = new List<string>();

            string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
            string[] strSelectworkdata_Worker;

            try
            {
                //  foreach (KeyValuePair<string, string> info in workinfo)
                //   {
                if (ninitcurrprocesscnt == 1)
                {
                    strworkrobot = "R_002";
                    strworkfile = "WID021404";
                }
                else if (ninitcurrprocesscnt == 2)
                {
                    strworkrobot = "R_001";
                    strworkfile = "WID021405";
                }
                else if (ninitcurrprocesscnt == 3)
                {
                    strworkrobot = "R_004";
                    strworkfile = "WID021406";
                }
                else
                {
                    if (b_sen3)
                    {
                        onSen3_run();
                    }
                    else
                    {

                    }
                    Invoke(new MethodInvoker(delegate ()
                    {
                        button7.Enabled = true;
                    }));
                    b_initpos = false;
                    ninitcurrprocesscnt = 0;
                    return;
                }


                strworkid = "<" + strworkfile + ">";

                strworkfile = Application.StartupPath + "\\" + strworkfile + ".xml";

                strworkdata.Clear();

                using (StreamReader sr1 = new System.IO.StreamReader(strworkfile, Encoding.Default))
                {
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        if (strTemp.IndexOf('<') < 0 && strTemp != "")
                        {
                            strworkdata.Add(strTemp);
                        }
                    }
                    sr1.Close();
                }



                Invoke(new MethodInvoker(delegate ()
                {
                    if (strworkdata.Count < 0)
                    {
                        MessageBox.Show("작업 내용이 존재하지 않습니다.");
                        return;
                    }
                }));


                strSelectRobot_Worker = new string[1];
                strSelectworkdata_Worker = new string[strworkdata.Count - 1];

                strSelectRobot_Worker[0] = strworkrobot;


                string strworkname = strworkdata[0];
                for (int i = 1; i < strworkdata.Count; i++)
                {
                    strSelectworkdata_Worker[i - 1] = strworkdata[i];
                }
                int nworkcnt = workcnt;
                //var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));
                //await task;
                Thread.Sleep(100);
                //   }


                Robotstatus_Updatetimer.Enabled = true;

                Thread.Sleep(100);
                bworkfinish = false;
                b_initpos = true;
                //label44.Text = "1";

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDemoinitRun err :={0}", ex.Message.ToString());
            }
        }


        private async void onDemoRun(Dictionary<string, string>workinfo, int workcnt)
        {
            

            string strworkfile = "";
            string strworkrobot = "";

            string strworkid = "";



            List<string> strSelectRobot = new List<string>();
            List<string> strworkdata = new List<string>();

            string[] strSelectRobot_Worker; //워크작업으로 전달하기 위해 형식 맞춤.. 
            string[] strSelectworkdata_Worker;

            try
            {
                foreach (KeyValuePair<string, string> info in workinfo)
                {
                    strworkrobot = info.Key;
                    strworkfile = info.Value;

                    strworkid = "<" + strworkfile + ">";

                    strworkfile = Application.StartupPath + "\\" + strworkfile + ".xml";

                    strworkdata.Clear();

                    using (StreamReader sr1 = new System.IO.StreamReader(strworkfile, Encoding.Default))
                    {
                        while (sr1.Peek() >= 0)
                        {
                            string strTemp = sr1.ReadLine();

                            if (strTemp.IndexOf('<') < 0 && strTemp != "")
                            {
                                strworkdata.Add(strTemp);
                            }
                        }
                        sr1.Close();
                    }



                    Invoke(new MethodInvoker(delegate ()
                    {
                        if (strworkdata.Count < 0)
                        {
                            MessageBox.Show("작업 내용이 존재하지 않습니다.");
                            return;
                        }
                    }));


                    strSelectRobot_Worker = new string[1];
                    strSelectworkdata_Worker = new string[strworkdata.Count - 1];

                    strSelectRobot_Worker[0] = strworkrobot;


                    string strworkname = strworkdata[0];
                    for (int i = 1; i < strworkdata.Count; i++)
                    {
                        strSelectworkdata_Worker[i - 1] = strworkdata[i];
                    }
                    int nworkcnt = workcnt;
                    //var task = Task.Run(() => worker.onWorkOrder_publish(strworkname, strworkid, strSelectRobot_Worker, strSelectworkdata_Worker, nworkcnt));
                    //await task;
                    Thread.Sleep(3000);
                }

                Robotstatus_Updatetimer.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDemoRun err :={0}", ex.Message.ToString());
            }
        }
        
        private async void onDemoStop(Dictionary<string, string>workinfo)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strworkfile = "";
                    string strworkrobot = "";

                    foreach (KeyValuePair<string, string> info in workinfo)
                    {
                        strworkrobot = info.Key;
                        strworkfile = info.Value;

                        if (Data.Instance.Robot_work_info.ContainsKey(strworkrobot))
                        {
                            string strgoal_id = Data.Instance.Robot_work_info[strworkrobot].strGoalid;
                            worker.onWorkCancel_publish(strworkrobot, strgoal_id);
                            Thread.Sleep(Data.Instance.nPulishDelayTime);

                            worker.onWorkMoveStop_publish(strworkrobot, strgoal_id);
                            Thread.Sleep(Data.Instance.nPulishDelayTime);
                            worker.onDeleteSelectRobotSubscribe(strworkrobot);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("button5_Click 시연1중지 err ==" + ex.Message.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e) //시연2
        {
            b_initpos = false;

            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            /*workinfo.Add("R_004", "WID021407");
            workinfo.Add("R_001", "WID021408");

            button9.Enabled = false;
            onDemoRun(workinfo, (int)(numericUpDown3.Value));
            button9.Enabled = true;
            */

         
            workinfo.Add("R_001", "WID021402");

            button9.Enabled = false;
            onDemoStop(workinfo);

            Thread.Sleep(1000);

            workinfo.Clear();

            workinfo.Add("R_001", "WID021402_2");

            onDemoRun(workinfo, (int)(numericUpDown1.Value));

            // button9.Enabled = true;
        }


        bool b_sen3 = false;
        private void button6_Click(object sender, EventArgs e) //시연3
        {
            b_initpos = false;

            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            workinfo.Add("R_002", "WID021401");
            workinfo.Add("R_001", "WID021402");
            workinfo.Add("R_004", "WID021403");

            button6.Enabled = false;
            onDemoStop(workinfo);
            Thread.Sleep(1000);

            oninitpos_run();
            b_sen3 = true;

        }

        private void onSen3_run()
        {
            Dictionary<string, string> workinfo = new Dictionary<string, string>();
            workinfo.Add("R_004", "WID021407");
       
            onDemoRun(workinfo, 1);
            Invoke(new MethodInvoker(delegate ()
            {
                button6.Enabled = true;

                button4.Enabled = true;
                button9.Enabled = true;
            }));
        }



        private void button8_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            workinfo.Add("R_004", "WID021407");
            workinfo.Add("R_001", "WID021408");
            button8.Enabled = false;
            onDemoStop(workinfo);
            button8.Enabled = true;
        }

        private void timer_initpos_Tick(object sender, EventArgs e)
        {
         /*   if(b_initpos && bworkfinish)
            {
                b_initpos = false;
                bworkfinish = false;
                label44.Text = "0";
            }
            */
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> workinfo = new Dictionary<string, string>();

            workinfo.Add("R_004", "WID021407");

            onDemoRun(workinfo, 1);
            // worker.onevttest();
        }
        #endregion




        /*
        int CursorX = 0; // 줌되는 첫번째 인덱스
        private void myTimer_Tick(object sender, EventArgs e)
        {

        }
        private void ViewAll()
        {
            //데이터는 ecg배열 ppg배열에 있어
            foreach (double x in ecg)
            {
                c1.Series["ECG"].Points.Add(x);
            }
            foreach (double x in ppg)
            {
                c1.Series["PPG"].Points.Add(x);
            }
        }
        private void ChartSetting()
        {
            //0.디폴트 ChartAreas와 Series 삭제
            c1.ChartAreas.Clear();
            c1.Series.Clear();
            //1.ChartAreas
            c1.ChartAreas.Add("Draw"); ;
            c1.ChartAreas["Draw"].BackColor = Color.Black;
            c1.ChartAreas["Draw"].AxisX.Minimum = 0;
            c1.ChartAreas["Draw"].AxisX.Maximum = ecgCount;
            c1.ChartAreas["Draw"].AxisX.Interval = 50;
            c1.ChartAreas["Draw"].AxisX.MajorGrid.LineColor = Color.Gray;
            c1.ChartAreas["Draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;  //너무길면 using 으로 보내주자
            c1.ChartAreas["Draw"].AxisY.Minimum = -2;
            c1.ChartAreas["Draw"].AxisY.Maximum = 4;
            c1.ChartAreas["Draw"].AxisY.Interval = 0.5;
            c1.ChartAreas["Draw"].AxisY.MajorGrid.LineColor = Color.Gray;
            c1.ChartAreas["Draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //2.Series
            c1.Series.Add("ECG");
            c1.Series["ECG"].ChartType = SeriesChartType.Line;
            c1.Series["ECG"].Color = Color.LightGreen;
            c1.Series["ECG"].BorderWidth = 2; ;
            c1.Series["ECG"].LegendText = "ECG"; //레전드는 범례
            c1.Series.Add("PPG");
            c1.Series["PPG"].ChartType = SeriesChartType.Line;
            c1.Series["PPG"].Color = Color.Orange;
            c1.Series["PPG"].BorderWidth = 2; ;
            c1.Series["PPG"].LegendText = "ECG"; //레전드는 범례
            //줌기능을 넣어보자!
            c1.ChartAreas["Draw"].CursorX.IsUserSelectionEnabled = true;
            //스크롤 색변경
            c1.ChartAreas["Draw"].AxisX.ScrollBar.ButtonColor = Color.Blue;
        }
        private int ecgCount;
        private int ppgCount;
        private void PpgRead()
        {
            string fileName = "../../Data/ppg.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName); //라인단위로 스트링에 저장한다
            double max = Double.MinValue;
            double min = Double.MaxValue;
            int i = 0;
            foreach (string line in lines)  //라인스 배열에 있는 각각의 스트링 배열에 대해서
            {
                ppg[i] = double.Parse(line);
                if (max < ppg[i]) max = ppg[i];
                else if (min > ppg[i]) min = ppg[i];
                i++;
            }
            ppgCount = i; //i 는 ppg의 데이터 개수
            MessageBox.Show("PPG : " + ppgCount + ", max = " + max + " , min = " + min);
        }
        double[] ecg = new double[50000];
        double[] ppg = new double[50000];
        private void EcgRead()
        {
            string fileName = "../../Data/ecg.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName); //라인단위로 스트링에 저장한다
            double max = Double.MinValue;
            double min = Double.MaxValue;
            int i = 0;
            foreach (string line in lines)  //라인스 배열에 있는 각각의 스트링 배열에 대해서
            {
                ecg[i] = double.Parse(line) + 2.5; // 그래프가 겹치지 않도록 값을 올림
                if (max < ecg[i]) max = ecg[i];
                else if (min > ecg[i]) min = ecg[i];
                i++;
            }
            ecgCount = i; //i 는 ecg의 데이터 개수
            MessageBox.Show("ECG : " + ecgCount + ", max = " + max + " , min = " + min);
        }
        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        { myTimer.Start(); }
        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
            c1.ChartAreas["Draw"].AxisX.ScaleView.Zoom(0, ecgCount);
        }

        bool flag;
        private void c1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                myTimer.Stop();
                flag = true;
            }

        }
        */


        private void button2_Click(object sender, EventArgs e)
        {

            myTimer.Enabled = true;
           // ChartSetting();
           // ViewAll();
            /*Graphics g = panel2.CreateGraphics();

            Pen pen = new Pen(Color.Green); //pen 객체생성및 속성정의(선을 그리기위해 pen이 필요)
            g.DrawLine(pen, 170, 90, 60, 60); //선그리기
            g.Dispose();
            */
           /* try
            {
                if (Data.Instance.isConnected)
                {
                    worker.onDeleteAllSubscribe();
                }
            }
            catch (Exception ex)
            { }
            */
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
}
}
