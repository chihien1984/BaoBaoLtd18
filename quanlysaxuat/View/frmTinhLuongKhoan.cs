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
using quanlysanxuat.Model;

namespace quanlysanxuat.View
{
    public partial class TinhLuongKhoan_uc : DevExpress.XtraEditors.XtraForm
    {
        public TinhLuongKhoan_uc()
        {
            InitializeComponent();
        }
        private string userName;
        private string departmen;
        #region formload
        private void frmTinhLuongKhoan_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            HienThiItemKhungThoiGianLamViec();
            DocDSNhanVien();//Đọc danh sách nhân viên
            //this.gridView3.Appearance.Row.Font = new Font("Tahoma", 7f);
            DocNhatKySanXuat();//Nhật ký sản xuất
            //gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            FormatTime_Star();
            FormatTime_End();
            DocDSCongDoanDonhangTrienKhai();
            //userName = Login.Username;
            //departmen = MainDev.department;
            gvDSDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;//ép nhỏ column checked
            this.gvDSDinhMucCongDoan.Appearance.Row.Font = new Font("Tahoma", 7f);
            this.gvNhatKyCongViec.Appearance.Row.Font = new Font("Tahoma", 7f);
            if (Login.role=="1")
            {
                btnTraCuNhatKyCongViec.Visible = true;
            }
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
        SANXUATDbContext db = new SANXUATDbContext();
        private void gvNhatKyCongViec_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Ma")
            {
                var value = gvNhatKyCongViec.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblKhungThoiGianLamViecs.FirstOrDefault(x => x.Ma == (string)value);
                if (dt != null)
                {
                    gvNhatKyCongViec.SetRowCellValue(e.RowHandle, "HeSo", dt.HeSo);
                }
            }
        }
        private void ShowCapNhat()
        {
            if (gvDSDinhMucCongDoan.SelectedRowsCount>=1)
            {
                btnCapNhatCongDoanLamViec.Enabled=true;
            }
            else
            {
                btnCapNhatCongDoanLamViec.Enabled = false;
            }
        }
        private void DocDSCongDoanDonhangTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select id IDCongDoan,Iden IDChiTietDonHang,madh,
				c.MaSP,c.Soluong,SoChiTiet,Dongia_CongDoan,Tencondoan,c.Tenquicach,Dinhmuc from tblDHCT c
				left outer join 
				(select id,Masp,Tencondoan,Dinhmuc,
				Dongia_CongDoan,SoChiTiet
				from tblDMuc_LaoDong where Tensp <>'' and 
				Tencondoan<>'' and Dongia_CongDoan >0)l
				on c.MaSP=l.MaSP where Dongia_CongDoan<>'' and Iden in 
                (select IdPSX from tblchitietkehoach where ngaytrienkhai 
				between '{0}' and '{1}' 
				group by IdPSX) order by IDChiTietDonHang desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grDSDinhMucCongDoan.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void btnDocDSDinhMucCongDoan_Click(object sender, EventArgs e)
        {
            DocDSCongDoanDonhangTrienKhai();
            HienThiItemKhungThoiGianLamViec();
        }
        private void FormatTime_Star()
        {
            //repositoryItemTimeEditBatDau.Mask.EditMask = "HH:mm";
            //repositoryItemTimeEditBatDau.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //repositoryItemTimeEditBatDau.Mask.UseMaskAsDisplayFormat = true;
        }

        private void FormatTime_End()
        {
            //repositoryItemTimeEditKetThuc.Mask.EditMask = "HH:mm";
            //repositoryItemTimeEditKetThuc.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //repositoryItemTimeEditKetThuc.Mask.UseMaskAsDisplayFormat = true;
        }

        private void DocDSNhanVien()//đọc danh sách nhân viên
        {
            ketnoi Connect = new ketnoi();
            lookHoTenNhanVien.Properties.DataSource = Connect.laybang(@"select HoTen,Sothe,MaBP,
                    To_bophan from tblDSNHANVIEN NV
                    left outer join
                    tblPHONGBAN PB on NV.MaBP = PB.Ma_bophan where NV.HoTen <> ''");
            lookHoTenNhanVien.Properties.DisplayMember = "HoTen";
            lookHoTenNhanVien.Properties.ValueMember = "HoTen";
            lookHoTenNhanVien.Properties.NullText = null;
            Connect.dongketnoi();
        }
 
        #region  Đọc danh sách đơn hàng
        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string point = "";
            //point = gridView1.GetFocusedDisplayText();
            //txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(Madh_col1);
            //txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(masp_col1);
            //txtTenSanPham.Text = gridView1.GetFocusedRowCellDisplayText(sanpham_col1);
            //txtSoLuong.Text = gridView1.GetFocusedRowCellDisplayText(Soluong_DH_col1);
            //txtDonVi.Text = gridView1.GetFocusedRowCellDisplayText(donvitinh_col1);
            //txtidDonHang.Text = gridView1.GetFocusedRowCellDisplayText(donHangID_col1);
        }

