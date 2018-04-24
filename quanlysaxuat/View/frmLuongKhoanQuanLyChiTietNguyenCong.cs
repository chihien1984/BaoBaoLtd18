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
    public partial class frmLuongKhoanQuanLyChiTietNguyenCong : Form
    {
        public frmLuongKhoanQuanLyChiTietNguyenCong()
        {
            InitializeComponent();
        }
        string idDonHang;
        string idKhoanSoLuongLam;
        string idChiTietLuongKhoan;
        string maDonHang;string maSanPham;string sanPham;
        string soLuong;string nguyenCong;
        string congDoan;string soLuongCongDoan;string donGiaQuanLy;
        private void gridControl1_Click(object sender, EventArgs e)
        {
            string point = "";
             point = gridView1.GetFocusedDisplayText();
             this.idDonHang= gridView1.GetFocusedRowCellDisplayText(idDonHang_col1);
            this.idChiTietLuongKhoan= gridView1.GetFocusedRowCellDisplayText(idGiaChiTietQuanLy_col1);
            this.maDonHang= gridView1.GetFocusedRowCellDisplayText(maDonHang_col1);
            this.maSanPham= gridView1.GetFocusedRowCellDisplayText(idMaSanPham_col1);
            this.sanPham= gridView1.GetFocusedRowCellDisplayText(sanpham_col1);
            this.soLuong= gridView1.GetFocusedRowCellDisplayText(soluong_col1);
            this.congDoan= gridView1.GetFocusedRowCellDisplayText(congdoan_col1);
            this.nguyenCong= gridView1.GetFocusedRowCellDisplayText(nguyenCong_col1);
            this.soLuongCongDoan= gridView1.GetFocusedRowCellDisplayText(soluong_col1);
            this.donGiaQuanLy= gridView1.GetFocusedRowCellDisplayText(donGiaQuanLy_col1);
        }
        private void DocKhoanLuongQuanLyChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select T.*,C.madh MaDonHang,
                    C.Soluong*T.SoChiTiet SoLuongCongDoan
                    from KhoanLuongQuanLy_ChiTiet T
                    left outer join tblDHCT C on
                    T.IDDonHang=C.Iden");
            gridControl1.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void DocDSKhoanLuongQuanLySoLuongLam()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from KhoanLuongQuanLy_SoLuongLam");
            gridControl2.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void frmLuongKhoanQuanLyChiTietNguyenCong_Load(object sender, EventArgs e)
        {
            DocKhoanLuongQuanLyChiTiet();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DocKhoanLuongQuanLyChiTiet();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            KhoanLuongQuanLy_SoLuongLam();
        }
        private void KhoanLuongQuanLy_SoLuongLam()
        {
            //int[] listRowList = this.gridView2.GetSelectedRows();
            //if (listRowList.Count() < 1)
            //{
            //    MessageBox.Show("Chưa check", "Thông báo"); return;
            //}
            //else
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    DataRow rowData;
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gridView2.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"insert into KhoanLuongQuanLy_SoLuongLam
            //            (IDDonHang,IDChiTietLuongKhoan,
            //            MaDonHang,MaSanPham,
            //            SanPham,SoLuong,
            //            NguyenCong,CongDoan,
            //            SoLuongCongDoan,NgayLam,
            //            SoLuongLam,DonGia,
            //            ThanhTien,BoPhanThucHien,
            //            NguoiGhi,NgayGhi)
            //            values('{0}','{1}',
            //            N'{2}',N'{3}',
            //            N'{4}','{5}',
            //            N'{6}',N'{7}',
            //            '{8}','{9}',
            //            '{10}','{11}',
            //            '{12}','{13}',
            //            '{14}','{15}'))",
            //        this.idDonHang,this.idChiTietLuongKhoan,
            //        this.maDonHang,this.maSanPham,
            //        this.sanPham,this.soLuong,
            //        this.nguyenCong,this.congDoan,
            //        this.soLuongCongDoan,dpNgayGhi,
            //        txtSoLuongLam.Text,this.donGiaQuanLy
            //        );
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    con.Close();
            //    DocDSKhoanLuongQuanLySoLuongLam();
            //}
        }

        private void btnGhiSoLuongHoanThanh_Click(object sender, EventArgs e)
        {
            KhoanLuongQuanLy_SoLuongLam();
        }
    }
}
