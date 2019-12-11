using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;

using System.Net.WebSockets;
using System.Threading;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

using Syscon_Solution;

using System.Net.Sockets;
using System.Net;

using System.Globalization;
using System.Runtime.InteropServices;
using Syscon_Solution.LSprogram.Structure;
using Syscon_Solution.LSprogram;

namespace Syscon_Solution
{
    class LS_Socket
    {   
        public enum state { IDLE, RUNNING, REQUIRE, SCENARIO, RSERVATION };
        public state STATE = state.IDLE;
        public state STATE_PRE = state.IDLE;
        public const string GROUP_ABN100_1 = "ABN100_1";
        public const string GROUP_ABN100_2 = "ABN100_2";
        public const string GROUP_ABN100_3 = "ABN100c_3";
        public const string GROUP_ABN100_4 = "ABN100c_4";
        public const string GROUP_EBN100c_1 = "EBN100c_1";
        public const string GROUP_EBN100c_2 = "EBN100c_2";
        public const string GROUP_PARTSWAREHOUSE = "부품창고";
        public const string GROUP_ABN100PACK_1 = "ABN100c포장라인";
        public const string GROUP_ABN100PACK_2 = "ABN100c포장라인_2";
        public const string GROUP_ALARM = "경보시스템";



        public bool m7000 = false;
        public bool m7001 = false;
        public bool m7002 = false;
        public bool m7003 = false;
        public bool m7004 = false;
        public bool m7005 = false;
        public bool m7100 = false;
        public bool m7101 = false;
        public bool m7102 = false;
        public bool m7103 = false;
        public bool m7104 = false;
        public bool m7105 = false;
        public bool m7106 = false;
        public bool m7107 = false;
        public bool m7108 = false;
        public bool m7109 = false;
        public bool m7110 = false;
        public bool m7111 = false;
        public bool m7112 = false;
        public bool m7113 = false;
        public bool m7114 = false;
        public bool m7115 = false;
        public bool m7116 = false;
        public bool m7117 = false;
        public bool m7118 = false;
        public bool m7119 = false;
        public bool m7120 = false;
        public bool m7121 = false;
        public bool m7122 = false;
        public bool m7123 = false;
        public bool m7124 = false;
        public bool m7125 = false;
        public bool m7126 = false;
        public bool m7127 = false;
        public bool m7128 = false;
        public bool m7129 = false;
        public bool m7130 = false;
        public bool m7131 = false;
        public bool m7132 = false;
        public bool m7133 = false;
        public bool m7134 = false;
        public bool m7135 = false;
        public bool m7140 = false;

        public bool bConnected = false;
        private Socket clientsock = null;
        private Socket cbSock;
        private byte[] recvBuffer;

        private const int MAXSIZE = 4096;
        private string HOST = "192.168.55.163";
        private int PORT = 9800;
        public string GROUP { get; }
        public string[] ATCNo;

        public string strSocketid = "";

        Thread ServerConnect_Checkthread;

        private byte[] XIS_recvBuffer = new byte[1024];


        Dictionary<string/*ATC NO*/, string/* address name */> addrFromATC = new Dictionary<string, string>();
        public Dictionary<string/*ATCNo*/, bool/*isIdle*/> ATCNoState = new Dictionary<string, bool>();

        delegate void ctrl_Invoke(TextBox ctrl, string messgae, string Netmessage);


        public event LS_SocketResponse lssocketrev_Evt;
        public delegate void LS_SocketResponse(byte[] recvbuff);

        public event LS_Response lsrev_Evt;
        public delegate void LS_Response(string strPLCID, string strData, int[,] plc_data);

        public event LS_Connect lsconnect_Evt;
        public delegate void LS_Connect(string id, byte data);

        public event LS_ScenarioCall scenarioCall_Evt;
        public delegate void LS_ScenarioCall(string id, string atcNo, string addr);

        public event LS_ScenarioStart scenarioStart_Evt;
        public delegate void LS_ScenarioStart(string requiedID, string startID, string mission, string atcNo);

        public event LS_ATCNo_state ATCState_Evt;
        public delegate void LS_ATCNo_state(string id, string atcNo, bool value);

        

        int[,] recv_plcData = new int[8, 8];

        private bool _isDisconnect = false;

        public LS_Socket()
        {
        }

        mainForm form;

        public LS_Socket(string id, string strhost, int nport, string group, string[] m_ATCNo)
        {
            form = mainForm.getForm();            
            HOST = id;
            PORT = nport;
            GROUP = group;
            ATCNo = m_ATCNo;
            STATE = state.IDLE;
            STATE_PRE = state.IDLE;

            Console.Write("LS_Socket create. Host = " + HOST + " / Group = " + GROUP + " ATCNo = ");
            foreach (string atc in ATCNo)
            {
                Console.Write(atc + ", ");
            }
            Console.WriteLine("");

            if (GROUP == GROUP_PARTSWAREHOUSE)
            {
                addrFromATC.Add("1", "m7101");
                addrFromATC.Add("5_1", "m7102");
                addrFromATC.Add("5_2", "m7103");
                addrFromATC.Add("2", "m7104");
                addrFromATC.Add("6_1", "m7105");
                addrFromATC.Add("6_2", "m7106");
                addrFromATC.Add("9", "m7107");
                addrFromATC.Add("4_1", "m7108");
                addrFromATC.Add("4_2", "m7109");
                addrFromATC.Add("8", "m7110");
                addrFromATC.Add("3_1", "m7111");
                addrFromATC.Add("3_2", "m7112");
                addrFromATC.Add("7", "m7113");
                addrFromATC.Add("10", "m7114");
                addrFromATC.Add("28", "m7115");
                addrFromATC.Add("29", "m7116");
                addrFromATC.Add("15", "m7117");
                addrFromATC.Add("16", "m7118");
                addrFromATC.Add("20", "m7119");
                addrFromATC.Add("23", "m7120");
                addrFromATC.Add("24", "m7121");
                addrFromATC.Add("25", "m7122");
                addrFromATC.Add("26", "m7123");
                addrFromATC.Add("27", "m7124");
                addrFromATC.Add("11", "m7125");
                addrFromATC.Add("12", "m7126");
                addrFromATC.Add("13", "m7127");
                addrFromATC.Add("14", "m7128");
                addrFromATC.Add("17", "m7129");
                addrFromATC.Add("18", "m7130");
                addrFromATC.Add("19", "m7131");
                addrFromATC.Add("21", "m7132");
                addrFromATC.Add("22", "m7133");
                addrFromATC.Add("30_1", "m7134");
                addrFromATC.Add("30_2", "m7135");

            }
            if (GROUP == GROUP_ABN100PACK_1)
            {
                addrFromATC.Add("33", "m7101");
                addrFromATC.Add("32_1", "m7102");
                addrFromATC.Add("32_2", "m7103");
                addrFromATC.Add("32_3", "m7104");
                addrFromATC.Add("32_4", "m7105");
                addrFromATC.Add("32_5", "m7106");
                addrFromATC.Add("32_6", "m7107");
            }
            if (GROUP == GROUP_ABN100PACK_2)
            {
                addrFromATC.Add("34_1", "m7101");
                addrFromATC.Add("34_2", "m7102");
                addrFromATC.Add("34_3", "m7103");
                addrFromATC.Add("34_4", "m7104");
                addrFromATC.Add("34_5", "m7105");
                addrFromATC.Add("34_6", "m7106");
                addrFromATC.Add("35", "m7107");
            }

        }
        public string GetHost()
        {
            return HOST;
        }
        /// <summary>
        /// IDEL = 0 , RUNNING = 1, REQUIRE = 2, SCENARIO = 3, RESERVATION = 4
        /// </summary>
        /// <returns></returns>
        public int GetSTATE()
        {
            return (int)STATE;
        }
        public int GetSTATEPre()
        {   
            return (int)STATE_PRE;
        }