        #region ĐỌC CÔNG ĐOẠN SẢN PHẨM THEO ĐƠN HÀNG


        private void txtgan_hao_hut_TextChanged(object sender, EventArgs e)
        {
       
        }
        #endregion

        private void GhiCongDoan()//Ghi công đoạn sản phẩm vào hồ sơ phân bổ
        {
     //       double soluong = double.Parse(txtSoLuong.Text);
     //       SqlConnection con = new SqlConnection();
     //       con.ConnectionString = Connect.mConnect;
     //       if (con.State == ConnectionState.Closed)
     //           con.Open();
     //       DataRow rowData;
     //       //int[] listRowList = this.gridView2.GetSelectedRows();
     //       for (int i = 0; i < listRowList.Length; i++)
     //       {
     //           //rowData = this.gridView2.GetDataRow(listRowList[i]);
     //           string strQuery = string.Format(@"
				 //   insert into tblPhanBoLuong 
					//(DonHangID,
					//CongDoanID,MaSP,SoChiTiet,
					//SoLuongDH,SoLuongChiTietCongDoan,
					//DonGiaCong,ThanhTien,NguoiGhi,
					//NgayGhi) values 
				 //  ('{0}','{1}','{2}',
     //               '{3}','{4}','{5}',
					//'{6}','{7}','{8}',
					//GetDate())",
     //               txtidDonHang.Text,
     //               rowData["id"],
     //               txtMaSanPham.Text,
     //               rowData["SoChiTiet"],
     //               soluong,
     //               rowData["SoLuongChiTietCongDoan"],
     //               rowData["Dongia_CongDoan"],
     //               rowData["ThanhTien"],
     //               txtUserName.Text);
     //           SqlCommand cmd = new SqlCommand(strQuery, con);
     //           cmd.ExecuteNonQuery();
     //       }
     //       con.Close();
        }
        private void btnPhanBoLuongKhoan_Click(object sender, EventArgs e)
        {
            GhiCongDoan();
        }

        private void btnLuongPhanBo_Click(object sender, EventArgs e)
        {
            SoPhanBoLuongKhoan();
        }

