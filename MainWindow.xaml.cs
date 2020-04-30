using AutoUpdateApps.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AutoUpdateApps
{
    public partial class MainWindow : Window
    {
        public static int sourcecount, destCount;  

        public MainWindow()
        {
            InitializeComponent();
            IsDirectoryAvailable();
        }        

        public void IsDirectoryAvailable()
        {            
            if (Directory.Exists(PengaturanAplikasi.Source))
                sourcecount = Directory.GetFiles(PengaturanAplikasi.Source, "*", SearchOption.AllDirectories).Length;
            else
                sourcecount = 0;

            if (Directory.Exists(PengaturanAplikasi.Destination))
                destCount = Directory.GetFiles(PengaturanAplikasi.Destination, "*", SearchOption.AllDirectories).Length;
            else
                destCount = 0;            
        }

        public static void ProsesUpdate()
        {
            Copy(PengaturanAplikasi.Source, PengaturanAplikasi.Destination);
            NotifTransaksi();

            //dimatikan, karena statis ke aplikasi tujuan
            //Process EMR = new Process();
            //EMR.StartInfo.FileName = Pengaturan.TargetStartApps;
            //EMR.Start();            
        }        

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            if (!diTarget.Exists)
            {
                CopyAll(diSource, diTarget);
            }
            else
            {
                Directory.Delete(PengaturanAplikasi.Destination, true);
                CopyAll(diSource, diTarget);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Dispatcher.Invoke(DispatcherPriority.Render, new Action(() =>
            {
                Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    ProsesUpdate();
                    for (int i = 1; i < sourcecount; i++)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(TaskCallBack), i);
                        Thread.Sleep(0);
                    }
                }));
            }));
        }
         
        public void TaskCallBack(object ThreadNumber)
        {
            string ThreadName = "Thread " + ThreadNumber.ToString();
            for (int i = 1; i < sourcecount; i++)            
                lblJumlah.Content = ThreadName + " :" + i.ToString();
            
            MessageBox.Show(ThreadName = " Finished");
        }

        public static void NotifTransaksi()
        {
            MessageBoxResult result = MessageBox.Show("proses update selesai", "Electonic Medical Record", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }   
}
