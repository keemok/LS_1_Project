using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


using System.Drawing;
using SnmpSharpNet;

namespace Syscon_Solution
{
    #region BasicType






    public enum Topics
    {
        robolist = 0, xislist,
    };
    public enum RosOperation
    {
        publish = 0, advertise, subscribe, unsubscribe, call_service, service_response
    }
    public enum LiftStatus
    {
        Stop = 0, Up, Top, Down, Bottom
    }
    public enum ConveyStatus
    {
        Stop = 0, Forward = 1, Backward = -1
    }
    public enum LoadingStatus
    {
        Empty = 0, Loaded, Abnormal
    }
    public enum RobotWorkStatus
    {
        Idle = 0, Driving, Loading, Parking, Returning, Waiting, Charging
    }

    public class Header
    {
        uint seq;
        SysconTime stamp = new SysconTime();
        string frame_id;
    }
    public class APsnmp_client
    {
        public string connectAP;
        public int TXrate;
        public OctetString noiseLevel;
    }
    public class MultiArrayDimension
    {
        public string label;
        public int size;
        public int stride;
    };
    public class MultiArrayLayout
    {
        public List<MultiArrayDimension> dim;
        public uint data_offset;
    }
    public class Int32MultiArray
    {
        public MultiArrayLayout layout;
        public List<int> data = new List<int>();

        public override string ToString()
        {
            var strData = string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Count; i++)
                builder.AppendFormat("{0}:{1},", i, data[i]);

