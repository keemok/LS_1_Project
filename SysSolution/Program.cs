//#define _workorder // _commtest _workorder

//#undef _commtest // _demo



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SysSolution
{
    static class Program
    {
       
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // demo, commtest 

          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if _demo
            Application.Run(new DemoForm0131());
           Data.Instance.g_strRunmode= "demo";
#elif _commtest
            Application.Run(new Form1());
            Data.Instance.g_strRunmode = "commtest";
#elif _workorder

            Application.Run(new WorkOrderForm());
           Data.Instance.g_strRunmode= "demo";
#elif _map
            Application.Run(new MapDspForm());
           Data.Instance.g_strRunmode= "demo";
#elif _robotstatus

            Application.Run(new RobotStatusForm());
           Data.Instance.g_strRunmode= "RobotStatusForm";
#elif _jobhistory
            Application.Run(new RobotJob_HistoryForm());
            Data.Instance.g_strRunmode = "RobotJob_HistoryForm"; 

#elif _sol
            Application.Run(new Frm.DashboardForm());
            Data.Instance.g_strRunmode = "DashboardForm"; 
#elif _urtest
            Application.Run(new UR_Sample.URControl_TestFrm());
            Data.Instance.g_strRunmode = "DashboardForm";
#elif _statusone
            Application.Run(new RobotStatusForm_One());
            Data.Instance.g_strRunmode = "RobotStatusForm_One";
#elif _fleet
            Application.Run(new FleetManager.FleetManager_MainForm());
            Data.Instance.g_strRunmode = "FleetManager_MainForm";
#elif _delivery
            Application.Run(new Delivery.DeliveryForm());
            Data.Instance.g_strRunmode = "DeliveryForm";
#endif


        }
    }
}