        //Ghi dữ liệu từ phân bổ đơn hàng vào nhật ký công đoạn phân bổ
        private void SoPhanBoLuongKhoan()
        {
        //    double soluong = double.Parse(txtSoLuong.Text);

        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = Connect.mConnect;
        //    if (con.State == ConnectionState.Closed)
        //        con.Open();
        //    DataRow rowData;
        //    int[] listRowList = this.gridView2.GetSelectedRows();

        //    for (int i = 0; i < listRowList.Length; i++)
        //    {
        //        rowData = this.gridView2.GetDataRow(listRowList[i]);
        //        string strQuery = string.Format(@"
				    //insert into tblNHATKYCD_PhanBo 
        //            (IDCongDoan,IDDonHang,
        //            NguyenCong,TenCongDoan,
        //            NgayLap,ToThucHien,
        //            MaSP,TenSP,
        //            DonVi,DinhMuc,
        //            Dongia_CongDoan,SoChiTiet,
        //            SoLuongChiTietCongDoan,ThanhTien,
        //            TrungCongDoan,SoLuongCongDoan,NguoiGhi,MaDonHang,NgayGhi)
        //            values
        //           ('{0}','{1}',
        //            N'{2}',N'{3}',
        //            '{4}',N'{5}',
        //            N'{6}',N'{7}',
        //            N'{8}','{9}',
        //            '{10}','{11}',
        //            '{12}','{13}',
        //            '{14}','{15}','{16}',N'{17}',GetDate())",
        //            rowData["IDCongDoan"], txtidDonHang.Text,
        //            rowData["NguyenCong"], rowData["TenCD"],
        //            dpNgayLap.Value == null ? "" : dpNgayLap.Value.ToString("yyyy-MM-dd"), rowData["ToThucHien"],
        //            txtMaSanPham.Text, txtTenSanPham.Text,
        //            txtDonVi.Text, rowData["DinhMuc"],
        //            rowData["Dongia_CongDoan"], rowData["SoChiTiet"],
        //            rowData["SoLuongChiTietCongDoan"], rowData["ThanhTien"],
        //            rowData["TrungCongDoan"], soluong, txtUserName.Text,txtMadh.Text);
        //        SqlCommand cmd = new SqlCommand(strQuery, con);
        //        cmd.ExecuteNonQuery();
        //    }
        //    con.Close();
        }
        private void gridControl3_DoubleClick(object sender, EventArgs e)
        {
            AddPhanBoCongDoan();
            DocNhatKySanXuat();
        }
        private void DocNhatKySanXuat()
        {
            //ketnoi kn = new ketnoi();
            //string sqlString = string.Format(@"select IDNhatKyCongViec,IDPhanBoCongDoan,IDCongDoan,
            //        IDDonHang,NgayLam,
            //        left(cast(BatDau as time(7)),5)BatDau,
            //        left(cast(KetThuc as time(7)),5)KetThuc,
            //        MaThe,HoTen,MaDonHang,MaSanPham,SanPham,
            //        SoLuongCongDoanDonHang,
            //        NguyenCong,TenCongDoan,DinhMucCong,
            //        TienCong,SoLuongLam,SoLuongLuyKe,MaBoPhanQuanLy,
            //        ThuocTinh,TrangThai,TienHanhChinh,TienTangCa,
            //        TrangThaiCong,TienVuotNangSuat,NgayGanThuocTinh,
            //        NguoiGanThuocTinh,username,user_group,NgayGhi
            //        from tblNHATKYCD_test 
            //        where MaThe='{0}' and NgayLam='{1}' and HoTen = N'{2}'",
            //    txtSoThe.Text,
            //    dpNgayLam.Value.ToString("yyyy-MM-dd"),
            //    lookHoTenNhanVien.Text);
            //gridControl4.DataSource = kn.laybang(sqlString);
            //kn.dongketnoi();
        }
        private void AddPhanBoCongDoan()//add các công đoạn được phân bổ vào sổ nhật ký công việc
        {
        //    if (txtSoThe.Text == "")
        //    {
        //        MessageBox.Show("Chọn tên nhân viên", "Thông báo"); return;
        //    }
        //    else
        //    {
        //        SqlConnection con = new SqlConnection();
        //        con.ConnectionString = Connect.mConnect;
        //        if (con.State == ConnectionState.Closed)
        //            con.Open();
        //        double soluongChiTietDH = Double.Parse(txtChiTietDonHang.Text.ToString());
        //        double soluong = Convert.ToDouble(txtSoLuong.Text);
        //        double dinhmuc = Convert.ToDouble(txtDinhMuc.Text);
        //        double tiencong = Convert.ToDouble(txtDonGiaCong.Text);
        //        DataRow rowData;
        //        int[] listRowList = this.gridView3.GetSelectedRows();
        //        for (int i = 0; i < listRowList.Length; i++)
        //        {
        //            rowData = this.gridView3.GetDataRow(listRowList[i]);
        //            string strQuery = string.Format(@"
				    //insert into tblNHATKYCD_test(
        //            IDPhanBoCongDoan,IDCongDoan,
        //            IDDonHang,NgayLam,
        //            MaThe,HoTen,
        //            MaDonHang,MaSanPham,
        //            SanPham,SoLuongCongDoanDonHang,
        //            NguyenCong,TenCongDoan,
        //            DinhMucCong,TienCong,
        //            username,NgayGhi) values 
        //               ('{0}','{1}',
        //                '{2}','{3}',
        //                '{4}',N'{5}',
        //               N'{6}',N'{7}',
        //               N'{8}','{9}',
        //               N'{10}',N'{11}',
        //                '{12}','{13}',
        //                '{14}',GetDate())",
        //                txtidPhanBo.Text, txtidCongDoan.Text,
        //                txtidDonHang.Text, dpNgayLam.Value.ToString("yyyy-MM-dd"),
        //                txtSoThe.Text, lookHoTenNhanVien.Text,
        //                txtMadh.Text, txtMaSanPham.Text,
        //                txtTenSanPham.Text, soluongChiTietDH,
        //                txtNguyenCong.Text, txtTenCongDoan.Text,
        //                dinhmuc, tiencong,
        //                txtUserName.Text);
        //            SqlCommand cmd = new SqlCommand(strQuery, con);
        //            cmd.ExecuteNonQuery();
        //        }
        //        con.Close();
        //    }
        }
        //Ghi công việc vào sổ nhật ký công việc
        private void gridControl3_Click(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView8.GetFocusedDisplayText();
            //txtDinhMuc.Text = gridView3.GetFocusedRowCellDisplayText(dinhMuc_gr3);
            //txtDonGiaCong.Text = gridView3.GetFocusedRowCellDisplayText(dongiaCongDoan_gr3);
            //txtTenCongDoan.Text = gridView3.GetFocusedRowCellDisplayText(tenCongDoan_gr3);
            //txtidDonHang.Text = gridView3.GetFocusedRowCellDisplayText(idDonHang_gr3);
            //txtidCongDoan.Text = gridView3.GetFocusedRowCellDisplayText(idCongDoan_gr3);
            //dpNgayLapPhanBo.Text = gridView3.GetFocusedRowCellDisplayText(ngayGhi_gr3);
            //txtToThucHien.Text = gridView3.GetFocusedRowCellDisplayText(toThucHien_gr3);
            //txtMaSanPham.Text = gridView3.GetFocusedRowCellDisplayText(maSanPham_gr3);
            //txtTenSanPham.Text = gridView3.GetFocusedRowCellDisplayText(tenSanPham_gr3);
            //txtDonVi.Text = gridView3.GetFocusedRowCellDisplayText(donvi_gr3);
            //txtSoChiTiet.Text = gridView3.GetFocusedRowCellDisplayText(soChiTiet_gr3);
            //txtChiTietDonHang.Text = gridView3.GetFocusedRowCellDisplayText(chiTietDonHang_gr3);
            //txtSoLuong.Text = gridView3.GetFocusedRowCellDisplayText(soLuongCongDoan_gr3);
            //txtNguyenCong.Text = gridView3.GetFocusedRowCellDisplayText(nguyenCong_gr3);
            //txtidPhanBo.Text = gridView3.GetFocusedRowCellDisplayText(idPhanBo_gr3);
            //txtMadh.Text = gridView3.GetFocusedRowCellDisplayText(maDH_gr3);
        }