            return builder.ToString();
        }
    }
    public class Float32MultiArray
    {
        public MultiArrayLayout layout = new MultiArrayLayout();
        public List<float> data = new List<float>();

        public override string ToString()
        {
            var strData = string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Count; i++)
                builder.AppendFormat("{0}:{1:0.000},", i, data[i]);

            return builder.ToString();
        }
    }
    public class ControllerState
    {
        public string topic;
        public Float32MultiArray msg = new Float32MultiArray();
        public RosOperation op = new RosOperation();
    }
    public class Pose2D
    {
        public double x;
        public double y;
        public double theta;

        public override string ToString()
        {
            return string.Format("(x:{0:0.000},y:{1:0.000},theta:{2:0.000})", x, y, theta);
        }
    }
    public class Vector3H
    {
        public double x;
        public double y;
        public double z;
        public static Vector3H zero
        {
            get { return new Vector3H() { x = 0.0, y = 0.0, z = 0.0 }; }
        }
    }
    public class SysconTime
    {
        public long secs;
        public long nsecs;
        public static SysconTime zero;
    }
    public class Vector4H
    {
        public double x;
        public double y;
        public double z;
        public double w;
        public static Vector4H zero
        {
            get { return new Vector4H() { x = 0.0, y = 0.0, z = 0.0, w = 1.0 }; }
        }
    }
    public class Twist
    {
        public Vector3H linear = new Vector3H();
        public Vector3H angular = new Vector3H();
        public static Twist zero
        {
            get { return new Twist() { linear = Vector3H.zero, angular = Vector3H.zero }; }
        }
        public override string ToString()
        {
            return string.Format("linear:({0:0.000},{1:0.000},{2:0.000}) angle:({3:0.000},{4:0.000},{5:0.000})",
                linear.x,
                linear.y,
                linear.z,
                angular.x,
                angular.y,
                angular.z
                );
        }
    }

    public class testfloat32
    {
        public float data;

        public override string ToString()
        {
            return string.Format("(data:{0:0.000})", data);
        }
    }

    public class testfloat32Recv
    {
        public string topic;
        public testfloat32 msg;
        //public RosOperation op;
    }
    public class warehouse_inout
    {
        public string in_;
        public string out_;
    }
    public class Pose
    {
        public Vector3H position;
        public Vector4H orientation;

        public static Pose zero
        {
            get { return new Pose() { position = Vector3H.zero, orientation = Vector4H.zero }; }
        }
        public override string ToString()
        {
            return string.Format("l:({0:0.000},{1:0.000},{2:0.000}) a:({3:0.000},{4:0.000},{5:0.000},{6:0.000})",
                position.x,
                position.y,
                position.z,
                orientation.x,
                orientation.y,
                orientation.z,
                orientation.w
                );
        }
    }
    public class Module
    {
        public MotorState motor;
        public Float32MultiArray bms;
        public Int32MultiArray bumper;
    }
    public class Modules
    {
        public List<LiftState> lift_modules;
        public List<ConvState> conv_modules;
    }
    #endregion




    #region Robot type

    public class TOPIC_NAME
    {
        public string topic;
    }



    public class MotorInformation
    {
        public string topic;
        public MotorState msg;
        public RosOperation op;
    }
    public class MotorState
    {
        public Float32MultiArray rpm;
        public Float32MultiArray cmd_rpm;
        public Twist feed_vel;
        public Twist cmd_vel;
    }

    public class PlatformState
    {
        public MotorState motor;
        public Float32MultiArray ultra_sonic;
        public Float32MultiArray BMS;
        public LidarScan scan;
        public Int32MultiArray bumper;
    }
    public class LiftState
    {
        public int mid;  // id
        public int moving_status;  //  0:STOP  1:UP  2:TOP  3:DOWN 4:BOTTOM 
        public int loading_status; //  0:EMPTY 1:Loaded 2:Abnormal
        public float height; //  height

        public override string ToString()
        {
            LiftStatus liftstatus = (LiftStatus)moving_status;
            LoadingStatus loadstatus = (LoadingStatus)loading_status;
            return string.Format("id:{0} MoveState:{1} LoadState:{2} Height:{3}", mid, liftstatus, loadstatus, height);
        }
    }

    public class ConvState
    {
        public int mid;
        public int moving_status; //   0:STOP  1:FORWARD  -1:BACKWARD
        public int loading_status; //  0:EMPTY 1:Loaded 2:Abnormal

        public override string ToString()
        {
            LiftStatus liftstatus = (LiftStatus)moving_status;
            LoadingStatus loadstatus = (LoadingStatus)loading_status;
            return string.Format("id:{0} MoveState:{1} LoadState:{2}", mid, liftstatus, loadstatus);
        }
    }

    public class UltrasonicModInfo
    {
        public string topic;
        public Float32MultiArray msg = new Float32MultiArray();
        public RosOperation op = new RosOperation();
    }


    public class RobotConvInfo
    {
        public string topic;
        public Int32MultiArray msg = new Int32MultiArray();
        public RosOperation op = new RosOperation();
    }

    public class CommmonFormat
    {
        public string topic;
        public string uid;
        public string id;
        public RosOperation op;
    }

    public class WorkGoal
    {
        public string topic;
        public WAS_GOAL msg;
        public RosOperation op;
    }

    public class WorkFeedback
    {
        public string topic;
        public WAS_FEEDBACK msg;
        public RosOperation op;
    }

    public class WorkResult
    {
        public string topic;
        public WAS_RESULT msg;
        public RosOperation op;
    }

    public class WorkPauseArg
    {
        public string data;
    }
    public class WorkResumeArg
    {
        public string data;
    }

    public class WorkStatus
    {
        public string topic;
        public WAS_STATUS msg;
        public RosOperation op;
    }

    public class RobotState_1
    {
        public string topic;
        public RobotState_msg msg;
    }
    public class RobotState_msg
    {
        public string RID;
        public Pose2D pose;
        public string type;
        public int lift_status;
        public int workstate;

    }

    public class LidarScan
    {
        public string topic;
        public LidarScan_msg msg;
    }

    public class LidarScan_msg
    {
        public Header header;
        public float angle_min;
        public float angle_max;
        public float angle_increment;
        public float time_increment;
        public float scan_time;
        public float range_min;
        public float range_max;
        public List<float> ranges;
        public List<float> intensities;
    }

    public class UltrasonicRawInfo
    {
        public string topic;
        public Float32MultiArray msg = new Float32MultiArray();
        public RosOperation op = new RosOperation();
    }

    public class BMSInfo
    {
        public string topic;
        public Float32MultiArray msg = new Float32MultiArray();
        public RosOperation op = new RosOperation();
    }

    public class RobotLiftInfo
    {
        public string topic;
        public Int32MultiArray msg = new Int32MultiArray();
        public RosOperation op = new RosOperation();
    }

    public class Robot_Status_info //로봇에 동작 및 상태에 대한 정보 
    {
        public RobotState_1 robotstate;
        public MotorInformation motorstate;
        public LidarScan lidar;
        public UltrasonicRawInfo ultrasonic;
        public ControllerState controllerstate;
        public WorkFeedback workfeedback;
        public WorkResult workresult;
        public TaskFeedback taskfeedback;
        public TaskResult taskresult;
        public BMSInfo bmsinfo;
        public MapInformation mapinfo;
        public MapInformation globalcostmap;
        public MapInformation localcostmap;
        public MapPlanInformation globalplan;
        public UR_StatusInformation ur_status;
        public Except_StatusInformation except_status;
        public Work_requestInformation work_request;
        public RobotLiftInfo lift_info;
        public MarkerDetection_Information markers;
        public CamInformation cam1;
        public CamInformation cam2;
        public LookAheadInformation lookahead;
        public GoalRunnig_StatusInformation goalrunnigstatus;
        public RobotCurrAngluar_Infomation currAngluar;
        public RobotState robotstate_;
    }

    public class Robot_Work_Data
    {
        public string strTopic;
        public string strWorkData;
        public string strActionType;
        public string strActionIdx;
    }



    public class Robot_Task_Info
    {
        public string strTaskID;
        public List<MisssionInfo> robotmisssion_info;
    }

    /// <summary> 해당 로봇이 처리하기 위한 미션 정보의 모음 . </summary>
    public class Robot_Mission_Info
    {
        public string strGroupID;
        public string strMissionName;
        public string strMissionID;
        public string strTriggerflag;
        public bool bMission_ongoing;
        public int nCnt;
        public int nGoingTime;
        public int nMissionLevel;

    }

    public class MissionChange
    {
        public string data;
    }

    public class TaskResult
    {
        public string topic;
        public WAS_RESULT msg;
        public RosOperation op;
    }

    public class RESULT
    {
        public float elapsed_time;
    }

    public class WAS_RESULT
    {
        public Header header = new Header();
        public GoalStatus status = new GoalStatus();
        public RESULT result = new RESULT();
    }
    public class TARU_RESULT
    {
        public Header header = new Header();
        public GoalStatus status = new GoalStatus();
        public TaskFlowFeedback result = new TaskFlowFeedback();
    }
    public class mouseClick_point
    {
        public float x;
        public float y;
        public float z = 0;
    }
    public class Polygon
    {
        public List<mouseClick_point> point = new List<mouseClick_point>();
    }
    public class CautionArea
    {
        public string area_id;
        public string area_description;
        public Polygon polygon;
    }
    public class Area
    {
        public List<CautionArea> cautionarea = new List<CautionArea>();
    }
    public class TaskFeedback
    {
        public string topic;
        public TASK_FEEDBACK msg;
        public RosOperation op;
    }

    public class TaskFlowFeedback
    {
        public string task_id;
        public string work_id;
        public int action_indx;
        public int mission_indx;
        public bool act_complete;
        public bool mission_complete;
        public bool task_complete;
        public int loop_flag;
        public int loop_count;
        public bool is_paused;
        public float elapsed_time;
        public float estm_time;
    }

    public class TASK_FEEDBACK
    {
        public Header header = new Header();
        public GoalStatus status = new GoalStatus();
        public TaskFlowFeedback feedback = new TaskFlowFeedback();
    }

    /// <summary> task 스레드에서 현재 task의 정보 체크 테이블. </summary>
    public class Task_checkThread_TableInfo
    {
        public List<Task_checkThread_Info> taskcheck_info;
    }
    public class Task_checkThread_Info
    {
        public string strrobotid;
        public TaskFlowFeedback taskfeedback;
    }

    /// <summary> task order 토픽 형식 . </summary>
    public class Task_Order
    {
        public string task_id;
        public int start_idx;
        public int loop_flag;
        public string missionlist;
        public string robotlist;
    }

    public class Robot_RegInfo
    {
        public string robot_id;
        public string robot_name;
        public string robot_ip;
        public string robot_group;
        public string robot_version;
        public string map_id;
        public string map_name;
    }


    public class ConnectedDevices_Info
    {
        public string topic;
        public ConnectedDevices msg;
        public RosOperation op;
    }
    public class ConnectedDevices
    {
        public List<Devices> devices;
    }
    public class Devices
    {
        public int type;
        public string id;
        public string address;
    }

    public class TempMove
    {
        public float direction;
        public float distance;
    }

    public class SPCore
    {
        public string data;
    }

    public class MAP_Save 
    {
        public string data; //data=> RID,map_id
    }


    /// <summary> 로봇에 대한 모든 정보 기록...(작업내용, 상태 등등). </summary>
    public class Robot_WorkKInfo 
    {
        /// <summary> 로봇id </summary>
        public string strRobotID;
        /// <summary> 현재 작업코드 </summary>
        public string strWorkID;
        /// <summary> 현재 작업이름 </summary>
        public string strWorkName;
        /// <summary> 반복횟수 </summary>
        public int nWork_cnt;
        public string strLoop_Flag;
        /// <summary> 현재횟수 </summary>.........
        public int nCurrWork_cnt;

        /// <summary> 로봇 우선순위 level </summary>
        public int nPriorityLevel;

        /// <summary> Goal ID </summary>
        public string strGoalid;
     
        /// <summary> 현재 물건을 들고 있는지 </summary>.........
        public bool bGoodsLoad;
        /// <summary> 물건 선적 단계.. 0: 대기 1: 선적, 2: 하적.. 선적과 하적을 하면 작업량이 1증가. </summary>.........
        public int nGoodsLoadStep;


        public string strWorkTime;
        /// <summary> 로봇이 살아있는지 여부 </summary>
        public bool m_bLive;
        /// <summary> 현재 토픽 및 작업내용  </summary>
        /// 
        public int nActionidx;
        public int nTotalActionidx;
        public List<Robot_Work_Data> robot_workdata;
 
        /// <summary> 현재 작업이 뭔지?? 대기위치이동 인지, 작업1인지.. 쉬는중인지 </summary>
        public string strWorkKind;
        /// <summary> 현재 로봇의 위치 대기위치인지, 작업1위치인지.. 다른위치인지 </summary>
        public string strRobotWorkPosition;

        public Image costmap;
        public float costmap_originX;
        public float costmap_originY;

        public Image globalcostmap;
        public float globalcostmap_originX;
        public float globalcostmap_originY;

        public bool task_finished;
        public Robot_Task_Info robottask_info;

        public bool mission_complete;
        
        /// <summary> subscribe으로 들어오는 로봇 정보 </summary>
        public Robot_Status_info robot_status_info;

        
    }

  



    /// <summary> 로봇의 현재 진행 정보기록 </summary>
    public class Robot_Going_Status
    {
        public string strRobotName;
        public string strRobotID;
        public string strMisssionName;
        public string strMisssionID;
        public string strStatus;
        public int nRepeteCnt;
       
    }

    public enum ROBOT_JOB_TPYE
    {
        waitingrunnig_job=1,
        Jobrunnig_job,
    }

    /// <summary> 로봇들의 현재 진행 정보기록 모음 </summary>
    public class Robots_CurrentGoing_Status
    {
        public List<Robot_Going_Status> robot_going_status;
    }

    
    public enum JOB_TYPE
    {
        sequence=1,
        synchronous,
    }

    /// <summary> 수행할 작업 정보(여러 미션(work)가 합쳐져있다. </summary>
    public class JobInfo
    {
        public string strJobname;  //수행 작업이름
        public int nJobType;  //작업 종류.. 순차 작업 or 동시 작업
    }

    /// <summary> 수행할 작업 모음.. 스케줄. </summary>
    public class TotalJobSchedule
    {
        public List<JobInfo> jobInfo;
    }

    /// <summary> 대기 장소로 이동할 로봇 정보 및 미션 정보 </summary>
    public class WaitingPos_RobotInfo
    {
        public string strRobotName;
        public string strRobotID;
        public string strMisssionName;
        public string strMisssionID;
        public int nCnt;
        public int nTime_min;
    }

    /// <summary> 대기 장소로 이동할 로봇들 모음.. 해당 정보는 능동적으로 수정 가능 </summary>
    public class WaitingPos_Table
    {
        public List<WaitingPos_RobotInfo> waitpos_robotinfo;
    }


    /// <summary> 미션(work)를 수행할 로봇 정보 및 미션 정보 </summary>
    public class JobPos_RobotInfo
    {
        public string strRobotName;
        public string strRobotID;
        public string strMisssionName;
        public string strMisssionID;
        public int nCnt;
        public int nTime_min;
        public int nStartidx;
    }

    public class Node_mission
    {
        public string mission_name;
        public string mission_id;
        public string work;
    }
    public class Docking_mission
    {
        public string taskid;
        public string taskname;
        public string missionlist;
        public string robotlist;
    }
    public class current_mission
    {
        public string atcNO;
        public string startID;
        public string requiredID;
    }

    /// <summary> 동일 미션(work)를 수행할 로봇들 모음. </summary>
    public class JobPos_Table
    {
        public List<JobPos_RobotInfo> jobpos_robotinfo;
    }
    public class Robot_mission
    {
        public string missionname;
        public string missionid;
    }
    /// <summary> 미션(work) 정보.. 이름, id, 레벨. </summary>
    public class MisssionInfo
    {
        public string strMisssionName;
        public string strMisssionID;
        public string strTrigger_flag;
        public int nMisssionLevel;
        public int nStartidx;
        public int nMissionLoopflag;
        public string work;
    }

    public class ATC_
    {
        public PointF pointf;
        public string atc_name;
    }
    public class MissionList
    {
        public string missionID; // 실제 db 미션 ID
        public string mission;
    }


    /// <summary> 미션(work) 정보 모음 </summary>
    public class MissionList_Table
    {
        public List<MisssionInfo> missioninfo;
    }

    public class Task_Info
    {
        public string task_id;
        public string task_name;
        public string mission_id_list;//=new List<string>();
        public string robot_id_list;// = new List<string>();
        public string taskloopflag;
        public string start_idx;
        public string task_status;
        public string robot_group_id;
    }

    public class LS_Mission
    {
        public string mission_id;
        public string mission_name;
        public string trigger_flag;
        public List<WorkFlowGoal> work;
    }

    public class DB_MissionData
    {
        public List<Action> work = new List<Action>();
    }
    public class Reservation_work
    {
        public List<Reservation_work_list> Reservation_list;
    }
    public class Reservation_work_list
    {
        public string requiredID;
        public string startID;
        public string mission;
        public string atcnumber;
    }
    public class Robot_Group_List
    {
        public List<Robot_Group> robotgroup;
    }
    public class Robot_Group
    {
        public string robot_group_id;
        public string robot_group_name;
    }

    public class Xis_Info_List
    {
        public List<Xis_Info> xisinfo;
    }
    public class Xis_Info
    {
        public string xis_id;
        public string address;
        public string xis_name;
        public string xis_version;
        public string xis_group_id;
        public string trigger_id_list;
    }

    public class Trigger_Info_List
    {
        public List<Trigger_Info> triggerinfo;
    }
    public class Trigger_Info
    {
        public string trigger_id;
        public string trigger_name;
    }


    public class Action_Data
    {
        public string strWorkData;
        public string strActionType;
    }

    public class Action_Data_List
    {
        public List<Action_Data> action_data;
    }



    public class Robot_LiveInfo
    {
        public RobotInformation robotinfo;
    }
    
    
    

    public class Map_list
    {
        public string map_id;
        public string map_name;
    }

    public class Robot_Status
    {
        public string robot_id;
        public string work_id;
        public string work_status;
        public string work_cnt;
        public string action_idx;
    }



    public class JobSchedule
    {
        public string job_id;
        public string job_name;
        public List<string> mission_id;//=new List<string>();
        public List<string> unloadmission_id;//=new List<string>();
        public List<string> waitmission_id;//=new List<string>();
        public List<string> robot_id;// = new List<string>();
        public string call_type;
        public string job_status;
        public string job_group;
    }

    public class JobScheduleTable
    {
        public List<Job_MissionInfo> beginmission;
        public List<Job_MissionInfo> endmission;
        public List<Job_MissionInfo> waitmission;

        public List<string> robotid;
        public List<bool> bRobotuse;
    }

    public class Job_MissionInfo
    {
        public string missionid;
        public List<Job_Mission_ActInfo> actinfo;
    }

    public class Job_Mission_ActInfo
    {
        public int actidx;
        public float xpos;
        public float ypos;
        public string acttype;

        public string act_robotname;
        public string act_robotid;
        public string robot_status;
        public bool bXis_use;
    }





    public class RobotState
    {
        //public string RID;
        //public Pose2D pose;
        //public int workstate;
        //public PlatformState platform;
        //public Modules modules;
        public string topic;
        public string RID;
        public string type;
        public Pose2D pose;
        public int workstate;
    }
    public enum RobotOperator
    {
        GO = 0, BACK, RIGHT, LEFT, TURN_RIGHT, TURN_LEFT, STOP
    }
    
    public class RoboStateList
    {
        public List<RobotState> robolist;
    }
    public class RobotInformation
    {
        public string topic;
        public RoboStateList msg;
        public RosOperation op;
    }

    //20190314 add
    /// <summary>
    /// 로봇의 비상관련 정보를 저장
    /// </summary>
    public class Except_StatusInformation
    {
        public string topic;
        public Except_Status msg;
        public RosOperation op;
    }

    public class Except_Status
    {
        public short data;
    }
    /// <summary>
    /// ROS서버에서 미션을 재요청할때 사용
    /// </summary>
    public class Work_requestInformation
    {
        public string topic;
        public Work_request msg;
        public RosOperation op;
    }

    public class Work_request
    {
        public string wid;
        public int start_idx;
    }

    /// <summary>
    /// 마커 인식 정보
    /// </summary>
    public class MarkerDetection_Information
    {
        public string topic;
        public MarkerDetection msg;
        public RosOperation op;
    }

    public class MarkerDetection
    {
        public Header header;
        public List<AlvarMarkers> markers;
    }
    public class AlvarMarkers
    {
        public Header header;
        public int id;
        public int confidence;
        public PoseStamped pose;
    }


    public class CamInformation
    {
        public string topic;
        public CompressedImage msg;
        public RosOperation op;

    }

    public class CompressedImage
    {
        public Header header;
        /*public string format;
        //public List<int> data;
        public string data;
        */

        public int height;
        public int width;
        public string encoding;
        public short is_bigendian;
        public int step;
        // public List<int> data;
        public string data;
    }

    public class LiftStatus_Set
    {
        public LiftStatus_Set_Arg msg;
    }

    public class LiftStatus_Set_Arg
    {
        public int data;
    }

    public class LookAheadInformation
    {
        public string topic;
        public PointStamped msg;
        public RosOperation op;

    }
    public class PointStamped
    {
        public Header header;
        public Point1 point;
        
    }
    public class Point1
    {
        public double x;
        public double y;
        public double z;
    }

    /// <summary>
    /// 로봇 글로벌 플랜과 비교하여 현재 앵글값
    /// </summary>
    public class RobotCurrAngluar_Infomation
    {
        public string topic;
        public RobotCurrAngluar msg;
        public RosOperation op;
    }
    public class RobotCurrAngluar
    {
        public float data;
    }

    /// <summary>
    /// 로봇 자율주행 상태 정보
    /// </summary>
    public class GoalRunnig_StatusInformation
    {
        public string topic;
        public GoalStatusArray msg;
        public RosOperation op;
    }
    public class GoalStatusArray
    {
        public Header header;
        public List<GoalStatus> status_list;
    }

    #endregion

    #region UR type  20190313

    public class UR_StatusInformation
    {
        public string topic;
        public UR_Status msg;
        public RosOperation op;
    }

    public class UR_Status
    {
        public string status;
        public JointState arm_status;
    }

    public class JointState
    {
        public Header header = new Header();
        public List<string> name;
        public List<float> position;
        public List<float> velocity;
        public List<float> effort;
    }

    #endregion

    #region Map

    public class MapInfo
    {
        public SysconTime map_load_time;
        public double resolution;
        public int width;
        public int height;
        public Pose origin;
    }

    public class MapState
    {
        public Header header;
        public MapInfo info;
        public List<short> data;
    }
    public class MapInformation
    {
        public string topic;
        public MapState msg;
        public RosOperation op;
    }

    public class PoseStamped
    {
        public Header header;
        public Pose pose;
    }
    public class MapPlan
    {
        public Header header;
        public List<PoseStamped> poses;

    }

    public class MapPlanInformation
    {
        public string topic;
        public MapPlan msg;
        public RosOperation op;

    }

    #endregion

   





    #region XIS Data

    public class XISState
    {
        public string XID;
        public string adress;
        public bool connectivity;
        public Int32MultiArray status;
    }
    public class XISStateList
    {
        public List<XISState> xislist;
    }
    public class XISInformation
    {
        public string topic;
        public XISStateList msg;
        public RosOperation op;
    }

   

    #endregion


    #region Connectivities

    public class Connectivities
    {
        public string node_name;
        public bool connectivity;
    }
    public class ConnectivitiesList
    {
        public List<Connectivities> connectivities;
    }
    public class ConnectivitiesInformation
    {
        public string id;
        public ConnectivitiesList values;
        public RosOperation op;
        public bool result;
        public string service;

    }

    //public class ConnectivitiesList
    //{
    //    public string id;
    //    public ConnectivitiesList values;
    //    public RosOperation op;
    //    public bool result;
    //    public string service;

    //}

    #endregion



   

    #region Work Order
    public class SetPoint
    {
        public float x;
        public float y;
        public float theta;
        public List<ParameterSet> param = new List<ParameterSet>();
    }

    public class SetMove
    {
        public int mode;
        public float target;
    }

    public class SetRoboact
    {
        public int command;
        public float act;
    }

    public class SetPLC
    {
        public Int32MultiArray reg;
    }

    public class SetWorkState
    {
        public RobotWorkStatus state = new RobotWorkStatus();
    }

    public class WorkList
    {
        public int selectWork;
        public int selectAct;
        public List<RobotWorks> work = new List<RobotWorks>();
    }

    public class RobotWorks
    {
        public string WID;
        public List<RobotActions> act = new List<RobotActions>();
    }

    public class RobotActions
    {
        public Action_mode act_mode;
        public WayPoint way_point = new WayPoint();
        public BasicMove basic_move = new BasicMove();
        public RobotAct robot_act = new RobotAct();
        public ControlPLC control_plc = new ControlPLC();    
    }
    public enum Action_mode
    {
        WAY_POINT = 0, BASIC_MOVE, ROBOT_ACT, CONTROL_PLC
    }    

    public class WayPoint
    {
        public float x { set; get; }
        public float y { set; get; }
        public float theta { set; get; }
        public string qual { set; get; }

        [JsonIgnore]
        public List<ParameterSet> Params = new List<ParameterSet>();
    }

    public class BasicMove
    {
        public int mode { set; get; }
        public float target { set; get; }
    }

    public class RobotAct
    {
        public RobotAct_Mode command;
        public float act { set; get; }
    }

    public class ControlPLC
    {
        public int mode; //0 : send, 1 : check
        public Int32MultiArray reg = new Int32MultiArray();
    }

    public enum RobotAct_Mode
    {
        LIFT_TOP_BUTTOM = 0, LIFT_SET_HEIGHT, CONVEYOR_TRASFER, CONVEYOR_RECEIVE
    }

	public class GoalID
	{
		public SysconTime stamp = new SysconTime();
		public string id = "";
	}

    public class WorkFlowGoal
    {
        public string work_id;
        public int action_start_idx;
        public int loop_flag; //20190510 add
        
        public List<Action> work = new List<Action>();
    }

	public class Action
	{
		public int action_type;
   	    public List<float> action_args = new List<float>();
		public List<ParameterSet>action_params = new List<ParameterSet>();
	}

	public class WAS_GOAL
	{
		public Header header = new Header();
		public GoalID goal_id = new GoalID();
        
        public WorkFlowGoal goal = new WorkFlowGoal();
    }

	public class WAS_CANCEL
	{
		public GoalID goal_id = new GoalID();
	}

    public class GoalStatus
    {
        public GoalID goal_id = new GoalID();
        public int status;
        public string text;
    }

    public class FEED_BACK
	{
		public int action_indx;
        public int mission_indx;
        public bool act_complete;//20190510 add
        public bool mission_complete;
        public bool task_complete;
        public int loop_flag;
        public int loop_count;
        public bool is_paused;
		public float elapsed_time;
	}

	public class WAS_FEEDBACK
	{
		public Header header = new Header();
		public GoalStatus status = new GoalStatus();
		public FEED_BACK feedback = new FEED_BACK();
	}    

    

	public class WAS_STATUS
	{
        public Header header = new Header();
        public List<GoalStatus> status_list = new List<GoalStatus>();
        public GoalID goal_id = new GoalID();
	}

   

    public class RealtimeWorkStatus
    {
        public int workidx;
        public bool chkCancel;
        public int workcnt;

    }

    //public class WorkRobotStatus
    //{
    //    public bool chkCancel;
    //    public int targetnum;
    //    public int workcnt;        
    //    public List<string[]> robotData = new List<string[]>(); //[0] : Robot ID, [1] 진행중인 Action Index, [2] 작업 진행 횟수 
    //}

    public class WorkRobotStatus
    {
        public string WID;
        public int workStatus;  // -1 = idle, 0 = Cancel, 1 = Working, 2 = Pause
        public int targetnum;
        public int workcnt;
        public int actidx;
    }

    #endregion

    #region Service Response
    public class Response
    {
        public bool success;
        public string message;
    }
    #endregion

    #region Parameter Setting

    public class ParameterSet
    {
        public string param_name;
		public string type;
        public string value;
    }
    #endregion
}
