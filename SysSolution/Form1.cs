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



namespace SysSolution
{
    public partial class Form1 : Form
    {
        Worker worker;
        Worker worker2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if _commtest
            Data.Instance.MAINFORM = this;
#endif

            worker = new Worker(this,1);
       
            //Worklist 읽기
            //robot ip 읽기
        
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString(); 
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
                    Console.WriteLine(ex.Message.ToString());
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
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
                           
                            btnConnect.Text = "disconnect";
                        }
                    }
                    else
                    {
                        ROSDisconnect();
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
                Console.WriteLine(ex.Message.ToString());
            }
        }
        #endregion

        private async void btnPublish_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            for (int i = 0; i < 50; i++)
            {
                var task = Task.Run(() => worker.onPublshTest(this, float.Parse(textBox7.Text.ToString()),i));
                await task;
            }
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            worker.onSubscribeTest();
           
        }

        private void btnDelSubscribe_Click(object sender, EventArgs e)
        {
            worker.onDeleteAllSubscribe();
        }

        private void btnDelSelectSubscribe_Click(object sender, EventArgs e)
        {
            //string topic = "";
            //  topic = txtDeleteTopic.Text.ToString();

            //  worker.onDeleteSelectSubscribe(topic);

            groupBox1.Invalidate();
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            worker.onServiceTest(this);
        }


        public void onListmsg(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox1.Items.Add(msg);
               // listBox1.SelectedIndex = listBox1.Items.Count - 1;

               if(listBox1.Items.Count> 1000)
                    listBox1.Items.Clear();
            }));

        }

        public void onListPublish(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox2.Items.Add(msg);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
            }));

        }

        public void onListService(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBox3.Items.Add(msg);
                listBox3.SelectedIndex = listBox3.Items.Count - 1;
            }));

        }

      public void updateDP(string strtopic, string msg, string strcnt)
        {
            groupBox1.Invalidate();
            onListmsg(string.Format("topic={0}..data={1}",strtopic,msg));
            Invoke(new MethodInvoker(delegate ()
            {
                txtRecvcnt.Text = strcnt;
            }));
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                t1.Text = Data.Instance.t[0];
                t2.Text = Data.Instance.t[1];
                t3.Text = Data.Instance.t[2];
                t4.Text = Data.Instance.t[3];
                t5.Text = Data.Instance.t[4];
                t6.Text = Data.Instance.t[5];
                t7.Text = Data.Instance.t[6];
                t8.Text = Data.Instance.t[7];
                t9.Text = Data.Instance.t[8];
                t10.Text = Data.Instance.t[9];

                t11.Text = Data.Instance.t[10];
                t12.Text = Data.Instance.t[11];
                t13.Text = Data.Instance.t[12];
                t14.Text = Data.Instance.t[13];
                t15.Text = Data.Instance.t[14];
                t16.Text = Data.Instance.t[15];
                t17.Text = Data.Instance.t[16];
                t18.Text = Data.Instance.t[17];
                t19.Text = Data.Instance.t[18];
                t20.Text = Data.Instance.t[19];

                t21.Text = Data.Instance.t[20];
                t22.Text = Data.Instance.t[21];
                t23.Text = Data.Instance.t[22];
                t24.Text = Data.Instance.t[23];
                t25.Text = Data.Instance.t[24];
                t26.Text = Data.Instance.t[25];
                t27.Text = Data.Instance.t[26];
                t28.Text = Data.Instance.t[27];
                t29.Text = Data.Instance.t[28];
                t30.Text = Data.Instance.t[29];

                t31.Text = Data.Instance.t[30];
                t32.Text = Data.Instance.t[31];
                t33.Text = Data.Instance.t[32];
                t34.Text = Data.Instance.t[33];
                t35.Text = Data.Instance.t[34];
                t36.Text = Data.Instance.t[35];
                t37.Text = Data.Instance.t[36];
                t38.Text = Data.Instance.t[37];
                t39.Text = Data.Instance.t[38];
                t40.Text = Data.Instance.t[39];

                t41.Text = Data.Instance.t[40];
                t42.Text = Data.Instance.t[41];
                t43.Text = Data.Instance.t[42];
                t44.Text = Data.Instance.t[43];
                t45.Text = Data.Instance.t[44];
                t46.Text = Data.Instance.t[45];
                t47.Text = Data.Instance.t[46];
                t48.Text = Data.Instance.t[47];
                t49.Text = Data.Instance.t[48];
                t50.Text = Data.Instance.t[49];
            }));
            // MessageBox.Show("ee");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* XML xml = new XML();
             TreeView tvActlist = new TreeView();
             string startPath = Application.StartupPath;
             string path = string.Empty;
             path = startPath + @"\Worklist.xml";
             xml.SaveActionToXML(path, tvActlist);
         */

            string startPath = Application.StartupPath;
            string path = string.Empty;
            path = startPath + @"\test.txt";

            string[] strbuf = File.ReadAllLines(path);
           for(int i=0; i<strbuf.Length; i++)
            {

            }
          

        }


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if (saveFile.ShowDialog() != DialogResult.OK) return;

            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.SerializeTreeView(this.treeView1, saveFile.FileName);
        }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if (openFile.ShowDialog() != DialogResult.OK) return;
            this.treeView1.Nodes.Clear();
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.DeserializeTreeView(this.treeView1, openFile.FileName);


        }

     
      
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if (openFile.ShowDialog() != DialogResult.OK) return;

            this.treeView1.Nodes.Clear();
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.LoadXmlFileInTreeView(this.treeView1, openFile.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string keyName = "WID" + DateTime.Now.ToString("hhMMss");
            //TreeNode tn = tvActlist.SelectedNode;
            treeView1.Nodes[0].Nodes.Add(new TreeNode(keyName));
        }

        private void button4_Click(object sender, EventArgs e)
        {
           listBox1.Items.Clear();

           worker.onRobotPosition_subscribe("R_002");
           worker.onMotorState_subscribe("R_002");
           worker.onUltraSonicRaw_subscribe("R_002");
           worker.onLidarScan_subscribe("R_002");


           worker.onWorkFeedback_subscribe("R_002");
           worker.onWorkResult_subscribe("R_002");
         
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if (saveFile.ShowDialog() != DialogResult.OK) return;

            //TreeViewSerializer serializer = new TreeViewSerializer();
            //serializer.SerializeTreeView(this.treeView1, saveFile.FileName);

            ExportToXml(this.treeView1, saveFile.FileName);
        }
        private StreamWriter sr;
        public void ExportToXml(TreeView tv, string filename)
        {
            sr = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            //Write the header
            sr.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            //Write our root node
            //sr.WriteLine("<" + tv.Nodes[0].Text + ">");	
            string strrootnode = tv.Nodes[0].Text;
            //strrootnode.Replace("<", "'");
            //strrootnode.Replace(">", "'");
            //sr.WriteLine("<" + strrootnode + ">");
            foreach (TreeNode node in tv.Nodes)
            {
                string strsubnode = node.Text;
                sr.WriteLine("<" + strsubnode + ">");
                saveNode(node.Nodes);
                sr.WriteLine("</" + strsubnode + ">");
            }

            //Close the root node	
            //sr.WriteLine("</" + strrootnode + ">");
            sr.Close();
        }

        public void saveNode(TreeNodeCollection tnc)
        {
            foreach (TreeNode node in tnc)
            {
                //If we have child nodes, we'll write 
                //a parent node, then iterrate through
                //the children

                if (node.Nodes.Count > 0)
                {
                    sr.WriteLine("<" + node.Text + ">");
                    saveNode(node.Nodes);
                    sr.WriteLine("</" + node.Text + ">");
                }
                else //No child nodes, so we just write the text
                {
                    sr.WriteLine(node.Text + "\n");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {


            Action act = new Action();
            List<ParameterSet> listparam = new List<ParameterSet>();
            ParameterSet param;

            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Lift-Conveyor-Control/mode:Top_Bottom/action:Buttom"

            string strgoal = "type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3";
            string[] strgoal_sub = strgoal.Split('/');
            try
            {
                if (strgoal_sub.Length > 0)
                {
                    if (strgoal_sub[0].IndexOf(':') > 0)
                    {
                        if (strgoal_sub[0].Split(':')[1].Equals("Goal-Point"))
                        {
                            float x, y, th;
                            act.action_type = (int)Data.ACTION_TYPE.Goal_Point;
                            string[] strgoal_sub_act_param = strgoal_sub[1].Split(':');
                            x = float.Parse(strgoal_sub_act_param[1]);

                            strgoal_sub_act_param = strgoal_sub[2].Split(':');
                            y = float.Parse(strgoal_sub_act_param[1]);

                            strgoal_sub_act_param = strgoal_sub[3].Split(':');
                            th = float.Parse(strgoal_sub_act_param[1]);

                            act.action_args.Add(x);
                            act.action_args.Add(y);
                            act.action_args.Add(th);

                            for (int i = 5; i < strgoal_sub.Length; i++)
                            {
                                param = new ParameterSet();
                                string[] strgoal_sub_params = strgoal_sub[i].Split(':');
                                param.param_name = strgoal_sub_params[0];
                                param.value = strgoal_sub_params[1];

                                listparam.Add(param);
                            }
                            act.action_params = listparam;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
