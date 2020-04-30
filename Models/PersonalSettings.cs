using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AutoUpdateApps.Models
{
    public class PersonalSettings
    {
        public static class UserLocalIP
        {
            public static string LocalIP { get; set; }
            public static string NamaKomputer { get; set; }

            public static string GetHost()
            {
                IPHostEntry host = Dns.GetHostEntry(GetLocalIP());
                LocalIP = host.HostName.ToString();
                return LocalIP;
            }

            public static string GetLocalIP()
            {
                var HostName = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in HostName.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        NamaKomputer = ip.ToString();
                    }
                }
                return NamaKomputer;
            }
        }
    }
}