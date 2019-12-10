using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace SysSolution
{
    public class Worker 
    {
        Form1 mainForm;
        DemoForm0131 DemoForm;
        RobotStatusForm robotStatusForm;
        MapDspForm mapdspForm;
        WorkOrderForm workorderForm;
        Frm.DashboardForm dashboardForm;
        UR_Sample.URControl_TestFrm urcontrol_testform;
        RobotStatusForm_One robotstatusform_one;

        FleetManager.FleetManager_MainForm fleetmanager_main;
        Delivery.DeliveryForm deliveryForm1;

        #region Event
        public event InitPosComplete initpos_Evt;
        public event WorkPosComplete workpos_Evt;
        public event LoopPosComplete looppos_Evt;
        public event MapInfoComplete mapinfo_Evt;
        public event GlobalCostInfoComplete Globalcostmapinfo_Evt;

        public event LocalCostInfoComplete Localcostmapinfo_Evt;

        public event GlobalpathComplete Globalpath_Evt;


        public event Work_request_FromRobot workrequest_Evt;  //미션재요청 이벤트
        public event ExceptStatus_FromRobot exceptstatus_Evt; //로봇 비상상황 발생 이벤트


        public event WorkResultResponse workresult_Evt; //20190510 add
        public event WorkFeedbackResponse workfeedback_Evt; //20190510 add

        public event RobotMotorStateResponse robotmotorstate_Evt; //20190523 add
        public event Cam1DataResponse cam1data_Evt; //20190523 add
        public event Cam2DataResponse cam2data_Evt; //20190523 add
        public event RobotPostionResponse robotpositionstate_Evt;


        public delegate void InitPosComplete();
        public delegate void WorkPosComplete(string strrobotid);
        public delegate void LoopPosComplete(string strrobotid);
        public delegate void MapInfoComplete();
        public delegate void GlobalCostInfoComplete();
        public delegate void LocalCostInfoComplete();
        public delegate void Work_request_FromRobot(string strrobotid);
        public delegate void ExceptStatus_FromRobot(string strrobotid);

        public delegate void GlobalpathComplete(string strrobotid);


        public delegate void WorkResultResponse(string strrobotid);
        public delegate void WorkFeedbackResponse(string strrobotid);

        public delegate void RobotMotorStateResponse(string strrobotid);
        public delegate void Cam1DataResponse(string strrobotid);
        public delegate void Cam2DataResponse(string strrobotid);
        public delegate void RobotPostionResponse(string strrobotid); 


        //2019-3-7 ~~~~

        public event RobotLiveCheck robotlivechk_Evt;
        public delegate void RobotLiveCheck();
        #endregion


        public void onevttest()
        {
            initpos_Evt();
        }

        int m_nWorkeridx = 0;

      /*  public string[] ActionList = { "Goal-Point", "Basic-Move", "Lift-Control", "Conveyor-Control", "PLC", "Way-Point" };
        public string[] MoveList = { "Go", "Rotate" };
        public string[] LiftActList = { "Top-Bottom", "Set-Height" };
        public string[] LiftDetailActList = { "Top", "Bottom" };
        public string[] ConvActList = { "Transfer", "Receiver" };
        public string[] ConvDetailActList = { "Forward", "Backward" };
        public string[] PLCActList = { "Send", "Check" };
        public string[] PLCDetailList1 = { "Transfer_Ready", "Transfer_End", "Receive_Ready", "Receive_End" };
        public string[] PLCDetailList2 = { "Transfer_Ready_Check", "Transfer_End_Check", "Receive_Ready_Check", "Receive_End_Check" };
        public string[] QualityList = { "A", "B", "C", "D", "E" };
        public string[] arrParam_name = { "max_trans_acc", "max_rot_acc", "max_trans_vel", "min_trans_vel", "max_rot_vel", "min_rot_vel", "heading_yaw", "ign_ang_err", "min_in_place_rot_vel", "arriving_distance", "clearing_tol_cond", "yaw_goal_tolerance",
            "xy_goal_tolerance", "wp_tolerance", "sim_time", "sim_granularity", "angular_sim_granularity", "controller_freq", "dwa", "dwa_ang_inc", "dwa_lin_dec", "dwa_ang_iter", "dwa_lin_iter"};
        public string[] radianParam = { "max_rot_acc", "max_rot_vel", "min_rot_vel", "heading_yaw", "ign_ang_err", "min_in_place_rot_vel", "clearing_tol_cond", "yaw_goal_tolerance" };
        */

        public Worker()
        {

        }

        public Worker(Form1 frm,int nidx)
        {
            mainForm = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(DemoForm0131 frm, int nidx)
        {
            DemoForm = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(RobotStatusForm frm, int nidx)
        {
            robotStatusForm = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(MapDspForm frm, int nidx)
        {
            mapdspForm = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(WorkOrderForm frm, int nidx)
        {
            workorderForm = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(Frm.DashboardForm frm, int nidx)
        {
            dashboardForm = frm;
            m_nWorkeridx = nidx;
        }


        public Worker(UR_Sample.URControl_TestFrm frm, int nidx)
        {
            urcontrol_testform = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(RobotStatusForm_One frm, int nidx)
        {
            robotstatusform_one = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(FleetManager.FleetManager_MainForm frm, int nidx)
        {
            fleetmanager_main = frm;
            m_nWorkeridx = nidx;
        }

        public Worker(Delivery.DeliveryForm frm, int nidx)
        {
            deliveryForm1 = frm;
            m_nWorkeridx = nidx;
        }

        


        #region subscribe

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

                    await ros.AddSubscriber(strRobot+list.topic_robot_state, list.msg_robotstate, onSubscribe_Recv_RobotPosition);
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

                    await ros.AddSubscriber(strRobot+list.topic_motorState, list.msg_motorState, onSubscribe_Recv_MotorState);
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
                Console.Out.WriteLine("onWorkCancel_publish err :={0}", ex.Message.ToString());
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
        public async void onRobotGoalrunnigStatus(string strRobot)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    await ros.AddSubscriber(strRobot + list.topic_GoalrunningStatus, list.msg_GoalrunningStatus, onSubscribe_Recv_RobotGoalrunningStatus);
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


        #endregion

        #region publish

        public void onPublished(string strtopic, string strmsgtype, JObject obj)
        {

            rosinterface ros = new rosinterface();
            ros.PublisherTopicMsgtype(strtopic, strmsgtype);
            Thread.Sleep(200);
            ros.publisher(obj);
            Thread.Sleep(200);
        }

        #endregion

        #region 작업 지시 관련
        /// <summary>
        /// 로봇이 작업하기전에 로봇정보 구조체를 초기화 하는 부분
        /// </summary>
        public Robot_WorkKInfo onNewRobotWorkInfo_initial(string strrobotid,string strworkid,int workcnt,int nactionidx,string strgoalid,string strworkname)
        {
            Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
            robot_workinfo.robot_workdata = new List<Robot_Work_Data>();

            robot_workinfo.strRobotID = strrobotid;
            robot_workinfo.strWorkID = strworkid;
            robot_workinfo.nWork_cnt = workcnt;
            robot_workinfo.nCurrWork_cnt = 1;
            robot_workinfo.nActionidx = nactionidx;
            robot_workinfo.strGoalid = strgoalid;
            robot_workinfo.strWorkName = strworkname;
            robot_workinfo.strLoop_Flag = "wait";

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

        /// <summary>
        /// 작업 지시 publish
        ///  topic : $(RID)/WAS/goal
        ///  type  : syscon_msgs/WorkFlowActionGoal
        /// </summary>
        public async void onWorkOrder_publish(string strWorkname, string strWorkid, string[] strRobot_array,string[] strData_array, int nWorkcnt, int nactidx)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            WAS_GOAL work_data = new WAS_GOAL();
            work_data.goal_id.id = strWorkid;// DateTime.Now.ToString("yyyyMMddhhmmss");

            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Basic-Move/mode:Go/target:10"
            //"type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom"
            //"type:PLC/mode:Send/action:Transfer_Ready"
            try
            {

                TopicList list = new TopicList();
                for (int nrobotIdx = 0; nrobotIdx < strRobot_array.Length; nrobotIdx++)//선택된 로봇수 만큼 반복
                {
                    

                    //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                    if (Data.Instance.Robot_work_info.ContainsKey(strRobot_array[nrobotIdx]))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkID = strWorkid;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt = nWorkcnt;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt = 1;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx = 0;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strGoalid = work_data.goal_id.id;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkName = strWorkname;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strLoop_Flag = "wait";

                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nTotalActionidx = strData_array.Length;


                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {
                            string straction = strData_array[nworkidx];

                       
                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";
                            else if (straction_sub[0].Split(':')[1].Equals("Action_wait"))
                                stractiontype = "actionwait";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();
                            robot_work_data.strTopic = "";
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;
                            

                            Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Add(robot_work_data);
                        }
                    }
                    else //새로 작업하는 로봇은 새로 추가.. 
                    {
                        Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                        robot_workinfo.robot_workdata = new List<Robot_Work_Data>();


                        robot_workinfo.strRobotID = strRobot_array[nrobotIdx];
                        robot_workinfo.strWorkID = strWorkid;
                        robot_workinfo.nWork_cnt = nWorkcnt;
                        robot_workinfo.nCurrWork_cnt = 1;
                        robot_workinfo.nActionidx = 0;
                        robot_workinfo.strGoalid = work_data.goal_id.id;
                        robot_workinfo.strWorkName = strWorkname;
                        robot_workinfo.strLoop_Flag = "wait";
                        robot_workinfo.nTotalActionidx = strData_array.Length;

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

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {
                            
                            string straction = strData_array[nworkidx];

                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";
                            else if (straction_sub[0].Split(':')[1].Equals("Action_wait"))
                                stractiontype = "actionwait";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();
                            
                            robot_work_data.strTopic = strRobot_array[nrobotIdx] + list.topic_goal;
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;

                            robot_workinfo.robot_workdata.Add(robot_work_data);
                        }

                        Data.Instance.Robot_work_info.Add(strRobot_array[nrobotIdx], robot_workinfo);
                    }

                    //로봇파일에 로봇상태 정보 갱신
                    if (Data.Instance.Robot_status_info.ContainsKey(strRobot_array[nrobotIdx]))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]];
                        string[] strRobotstatus = strtemp.Split(',');
                        strRobotstatus[2] = strWorkid;

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot_array[nrobotIdx]))
                        {
                            strRobotstatus[3] = Data.Instance.robots_currgoing_status[strRobot_array[nrobotIdx]].strStatus;
                        }

                        strRobotstatus[4] = string.Format("{0}",Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt);
                        strRobotstatus[5] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt);
                        strRobotstatus[6] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx);

                        strRobotstatus[7] = "stop";
                        if (Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg == null)
                            strRobotstatus[7] = "stop";
                        else
                        {
                                int nliftstate = Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg.lift_status;
                                if (nliftstate == 0)
                                    strRobotstatus[7] = "stop";
                                else if (nliftstate == 1)
                                    strRobotstatus[7] = "lift_uping";
                                else if (nliftstate == 2)
                                    strRobotstatus[7] = "lift_top";
                                else if (nliftstate == -1)
                                    strRobotstatus[7] = "lift_downing";
                                else if (nliftstate == -2)
                                    strRobotstatus[7] = "lift_bottom";
                            
                            else strRobotstatus[7] = "stop";
                        }
                        
                        strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);

                        Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]] = strtemp;
                    }

                    work_data = onRobotMissionData_Make(strWorkid,work_data, strData_array, nactidx);

                   
                    string strobj = JsonConvert.SerializeObject(work_data);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();

                    Thread.Sleep(500);

                    ros.PublisherTopicMsgtype(strRobot_array[nrobotIdx] + list.topic_goal, list.msg_goal);
                    Thread.Sleep(200);
                    ros.publisher(obj);
                    Thread.Sleep(200);

                    Console.Out.WriteLine("work mission send 1 :={0}", strRobot_array[nrobotIdx]);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkOrder_publish err :={0}", ex.Message.ToString());
            }
        }

        /// <summary>
        /// 작업 지시 publish로 작업지시할때 마다 subscribe를 삭제한것..
        /// subscribe는 처음에만 한번하기로..
        ///  topic : $(RID)/WAS/goal
        ///  type  : syscon_msgs/WorkFlowActionGoal
        /// </summary>
        public async void onWorkOrder_publish_new(string strWorkname, string strWorkid, string[] strRobot_array, string[] strData_array, int nWorkcnt)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            WAS_GOAL work_data = new WAS_GOAL();
            work_data.goal_id.id = strWorkid;// DateTime.Now.ToString("yyyyMMddhhmmss");

            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Basic-Move/mode:Go/target:10"
            //"type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom"
            //"type:PLC/mode:Send/action:Transfer_Ready"
            try
            {

                TopicList list = new TopicList();
                for (int nrobotIdx = 0; nrobotIdx < strRobot_array.Length; nrobotIdx++)//선택된 로봇수 만큼 반복
                {


                    //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                    if (Data.Instance.Robot_work_info.ContainsKey(strRobot_array[nrobotIdx]))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkID = strWorkid;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt = nWorkcnt;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt = 1;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx = 0;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strGoalid = work_data.goal_id.id;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkName = strWorkname;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strLoop_Flag = "wait";
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nTotalActionidx = strData_array.Length;

                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {
                            string straction = strData_array[nworkidx];
                      
                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();
                            robot_work_data.strTopic = "";
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;


                            Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Add(robot_work_data);
                        }
                    }
                    else //새로 작업하는 로봇은 새로 추가.. 
                    {
                        Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                        robot_workinfo.robot_workdata = new List<Robot_Work_Data>();


                        robot_workinfo.strRobotID = strRobot_array[nrobotIdx];
                        robot_workinfo.strWorkID = strWorkid;
                        robot_workinfo.nWork_cnt = nWorkcnt;
                        robot_workinfo.nCurrWork_cnt = 1;
                        robot_workinfo.nActionidx = 0;
                        robot_workinfo.strGoalid = work_data.goal_id.id;
                        robot_workinfo.strWorkName = strWorkname;
                        robot_workinfo.strLoop_Flag = "wait";
                        robot_workinfo.nTotalActionidx = strData_array.Length;


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

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {

                            string straction = strData_array[nworkidx];
                        

                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();

                            robot_work_data.strTopic = strRobot_array[nrobotIdx] + list.topic_goal;
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;

                            robot_workinfo.robot_workdata.Add(robot_work_data);
                        }

                        Data.Instance.Robot_work_info.Add(strRobot_array[nrobotIdx], robot_workinfo);
                    }

                    //로봇파일에 로봇상태 정보 갱신
                    if (Data.Instance.Robot_status_info.ContainsKey(strRobot_array[nrobotIdx]))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]];
                        string[] strRobotstatus = strtemp.Split(',');
                        strRobotstatus[2] = strWorkid;

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot_array[nrobotIdx]))
                        {
                            strRobotstatus[3] = Data.Instance.robots_currgoing_status[strRobot_array[nrobotIdx]].strStatus;
                        }

                        strRobotstatus[4] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt);
                        strRobotstatus[5] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt);
                        strRobotstatus[6] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx);
                        strRobotstatus[7] = "stop";

                        if (Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg == null)
                            strRobotstatus[7] = "stop";
                        else
                        {
                            int nliftstate = Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg.lift_status;
                            if (nliftstate == 0)
                                strRobotstatus[7] = "stop";
                            else if (nliftstate == 1)
                                strRobotstatus[7] = "lift_uping";
                            else if (nliftstate == 2)
                                strRobotstatus[7] = "lift_top";
                            else if (nliftstate == -1)
                                strRobotstatus[7] = "lift_downing";
                            else if (nliftstate == -2)
                                strRobotstatus[7] = "lift_bottom";

                            else strRobotstatus[7] = "stop";
                        }
                        strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);

                        Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]] = strtemp;
                    }

                    work_data = onRobotMissionData_Make(strWorkid,work_data, strData_array, 0);


                    string strobj = JsonConvert.SerializeObject(work_data);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();

                    ros.PublisherTopicMsgtype(strRobot_array[nrobotIdx] + list.topic_goal, list.msg_goal);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                   // onPublished(strRobot_array[nrobotIdx] + list.topic_goal, list.msg_goal, obj);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkOrder_publish err :={0}", ex.Message.ToString());
            }
        }

        public async void onRobotRequest_WorkOrder_publish(string strWorkname, string strWorkid, string[] strRobot_array, string[] strData_array, int nWorkcnt, int nactionidx)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            WAS_GOAL work_data = new WAS_GOAL();
            work_data.goal_id.id = strWorkid;// DateTime.Now.ToString("yyyyMMddhhmmss");

            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Basic-Move/mode:Go/target:10"
            //"type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom"
            //"type:PLC/mode:Send/action:Transfer_Ready"
            //"type:Chain/chain_to:R_005"
            try
            {
                TopicList list = new TopicList();
                for (int nrobotIdx = 0; nrobotIdx < strRobot_array.Length; nrobotIdx++)//선택된 로봇수 만큼 반복
                {
                    //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                    if (Data.Instance.Robot_work_info.ContainsKey(strRobot_array[nrobotIdx]))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkID = strWorkid;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt = nWorkcnt;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt = 1;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx = nactionidx;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strGoalid = work_data.goal_id.id;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strWorkName = strWorkname;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].strLoop_Flag = "wait";
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nTotalActionidx = strData_array.Length;
                        Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {
                            string straction = strData_array[nworkidx];
                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();
                            robot_work_data.strTopic = "";
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;


                            Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_workdata.Add(robot_work_data);
                        }
                    }
                    else //새로 작업하는 로봇은 새로 추가.. 
                    {
                        Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                        robot_workinfo.robot_workdata = new List<Robot_Work_Data>();


                        robot_workinfo.strRobotID = strRobot_array[nrobotIdx];
                        robot_workinfo.strWorkID = strWorkid;
                        robot_workinfo.nWork_cnt = nWorkcnt;
                        robot_workinfo.nCurrWork_cnt = 1;
                        robot_workinfo.nActionidx = nactionidx;
                        robot_workinfo.strGoalid = work_data.goal_id.id;
                        robot_workinfo.strWorkName = strWorkname;
                        robot_workinfo.strLoop_Flag = "wait";
                        robot_workinfo.nTotalActionidx = strData_array.Length;

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

                      //  robot_workinfo.robot_status_info.lift_info = new RobotLiftInfo();

                        for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                        {

                            string straction = strData_array[nworkidx];
                            string[] straction_sub = straction.Split('/');
                            string stractiontype = "";
                            if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                                stractiontype = "주행";
                            else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                                stractiontype = "기본움직임";
                            else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                                stractiontype = "리프트&컨베어";
                            else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                                stractiontype = "PLC";
                            else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                                stractiontype = "Chain";
                            else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                                stractiontype = "URMission";
                            else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                                stractiontype = "stablepallet";

                            Robot_Work_Data robot_work_data = new Robot_Work_Data();

                            robot_work_data.strTopic = strRobot_array[nrobotIdx] + list.topic_goal;
                            robot_work_data.strWorkData = strData_array[nworkidx];
                            robot_work_data.strActionType = stractiontype;

                            robot_workinfo.robot_workdata.Add(robot_work_data);
                        }

                        Data.Instance.Robot_work_info.Add(strRobot_array[nrobotIdx], robot_workinfo);
                    }

                    //로봇파일에 로봇상태 정보 갱신
                    if (Data.Instance.Robot_status_info.ContainsKey(strRobot_array[nrobotIdx]))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]];
                        string[] strRobotstatus = strtemp.Split(',');
                        strRobotstatus[2] = strWorkid;

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strRobot_array[nrobotIdx]))
                        {
                            strRobotstatus[3] = Data.Instance.robots_currgoing_status[strRobot_array[nrobotIdx]].strStatus;
                        }

                        strRobotstatus[4] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nWork_cnt);
                        strRobotstatus[5] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nCurrWork_cnt);
                        strRobotstatus[6] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].nActionidx);
                        strRobotstatus[7] = "stop";
                        if (Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg == null)
                            strRobotstatus[7] = "stop";
                        else
                        {
                            int nliftstate = Data.Instance.Robot_work_info[strRobot_array[nrobotIdx]].robot_status_info.robotstate.msg.lift_status;
                            if (nliftstate == 0)
                                strRobotstatus[7] = "stop";
                            else if (nliftstate == 1)
                                strRobotstatus[7] = "lift_uping";
                            else if (nliftstate == 2)
                                strRobotstatus[7] = "lift_top";
                            else if (nliftstate == -1)
                                strRobotstatus[7] = "lift_downing";
                            else if (nliftstate == -2)
                                strRobotstatus[7] = "lift_bottom";

                            else strRobotstatus[7] = "stop";
                        }
                        strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);


                        Data.Instance.Robot_status_info[strRobot_array[nrobotIdx]] = strtemp;
                    }

                    work_data = onRobotMissionData_Make(strWorkid,work_data, strData_array, nactionidx);


                    string strobj = JsonConvert.SerializeObject(work_data);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();
                    
                    ros.PublisherTopicMsgtype(strRobot_array[nrobotIdx] + list.topic_goal, list.msg_goal);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                     //                  onPublished(strRobot_array[nrobotIdx] + list.topic_goal, list.msg_goal, obj);
                    //onSelectRobotStatus_subscribe(strRobot_array[nrobotIdx]);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onRobotRequest_WorkOrder_publish err :={0}", ex.Message.ToString());
            }
        }


        public async void onLoopWork_Publish(string strrobot, int ncurrworkcnt)
        {
            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Basic-Move/mode:Go/target:10"
            //"type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom"
            //"type:PLC/mode:Send/action:Transfer_Ready"
            try
            {

                TopicList list = new TopicList();

                //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                if (Data.Instance.Robot_work_info.ContainsKey(strrobot))  //기존에 작업된 로봇은 정보만 업데이트
                {

                    WAS_GOAL work_data = new WAS_GOAL();
                    work_data.goal_id.id = Data.Instance.Robot_work_info[strrobot].strGoalid;


                    Data.Instance.Robot_work_info[strrobot].nCurrWork_cnt = ncurrworkcnt;
                    Data.Instance.Robot_work_info[strrobot].strLoop_Flag = "wait";

                    string strWorkid = Data.Instance.Robot_work_info[strrobot].strWorkID;


                    string[] strdata_array = new string[Data.Instance.Robot_work_info[strrobot].robot_workdata.Count];
                    for (int i = 0; i < Data.Instance.Robot_work_info[strrobot].robot_workdata.Count; i++)
                    {
                        strdata_array[i] = Data.Instance.Robot_work_info[strrobot].robot_workdata[i].strWorkData;
                    }

                    work_data = onRobotMissionData_Make(strWorkid,work_data, strdata_array,0);

                    string strobj = JsonConvert.SerializeObject(work_data);
                    JObject obj = JObject.Parse(strobj);

                    rosinterface ros = new rosinterface();

                    //Thread.Sleep(3000);

                    //onSelectRobotStatus_subscribe(Data.Instance.Robot_work_info[strrobot].strRobotID);

                    Thread.Sleep(500);
                    
                    ros.PublisherTopicMsgtype(Data.Instance.Robot_work_info[strrobot].strRobotID + list.topic_goal, list.msg_goal);
                    Thread.Sleep(200);
                    ros.publisher(obj);
                    Thread.Sleep(200);
                    
                    //onPublished(Data.Instance.Robot_work_info[strrobot].strRobotID + list.topic_goal, list.msg_goal, obj);

                    Console.Out.WriteLine("loop mission send {0}", strrobot);

                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(500);
                        int ncnt = Data.Instance.Robot_work_info[strrobot].robot_status_info.goalrunnigstatus.msg.status_list.Count;

                        if (ncnt > 0)
                        {
                            if (Data.Instance.Robot_work_info[strrobot].robot_status_info.goalrunnigstatus.msg.status_list[ncnt - 1].status != 1)
                            {
                               
                               ros.PublisherTopicMsgtype(Data.Instance.Robot_work_info[strrobot].strRobotID + list.topic_goal, list.msg_goal);
                               Thread.Sleep(200);
                               ros.publisher(obj);
                               Thread.Sleep(200);
                               
                               //onPublished(Data.Instance.Robot_work_info[strrobot].strRobotID + list.topic_goal, list.msg_goal, obj);

                                Console.Out.WriteLine("loop mission retry send {0}", strrobot);
                            }
                            else break;
                        }
                        
                    }
                 

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onLoopWork_Publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 작업 지시 publish
        ///  topic : $(RID)/WAS/goal
        ///  type  : syscon_msgs/WorkFlowActionGoal
        /// </summary>
        //public async void onFleetManager_WorkOrder_publish(string strWorkname, string strWorkid, string[] strRobot_array, string[] strData_array, int nWorkcnt, int nactidx)
        public async void onFleetManager_WorkOrder_publish(string strWorkname, string strWorkid, string strRobot_id, string[] strData_array, int nWorkcnt, int nactidx)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            WAS_GOAL work_data = new WAS_GOAL();
            work_data.goal_id.id = strWorkid;// DateTime.Now.ToString("yyyyMMddhhmmss");

            //"type:Goal-Point/x:1.5/y:-8/theta:0.0/qual:C/max_trans_acc:1.0/max_rot_acc:11.45/max_trans_vel:0.7/min_trans_vel:0.1/max_rot_vel:17.18/min_rot_vel:8.6/heading_yaw:30/ign_ang_err:5.72/min_in_place_rot_vel:30/arriving_distance:0.5/clearing_tol_cond:5.15/yaw_goal_tolerance:8/xy_goal_tolerance:0.2/wp_tolerance:1.5/sim_time:1.5/sim_granularity:0.025/angular_sim_granularity:0.025/controller_freq:1.5/dwa:true/dwa_ang_inc:0.1/dwa_lin_dec:0.1/dwa_ang_iter:4/dwa_lin_iter:3"
            //"type:Basic-Move/mode:Go/target:10"
            //"type:Lift-Conveyor-Control/mode:Top-Bottom/action:Bottom"
            //"type:PLC/mode:Send/action:Transfer_Ready"
            try
            {

                TopicList list = new TopicList();
               
                //Robot_WorkKInfo 에 정보 입력(로봇 id, work id , work 내용, 로봇 상태 저장 등 )
                if (Data.Instance.Robot_work_info.ContainsKey(strRobot_id))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    Data.Instance.Robot_work_info[strRobot_id].strWorkID = strWorkid;
                    Data.Instance.Robot_work_info[strRobot_id].nWork_cnt = 1;
                    Data.Instance.Robot_work_info[strRobot_id].nCurrWork_cnt = 1;
                    Data.Instance.Robot_work_info[strRobot_id].nActionidx = 0;
                    Data.Instance.Robot_work_info[strRobot_id].strGoalid = work_data.goal_id.id;
                    Data.Instance.Robot_work_info[strRobot_id].strWorkName = strWorkname;
                    Data.Instance.Robot_work_info[strRobot_id].strLoop_Flag = "wait";

                    Data.Instance.Robot_work_info[strRobot_id].nTotalActionidx = strData_array.Length;


                    Data.Instance.Robot_work_info[strRobot_id].robot_workdata.Clear(); //기존 로봇데이타 클리어. 

                    for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                    {
                        string straction = strData_array[nworkidx];


                        string[] straction_sub = straction.Split('/');
                        string stractiontype = "";
                        if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                            stractiontype = "주행";
                        else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                            stractiontype = "기본움직임";
                        else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                            stractiontype = "리프트&컨베어";
                        else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                            stractiontype = "PLC";
                        else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                            stractiontype = "Chain";
                        else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                            stractiontype = "URMission";
                        else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                            stractiontype = "stablepallet";
                        else if (straction_sub[0].Split(':')[1].Equals("Action_wait"))
                            stractiontype = "actionwait";

                        Robot_Work_Data robot_work_data = new Robot_Work_Data();
                        robot_work_data.strTopic = "";
                        robot_work_data.strWorkData = strData_array[nworkidx];
                        robot_work_data.strActionType = stractiontype;


                        Data.Instance.Robot_work_info[strRobot_id].robot_workdata.Add(robot_work_data);
                    }
                }
                else //새로 작업하는 로봇은 새로 추가.. 
                {
                    Robot_WorkKInfo robot_workinfo = new Robot_WorkKInfo();
                    robot_workinfo.robot_workdata = new List<Robot_Work_Data>();


                    robot_workinfo.strRobotID = strRobot_id;
                    robot_workinfo.strWorkID = strWorkid;
                    robot_workinfo.nWork_cnt = 1;
                    robot_workinfo.nCurrWork_cnt = 1;
                    robot_workinfo.nActionidx = 0;
                    robot_workinfo.strGoalid = work_data.goal_id.id;
                    robot_workinfo.strWorkName = strWorkname;
                    robot_workinfo.strLoop_Flag = "wait";
                    robot_workinfo.nTotalActionidx = strData_array.Length;

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

                    for (int nworkidx = 0; nworkidx < strData_array.Length; nworkidx++)
                    {

                        string straction = strData_array[nworkidx];

                        string[] straction_sub = straction.Split('/');
                        string stractiontype = "";
                        if (straction_sub[0].Split(':')[1].Equals("Goal-Point"))
                            stractiontype = "주행";
                        else if (straction_sub[0].Split(':')[1].Equals("Basic-Move"))
                            stractiontype = "기본움직임";
                        else if (straction_sub[0].Split(':')[1].Equals("Lift-Conveyor-Control"))
                            stractiontype = "리프트&컨베어";
                        else if (straction_sub[0].Split(':')[1].Equals("PLC"))
                            stractiontype = "PLC";
                        else if (straction_sub[0].Split(':')[1].Equals("Chain"))
                            stractiontype = "Chain";
                        else if (straction_sub[0].Split(':')[1].Equals("URMISSION"))
                            stractiontype = "URMission";
                        else if (straction_sub[0].Split(':')[1].Equals("Stable_pallet"))
                            stractiontype = "stablepallet";
                        else if (straction_sub[0].Split(':')[1].Equals("Action_wait"))
                            stractiontype = "actionwait";

                        Robot_Work_Data robot_work_data = new Robot_Work_Data();

                        robot_work_data.strTopic = strRobot_id + list.topic_goal;
                        robot_work_data.strWorkData = strData_array[nworkidx];
                        robot_work_data.strActionType = stractiontype;

                        robot_workinfo.robot_workdata.Add(robot_work_data);
                    }

                    Data.Instance.Robot_work_info.Add(strRobot_id, robot_workinfo);
                }

                //로봇파일에 로봇상태 정보 갱신
                if (Data.Instance.Robot_status_info.ContainsKey(strRobot_id))
                {
                    string strtemp = Data.Instance.Robot_status_info[strRobot_id];
                    string[] strRobotstatus = strtemp.Split(',');
                    strRobotstatus[2] = strWorkid;

                    strRobotstatus[4] = string.Format("{0}", Data.Instance.Robot_work_info[strRobot_id].nActionidx);


                    strtemp = string.Format("{0},{1},{2},{3},{4}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4]);

                    Data.Instance.Robot_status_info[strRobot_id] = strtemp;
                }

                work_data = onFleet_RobotMissionData_Make(strWorkid, work_data, strData_array, nactidx, nWorkcnt);


                string strobj = JsonConvert.SerializeObject(work_data);
                JObject obj = JObject.Parse(strobj);

                rosinterface ros = new rosinterface();

                Thread.Sleep(500);

                ros.PublisherTopicMsgtype(strRobot_id + list.topic_goal, list.msg_goal);
                Thread.Sleep(200);
                ros.publisher(obj);
                Thread.Sleep(200);

                Console.Out.WriteLine("work mission send 1 :={0}", strRobot_id);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onFleetManager_WorkOrder_publish err :={0}", ex.Message.ToString());
            }
        }



        public WAS_GOAL onRobotMissionData_Make(string strWorkid, WAS_GOAL work_data, string[] strData_array,int nactionidx)
        {
            work_data.goal.work_id = strWorkid;
            
            work_data.goal.action_start_idx = nactionidx;
            work_data.goal.loop_flag = 1;

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


                                bool diffChek = false;

                                
                                param.value = strgoal_sub_params[1];

                                if (strgoal_sub_params[1] == "true" || strgoal_sub_params[1] == "false")
                                    param.type = "bool";
                                else
                                    param.type = "float";

                                // }
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

                            if(strmode[1]=="0")
                                act.action_args.Add(0);
                            else act.action_args.Add(float.Parse(strdata[1]));

                        }
                        work_data.goal.work.Add(act);
                    }
                }
            }

            return work_data;
        }

        public WAS_GOAL onFleet_RobotMissionData_Make(string strWorkid, WAS_GOAL work_data, string[] strData_array, int nactionidx, int nworkcnt)
        {
            work_data.goal.work_id = strWorkid;
            work_data.goal.action_start_idx = nactionidx;
            work_data.goal.loop_flag = nworkcnt;

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


                                bool diffChek = false;


                                param.value = strgoal_sub_params[1];

                                if (strgoal_sub_params[1] == "true" || strgoal_sub_params[1] == "false")
                                    param.type = "bool";
                                else
                                    param.type = "float";

                                // }
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
                        work_data.goal.work.Add(act);
                    }
                }
            }

            return work_data;
        }

        public float DegreeToRadian(string degree)
        {
            return ((float)(Math.PI / 180.0f) * float.Parse(degree));
        }

        public float RadianToDegree(string radian)
        {
            return (float)((float.Parse(radian) * 180.0f) / Math.PI);// ((float)(Math.PI / 180.0f) * float.Parse(radian));
        }


        /// <summary>
        /// 작업 지시 취소 publish
        ///  topic : $(RID)/WAS/cancel
        ///  type  : actionlib_msgs/GoalID
        /// </summary>
        public async void onWorkCancel_publish(string strRobot,string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    GoalID goal_id = new GoalID();
                    goal_id.id = strgoal_id;

                    string strobj =  JsonConvert.SerializeObject(goal_id);
                    JObject obj = new JObject();// JObject.Parse(strobj);
                    
                    ros.PublisherTopicMsgtype(strRobot + list.topic_workcancle, list.msg_workcancle);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                   // onPublished(strRobot + list.topic_workcancle, list.msg_workcancle, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkCancel_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 작업 지시 일시정지 publish
        ///  topic : $(RID)/WAS/stop
        ///  type  : std_msgs/String
        /// </summary>
        public async void onWorkPause_publish(string strRobot, string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    WorkPauseArg workpausearg = new WorkPauseArg();
                    workpausearg.data = "";

                    string strobj = JsonConvert.SerializeObject(workpausearg);
                    JObject obj =  JObject.Parse(strobj);

                    ros.PublisherTopicMsgtype(strRobot + list.topic_workpause, list.msg_workpause);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                   // onPublished(strRobot + list.topic_workpause, list.msg_workpause, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkPause_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 작업 지시 재시작 publish
        ///  topic : $(RID)/WAS/resume
        ///  type  : std_msgs/String
        /// </summary>
        public async void onWorkResume_publish(string strRobot, string strgoal_id)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    WorkResumeArg workresumearg = new WorkResumeArg();
                    workresumearg.data = "";

                    string strobj = JsonConvert.SerializeObject(workresumearg);
                    JObject obj = JObject.Parse(strobj);

                    
                    ros.PublisherTopicMsgtype(strRobot + list.topic_workresume, list.msg_workresume);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                    ///onPublished(strRobot + list.topic_workresume, list.msg_workresume, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkResume_publish err :={0}", ex.Message.ToString());
            }
        }


        /// <summary>
        /// 작업 지시 바로 정지 publish
        ///  topic : $(RID)/move_base/cancel
        ///  type  : actionlib_msgs/GoalID
        /// </summary>
        public async void onWorkMoveStop_publish(string strRobot, string strgoal_id)
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
                    JObject obj = new JObject();// JObject.Parse(strobj);
                                                
                                                ros.PublisherTopicMsgtype(strRobot + list.topic_workmovestop, list.msg_workmovestop);
                                                Thread.Sleep(Data.Instance.nPulishDelayTime);
                                                ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                   
                   // onPublished(strRobot + list.topic_workmovestop, list.msg_workmovestop, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkCancel_publish err :={0}", ex.Message.ToString());
            }
        }



        /// <summary>
        /// set liftstaus publish
        ///  topic : $(RID)/set_liftstatus
        ///  type  : std_msgs/Int32
        /// </summary>
        public async void onRobot_SetLiftstatus_publish(string strRobot, LiftStatus_Set liftstatus_set)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();
                    TopicList list = new TopicList();

                    
                    string strobj = JsonConvert.SerializeObject(liftstatus_set);
                    JObject obj = new JObject();// JObject.Parse(strobj);

                     ros.PublisherTopicMsgtype(strRobot + list.topic_set_liftstatus, list.msg_set_liftstatus);
                     Thread.Sleep(Data.Instance.nPulishDelayTime);
                     ros.publisher(obj);
                    Thread.Sleep(Data.Instance.nPulishDelayTime);
                    
                   // onPublished(strRobot + list.topic_set_liftstatus, list.msg_set_liftstatus, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onWorkCancel_publish err :={0}", ex.Message.ToString());
            }
        }



        public async void onSelectRobotStatus_subscribe(string strrobotid)
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

            onRobotPosition_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onMotorState_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onGlobalPlanner_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

