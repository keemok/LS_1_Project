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


namespace Syscon_Solution.MappingManager
{
    public partial class MappingMain : Form
    {
        
        public FleetManager.Comm.Comm_bridge commBridge;
        public FleetManager.DB.DB_bridge dbBridge = new FleetManager.DB.DB_bridge();

        Mapping_Ctrl mappingctrl;

        public ingdlg ingdlg = new ingdlg();
        Thread ServerConnect_Checkthread;

        public MappingMain()
        {
            InitializeComponent();
        }
        


        private void MappingMain_Load(object sender, EventArgs e)
        {
#if _mapping
            Data.Instance.MAINFORM = this;

            commBridge = new FleetManager.Comm.Comm_bridge();
#endif

            dbBridge.onDBConnectionPath_OpenCheck();

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

            onRobotListCheck();

            //mappingctrl = new Mapping_Ctrl(this);

            panel2.Controls.Add(mappingctrl);

        }

        private void onRobotListCheck()
        {
            dbBridge.onDBRead_Robotlist("all");
            //   onDBRead_RobotStatus();
            onRobots_WorkInfo_InitSet();
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
                cnt = Data.Instance.Robot_RegInfo_list.Count;
                if (cnt > 0)
                {
                    foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                    {
                        string strrobotid = info.Key;
                        Robot_RegInfo value = info.Value;

                        Data.Instance.Robot_work_info.Add(strrobotid, commBridge.onNewRobotWorkInfo_initial(strrobotid, "", 1, 0, "", ""));
                    }
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
                    //worker.onDeleteAllSubscribe_Compulsion();
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
                        ServerConnect_Checkthread.Start();


                        if (Data.Instance.isConnected == true)
                        {

                            btnConnect.Enabled = true;
                            btnConnect.Text = "disconnect";


                            onSuscribe_RobotsStatus_Basic();

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
                    ROSDisconnect();
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

                Invoke(new MethodInvoker(delegate ()
                {
                    btnConnect.Text = "connect";
                    btnConnect.Enabled = true;
                }));
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        private void onConnectCheck()
        {
            try
            {
                for (; ; )
                {
                    if (Data.Instance.bFormClose) break;

                    if (!Data.Instance.isConnected) break;

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

        #endregion


        private void onSuscribe_RobotsStatus_Basic()
        {
            try
            {

              
                if (Data.Instance.isConnected)
                {
                    int cnt = 0;
                    cnt = Data.Instance.Robot_RegInfo_list.Count;
                    if (cnt > 0)
                    {
                        foreach (KeyValuePair<string, Robot_RegInfo> info in Data.Instance.Robot_RegInfo_list)
                        {
                            string strrobotid = info.Key;
                            Robot_RegInfo value = info.Value;

                            commBridge.onSelectRobotStatus_Basic_subscribes(strrobotid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSuscribe_RobotsStatus_Basic err :={0}", ex.Message.ToString());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strAddr = txtAddr.Text.ToString();

            btnConnect.Enabled = false;
            ROSConnection(strAddr);
        }

        public void MapInfoComplete(string strrobotid)
        {
            try
            {
                mappingctrl.MapInfoComplete(strrobotid);
                
               // TopicList list = new TopicList();
               // commBridge.onDeleteSelectSubscribe(strrobotid + list.topic_staticMap);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        public void LocalCostInfoComplete(string strrobotid)
        {
            try
            {
               
                    mappingctrl.LocalCostInfoComplete(strrobotid);
              

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("LocalCostInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        private void MappingMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Data.Instance.G_SqlCon != null)
            {
                if (Data.Instance.G_SqlCon.State == ConnectionState.Open)
                {
                    Data.Instance.G_SqlCon.Close();
                }
            }

            if (Data.Instance.G_DynaSqlCon != null)
            {
                if (Data.Instance.G_DynaSqlCon.State == ConnectionState.Open)
                {
                    Data.Instance.G_DynaSqlCon.Close();
                }
            }

            if (ServerConnect_Checkthread != null)
            {
                ServerConnect_Checkthread.Abort();
                ServerConnect_Checkthread = null;
            }

            if(mappingctrl.manualRun_thread!=null)
            {
                mappingctrl.bThreadBreak = true;
                mappingctrl.manualRun_thread.Abort();
                mappingctrl.manualRun_thread = null;
            }
        }
    }
}
