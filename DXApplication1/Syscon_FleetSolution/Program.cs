using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;

namespace Syscon_Solution
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            //Application.Run(new LS_TEST.mappingform());
            //Application.Run(new Fleet_Main());



            //Application.Run(new Exhibition.exhibitionMain());
            Application.Run(new LSprogram.connectionForm());
            //Application.Run(new LSprogram.conntest());
            //Application.Run(new LSprogram.mainForm());

            //Application.Run(new LSprogram.plctestform());

        }
    }
}