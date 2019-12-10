using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosbridge.Client;



namespace SysSolution
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

        public Dictionary<string, WAS_RESULT> WorkResult = new Dictionary<string, WAS_RESULT>();
        public Dictionary<string, WAS_STATUS> WorkStatus = new Dictionary<string, WAS_STATUS>();
        public Dictionary<string, WAS_FEEDBACK> WorkFeedBack = new Dictionary<string, WAS_FEEDBACK>();

        public bool chkworking { get; set; } = false;
        public Dictionary<string, WorkRobotStatus> RealtimeWorkStatus = new Dictionary<string, WorkRobotStatus>();  //key = robot id, value = robot status
        #endregion

        public bool EmergencyMode { get; set; }     //비상리모콘을 위한 변수
        public string SelectedRobotName { get; set; }
        public string SelectedDefaultRobotName { get; set; }


        public string g_strRunmode = "";

        public bool g_bRunning = false;

#if _commtest
        public Form1 MAINFORM { get; set; }
#elif _demo
         public DemoForm0131 MAINFORM { get; set; }
#elif _map
        public MapDspForm MAINFORM { get; set; }
#elif _robotstatus
         public RobotStatusForm MAINFORM { get; set; }
#elif _workorder
        public WorkOrderForm MAINFORM { get; set; }
#elif _jobhistory
        public RobotJob_HistoryForm MAINFORM { get; set; }
#elif _sol
        public Frm.DashboardForm MAINFORM { get; set; }
#elif _urtest
        public UR_Sample.URControl_TestFrm MAINFORM { get; set; }

#elif _statusone
        public RobotStatusForm_One MAINFORM { get; set; }

#elif _fleet
        public FleetManager.FleetManager_MainForm MAINFORM { get; set; }
#endif



        //jo update 2019-3-7 ~~
        public Dictionary<string, Robot_WorkKInfo> Robot_work_info = new Dictionary<string, Robot_WorkKInfo>();  //key = robot id, value = Robot_WorkKInfo
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

        public enum ACTION_TYPE
        {
            Goal_Point=1,
            Basic_Move,
            Lift_Conveyor_Control,
            CHAIN,
            UR_MISSION,
            Stable_Pallet,
            Action_wait,
        }

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
        public int nPulishDelayTime = 50;
        public int nWorkDelayTime = 0;
        
        //end jo update

        public string GetFirstRobotName()
        {
            if (InformationRobot == null) return null;
            if (InformationRobot.msg.robolist.Count <1  ) return null;
            return InformationRobot.msg.robolist[0].RID;
        }

        public List<Subscriber> subs = new List<Subscriber>();


        public string[] t = new string[50];

    }
}
