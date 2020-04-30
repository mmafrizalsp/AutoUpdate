using System;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdateApps.Models
{
    public static class PengaturanAplikasi
    {
        public static string Source { get; set; }
        public static string Destination { get; set; }
        public static string LogPath { get; set; }
        public static string TargetStartApps { get; set; }
    }

    public class PengaturanViewModel
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string LogPath { get; set; }
        public string TargetStartApps { get; set; }
    }
}