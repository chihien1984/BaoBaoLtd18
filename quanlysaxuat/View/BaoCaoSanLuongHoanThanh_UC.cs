using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class BaoCaoSanLuongHoanThanh_UC : UserControl
    {
        public BaoCaoSanLuongHoanThanh_UC()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            grBaoCaoSanLuong.ShowPrintPreview();
        }

        private void frmBaoCaoSanLuongHoanThanh_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Today.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            dpTuNgayCT.Text = DateTime.Today.ToString("01/MM/yyyy");
            dpDenNgayCT.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            DocDonHangHoanThanhSanLuong();
            this.grBaoCaoSanLuong.Appearance.Row.Font = new Font("Times New Roman", 7f);
            HienThiPhongBan();
            TraCuuThongTinBoPhanPhanTichThongKe();
            TheHienGiaoHangTheoNgay();
            /*Chi tiết giao hàng theo ngày*/
            HienThiPhongBanChiTiet();
            TraCuuThongTinBoPhanPhanTichChiTiet();
        }
        private void cbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            TraCuuThongTinBoPhanPhanTichThongKe();
            TheHienGiaoHangTheoNgay();
        }
        private void HienThiPhongBan()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select To_bophan from tblPHONGBAN 
                where PhanTichTienDo is not null");
            cbBoPhan.DataSource = kn.laybang(sqlQuery);
            cbBoPhan.DisplayMember = "To_bophan";
            cbBoPhan.ValueMember = "To_bophan";
            kn.dongketnoi();
        }
        private void HienThiPhongBanChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select To_bophan from tblPHONGBAN 
                where PhanTichTienDo is not null");
            cbTheHienBoPhanChiTiet.DataSource = kn.laybang(sqlQuery);
            cbTheHienBoPhanChiTiet.DisplayMember = "To_bophan";
            cbTheHienBoPhanChiTiet.ValueMember = "To_bophan";
            kn.dongketnoi();
        }
        private string functionPhanTich;
        private string tableID;
        private void TraCuuThongTinBoPhanPhanTichThongKe()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlQuery = string.Format(@"select PhanTichTienDo,Matable
                    from tblPHONGBAN where PhanTichTienDo is not null and To_bophan like N'{0}'", cbBoPhan.Text);
                cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    functionPhanTich = reader.GetString(0);
                    tableID = reader.GetString(1);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Message");
            };
        }
        private void TraCuuThongTinBoPhanPhanTichChiTiet()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlQuery = string.Format(@"select Matable,fieldSL
                    from tblPHONGBAN where PhanTichTienDo is not null and To_bophan like N'{0}'", 
                    cbTheHienBoPhanChiTiet.Text);
                cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    tableID = reader.GetString(0);
                    field = reader.GetString(1);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Message");
            };
        }
        private void cbTheHienBoPhanChiTiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            TraCuuThongTinBoPhanPhanTichChiTiet();
            TracuuGiaoHangChiTiet();
        }

        private void DocDonHangHoanThanhSanLuong()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from tblchitietkehoach where 
                ngaytrienkhai between '{0}' and '{1}' 
                order by ngaytrienkhai desc,IDSP asc",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grcBaoCaoSanLuong.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void btnTraCuuSanLuongHoanThanh_Click(object sender, EventArgs e)
        {
            DocDonHangHoanThanhSanLuong();
        }

        private void TraCuuXuatKho_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from tblchitietkehoach where 
               ngay11 between '{0}' and '{1}' 
               order by ngaytrienkhai desc,IDSP asc",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grcBaoCaoSanLuong.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
  
        private void btnXemDHConLai_Click(object sender, EventArgs e)
        {
            TheHienGiaoHangTheoNgay();
        }

        private void btnALLDMGIAOHANG_Click(object sender, EventArgs e)
        {

        }
        private void TheHienGiaoHangTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from {0}('{1}','{2}')",
                functionPhanTich,
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grPhanTichTienDo.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void btnTraCuuGiaoHangChiTiet_Click(object sender, EventArgs e)
        {
            TracuuGiaoHangChiTiet();
        }
        private void TracuuGiaoHangChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select IDSP IDTrienKhai,
                IDChiTietDonHang,MaDonHang,
                k.MaPo,MaSanPham,k.TenQuiCachSanPham,
                k.ChiTietSanPham,SLSPLR,k.So_CT,k.soluongsx,
                {0} SoGiao,ngaynhan NgayGiao,MaSQL,ngaynhan
                from {1} t
                inner join 
                (select IDSP IDTrienKhai,IdPSX IDChiTietDonHang,
                madh MaDonHang,MaPo,mabv MaSanPham,
                SPLR TenQuiCachSanPham,sanpham ChiTietSanPham,soluongsx,
                So_CT,SLSPLR from tblchitietkehoach)k
                on t.IDSP=k.IDTrienKhai where cast(t.ngaynhan as date)
                between '{2}' and '{3}'",
                field,tableID,
                dpTuNgayCT.Value.ToString("MM-dd-yyyy"),
                dpDenNgayCT.Value.ToString("MM-dd-yyyy"));
            grChiTietGiaoHang.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void grPhanTichTienDo_DoubleClick(object sender, EventArgs e)
        {
            frmChiTietSanXuat DSChiTietSanXuat = new frmChiTietSanXuat(madonHang, maSanPham,
            tenSanPham, Login.Username, idDonHang, idTrienKhai,
            soLuong, thongBao, tableID);
            DSChiTietSanXuat.ShowDialog();
        }
            private string madonHang;
            private string maSanPham;
            private string tenSanPham;
            private string soLuong;
            private string thongBao;
            private string idDonHang;
            private string idTrienKhai;
            private string field;
        private void grPhanTichTienDo_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvPhanTichTienDo.GetFocusedDisplayText();
            madonHang = gvPhanTichTienDo.GetFocusedRowCellDisplayText(madonhang_pt);
            soLuong = gvPhanTichTienDo.GetFocusedRowCellDisplayText(Soluongsx_grid);
            tenSanPham = gvPhanTichTienDo.GetFocusedRowCellDisplayText(tensanpham_pt);
            maSanPham = gvPhanTichTienDo.GetFocusedRowCellDisplayText(masanpham_pt);
            thongBao = gvPhanTichTienDo.GetFocusedRowCellDisplayText(congdoanthuchien_pt);
            idDonHang = gvPhanTichTienDo.GetFocusedRowCellDisplayText(iddonhangchitiet_pt);
            idTrienKhai = gvPhanTichTienDo.GetFocusedRowCellDisplayText(idtrienkhai_pt);
            txtMaSanPham.Text = maSanPham;
        }

        private void btnXemBV_Click(object sender, EventArgs e)
        {
            frmLoading f2 =
               new frmLoading(maSanPham, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnLayout_PSX_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + madonHang + "'");
            RpPhieusanxuat rpPHIEUSANXUAT = new RpPhieusanxuat();
            rpPHIEUSANXUAT.DataSource = dt;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = madonHang;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void grChiTietGiaoHang_Click(object sender, EventArgs e)
        {
            txtMaSanPhamChiTiet.Text = gvPhanTichTienDo.GetFocusedRowCellDisplayText(masanpham_pt);
        }

        private void btnXemBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 =
               new frmLoading(txtMaSanPhamChiTiet.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnBaoCaoChuaXuatHang_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from tblchitietkehoach where 
               soluongyc < BTPT11 or BTPT11 is null and ngaytrienkhai between 
			   '{0}' and '{1}' 
                order by ngaytrienkhai desc,IDSP asc",
                dpTu.Value.ToString("MM-dd-yyyy"),
                dpDen.Value.ToString("MM-dd-yyyy"));
            grcBaoCaoSanLuong.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
    }
}
