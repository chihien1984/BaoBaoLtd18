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

namespace quanlysanxuat
{
    public partial class frmTinh_DinhMuc_Thuong : DevExpress.XtraEditors.XtraForm
    {
        public frmTinh_DinhMuc_Thuong()
        {
            InitializeComponent();
        }
        private void UcTinh_DinhMuc_Thuong_Load(object sender, EventArgs e)
        {
            txtMember.Text = Login.Username;
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            dpTienThuongTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpTienThuongDen.Text = DateTime.Now.ToString();
            DanhSachSanPham();
            DocDSDonHangXuat_TheoNgay();
            TongHop();
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView3.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            btnCapNhat_DonGia_Thuong.Enabled = false;
            btnxoa_capnhat_dongia_thuong.Enabled = false;
            btnBoTrungCongDoan.Enabled = false;
            btnSua_Bo_Phan.Enabled = false;
            DSToThucHien();
        }
        void DanhSachSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"
                select Code,S.Masp,Tensp,
                DonGiaThuong,NguoiLapDGThuong,
				NgayLapDGThuong,DonGiaThuong,L.Masp DinhMuc from tblSANPHAM S left outer join
				(select Masp from tblDMuc_LaoDong group by Masp) L
				on S.Masp=L.Masp order by Code DESC");
            kn.dongketnoi();
        }
        void DanhSachSanPhamTheoTen(string maSanPham)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"
                select Code,S.Masp,Tensp,
                DonGiaThuong,NguoiLapDGThuong,
				NgayLapDGThuong,DonGiaThuong,L.Masp DinhMuc from tblSANPHAM S left outer join
				(select Masp from tblDMuc_LaoDong group by Masp) L
				on S.Masp=L.Masp where S.Masp='" + maSanPham + "' order by Code DESC");
            kn.dongketnoi();
        }
        void DanhSachCongDoan()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"
                select NguyenCong,Dongia_CongDoan,id,Masp,Tensp,Tencondoan,Dongia_CongDoan*SoChiTiet Dongia,
                DonGiaThuongPhanBo,BoPhanThucHien,TrungCongDoan,SoChiTiet from 
                tblDMuc_LaoDong");
            kn.dongketnoi();
        }
        void DSToThucHien()
        {
            ketnoi Connect = new ketnoi();
            repositoryItemToThucHien.DataSource = Connect.laybang(@"
                select BoPhan  from tblPHONGBAN_TK");
            repositoryItemToThucHien.DisplayMember = "BoPhan";
            repositoryItemToThucHien.ValueMember = "BoPhan";
            repositoryItemToThucHien.NullText = null;
            Connect.dongketnoi();
        }
        void DocDSCongDoan_TheoTen(string Masp)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"
               select NguyenCong,Dongia_CongDoan,NguyenCong,SoChiTiet,id,Masp,Tensp,Tencondoan,
               Dongia_CongDoan*SoChiTiet Dongia,DonGiaThuongPhanBo,
               BoPhanThucHien,TrungCongDoan from tblDMuc_LaoDong where
               Masp='" + Masp + "'");
            kn.dongketnoi();
        }
        private void btnCapNhat_DonGia_Thuong_Click(object sender, EventArgs e)
        {
            if (this.codinhMuc == "")
            { MessageBox.Show("Sản phẩm này chưa có định mức công,\n không thể cập nhật giá,\n không thể phân bổ", "Liên hệ A.Được"); return; }
            int[] listRowList = this.gridView3.GetSelectedRows();
            if (listRowList.Count() < 1) { MessageBox.Show("Cần chọn trước khi cập nhật", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSANPHAM set DonGiaThuong='{0}',
                        NguoiLapDGThuong=N'{1}',NgayLapDGThuong=GetDate() where Code='{2}'",
                        rowData["DonGiaThuong"],
                        txtMember.Text,
                        rowData["Code"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                PhanBoDonGiaThuong();
                DanhSachSanPhamTheoTen(txtMasp.Text);
                DocDSCongDoan_TheoTen(txtMasp.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        #region phân bổ đơn giá thưởng vào danh mục định mức công đoạn
        private void PhanBoDonGiaThuong()
        {
            if (txtMasp.Text == "")
            { MessageBox.Show("Mã sản phẩm không được rỗng"); return; }
            else
            {
                string nguoiDung = txtMember.Text;
                try
                {
                    ketnoi kn = new ketnoi();
                    string sqlStr =string.Format(@"update tblDMuc_LaoDong set DonGiaThuongPhanBo=P.DG_ThuongPhanBo, 
                   NguoiGhiDGThuong=N'{0}',NgayGhiDGThuong=Getdate()
                   from 
				   (select id,
                   (Dongia_CongDoan*SoChiTiet*DonGiaThuong/TongDG_Cong) DG_ThuongPhanBo from
                    (select SoChiTiet,id,Masp,Dongia_CongDoan*SoChiTiet Dongia,
                    Dongia_CongDoan from tblDMuc_LaoDong where 
					Dongia_CongDoan >0 and (TrungCongDoan ='' or TrungCongDoan is null))L
				    left outer join
                    (select Masp,sum(Dongia_CongDoan*SoChiTiet)TongDG_Cong from tblDMuc_LaoDong
				    where TrungCongDoan ='' or TrungCongDoan is null group by Masp)G
                    on L.Masp=G.Masp
                    left outer join tblSANPHAM S 
                    on G.Masp=S.Masp where DonGiaThuong !='')P
				    where tblDMuc_LaoDong.id=P.id", txtMember.Text);
                    gridControl1.DataSource = kn.xulydulieu(sqlStr);
                    kn.dongketnoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
                }
            }
        }
        #endregion

        private void btnxoa_capnhat_dongia_thuong_Click(object sender, EventArgs e)
        {
            //this.gridView3.OptionsSelection.MultiSelectMode
            //= DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect.;
            int[] listRowList = this.gridView3.GetSelectedRows();
            if (listRowList.Count() > 1) { MessageBox.Show("Chỉ được xóa từng dòng", "Thông báo");return; }
            if (listRowList.Count() <1) { MessageBox.Show("Cần chọn trước khi xóa", "Thông báo");return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
               
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSANPHAM set DonGiaThuong='0',
                        NguoiLapDGThuong=N'{0}',NgayLapDGThuong=GetDate() where Code='{1}'",
                        txtMember.Text,
                        rowData["Code"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                XoaDulieuPhanBo();
                DanhSachSanPhamTheoTen(txtMasp.Text);
                DocDSCongDoan_TheoTen(txtMasp.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        void XoaDulieuPhanBo()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.xulydulieu(
                   @"update tblDMuc_LaoDong set DonGiaThuongPhanBo='0' 
				   where Masp='"+txtMasp.Text+"'");
            kn.dongketnoi();
        }
        private void btnDoc_DanhSach_SanPham_Click(object sender, EventArgs e)
        {
            DanhSachSanPham();
        }

        private void btnDoc_DanhSach_CongDoan_Click(object sender, EventArgs e)
        {
            DanhSachCongDoan();
        }
        #region Cập nhật đơn giá phân bổ
        private void btncap_nhat_thuong_ds_xuat_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("update tbl11 set DonGiaThuong=S.DonGiaThuong,"+
               " TienThuongXuatKho=(ISNULL(BTPT11,0)*S.DonGiaThuong), "+
               " NguoiGhiDGThuong=N'"+txtMember.Text+"', "+
               " NgayGhiDGThuong=GetDate() "+
               " from ( select Masp,DonGiaThuong from tblSANPHAM)S " +
               " where tbl11.mabv=S.Masp and cast(ngaynhan as date) between '" + dptu_ngay.Value.ToString("MM/dd/yyyy") + "' and '" + dpden_ngay.Value.ToString("MM/dd/yyyy") + "'");
            kn.dongketnoi();
            DocDSDonHangXuat_TheoNgay();
            TongHop();
        }
        #endregion
        void DocDSDonHangXuat_TheoNgay()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select T.*,P.DonGiaThuong DaCoGia from
                (select min(MaGH)MaGH,cast(min(ngaynhan)as date)ngaynhan,madh,mabv,max(chitietsanpham)sanpham,
                max(soluongsx)soluongsx,
                sum(BTPT11)BTPT11,sum(BTPT11*DonGiaThuong)TienThuongXuatKho,max(DonGiaThuong)DonGiaThuong
                from tbl11 where madh !='' and mabv <>'' group by madh,mabv)T
				left outer join tblSANPHAM P on T.mabv=P.Masp where cast(ngaynhan as date) between '" + dptu_ngay.Value.ToString("MM/dd/yyyy") + "' and '" + dpden_ngay.Value.ToString("MM/dd/yyyy") + "'");
            kn.dongketnoi();
        }
        void DocDSDonHangXuat_MaSP_TrongThang()
        {
            
        }
        void DocTatCaDSDonHangXuat()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select T.*,P.DonGiaThuong DaCoGia from
                (select min(MaGH)MaGH,cast(min(ngaynhan)as date)ngaynhan,madh,mabv,max(chitietsanpham)sanpham,
                max(soluongsx)soluongsx,
                sum(BTPT11)BTPT11,sum(BTPT11*DonGiaThuong)TienThuongXuatKho,max(DonGiaThuong)DonGiaThuong
                from tbl11 where madh !='' and mabv <>'' group by madh,mabv)T
				left outer join tblSANPHAM P on T.mabv=P.Masp");
            kn.dongketnoi();
        }
        public string codinhMuc;
        private void gridControl3_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView3.GetFocusedDisplayText();
            txtMasp.Text = gridView3.GetFocusedRowCellDisplayText(masp_col3);
            txttensanpham.Text = gridView3.GetFocusedRowCellDisplayText(tensanpham_col3);
            DocDSCongDoan_TheoTen(gridView3.GetFocusedRowCellDisplayText(coDinhMuc_col3));
            this.codinhMuc= gridView3.GetFocusedRowCellDisplayText(coDinhMuc_col3);
            ShowButtonSave_Edit();
        }
        void ShowButtonSave_Edit()
        {
            if (gridView3.GetSelectedRows().Count() > 0)
            {
                btnCapNhat_DonGia_Thuong.Enabled = true;
                btnxoa_capnhat_dongia_thuong.Enabled = true;
            }
            else
            {
                btnCapNhat_DonGia_Thuong.Enabled = false;
                btnxoa_capnhat_dongia_thuong.Enabled = false;
            };
            if (gridView1.GetSelectedRows().Count() > 0)
            {
                btnBoTrungCongDoan.Enabled = true;
                btnSua_Bo_Phan.Enabled = true;
            }
            else
            {
                btnBoTrungCongDoan.Enabled = false;
                btnSua_Bo_Phan.Enabled = false;
            };
        }

        public string maDonHang;
        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView2.GetFocusedDisplayText();
            txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(masp_col2);
            txttensanpham.Text = gridView2.GetFocusedRowCellDisplayText(tensanpham_col2);
            DocDSCongDoan_TheoTen(txtMasp.Text);
            DanhSachSanPhamTheoTen(txtMasp.Text);
            DocTatCa_ChiTiet_SoLuongXuat_MaSP();
            this.maDonHang= gridView2.GetFocusedRowCellDisplayText(madh_col2);
        }

        private void btndoc_tatca_ds_xuat(object sender, EventArgs e)
        {
            DocTatCaDSDonHangXuat();
        }

        private void btnDocDSDonHangXuat_TheoNgay_Click(object sender, EventArgs e)
        {
            DocDSDonHangXuat_TheoNgay();
        }
        void DocTatCa_ChiTiet_SoLuongXuat_TheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select L.NguyenCong,D.*,L.Tencondoan,L.Macongdoan,
               L.DonGiaThuongPhanBo,Dongia_CongDoan,DonGia,L.BoPhanThucHien, 
               (L.DonGiaThuongPhanBo * D.BTPT11)TienThuongXuatKhoCD 
			   from 
				(select T.*,P.DonGiaThuong DaCoGia from
                (select min(MaGH)MaGH,cast(min(ngaynhan)as date)ngaynhan,madh,mabv,max(chitietsanpham)sanpham,
                max(soluongsx)soluongsx,
                sum(BTPT11)BTPT11,sum(BTPT11*DonGiaThuong)TienThuongXuatKho,max(DonGiaThuong)DonGiaThuong
                from tbl11 where madh !='' and mabv <>'' group by madh,mabv)T
				left outer join tblSANPHAM P on T.mabv=P.Masp where cast(ngaynhan as date) 
				between '{0}' and '{1}')D left outer join 
                (select NguyenCong,DonGiaThuongPhanBo,Masp,Tencondoan,Macongdoan,
                Dongia_CongDoan*SoChiTiet DonGia ,Dongia_CongDoan,BoPhanThucHien
                from tblDmuc_LaoDong)L on D.mabv=L.Masp",
                dptu_ngay.Value.ToString("MM/dd/yyyy"),
                dpden_ngay.Value.ToString("MM/dd/yyyy"));
                gridControl4.DataSource = kn.laybang(sqlQuery);
                kn.dongketnoi();
        }
        void DocTatCa_ChiTiet_SoLuongXuat_MaSP()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select T.*,L.NguyenCong,L.Tencondoan,L.Macongdoan, " +
               "  L.DonGiaThuongPhanBo,L.Dongia_CongDoan*L.SoChiTiet Dongia_CongDoan,L.BoPhanThucHien, " +
               " (L.DonGiaThuongPhanBo*T.BTPT11)TienThuongXuatKhoCD " +
               " from (select min(MaGH)MaGH,cast(min(ngaynhan)as date)ngaynhan,madh,mabv, " +
               " max(chitietsanpham)sanpham, " +
               " max(soluongsx)soluongsx, " +
               " sum(BTPT11)BTPT11,min(DonGiaThuong)DonGiaThuong from tbl11 where cast(ngaynhan as Date) " +
               " between '" + dptu_ngay.Value.ToString("MM/dd/yyyy") + "' " +
               " and '" + dpden_ngay.Value.ToString("MM/dd/yyyy") + "' and madh !='' " +
               " and mabv <>'' and DonGiaThuong >0 " +
               " group by madh,mabv)T left outer join " +
               " tblDmuc_LaoDong L  on L.Masp=T.mabv " +
               " where L.DonGiaThuongPhanBo !='' and T.madh='"+maDonHang+"' and T.mabv='"+txtMasp.Text+"'");
            kn.dongketnoi();
        }
        void DocTatCa_ChiTiet_SoLuongXuat_TheoDonhang()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select T.*,L.NguyenCong,L.Tencondoan, " +
               " L.DonGiaThuongPhanBo,L.Dongia_CongDoan,L.BoPhanThucHien, " +
               " (L.DonGiaThuongPhanBo*T.BTPT11)TienThuongXuatKho " +
               " from (select min(MaGH)MaGH,cast(min(ngaynhan)as date)ngaynhan,madh,mabv, " +
               " max(chitietsanpham)sanpham, " +
               " max(soluongsx)soluongsx, " +
               " sum(BTPT11)BTPT11,min(DonGiaThuong)DonGiaThuong from tbl11 where cast(ngaynhan as Date)ngaynhan " +
               " between '" + dptu_ngay.Value.ToString("MM/dd/yyyy") + "' and '" + dpden_ngay.Value.ToString("MM/dd/yyyy") + "' and madh !='' " +
               " and mabv <>'' " +
               " group by madh,mabv)T left outer join " +
               " tblDmuc_LaoDong L  on L.Masp=T.mabv " +
               " where L.DonGiaThuongPhanBo !='' and Masp='"+txtMasp.Text+"' ");
            kn.dongketnoi();
        }
        private void btnDocTatCa_ChiTiet_SoLuongXuat_TheoNgay_Click(object sender, EventArgs e)
        {
            DocTatCa_ChiTiet_SoLuongXuat_TheoNgay();
        }


        void DocTatCa_ChiTiet_SoLuongXuat()
        {
            
        }
     

        private void btnDocTatCa_ChiTiet_SoLuongXuat_Click(object sender, EventArgs e)
        {

        }
        void TongHop()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from TongTienVaDonHangTre_func('{0}','{1}')",
                dpTienThuongTu.Value.ToString("MM/dd/yyyy"),
                dpTienThuongDen.Value.ToString("MM/dd/yyyy"));
            gridControl5.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void btnTongHop_TienThuong_Click(object sender, EventArgs e)
        {
            TongHop();
        }

        private void btnDonGiaCongDoanThuong_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnExDonHangXuat_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void btnExDonGiaThuong_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }
        private void btnExprtTongHop_Click(object sender, EventArgs e)
        {
            gridControl5.ShowPrintPreview();
        }

        private void btnBoTrungCongDoan_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView1.GetSelectedRows();
            if (listRowList.Count() < 1) { MessageBox.Show("Cần tích chọn", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set TrungCongDoan=N'{0}'
			            where id='{1}'", 
                        rowData["TrungCongDoan"],
                        rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                XoaDulieuPhanBo();
                PhanBoDonGiaThuong();
                DanhSachSanPhamTheoTen(txtMasp.Text);
                DocDSCongDoan_TheoTen(txtMasp.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView1.GetFocusedDisplayText();
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(maSanPham_col1);
        
        }

        private void btnMaSP_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnSua_Bo_Phan_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView1.GetSelectedRows();
            if (listRowList.Count() < 1) { MessageBox.Show("Cần tích chọn", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set BoPhanThucHien=N'{0}'
			            where id='{1}'",
                        rowData["BoPhanThucHien"],
                        rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSCongDoan_TheoTen(txtMasp.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void gridControl3_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButtonSave_Edit();
        }

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButtonSave_Edit();
        }
    }
}
