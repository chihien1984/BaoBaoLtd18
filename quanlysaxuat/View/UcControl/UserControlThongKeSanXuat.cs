using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlysanxuat.Model;

namespace quanlysanxuat.View.UcControl
{
    public partial class UserControlThongKeSanXuat : DevExpress.XtraEditors.XtraForm
    {
        public UserControlThongKeSanXuat()
        {
            InitializeComponent();
        }
        private void UserControlThongKeSanXuat_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dpTu1.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen1.Text = DateTime.Now.ToString("dd-MM-yyyy");
            THThongKe();
        }
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            THThongKe();
        }
        private async void THThongKe()
        {
            await Task.Run(() =>
            {
                Function.ConnectSanXuat();
                string sqlQuery = string.Format(@"select ToThucHien,
                sum(case when TinhTrang = N'Quá hạn' then 1 end) QuaHan,
                sum(case when TinhTrang = N'Hoàn thành' then 1 end) HoanThanh,
                sum(case when TinhTrang = N'Chưa khởi động' then 1 end) ChuaKhoiDong,
                sum(case when TinhTrang = N'Đang thực hiện' then 1 end) DangThucHien,
                sum(case when TinhTrang<>'' then 1 end)TongNhan
                from viewPhanTichTTTrienKhai
                where NgayLap between '{0}' and '{1}'
                and ToThucHien 
                in ('Cat-tole','Dong-goi','Dap-la-khoan','Han')
                group by ToThucHien",
                      dpTu.Value.ToString("yyyy-MM-dd"),
                      dpDen.Value.ToString("yyyy-MM-dd"));
                      Invoke((Action)(() => {grThongKe.DataSource = Function.GetDataTable(sqlQuery);}));
            });
        }

        private void btnExportTongHop_Click(object sender, EventArgs e)
        {
            grThongKe.ShowPrintPreview();
        }

        private void btnTraCuuChiTiet_Click(object sender, EventArgs e)
        {
            THTraCuuChiTiet();
        }
        private async void THTraCuuChiTiet()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ToThucHien,TinhTrang Nhom,TinhTrang,MaDonHang,
                    MaSanPham,TenSanPham,
                    SoLuongDonHang,BatDau,
                    KetThuc,NgoaiQuan,GhiChu,
                    NgayGiao,SoLuongGiao
                    from viewPhanTichTTTrienKhai 
                    where NgayLap 
                    between '{0}' and '{1}' and ToThucHien 
                    in ('Cat-tole','Dong-goi','Dap-la-khoan','Han')",
                      dpTu1.Value.ToString("yyyy-MM-dd"),
                      dpDen1.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => { grChiTiet.DataSource = Function.GetDataTable(sqlQuery); }));
            });
            gvChiTiet.Columns["Nhom"].GroupIndex = 0;
            gvChiTiet.ExpandAllGroups();
        }
        private string toThucHien;
        private void grThongKe_Click(object sender, EventArgs e)
        {
            string point;
            point = gvThongKe.GetFocusedDisplayText();
             toThucHien = gvThongKe.GetFocusedRowCellDisplayText(tothuchien_thongke);
            THTraCuuChiTietTheoTo();
        }
        private async void THTraCuuChiTietTheoTo()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ToThucHien,TinhTrang Nhom,TinhTrang,MaDonHang,
                    MaSanPham,TenSanPham,
                    SoLuongDonHang,BatDau,
                    KetThuc,NgoaiQuan,GhiChu,
                    NgayGiao,SoLuongGiao
                    from viewPhanTichTTTrienKhai 
                    where NgayLap 
                    between '{0}' and '{1}' and ToThucHien like N'{2}'",
                      dpTu.Value.ToString("yyyy-MM-dd"),
                      dpDen.Value.ToString("yyyy-MM-dd"),
                      toThucHien);
                Invoke((Action)(() => { grChiTiet.DataSource = Function.GetDataTable(sqlQuery); }));
            });
            gvChiTiet.Columns["Nhom"].GroupIndex = 0;
            gvChiTiet.ExpandAllGroups();
        }
    }
}
