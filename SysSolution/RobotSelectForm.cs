using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace SysSolution
{
    public partial class RobotSelectForm : Form
    {
        string m_strRobot_Status_File = "";

        public RobotSelectForm()
        {
            InitializeComponent();
        }

        public List<string> selectRobot = new List<string>(); 

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((Convert.ToBoolean(row.Cells[0].Value) == true))
                {
                    selectRobot.Add((row.Cells[1].Value != null) ? row.Cells[1].Value.ToString() : "");
                }
            }

            if(selectRobot.Count < 1)
            {
                MessageBox.Show("선택된 로봇이 없습니다.");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void onInit(string strfile)
        {
            m_strRobot_Status_File = strfile;
        }

        private void RobotSelectForm_Load(object sender, EventArgs e)
        {
            onRobotFile_Open();
        }

        private void onRobotFile_Open()
        {
            try
            {
                using (StreamReader sr1 = new System.IO.StreamReader(m_strRobot_Status_File, Encoding.Default))
                {
                    int ncnt = 0; //파일에 첫줄은 항목명으로 빼고 읽기 위해 선언
                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();
                        if (ncnt != 0)
                        {
                            string[] strRobotstatus = strTemp.Split(',');
                            
                            if(strRobotstatus[2]=="" && strRobotstatus[3]=="wait")
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1[1, dataGridView1.Rows.Count - 2].Value = strRobotstatus[0];
                            }

                        }
                        ncnt++;
                    }

                    sr1.Close();
                }            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotFile_Open2 err :={0}", ex.Message.ToString());
            }
            
        }

     }
}
