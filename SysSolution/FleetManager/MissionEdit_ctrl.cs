using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

using MySql.Data.MySqlClient;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace SysSolution.FleetManager
{
    public partial class MissionEdit_ctrl : UserControl
    {
        FleetManager_MainForm mainform;

        string strSelectedMissionID="";
        int nSelectedActionidx = 0;

        public MissionEdit_ctrl()
        {
            InitializeComponent();
        }

        public MissionEdit_ctrl(FleetManager_MainForm frm)
        {
            mainform = frm;
            InitializeComponent();
        }

        private void MissionEdit_ctrl_Load(object sender, EventArgs e)
        {
            mainform.worker.mapinfo_Evt += new Worker.MapInfoComplete(this.MapInfoComplete);
            mainform.worker.Localcostmapinfo_Evt += new Worker.LocalCostInfoComplete(this.LocalCostInfoComplete);
            mainform.worker.Globalpath_Evt += new Worker.GlobalpathComplete(this.GlobalpathComplete);
        }

        public void onInitSet()
        {
            onEditCtrlClear();
            onMissionListRead();

            cboRobotID.SelectedIndex = 0;
        }

        private void onEditCtrlClear()
        {
            listBox_Mission.Items.Clear();
            listBox_ActionData.Items.Clear();

            onActionOptCtrlClear(false,false,false,false);

            txtratio.Text = "1";
        }

        private void onActionOptCtrlClear(bool b1, bool b2, bool b3, bool b4)
        {
            txtGoalpoint_X.Text = "";
            txtGoalpoint_Y.Text = "";
            txtGoalpoint_theta.Text = "";

            chkmax_trans_vel.Checked = false;
            chkxy_goal_tolerance.Checked = false;
            chkyaw_goal_tolerance.Checked = false;
            chkp_drive.Checked = false;
            chkd_drive.Checked = false;
            chkwp_tolerance.Checked = false;
            chkavoid.Checked = false;

            txtmax_trans_vel.Text = "";
            txtxy_goal_tolerance.Text = "";
            txtyaw_goal_tolerance.Text = "";
            txtp_drive.Text = "";
            txtd_drive.Text = "";
            txtwp_tolerance.Text = "";
            cboavoid.SelectedIndex = 0;

            txtPalletDist.Text = "";
            txtPalletID.Text = "";
            cboPalletmode.SelectedIndex = 0;

            cboBasicmove_mode.SelectedIndex = 0;
            txtBasicmove_action.Text = "";

            txtXisWaitTime.Text = "0";


            txtGoalpoint_X.Enabled = b1;
            txtGoalpoint_Y.Enabled = b1;
            txtGoalpoint_theta.Enabled = b1;

            chkmax_trans_vel.Enabled = b1;
            chkxy_goal_tolerance.Enabled = b1;
            chkyaw_goal_tolerance.Enabled = b1;
            chkp_drive.Enabled = b1;
            chkd_drive.Enabled = b1;
            chkwp_tolerance.Enabled = b1;
            chkavoid.Enabled = b1;

            txtmax_trans_vel.Enabled = b1;
            txtxy_goal_tolerance.Enabled = b1;
            txtyaw_goal_tolerance.Enabled = b1;
            txtp_drive.Enabled = b1;
            txtd_drive.Enabled = b1;
            txtwp_tolerance.Enabled = b1;
            cboavoid.Enabled = b1;

            groupBox_basicmove.Enabled = b2;


            groupBox_lift.Enabled = b3;

            groupBox_ActionWait.Enabled = b4;

            
        }

        private void onMissionListRead()
        {
            try
            {
                mainform.ingdlg.onLblMsg("미션정보 읽어오는중...");
                mainform.ingdlg.Show();

                mainform.onDBRead_Missionlist();

                int ncnt = mainform.missionlisttable.missioninfo.Count;

                if (mainform.actiondatalitTable.Count>0)
                    mainform.actiondatalitTable.Clear();

                for (int i = 0; i < ncnt; i++)
                {
                    string strdata = "";
                    strdata = string.Format("{0}({1})", mainform.missionlisttable.missioninfo[i].strMisssionName, mainform.missionlisttable.missioninfo[i].strMisssionID);
                    listBox_Mission.Items.Add(strdata);

                    onActionListRead(mainform.missionlisttable.missioninfo[i].strMisssionID);
                }
                mainform.ingdlg.Hide();
            }
            catch(Exception ex)
            {
                Console.WriteLine("onMissionListRead err=" + ex.Message.ToString());
            }

        }
        private void onActionListRead(string strmissionid)
        {
            try
            {
               string strworkfile = "..\\Ros_info\\" + strmissionid + ".xml";
                using (StreamReader sr1 = new System.IO.StreamReader(strworkfile, Encoding.Default))
                {
                    Action_Data_List actiondatalist = new Action_Data_List();
                    actiondatalist.action_data = new List<Action_Data>();

                    while (sr1.Peek() >= 0)
                    {
                        string strTemp = sr1.ReadLine();

                        if (strTemp.IndexOf('<') < 0 && strTemp != "")
                        {
                            Action_Data actiondata = new Action_Data();

                            string[] straction_sub = strTemp.Split('/');

                            actiondata.strWorkData = strTemp;
                            if(straction_sub.Length==1)
                                actiondata.strActionType = "";
                            else actiondata.strActionType = straction_sub[0].Split(':')[1];

                            actiondatalist.action_data.Add(actiondata);
                        }
                    }
                    sr1.Close();

                    if (actiondatalist.action_data.Count > 0)
                        mainform.actiondatalitTable.Add(strmissionid, actiondatalist);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onActionListRead err=" + ex.Message.ToString());
            }
        }


        private void listBox_Mission_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox_Mission.SelectedIndex > -1)
                {
                    int nselectedidx = listBox_Mission.SelectedIndex;

                    strSelectedMissionID = mainform.missionlisttable.missioninfo[nselectedidx].strMisssionID;
                    nSelectedActionidx = 0;

                    if (mainform.actiondatalitTable.ContainsKey(strSelectedMissionID))
                    {
                        listBox_ActionData.Items.Clear();
                        onActionOptCtrlClear(false, false, false,false);

                        int cnt =mainform.actiondatalitTable[strSelectedMissionID].action_data.Count;

                        for(int i=1; i<cnt; i++)
                        {
                            string strtype = mainform.actiondatalitTable[strSelectedMissionID].action_data[i].strActionType;
                            listBox_ActionData.Items.Add(string.Format("Action({0})", strtype));
                        }

                        pb_map.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("listBox_Mission_SelectedIndexChanged err=" + ex.Message.ToString());
            }
        }

        private void listBox_ActionData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox_ActionData.SelectedIndex > -1)
                {
                    int nselectedidx = listBox_ActionData.SelectedIndex;

                    nSelectedActionidx = nselectedidx+1;

                    onActionDataDP(nselectedidx + 1);
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("listBox_ActionData_SelectedIndexChanged err=" + ex.Message.ToString());
            }
        }

        public void onActionDataDP(int nselectedidx)
        {
            string stractiontype = mainform.actiondatalitTable[strSelectedMissionID].action_data[nselectedidx].strActionType;
            string stractiondata = mainform.actiondatalitTable[strSelectedMissionID].action_data[nselectedidx].strWorkData;

            string[] strgoal_sub = stractiondata.Split('/');

            if (stractiontype.Equals("Goal-Point"))
            {
                onActionOptCtrlClear(true, false, false, false);
                string[] strgoal_sub_act_param = strgoal_sub[1].Split(':');
                txtGoalpoint_X.Text = (strgoal_sub_act_param[1]);

                strgoal_sub_act_param = strgoal_sub[2].Split(':');
                txtGoalpoint_Y.Text = (strgoal_sub_act_param[1]);

                strgoal_sub_act_param = strgoal_sub[3].Split(':');
                txtGoalpoint_theta.Text = (strgoal_sub_act_param[1]);

                for (int i = 5; i < strgoal_sub.Length; i++)
                {
                    string[] strgoal_sub_params = strgoal_sub[i].Split(':');
                    if (strgoal_sub_params[0] == "max_trans_vel")
                    {
                        chkmax_trans_vel.Checked = true;
                        txtmax_trans_vel.Text = strgoal_sub_params[1];
                    }
                    else if (strgoal_sub_params[0] == "xy_goal_tolerance")
                    {
                        chkxy_goal_tolerance.Checked = true;
                        txtxy_goal_tolerance.Text = strgoal_sub_params[1];

                    }
                    else if (strgoal_sub_params[0] == "yaw_goal_tolerance")
                    {
                        chkyaw_goal_tolerance.Checked = true;
                        txtyaw_goal_tolerance.Text = strgoal_sub_params[1];
                    }
                    else if (strgoal_sub_params[0] == "p_drive")
                    {
                        chkp_drive.Checked = true;
                        txtp_drive.Text = strgoal_sub_params[1];
                    }
                    else if (strgoal_sub_params[0] == "d_drive")
                    {
                        chkd_drive.Checked = true;
                        txtd_drive.Text = strgoal_sub_params[1];
                    }
                    else if (strgoal_sub_params[0] == "wp_tolerance")
                    {
                        chkwp_tolerance.Checked = true;
                        txtwp_tolerance.Text = strgoal_sub_params[1];
                    }
                    else if (strgoal_sub_params[0] == "avoid")
                    {
                        chkavoid.Checked = true;
                        if (strgoal_sub_params[1] == "false")
                            cboavoid.SelectedIndex = 0;
                        else cboavoid.SelectedIndex = 1;
                    }
                }

            }
            else if (stractiontype.Equals("Basic-Move"))
            {
                onActionOptCtrlClear(false, true, false, false);
                string[] strmode = strgoal_sub[1].Split(':');
                string[] stract = strgoal_sub[2].Split(':');

                if (strmode[1] == "Go") cboBasicmove_mode.SelectedIndex = 0;
                else cboBasicmove_mode.SelectedIndex = 1;

                txtBasicmove_action.Text = stract[1];
            }
            else if (stractiontype.Equals("Stable_pallet"))
            {
                onActionOptCtrlClear(false, false, true, false);
                string[] strmode = strgoal_sub[1].Split(':');
                string[] strdist = strgoal_sub[2].Split(':');
                string[] strforwardbackward = strgoal_sub[3].Split(':');
                string[] strpalletid = strgoal_sub[4].Split(':');

                if (strmode[1] == "-1") cboPalletmode.SelectedIndex = 0;
                else if (strmode[1] == "0") cboPalletmode.SelectedIndex = 1;
                else if (strmode[1] == "1") cboPalletmode.SelectedIndex = 2;

                txtPalletDist.Text = strdist[1];

                if (strforwardbackward[1] == "1") radioButton_forward.Checked = true;
                else radioButton_backward.Checked = true;

                txtPalletID.Text = strpalletid[1];
            }
            else if (stractiontype.Equals("Action_wait"))
            {
                onActionOptCtrlClear(false, false, false, true);
                string[] strmode = strgoal_sub[1].Split(':');
                string[] strXisID = strgoal_sub[2].Split(':');

                if (strmode[1] == "0") cboXisWait_mode.SelectedIndex = 0;
                else
                {
                    txtXisWaitTime.Text = strmode[1];
                    cboXisWait_mode.SelectedIndex = 1;
                }

                txtXisWait_ID.Text = strXisID[1];
            }
        }

        private void btnMissionSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "" && nSelectedActionidx > -1)
                {
                    mainform.ingdlg.onLblMsg("정보 저장중...");
                    mainform.ingdlg.Show();

                    //action list table update ,onActionList_Save()
                    string stractiontype = mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx].strActionType;
                    string stractdata = "";
                    if (stractiontype.Equals("Goal-Point"))
                    {
                        string strdata = "";
                        stractdata = "type:Goal-Point";

                        strdata = txtGoalpoint_X.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/x:{0}", strdata);

                        strdata = txtGoalpoint_Y.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/y:{0}", strdata);

                        strdata = txtGoalpoint_theta.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/theta:{0}", strdata);

                        stractdata += "/qual:C";

                        if (chkmax_trans_vel.Checked)
                        {
                            strdata = txtmax_trans_vel.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/max_trans_vel:{0}", strdata);
                        }

                        if (chkxy_goal_tolerance.Checked)
                        {
                            strdata = txtxy_goal_tolerance.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/xy_goal_tolerance:{0}", strdata);
                        }

                        if (chkyaw_goal_tolerance.Checked)
                        {
                            strdata = txtyaw_goal_tolerance.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/yaw_goal_tolerance:{0}", strdata);
                        }

                        if (chkp_drive.Checked)
                        {
                            strdata = txtp_drive.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/p_drive:{0}", strdata);
                        }

                        if (chkd_drive.Checked)
                        {
                            strdata = txtd_drive.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/d_drive:{0}", strdata);
                        }

                        if (chkwp_tolerance.Checked)
                        {
                            strdata = txtwp_tolerance.Text.ToString();
                            if (strdata == "") strdata = "0";
                            stractdata += string.Format("/wp_tolerance:{0}", strdata);
                        }

                        if (chkavoid.Checked)
                        {
                            if (cboavoid.SelectedIndex == 0) strdata = "false";
                            else strdata = "true";

                            stractdata += string.Format("/avoid:{0}", strdata);
                        }


                    }
                    else if (stractiontype.Equals("Basic-Move"))
                    {
                        string strdata = "";
                        stractdata = "type:Basic-Move";

                        if (cboBasicmove_mode.SelectedIndex == 0) strdata = "Go";
                        else strdata = "Rotate";
                        stractdata += string.Format("/mode:{0}", strdata);

                        strdata = txtBasicmove_action.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/target:{0}", strdata);
                    }
                    else if (stractiontype.Equals("Stable_pallet"))
                    {
                        string strdata = "";
                        stractdata = "type:Stable_pallet";

                        if (cboPalletmode.SelectedIndex == 0) strdata = "-1";
                        else if (cboPalletmode.SelectedIndex == 1) strdata = "0";
                        else if (cboPalletmode.SelectedIndex == 2) strdata = "1";
                        stractdata += string.Format("/mode:{0}", strdata);

                        strdata = txtPalletDist.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/dist_to_pallet:{0}", strdata);

                        if(radioButton_forward.Checked) stractdata += string.Format("/bf_mode:1");
                        else stractdata += string.Format("/bf_mode:-1");

                        strdata = txtPalletID.Text.ToString();
                        if (strdata == "") strdata = "0";
                        stractdata += string.Format("/pal_check:{0}", strdata);

                    }
                    else if (stractiontype.Equals("Action_wait"))
                    {
                        string strdata = "";
                        stractdata = "type:Action_wait";
                        string waittime = txtXisWaitTime.Text.ToString();
                        string xisid = txtXisWait_ID.Text.ToString();
                        if(cboXisWait_mode.SelectedIndex==0)
                        {
                            stractdata += string.Format("/mode:0");

                        }
                        else stractdata += string.Format("/mode:{0}", waittime);

                        stractdata += string.Format("/XID:{0}", xisid);
                    }

                     if (stractdata != "")
                    {
                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx].strWorkData = stractdata;

                        onMissionFile_Save(strSelectedMissionID);
                    }

                    mainform.ingdlg.Hide();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnMissionSave_Click err=" + ex.Message.ToString());
            }
        }

        private void onMissionFile_Save(string strMissionID)
        {
            try
            {
                //mission file update , onMissionSave()
                string strworkfile = "..\\Ros_info\\" + strMissionID + ".xml";
                using (StreamWriter sw = new System.IO.StreamWriter(strworkfile, false, Encoding.Default))
                {
                    int cnt = mainform.actiondatalitTable[strMissionID].action_data.Count;
                    string strdata = "";
                    strdata = string.Format("<{0}>", strMissionID);
                    sw.WriteLine(strdata);

                    sw.WriteLine("<name>");
                    strdata = mainform.actiondatalitTable[strMissionID].action_data[0].strWorkData;
                    sw.WriteLine(strdata);
                    sw.WriteLine("</name>");

                    for (int i = 1; i < cnt; i++)
                    {
                        string stracttype = mainform.actiondatalitTable[strMissionID].action_data[i].strActionType;
                        strdata = mainform.actiondatalitTable[strMissionID].action_data[i].strWorkData;
                        sw.WriteLine(string.Format("<{0}>", stracttype));
                        sw.WriteLine(strdata);
                        sw.WriteLine(string.Format("</{0}>", stracttype));
                    }

                    strdata = string.Format("</{0}>", strMissionID);
                    sw.WriteLine(strdata);
                    sw.Close();
                }

                onDBSave_MissionInfo(strMissionID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("onMissionFile_Save err=" + ex.Message.ToString());
            }

        }

        private void onDBSave_MissionInfo(string strmissionid)
        {
            try
            {
                int nactcnt = mainform.actiondatalitTable[strmissionid].action_data.Count;
                if(nactcnt > 0)
                {
                    DB_MissionData db_missiondata = new DB_MissionData();

                    //db_missiondata에 넣고
                    string[] strActdata_buf = new string[nactcnt-1];
                    for (int i = 1; i < nactcnt; i++)
                    {
                        strActdata_buf[i-1] = mainform.actiondatalitTable[strmissionid].action_data[i].strWorkData;
                    }

                    db_missiondata = onDB_MissionData_Json_Make(strActdata_buf);

                    // json 묶고
                    string strMissionData_Json = JsonConvert.SerializeObject(db_missiondata);


                    //DB 에 저장
                    mainform.onDBUpdate_Missionlist(strmissionid, strMissionData_Json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onDBSave_MissionInfo err=" + ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e) //미션DB에서 액션정보 읽어오기 테스트
        {
            string strid = mainform.missionlisttable.missioninfo[0].strMisssionID;
            string strwork = mainform.missionlisttable.missioninfo[0].work;

            DB_MissionData db_missiondata = new DB_MissionData();
            db_missiondata = JsonConvert.DeserializeObject<DB_MissionData>(strwork);
            WAS_GOAL work_data = new WAS_GOAL();

            work_data.goal.work_id = "1";
            work_data.goal.action_start_idx = 0;
            work_data.goal.loop_flag = 1;

            for(int i=0; i<db_missiondata.work.Count; i++)
            {
                work_data.goal.work.Add(db_missiondata.work[i]);
            }

        }


        public DB_MissionData onDB_MissionData_Json_Make(string[] strData_array)
        {
            DB_MissionData db_missiondata = new DB_MissionData();

            for (int ndataIdx = 0; ndataIdx < strData_array.Length; ndataIdx++) //작업내용만큼 반복
            {
                Action act = new Action();
                List<ParameterSet> listparam = new List<ParameterSet>();
                ParameterSet param;

                string strgoal = "";// "type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3";

                strgoal = strData_array[ndataIdx];

                string[] strgoal_sub = strgoal.Split('/');

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

                            int nparamidx = 0;
                            for (int i = 5; i < strgoal_sub.Length; i++)
                            {
                                param = new ParameterSet();
                                string[] strgoal_sub_params = strgoal_sub[i].Split(':');
                                param.param_name = strgoal_sub_params[0];

                                param.value = strgoal_sub_params[1];

                                if (strgoal_sub_params[1] == "true" || strgoal_sub_params[1] == "false")
                                    param.type = "bool";
                                else
                                    param.type = "float";

                                nparamidx++;

                                listparam.Add(param);
                            }
                            act.action_params = listparam;


                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("Basic-Move"))
                        {
                            act.action_type = (int)Data.ACTION_TYPE.Basic_Move;

                            //mode = 0(직진), 1(회전)
                            //target = 거리 or  회전각도
                            string[] strmode = strgoal_sub[1].Split(':');  //ex->mode: Go  
                            string[] strtarget = strgoal_sub[2].Split(':');//ex->target:10

                            if (strmode[1] == Data.Instance.MoveList[0])
                            {
                                act.action_args.Add(0);
                            }
                            else
                            {
                                act.action_args.Add(1);
                            }
                            act.action_args.Add(float.Parse(strtarget[1]));

                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                        {
                            act.action_type = (int)Data.ACTION_TYPE.Lift_Conveyor_Control;

                            //mode = 0(lift top or bottom), 1(lift 높이 up), 2(conveyor transfer), 3(conveyor receive)
                            /*action =  mode 0 (1 : top, -1 : bottom)
                                        mode 1 (원하는 높이)
                                        mode 2 (1 : forward, -1 : backward)
                                        mode 3 (1 : forward, -1 : backward)
                            */
                            string[] strmode = strgoal_sub[1].Split(':');  //ex->mode:Top-Bottom  
                            string[] straction = strgoal_sub[2].Split(':');//ex->action:top

                            if (strmode[1] == Data.Instance.LiftActList[0])  //Top-Bottom
                            {
                                act.action_args.Add(0);
                                if (straction[1] == Data.Instance.LiftDetailActList[0])
                                {
                                    act.action_args.Add(1);
                                }
                                else
                                {
                                    act.action_args.Add(-1);
                                }
                            }
                            else if (strmode[1] == Data.Instance.LiftActList[1]) //Set-Height
                            {
                                act.action_args.Add(1);
                                act.action_args.Add(float.Parse(straction[1]));
                            }
                            else if (strmode[1] == Data.Instance.ConvActList[1]) //Transfer
                            {
                                act.action_args.Add(2);
                                if (straction[1] == Data.Instance.ConvDetailActList[0])
                                {
                                    act.action_args.Add(1);
                                }
                                else
                                {
                                    act.action_args.Add(-1);
                                }
                            }
                            else if (strmode[1] == Data.Instance.ConvActList[2]) //Receiver
                            {
                                act.action_args.Add(3);
                                if (straction[1] == Data.Instance.ConvDetailActList[0])
                                {
                                    act.action_args.Add(1);
                                }
                                else
                                {
                                    act.action_args.Add(-1);
                                }
                            }

                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("Chain"))
                        {
                            act.action_type = (int)Data.ACTION_TYPE.CHAIN;

                            string[] strgoal_sub_act_param = strgoal_sub[1].Split(':');

                            string strchaintoRobotid = strgoal_sub_act_param[1];

                            param = new ParameterSet();

                            param.param_name = strgoal_sub_act_param[0];

                            param.type = "string";

                            param.value = strgoal_sub_act_param[1];

                            act.action_params.Add(param);

                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("URMISSION"))
                        {
                            //mode = 0(ur_switch), 1(ur_run)
                            /*count number =  mode 1 일때 사용하고.. 반복횟수
                            */
                            act.action_type = (int)Data.ACTION_TYPE.UR_MISSION;

                            string[] strmode = strgoal_sub[1].Split(':');
                            string[] strcountnumber = strgoal_sub[2].Split(':');


                            if (strmode[1] == "0")
                            {
                                act.action_args.Add(0);
                                act.action_args.Add(1);
                            }
                            else if (strmode[1] == "1")
                            {
                                act.action_args.Add(1);
                                act.action_args.Add(float.Parse(strcountnumber[1]));
                            }

                            int nparamidx = 0;
                            for (int i = 3; i < strgoal_sub.Length; i++)
                            {
                                param = new ParameterSet();
                                string[] strgoal_sub_params = strgoal_sub[i].Split(':');

                                param.param_name = strgoal_sub_params[0];
                                param.value = strgoal_sub_params[1];
                                param.type = "string";

                                nparamidx++;

                                listparam.Add(param);
                            }
                            act.action_params = listparam;
                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("Stable_pallet"))
                        {
                            //mode = 0
                            /*dist_to_pallet = 1.1 거치대까지 거리. 
                            */
                            act.action_type = (int)Data.ACTION_TYPE.Stable_Pallet;

                            string[] strmode = strgoal_sub[1].Split(':');
                            string[] strdist = strgoal_sub[2].Split(':');
                            string[] strforward = strgoal_sub[3].Split(':');


                            act.action_args.Add(float.Parse(strmode[1]));
                            act.action_args.Add(float.Parse(strdist[1]));
                            act.action_args.Add(float.Parse(strforward[1]));

                            int nparamidx = 0;
                            for (int i = 3; i < strgoal_sub.Length; i++)
                            {
                                param = new ParameterSet();
                                string[] strgoal_sub_params = strgoal_sub[i].Split(':');

                                param.param_name = strgoal_sub_params[0];
                                param.value = strgoal_sub_params[1];
                                param.type = "string";

                                nparamidx++;

                                listparam.Add(param);
                            }
                            act.action_params = listparam;
                        }
                        else if (strgoal_sub[0].Split(':')[1].Equals("Action_wait"))
                        {
                            //mode = 0
                            /*dist_to_pallet = 1.1 거치대까지 거리. 
                            */
                            act.action_type = (int)Data.ACTION_TYPE.Action_wait;

                            string[] strmode = strgoal_sub[1].Split(':');
                            string[] strdata = strgoal_sub[2].Split(':');

                            if (strmode[1] == "0")
                                act.action_args.Add(0);
                            else act.action_args.Add(float.Parse(strmode[1]));

                        }

                        db_missiondata.work.Add(act);
                    }
                }
            }

            return db_missiondata;
        }

        private void btnMissionInsert_Click(object sender, EventArgs e)
        {
            try
            {
                MissionInsertDlg missiondlg = new MissionInsertDlg();
                if(missiondlg.ShowDialog()==DialogResult.OK)
                {
                    string missionname = missiondlg.strMissionName;
                    string missionid = missiondlg.strMissionID;
                    string missionlevel = missiondlg.strMissionLevel;

                    //미션리스트 DB저장
                    mainform.onDBInsert_Missionlist(missionname, missionid, missionlevel);

                    //기본 액션으로 미션 파일 저장
                    string strworkfile = "..\\Ros_info\\" + missionid + ".xml";
                    using (StreamWriter sw = new System.IO.StreamWriter(strworkfile, false, Encoding.Default))
                    {
                        string strdata = "";
                        strdata = string.Format("<{0}>", missionid);
                        sw.WriteLine(strdata);

                        sw.WriteLine("<name>");
                        sw.WriteLine(missionname);
                        sw.WriteLine("</name>");
                        sw.WriteLine(string.Format("<Goal-Point>"));
                        sw.WriteLine("type:Goal-Point/x:0/y:0/theta:0/qual:C/max_trans_vel:0.7/xy_goal_tolerance:0.15/yaw_goal_tolerance:0.05/p_drive:0.4/d_drive:1.2/wp_tolerance:1/avoid:false");
                        sw.WriteLine(string.Format("</Goal-Point>"));
                        strdata = string.Format("</{0}>", missionid);
                        sw.WriteLine(strdata);
                        sw.Close();
                    }

                    //화면 갱신
                    onInitSet();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnMissionInsert_Click err=" + ex.Message.ToString());
            }
        }

        private void btnMissionDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "")
                {
                    string strMsg =string.Format("미션ID: {0}  삭제하시겠습까?",strSelectedMissionID);

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        //미션리스트DB 삭제
                        mainform.onDBDelete_Missionlist(strSelectedMissionID);

                        //미션파일 삭제..
                        string strworkfile = "..\\Ros_info\\" + strSelectedMissionID + ".xml";
                        if(File.Exists(strworkfile))
                        {
                            File.Delete(strworkfile);
                        }

                        //화면 재갱신
                        onInitSet();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnMissionDelete_Click err=" + ex.Message.ToString());
            }
        }

        private void btnActionInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "")
                {
                    ActionInsertDlg actiondlg = new ActionInsertDlg();
                    if (actiondlg.ShowDialog() == DialogResult.OK)
                    {
                        string stractiontype = actiondlg.strActiontype;
                        string stractdata = "";
                        if(stractiontype.Equals("Goal-Point"))
                        {
                            stractdata = "type:Goal-Point/x:0/y:0/theta:0/qual:C/max_trans_vel:0.7/xy_goal_tolerance:0.15/yaw_goal_tolerance:0.05/p_drive:0.4/d_drive:1.2/wp_tolerance:1/avoid:false";
                        }
                        else if (stractiontype.Equals("Basic-Move"))
                        {
                            stractdata = "type:Basic-Move/mode:Go/target:1";
                        }
                        else if (stractiontype.Equals("Stable_pallet"))
                        {
                            stractdata = "type:Stable_pallet/mode:-1/dist_to_pallet:1/bf_mode:1/pal_check:11411";
                        }
                        else if (stractiontype.Equals("Action_wait"))
                        {
                            stractdata = "type:Action_wait/mode:1/XID:1";
                        }
                        //액션리스트 테이블 추가
                        int cnt = mainform.actiondatalitTable[strSelectedMissionID].action_data.Count;
                        Action_Data actiondata = new Action_Data();
                        actiondata.strActionType = stractiontype;
                        actiondata.strWorkData = stractdata;
                        mainform.actiondatalitTable[strSelectedMissionID].action_data.Add(actiondata);

                        //미션파일 저장
                        onMissionFile_Save(strSelectedMissionID);
                        //화면 갱신
                        onInitSet();
                    }
                }
                else
                {
                    MessageBox.Show("액션을 추가할 미션을 선택하세요.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnActionInsert_Click err=" + ex.Message.ToString());
            }
        }

        private void btnActionDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "")
                {
                    string strMsg = string.Format("액션 : {0}  삭제하시겠습까?", nSelectedActionidx);

                    if (DialogResult.OK == MessageBox.Show(strMsg, "확인", MessageBoxButtons.OKCancel))
                    {
                        mainform.actiondatalitTable[strSelectedMissionID].action_data.RemoveAt(nSelectedActionidx);

                        //미션파일 저장
                        onMissionFile_Save(strSelectedMissionID);
                        //화면 갱신
                        onInitSet();
                    }
                }
                else
                {
                    MessageBox.Show("액션을 삭제할 미션을 선택하세요.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnActionDelete_Click err=" + ex.Message.ToString());
            }
        }

        private void btnActionidxUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "")
                {
                    if (nSelectedActionidx > 0 && nSelectedActionidx <= listBox_ActionData.Items.Count)
                    {
                        Action_Data act_data = mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx];
                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx] = mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx - 1];
                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx - 1] = act_data;

                        int listidx = nSelectedActionidx - 1;
                        object listitem = listBox_ActionData.Items[listidx];
                        listBox_ActionData.Items[listidx] = listBox_ActionData.Items[listidx-1];
                        listBox_ActionData.Items[listidx - 1] = listitem;
                        //미션파일 저장
                        onMissionFile_Save(strSelectedMissionID);

                        listBox_ActionData.SelectedIndex = nSelectedActionidx - 2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnActionidxUp_Click err=" + ex.Message.ToString());
            }
        }

        private void btnActionidxDn_Click(object sender, EventArgs e)
        {
            try
            {
                if (strSelectedMissionID != "")
                {
                    if (nSelectedActionidx > 0 && nSelectedActionidx <= listBox_ActionData.Items.Count-1)
                    {
                       Action_Data act_data = mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx];
                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx] = mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx + 1];
                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nSelectedActionidx + 1] = act_data;

                        int listidx = nSelectedActionidx - 1;
                        object listitem = listBox_ActionData.Items[listidx];
                        listBox_ActionData.Items[listidx] = listBox_ActionData.Items[listidx + 1];
                        listBox_ActionData.Items[listidx + 1] = listitem;
                        //미션파일 저장
                        onMissionFile_Save(strSelectedMissionID);

                        listBox_ActionData.SelectedIndex = nSelectedActionidx;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("btnActionidxDn_Click err=" + ex.Message.ToString());
            }
        }


        

        #region map 관련
        string m_strRobotName = "R_006";

        float resoultion1 = (float)0.025;
        float ori_x = -10;
        float ori_y = (float)-10;
        byte[] sourceMapValues;
        int nSourceMapWidth = 0;
        int nSourceMapHeight = 0;

        float resoultion_localcost = (float)0.1;
        float ori_x_localcost = (float)0.8;//(float)2.1;
        float ori_y_localcost = (float)-0.3;//(float)-0.8;
        byte[] sourceMaplocalcostValues;
        int nSourceMap_localcostWidth = 0;
        int nSourceMap_localcostHeight = 0;
        float ratio_localcost = 4;
        Image imgcostmap;
        Dictionary<string, Image> imgcostmaplist = new Dictionary<string, Image>();

        float dOrignX = 0;
        float dOrignY = 0;

        bool bMaploading = false;
        bool bcostmaploading = false;
        bool bMapMouseDN = false;
        bool bPosRectSelect = false;
        int nPosRectidx = 0;
        float ratio = 1;

        float translate_x = 0;
        float translate_y = 0;

        List<RectangleF> waringRectList = new List<RectangleF>();
        List<WarningRegionClass> waringPointList = new List<WarningRegionClass>();

        List<Rectangle> posRectList = new List<Rectangle>();
        Rectangle RectTmp = new Rectangle();

        float W_x_1;
        float W_y_1;
        float W_x_2;
        float W_y_2;

        double[] robot_globalpath_x= new double[100];
        double[] robot_globalpath_y = new double[100];
        double[] robot_path_x = new double[10000];
        double[] robot_path_y = new double[10000];
        long nrobotpathidx = 0;

        private void btnMaploading_Click(object sender, EventArgs e)
        {
             if (Data.Instance.isConnected)
            {
                timer1.Interval = 500;
                timer1.Start();

                m_strRobotName = cboRobotID.SelectedItem.ToString();

                onSelectRobotMap_monitor(m_strRobotName);
            }
        }

        private void btnCostmap_Click(object sender, EventArgs e)
        {
            if (Data.Instance.isConnected)
            {
                for (int idx = 0; idx < mainform.G_robotList.Count; idx++)
                {
                    onSelectRobotLocalCostMap_monitor(mainform.G_robotList[idx]);
                }
            }
        }

        public void onSelectRobotMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    mainform.worker.onSelectRobotMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        public void onSelectRobotLocalCostMap_monitor(string strrobotid)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    mainform.worker.onSelectRobotLocalCostMap_monitor_subscribe(strrobotid);
                    Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("onSelectRobotLocalCostMap_monitor err=" + ex.Message.ToString());
                }
            }
        }

        public void MapInfoComplete()
        {
            try
            {
                int width = 0;
                int height = 0;
                double resolution = 0;
                width = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.width;
                height = Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.height;
                resoultion1 = (float)Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.resolution;
                ori_x =  (float)Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.origin.position.x;
                ori_y = (float)Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.info.origin.position.y;

                nSourceMapWidth = width;
                nSourceMapHeight = height;

                sourceMapValues = new byte[width * height];

                for (var y = 0; y < width * height; y++)
                {
                    sourceMapValues[y] = (byte)(Data.Instance.Robot_work_info[m_strRobotName].robot_status_info.mapinfo.msg.data[y]);
                }

                onMapDisplay1();
       
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("MapInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        public void LocalCostInfoComplete()
        {
            try
            {
                if (bMaploading)
                {
                    for( int i=0; i<mainform.G_robotList.Count; i++)
                    {
                        string strrobotname = mainform.G_robotList[i];

                        if (Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg == null) continue;

                        int width = 0;
                        int height = 0;
                        double resolution = 0;
                        width = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.width;
                        height = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.height;
                        resolution = Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.resolution;
                        Data.Instance.Robot_work_info[strrobotname].costmap_originX = (float)Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.origin.position.x;
                        Data.Instance.Robot_work_info[strrobotname].costmap_originY = (float)Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.info.origin.position.y;
                        nSourceMap_localcostWidth = width;
                        nSourceMap_localcostHeight = height;


                        sourceMaplocalcostValues = new byte[width * height];

                        for (var y = 0; y < width * height; y++)
                        {
                            sourceMaplocalcostValues[y] = (byte)(Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg.data[y]);
                        }

                        Bitmap bmSource = new Bitmap(nSourceMap_localcostWidth, nSourceMap_localcostHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                        Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMaplocalcostValues, "cost");

                        Image imgSource_Chg = ZoomIn(bmSource, resolution / resoultion1);

                        Data.Instance.Robot_work_info[strrobotname].costmap = (Image)imgSource_Chg.Clone(); 

                    }

                    bcostmaploading = true;

                    onMapDisplay1();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("LocalCostInfoComplete err :={0}", ex.Message.ToString());
            }
        }

        private void GlobalpathComplete(string strrobotid)
        {
          /*  if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg != null)
            {
                if (Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses.Count > 0)
                {
                    List<PoseStamped> pose = new List<PoseStamped>();
                    pose = Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.msg.poses;

                    robot_path_x = new double[1000];
                    robot_path_y = new double[1000];
                    nrobotpathidx = 0;
                }
            }
            */
        }

        public void onMapDisplay1()
        {
            try
            {
                string path = "..\\Ros_info\\map\\mapinfo.bmp";
                int width = 0, height = 0;
              
                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");


                //   dOrignX = ((ori_x*-1) / resoultion1)* ratio + translate_x;
                //   dOrignY = ((ori_y) / resoultion1)* ratio - translate_y;

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;
                dOrignY = nSourceMapHeight - dOrignY;


                bMaploading = true;

                if (bcostmaploading)
                {
                    Bitmap bmcost = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);
                    Graphics g2 = Graphics.FromImage(bmcost);

                    for (int ii = 0; ii < mainform.G_robotList.Count; ii++)
                    {
                        string strrobotname = mainform.G_robotList[ii];

                        if (Data.Instance.Robot_work_info[strrobotname].robot_status_info.localcostmap.msg == null) continue;

                        float cellX1 = Data.Instance.Robot_work_info[strrobotname].costmap_originX / resoultion1;
                        float cellY1 = Data.Instance.Robot_work_info[strrobotname].costmap_originY / resoultion1;

                        PointF pos = new PointF();
                        pos.X = dOrignX + cellX1;
                        pos.Y = dOrignY - cellY1;

                        Bitmap costmap_temp =(Bitmap) Data.Instance.Robot_work_info[strrobotname].costmap;

                        Rectangle cost_map = new Rectangle(0, 0, costmap_temp.Width, costmap_temp.Height);
                        System.Drawing.Imaging.BitmapData bmpData_costmap =
                           costmap_temp.LockBits(cost_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                           costmap_temp.PixelFormat);

                        IntPtr ptr_costmap = bmpData_costmap.Scan0;
                        byte[] sourceMapValues_costmap = new byte[costmap_temp.Width * costmap_temp.Height * 4];
                        {
                            System.Runtime.InteropServices.Marshal.Copy(ptr_costmap, sourceMapValues_costmap, 0, costmap_temp.Width * costmap_temp.Height * 4);
                        }

                        costmap_temp.UnlockBits(bmpData_costmap);
                        int pos_cost = 0;
                        for (int iy=0; iy<costmap_temp.Height; iy++)
                        {
                            for (int ix = 0; ix < costmap_temp.Width; ix++)
                            {
                                int color1 = (int)sourceMapValues_costmap[pos_cost];
                                int color2 = (int)sourceMapValues_costmap[pos_cost + 1];
                                int color3 = (int)sourceMapValues_costmap[pos_cost + 2];
                                int color4 = (int)sourceMapValues_costmap[pos_cost + 3];

                                if (color1 == 0 && color2 == 0 && color3 == 0) { }
                                else if (color1 == 255 && color2 == 255 && color3 == 255) { }
                                else if (color1 == 0x80 || color2 == 0x80 || color3 == 0x80 || color4 == 0x80)
                                {
                                }
                                else if (color1 == 0xbe || color2 == 0xbe || color3 == 0xbe || color4 == 0xbf)
                                {
                                }
                                else if (color1 == 0x40 || color2 == 0x40 || color3 == 0x40 || color4 == 0x40)
                                {
                                }
                                else
                                {
                                    bmcost.SetPixel((int)pos.X + ix, (int)(pos.Y - costmap_temp.Height + iy), Color.FromArgb((int)sourceMapValues_costmap[pos_cost], (int)sourceMapValues_costmap[pos_cost + 1], (int)sourceMapValues_costmap[pos_cost + 2]));
                                }
                                pos_cost += 4;
                            }
                        }
                       // bmcost.SetPixel()
                        //g2.DrawImage(costmap_temp, (int)pos.X, (int)pos.Y - costmap_temp.Height, costmap_temp.Width, costmap_temp.Height);
                    }

                    
                  
                    Rectangle r_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_map =
                       bmSource.LockBits(r_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmSource.PixelFormat);

                    IntPtr ptr_map = bmpData_map.Scan0;
                    byte[] sourceMapValues_map = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_map, sourceMapValues_map, 0, nSourceMapWidth * nSourceMapHeight * 4);
                    }

                    bmSource.UnlockBits(bmpData_map);

                    Rectangle r1 = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_r1 =
                       bmcost.LockBits(r1, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmcost.PixelFormat);

                    IntPtr ptr_r1 = bmpData_r1.Scan0;
                    byte[] sourceMapValues_r1 = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_r1, sourceMapValues_r1, 0, bmcost.Width * bmcost.Height * 4);
                    }
                    bmcost.UnlockBits(bmpData_r1);

                    int cnt = 0;
                    for (int i = 0; i < nSourceMapWidth * nSourceMapHeight * 4; i++)
                    {
                        if (sourceMapValues_r1[i] == 0x00)
                        {

                        }
                        else if (sourceMapValues_r1[i] == 0xff)
                        {

                        }
                        else if (sourceMapValues_r1[i] == 0x80 || sourceMapValues_r1[i + 1] == 0x80 || sourceMapValues_r1[i + 2] == 0x80 || sourceMapValues_r1[i + 3] == 0x80)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0xbe || sourceMapValues_r1[i + 1] == 0xbe || sourceMapValues_r1[i + 2] == 0xbe || sourceMapValues_r1[i + 3] == 0xbf)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0x40 || sourceMapValues_r1[i + 1] == 0x40 || sourceMapValues_r1[i + 2] == 0x40 || sourceMapValues_r1[i + 3] == 0x40)
                        {
                            i += 4;
                        }
                        else
                        {
                            {
                                sourceMapValues_map[i] = (byte)(sourceMapValues_r1[i]);
                            }
                        }

                    }



                    Rectangle or_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_ormap =
                       bmMergeOKSource.LockBits(or_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmMergeOKSource.PixelFormat);
                    IntPtr ptr_or = bmpData_ormap.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues_map, 0, ptr_or, nSourceMapWidth * nSourceMapHeight * 4);

                    bmMergeOKSource.UnlockBits(bmpData_ormap);

                    g2.Dispose();
                }

                dOrignX = dOrignX * ratio + translate_x;
                dOrignY = dOrignY * ratio + translate_y;

                Image imgSource_Chg;

                if (bcostmaploading)
                {
                    imgSource_Chg = ZoomIn(bmMergeOKSource, ratio);
                }
                else
                {
                    imgSource_Chg = ZoomIn(bmSource, ratio);
                }

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);
                g.TranslateTransform(translate_x, translate_y);
                g.DrawImage(imgSource_Chg, new PointF(0, 0));

                pb_map.Image = translateBmp;

                pb_map.Invalidate();

                bmSource.Dispose();
                bmMergeOKSource.Dispose();
              

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapDisplay1 err :={0}", ex.Message.ToString());
            }
        }

        public void onMapDisplay()
        {
            try
            {
                string path = "..\\Ros_info\\map\\mapinfo.bmp";
                int width = 0, height = 0;

                #region 8bit파일을 로드 하여 32bit 이미지로 화면에 표시
                Bitmap bits = new Bitmap(path);
                width = bits.Width;
                height = bits.Height;

                nSourceMapWidth = width;
                nSourceMapHeight = height;

                sourceMapValues = new byte[width * height];

                Rectangle rect3 = new Rectangle(0, 0, width, height);
                System.Drawing.Imaging.BitmapData bmpData3 =
                   bits.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                   bits.PixelFormat);

                IntPtr ptr2 = bmpData3.Scan0;

                {
                    System.Runtime.InteropServices.Marshal.Copy(ptr2, sourceMapValues, 0, nSourceMapWidth * nSourceMapHeight);
                }

                bits.UnlockBits(bmpData3);
               
                Bitmap bmSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Bitmap bmMergeOKSource = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMapValues, "gray");


                //   dOrignX = ((ori_x*-1) / resoultion1)* ratio + translate_x;
                //   dOrignY = ((ori_y) / resoultion1)* ratio - translate_y;

                dOrignX = ((ori_x * -1) / resoultion1);
                dOrignY = ((ori_y) / resoultion1);

                if (dOrignY < 0) dOrignY *= -1;

                bMaploading = true;

                  onLocalCostMapDisplay();
               
                {
                    Bitmap bmcost = new Bitmap(nSourceMapWidth, nSourceMapHeight, PixelFormat.Format32bppRgb);
                    Graphics g2 = Graphics.FromImage(bmcost);

                    float cellX1 = ori_x_localcost / resoultion1;
                    float cellY1 = ori_y_localcost / resoultion1;

                    PointF pos = new PointF();
                    pos.X = dOrignX + cellX1 ;
                    pos.Y = dOrignY - cellY1 ;

                    g2.DrawImage(imgcostmap, (int)pos.X, (int)pos.Y - 120, imgcostmap.Width, imgcostmap.Height);
                     //g2.DrawImage(imgcostmap, (int)pos.X  - translate_x, (int)pos.Y  - translate_y, imgcostmap.Width, imgcostmap.Height);


                    Rectangle r_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_map =
                       bmSource.LockBits(r_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmSource.PixelFormat);

                    IntPtr ptr_map = bmpData_map.Scan0;
                    byte[] sourceMapValues_map = new byte[nSourceMapWidth * nSourceMapHeight * 4];
                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr_map, sourceMapValues_map, 0, nSourceMapWidth * nSourceMapHeight * 4);
                    }

                    bmSource.UnlockBits(bmpData_map);

                    Rectangle r1 = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_r1 =
                       bmcost.LockBits(r1, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmcost.PixelFormat);

                    IntPtr ptr_r1 = bmpData_r1.Scan0;
                    byte[] sourceMapValues_r1 = new byte[nSourceMapWidth * nSourceMapHeight*4];
                    
                    {
                       
                        System.Runtime.InteropServices.Marshal.Copy(ptr_r1, sourceMapValues_r1, 0, bmcost.Width * bmcost .Height*4);
                        //System.Runtime.InteropServices.Marshal.Copy(ptr_r1, sourceMapValues_r1, 0, nSourceMapWidth * nSourceMapHeight * 4);
                    }

                    bmcost.UnlockBits(bmpData_r1);
                    int cnt = 0;
                    for (int i=0; i< nSourceMapWidth * nSourceMapHeight * 4; i++)
                    {
                        if (sourceMapValues_r1[i] == 0x00)
                        {

                        }
                        else if (sourceMapValues_r1[i] == 0xff)
                        {

                        }
                        
                        else if ( sourceMapValues_r1[i] == 0x80 || sourceMapValues_r1[i+1] == 0x80 || sourceMapValues_r1[i + 2] == 0x80 || sourceMapValues_r1[i + 3] == 0x80)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0xbe || sourceMapValues_r1[i + 1] == 0xbe || sourceMapValues_r1[i + 2] == 0xbe || sourceMapValues_r1[i + 3] == 0xbf)
                        {
                            i += 4;
                        }
                        else if (sourceMapValues_r1[i] == 0x40 || sourceMapValues_r1[i + 1] == 0x40 || sourceMapValues_r1[i + 2] == 0x40 || sourceMapValues_r1[i + 3] == 0x40)
                        {
                            i += 4;
                        }
                        else
                        {
                            
                            {
                                sourceMapValues_map[i] = (byte)(sourceMapValues_r1[i]);
                            }
                        }
                        
                    }



                    Rectangle or_map = new Rectangle(0, 0, nSourceMapWidth, nSourceMapHeight);
                    System.Drawing.Imaging.BitmapData bmpData_ormap =
                       bmMergeOKSource.LockBits(or_map, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bmMergeOKSource.PixelFormat);
                    IntPtr ptr_or = bmpData_ormap.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(sourceMapValues_map, 0, ptr_or, nSourceMapWidth * nSourceMapHeight * 4);

                    bmMergeOKSource.UnlockBits(bmpData_ormap);

                    g2.Dispose();
                }

                dOrignX = dOrignX * ratio + translate_x;
                dOrignY = dOrignY * ratio + translate_y;

                Image imgSource_Chg = ZoomIn(bmMergeOKSource, ratio);

                Bitmap translateBmp = new Bitmap(imgSource_Chg.Width, imgSource_Chg.Height);
                translateBmp.SetResolution(imgSource_Chg.HorizontalResolution, imgSource_Chg.VerticalResolution);

                Graphics g = Graphics.FromImage(translateBmp);

                //Graphics g = Graphics.FromImage(imgSource_Chg);
                
                g.TranslateTransform(translate_x, translate_y);

                g.DrawImage(imgSource_Chg, new PointF(0, 0));

                pb_map.Image = translateBmp;
                
           

                pb_map.Invalidate();

                bmSource.Dispose();
                bmMergeOKSource.Dispose();
                #endregion

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapDisplay err :={0}", ex.Message.ToString());
            }
        }

        public void onLocalCostMapDisplay()
        {
            try
            {
                if (bMaploading)
                {
                    string path = "..\\Ros_info\\map\\map_localcost.bmp";
                    int width = 0, height = 0;
                    
                    Bitmap bits = new Bitmap(path);
                    width = bits.Width;
                    height = bits.Height;

                    nSourceMap_localcostWidth = width;
                    nSourceMap_localcostHeight = height;

                    sourceMaplocalcostValues = new byte[width * height];

                    Rectangle rect3 = new Rectangle(0, 0, width, height);
                    System.Drawing.Imaging.BitmapData bmpData3 =
                       bits.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                       bits.PixelFormat);

                    IntPtr ptr2 = bmpData3.Scan0;

                    {
                        System.Runtime.InteropServices.Marshal.Copy(ptr2, sourceMaplocalcostValues, 0, nSourceMap_localcostWidth * nSourceMap_localcostHeight);
                    }

                    bits.UnlockBits(bmpData3);


                    Bitmap bmSource = new Bitmap(nSourceMap_localcostWidth, nSourceMap_localcostHeight, PixelFormat.Format32bppRgb);//, PixelFormat.Format8bppIndexed);

                    Map_Robot_Image_Processing2(ref bmSource, bmSource.Width, bmSource.Height, sourceMaplocalcostValues, "cost");


                    Image imgSource_Chg = ZoomIn(bmSource, ratio_localcost);

                    imgcostmap = (Image)imgSource_Chg.Clone(); ;
 

                   
                    pb_cost.Image = imgcostmap;// (Bitmap)(bmSource.Clone());

                    pb_cost.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLocalCostMapDisplay err :={0}", ex.Message.ToString());
            }
        }

        private void pb_map_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen pen = new Pen(Brushes.Red, 1);
                pen.DashStyle = DashStyle.Dash;

                if (bMaploading)
                {
                    e.Graphics.DrawLine(pen, (float)dOrignX-10, (float)dOrignY, (float)dOrignX +10, (float)dOrignY);
                    e.Graphics.DrawLine(pen, (float)dOrignX , (float)dOrignY-10, (float)dOrignX , (float)dOrignY+10);
                    e.Graphics.DrawEllipse(Pens.Red, dOrignX - 10, dOrignY - 10, 20, 20);
                    //e.Graphics.FillEllipse(Brushes.Blue, dOrignX - 10, dOrignY - 10, 20, 20);

                    if (mainform.actiondatalitTable.ContainsKey(strSelectedMissionID))
                    {
                        int cnt = mainform.actiondatalitTable[strSelectedMissionID].action_data.Count;

                        Pen pen2 = new Pen(Brushes.Blue, 3);
                        pen2.DashStyle = DashStyle.Dash;
                        List<PointF> linepos = new List<PointF>();

                        posRectList.Clear();

                        for (int i = 0; i < cnt; i++)
                        {
                            if (mainform.actiondatalitTable[strSelectedMissionID].action_data[i].strActionType.Equals("Goal-Point"))
                            {
                                string stractiondata = mainform.actiondatalitTable[strSelectedMissionID].action_data[i].strWorkData;
                                string[] strgoal_sub = stractiondata.Split('/');

                                string[] strgoal_sub_act_param = strgoal_sub[1].Split(':');
                                float fx = float.Parse(strgoal_sub_act_param[1]);

                                strgoal_sub_act_param = strgoal_sub[2].Split(':');
                                float fy = float.Parse(strgoal_sub_act_param[1]);

                                strgoal_sub_act_param = strgoal_sub[3].Split(':');
                                float ftheta = float.Parse(strgoal_sub_act_param[1]);

                                float cellX = fx / resoultion1;
                                float cellY = fy / resoultion1;

                                PointF pos = new PointF();
                                pos.X = dOrignX + cellX * ratio;
                                pos.Y = dOrignY - cellY * ratio;
                                linepos.Add(pos);
                                //e.Graphics.DrawLine()
                                //e.Graphics.DrawEllipse(pen2, pos.X, pos.Y, (float)radiusX, (float)radiusY);

                                Rectangle rectpos = new Rectangle();
                                rectpos.X = (int)(pos.X - 4);
                                rectpos.Y = (int)(pos.Y - 4);
                                rectpos.Width = 8;
                                rectpos.Height = 8;
                                Pen penP = new Pen(Brushes.Gray, 1);

                                posRectList.Add(rectpos);

                                e.Graphics.DrawRectangle(penP, rectpos);
                            }
                            else
                            {
                                Rectangle rectpos = new Rectangle();
                                rectpos.X = -1000;
                                rectpos.Y = -1000;
                                rectpos.Width = 8;
                                rectpos.Height = 8;
                                posRectList.Add(rectpos);
                            }
                        }

                        if (linepos.Count > 0)
                        {
                            Pen pen3 = new Pen(Brushes.Yellow, 2);
                            for (int j = 0; j < linepos.Count; j++)
                            {
                                if (j == linepos.Count - 1)
                                { }
                                else e.Graphics.DrawLine(pen3, linepos[j], linepos[j + 1]);
                            }
                            pen3.Dispose();
                        }

                        pen2.Dispose();
                    }

                    for (int idx = 0; idx < mainform.G_robotList.Count; idx++)
                    {
                        if (Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.robotstate != null)
                        {
                            if (Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.robotstate.msg != null)
                            {
                                float robotx = (float)Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.robotstate.msg.pose.x;
                                float roboty = (float)Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.robotstate.msg.pose.y;
                                float robottheta = (float)Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.robotstate.msg.pose.theta;

                                float cellX = robotx / resoultion1;
                                float cellY = roboty / resoultion1;

                                PointF pos = new PointF();
                                pos.X = dOrignX + cellX * ratio;
                                pos.Y = dOrignY - cellY * ratio;
                                Pen pen_robot = new Pen(Brushes.BlueViolet, 3);
                                pen_robot.DashStyle = DashStyle.Solid;
                                e.Graphics.DrawEllipse(pen_robot, pos.X - 10, pos.Y - 10, 20, 20);
                                e.Graphics.DrawString(string.Format("{0}", mainform.G_robotList[idx]),new Font("고딕체",5),Brushes.Black, pos.X - 10, pos.Y-20);

                                //global plan dp
                                if (Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.globalplan != null)
                                {
                                    if (Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.globalplan.msg != null)
                                    {
                                        int ncnt = Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.globalplan.msg.poses.Count;
                                        List<PoseStamped> path_pose = new List<PoseStamped>();
                                        path_pose = Data.Instance.Robot_work_info[mainform.G_robotList[idx]].robot_status_info.globalplan.msg.poses;

                                        if (ncnt > 0)
                                        {
                                            PointF[] pathPoint_buf = new PointF[ncnt];
                                            int path_idx = 0;
                                            for (int i = 0; i < ncnt; i++)
                                            {
                                                float path_x_tmp = (float)path_pose[i].pose.position.x;
                                                float path_y_tmp = (float)path_pose[i].pose.position.y;

                                                PointF pos_path = new PointF();
                                                float cellX_path = path_x_tmp / resoultion1;
                                                float cellY_path = path_y_tmp / resoultion1;
                                                pos_path.X = dOrignX + cellX_path * ratio;
                                                pos_path.Y = dOrignY - cellY_path * ratio;

                                                pathPoint_buf[path_idx] = pos_path;
                                                path_idx++;
                                            }
                                            Pen pen_path = new Pen(Brushes.YellowGreen, 3);
                                            e.Graphics.DrawLines(pen_path, pathPoint_buf);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (chkInitPosSet.Checked && bMapMouseDN)
                    {
                        Pen pen_initposset = new Pen(Brushes.Green, 4);
                        PointF arrow_1 = new PointF(initposset_second.X -10, initposset_second.Y-10);
                        PointF arrow_2 = new PointF(initposset_second.X + 10, initposset_second.Y - 10);

                        //e.Graphics.DrawLine(pen_initposset, initposset_second, arrow_1);
                        //e.Graphics.DrawLine(pen_initposset, initposset_second, arrow_2);

                        e.Graphics.DrawLine(pen_initposset,initposset_first,initposset_second);
                    }

                    Pen pen4 = new Pen(Brushes.Red, 2);
                    pen4.DashStyle = DashStyle.Dot;
                    if (bMapMouseDN)
                    {
                        e.Graphics.DrawRectangle(pen4, RectTmp);
                    }
                  /*  if (waringRectList.Count > 0)
                    {
                        for (int i = 0; i < waringRectList.Count; i++)
                        {
                            RectangleF rectT = waringRectList[i];
                            rectT.X = (float)((rectT.X  + translate_x) * ratio);
                            rectT.Y = (float)((rectT.Y  + translate_y) * ratio);
                            rectT.Width = (float)(rectT.Width * ratio);
                            rectT.Height = (float)(rectT.Height * ratio);

                            e.Graphics.DrawRectangle(pen4, rectT.X, rectT.Y, rectT.Width, rectT.Height);
                        }
                    }*/

                    if(waringPointList.Count>0)
                    {
                        for (int i = 0; i < waringPointList.Count; i++)
                        {
                            WarningRegionClass wariningregion = new WarningRegionClass();
                            wariningregion= waringPointList[i];
                            float wx = wariningregion.nX1;
                            float wy = wariningregion.nY1;
                            float wx2 = wariningregion.nX2;
                            float wy2 = wariningregion.nY2;

                            float cellX = wx / resoultion1;
                            float cellY = wy / resoultion1;

                            float cellX2 = wx2 / resoultion1;
                            float cellY2 = wy2 / resoultion1;

                            PointF pos = new PointF();
                            pos.X = dOrignX + cellX * ratio;
                            pos.Y = dOrignY - cellY * ratio;

                            PointF pos2 = new PointF();
                            pos2.X = dOrignX + cellX2 * ratio;
                            pos2.Y = dOrignY - cellY2 * ratio;


                            e.Graphics.DrawRectangle(pen4, pos.X, pos.Y, pos2.X- pos.X, pos2.Y - pos.Y );

                            
                        }
                    }
                    pen4.Dispose();

                }
                pen.Dispose();
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("pb_map_Paint err :={0}", ex.Message.ToString());
            }
        }

        float nangluar_x1 = 0;
        float nangluar_x2 = 0;
        float nangluar_y1 = 0;
        float nangluar_y2 = 0;

        PointF initposset_first = new PointF();
        PointF initposset_second = new PointF();

        private void pb_map_MouseDown(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

            //  nx = (int)(nx - translate_x);
            //  ny = (int)(ny - translate_y);

            float tmpOriginX = dOrignX / ratio;
            float tmpOriginY = dOrignY / ratio;

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;
            nangluar_x1 = currXpos;
            nangluar_y1 = currYpos;




            W_x_1 = e.X;
            W_y_1 = e.Y;

            initposset_first = new PointF();

            if (chkAngluar.Checked)
            { }
            else if(chkInitPosSet.Checked)
            {
                initposset_first.X = nx;
                initposset_first.Y = ny;

                bMapMouseDN = true;
            }
            else
            {
                if (posRectList.Count > 0)
                {
                    for (int i = 0; i < posRectList.Count; i++)
                    {
                        if (posRectList[i].Contains(e.X, e.Y))
                        {
                            bPosRectSelect = true;
                            nPosRectidx = i;
                            break;
                        }
                    }
                }

                if (!bPosRectSelect) bMapMouseDN = true;
            }
        }

        private void pb_map_MouseUp(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

            //  nx = (int)(nx - translate_x);
            //  ny = (int)(ny - translate_y);

            float tmpOriginX = dOrignX / ratio;
            float tmpOriginY = dOrignY / ratio;

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;

            nangluar_x2 = currXpos;
            nangluar_y2 = currYpos;

            float TargetAngle = (float)(Math.Atan2(nangluar_y2- nangluar_y1, nangluar_x2- nangluar_x1) * 180f / Math.PI);
            TargetAngle = mainform.worker.DegreeToRadian(string.Format("{0:f2}", TargetAngle));
            TargetAngle *= -1;
            txtAngluar.Text = string.Format("{0:f2}", TargetAngle);

            W_x_2 = e.X;
            W_y_2 = e.Y;
            if (chkAngluar.Checked)
            {
            }
            else if (chkInitPosSet.Checked)
            {
                initposset_first = new PointF();
                initposset_second = new PointF();
            

            }
            else
            {
                if (bMapMouseDN)
                {
                    RectangleF rectT = RectTmp;
                    //rectT.X = (float)((rectT.X / ratio) -translate_x);
                    //rectT.Y = (float)((rectT.Y / ratio)-translate_y);

                    rectT.X = (float)((rectT.X - translate_x) / ratio);
                    rectT.Y = (float)((rectT.Y - translate_y) / ratio);

                    rectT.Width = (float)(rectT.Width / ratio);
                    rectT.Height = (float)(rectT.Height / ratio);
                    waringRectList.Add(rectT);

                    WarningRegionClass wariningregion = new WarningRegionClass();
                    wariningregion.nX1 = (float)(((W_x_1 - dOrignX) * resoultion1)) / ratio;
                    wariningregion.nY1 = (float)(((W_y_1 - dOrignY) * resoultion1)) / ratio * -1;
                    wariningregion.nX2 = (float)(((W_x_2 - dOrignX) * resoultion1)) / ratio;
                    wariningregion.nY2 = (float)(((W_y_2 - dOrignY) * resoultion1)) / ratio * -1;

                    waringPointList.Add(wariningregion);

                }
            }

            bMapMouseDN = false;
            bPosRectSelect = false;
        }

    private void pb_map_MouseClick(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

          //  nx = (int)(nx - translate_x);
          //  ny = (int)(ny - translate_y);

            float tmpOriginX = dOrignX / ratio;
            float tmpOriginY = dOrignY / ratio;

            float currXpos = (float)(((nx - dOrignX) * resoultion1))/ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1))/ratio;

            txtXpos.Text = string.Format("{0:f2}", currXpos);
            txtYpos.Text = string.Format("{0:f2}", currYpos*-1);

            if(chkDelivery.Checked)
            {
                onDeliveryRun(currXpos, currYpos, (float)0.7);
            }

        }

        private async void onDeliveryRun(float s_x, float s_y, float robotspeedvalue)
        {
            string strworkid = "delivery";
            string strRobot = cboRobotID.SelectedItem.ToString();
            int nworkcnt = 1;
            int nactidx = 0;

            string[] strSelectworkdata_Worker = new string[2];
            
            strSelectworkdata_Worker[0] = string.Format("type:Goal-Point/x:{0}/y:{1}/theta:0/qual:C/max_trans_vel:{2:f1}" +
                        "/xy_goal_tolerance:0.18/yaw_goal_tolerance:0.05/p_drive:0.75/d_drive:1.2/wp_tolerance:1/avoid:false", s_x, s_y,  robotspeedvalue);

            var task = Task.Run(() => mainform.worker.onFleetManager_WorkOrder_publish("delivery", strworkid, strRobot, strSelectworkdata_Worker, nworkcnt, nactidx));
            await task;
        }



        private void pb_map_MouseMove(object sender, MouseEventArgs e)
        {
            int nx = e.X;
            int ny = e.Y;
            txtXcell.Text = string.Format("{0}", e.X);
            txtYcell.Text = string.Format("{0}", e.Y);

         
            //    nx = (int)(nx - translate_x);
            //   ny = (int)(ny - translate_y);

            float currXpos = (float)(((nx - dOrignX) * resoultion1)) / ratio;
            float currYpos = (float)(((ny - dOrignY) * resoultion1)) / ratio;

            txtXpos.Text = string.Format("{0:f2}", currXpos);
            txtYpos.Text = string.Format("{0:f2}", currYpos * -1);
            if (chkAngluar.Checked)
            {
                nangluar_x2 = currXpos;
                nangluar_y2 = currYpos;

                float TargetAngle = (float)(Math.Atan2(nangluar_y2 - nangluar_y1, nangluar_x2 - nangluar_x1) * 180f / Math.PI);
                TargetAngle = mainform.worker.DegreeToRadian(string.Format("{0:f2}", TargetAngle));
                TargetAngle *= -1;
                txtAngluar.Text = string.Format("{0:f2}", TargetAngle);
            }
            else if (chkInitPosSet.Checked)
            {
                initposset_second = new PointF();
                initposset_second.X = nx;
                initposset_second.Y = ny;
            }
            else
            {
                if (bMapMouseDN)
                {
                    Rectangle rect = new Rectangle();
                    rect.X = (int)(W_x_1);
                    rect.Y = (int)(W_y_1);
                    rect.Width = (int)((nx - (int)W_x_1));
                    rect.Height = (int)((ny - (int)W_y_1));

                    RectTmp = rect;

                    pb_map.Invalidate();
                }

                if (bPosRectSelect)
                {
                    if (posRectList.Count > 0)
                    {
                        string stractiondata = mainform.actiondatalitTable[strSelectedMissionID].action_data[nPosRectidx].strWorkData;
                        string[] strgoal_sub = stractiondata.Split('/');

                        string[] strgoal_sub_act_param = strgoal_sub[3].Split(':');
                       

                        int idx = stractiondata.IndexOf("qual:C");
                        string strtemp = stractiondata.Substring(idx + 6);

                        string strdata = string.Format("type:Goal-Point/x:{0}/y:{1}/theta:{2}/qual:C", currXpos, currYpos * -1, strgoal_sub_act_param[1]);
                        strdata += strtemp;

                        mainform.actiondatalitTable[strSelectedMissionID].action_data[nPosRectidx].strWorkData = strdata;

                        onActionDataDP(nPosRectidx);
                        nSelectedActionidx = nPosRectidx;

                        pb_map.Invalidate();
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            translate_y -= 50;
            txtTY.Text = string.Format("{0}", translate_y);
            onMapDisplay1();
        }

        private void btnDn_Click(object sender, EventArgs e)
        {
            translate_y += 50;
            txtTY.Text = string.Format("{0}", translate_y);
            onMapDisplay1();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            translate_x -= 50;
            txtTX.Text = string.Format("{0}", translate_x);
            onMapDisplay1();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            translate_x += 50;
            txtTX.Text = string.Format("{0}", translate_x);
            onMapDisplay1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ratio += 0.1f;
            txtratio.Text = string.Format("{0}", ratio);

            onMapDisplay1();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ratio -= 0.1f;
            txtratio.Text = string.Format("{0}", ratio);
            onMapDisplay1();

        }


        private void Map_Robot_Image_Processing2(ref Bitmap bmSource, int Width, int Height, byte[] sourcemapvalue, string strfiltername)
        {
            try
            {
                //
                // 여기서 부터 Picture Box의 이미지를 복사해 오는 부분입니다
                //
                Rectangle rect = new Rectangle(0, 0, bmSource.Width, bmSource.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmSource.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmSource.PixelFormat);

                IntPtr ptr = bmpData.Scan0;
                byte[] rgbValues;


                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    rgbValues = new byte[Width * Height * 4];
                }
                else
                {
                    rgbValues = new byte[Width * Height];
                }

                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    var k = 0;
                    for (var y = 0; y < Height; y++)
                    {
                        for (var x = 0; x < Width; x++)
                        {
                            byte btemp = sourcemapvalue[y * Width + x];

                            if (strfiltername == "gray" || strfiltername == "cost")
                            {
                                //if (btemp == 0) btemp = 0xff;
                                //else if (btemp == 0xff) btemp = 0xf0;
                            }
                            else
                            {
                                if (btemp == 0) btemp = 0xff;
                            }

                            #region yellow filter
                            if (strfiltername == "yellow")
                            {
                                if (btemp == 0xff)
                                {
                                    rgbValues[k] = btemp;
                                    rgbValues[k + 1] = btemp;
                                    rgbValues[k + 2] = btemp;

                                }
                                else
                                {
                                    if (btemp > 0x9b) btemp = 0x9b;
                                    if (btemp < 0x24) btemp = 0x24;

                                    rgbValues[k + 1] = 0xff;// (byte)(0xff- btemp);
                                    rgbValues[k + 2] = 0xff;// (byte)(0xff - btemp);


                                    rgbValues[k + 3] = btemp;
                                }


                            }
                            #endregion




                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray" || strfiltername == "cost")
                            {
                                rgbValues[k] = btemp;
                                rgbValues[k + 1] = btemp;
                                rgbValues[k + 2] = btemp;
                            }

                          
                            #endregion

                            k += 4;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Width * Height; i++)
                    {
                        rgbValues[i] = sourcemapvalue[i];
                    }
                }

                //
                // 여기까지가 Marshal Copy로 rgbValues 버퍼로 영상을 Copy해 오는 부분입니다.
                //

                //
                // 여기서부터 2차원 배열로 1차원 영상을 옮기는 부분입니다
                //
                double[,] Source = new double[Width, Height];
                double[,] Target = new double[Width, Height];

                int XPos, YPos = 0;
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;

                        if (strfiltername == "yellow" || strfiltername == "red")

                            XPos = 3;//blue xpos

                        if (strfiltername == "gray")
                            XPos = 0; //gray xpos

                        for (int nW = 0; nW < Width; nW++)
                        {
                            Source[nW, nH] = rgbValues[XPos + YPos];
                            Target[nW, nH] = rgbValues[XPos + YPos];
                            XPos += 4;
                        }
                        YPos += Width * 4;
                    }
                }
                else
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {
                            Source[nW, nH] = rgbValues[XPos + YPos];
                            Target[nW, nH] = rgbValues[XPos + YPos];
                            XPos++;
                        }
                        YPos += Width;
                    }
                }

                //
                // 여기까지는 2차원 배열로 영상을 복사하는 부분입니다.
                //

                //좌우반전//
                int nconvert = 0;
                /*   double[,] bconvertTarget = new double[Width, Height];
                   for (int nh = 0; nh < Height; nh++)
                   {
                       nconvert = 0;
                       for (int nw = Width; nw > 0; nw--)
                       {
                           bconvertTarget[nw-1, nh] = Target[nconvert, nh];
                           nconvert++;
                       }
                   }
                   */
                //상하반전
                nconvert = 0;
                double[,] bconvertTarget = new double[Width, Height];
                for (int nh = 0; nh < Height; nh++)
                {
                    nconvert = 0;
                    for (int nw = 0; nw < Width; nw++)
                    {
                        bconvertTarget[nw, Height - nh - 1] = Target[nw, nh];
                        //nconvert++;
                    }
                }



                //
                // 여기서 부터는 2차원 배열을 다시 1차원 버터로 옮기는 부분입니다
                //

                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    rgbValues = new byte[Width * Height * 4];
                }
                else
                {
                    rgbValues = new byte[Width * Height];
                }

                YPos = 0;
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {
                            #region yellow filter
                            if (strfiltername == "yellow")
                            {
                                byte btemp = (byte)bconvertTarget[nW, nH];


                                if (btemp == 0) btemp = 0xff;

                                if (btemp == 0xff)
                                {
                                    rgbValues[XPos + YPos] = btemp;
                                    rgbValues[XPos + YPos + 1] = btemp;
                                    rgbValues[XPos + YPos + 2] = btemp;

                                }
                                else
                                {
                                    if (btemp > 0x9b) btemp = 0x9b;
                                    if (btemp < 0x24) btemp = 0x24;

                                    rgbValues[XPos + YPos + 1] = 0xff;// (byte)(0xff- btemp);
                                    rgbValues[XPos + YPos + 2] = 0xff;// (byte)(0xff - btemp);


                                    rgbValues[XPos + YPos + 3] = btemp;
                                }
                            }
                            #endregion




                            #region  gray filter 그레이는 r,g,b가 동일 값으로 들어감
                            if (strfiltername == "gray")
                            {
                                bconvertTarget[nW, nH] = (byte)(255 - (255 * bconvertTarget[nW, nH]) / 100);
                                rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 1] = (byte)bconvertTarget[nW, nH];
                                rgbValues[XPos + YPos + 2] = (byte)bconvertTarget[nW, nH];
                            }
                            #endregion

                            #region  cost map filter
                            if (strfiltername == "cost")
                            {
                                if (bconvertTarget[nW, nH] > 1 && bconvertTarget[nW, nH] < 99)
                                {
                                    bconvertTarget[nW, nH] = (byte)((255 * bconvertTarget[nW, nH]) / 100);
                                    rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];
                                    rgbValues[XPos + YPos + 1] = 0;
                                    rgbValues[XPos + YPos + 2] = (byte)(255 - bconvertTarget[nW, nH]);
                                    rgbValues[XPos + YPos + 3] = 255;
                                }

                                else if (bconvertTarget[nW, nH] == 100)
                                {
                                    rgbValues[XPos + YPos] = 255;
                                    rgbValues[XPos + YPos + 1] = 0;
                                    rgbValues[XPos + YPos + 2] = 255;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }
                                else if (bconvertTarget[nW, nH] > 101 && bconvertTarget[nW, nH] < 128)
                                {
                                    rgbValues[XPos + YPos] = 0;
                                    rgbValues[XPos + YPos + 1] = 255;
                                    rgbValues[XPos + YPos + 2] = 0;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }
                                else if (bconvertTarget[nW, nH] > 128 && bconvertTarget[nW, nH] < 255)
                                {
                                    rgbValues[XPos + YPos] = 255;
                                    rgbValues[XPos + YPos + 1] = (byte)((255 * (bconvertTarget[nW, nH] - 128)) / (254 - 128));
                                    rgbValues[XPos + YPos + 2] = 0;
                                    rgbValues[XPos + YPos + 3] = 255;
                                }
                                else
                                {
                                    rgbValues[XPos + YPos] = 0xff;
                                    rgbValues[XPos + YPos + 1] = 0xff;
                                    rgbValues[XPos + YPos + 2] = 0xff;
                                }
                            }
                            #endregion

                            XPos += 4;
                        }
                        YPos += Width * 4;

                    }
                }
                else
                {
                    for (int nH = 0; nH < Height; nH++)
                    {
                        XPos = 0;
                        for (int nW = 0; nW < Width; nW++)
                        {
                            rgbValues[XPos + YPos] = (byte)bconvertTarget[nW, nH];

                            XPos++;
                        }
                        YPos += Width;
                    }
                }


                //
                // 다시 Marshal Copy로 Picture Box로 옮기는 부분입니다
                //
                if (bmSource.PixelFormat == PixelFormat.Format32bppArgb || bmSource.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height * 4);
                }
                else
                {
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, Width * Height);
                }

                bmSource.UnlockBits(bmpData);

                //System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(6, 6, Width - 12, Height - 12);
                System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(0, 0, Width , Height);
                Bitmap bmpTemp = bmSource.Clone(cropArea, bmSource.PixelFormat);
                bmSource.Dispose();
                bmSource = null;
                bmSource = (Bitmap)(bmpTemp.Clone());
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Map_Robot_Image_Processing2 err :={0}", ex.Message.ToString());
            }

        }

        public Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        Image ZoomIn(Image img, double nresolution)
        {

            Bitmap bmp = new Bitmap(img, (int)(img.Width * nresolution), (int)(img.Height * nresolution));
            bmp.SetResolution((int)(bmp.VerticalResolution * nresolution), (int)(bmp.HorizontalResolution * nresolution));
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pb_map.Invalidate();
        }

       
    }

    public class MapClass
    {
        public int nMapWidth;
        public int nMapHeight;
        public int Mapresoultion;
        
    }

    public class WarningRegionClass
    {
        public float nX1;
        public float nY1;
        public float nX2;
        public float nY2;
    }

    public class LocalCostMap_DataInfomation
    {
        public List<LocalCostMap_Data> localcostmap;
    }

    public class LocalCostMap_Data
    {
        public float originX;
        public float originY;
        public float resoultion;
       
    }

    #endregion
}
