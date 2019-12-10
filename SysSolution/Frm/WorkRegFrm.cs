using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysSolution.Frm
{
    public partial class WorkRegFrm : Form
    {
        public WorkRegFrm()
        {
            InitializeComponent();
        }

        DashboardForm mainForm;

        public WorkRegFrm(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void WorkRegFrm_Load(object sender, EventArgs e)
        {
            txtMissionName.Text = "이송 작업 ";
            numericUpDown_missionlevel.Value = 1;
            chkMapList.SelectedIndex = 0;


            string[] strRobotstatus = { "true", "이송작업", "맵1", "3", "1", "500", "4시간" };//, "파레트 작업","파레트2 작업"};
            dataGridView_RegWorkList.Rows.Add(strRobotstatus);

            string[] strRobotstatus2 = { "false", "파레트 작업", "맵1", "5", "1", "500", "8시간" };//, "파레트 작업","파레트2 작업"};
            dataGridView_RegWorkList.Rows.Add(strRobotstatus2);

            string[] strRobotstatus3 = { "false", "파레트2 작업", "맵2", "5", "1", "500", "8시간" };//, "파레트 작업","파레트2 작업"};
            dataGridView_RegWorkList.Rows.Add(strRobotstatus3);

            string[] strRobotreglist = { "로봇500", "R_004", "1", "이송"};
            dataGridView_RegRobotList.Rows.Add(strRobotreglist);

            string[] strRobotreglist2 = { "로봇501", "R_005", "2", "파레트" };
            dataGridView_RegRobotList.Rows.Add(strRobotreglist2);

            string[] strRobotreglist3 = { "로봇1000", "R_006", "2", "파레트" };
            dataGridView_RegRobotList.Rows.Add(strRobotreglist3);

            string[] strRobotreglist4 = { "로봇100", "R_007", "1", "이송" };
            dataGridView_RegRobotList.Rows.Add(strRobotreglist4);


            dataGridView_WorkingRobotReg.Rows.Add(strRobotreglist2);

            dataGridView_WorkingRobotReg.Rows.Add(strRobotreglist3);

            listBox_ActionList.Items.Add("way point1");
            listBox_ActionList.Items.Add("way point2");
            listBox_ActionList.Items.Add("way point3");
            listBox_ActionList.Items.Add("lift up");
            listBox_ActionList.Items.Add("way point4");
            listBox_ActionList.Items.Add("way point5");
        }
    }
}
