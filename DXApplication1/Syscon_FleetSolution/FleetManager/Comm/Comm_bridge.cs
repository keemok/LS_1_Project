using System;
using System.Collections.Generic;

//add using info
using Rosbridge.Client;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Syscon_Solution.FleetManager.Comm
{
    public class Comm_bridge
    {
        LSprogram.mainForm mainform;
        //Exhibition.exhibitionMain exhibition;
        LS_TEST.mappingform mappingform;
        LSprogram.connectionForm connectionform;

        public event TaskResultResponse taskresult_Evt; 
        public event TaskFeedbackResponse taskfeedback_Evt;

        public event MapInfoComplete mapinfo_Evt;
        public event GlobalCostInfoComplete Globalcostmapinfo_Evt;
        public event LocalCostInfoComplete Localcostmapinfo_Evt;
        public event GlobalpathComplete Globalpath_Evt;

        //public event WaitPos_Complete Waitpos_evt;

        public event RobotStateComplete RobotState_Evt;

        public event MoveBase_StatusComplete MoveBase_Status_Evt;



        public delegate void TaskResultResponse(string strrobotid);
        public delegate void TaskFeedbackResponse(string strrobotid);

        public delegate void MapInfoComplete(string strrobotid);
        public delegate void GlobalCostInfoComplete(string strrobotid);
        public delegate void LocalCostInfoComplete(string strrobotid);
        public delegate void GlobalpathComplete(string strrobotid);
        public delegate void WaitPos_Complete(string strrobotid);

        public delegate void RobotStateComplete(string strrobotid);


        public delegate void MoveBase_StatusComplete(string strrobotid);

        public Comm_bridge()
        {

        }
        public Comm_bridge(LS_TEST.mappingform mappingForm)
        {
            this.mappingform = mappingForm;
        }
        public Comm_bridge(Exhibition.exhibitionMain exhibitionmain)
        {
            //this.exhibition = exhibitionmain;
        }
        public Comm_bridge(LSprogram.mainForm mainform_)
        {
            this.mainform = mainform_;
        }
        public Comm_bridge(LSprogram.connectionForm frm)
        {
            connectionform = frm;
        }
              
        #region TASK 지시 관련
        /// <summary>
        /// 로봇이 작업하기전에 로봇정보 구조체를 초기화 하는 부분
        /// </summary>
        public Robot_WorkKInfo onNewRobotWorkInfo_initial(string strrobotid, string strworkid, int workcnt, int nactionidx, string strgoalid, string strworkname)
        {
            Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
            robot_workinfo.robot_workdata = new List<Robot_Work_Data>();

            robot_workinfo.strRobotID = strrobotid;
            robot_workinfo.strWorkID = strworkid;
            robot_workinfo.nWork_cnt = workcnt;
            robot_workinfo.nCurrWork_cnt = 1;
            robot_workinfo.nActionidx = nactionidx;
            robot_workinfo.strWorkName = strworkname;
            robot_workinfo.strLoop_Flag = "wait";

            robot_workinfo.nPriorityLevel = 1;

            robot_workinfo.robottask_info = new Robot_Task_Info();

            robot_workinfo.robot_status_info = new Robot_Status_info();
            robot_workinfo.robot_status_info.robotstate = new RobotState_1();
            robot_workinfo.robot_status_info.motorstate = new MotorInformation();
            robot_workinfo.robot_status_info.lidar = new LidarScan();
            robot_workinfo.robot_status_info.ultrasonic = new UltrasonicRawInfo();
            robot_workinfo.robot_status_info.workfeedback = new WorkFeedback();
            robot_workinfo.robot_status_info.workresult = new WorkResult();
            robot_workinfo.robot_status_info.bmsinfo = new BMSInfo();
            robot_workinfo.robot_status_info.mapinfo = new MapInformation();
            robot_workinfo.robot_status_info.globalcostmap = new MapInformation();
            robot_workinfo.robot_status_info.localcostmap = new MapInformation();
            robot_workinfo.robot_status_info.globalplan = new MapPlanInformation();
            robot_workinfo.robot_status_info.ur_status = new UR_StatusInformation();
            robot_workinfo.robot_status_info.lift_info = new RobotLiftInfo();
            robot_workinfo.robot_status_info.markers = new MarkerDetection_Information();

            return robot_workinfo;
        }


        public async void onTaskorder_LS()
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }


        }

        /// <summary>
        /// 작업 지시 publish
        ///  topic : /RIDiS/taskorder
        ///  type  : syscon_msgs/TaskOrker
        /// </summary>
        public void onTaskrder_publish(Task_Order taskorder, string taskname, List<MisssionInfo> missioninfo_list)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }


            try
            {
                TopicList list = new TopicList();

                if (taskorder.robotlist == "")
                {
                    return;
                }
                string taskid = taskorder.task_id;
                string missionlist = taskorder.missionlist;
                string robotlist = taskorder.robotlist;
                string[] robotlist_buf = robotlist.Split(',');
                string[] missionlist_buf = missionlist.Split(',');

                int robotcnt = robotlist_buf.Length;
                for (int i = 0; i < robotcnt; i++)
                {
                    string strRobot_id = robotlist_buf[i];

                    //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                    if (Data.Instance.Robot_work_info.ContainsKey(strRobot_id))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        Data.Instance.Robot_work_info[strRobot_id].strWorkID = taskid;
                        Data.Instance.Robot_work_info[strRobot_id].nWork_cnt = 1;
                        Data.Instance.Robot_work_info[strRobot_id].nCurrWork_cnt = 1;
                        Data.Instance.Robot_work_info[strRobot_id].nActionidx = 0;
                        Data.Instance.Robot_work_info[strRobot_id].strWorkName = taskname;
                        Data.Instance.Robot_work_info[strRobot_id].strLoop_Flag = "wait";

                        Data.Instance.Robot_work_info[strRobot_id].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                        Data.Instance.Robot_work_info[strRobot_id].robottask_info.strTaskID = taskid;
                        Data.Instance.Robot_work_info[strRobot_id].robottask_info.robotmisssion_info = new List<MisssionInfo>();
                        Data.Instance.Robot_work_info[strRobot_id].task_finished = false;
                        Data.Instance.Robot_work_info[strRobot_id].robottask_info.robotmisssion_info = missioninfo_list;
                    }
                    else //새로 작업하는 로봇은 새로 추가.. 
                    {
                        Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();



                        robot_workinfo.strRobotID = strRobot_id;
                        robot_workinfo.strWorkID = taskid;
                        robot_workinfo.nWork_cnt = 1;
                        robot_workinfo.nCurrWork_cnt = 1;
                        robot_workinfo.nActionidx = 0;
                        robot_workinfo.strWorkName = taskname;
                        robot_workinfo.strLoop_Flag = "wait";
                        robot_workinfo.task_finished = false;

                        robot_workinfo.robot_status_info = new Robot_Status_info();
                        robot_workinfo.robot_status_info.robotstate = new RobotState_1();
                        robot_workinfo.robot_status_info.motorstate = new MotorInformation();
                        robot_workinfo.robot_status_info.lidar = new LidarScan();
                        robot_workinfo.robot_status_info.ultrasonic = new UltrasonicRawInfo();
                        robot_workinfo.robot_status_info.workfeedback = new WorkFeedback();
                        robot_workinfo.robot_status_info.workresult = new WorkResult();
                        robot_workinfo.robot_status_info.bmsinfo = new BMSInfo();
                        robot_workinfo.robot_status_info.mapinfo = new MapInformation();
                        robot_workinfo.robot_status_info.globalcostmap = new MapInformation();
                        robot_workinfo.robot_status_info.localcostmap = new MapInformation();
                        robot_workinfo.robot_status_info.globalplan = new MapPlanInformation();
                        robot_workinfo.robot_status_info.ur_status = new UR_StatusInformation();

                        robot_workinfo.robot_workdata = new List<Robot_Work_Data>();
                        robot_workinfo.robottask_info = new Robot_Task_Info();
                        robot_workinfo.robottask_info.robotmisssion_info = new List<MisssionInfo>();


                        robot_workinfo.robottask_info.strTaskID = taskid;
                        robot_workinfo.robottask_info.robotmisssion_info = missioninfo_list;
                        

                        Data.Instance.Robot_work_info.Add(strRobot_id, robot_workinfo);
                    }
                }


                string strobj = JsonConvert.SerializeObject(taskorder);
                JObject obj = JObject.Parse(strobj);

                rosinterface ros = new rosinterface();

                ros.PublisherTopicMsgtype(list.topic_taskorder, list.msg_taskorder);
                Thread.Sleep(Data.Instance.nPulishDelayTime);
                ros.publisher(obj);
                Thread.Sleep(Data.Instance.nPulishDelayTime);
                Console.Out.WriteLine("task send 1 :={0}", taskid);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskrder_publish err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// task 취소 publish
        ///  topic : $(RID)/TARU/cancel
        ///  type  : actionlib_msgs/GoalID
        /// </summary>
        public async void onTaskCancel_publish(string strRobot, string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    GoalID goal_id = new GoalID();
                    goal_id.id = strgoal_id;

                    string strobj = JsonConvert.SerializeObject(goal_id);
                    JObject obj =  JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_taskcancel, list.msg_taskcancel);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskCancel_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// task 취소 publish
        ///  topic : $(RID)/TARU/pause
        ///  type  : actionlib_msgs/GoalID
        /// </summary>
        public async void onTaskPause_publish(string strRobot, string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    MissionChange missionchg = new MissionChange();
                    missionchg.data = strgoal_id;

                    string strobj = JsonConvert.SerializeObject(missionchg);
                    JObject obj =  JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_taskpause, list.msg_taskpause);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskPause_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// task 취소 publish
        ///  topic : $(RID)/TARU/resume
        ///  type  : std_msgs/String
        /// </summary>
        public async void onTaskResume_publish(string strRobot, string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    MissionChange missionchg = new MissionChange();
                    missionchg.data = strgoal_id;

                    string strobj = JsonConvert.SerializeObject(missionchg);
                    JObject obj =  JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_taskresume, list.msg_taskresume);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskPause_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// mission change publish
        ///  topic : $(RID)/TARU/mission_change
        ///  type  : std_msgs/String
        /// </summary>
        public async void onMissionChange_publish(string strRobot, string strmission_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    MissionChange missionchg = new MissionChange();
                    missionchg.data = strmission_id;

                    string strobj = JsonConvert.SerializeObject(missionchg);
                    JObject obj =  JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_missionchg, list.msg_missionchg);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onmissionchange_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// temp move  publish
        ///  topic : $(RID)/tempo_move
        ///  type  : syscon_msgs/TempoMove
        /// </summary>
        public async void onTempoMove_publish(string strRobot, float direction, float distance)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    TempMove tmpmove = new TempMove();
                    tmpmove.direction = direction;
                    tmpmove.distance = distance;

                    string strobj = JsonConvert.SerializeObject(tmpmove);
                    JObject obj = JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_tempomove, list.msg_tempomove);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTempoMove_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// onWorkGoal_publish
        ///  topic : $(RID)/WAS/goal
        ///  type  : syscon_msgs/WorkFlowActionGoal
        /// </summary>
        public async void onWorkGoal_publish(string strWorkname, string strWorkid, string strRobot_id, int nactidx, int nworkcnt, Action[] act)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            WAS_GOAL work_data = new WAS_GOAL();
            work_data.goal_id.id = strWorkid;// DateTime.Now.ToString("yyyyMMddhhmmss");

     
            try
            {

                TopicList list = new TopicList();

                work_data.goal.loop_flag = nworkcnt;
                work_data.goal.work_id = strWorkid;
                work_data.goal.action_start_idx = nactidx;

                int nactcnt = act.Length;
                for(int i=0; i<nactcnt; i++)
                 work_data.goal.work.Add(act[i]);


                string strobj = JsonConvert.SerializeObject(work_data);
                JObject obj = JObject.Parse(strobj);

                rosinterface ros = new rosinterface();

                ros.PublisherTopicMsgtype(strRobot_id + list.topic_goal, list.msg_goal);
                Thread.Sleep(200);
                ros.publisher(obj);
                Thread.Sleep(200);

                Console.Out.WriteLine("work mission send 1 :={0}", strRobot_id);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkGoal_publish err :={0}", ex.Message.ToString());
            }
        }



        /// <summary>
        /// SP_routine  publish
        ///  topic : $(RID)/sp_routine
        ///  type  : std_msgs/String
        /// </summary>
        public async void onSP_routine_publish(string strRobot, string strmode)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    SPCore sp_core = new SPCore();
                    sp_core.data = strmode;
                   
                    string strobj = JsonConvert.SerializeObject(sp_core);
                    JObject obj = JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_sp_routine, list.msg_sp_routine);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTempoMove_publish err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// map_save  publish
        ///  topic : /RIDiS/save_map_db
        ///  type  : std_msgs/String
        /// </summary>
        public async void onMap_Save_publish( string strdata)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    MAP_Save mapsave = new MAP_Save();
                    mapsave.data = strdata;

                    string strobj = JsonConvert.SerializeObject(mapsave);
                    JObject obj = JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(list.topic_map_save, list.msg_map_save);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTempoMove_publish err :={0}", ex.Message.ToString());
            }
        }


        public void onManualRun(Twist data, string strrobot)
        {
            if (Data.Instance.isConnected)
            {
                try
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();



                    // string strrobot = (string)cboRobotID.SelectedItem;

                    string strobj = JsonConvert.SerializeObject(data);
                    JObject obj = JObject.Parse(strobj);


                    ros.PublisherTopicMsgtype(strrobot + list.topic_cmdvel, list.msg_cmdvel);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("pictureBox_up_left_diagonal_MouseDown err :={0}", ex.Message.ToString());
                }
            }
        }

        #endregion


        #region publish




        public void onPublished(string strtopic, string strmsgtype, JObject obj)
        {

            rosinterface ros = new rosinterface();
            ros.PublisherTopicMsgtype(strtopic, strmsgtype);
            Thread.Sleep(Data.Instance.nPulishDelayTime);
            ros.publisher(obj);
            Thread.Sleep(Data.Instance.nPulishDelayTime);
        }
        #endregion
        public async void onControllerstate_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_controller_state, list.msg_controllerstate, onSubscribe_Recv_Controllerstate);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("oncontrollerstate subscribe error");
            }
        }
        public async void onSelectRobotStatus_Basic_subscribes(string strrobotid)
        {

            try
            {
                TopicList list = new TopicList();
                rosinterface ros = new rosinterface();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate = new RobotState_1();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.motorstate = new MotorInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.lidar = new LidarScan();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.ultrasonic = new UltrasonicRawInfo();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback = new WorkFeedback();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.workresult = new WorkResult();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo = new BMSInfo();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo = new MapInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalcostmap = new MapInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.localcostmap = new MapInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan = new MapPlanInformation();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.ur_status = new UR_StatusInformation();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request = new Work_requestInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.except_status = new Except_StatusInformation();

                //Data.Instance.Robot_work_info[strrobotid].robot_status_info.lift_info = new RobotLiftInfo();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.markers = new MarkerDetection_Information();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.lookahead = new LookAheadInformation();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.currAngluar = new RobotCurrAngluar_Infomation();

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus = new GoalRunnig_StatusInformation();
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate_ = new RobotState();


                Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.topic = strrobotid + list.topic_robot_state;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.motorstate.topic = strrobotid + list.topic_motorState;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.lidar.topic = strrobotid + list.topic_lidarscan;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.ultrasonic.topic = strrobotid + list.topic_ultrasonic_raw;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.workfeedback.topic = strrobotid + list.topic_workfeedback;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.workresult.topic = strrobotid + list.topic_workresult;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.topic = strrobotid + list.topic_bms;


                Data.Instance.Robot_work_info[strrobotid].robot_status_info.ur_status.topic = strrobotid + list.topic_urstatus;

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request.topic = strrobotid + list.topic_workrequest;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.except_status.topic = strrobotid + list.topic_except_check;
                //Data.Instance.Robot_work_info[strrobotid].robot_status_info.lift_info.topic = strrobotid + list.topic_lift_state;

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.markers.topic = strrobotid + list.topic_markerdetect;

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalplan.topic = strrobotid + list.topic_globalPath;

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.lookahead.topic = strrobotid + list.topic_lookahead;
                Data.Instance.Robot_work_info[strrobotid].robot_status_info.currAngluar.topic = strrobotid + list.topic_robotcurrAngluar;

                Data.Instance.Robot_work_info[strrobotid].robot_status_info.goalrunnigstatus.topic = strrobotid + list.topic_GoalrunningStatus;


                onRealtimeRobotStatus_subscribe(); // 로봇 좌표 및 워크스테이트 
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                onRobotPosition_subscribe(strrobotid); // 안씀
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                onMotorState_subscribe(strrobotid); // 모터 상태
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);


                onGlobalPlanner_subscribe(strrobotid); // 로봇 좌표
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                //task 수행시만
                onTaskFeedback_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                onTaskResult_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                onRobotLookAhead_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                onRobotCurrAngluar_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                onBMS_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);


            }
            catch (Exception e)
            {

                Console.WriteLine("에러 : {0}", e);
            }

        }

