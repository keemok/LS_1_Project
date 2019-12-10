using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syscon_Solution
{
    public class TopicList
    {

        // jo update
        public string topic_robot_state = "/robot_state";  //로봇 위치 , 상태 및 리프트 상태topic
        public string msg_robotstate = "syscon_msgs/RobotState";
        public string topic_motorState = "/motor_status";   //모터 상태 topic
        public string msg_motorState = "syscon_msgs/MotorState";
        public string topic_ultrasonic_raw = "/ultra_sonic_raw"; //초음파 데이타 topic
        public string msg_ultrasonic_raw = "std_msgs/Float32MultiArray";
        public string topic_lidarscan = "/scan";    //Lidar 데이타 topic
        public string msg_lidarscan = "sensor_msgs/LaserScan";

        public string topic_bms = "/bms";
        public string msg_bms = "std_msgs/Float32MultiArray";

        public string topic_staticMap = "/map";
        public string msg_staticMap = "nav_msgs/OccupancyGrid";

        public string topic_globalCost = "/move_base/global_costmap/costmap";
        public string msg_globalCost = "nav_msgs/OccupancyGrid";

        public string topic_localCost = "/move_base/local_costmap/costmap";
        public string msg_localCost = "nav_msgs/OccupancyGrid";

        public string topic_globalPath = "/move_base/GlobalPlanner/plan";
        public string msg_globalPath = "nav_msgs/Path";

        public string topic_workpause = "/WAS/pause";
        public string msg_workpause = "std_msgs/String";

        public string topic_workresume = "/WAS/resume";
        public string msg_workresume = "std_msgs/String";

        public string topic_cmdvel = "/cmd_vel";
        public string msg_cmdvel = "geometry_msgs/Twist";

        public string topic_controller_state = "/controller_state"; // CPU, MEM 점유율 상태 Topic
        public string msg_controllerstate = "std_msgs/Float32MultiArray";

        //ur_status
        public string topic_urstatus ="/ur_status";
        public string msg_urstatus = "syscon_msgs/URStatus";

        //20190314 add
        public string topic_workrequest = "/WAS/request";
        public string msg_workrequest = "syscon_msgs/WorkRequest";

        public string topic_except_check = "/except_check";
        public string msg_except_check = "std_msgs/Int16";

        public string topic_xisStatus = "xislist";
        public string msg_xisStatus = "syscon_msgs/XISList";

        //20190316 add
        public string topic_lift_state = "/lift_state";
        public string msg_lift_state = "std_msgs/Int32MultiArray";

        public string topic_markerdetect = "/ar_pose_marker";
        public string msg_markerdetect = "ar_track_alvar_msgs/AlvarMarkers";

        public string topic_cam1_info = "/cam_1/color/image_raw"; // compressed";
        public string msg_cam1_info = "sensor_msgs/Image";

        //public string topic_cam2_info = "/cam_2/color/image_raw/compressed";
        public string topic_cam2_info = "/cam_2/color/image_raw";
        public string msg_cam2_info = "sensor_msgs/Image";

        //20190318 add
        public string topic_set_liftstatus = "/set_liftstatus";
        public string msg_set_liftstatus = "std_msgs/Int32";

        //20190319 add
        public string topic_lookahead = "/lookahead";
        public string msg_lookahead = "geometry_msgs/PointStamped";

        public string topic_GoalrunningStatus = "/move_base/status";
        public string msg_GoalrunningStatus = "actionlib_msgs/GoalStatusArray";

        public string topic_robotcurrAngluar = "/response";
        public string msg_robotcurrAngluar = "std_msgs/Float32";

        public string topic_taskorder = "/RIDiS/taskorder";
        public string msg_taskorder = "syscon_msgs/TaskOrder";

        public string topic_taskfeedback = "/TARU/feedback";
        public string msg_taskfeedback = "syscon_msgs/TaskFlowActionFeedback";

        public string topic_taskresult = "/TARU/result";
        public string msg_taskresult = "syscon_msgs/TaskFlowActionResult";

        public string topic_taskcancel = "/TARU/cancel";
        public string msg_taskcancel = "actionlib_msgs/GoalID";

        public string topic_taskpause = "/TARU/pause";
        public string msg_taskpause= "std_msgs/String";

        public string topic_taskresume = "/TARU/resume";
        public string msg_taskresume = "std_msgs/String";

        public string topic_missionchg = "/TARU/mission_change";
        public string msg_missionchg = "std_msgs/String";

        public string topic_connecteddevices = "/RIDiS/connected_devices";
        public string msg_connecteddevices = "syscon_msgs/ConnectedDevice";

        public string topic_tempomove = "/tempo_move";
        public string msg_tempomove = "syscon_msgs/TempoMove";

        public string topic_sp_routine = "/sp_routine";
        public string msg_sp_routine = "std_msgs/String";

        public string topic_map_save = "/RIDiS/save_map_db";
        public string msg_map_save = "std_msgs/String";

        // end jo update


        public string topic_test_1 = "com_test/pub0";
        public string msg_test_1 = "std_msgs/Float32";

        
        
        
        /* Read */
        

        public string topic_localPath = "move_base/TebLocalPlannerROS/local_plan";
        public string msg_localPath = "nav_msgs/Path";

        public string topic_robotlist = "robolist";
        public string msg_robotlist = "syscon_msgs/RoboList";

        public string topic_simroboStatus = "robolist_S";
        public string msg_simroboStatus = "syscon_msgs/RoboList";

        public string topic_plcMonitoring = "";
        public string msg_plcMonitoring = "";

        public string topic_palletMonitoring = "";
        public string msg_palletMonitoring = "";

        public string topic_chargingMonitoring = "";
        public string msg_chargingMonitoring = "";

      

        

        public string topic_robotPose = "/amcl_pose";
        public string msg_robotPose = "Pose";
        
        public string topic_ultrasonic_modified = "/ultrasonic_modified";
        public string msg_ultrasonic_modified = "std_msgs/Float32MultiArray";

       

       

        public string topic_conv_state = "/conv_state";
        public string msg_conv_state = "std_msgs/Int32MultiArray";

        /* Write */
        public string topic_manVel = "man_vel";
        public string msg_manVel = "geometry_msgs/Twist";

        public string topic_manLift = "man_lift";
        public string msg_manLift = "std_msgs/Int32";

        public string topic_manConv = "man_conv";
        public string msg_manConv = "std_msgs/Int32";

        //public string topic_workOrder = "work_order";
        //public string msg_workOrder = "syscon_msgs/WorkOrder";

        //public string topic_setGoal = "set_goal";
        //public string msg_setGoal = "geometry_msgs/Pose2D";

        public string topic_plcop = "plc_op";
        public string msg_plcop = "std_msgs/Int32MultiArray";

        public string topic_palletblink = "pallet_blink";
        public string msg_palletblink = "std_msgs/Int32";

        public string topic_palleton = "pallet_on";
        public string msg_palleton = "std_msgs/Int32";

        public string topic_chargeblink = "charge_blink";
        public string msg_chargeblink = "std_msgs/Int32";

        public string topic_chargeon = "charge_on";
        public string msg_chargeon = "std_msgs/Int32";

        

        /* Work Order */
         //public string topic_goal = "/WAS/goal";
        public string topic_goal = "/WAS/goal_";
        public string msg_goal = "syscon_msgs/WorkFlowActionGoal";

		public string topic_workcancle = "/WAS/cancel";
		public string msg_workcancle = "actionlib_msgs/GoalID";

		public string topic_workfeedback = "/WAS/feedback";
		public string msg_workfeedback = "syscon_msgs/WorkFlowActionFeedback";

		public string topic_workresult = "/WAS/result";
		public string msg_workresult = "syscon_msgs/WorkFlowActionResult";

        public string topic_workstatus = "/WAS/status";
        public string msg_workstatus = "actionlib_msgs/GoalStatusArray";

        public string topic_workmovestop = "/move_base/cancel";
        public string msg_workmovestop = "actionlib_msgs/GoalID";


        /* Service */
        public string service_CHARGE = "CHARGE";
        public string service_HOME = "HOME";
        public string service_ESTOP = "ESTOP";
        public string service_ECO = "ECO";
        public string service_Connect_Status = "connectivities";
        public string service_Connect_XIS = "xis_req";

        public string service_set_param = "/rosapi/set_param";
        public string service_get_param = "/rosapi/get_param";
        public string service_move_square = "R_TR501_000/move_square_service";
        public string service_move_square_s = "R_STR501_000/move_square_service";

        public string service_set_point = "/set_point";
        public string service_set_move = "/set_move";
        public string service_set_roboact = "/set_roboact";
        public string service_set_plc = "/set_plc";
        public string service_set_workstate = "/set_workstate";

        #region Parameter
        #region Robot Move base
        /* Robot Configuration Parameters */
        public string param_maxtransadd = "/move_base/PurePlanner/max_trans_acc"; //최대 이동가속도 설정
        public string param_maxrotacc = "/move_base/PurePlanner/max_rot_acc";    //최대 회전가속도 설정
        public string param_maxtransvel = "/move_base/PurePlanner/max_trans_vel"; //최대 이동속도 설정
        public string param_mintransvel = "/move_base/PurePlanner/min_trans_vel"; //최소 이동속도 설정
        public string param_maxrotvel = "/move_base/PurePlanner/max_rot_vel"; //최대 회전속도 설정
        public string param_minrotvel = "/move_base/PurePlanner/min_rot_vel"; //최소 회전속도 설정
        public string param_mininplacerotvel = "/move_base/PurePlanner/min_in_place_rot_vel";   //최소 제자리 회전속도 설정


        /* Goal Tolerance Parameters */
        public string param_yawgoaltolerance = "/move_base/PurePlanner/yaw_goal_tolerance"; //목표점 포즈 정확도 설정
        public string param_xygoaltolerance = "/move_base/PurePlanner/xy_goal_tolerance";  //목표점 위치 정확도 설정
        public string param_wptolerance = "/move_base/PurePlanner/wp_tolerance";    //Global Path와의 거리정도 설정

        /* Forward Simulation Parameters */
        public string param_simtime = "/move_base/PurePlanner/sim_time";    //장애물 회피를 위한 simulation 시간 설정
        public string param_simgranularity = "/move_base/PurePlanner/sim_granularity";  //simulation 시간의 이동속도 Step size 설정
        public string param_angularsimgranularity = "/move_base/PurePlanner/angular_sim_granularity";   //Simualtion 시간의 회전속도 Step Size 설정
        public string param_controller_freq = "/move_base/PurePlanner/controller_freq"; //Local Planner의 제어 주기 설정
        public string param_dwa = "/move_base/PurePlanner/dwa"; //회피기동(DWA)의 설정 유무

        /* Global CostMap Parameters */
        public string param_gbinflationradius = "/move_base/global_costmap/obstacle_layer/inflation_radius";  //Global Costmap의 Inflation 영역 설정
        public string param_gbupdatefrequency = "/move_base/global_costmap/update_frequency"; //Global Costmap의 업데이트 주기 설정
        public string param_gbpublishfrequency = "/move_base/global_costmap/publish_frequency";   //Global Costmap의 데이터 발행 주기 설정

        /* Local Costmap Parameters */
        public string param_lcinflationradius = "/move_base/local_costmap/inflation_layer/inflation_radius";    //Local costmap의 Inflation 영역 설정
        public string param_lcupdatefrequency = "/move_base/local_costmap/update_frequency";    //local costmap의 업데이트 주기 설정
        public string param_lcpublishfrequency = "/move_base/local_costmap/publish_frequency";  //local costmap의 데이터 발행 주기 설정
        public string param_lcwidth = "/move_base/local_costmap/width"; //local costmap의 지도크기(width) 설정
        public string param_lcheight = "/move_base/local_costmap/height";   //local costmap의 지도크기(height) 설정
        public string param_lcresolution = "/move_base/local_costmap/resolution";   //local costmap의 지도 해상도 설정

        /* etc */
        public string param_oscillationdistance = "/move_base/oscillation_distance";   //Path oscillation의 거리 threshhold 설정
        public string param_oscillationtimeout = "/move_base/oscillation_timeout";  //Path oscillation의 시간 threshhold 설정
        public string param_controllerfrequency = "/move_base/controller_frequency";    //Local Planner의 제어 주기 설정
        public string param_controllerpatience = "/move_base/controller_patience";  //Local Planner의 경로생성 실패 가능 횟수 설정
        public string param_plannerfrequnecy = "/move_base/planner_frequency";  //Globla Planner의 제어 주기 설정
        public string param_plannerpatience = "/move_base/planner_patience";    //Global Planner의 경로생성 실패 가능횟수 설정
        public string param_recoverybehaviorenabled = "/move_base/recovery_behavior_enabled";   //Recovery Behavior의 작동 유무 설정
        #endregion

        #region Robot amcl
        /* Overall filter parameters */
        public string param_initialposex = "/amcl/initial_pose_x";  //로봇의 초기 위치 설정(x)
        public string param_initalposey = "/amcl/initial_pose_y";   //로봇의 초기 위치 설정(y)
        public string param_initialposea = "amcl/initial_pose_a";  //로봇의 초기 위치 설정(a)
        public string param_maxparticles = "/amcl/max_particles";   //최대 파티클 필터 수 설정
        public string param_minparticles = "/amcl/min_particles";   //최소 파티클 필터 수 설정
        public string param_updatemina = "/amcl/update_min_a";  //업데이트를 위한 최소 이동각도 설정
        public string param_updatemind = "/amcl/update_min_d";  //업데이트를 위한 최소 이동거리 설정

        /* Odometry Model Parameters */
        public string param_odommodeltype = "/amcl/odom_model_type";
        public string param_odomalpha1 = "/amcl/odom_alpha1";
        public string param_odomalpha2 = "/amcl/odom_alpha2";
        public string param_odomalpha3 = "/amcl/odom_alpha3";
        public string param_odomalpha4 = "/amcl/odom_alpha4";
        public string param_odomalpha5 = "/amcl/odom_alpha5";
        #endregion
        #endregion
    }
}
