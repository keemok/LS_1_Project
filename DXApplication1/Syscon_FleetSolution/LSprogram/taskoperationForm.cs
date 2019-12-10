using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Syscon_Solution.LSprogram
{
    public partial class taskoperationForm : UserControl
    {
        mainForm mainform;
        firstForm firstform;
        public taskoperationForm(mainForm frm)
        {
            InitializeComponent();
            mainform = frm;
            onDBRead_missioninfo();
            Readnodemission();
        }
        public taskoperationForm(firstForm frm)
        {
            firstform = frm;
        }
        public taskoperationForm()
        {
            InitializeComponent();

        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {

        }



        public void oninit()
        {
            mainform.dbBridge.onDBRead_DockingList();
            mainform.dbBridge.onDBRead_NodeList();
            
            nodexy_Insert();
            
        }

        string[] missionbuf_in;
        string[] missionbuf_out;

        private void Readnodemission()
        {
            int i = 0;
            foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
            {
                mission_list[i] = mission.Value.mission_id;
                i++;
            }
        }
        public void parking(string missionlist, string strrobotid)
        {
            string sql = "";
            try
            {
                DateTime dt = DateTime.Now;
                string strtime = string.Format("{0:d4}{1:d2}", dt.Minute, dt.Millisecond);


                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;

                insertCommand.CommandText = "INSERT INTO task_t(task_id,task_name,mission_list,robot_list," +
                    "loop_flag,start_idx,task_status,robot_group_id)" +
                    "VALUES(@task_id,@task_name,@mission_list,@robot_list,@loop_flag,@start_idx,@task_status,@robot_group_id)";
                insertCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@task_name", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@mission_list", MySqlDbType.LongText);
                insertCommand.Parameters.Add("@robot_list", MySqlDbType.LongText);
                insertCommand.Parameters.Add("@loop_flag", MySqlDbType.Int16, 11);
                insertCommand.Parameters.Add("@start_idx", MySqlDbType.Int16, 11);
                insertCommand.Parameters.Add("@task_status", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@robot_group_id", MySqlDbType.VarChar, 45);

                insertCommand.Parameters[0].Value = strtime;
                insertCommand.Parameters[1].Value = string.Format("{0}_Parking", strrobotid);
                insertCommand.Parameters[2].Value = missionlist;
                insertCommand.Parameters[3].Value = strrobotid;
                insertCommand.Parameters[4].Value = 1;
                insertCommand.Parameters[5].Value = 0;
                insertCommand.Parameters[6].Value = "wait";
                insertCommand.Parameters[7].Value = "ALL";

                insertCommand.ExecuteNonQuery();

                string[] missionbuf = new string[] { missionlist };
                mainform.onTaskOrder(strtime, missionbuf, string.Format(strrobotid + "_Parking"), missionlist, strrobotid, 1);
                onTaskResum(strrobotid);
            }
            catch (Exception e)
            {
                Console.WriteLine("에러 -> {0}", e);
            }
        }

        public async void taskSave(string atcno, string missionlist, string strrobotid)
        {
            string sql = "";
            try
            {
                DateTime dt = DateTime.Now;
                string task_id = string.Format("{0:d4}{1:d2}", dt.Minute, dt.Millisecond);

                if(atcno == "99")
                {
                    task_id = "RETURN";
                }
                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = Data.Instance.G_DynaSqlCon;

                insertCommand.CommandText = "INSERT INTO task_t(task_id,task_name,mission_list,robot_list," +
                    "loop_flag,start_idx,task_status,robot_group_id)" +
                    "VALUES(@task_id,@task_name,@mission_list,@robot_list,@loop_flag,@start_idx,@task_status,@robot_group_id)";
                insertCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@task_name", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@mission_list", MySqlDbType.LongText);
                insertCommand.Parameters.Add("@robot_list", MySqlDbType.LongText);
                insertCommand.Parameters.Add("@loop_flag", MySqlDbType.Int16, 11);
                insertCommand.Parameters.Add("@start_idx", MySqlDbType.Int16, 11);
                insertCommand.Parameters.Add("@task_status", MySqlDbType.VarChar, 45);
                insertCommand.Parameters.Add("@robot_group_id", MySqlDbType.VarChar, 45);

                insertCommand.Parameters[0].Value = task_id;
                insertCommand.Parameters[1].Value = strrobotid;
                insertCommand.Parameters[2].Value = missionlist;
                insertCommand.Parameters[3].Value = textBox3.Text.ToString();
                insertCommand.Parameters[4].Value = 1;
                insertCommand.Parameters[5].Value = 0;
                insertCommand.Parameters[6].Value = "wait";
                insertCommand.Parameters[7].Value = "ALL";

                insertCommand.ExecuteNonQuery();

                string[] missionbuf = new string[] { missionlist };
                mainform.onTaskOrder(task_id, missionbuf, strrobotid, missionlist, strrobotid, 1);
                //Thread.Sleep(1000);
                onTaskResum(strrobotid);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("에러 -> {0}", e);
            }
        }

        private string addDockingmission(string originalmission, string dockingmission)
        {
            string result = originalmission + "," + dockingmission;
            return result;
        }
        public string ConvertString(string[] array)
        {
            string result = string.Join(",", array);
            return result;
        }
        public void messageTest(string requiredID)
        {
            MessageBox.Show("slakdfj{0}", requiredID);
        }
        private async void onTaskOrder(string strTaskid, string[] missionbuf, string taskname, string strMissionid_list, string strRobotlist, int ntaskcnt)
        {
            try
            {
                Task_Order taskorder = new Task_Order();
                int cnt = missionbuf_in.Length;
                List<MisssionInfo> missioninfo_list = new List<MisssionInfo>();
                for (int i = 0; i < cnt; i++)
                {
                    MisssionInfo missioninfo = mainform.dbBridge.onDBRead_Mission(missionbuf_in[i]);
                    missioninfo_list.Add(missioninfo);
                }

                taskorder.task_id = strTaskid;
                taskorder.loop_flag = ntaskcnt;
                taskorder.missionlist = strMissionid_list;
                taskorder.robotlist = strRobotlist;

                mainform.commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list);

                // var task = Task.Run(() => mainform.commBridge.onTaskrder_publish(taskorder, taskname, missioninfo_list));
                // await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskOrder err=" + ex.Message.ToString());
            }
        }
        public async void onTaskResum(string robotid)
        {
            try
            {
                string strRobot = robotid;
                mainform.commBridge.onTaskResume_publish(strRobot, "");
                Console.WriteLine("resume = {0}", strRobot);

                //onConsolemsgDp(string.Format("resume = {0}", strRobot));
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskResum err=" + ex.Message.ToString());
            }
        }
        public void onDBRead_missioninfo()
        {
            string sql = "";

            try
            {
                sql = "select * from mission_t";

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
                da.Fill(ds);

                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Robot_mission temp = new Robot_mission();

                        string strmissionid = ds.Tables[0].Rows[i]["mission_id"].ToString();
                        string strmissionname = ds.Tables[0].Rows[i]["mission_name"].ToString();

                        temp.missionid = strmissionid;
                        temp.missionname = strmissionname;

                        robot_info.Add(strmissionname, temp);
                    }
                }
            }
            catch
            {

            }
        }
        Dictionary<string/*미션 이름*/, Robot_mission> robot_info = new Dictionary<string, Robot_mission>();
        private void waitBtn_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Robot_mission> info in robot_info)
            {
                if (info.Key == "WAIT")
                {
                    //LS_taskorder("R_007", info.Value.missionid);
                    Thread.Sleep(1000);
                }
                else
                {
                }
            }
        }
        string dock_in, dock_out;
        public void docking_A()
        {
            foreach (KeyValuePair<string, Robot_mission> info in robot_info)
            {
                if (info.Key == "NODE_3")
                {
                    dock_in = info.Value.missionid;
                }
                if (info.Key == "NODE_1")
                {
                    dock_out = info.Value.missionid;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dijkstra_in();
        }




        #region dijkstra variable
        const int MAX_NODE = 15;
        const int INF = 100000;
        int[,] a = new int[MAX_NODE, MAX_NODE] {{ 0, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, },
                                              {INF, 0, INF, INF, 1, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
    {INF,   1,  0,  INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{1,   INF,    INF,    0,  INF,  INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    1,    0,  INF,  INF,   1,    INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    1,    INF,    1,    0,  INF,    INF,   INF,  INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    1,  INF,    INF,    0,  INF,  INF,    INF,    INF,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,  INF,    1,    0,  1,  INF,    1,    INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    1,    INF,    INF,    0,  INF,    INF,    INF,  INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    1,  INF,    INF,    0,  INF,  INF,    INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    2,    0,  INF,  INF,    1,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,    INF,    1,    0,  INF,    INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,  INF,    INF,    0,  INF,    INF},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,  0,  1},
{INF,   INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    INF,    1,    INF,    INF,  0} };

        #endregion
        string startNode;
        int s, e;
        //nodeXY[] nodeXY_pose = new nodeXY[MAX_NODE];
        string[] mission_list = new string[MAX_NODE];
        class nodeXY
        {
            public float nodeX;
            public float nodeY;
        }
        private void nodexy_Insert()
        {
            int idx = 0;
            foreach (KeyValuePair<string, WorkFlowGoal> mission in Data.Instance.nodemissionDic)
            {
                //mission_list[idx] = mission.Value.work[0].action_args;
                idx++;
            }
        }
        private void dijkstra_in()
        {



            int i, j, k, r, min;
            int[] v = new int[MAX_NODE];
            int[] distance = new int[MAX_NODE];
            int[] via = new int[MAX_NODE];
            int bb1 = 1;
            k = 0;


            //s = int.Parse(textBox1.Text.ToString());
            //e = int.Parse(textBox2.Text.ToString());
            s = int.Parse(textBox1.Text);
            e = int.Parse(textBox2.Text);




            for (j = 0; j < MAX_NODE; j++)
            {
                v[j] = 0;
                distance[j] = INF;
            }

            distance[s - 1] = 0;

            int[] path = new int[MAX_NODE];
            int path_cnt = 0;
            for (i = 0; i < MAX_NODE; i++)
            {
                min = INF;
                for (j = 0; j < MAX_NODE; j++)
                {
                    if (v[j] == 0 && distance[j] < min)
                    {
                        k = j;
                        min = distance[j];
                    }
                }

                v[k] = 1;

                if (min == INF) break;
                {
                    for (j = 0; j < MAX_NODE; j++)
                    {
                        if (distance[j] > distance[k] + a[k, j])
                        {
                            distance[j] = distance[k] + a[k, j];
                            via[j] = k;
                        }
                    }
                }


                path_cnt = 0;
                k = e - 1;
                while (bb1 < 3)
                {
                    if (path_cnt > MAX_NODE - 1) break;

                    path[path_cnt++] = k;

                    if (k == s - 1) break;

                    k = via[k];
                }

            }

            string strdist = "";
            string strnode = "";

            strdist = string.Format("{0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);
            Console.WriteLine("\n {0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);

            strnode = " 경로는 : ";
            missionbuf_in = new string[path_cnt];
            Console.WriteLine(" 경로는 : ");
            int n = 0;

            for (i = path_cnt - 1; i >= 1; i--)
            {
                Console.WriteLine("{0} -> ", path[i] + 1);
                missionbuf_in[n] = mission_list[path[i]];
                n++;
            }
            missionbuf_in[n] = mission_list[path[i]];
            Console.WriteLine("{0}입니다", path[i] + 1);
            n = 0;

            test();
        }
        public void dijkstra_2(int start,int end)
        {

            if (start > MAX_NODE) return;

            int i, j, k, r, min;
            int[] v = new int[MAX_NODE];
            int[] distance = new int[MAX_NODE];
            int[] via = new int[MAX_NODE];
            int bb1 = 1;
            k = 0;


            //s = int.Parse(textBox1.Text.ToString());
            //e = int.Parse(textBox2.Text.ToString());
            
            




            for (j = 0; j < MAX_NODE; j++)
            {
                v[j] = 0;
                distance[j] = INF;
            }

            distance[start - 1] = 0;

            int[] path = new int[MAX_NODE];
            int path_cnt = 0;
            for (i = 0; i < MAX_NODE; i++)
            {
                min = INF;
                for (j = 0; j < MAX_NODE; j++)
                {
                    if (v[j] == 0 && distance[j] < min)
                    {
                        k = j;
                        min = distance[j];
                    }
                }

                v[k] = 1;

                if (min == INF) break;
                {
                    for (j = 0; j < MAX_NODE; j++)
                    {
                        if (distance[j] > distance[k] + a[k, j])
                        {
                            distance[j] = distance[k] + a[k, j];
                            via[j] = k;
                        }
                    }
                }


                path_cnt = 0;
                k = end - 1;
                while (bb1 < 3)
                {
                    if (path_cnt > MAX_NODE - 1) break;

                    path[path_cnt++] = k;

                    if (k == start - 1) break;

                    k = via[k];
                }

            }

            string strdist = "";
            string strnode = "";

            strdist = string.Format("{0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", start, end, distance[end - 1]);
            Console.WriteLine("\n {0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", start, end, distance[end - 1]);

            strnode = " 경로는 : ";
            missionbuf_in = new string[path_cnt];
            Console.WriteLine(" 경로는 : ");
            int n = 0;

            for (i = path_cnt - 1; i >= 1; i--)
            {
                Console.WriteLine("{0} -> ", path[i] + 1);
                missionbuf_in[n] = mission_list[path[i]];
                n++;
            }
            missionbuf_in[n] = mission_list[path[i]];
            Console.WriteLine("{0}입니다", path[i] + 1);
            n = 0;

            test();
        }

        private void test()
        {

            try
            {
                int cnt = missionbuf_in.Count();


                WorkFlowGoal workflowgoal = new WorkFlowGoal();

                workflowgoal.work_id = "TEMP_";
                workflowgoal.action_start_idx = 0;
                workflowgoal.loop_flag = 1;


                for (int i = 0; i < cnt; i++)
                {
                    DB_MissionData db_missiondata = new DB_MissionData();
                    MisssionInfo missioninfo = new MisssionInfo();
                    string strmissionid = missionbuf_in[i];
                    missioninfo = mainform.dbBridge.onDBRead_Mission(strmissionid);

                    string strwork = missioninfo.work;
                    db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);
                    float x = db_missiondata.work[0].action_args[0];
                    float y = db_missiondata.work[0].action_args[1];
                    float theta = db_missiondata.work[0].action_args[2];

                    Action act = new Action();
                    act.action_type = (int)Data.ACTION_TYPE.Goal_Point;

                    act.action_args.Add(x);
                    act.action_args.Add(y);
                    act.action_args.Add(theta);
                    ParameterSet paramset = new ParameterSet();
                    paramset.param_name = "max_trans_vel";
                    paramset.type = "float";
                    paramset.value = "1.0";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "xy_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.15";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "yaw_goal_tolerance";
                    paramset.type = "float";
                    paramset.value = "0.05";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "p_drive";
                    paramset.type = "float";
                    paramset.value = "0.4";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "d_drive";
                    paramset.type = "float";
                    paramset.value = "1.2";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "wp_tolerance";
                    paramset.type = "float";
                    paramset.value = "1";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "avoid";
                    paramset.type = "bool";
                    paramset.value = "true";
                    act.action_params.Add(paramset);

                    paramset = new ParameterSet();
                    paramset.param_name = "passing_flag";
                    paramset.type = "bool";
                    paramset.value = "true";
                    act.action_params.Add(paramset);

                    workflowgoal.work.Add(act);
                }
                string strMissionData_Json = JsonConvert.SerializeObject(workflowgoal);
                mainform.dbBridge.onDBInsert_Missionlist(DateTime.Now.ToString("LS_" + "yyyyMMddhhmm"), "TEMP_", "0", "", strMissionData_Json);
            }
            catch (Exception e)
            {
                Console.WriteLine("insert mission buf error -> {0}", e);
            }
        }
        WorkFlowGoal workflowgoal = new WorkFlowGoal();
        public string onDBRead_Mission_action(string strmissionid)
        {
            string sql = "";
            string stractdata = "";
            try
            {
                sql = string.Format("SELECT work FROM missionlist_t where mission_id='{0}'", strmissionid);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, Data.Instance.G_DynaSqlCon);
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
        private void dijkstra_out()
        {
            int i, j, k, r, min;
            int[] v = new int[MAX_NODE];
            int[] distance = new int[MAX_NODE];
            int[] via = new int[MAX_NODE];
            int bb1 = 1;
            k = 0;

            //s = int.Parse(textBox1.Text.ToString());
            //e = int.Parse(textBox2.Text.ToString());
            s = int.Parse(textBox1.Text);
            e = int.Parse(textBox2.Text);

            for (j = 0; j < MAX_NODE; j++)
            {
                v[j] = 0;
                distance[j] = INF;
            }

            distance[s - 1] = 0;

            int[] path = new int[MAX_NODE];
            int path_cnt = 0;
            for (i = 0; i < MAX_NODE; i++)
            {
                min = INF;
                for (j = 0; j < MAX_NODE; j++)
                {
                    if (v[j] == 0 && distance[j] < min)
                    {
                        k = j;
                        min = distance[j];
                    }
                }

                v[k] = 1;

                if (min == INF) break;
                {
                    for (j = 0; j < MAX_NODE; j++)
                    {
                        if (distance[j] > distance[k] + a[k, j])
                        {
                            distance[j] = distance[k] + a[k, j];
                            via[j] = k;
                        }
                    }
                }


                path_cnt = 0;
                k = e - 1;
                while (bb1 < 3)
                {
                    if (path_cnt > MAX_NODE - 1) break;

                    path[path_cnt++] = k;

                    if (k == s - 1) break;

                    k = via[k];
                }

            }

            string strdist = "";
            string strnode = "";

            strdist = string.Format("{0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);

            Console.WriteLine("\n {0}에서 출발하여, {1}로 가는 최단 거리는 {2}입니다.\n", s, e, distance[e - 1]);

            strnode = " 경로는 : ";
            missionbuf_out = new string[path_cnt];
            Console.WriteLine(" 경로는 : ");
            int n = 0;
            for (i = path_cnt - 1; i >= 1; i--)
            {
                Console.WriteLine("{0} -> ", path[i] + 1);

                //strnode += string.Format("{0},", path[i] + 1);
                missionbuf_out[n] = mission_list[path[i]];
                n++;
            }
            missionbuf_out[n] = mission_list[path[i]];
            n = 0;
            //strnode += string.Format("{0},", path[i] + 1);


            Console.WriteLine("{0}입니다", path[i] + 1);

            //listBox1.Items.Add(strdist);
            //listBox1.Items.Add(strnode);
        }
        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string temp = ConvertString(missionbuf_in);
            taskSave("999", temp, textBox3.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql = "";

            try
            {
                MySqlCommand deleteCommand = new MySqlCommand();

                deleteCommand = new MySqlCommand();
                deleteCommand.Connection = Data.Instance.G_DynaSqlCon;
                deleteCommand.CommandText = "DELETE FROM task_t WHERE task_id=@task_id";

                deleteCommand.Parameters.Add("@task_id", MySqlDbType.VarChar, 45);

                deleteCommand.Parameters[0].Value = "LS_TEST_";

                deleteCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                // MessageBox.Show("onDBDelete_JobSchedule err" + ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //    string[] startmission = ConvertString(missionbuf_in).Split(',');
            //    //onTaskOrder("LS_TEST_", startmission, "LS TASK테스트", value, "R_007", 1);
            //    onTaskOrder("LS_TEST_", startmission, "LS TASK테스트", missionbuf_in, "R_007", 1);
            Thread.Sleep(1000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            onTaskResum("R_007");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            onTaskPause("R_007");
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //ros 연결후 
                if (Data.Instance.isConnected)
                {

                    string taskid = "LS_TEST_";

                    string missionlist = ConvertString(missionbuf_in);
                    string robotlist = "R_007";

                    string[] robotbuf = robotlist.Split(',');

                    onTaskCancel(robotbuf);
                    {
                        //db 정보 갱신
                        mainform.dbBridge.onDBUpdate_Tasklist_status(taskid, "wait");

                        //task operation db 삭제 
                        for (int i2 = 0; i2 < robotbuf.Length; i2++)
                        {
                            mainform.dbBridge.onDBDelete_TaskOperation(taskid, robotbuf[i2]);
                        }

                        //쓰레드 삭제
                        int threadcnt = Data.Instance.TaskCheck_threadList.Count;
                        if (threadcnt > 0)
                        {
                            int thridx = 0;
                            TaskCheck_class taskclass = Data.Instance.TaskCheck_threadList.ElementAt(thridx);

                            if (taskclass.strTaskid == taskid)
                            {
                                if (taskclass.taskthred != null)
                                {
                                    taskclass.taskthred.Abort();
                                    taskclass = null;
                                }

                                Data.Instance.TaskCheck_threadList.RemoveAt(thridx);
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnTaskStop_Click err=" + ex.Message.ToString());
            }
        }
        private async void onTaskCancel(string[] robotbuf)
        {
            try
            {
                int nrobottotalcnt = robotbuf.Length;

                for (int nrobotcnt = 0; nrobotcnt < nrobottotalcnt; nrobotcnt++)
                {

                    string strRobot = robotbuf[nrobotcnt];
                    var task = Task.Run(() => mainform.commBridge.onTaskCancel_publish(strRobot, ""));
                    await task;

                    Thread.Sleep(100);

                    Console.WriteLine("cancel = {0}" + strRobot);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onTaskCancel err=" + ex.Message.ToString());
            }
        }

        string value;
        private void button10_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Docking_mission> mission in Data.Instance.docking_mission_list)
            {
                if (mission.Key.Contains("DOCKING_A"))
                {
                    value = addDockingmission(ConvertString(missionbuf_in), mission.Value.missionlist);
                    Console.WriteLine(value);
                }
            }
        }
        private void whatisDocking(string dockingpoint)
        {
            if (dockingpoint == "B")
            {
                textBox2.Text = "4";

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dijkstra_out();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            LSprogram.etc.alarmForm alarmform;
            alarmform = new etc.alarmForm();

            alarmform.Show();
            alarmform.alarmOccur("ABORT");

        }

        private void button23_Click(object sender, EventArgs e)
        {

        }
        //GO_12_3,MID20191115161141,GO_2_9,MID20191115114505,GO_6_12
        string[] tempbuf = { "GO_12_3", "MID20191115161141", "GO_2_9", "MID20191115114505", "GO_6_12" };

        private void button24_Click(object sender, EventArgs e)
        {
            onTaskOrder("0044447", tempbuf, "LS TASK테스트", ConvertString(tempbuf), "R_005", 1);
            Thread.Sleep(500);
            onTaskResum("R_006");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            mainform.LS_ScenarioStart("22", "192.168.102.22", "m7102", "22");
        }


        string[] nodeName_list = new string[] { "1", "2", "3", "4", "4", "5", "6","5","7","7",
            "8","9","9","10","10","11","11","12","13","14","15"};
        public string findNode(string strrobotid)
        {
            int robotX = (int)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.x;
            int robotY = (int)Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.msg.pose.y;

            Point pt = new Point(robotX, robotY);

            for (int i = 0; i < mainform.node_area_.Count(); i++)
            {
                if (mainform.node_area_[i].Contains(pt))
                {
                    return nodeName_list[i];
                }
            }
            return "NULL";

            //WorkFlowGoal temp_action = new WorkFlowGoal();
            //string missionname = "";
            //foreach (KeyValuePair<string, Node_mission> mission in Data.Instance.node_mission_list)
            //{
            //    temp_action = JsonConvert.DeserializeObject<WorkFlowGoal>(mission.Value.work);

            //    double mintemp = 0;
            //    double nodeX = temp_action.work[0].action_args[0];
            //    double nodeY = temp_action.work[0].action_args[1];
            //    mintemp = onPointToPointDist(robotX, robotY, nodeX, nodeY);
            //    string strTemp = Data.Instance.node_mission_list[mission.Key].mission_name;
            //    if (min > mintemp)
            //    {
            //        min = mintemp;
            //        startNode = strTemp;
            //        string temp = Regex.Replace(strTemp,@"\D","");
            //        s = int.Parse(temp);
            //        textBox1.Text = temp;
            //    }

            //}

            //Console.WriteLine("가장 가까운 노드는 : {0}, 거리 :{1}", missionname, min);
        }
        private double onPointToPointDist(double x1, double y1, double x2, double y2)
        {
            double hypo = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return hypo;
        }
    }


}
