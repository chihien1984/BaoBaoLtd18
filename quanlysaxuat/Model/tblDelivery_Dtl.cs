using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    public partial class tblDelivery_Dtl
    {
        public Nullable<Double> TyleNangsuat { set; get; }
        public string Ma_NguonLuc { set; get; }
        public Int64 id { set; get; }
        public string Madh { set; get; }//Ma don hang
        public string Masp { set; get; }
        public string Tensp { set; get; }
        public string Ma_CongDoan {set;get;}
        public string CongDoan { set; get; }
        public Nullable<Double> Thoigian_Dinhmuc { set; get; }
        public Nullable<Double> Soluong_DonHang { set; get; }
        public Nullable<Double> TG_ChuanBi { set; get; }
        public Nullable<Double> TG_DuTru { set; get; }
        public Nullable<DateTime> BatDau { set; get; }
        public Nullable<DateTime> KetThuc { set; get; }
        public Nullable<double> WorkDay { set; get; }
        public Nullable<double> TG_DUTINH { set; get; }
        public Nullable<Double> TONGTG_DUTINH { set; get; }
        public Int64 idDonHang { set; get; }
        public string Ma_BoPhan { set; get; }
        public string To_bophan { set; get; }
        public Int64 ID_DMLD { set; get; }
    }
}
