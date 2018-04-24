using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Persistent.BaseImpl;

namespace quanlysanxuat
{
    public partial class UCDANHMUC_DONHANG : DevExpress.XtraEditors.XtraForm
    {
        public UCDANHMUC_DONHANG()
        {
            InitializeComponent();
        }
        private string maDonHang;
        //Thể hiện đơn hàng theo tên người đăng nhập
        private void TheHienDonHangTheoNguoiDangNhap()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select Code,Ngaydh,DH.madh,LoaiDH,nvkd,Khachhang,Diachi,MaPO, 
                                  PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, 
                                  thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri 
                                  from tblDHCt group by madh) CT on DH.madh = CT.madh 
                                  where cast(Ngaydh as Date) between '{0}' and '{1}' 
                                  and nvkd like N'{2}' order by Ngaydh desc", 
                                  dptu_ngay.Value.ToString("yyyy/MM/dd"), 
                                  dpden_ngay.Value.ToString("yyyy/MM/dd"),
                                  Login.Username);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grDonHang.DataSource = dt;
            con.Close();
            xtraTabControl1.SelectedTabPage = xtraTabPageDonHang;
        }
        //thể hiện tất cả đơn hàng 
        private void TheHienTatCaDonHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"execute BaoCaoDHThanhPhamXatKho_proc 
					@min = '{0}',
					@max = '{1}'",
                                  dptu_ngay.Value.ToString("yyyy-MM-dd"),
                                  dpden_ngay.Value.ToString("yyyy-MM-dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grDonHang.DataSource = dt;
            con.Close();
            xtraTabControl1.SelectedTabPage = xtraTabPageDonHang;
        }
        //Thể hiện chi tiết đơn hàng theo tên người đăng nhập
        private void TheHienChiTietDonHangTheoNguoiDangNhap()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
                string sqlQuery = string.Format(@"execute BaoCaoDHThanhPhamXatKhoTheoNhanVien_proc 
					                @min= '{0}',
					                @max= '{1}',
					                @NhanVien= N'{2}'",
                                  dptu_ngay.Value.ToString("yyyy/MM/dd"),
                                  dpden_ngay.Value.ToString("yyyy/MM/dd"),
                                  Login.Username);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grChiTietDonHang.DataSource = dt;
            con.Close();
            gvChiTietDonHang.Columns["Code"].GroupIndex = 0;
            gvChiTietDonHang.ExpandAllGroups();
            xtraTabControl1.SelectedTabPage = xtraTabPageChiTietDonHang;
        }
        //Thể hiện chi tiết đơn hàng
        private void TheHienTatCaChiTietDonHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"execute BaoCaoDHThanhPhamXatKho_proc '{0}','{1}'",
                              dptu_ngay.Value.ToString("yyyy/MM/dd"),
                              dpden_ngay.Value.ToString("yyyy/MM/dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grChiTietDonHang.DataSource = dt;
            con.Close();
            gvChiTietDonHang.Columns["Code"].GroupIndex = 0;
            gvChiTietDonHang.ExpandAllGroups();
            xtraTabControl1.SelectedTabPage = xtraTabPageChiTietDonHang;
        }
        //formload
        private void UCDANHMUC_DONHANG_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpden_ngay.Text = DateTime.Now.ToString("yyyy-MM-dd");
            gvDonHang.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvChiTietDonHang.Appearance.Row.Font = new Font("Segoe UI", 8f);
            QuyenDocToanBoDonHang(); 
            QuyenDoanToanBoChiTietDoanhang();
            TongLaoDongCan();//Số lao động cần
        }

        private void TheHienChiTietDonHangTheoMaDonHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select * from tblDONHANG where madh like N'{0}'", maDonHang);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grChiTietDonHang.DataSource = dt;
            con.Close();
        }

        private void ExportDH_Click(object sender, EventArgs e)
        {
            grDonHang.ShowPrintPreview();
        }

        private void ExportCTDH_Click(object sender, EventArgs e)
        {
            grChiTietDonHang.ShowPrintPreview();
        }

        private void btnrefresh_DH_Click(object sender, EventArgs e)
        {
            QuyenDocToanBoDonHang();
        }
        //Nếu quyền Admin thì có thể xem tất cả
        private void QuyenDocToanBoDonHang()
        {
            if (ClassUser.User =="99999"|| ClassUser.User == "00199")
            {
                xtraTabControl1.SelectedTabPage = xtraTabPageDonHang;
                TheHienTatCaDonHang();
            }
            else
            {
                TheHienDonHangTheoNguoiDangNhap();
            }
        }
        private void QuyenDoanToanBoChiTietDoanhang()
        {
            if (ClassUser.User=="99999"||ClassUser.User== "00199")
            {
              TheHienTatCaChiTietDonHang();
                gvChiTietDonHang.Columns["Code"].GroupIndex = 0;
                gvChiTietDonHang.ExpandAllGroups();
            }
            else
            {
                TheHienChiTietDonHangTheoNguoiDangNhap();
            }
        }

        private void btnrefresh_CTDH_Click(object sender, EventArgs e)
        {
            QuyenDoanToanBoChiTietDoanhang();
        }

        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            gvChiTietDonHang.Columns["Code"].GroupIndex = -1;
        }
        
      
        private void grDonHang_Click(object sender, EventArgs e)
        {
            string point;
            point = gvDonHang.GetFocusedDisplayText();
            maDonHang = gvDonHang.GetFocusedRowCellDisplayText(Madh);
            TheHienChiTietDonHangTheoMaDonHang();
        }

        private void btnTraCuuLaoDongCan_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageChiTietNhuCauLaoDong;
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select a.MaSP,a.Tenquicach,
                (a.SoLuong*b.NhanCong)TongNC from
                (select MaSP,Tenquicach,sum(Soluong)SoLuong from tblDHCT 
                where ngaygiao 
                between '{0}' and '{1}'
                group by MaSP,Tenquicach)a
                left outer join
                (select Masp,sum(NhanCong)NhanCong
                from viewDinhMucNhanCong group by Masp)b
                on a.MaSP=b.Masp",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            grNhanSuDapUng.DataSource = Model.Function.GetDataTable(sqlQuery);
            TongLaoDongCan();
        }
        private void TongLaoDongCan()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select sum(TongNC)TongNguoi from
                (select a.MaSP,a.Tenquicach,(a.SoLuong*b.NhanCong)TongNC from
                (select MaSP,Tenquicach,sum(Soluong)SoLuong from tblDHCT 
                where ngaygiao 
                between '{0}' and '{1}'
                group by MaSP,Tenquicach)a
                left outer join
                (select Masp,sum(NhanCong)NhanCong
                from viewDinhMucNhanCong group by Masp)b
                on a.MaSP=b.Masp)c",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery,con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtTongLaoDongCanThiet.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }

        private void btnTraCuuLaoDongCanTrongLam_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageNhuCauCanTrongNam;
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select Thang,sum(TongNC)TongNguoi from
                (select Thang,a.MaSP,a.Tenquicach,(a.SoLuong*b.NhanCong)TongNC from
                (select month(ngaygiao)Thang,MaSP,Tenquicach,sum(Soluong)SoLuong
                from tblDHCT where year(ngaygiao) = year('{0}')
                and MaSP <>''
                group by month(ngaygiao),MaSP,Tenquicach)a
                left outer join
                (select Masp,sum(NhanCong)NhanCong
                from viewDinhMucNhanCong group by Masp)b
                on a.MaSP=b.Masp)c
                group by Thang",
                dptu_ngay.Value.ToString("yyyy-MM-dd"));
            grLaoDongTrongNam.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
    }
}