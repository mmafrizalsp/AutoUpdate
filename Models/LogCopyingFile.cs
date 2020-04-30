using System;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdateApps.Models
{
    public class LogCopyingFile
    {
        public DateTime? TimeExecute { get; set; }
        public string FileName { get; set; }
        public string IpRequested { get; set; }
        public string HostReqested { get; set; }
    }
}