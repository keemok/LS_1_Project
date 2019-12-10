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

using MySql.Data.MySqlClient;

namespace SysSolution.Delivery
{
    public partial class DeliveryForm : Form
    {
        public Frm.ingdlg ingdlg = new Frm.ingdlg();

        public Worker worker;
        FleetManager.Map.MapCtrl mapctrl1;


      //  public static string G_strDBServer_connectstring = "data source=127.0.0.1; database=fleet_db; user id=nickyjo; password=r023866677!; charset=utf8";

     //   public static string G_strDynaWorksDBServer_connectstring = "data source=127.0.0.1; database=fleet_db; user id=nickyjo; password=r023866677!; charset=utf8";

        public string m_strDBConnectstring = "";
        string m_strDBConnectionPath = "..//Ros_info//dbconnectionpath.txt";

        public MySqlConnection G_SqlCon = null;
        public MySqlConnection G_DynaSqlCon = null;

        public List<string> G_robotList = new List<string>();

        public DeliveryForm()
        {
            InitializeComponent();
        }

        private void DeliveryForm_Load(object sender, EventArgs e)
        {
            worker = new Worker(this, 1);
#if _delivery
            mapctrl1 = new FleetManager.Map.MapCtrl(this);
#endif
            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);

            panel1.Controls.Add(mapctrl1);