#if _fleet
            onWorkFeedback_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onWorkResult_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotLookAhead_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotGoalrunnigStatus(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotCam1Status_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onRobotCam2Status_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotCurrAngluar_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            // onSelectRobotLocalCostMap_monitor_subscribe(strrobotid);
            // Thread.Sleep(Data.Instance.nSubscribeDelayTime);
#endif
#if _workorder
            onWorkFeedback_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onWorkResult_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

         //   onURStatus_subscribe(strrobotid);
         //   Thread.Sleep(Data.Instance.nSubscribeDelayTime);

         //   onWorkRequest_subscribe(strrobotid);
         //   Thread.Sleep(Data.Instance.nSubscribeDelayTime);
         //   onExceptStatus_subscribe(strrobotid);
         //   Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotLookAhead_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotGoalrunnigStatus(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
#endif
#if _sol
            onRobotMarkerStatus_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onLidarScan_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
           // onUltraSonicRaw_subscribe(strrobotid);
          //  Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onBMS_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            if(strrobotid=="R_007")
            {
                onRobotCam1Status_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                onRobotCam2Status_subscribe(strrobotid);
                Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            }
#endif
#if _statusone
            
            onLidarScan_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onBMS_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onRobotCam1Status_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onRobotCam2Status_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            
#endif
            //onRobotLiftStatus_subscribe(strrobotid);
            //Thread.Sleep(Data.Instance.nSubscribeDelayTime);
        }


        public async void onSelectXIS_subscribe()
        {
            onRequestXisStatus_subscribe();
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
        }


        public async void onSelectRobotStatus_monitor_subscribe(string strrobotid)
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

            Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request = new Work_requestInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.except_status = new Except_StatusInformation();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info.robotstate.topic = strrobotid + list.topic_robot_state;
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.motorstate.topic = strrobotid + list.topic_motorState;
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.lidar.topic = strrobotid + list.topic_lidarscan;
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.ultrasonic.topic = strrobotid + list.topic_ultrasonic_raw;
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.bmsinfo.topic = strrobotid + list.topic_bms;


            Data.Instance.Robot_work_info[strrobotid].robot_status_info.work_request.topic = strrobotid + list.topic_workrequest;
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.except_status.topic = strrobotid + list.topic_except_check;


            onRobotPosition_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onMotorState_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onLidarScan_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onUltraSonicRaw_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onBMS_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

            onWorkRequest_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
            onExceptStatus_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
        }

        public async void onSelectRobotMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

                     Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();
            
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.mapinfo.topic = strrobotid + list.topic_staticMap;
            
            onMapInfo_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
           
        }

        public async void onSelectRobotCostMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();
          
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalcostmap = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.globalcostmap.topic = strrobotid + list.topic_globalCost;

            onGlobalCostmap_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);

        }


        public async void onSelectRobotLocalCostMap_monitor_subscribe(string strrobotid)
        {
            TopicList list = new TopicList();
            rosinterface ros = new rosinterface();

            Data.Instance.Robot_work_info[strrobotid].robot_status_info = new Robot_Status_info();
           
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.localcostmap = new MapInformation();
            Data.Instance.Robot_work_info[strrobotid].robot_status_info.localcostmap.topic = strrobotid + list.topic_localCost;

            onLocalCostmap_subscribe(strrobotid);
            Thread.Sleep(Data.Instance.nSubscribeDelayTime);
        }

        /// <summary>
        /// 로봇 작업 지시 취소시 subscrbie한 항목들 삭제
        /// </summary>
        public void onDeleteSelectRobotSubscribe(string strrobot)
        {

            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(strrobot))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        // if (!DemoForm.chkMonitoringFlag.Checked)
                        //  {
                        //unsubscribe
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.motorstate.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.lidar.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.ultrasonic.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.workfeedback.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.workresult.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.bmsinfo.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);


                        //onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.lift_info.topic);
                        //Thread.Sleep(Data.Instance.nSubscribeDelayTime);



                        Thread.Sleep(Data.Instance.nWorkDelayTime);
                            //로봇파일에 로봇상태 정보 갱신
                            if (Data.Instance.Robot_status_info.ContainsKey(strrobot))
                            {
                                string strtemp = Data.Instance.Robot_status_info[strrobot];
                                string[] strRobotstatus = strtemp.Split(',');
                                strRobotstatus[2] = "";
                                strRobotstatus[3] = "wait";
                                strRobotstatus[4] = "1";
                                strRobotstatus[5] = "1";
                                strRobotstatus[6] = "0";
                                strRobotstatus[7] = "stop";

                            strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);

                                Data.Instance.Robot_status_info[strrobot] = strtemp;

                            }

                            //로봇정보 삭제
                            Data.Instance.Robot_work_info.Remove(strrobot);
                         //   if(Data.Instance.robots_currgoing_status.ContainsKey(strrobot))
                         //   {
                          //  Data.Instance.robots_currgoing_status.Remove(strrobot);
                         //   }
                        }
                    }
                    else
                    {
                        return;
                    }
             //   }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDeleteSelectRobotSubscribe err :={0}", ex.Message.ToString());
               // Console.WriteLine(ex.Message.ToString());
            }
        }


        public void onDeleteSelectRobot_monitor_Subscribe(string strrobot)
        {

            try
            {
                if (Data.Instance.isConnected)
                {
                    if (Data.Instance.Robot_work_info.ContainsKey(strrobot))  //기존에 작업된 로봇은 정보만 업데이트
                    {
                        // if (!DemoForm.chkMonitoringFlag.Checked)
                        //  {
                        //unsubscribe
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.robotstate.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.motorstate.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.lidar.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.ultrasonic.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);
                        onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.bmsinfo.topic);
                        Thread.Sleep(Data.Instance.nSubscribeDelayTime);

                        //onDeleteSelectSubscribe(Data.Instance.Robot_work_info[strrobot].robot_status_info.lift_info.topic);

                        //로봇정보 삭제
                        Data.Instance.Robot_work_info.Remove(strrobot);
                      //  if (Data.Instance.robots_currgoing_status.ContainsKey(strrobot))
                    //    {
                      //      Data.Instance.robots_currgoing_status.Remove(strrobot);
                      //  }
                    }
                }
                else
                {
                    return;
                }
                //   }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onDeleteSelectRobot_monitor_Subscribe err :={0}", ex.Message.ToString());
                //Console.WriteLine(ex.Message.ToString());
            }
        }


        

