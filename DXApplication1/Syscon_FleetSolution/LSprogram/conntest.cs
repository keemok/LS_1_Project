using Rosbridge.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram
{
    public partial class conntest : Form
    {
        public conntest()
        {
            InitializeComponent();
        }
        public void connection_()
        {
            ROSConnection("ws://192.168.20.28:9090");
        }
        private async void ROSConnection(string strAddr)
        {
            if (Data.Instance.isConnected)
            {
                try
                {

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
                    
                    string uri = strAddr;
                    Data.Instance.socket = new Rosbridge.Client.Socket(new Uri(uri));

                    Data.Instance.md = new MessageDispatcher(Data.Instance.socket, new MessageSerializerV2_0());
                    await Data.Instance.md.StartAsync();

                    if (Data.Instance.socket.Connected)
                    {
                    }
                    Data.Instance.isConnected = true;

                    

                    //dbBridge.onDBInsert_Alarm("Ridis 연결 완료", "Solution - Ridis간 통신 연결 상태 양호", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1);
                    //Thread th = new Thread(pingTest);
                    //th.IsBackground = true;
                    //th.Start();
                    Thread.Sleep(500);
                    //this.Visible = false;


                }
                catch (Exception ex)
                {
                    ROSDisconnect();
                    if (Data.Instance.isConnected == false)
                    {
                        //ingdlg.Hide();
                        MessageBox.Show("연결에 실패하였습니다." + ex);
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
                    this.Visible = true;
                    //connButton.Text = "connect";
                    //connButton.Enabled = true;
                }));
                Data.Instance.socket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ROSDisconnect err = " + ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ROSConnection("ws://192.168.20.28:9090");
            
        }
        public void test()
        {

        }
    }
}
