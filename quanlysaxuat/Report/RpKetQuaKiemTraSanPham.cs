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
    public partial class RpKetQuaKiemTraSanPham : DevExpress.XtraReports.UI.XtraReport
    {
        public RpKetQuaKiemTraSanPham()
        {
            InitializeComponent();
        }
        string idketquakiemtra;
        private void ThKetQuaChiTiet()
        {
           
        }

        private void RpKiemTraSanPham_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            idketquakiemtra = View.frmKetQuaKiemTraSanPham.idKiemTra;
            KetQuaKiemTraThongTin();//List thông tin kiểm tra sản phẩm
            KetQuaKiemTraNgoaiQuan();//List kết quả kiểm tra ngoại quan
            KetQuaKiemTraNoiDung();//List ket qua kiem noi dung kiem tra
        }
        //DataTable Thong tin kiem tra san phan
        private void KetQuaKiemTraThongTin()
        {
            //Thong tin lo hang
            var sqlQueryThongTin = string.Format(@"SELECT ID,IDKQKiemTra,FinalCheck,
                MaSanPham,TenSanPham,NgayBHBV,PO,PhieuSanXuat,
                CodeBB,NgayGiao,SoLuongGiao,SoThungBich,
                TomTatKetQua,KetQuaKiem,HDKhacPhuc,PPXuLy,
                KQPPXuLy,NguoiLap,NgayLap,NguoiHC,
                NgayHC,ToXacNhan  FROM KetQuaKTSP where IDKQKiemTra like '{0}'", idketquakiemtra);
            var dt = Function.GetDataTable(sqlQueryThongTin);
                xrTableCellMaSPKH.DataBindings.Add("Text",dt, "MaSanPham");
                xrTableCellTenSanPham.DataBindings.Add("Text",dt, "TenSanPham");
                xrTableCellNgayBanHanh.DataBindings.Add("Text", dt, "NgayBHBV").FormatString = "{0:dd/MM/yyyy}";

                xrTableCellMaPo.DataBindings.Add("Text",dt, "PO");
                xrTableCellPSX.DataBindings.Add("Text",dt, "PhieuSanXuat");
                xrTableCellCodeBB.DataBindings.Add("Text",dt, "CodeBB");
                xrTableCellNguoiLap.DataBindings.Add("Text",dt, "NguoiLap");
                xrTableCellNgayLap.DataBindings.Add("Text",dt, "NgayLap").FormatString = "{0:dd/MM/yyyy}";
            //Nguoi lap ngay lap

            //Khach hang nha cung cap
                xrTableCellNgayGiao.DataBindings.Add("Text",dt, "NgayGiao").FormatString = "{0:dd/MM/yyyy}";
                xrTableCellSoLuongGiao.DataBindings.Add("Text",dt, "SoLuongGiao").FormatString="{0:#,#}";
                xrTableCellSoThungBich.DataBindings.Add("Text",dt, "SoThungBich");
            //
                xrTableCellToSanXuat.DataBindings.Add("Text",dt, "ToXacNhan");
                xrTableCellTomTatKetQua.DataBindings.Add("Text",dt, "TomTatKetQua");
                xrTableCellKetQuaXuLy.DataBindings.Add("Text",dt, "KQPPXuLy");
                xrTableCellMaQuanLy.DataBindings.Add("Text",dt, "IDKQKiemTra");
            //finalcheck
                string finalCheck = dt.Rows[0]["FinalCheck"].ToString();
                if (finalCheck == "") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false;}
                else if (finalCheck == "First Fty Final") { ckFinalFirst.Checked = true; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = false;}
                else if (finalCheck == "2nd Fty Final") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = true; ckInLine.Checked = false; ckPilot.Checked = false;}
                else if (finalCheck == "In-Line") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = true; ckPilot.Checked = false;}
                else if (finalCheck == "Pilot Run") { ckFinalFirst.Checked = false; ckFinalfty2nd.Checked = false; ckInLine.Checked = false; ckPilot.Checked = true;};
            //kết quả kiểm tra
            string ketquakiemtra = dt.Rows[0]["KetQuaKiem"].ToString();
            if(ketquakiemtra == "") { ckKetQuaYes.Checked = false ; ckKetQuaNo.Checked = false;}
            else if (ketquakiemtra == "1") { ckKetQuaYes.Checked = true;}
            else if (ketquakiemtra == "0") { ckKetQuaNo.Checked = true;}
            //Hành động khắc phục
            string hanhdongkppn = dt.Rows[0]["HDKhacPhuc"].ToString();
            if (ketquakiemtra == "") { ckKhacPhucYes.Checked = false; ckKhacPhucNo.Checked = false; }
            else if (hanhdongkppn == "1") { ckKhacPhucYes.Checked = true; }
            else if (hanhdongkppn == "0") { ckKhacPhucNo.Checked = true; }
            //Phương pháp xử lý
            string phuongphapxuly = dt.Rows[0]["PPXuLy"].ToString();
            if (phuongphapxuly == "") { xrCheckTraVeNoiSanXuat.Checked = false;xrCheckLuaChonKhac.Checked = false; }
            else if (phuongphapxuly == "Trả về nơi sản xuất") { xrCheckTraVeNoiSanXuat.Checked = true; }
            else if (phuongphapxuly == "Lựa chọn khác") { xrCheckLuaChonKhac.Checked = true;}


        }
        private void KetQuaKiemTraNgoaiQuan()
        {
            var sqlQueryNgoaiQuan = string.Format(@"select IDKQKiemTra,MucKiemTra,AQLLevel,SoLuongKiem,
                LoiChapNhan,LoiPhatHien,KetQuaYes,KetQuaNo
                from KetQuaKTSPNgoaiQuan 
                where IDKQKiemTra like '{0}'",idketquakiemtra);
            var dataTable = Function.GetDataTable(sqlQueryNgoaiQuan);
            string muc = dataTable.Rows[0]["MucKiemTra"].ToString();
            string muc1 = dataTable.Rows[1]["MucKiemTra"].ToString();

            string aql = dataTable.Rows[0]["AQLLevel"].ToString();
            string aql1 = dataTable.Rows[1]["AQLLevel"].ToString();
            
            string sokiem = dataTable.Rows[0]["SoLuongKiem"].ToString();
            string sokiem1 = dataTable.Rows[1]["SoLuongKiem"].ToString();
            
            string loichapnhan = dataTable.Rows[0]["LoiChapNhan"].ToString();
            string loichapnhan1 = dataTable.Rows[1]["LoiChapNhan"].ToString();
            
            string loiphathien = dataTable.Rows[0]["LoiPhatHien"].ToString();
            string loiphathien1 = dataTable.Rows[1]["LoiPhatHien"].ToString();

            bool ketquayes = Convert.ToBoolean(dataTable.Rows[0]["KetQuaYes"].ToString());
            bool ketquayes1 = Convert.ToBoolean(dataTable.Rows[1]["KetQuaYes"].ToString());

            bool ketquano = Convert.ToBoolean(dataTable.Rows[0]["KetQuaNo"].ToString());
            bool ketquano1 = Convert.ToBoolean(dataTable.Rows[1]["KetQuaNo"].ToString());

            xrTableCellMucKiem.Text= muc;
            xrTableCellMucKiem1.Text= muc1;
            xrTableCellAQL.Text = aql;
            xrTableCellAQL1.Text = aql1;
            xrTableCellSoLuongKiem.Text = sokiem;
            xrTableCellSoLuongKiem1.Text = sokiem1;
            xrTableCellLoiChapNhan.Text = loichapnhan;
            xrTableCellLoiChapNhan1.Text = loichapnhan1;
            xrTableCellLoiPhatHien.Text = loiphathien;
            xrTableCellLoiPhatHien1.Text = loiphathien1;
            xrCheckBoxKQYes.Checked = ketquayes;
            xrCheckBoxKQYes1.Checked = ketquayes1;
            xrCheckBoxNo.Checked = ketquano;
            xrCheckBoxNo1.Checked = ketquano1;
        }
        private void KetQuaKiemTraNoiDung()
        {
            string strQuery = string.Format(@"SELECT ID,IDNoiDung,NoiDungKT,HangMuc,
                IDKQKiemTra,KetQuaChiTiet FROM KetQuaKTSPNoiDung where IDKQKiemTra like '{0}'", idketquakiemtra);
            DataTable dataTable = GetDataTable(strQuery);
            var xrl11 = dataTable.Select("IDNoiDung like '%" + xr11.Text + "%'");
            DataRow[] xrl12 = dataTable.Select("IDNoiDung like '%" + xr12.Text + "%'");
            DataRow[] xrl13 = dataTable.Select("IDNoiDung like '%" + xr13.Text + "%'");
            DataRow[] xrl14 = dataTable.Select("IDNoiDung like '%" + xr14.Text + "%'");
            DataRow[] xrl21 = dataTable.Select("IDNoiDung like '%" + xr21.Text + "%'");
            DataRow[] xrl31 = dataTable.Select("IDNoiDung like '%" + xr31.Text + "%'");
            DataRow[] xrl32 = dataTable.Select("IDNoiDung like '%" + xr32.Text + "%'");
            DataRow[] xrl41 = dataTable.Select("IDNoiDung like '%" + xr41.Text + "%'");
            DataRow[] xrlNoiDungKhac = dataTable.Select("IDNoiDung like '%" + xr41.Text + "%'");

 
            //string a = xrl11[0]["KetQuaChiTiet"].ToString();
            if (xrl11[0]["KetQuaChiTiet"].ToString() == "")       { ck11Yes.Checked = false; ck11No.Checked = false; }
            else if (xrl11[0]["KetQuaChiTiet"].ToString() == "1") { ck11Yes.Checked = true;}
            else if(xrl11[0]["KetQuaChiTiet"].ToString()  == "0") { ck11No.Checked = true;};

            if (xrl12[0]["KetQuaChiTiet"].ToString() == "")      { ck12Yes.Checked = false; ck12No.Checked = false; }
            else if(xrl12[0]["KetQuaChiTiet"].ToString() == "1") { ck12Yes.Checked = true;}
            else if(xrl12[0]["KetQuaChiTiet"].ToString() == "0") { ck12No.Checked = true;};

            if (xrl13[0]["KetQuaChiTiet"].ToString() == "")      { ck13Yes.Checked = false; ck13No.Checked = false; }
            else if(xrl13[0]["KetQuaChiTiet"].ToString() == "1") { ck13Yes.Checked = true;}
            else if(xrl13[0]["KetQuaChiTiet"].ToString() == "0") { ck13No.Checked = true;};

            if (xrl14[0]["KetQuaChiTiet"].ToString() == "")      { ck14Yes.Checked = false; ck14No.Checked = false; }
            else if(xrl14[0]["KetQuaChiTiet"].ToString() == "1") { ck14Yes.Checked = true;}
            else if(xrl14[0]["KetQuaChiTiet"].ToString() == "0") { ck14No.Checked = true;};

            if (xrl21[0]["KetQuaChiTiet"].ToString() == "")      { ck21Yes.Checked = false; ck21No.Checked = false; }
            else if(xrl21[0]["KetQuaChiTiet"].ToString() == "1") { ck21Yes.Checked = true;}
            else if(xrl21[0]["KetQuaChiTiet"].ToString() == "0") { ck21No.Checked = true;};

            if (xrl31[0]["KetQuaChiTiet"].ToString() == "")      { ck31Yes.Checked = false; ck31No.Checked = false; }
            else if(xrl31[0]["KetQuaChiTiet"].ToString() == "1") { ck31Yes.Checked = true;}
            else if(xrl31[0]["KetQuaChiTiet"].ToString() == "0") { ck31No.Checked = true;};

            if (xrl32[0]["KetQuaChiTiet"].ToString() == "")      { ck32Yes.Checked = false; ck32No.Checked = false; }
            else if(xrl32[0]["KetQuaChiTiet"].ToString() == "1") { ck32Yes.Checked = true;}
            else if(xrl32[0]["KetQuaChiTiet"].ToString() == "0") { ck32No.Checked = true;};

            if (xrl41[0]["KetQuaChiTiet"].ToString() == "")      { ck41Yes.Checked = false; ck41No.Checked = false; }
            else if(xrl41[0]["KetQuaChiTiet"].ToString() == "1") { ck41Yes.Checked = true;}
            else if(xrl41[0]["KetQuaChiTiet"].ToString() == "0") { ck41No.Checked = true;};
            xrTableCellNoiDungLoiKhac.Text = xrlNoiDungKhac[0]["NoiDungKT"].ToString();
        }

        //Thể hiện nội dung Check

        //GetDataTable dùng chung
        private DataTable GetDataTable(string stringQuery)
        {
            Function.ConnectSanXuat();
            var dataTable = Function.GetDataTable(stringQuery);
            Function.Disconnect();
            return dataTable;
        }
    }
}
