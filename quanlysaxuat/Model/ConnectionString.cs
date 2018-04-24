using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace quanlysanxuat
{   

    public class Connect
    {
        public static string mConnect = Mahoa.Decrypt(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
        //public static string mConnect = "Data Source=192.168.1.22;Initial Catalog = SANXUAT; Persist Security Info=True;User ID = Data; Password=6ZF4T%69^#u8";

        public string ConectSanxuat;
        public string ConnectChamCong;
        public Connect()
        {
            ConectSanxuat = "Data Source=192.168.1.22;Initial Catalog = SANXUAT; Persist Security Info=True;User ID = Data; Password=6ZF4T%69^#u8";   
        }
    }
}