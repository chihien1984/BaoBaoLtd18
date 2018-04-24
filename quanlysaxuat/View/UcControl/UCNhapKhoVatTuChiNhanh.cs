using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering.Templates;
using quanlysanxuat.Model;
using quanlysanxuat.Report;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Commands;

namespace quanlysanxuat.View.UcControl
{
    public partial class UCNhapKhoVatTuChiNhanh : DevExpress.XtraEditors.XtraForm
    {
        public UCNhapKhoVatTuChiNhanh()
        {
            InitializeComponent();
        }
        private string maphieu;
        private DateTime ngaylap;
        private string tenkho;

        private void btnLapPhieuNhapXuat_Click(object sender, EventArgs e)
        {
            frmNhapXuatVatTuChiNhanh nhapxuatkhocn = new frmNhapXuatVatTuChiNhanh("", ngaylap==null?DateTime.Now:ngaylap, "");
            nhapxuatkhocn.ShowDialog();
            THChiTietNhapTheoNgay();
            THChiTietXuatTheoNgay();
            THTonHopXuatNhapTonTheoNgay();
        }

        //formload
        private void UCNhapKhoVatTuChiNhanh_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            this.gvTonHopXuatNhapTon.Appearance.Row.Font = new Font("Segoe UI", 7f);
            this.gvChiTietNhap.Appearance.Row.Font = new Font("Segoe UI", 7f);
            this.gvChiTietXuat.Appearance.Row.Font = new Font("Segoe UI", 7f);
            THTonHopXuatNhapTonTheoNgay();
            THChiTietNhapTheoNgay();
            THChiTietXuatTheoNgay();
        }
        private void THTonHopXuatNhapTonTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"execute TonHopNhapXuatTonChiNhanh '{0}','{1}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grTonHopXuatNhapTon.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//dong ket noi
        }
        private void THChiTietNhapTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua,SoQuyDoi
			from VatTuNhapXuat where left(MaChungTu,2) = 'PN' and Del like 0
            and NgayChungTu between '{0}' and '{1}'",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"));
            grChiTietNhap.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void THChiTietXuatTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua,SoQuyDoi
			from VatTuNhapXuat where left(MaChungTu,2) = 'PX' and Del like 0
            and NgayChungTu between '{0}' and '{1}'", 
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"));
            grChiTietXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        //Su kien click vao so tong hop xuat nhap ton
        string mavattu;
        private void grTonHopXuatNhapTon_Click(object sender, EventArgs e)
        {
            if (gvTonHopXuatNhapTon.DataSource == null) { return; }
            else
            {
                string point = "";
                point = gvTonHopXuatNhapTon.GetFocusedDisplayText();
                mavattu = gvTonHopXuatNhapTon.GetFocusedRowCellDisplayText(mavattu_xnt);
                THChiTietNhapTheoMaVatTu();
                THChiTietXuatTheoMaVatTu();
            }
        }
        private void THChiTietXuatTheoMaVatTu()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua
			from VatTuNhapXuat where NgayChungTu between '{0}' and '{1}' and
			left(MaChungTu,2) = 'PX'
			and MaVatTu like N'{2}' and Del like 0",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"),
            mavattu);
            grChiTietXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }    

        private void THChiTietNhapTheoMaVatTu()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua,SoQuyDoi
			from VatTuNhapXuat where NgayChungTu between '{0}' and '{1}' and
			left(MaChungTu,2) = 'PN'
			and MaVatTu like N'{2}' and Del like 0",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"),
            mavattu);
            grChiTietNhap.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void THChiTietXuatTheoMaPhieuXuat()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua
			from VatTuNhapXuat where left(MaChungTu,2) = 'PX' and MaChungTu like N'{0}'", maphieu);
            grChiTietXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void THChiTietNhapTheoPhieuNhap()
        {
            Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select IDVatTu,IDKeHoachVatTu,MaChungTu,
			NgayChungTu,MaVatTu,TenVatTu,DonVi,SoLuong,DonGia,ThanhTien,
			DienGiai,Del,NguoiLap,NgayLap,NguoiSua,NgaySua,SoQuyDoi
			from VatTuNhapXuat where left(MaChungTu,2) = 'PN'
			and MaChungTu like N'{0}'", maphieu);
            grChiTietNhap.DataSource = Model.Function.GetDataTable(sqlQuery);
            Function.Disconnect();//Mo ket noi
        }
        private void btnChiTietNhap_Click(object sender, EventArgs e)
        {
            THChiTietNhapTheoNgay();
        }

        private void btnChiTietXuat_Click(object sender, EventArgs e)
        {
            THChiTietXuatTheoNgay();
        }

        private void btnTongHopXuatNhapTon_Click(object sender, EventArgs e)
        {
            THTonHopXuatNhapTonTheoNgay();
        }

        private void grChiTietNhap_Click(object sender, EventArgs e)
        {
            if (gvChiTietNhap.DataSource == null) { return; }
            else
            {
             
                frmNhapXuatVatTuChiNhanh nhapxuatkhocn = new frmNhapXuatVatTuChiNhanh(maphieu, ngaylap, tenkho);
                nhapxuatkhocn.ShowDialog();
                THChiTietNhapTheoNgay();
                THChiTietXuatTheoNgay();
                THTonHopXuatNhapTonTheoNgay();
            }
        }

        private void grChiTietXuat_Click(object sender, EventArgs e)
        {
            if (gvChiTietXuat.DataSource == null) 
            { return; }
            else
            {
                frmNhapXuatVatTuChiNhanh nhapxuatkhocn = new frmNhapXuatVatTuChiNhanh(maphieu, ngaylap, tenkho);
                nhapxuatkhocn.ShowDialog();
                THChiTietNhapTheoNgay();
                THChiTietXuatTheoNgay();
                THTonHopXuatNhapTonTheoNgay();
            }
        }

        private void btnExportXuatNhapTon_Click(object sender, EventArgs e)
        {
            gvTonHopXuatNhapTon.ShowPrintPreview(); 
        }

        private void btnExportChiTietNhap_Click(object sender, EventArgs e)
        {
            gvChiTietNhap.ShowPrintPreview();
        }

        private void btnExportChiTietXuat_Click(object sender, EventArgs e)
        {
            gvChiTietXuat.ShowPrintPreview();
        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
          Function.ConnectSanXuat();//Mo ket noi
          string sqlQuery = string.Format(@"select * from NhapXuatKhoCN_vw where 
              left(MaChungTu,2)='PN' and MaChungTu like N'{0}'", maphieu);
          ReportNhapKhoVatTuCN nhapKho = new ReportNhapKhoVatTuCN();
          nhapKho.DataSource = Function.GetDataTable(sqlQuery);
          nhapKho.DataMember = "Table";
          nhapKho.CreateDocument(false);
          nhapKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = maphieu;
            PrintTool tool = new PrintTool(nhapKho.PrintingSystem);
            nhapKho.ShowPreviewDialog();
          Function.Disconnect();
        }

        private void btnPhieuXuat_Click(object sender, EventArgs e)
        {
          Function.ConnectSanXuat();//Mo ket noi
          string sqlQuery = string.Format(@" select * from NhapXuatKhoCN_vw where left(MaChungTu,2)='PX' 
                      and MaChungTu like  N'{0}'", maphieu);
          ReportXuatKhoVatTuCN XuatKho = new ReportXuatKhoVatTuCN();
          XuatKho.DataSource = Function.GetDataTable(sqlQuery);
          XuatKho.DataMember = "Table";
          XuatKho.CreateDocument(false);
          XuatKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = maphieu;
            PrintTool tool = new PrintTool(XuatKho.PrintingSystem);
            XuatKho.ShowPreviewDialog();
          Function.Disconnect();
        }
         private void grChiTietNhap_Click_1(object sender, EventArgs e)
        {
            if (gvChiTietNhap.GetRowCellValue(gvChiTietNhap.FocusedRowHandle, gvChiTietNhap.Columns["MaChungTu"]) == null)
                return;
            string point = "";
            point = gvChiTietNhap.GetFocusedDisplayText();
            maphieu = gvChiTietNhap.GetFocusedRowCellDisplayText(maphieu_colnhap);
            tenkho = gvChiTietNhap.GetFocusedRowCellDisplayText(tenkho_nhap);
            ngaylap = Convert.ToDateTime(gvChiTietNhap.GetFocusedRowCellDisplayText(ngaylap_colnhap));

        }
        private void grChiTietXuat_Click_1(object sender, EventArgs e)
        {
            if (gvChiTietXuat.GetRowCellValue(gvChiTietXuat.FocusedRowHandle, gvChiTietXuat.Columns["MaChungTu"]) == null)
                return;
            string point = "";
            point = gvChiTietXuat.GetFocusedDisplayText();
            maphieu = gvChiTietXuat.GetFocusedRowCellDisplayText(maphieu_maxuat);
            tenkho = gvChiTietXuat.GetFocusedRowCellDisplayText(tenkho_xuat);
            ngaylap = Convert.ToDateTime(gvChiTietXuat.GetFocusedRowCellDisplayText(ngaylap_colxuat));
        }
    }
}
