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
{ public delegate void ClickMe(string message);
    public partial class TrienKhaiKeHoachSanXuatGiaoNhan_UC : UserControl
    {
       

        public TrienKhaiKeHoachSanXuatGiaoNhan_UC()
        {
            InitializeComponent();
        }
        private string boPhanDangNhap;
        private void frmNhanKeHoachSanXuat_Load(object sender, EventArgs e)
        {
            boPhanDangNhap = MainDev.department;
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            DocDSDonHangTrienKhaiTheoThoiGian();
            this.gvGiaoHang.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.gvNhanHang.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.gvTrienKhai.Appearance.Row.Font = new Font("Times New Roman", 7f);
        }
        private void DocSoLuongGiaoHangTheoIDTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"");
            gcGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void DocSoLuongNhanHangTheoIDTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@" SELECT ID,IDTrienKhai,
                 IDChiTietDonHang,ToGiao,NgayGiao,
                 SoGiao,ToNhan,NgayNhan,SoNhan,HangLoiHu,NguoiGiao,
                 NgayGhiGiao,NgayHieuChinhGiao,NguoiNhan,NgayGhiNhan,
                 NgayHieuChinhNhan,TrangThaiGiao,TrangThaiNhan
                 FROM TrienKhaiKeHoachSanXuatGiaoNhanChiTiet where 
                 IDTrienKhai like '{0}'",txtIDTrienKhai.Text);
            gcNhanHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void ShowDonHangNhan()
        {
            if (Login.role=="1"||Login.role=="1039")
            {
                DocTatCaDonHangTrienKhaiTheoThoiGian();
            }
            else
            {
                DocDSDonHangTrienKhaiTheoThoiGian();
            }
        }
        private void DocDSDonHangTrienKhaiTheoThoiGian()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"SELECT MaLapGhep,ID,MaDonHang,MaSanPham,
                    TenSanPham,SoDonHang,DonViSanPham,
                    MaChiTiet,TenChiTiet,SoChiTietSanPham,
                    DonViChiTiet,SoLuongChiTietDonHang,
                    TonKho,SoLuongYCSanXuat,IDDonHang,
                    IDChiTietDonHang,IDChiTietSanPham,
                    ToThucHien,BatDau,KetThuc,
                    MaCongDoan,TenCongDoan,DinhMuc,
                    CongSuatChuyenNgay,SoThuTu,NgayLap,
                    NguoiLap,NgayHieuChinh,CongSuatChuyenGio,
                    NgayGiao,SoLuongGiao,ToNhan,
                    NgayNhan,SoNhan,HuHongThatLac,
                    NguyenNhanSoBienBan
                    FROM TrienKhaiKeHoachSanXuat where NgayLap
                    between '{0}' and '{1}' and ToThucHien like N'{2}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), 
                boPhanDangNhap);
            gcTrienKhai.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void DocTatCaDonHangTrienKhaiTheoThoiGian()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"SELECT MaLapGhep,ID,MaDonHang,MaSanPham,
                    TenSanPham,SoDonHang,DonViSanPham,
                    MaChiTiet,TenChiTiet,SoChiTietSanPham,
                    DonViChiTiet,SoLuongChiTietDonHang,
                    TonKho,SoLuongYCSanXuat,IDDonHang,
                    IDChiTietDonHang,IDChiTietSanPham,
                    ToThucHien,BatDau,KetThuc,
                    MaCongDoan,TenCongDoan,DinhMuc,
                    CongSuatChuyenNgay,SoThuTu,NgayLap,
                    NguoiLap,NgayHieuChinh,CongSuatChuyenGio,
                    NgayGiao,SoLuongGiao,ToNhan,
                    NgayNhan,SoNhan,HuHongThatLac,
                    NguyenNhanSoBienBan
                    FROM TrienKhaiKeHoachSanXuat where NgayLap
                    between '{0}' and '{1}' ",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            gcTrienKhai.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void btnKetHoachSanXuat_Click(object sender, EventArgs e)
        {
            ShowDonHangNhan();
        }
     
        //public static string IDChiTietDonHang;
        //public static string IDTrienKhai;
        
        #region goi giao dien nhập chi tiết giao hàng
        private void GoiGiaoDienNhanHangTheoMaToaGiao()
        {
            TrienKhaiGiaoNhanChiTiet giaonhan = new TrienKhaiGiaoNhanChiTiet(lbpointsave.Text);
            giaonhan.ShowDialog();
            UpdateSoLuongGiaoChiTietTrienKhaiKeHoach();
            DocSoLuongChiTietGiaoHang();
            DocSoLuongChiTietNhanHang();
            ShowDonHangNhan();
        }
 
        //Xoa đi các chi tiết toa giao nêu người dùng không cập nhật số lượng kèm theo
        private void XoaToaKhongCoSoLuong()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"delete from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where PointSave  like N'{0}' 
				and (SoGiao ='' or SoGiao is null)", lbpointsave.Text);
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        #endregion
        private void gcTrienKhai_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvTrienKhai.GetFocusedDisplayText();
            txtIDTrienKhai.Text = gvTrienKhai.GetFocusedRowCellDisplayText(idtrienkhaikehoach);
            txtIDChiTietDonHang.Text= gvTrienKhai.GetFocusedRowCellDisplayText(idChiTietDonHang);
            txtMaSanPham.Text= gvTrienKhai.GetFocusedRowCellDisplayText(masanpham);
            txtSanPham.Text= gvTrienKhai.GetFocusedRowCellDisplayText(tensanpham);
            DocSoLuongChiTietGiaoHang();
            DocSoLuongChiTietNhanHang();
        }

        private void ckCheckKeHoachSanXuat_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCheckKeHoachSanXuat.Checked==true)
            {
                gvTrienKhai.OptionsSelection.MultiSelectMode
                    = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            }
            else if(ckCheckKeHoachSanXuat.Checked == false)
            {
                gvTrienKhai.OptionsSelection.MultiSelectMode
                  = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            }
            gvTrienKhai.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

      
        private void btnTaoDuLieuGiaoNhan_Click(object sender, EventArgs e)
        {
            TaoMaToaGiaoNhanHang();//tạo mã toa giao hàng vào Table
            int[] listRowList = gvTrienKhai.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvTrienKhai.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
                    (IDTrienKhai,IDChiTietDonHang,ToGiao,NgayGiao,PointSave) values 
                    ('{0}','{1}',N'{2}','{3}','{4}')",
                 rowData["ID"],rowData["IDChiTietDonHang"],
                 rowData["ToThucHien"],dpNgayGhi.Value.ToString("yyyy-MM-dd"), lbpointsave.Text);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            GoiGiaoDienNhanHangTheoMaToaGiao();//goi giao dien nhập chi tiết giao hàng
        }
        #region Tạo mã toa giao hàng
        private void TaoMaToaGiaoNhanHang()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed) 
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                lbpointsave.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        #endregion
        #region Đọc chi tiết sổ giao hàng - sổ nhận hàng
        private void DocSoLuongChiTietNhanHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from 
                TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai like '{0}' and SoGiao>0 order by ID DESC", txtIDTrienKhai.Text);
            gcNhanHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void DocSoLuongChiTietGiaoHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from 
                TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai like '{0}' and SoGiao>0 order by ID DESC", txtIDTrienKhai.Text);
            gcGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        #endregion

        private void btnGhiSoLuongNhanHang_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvNhanHang.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvNhanHang.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet set
                        SoNhan='{0}',NguoiGhiNhan = N'{1}',NgayGhiNhan = GETDATE(),HangLoiHu = '{2}',
                        NguoiNhan=N'{3}' where ID='{4}'",
                        rowData["SoNhan"],
                        Login.Username,
                        rowData["HangLoiHu"],
                        MainDev.department,
                        rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            UpdateSoLuongNhanChiTietTrienKhaiKeHoach();//cập nhật số lượng nhận chi tiết vào triển khai lịch sản xuất
            con.Close();
            DocSoLuongChiTietGiaoHang();
            DocSoLuongChiTietNhanHang();
            ShowDonHangNhan();
        }
        #region Cập nhật số lượng từ số giao hàng chi tiết vào số sổ triển khai 
        private void UpdateSoLuongNhanChiTietTrienKhaiKeHoach()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat set SoNhan=t.SoNhan,
                NgayNhan=t.NgayNhan,ToNhan=t.ToNhan,HuHongThatLac=HangLoiHu
                from(select IDTrienKhai,Sum(SoNhan) SoNhan,max(NgayNhan)NgayNhan,
                max(ToNhan)ToNhan,sum(HangLoiHu)HangLoiHu
                from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet where SoNhan >0
                group by IDTrienKhai)t
                where TrienKhaiKeHoachSanXuat.ID=t.IDTrienKhai");
            var dt = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        #endregion
        #region Cập nhật số lượng từ số giao hàng chi tiết vào số sổ triển khai 
        private void UpdateSoLuongGiaoChiTietTrienKhaiKeHoach()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat 
                set SoLuongGiao=t.SoGiao,NgayGiao=t.NgayGiao
                from(select IDTrienKhai,Sum(SoGiao) SoGiao,max(NgayGiao)NgayGiao
                from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet where SoNhan >0
                group by IDTrienKhai)t
                where TrienKhaiKeHoachSanXuat.ID=t.IDTrienKhai");
            var dt = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        #endregion

        private void btnXoaSoLuongNhanHang_Click(object sender, EventArgs e)
        {}

        private void gcNhanHang_MouseMove(object sender, MouseEventArgs e)
        {
            gvNhanHang.OptionsSelection.MultiSelectMode
                    = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvNhanHang.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void btnSoChiTietChuaNhan_Click(object sender, EventArgs e)
        {}
    }
}