using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class plctestform : Form
    {
        public plctestform()
        {
            InitializeComponent();
        }
        List<LS_Socket> ABN100_1;
        List<LS_Socket> ABN100_2;
        List<LS_Socket> ABN100_3;
        List<LS_Socket> ABN100_4;
        Dictionary<string/*PLCID*/, LS_Socket> PLC_Socket_Info = new Dictionary<string/*PLCID*/, LS_Socket>();
        Dictionary<string/*PLCID*/, int[,]> PLCData = new Dictionary<string/*PLCID*/, int[,]>();
        List<string> list_id = new List<string>();


        private void checkCondition(string requiredID, string atcNo)
        {
            LS_Socket tmp = null;
            try
            {
                for (int i = 0; i < list_id.Count; i++)
                {
                    tmp = PLC_Socket_Info[list_id[i]].getSocketFromATCNo("",atcNo);  //그룹 명 추가하도록 되었습니다.
                    if (tmp != null)
                    {
                        tmp.checkCondition(requiredID, atcNo);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }
        string[] ATCNo;
        private void PLC_Connect()
        {
            ABN100_1 = new List<LS_Socket>();
            ABN100_2 = new List<LS_Socket>();
            ABN100_3 = new List<LS_Socket>();
            ABN100_4 = new List<LS_Socket>();

            //string[] ATCNo = { "0", "0", "0" };
            // ABN100#1     ip : 11~13, 15~22  
            {
                ATCNo = new string[] { "12" };
                tryConnectABNc_Nomal("192.168.102.11", "192.168.102.11", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "14" };
                tryConnectABNc_Nomal("192.168.102.12", "192.168.102.12", "ABN100_1", ATCNo, 1);               
                ATCNo = new string[] { "11","13" };
                tryConnectABNc_Nomal("192.168.102.13", "192.168.102.13", "ABN100_1", ATCNo, 1);                
                ATCNo = new string[] { "14"};
                tryConnectABNc_Nomal("192.168.102.15", "192.168.102.15", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "1" };
                tryConnectABNc_Nomal("192.168.102.16", "192.168.102.16", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "15" };
                tryConnectABNc_Nomal("192.168.102.17", "192.168.102.17", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "16" };
                tryConnectABNc_Nomal("192.168.102.18", "192.168.102.18", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "2" };
                tryConnectABNc_Nomal("192.168.102.19", "192.168.102.19", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.20", "192.168.102.20", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "34_1","34_2","35" };
                tryConnectABNc_Nomal("192.168.102.21", "192.168.102.21", "ABN100_1", ATCNo, 1);
                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.22", "192.168.102.22", "ABN100_1", ATCNo, 1);

                // ABN100#2    ip : 31~33  ,36, 37, 39~42
                ATCNo = new string[] { "18" };
                tryConnectABNc_Nomal("192.168.102.31", "192.168.102.31", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "4" };
                tryConnectABNc_Nomal("192.168.102.32", "192.168.102.32", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "17","19" };
                tryConnectABNc_Nomal("192.168.102.33", "192.168.102.33", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "5" };
                tryConnectABNc_Nomal("192.168.102.36", "192.168.102.36", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "20" };
                tryConnectABNc_Nomal("192.168.102.37", "192.168.102.37", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "6" };
                tryConnectABNc_Nomal("192.168.102.39", "192.168.102.39", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.40", "192.168.102.40", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "32_1","32_2","33" };
                tryConnectABNc_Nomal("192.168.102.41", "192.168.102.41", "ABN100_2", ATCNo, 2);
                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.42", "192.168.102.42", "ABN100_2", ATCNo, 2);

                // ABN100#3   ip : 51~53, 56~62

                ATCNo = new string[] { "22" };
                tryConnectABNc_Nomal("192.168.102.51", "192.168.102.51", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "8" };
                tryConnectABNc_Nomal("192.168.102.52", "192.168.102.52", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "21" };
                tryConnectABNc_Nomal("192.168.102.53", "192.168.102.53", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "7" };
                tryConnectABNc_Nomal("192.168.102.56", "192.168.102.56", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "23" };
                tryConnectABNc_Nomal("192.168.102.57", "192.168.102.57", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "24" };
                tryConnectABNc_Nomal("192.168.102.58", "192.168.102.58", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "9" };
                tryConnectABNc_Nomal("192.168.102.59", "192.168.102.59", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "10" };
                tryConnectABNc_Nomal("192.168.102.60", "192.168.102.60", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "32_5","32_6","33" };
                tryConnectABNc_Nomal("192.168.102.61", "192.168.102.61", "ABN100c_3", ATCNo, 3);
                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.62", "192.168.102.62", "ABN100c_3", ATCNo, 3);

                // ABN100#4    ip : 71~73  , 76~82
                ATCNo = new string[] { "26" };
                tryConnectABNc_Nomal("192.168.102.71", "192.168.102.71", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "4" };
                tryConnectABNc_Nomal("192.168.102.72", "192.168.102.72", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "25","27" };
                tryConnectABNc_Nomal("192.168.102.73", "192.168.102.73", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "5" };
                tryConnectABNc_Nomal("192.168.102.76", "192.168.102.76", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "28" };
                tryConnectABNc_Nomal("192.168.102.77", "192.168.102.77", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "29" };
                tryConnectABNc_Nomal("192.168.102.78", "192.168.102.78", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "6" };
                tryConnectABNc_Nomal("192.168.102.79", "192.168.102.79", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "3" };
                tryConnectABNc_Nomal("192.168.102.80", "192.168.102.80", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "34_3","34_4","35" };
                tryConnectABNc_Nomal("192.168.102.81", "192.168.102.81", "ABN100c_4", ATCNo, 4);
                ATCNo = new string[] { "30" };
                tryConnectABNc_Nomal("192.168.102.82", "192.168.102.82", "ABN100c_4", ATCNo, 4);

                // EBN100_1,2 , 부품창고, ABN100포장_1,2, 경보시스템                
                ATCNo = new string[] { "34_5","35","32_3","33" };
                tryConnectABNc_Nomal("192.168.102.91", "192.168.102.91", "EBN100c_1", ATCNo, 0);
                ATCNo = new string[] { "34_6","35","32_4","33" };
                tryConnectABNc_Nomal("192.168.102.92", "192.168.102.92", "EBN100c_2", ATCNo, 0);
                ATCNo = new string[] { "1", "5-1", "5-2", "2", "6-1", "6-2", "9", "4-1", "4-2", "8", "3-1", "3-2", "7", "10", "28", "29", "15", "16", "20", "23", "24", "25", "26", "27", "11", "12", "13", "14", "17", "18", "19", "21", "22", "30-1", "30-2" };
                tryConnectABNc_Nomal("192.168.102.93", "192.168.102.93", "부품창고", ATCNo, 0);
                ATCNo = new string[] { "33", "32_1", "32_2", "32_3", "32_4", "32_5", "32_6" };
                tryConnectABNc_Nomal("192.168.102.94", "192.168.102.94", "ABN100c포장라인", ATCNo, 0);
                ATCNo = new string[] { "34_1", "34-2", "34-3", "34-4", "34-5", "34-6", "35" };
                tryConnectABNc_Nomal("192.168.102.95", "192.168.102.95", "ABN100c포장라인_2", ATCNo, 0);
                ATCNo = new string[] { "없음" };
                tryConnectABNc_Nomal("192.168.102.96", "192.168.102.96", "경보시스템", ATCNo, 0);
            }
        }
        private void tryConnectABNc_Nomal(string ip, string id, string group, string[] ATC, int lineNo)
        {
            LS_Socket tmpSocket;
            tmpSocket = tryConnect(id, ip, group, ATC);
            switch (lineNo)
            {
                case 1:
                    if (ABN100_1.Contains(tmpSocket))
                    {
                        ABN100_1.Remove(tmpSocket);
                    }
                    ABN100_1.Add(tmpSocket);
                    break;
                case 2:
                    if (ABN100_2.Contains(tmpSocket))
                    {
                        ABN100_2.Remove(tmpSocket);
                    }
                    ABN100_2.Add(tmpSocket);
                    break;
                case 3:
                    if (ABN100_3.Contains(tmpSocket))
                    {
                        ABN100_3.Remove(tmpSocket);
                    }
                    ABN100_3.Add(tmpSocket);
                    break;
                case 4:
                    if (ABN100_4.Contains(tmpSocket))
                    {
                        ABN100_4.Remove(tmpSocket);
                    }
                    ABN100_4.Add(tmpSocket);
                    break;
                default:
                    break;
            }

        }
        private LS_Socket tryConnect(string id, string ip, string group, string[] m_ATCNo)
        {
            //id = ip; // id를 ip 로 사용

            LS_Socket lssocket = new LS_Socket(id, ip, 2004, group, m_ATCNo);
            lssocket.onLS_SocketInit();
            lssocket.lsconnect_Evt += new LS_Socket.LS_Connect(this.LS_Connect);
            lssocket.lsrev_Evt += new LS_Socket.LS_Response(this.LS_Response);
            lssocket.scenarioCall_Evt += new LS_Socket.LS_ScenarioCall(this.LS_ScenarioCall);
            lssocket.scenarioStart_Evt += new LS_Socket.LS_ScenarioStart(this.LS_ScenarioStart);

            if (PLC_Socket_Info.ContainsKey(id))
            {
                PLC_Socket_Info[id].Disconnect();
                PLC_Socket_Info.Remove(id);
            }

            PLC_Socket_Info.Add(id, lssocket);
            //addLineComboBox(id);
            list_id.Add(id);
            return lssocket;
        }

        public void LS_ScenarioStart(string requiredID, string startID, string mission, string atcNo)
        {
            List<string> list = new List<string>(Data.Instance.Enable_Robot.Keys);
            requiredid = requiredID;
            startid = startID;
            string robotid = "";


            Console.WriteLine("Required ID :{0} , startid : {1},mission : {2}, actno : {3}", requiredID, startID, mission, atcNo);
            //atcNo -> data type : string...eg) 30-1,24,22,7...
            //requiredID -> data type : string...eg) 192.168.102.11...
            //startID -> data type : string...eg) 192.168.10.93(부품창고)
            //mission -> data type : string...eg) 부품창고면 m7102, description : plc address


            
        }


        public class Reservation_work_list
        {
            public string requiredID;
            public string startID;
            public string mission;
            public string atcnumber;
        }
        public class Reservation_work
        {
            public List<Reservation_work_list> Reservation_list;
        }
        List<Reservation_work_list> reservation_list = new List<Reservation_work_list>();
       
        private void robotflagTimer_Tick(object sender, EventArgs e)
        {
            check_Lastholding("R_001", 0);
            check_Lastholding("R_002", 1);
            check_Lastholding("R_003", 2);
            check_Lastholding("R_004", 3);
            check_Lastholding("R_005", 4);
            check_Lastholding("R_006", 5);
            check_Lastholding("R_007", 6);
        }
        private void check_Lastholding(string strrobotid, int idx)
        {
            float temp;
            int index = 0;
            try
            {
                float temp1 = Data.Instance.Robot_work_info[strrobotid].robot_status_info.controllerstate.msg.data[0];
                temp = temp1;
                if (temp == temp1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                if (index == 5)
                {
                    Data.Instance.robotFlag[strrobotid] = false;
                }
                else
                {
                    Data.Instance.robotFlag[strrobotid] = true;
                }
            }
            catch
            {
                Console.WriteLine("error");
            }
        }



     

       
        public void LS_Connect(string id, byte data)
        {
            try
            {
                if (data == 0x01) // connect success
                {
                    int[,] plc = new int[8, 10];
                    if (PLCData.ContainsKey(id))
                    {
                        PLCData.Remove(id);
                    }
                    PLCData.Add(id, plc);
                }
                else if (data == 0x02) //delete id
                {
                    PLCData.Remove(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LS_Connect err ..{0}", ex.Message.ToString());
            }
        }
        public void LS_Response(string strPLCID, string strData, int[,] plc_data)
        {
            try
            {
                if (PLCData.ContainsKey(strPLCID))
                {
                    for (int i = 0; i < plc_data.GetLength(0); i++)
                    {
                        for (int j = 0; j < plc_data.GetLength(1); j++)
                        {
                            PLCData[strPLCID][i, j] = plc_data[i, j];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("LS_Response err..{0}", ex.Message.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string[] id = { "192.168.0.21", "192.168.0.150" };
            foreach (string pid in list_id)
            {

                if (PLC_Socket_Info.ContainsKey(pid))
                {
                    if (PLC_Socket_Info[pid].bConnected)
                    {
                        //Console.WriteLine("timer_Tick " + pid + " data recv request");
                        //Checkthedata();
                        PLC_Socket_Info[pid].onXGT_ContinuousByteDataRead(6);
                    }
                    //else
                    //{
                    //    timer1.Enabled = false;
                    //}
                }
            }
            //for (int i = 0; i < id.Count(); i++)
            //{
            //    if (PLC_Socket_Info.ContainsKey(id[i]))
            //    {
            //        if (PLC_Socket_Info[id[i]].bConnected)
            //        {
            //            //Checkthedata();
            //            PLC_Socket_Info[id[i]].onXGT_ContinuousByteDataRead(6);

            //        }
            //        else
            //        {
            //            timer1.Enabled = false;
            //        }
            //    }
            //}
        }
        /*
        private void Checkthedata()
        {

            foreach (KeyValuePair<string, LS_Socket> info in PLC_Socket_Info)
            {
                if (info.Value.GetSTATE() == 2)
                {
                    //Console.WriteLine("로봇 태스크 전달");
                    checkCondition("192.168.0.21", "12");
                    //string[] temp = info.Value.ATCNo[0].Split(',');
                    //for(int i=0;i<temp.Count();i++)
                    //{

                    //}
                    
                    //로봇 태스크 전달
                    if ((PLC_Socket_Info["192.168.0.21"].GetSTATE() == 3))
                    {
                        Console.WriteLine("시나리오 스타트--------------------");
                    }



                    //mission complete
                    if (PLC_Socket_Info["192.168.0.21"].m7102 == true)
                    {
                        //공박스 수거
                    }
                    else
                    {
                        // 복귀
                    }
                }
            }
            }
            */

       

        private void dataCheck_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PLC_Socket_Info[requiredid].SetSTATE(0);
                PLC_Socket_Info[startid].SetSTATE(0);

            }
            catch
            {

            }
        }



        // trigger on   when required a scenario start request
        public void LS_ScenarioCall(string id, string atcNo, string scen)
        {

            if (PLC_Socket_Info.ContainsKey(id))
            {
                Console.WriteLine("시나리오 요청 " + id + "  /  " + atcNo);
                checkCondition(id, atcNo);
                if (PLC_Socket_Info[id].GetSTATE() == 0) // IDEL
                {
                    Console.WriteLine("plc state : IDLE");
                    PLC_Socket_Info[id].SetSTATE(2);
                }
                if (PLC_Socket_Info[id].GetSTATE() == 2) //required
                {
                    Console.WriteLine("plc state : REQUIRED");
                    checkCondition(id, atcNo);

                }
            }
        }
        string requiredid, startid;

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            foreach (string pid in list_id)
            {

                if (PLC_Socket_Info.ContainsKey(pid))
                {
                    if (PLC_Socket_Info[pid].bConnected)
                    {
                        PLC_Socket_Info[pid].onXGT_ContinuousByteDataRead(6);
                    }
                }
            }
        }

       
        private void plctestform_Load(object sender, EventArgs e)
        {
            PLC_Connect();
            timer1.Interval = 100;
            timer1.Start();
        }
    }
    }