        private void lookDanhSachNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView8.GetFocusedDisplayText();
            //txtMaTo.Text = gridView8.GetFocusedRowCellDisplayText(maTo_lk);
            //txtTenTo.Text = gridView8.GetFocusedRowCellDisplayText(tenTo_lk);
            //txtSoThe.Text = gridView8.GetFocusedRowCellDisplayText(soThe_lk);
        }

        //Đọc bản phân bổ để ghi sổ thống kê cho nhân viên
        private void btnDocDSPhanBoLuong_Click(object sender, EventArgs e)
        {
       
        }

        private void btnGhiNhatKy_Click(object sender, EventArgs e)
        {

        }

        private void grDSDinhMucCongDoan_DoubleClick(object sender, EventArgs e)
        {
            GhiTungDong();
        }

        private void grDSDinhMucCongDoan_Click(object sender, EventArgs e)
        {
            BinDingDonHangTrienKhaiCongDoan();
            //GhiTungDong();
        }
        private string idChiTietDonHang;
        private string idCongDoan;
        private string maDonHang;
        private string maSanPham;
        private string soLuong;
        private string soChiTiet;
        private string donGiaCongDoan;
        private string tenCongDoan;
        private string tenQuiCach;
        private string idTrienKhai;
        private string dinhMucCongDoan;
        private void BinDingDonHangTrienKhaiCongDoan()
        {
            string point = "";
            point = gvDSDinhMucCongDoan.GetFocusedDisplayText();
            idChiTietDonHang = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(idchitietdonhang_gr);
            idCongDoan = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(idcongdoan_gr); 
            maDonHang = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(madonhang_gr);
            txtMaSanPham.Text= gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(masanpham_gr);
            maSanPham = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(masanpham_gr); 
            soLuong = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(soluongsanxuat_gr);
            soChiTiet = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(sochitiet_gr);
            donGiaCongDoan = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(dongia_gr);
            tenCongDoan = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(congdoan_gr); 
            tenQuiCach = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(tenquicach_gr);
            txtTenSanPham.Text= gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(tenquicach_gr);
            idTrienKhai = gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(idTrienKhai_gr);
            dinhMucCongDoan= gvDSDinhMucCongDoan.GetFocusedRowCellDisplayText(dinhmuc_gr);
        }
        private void GhiTungDong()
        {
            if (txtSoThe.Text=="")
            {
                MessageBox.Show("Mã thẻ không được trống", "Message");return;
            }
            else
            {
                ketnoi kn = new ketnoi();
                string sqlQuery = string.Format(@"insert into tblNHATKYCD_test (NgayLam,MaThe,
				    HoTen,MaDonHang,
				    MaSanPham,SanPham,
				    TenCongDoan,DinhMucCong,
				    TienCong,IDTrienKhai,
                    IDCongDoan,IDChiTietDonHang)
				    values('{0}',N'{1}',
				    N'{2}',N'{3}',
				    N'{4}',N'{5}',
				    N'{6}',N'{7}',
				    N'{8}','{9}',
                     '{10}','{11}')",
                    dpNgayLam.Value.ToString("yyyy-MM-dd"),txtSoThe.Text,
                    lookHoTenNhanVien.Text,maDonHang,
                    maSanPham,tenQuiCach,
                    tenCongDoan,dinhMucCongDoan,
                    donGiaCongDoan,idTrienKhai, idCongDoan,idChiTietDonHang);
                int kq = kn.xulydulieu(sqlQuery);
                if (kq>0)
                {
                    MessageBox.Show("không thành công","Message");
                }
                kn.dongketnoi();
                DocNhatKyCongViec();
            }
        }

        
        private void GhiNhieuDong()
        {

        }

        private void grDSDinhMucCongDoan_MouseMove(object sender, MouseEventArgs e)
        {
            ShowCapNhat();
        }

        private void btnTraCuNhatKyCongViec_Click(object sender, EventArgs e)
        {
            DocTatCaDSNhanVien();
        }

        private void DocNhatKyCongViec()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from tblNHATKYCD_test where HoTen 
				like N'{0}' and NgayLam like '{1}' 
				order by ID Desc", lookHoTenNhanVien.Text,
                dpNgayLam.Value.ToString("yyyy-MM-dd"));
            grNhatKyCongViec.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvNhatKyCongViec.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void DocTatCaDSNhanVien()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select N'Công việc:'+TenCongDoan+N'; QC:'
				+SanPham+'('+MaSanPham+')'+N'; Mã đơn:'+MaDonHang Tasked,* from tblNHATKYCD_test 
                where NgayLam between '{0}' and '{1}'
				order by ID Desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grNhatKyCongViec.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvNhatKyCongViec.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void lookHoTenNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView8.GetFocusedDisplayText();
            txtSoThe.Text = gridView8.GetFocusedRowCellDisplayText(soThe_lk);
            txtBoPhan.Text = gridView8.GetFocusedRowCellDisplayText(maTo_lk);
        }

        private void btnCapNhatSoLuong_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gvNhatKyCongViec.GetSelectedRows();
            if (listRowList.Count() < 1)
            {
                MessageBox.Show("Chưa check", "Thông báo");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNhatKyCongViec.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"update tblNHATKYCD_test 
                          set BatDau='{0}',
                          KetThuc='{1}',
                          SoLuongLam='{2}',
                          MaCa='{3}'
                          where ID ='{4}';
                        update tblNHATKYCD_test
                        set HeSo=v.HeSo
				        from (select Ma,HeSo
				        from tblKhungThoiGianLamViec)v
				        where tblNHATKYCD_test.MaCa=v.Ma
				        and tblNHATKYCD_test.ID='{4}'",
                    rowData["BatDau"],
                    rowData["KetThuc"],
                    rowData["SoLuongLam"],
                    rowData["MaCa"],
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocNhatKyCongViec();
            }
        }

        private void btnCapNhatCongDoanLamViec_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gvNhatKyCongViec.GetSelectedRows();
            if (listRowList.Count() < 1)
            {
                MessageBox.Show("Chưa check", "Thông báo");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNhatKyCongViec.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into tblNHATKYCD_test (NgayLam,MaThe,
				        HoTen,MaDonHang,
				        MaSanPham,SanPham,
				        TenCongDoan,DinhMucCong,
				        TienCong,IDTrienKhai,
                        IDCongDoan,IDChiTietDonHang)
				        values('{0}',N'{1}',
				        N'{2}',N'{3}',
				        N'{4}',N'{5}',
				        N'{6}',N'{7}',
				        N'{8}','{9}',
                         '{10}','{11}')",
                dpNgayLam.Value.ToString("yyyy-MM-dd"), 
                txtSoThe.Text,
                lookHoTenNhanVien.Text, 
                rowData["madh"],
                rowData["MaSP"], 
                rowData["Tenquicach"],
                rowData["Tencondoan"],
                rowData["Dinhmuc"],
                rowData["Dongia_CongDoan"],
                rowData["IDChiTietDonHang"],
                rowData["IDCongDoan"],
                rowData["IDChiTietDonHang"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocNhatKyCongViec();
            }
        }

        private void grNhatKyCongViec_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvNhatKyCongViec.SelectedRowsCount>0)
            {
                btnCapNhatSoLuong.Enabled = true;
            }
            else
            {
                btnCapNhatSoLuong.Enabled = false;
            }
        }
    }
}