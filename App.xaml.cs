using AutoUpdateApps.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AutoUpdateApps
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcherOnUnhandledException;
            Dispatcher.UnhandledException += DispatcherOnUnhandledException;
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            new Log().IntoLog(e.Exception.GetBaseException().ToString());
            e.Handled = true;
        }

        private void CurrentDispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            new Log().IntoLog(e.Exception.GetBaseException().ToString());
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var jsonFile = File.ReadAllText(path + "\\pengaturan.json");
            var dist = JsonConvert.DeserializeObject<PengaturanViewModel>(jsonFile);
            if (dist != null)
            {
                PengaturanAplikasi.Source = dist.Source;
                PengaturanAplikasi.Destination = dist.Destination;
                PengaturanAplikasi.TargetStartApps = dist.TargetStartApps;
            }
        }
    }
}
