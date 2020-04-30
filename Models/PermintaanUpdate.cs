using AutoUpdateApps.Models;
//using Medifirst2000.Framework;

namespace AutoUpdateApps.Models
{
    public class PermintaanUpdate
    {
        public static bool UpdateRequested { get; set; }

        public static bool CheckUpdateRequest()
        {
            //var medi = new MedifirstSql(Pengaturan.Koneksi);
            //var query = medi.SqlReader("SELECT 1 FROM tempSIMRSUpdate WHERE LocalIP = '" + UserLocalIP.LocalIP + "' AND NamaPC = '" + UserLocalIP.NamaKomputer + "'");

            //while (query.Read())
            //    UpdateRequested = true;

            return UpdateRequested;
        }
    }
}