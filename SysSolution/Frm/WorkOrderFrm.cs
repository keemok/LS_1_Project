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
    public partial class WorkOrderFrm : Form
    {
        public WorkOrderFrm()
        {
            InitializeComponent();
        }

        DashboardForm mainForm;

        public WorkOrderFrm(DashboardForm frm)
        {
            mainForm = frm;
            InitializeComponent();
        }

        private void WorkOrderFrm_Load(object sender, EventArgs e)
        {
            listBox_robot.Items.Add("로봇500");
            listBox_robot.Items.Add("로봇501");
            listBox_robot.Items.Add("로봇100");
            listBox_robot.Items.Add("로봇1000");
            listBox_robot.Items.Add("로봇5003");

            radioButton_avoid.Checked = true;
            chklevelrunnig.Checked = true;


            string[] strWorklist = { "true", "이송작업", "맵1","5","1","0","5시간" };
            dataGridView_RegWorklist.Rows.Add(strWorklist);

            string[] strWorklist2 = { "false", "파레트작업", "맵1", "5", "1", "0", "5시간" };
            dataGridView_RegWorklist.Rows.Add(strWorklist2);

            string[] strWorklist3 = { "false", "파레트작업", "맵2", "4", "1", "100", "0" };
            dataGridView_RegWorklist.Rows.Add(strWorklist3);


            string[] strRobotWorkStatuslist = { "R_004", "192.168.0.11", "wait", "500", "100", "1"};
            dataGridView_WorkingRobotList.Rows.Add(strRobotWorkStatuslist);

            string[] strRobotWorkStatuslist2 = { "R_005", "192.168.0.12", "wait", "500", "110", "1" };
            dataGridView_WorkingRobotList.Rows.Add(strRobotWorkStatuslist2);

            string[] strRobotWorkStatuslist3 = { "R_006", "192.168.0.13", "job", "500", "200", "1" };
            dataGridView_WorkingRobotList.Rows.Add(strRobotWorkStatuslist3);

            string[] strRobotWorkStatuslist4 = { "R_007", "192.168.0.14", "job", "400", "300", "1" };
            dataGridView_WorkingRobotList.Rows.Add(strRobotWorkStatuslist4);

            string[] strRobotWorkStatuslist5 = { "R_008", "192.168.0.15", "job", "500", "200", "1" };
            dataGridView_WorkingRobotList.Rows.Add(strRobotWorkStatuslist5);
        }
    }
}
