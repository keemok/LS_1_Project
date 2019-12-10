using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syscon_Solution.LSprogram
{
    class APsnmp
    {
        string deviceName;
        string ipAddr;
        string OID;
        const int nPort = 161;
        const int timeOut = 2000;
        const int retry = 2;
        OctetString community = new OctetString("private");
        Oid test = new Oid(".1.3.6.1.6.3.16.1.4.1.9.9.77.121.82.87.71.114.111.117.112.0.0.1");
        //Oid connectAP = new Oid(".1.3.6.1.4.1.8691.15.35.1.11.17.1.10");
        Pdu pdu = new Pdu(PduType.Get);
        public string response;

        public event SnmpAsyncResponse snmpRecv_evt;


        public APsnmp(string ipadress)
        {
            ipAddr = ipadress;
        }

        public void getData()
        {

            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(ipAddr);
            UdpTarget target = new UdpTarget((System.Net.IPAddress)agent, nPort, timeOut, retry);

            if (pdu.RequestId != 0)
            {
                pdu.RequestId += 1;
                pdu.VbList.Clear();
                pdu.VbList.Add(test);

                SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);

                if (result != null)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        Console.WriteLine("Error in SNMP. Error {0}, Index {1}",
                            result.Pdu.ErrorStatus,
                            result.Pdu.ErrorIndex);
                    }
                    else
                    {
                        response = result.Pdu.VbList[0].Value.ToString();

                        Console.WriteLine("Data is {0} .", result.Pdu.VbList[0].Value.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("No response received from SNMP");
                }
            }
            target.Close();
        }
    }
}
