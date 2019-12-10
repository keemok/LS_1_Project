using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosbridge.Client;

//add using
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Syscon_Solution
{


    public class Data : Singleton<Data>
    {
        public bool isDBConnected { get; set; } = false;
       
        
        #region ROS Interface 변수
        public MessageDispatcher md { get; set; }
        public Publisher publisher { get; set; }
        public Subscriber subscriber { get; set; }
        public ServiceClient srvclient { get; set; }
        public Socket socket { get; set; }

        public bool isConnected { get; set; } = false;

        public List<Subscriber> subs = new List<Subscriber>();

        #endregion

        #region Topic Data
        public RobotInformation InformationRobot { get; set; }
        public RobotInformation InformationServiceRobot { get; set; }
        public XISInformation InformationXis { get; set; }
        public ConnectivitiesInformation InformationConn { get; set; }
        public MotorInformation RobotMotorState = new MotorInformation();
        public UltrasonicRawInfo UltraSonicRaw = new UltrasonicRawInfo();
        public UltrasonicModInfo UltraSonicMod = new UltrasonicModInfo();
        public BMSInfo BmsState = new BMSInfo();
        public RobotLiftInfo RobotLiftState = new RobotLiftInfo();
        public RobotConvInfo RobotConvState = new RobotConvInfo();     
        #endregion

        #region Work order Data
        public bool chkWorkstart { get; set; } = false;

        public WorkList RobotWorkList = new WorkList();

        public WAS_FEEDBACK RobotWorkFeedback { get; set; }
        public WAS_RESULT RobotWorkResult { get; set; }
        public WAS_STATUS RobotWorkStatus { get; set; }

        // public Dictionary<string, WAS_RESULT> WorkResult = new Dictionary<string, WAS_RESULT>();
        // public Dictionary<string, WAS_STATUS> WorkStatus = new Dictionary<string, WAS_STATUS>();
        // public Dictionary<string, WAS_FEEDBACK> WorkFeedBack = new Dictionary<string, WAS_FEEDBACK>();

        //  public bool chkworking { get; set; } = false;
        //  public Dictionary<string, WorkRobotStatus> RealtimeWorkStatus = new Dictionary<string, WorkRobotStatus>();  //key = robot id, value = robot status
        #endregion

        #region jo update syscon_solution ver 0.1

        public bool bFormClose = false;

#if _fleetmain
        public Fleet_Main MAINFORM { get; set; }
#elif  _mapping
        public MappingManager.MappingMain MAINFORM { get; set; }
#endif
        #region DB 관련
        public MySqlConnection G_SqlCon = null;
        public MySqlConnection G_DynaSqlCon = null;

        #endregion


        
        public List<string> G_robotList = new List<string>();

        public ConnectedDevices_Info ConnectDevicesInfo = new ConnectedDevices_Info();

        /// <summary>
        /// db에 등록된 로봇 정보 리스트
        /// </summary>
        public Dictionary<string, Robot_RegInfo> Robot_RegInfo_list = new Dictionary<string, Robot_RegInfo>();
        public Dictionary<int, Robot_RegInfo> Robot_RegInfo_list_ip = new Dictionary<int, Robot_RegInfo>();


        public Dictionary<string, bool> Robot_Enable_list = new Dictionary<string, bool>();
        public Dictionary<string, bool> Robot_Disable_list = new Dictionary<string, bool>();


        public MissionList_Table missionlisttable = new MissionList_Table();
        
        /// <summary>
        /// db에 등록된 작업 정보 리스트
        /// </summary>
        public Dictionary<string, Task_Info> Task_list = new Dictionary<string, Task_Info>();


        public Dictionary<string, WorkFlowGoal> nodemissionDic = new Dictionary<string, WorkFlowGoal>();
        public Dictionary<string, LS_Mission> LS_Mission_list = new Dictionary<string, LS_Mission>();



        /// <summary>
        /// 로봇 그룹정보 리스트
        /// </summary>
        public Robot_Group_List robotgroup_list = new Robot_Group_List();
        
        public Xis_Info_List xisInfo_list = new Xis_Info_List();

        public Trigger_Info_List triggerInfo_list = new Trigger_Info_List();


        public List<TaskCheck_class> TaskCheck_threadList = new List<TaskCheck_class>();


        public Dictionary<string, Robot_WorkKInfo> Robot_work_info = new Dictionary<string, Robot_WorkKInfo>();  //key = robot id, value = Robot_WorkKInfo


        public Dictionary<string, int> Session_2_path = new Dictionary<string, int>();

        public string robot_id;

        List<Rectangle> nextNode = new List<Rectangle>();
        
        public enum ACTION_TYPE
        {
            Goal_Point = 1,
            Basic_Move,
            Lift_Conveyor_Control,
            CHAIN,
            UR_MISSION,
            Stable_Pallet,
            Action_wait,
            Docking,
        }
        #endregion 


        public Dictionary<string, MissionList> mission_list = new Dictionary<string, MissionList>();


        //jo update 2019-3-7 ~~
        public Robot_LiveInfo robot_liveinfo = new Robot_LiveInfo();
        public Dictionary<string, XISState> XIS_Status_Info = new Dictionary<string, XISState>();
        //실시간으로 로봇정보를 파일에 저장하고 화면 갱신을 위해.. robot_id,ip,work id,work status => Robot.txt
        public Dictionary<string, string> Robot_status_info = new Dictionary<string, string>();  //key = robot id, value = 로봇파일에 저장할 내용
        public TotalJobSchedule totalJobschedule = new TotalJobSchedule();
        public WaitingPos_Table waitingpos_table = new WaitingPos_Table();
        public JobPos_Table jobpos_table = new JobPos_Table();

        /// <summary>
        /// 현재 움직이고 있는 모든 로봇들의 상태를 기록한다.
        /// </summary>
        public Dictionary<string, Robot_Going_Status> robots_currgoing_status = new Dictionary<string, Robot_Going_Status>();

        public Dictionary<string, WaitingPos_RobotInfo> waitingpos_RobotsInfo = new Dictionary<string, WaitingPos_RobotInfo>();
        public Dictionary<string, JobPos_RobotInfo> jobos_robotsInfo = new Dictionary<string, JobPos_RobotInfo>();
        public MissionList_Table missionlist_table = new MissionList_Table();


        public Dictionary<string, Node_mission> node_mission_list = new Dictionary<string, Node_mission>();
        public Dictionary<string, Docking_mission> docking_mission_list = new Dictionary<string, Docking_mission>();
        public Dictionary<string, string> docking_warehouse = new Dictionary<string, string>();
        public Dictionary<string, string> node_area = new Dictionary<string, string>();
        public Dictionary<string, RectangleF> location_check_node = new Dictionary<string, RectangleF>();

        public Dictionary<string, current_mission> current_mission = new Dictionary<string, current_mission>();

        public Dictionary<string, bool> Enable_Robot = new Dictionary<string, bool>();
        public Dictionary<string, bool> Disable_Robot = new Dictionary<string, bool>();

        public Dictionary<string, bool> onRobot = new Dictionary<string, bool>();
        public Dictionary<string, bool> offRobot = new Dictionary<string, bool>();
        public Dictionary<string, bool> robotFlag = new Dictionary<string, bool>();

        public Dictionary<string, warehouse_inout> warehouse = new Dictionary<string, warehouse_inout>();
        public Dictionary<string, warehouse_inout> callByplc = new Dictionary<string, warehouse_inout>();
        public Dictionary<string, string> calllist_ip = new Dictionary<string, string>();

        public bool EmergencyMode { get; set; }     //비상리모콘을 위한 변수
        public string SelectedRobotName { get; set; }
        public string SelectedDefaultRobotName { get; set; }

        public string g_strRunmode = "";
        public bool g_bRunning = false;

        public int nFormidx = 0; //현재 오픈된 폼인덱스 

        public bool bWaitPos_Run = false; //대기장소 이동중인지 
        public bool bRunToWaitPos = false;
        public int nWaitPos_Robot_idx = 0; //대기장소로 이동하는 로봇들의 현재 인덱스

        public bool bCrashcheckStop = false;
        public bool bCrashcheckPause = false;
        public bool bMissionCompleteCheck = false;

        /// <summary>
        /// Task가 운영중이지 확인 플래그
        /// </summary>
        public bool bTaskRun = false;
        public string strTaskRun_StartTime = "";

        #region 성능평가 항목을 위한 변수들

        public bool bRobotPosRec = false;

        #endregion

        public enum FORM_IDX
        {
            Edit_MissionForm = 1,
            Edit_TaskForm,
            Edit_MapForm,
            Operaion_TaskForm,
            Monitoring_Map,
            Monitoring_Robot,
        };
        public enum FORM_IDX_LS
        {
            mainFORM= 1,
            mapmonitoringFORM,
            robotmonitoringFORM,
            missioneditFORM,
            taskmodifyFORM,
            mapeditFORM,
            logFORM,
            settingFORM,
            taskFORM,
        };
        public bool robot0Status = false;
        public bool robot1Status = false;
        public bool robot2Status = false;
        public bool robot3Status = false;
        public bool robot4Status = false;
        public bool robot5Status = false;
        public bool robot6Status = false;

        public bool[] robotStatus = { false, false, false, false, false, false };
        


        public string[] ActionList = { "Goal-Point", "Basic-Move", "Lift-Control", "Conveyor-Control", "PLC", "Way-Point" };
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

        public int nSubscribeDelayTime = 60;
        public int nPulishDelayTime = 100;
        public int nWorkDelayTime = 0;
        
        //end jo update

        public string GetFirstRobotName()
        {
            if (InformationRobot == null) return null;
            if (InformationRobot.msg.robolist.Count <1  ) return null;
            return InformationRobot.msg.robolist[0].RID;
        }

        public string[] t = new string[50];
        public string globalRobotstring = "";


    }
}
