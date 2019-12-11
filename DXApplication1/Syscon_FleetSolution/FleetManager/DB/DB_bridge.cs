using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//add using
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;

namespace Syscon_Solution.FleetManager.DB
{
    public class DB_bridge
    {
        public DB_bridge()
        {

        }
        LSprogram.mainForm mainForm;
        public DB_bridge(LSprogram.mainForm mainform)
        {
            this.mainForm = mainform;
        }
        public string m_strDBConnectstring = "";
        string m_strDBConnectionPath = "..//Ros_info//dbconnectionpath.txt";

        public string m_strRIDiS_DBConnectstring = "";
        string m_strRIDiS_DBConnectionPath = "..//Ros_info//Ridis_dbconnectionpath.txt";


        //  public MySqlConnection G_SqlCon = null;
        //  public MySqlConnection G_DynaSqlCon = null;

        #region DB 관련
        public void onUpdate(string cautionData)
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();

                updateCommand = new MySqlCommand();
                updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                updateCommand.CommandText = "UPDATE map_t SET caution_area = @cautionarea WHERE (idx = 2)";

                updateCommand.Parameters.Add("@cautionarea", MySqlDbType.LongText);

                updateCommand.Parameters[0].Value = cautionData;

                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                // MessageBox.Show("onDBDelete_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                // MessageBox.Show("onDBDelete_Missionlist err" + ex2.Message.ToString());
            }
        }

        public void onDBAtcconv_insert(string data)
        {
            try
            {
                MySqlCommand insertCommand = new MySqlCommand();

                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "INSERT INTO etc_info_conv(pointdata) VALUES(@data)";

                insertCommand.Parameters.Add("@data", MySqlDbType.VarChar, 50);
                insertCommand.Parameters[0].Value = data;

                insertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("ondbnodearea insert error {0}", e);
            }
        }
        public void onDBnodearea_Insert(string data)
        {
            try
            {
                MySqlCommand insertCommand = new MySqlCommand();

                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "INSERT INTO etc_info(node_area) VALUES(@data)";

                insertCommand.Parameters.Add("@data", MySqlDbType.MediumText);
                insertCommand.Parameters[0].Value = data;

                insertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("ondbnodearea insert error {0}", e);
            }
        }
        public void onDBConnectionPath_OpenCheck()
        {
            try
            {
                if (!File.Exists(m_strDBConnectionPath))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strDBConnectionPath, false, Encoding.Default))
                    {
                        sw.WriteLine("data source=192.168.20.28; database=ridis_db; user id=syscon; password=r023866677!; charset=utf8");
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


                if (!File.Exists(m_strRIDiS_DBConnectionPath))
                {
                    using (StreamWriter sw = new System.IO.StreamWriter(m_strRIDiS_DBConnectionPath, false, Encoding.Default))
                {

                    sw.WriteLine("data source=192.168.20.28; database=ridis_db; user id=syscon; password=r023866677!; charset=utf8");
                    sw.Close();
                }
            }

                using (StreamReader sr1 = new System.IO.StreamReader(m_strRIDiS_DBConnectionPath, Encoding.Default))
                {


                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        m_strRIDiS_DBConnectstring = strTemp;
                    }
                    sr1.Close();

                }

                if (m_strDBConnectstring == "" || m_strRIDiS_DBConnectstring == "")
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDBConnectionPath_OpenCheck  err :={0}", ex.Message.ToString());
            }
        }


        public int onInitSql()
        {
            string strcnn = m_strDBConnectstring;

            try
            {
                if (Data.Instance.G_SqlCon != null)
                {
                    if (Data.Instance.G_SqlCon.State == ConnectionState.Open)
                    {
                        Data.Instance.G_SqlCon.Close();
                    }
                }

                Data.Instance.G_SqlCon = new MySqlConnection(strcnn);

                Data.Instance.G_SqlCon.Open();

                return 0;
            }
            catch (MySqlException _e)
            {
                // MessageBox.Show(_e.Message.ToString());
                return 1;
            }
        }

        public int onRIDiS_InitSql()
        {
            string strcnn = "data source=192.168.20.28;Database=ridis_db;user id=syscon;password=r023866677!; charset=utf8";

            try
            {
                if (Data.Instance.G_DynaSqlCon != null)
                {
                    if (Data.Instance.G_DynaSqlCon.State == ConnectionState.Open)
                    {
                        Data.Instance.G_DynaSqlCon.Close();
                    }
                }

                Data.Instance.G_DynaSqlCon = new MySqlConnection(strcnn);

                Data.Instance.G_DynaSqlCon.Open();

                return 0;
            }
            catch (MySqlException _e)
            {
                // MessageBox.Show(_e.Message.ToString());
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
                //  G_robotList.Clear();

                if (strgroup == "all")
                    sql = string.Format("SELECT * FROM robot_t ");
                else
                    sql = string.Format("SELECT * FROM robot_t where robot_group_id='{0}'", strgroup);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                Data.Instance.Robot_RegInfo_list.Clear();

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        int idx = (int)ds.Tables[0].Rows[i]["idx"];
                        string strrobotid = ds.Tables[0].Rows[i]["robot_id"].ToString();
                        string strrobotname = ds.Tables[0].Rows[i]["robot_name"].ToString();
                        string strrobotip = ds.Tables[0].Rows[i]["address"].ToString();
                        string strrobotgroup = ds.Tables[0].Rows[i]["robot_group_id"].ToString();
                        string strrobotver = ds.Tables[0].Rows[i]["robot_version"].ToString();
                        string strmapid = ds.Tables[0].Rows[i]["map_id"].ToString();
                        string strmapname = "";

                        //sql = string.Format("SELECT * FROM map_t where map_id='{0}'", strmapid);

                        //DataSet ds2 = new DataSet();
                        //MySqlDataAdapter da2 = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                        //da2.Fill(ds2);

                        //int ncnt2 = ds2.Tables[0].Rows.Count;
                        //if (ncnt2 > 0)
                        //{
                        //    strmapname = ds2.Tables[0].Rows[0]["map_name"].ToString();
                        //}

                        // G_robotList.Add(strrobotid);

                        Robot_RegInfo robotreginfo = new Robot_RegInfo();
                        robotreginfo.robot_id = strrobotid;
                        robotreginfo.robot_name = strrobotname;
                        robotreginfo.robot_ip = strrobotip;
                        robotreginfo.robot_group = strrobotgroup;
                        robotreginfo.robot_version = strrobotver;
                        robotreginfo.map_id = strmapid;
                        robotreginfo.map_name = strmapname;
                        int idx_ = idx;

                        Data.Instance.Robot_RegInfo_list.Add(strrobotid, robotreginfo);
                        Data.Instance.Robot_RegInfo_list_ip.Add(idx_, robotreginfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.Out.WriteLine("onDBRead_Robotlist err :={0}", ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.Out.WriteLine("onDBRead_Robotlist err :={0}", ex2.Message.ToString());
            }
        }
        public void onDBRead_conveyor()
        {
            string sql = "";

            try
            {
                sql = string.Format("SELECT * FROM etc_info_conv");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                ATC_ temp;
                for (int i = 0; i < count; i++)
                {
                    temp = new ATC_();
                    string pointdata = ds.Tables[0].Rows[i]["pointdata"].ToString();
                    temp = JsonConvert.DeserializeObject<ATC_>(pointdata);
                    Data.Instance.conveyor_location.Add(temp);
                }
                
            }
            catch
            {
                Console.WriteLine("ondbread actinfo error");
            }
        }
        public void onDBRead_ACTinfo()
        {
            string sql = "";

            try
            {
                sql = string.Format("SELECT * FROM mission_t");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    MissionList missionlist = new MissionList();


                    string mission_id = ds.Tables[0].Rows[i]["mission_id"].ToString();
                    string mission_name = ds.Tables[0].Rows[i]["mission_name"].ToString();
                    string mission_act = ds.Tables[0].Rows[i]["trigger_flag"].ToString();

                    missionlist.missionID = mission_id;
                    missionlist.mission = mission_name;

                    Data.Instance.mission_list.Add(mission_name, missionlist);
                }
            }
            catch
            {
                Console.WriteLine("ondbread actinfo error");
            }
        }

        public void onDBRead_DockingList()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * FROM task_t where task_name like'%DOCKING%'");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {

                        Docking_mission temp = new Docking_mission();
                        string strtaskid = ds.Tables[0].Rows[i]["task_id"].ToString();
                        string strtaskname = ds.Tables[0].Rows[i]["task_name"].ToString();
                        string strmissionlist = ds.Tables[0].Rows[i]["mission_list"].ToString();
                        string strrobotlist = ds.Tables[0].Rows[i]["robot_list"].ToString();

                        temp.taskid = strtaskid;
                        temp.taskname = strtaskname;
                        temp.missionlist = strmissionlist;
                        temp.robotlist = strrobotlist;

                        Data.Instance.docking_mission_list.Add(strtaskname, temp);
                    }
                }
            }
            catch
            {

            }
        }

        public void onDBRead_ABN()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT node_area,node_name FROM etc_info");
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string nodearea = ds.Tables[0].Rows[i]["node_area"].ToString();
                        string nodename = ds.Tables[0].Rows[i]["node_name"].ToString();
                        Data.Instance.node_area.Add(nodename, nodearea);
                    }
                }
                node_area();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void onDBInsert_currentmission(string strrobotid, string current_mission, string required_id, string start_id)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "INSERT INTO current_mission(robot_id,current_mission,required_id,start_id) VALUES(@robot_id, @current_mission,@required_id,@start_id)";

                insertCommand.Parameters.Add("@robot_id", MySqlDbType.String, 10);
                insertCommand.Parameters.Add("@current_mission", MySqlDbType.VarChar, 20);
                insertCommand.Parameters.Add("@required_id", MySqlDbType.VarChar, 20);
                insertCommand.Parameters.Add("@start_id", MySqlDbType.VarChar, 20);




                insertCommand.Parameters[0].Value = strrobotid;
                insertCommand.Parameters[1].Value = current_mission;
                insertCommand.Parameters[2].Value = required_id;
                insertCommand.Parameters[3].Value = start_id;

                insertCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex2.Message.ToString());
            }

        }
        public void onDBInsert_taskcomplete(string strrobotid)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "INSERT INTO task_report(robotname,reg_date) VALUES(@robot_id, @time)";

                insertCommand.Parameters.Add("@robot_id", MySqlDbType.String, 10);
                insertCommand.Parameters.Add("@time", MySqlDbType.Datetime);



                insertCommand.Parameters[0].Value = strrobotid;
                insertCommand.Parameters[1].Value = DateTime.Now;

                insertCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex2.Message.ToString());
            }
        }
        public void onDBRead_currentmission()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT robot_id,current_mission,required_id,start_id FROM current_mission");
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);
                current_mission temp;
                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        temp = new current_mission();
                        string robotid = ds.Tables[0].Rows[i]["robot_id"].ToString();
                        string currentmission = ds.Tables[0].Rows[i]["current_mission"].ToString();
                        string requiredid = ds.Tables[0].Rows[i]["required_id"].ToString();
                        string startid = ds.Tables[0].Rows[i]["start_id"].ToString();
                        temp.atcNO = currentmission;
                        temp.requiredID = requiredid;
                        temp.startID = startid;

                        foreach (KeyValuePair<string, current_mission> mission in Data.Instance.current_mission)
                        {
                            if (mission.Key.Contains(robotid))
                            {
                                Data.Instance.current_mission.Remove(mission.Key);
                            }
                        }
                        Data.Instance.current_mission.Add(robotid, temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void onDBRead_nodearea()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT node_area,node_name FROM etc_info");
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string nodearea = ds.Tables[0].Rows[i]["node_area"].ToString();
                        string nodename = ds.Tables[0].Rows[i]["node_name"].ToString();
                        Data.Instance.node_area.Add(nodename, nodearea);
                    }
                }
                node_area();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        float Wheelratio = (float)0.33;
        float resolution = (float)0.019999999552965164;
        float dOrignX = (float)169.20;
        float dOrignY = (float)821.25;
        public void node_area()
        {
            RectangleF rect;
            node temp = new node();
            foreach (KeyValuePair<string, string> node in Data.Instance.node_area)
            {
                rect = new RectangleF();
                temp = JsonConvert.DeserializeObject<node>(node.Value);

                float x = temp.nX1;
                float y = temp.nY1;
                float x2 = temp.nX2;
                float y2 = temp.nY2;

                float cellx = x / resolution;
                float celly = y / resolution;
                float cellx2 = x2 / resolution;
                float celly2 = y2 / resolution;

                PointF pos = new PointF();
                pos.X = dOrignX + cellx * Wheelratio;
                pos.Y = dOrignY - celly * Wheelratio;

                PointF pos2 = new PointF();
                pos2.X = dOrignX + cellx2 * Wheelratio;
                pos2.Y = dOrignY - celly2 * Wheelratio;

                rect.X = pos.X;
                rect.Y = pos.Y;
                rect.Width = pos2.X - pos.X;
                rect.Height = pos2.Y - pos.Y;
                Data.Instance.location_check_node.Add(node.Key, rect);
            }
        }
        public class node
        {
            public float nX1;
            public float nY1;
            public float nX2;
            public float nY2;
        }
        public void onDBRead_warehouse()
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * FROM mission_t where mission_name like 'ATC_%_wh'");
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string missionid = ds.Tables[0].Rows[i]["mission_name"].ToString();
                        string missionname = ds.Tables[0].Rows[i]["mission_id"].ToString();
                        Data.Instance.docking_warehouse.Add(missionname, missionid);
                    }
                }
            }
            catch
            {
                Console.WriteLine("warehouse docking list load fail");
            }
        }
        public void onDBRead_NodeList()
        {
            string sql = "";
            try
            {

                sql = string.Format("SELECT * FROM mission_t where mission_name like '%NODE%'");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {

                        Node_mission temp = new Node_mission();
                        string strmissionname = ds.Tables[0].Rows[i]["mission_name"].ToString();
                        string strworkdata = ds.Tables[0].Rows[i]["work"].ToString();
                        string strmissionid = ds.Tables[0].Rows[i]["mission_id"].ToString();


                        temp.mission_name = strmissionname;
                        temp.work = strworkdata;
                        temp.mission_id = strmissionid;

                        Data.Instance.node_mission_list.Add(strmissionname, temp);
                    }
                }
            }
            catch (MySqlException ex)
            {
            }
            catch (Exception ex2)
            {
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


                Data.Instance.missionlisttable.missioninfo = new List<MisssionInfo>();

                sql = string.Format("SELECT * FROM mission_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        MisssionInfo missioninfo = new MisssionInfo();

                        string strmissionid = ds.Tables[0].Rows[i]["mission_id"].ToString();
                        string strmissionname = ds.Tables[0].Rows[i]["mission_name"].ToString();
                        string strtrigger_flag = ds.Tables[0].Rows[i]["trigger_flag"].ToString();
                        string strworkdata = ds.Tables[0].Rows[i]["work"].ToString();

                        missioninfo.strMisssionID = strmissionid;
                        missioninfo.strMisssionName = strmissionname;
                        missioninfo.strTrigger_flag = strtrigger_flag;
                        missioninfo.work = strworkdata;

                        Data.Instance.missionlisttable.missioninfo.Add(missioninfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBRead_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                // MessageBox.Show("onDBRead_Missionlist err" + ex2.Message.ToString());
            }
        }

        public MisssionInfo onDBRead_Mission(string mission_id)
        {
            string sql = "";
            bool bdataok = false;
            MisssionInfo missioninfo = new MisssionInfo();

            try
            {


                Data.Instance.missionlisttable.missioninfo = new List<MisssionInfo>();

                sql = string.Format("SELECT * FROM mission_t where mission_id = '{0}'", mission_id);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);



                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {


                        string strmissionid = ds.Tables[0].Rows[i]["mission_id"].ToString();
                        string strmissionname = ds.Tables[0].Rows[i]["mission_name"].ToString();
                        string strtrigger_flag = ds.Tables[0].Rows[i]["trigger_flag"].ToString();
                        string strworkdata = ds.Tables[0].Rows[i]["work"].ToString();

                        missioninfo.strMisssionID = strmissionid;
                        missioninfo.strMisssionName = strmissionname;
                        missioninfo.strTrigger_flag = strtrigger_flag;
                        missioninfo.work = strworkdata;
                    }
                }

                return missioninfo;
            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBRead_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                // MessageBox.Show("onDBRead_Missionlist err" + ex2.Message.ToString());
            }

            return missioninfo;
        }
        public void onDBInsert_Alarm(string message, string description, int level)
        {
            string sql = "";
            MySqlCommand insertCommand = new MySqlCommand();
            insertCommand.Connection = Data.Instance.G_DynaSqlCon;
            insertCommand.CommandText = "Insert into alarm_log(alarm_message,description,remark,level,time_occur) VALUES(@message,description,remark,level,time_occur)";

            insertCommand.Parameters.Add("@message", MySqlDbType.VarChar, 45);
            insertCommand.Parameters.Add("@description", MySqlDbType.VarChar, 45);
            insertCommand.Parameters.Add("@remark", MySqlDbType.VarChar, 45);
            insertCommand.Parameters.Add("@level", MySqlDbType.Int16, 1);
            insertCommand.Parameters.Add("@time", MySqlDbType.VarChar, 45);

            insertCommand.Parameters[0].Value = message;
            insertCommand.Parameters[1].Value = description;
            insertCommand.Parameters[2].Value = "";
            insertCommand.Parameters[3].Value = level;
            insertCommand.Parameters[4].Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            insertCommand.ExecuteNonQuery();

            //seq,name,alarm_message,description,time_occur,remark
        }
        public void onDBInsert_Reservation(string requiredID, string startID, string mission, string atcNo)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "Insert into reservation_t(required,start,mission,atcnumber) VALUES(@required,@start,@mission,@atcnumber)";

                insertCommand.Parameters.Add("@required", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@start", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@mission", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@atcnumber", MySqlDbType.VarChar, 45);

                insertCommand.Parameters[0].Value = requiredID;
                insertCommand.Parameters[1].Value = startID;
                insertCommand.Parameters[2].Value = mission;
                insertCommand.Parameters[3].Value = atcNo;

                insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ondbinsert_reservation error : {0}", ex);
            }
        }


        /// <summary>
        /// 미션 리스트에 미션 저장
        /// </summary>
        public void onDBInsert_Missionlist(string strmissionname, string strmissionid, string strmissionlevel, string strtriggerflag, string stract)
        {
            string sql = "";

            try
            {
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                insertCommand.CommandText = "INSERT INTO mission_t(mission_id, mission_name,trigger_flag,work) VALUES(@mission_id, @mission_name,@trigger_flag,@work)";

                insertCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@mission_name", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@trigger_flag", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@work", MySqlDbType.LongText);
                // insertCommand.Parameters.Add("@start_idx", MySqlDbType.Int16, 11);
                //  insertCommand.Parameters.Add("@loop_flag", MySqlDbType.Int16, 11);



                insertCommand.Parameters[0].Value = strmissionid;
                insertCommand.Parameters[1].Value = strmissionname;
                insertCommand.Parameters[2].Value = strtriggerflag;
                insertCommand.Parameters[3].Value = stract;
                //insertCommand.Parameters[3].Value = 0;
                //insertCommand.Parameters[4].Value = 1;

                insertCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //  MessageBox.Show("onDBInsert_Missionlist err" + ex2.Message.ToString());
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
                deleteCommand.Connection = Data.Instance.G_DynaSqlCon;
                deleteCommand.CommandText = "DELETE FROM mission_t WHERE mission_id=@mission_id";

                deleteCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 45);

                deleteCommand.Parameters[0].Value = strmissionid;

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                // MessageBox.Show("onDBDelete_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                // MessageBox.Show("onDBDelete_Missionlist err" + ex2.Message.ToString());
            }
        }

        public void onDBUpdate_current_mission(string strrobotid, string startid, string requiredid, string atcno)
        {
            string sql = "";

            try
            {
                onDBRead_currentmission();
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                updateCommand.CommandText = "UPDATE current_mission SET current_mission=@atcno,required_id=@requiredid,start_id=@startid where robot_id=@robotid";

                updateCommand.Parameters.Add("@atcno", MySqlDbType.VarChar, 20);
                updateCommand.Parameters.Add("@startid", MySqlDbType.VarChar, 20);
                updateCommand.Parameters.Add("@requiredid", MySqlDbType.VarChar, 20);


                updateCommand.Parameters[0].Value = atcno;
                updateCommand.Parameters[1].Value = requiredid;
                updateCommand.Parameters[2].Value = startid;

                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBUpdate_Frommission err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBUpdate_Frommission err" + ex2.Message.ToString());
            }
        }
        public void onDBUpdate_Frommission(string strrobotid, string from, string to)
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                updateCommand.CommandText = "UPDATE robot_job_t SET from_job=@from,to_job=@to where robot_id=@robotid";

                updateCommand.Parameters.Add("@from", MySqlDbType.VarChar, 30);
                updateCommand.Parameters.Add("@to", MySqlDbType.VarChar, 30);
                updateCommand.Parameters.Add("@robotid", MySqlDbType.VarChar, 45);


                updateCommand.Parameters[0].Value = from;
                updateCommand.Parameters[1].Value = to;
                updateCommand.Parameters[2].Value = strrobotid;

                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBUpdate_Frommission err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBUpdate_Frommission err" + ex2.Message.ToString());
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
                updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                updateCommand.CommandText = "UPDATE mission_t SET work=@work  where mission_id=@mission_id";

                updateCommand.Parameters.Add("@work", MySqlDbType.LongText);
                updateCommand.Parameters.Add("@mission_id", MySqlDbType.VarChar, 45);


                updateCommand.Parameters[0].Value = stractdata;
                updateCommand.Parameters[1].Value = strmissionid;

                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBUpdate_Missionlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //   MessageBox.Show("onDBUpdate_Missionlist err" + ex2.Message.ToString());
            }
        }


        /// <summary>
        /// 등록된 작업 리스트 읽기
        /// </summary>
        public void onDBRead_Tasklist()
        {
            try
            {

                string sql = string.Format("SELECT * FROM task_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                Data.Instance.Task_list.Clear();


                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string task_id = ds.Tables[0].Rows[i]["task_id"].ToString();
                        string task_name = ds.Tables[0].Rows[i]["task_name"].ToString();
                        string mission_id_list = ds.Tables[0].Rows[i]["mission_list"].ToString();
                        string robot_id_list = ds.Tables[0].Rows[i]["robot_list"].ToString();
                        string loop_flag = ds.Tables[0].Rows[i]["loop_flag"].ToString();
                        string task_status = ds.Tables[0].Rows[i]["task_status"].ToString();
                        string robot_group_id = ds.Tables[0].Rows[i]["robot_group_id"].ToString();

                        Task_Info taskinfo = new Task_Info();

                        taskinfo.task_id = task_id;
                        taskinfo.task_name = task_name;
                        taskinfo.taskloopflag = loop_flag;
                        taskinfo.task_status = task_status;
                        taskinfo.mission_id_list = mission_id_list;
                        taskinfo.robot_id_list = robot_id_list;
                        taskinfo.robot_group_id = robot_group_id;



                        Data.Instance.Task_list.Add(task_id, taskinfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBRead_Tasklist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBRead_Tasklist err" + ex2.Message.ToString());
            }
        }
        public void onDBRead_Reservation()
        {
            try
            {
                string sql = string.Format("SELECT * FROM reservation_t");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {

                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 등록된 로봇그룹 리스트 읽기
        /// </summary>
        public void onDBRead_RobotGrouplist()
        {
            try
            {
                Data.Instance.robotgroup_list.robotgroup = new List<Robot_Group>();

                string sql = string.Format("SELECT * FROM robot_group_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                Data.Instance.robotgroup_list.robotgroup.Clear();


                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string _id = ds.Tables[0].Rows[i]["robot_group_id"].ToString();
                        string _name = ds.Tables[0].Rows[i]["robot_group_name"].ToString();


                        Robot_Group robotgroup = new Robot_Group();

                        robotgroup.robot_group_id = _id;
                        robotgroup.robot_group_name = _name;


                        Data.Instance.robotgroup_list.robotgroup.Add(robotgroup);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBRead_RobotGrouplist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBRead_RobotGrouplist err" + ex2.Message.ToString());
            }
        }


        /// <summary>
        /// Task DB 저장
        /// </summary>
        public void onDBSave_Task(string strjobsavekind, Task_Info task)
        {
            string sql = "";

            try
            {


                if (strjobsavekind == "insert")
                {

                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = Data.Instance.G_DynaSqlCon;

                    insertCommand.CommandText = "INSERT INTO task_t(task_id, task_name,mission_list,robot_list,loop_flag, start_idx,task_status,robot_group_id)" +
                        "VALUES(@task_id, @task_name,@mission_list,@robot_list,@loop_flag, @start_idx,@task_status,@robot_group_id)";

                    insertCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@task_name", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@mission_list", MySqlDbType.LongText);
                    insertCommand.Parameters.Add("@robot_list", MySqlDbType.LongText);
                    insertCommand.Parameters.Add("@loop_flag", MySqlDbType.Int16, 11);
                    insertCommand.Parameters.Add("@start_idx", MySqlDbType.Int16, 11);
                    insertCommand.Parameters.Add("@task_status", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@robot_group_id", MySqlDbType.VarChar, 45);


                    insertCommand.Parameters[0].Value = task.task_id;
                    insertCommand.Parameters[1].Value = task.task_name;
                    insertCommand.Parameters[2].Value = task.mission_id_list;
                    insertCommand.Parameters[3].Value = task.robot_id_list;
                    insertCommand.Parameters[4].Value = int.Parse(task.taskloopflag);
                    insertCommand.Parameters[5].Value = int.Parse(task.start_idx);
                    insertCommand.Parameters[6].Value = task.task_status;
                    insertCommand.Parameters[7].Value = task.robot_group_id;

                    insertCommand.ExecuteNonQuery();
                }
                else if (strjobsavekind == "update")
                {
                    MySqlCommand updateCommand = new MySqlCommand();
                    updateCommand.Connection = Data.Instance.G_DynaSqlCon;

                    updateCommand.CommandText = "UPDATE task_t SET task_name=@task_name," +
                        "mission_list=@mission_list" +
                        ", robot_list=@robot_list  " +
                        ", loop_flag=@loop_flag, start_idx=@start_idx, task_status=@task_status,robot_group_id=@robot_group_id where task_id=@task_id";


                    updateCommand.Parameters.Add("@task_name", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@mission_list", MySqlDbType.LongText);
                    updateCommand.Parameters.Add("@robot_list", MySqlDbType.LongText);
                    updateCommand.Parameters.Add("@loop_flag", MySqlDbType.Int16, 11);
                    updateCommand.Parameters.Add("@start_idx", MySqlDbType.Int16, 11);
                    updateCommand.Parameters.Add("@task_status", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@robot_group_id", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);

                    updateCommand.Parameters[0].Value = task.task_name;
                    updateCommand.Parameters[1].Value = task.mission_id_list;
                    updateCommand.Parameters[2].Value = task.robot_id_list;
                    updateCommand.Parameters[3].Value = task.taskloopflag;
                    updateCommand.Parameters[4].Value = task.start_idx;
                    updateCommand.Parameters[5].Value = task.task_status;
                    updateCommand.Parameters[6].Value = task.robot_group_id;
                    updateCommand.Parameters[7].Value = task.task_id;

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                //  MessageBox.Show("onDBDelete_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //  MessageBox.Show("onDBDelete_Robotlist err" + ex2.Message.ToString());
            }
        }

        /// <summary>
        /// task DB 삭제
        /// </summary>
        public void onDBDelete_Task(string task_id)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = Data.Instance.G_DynaSqlCon;
                deleteCommand.CommandText = "DELETE FROM task_t WHERE task_id=@task_id";

                deleteCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);

                deleteCommand.Parameters[0].Value = task_id;

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                // MessageBox.Show("onDBDelete_JobSchedule err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                //  MessageBox.Show("onDBDelete_JobSchedule err" + ex2.Message.ToString());
            }
        }


        /// <summary>
        /// 등록된 XIS 리스트 읽기
        /// </summary>
        public void onDBRead_XisInfolist()
        {
            try
            {
                Data.Instance.xisInfo_list.xisinfo = new List<Xis_Info>();

                string sql = string.Format("SELECT * FROM xis_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                Data.Instance.xisInfo_list.xisinfo.Clear();


                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string _id = ds.Tables[0].Rows[i]["xis_id"].ToString();
                        string _address = ds.Tables[0].Rows[i]["address"].ToString();
                        string _name = ds.Tables[0].Rows[i]["xis_name"].ToString();
                        string _version = ds.Tables[0].Rows[i]["xis_version"].ToString();
                        string _group_id = ds.Tables[0].Rows[i]["xis_group_id"].ToString();
                        string _trigger_id_list = ds.Tables[0].Rows[i]["trigger_id_list"].ToString();

                        Xis_Info xisinfo = new Xis_Info();

                        xisinfo.xis_id = _id;
                        xisinfo.address = _address;
                        xisinfo.xis_name = _name;
                        xisinfo.xis_version = _version;
                        xisinfo.xis_group_id = _group_id;
                        xisinfo.trigger_id_list = _trigger_id_list;

                        Data.Instance.xisInfo_list.xisinfo.Add(xisinfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBRead_XisInfolist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBRead_XisInfolist err" + ex2.Message.ToString());
            }
        }


        /// <summary>
        /// 등록된 trigger 리스트 읽기
        /// </summary>
        public void onDBRead_TriggerInfolist()
        {
            try
            {
                Data.Instance.triggerInfo_list.triggerinfo = new List<Trigger_Info>();

                string sql = string.Format("SELECT * FROM trigger_t ");

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                Data.Instance.triggerInfo_list.triggerinfo.Clear();


                int ncnt = ds.Tables[0].Rows.Count;
                if (ncnt > 0)
                {
                    for (int i = 0; i < ncnt; i++)
                    {
                        string _id = ds.Tables[0].Rows[i]["trigger_id"].ToString();
                        string _name = ds.Tables[0].Rows[i]["trigger_name"].ToString();

                        Trigger_Info triggerinfo = new Trigger_Info();

                        triggerinfo.trigger_id = _id;
                        triggerinfo.trigger_name = _name;


                        Data.Instance.triggerInfo_list.triggerinfo.Add(triggerinfo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBRead_TriggerInfolist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBRead_TriggerInfolist err" + ex2.Message.ToString());
            }
        }

        public void onDBUpdate_Tasklist_status(string taskid, string status)//"run")
        {
            string sql = "";

            try
            {
                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                updateCommand.CommandText = "UPDATE task_t SET task_status=@task_status where task_id=@task_id";

                updateCommand.Parameters.Add("@task_status", MySqlDbType.VarChar, 45);
                updateCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);

                updateCommand.Parameters[0].Value = status;
                updateCommand.Parameters[1].Value = taskid;

                updateCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBUpdate_Tasklist_status err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBUpdate_Tasklist_status err" + ex2.Message.ToString());
            }
        }

        public void onDBSave_TaskOperation(string taskid, string robot_id, string mission_list, string currmission_id, int curraction_idx, string strkind) //strkind => insert, update
        {
            string sql = "";

            try
            {
                if (strkind == "update")
                {
                    MySqlCommand updateCommand = new MySqlCommand();
                    updateCommand.Connection = Data.Instance.G_DynaSqlCon;
                    updateCommand.CommandText = "UPDATE task_operation_t SET curr_mission_id=@curr_mission_id , curr_action_idx=@curr_action_idx where task_id=@task_id and robot_id=@robot_id";

                    updateCommand.Parameters.Add("@curr_mission_id", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@curr_action_idx", MySqlDbType.Int16, 11);
                    updateCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                    updateCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);


                    updateCommand.Parameters[0].Value = currmission_id;
                    updateCommand.Parameters[1].Value = curraction_idx;
                    updateCommand.Parameters[2].Value = taskid;
                    updateCommand.Parameters[3].Value = robot_id;

                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    MySqlCommand insertCommand = new MySqlCommand();
                    insertCommand.Connection = Data.Instance.G_DynaSqlCon;
                    insertCommand.CommandText = "INSERT INTO task_operation_t(task_id, robot_id,mission_list) VALUES(@task_id, @robot_id,@mission_list)";

                    insertCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);
                    insertCommand.Parameters.Add("@mission_list", MySqlDbType.LongText);


                    insertCommand.Parameters[0].Value = taskid;
                    insertCommand.Parameters[1].Value = robot_id;
                    insertCommand.Parameters[2].Value = mission_list;

                    insertCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBSave_TaskOperation err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBSave_TaskOperation err" + ex2.Message.ToString());
            }
        }

        public void onDBDelete_TaskOperation(string taskid, string robot_id)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = Data.Instance.G_DynaSqlCon;
                if (robot_id == "")
                {
                    deleteCommand.CommandText = "DELETE FROM task_operation_t WHERE task_id=@task_id ";

                    deleteCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);

                    deleteCommand.Parameters[0].Value = taskid;
                }
                else
                {
                    deleteCommand.CommandText = "DELETE FROM task_operation_t WHERE task_id=@task_id and robot_id=@robot_id";

                    deleteCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                    deleteCommand.Parameters.Add("@robot_id", MySqlDbType.VarChar, 45);

                    deleteCommand.Parameters[0].Value = taskid;
                    deleteCommand.Parameters[1].Value = robot_id;
                }
                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("onDBDelete_TaskOperation err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
                Console.WriteLine("onDBDelete_TaskOperation err" + ex2.Message.ToString());
            }
        }

        /*
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

              //  Map_list.Clear();

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

                //        Map_list.Add(strmapid, maplist);


                    }
                }
            }
            catch (MySqlException ex)
            {
             //   MessageBox.Show("onDBRead_Maplist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
              //  MessageBox.Show("onDBRead_Maplist err" + ex2.Message.ToString());
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

               // Robot_Status_list.Clear();

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

                  //      Robot_Status_list.Add(strrobotid, robotstatus);
                    }
                }
            }
            catch (MySqlException ex)
            {
              //  MessageBox.Show("onDBRead_RobotStatus err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
             //   MessageBox.Show("onDBRead_RobotStatus err" + ex2.Message.ToString());
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


              //  missionlisttable.missioninfo = new List<MisssionInfo>();

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
              //  MessageBox.Show("onDBRead_Mission_action err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
             //   MessageBox.Show("onDBRead_Mission_action err" + ex2.Message.ToString());
            }

            return stractdata;
        }


        

       

        

       


        

       

       

        /// <summary>
        /// 로봇 등록
        /// </summary>
        public void onDBInsert_Robotlist(string robotid, string robotname, string robotip, string robotgroup, string map)
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
             //   MessageBox.Show("onDBInsert_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
               // MessageBox.Show("onDBInsert_Robotlist err" + ex2.Message.ToString());
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
              //  MessageBox.Show("onDBUpdate_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
              //  MessageBox.Show("onDBUpdate_Robotlist err" + ex2.Message.ToString());
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
              //  MessageBox.Show("onDBDelete_Robotlist err" + ex.Message.ToString());
            }
            catch (Exception ex2)
            {
             //   MessageBox.Show("onDBDelete_Robotlist err" + ex2.Message.ToString());
            }
        }
        */

        #endregion
    }
}
