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


namespace SysSolution.FleetManager
{
    public partial class FleetManager_MainForm : Form
    {
       // public static string G_strDBServer_connectstring = "data source=127.0.0.1; database=fleet_db; user id=nickyjo; password=r023866677!; charset=utf8";

      //  public static string G_strDynaWorksDBServer_connectstring = "data source=127.0.0.1; database=fleet_db; user id=nickyjo; password=r023866677!; charset=utf8";

        public Worker worker;
        MissionEdit_ctrl missioneditctrl;
        SettingMainCtrl settingmain_ctrl;
        Monitoring.MonitoringMain_Ctrl monitoringmainctrl;
#if !_fleetdemo
        order.JobOrder_ctrl joborderctrl;
#elif _fleetdemo
        Demo.JobOrder_Demo_ctrl jobdemoorderctrl;
#endif

        public MySqlConnection G_SqlCon = null;
        public MySqlConnection G_DynaSqlCon = null;

        public List<string> G_robotList = new List<string>();

        /// <summary>
        /// db에 등록된 로봇 정보 리스트
        /// </summary>
        public Dictionary<string, Robot_RegInfo> Robot_RegInfo_list = new Dictionary<string, Robot_RegInfo>();

        /// <summary>
        /// db에 등록된 로봇상태 정보 리스트
        /// </summary>
        public Dictionary<string, Robot_Status> Robot_Status_list = new Dictionary<string, Robot_Status>();

        /// <summary>
        /// 작업스케줄table를 리스트로 관리한다. key = job id
        /// </summary>
        public Dictionary<string, JobScheduleTable> Job_ScheduleTable_list = new Dictionary<string, JobScheduleTable>();

        /// <summary>
        /// db에 등록된 맵 리스트
        /// </summary>
        public Dictionary<string, Map_list> Map_list = new Dictionary<string, Map_list>();
        
        /// <summary>
        /// db에 등록된 작업 정보 리스트
        /// </summary>
        public Dictionary<string, JobSchedule> JobSchedule_list = new Dictionary<string, JobSchedule>();

        public MissionList_Table missionlisttable = new MissionList_Table();
        public Dictionary<string, Action_Data_List> actiondatalitTable = new Dictionary<string, Action_Data_List>();

        public Frm.ingdlg ingdlg = new Frm.ingdlg();

        public string m_strDBConnectstring = "";
        string m_strDBConnectionPath = "..//Ros_info//dbconnectionpath.txt";

