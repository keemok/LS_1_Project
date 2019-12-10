using Rosbridge.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class connectionForm : Form
    {
        public connectionForm()
        {
            commBridge = new FleetManager.Comm.Comm_bridge(this);
            InitializeComponent();
            //commBridge.taskfeedback_Evt += new FleetManager.Comm.Comm_bridge.TaskFeedbackResponse(this.TaskFeedbackResponse);
            commBridge.taskresult_Evt += new FleetManager.Comm.Comm_bridge.TaskResultResponse(this.TaskResultResponse);
            commBridge.taskfeedback_Evt += new FleetManager.Comm.Comm_bridge.TaskFeedbackResponse(this.TaskFeedbackResponse);
        }

        private void TaskFeedbackResponse(string strrobotid)
        {
            if (Data.Instance.Robot_work_info[strrobotid].mission_complete)
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.task_complete) return;

                Data.Instance.Robot_work_info[strrobotid].mission_complete = false;

                //this.TaskFeedback_Complete(strrobotid);
            }
            else
            {
                TaskFeedback_Ing(strrobotid);
            }
        }

        private void TaskFeedback_Ing(string strrobotid)
        {
            
            //Console.WriteLine("{0} 태스크 진행중 ....index -> {1}",strrobotid, Data.Instance.Robot_work_info[strrobotid].robot_status_info.taskfeedback.msg.feedback.mission_indx);
            mainform.TaskFeedback_Ing(strrobotid);
        }

        //string strtime = string.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        public void TaskResultResponse(string strrobotid)
        {
            try
            {
                mainform.TaskFeedbackResponse(strrobotid);
            }
            catch { }
        }

        Thread ServerConnect_Checkthread;
        LSprogram.mainForm mainform = new mainForm();

        private void button1_Click(object sender, EventArgs e)
        {
            dbBridge.onDBConnectionPath_OpenCheck();
            if (dbBridge.onRIDiS_InitSql() == 0)
            { }
            else
            {
                //연결 실패
                Data.Instance.isDBConnected = false;
                MessageBox.Show("RIDiS 데이타베이스 연결 에러, 점검후 사용하세요.");
                return;
            }
            onRobotListCheck();
            dbBridge.onDBRead_NodeList();
            dbBridge.onDBRead_warehouse();
            dbBridge.onDBRead_nodearea();
            warehouse_list();
            callByplc_list();
            
            ROSConnection("ws://192.168.20.28:9090");
        }
        private void onread_iplist()
        {

        }
        private void callByplc_list()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //textBox2.AppendText("Warehouse in & out list Loading...\r\n");
            }));
            warehouse_inout temp = new warehouse_inout();
            temp.in_ = "14";
            temp.out_ = "15";
            Data.Instance.callByplc.Add("22", temp);
            Data.Instance.callByplc.Add("8", temp);
            Data.Instance.callByplc.Add("30_C", temp);
            Data.Instance.callByplc.Add("21", temp);
            Data.Instance.callByplc.Add("7", temp);
            Data.Instance.callByplc.Add("23", temp);
            Data.Instance.callByplc.Add("24", temp);
            temp.in_ = "12";
            temp.out_ = "11";
            Data.Instance.callByplc.Add("18", temp);
            Data.Instance.callByplc.Add("4_B", temp);
            Data.Instance.callByplc.Add("30_B", temp);
            Data.Instance.callByplc.Add("17", temp);
            Data.Instance.callByplc.Add("19", temp);
            Data.Instance.callByplc.Add("5_B", temp);
            Data.Instance.callByplc.Add("20", temp);
            Data.Instance.callByplc.Add("32_5", temp);
            Data.Instance.callByplc.Add("32_6", temp);
            Data.Instance.callByplc.Add("33_C", temp);
            temp.in_ = "8";
            temp.out_ = "9";
            Data.Instance.callByplc.Add("6_B", temp);
            Data.Instance.callByplc.Add("3_B", temp);
            Data.Instance.callByplc.Add("35_B", temp);
            Data.Instance.callByplc.Add("34_3", temp);
            Data.Instance.callByplc.Add("34_4", temp);
            Data.Instance.callByplc.Add("16", temp);
            Data.Instance.callByplc.Add("15", temp);
            Data.Instance.callByplc.Add("1_A_U", temp);
            Data.Instance.callByplc.Add("14", temp);
            Data.Instance.callByplc.Add("13", temp);
            Data.Instance.callByplc.Add("11", temp);
            Data.Instance.callByplc.Add("30_A", temp);
            Data.Instance.callByplc.Add("4_A", temp);
            Data.Instance.callByplc.Add("12", temp);
            temp.in_ = "6";
            temp.out_ = "5";
            Data.Instance.callByplc.Add("34_1", temp);
            Data.Instance.callByplc.Add("34_2", temp);
            Data.Instance.callByplc.Add("35_A", temp);
            Data.Instance.callByplc.Add("3", temp);
            Data.Instance.callByplc.Add("1_A_D", temp);
            Data.Instance.callByplc.Add("2", temp);
            Data.Instance.callByplc.Add("26", temp);
            Data.Instance.callByplc.Add("4_D", temp);
            Data.Instance.callByplc.Add("30_D", temp);
            Data.Instance.callByplc.Add("25", temp);
            Data.Instance.callByplc.Add("27", temp);
            Data.Instance.callByplc.Add("5_D", temp);
            Data.Instance.callByplc.Add("28", temp);
            Data.Instance.callByplc.Add("29", temp);
            temp.in_ = "3";
            temp.out_ = "2";
            Data.Instance.callByplc.Add("32_1", temp);
            Data.Instance.callByplc.Add("32_2", temp);
            Data.Instance.callByplc.Add("33_D", temp);
            Data.Instance.callByplc.Add("3_D", temp);
            Data.Instance.callByplc.Add("6_D", temp);
            temp.in_ = "11";
            temp.out_ = "14";
            Data.Instance.callByplc.Add("34_5", temp);
            Data.Instance.callByplc.Add("32_3", temp);
            Data.Instance.callByplc.Add("35", temp);
            Data.Instance.callByplc.Add("33_RA", temp);
            temp.in_ = "8";
            temp.out_ = "11";
            Data.Instance.callByplc.Add("34_6", temp);
            Data.Instance.callByplc.Add("32_4", temp);
            Data.Instance.callByplc.Add("35_RB", temp);
            Data.Instance.callByplc.Add("33_RB", temp);
        }
        private void warehouse_list()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                textBox2.AppendText("Warehouse in & out list Loading...\r\n");
            }));
            warehouse_inout temp = new warehouse_inout();
            temp.in_ = "14";
            temp.out_ = "15";
            Data.Instance.warehouse.Add("30_C", temp);
            Data.Instance.warehouse.Add("24", temp);
            Data.Instance.warehouse.Add("23", temp);
            Data.Instance.warehouse.Add("7", temp);
            Data.Instance.warehouse.Add("21", temp);
            Data.Instance.warehouse.Add("8", temp);
            Data.Instance.warehouse.Add("22", temp);
            temp = new warehouse_inout();
            temp.in_ = "12";
            temp.out_ = "11";
            Data.Instance.warehouse.Add("32_5", temp);
            Data.Instance.warehouse.Add("32_6", temp);
            Data.Instance.warehouse.Add("33_C", temp);
            Data.Instance.warehouse.Add("10", temp);
            Data.Instance.warehouse.Add("9", temp);
            Data.Instance.warehouse.Add("18", temp);
            Data.Instance.warehouse.Add("4_B", temp);
            Data.Instance.warehouse.Add("30_B", temp);
            Data.Instance.warehouse.Add("17", temp);
            Data.Instance.warehouse.Add("19", temp);
            Data.Instance.warehouse.Add("5_B", temp);
            Data.Instance.warehouse.Add("20", temp);
            temp = new warehouse_inout();
            temp.in_ = "8";
            temp.out_ = "9";
            Data.Instance.warehouse.Add("3_B", temp);
            Data.Instance.warehouse.Add("6_B", temp);
            Data.Instance.warehouse.Add("35_B", temp);
            Data.Instance.warehouse.Add("34_3", temp);
            Data.Instance.warehouse.Add("34_4", temp);
            Data.Instance.warehouse.Add("16", temp);
            Data.Instance.warehouse.Add("15", temp);
            Data.Instance.warehouse.Add("1_A1", temp);
            Data.Instance.warehouse.Add("14", temp);
            Data.Instance.warehouse.Add("13", temp);
            Data.Instance.warehouse.Add("11", temp);
            Data.Instance.warehouse.Add("30_A", temp);
            Data.Instance.warehouse.Add("4_A", temp);
            Data.Instance.warehouse.Add("12", temp);
            temp = new warehouse_inout();
            temp.in_ = "6";
            temp.out_ = "5";
            Data.Instance.warehouse.Add("34_1", temp);
            Data.Instance.warehouse.Add("34_2", temp);
            Data.Instance.warehouse.Add("35_A", temp);
            Data.Instance.warehouse.Add("3", temp);
            Data.Instance.warehouse.Add("1_A2", temp);
            Data.Instance.warehouse.Add("2", temp);
            Data.Instance.warehouse.Add("26", temp);
            Data.Instance.warehouse.Add("4", temp);
            Data.Instance.warehouse.Add("30_D", temp);
            Data.Instance.warehouse.Add("25", temp);
            Data.Instance.warehouse.Add("27", temp);
            Data.Instance.warehouse.Add("5_D", temp);
            Data.Instance.warehouse.Add("28", temp);
            Data.Instance.warehouse.Add("29", temp);
            temp = new warehouse_inout();
            temp.in_ = "3";
            temp.out_ = "2";
            Data.Instance.warehouse.Add("32_1", temp);
            Data.Instance.warehouse.Add("32_2", temp);
            Data.Instance.warehouse.Add("33_D", temp);
            Data.Instance.warehouse.Add("3_D", temp);
            Data.Instance.warehouse.Add("6_D", temp);
            Invoke(new MethodInvoker(delegate ()
            {
                textBox2.AppendText("Warehouse in & out list OK...\r\n");
            }));

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
                    //textBox2.AppendText("접속 에러.\r\n");
                    Console.WriteLine("ROSConnection err =" + ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    //ingdlg.onLblMsg("서버에 연결중입니다.");
                    //ingdlg.Show();
                    
                    Invoke(new MethodInvoker(delegate ()
                    {
                        textBox2.AppendText("DB에 연결중입니다...\r\n");
                    }));
                    dbBridge.onDBInsert_Alarm("DB연결 완료", "DB 연결 완료", 1);
                    //RIDiS DB 연결
                    if (dbBridge.onRIDiS_InitSql() == 0)
                    { }
                    else
                    {
                        //연결 실패
                        Data.Instance.isDBConnected = false;
                        MessageBox.Show("RIDiS 데이타베이스 연결 에러, 점검후 사용하세요.");
                        return;
                    }
                    string uri = strAddr;
                    Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                    Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                    await Data.Instance.md.StartAsync();

                    if (Data.Instance.socket.Connected)
                    {
                    }
                    Data.Instance.isConnected = true;

                    if (ServerConnect_Checkthread != null)
                    {
                        ServerConnect_Checkthread.Abort();
                        ServerConnect_Checkthread = null;
                    }
                    ServerConnect_Checkthread = new Thread(onConnectCheck);
                    ServerConnect_Checkthread.IsBackground = true;
                    ServerConnect_Checkthread.Start();
                    textBox2.AppendText("로봇에 대한 Subscribe 진행중\r\n");
                    if (Data.Instance.isConnected == true)
                    {
                        onSuscribe_RobotsStatus_Basic();
                    }
                    Console.WriteLine("connection successs");
                    textBox2.AppendText("모든 로봇 Check 완료. RiDIS 연결 완료.");

                    Thread.Sleep(500);
                    
                    mainform.Show();

                }
                catch (Exception ex)
                {
                    ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
                        //ingdlg.Hide();
                        MessageBox.Show("연결에 실패하였습니다."+ex);
                    }
                    return;
                }
            }
        }

        FleetManager.Comm.Comm_bridge commBridge;
        FleetManager.DB.DB_bridge dbBridge = new FleetManager.DB.DB_bridge();

        private void onRobotListCheck()
        {
            dbBridge.onDBRead_Robotlist("all");
            //   onDBRead_RobotStatus();
            onRobots_WorkInfo_InitSet();
        }
        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
                Data.Instance.Robot_work_info.Clear();
                //foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                int cnt = 0;
                cnt = Data.Instance.Robot_RegInfo_list.Count;
                if (cnt > 0)
                {
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    {
                        string strrobotid = info.Key;
                        Robot_RegInfo value = info.Value;
                        Invoke(new MethodInvoker(delegate ()
                        {
                            textBox2.AppendText(strrobotid + " Work info check finish...\r\n");
                        }));
                        Data.Instance.Robot_work_info.Add(strrobotid, commBridge.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_WorkInfo_InitSet err :={0}", ex.Message.ToString());
            }
        }
        private void onSuscribe_RobotsStatus_Basic()
        {
            try
            {


                if (Data.Instance.isConnected)
                {
                    //Dictionary<string, string> robotinfo = Data.Instance.Robot_status_info;
                    //foreach (KeyValuePair<string, string> info in robotinfo)
                    int cnt = 0;
                    cnt = Data.Instance.Robot_RegInfo_list.Count;
                    if (cnt > 0)
                    {
                        foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                        {
                            string strrobotid = info.Key;
                            Robot_RegInfo value = info.Value;

                            commBridge.onSelectRobotStatus_Basic_subscribes(strrobotid);
                            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                            commBridge.onControllerstate_subscribe(strrobotid);
                            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                            commBridge.onTaskResult_subscribe(strrobotid);
                            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                            Invoke(new MethodInvoker(delegate ()
                            {
                                textBox2.AppendText(strrobotid + " Subscribe...\r\n");
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus_Basic err :={0}", ex.Message.ToString());
            }
        }

        List<robotStatus_> robotStatus = new List<robotStatus_>();
        public class robotStatus_
        {
            public string robotid;
            public int workState;
        }


        


        System.Net.NetworkInformation.PingOptions options = new PingOptions();
        bool bformclose = false;
        //string[] ipAddress = { "192.168.0.49", "192.168.0.50", "192.168.0.51", "192.168.0.52", "192.168.0.53", "192.168.0.55" };
        Ping pingSender = new Ping();

        public bool pingTest(string robotip)
        {
            options.DontFragment = true;
            string data = "this is test message";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            //int cnt = Data.Instance.Robot_RegInfo_list.Count-1;
            //string[] ipAddr = new string[cnt];
            //for(int j=0;j<cnt;j++)
            //{
            //    ipAddr[j] = Data.Instance.Robot_RegInfo_list_ip[j+1].robot_ip;
            //}

            //while (true)
            //{
                //if (bformclose) break;

                //for (int i = 0; i < cnt; i++)
                //{
                    try
                    {

                        PingReply reply = pingSender.Send(robotip, timeout, buffer, options);

                        if (reply.Status == IPStatus.Success)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("에러 {0} : ", ex);
                return false;
                    }

                    //Thread.Sleep(500);
                //}
            //}
        }

        private void onConnectCheck()
        {
            try
            {
                for (; ; )
                {
                    if (Data.Instance.bFormClose) break;

                    if (!Data.Instance.isConnected)
                    {
                        //textBox2.Clear();
                        //textBox2.AppendText("Disconnect");
                        break;
                    }

                    if (!Data.Instance.socket.Connected)
                    {
                        
                        ROSDisconnect();
                        break;
                    }
                    Thread.Sleep(10);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectCheck err :={0}", ex.Message.ToString());
            }
        }
        public void onCompulsion_Close() //강제종료시 처리할 부분들
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    //worker.onDeleteAllSubscribe_Compulsion();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onCompulsion_Close err" + ex.Message.ToString());
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
                
                Invoke(new MethodInvoker(delegate ()
                {
                    this.Visible = true;
                    //connButton.Text = "connect";
                    //connButton.Enabled = true;
                }));
                Data.Instance.socket = null;
                Data.Instance.warehouse.Clear();
                Data.Instance.callByplc.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
                
            }
        }

        LS_Socket test = new LS_Socket();
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void connectionForm_Load(object sender, EventArgs e)
        {

        }

        private void connectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void connectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
