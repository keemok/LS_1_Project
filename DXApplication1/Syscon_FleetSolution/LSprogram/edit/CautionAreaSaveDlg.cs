using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syscon_Solution.LSprogram.edit
{
    public partial class CautionAreaSaveDlg : Form
    {
        public string areaID { get; set; }
        public string description { get; set; }


        public CautionAreaSaveDlg()
        {
            InitializeComponent();
        }
        public void onInitSet(int strrobotid)
        {
            areaTextbox.Text = "";
            descriptionTextbox.Text = "";

        }
        private void button7_Click(object sender, EventArgs e)
        {
            areaID = areaTextbox.Text.ToString();
            description = descriptionTextbox.Text.ToString();

            if (areaID.Length > 15 || areaID.Length < 0)
            {
                MessageBox.Show("Area id는 15자를 초과할 수 없습니다.");
                return;
            }
            if (String.IsNullOrWhiteSpace(areaTextbox.Text))
            {
                MessageBox.Show("Area id를 입력해주세요.");
                return;

            }
            if (descriptionTextbox.TextLength > 15 || descriptionTextbox.TextLength < 0)
            {
                MessageBox.Show("Description은 40자를 초과할 수 없습니다.");
                return;
            }
            if (string.IsNullOrWhiteSpace(descriptionTextbox.Text))
            {
                MessageBox.Show("Description을 입력해주세요.");
                return;
            }

            this.DialogResult = DialogResult.OK;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