        public FleetManager_MainForm()
        {
            InitializeComponent();
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

#region DB 관련
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


        private int onDynaWorkInitSql()
        {
            string strcnn = m_strDBConnectstring;

            try
            {
                if (G_DynaSqlCon != null)
                {
                    if (G_DynaSqlCon.State == ConnectionState.Open)
                    {
                        G_DynaSqlCon.Close();
                    }
                }

                G_DynaSqlCon = new MySqlConnection(strcnn);

                G_DynaSqlCon.Open();

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

                if(strgroup=="all")
                    sql = string.Format("SELECT * FROM robot_list_t ");
                else
                    sql = string.Format("SELECT * FROM robot_list_t where robot_group='{0}'",strgroup);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                Robot_RegInfo_list.Clear();

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

                        Robot_RegInfo robotreginfo = new Robot_RegInfo();
                        robotreginfo.robot_id = strrobotid;
                        robotreginfo.robot_name = strrobotname;
                        robotreginfo.robot_ip = strrobotip;
                        robotreginfo.robot_group = strrobotgroup;
                        robotreginfo.map_id = strmapid;
                        robotreginfo.map_name = strmapname;
                        Robot_RegInfo_list.Add(strrobotid, robotreginfo);
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
        /// 등록된 맵리스트 읽기
        /// </summary>
        public void onDBRead_Maplist()
        {
            string sql = "";
            bool bdataok = false;

            try
            {
              
                 sql = string.Format("SELECT * FROM map_t ");
              
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                Map_list.Clear();

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strmapid = ds.Tables[0].Rows[i]["map_id"].ToString();
                        string strmapname = ds.Tables[0].Rows[i]["map_name"].ToString();

                        Map_list maplist = new Map_list();
                        maplist.map_id = strmapid;
                        maplist.map_name = strmapname;

                        Map_list.Add(strmapid, maplist);

                        
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_Maplist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_Maplist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 등록된 로봇의 워크 상태읽기
        /// </summary>
        public void onDBRead_RobotStatus()
        {
            try
            {
                Data.Instance.Robot_status_info.Clear();

                string sql = string.Format("SELECT * FROM robot_jobstatus_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                Robot_Status_list.Clear();

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string strrobotid = ds.Tables[0].Rows[i]["robot_id"].ToString();
                        string strworkid = ds.Tables[0].Rows[i]["work_id"].ToString();
                        string strworkstatus = ds.Tables[0].Rows[i]["work_status"].ToString();
                        string strworkcnt = ds.Tables[0].Rows[i]["work_cnt"].ToString();
                        string stractionidx = ds.Tables[0].Rows[i]["action_idx"].ToString();

                        if (strworkid == "") strworkid = "";
                        if (strworkstatus == "") strworkstatus = "wait";


                        string strTemp = string.Format("{0},{1},{2},{3},{4}", strrobotid, strworkid, strworkstatus, strworkcnt, stractionidx);

                        Data.Instance.Robot_status_info.Add(strrobotid, strTemp);

                        Robot_Status robotstatus = new Robot_Status();
                        robotstatus.robot_id = strrobotid;
                        robotstatus.work_id = strworkid;
                        robotstatus.work_status = strworkstatus;
                        robotstatus.work_cnt = strworkcnt;
                        robotstatus.action_idx = stractionidx;

                        Robot_Status_list.Add(strrobotid, robotstatus);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_RobotStatus err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_RobotStatus err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 등록된 미션 리스트 읽어오기
        /// </summary>
        public void onDBRead_Missionlist()
        {
            string sql = "";
            bool bdataok = false;

            try
            {
               

                missionlisttable.missioninfo = new List<MisssionInfo>();

                sql = string.Format("SELECT * FROM missionlist_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        MisssionInfo missioninfo = new MisssionInfo();

                        string strmissionid = ds.Tables[0].Rows[i]["mission_id"].ToString();
                        string strmissionname = ds.Tables[0].Rows[i]["mission_name"].ToString();
                        int nmissionlevel = int.Parse(ds.Tables[0].Rows[i]["mission_level"].ToString());
                        string strworkdata = ds.Tables[0].Rows[i]["work"].ToString();

                        missioninfo.strMisssionID = strmissionid;
                        missioninfo.strMisssionName = strmissionname;
                        missioninfo.nMisssionLevel = nmissionlevel;
                        missioninfo.work = strworkdata;

                        missionlisttable.missioninfo.Add(missioninfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_Missionlist err" + ex2.Message.ToString());
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


                missionlisttable.missioninfo = new List<MisssionInfo>();

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


        /// <summary>
        /// 등록된 작업 리스트 읽기
        /// </summary>
        public void onDBRead_Joblist()
        {
            try
            {
             
                string sql = string.Format("SELECT * FROM job_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, G_SqlCon);
                da.Fill(ds);

                JobSchedule_list.Clear();
                

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string job_id = ds.Tables[0].Rows[i]["job_id"].ToString();
                        string job_name = ds.Tables[0].Rows[i]["job_name"].ToString();
                        string mission_id_list = ds.Tables[0].Rows[i]["loading_mission_id_list"].ToString();
                        string unloadmission_id_list = ds.Tables[0].Rows[i]["unloading_mission_id_list"].ToString();
                        string waitmission_id_list = ds.Tables[0].Rows[i]["wait_mission_id_list"].ToString();
                        string robot_id_list = ds.Tables[0].Rows[i]["robot_id_list"].ToString();
                        string call_type = ds.Tables[0].Rows[i]["call_type"].ToString();
                        string job_status = ds.Tables[0].Rows[i]["job_status"].ToString();
                        string job_group = ds.Tables[0].Rows[i]["job_group"].ToString();

                        JobSchedule job = new JobSchedule();
                        job.mission_id = new List<string>();
                        job.unloadmission_id = new List<string>();
                        job.waitmission_id = new List<string>();
                        job.robot_id = new List<string>();
                        job.job_id = job_id;
                        job.job_name = job_name;
                        job.job_group = job_group;
                        string[] missionlist = mission_id_list.Split(',');
                        string[] unloadmissionlist = unloadmission_id_list.Split(',');
                        string[] waitmissionlist = waitmission_id_list.Split(',');
                        string[] robotlist = robot_id_list.Split(',');

                        for(int j=0; j< missionlist.Length; j++)
                        {
                            if (missionlist[j] == "") missionlist[j] = "Not"; 
                            job.mission_id.Add(missionlist[j]);
                        }
                        for (int j = 0; j < unloadmissionlist.Length; j++)
                        {
                            if (unloadmissionlist[j] == "") unloadmissionlist[j] = "Not";
                            job.unloadmission_id.Add(unloadmissionlist[j]);
                        }
                        for (int j = 0; j < waitmissionlist.Length; j++)
                        {
                            if (waitmissionlist[j] == "") waitmissionlist[j] = "Not"; 
                            job.waitmission_id.Add(waitmissionlist[j]);
                        }

                        for (int j2 = 0; j2 < robotlist.Length; j2++)
                        {
                            job.robot_id.Add(robotlist[j2]);
                        }
                        job.job_status = job_status;
                        job.call_type = call_type;

                        JobSchedule_list.Add(job_id, job);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBRead_Joblist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBRead_Joblist err" + ex2.Message.ToString());
            }
        }

        public void onDBUpdate_Joblist_status(string jobid,string status)//"run")
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = G_SqlCon;
                updateCommand.CommandText = "UPDATE job_t SET job_status=@job_status where job_id=@job_id";

                updateCommand.Parameters.Add("@job_status", MySqlDbType.VarChar, 45);
                updateCommand.Parameters.Add("@job_id", MySqlDbType.VarChar, 45);
             
                updateCommand.Parameters[0].Value = status;
                updateCommand.Parameters[1].Value = jobid;
          
                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBUpdate_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBUpdate_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// job 스케줄 DB 저장
        /// </summary>
        public void onDBSave_JobSchedule(string strjobsavekind, JobSchedule job)
        {
            string sql = "";

            try
            {
                string jobid = job.job_id;
                string jobname = job.job_name;
                string missionlist = "";
                string unloadmissionlist = "";
                string waitmissionlist = "";
                string robotlist = "";
                int cnt = job.mission_id.Count;
                for (int i = 0; i < cnt; i++)
                {
                    missionlist += job.mission_id[i];
                    if (i < cnt - 1)
                        missionlist += ",";
                }

                cnt = job.unloadmission_id.Count;
                for (int i = 0; i < cnt; i++)
                {
                    unloadmissionlist += job.unloadmission_id[i];
                    if (i < cnt - 1)
                        unloadmissionlist += ",";
                }

                cnt = job.waitmission_id.Count;
                for (int i = 0; i < cnt; i++)
                {
                    waitmissionlist += job.waitmission_id[i];
                    if (i < cnt - 1)
                        waitmissionlist += ",";
                }

                cnt = job.robot_id.Count;
                for (int i = 0; i < cnt; i++)
                {
                    robotlist += job.robot_id[i];
                    if (i < cnt - 1)
                        robotlist += ",";
                }

                string calltype = job.call_type;
                string jobstatus = job.job_status;
                string jobgroup = job.job_group;

                if (strjobsavekind == "insert")
                {

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = G_SqlCon;

                    insertCommand.CommandText = "INSERT INTO job_t(job_id, job_name,loading_mission_id_list,unloading_mission_id_list,wait_mission_id_list, robot_id_list,call_type,job_status,job_group)" +
                        "VALUES(@job_id, @job_name,@loading_mission_id_list,@unloading_mission_id_list,@wait_mission_id_list, @robot_id_list,@call_type,@job_status,@job_group)";

                    insertCommand.Parameters.Add("@job_id", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@job_name", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@loading_mission_id_list", MySqlDbType.MediumText);
                    insertCommand.Parameters.Add("@unloading_mission_id_list", MySqlDbType.MediumText);
                    insertCommand.Parameters.Add("@wait_mission_id_list", MySqlDbType.MediumText);
                    insertCommand.Parameters.Add("@robot_id_list", MySqlDbType.MediumText);
                    insertCommand.Parameters.Add("@call_type", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@job_status", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@job_group", MySqlDbType.VarChar, 45);

                    insertCommand.Parameters[0].Value = jobid;
                    insertCommand.Parameters[1].Value = jobname;
                    insertCommand.Parameters[2].Value = missionlist;
                    insertCommand.Parameters[3].Value = unloadmissionlist;
                    insertCommand.Parameters[4].Value = waitmissionlist;
                    insertCommand.Parameters[5].Value = robotlist;
                    insertCommand.Parameters[6].Value = calltype;
                    insertCommand.Parameters[7].Value = jobstatus;
                    insertCommand.Parameters[8].Value = jobgroup;

                    insertCommand.ExecuteNonQuery();
                }
                else if (strjobsavekind == "update")
                {
                    MySqlCommand updateCommand = new MySqlCommand();
                    updateCommand.Connection = G_SqlCon;
                    updateCommand.CommandText = "UPDATE job_t SET  job_name=@job_name,loading_mission_id_list=@loading_mission_id_list," +
                        "unloading_mission_id_list=@unloading_mission_id_list" +
                        ",wait_mission_id_list=@wait_mission_id_list, robot_id_list=@robot_id_list  " +
                        ", call_type=@call_type, job_status=@job_status, job_group=@job_group where job_id=@job_id";

                    
                    updateCommand.Parameters.Add("@job_name", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@loading_mission_id_list", MySqlDbType.MediumText);
                    updateCommand.Parameters.Add("@unloading_mission_id_list", MySqlDbType.MediumText);
                    updateCommand.Parameters.Add("@wait_mission_id_list", MySqlDbType.MediumText);
                    updateCommand.Parameters.Add("@robot_id_list", MySqlDbType.MediumText);
                    updateCommand.Parameters.Add("@call_type", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@job_status", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@job_group", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@job_id", MySqlDbType.VarChar, 45);

                    updateCommand.Parameters[0].Value = jobname;
                    updateCommand.Parameters[1].Value = missionlist;
                    updateCommand.Parameters[2].Value = unloadmissionlist;
                    updateCommand.Parameters[3].Value = waitmissionlist;
                    updateCommand.Parameters[4].Value = robotlist;
                    updateCommand.Parameters[5].Value = calltype;
                    updateCommand.Parameters[6].Value = jobstatus;
                    updateCommand.Parameters[7].Value = jobgroup;
                    updateCommand.Parameters[8].Value = jobid;


                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBDelete_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBDelete_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// Job 스케줄 DB 삭제
        /// </summary>
        public void onDBDelete_JobSchedule(string jobid)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = G_SqlCon;
                deleteCommand.CommandText = "DELETE FROM job_t WHERE job_id=@job_id";

                deleteCommand.Parameters.Add("@job_id", MySqlDbType.VarChar, 45);

                deleteCommand.Parameters[0].Value = jobid;

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBDelete_JobSchedule err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBDelete_JobSchedule err" + ex2.Message.ToString());
            }
        }


        /// <summary>
        /// 미션 리스트에 미션 저장
        /// </summary>
        public void onDBInsert_Missionlist(string strmissionname, string strmissionid, string strmissionlevel)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = G_SqlCon;
                insertCommand.CommandText = "INSERT INTO missionlist_t(mission_id, mission_name,mission_level) VALUES(@mission_id, @mission_name,@mission_level)";

                insertCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 100);
                insertCommand.Parameters.Add("@mission_name", MySqlDbType.VarChar, 100);
                insertCommand.Parameters.Add("@mission_level", MySqlDbType.Int16, 11);

                insertCommand.Parameters[0].Value = strmissionid;
                insertCommand.Parameters[1].Value = strmissionname;
                insertCommand.Parameters[2].Value = int.Parse(strmissionlevel);

                insertCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBInsert_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBInsert_Missionlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 미션 리스트에 미션 액션 업데이트 
        /// </summary>
        public void onDBUpdate_Missionlist(string strmissionid, string stractdata)
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = G_SqlCon;
                updateCommand.CommandText = "UPDATE missionlist_t SET work=@work  where mission_id=@mission_id";

                updateCommand.Parameters.Add("@work", MySqlDbType.MediumText);
                updateCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 100);
              

                updateCommand.Parameters[0].Value = stractdata;
                updateCommand.Parameters[1].Value = strmissionid;
               
                updateCommand.ExecuteNonQuery();
    
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBUpdate_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBUpdate_Missionlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 미션 리스트에 미션 삭제
        /// </summary>
        public void onDBDelete_Missionlist(string strmissionid)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = G_SqlCon;
                deleteCommand.CommandText = "DELETE FROM missionlist_t WHERE mission_id=@mission_id";

                deleteCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 100);

                deleteCommand.Parameters[0].Value = strmissionid;

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBDelete_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBDelete_Missionlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 등록
        /// </summary>
        public void onDBInsert_Robotlist(string robotid, string robotname, string robotip,string robotgroup,string map)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = G_SqlCon;
                insertCommand.CommandText = "INSERT INTO robot_list_t(robot_id, robot_name,robot_ip, robot_group,map_id) VALUES(@robot_id, @robot_name,@robot_ip, @robot_group,@map_id)";

                insertCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@robot_name", MySqlDbType.VarChar, 100);
                insertCommand.Parameters.Add("@robot_ip", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@robot_group", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@map_id", MySqlDbType.VarChar, 45);

                insertCommand.Parameters[0].Value = robotid;
                insertCommand.Parameters[1].Value = robotname;
                insertCommand.Parameters[2].Value = robotip;
                insertCommand.Parameters[3].Value = robotgroup;
                insertCommand.Parameters[4].Value = map;

                insertCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBInsert_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBInsert_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 변경
        /// </summary>
        public void onDBUpdate_Robotlist(string robotid, string robotname, string robotip, string robotgroup, string map)
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = G_SqlCon;
                updateCommand.CommandText = "UPDATE robot_list_t SET robot_name=@robot_name, robot_ip=@robot_ip,robot_group=@robot_group, map_id=@map_id  where robot_id=@robot_id";

                updateCommand.Parameters.Add("@robot_name", MySqlDbType.VarChar, 100);
                updateCommand.Parameters.Add("@robot_ip", MySqlDbType.VarChar, 45);
                updateCommand.Parameters.Add("@robot_group", MySqlDbType.VarChar, 45);
                updateCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);
                updateCommand.Parameters.Add("@map_id", MySqlDbType.VarChar, 45);

                updateCommand.Parameters[0].Value = robotname;
                updateCommand.Parameters[1].Value = robotip;
                updateCommand.Parameters[2].Value = robotgroup;
                updateCommand.Parameters[3].Value = robotid;
                updateCommand.Parameters[4].Value = map;
                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBUpdate_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBUpdate_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 삭제
        /// </summary>
        public void onDBDelete_Robotlist(string robotid)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = G_SqlCon;
                deleteCommand.CommandText = "DELETE FROM robot_list_t WHERE robot_id=@robot_id";

                deleteCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);

                deleteCommand.Parameters[0].Value = robotid;

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("onDBDelete_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show("onDBDelete_Robotlist err" + ex2.Message.ToString());
            }
        }

   
#endregion


        private void FleetManager_MainForm_Load(object sender, EventArgs e)
        {
#if _fleet
            Data.Instance.MAINFORM = this;
#endif
            worker = new Worker(this, 1);

            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);

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

                if(m_strDBConnectstring=="")
                {
                    MessageBox.Show("DB connection file open 에러");
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("FleetManager_MainForm_Load dbconnection file open  err :={0}", ex.Message.ToString());
            }



            if (onInitSql() == 0)
            {
                Data.Instance.isDBConnected = true;

                onDBRead_Robotlist("all");
                onDBRead_RobotStatus();

                onRobots_WorkInfo_InitSet();

                missioneditctrl = new MissionEdit_ctrl(this);
                settingmain_ctrl = new SettingMainCtrl(this);

                monitoringmainctrl = new Monitoring.MonitoringMain_Ctrl(this);
#if !_fleetdemo
                joborderctrl = new order.JobOrder_ctrl(this);
#elif _fleetdemo
                jobdemoorderctrl = new Demo.JobOrder_Demo_ctrl(this);
#endif
            }
            else
            {
                Data.Instance.isDBConnected = false;
                MessageBox.Show("데이타베이스 연결 에러, 점검후 사용하세요.");
                return;
            }
        }

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

      

        /// <summary>
        /// 등록된 로봇의 워크 정보를 초기화 한다.
        /// </summary>
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
                  // string key = info.Key;
                  //  string value = info.Value;
                  //  string[] strvalue = value.Split(',');
                  //  string strrobotid = strvalue[0];
                    string strrobotid = G_robotList.ElementAt(i).ToString();
                    Data.Instance.Robot_work_info.Add(strrobotid, worker.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobots_WorkInfo_InitSet err :={0}", ex.Message.ToString());
            }
        }

        private void FleetManager_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (G_SqlCon != null)
                {
                    if (G_SqlCon.State == ConnectionState.Open)
                    {
                        G_SqlCon.Close();
                    }
                }

                if (monitoringmainctrl != null)
                {
                    if (monitoringmainctrl.robotmonitoringctrl != null)
                    {
                        if (monitoringmainctrl.robotmonitoringctrl.m_bMonitoring)
                        {
                            monitoringmainctrl.robotmonitoringctrl.m_bMonitoring = false;
                        }
                    }
                }
            }
            catch (MySqlException _e)
            {
                MessageBox.Show(_e.Message.ToString());
            }
            catch (Exception _e1)
            {
                MessageBox.Show("FleetManager_MainForm_FormClosing err"+_e1.Message.ToString());
            }
        }

        private void btnMaploading_Click(object sender, EventArgs e)
        {

        }

        private void lblMissionEdit_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);
            panel1.Controls.Add(missioneditctrl);
            missioneditctrl.onInitSet();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Label ownbtn = (Label)sender;
            ownbtn.ForeColor = Color.White;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Label ownbtn = (Label)sender;
            ownbtn.ForeColor = System.Drawing.Color.Khaki;
        }

        private void lblSetting_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);

            panel1.Controls.Add(settingmain_ctrl);
            settingmain_ctrl.onInitSet();
        }

        private void lblJobOrder_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);

            panel1.Size = new Size(1760, 940);
#if !_fleetdemo
            panel1.Controls.Add(joborderctrl);
            joborderctrl.onInitSet();
#elif _fleetdemo
            panel1.Controls.Add(jobdemoorderctrl);
            jobdemoorderctrl.onInitSet();
    
#endif
        }

        private void lblMonitoring_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 1) panel1.Controls.RemoveAt(0);

            panel1.Controls.Add(monitoringmainctrl);
            monitoringmainctrl.onInitSet();
            
        }


        public double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }

      
    }
}
