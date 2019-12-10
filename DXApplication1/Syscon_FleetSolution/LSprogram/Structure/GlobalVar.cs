using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Syscon_Solution.LSprogram.Structure
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class GlobalVar
    {


        #region 통신 Command 및 structure

        //mx7100 -> mx710.0 => 710*16 + 0(bit) //bit로 변환
        //m7100 => m710.0 => 710*2 + 0bit //byte로 변환

        #region struct 모음

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STApplicationHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] companyid;// = new byte[8];
            public ushort Reserved;
            public ushort PLCinfo;
            public byte CPUinfo;
            public byte SourceofFrame;
            public ushort InvokeID;
            public ushort Length;
            public byte moduleposition;
            public byte Reserved2;
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_One_Data
        {
            public ushort cmd;// 0x0054
            public ushort Datatype; //0x0014
            public ushort Reserved2;//0x0000
            public ushort blockcnt; //0x0001
            public ushort variable_length; //0x0006    ex)%M7000
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Continuous_ByteData
        {
            public ushort cmd;// 0x0054
            public ushort Datatype; //0x0014
            public ushort Reserved2;//0x0000
            public ushort blockcnt; //0x0001
            public ushort variable_length; //0x0006    ex)%M7000
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] variable; // %M7000
            public ushort Data_count; //0x0006  ex)데이타개수 6개
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Read_header
        {
            public ushort cmd;// 0x0054
            public ushort Datatype; //0x0014
            public ushort Reserved2;//0x0000
            public ushort blockcnt; //0x0001
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Read_Variable
        {
            public ushort variable_length; //0x0006    ex)%M7000
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] variable; // %M7000
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Write_Data
        {
            public ushort cmd;// 0x0058
            public ushort Datatype; //0x0000
            public ushort Reserved2;//0x0000
            public ushort cnt; //0x0001
            public ushort variable_length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] variable;
            public ushort Data_lenght; //0x0001  
            public byte Data; //1 or 0
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Continuance_Data_recv
        {
            public ushort cmd;// 0x0055
            public ushort Datatype; //0x0014
            public ushort Reserved2;//0x0000
            public ushort err_status;//0x0000
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct STInstruction_Continuance_Data_recv_sub
        {
            public ushort blockcnt;
            public ushort data_count;

        };


        #endregion
        #endregion

        #region memcpy

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        struct STEthFirstFrame2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] companyid;// = new byte[8];

            public ushort command;
            public ushort length;
        };


        [DllImport("MSVCR71.DLL")]
        public static extern void memcpy(ref ushort dest, ref byte src, int count);
        [DllImport("MSVCR71.DLL")]
        public static extern void memcpy(ref byte dest, ref byte src, int count);
        [DllImport("MSVCR71.DLL")]
        public static extern void memcpy(ref uint dest, ref byte src, int count);
        [DllImport("MSVCR71.DLL")]
        public static extern void memcpy(ref byte dest, ref uint src, int count);
        [DllImport("MSVCR71.DLL")]
        public static extern void memcpy(ref byte dest, ref ushort src, int count);
        #endregion

        public static bool IsInputNumber_In_TextBox(string strData)
        {
            char[] a = new char[strData.Length];

            a = strData.ToCharArray(0, strData.Length);
            for (int i = 0; i < strData.Length; i++)
            {
                if (a[i] < 48 || a[i] > 57)
                {
                    return false;
                }
            }
            return true;
        }


        /*
		 * 텍스트파일을 읽어서 날짜형태로 변환....
		 */
        public static DateTime OnChange_stringToDate(string strdate)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime dateTimeValueA = Convert.ToDateTime(strdate, culture);

            return dateTimeValueA;
        }



        public GlobalVar()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //
        }
    }
}