        /// <summary>
        /// IDEL = 0 , RUNNING = 1, REQUIRE = 2, SCENARIO = 3, RESERVATION = 4
        /// </summary>
        /// <param name="m_state"></param>
        public void SetSTATE(int m_state)
        {
            STATE_PRE = STATE;
            STATE = (state)m_state;

        }
        public void SetSTATEPre(int state)
        {
            STATE_PRE = (state)state;
        }

        public void onLS_SocketInit()
        {

            recvBuffer = new byte[MAXSIZE];
            DoConnect();

            this.lssocketrev_Evt += new LS_SocketResponse(this.LS_SocketRecv);
        }
        public void onLS_SocketInit2()
        {

            recvBuffer = new byte[MAXSIZE];
            DoConnect();

            this.lssocketrev_Evt += new LS_SocketResponse(this.LS_SocketRecv);

            try
            {

                bConnected = true;
                //asdf
                Console.WriteLine("어쨌든 서버 접속 성공..");
                lsconnect_Evt(HOST, 0x01);


                if (ServerConnect_Checkthread != null)
                {
                    ServerConnect_Checkthread.Abort();
                    ServerConnect_Checkthread = null;
                }
                ServerConnect_Checkthread = new Thread(onConnectCheck);
                ServerConnect_Checkthread.Start();
                {
                    m7100 = Convert.ToBoolean(1);
                    m7101 = Convert.ToBoolean(1);
                    m7102 = Convert.ToBoolean(0);
                    m7103 = Convert.ToBoolean(0);
                    m7104 = Convert.ToBoolean(0);
                    m7105 = Convert.ToBoolean(0);
                    m7106 = Convert.ToBoolean(0);
                    m7107 = Convert.ToBoolean(0);
                    m7108 = Convert.ToBoolean(0);
                    m7109 = Convert.ToBoolean(0);
                    m7110 = Convert.ToBoolean(0);
                    m7111 = Convert.ToBoolean(0);
                    m7112 = Convert.ToBoolean(0);
                    m7113 = Convert.ToBoolean(0);
                    m7114 = Convert.ToBoolean(0);
                    m7115 = Convert.ToBoolean(0);
                    m7116 = Convert.ToBoolean(0);
                    m7117 = Convert.ToBoolean(0);
                    m7118 = Convert.ToBoolean(0);
                    m7119 = Convert.ToBoolean(0);
                    m7120 = Convert.ToBoolean(0);
                    m7121 = Convert.ToBoolean(0);
                    m7122 = Convert.ToBoolean(0);
                    m7123 = Convert.ToBoolean(0);
                    m7124 = Convert.ToBoolean(0);
                    m7125 = Convert.ToBoolean(0);
                    m7126 = Convert.ToBoolean(1);
                    m7127 = Convert.ToBoolean(0);
                    m7128 = Convert.ToBoolean(0);
                    m7129 = Convert.ToBoolean(0);
                    m7130 = Convert.ToBoolean(0);
                    m7131 = Convert.ToBoolean(0);
                    m7132 = Convert.ToBoolean(0);
                    m7133 = Convert.ToBoolean(0);
                    m7134 = Convert.ToBoolean(0);
                    m7135 = Convert.ToBoolean(0);
                    m7140 = Convert.ToBoolean(0);

                }

            }
            catch (SocketException se)
            {
                if (se.SocketErrorCode == SocketError.NotConnected)
                {
                    Console.WriteLine("서버 접속 실패..callback{0} {1}", se.NativeErrorCode, HOST);
                    DoConnect();
                }
            }


        }


        public void DoConnect()
        {
            clientsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            BeginConnect();
        }

