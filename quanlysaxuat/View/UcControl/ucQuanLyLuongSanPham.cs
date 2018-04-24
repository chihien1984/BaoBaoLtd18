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
using DevExpress.XtraReports.UI;
using DevExpress.DemoData.Models.Vehicles;
using quanlysanxuat.View.UcControl;

namespace quanlysanxuat
{

    public partial class UCNHATKYCONGVIEC : DevExpress.XtraEditors.XtraForm
    {
        public UCNHATKYCONGVIEC()
        {
            InitializeComponent();
        }
        private string userName;
        private string role;
        private string maDonHang;
        private string maSanPham;
        private string tenQuiCach;
        private string userGroup;
        #region formload
        private void UCNHATKYCONGVIEC_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            userName = MainDev.username;
            //userGroup = MainDev.department;
            dptu_ngay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();

            dpQuanLyTuNgay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpQuanLyDenNgay.Text = dpden_ngay.Text;
            dateTimePickerTu_GiaBan.Text = dptu_ngay.Text;
            dateTimePickerDen_GiaBan.Text = dpden_ngay.Text;
            dpTraCuuTu.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dpTraCuuDen.Text = DateTime.Now.ToString();
            CbxNhanVien_Resource();
            dpBaoCaoTongTuNgay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpBaoCaoTongDenNgay.Text = dpden_ngay.Text;
            dateTimePickerTuTongHopDoanhThuBoPhan.Text = dptu_ngay.Text;
            dateTimePickerDenTongHopDoanhThuBoPhan.Text = dpden_ngay.Text;

            dpTuSoCongViec.Text = DateTime.Now.ToString();
            dpDenSoCongViec.Text = DateTime.Now.ToString();

            BoPhanGhiNhatKy();
            if (ClassUser.User == "224" || ClassUser.User == "00840"|| ClassUser.User == "99999")
            {
                QuanLyThongKe.PageVisible = true;
                DonGiaSP_TaoRa.PageVisible = true;
                doanhthubophan.PageVisible = true;
                btnQuanLySoThongKe.Visible = true;
            }
            else
            {
                QuanLyThongKe.PageVisible = false;
                DonGiaSP_TaoRa.PageVisible = true;
                doanhthubophan.PageVisible = true;
                btnQuanLySoThongKe.Visible = false;
            }
            DanhMucPhongBan();
            DonHangTrongThang();
            DoanhSoBoPhan();
            this.gvDonHangKeHoach.Appearance.Row.Font = new Font("Segoe UI", 8f);
            this.gvNhatKyCongViec.Appearance.Row.Font = new Font("Segoe UI", 8f);
            this.gvQuanLyThongKe.Appearance.Row.Font = new Font("Segoe UI", 8f);
            HienThiItemKhungThoiGianLamViec();
            HienThiItemKhungThoiGianLamViecQuanLyThongKe();
            HienThiItemKhungThoiGianLamViecNhatKy();
            DonHangSanXuat();
            TraCuuSoCongViec();
            THNoiSuDungMay();// combobox nơi sử dụng máy
            THDSMaySanXuat();//Load danh mục - sổ triển khai
            ThDMMayHieuChinh();//Danh mục máy - sổ nhật ký
            ThDSMaHieuMayTongHop();//Mã hiệu máy - sổ tổng hợp
            gvDonHangKeHoach.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvNhatKyCongViec.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvQuanLyThongKe.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #endregion
        private void HienThiItemKhungThoiGianLamViec()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditMaCa.DataSource =
                kn.laybang(@"select Ma,TenGoi,Tu,Den,HeSo
                from tblKhungThoiGianLamViec");
            repositoryItemGridLookUpEditMaCa.DisplayMember = "Ma";
            repositoryItemGridLookUpEditMaCa.ValueMember = "Ma";
            maCa_.ColumnEdit = repositoryItemGridLookUpEditMaCa;
        }
        private void HienThiItemKhungThoiGianLamViecQuanLyThongKe()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditqltkMaCa.DataSource =
                kn.laybang(@"select Ma,TenGoi,Tu,Den,HeSo
                from tblKhungThoiGianLamViec");
            repositoryItemGridLookUpEditqltkMaCa.DisplayMember = "Ma";
            repositoryItemGridLookUpEditqltkMaCa.ValueMember = "Ma";
            maCa_.ColumnEdit = repositoryItemGridLookUpEditqltkMaCa;
        }
        private void HienThiItemKhungThoiGianLamViecNhatKy()
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpEditMaCaNK.DataSource =
                kn.laybang(@"select Ma,TenGoi,Tu,Den,HeSo
                from tblKhungThoiGianLamViec");
            repositoryItemGridLookUpEditMaCaNK.DisplayMember = "Ma";
            repositoryItemGridLookUpEditMaCaNK.ValueMember = "Ma";
            maCa_.ColumnEdit = repositoryItemGridLookUpEditMaCaNK;
        }

        #region Sổ NHẬT KÝ SẢN XUẤT
        private void TraCuuSoCongViec()
        {
            ketnoi kn = new ketnoi();
            string sqlSql = string.Format(@"select MaHieuMay,
                            CONGDOAN+'; '+MASP+'; '+SANPHAM+'; '+MADH+'; '+MaPo CongViecThucHien,
                            convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU<'17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,
              * from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' and MEMBER like N'{2}' 
                order by NGAYTHUCHIEN DESC,HOTEN ASC, MATHE ASC",
                dpTuSoCongViec.Value.ToString("yyyy-MM-dd"),
                dpDenSoCongViec.Value.ToString("yyyy-MM-dd"),
                userName);
            grNhatKyCongViec.DataSource = kn.laybang(sqlSql);
            kn.dongketnoi();
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = -1;
        }
        private void btnQuanLySoThongKe_Click(object sender, EventArgs e)
        {
            TheHienSoQuanLyThongKe();
        }
        private void TheHienSoQuanLyThongKe()
        {
            ketnoi kn = new ketnoi();
            string sqlSql = string.Format(@"select 
                            MaHieuMay,CONGDOAN+'; '+MASP+'; '+SANPHAM+'; '+MADH+'; '+MaPo CongViecThucHien,
                            convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU<'17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,* from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' 
                order by NGAYTHUCHIEN DESC,HOTEN ASC, MATHE ASC",
                dpTuSoCongViec.Value.ToString("yyyy-MM-dd"),
                dpDenSoCongViec.Value.ToString("yyyy-MM-dd"));
            grNhatKyCongViec.DataSource = kn.laybang(sqlSql);
            kn.dongketnoi();
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = -1;

            //gvDonHangKeHoach.OptionsSelection.MultiSelect = true;
            //gvDonHangKeHoach.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gvDonHangKeHoach.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //gvDonHangKeHoach.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
        }
        private void BtnList_CongDoan_Click(object sender, EventArgs e)
        {
            TraCuuSoCongViec();
        }
        #endregion

        #region danh sách sau khi thêm
        private void TheHienDanhSachThem()
        {
            ketnoi kn = new ketnoi();
            string sqlSql = string.Format(@"select MaHieuMay,CONGDOAN+'; '+MASP+'; '+SANPHAM+'; '+MADH CongViecThucHien,
                            convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU<'17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,* from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' 
                and HOTEN like N'{2}' and MEMBER like N'{3}' 
                order by NGAYTHUCHIEN DESC,HOTEN ASC, MATHE ASC",
                dpTuSoCongViec.Value.ToString("yyyy-MM-dd"),
                dpDenSoCongViec.Value.ToString("yyyy-MM-dd"),
                txtHoTen.Text, userName);
            grNhatKyCongViec.DataSource = kn.laybang(sqlSql);
            kn.dongketnoi();
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = -1;
        }
        #endregion

