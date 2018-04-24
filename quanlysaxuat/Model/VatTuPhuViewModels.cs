using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat.Model
{
  public class VatTuPhuViewModels
    {
        public long IDVatTu { get; set; }
        public string MaVatTu { get; set; }
        public string TenVatTu { get; set; }
        public Nullable<double> Soluong { get; set; }
        public string DonVi { get; set; }
        public string Nguoilap { get; set; }
        public Nullable<System.DateTime> Ngaylap { get; set; }
        public Nullable<double> TongNhap { get; set; }
        public Nullable<double> TongXuat { get; set; }
        public Nullable<double> Toncuoi { get; set; }
        public string ViTri { get; set; }
        public Nullable<double> DinhMucTon { get; set; }
    }
}
