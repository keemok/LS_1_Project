using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Syscon_Solution.Exhibition
{
    class SubconSocket
    {
        public bool bConnected = false;
        private Socket client = null;
        private Socket cbSock;
        private byte[] recvBuffer;

        private const int MAX_BUFFER_SIZE = 4096;
        private string HOST;
        private int PORT;

        public string recvMsg = "";

        public string strSocketId = "";

        public event SocketResponse socketRecvEvent;
        public delegate void SocketResponse(byte[] recvBuff, int length);

        public event SubconResponse subconRecvEvent;
        public delegate void SubconResponse(string strID, string data);

        public event SubconBtnResponse subconBtnRecvEvent;
        public delegate void SubconBtnResponse(string strID, byte[] data, byte[] info);

        private byte Subcon_start = 0xaa;
        private byte Subcon_end = 0x55;

        private byte Subcon_ID_idx = 1;
        private byte Subcon_CMD1_idx = 1;
        private byte Subcon_CMD2_idx = 2;
        private byte Subcon_CMD3_idx = 3;
        private byte Subcon_DATA1_idx = 4;
        private byte Subcon_DATA2_idx = 5;
        private byte Subcon_DATA3_idx = 6;
        private byte Subcon_DATA4_idx = 7;

        public SubconSocket()
        {
        }

        public SubconSocket(string strhost, int nport)
        {
            HOST = strhost;
            PORT = nport;

        }

        public void onSocketInit()
        {
            recvBuffer = new byte[MAX_BUFFER_SIZE];
            DoConnect();

            this.socketRecvEvent += new SocketResponse(this.SocketRecv);

        }

        public void DoConnect()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            BeginConnect();
        }

        public void BeginConnect()
        {
            Console.WriteLine("WATING CONNECT... SUBCON IP IS " + HOST);

            try
            {
                client.BeginConnect(HOST, PORT, new AsyncCallback(ConnectCallBack), client);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Connect err : " + se.Message.ToString());
            }

        }

        Thread ServerConnect_Checkthread;

        private void ConnectCallBack(IAsyncResult IAR)
        {
            try
            {
                Socket tempSocket = (Socket)IAR.AsyncState;
                IPEndPoint svrEP = (IPEndPoint)tempSocket.RemoteEndPoint;

                bConnected = true;
                Console.WriteLine("CONNECT SUCESSCE");
                tempSocket.EndConnect(IAR);

                if (ServerConnect_Checkthread != null)
                {
                    ServerConnect_Checkthread.Abort();
                    ServerConnect_Checkthread = null;
                }
                ServerConnect_Checkthread = new Thread(onConnectCheck);
                ServerConnect_Checkthread.IsBackground = true;
                ServerConnect_Checkthread.Start();


                strSocketId = tempSocket.Handle.ToString();
                cbSock = tempSocket;
                cbSock.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallBack), cbSock);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Connect callback err : " + se.Message.ToString());
            }
        }

        public void BeginSend(string message)
        {
            try
            {
                if (client.Connected)
                {
                    byte[] buffer = new UTF8Encoding().GetBytes(message);
                    client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), message);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Send message err : " + se.Message.ToString());
            }
        }

        public void BeginByteSend(byte[] buffer, int length)
        {
            string msg = "send= ";
            for (int i = 0; i < length; i++)
            {
                msg += string.Format("0x{0:x2}..", buffer[i]);
            }
            subconRecvEvent(HOST, msg);


            try
            {
                if (client.Connected)
                {
                    // byte[] buffer = new UTF8Encoding().GetBytes(message);
                    string message = Encoding.Unicode.GetString(buffer, 0, length);
                    client.BeginSend(buffer, 0, length, SocketFlags.None, new AsyncCallback(SendCallBack), message);
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
            //Console.WriteLine("SEND MESSAGE ");
        }

        public string Receive()
        {

            if (cbSock != null)
            {
                if (cbSock.Connected)
                {
                    cbSock.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallBack), cbSock);

                    return getReceiveMsg();
                }
                return "ERROR";
            }

            return "Socket is disconnected";
        } 
        string getReceiveMsg()
        {
            return recvMsg;
        }

        bool bchargestart = false;
        private byte[] XIS_recvBuffer = new byte[1024];
        int XIS_recv_curridx = 0;

        private void OnReceiveCallBack(IAsyncResult IAR)
        {

            Socket tempsock = (Socket)IAR.AsyncState;

            if (!tempsock.Connected) return;

            int nReadsize = 0;
            try
            {
                nReadsize = tempsock.EndReceive(IAR);
            }
            catch (Exception e)
            {
                Console.WriteLine("OnReceiveCallBack err " + e.Message.ToString());
            }


            string strhex = "";

            if (nReadsize != 0)
            {

                for (int i = 0; i < nReadsize; i++)
                {
                    string strtemp = string.Format("0x{0:x2}..", recvBuffer[i]);


                    if (recvBuffer[i] == Subcon_start && !bchargestart)
                    {
                        bchargestart = true;
                        XIS_recvBuffer[XIS_recv_curridx] = recvBuffer[i];
                        XIS_recv_curridx++;
                    }
                    else if (bchargestart)
                    {
                        XIS_recvBuffer[XIS_recv_curridx] = recvBuffer[i];
                        XIS_recv_curridx++;

                        if (recvBuffer[i] == Subcon_end)
                        {
                            socketRecvEvent(XIS_recvBuffer, XIS_recv_curridx);   // 받기 완료한 버퍼

                            XIS_recv_curridx = 0;
                            bchargestart = false;
                            XIS_recvBuffer = new byte[1024];
                        }
                    }
                    strhex += strtemp;
                }


            }
            //subconRecvEvent(HOST,strhex);

            Receive();
        }

        public void SocketRecv(byte[] recvBuff, int length)
        {
            try
            {
                string strhex = "recv= ";

                for (int i = 0; i < length; i++)
                {
                    string strtemp = string.Format("0x{0:x2}..", recvBuffer[i]);

                    strhex += strtemp;
                }
                subconRecvEvent(HOST, strhex);

                byte recv_crc = 0x00;

                recv_crc = recvBuff[8];

                byte cal_crc = 0x00;
                for (int i = 1; i < 8; i++)
                    cal_crc = (byte)(cal_crc ^ recvBuff[i]);
                if (recv_crc == cal_crc)
                {
                    byte ID = 0x00;
                    byte CMD1 = 0x00;
                    byte CMD2 = 0x00;
                    byte CMD3 = 0x00;
                    byte Data1 = 0x00;
                    byte Data2 = 0x00;
                    byte Data3 = 0x00;
                    byte Data4 = 0x00;
                    byte CRC = 0x00;


                    ID = recvBuff[Subcon_ID_idx];
                    CMD1 = recvBuff[Subcon_CMD1_idx];
                    CMD2 = recvBuff[Subcon_CMD2_idx];
                    CMD3 = recvBuff[Subcon_CMD3_idx];

                    Data1 = recvBuff[Subcon_DATA1_idx];
                    Data2 = recvBuff[Subcon_DATA2_idx];
                    Data3 = recvBuff[Subcon_DATA3_idx];
                    Data4 = recvBuff[Subcon_DATA4_idx];
                    CRC = recvBuff[8];

                    if (CMD1 == 0x40) //충전대
                    {
                        if (CMD2 == 0x00) //대기
                        {
                            Console.WriteLine("HOST={0}...XIS 충전대기", HOST);

                            subconRecvEvent(HOST, "XIS 충전대기");
                        }
                        else if (CMD2 == 0x01) //도킹완료
                        {
                            Console.WriteLine("HOST={0}...XIS 충전 도킹완료", HOST);
                            subconRecvEvent(HOST, "XIS 충전 도킹완료");
                        }
                    }
                    //if (ID == 0x50) //컨베이어
                    //{
                    //    if(CMD2 == 0x00) // 대기상태
                    //    {
                    //        subconRecvEvent(HOST, "컨베이어 대기상태");
                    //    }else if (CMD2 == 0x01) // 선/하적 중
                    //    {
                    //        subconRecvEvent(HOST, "컨베이어 선/하적 중");
                    //    }else if (CMD2 == 0x02) // 선/하적 완료
                    //    {
                    //        subconRecvEvent(HOST, "컨베이어 선/하적 완료");
                    //    }
                    //    else if (CMD2 == 0x03) // 
                    //    {
                    //        //subconRecvEvent(HOST, "컨베이어 ");
                    //    }
                    //    else if (CMD2 == 0x04) // 
                    //    {
                    //        //subconRecvEvent(HOST, "컨베이어 ");
                    //    }
                    //    else if (CMD2 == 0x05) // 
                    //    {
                    //        //subconRecvEvent(HOST, "컨베이어 ");
                    //    }
                    //}
                    if (CMD1 == 0x10) // PC -> Sub CON 메인보드 명령
                    {
                        //if (CMD2 == 0x01) //workstate ( LED 관련)
                        //{
                        //    if (CMD3 == 0x10) // 
                        //    {
                        //        //subconRecvEvent(HOST, "workstate ");
                        //    }
                        //    else if (CMD3 == 0x11) //  workstate 대기상태
                        //    {
                        //        subconRecvEvent(HOST, "workstate 대기상태");
                        //    }
                        //    else if (CMD3 == 0x21) //  workstate 미션 수행 중
                        //    {
                        //        subconRecvEvent(HOST, "workstate 미션 수행 중");
                        //    }
                        //    else if (CMD3 == 0x23) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 미션 일시 정지");
                        //    }
                        //    else if (CMD3 == 0x31) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 리프트/컨베이어 작동 중");
                        //    }
                        //    else if (CMD3 == 0x41) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 맵핑");
                        //    }
                        //    else if (CMD3 == 0x51) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 수동제어");
                        //    }
                        //    else if (CMD3 == 0x61) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 충전 중");
                        //    }
                        //    else if (CMD3 == 0x64) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 충전 이동 중");
                        //    }
                        //    else if (CMD3 == 0x71) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 배터리 경고");
                        //    }
                        //    else if (CMD3 == 0x73) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 주행 근접 경고");
                        //    }
                        //    else if (CMD3 == 0x81) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 소프트웨어 에러 ㅡ 알고리즘");
                        //    }
                        //    else if (CMD3 == 0x82) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 하드웨어 에러 ㅡ 버튼, 라이다");
                        //    }
                        //    else if (CMD3 == 0x83) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 외부 통신 에러 ㅡ 무선 통신");
                        //    }
                        //    else if (CMD3 == 0x84) //  workstate 
                        //    {
                        //        subconRecvEvent(HOST, "workstate 내부 통신 에러 ㅡ 디바이스 통신");
                        //    }
                        //}
                        //else if (CMD2 == 0x02) // lift 관련 
                        //{
                        //    if (CMD3 == 0x00)
                        //    {
                        //        subconRecvEvent(HOST, "lift Stop");
                        //    }
                        //    else if (CMD3 == 0x01)
                        //    {
                        //        subconRecvEvent(HOST, "lift UP");
                        //    }
                        //    else if (CMD3 == 0x02)
                        //    {
                        //        subconRecvEvent(HOST, "lift DOWN");
                        //    }
                        //}
                        //else if(CMD2 == 0x03) // 음악 선택 재생
                        //{
                        //    if(CMD3 == 0x00)
                        //    {
                        //        subconRecvEvent(HOST, "MP3 Stop");
                        //    }
                        //    else if(CMD3 == 0x01)
                        //    {

                        //        subconRecvEvent(HOST, "MP3 Play : "+Data4 + "선택 됨"); //Data4 최대 256개
                        //    }
                        //    else if(CMD3 == 0x02)
                        //    {

                        //        subconRecvEvent(HOST, "MP3 Sound Volume : "+Data4);  //Data4 는 0~31 까지 설정
                        //    }
                        //}
                        //else if(CMD2 == 0x04) //Main Motor
                        //{
                        //    if (CMD3 == 0x06)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor 즉시 Stop");
                        //    }
                        //    else if (CMD3 == 0x05)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor Stop ramp에 의한 Stop");
                        //    }
                        //    else if (CMD3 == 0x02)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor 후진");
                        //    }
                        //    else if (CMD3 == 0x01)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor 전진");
                        //    }
                        //    else if (CMD3 == 0x03)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor 좌회전");
                        //    }
                        //    else if (CMD3 == 0x00)
                        //    {
                        //        subconRecvEvent(HOST, "Main Motor 우회전");
                        //    }
                        //}
                    }
                    if (CMD1 == 0x20) //Sub Con -> PC 상태값 전송
                    {
                        if (CMD2 == 0x01) // 버튼 관련
                        {
                            if (CMD3 == 0x00)
                            {
                                if (Data1 == 0x00)
                                {
                                    subconRecvEvent(HOST, "EMG Button Normal");
                                }
                                else if (Data1 == 0x01)
                                {
                                    subconRecvEvent(HOST, "ENG Button 버튼 눌림");
                                }
                            }
                            else if (CMD3 == 0x01) //미션 버튼
                            {
                                if (Data1 == 0x01)
                                {
                                    subconRecvEvent(HOST, "미션 버튼 일시정지");
                                }
                                else if (Data1 == 0x02)
                                {
                                    subconRecvEvent(HOST, "미션 버튼 시작");
                                }
                            }
                        }
                        else if (CMD2 == 0x03)
                        {
                            if (CMD3 == 0x00)
                            {
                                subconRecvEvent(HOST, "리프트 정지 시");
                            }
                            else if (CMD3 == 0x01)
                            {
                                subconRecvEvent(HOST, "리프트 TOP 도착 시");
                            }
                            else if (CMD3 == 0x02)
                            {
                                subconRecvEvent(HOST, "리프트 Bottom 도착 시");
                            }
                            else if (CMD3 == 0x03)
                            {
                                subconRecvEvent(HOST, "리프트 비정상 정지 ㅡ 에러");
                            }
                        }
                        else if (CMD2 == 0x04) // Lidar Field Output Signal
                        {
                            if (CMD3 == 0x00)
                            {
                                subconRecvEvent(HOST, "Lidar 응급 정지");
                            }
                            else if (CMD3 == 0x01)
                            {
                                if (Data1 == 0x01)
                                {
                                    subconRecvEvent(HOST, "Lidar 경고 영역1 Front 감지");
                                }
                                else if (Data1 == 0x02)
                                {
                                    subconRecvEvent(HOST, "Lidar 경고 영역1 Rear 감지");
                                }
                            }
                            else if (CMD3 == 0x02)
                            {
                                if (Data1 == 0x01)
                                {
                                    subconRecvEvent(HOST, "Lidar 경고 영역2 Front 감지");
                                }
                                else if (Data1 == 0x02)
                                {
                                    subconRecvEvent(HOST, "Lidar 경고 영역2 Rear 감지");
                                }
                            }
                        }
                        else if (CMD2 == 0x0A) //IO2 제어
                        {
                            if (CMD3 == 0x00) // 미션수행 프로토콜 전송
                            {
                                if (Data4 == 0x01)
                                {
                                    subconRecvEvent(HOST, "CONVWORKING");
                                    Console.WriteLine("컨베이어 미션중");
                                }
                                else if (Data4 == 0x02)
                                {
                                    subconRecvEvent(HOST, "CONVFINISH");
                                    Console.WriteLine("컨베이어 미션 완료");
                                }
                                else if (Data4 == 0x03)
                                {
                                    subconRecvEvent(HOST, "CONVERROR");
                                    Console.WriteLine("컨베이어 에러 0x03 -> 센서 또는 롤러 에러");
                                }
                                else if (Data4 == 0x04)
                                {
                                    subconRecvEvent(HOST, "CONVORDERERROR");
                                    Console.WriteLine("컨베이어 에러 0x04 -> 명령 에러 ");
                                }
                            }
                        }
                    }
                    if (CMD1 == 0xF1) // 버튼장비 or LED Display 장비
                    {
                        if (CMD2 == 0x10)
                        {
                            if (CMD3 == 0x01) // LED 점소등 관련
                            {
                                if (Data4 == 0x01) subconRecvEvent(HOST, "BOTHOFF");
                                else if (Data4 == 0x02) subconRecvEvent(HOST, "REDON");
                                else if (Data4 == 0x03) subconRecvEvent(HOST, "GREENON");
                            }
                        }
                        else if (CMD2 == 0x20) //버튼장비 -> PC
                        {
                            if (CMD3 == 0x01)
                            {
                                subconRecvEvent(HOST, "버튼장비 > PC 적색 버튼 ON");
                                byte[] data = new byte[2];  // [type , contents]
                                data[0] = 0x02;  // button
                                data[1] = 0x01;  // led red on
                                subconBtnRecvEvent(HOST, recvBuff, data);

                            }
                            else if (CMD3 == 0x02)
                            {
                                subconRecvEvent(HOST, "버튼장비 > pc 녹색 버튼 ON");
                                byte[] data = new byte[2];  // [type , contents]
                                data[0] = 0x02;  // button
                                data[1] = 0x02;  // led green on
                                subconBtnRecvEvent(HOST, recvBuff, data);

                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Subcon checksum error crc : " + string.Format("0x{0:x2}..", recv_crc) + " / " + string.Format("0x{0:x2}..", cal_crc));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Subcon_SocketRecv err ..{0}", ex.Message.ToString());
            }
        }

        public void DisConnect()
        {
            try
            {
                if (client.Connected)
                {
                    this.socketRecvEvent -= new SocketResponse(this.SocketRecv);

                    cbSock.Shutdown(SocketShutdown.Both);
                    cbSock.Close();
                    cbSock = null;

                    client = null;
                }

                bConnected = false;
                Console.WriteLine("DISCONNECT");
            }
            catch (Exception e)
            {
                Console.WriteLine("Disconnect err : " + e.Message.ToString());
            }
        }

        private byte ChkSum(byte[] buff)
        {

            byte cal_crc = 0x00;
            for (int i = 1; i < 8; i++)
                cal_crc = (byte)(cal_crc ^ buff[i]);

            return cal_crc;
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

                    client.Send(tempbyte);

                    // BeginByteSend(tempbyte, 2);
                    //BeginSend("0xff");

                    if (!client.Connected)
                    {
                        DisConnect();
                        break;
                    }

                    Thread.Sleep(2000);

                    //Console.Out.WriteLine("check");
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectCheck err :={0}", ex.Message.ToString());
            }
        }

    }//SubconSocket class end

}

