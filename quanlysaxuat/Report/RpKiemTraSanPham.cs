using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Model;
using System.Data;
using System.Linq;

namespace quanlysanxuat.Report
{
    public partial class RpKiemTraSanPham : DevExpress.XtraReports.UI.XtraReport
    {
        public RpKiemTraSanPham()
        {
            InitializeComponent();
        }
        private void ThCheckNoiDung()
        {
            string maPhieuKiem = View.frmKiemTraSanPham.maPhieuKiem;
        }

        private void RpKiemTraSanPham_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            string idKiemTra = View.frmKiemTraSanPham.maPhieuKiem;
            Function.ConnectSanXuat();
            var sqlQuery = string.Format(@"select IDNoiDung,HangMuc,NoiDung,
                    CheckNoiDung,IDKiemTra
                    from tblKiemTraHangHoaNoiDung
                    where IDKiemTra like '{0}'", View.frmKiemTraSanPham.maPhieuKiem);
            var dataTable = Function.GetDataTable(sqlQuery);
            if (dataTable != null)
            {
                var kq11 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr11.Text).FirstOrDefault();
                if (kq11["CheckNoiDung"].ToString() == "1") { ck11Yes.Checked = true; ck11No.Checked = false; }
                else if (kq11["CheckNoiDung"].ToString() == "0") { ck11Yes.Checked = false; ck11No.Checked = true; };
                var kq12 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr12.Text).FirstOrDefault();
                if (kq12["CheckNoiDung"].ToString() == "1") { ck12Yes.Checked = true; ck12No.Checked = false; }
                else if (kq12["CheckNoiDung"].ToString() == "0") { ck12Yes.Checked = false; ck13No.Checked = true; };
                var kq13 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr13.Text).FirstOrDefault();
                if (kq13["CheckNoiDung"].ToString() == "1") { ck13Yes.Checked = true; ck13No.Checked = false; }
                else if (kq13["CheckNoiDung"].ToString() == "0") { ck13Yes.Checked = false; ck13No.Checked = true; };
                var kq14 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr14.Text).FirstOrDefault();
                if (kq14["CheckNoiDung"].ToString() == "1") { ck14Yes.Checked = true; ck14No.Checked = false; }
                else if (kq14["CheckNoiDung"].ToString() == "0") { ck14Yes.Checked = false; ck14No.Checked = true; };
                var kq21 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr21.Text).FirstOrDefault();
                if (kq21["CheckNoiDung"].ToString() == "1") { ck21Yes.Checked = true; ck21No.Checked = false; }
                else if (kq21["CheckNoiDung"].ToString() == "0") { ck21Yes.Checked = false; ck21No.Checked = true; };
                var kq31 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr31.Text).FirstOrDefault();
                if (kq31["CheckNoiDung"].ToString() == "1") { ck31Yes.Checked = true; ck31No.Checked = false; }
                else if (kq31["CheckNoiDung"].ToString() == "0") { ck31Yes.Checked = false; ck31No.Checked = true; };
                var kq32 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr32.Text).FirstOrDefault();
                if (kq32["CheckNoiDung"].ToString() == "1") { ck32Yes.Checked = true; ck32No.Checked = false; }
                else if (kq32["CheckNoiDung"].ToString() == "0") { ck32Yes.Checked = false; ck32No.Checked = true; };
                var kq41 = dataTable.AsEnumerable().Where(x => x.Field<string>("IDNoiDung") == xr41.Text).FirstOrDefault();
                if (kq41["CheckNoiDung"].ToString() == "1") { ck41Yes.Checked = true; ck41No.Checked = false; }
                else if (kq41["CheckNoiDung"].ToString() == "0") { ck41Yes.Checked = false; ck41No.Checked = true; };
            };
            //Checked
            var sqlQueryFinal = string.Format(@"select IDKiemTra,NguoiLap,NgayLap,MaQuanLy,
	            CheckKPPN,CheckKQ,FinalCheck
                from tblKiemTraHangHoa
                group by IDKiemTra,NguoiLap,NgayLap,
				MaQuanLy,CheckKPPN,CheckKQ,FinalCheck");
            var dtFinal = Function.GetDataTable(sqlQueryFinal);
            if (dtFinal != null)
            {
                var ckKetQuaFinal = dtFinal.AsEnumerable().Where(x => x.Field<string>("IDKiemTra") == idKiemTra).FirstOrDefault();
                //check final
                if (ckKetQuaFinal["FinalCheck"].ToString() == "") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false; };
                if (ckKetQuaFinal["FinalCheck"].ToString() == "First Fty Final") { ckFinalFirst.Checked = true; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false; };
                if (ckKetQuaFinal["FinalCheck"].ToString() == "2nd Fty Final") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = true; ckInLine.Checked = false; ckPilot.Checked = false; };
                if (ckKetQuaFinal["FinalCheck"].ToString() == "In-Line") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = true; ckPilot.Checked = false; };
                if (ckKetQuaFinal["FinalCheck"].ToString() == "Pilot Run") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = true; };
                //Kết quả kiểm
                //string a = ckKetQuaFinal["CheckKQ"].ToString();
                //Console.WriteLine(a);
                ckKetQuaNo.Checked = false;ckKetQuaYes.Checked = false;
                ckKhacPhucNo.Checked = false;ckKhacPhucYes.Checked = false;
                if (ckKetQuaFinal["CheckKQ"].ToString() == "True") { ckKetQuaYes.Checked = true;}
                else if (ckKetQuaFinal["CheckKQ"].ToString() == "False") { ckKetQuaNo.Checked = false;}
                //if (ckKetQuaFinal["CheckKQ"].ToString() == "True") { ckKetQuaYes.Checked = true;}
                //else if (ckKetQuaFinal["CheckKQ"].ToString() == "False") { ckKetQuaYes.Checked = false; ckKetQuaNo.Checked = true;};

                //Check khắc phục phòng ngừa
                if (ckKetQuaFinal["CheckKPPN"].ToString() == "True") { ckKhacPhucYes.Checked = true;}
                else if (ckKetQuaFinal["CheckKPPN"].ToString() == "False") { ckKhacPhucYes.Checked = false;};
            }
        }
    }
}
