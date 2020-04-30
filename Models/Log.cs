using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;

namespace AutoUpdateApps.Models
{
    public class Log
    {
        public void IntoLog(string log)
        {
            ThreadPool.QueueUserWorkItem(CreateCallBack, log);
        }

        private void CreateCallBack(object state)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Log " + DateTime.Now.ToString("yyy-MMM-dd", new CultureInfo("id-ID")) + ".txt";
            var date = DateTime.Now.ToString("yyy-MMM-dd hh:mm:ss", new CultureInfo("id-ID"));
            Debug.Print(Environment.NewLine + date + "   " + state);
            try
            {
                File.AppendAllText(path, Environment.NewLine + "[" + date + "] " + state);
            }
            catch (Exception e)
            {
                File.AppendAllText(path, "error: " + e.Message.ToString());
            }
        }
    }
}