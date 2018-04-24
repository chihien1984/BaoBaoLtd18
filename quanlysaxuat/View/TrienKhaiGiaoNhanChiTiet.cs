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
    public partial class TrienKhaiGiaoNhanChiTiet : DevExpress.XtraEditors.XtraForm
    {
        private string point;
        public TrienKhaiGiaoNhanChiTiet(string point)
        {
            InitializeComponent();
            this.point = point;
        }
        private async void ThDuLieuGiaoHang(string query)
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = query;
                Invoke((Action)(() =>
                {
                    grTrienKhaiGiaoNhan.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        #region formload
        private void TrienKhaiGiaoNhanChiTiet_Load(object sender, EventArgs e)
        {
            lbpointsave.Text = this.point;
            if (this.point == "")
            {
                TraCuuGiaoHangTheoTo();
            }
            DocToGiaoNhanDaLapTheoMoiLanGhi();
            this.gvTrienKhaiGiaoNhan.Appearance.Row.Font = new Font("Segoe UI", 11f);
            dpTu.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            DocToNhan();

        }
       
        private void DocToNhan()
        {
            int i = 0;
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            var dt = kn.laybang(sqlQuery);
            while (i < dt.Rows.Count)
            {
                repositoryItemCbBoPhanNhan.Items.Add(dt.Rows[i]["ToThucHien"]);
                i++;
            }
        }
        #endregion
        private void DocToGiaoNhanDaLapTheoMoiLanGhi()
        {
            string sqlQuery =
               string.Format(@"select b.MaDonHang +'; '+ b.MaSanPham +'; '+
            b.TenChiTiet +'; '+ format(b.SoLuongDonHang,'#,##')+' '+b.DonViChiTiet ThongTin,
			b.SoLuongChiTietDonHang,b.TonKho,b.SoLuongYCSanXuat,a.* from
            (select PointSave,ID,IDTrienKhai,IDChiTietDonHang,
            NgayGiao,SoGiao,ToNhan,NguoiGhiGiao,NgayGhiGiao,ToThucHien
            from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
            where PointSave like '{0}')a inner join
            (select ID,ParentID,IDTrienKhai,IDDonHang,IDChiTietDonHang,
            IDChiTietSanPham,IDSanPham,MaDonHang,MaPo,MaSanPham,
            TenSanPham,MaLoai,TenLoai,SoLuongLoai,SoLuongDonHang,
            DonViSanPham,MaLapGhep,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
            SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
            TenCongDoan,DinhMuc,CongSuatChuyenNgay,SoThuTu,
            NgayLap,NguoiLap,NgayHieuChinh,CongSuatChuyenGio,NgayGiao,SoLuongGiao,''SoDaSanXuat,
            ToNhan,NgayNhan,SoNhan,HuHongThatLac,NguyenNhanSoBienBan,MucDo,DienGiaiCongDoan,
            SoLanCongDoan,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu,
            KhoGiaoHang,TinhTrang,UuTien,TinhTrangNgung,DonGiaKhoanTo,
            SoLuongTinhGiaKhoan,ThanhTienKhoanTo
            from TrienKhaiKeHoachSanXuat)b
			on a.IDTrienKhai=b.IDTrienKhai 
			order by NgayGhiGiao desc", lbpointsave.Text);
            ThDuLieuGiaoHang(sqlQuery);
            gvTrienKhaiGiaoNhan.SelectAll();
            gvTrienKhaiGiaoNhan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void btnGiaoNhanHangSanXuat_Click(object sender, EventArgs e)
        {
            DocTatCaGiaoNhan();
        }
        private void DocTatCaGiaoNhan()
        {
            string sqlQuery =
                string.Format(@"select g.ID,IDTrienKhai,g.IDChiTietDonHang,
                t.MaDonHang+'-'+t.MaSanPham ThongTin,
                ToGiao,g.NgayGiao,SoGiao,
                g.ToNhan,g.NgayNhan,g.SoNhan,
                HangLoiHu,NguoiGiao,NgayGhiGiao,NgayHieuChinhGiao,
                NguoiNhan,NgayGhiNhan,NgayHieuChinhNhan,
                TrangThaiGiao,TrangThaiNhan,
                t.TenChiTiet,
                t.SoChiTietSanPham,t.SoLuongYCSanXuat
                from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet g
                left outer join TrienKhaiKeHoachSanXuat t
                on g.IDTrienKhai=t.ID order by g.ID DESC");
            ThDuLieuGiaoHang(sqlQuery);
            gvTrienKhaiGiaoNhan.SelectAll();
        }
        private void btnTaoDuLieuGiaoNhan_Click(object sender, EventArgs e)
        {
        }

        private void gcTrienKhaiGiaoNhan_Click(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhatDuLieuGiaoNhan_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvTrienKhaiGiaoNhan.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvTrienKhaiGiaoNhan.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
                    set SoGiao = '{0}',ToNhan = N'{1}',NguoiGiao = N'{2}', NgayGhiGiao = GetDate()
                    where ID = '{3}'",
                rowData["SoGiao"],
                rowData["ToNhan"],
                Login.Username,
                rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TraCuuGiaoHangTheoTo();
        }

        private void btnXoaDuLieuGiaoNhan_Click(object sender, EventArgs e)
        {
            int[] listRowList = gvTrienKhaiGiaoNhan.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvTrienKhaiGiaoNhan.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"delete from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
                    where ID = '{0}'", rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TraCuuGiaoHangTheoTo();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (ClassUser.User == "00288" || ClassUser.User == "99999")
            {
                TraCuuQuyenAdmin();
            }
            else
            {
                TraCuuGiaoHangTheoTo();
            }
        }
        
        private void TraCuuGiaoHangTheoTo()
        {
            string sqlQuery =
                string.Format(@"select b.MaDonHang +'; '+ b.MaSanPham +'; '+
                            b.TenChiTiet +'; '+ format(b.SoLuongDonHang,'#,##')+' '+b.DonViChiTiet ThongTin,
			                b.SoLuongChiTietDonHang,b.TonKho,b.SoLuongYCSanXuat,a.* from
                            (select PointSave,ID,IDTrienKhai,IDChiTietDonHang,
                            NgayGiao,SoGiao,ToNhan,NguoiGhiGiao,NgayGhiGiao,ToThucHien
                            from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
                            where NgayGiao 
			                between '{0}' and '{1}' 
			                and ToThucHien in (select WorkLocation from tblResourcesUser where UserName like '{2}'))a inner join
                            (select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
                               MaDonHang,MaPo,MaSanPham,
                               TenSanPham,TenLoai,SoLuongDonHang,
                               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
                               SoLuongYCSanXuat,''SoDaSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
                               TenCongDoan,SoThuTu,
                               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
                               from TrienKhaiKeHoachSanXuat)b
			                on a.IDTrienKhai=b.IDTrienKhai 
			                order by NgayGhiGiao desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"),
                ClassUser.User);
            ThDuLieuGiaoHang(sqlQuery);
        }
        private void TraCuuQuyenAdmin()
        {
            string sqlQuery =
               string.Format(@"select b.MaDonHang +'; '+ b.MaSanPham +'; '+
            b.TenChiTiet +'; '+ format(b.SoLuongDonHang,'#,##')+' '+b.DonViChiTiet ThongTin,
			b.SoLuongChiTietDonHang,b.TonKho,b.SoLuongYCSanXuat,a.* from
            (select PointSave,ID,IDTrienKhai,IDChiTietDonHang,
            NgayGiao,SoGiao,ToNhan,NguoiGhiGiao,NgayGhiGiao,ToThucHien
            from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
            where NgayGiao between '{0}' and '{1}') a inner join
            (select ID,ParentID,IDTrienKhai,IDDonHang,IDChiTietDonHang,
            IDChiTietSanPham,IDSanPham,MaDonHang,MaPo,MaSanPham,
            TenSanPham,MaLoai,TenLoai,SoLuongLoai,SoLuongDonHang,
            DonViSanPham,MaLapGhep,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
            SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
            TenCongDoan,DinhMuc,CongSuatChuyenNgay,SoThuTu,
            NgayLap,NguoiLap,NgayHieuChinh,CongSuatChuyenGio,NgayGiao,SoLuongGiao,''SoDaSanXuat,
            ToNhan,NgayNhan,SoNhan,HuHongThatLac,NguyenNhanSoBienBan,MucDo,DienGiaiCongDoan,
            SoLanCongDoan,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu,
            KhoGiaoHang,TinhTrang,UuTien,TinhTrangNgung,DonGiaKhoanTo,
            SoLuongTinhGiaKhoan,ThanhTienKhoanTo
            from TrienKhaiKeHoachSanXuat)b
			on a.IDTrienKhai=b.IDTrienKhai 
			order by NgayGhiGiao desc",
               dpTu.Value.ToString("yyyy-MM-dd"),
               dpDen.Value.ToString("yyyy-MM-dd"));
            ThDuLieuGiaoHang(sqlQuery);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            grTrienKhaiGiaoNhan.ShowPrintPreview();
        }
    }
}