            try
            {
                if (!File.Exists(m_strDBConnectionPath))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strDBConnectionPath, false, Encoding.Default))
                    {

                        sw.WriteLine("data source=192.168.0.201; database=fleet_db; user id=nickyjo; password=r023866677!; charset=utf8");
                        sw.Close();
                    }
                }

                using (StreamReader sr1 = new System.IO.StreamReader(m_strDBConnectionPath, Encoding.Default))
                {


                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();


                        m_strDBConnectstring = strTemp;
                    }
                    sr1.Close();

                }

                if (m_strDBConnectstring == "")
                {
                    MessageBox.Show("DB connection file open 에러");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("FleetManager_MainForm_Load dbconnection file open  err :={0}", ex.Message.ToString());
            }


            cboRobotID.SelectedIndex = 3;

            if (onInitSql() == 0)
            {
                Data.Instance.isDBConnected = true;

                onDBRead_Robotlist("all");
                onRobots_WorkInfo_InitSet();
            }
            else
            {
                Data.Instance.isDBConnected = false;
                MessageBox.Show("데이타베이스 연결 에러, 점검후 사용하세요.");
                return;
            }
        }

        private int onInitSql()
        {
            string strcnn = m_strDBConnectstring;

            try
            {
                if (G_SqlCon != null)
                {
                    if (G_SqlCon.State == ConnectionState.Open)
                    {
                        G_SqlCon.Close();
                    }
                }

                G_SqlCon = new MySqlConnection(strcnn);

                G_SqlCon.Open();

                return 0;
            }
            catch (MySqlException _e)
            {
                MessageBox.Show(_e.Message.ToString());
                return 1;
            }
        }

        /// <summary>
        /// 등록된 로봇 리스트 읽어오기
        /// </summary>
        public void onDBRead_Robotlist(string strgroup)
        {
            string sql = "";
            bool bdataok = false;

            try
            {
                G_robotList.Clear();

                if (strgroup == "all")
                    sql = string.Format("SELECT * FROM robot_list_t ");
                else
                    sql = string.Format("SELECT * FROM robot_list_t where robot_group='{0}'", strgroup);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

            
                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = ds.Tables[0].Rows[i]["robot_id"].ToString();
                        string strrobotname = ds.Tables[0].Rows[i]["robot_name"].ToString();
                        string strrobotip = ds.Tables[0].Rows[i]["robot_ip"].ToString();
                        string strrobotgroup = ds.Tables[0].Rows[i]["robot_group"].ToString();
                        string strmapid = ds.Tables[0].Rows[i]["map_id"].ToString();
                        string strmapname = "";
                        sql = string.Format("SELECT * FROM map_t where map_id='{0}'", strmapid);

                        DataSet ds2 = new DataSet();
                        MySqlDataAdapter da2 = new MySqlDataAdapter(sql, G_SqlCon);
                        da2.Fill(ds2);

                        int ncnt2 = ds2.Tables[0].Rows.Count;
                        if (ncnt2 > 0)
                        {
                            strmapname = ds2.Tables[0].Rows[0]["map_name"].ToString();
                        }


                        G_robotList.Add(strrobotid);

             
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 등록된 미션 액션 읽어오기
        /// </summary>
        public string onDBRead_Mission_action(string strmissionid)
        {
            string sql = "";
            bool bdataok = false;
            string stractdata = "";
            try
            {
                sql = string.Format("SELECT work FROM missionlist_t where mission_id='{0}'", strmissionid);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    string strworkdata = ds.Tables[0].Rows[0]["work"].ToString();
                    stractdata = strworkdata;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_Mission_action err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_Mission_action err" + ex2.Message.ToString());
            }

            return stractdata;
        }

        public void onRobots_WorkInfo_InitSet()
        {
            try
            {
                Data.Instance.Robot_work_info.Clear();
                //foreach (KeyValuePair<string, string> info in Data.Instance.Robot_status_info)
                int cnt = 0;
                cnt = G_robotList.Count();
                for (int i = 0; i < cnt; i++)
                {
                    string strrobotid = G_robotList.ElementAt(i).ToString();
                    Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_WorkInfo_InitSet err :={0}", ex.Message.ToString());
            }
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

                            onSuscribe_RobotsStatus();

                            ingdlg.Hide();

                        }
                    }
                    else
                    {
                        ROSDisconnect();

                        btnConnect.Enabled = true;
                        ingdlg.Hide();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

#endregion


        private void onSuscribe_RobotsStatus()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    //Dictionary<string, string> robotinfo = Data.Instance.Robot_status_info;
                    //foreach (KeyValuePair<string, string> info in robotinfo)
                    int cnt = 0;
                    cnt = G_robotList.Count();
                    for (int i = 0; i < cnt; i++)
                    {
                        string strrobotid = G_robotList.ElementAt(i).ToString();
                        worker.onSelectRobotStatus_subscribe(strrobotid);
                    }
                    worker.onSelectXIS_subscribe();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus err :={0}", ex.Message.ToString());
            }

        }

          

    private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

        private WAS_GOAL onMissionDataRead(string strmissionid , int startidx)
        {
            string strid = strmissionid;
            WAS_GOAL work_data = new WAS_GOAL();
            try
            {
                //db에서 미션 액션정보 읽기 
                string strwork = onDBRead_Mission_action(strmissionid);//mainform.missionlisttable.missioninfo[0].work;

                DB_MissionData db_missiondata = new DB_MissionData();
                db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);

                work_data.goal.work_id = strmissionid;
                work_data.goal.action_start_idx = startidx;
                work_data.goal.loop_flag = 1;

                for (int i = 0; i < db_missiondata.work.Count; i++)
                {
                    work_data.goal.work.Add(db_missiondata.work[i]);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMissionDataRead err :={0}", ex.Message.ToString());
            }

            return work_data;
        }

        private void onDeliveryMove(string strmissionid, int actidx)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strRobot_id = cboRobotID.SelectedItem.ToString();
                    WAS_GOAL work_data = onMissionDataRead(strmissionid, actidx);
                    string strobj = JsonConvert.SerializeObject(work_data);
                    JObject obj = JObject.Parse(strobj);

                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    Thread.Sleep(500);

                    ros.PublisherTopicMsgtype(strRobot_id + list.topic_goal, list.msg_goal);
                    Thread.Sleep(200);
                    ros.publisher(obj);
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("onDeliveryMove err :={0}", ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("서버에 연결하세요.");
            }
        }

        /// <summary>
        ///  로봇 현재위치에서 미션에 액션들과 제일 가까운 액션을 찾는함수
        /// </summary>
        private int onRobotActidxCheck_DistanceMin_ToMission(float robot_x, float robot_y, string strmissionid)
        {
            int nactidx = 0;
            try
            {
                //db에서 미션 액션정보 읽기 
                string strwork = onDBRead_Mission_action(strmissionid);
                DB_MissionData db_missiondata = new DB_MissionData();
                db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);

                int nworkcnt = db_missiondata.work.Count;
                double distmin = 1000;
                int distmin_actidx = 0;
                if (nworkcnt > 0)
                {
                    for (int i = 0; i < nworkcnt; i++)
                    {
                        Action act = db_missiondata.work[i];
                        if (act.action_type == 1)
                        {
                            float x = act.action_args[0];
                            float y = act.action_args[1];

                            double disttmp = onPointToPointDist(robot_x, robot_y, x, y);

                            if (distmin > disttmp)
                            {
                                distmin = disttmp;
                                distmin_actidx = i;
                            }
                        }
                    }
                }
                nactidx = distmin_actidx;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotActidxCheck_DistanceMin_ToMission err :={0}", ex.Message.ToString());
            }

            return nactidx;
        }


        private double onDistanceMin_ToMission(float robot_x, float robot_y, string strmissionid)
        {
            int nactidx = 0;
            double distmin = 1000;
            try
            {
                //db에서 미션 액션정보 읽기 
                string strwork = onDBRead_Mission_action(strmissionid);
                DB_MissionData db_missiondata = new DB_MissionData();
                db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);

                int nworkcnt = db_missiondata.work.Count;
                
                if (nworkcnt > 0)
                {
                    for (int i = 0; i < nworkcnt; i++)
                    {
                        Action act = db_missiondata.work[i];
                        if (act.action_type == 1)
                        {
                            float x = act.action_args[0];
                            float y = act.action_args[1];

                            double disttmp = onPointToPointDist(robot_x, robot_y, x, y);

                            if (distmin > disttmp)
                            {
                                distmin = disttmp;
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotActidxCheck_DistanceMin_ToMission err :={0}", ex.Message.ToString());
            }

            return distmin;
        }

        private void btnLoadingPosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115148";
            if (Data.Instance.isConnected)
            {
                string strRobot_id = cboRobotID.SelectedItem.ToString();
                if (Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg == null)
                {
                    MessageBox.Show("현재 로봇 좌표를 확인할수 없습니다.");
                    return;
                }

                double r_x = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.x;
                double r_y = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.y;

                //int nstartact_idx = onRobotActidxCheck_DistanceMin_ToMission((float)r_x, (float)r_y, strmissionid);

                onDeliveryMove(strmissionid, 0);

                onbtnDelay(btnLoadingPosMove, 1000);
            }
        }

        private void btn1PosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115213";

            string strRobot_id = cboRobotID.SelectedItem.ToString();
            if (Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg == null)
            {
                MessageBox.Show("현재 로봇 좌표를 확인할수 없습니다.");
                return;
            }

            double r_x = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.x;
            double r_y = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.y;

            if (onDistanceMin_ToMission((float)r_x, (float)r_y, "MID20190516115148") < 1)
            {
                onDeliveryMove(strmissionid, 0);
            }
            else
            {
                int nstartact_idx = onRobotActidxCheck_DistanceMin_ToMission((float)r_x, (float)r_y, strmissionid);
                onDeliveryMove(strmissionid, nstartact_idx);
            }

            onbtnDelay(btn1PosMove, 1000);
        }

        private void btn2PosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115333";
            string strRobot_id = cboRobotID.SelectedItem.ToString();
            if (Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg == null)
            {
                MessageBox.Show("현재 로봇 좌표를 확인할수 없습니다.");
                return;
            }

            double r_x = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.x;
            double r_y = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.y;
            if (onDistanceMin_ToMission((float)r_x, (float)r_y, "MID20190516115148") < 1)
            {
                onDeliveryMove(strmissionid, 0);
            }
            else
            {
                int nstartact_idx = onRobotActidxCheck_DistanceMin_ToMission((float)r_x, (float)r_y, strmissionid);
                onDeliveryMove(strmissionid, nstartact_idx);
            }

            onbtnDelay(btn2PosMove, 1000);
        }
        private void btn3PosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115347";
            string strRobot_id = cboRobotID.SelectedItem.ToString();
            if (Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg == null)
            {
                MessageBox.Show("현재 로봇 좌표를 확인할수 없습니다.");
                return;
            }

            double r_x = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.x;
            double r_y = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.y;
            if (onDistanceMin_ToMission((float)r_x, (float)r_y, "MID20190516115148") < 1)
            {
                onDeliveryMove(strmissionid, 0);
            }
            else
            {
                int nstartact_idx = onRobotActidxCheck_DistanceMin_ToMission((float)r_x, (float)r_y, strmissionid);
                onDeliveryMove(strmissionid, nstartact_idx);
            }

            onbtnDelay(btn3PosMove, 1000);
        }

        private void btn4PosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115358";

            string strRobot_id = cboRobotID.SelectedItem.ToString();
            if (Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg == null)
            {
                MessageBox.Show("현재 로봇 좌표를 확인할수 없습니다.");
                return;
            }

            double r_x = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.x;
            double r_y = Data.Instance.Robot_work_info[strRobot_id].robot_status_info.robotstate.msg.pose.y;
            if (onDistanceMin_ToMission((float)r_x, (float)r_y, "MID20190516115148") < 1)
            {
                onDeliveryMove(strmissionid, 0);
            }
            else
            {
                int nstartact_idx = onRobotActidxCheck_DistanceMin_ToMission((float)r_x, (float)r_y, strmissionid);
                onDeliveryMove(strmissionid, nstartact_idx);
            }
            onbtnDelay(btn4PosMove, 1000);
        }
        private void btnWaitPosMove_Click(object sender, EventArgs e)
        {
            string strmissionid = "MID20190516115409";
            onDeliveryMove(strmissionid, 0);

            onbtnDelay(btnWaitPosMove, 1000);
        }

        private async void btnJobStop_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                    }));

                    string strworkrobot = cboRobotID.SelectedItem.ToString();
                    string strgoal_id = "";
                    var task = Task.Run(() => worker.onWorkCancel_publish(strworkrobot, strgoal_id));
                    await task;
                    onbtnDelay(btnJobStop, 1000);
                   // MessageBox.Show("작업이 취소되었습니다.");

                    btnPauseRestart.Text = "일시정지";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnJobStop_Click err ==" + ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("서버에 연결하세요.");
            }
        }

        private async void btnPauseRestart_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    string strworkrobot = cboRobotID.SelectedItem.ToString();
                    string strgoal_id = "";
                    if (btnPauseRestart.Text == "일시정지")
                    {
                        worker.onWorkPause_publish(strworkrobot, strgoal_id);
                        btnPauseRestart.Text = "재시작";
                    }
                    else
                    {
                        worker.onWorkResume_publish(strworkrobot, strgoal_id);
                        btnPauseRestart.Text = "일시정지";
                    }

                    onbtnDelay(btnPauseRestart, 1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("btnPauseRestart_Click err ==" + ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("서버에 연결하세요.");
            }
        }


        private void onbtnDelay(Button btn, int ndelaytime)
        {
            Invoke(new MethodInvoker(delegate ()
            {
               
                Color bk_clr = btn.ForeColor;
                Color bk_clr2 = btn.BackColor;
                btn.ForeColor = Color.Gray;
                btn.BackColor = Color.Gray;
                btn.Enabled = false;
                Thread.Sleep(ndelaytime);
                btn.Enabled = true;
                btn.ForeColor = bk_clr;
                btn.BackColor = bk_clr2;
                btn.Invalidate();
            }));

        }


        public double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }
    }
}