#endregion


#region 콜백 함수
        public void onSubscribe_Recv_RobotLive(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                RobotInformation robotinfo = new RobotInformation();
                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                //onDP(Topic.topic, string.Format("{0}", data));

                robotinfo = JsonConvert.DeserializeObject<RobotInformation>(data);

                if(Data.Instance.robot_liveinfo.robotinfo==null)
                {
                    Data.Instance.robot_liveinfo.robotinfo = new RobotInformation();
                }

               Data.Instance.robot_liveinfo.robotinfo = robotinfo;
#if _sol
                robotlivechk_Evt();
#endif
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

                onDP(Topic.topic, string.Format("{0}", data));
               
              

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    robotstate = JsonConvert.DeserializeObject<RobotState_1>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate = robotstate;
#if _fleet
                   // robotpositionstate_Evt(strrobotid[0]);
#endif

                }
                else
                {
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

                onDP(Topic.topic, string.Format("{0}", data));

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

                onDP(Topic.topic, string.Format("{0}", data));

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

                onDP(Topic.topic, string.Format("{0}", data));


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

                onDP(Topic.topic, string.Format("{0}", data));


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

                onDP(Topic.topic, string.Format("{0}", data));

                string[] strrobotid = Topic.topic.Split('/');

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    WorkFeedback Result = JsonConvert.DeserializeObject<WorkFeedback>(data);


                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workfeedback = Result;

                    int nactionidx= Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workfeedback.msg.feedback.action_indx;

                    Data.Instance.Robot_work_info[strrobotid[0]].nActionidx = nactionidx;

#if (_fleet || _fleetdemo)
                    if (Data.Instance.Robot_status_info.ContainsKey(strrobotid[0]))
                    {
                        workfeedback_Evt(strrobotid[0]);
                        return;
                    }
#endif

                    if (Data.Instance.Robot_status_info.ContainsKey(strrobotid[0]))
                    {
                        string strtemp = Data.Instance.Robot_status_info[strrobotid[0]];
                        string[] strRobotstatus = strtemp.Split(',');
                        strRobotstatus[2] = Data.Instance.Robot_work_info[strrobotid[0]].strWorkID;

                        if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid[0]))
                        {
                            strRobotstatus[3] = Data.Instance.robots_currgoing_status[strrobotid[0]].strStatus;
                        }

                        strRobotstatus[4] = string.Format("{0}", Data.Instance.Robot_work_info[strrobotid[0]].nWork_cnt);
                        strRobotstatus[5] = string.Format("{0}", Data.Instance.Robot_work_info[strrobotid[0]].nCurrWork_cnt);
                        strRobotstatus[6] = string.Format("{0}", nactionidx);
                        strRobotstatus[7] = "stop";
                        if (Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate.msg == null)
                            strRobotstatus[7] = "stop";
                        else
                        {
                            int nliftstate = Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate.msg.lift_status;
                            if (nliftstate == 0)
                                strRobotstatus[7] = "stop";
                            else if (nliftstate == 1)
                                strRobotstatus[7] = "lift_uping";
                            else if (nliftstate == 2)
                                strRobotstatus[7] = "lift_top";
                            else if (nliftstate == -1)
                                strRobotstatus[7] = "lift_downing";
                            else if (nliftstate == -2)
                                strRobotstatus[7] = "lift_bottom";

                            else strRobotstatus[7] = "stop";
                        }
                        strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);


                        Data.Instance.Robot_status_info[strrobotid[0]] = strtemp;
                    }



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

                if (strrobotid[0] == "R_007") return;


                onDP(Topic.topic, string.Format("{0}", data));

                 Console.Out.WriteLine("work result recv :={0}", strrobotid[0]);

                if (Data.Instance.Robot_work_info.ContainsKey(strrobotid[0]))  //기존에 작업된 로봇은 정보만 업데이트
                {
                    var Result = JsonConvert.DeserializeObject<WorkResult>(data);

                    Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.workresult = Result;
                    int nworkcnt = 0;
                    nworkcnt = Data.Instance.Robot_work_info[strrobotid[0]].nCurrWork_cnt;
#if (_fleet || _fleetdemo)

                    workresult_Evt(strrobotid[0]);
                    return;
#endif

                   // workrequest_Evt(strrobotid[0]);

                    if (nworkcnt < Data.Instance.Robot_work_info[strrobotid[0]].nWork_cnt)
                    {
                        if (Data.Instance.Robot_status_info.ContainsKey(strrobotid[0]))
                        {
                            string strtemp = Data.Instance.Robot_status_info[strrobotid[0]];
                            string[] strRobotstatus = strtemp.Split(',');
                            strRobotstatus[2] = Data.Instance.Robot_work_info[strrobotid[0]].strWorkID;
                            if (Data.Instance.robots_currgoing_status.ContainsKey(strrobotid[0]))
                            {
                                strRobotstatus[3] = Data.Instance.robots_currgoing_status[strrobotid[0]].strStatus;
                            }
                            strRobotstatus[4] = string.Format("{0}", Data.Instance.Robot_work_info[strrobotid[0]].nWork_cnt);
                            strRobotstatus[5] = string.Format("{0}", nworkcnt+1);
                            strRobotstatus[6] = "0";

                            if (Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate.msg == null)
                                strRobotstatus[7] = "stop";
                            else
                            {
                                int nliftstate = Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.robotstate.msg.lift_status;
                                if (nliftstate == 0)
                                    strRobotstatus[7] = "stop";
                                else if (nliftstate == 1)
                                    strRobotstatus[7] = "lift_uping";
                                else if (nliftstate == 2)
                                    strRobotstatus[7] = "lift_top";
                                else if (nliftstate == -1)
                                    strRobotstatus[7] = "lift_downing";
                                else if (nliftstate == -2)
                                    strRobotstatus[7] = "lift_bottom";

                                else strRobotstatus[7] = "stop";
                            }
                            strtemp = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", strRobotstatus[0], strRobotstatus[1], strRobotstatus[2], strRobotstatus[3], strRobotstatus[4], strRobotstatus[5], strRobotstatus[6], strRobotstatus[7]);

                            Data.Instance.Robot_status_info[strrobotid[0]] = strtemp;

                        }

                        Data.Instance.Robot_work_info[strrobotid[0]].strLoop_Flag = "loop";

                        looppos_Evt(strrobotid[0]);

                    }
                    else
                    {
                        //log 기록 

#if _sol

#elif _workorder

#elif (_fleet || _fleetdemo)

                    
#else
                    //unsubscribe
                    onDeleteSelectRobotSubscribe(strrobotid[0]);
                    Thread.Sleep(100);
#endif


#if _demo
                        initpos_Evt();
#elif _workorder
                        workpos_Evt(strrobotid[0]);
#endif

                    }

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

                    mapinfo_Evt();

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

                    Globalcostmapinfo_Evt();

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

                    Localcostmapinfo_Evt();

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

#if _sol
                Globalpath_Evt(strrobotid[0]);
#elif _workorder
                Globalpath_Evt(strrobotid[0]);
#elif _statusone
                Globalpath_Evt(strrobotid[0]);
#elif _fleet || _fleetdemo
                Globalpath_Evt(strrobotid[0]);
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
        


        public void onSubscribe_Recv_RobotGoalrunningStatus(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);
                string[] strrobotid = Topic.topic.Split('/');

                GoalRunnig_StatusInformation goalstatus = new GoalRunnig_StatusInformation();
                goalstatus = JsonConvert.DeserializeObject<GoalRunnig_StatusInformation>(data);

                Data.Instance.Robot_work_info[strrobotid[0]].robot_status_info.goalrunnigstatus = goalstatus;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribe_Recv_RobotGoalrunningStatus err :={0}", ex.Message.ToString());
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

                Work_requestInformation workrequest=  new Work_requestInformation();
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

                if(strrobotid[0]=="R_005")
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

                
                byte []bytecam =Convert.FromBase64String(strcam);

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

        public  void onDeleteAllSubscribe_Compulsion()
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

        public  void onDeleteSelectSubscribe(string strTopic)
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


        public void onDP(string strtopic, string data)
        {
#if _robotstatus
            robotStatusForm.updateDP(strtopic, string.Format("{0}", data), "0");
#elif _demo
            DemoForm.updateDP(strtopic, string.Format("{0}", data), "0");

#elif _map
           mapdspForm.updateDP(strtopic, string.Format("{0}", data), "0");
#endif
            // mainForm.updateDP(strtopic, string.Format("{0}", data), "0");
        }

#region test
        public async void onSubscribeTest()
        {
            recvcnt = new int[50]; 
            try
            {
                if (Data.Instance.isConnected)
                {
                    TopicList list = new TopicList();
                    rosinterface ros = new rosinterface();

                    for (int i = 0; i < 50; i++)
                    {
                        await ros.AddSubscriber(string.Format("com_test/pub{0}",i), "std_msgs/Float32", onSubscribe_Recv_Test);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onSubscribeTest err :={0}", ex.Message.ToString());
            }
        }
        int[] recvcnt = new int[50];
        public void onSubscribe_Recv_Test(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                string data = e.Message.ToString();
                testfloat32Recv testfloat = new testfloat32Recv();

                TOPIC_NAME Topic = JsonConvert.DeserializeObject<TOPIC_NAME>(data);

                testfloat = JsonConvert.DeserializeObject<testfloat32Recv>(data);

                //Console.Out.WriteLine(string.Format("{0}=={1}", m_nWorkeridx, data));

                mainForm.onListmsg(string.Format("{0}=={1}", testfloat.topic, testfloat.msg.data));

                for (int i = 0; i < 50; i++)
                {
                    if (testfloat.topic.Equals(string.Format("com_test/pub{0}",i)))
                    {
                        Data.Instance.t[i] = string.Format("{0}", testfloat.msg.data);
                        recvcnt[i] += 1;

                        break;
                    }
                }

                string strcnt = "";
                for(int i=0; i<50; i++)
                {
                    strcnt += string.Format("{0} cnt ={1}..",i, recvcnt[i]);
                }
                
               // mainForm.updateDP(testfloat.topic, string.Format("{0}", testfloat.msg.data),strcnt);
                


                
                
                // IsTopicUpdate = true;

                //  DisplayRobotStatus();
            }
            catch (Exception ex)
            {
                Console.WriteLine("onSubscribe_Recv_Test err" + ex.Message.ToString());
            }
        }

       


        public async void onPublshTest(Form1 frm,float fValue,int idx)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    rosinterface ros = new rosinterface();

                    testfloat32 testfloat = new testfloat32();
                    testfloat.data = (float)(fValue + idx);
                    string strobj = JsonConvert.SerializeObject(testfloat);
                    JObject obj = JObject.Parse(strobj);


                    //ros.PublisherTopicMsgtype(robot + list.topic_cmdvel, list.msg_cmdvel);

                    ros.PublisherTopicMsgtype(string.Format("com_test/sub{0}", idx), "std_msgs/Float32");
                    Thread.Sleep(100);
                    ros.publisher(obj);

                    frm.onListPublish(string.Format("com_test/sub{0}", idx));

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onPublshTest err :={0}", ex.Message.ToString());
            }
        }

        public async void onServiceTest(Form1 frm)
        {
            try
            {
                if (Data.Instance.isConnected)
                {
                    ServiceClient _serviceClient = new ServiceClient("com_test/srv0", Data.Instance.md);

                    JArray argsList = JArray.Parse("[]");
                    var result = await _serviceClient.Call(argsList.ToObject<List<dynamic>>());

                    frm.onListService(result.ToString());

                 }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("onServiceTest err :={0}", ex.Message.ToString());
            }

        }


#endregion
    }
}
