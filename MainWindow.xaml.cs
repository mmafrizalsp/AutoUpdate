using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace AutoUpdateApps
{
    public partial class MainWindow : Window
    {
        //public static string source = "D:\\kerjaan\\kanujoso\\Project Kanujoso Pusat\\Production\\MedifirstV2\\Medifirst2000.ElectronicMedicalRecord\\Medifirst2000.ElectronicMedicalRecord\\bin\\Debug";
        public static string source = @"\\172.16.0.190\\EMR update\\20200421 23.38\\20200421 23.38";
        public static string destionation = "C:\\==EXE\\EMR";
        public int sourcecount, destCount;
        public BackgroundWorker worker = new BackgroundWorker();        

        public MainWindow()
        {
            InitializeComponent();
            IsDirectoryAvailable();

            //lblJumlah.Content = destCount + " of " + sourcecount;
            //ProsesUpdate();
        }        

        public void IsDirectoryAvailable()
        {
            if (Directory.Exists(source))
                sourcecount = Directory.GetFiles(source, "*", SearchOption.AllDirectories).Length;
            else
                sourcecount = 0;

            if (Directory.Exists(destionation))
                destCount = Directory.GetFiles(destionation, "*", SearchOption.AllDirectories).Length;
            else
                destCount = 0;            
        }

        public static void ProsesUpdate()
        {
            Copy(source, destionation);
            MessageBox.Show("Update berhasil");

            Application.Current.Shutdown();

            Process EMR = new Process();
            EMR.StartInfo.FileName = "C:\\==EXE\\EMR\\Medifirst2000.ElectronicMedicalRecord.exe";
            EMR.Start();
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
                Directory.Delete(destionation, true);
                CopyAll(diSource, diTarget);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => {
                Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    ProsesUpdate();
                }));
            }));
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
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