#region subscribe

        public async void onConnectedDevices_subscribe()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(list.topic_connecteddevices, list.msg_connecteddevices, onSubscribe_Recv_ConnectedDevices);
                    //await ros.AddSubscriber(list.topic_test_1, list.msg_test_1, RealtimeRobotStatus);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onConnectedDevices_subscribe err :={0}", ex.Message.ToString());
            }
        }

        public async void onRealtimeRobotStatus_subscribe()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(list.topic_robotlist, list.msg_robotlist, onSubscribe_Recv_RobotLive);
                    //await ros.AddSubscriber(list.topic_test_1, list.msg_test_1, RealtimeRobotStatus);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRealtimeRobotStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 실시간 로봇 위치 및 리프트 상태 파악
        ///  topic : $(RID)/robot_state
        ///  type  : syscon_msgs/RobotState
        /// </summary>
        public async void onRobotPosition_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_robot_state, list.msg_robotstate, onSubscribe_Recv_RobotPosition);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRealtimeRobotStatus_subscribe err :={0}", ex.Message.ToString());

            }
        }

        /// <summary>
        /// 실시간 로봇 motor , 속도  파악
        ///  topic : $(RID)/motor_state
        ///  type  : syscon_msgs/MotorState
        /// </summary>
        public async void onMotorState_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_motorState, list.msg_motorState, onSubscribe_Recv_MotorState);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMotorStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 실시간 초음파데이타 
        ///  topic : $(RID)/ultrasonic_raw
        ///  type  : std_msgs/Float32MultiArray
        /// </summary>
        public async void onUltraSonicRaw_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_ultrasonic_raw, list.msg_ultrasonic_raw, onSubscribe_Recv_UltraSonicRaw);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMotorStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 실시간 LIDAR 데이타  
        ///  topic : $(RID)/scan
        ///  type  : sensor_msgs/LaserScan
        /// </summary>
        public async void onLidarScan_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_lidarscan, list.msg_lidarscan, onSubscribe_Recv_LidarScan);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLidarScan_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 실시간 BMS 데이타  
        ///  topic : $(RID)/bms
        ///  type  : std_msgs/Float32MultiArray
        /// </summary>
        public async void onBMS_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_bms, list.msg_bms, onSubscribe_Recv_BMS);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onBMS_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 맵 정보  
        ///  topic : $(RID)/map
        ///  type  : nav_msgs/OccupancyGrid
        /// </summary>
        public async void onMapInfo_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_staticMap, list.msg_staticMap, onSubscribe_Recv_MapInfo);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onMapInfo_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 글로벌 코스트맵 정보 
        ///  topic : $(RID)/move_base/global_costmap/costmap
        ///  type  : nav_msgs/OccupancyGrid
        /// </summary>
        public async void onGlobalCostmap_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_globalCost, list.msg_globalCost, onSubscribe_Recv_GlobalCostmap);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onGlobalCostmap_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 로컬 코스트맵 정보   
        ///  topic : $(RID)/move_base/local_costmap/costmap
        ///  type  : nav_msgs/OccupancyGrid
        /// </summary>
        public async void onLocalCostmap_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_localCost, list.msg_localCost, onSubscribe_Recv_LocalCostmap);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLocalCostmap_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 글로벌 plan 정보  
        ///  topic : $(RID)/move_base/GlobalPlanner/plan
        ///  type  : nav_msgs/Path
        /// </summary>
        public async void onGlobalPlanner_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_globalPath, list.msg_globalPath, onSubscribe_Recv_GlobalPlanner);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onGlobalPlanner_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// task feedback subscribe
        /// </summary>
        public async void onTaskFeedback_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();
                    await ros.AddSubscriber(strRobot + list.topic_taskfeedback, list.msg_taskfeedback, onSubscribe_Recv_TaskFeedback);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskFeedback_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// Task결과 subscribe
        /// </summary>
        public async void onTaskResult_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();
                    await ros.AddSubscriber(strRobot + list.topic_taskresult, list.msg_taskresult, onSubscribe_Recv_TaskResult);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onTaskResult_subscribe err :={0}", ex.Message.ToString());
            }
        }



        /// <summary>
        /// 작업 feedback subscribe
        ///  topic : $(RID)/WAS/feedback
        ///  type  : syscon_msgs/WorkFlowActionFeedback
        /// </summary>
        public async void onWorkFeedback_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();
                    await ros.AddSubscriber(strRobot + list.topic_workfeedback, list.msg_workfeedback, onSubscribe_Recv_WorkFeedback);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkFeedback_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 작업 결과 subscribe
        ///  topic : $(RID)/WAS/result
        ///  type  : syscon_msgs/WorkFlowActionResult
        /// </summary>
        public async void onWorkResult_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();
                    await ros.AddSubscriber(strRobot + list.topic_workresult, list.msg_workresult, onSubscribe_Recv_WorkResult);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkResult_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// UR 상태 체크 
        ///  topic : $(RID)/ur_status
        ///  type  : syscon_msgs/URStatus
        /// </summary>
        public async void onURStatus_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_urstatus, list.msg_urstatus, onSubscribe_Recv_URStatus);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRealtimeRobotStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 미션 재요청시 
        ///  topic : $(RID)/WAS/request
        ///  type  : syscon_msgs/WorkRequest
        /// </summary>
        public async void onWorkRequest_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_workrequest, list.msg_workrequest, onSubscribe_Recv_WorkRequest);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkRequest_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 비상상황에 대한 
        ///  topic : $(RID)/except_check
        ///  type  : std_msgs/Int16
        /// </summary>
        public async void onExceptStatus_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_except_check, list.msg_except_check, onSubscribe_Recv_ExceptCheck);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onExceptStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 로봇 리프트 위치 파악
        ///  topic : $(RID)/lift_state
        ///  type  : std_msgs/Int32MultiArray
        /// </summary>
        public async void onRobotLiftStatus_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_lift_state, list.msg_lift_state, onSubscribe_Recv_RobotLiftStatus);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotLiftStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 마커 파악
        ///  topic : $(RID)/ar_pose_marker
        ///  type  : ar_track_alvar_msgs/AlvarMarkers
        /// </summary>
        public async void onRobotMarkerStatus_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_markerdetect, list.msg_markerdetect, onSubscribe_Recv_RobotMarkerStatus);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotMarkerStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 cam1 파악
        ///  topic : $(RID)/cam_1/color/image_raw/compressed
        ///  type  : sensor_msgs/CompressedImage
        /// </summary>
        public async void onRobotCam1Status_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_cam1_info, list.msg_cam1_info, onSubscribe_Recv_RobotCam1Status);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotCam1Status_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 cam2 파악
        ///  topic : $(RID)/cam_2/color/image_raw
        ///  type  : sensor_msgs/CompressedImage
        /// </summary>
        public async void onRobotCam2Status_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_cam2_info, list.msg_cam2_info, onSubscribe_Recv_RobotCam2Status);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotCam2Status_subscribe err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 로봇 LookAhead 파악
        ///  topic : $(RID)/lookahead
        ///  type  : geometry_msgs/Point
        /// </summary>
        public async void onRobotLookAhead_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_lookahead, list.msg_lookahead, onSubscribe_Recv_RobotLookAhead);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotLookAhead_subscribe err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 로봇 자유주행 상태 파악
        ///  topic : $(RID)/move_base/status
        ///  type  : actionlib_msgs/GoalStatusArrray
        /// </summary>
        public async void onRobotMoveBaseStatus(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_GoalrunningStatus, list.msg_GoalrunningStatus, onSubscribe_Recv_RobotMoveBase_Status);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotGoalrunnigStatus err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 로봇 자유주행 상태 파악
        ///  topic : $(RID)/response
        ///  type  : std_msgs/Float32
        /// </summary>
        public async void onRobotCurrAngluar_subscribe(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_robotcurrAngluar, list.msg_robotcurrAngluar, onSubscribe_Recv_RobotCurrAngluar);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotCurrAngluar_subscribe err :={0}", ex.Message.ToString());
            }
        }



        public async void onRequestXisStatus_subscribe()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(list.topic_xisStatus, list.msg_xisStatus, onSubscribe_Recv_XisStatus);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRequestXisStatus_subscribe err :={0}", ex.Message.ToString());
            }
        }

        public async void onSelectRobotMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

            if (Data.Instance.Robot_work_info[strrobotid].robot_status_info == null)
                Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.topic = strrobotid + list.topic_staticMap;

            onMapInfo_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

        }

        public async void onSelectRobotLocalCostMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

            if(Data.Instance.Robot_work_info[strrobotid].robot_status_info==null)
                Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info.localcostmap = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.localcostmap.topic = strrobotid + list.topic_localCost;

            onLocalCostmap_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
        }
        public void onSubscribe_Recv_Controllerstate(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                ControllerState controller = new ControllerState();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    controller = JsonConvert.DeserializeObject<ControllerState>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.controllerstate = controller;

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_Controllerstate err :={0}", ex.Message.ToString());
                //   Console.WriteLine(ex.Message.ToString());
            }
        }
        public async void onSelectRobotCostMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

            if (Data.Instance.Robot_work_info[strrobotid].robot_status_info == null)
                Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalcostmap = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalcostmap.topic = strrobotid + list.topic_globalCost;

            onGlobalCostmap_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

        }