        int nReadByte_Count = 0;
        public void onXGT_ContinuousByteDataRead(int bytecnt)
        {
            if (_isDisconnect)
            {
                return;
            }
            nReadByte_Count = bytecnt;

            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';


            header.Reserved = 0;
            header.PLCinfo = 0;

            header.CPUinfo = 0xa0;
            header.SourceofFrame = 0x33;
            header.InvokeID = 0x0000;

            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_Continuous_ByteData instruction_data = new GlobalVar.STInstruction_Continuous_ByteData();
            instruction_data.variable = new byte[7];

            instruction_data.cmd = 0x0054;
            instruction_data.Datatype = 0x00014; //0x0001;//
            instruction_data.Reserved2 = 0x0000;
            instruction_data.blockcnt = 0x0001;
            instruction_data.variable_length = 0x0007;

            //m7100 => m710.0 => 710*2 + 0bit //byte로 변환

            instruction_data.variable[0] = (byte)'%';
            instruction_data.variable[1] = (byte)'M';
            instruction_data.variable[2] = (byte)'B';
            instruction_data.variable[3] = (byte)'1';
            instruction_data.variable[4] = (byte)'4';
            instruction_data.variable[5] = (byte)'2';
            instruction_data.variable[6] = (byte)'0';

            instruction_data.Data_count = (ushort)nReadByte_Count;

            header.Length = (ushort)(Marshal.SizeOf(instruction_data));


            byte[] pbuffer = new byte[256];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_data, (IntPtr)fixed_buffer, false);
                }
            }

            length += (ushort)(Marshal.SizeOf(instruction_data));

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }

        }

        public void onXGT_ContinuousDataRead()
        {
            if (_isDisconnect)
            {
                return;
            }
            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';


            header.Reserved = 0;
            header.PLCinfo = 0;

            header.CPUinfo = 0xa0;
            header.SourceofFrame = 0x33;
            header.InvokeID = 0x0000;

            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_Read_header instruction_header = new GlobalVar.STInstruction_Read_header();
            //instruction_data.variable = new byte[7];

            instruction_header.cmd = 0x0054;
            instruction_header.Datatype = 0x0000;
            instruction_header.Reserved2 = 0x0000;
            instruction_header.blockcnt = 0x0004;

            header.Length = (ushort)(Marshal.SizeOf(instruction_header));

            byte[] variable_buffer = new byte[1024];
            int variable_length = 0;

            for (int i = 0; i < 4; i++)
            {
                GlobalVar.STInstruction_Read_Variable instruction_variable = new GlobalVar.STInstruction_Read_Variable();

                instruction_variable.variable_length = 0x0008;
                instruction_variable.variable = new byte[8];

                instruction_variable.variable[0] = (byte)'%';
                instruction_variable.variable[1] = (byte)'M';
                instruction_variable.variable[2] = (byte)'X';
                instruction_variable.variable[3] = (byte)'1';
                instruction_variable.variable[4] = (byte)'1';
                instruction_variable.variable[5] = (byte)'3';
                instruction_variable.variable[6] = (byte)'6';
                instruction_variable.variable[7] = (byte)((byte)'0' + (byte)i);

                unsafe
                {
                    fixed (byte* fixed_buffer = &variable_buffer[variable_length])
                    {
                        Marshal.StructureToPtr(instruction_variable, (IntPtr)fixed_buffer, false);
                    }
                }
                variable_length += (ushort)(Marshal.SizeOf(instruction_variable));
            }


            header.Length += (ushort)variable_length;

            byte[] pbuffer = new byte[1024];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_header, (IntPtr)fixed_buffer, false);
                }
            }

            length += (ushort)(Marshal.SizeOf(instruction_header));


            Array.Copy(variable_buffer, 0, pbuffer, length, variable_length);


            length += (ushort)variable_length;

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }
        }

        public void onXGT_OneDataRead()
        {
            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';


            header.Reserved = 0;
            header.PLCinfo = 0;

            header.CPUinfo = 0xa0;
            header.SourceofFrame = 0x33;
            header.InvokeID = 0x0000;

            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_One_Data instruction_data = new GlobalVar.STInstruction_One_Data();
            //instruction_data.variable = new byte[7];

            instruction_data.cmd = 0x0054;
            instruction_data.Datatype = 0x0000;
            instruction_data.Reserved2 = 0x0000;
            instruction_data.blockcnt = 0x0001;
            instruction_data.variable_length = 0x0008;



            header.Length = 18;


            byte[] pbuffer = new byte[256];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_data, (IntPtr)fixed_buffer, false);
                }
            }

            length += 10;


            //mx7100 -> mx710.0 => 710*16 + 0(bit)
            pbuffer[length] = (byte)'%'; length++;
            pbuffer[length] = (byte)'M'; length++;
            pbuffer[length] = (byte)'X'; length++;
            pbuffer[length] = (byte)'1'; length++;
            pbuffer[length] = (byte)'1'; length++;
            pbuffer[length] = (byte)'3'; length++;
            pbuffer[length] = (byte)'6'; length++;
            pbuffer[length] = (byte)'0'; length++;

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }

        }

        public void onXGT_OneByteDataRead()
        {
            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';


            header.Reserved = 0;
            header.PLCinfo = 0;

            header.CPUinfo = 0xa0;
            header.SourceofFrame = 0x33;
            header.InvokeID = 0x0000;

            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_One_Data instruction_data = new GlobalVar.STInstruction_One_Data();
            //instruction_data.variable = new byte[7];

            instruction_data.cmd = 0x0054;
            instruction_data.Datatype = 0x0001; //0x0001;//
            instruction_data.Reserved2 = 0x0000;
            instruction_data.blockcnt = 0x0001;
            instruction_data.variable_length = 0x0007;



            header.Length = 17;


            byte[] pbuffer = new byte[256];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_data, (IntPtr)fixed_buffer, false);
                }
            }

            length += 10;


            //mx7100 -> mx710.0 => 710*2 + 0(bit)
            pbuffer[length] = (byte)'%'; length++;
            pbuffer[length] = (byte)'M'; length++;
            pbuffer[length] = (byte)'B'; length++;
            pbuffer[length] = (byte)'1'; length++;
            pbuffer[length] = (byte)'4'; length++;
            pbuffer[length] = (byte)'2'; length++;
            pbuffer[length] = (byte)'0'; length++;

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }

        }

        public void onXGT_DataSend()
        {
            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();  // 프레임 구조 중 헤더구조
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';


            header.Reserved = 0;
            header.PLCinfo = 0;  // Don't care (client -> server)

            header.CPUinfo = 0xa0;   //XGK : 0xa0   XGI : 0xA4    XGR : 0xA8
            header.SourceofFrame = 0x33;  // client -> server : 0x33    server -> client : 0x11
            header.InvokeID = 0x0000;  // 프레임간의 순서를 구별하기 위한 ID
            //lenth의 경우 Instruction 의 바이트 크기이므로 뒤에서 값을 넣는다.
            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_Write_Data instruction_data = new GlobalVar.STInstruction_Write_Data();   // 프레임 구조 중 프레임 기본 구조
            //instruction_data.variable = new byte[7];

            instruction_data.cmd = 0x0058;  // 요구 : 0x0058    응답 : 0x0059
            instruction_data.Datatype = 0x0000;
            instruction_data.Reserved2 = 0x0000;
            instruction_data.cnt = 0x0001;
            instruction_data.variable_length = 0x0008;
            instruction_data.variable = new byte[8];

            instruction_data.variable[0] = (byte)'%';
            instruction_data.variable[1] = (byte)'M';
            instruction_data.variable[2] = (byte)'X';
            instruction_data.variable[3] = (byte)'1';
            instruction_data.variable[4] = (byte)'1';
            instruction_data.variable[5] = (byte)'3';
            instruction_data.variable[6] = (byte)'6';
            instruction_data.variable[7] = (byte)'1';

            instruction_data.Data_lenght = 1;
            instruction_data.Data = 0; //1 = on , 0 = off

            header.Length = (ushort)(Marshal.SizeOf(instruction_data));


            byte[] pbuffer = new byte[256];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_data, (IntPtr)fixed_buffer, false);
                }
            }

            length += (ushort)(Marshal.SizeOf(instruction_data));

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }

        }

        public void onXGT_DataSend(string msg, bool isOn)
        {
            ushort instruction_len = 0;

            GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();  // 프레임 구조 중 헤더구조
            header.companyid = new byte[8];

            header.companyid[0] = (byte)'L';
            header.companyid[1] = (byte)'S';
            header.companyid[2] = (byte)'I';
            header.companyid[3] = (byte)'S';
            header.companyid[4] = (byte)'-';
            header.companyid[5] = (byte)'X';
            header.companyid[6] = (byte)'G';
            header.companyid[7] = (byte)'T';

            char[] msg_char = msg.ToCharArray();


            header.Reserved = 0;
            header.PLCinfo = 0;  // Don't care (client -> server)

            header.CPUinfo = 0xa0;   //XGK : 0xa0   XGI : 0xA4    XGR : 0xA8
            header.SourceofFrame = 0x33;  // client -> server : 0x33    server -> client : 0x11
            header.InvokeID = 0x0000;  // 프레임간의 순서를 구별하기 위한 ID
            //lenth의 경우 Instruction 의 바이트 크기이므로 뒤에서 값을 넣는다.
            header.moduleposition = 0x00;
            header.Reserved2 = 0x00;

            GlobalVar.STInstruction_Write_Data instruction_data = new GlobalVar.STInstruction_Write_Data();   // 프레임 구조 중 프레임 기본 구조
            //instruction_data.variable = new byte[7];

            instruction_data.cmd = 0x0058;  // 요구 : 0x0058    응답 : 0x0059
            instruction_data.Datatype = 0x0000;
            instruction_data.Reserved2 = 0x0000;
            instruction_data.cnt = 0x0001;
            instruction_data.variable_length = 0x0008;
            instruction_data.variable = new byte[8];

            instruction_data.variable[0] = (byte)msg_char[0];
            instruction_data.variable[1] = (byte)msg_char[1];
            instruction_data.variable[2] = (byte)msg_char[2];
            instruction_data.variable[3] = (byte)msg_char[3];
            instruction_data.variable[4] = (byte)msg_char[4];
            instruction_data.variable[5] = (byte)msg_char[5];
            instruction_data.variable[6] = (byte)msg_char[6];
            instruction_data.variable[7] = (byte)msg_char[7];

            instruction_data.Data_lenght = 1;
            if (isOn) instruction_data.Data = 1;
            else instruction_data.Data = 0;

            header.Length = (ushort)(Marshal.SizeOf(instruction_data));


            byte[] pbuffer = new byte[256];
            int length = 0;

            unsafe
            {
                fixed (byte* fixed_buffer = pbuffer)
                {
                    Marshal.StructureToPtr(header, (IntPtr)fixed_buffer, false);
                }
            }

            length = 20;

            unsafe
            {
                fixed (byte* fixed_buffer = &pbuffer[20])
                {
                    Marshal.StructureToPtr(instruction_data, (IntPtr)fixed_buffer, false);
                }
            }

            length += (ushort)(Marshal.SizeOf(instruction_data));

            try
            {
                if (clientsock.Connected)
                {
                    string message = Encoding.Unicode.GetString(pbuffer, 0, length);
                    clientsock.BeginSend(pbuffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);

                    string strtemp = "";


                    for (int i = 0; i < length; i++)
                    {
                        strtemp += string.Format("0x{0:x2} ", pbuffer[i]);
                    }


                    //Console.WriteLine("send => {0}", strtemp);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }

        }


        public void onModbus_Send()
        {

        }

        public void BeginConnect()
        {
            Console.WriteLine("서버 접속 대기중...");

            try
            {
                clientsock.BeginConnect(HOST, PORT, new AsyncCallback(ConnectCallBack), clientsock);
            }
            catch (SocketException se)
            {
                Console.WriteLine("서버 접속 실패.. 재시도..{0}  {1}", HOST, se.NativeErrorCode);
                DoConnect();
            }
        }

        private void ConnectCallBack(IAsyncResult IAR)
        {
            try
            {

                if (clientsock.Connected)
                //if(true)
                {
                    Socket tempSocket = (Socket)IAR.AsyncState;
                    IPEndPoint svrEP = (IPEndPoint)tempSocket.RemoteEndPoint;

                    bConnected = true;

                    Console.WriteLine("서버 접속 성공.. {0}", HOST);
                    tempSocket.EndConnect(IAR);
                    lsconnect_Evt(HOST, 0x01);


                    if (ServerConnect_Checkthread != null)
                    {
                        ServerConnect_Checkthread.Abort();
                        ServerConnect_Checkthread = null;
                    }
                    ServerConnect_Checkthread = new Thread(onConnectCheck);
                    ServerConnect_Checkthread.Start();


                    strSocketid = tempSocket.Handle.ToString();

                    cbSock = tempSocket;
                    cbSock.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), cbSock);
                }
                else
                {
                    Console.WriteLine("서버 접속 실패.. 재접속 시도 {0}", HOST);
                    DoConnect();
                }


            }
            catch (SocketException se)
            {
                if (se.SocketErrorCode == SocketError.NotConnected)
                {
                    Console.WriteLine("서버 접속 실패.. 재시도..callback{0}  {1}", HOST, se.Message.ToString());
                    DoConnect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void onConnectCheck()
        {
            try
            {
                for (; ; )
                {
                    byte[] tempbyte = new byte[2];
                    tempbyte[0] = 0xff;
                    tempbyte[1] = 0xff;

                    clientsock.Send(tempbyte);
                    if (Data.Instance.bFormClose) break;

                    if (!clientsock.Connected)
                    {
                        Disconnect();
                        break;
                    }

                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectCheck err :={0}", ex.Message.ToString());
            }
        }

        public void BeginSend(string message)
        {
            try
            {
                if (clientsock.Connected)
                {
                    byte[] buffer = new UTF8Encoding().GetBytes(message);
                    clientsock.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), message);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("전송 에러 ...{0}", se.Message.ToString());
            }
        }

        private void SendCallBack(IAsyncResult IAR)
        {
            string message = (string)IAR.AsyncState;
            //Console.WriteLine("전송 완료 callback");
        }

        public void Receive()
        {
            if (cbSock.Connected)
                cbSock.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), cbSock);
        }

        private void OnReceiveCallback(IAsyncResult IAR)
        {
            try
            {
                if (cbSock == null) return;

                Socket tempsock = (Socket)IAR.AsyncState;

                if (!tempsock.Connected) return;

                int nReadsize = tempsock.EndReceive(IAR);
                if (nReadsize != 0)
                {
                    string strhex = "";

                    string sdata = Encoding.Unicode.GetString(recvBuffer, 0, nReadsize);


                    GlobalVar.STApplicationHeader header = new GlobalVar.STApplicationHeader();
                    header.companyid = new byte[8];


                    GlobalVar.STInstruction_Continuance_Data_recv instruction_recv_header = new GlobalVar.STInstruction_Continuance_Data_recv();
                    GlobalVar.STInstruction_Continuance_Data_recv_sub instruction_recv_sub = new GlobalVar.STInstruction_Continuance_Data_recv_sub();
                    int length = 0;
                    unsafe
                    {
                        fixed (byte* fixed_buffer = recvBuffer)
                        {
                            header = (GlobalVar.STApplicationHeader)(Marshal.PtrToStructure((IntPtr)fixed_buffer, header.GetType()));
                        }
                    }
                    length += (ushort)(Marshal.SizeOf(header));


                    unsafe
                    {
                        fixed (byte* fixed_buffer = &recvBuffer[length])
                        {
                            instruction_recv_header = (GlobalVar.STInstruction_Continuance_Data_recv)(Marshal.PtrToStructure((IntPtr)fixed_buffer, instruction_recv_header.GetType()));
                        }
                    }
                    length += (ushort)(Marshal.SizeOf(instruction_recv_header));


                    if (instruction_recv_header.err_status == 0x00)
                    {
                        recv_plcData = new int[8, 8];

                        unsafe
                        {
                            fixed (byte* fixed_buffer = &recvBuffer[length])
                            {
                                instruction_recv_sub = (GlobalVar.STInstruction_Continuance_Data_recv_sub)(Marshal.PtrToStructure((IntPtr)fixed_buffer, instruction_recv_sub.GetType()));
                            }
                        }
                        length += (ushort)(Marshal.SizeOf(instruction_recv_sub));
                        int recvdata_cnt = instruction_recv_sub.data_count;
                        if (recvdata_cnt > 0)
                        {
                            byte[] recvbyte_buff = new byte[recvdata_cnt];

                            Array.Copy(recvBuffer, length, recvbyte_buff, 0, recvdata_cnt);

                            string strtemp2 = "";
                            string strtemp3 = "";
                            for (int i = 0; i < recvdata_cnt; i++)
                            {
                                strtemp2 += string.Format("0x{0:x2} ", recvbyte_buff[i]);

                                byte bitbyte = 0x00;
                                for (int j = 0; j < 8; j++)
                                {
                                    bitbyte = (byte)((recvbyte_buff[i] >> j) & 0x01);

                                    recv_plcData[i, j] = (int)bitbyte;

                                    strtemp3 += string.Format("{0}, ", bitbyte);
                                }
                            }

                            //Console.WriteLine("recv data => {0}", strtemp2);
                            //Console.WriteLine("recv bit data => {0}", strtemp3);

                            lsrev_Evt(HOST, "", recv_plcData);

                            OnReceiveDATA();
                        }
                    }
                }

                Receive();
            }
            catch (SocketException se)
            {
                if (se.SocketErrorCode == SocketError.ConnectionReset)
                {
                    BeginConnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Disconnect err ..{0}", ex.Message.ToString());
            }
        }

        public bool _isShowWarehouseInfo { get; set; }
        private void OnReceiveDATA()
        {

            try
            {
                if (GROUP == GROUP_ABN100_1 || GROUP == GROUP_ABN100_2 || GROUP == GROUP_ABN100_3 || GROUP == GROUP_ABN100_4)  // 조립 라인 데이터일 경우
                {
                    m7100 = Convert.ToBoolean(recv_plcData[0, 0]);
                    m7101 = Convert.ToBoolean(recv_plcData[0, 1]);
                    m7102 = Convert.ToBoolean(recv_plcData[0, 2]);
                    m7103 = Convert.ToBoolean(recv_plcData[0, 3]);


                    if (m7100 == true) // 라인  가동중인가
                    {
                        if (HOST != "192.168.102.21" && HOST != "192.168.102.41" && HOST != "192.168.102.61" && HOST != "192.168.102.81" && !ATCNo.Contains("30"))
                        {
                            if (m7101 == true) // IN 부품박스 
                            {
                                // 이벤트
                                addATCNoState(ATCNo[0], m7101);
                                scenarioCall_Evt(HOST, ATCNo[0], "m7101 IN 부품박스");

                            }
                            if (m7102 == true) // IN 부품박스 T상
                            {
                                // 이벤트
                                addATCNoState(ATCNo[1], m7102);
                                scenarioCall_Evt(HOST, ATCNo[1], "m7102 IN 부품박스 T상");
                            }
                        }

                        if (HOST == "192.168.102.61" || HOST == "192.168.102.41") // 적재 자동화
                        {
                            if (m7101 == true) // IN 공박스 
                            {
                                // 이벤트
                                addATCNoState("35", m7101);
                                scenarioCall_Evt(HOST, "35", "m7101 IN 공박스");
                            }
                        }
                        if (HOST == "192.168.102.21" || HOST == "192.168.102.81") // 적재 자동화
                        {
                            if (m7101 == true) // IN 공박스 
                            {
                                // 이벤트
                                addATCNoState("33", m7101);
                                scenarioCall_Evt(HOST, "33", "m7101 IN 공박스");
                            }
                        }
                        //if(HOST == "192.168.102.21"||HOST == "192.168.102.41")
                        //{
                        //    if (m7101 == true) // IN 공박스 
                        //    {
                        //        scenarioCall_Evt(HOST, "35", "m7101 IN 공박스");

                        //    }
                        //}
                        //if (HOST == "192.168.102.61" || HOST == "192.168.102.81")
                        //{
                        //    if (m7101 == true) // IN 공박스 
                        //    {
                        //        scenarioCall_Evt(HOST, "33", "m7101 IN 공박스");

                        //    }
                        //}
                    }


                }
                else if (GROUP == GROUP_EBN100c_1 || GROUP == GROUP_EBN100c_2) //적재자동화
                {
                    m7100 = Convert.ToBoolean(recv_plcData[0, 0]);
                    m7101 = Convert.ToBoolean(recv_plcData[0, 1]);
                    m7102 = Convert.ToBoolean(recv_plcData[0, 2]);
                    m7103 = Convert.ToBoolean(recv_plcData[0, 3]);
                    m7104 = Convert.ToBoolean(recv_plcData[0, 4]);
                    m7105 = Convert.ToBoolean(recv_plcData[0, 5]);
                    m7106 = Convert.ToBoolean(recv_plcData[0, 6]);

                    if (m7100 == true) // 라인  가동중인가
                    {
                        if (m7101 == true) // IN 공박스 
                        {
                            if (GROUP == GROUP_EBN100c_1)
                            {
                                string target = "192.168.102.95";
                                if (form.PLC_Socket_Info.ContainsKey(target))
                                {
                                    if (form.PLC_Socket_Info[target].m7107)  //포장라인2 에 공박스가 있는지
                                    {
                                        addATCNoState("35", m7101);
                                        scenarioCall_Evt(HOST, "35", "m7101 IN 공박스"); 
                                    }
                                    else
                                    {
                                        addATCNoState("33", m7101);
                                        scenarioCall_Evt(HOST, "33", "m7101 IN 공박스"); 
                                    }
                                }

                            }
                            else if(GROUP == GROUP_EBN100c_2)
                            {
                                string target = "192.168.102.94";
                                if (form.PLC_Socket_Info.ContainsKey(target))
                                {
                                    if (form.PLC_Socket_Info[target].m7101)  //포장라인1 에 공박스가 있는지
                                    {
                                        addATCNoState("33", m7101);
                                        scenarioCall_Evt(HOST, "33", "m7101 IN 공박스");  
                                    }
                                    else
                                    {
                                        addATCNoState("35", m7101);
                                        scenarioCall_Evt(HOST, "35", "m7101 IN 공박스");
                                    }
                                }

                            }

                        }
                        if (m7102 == true) // OUT 제품박스
                        {
                            //if(m7104 == true) // 2p
                            //{
                            //    if(GROUP == GROUP_EBN100c_1)
                            //    {
                            //        scenarioCall_Evt(HOST, "32_3", "m7104 OUT 제품박스");
                            //    }else if(GROUP == GROUP_EBN100c_2)
                            //    {
                            //        scenarioCall_Evt(HOST, "32_4", "m7104 OUT 제품박스");
                            //    }

                            //}
                            //if (m7105 == true)  // 3p
                            //{
                            //    if (GROUP == GROUP_EBN100c_1)
                            //    {
                            //        scenarioCall_Evt(HOST, "32_3", "m7105 OUT 제품박스");
                            //    }
                            //    else if (GROUP == GROUP_EBN100c_2)
                            //    {
                            //        scenarioCall_Evt(HOST, "34_6", "m7105 OUT 제품박스");
                            //    }
                            //}
                            //if (m7106 == true)  // 4p
                            //{
                            //    if (GROUP == GROUP_EBN100c_1)
                            //    {
                            //        scenarioCall_Evt(HOST, "34_5", "m7106 OUT 제품박스");
                            //    }
                            //    else if (GROUP == GROUP_EBN100c_2)
                            //    {
                            //        scenarioCall_Evt(HOST, "34_6", "m7106 OUT 제품박스");
                            //    }
                            //}
                        }
                    }

                }
                else if (GROUP == GROUP_PARTSWAREHOUSE) //부품 창고
                {
                    try
                    {

                        m7100 = Convert.ToBoolean(recv_plcData[0, 0]);
                        m7101 = Convert.ToBoolean(recv_plcData[0, 1]);
                        m7102 = Convert.ToBoolean(recv_plcData[0, 2]);
                        m7103 = Convert.ToBoolean(recv_plcData[0, 3]);
                        m7104 = Convert.ToBoolean(recv_plcData[0, 4]);
                        m7105 = Convert.ToBoolean(recv_plcData[0, 5]);
                        m7106 = Convert.ToBoolean(recv_plcData[0, 6]);
                        m7107 = Convert.ToBoolean(recv_plcData[0, 7]);
                        m7108 = Convert.ToBoolean(recv_plcData[1, 0]);
                        m7109 = Convert.ToBoolean(recv_plcData[1, 1]);
                        m7110 = Convert.ToBoolean(recv_plcData[2, 0]);
                        m7111 = Convert.ToBoolean(recv_plcData[2, 1]);
                        m7112 = Convert.ToBoolean(recv_plcData[2, 2]);
                        m7113 = Convert.ToBoolean(recv_plcData[2, 3]);
                        m7114 = Convert.ToBoolean(recv_plcData[2, 4]);
                        m7115 = Convert.ToBoolean(recv_plcData[2, 5]);
                        m7116 = Convert.ToBoolean(recv_plcData[2, 6]);
                        m7117 = Convert.ToBoolean(recv_plcData[2, 7]);
                        m7118 = Convert.ToBoolean(recv_plcData[3, 0]);
                        m7119 = Convert.ToBoolean(recv_plcData[3, 1]);
                        m7120 = Convert.ToBoolean(recv_plcData[4, 0]);
                        m7121 = Convert.ToBoolean(recv_plcData[4, 1]);
                        m7122 = Convert.ToBoolean(recv_plcData[4, 2]);
                        m7123 = Convert.ToBoolean(recv_plcData[4, 3]);
                        m7124 = Convert.ToBoolean(recv_plcData[4, 4]);
                        m7125 = Convert.ToBoolean(recv_plcData[4, 5]);
                        m7126 = Convert.ToBoolean(recv_plcData[4, 6]);
                        m7127 = Convert.ToBoolean(recv_plcData[4, 7]);
                        m7128 = Convert.ToBoolean(recv_plcData[5, 0]);
                        m7129 = Convert.ToBoolean(recv_plcData[5, 1]);
                        m7130 = Convert.ToBoolean(recv_plcData[6, 0]);
                        m7131 = Convert.ToBoolean(recv_plcData[6, 1]);
                        m7132 = Convert.ToBoolean(recv_plcData[6, 2]);
                        m7133 = Convert.ToBoolean(recv_plcData[6, 3]);
                        m7134 = Convert.ToBoolean(recv_plcData[6, 4]);
                        m7135 = Convert.ToBoolean(recv_plcData[6, 5]);
                        m7140 = Convert.ToBoolean(recv_plcData[7, 1]);

                        if (_isShowWarehouseInfo)
                        {
                            Console.WriteLine("Parts Warehouse m7130 = " + m7130 + "  m7131 = " + m7131 + "  m7132 = " + m7132 + "  m7133 = " + m7133 + "  m7134 = " + m7134 + "  m7135 = " + m7135 + "  m7140 = " + m7140);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OnReceiveDATA err ..{0}", ex.Message.ToString());
                    }

                    if (m7100 == true)
                    {
                        if (m7134 == false)
                        {
                            addATCNoState("30_1", m7134);
                            scenarioCall_Evt(HOST, "30_1", "m7134");
                        }
                        else if (m7135 == false)
                        {
                            addATCNoState("30_2", m7135);
                            scenarioCall_Evt(HOST, "30_2", "m7135");
                        }
                    }

                }
                else if (GROUP == GROUP_ABN100PACK_1 || GROUP == GROUP_ABN100PACK_2) // 포장라인
                {
                    m7100 = Convert.ToBoolean(recv_plcData[0, 0]);
                    m7101 = Convert.ToBoolean(recv_plcData[0, 1]);
                    m7102 = Convert.ToBoolean(recv_plcData[0, 2]);
                    m7103 = Convert.ToBoolean(recv_plcData[0, 3]);
                    m7104 = Convert.ToBoolean(recv_plcData[0, 4]);
                    m7105 = Convert.ToBoolean(recv_plcData[0, 5]);
                    m7106 = Convert.ToBoolean(recv_plcData[0, 6]);
                    m7107 = Convert.ToBoolean(recv_plcData[0, 7]);
                    m7108 = Convert.ToBoolean(recv_plcData[1, 0]);

                    if (m7100 == true) // 라인  가동중인가
                    {
                        if (GROUP == GROUP_ABN100PACK_1)  // 포장1 제품 박스 만재 콜
                        {
                            if (m7103 == false)
                            {
                                string atc = getATCFromAddr("m7103");
                                addATCNoState(atc, m7103);
                                scenarioCall_Evt(HOST, atc, "m7103 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_2 로부터 받아야함 
                            }
                            else if (m7102 == false)
                            {
                                string atc = getATCFromAddr("m7102");
                                addATCNoState(atc, m7102);
                                scenarioCall_Evt(HOST, atc, "m7102 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_1 로부터 받아야함 
                            }
                            if (m7104 == false)
                            {
                                string atc = getATCFromAddr("m7104");
                                addATCNoState(atc, m7104);
                                scenarioCall_Evt(HOST, atc, "m7104 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_3 로부터 받아야함 
                            }
                            if (m7105 == false)
                            {
                                string atc = getATCFromAddr("m7105");
                                addATCNoState(atc, m7105);
                                scenarioCall_Evt(HOST, atc, "m7105 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_4 로부터 받아야함 
                            }
                            if (m7107 == false)
                            {
                                string atc = getATCFromAddr("m7107");
                                addATCNoState(atc, m7107);
                                scenarioCall_Evt(HOST, atc, "m7107 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_6 로부터 받아야함 
                            }
                            else if (m7106 == false)
                            {
                                string atc = getATCFromAddr("m7106");
                                addATCNoState(atc, m7106);
                                scenarioCall_Evt(HOST, atc, "m7106 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_5 로부터 받아야함 
                            }
                        }
                        else if (GROUP == GROUP_ABN100PACK_2)  //포장2 라인 제품박스 만재 콜
                        {
                            if (m7102 == false)
                            {
                                string atc = getATCFromAddr("m7102");
                                addATCNoState(atc, m7102);
                                scenarioCall_Evt(HOST, atc, "m7102 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_2 로부터 받아야함 
                            }
                            else if (m7101 == false)
                            {
                                string atc = getATCFromAddr("m7101");
                                addATCNoState(atc, m7101);
                                scenarioCall_Evt(HOST, atc, "m7101 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 34_1 로부터 받아야함 
                            }
                            if (m7104 == false)
                            {
                                string atc = getATCFromAddr("m7104");
                                addATCNoState(atc, m7104);
                                scenarioCall_Evt(HOST, atc, "m7104 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_4 로부터 받아야함 
                            }
                            else if (m7103 == false)
                            {
                                string atc = getATCFromAddr("m7103");
                                addATCNoState(atc, m7103);
                                scenarioCall_Evt(HOST, atc, "m7103 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_3 로부터 받아야함 
                            }
                            if (m7105 == false)
                            {
                                string atc = getATCFromAddr("m7105");
                                addATCNoState(atc, m7105);
                                scenarioCall_Evt(HOST, atc, "m7105 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_5 로부터 받아야함 
                            }
                            if (m7106 == false)
                            {
                                string atc = getATCFromAddr("m7106");
                                addATCNoState(atc, m7106);
                                scenarioCall_Evt(HOST, atc, "m7106 제품 박스 만재 False");  // ABN 포장라인#2 ATC No 32_6 로부터 받아야함 
                            }
                        }
                    }

                }
                else if (GROUP == GROUP_ALARM) //경보 시스템
                {
                    m7000 = Convert.ToBoolean(recv_plcData[0, 0]);
                    m7001 = Convert.ToBoolean(recv_plcData[0, 1]);
                    m7002 = Convert.ToBoolean(recv_plcData[0, 2]);
                    m7003 = Convert.ToBoolean(recv_plcData[0, 3]);
                    m7004 = Convert.ToBoolean(recv_plcData[0, 4]);
                    m7005 = Convert.ToBoolean(recv_plcData[0, 5]);

                }
                checkATCNoState();  //asdf
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("OnReceiveDATA err ..{0}", ex.Message.ToString());
            }

        }
        public string startATCNo = "";
        public void checkCondition(string requiedID, string atcNo)
        {
            if (GROUP == GROUP_ABN100_1 || GROUP == GROUP_ABN100_2 || GROUP == GROUP_ABN100_3 || GROUP == GROUP_ABN100_4)
            {
                if (m7100 == true)
                {
                    if (atcNo == "30_1" || atcNo == "30_2")  // 공박스 수거
                    {
                        if (ATCNo.Contains("30"))
                        {
                            if (m7102 == true)
                            {
                                //if (!ATCNoState.ContainsKey("30"))
                                //{
                                //    addATCNoState("30", m7102);
                                //}
                                addATCNoState(atcNo, m7102);
                                scenarioStart_Evt(requiedID, HOST, "m7102", atcNo);
                            }
                        }
                    }
                    else if (ATCNo.Contains(atcNo))  // 적재 자동화
                    {
                        bool condition = m7102;
                        if (condition == true)
                        {
                            // scen start!!!
                            addATCNoState(atcNo, m7102);
                            scenarioStart_Evt(requiedID, HOST, "m7102", atcNo);
                        }

                    } //asdf 
                }
            }
            else if (GROUP == GROUP_PARTSWAREHOUSE)
            {
                if (m7100 == true)
                {
                    bool condition = getAddrFromATC(atcNo);
                    if (condition == true)
                    {
                        // scen start!!!
                        addATCNoState(atcNo, condition);
                        scenarioStart_Evt(requiedID, HOST, addrFromATC[atcNo], atcNo);
                    }
                    if (atcNo == "5")
                    {
                        if (m7102 == true)
                        {
                            addATCNoState("5_1", m7102);
                            scenarioStart_Evt(requiedID, HOST, "m7102", "5_1");
                            startATCNo = "5_1";
                        }
                        else if (m7103 == true)
                        {

                            addATCNoState("5_2", m7103);
                            scenarioStart_Evt(requiedID, HOST, "m7103", "5_2");
                            startATCNo = "5_2";
                        }
                    }
                    if (atcNo == "6")
                    {
                        if (m7105 == true)
                        {
                            addATCNoState("6_1", m7105);
                            scenarioStart_Evt(requiedID, HOST, "m7105", "6_1");
                            startATCNo = "6_1";
                        }
                        else if (m7106 == true)
                        {
                            addATCNoState("6_2", m7106);
                            scenarioStart_Evt(requiedID, HOST, "m7106", "6_2");
                            startATCNo = "6_2";
                        }
                    }
                    if (atcNo == "4")
                    {
                        if (m7108 == true)
                        {
                            addATCNoState("4_1", m7108);
                            scenarioStart_Evt(requiedID, HOST, "m7108", "4_1");
                            startATCNo = "4_1";
                        }
                        else if (m7109 == true)
                        {
                            addATCNoState("4_2", m7109);
                            scenarioStart_Evt(requiedID, HOST, "m7109", "4_2");
                            startATCNo = "4_2";
                        }
                    }
                    if (atcNo == "3")
                    {
                        if (m7111 == true)
                        {
                            addATCNoState("3_1", m7111);
                            scenarioStart_Evt(requiedID, HOST, "m7111", "3_1");
                            startATCNo = "3_1";
                        }
                        else if (m7112 == true)
                        {
                            addATCNoState("3_2", m7112);
                            scenarioStart_Evt(requiedID, HOST, "m7112", "3_2");
                            startATCNo = "3_2";
                        }
                    }
                }
            }
            else if (GROUP == GROUP_ABN100PACK_1 || GROUP == GROUP_ABN100PACK_2)
            {
                if (m7100)
                {
                    bool condition = getAddrFromATC(atcNo);
                    if (condition == true)
                    {
                        // scen start!!!
                        addATCNoState(atcNo, condition);
                        scenarioStart_Evt(requiedID, HOST, addrFromATC[atcNo], atcNo);
                    }
                }
            }
            else if (GROUP == GROUP_EBN100c_1 || GROUP == GROUP_EBN100c_2)
            {
                if (m7100 == true)
                {
                    if (m7102 == true)  // OUT 제품박스
                    {
                        if (GROUP == GROUP_EBN100c_1)
                        {
                            if (atcNo == "32_3")
                            {
                                if (m7104 == true) // 2P
                                {
                                    addATCNoState(atcNo, m7104);
                                    scenarioStart_Evt(requiedID, HOST, "m7104", atcNo);
                                }
                                if (m7105 == true)  // 3P
                                {
                                    addATCNoState(atcNo, m7105);
                                    scenarioStart_Evt(requiedID, HOST, "m7105", atcNo);
                                }
                            }
                            else if (atcNo == "34_5")
                            {
                                if (m7106 == true)  // 4P
                                {
                                    addATCNoState(atcNo, m7106);
                                    scenarioStart_Evt(requiedID, HOST, "m7106", atcNo);
                                }
                            }
                        }
                        else if (GROUP == GROUP_EBN100c_2)
                        {
                            if (atcNo == "32_4")
                            {
                                if (m7104 == true)  // 2P
                                {
                                    addATCNoState(atcNo, m7104);
                                    scenarioStart_Evt(requiedID, HOST, "m7104", atcNo);
                                }
                            }
                            else if (atcNo == "34_6")
                            {
                                if (m7105 == true)  // 3P
                                {
                                    addATCNoState(atcNo, m7105);
                                    scenarioStart_Evt(requiedID, HOST, "m7105", atcNo);
                                }
                                if (m7106 == true)  // 4P
                                {
                                    addATCNoState(atcNo, m7106);
                                    scenarioStart_Evt(requiedID, HOST, "m7106", atcNo);
                                }
                            }
                        }
                    }
                }
            }
        }

        public string getATCFromAddr(string addr)
        {
            string str = "";
            if (GROUP == GROUP_ABN100PACK_1 || GROUP == GROUP_ABN100PACK_2)
            {
                str = addrFromATC.FirstOrDefault(x => x.Value == addr).Key;
            }
            return str;
        }
        public bool getAddrFromATC(string atcNo)
        {
            //if (addrFromATC.ContainsKey(atcNo))
            //{
            //    if(GROUP == GROUP_PARTSWAREHOUSE)
            //    {
            if (addrFromATC.ContainsKey(atcNo))
            {
                switch (addrFromATC[atcNo])
                {
                    case "m7100":
                        return m7100;
                    case "m7101":
                        return m7101;
                    case "m7102":
                        return m7102;
                    case "m7103":
                        return m7103;
                    case "m7104":
                        return m7104;
                    case "m7105":
                        return m7105;
                    case "m7106":
                        return m7106;
                    case "m7107":
                        return m7107;
                    case "m7108":
                        return m7108;
                    case "m7109":
                        return m7109;
                    case "m7110":
                        return m7110;
                    case "m7111":
                        return m7111;
                    case "m7112":
                        return m7112;
                    case "m7113":
                        return m7113;
                    case "m7114":
                        return m7114;
                    case "m7115":
                        return m7115;
                    case "m7116":
                        return m7116;
                    case "m7117":
                        return m7117;
                    case "m7118":
                        return m7118;
                    case "m7119":
                        return m7119;
                    case "m7120":
                        return m7120;
                    case "m7121":
                        return m7121;
                    case "m7122":
                        return m7122;
                    case "m7123":
                        return m7123;
                    case "m7124":
                        return m7124;
                    case "m7125":
                        return m7125;
                    case "m7126":
                        return m7126;
                    case "m7127":
                        return m7127;
                    case "m7128":
                        return m7128;
                    case "m7129":
                        return m7129;
                    case "m7130":
                        return m7130;
                    case "m7131":
                        return m7131;
                    case "m7132":
                        return m7132;
                    case "m7133":
                        return m7133;
                    case "m7134":
                        return m7134;
                    case "m7135":
                        return m7135;
                }
            }
            //}
            //if(GROUP == GROUP_ABN100PACK_1 || GROUP==GROUP_ABN100PACK_2)
            //{
            //    switch (addrFromATC[atcNo])
            //    {
            //        case "m7101":
            //            return m7101;
            //        case "m7102":
            //            return m7102;
            //        case "m7103":
            //            return m7103;
            //        case "m7104":
            //            return m7104;
            //        case "m7105":
            //            return m7105;
            //        case "m7106":
            //            return m7106;
            //        case "m7107":
            //            return m7107;
            //    }
            //}

            //}
            return false;
        }

        private bool getIsValidGroup(string group)
        {
            bool _isValidGroup = false;

            if(group == GROUP_ABN100_1 || group == GROUP_ABN100_2 || group == GROUP_ABN100_3 || group == GROUP_ABN100_4)
            {
                if (GROUP == GROUP_ABN100_1 || GROUP == GROUP_ABN100_2 || GROUP == GROUP_ABN100_3 || GROUP == GROUP_ABN100_4)
                {
                    _isValidGroup = false;
                }
                else
                {
                    _isValidGroup = true;
                }
            }
            else if(GROUP != group)
            {
                _isValidGroup = true;
            }



            return _isValidGroup;
        }

        /// <summary>
        /// atcNo 에 해당하는 LS_Socket클래스 반환
        /// </summary>
        /// <param name="atcNo"></param>
        /// <returns></returns>
        public LS_Socket getSocketFromATCNo(string group, string atcNo)
        {
            if (getIsValidGroup(group))
            {

                if (atcNo == "4" || atcNo == "5" || atcNo == "6" || atcNo == "3")
                {
                    if (GROUP == GROUP_PARTSWAREHOUSE)
                    {
                        return this;
                    }
                }
                else if (atcNo == "33")
                {
                    if (GROUP == GROUP_ABN100PACK_1)
                    {
                        return this;
                    }
                }
                else if (atcNo == "35")
                {
                    if (GROUP == GROUP_ABN100PACK_2)
                    {
                        return this;
                    }
                }
                else if (GROUP == GROUP_PARTSWAREHOUSE || GROUP == GROUP_ABN100PACK_1 || GROUP == GROUP_ABN100PACK_2 || GROUP == GROUP_EBN100c_1 || GROUP == GROUP_EBN100c_2)
                {
                    for (int i = 0; i < ATCNo.Length; i++)
                    {
                        if (ATCNo[i] == atcNo)
                        {
                            return this;
                        }
                    }

                }
                else if (GROUP == GROUP_ABN100_1 || GROUP == GROUP_ABN100_2 || GROUP == GROUP_ABN100_3 || GROUP == GROUP_ABN100_4)
                {
                    if (atcNo == "30_1" || atcNo == "30_2")
                    {
                        for (int i = 0; i < ATCNo.Length; i++)
                        {
                            if (ATCNo[i] == "30")
                            {
                                if (GetSTATE() != (int)state.SCENARIO)
                                {
                                    if (m7100 == true && m7102 == true)
                                        return this;
                                }

                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ATCNo.Length; i++)
                        {
                            if (ATCNo[i] == atcNo)
                            {
                                return this;
                            }
                        }
                    }

                }
            }
            return null;
        }

        private void addATCNoState(string atcNo, bool value)
        {
            //Console.WriteLine("addATCNoState atcNo [{0}] , value [{1}]",atcNo,value);
            try
            {
                if (ATCNoState.ContainsKey(atcNo))
                {
                    //if(ATCNoState[atcNo] != value)
                    //{
                    //    ATCNoState[atcNo] = value;
                    //}
                }
                else
                {
                    ATCNoState.Add(atcNo, value);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("addATCNoState err...{0}", ex.Message.ToString());
            }
        }

        void clearSituation(string atcNo)
        {
            //if (STATE_PRE == state.REQUIRE)
            //    ATCState_Evt(HOST,atcNo, false);
            if(atcNo == "3" || atcNo == "4" || atcNo == "5" || atcNo == "6")
            {
                if(startATCNo != "")atcNo = startATCNo;
            }
            ATCState_Evt(HOST, atcNo, false);
            SetSTATE(0);
            ATCNoState.Remove(atcNo);
        }

        private void checkATCNoState()
        {
            string atc = "";
            try
            {
                foreach (string atcNo in new List<string>(ATCNoState.Keys))
                {
                    atc = atcNo;
                    //Console.WriteLine("atcNo = "+atcNo + " / ip = "+HOST);
                    if (GROUP == GROUP_ABN100_1 || GROUP == GROUP_ABN100_2 || GROUP == GROUP_ABN100_3 || GROUP == GROUP_ABN100_4)
                    {
                        if (HOST == "192.168.102.13" || HOST == "192.168.102.33" || HOST == "192.168.102.73")  // R-T 상
                        {
                            if (atcNo == "11" || atcNo == "17" || atcNo == "25") // R상
                            {
                                if (m7101 != ATCNoState[atcNo])
                                {
                                    clearSituation(atcNo);
                                    //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                                }
                            }
                            else // T상
                            {
                                if (m7102 != ATCNoState[atcNo])
                                {
                                    clearSituation(atcNo);
                                    //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                                }
                            }
                            // 유민상
                        }
                        else if (HOST == "192.168.102.21" || HOST == "192.168.102.41" || HOST == "192.168.102.61" || HOST == "192.168.102.81") // 적재 자동화
                        {
                            if (atcNo == "35" || atcNo == "33")
                            {
                                if (m7101 != ATCNoState[atcNo])
                                {
                                    clearSituation(atcNo);
                                    //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                                }
                            }
                            else
                            {
                                if (m7102 != ATCNoState[atcNo])
                                {
                                    clearSituation(atcNo);
                                    //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                                }
                            }
                        }
                        else if (HOST == "192.168.102.22" || HOST == "192.168.102.42" || HOST == "192.168.102.62" || HOST == "192.168.102.82")  //공박스
                        {
                            if (m7102 != ATCNoState[atcNo])
                            {
                                clearSituation(atcNo);
                                //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                            }
                        }
                        else
                        {
                            if (m7101 != ATCNoState[atcNo]) // 일반
                            {
                                clearSituation(atcNo);
                                //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                            }
                        }
                    }
                    else if (GROUP == GROUP_EBN100c_1 || GROUP == GROUP_EBN100c_2)
                    {
                        if (atcNo == "33" || atcNo == "35") //공박스
                        {
                            if (m7101 != ATCNoState[atcNo])
                            {
                                clearSituation(atcNo);
                                //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                            }
                        }
                        else  //폴
                        {
                            if (m7102 != ATCNoState[atcNo])
                            {
                                clearSituation(atcNo);
                                //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                            }
                        }
                    }
                    else  // warehouse, packing line 1~2
                    {
                        if (getAddrFromATC(atcNo) != ATCNoState[atcNo]) // 상황해제    // getAddrFromATC 에서 atcNo 없을 경우 false 로 들어온다....
                        {
                            clearSituation(atcNo);
                            //Console.WriteLine("상황 해제 {0} / {1}", HOST, atcNo);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("checkATCNoState err...{0} / HOST {1} / ATCNo {2}", ex.Message.ToString(),HOST ,atc);
            }
        }

        public void LS_SocketRecv(byte[] recvbuff)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("XIS_SocketRecv err ..{0}", ex.Message.ToString());
            }

        }

        public void Disconnect()
        {
            _isDisconnect = true;
            try
            {
                if (clientsock.Connected)
                {
                    this.lssocketrev_Evt -= new LS_SocketResponse(this.LS_SocketRecv);

                    cbSock.Shutdown(SocketShutdown.Both);
                    cbSock.Close();
                    cbSock = null;

                    clientsock = null;
                }

                bConnected = false;
                lsconnect_Evt(HOST, 0x02);
                Console.WriteLine("Disconnect");
            }
            catch (SocketException se)
            {
                lsconnect_Evt(HOST, 0x02);
                Console.WriteLine("Disconnect err ..{0}", se.Message.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Disconnect err ..{0}", e.Message.ToString());
            }
        }

    }
}