        #region DANH SÁCH SAU KHI SỬA
        private void TheHienDanhSachSua()
        {
            ketnoi kn = new ketnoi();
            string sqlSql = string.Format(@"select 
                            MaHieuMay,CONGDOAN+'; '+MASP+'; '+SANPHAM+'; '+MADH CongViecThucHien,
                            convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU < '17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,* from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' 
                and MATHE like '{2}' 
                and HOTEN like N'{3}' 
                and MEMBER  like N'{4}'
                order by NGAYTHUCHIEN DESC,HOTEN ASC, MATHE ASC",
                dpTuSoCongViec.Value.ToString("yyyy/MM/dd"),
                dpDenSoCongViec.Value.ToString("yyyy/MM/dd"),
                gridLookUpEditMaThe.Text,
                txtHoTen.Text, userName);
            grNhatKyCongViec.DataSource = kn.laybang(sqlSql);
            kn.dongketnoi();
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = -1;
        }
        #endregion
        #region Danh sách dữ liệu công đoạn + Đơn hàng sản xuất
        private void DonHangSanXuat()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ''MaHieuMay,''MaCa,''BatDau,
			  ''KetThuc,
              ''NgayTH,''SLTH,l.id,l.DonGiaHeSo,IdPSX,
                cast(ngaytrienkhai as date)ngaytrienkhai,
                Tothuchien,madh,MaPo,mabv,Tensp,k.soluongsx,l.Macongdoan,Tencondoan,
				l.Dongia_CongDoan,l.Dinhmuc
				from (select IDChiTietDonHang IdPSX,max(MaSanPham) mabv,max(MaDonHang) madh,max(MaPo)MaPo,
				max(SoLuongYCSanXuat) soluongsx,
                min(NgayLap) ngaytrienkhai from TrienKhaiKeHoachSanXuat
                where IDChiTietDonHang is not null and MaSanPham <>'' group by IDChiTietDonHang) k
                left outer join
                tblDMuc_LaoDong l on l.Masp=k.mabv where
                ngaytrienkhai between '{0}' and '{1}'  
                and Macongdoan <> '' order by IdPSX DESC",
                dptu_ngay.Value.ToString("MM-dd-yyyy"),
                dpden_ngay.Value.ToString("MM-dd-yyyy"));
            grDonHangKeHoach.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();

        }

        private void btnloadgrid1_Click(object sender, EventArgs e)
        {
            DonHangSanXuat();
            HienThiItemKhungThoiGianLamViec();
        }
        #endregion
        #region Cập nhật Hệ số ca cho những trường hợp null
        private void CapNhatHeSoCa()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update tblNHATKYCD_
                            set HeSo=v.HeSo
				            from (select Ma,HeSo
				            from tblKhungThoiGianLamViec)v
				            where tblNHATKYCD_.MaCa=v.Ma
				            and tblNHATKYCD_.HeSo is null");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }

        #endregion
        #region Thêm
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            GhiSoNhatKyCongViec();
        }
        private void GhiSoNhatKyCongViec()
        {
            if (gridLookUpEditMaThe.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Chưa có Mã Thẻ hoặc tên để ghi"); return;
            }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvDonHangKeHoach.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDonHangKeHoach.GetDataRow(listRowList[i]);
                    if (rowData["NgayTH"].ToString() == "") { return; }
                    else
                    {
                        string strQuery = string.Format(@"insert into tblNHATKYCD_ 
                          (MADH,SOLUONG,MA_CD,CONGDOAN,DINHMUC,DONGIA,BATDAU,
                          KETTHUC,NGAYTHUCHIEN,SL_THUCHIEN,IDPSX,MEMBER,SANPHAM,
                          MATHE,HOTEN,MABP,BPGHI,MASP,
                          DONGIAAPDUNG,CongDoanID,NGAYGHI,MaCa,MaPo,MaHieuMay)
                          VALUES (N'{0}','{1}',N'{2}',N'{3}','{4}','{5}','{6}','{7}',
                          '{8}','{9}','{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',
                          N'{16}',N'{17}','{18}','{19}',GetDate(),'{20}',N'{21}',N'{22}')",
                          rowData["madh"],
                          rowData["soluongsx"],
                          rowData["Macongdoan"],
                          rowData["Tencondoan"],
                          rowData["Dinhmuc"],
                          rowData["DonGiaHeSo"],
                          rowData["BatDau"],
                          rowData["KetThuc"],
                          rowData["NgayTH"] == DBNull.Value ? ""
                            : Convert.ToDateTime(rowData["NgayTH"]).ToString("yyyy-MM-dd"),
                          rowData["SLTH"],
                          rowData["IdPSX"],
                          userName,
                          rowData["Tensp"],
                          gridLookUpEditMaThe.Text,
                          txtHoTen.Text,
                          txtMaBP.Text,
                          userGroup,
                          rowData["mabv"],
                          rowData["DonGiaHeSo"],
                          rowData["id"],
                          rowData["MaCa"],
                          rowData["MaPo"],
                          rowData["MaHieuMay"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
                CapNhatHeSoCa();//Cập nhật hệ số ca làm việc
                CapNhatTienLuongSanPham();//Cập nhật tiền lương làm việc
                CapNhatThoiGianLam();//Cập nhật thời gian làm việc
                GhiTienVuotDinhMuc();//Cập nhật tiền vượt định mức
                TheHienDanhSachThem();//Thể hiện danh sách sau khi thêm
                //DonHangSanXuat();
                //grDonHangKeHoach.check
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex, "Thông báo");
            }
        }
        #endregion
        private void CapNhatTienLuongSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery =
                string.Format(@"update tblNHATKYCD_ 
              set ThanhTien = SL_THUCHIEN*DONGIA*HeSo
              where HeSo is not null 
              and SL_THUCHIEN > 0 
              and KETTHUC>BATDAU");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        //Thời gian làm việc nếu từ 7h30 đến 16h30 thì trừ 1 tiếng giờ nghi trưa
        //Trường hợp này chỉ áp dụng đối với ca hành chánh
        private void CapNhatThoiGianLam()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery =
            string.Format(@"update tblNHATKYCD_ 
             set SoGioLamViec = p.ThoiGianLamViec from
			 (select ID,CASE WHEN BATDAU < '17:00' 
							 AND DATEPART(HH,BATDAU) < 12 
							 AND DATEPART(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-60
							 WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
							 ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC) 
                             END ThoiGianLamViec from tblNHATKYCD_)p
              where tblNHATKYCD_.ID=p.ID and HeSo is not null 
              and KETTHUC>BATDAU");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        //Tiền vượt = ((Số lượng làm ra / Số giờ) - Định mức)x Số giờ x Đơn giá x 0.2
        private void CapNhatTienVuotDinhMuc()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery =
                string.Format(@"update tblNHATKYCD_ set 
              TienVuot=((SL_THUCHIEN/(SoGioLamViec/60)-DINHMUC)*
			  (SoGioLamViec/60)*DONGIA*0.2 from tblNHATKYCD_
              where HeSo is not null 
              and SL_THUCHIEN > 0 
              and SL_THUCHIEN/(SoGioLamViec/60)-DINHMUC>0");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        private void GhiTienVuotDinhMuc()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery =
                string.Format(@"update tblNHATKYCD_ set 
              TienVuot=(SL_THUCHIEN/(SoGioLamViec/60)-DINHMUC)*
			  (SoGioLamViec/60)*DONGIA*0.2
			  where (SL_THUCHIEN/(SoGioLamViec/60)-DINHMUC)>0 
			  and TienVuot is null");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        private void CapNhatTienLuongSanPhamChinhSua()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Connect.mConnect;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            int[] listRowList = this.gvNhatKyCongViec.GetSelectedRows();
            ketnoi kn = new ketnoi();
            string sqlQuery =
                string.Format(@"update tblNHATKYCD_ set ThanhTien = SL_THUCHIEN*DONGIA*HeSo
              where HeSo is not null 
              and SL_THUCHIEN > 0 
              and KETTHUC>BATDAU");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }

        #region SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNhatKyCongViec.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNhatKyCongViec.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblNHATKYCD_ 
                        set NGAYTHUCHIEN ='{0}',BATDAU='{1}',KETTHUC='{2}',SL_THUCHIEN='{3}',MABP=N'{4}',
                        NGAYGHI=GetDate(),MaCa=N'{5}',MaHieuMay = N'{6}' 
                        WHERE ID like '{7}' AND (THUOCTINH ='' or THUOCTINH is null);
                        update tblNHATKYCD_
                            set HeSo=v.HeSo
				            from (select Ma,HeSo
				            from tblKhungThoiGianLamViec)v
				            where tblNHATKYCD_.MaCa=v.Ma
				            and tblNHATKYCD_.ID like '{7}';
                        update tblNHATKYCD_ set ThanhTien = SL_THUCHIEN*DONGIA*HeSo 
                        where tblNHATKYCD_.ID like '{7}' 
                        and (THUOCTINH ='' or THUOCTINH is null);
                        update tblNHATKYCD_ 
                        set SoGioLamViec = p.ThoiGianLamViec from
			            (select ID,CASE WHEN BATDAU<'17:00' 
						  AND DATEPART(HH,BATDAU) < 12 
						  AND DATEPART(HH,KETTHUC) > 13
                          and MaCa = 'HC'
						  THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-60
						  WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
						  ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC) 
                          END ThoiGianLamViec from tblNHATKYCD_)p
                          where tblNHATKYCD_.ID=p.ID and tblNHATKYCD_.ID like '{7}'",
                        Convert.ToDateTime(rowData["NGAYTHUCHIEN"]).ToString("yyyy-MM-dd"),
                        rowData["BATDAU"],
                        rowData["KETTHUC"],
                        rowData["SL_THUCHIEN"],
                        rowData["MABP"],
                        rowData["MaCa"],
                        rowData["MaHieuMay"],
                        rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatHeSoCa();//Cập nhật hệ số ca làm việc
                CapNhatTienLuongSanPham();//Cập nhật tiền lương làm việc
                CapNhatThoiGianLam();//Cập nhật thời gian làm việc
                CapNhatTienVuotDinhMuc();//Cập nhật tiền vượt định mức
                TheHienDanhSachSua();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        #endregion
        #region Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNhatKyCongViec.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNhatKyCongViec.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblNHATKYCD_ where ID ='{0}'
                        and (THUOCTINH ='' or THUOCTINH is null)",
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatHeSoCa();//Cập nhật hệ số ca làm việc
                CapNhatTienLuongSanPham();//Cập nhật tiền lương làm việc
                CapNhatThoiGianLam();//Cập nhật thời gian làm việc
                CapNhatTienVuotDinhMuc();//Cập nhật tiền vượt định mức
                TheHienDanhSachSua();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã chọn dòng khóa", "Thông báo");
            }
        }
        #endregion
        #region from Load
        private void DonHangTrongThang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ''MaHieuMay,''MaCa,LD.id,LD.DonGiaHeSo,IdPSX,
                convert(Date,ngaytrienkhai)ngaytrienkhai,Tothuchien,
                (convert(nvarchar,IdPSX)+':'+Tensp+N' Số lượng:'+convert(nvarchar,format(soluongsx,'#,##0'))+N'  Đơn vị') Nhom,madh,mabv,
                Tensp,KH.soluongsx,LD.Macongdoan,Tencondoan,
				LD.Dongia_CongDoan,LD.Dinhmuc,BatDau='',
				KetThuc='',NgayTH='',SLTH='' from 
                (select IdPSX,max(mabv) mabv,max(madh) madh,max(soluongsx) soluongsx,min(ngaytrienkhai) ngaytrienkhai
                from tblchitietkehoach where IdPSX is not null and mabv <>'' group by IdPSX) KH
                left outer join
                tblDMuc_LaoDong LD on LD.Masp=KH.mabv where ngaytrienkhai between 
                '{0}' and '{1}'  and Macongdoan <> '' order by IdPSX ASC)",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            grDonHangKeHoach.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            //gridView2.ExpandAllGroups();
        }
        #endregion
        #region Danh sách tổ (Bộ phận) nhập nhật ký công việc
        private void BoPhanGhiNhatKy()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select Tenpb from Admin where Taikhoan like N'" + userName + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userGroup = reader.GetString(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Message");
            };
        }
        #endregion
        #region Chọn nhân mã thẻ nhân viên
        private void CbxNhanVien_Resource()//Combobox chon ma nguon luc trong grid control
        {
            ketnoi Connect = new ketnoi();
            gridLookUpEditMaThe.Properties.DataSource = Connect.laybang(@"select Sothe,HoTen,MaBP,
                    To_bophan from tblDSNHANVIEN NV
                    left outer join
                    tblPHONGBAN PB on NV.MaBP = PB.Ma_bophan where NV.HoTen <> ''");
            gridLookUpEditMaThe.Properties.DisplayMember = "Sothe";
            gridLookUpEditMaThe.Properties.ValueMember = "Sothe";
            gridLookUpEditMaThe.Properties.NullText = null;
            Connect.dongketnoi();
        }
        #region CHỌN DANH MỤC BỘ PHẬN QUẢN LÝ CÔNG NHÂN 
        private void DanhMucPhongBan()
        {
            ketnoi Connect = new ketnoi();
            gridlookupTenBP.Properties.DataSource = Connect.laybang(@"SELECT BoPhan,MaBoPhan FROM tblPHONGBAN_TK");
            gridlookupTenBP.Properties.DisplayMember = "BoPhan";
            gridlookupTenBP.Properties.ValueMember = "BoPhan";
            gridlookupTenBP.Properties.NullText = null;
            Connect.dongketnoi();
        }
        private void txtTenBP_EditValueChanged(object sender, EventArgs e)
        {
            MaPhongBan();
        }
        private void MaPhongBan()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT BoPhan,MaBoPhan FROM tblPHONGBAN_TK WHERE BoPhan like N'" + gridlookupTenBP.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtMaBP.Text = reader.GetString(1);
                }
                con.Close();
            }
            catch { };
        }
        #endregion
        private void gridLookUpEditMaThe_EditValueChanged(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView8.GetFocusedDisplayText();
            txtHoTen.Text = gridView8.GetFocusedRowCellDisplayText(HoTen_gl);
            txtMaBP.Text = gridView8.GetFocusedRowCellDisplayText(MaBP_gl);
            //gridlookupTenBP.Text = gridView8.GetFocusedRowCellDisplayText(ToBP_gl);
        }
        #endregion
        #region thay đổi tên khi người dùng lỡ lăn chuột
        private void TextChange(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select HoTen from tblDSNHANVIEN where Sothe like '" + gridLookUpEditMaThe.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtHoTen.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        #endregion
        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (gvDonHangKeHoach.GetRowCellValue(gvDonHangKeHoach.FocusedRowHandle, gvDonHangKeHoach.Columns["Tothuchien"]) == null)
                return;
            string Gol = "";
            Gol = gvDonHangKeHoach.GetFocusedDisplayText();
            txtMasp.Text = gvDonHangKeHoach.GetFocusedRowCellDisplayText(Masp_grid2)==null?"0": gvDonHangKeHoach.GetFocusedRowCellDisplayText(Masp_grid2);
            //txtSanpham.Text = gvDonHangKeHoach.GetFocusedRowCellDisplayText(Sanpham_grid2);
            maDonHang = gvDonHangKeHoach.GetFocusedRowCellDisplayText(Madh_grid2)==null?"0": gvDonHangKeHoach.GetFocusedRowCellDisplayText(Madh_grid2);
        }

        #region Event Hot key
        private void UCNHATKYCONGVIEC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N: // Them moi key n
                    break;
                case Keys.F5:// tra cứu đơn hàng kế hoạch
                    break;
                case Keys.S: // Ghi key (ctrl + S)
                    if (e.Control)
                        btnThem.PerformClick();
                    break;
                case Keys.D://Delete (ctrl + D)
                    if (e.Control)
                        btnXoa.PerformClick();
                    break;
                case Keys.R: //Sửa (ctrl + Sửa)
                    if (e.Control)
                        btnSua.PerformClick();
                    break;
                    //case Keys.F5: // Refresh
                    //    MessageBox.Show("Làm mới");
                    //    break;
                    //case Keys.D: // Delete
                    //    if (e.Alt)
                    //        MessageBox.Show("Del");
                    //    break;
                    //case Keys.F: // Delete
                    //    if (e.Shift)
                    //        MessageBox.Show("Del");
                    //    break;
            }
            #endregion
        }
        #region  Sổ Nhật Ký Công Việc quản lý sử dụng 
        private void SoNhatKyCV_QuanLy()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select 
                            MaHieuMay,CONGDOAN,MASP,SANPHAM,MADH,SOLUONG,convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU<'17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,CASE WHEN BATDAU<'17:00' 
							 AND DATEPART(HH,BATDAU) < 12 
							 AND DATEPART(HH,KETTHUC) > 13
							 THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-60
							 WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
							 ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC) END GioLam,
              * from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' order by NGAYTHUCHIEN DESC",
                      dpQuanLyTuNgay.Value.ToString("yyyy/MM/dd"),
                      dpQuanLyDenNgay.Value.ToString("yyyy/MM/dd"));
            grQuanLyThongKe.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvQuanLyThongKe.Columns["MATHE"].GroupIndex = -1;
            this.gvQuanLyThongKe.OptionsSelection.MultiSelectMode
          = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void BtnSoNhatKyTK_Click(object sender, EventArgs e)
        {
            SoNhatKyCV_QuanLy();
            gvQuanLyThongKe.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            ThDSMaHieuMayTongHop();
        }

        #endregion
        #region GÁN THUỘC TÍNH CHỈ ĐỌC SỔ NHẬT KÝ CÔNG ĐOẠN
        private void BtnKhoaSuaXoa_Click(object sender, EventArgs e)
        {
            if (ClassUser.User != "224" || ClassUser.User != "00840"|| ClassUser.User != "99999")//Cho phép 2 quyền được gán gồm admin và quản lý thống kê
            { MessageBox.Show("Bạn không có quyền gán"); return; }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    int[] listRowList = this.gvQuanLyThongKe.GetSelectedRows();
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gvQuanLyThongKe.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblNHATKYCD_ set THUOCTINH = N'{0}',
                          NGUOIGANTTINH = N'{1}',NGAYTHUOCTINH = GetDate() where ID='{2}'",
                              "Khóa",
                              userName, rowData["ID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close(); SoNhatKyCV_QuanLy();
                }
                catch (Exception)
                {
                    MessageBox.Show("không hợp lệ", "Thông báo");
                }
            }
        }
        #endregion


        private void btnSuaQuanLyTT_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvQuanLyThongKe.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvQuanLyThongKe.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblNHATKYCD_
                        set NGAYTHUCHIEN='{0}',BATDAU='{1}',KETTHUC='{2}',
                        SL_THUCHIEN='{3}',NGUOIGANTTINH='{4}',MABP='{5}',
                        NGAYTHUOCTINH=GetDate(),MaCa=N'{6}',MaHieuMay=N'{7}'
                        WHERE ID='{8}';
                        update tblNHATKYCD_
                            set HeSo=v.HeSo
				            from (select Ma,HeSo
				            from tblKhungThoiGianLamViec)v
				            where tblNHATKYCD_.MaCa=v.Ma
				            and ID like '{8}';
                        update tblNHATKYCD_ set ThanhTien = SL_THUCHIEN*DONGIA*HeSo where ID like '{8}';
                        update tblNHATKYCD_ 
                        set SoGioLamViec = p.ThoiGianLamViec from
			            (select ID,CASE WHEN BATDAU<'17:00' 
						  AND DATEPART(HH,BATDAU) < 12 
						  AND DATEPART(HH,KETTHUC) > 13
                          and MaCa = 'HC'
						  THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-60
						  WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
						  ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC) 
                          END ThoiGianLamViec from tblNHATKYCD_)p
                        where tblNHATKYCD_.ID=p.ID and tblNHATKYCD_.ID like '{8}'",
                        rowData["NGAYTHUCHIEN"] == DBNull.Value ? ""
                        : Convert.ToDateTime(rowData["NGAYTHUCHIEN"]).ToString("yyyy-MM-dd"),
                        rowData["BATDAU"],
                        rowData["KETTHUC"],
                        rowData["SL_THUCHIEN"],
                        userName,
                        rowData["MABP"],
                        rowData["MaCa"],
                        rowData["MaHieuMay"],
                        rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatHeSoCa();//Cập nhật hệ số ca làm việc
                CapNhatTienLuongSanPham();//Cập nhật tiền lương làm việc
                CapNhatThoiGianLam();//Cập nhật thời gian làm việc
                CapNhatTienVuotDinhMuc();//Cập nhật tiền vượt định mức
                SoNhatKyCV_QuanLy();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnXoa_QLThuocTinh_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvQuanLyThongKe.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvQuanLyThongKe.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblNHATKYCD_ where ID ='{0}'",
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); SoNhatKyCV_QuanLy();
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn đã xóa quá nhiều dòng", "THÔNG BÁO");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvQuanLyThongKe.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvQuanLyThongKe.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblNHATKYCD_ set THUOCTINH='',
                          NGUOIGANTTINH=N'{1}',NGAYTHUOCTINH=GetDate() where ID = '{2}'", "Khóa",
                          userName, rowData["Num"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); SoNhatKyCV_QuanLy();
            }
            catch (Exception)
            {
                MessageBox.Show("không hợp lệ", "Thông báo");
            }
        }

        private void BtnXuatPhieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "'");
            XRCongDoan CongDoan = new XRCongDoan();
            CongDoan.DataSource = dt;
            CongDoan.DataMember = "Table";
            CongDoan.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnSoNhatKy_Click(object sender, EventArgs e)
        {
            gvNhatKyCongViec.ShowPrintPreview();
        }
        //Group
        private void btnUngroup_Click(object sender, EventArgs e)
        {
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = 0;
            gvNhatKyCongViec.ExpandAllGroups();
            this.gvNhatKyCongViec.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }
        //Group
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gvQuanLyThongKe.Columns["MATHE"].GroupIndex = 0;
            gvQuanLyThongKe.ExpandAllGroups();
            this.gvQuanLyThongKe.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }

        private void btnExportQuanLyTK_Click(object sender, EventArgs e)
        {
            gvQuanLyThongKe.ShowPrintPreview();
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            frmDanhSachNV danhSachNV = new frmDanhSachNV();
            danhSachNV.Member = userName;
            danhSachNV.ShowDialog();
            CbxNhanVien_Resource();
        }

        private void btnAdSoNhatKy_Click(object sender, EventArgs e)
        {
            QuanLySoNhatKy();
        }
        private void QuanLySoNhatKy()
        {
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"select 
                            MaHieuMay,convert(int,SL_THUCHIEN*60/
							(CASE WHEN BATDAU<'17:00'
							 AND datepart(HH,BATDAU) < 12 
							 AND datepart(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN datediff(minute,BATDAU,KETTHUC) -60
							 WHEN datediff(minute,BATDAU,KETTHUC)<1 THEN Null
							 ELSE datediff(minute,BATDAU,KETTHUC)END))SanLuongBinhQuan,* from tblNHATKYCD_ 
                where ThanhTien is not null 
                and NGAYTHUCHIEN between '{0}' and '{1}' order by NGAYTHUCHIEN DESC,HOTEN ASC, MATHE ASC",
            dptu_ngay.Value.ToString("MM-dd-yyyy"),
            dpden_ngay.Value.ToString("MM-dd-yyyy"));
            grNhatKyCongViec.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
            gvNhatKyCongViec.Columns["MATHE"].GroupIndex = -1;
            this.gvNhatKyCongViec.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gvNhatKyCongViec.OptionsSelection.CheckBoxSelectorColumnWidth = 20;//ép nhỏ column checked
        }

        private void btnTongTienNV_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select max(MEMBER)MEMBER,sum(t.TienVuot)TienVuot,
                sum(isnull(TienVuot,0)+TongTien)TongTien,
                t.MATHE,t.HOTEN,sum(TienHC)TienHC,sum(TienTC)TienTC,
                sum(TienDC1)TienDC1,sum(TienDC2)TienDC2,sum(TienCT)TienCT,sum(t.TongTien)ThanhTien,
                sum(GioHC)GioHC,sum(GioTC)GioTC,sum(GioDC1)GioDC1,
                sum(GioDC2)GioDC2,sum(GioCT)GioCT,sum(TongGioLam)TongGioLam,
                min(NGAYTHUCHIEN)TuNgay,max(NGAYTHUCHIEN)DenNgay,
                Datediff(DD,min(NGAYTHUCHIEN),max(NGAYTHUCHIEN))+1 SoNgayLam
                from
                (select 
                case when MaCa like N'HC' then ThanhTien end TienHC,
                case when MaCa like N'TC' then ThanhTien end TienTC,
                case when MaCa like N'1-DC1' then ThanhTien end TienDC1,
                case when MaCa like N'2-DC2' then ThanhTien end TienDC2,
                case when MaCa like N'CN' then ThanhTien end TienCT,
                ThanhTien TongTien,
                case when MaCa like N'HC' then SoGioLamViec/60 end GioHC,
                case when MaCa like N'TC' then SoGioLamViec/60 end GioTC,
                case when MaCa like N'1-DC1' then SoGioLamViec/60 end GioDC1,
                case when MaCa like N'2-DC2' then SoGioLamViec/60 end GioDC2,
                case when MaCa like N'CN' then SoGioLamViec/60 end GioCT,
                SoGioLamViec/60 TongGioLam,
                * from tblNHATKYCD_ where NGAYTHUCHIEN 
                between '{0}' and '{1}' and ThanhTien is not null)t
                group by t.MATHE,t.HOTEN",
                dpBaoCaoTongTuNgay.Value.ToString("yyyy-MM-dd"),
                dpBaoCaoTongDenNgay.Value.ToString("yyyy-MM-dd"));
            grTongHop.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            xtraTabControl2.SelectedTabPage = xtbTongHop;
        }
        #region Tổng thành tiền của các nhân viên thuộc bộ phận
        private void btnTongTien_BoPhan_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnDinhMuc_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTongThanhTienBPGhi_Click(object sender, EventArgs e)
        {

        }
        private string maThe;
        private string hoTen;
        private void gridControl4_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvTongHop.GetFocusedDisplayText();
            maThe = gvTongHop.GetFocusedRowCellDisplayText(mathe_tonghop);
            hoTen = gvTongHop.GetFocusedRowCellDisplayText(hoten_tonghop);
            TheHienTongHopChiTiet();
        }

        private void txtMATHE_TextChanged(object sender, EventArgs e)
        {

        }
        private void TheHienTongHopChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select MEMBER,MaCa,HeSo,
                             CONVERT(int,SL_THUCHIEN*60/(CASE WHEN BATDAU<'17:00' 
							 AND DATEPART(HH,BATDAU) < 12 
							 AND DATEPART(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-6
							 WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
							 ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC)END)) SanLuongBinhQuan,
                case when MaCa like N'HC' then ThanhTien end TienHC,
                case when MaCa like N'TC' then ThanhTien end TienTC,
                case when MaCa like N'1-DC1' then ThanhTien end TienDC1,
                case when MaCa like N'2-DC2' then ThanhTien end TienDC2,
                case when MaCa like N'CN' then ThanhTien end TienCT,  
                ThanhTien TongTien,
                case when MaCa like N'HC' then SoGioLamViec/60 end GioHC,
                case when MaCa like N'TC' then SoGioLamViec/60 end GioTC,
                case when MaCa like N'1-DC1' then SoGioLamViec/60 end GioDC1,
                case when MaCa like N'2-DC2' then SoGioLamViec/60 end GioDC2,
                case when MaCa like N'CN' then SoGioLamViec/60 end GioCT,
                SoGioLamViec/60 TongGio,
                * from tblNHATKYCD_ where NGAYTHUCHIEN 
                between '{0}' and '{1}' and
                MATHE like '{2}' order by NGAYTHUCHIEN ASC",
                     dpBaoCaoTongTuNgay.Value.ToString("yyyy-MM-dd"),
                     dpBaoCaoTongDenNgay.Value.ToString("yyyy-MM-dd"),
                     maThe);
            grTongHopChiTiet.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvTongHopChiTiet.Columns["HOTEN"].GroupIndex = -1;
        }
        private void TheHienTongHopChiTietTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select TienVuot,ThanhTien,isnull(TienVuot,0)+ThanhTien TongTien,MEMBER,MaCa,HeSo,
                             CONVERT(int,SL_THUCHIEN*60/(CASE WHEN BATDAU<'17:00' 
							 AND DATEPART(HH,BATDAU) < 12 
							 AND DATEPART(HH,KETTHUC) > 13
                             and MaCa = 'HC'
							 THEN DATEDIFF(MINUTE,BATDAU,KETTHUC)-60
							 WHEN DATEDIFF(MINUTE,BATDAU,KETTHUC)<1 THEN Null
							 ELSE DATEDIFF(MINUTE,BATDAU,KETTHUC)END)) SanLuongBinhQuan,
                case when MaCa like N'HC' or MaCa like N'0-DC0' then ThanhTien end TienHC,
                case when MaCa like N'TC' then ThanhTien end TienTC,
                case when MaCa like N'1-DC1' then ThanhTien end TienDC1,
                case when MaCa like N'2-DC2' then ThanhTien end TienDC2,
                case when MaCa like N'CN' then ThanhTien end TienCT,  
                ThanhTien TongTien,
                case when MaCa like N'HC' or MaCa like N'0-DC0' then SoGioLamViec/60 end GioHC,
                case when MaCa like N'TC' then SoGioLamViec/60 end GioTC,
                case when MaCa like N'1-DC1' then SoGioLamViec/60 end GioDC1,
                case when MaCa like N'2-DC2' then SoGioLamViec/60 end GioDC2,
                case when MaCa like N'CN' then SoGioLamViec/60 end GioCT,                
                SoGioLamViec/60 TongGio,
                * from tblNHATKYCD_ where NGAYTHUCHIEN 
                between '{0}' and '{1}' order by NGAYTHUCHIEN ASC",
                     dpBaoCaoTongTuNgay.Value.ToString("yyyy-MM-dd"),
                     dpBaoCaoTongDenNgay.Value.ToString("yyyy-MM-dd"));
            grTongHopChiTiet.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvTongHopChiTiet.Columns["HOTEN"].GroupIndex = -1;
            xtraTabControl2.SelectedTabPage = xtbChiTiet;
        }
        private void btnTraCuuChiTiet_Click(object sender, EventArgs e)
        {
            TheHienTongHopChiTietTheoNgay();
        }

        #region CẬP NHẬT ĐƠN GIÁ TÍNH DOANH THU SẢN XUẤT
        private void btnDoanhThuBoPhan_Click(object sender, EventArgs e)
        {
            SoNhatKySanXuat_DonGia();
        }
        private void SoNhatKySanXuat_DonGia()
        {
            //ketnoi kn = new ketnoi();
            //gridControl8.DataSource = kn.laybang(@"select MEMBER,NK.MA_CD,NK.CONGDOAN,
            //    NK.NGAYTHUCHIEN,NK.ID,NK.MASP,NK.SANPHAM,NK.SOLUONG,
            //    SL_THUCHIEN,DonGiaHeSo,NK.DONGIA,
            //    (DonGiaHeSo*SL_THUCHIEN)DOANHTHUBOPHAN,NK.MEMBER
            //    from tblNHATKYCD_ NK
            //    left outer join
            //    tblDMuc_LaoDong LD on NK.MA_CD=LD.Macongdoan
            //   WHERE NK.MEMBER<>'' and  NGAYTHUCHIEN between '" + dateTimePickerTu_GiaBan.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePickerDen_GiaBan.Value.ToString("yyyy/MM/dd") + "' order by NGAYTHUCHIEN asc");
            //kn.dongketnoi();
            //gridView12.ExpandAllGroups();

            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select MEMBER,NK.MA_CD,NK.CONGDOAN,
                NK.NGAYTHUCHIEN, NK.ID, NK.MASP, NK.SANPHAM, NK.SOLUONG,
                SL_THUCHIEN, DonGiaHeSo, NK.DONGIA,
                (DonGiaHeSo * SL_THUCHIEN)DOANHTHUBOPHAN, NK.MEMBER
                from tblNHATKYCD_ NK
                left outer join
                tblDMuc_LaoDong LD on NK.MA_CD = LD.Macongdoan
               WHERE NK.MEMBER <> '' and  NGAYTHUCHIEN between '{0}' and '{1}' order by NGAYTHUCHIEN asc",
                dateTimePickerTu_GiaBan.Value.ToString("yyyy/MM/dd"),
                dateTimePickerDen_GiaBan.Value.ToString("yyyy/MM/dd"));
            grBaoCaoNgay.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }
        private void DanhMucApGia()
        {
            ketnoi kn = new ketnoi();
            gridControl8.DataSource = kn.laybang("");
            kn.dongketnoi();
        }
        private void DanhMucDonGia_Cu()
        {
            //ketnoi Connect = new ketnoi();
            //txtGiaBan.Properties.DataSource = Connect.laybang(@"select CAST(thoigianthaydoi AS date) NGAYLAP,
            //MaSP,Tenquicach,dongia
            //from tblDHCT WHERE MaSP <> '' AND dongia> 0
            //ORDER BY MaSP ASC, NGAYLAP DESC");
            //txtGiaBan.Properties.DisplayMember = "dongia";
            //txtGiaBan.Properties.ValueMember = "dongia";
            //txtGiaBan.Properties.NullText = null;
            //Connect.dongketnoi();
        }

        private void gridLookUpEditXemGiaSP_EditValueChanged(object sender, EventArgs e)
        {
            string Position = "";
            Position = gridView13.GetFocusedDisplayText();
            txtGiaBan.Text = gridView13.GetFocusedRowCellDisplayText(dongia_Lookup);
        }

        private void btnGhiDuLieu_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    DataRow rowData;
            //    int[] listRowList = this.gridView12.GetSelectedRows();
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gridView12.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"update tbl11 set DonGiaHeSo='{0}'
            //             WHERE Num='{1}'",
            //             rowData["DonGiaHeSo"] =
            //             rowData["dongia"],
            //             rowData["Num"]);
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    con.Close(); SoNhatKySanXuat_DonGia();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Không hợp lệ");
            //}


        }
        #endregion

        private void btnXemBV_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnTongHopDoanhThuBoPhan_Click(object sender, EventArgs e)
        {
            DoanhSoBoPhan();
        }

        private void DoanhSoBoPhan()
        {
            ketnoi kn = new ketnoi();
            gridControl9.DataSource = kn.laybang(@"select sum(DonGiaHeSo*SL_THUCHIEN)DOANHTHUBOPHAN,NK.MEMBER
                        from tblNHATKYCD_ NK
                        left outer join
                        tblDMuc_LaoDong LD
            on NK.MA_CD=LD.Macongdoan
                        WHERE MEMBER <> '' and NK.NGAYTHUCHIEN between '" + dateTimePickerTuTongHopDoanhThuBoPhan.Value.ToString("yyyy/MM/dd") + "' and '" + dateTimePickerDenTongHopDoanhThuBoPhan.Value.ToString("yyyy/MM/dd") + "' "
               + " group by MEMBER order by DOANHTHUBOPHAN ASC");
            kn.dongketnoi();
        }
        private void btnThongKe_TungNgay_Click(object sender, EventArgs e)
        {
            TheHienBaoCaoTungNgay();
        }

        private void TheHienBaoCaoTungNgay()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select a.*,d.* from (select sum(t.TienVuot)TienVuot,
                t.NGAYTHUCHIEN,right('0000'+t.MATHE,5)MaThe,t.HOTEN,
				sum(TienHC) TienHC,sum(TienTC) TienTC,
                sum(TienDC1) TienDC1,sum(TienDC2) TienDC2,sum(TienCT)TienCT,
				sum(t.TongTien)ThanhTien,
				sum(t.TongTien+isnull(TienVuot,0)) TongTien,
                sum(GioHC)/60 GioHC,sum(GioTC)/60 GioTC,sum(GioDC1)/60 GioDC1,
                sum(GioDC2)/60 GioDC2,sum(GioCT)GioCT,
                sum(TongGioLam)/60 TongGioLam,
                Min(t.BATDAU)BATDAU,Max(KETTHUC)KETTHUC,t.BPGHI,t.MEMBER
                from
                (select 
                case when MaCa like N'HC' or MaCa like N'0-DC0' then ThanhTien end TienHC,
                case when MaCa like N'TC' then ThanhTien end TienTC,
                case when MaCa like N'1-DC1' then ThanhTien end TienDC1,
                case when MaCa like N'2-DC2' then ThanhTien end TienDC2,
                case when MaCa like N'CN' then ThanhTien end TienCT,
                ThanhTien TongTien,
                case when MaCa like N'HC' or MaCa like N'0-DC0' then SoGioLamViec end GioHC,
                case when MaCa like N'TC' then SoGioLamViec end GioTC,
                case when MaCa like N'1-DC1' then SoGioLamViec end GioDC1,
                case when MaCa like N'2-DC2' then SoGioLamViec end GioDC2,
                case when MaCa like N'CN' then SoGioLamViec end GioCT,
                SoGioLamViec TongGioLam,
                * from tblNHATKYCD_ where NGAYTHUCHIEN 
                between '{0}' and '{1}' and ThanhTien is not null)t
                group by t.MATHE,t.HOTEN,t.NGAYTHUCHIEN,t.BPGHI,t.MEMBER)a
				left outer join
				(select b.* from
				(select UserEnrollNumber from [datachamcong moi].[dbo].CheckInOut
				where TimeDate 
				between '{0}' and '{1}' 
		
				group by UserEnrollNumber)a
				left outer join
				(select right('0000'+cast(UserEnrollNumber as nvarchar),5)MaThe,TimeDate,min(TimeStr)TimeIn,
				max(TimeStr)TimeOut
				from [datachamcong moi].[dbo].CheckInOut where TimeDate 
				between '{0}' and '{1}' 

				group by UserEnrollNumber,TimeDate)b
				on a.UserEnrollNumber=b.MaThe)d
				on a.MaThe= d.MaThe and a.NGAYTHUCHIEN=d.TimeDate",
                dpBaoCaoTongTuNgay.Value.ToString("MM-dd-yyyy"),
                dpBaoCaoTongDenNgay.Value.ToString("MM-dd-yyyy"));
            grBaoCaoNgay.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
            xtraTabControl2.SelectedTabPage = xtbThongKeTheoNgay;
        }

        private void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.gvDonHangKeHoach.GetSelectedRows().Count() > 0)
            {
                btnThem.Enabled = true;
            }
            else
            {
                btnThem.Enabled = false;
            }
        }

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.gvNhatKyCongViec.GetSelectedRows().Count() > 0)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
        private void DocSoNhatKyCV_QuanLy_Xem()
        {
            ketnoi kn = new ketnoi();
            gridControl11.DataSource = kn.laybang(@"select * from LuongSanPham_func('{0}','{1}') WHERE NGAYTHUCHIEN
                      between '" + dpQuanLyTuNgay.Value.ToString("yyyy/MM/dd") + "' and '" + dpQuanLyDenNgay.Value.ToString("yyyy/MM/dd") + "' order by NGAYTHUCHIEN ASC");
            kn.dongketnoi();
        }
        private void btnTraCuuSoNhatKy_Click(object sender, EventArgs e)
        {
            DocSoNhatKyCV_QuanLy_Xem();
        }

        private void grTongHopChiTiet_Click(object sender, EventArgs e)
        {

        }
        private void btnBaoCaoThang_Click(object sender, EventArgs e)
        {
            TheHienBaoCaoThang();
        }
        private void TheHienBaoCaoThang()
        {
            this.gvBaoCaoThang.Appearance.Row.Font = new Font("Tahoma", 8f);
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Max(year(NGAYTHUCHIEN))Nam,Max(month(NGAYTHUCHIEN))Thang,
            MATHE,HOTEN,MEMBER,sum(ThanhTien)ThanhTien,
			sum(ThanhTien+isnull(TienVuot,0))TongTien,sum(TienVuot)TienVuot,
            sum(case when Day(NGAYTHUCHIEN) = 01 then SoGioLamViec/60 else 0 end) date01,
            sum(case when Day(NGAYTHUCHIEN) = 02 then SoGioLamViec/60 else 0 end) date02,
            sum(case when Day(NGAYTHUCHIEN) = 03 then SoGioLamViec/60 else 0 end) date03,
            sum(case when Day(NGAYTHUCHIEN) = 04 then SoGioLamViec/60 else 0 end) date04,
            sum(case when Day(NGAYTHUCHIEN) = 05 then SoGioLamViec/60 else 0 end) date05,
            sum(case when Day(NGAYTHUCHIEN) = 06 then SoGioLamViec/60 else 0 end) date06,
            sum(case when Day(NGAYTHUCHIEN) = 07 then SoGioLamViec/60 else 0 end) date07,
            sum(case when Day(NGAYTHUCHIEN) = 08 then SoGioLamViec/60 else 0 end) date08,
            sum(case when Day(NGAYTHUCHIEN) = 09 then SoGioLamViec/60 else 0 end) date09,
            sum(case when Day(NGAYTHUCHIEN) = 10 then SoGioLamViec/60 else 0 end) date10,
            sum(case when Day(NGAYTHUCHIEN) = 11 then SoGioLamViec/60 else 0 end) date11,
            sum(case when Day(NGAYTHUCHIEN) = 12 then SoGioLamViec/60 else 0 end) date12,
            sum(case when Day(NGAYTHUCHIEN) = 13 then SoGioLamViec/60 else 0 end) date13,
            sum(case when Day(NGAYTHUCHIEN) = 14 then SoGioLamViec/60 else 0 end) date14,
            sum(case when Day(NGAYTHUCHIEN) = 15 then SoGioLamViec/60 else 0 end) date15,
            sum(case when Day(NGAYTHUCHIEN) = 16 then SoGioLamViec/60 else 0 end) date16,
            sum(case when Day(NGAYTHUCHIEN) = 17 then SoGioLamViec/60 else 0 end) date17,
            sum(case when Day(NGAYTHUCHIEN) = 18 then SoGioLamViec/60 else 0 end) date18,
            sum(case when Day(NGAYTHUCHIEN) = 19 then SoGioLamViec/60 else 0 end) date19,
            sum(case when Day(NGAYTHUCHIEN) = 20 then SoGioLamViec/60 else 0 end) date20,
            sum(case when Day(NGAYTHUCHIEN) = 21 then SoGioLamViec/60 else 0 end) date21,
            sum(case when Day(NGAYTHUCHIEN) = 22 then SoGioLamViec/60 else 0 end) date22,
            sum(case when Day(NGAYTHUCHIEN) = 23 then SoGioLamViec/60 else 0 end) date23,
            sum(case when Day(NGAYTHUCHIEN) = 24 then SoGioLamViec/60 else 0 end) date24,
            sum(case when Day(NGAYTHUCHIEN) = 25 then SoGioLamViec/60 else 0 end) date25,
            sum(case when Day(NGAYTHUCHIEN) = 26 then SoGioLamViec/60 else 0 end) date26,
            sum(case when Day(NGAYTHUCHIEN) = 27 then SoGioLamViec/60 else 0 end) date27,
            sum(case when Day(NGAYTHUCHIEN) = 28 then SoGioLamViec/60 else 0 end) date28,
            sum(case when Day(NGAYTHUCHIEN) = 29 then SoGioLamViec/60 else 0 end) date29,
            sum(case when Day(NGAYTHUCHIEN) = 30 then SoGioLamViec/60 else 0 end) date30,
            sum(case when Day(NGAYTHUCHIEN) = 31 then SoGioLamViec/60 else 0 end) date31,
            sum(case when Day(NGAYTHUCHIEN) = 01 then ThanhTien else 0 end) salary01,
            sum(case when Day(NGAYTHUCHIEN) = 02 then ThanhTien else 0 end) salary02,
            sum(case when Day(NGAYTHUCHIEN) = 03 then ThanhTien else 0 end) salary03,
            sum(case when Day(NGAYTHUCHIEN) = 04 then ThanhTien else 0 end) salary04,
            sum(case when Day(NGAYTHUCHIEN) = 05 then ThanhTien else 0 end) salary05,
            sum(case when Day(NGAYTHUCHIEN) = 06 then ThanhTien else 0 end) salary06,
            sum(case when Day(NGAYTHUCHIEN) = 07 then ThanhTien else 0 end) salary07,
            sum(case when Day(NGAYTHUCHIEN) = 08 then ThanhTien else 0 end) salary08,
            sum(case when Day(NGAYTHUCHIEN) = 09 then ThanhTien else 0 end) salary09,
            sum(case when Day(NGAYTHUCHIEN) = 10 then ThanhTien else 0 end) salary10,
            sum(case when Day(NGAYTHUCHIEN) = 11 then ThanhTien else 0 end) salary11,
            sum(case when Day(NGAYTHUCHIEN) = 12 then ThanhTien else 0 end) salary12,
            sum(case when Day(NGAYTHUCHIEN) = 13 then ThanhTien else 0 end) salary13,
            sum(case when Day(NGAYTHUCHIEN) = 14 then ThanhTien else 0 end) salary14,
            sum(case when Day(NGAYTHUCHIEN) = 15 then ThanhTien else 0 end) salary15,
            sum(case when Day(NGAYTHUCHIEN) = 16 then ThanhTien else 0 end) salary16,
            sum(case when Day(NGAYTHUCHIEN) = 17 then ThanhTien else 0 end) salary17,
            sum(case when Day(NGAYTHUCHIEN) = 18 then ThanhTien else 0 end) salary18,
            sum(case when Day(NGAYTHUCHIEN) = 19 then ThanhTien else 0 end) salary19,
            sum(case when Day(NGAYTHUCHIEN) = 20 then ThanhTien else 0 end) salary20,
            sum(case when Day(NGAYTHUCHIEN) = 21 then ThanhTien else 0 end) salary21,
            sum(case when Day(NGAYTHUCHIEN) = 22 then ThanhTien else 0 end) salary22,
            sum(case when Day(NGAYTHUCHIEN) = 23 then ThanhTien else 0 end) salary23,
            sum(case when Day(NGAYTHUCHIEN) = 24 then ThanhTien else 0 end) salary24,
            sum(case when Day(NGAYTHUCHIEN) = 25 then ThanhTien else 0 end) salary25,
            sum(case when Day(NGAYTHUCHIEN) = 26 then ThanhTien else 0 end) salary26,
            sum(case when Day(NGAYTHUCHIEN) = 27 then ThanhTien else 0 end) salary27,
            sum(case when Day(NGAYTHUCHIEN) = 28 then ThanhTien else 0 end) salary28,
            sum(case when Day(NGAYTHUCHIEN) = 29 then ThanhTien else 0 end) salary29,
            sum(case when Day(NGAYTHUCHIEN) = 30 then ThanhTien else 0 end) salary30,
            sum(case when Day(NGAYTHUCHIEN) = 31 then ThanhTien else 0 end) salary31
            from tblNHATKYCD_ where NGAYTHUCHIEN between '{0}' and '{1}'
            group by MATHE,HOTEN,MEMBER",
            dpBaoCaoTongTuNgay.Value.ToString("yyyy-MM-dd"),
            dpBaoCaoTongDenNgay.Value.ToString("yyyy-MM-dd"));
            grBaoCaoThang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            xtraTabControl2.SelectedTabPage = xtbBaoCaoThang;
        }

        private void grNhatKyCongViec_Click(object sender, EventArgs e)
        {
            if (gvNhatKyCongViec.GetRowCellValue(gvNhatKyCongViec.FocusedRowHandle, gvNhatKyCongViec.Columns["NGAYTHUCHIEN"]) == null)
                return;
            string point = "";
            point = gvNhatKyCongViec.GetFocusedDisplayText();
            //gridLookUpEditMaThe.Text = gvNhatKyCongViec.GetFocusedRowCellDisplayText(gridColumn27);
        }

        private void btnExTongHop_Click(object sender, EventArgs e)
        {
            grTongHop.ShowPrintPreview();
        }

        private void btnExChiTiet_Click(object sender, EventArgs e)
        {
            grTongHopChiTiet.ShowPrintPreview();
        }

        private void btnExNgay_Click(object sender, EventArgs e)
        {
            grBaoCaoNgay.ShowPrintPreview();
        }

        private void btnExThang_Click(object sender, EventArgs e)
        {
            grBaoCaoThang.ShowPrintPreview();
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            //TheHienDanhSachSua();
        }

        private void btnTongHopBoPhan_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select MEMBER,sum(ThanhTien)ThanhTien,
                min(NGAYTHUCHIEN)TuNgay,Max(NGAYTHUCHIEN)DenNgay
                from tblNHATKYCD_
                where NGAYTHUCHIEN between '{0}' and '{1}'
                group by MEMBER
                order by ThanhTien DESC",
            dpBaoCaoTongTuNgay.Value.ToString("yyyy-MM-dd"),
            dpBaoCaoTongDenNgay.Value.ToString("yyyy-MM-dd"));
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            grTongLuongTo.DataSource = dt;
            xtraTabControl2.SelectedTabPage = xtraTongHopBoPhan;
        }

        private void btnTheHienTruocThang5_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ''MaCa,''BatDau,
			  ''KetThuc,
              ''NgayTH,''SLTH,l.id,l.DonGiaHeSo,IdPSX,
                cast(ngaytrienkhai as date)ngaytrienkhai,
                Tothuchien,madh,MaPo,mabv,Tensp,k.soluongsx,l.Macongdoan,Tencondoan,
				l.Dongia_CongDoan,l.Dinhmuc
				from (select IdPSX,max(mabv) mabv,max(madh) madh,max(MaPo)MaPo,
				max(soluongsx) soluongsx,
                min(ngaytrienkhai) ngaytrienkhai from tblchitietkehoach
                where IdPSX is not null and mabv <>'' group by IdPSX) k
                left outer join
                tblDMuc_LaoDong l on l.Masp=k.mabv where
                ngaytrienkhai between '{0}' and '{1}'  
                and Macongdoan <> '' order by IdPSX DESC",
                dptu_ngay.Value.ToString("MM-dd-yyyy"),
                dpden_ngay.Value.ToString("MM-dd-yyyy"));
            grDonHangKeHoach.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            //gvDonHangKeHoach.OptionsSelection.MultiSelect = true;
            //gvDonHangKeHoach.OptionsSelection.CheckBoxSelectorColumnWidth = 10;
            //gvDonHangKeHoach.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //gvDonHangKeHoach.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
        }
        private void btnDanhMucMay_Click(object sender, EventArgs e)
        {
            CongSuatMayUserControl congSuat = new CongSuatMayUserControl();
            congSuat.ShowDialog();
            THNoiSuDungMay();
        }
        private void THNoiSuDungMay()
        {
            Model.Function.ConnectSanXuat();
           string sqlQuery = string.Format(@"select NoiSuDung from DanhMucMay group by NoiSuDung");
           cbNoiSuDungMay.DataSource = Model.Function.GetDataToTable(sqlQuery);
           cbNoiSuDungMay.DisplayMember = "NoiSuDung";
           cbNoiSuDungMay.ValueMember = "NoiSuDung";
        }
        private async void THDSMaySanXuat()
        {
           string noisudung = cbNoiSuDungMay.Text;
           Model.Function.ConnectSanXuat();
           await Task.Run(() =>
           {
               string sqlQuery = string.Format(@"select ID,TenMayMocThietBi,MaCu,MaHieuMay,
	                NoiSuDung,QuanLy,NamMua,
	                TinhTrang,GhiChu from DanhMucMay where NoiSuDung like N'{0}'",
                    noisudung);
               Invoke((Action)(() =>
               {
                   repositoryItemGridLookUpEditDSMay.DataSource = null;
                   repositoryItemGridLookUpEditDSMay.DataSource = Model.Function.GetDataToTable(sqlQuery);
                   repositoryItemGridLookUpEditDSMay.DisplayMember = "MaHieuMay";
                   repositoryItemGridLookUpEditDSMay.ValueMember = "MaHieuMay";
               }));
           });
        }
        private async void ThDMMayHieuChinh()
        {
            string noisudung = cbNoiSuDungMay.Text;
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,TenMayMocThietBi,MaCu,MaHieuMay,
	                NoiSuDung,QuanLy,NamMua,
	                TinhTrang,GhiChu from DanhMucMay",
                     noisudung);
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDSMayEdit.DataSource = null;
                    repositoryItemGridLookUpEditDSMayEdit.DataSource = Model.Function.GetDataToTable(sqlQuery);
                    repositoryItemGridLookUpEditDSMayEdit.DisplayMember = "MaHieuMay";
                    repositoryItemGridLookUpEditDSMayEdit.ValueMember = "MaHieuMay";
                }));
            });
        }
        private async void ThDSMaHieuMayTongHop()
        {
            string noisudung = cbNoiSuDungMay.Text;
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,TenMayMocThietBi,MaCu,MaHieuMay,
	                NoiSuDung,QuanLy,NamMua,
	                TinhTrang,GhiChu from DanhMucMay",
                     noisudung);
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditMaHieuMay.DataSource = null;
                    repositoryItemGridLookUpEditMaHieuMay.DataSource = Model.Function.GetDataToTable(sqlQuery);
                    repositoryItemGridLookUpEditMaHieuMay.DisplayMember = "MaHieuMay";
                    repositoryItemGridLookUpEditMaHieuMay.ValueMember = "MaHieuMay";
                }));
            });
        }
        private void cbNoiSuDungMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            THDSMaySanXuat();
        }
    }
}