using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Syscon_Solution.LSprogram
{
    public partial class firstForm : UserControl
    {
        public firstForm()
        {
            InitializeComponent();
        }
        mainForm mainform;
        Label[] robotstate;
        Label[] robotBattery;
        Panel[] robotPanel;
        Label[] robotLocation;
        taskoperationForm taskoperationform;
        public firstForm(mainForm frm)
        {
            InitializeComponent();
            mainform = frm;
            taskoperationform = new taskoperationForm(this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            onTaskResum("R_001");
        }
        public async void onTaskResum(string robotid)
        {
            try
            {
                string strRobot = robotid;
                mainform.commBridge.onTaskResume_publish(strRobot, "");
                // Thread.Sleep(100);

                Console.WriteLine("resume = {0}", strRobot);

                //onConsolemsgDp(string.Format("resume = {0}", strRobot));
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskResum err=" + ex.Message.ToString());
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            onTaskPause("R_001");
        }
        public async void onTaskPause(string robotid)
        {
            try
            {
                string strRobot = robotid;
                mainform.commBridge.onTaskPause_publish(strRobot, "");
                Console.WriteLine("pause1 = {0}", strRobot);
                mainform.commBridge.onTaskPause_publish(strRobot, "");
                Console.WriteLine("pause2 = {0}", strRobot);
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            onTaskResum("R_002");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            onTaskPause("R_002");
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            onTaskResum("R_003");
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            onTaskPause("R_003");
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            onTaskResum("R_004");
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            onTaskPause("R_004");
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            onTaskResum("R_005");
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            onTaskPause("R_005");
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            onTaskResum("R_006");
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            onTaskPause("R_006"); //뮻ㅇㄷㅀ
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            onTaskResum("R_007");
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            onTaskPause("R_007");
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            taskCancel("R_006");
        }
        public async void onTaskCancel(string strrobotid)
        {
            try
            {
                //string strRobot = strrobotid;
                //var task = Task.Run(() => mainform.commBridge.onTaskCancel_publish(strRobot, ""));
                //await task;
                Invoke(new MethodInvoker(delegate ()
                {
                    this.textBox1.AppendText($"태스크 취소 : {strrobotid}\r\n");
                }));

                mainform.commBridge.onTaskCancel_publish(strrobotid, "");
                Console.WriteLine("cancel = {0}" + strrobotid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            taskCancel("R_007");
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            taskCancel("R_005");
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            taskCancel("R_004");
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            taskCancel("R_003");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            taskCancel("R_002");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            taskCancel("R_001");

        }
        public void DP_Session_1()
        {
            try
            {
                if (mainform.Session_1_mission.Count > -1)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Session_1_listbox.Items.Clear();
                        for (int i = 0; i < mainform.Session_1_mission.Count; i++)
                        {
                            Session_1_listbox.Items.Add($"ATC : {mainform.Session_1_mission[i].atcNO} START : {mainform.Session_1_mission[i].startID} REQUIRED : {mainform.Session_1_mission[i].requiredID}");
                        }
                    }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"------------------> DP_SESSION_1 ERROR <----------- {e}");
            }
        }
        public void DP_Session_2()
        {
            try
            {
                if (mainform.Session_2_mission.Count > -1)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Session_2_listbox.Items.Clear();
                        for (int i = 0; i < mainform.Session_2_mission.Count; i++)
                        {
                            Session_2_listbox.Items.Add($"ATC : {mainform.Session_2_mission[i].atcNO} START : {mainform.Session_2_mission[i].startID} REQUIRED : {mainform.Session_2_mission[i].requiredID}");
                        }
                    }));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"------------------> DP_SESSION_2 ERROR <----------- {e}");
            }
        }
        private void taskCancel(string strrobotid)
        {
            onTaskCancel(strrobotid);
            Thread.Sleep(100);
            if (mainform.resetPLC.Keys.Contains(strrobotid))
            {
                mainform.ScenarioList.Remove(mainform.resetPLC[strrobotid].atcNO); //수정한 부분
                mainform.PLC_Socket_Info[mainform.resetPLC[strrobotid].requiredID].SetSTATE(0);
                mainform.PLC_Socket_Info[mainform.resetPLC[strrobotid].startID].SetSTATE(0);
                mainform.setATCState(mainform.resetPLC[strrobotid].atcNO, false);
                mainform.resetPLC.Remove(strrobotid);
            }
            mainform.checkbRun(strrobotid);
            Invoke(new MethodInvoker(delegate ()
            {
                mainform.DP_currentmission(strrobotid, "-");
            }));
        }

        public void button2_Click(object sender, EventArgs e)
        {
            string str = "33";
            if (mainform.resetPLC.Count > 0)
            {
                foreach (KeyValuePair<string, mainForm.missionType> list in mainform.resetPLC)
                {
                    if (list.Value.atcNO.Contains(str))
                    {
                        mainform.PLC_Socket_Info[mainform.resetPLC[str].requiredID].SetSTATE(0);
                        mainform.PLC_Socket_Info[mainform.resetPLC[str].startID].SetSTATE(0);
                        mainform.setATCState(mainform.resetPLC[str].atcNO, false);

                    }
                }
            }
        }
        public void dp_listbox_1()
        {
            //포장라인
        }
        public void dp_listbox_2()
        {
            //부품창고
        }
        private void batteryState(float battery)
        {
            if (battery > 70)
            {

            }
        }

        private void firstForm_Load(object sender, EventArgs e)
        {
            taskoperationform = new taskoperationForm();
            robotstate = new Label[] { robot1State, robot2State, robot3State, robot4State, robot5State, robot6State, robot7State };
            robotBattery = new Label[] { robot1Battery, robot2Battery, robot3Battery, robot4Battery, robot5Battery, robot6Battery, robot7Battery };
            robotPanel = new Panel[] { robot1Panel, robot2Panel, robot3Panel, robot4Panel, robot5Panel, robot6Panel, robot7Panel };
            initial();
            check = new Thread(checkRobotstate);
            check.IsBackground = true;
            check.Start();



        }
        Thread check;

        string[] nodeName_list = new string[] { "2", "1", "4", "4", "7", "7", "10","10","13","3",
            "6","9","12","15","5","8","11","14","5","9","11","13"};

        float resolution = (float)0.019999999552965164;
        float ori_x = (float)-10.2549853515625;
        float ori_y = (float)-19.7270654296875;
        float dOrignX = (float)169.20;
        float dOrignY = (float)821.25;
        float Wheelratio = (float)0.33;


        public int findLocation(string strrobotid)
        {
            try
            {

                int result = 0;
                int workstate = 0;
                float robotx = 0, roboty = 0;
                if (Data.Instance.isConnected)
                {

                    for (int i = 0; i < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; i++)
                    {
                        if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count > 0)
                        {
                            if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].RID.Contains(strrobotid))
                            {
                                robotx = (float)Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].pose.x;
                                roboty = (float)Data.Instance.robot_liveinfo.robotinfo.msg.robolist[i].pose.y;

                                break;
                            }
                        }
                    }

                    float cellX = (float)(robotx / resolution);
                    float cellY = (float)(roboty / resolution);

                    Point pos = new Point();
                    pos.X = (int)(dOrignX + cellX * (float)Wheelratio);
                    pos.Y = (int)(dOrignY - cellY * (float)Wheelratio);

                    Point pt = new Point(pos.X, pos.Y);
                    for (int i = 0; i < mainform.node_area_.Count(); i++)
                    {
                        if (mainform.node_area_[i].Contains(pt))
                        {
                            result = int.Parse(nodeName_list[i]);
                            return result;
                        }
                    }
                }



                return 99;
            }
            catch (Exception e)
            {
                Console.WriteLine("find location error - > {0}", e);
                return -99;
            }
        }

        public void initial()
        {
            for (int i = 0; i < 7; i++)
            {
                robotstate[i].Text = "-";
                robotBattery[i].Text = "0%";
                robotPanel[i].Enabled = false;
            }
        }
        public void checkSessionmission()
        {

            while (true)
            {
                if (mainform.Session_1_mission.Count > 0)
                {
                    for (int i = 0; i < mainform.Session_1_mission.Count; i++)
                    {
                        string required = mainform.Session_1_mission[i].requiredID;
                        string start = mainform.Session_1_mission[i].startID;
                        string atc = mainform.Session_1_mission[i].atcNO;

                        string str = string.Format("{0} 요청. 시작 -> {1} ATC Number : {2}", required, start, atc);

                        Session_1_listbox.Items.Add(str);
                    }
                }
                if (mainform.Session_2_mission.Count > 0)
                {
                    for (int j = 0; j < mainform.Session_2_mission.Count; j++)
                    {
                        string required = mainform.Session_1_mission[j].requiredID;
                        string start = mainform.Session_1_mission[j].startID;
                        string atc = mainform.Session_1_mission[j].atcNO;

                        string str = string.Format("{0} 요청. 시작 -> {1} ATC Number : {2}", required, start, atc);

                        Session_2_listbox.Items.Add(str);
                    }
                }
                Thread.Sleep(100);
            }
        }

        // 로봇 배터리, 상태, 모니터링
        public void temp()
        {
            try
            {
                while(true)
                {
                    int workstate_ = 0;
                    
                }
            }
            catch
            {

            }
        }
        public void checkRobotstate()
        {
            try
            {
                while (true)
                {
                    int j = 0;
                    int workstate_ = 99;
                    List<string> key = new List<string>(Data.Instance.onRobot.Keys);

                    for (int i = 0; i < robotPanel.Count(); i++)
                    {
                        foreach (string robot in key)
                        {
                            if (robotPanel[i].Tag.ToString().Contains(robot))
                            {
                                Invoke(new MethodInvoker(delegate ()
                                {
                                    robotPanel[i].Enabled = true;

                                    if(Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo != null)
                                    {
                                        if(Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data.Count > 0)
                                        {
                                            if (Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3] > 70)
                                            {
                                                robotBattery[i].ForeColor = Color.Blue;
                                                robotBattery[i].Text = string.Format("{0:f1}%", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3]);
                                            }
                                            else if (Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3] > 25)
                                            {
                                                robotBattery[i].ForeColor = Color.GreenYellow;
                                                robotBattery[i].Text = string.Format("{0:f1}%", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3]);
                                            }
                                            else
                                            {
                                                robotBattery[i].ForeColor = Color.OrangeRed;
                                                robotBattery[i].Text = string.Format("{0:f1}%", Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Invoke(new MethodInvoker(delegate ()
                                        {
                                            robotPanel[i].Enabled = false;
                                        }));
                                    }
                                    //robotBatteryscale[i].Value = Data.Instance.Robot_work_info[robot].robot_status_info.bmsinfo.msg.data[3];

                                    for (j = 0; j < Data.Instance.robot_liveinfo.robotinfo.msg.robolist.Count; j++)
                                    {
                                        if (Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].RID.Contains(robot))
                                        {
                                            workstate_ = Data.Instance.robot_liveinfo.robotinfo.msg.robolist[j].workstate;
                                            j = 0;
                                            break;
                                        }
                                        else
                                        {
                                            workstate_ = 99;
                                        }
                                    }
                                    robotstate[i].Text = this.workstate(workstate_);



                                }));
                            }
                            else
                            {
                            }
                        }
                    }
                    //Console.WriteLine("Workstate = {0}",workstate_);
                    //for (int a = 0; a < RobotConnectedCnt.Count(); a++)
                    //{
                    //    RobotConnectedCnt[a] += 1;
                    //}
                    Thread.Sleep(300);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error -> {0}", e);

            }
        }
        private string workstate(int state)
        {

            switch (state)
            {
                case 0:
                    string result = "대기중";
                    return result;
                case 1:
                    result = "미션 수행중";
                    return result;
                case 2:
                    result = "이벤트 발생";
                    return result;
                case 3:
                    result = "중지 상태";
                    return result;
                case 4:
                    result = "미션 취소";
                    return result;
                case 5:
                    result = "ABORT";
                    return result;
                case 6:
                    result = "돌아가는중";
                    return result;
                case 7:
                    result = "충전중";
                    return result;
                default:
                    result = "-";
                    return result;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainform.bRun_006 = true;
        }

        private void robot6Parking_Click(object sender, EventArgs e)
        {
            string temp = "R_006_P";
            mainform.taskoperationform.parking(temp, "R_006");
            //listBox4.Items.Add("R_006 Parking");
            textBox1.AppendText("R_006 Parking start\r\n");
        }
        public void Robot_Parking(string strrobotid)
        {
            string missiontemp = "";
            if (strrobotid == "R_006")
                missiontemp = "R_006_P";
            else if (strrobotid == "R_007")
                missiontemp = "R_007"; //R_007_P 가 아닌지
            mainform.taskoperationform.parking(missiontemp, strrobotid);
            Invoke(new MethodInvoker(delegate ()
            {

                textBox1.AppendText($"Robotid :{strrobotid} Parking start\r\n");
            }));
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void robot7Parking_Click(object sender, EventArgs e)
        {
            string temp = "R_007_P";
            mainform.taskoperationform.parking(temp, "R_007");
            //listBox4.Items.Add("R_006 Parking");
            textBox1.AppendText("R_007 Parking start\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //DP_Session_2();
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            string msg = "%MX11200";
            if (ABN_LINE_1.IsOn)
            {
                for (int i = 0; i < mainform.ABN100_1.Count; i++)
                {
                    if (mainform.ABN100_1[i].GetHost() == "192.168.102.21")
                        continue;
                    else
                        mainform.ABN100_1[i].onXGT_DataSend(msg, true);
                }
            }
        }

        private void ABN_LINE_2_Toggled(object sender, EventArgs e)
        {
            string msg = "%MX11200";
            if (ABN_LINE_2.IsOn)
            {
                for (int i = 0; i < mainform.ABN100_2.Count; i++)
                {
                    if (mainform.ABN100_2[i].GetHost() == "192.168.102.21")
                        continue;
                    else
                        mainform.ABN100_2[i].onXGT_DataSend(msg, true);
                }
            }
        }

        private void ABN_LINE_3_Toggled(object sender, EventArgs e)
        {
            string msg = "%MX11200";
            if (ABN_LINE_3.IsOn)
            {
                for (int i = 0; i < mainform.ABN100_3.Count; i++)
                {
                    if (mainform.ABN100_3[i].GetHost() == "192.168.102.21")
                        continue;
                    else
                        mainform.ABN100_3[i].onXGT_DataSend(msg, true);
                }
            }
        }

        private void ABN_LINE_4_Toggled(object sender, EventArgs e)
        {
            string msg = "%MX11200";
            if (ABN_LINE_4.IsOn)
            {
                for (int i = 0; i < mainform.ABN100_4.Count; i++)
                {
                    if (mainform.ABN100_4[i].GetHost() == "192.168.102.21")
                        continue;
                    else
                        mainform.ABN100_4[i].onXGT_DataSend(msg, true);
                }
            }
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = Session_1_listbox.SelectedIndex;
                if (idx > -1)
                {
                    mainform.Session_1_mission.Add(mainform.Session_1_mission[idx]);
                    mainform.Session_1_mission.RemoveAt(idx);
                    DP_Session_1();
                }
            }
            catch
            {

            }
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = Session_1_listbox.SelectedIndex;
                int idxLast = Session_1_listbox.Items.Count - 1;
                if (idx > -1 && idx < idxLast)
                {
                    mainform.Session_1_mission.Insert(idx + 1, mainform.Session_1_mission[idx]);
                    mainform.Session_1_mission.RemoveAt(idx);
                    DP_Session_1();
                }
            }
            catch
            {


            }
        }

        private void simpleButton35_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = Session_1_listbox.SelectedIndex;
                if (idx > -1)
                {
                    mainform.Session_1_mission.Insert(idx + 1, mainform.Session_1_mission[idx]);
                    //mainform.Session_1_mission.RemoveAt(idx);
                    DP_Session_1();
                }
            }
            catch
            {

            }
        }
    }
}