#endregion

#region 콜백 함수
        public void onSubscribe_Recv_ConnectedDevices(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                ConnectedDevices_Info connectdevice = new ConnectedDevices_Info();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                connectdevice = JsonConvert.DeserializeObject<ConnectedDevices_Info>(data);

                if (Data.Instance.ConnectDevicesInfo == null)
                {
                    Data.Instance.ConnectDevicesInfo = new ConnectedDevices_Info();
                }

                Data.Instance.ConnectDevicesInfo = connectdevice;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_ConnectedDevices err :={0}", ex.Message.ToString());
            }
        }
        public void onSubscribe_Recv_RobotLive(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                RobotInformation robotinfo = new RobotInformation();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                robotinfo = JsonConvert.DeserializeObject<RobotInformation>(data);

                if (Data.Instance.robot_liveinfo.robotinfo == null)
                {
                    Data.Instance.robot_liveinfo.robotinfo = new RobotInformation();
                }

                Data.Instance.robot_liveinfo.robotinfo = robotinfo;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotLive err :={0}", ex.Message.ToString());
            }
        }


        public void onSubscribe_Recv_RobotPosition(object sender, MessageReceivedEventArgs e)
        {
            
            try
            {
                string data = e.Message.ToString();
                RobotState_1 robotstate = new RobotState_1();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');


                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    robotstate = JsonConvert.DeserializeObject<RobotState_1>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate = robotstate;

                    if(Data.Instance.bRobotPosRec)
                    {
#if !_mapping
                        RobotState_Evt(strrobotid[0]);
#endif
                    }
                }
                else
                {
                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate = null;
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotPosition err :={0}", ex.Message.ToString());
                
                // Console.WriteLine(ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_MotorState(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                MotorInformation motorstate = new MotorInformation();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    motorstate = JsonConvert.DeserializeObject<MotorInformation>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.motorstate = motorstate;

                    //robotmotorstate_Evt(strrobotid[0]);
                }
                else
                {
                    return;
                }



            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_MotorState err :={0}", ex.Message.ToString());
                // Console.WriteLine(ex.Message.ToString());
            }
        }
        public void onSubscribe_Recv_UltraSonicRaw(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                UltrasonicRawInfo ultrasonicraw = new UltrasonicRawInfo();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    ultrasonicraw = JsonConvert.DeserializeObject<UltrasonicRawInfo>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.ultrasonic = ultrasonicraw;

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_UltraSonicRaw err :={0}", ex.Message.ToString());
                //   Console.WriteLine(ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_LidarScan(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                LidarScan lidarscan = new LidarScan();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    lidarscan = JsonConvert.DeserializeObject<LidarScan>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.lidar = lidarscan;

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_LidarScan err :={0}", ex.Message.ToString());
                //    Console.WriteLine(ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_BMS(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                BMSInfo bmsinfo = new BMSInfo();

    
                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    bmsinfo = JsonConvert.DeserializeObject<BMSInfo>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.bmsinfo = bmsinfo;

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_LidarScan err :={0}", ex.Message.ToString());
                
                // Console.WriteLine(ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_WorkFeedback(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                //if (!Data.Instance.g_bRunning) return;
                string data = e.Message.ToString();

                Dictionary<string, WAS_FEEDBACK> result = new Dictionary<string, WAS_FEEDBACK>();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    WorkFeedback Result = JsonConvert.DeserializeObject<WorkFeedback>(data);


                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workfeedback = Result;

                    int nactionidx = Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workfeedback.msg.feedback.action_indx;

                    Data.Instance.Robot_work_info[strrobotid[0]].nActionidx = nactionidx;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_WorkFeedback err :={0}", ex.Message.ToString());
                Console.WriteLine("onSubscribe_Recv_WorkFeedback err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_WorkResult(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                //if (!Data.Instance.g_bRunning) return;
                string data = e.Message.ToString();

                Dictionary<string, WAS_RESULT> result = new Dictionary<string, WAS_RESULT>();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                Console.Out.WriteLine("work result recv :={0}", strrobotid[0]);

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    var Result = JsonConvert.DeserializeObject<WorkResult>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workresult = Result;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_WorkResult err :={0}", ex.Message.ToString());
            }
        }


        public void onSubscribe_Recv_TaskFeedback(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                //if (!Data.Instance.g_bRunning) return;
                string data = e.Message.ToString();

                Dictionary<string, WAS_FEEDBACK> result = new Dictionary<string, WAS_FEEDBACK>();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    TaskFeedback Result = JsonConvert.DeserializeObject<TaskFeedback>(data);


                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.taskfeedback = Result;
                    taskfeedback_Evt(strrobotid[0]);
                    //Data.Instance.Robot_work_info[strrobotid[0]].mission_complete = Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.taskfeedback.msg.feedback.mission_complete;

                    //Console.Out.WriteLine("task result recv :={0}", strrobotid[0]);
                    //Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.taskresult.msg.= true;


                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("onSubscribe_Recv_TaskFeedback err :={0}", ex.Message.ToString());
            }
        }
        
        public void onSubscribe_Recv_TaskResult(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                //if (!Data.Instance.g_bRunning) return;
                string data = e.Message.ToString();

                Dictionary<string, TARU_RESULT> result = new Dictionary<string, TARU_RESULT>();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                string[] strrobotid = Topic.topic.Split('/');

                Console.Out.WriteLine("태스크 결과 :={0}", strrobotid[0]);
                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.taskfeedback.msg.feedback.task_complete = true;

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  
                {
                    var Result = JsonConvert.DeserializeObject<TaskResult>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.taskresult = Result;
                    taskresult_Evt(strrobotid[0]);



                    //Data.Instance.Robot_work_info[strrobotid[0]].task_finished = true;


                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_TaskResult err :={0}", ex.Message.ToString());
            }
        }


        public void onSubscribe_Recv_MapInfo(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                MapInformation mapinfo = new MapInformation();

                //onDP(Topic.topic, string.Format("{0}", data));


                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    mapinfo = JsonConvert.DeserializeObject<MapInformation>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.mapinfo = mapinfo;

                    mapinfo_Evt(strrobotid[0]);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_MapInfo err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_GlobalCostmap(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                MapInformation costmapinfo = new MapInformation();

                //onDP(Topic.topic, string.Format("{0}", data));


                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    costmapinfo = JsonConvert.DeserializeObject<MapInformation>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.globalcostmap = costmapinfo;
#if !_mapping
                    Globalcostmapinfo_Evt(strrobotid[0]);
#endif

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_GlobalCostmap err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_LocalCostmap(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                MapInformation localcostmapinfo = new MapInformation();

                //onDP(Topic.topic, string.Format("{0}", data));

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    localcostmapinfo = JsonConvert.DeserializeObject<MapInformation>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.localcostmap = localcostmapinfo;
//#if !_mapping
                    Localcostmapinfo_Evt(strrobotid[0]);
//#endif
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_LocalCostmap err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_GlobalPlanner(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                MapPlanInformation mapplan = new MapPlanInformation();
                mapplan = JsonConvert.DeserializeObject<MapPlanInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.globalplan = mapplan;
#if !_mapping
                if (Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Map || Data.Instance.nFormidx == (int)Data.FORM_IDX.Monitoring_Robot)
                {

                    Globalpath_Evt(strrobotid[0]);

                }
#endif
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_GlobalPlanner err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_RobotLookAhead(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                LookAheadInformation lookahead = new LookAheadInformation();
                lookahead = JsonConvert.DeserializeObject<LookAheadInformation>(data);

                Header header = lookahead.msg.header;


                if (lookahead.msg == null) return;

                if (strrobotid[0] == "R_008")
                {
                    //Console.Out.WriteLine(data);
                }

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.lookahead = lookahead;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotLookAhead err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_RobotCurrAngluar(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                RobotCurrAngluar_Infomation info = new RobotCurrAngluar_Infomation();
                info = JsonConvert.DeserializeObject<RobotCurrAngluar_Infomation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.currAngluar = info;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotCurrAngluar err :={0}", ex.Message.ToString());
            }
        }



        public void onSubscribe_Recv_RobotMoveBase_Status(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                GoalRunnig_StatusInformation goalstatus = new GoalRunnig_StatusInformation();
                goalstatus = JsonConvert.DeserializeObject<GoalRunnig_StatusInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.goalrunnigstatus = goalstatus;

                MoveBase_Status_Evt(strrobotid[0]);

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotMoveBase_Status err :={0}", ex.Message.ToString());
            }
        }


        public void onSubscribe_Recv_URStatus(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                UR_StatusInformation ur_statusdata = new UR_StatusInformation();
                ur_statusdata = JsonConvert.DeserializeObject<UR_StatusInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.ur_status = ur_statusdata;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_URStatus err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_WorkRequest(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                Work_requestInformation workrequest = new Work_requestInformation();
                workrequest = JsonConvert.DeserializeObject<Work_requestInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.work_request = workrequest;

#if _sol
                workrequest_Evt(strrobotid[0]);
#endif
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_WorkRequest err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_ExceptCheck(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                if (!Data.Instance.g_bRunning) return;
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                Except_StatusInformation exceptstatus = new Except_StatusInformation();
                exceptstatus = JsonConvert.DeserializeObject<Except_StatusInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.except_status = exceptstatus;
#if _sol
                exceptstatus_Evt(strrobotid[0]);
#endif
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_ExceptCheck err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_RobotLiftStatus(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                RobotLiftInfo liftinfo = new RobotLiftInfo();
                liftinfo = JsonConvert.DeserializeObject<RobotLiftInfo>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.lift_info = liftinfo;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotLiftStatus err :={0}", ex.Message.ToString());
            }
        }
        public void onSubscribe_Recv_RobotMarkerStatus(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                MarkerDetection_Information marker = new MarkerDetection_Information();
                marker = JsonConvert.DeserializeObject<MarkerDetection_Information>(data);

                if (strrobotid[0] == "R_005")
                {
                    //  Console.Out.WriteLine(data);
                }
                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.markers = marker;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotMarkerStatus err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_RobotCam1Status(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                CamInformation cam = new CamInformation();
                cam = JsonConvert.DeserializeObject<CamInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.cam1 = cam;

                //   cam1data_Evt(strrobotid[0]);

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotCam1Status err :={0}", ex.Message.ToString());
            }
        }


        public void onSubscribe_Recv_RobotCam2Status(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                CamInformation cam = new CamInformation();
                cam = JsonConvert.DeserializeObject<CamInformation>(data);

                string strcam = cam.msg.data;


                byte[] bytecam = Convert.FromBase64String(strcam);

                //Encoding.

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.cam2 = cam;

                // cam2data_Evt(strrobotid[0]);

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotCam2Status err :={0}", ex.Message.ToString());
            }
        }

        public void onSubscribe_Recv_XisStatus(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                XISInformation xisinfo = new XISInformation();
                xisinfo = JsonConvert.DeserializeObject<XISInformation>(data);

                if (xisinfo.msg == null) return;
                if (xisinfo.msg.xislist.Count < 1) return;

                string strxid = xisinfo.msg.xislist[0].XID;
                XISState xissta = xisinfo.msg.xislist[0];
                if (Data.Instance.XIS_Status_Info.ContainsKey(xissta.XID))
                {
                    Data.Instance.XIS_Status_Info[strxid] = xissta; ;
                }
                else
                {
                    Data.Instance.XIS_Status_Info.Add(strxid, xissta);
                }

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_XisStatus err :={0}", ex.Message.ToString());
            }
        }



#endregion


#region 요청한 subscribe 삭제 파트

        public async void onDeleteAllSubscribe()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.DeleteAllSubscriber();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("DeleteAllsubscribe err :={0}", ex.Message.ToString());
            }
        }

        public void onDeleteAllSubscribe_Compulsion()
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    ros.DeleteAllSubscriber_Compulsion();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("DeleteAllsubscribe err :={0}", ex.Message.ToString());
            }
        }

        public void onDeleteSelectSubscribe(string strTopic)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    ros.DeleteSubscriber_Compulsion(strTopic);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("DeleteSelectsubscribe err :={0}", ex.Message.ToString());
            }
        }
#endregion
    }
}
