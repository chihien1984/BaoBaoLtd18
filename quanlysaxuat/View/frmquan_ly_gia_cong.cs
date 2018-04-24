using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Report;
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
using quanlysanxuat.Report;

namespace quanlysanxuat.View
{
    public partial class frmquan_ly_gia_cong : Form
    {
        public frmquan_ly_gia_cong()
        {
            InitializeComponent();
           
        }
        private void QuyenTaoYeuCau()
        {
            if (Login.role == "1" || Login.role == "1039")
            {
                btncap_nhat_yeu_cau_GC.Visible = true;
                btnSua_yeu_cau_gia_cong.Visible = true;
                btnXoa_yeu_cau_gia_cong.Visible = true;
               
            }
            
        }
        private void QuyenLapPhieuXuat()
        {
            if (Login.role == "1"|| Login.role == "22" || Login.role == "15")
            { 
            btnGhiPhieuXuat.Visible = true;
            btnSuaPhieuXuat.Visible = true;
            btnXoaPhieuXuat.Visible = true;
            btnGhiNhapKho_GC.Visible = true;
            btnXoa_NhapKho_GC.Visible = true;
            btnCapNhat_NhapKho_GC.Visible = true;
            btnGhiGiaCongNoiBo.Visible = true;
            }
        }
        #region form load
        private void frmquan_ly_gia_cong_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            txtnguoi_dung.Text = Login.Username;
            dpBatDau.Text = DateTime.Today.ToString("01/MM/yyyy");
            dpKetThuc.Text = DateTime.Today.ToString("dd/MM/yyyy");
            TraCuuDonHangTrienKhai();
            TraCuuYeuCauGiaCong();
            TraCuuYeuCauGiaCong_SoLuongXuat();
            TraCuuSoGiaCong();
            TraCuuDanhSachYeuCauGC();
            TraCuuDanhSachGiaCongNhap();
            QuyenTaoYeuCau();
            QuyenLapPhieuXuat();
            TaoMoiYeuCauGiaCong();
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            grvso_yc_gia_cong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            grvycgia_cong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView5.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            DocDSDonViGiaCong();
            DonHangTrienKhaiGiaCongNoiBo();
            gridView3.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            ThongKeGiaCong();
            this.gridView3.Appearance.Row.Font = new Font("Times New Roman", 8f);
            this.gridView4.Appearance.Row.Font = new Font("Times New Roman", 8f);
            this.gridView5.Appearance.Row.Font = new Font("Times New Roman", 8f);
        }
        #endregion

        #region YÊU CẦU GIA CÔNG
        private void LoadItemChiTietGiaCong()
        {
            repositoryItemCbLinhKienGiaCong.Items.Clear();
            repositoryItemCbLinhKienGiaCong.NullText = @"Nhập chi tiết GC vào đây";
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select TenPhukien from tblPHUKIEN_SP where MaSP=N'" + txtMaSP.Text + "' order by Masp ASC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemCbLinhKienGiaCong.Items.Add(dt.Rows[i]["TenPhukien"]);
            }
            kn.dongketnoi();
        }
        private void DocDSDonViGiaCong()
        {
            ketnoi kn = new ketnoi();
            cbMaDonViGC.Properties.DataSource = kn.laybang(@"Select Id,MaDVGC,TenDVGC,DiaChi,
                    Sodienthoai,fax,usercreate,datecreate
                    from tblDS_GIACONG");
            cbMaDonViGC.Properties.ValueMember = "MaDVGC";
            cbMaDonViGC.Properties.DisplayMember = "MaDVGC";
            kn.dongketnoi();
        }
        #region Tạo mã yêu cầu
        private void MaYeuCauGiaCong()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"	DECLARE @d DATE= GetDate()
                SELECT 'YC'+''+Right(CONVERT(nvarchar(10), @d, 112),6)+'-'+
                replace(left(CONVERT(time, GetDate()),5),':','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtma_yeu_cau.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btnma_yeu_cau_Click(object sender, EventArgs e)
        {
            MaYeuCauGiaCong();
        }
        #endregion

        #region Tra cứu đơn hàng
        private void TraCuuDonHangTrienKhai()
        {
            ketnoi kn = new ketnoi();
            var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
            var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
            var strQuery = string.Format(@"select D.Masp TrangThai, madh, C.Masp,
                Tenquicach, dvt, Soluong, ngaygiao,
                Khachhang, Mau_banve, Tonkho, ghichu,
                ngoaiquang, pheduyet, Diengiai, nguoithaydoi, Iden, 
                TrangThai from tblDHCT C
                left outer join(select distinct(Masp) from tblDMuc_LaoDong) D
                on D.MaSP = C.MaSP
                where cast(thoigianthaydoi as Date)
                between '{0}' and '{1}' order by Code Desc", batDau, ketThuc);
            grdanh_muc_don_hang.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void btnTraCuuDonHang_Click(object sender, EventArgs e)
        {
            TraCuuDonHangTrienKhai();
        }
        #endregion

        #region Tạo mới yêu cầu
        private void TaoMoiYeuCauGiaCong()
        {
            ketnoi kn = new ketnoi();
            gryc_gia_cong.DataSource = kn.laybang(@"select Top 0 NgayDi,LinhKienGiaCong='',
                SoLuongYCGC='',DonVi='',NoiDung='',cast(NgayYeuCau as Date)NgayYeuCau
                from tblYeuCauGiaCong");
            kn.dongketnoi();
        }
        #endregion

        #region Cập nhật
        private void CapNhatYeuCauGiaCong()
        {
            if (txtma_yeu_cau.Text == "")
            { MessageBox.Show("Mã phiếu không hợp lệ", "Thông báo"); return; }
            else
            {
                int[] listRowList = this.grvycgia_cong.GetSelectedRows();
                if (listRowList.Count() > 0)
                    try
                    {
                        var ngayLap = dpngay_lap.Value.ToString("yyyy-MM-dd");
                        double soLuongDH = Convert.ToDouble(txtSoLuongDH.Text);
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = Connect.mConnect;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        DataRow rowData;
                        for (int i = 0; i < listRowList.Length; i++)
                        {
                            rowData = this.grvycgia_cong.GetDataRow(listRowList[i]);
                            string strQuery = string.Format(@"
                                insert tblYeuCauGiaCong(
                                NgayLap,MaYeuCau,DonHangID,MaDonHang,
                                MaSanPham,SanPham,SoLuongDH,LinhKienGiaCong,
                                SoLuongYCGC,DonVi,NoiDung,NgayYeuCau,
                                NguoiLap,NgoaiQuan,NgayDi,KhachHang,NgayGhi)
                                values('{0}',N'{1}','{2}',N'{3}',
                                N'{4}',N'{5}','{6}',N'{7}',
                                '{8}',N'{9}',N'{10}','{11}',
                                N'{12}',N'{13}','{14}',N'{15}',GetDate())",
                              ngayLap, txtma_yeu_cau.Text, txtDonHangID.Text, txtma_don_hang.Text,
                              txtMaSP.Text, txtSanPham.Text, soLuongDH, rowData["LinhKienGiaCong"],
                              rowData["SoLuongYCGC"], rowData["DonVi"], rowData["NoiDung"],
                              rowData["NgayYeuCau"] == DBNull.Value ? "" :
                              Convert.ToDateTime(rowData["NgayYeuCau"]).ToString("yyyy-MM-dd"),
                              txtnguoi_dung.Text,txtNgoaiQuan.Text,
                              rowData["NgayDi"] == DBNull.Value ? "" :
                              Convert.ToDateTime(rowData["NgayDi"]).ToString("yyyy-MM-dd"),txtKhachHang.Text);
                            SqlCommand cmd = new SqlCommand(strQuery, con);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        TraCuuYeuCauGiaCong();
                        MessageBox.Show("Success", "Message");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("reason: " + ex, "Error message");
                    }
            }
        }
        #endregion

        #region sửa
        #endregion

        #region xóa
        #endregion

        private void grdanh_muc_don_hang_Click(object sender, EventArgs e)
        {
            string point = "";
            point = grvdanh_muc_dh.GetFocusedDisplayText();
            txtma_don_hang.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(Madh_col1);
            txtMaSP.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(masp_col1);
            txtSanPham.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(sanpham_col1);
            txtSoLuongDH.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(Soluong_DH_col1);
            txtDonHangID.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(donHangID_col1);
            txtNgoaiQuan.Text= grvdanh_muc_dh.GetFocusedRowCellDisplayText(ngoaiquang_col1);
            txtKhachHang.Text = grvdanh_muc_dh.GetFocusedRowCellDisplayText(khachhang_col1);
         
            LoadItemChiTietGiaCong();
        }

        private void btncap_nhat_yeu_cau_GC_Click(object sender, EventArgs e)
        {
            CapNhatYeuCauGiaCong();
        }


        #region Cập nhật
        private void btnSua_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.grvso_yc_gia_cong.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.grvso_yc_gia_cong.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(
                         @"update tblYeuCauGiaCong set
                            LinhKienGiaCong=N'{0}',
                            SoLuongYCGC='{1}',DonVi=N'{2}',NoiDung=N'{3}',NgayYeuCau='{4}',NgoaiQuan=N'{5}',NgayDi='{6}',
                            NgayGhi=GetDate() where YeuCauGiaCongID='{7}'",
                         rowData["LinhKienGiaCong"], rowData["SoLuongYCGC"], rowData["DonVi"],
                         rowData["NoiDung"],
                         rowData["NgayYeuCau"] == DBNull.Value ? "" :
                         Convert.ToDateTime(rowData["NgayYeuCau"]).ToString("MM-dd-yyyy"), 
                         rowData["NgoaiQuan"],
                         rowData["NgayDi"]==DBNull.Value?"":
                         Convert.ToDateTime(rowData["NgayDi"]).ToString("MM-dd-yyyy"),
                         rowData["YeuCauGiaCongID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    TraCuuYeuCauGiaCong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }
        #endregion
        private void btnXoa_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.grvso_yc_gia_cong.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.grvso_yc_gia_cong.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"
                            update tblYeuCauGiaCong set
                            TrangThai='X' where YeuCauGiaCongID='{0}'",
                            rowData["YeuCauGiaCongID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    TraCuuYeuCauGiaCong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }
        private void TraCuuYeuCauGiaCong()
        {
            var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
            var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
            ketnoi kn = new ketnoi();
            var strQuery = string.Format(@"select KhachHang,NgayDi,NgoaiQuan,YeuCauGiaCongID,NgayLap,MaYeuCau,
                    DonHangID,MaDonHang,MaSanPham,
                    SanPham,SoLuongDH,LinhKienGiaCong,
                    SoLuongYCGC,DonVi,NoiDung,NgayYeuCau,NgayGhi,
                    NguoiLap,TrangThai from tblYeuCauGiaCong where TrangThai is null and 
                    NgayLap between '{0}' and '{1}'", batDau, ketThuc);
            grso_yc_gia_cong.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void btnTraCuuYeuCau_Click(object sender, EventArgs e)
        {
            TraCuuYeuCauGiaCong();
        }
        private void grso_yc_gia_cong_Click(object sender, EventArgs e)
        {
            string Point = "";
            Point = grvso_yc_gia_cong.GetFocusedDisplayText();
        }

        #endregion

        #region TẠO PHIẾU XUẤT
        private void TaoMaPhieuXuat()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"DECLARE @d DATE= GetDate()
                SELECT 'PXGC'+': '+Right(CONVERT(nvarchar(10), @d, 112),6)+'-'+
                replace(left(CONVERT(time, GetDate()),5),':','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuXuat.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void TaoMaPhieuGiaCongNoiBo()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect; ;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"DECLARE @d DATE= GetDate()
                SELECT 'PXKH'+': '+Right(CONVERT(nvarchar(10), @d, 112),6)+'-'+
                replace(left(CONVERT(time, GetDate()),5),':','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaGiaCongNoiBo.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btnLayMaXuat_Click(object sender, EventArgs e)
        {
            TaoMaPhieuXuat();
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {

        }

        private void TraCuuYeuCauGiaCong_SoLuongXuat()
        {
            var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
            var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
            ketnoi kn = new ketnoi();
            var strQuery = string.Format(@"select y.SoLuongYCGC-isnull(x.SoLuongGC,0) ConLai,y.* from
					(select KhachHang,NgoaiQuan,
                    Soxuat=0,''SoQuiDoi,1.00 TyLeQuiDoi,''DonViQuiDoi,
					NgayDi,MaYeuCau,YeuCauGiaCongID,NgayLap,
                    DonHangID,MaDonHang,MaSanPham,
                    SanPham,SoLuongDH,LinhKienGiaCong,
                    SoLuongYCGC,DonVi,NoiDung,NgayYeuCau,NgayGhi,
                    NguoiLap,TrangThai from tblYeuCauGiaCong where NgayLap
                    between '{0}' and '{1}' and TrangThai is null)y
					left outer join
					(select sum(SoLuongGC)SoLuongGC,DonHangID,ChiTietSanPham
					from tblXuat_GiaCong where ChiTietSanPham <>''
					group by DonHangID,ChiTietSanPham)x
					on y.DonHangID=x.DonHangID 
                    and y.LinhKienGiaCong=x.ChiTietSanPham", batDau, ketThuc);
            gridControl5.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void btnTraCuYeuCauGiaCong_Click(object sender, EventArgs e)
        {
            TraCuuYeuCauGiaCong_SoLuongXuat();
        }
 
        private void TraCuuSoGiaCong()
        {
            var batDau = dpBatDau.Value.ToString("MM/dd/yyyy");
            var ketThuc = dpKetThuc.Value.ToString("MM/dd/yyyy");
            ketnoi kn = new ketnoi();
            var strQuery = string.Format(@"select BaoCaoSo,KetQua QCPass,KhachHang,
                            NgoaiQuan,NgayDi,Xoa,SoGiaCongID,
                            X.DonHangID,NgayLapPhieu,MaPhieuXuatGC,
                            DienGiai,MaDH,MaSP,SanPham,
                            SoLuongDH,ChiTietSanPham,NgayGioiHan,SoLuongGC,
                            DonVi,SoLuongYCGC,TenDonViGC,DiaChi,NguoiLap,
                            NgayGhi,SoQuiDoi,TyLeQuiDoi,DonViQuiDoi,TongBaoBi
                            from tblXuat_GiaCong X
							left outer join (select KetQua,BaoCaoSo,DonHangID from
							tblKiemTraHangHoa group by BaoCaoSo,DonHangID,KetQua)G
							on X.SoGiaCongID=G.DonHangID where Xoa is null and
                            NgayLapPhieu between '{0}' and '{1}'", batDau, ketThuc);
            gridControl4.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void btnTraCuSoGiaCong_Click(object sender, EventArgs e)
        {
            TraCuuSoGiaCong();
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gridView4.GetFocusedDisplayText();
            txtMaPhieuXuatIn.Text= gridView4.GetFocusedRowCellDisplayText(MaPhieuXuatGC_col4);
            txtTenDonViGiaCong.Text = gridView4.GetFocusedRowCellDisplayText(tengiacong_col4);
            txtDiaChiDonViGiaCong.Text = gridView4.GetFocusedRowCellDisplayText(diachigiacong_col4);
            txtTongTrongLuongBi.Text= gridView4.GetFocusedRowCellDisplayText(tongbi_col4);
            if (gridView4.GetFocusedRowCellDisplayText(QCPass_col4) == "")
            { btnin_phieu_xuat.Enabled = true; }
            else { btnin_phieu_xuat.Enabled = false; }
        }

        private void btnin_phieu_xuat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select * from viewxuatkho_giacong
                            where Xoa is null and MaPhieuXuatGC like N'" + txtMaPhieuXuatIn.Text + "'");
            Rpxuatkho_giacong Rpxuatkho = new Rpxuatkho_giacong();
            Rpxuatkho.DataSource = dt;
            Rpxuatkho.DataMember = "Table";
            Rpxuatkho.CreateDocument(false);
            Rpxuatkho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuXuatIn.Text;
            PrintTool tool = new PrintTool(Rpxuatkho.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }
        #region Cap nhat lai don vi gia cong & tong bao bi
        private void UpdateDonViGiaCongTongBiSauKhiSua()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"update tblXuat_GiaCong set 
                TenDonViGC=N'{0}',
                DiaChi=N'{1}',
                TongBaoBi=N'{2}'
				where MaPhieuXuatGC=N'{3}'",
                txtTenDonViGiaCong.Text, 
                txtDiaChiDonViGiaCong.Text,
                txtTongTrongLuongBi.Text,
                txtMaPhieuXuatIn.Text);
            int kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
        }
        #endregion
        private void btnSuaPhieuXuat_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView4.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView4.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(
                        @"update tblXuat_GiaCong 
                        set DonVi=N'{0}',SoLuongGC='{1}',
				        SoQuiDoi='{2}',
				        TyLeQuiDoi='{3}',DonViQuiDoi='{4}',
				        DienGiai=N'{5}',
                        NguoiLap=N'{6}',
                        ChiTietSanPham=N'{7}',
                        NgayDi='{8}',
                        NgayGioiHan='{9}',
                        NgayGhi=GetDate()
                        where SoGiaCongID='{10}'",
                        rowData["DonVi"], rowData["SoLuongGC"],
                        rowData["SoQuiDoi"],
                        rowData["TyLeQuiDoi"], rowData["DonViQuiDoi"],
                        rowData["DienGiai"],
                        txtnguoi_dung.Text,
                        rowData["ChiTietSanPham"], 
                        rowData["NgayDi"]==DBNull.Value?"":  Convert.ToDateTime(rowData["NgayDi"]).ToString("MM-dd-yyyy"),
                        rowData["NgayGioiHan"]==DBNull.Value?"":Convert.ToDateTime(rowData["NgayGioiHan"]).ToString("MM-dd-yyyy"),
                        rowData["SoGiaCongID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    UpdateDonViGiaCongTongBiSauKhiSua();
                    TraCuuSoGiaCong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnXoaPhieuXuat_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView4.GetSelectedRows();
            if (listRowList.Count() > 0)
                try
                {
                    var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView4.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(
                        @"update tblXuat_GiaCong 
                        set SoLuongGC='0', Xoa='x' where SoGiaCongID='{0}'",
                        rowData["SoGiaCongID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    TraCuuSoGiaCong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }
       
        private void btnGhiPhieuXuat_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuXuat.Text == "")
            { MessageBox.Show("Mã phiếu không hợp lệ", "Thông báo"); return; }
            else
            {
                int[] listRowList = this.gridView5.GetSelectedRows();
                if (listRowList.Count() > 0)
                    try
                    {
                        var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = Connect.mConnect;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        DataRow rowData;
                        for (int i = 0; i < listRowList.Length; i++)
                        {
                            rowData = this.gridView5.GetDataRow(listRowList[i]);
                            string strQuery = string.Format(@"
                           	insert into tblXuat_GiaCong 
                            (DonHangID,NgayLapPhieu,
                            MaPhieuXuatGC,
							DienGiai,MaDH,
                            MaSP,SanPham,
							SoLuongDH,ChiTietSanPham,
                            NgayGioiHan,SoLuongGC,
							DonVi,SoLuongYCGC,
							NguoiLap,MaDeNghiGC,
                            NgayDi,NgoaiQuan,
                            SoQuiDoi,TyLeQuiDoi,
                            DonViQuiDoi,
                            NgayGhi)
							values('{0}','{1}',
                            N'{2}',
							N'{3}',N'{4}',
                            N'{5}',N'{6}',
							'{7}',N'{8}',
                            '{9}','{10}',
							N'{11}','{12}',
							N'{13}',N'{14}',
                            N'{15}',N'{16}',
                            N'{17}',N'{18}',
                            N'{19}',
                            GetDate())",
                                 rowData["DonHangID"],
                                 ngayLap,
                                 txtMaPhieuXuat.Text,
                                 rowData["NoiDung"],
                                 rowData["MaDonHang"],
                                 rowData["MaSanPham"],
                                 rowData["SanPham"],
                                 rowData["SoLuongDH"],
                                 rowData["LinhKienGiaCong"],
                                 rowData["NgayYeuCau"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayYeuCau"]).ToString("yyyy-MM-dd"),
                                 rowData["Soxuat"],
                                 rowData["DonVi"], 
                                 rowData["SoLuongYCGC"],
                                 txtnguoi_dung.Text,
                                 rowData["MaYeuCau"],
                                 rowData["NgayDi"] == DBNull.Value ? "NgayDi" : Convert.ToDateTime(rowData["NgayDi"]).ToString("yyyy-MM-dd"),
                                 rowData["NgoaiQuan"],
                                 rowData["SoQuiDoi"],
                                 rowData["TyLeQuiDoi"],
                                 rowData["DonViQuiDoi"]);
                            SqlCommand cmd = new SqlCommand(strQuery, con);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        UpdateDonViGiaCongNgoai();
                        UpdateTongBaoBiGCNgoai();
                        TraCuuSoGiaCong();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("reason: " + ex, "Error message");
                    }
            }
        }
        #endregion
        #region Lưu tổng bao bì vào phiếu gia công
        private void UpdateTongBaoBiGCNgoai()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"update tblXuat_GiaCong set TongBaoBi=N'{0}' 
				where MaPhieuXuatGC='{1}'",txtTongTrongLuongBi.Text,txtMaPhieuXuat.Text);
            int kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
        }
        #endregion
      
        #region Lưu đơn vị gia công vào phiếu gia công
        private void UpdateDonViGiaCongNgoai()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"update tblXuat_GiaCong set TenDonViGC=N'{0}',DiaChi=N'{1}' 
				where MaPhieuXuatGC=N'{2}'",txtTenDonViGiaCong.Text,txtDiaChiDonViGiaCong.Text,txtMaPhieuXuat.Text);
            int kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
        }
        #endregion 
        #region Lưu tổng bao bì vào phiếu gia công khác
        private void UpdateTongBaoBiGCKhac()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"update tblXuat_GiaCong set TongBaoBi=N'{0}' 
				where MaPhieuXuatGC='{1}'", txtTongTrongLuongBi.Text, txtMaGiaCongNoiBo.Text);
            int kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
        }
        #endregion  
        #region Lưu đơn vị gia công vào phiếu gia công khác
        private void UpdateDonViGiaCongKhac()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"update tblXuat_GiaCong set TenDonViGC=N'{0}',DiaChi=N'{1}' 
				where MaPhieuXuatGC=N'{2}'", txtTenDonViGiaCong.Text, txtDiaChiDonViGiaCong.Text, txtMaGiaCongNoiBo.Text);
            int kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
        }
        #endregion
        #region CẬP NHẬT NHẬP KHO
        private void TraCuuDanhSachYeuCauGC()
        {
            var batDau = dpBatDau.Value.ToString("MM/dd/yyyy");
            var ketThuc = dpKetThuc.Value.ToString("MM/dd/yyyy");
            ketnoi kn = new ketnoi();
            var sqlQuery = string.Format(@"select MaDH,SoGiaCongID YeuCauGiaCongID,
                DonHangID,MaSP MaSanPham,MaPhieuXuatGC MaYeuCau,
                ChiTietSanPham LinhKienGiaCong,SoLuongGC SoLuongYCGC,
                DonVi,NgayGioiHan NgayYeuCau from tblXuat_GiaCong 
                where Xoa is null and NgayLapPhieu  between '{0}' and '{1}'", batDau,ketThuc);
            gridControl2.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void TraCuuDanhSachGiaCongNhap()
        {
            var batDau = dpBatDau.Value.ToString("MM/dd/yyyy");
            var ketThuc = dpKetThuc.Value.ToString("MM/dd/yyyy");
            ketnoi kn = new ketnoi();
            var sqlQuery = string.Format(@"select Xoa,NhapGCID,MaYeuCau,LinhKienGiaCong,
                SoLuongYCGC,DonVi,NgayYeuCau,
                SoLuongNhap,NgayNhap,NoiDungNhap,
                YeuCauGiaCongID,DonHangID,
                NguoiGhi,NgayGhi
                from tblXuat_GiaCong_Nhap
                where Xoa is null and NgayNhap between '{0}' and '{1}'", batDau, ketThuc);
            gridControl1.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
   

        private void btnTraCuuYCGiaCong_Click(object sender, EventArgs e)
        {
            TraCuuDanhSachYeuCauGC();
        }

        #endregion

        private void btnGhiNhapKho_GC_Click(object sender, EventArgs e)
        {
           int[] listRowList = this.gridView2.GetSelectedRows();
           if (listRowList.Count() != 1)
           { MessageBox.Show("Vui lòng chọn một đối tượng", "Thông báo"); return; }
           else
           {
               try
               {
                   var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                   SqlConnection con = new SqlConnection();
                   con.ConnectionString = Connect.mConnect;
                   if (con.State == ConnectionState.Closed)
                       con.Open();
                   DataRow rowData;
                   for (int i = 0; i < listRowList.Length; i++)
                   {
                       rowData = this.gridView2.GetDataRow(listRowList[i]);
                       string strQuery = string.Format(@"insert into tblXuat_GiaCong_Nhap 
                            (MaYeuCau,LinhKienGiaCong,
                            SoLuongYCGC,DonVi,NgayYeuCau,
                            SoLuongNhap,NgayNhap,NoiDungNhap,
                            YeuCauGiaCongID,DonHangID,
                            NguoiGhi,NgayGhi) values
                            (N'{0}',N'{1}',
                            '{2}',N'{3}','{4}',
                            '{5}','{6}',N'{7}',
                            '{8}','{9}',
                            N'{10}',GetDate())",
                            rowData["MaYeuCau"], rowData["LinhKienGiaCong"],
                            rowData["SoLuongYCGC"], rowData["DonVi"],
                            rowData["NgayYeuCau"]==DBNull.Value?""
                            :Convert.ToDateTime(rowData["NgayYeucau"]).ToString("MM-dd-yyyy"),
                            txtSoluongNhap.Text, ngayLap, txtNoiDungNhap.Text,
                            rowData["YeuCauGiaCongID"], rowData["DonHangID"],
                            txtnguoi_dung.Text);
                       SqlCommand cmd = new SqlCommand(strQuery, con);
                       cmd.ExecuteNonQuery();
                   }
                   con.Close();
                   TraCuuDanhSachGiaCongNhap();
               }
               catch (Exception ex)
               {
                   MessageBox.Show("reason: " + ex, "Error message");
               }
           }
        }

        private void btnCapNhat_NhapKho_GC_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView1.GetSelectedRows();
           
                try
                {
                    var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView1.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblXuat_GiaCong_Nhap 
                           set SoLuongNhap='{0}',NgayNhap='{1}',NoiDungNhap=N'{2}',
                            NguoiGhi=N'{3}',NgayGhi=GetDate() where NhapGCID='{4}'",
                             rowData["SoLuongNhap"], 
                             rowData["NgayNhap"]==DBNull.Value?"":
                             Convert.ToDateTime(rowData["NgayNhap"]).ToString("MM-dd-yyyy"),
                             rowData["NoiDungNhap"],
                             txtnguoi_dung.Text,rowData["NhapGCID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    TraCuuDanhSachGiaCongNhap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("reason: " + ex, "Error message");
                }
        }

        private void btnXoa_NhapKho_GC_Click(object sender, EventArgs e)
        {
          int[] listRowList = this.gridView1.GetSelectedRows();
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
                  string strQuery = string.Format(@"update tblXuat_GiaCong_Nhap 
                      set Xoa= N'x',NguoiGhi=N'{0}',
                      NgayGhi=GetDate() where NhapGCID='{1}'",
                      txtnguoi_dung.Text,rowData["NhapGCID"]);
                  SqlCommand cmd = new SqlCommand(strQuery, con);
                  cmd.ExecuteNonQuery();
              }
              con.Close();
              TraCuuDanhSachGiaCongNhap();
          }
          catch (Exception ex)
          {
              MessageBox.Show("reason: " + ex, "Error message");
          }
        }

        private void btnTraCuuNhapKho_GC_Click(object sender, EventArgs e)
        {
            TraCuuDanhSachGiaCongNhap();
        }

        private void btnPhieuYeuCau_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select SanPham,NgayDi,NgoaiQuan,YeuCauGiaCongID,
                DonHangID,MaDonHang,MaYeuCau,SoLuongDH,LinhKienGiaCong,SoLuongYCGC,
                DonVi,NgayYeuCau,NgayLap,NoiDung from 
                tblYeuCauGiaCong where  MaYeuCau 
                like N'" + txtma_yeu_cau.Text + "' and TrangThai is null");
            xryeu_cau_gia_cong yeuCauGiaCong = new xryeu_cau_gia_cong();
            yeuCauGiaCong.DataSource = dt;
            yeuCauGiaCong.DataMember = "Table";
            yeuCauGiaCong.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void grso_yc_gia_cong_Click_1(object sender, EventArgs e)
        {
            string point = "";
            point = grvso_yc_gia_cong.GetFocusedDisplayText();
            txtma_yeu_cau.Text = grvso_yc_gia_cong.GetFocusedRowCellDisplayText(maYC_colsoyc);
        }

        private void btnGoiBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSP.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiYeuCauGiaCong();
        }

        private void btnExport_phieu_xuat_Click(object sender, EventArgs e)
        {
            gridView4.ShowPrintPreview();
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            grvso_yc_gia_cong.ShowPrintPreview();
        }

        private void btnThemDanhMucDonViGiaCong_Click(object sender, EventArgs e)
        {
            frmDV_giacong themNCC = new frmDV_giacong();
            themNCC.ShowDialog();
            DocDSDonViGiaCong();
        }

        private void cbMaDonViGC_MouseMove(object sender, MouseEventArgs e)
        {
      
        }

        private void cbMaDonViGC_EditValueChanged(object sender, EventArgs e)
        {
            string point = "";
            point = gridLookUpEdit1View.GetFocusedDisplayText();
            txtTenDonViGiaCong.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(ten_colgiacong);
            txtDiaChiDonViGiaCong.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(diachi_colgiacong);
        }
        double soquidoi;
        double tylequidoi;
        double sothucxuat;
        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == sothucxuat_col5 || e.Column == tylequidoi_col5)
            {
                sothucxuat = Convert.ToDouble(gridView5.GetFocusedRowCellValue(sothucxuat_col5));
                tylequidoi = Convert.ToDouble(gridView5.GetFocusedRowCellValue(tylequidoi_col5));
                soquidoi = sothucxuat / tylequidoi;
                gridView5.SetFocusedRowCellValue(soquidoi_col5, soquidoi);
            }
            else {soquidoi = 0;}
        }

        private void btnMaPhieuXuatGiaCongNoiBo_Click(object sender, EventArgs e)
        {
            TaoMaPhieuGiaCongNoiBo();
        }

        private void btnGhiGiaCongNoiBo_Click(object sender, EventArgs e)
        {
            if (txtMaGiaCongNoiBo.Text == "")
            { MessageBox.Show("Mã phieu gia cong noi bo khong hợp lệ", "Thông báo"); return; }
            else
            {
                int[] listRowList = this.gridView3.GetSelectedRows();
                if (listRowList.Count() > 0)
                    try
                    {
                        var ngayLap = dpNgayLapPhieuXuat.Value.ToString("yyyy-MM-dd");
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = Connect.mConnect;
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        DataRow rowData;
                        for (int i = 0; i < listRowList.Length; i++)
                        {
                            rowData = this.gridView3.GetDataRow(listRowList[i]);
                            string strQuery = string.Format(@"
                           	insert into tblXuat_GiaCong 
                            (DonHangID,NgayLapPhieu,
                            MaPhieuXuatGC,DienGiai,
                            MaDH,MaSP,
                            SanPham,ChiTietSanPham,
                            NgayGioiHan,SoLuongGC,
							DonVi,MaDeNghiGC,
                            NgayDi,NgoaiQuan,
                            TenDonViGC,DiaChi,
                            SoQuiDoi,TyLeQuiDoi,
                            DonViQuiDoi,NguoiLap,
                            NgayGhi)
							values('{0}','{1}',
                                   N'{2}',N'{3}',
                                   N'{4}',N'{5}',
                                   N'{6}',N'{7}',
                                   '{8}','{9}',
                                   N'{10}',N'{11}',
                                   '{12}',N'{13}',
                                   N'{14}',N'{15}',
                                   '{16}','{17}',
                                   '{18}','{9}',
                                   GetDate())",
                                   rowData["Iden"],dpngay_lap.Value.ToString("MM-dd-yyyy"),
                                   txtMaGiaCongNoiBo.Text, rowData["DienGiai"],
                                   rowData["madh"], rowData["Masp"],rowData["Tenquicach"],
                                   rowData["ChiTietSanPham"],rowData["NgayGioiHan"], 
                                   rowData["SoLuongGC"],rowData["dvt"], 
                                   rowData["MaDeNghiGC"],rowData["NgayDi"],
                                   rowData["ngoaiquang"],txtTenDonViGiaCong.Text,
                                   txtDiaChiDonViGiaCong.Text,rowData["SoQuiDoi"],
                                   rowData["TyLeQuiDoi"],rowData["DonViQuiDoi"], 
                                   txtnguoi_dung.Text);
                            SqlCommand cmd = new SqlCommand(strQuery, con);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        UpdateDonViGiaCongKhac();
                        UpdateTongBaoBiGCKhac();
                        TraCuuSoGiaCong();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("reason: " + ex, "Error message");
                    }
            }
        }

        private void btnTraCuuGiaCongNoiBo_Click(object sender, EventArgs e)
        {
            DonHangTrienKhaiGiaCongNoiBo();
            this.gridView3.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }
        private void DonHangTrienKhaiGiaCongNoiBo()
        {
           ketnoi kn = new ketnoi();
           var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
           var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
           var strQuery = string.Format(@"select Num,MaGH,t.BTPT15,t.TRONGLUONG15,IdPSX Iden,''DienGiai,
          c.madh,c.mabv Masp,c.sanpham Tenquicach,c.soluongsx Soluong,
		  t.IDSP,t.BTPT15,t.chitietsanpham ChiTietSanPham,0 SoLuongGC,t.donvi dvt,''MaDeNghiGC,
		  cast('' as Date) NgayGioiHan,
		  cast('' as Date) NgayDi,c.ngoaiquang,
		   ''SoQuiDoi,1.00 TyLeQuiDoi,''DonViQuiDoi
		   from tbl15 t left outer join
		   tblchitietkehoach c on c.IDSP=t.IDSP where cast(ngaynhan as date)
           between '{0}' and '{1}' order by IDSP Desc",
           batDau, ketThuc);
           gridControl3.DataSource = kn.laybang(strQuery);
           kn.dongketnoi();
        }
        double soquidoi_noibo;
        double tylequidoi_noibo;
        double sothucxuat_noibo;
        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == thucxuatnoibo_col3 || e.Column == tylenoibo_col3)
            {
                sothucxuat_noibo = Convert.ToDouble(gridView3.GetFocusedRowCellValue(thucxuatnoibo_col3));
                tylequidoi_noibo = Convert.ToDouble(gridView3.GetFocusedRowCellValue(tylenoibo_col3));
                soquidoi_noibo = sothucxuat_noibo / tylequidoi_noibo;
                gridView3.SetFocusedRowCellValue(soquidoi_col3, soquidoi_noibo);
            }
            else { soquidoi_noibo = 0; }
        }

        private void btn_statistical_Click(object sender, EventArgs e)
        {
            ThongKeGiaCong();
        }
        private void ThongKeGiaCong()
        {
            ketnoi kn = new ketnoi();
            var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
            var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
            string sqlStr = string.Format(@"select x.*,n.SoLuongNhap,x.SoXuat-n.SoLuongNhap SoTon from
				(select max(SoGiaCongID)SoGiaCongID,max(MaDH)MaDH,max(MaSP)MaSP,max(SanPham)SanPham,
				DonHangID,ChiTietSanPham,SUM(SoLuongGC)SoXuat
				from tblXuat_GiaCong where Xoa is null and NgayLapPhieu between '{0}' and '{1}'
				group by DonHangID,ChiTietSanPham)x
				left outer join
				(select YeuCauGiaCongID,SUM(SoLuongNhap)SoLuongNhap from tblXuat_GiaCong_Nhap
				where Xoa is null
				group by YeuCauGiaCongID)n
				on x.SoGiaCongID=n.YeuCauGiaCongID", batDau,ketThuc);
            gridControl6.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void btnTaoPhieuMoi_Click(object sender, EventArgs e)
        {
            LoadNoiDungMoi();
            this.gridView3.OptionsView.NewItemRowPosition 
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }
 
        private void LoadNoiDungMoi()
        {
            ketnoi kn = new ketnoi();
            var batDau = dpBatDau.Value.ToString("yyyy/MM/dd");
            var ketThuc = dpKetThuc.Value.ToString("yyyy/MM/dd");
            var strQuery = string.Format(@"select Top 0 Iden,''DienGiai,madh, Masp,
		       Tenquicach,Soluong,''ChiTietSanPham,0 SoLuongGC,dvt,''MaDeNghiGC,
		       cast('' as Date) NgayGioiHan,
		       cast('' as Date) NgayDi,ngoaiquang,
		       ''SoQuiDoi,1.00 TyLeQuiDoi,''DonViQuiDoi
		       from tblDHCT
               where cast(thoigianthaydoi as Date)
               between '{0}' and '{1}' order by Code Desc",
            batDau, ketThuc);
            gridControl3.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        void reposiVatTu()
        {
            repositoryItemcbChiTietPhuKien.Items.Clear();
            ketnoi cn = new ketnoi();
            var dt = cn.laybang(@"select Ten_vat_lieu ChiTietSanPham  from tblDM_VATTU
                                  where Ten_vat_lieu <>'' union
		                          select Tenvlphu from 
                                  tblDM_VATLIEUPHU where Tenvlphu <>''");
            cn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemcbChiTietPhuKien.Items.Add(dt.Rows[i]["ChiTietSanPham"]);
            }
        }
        void reposiXuatGiaCong()
        {
            repositoryItemcbChiTietPhuKien.Items.Clear();
               ketnoi cn = new ketnoi();
            var dt = cn.laybang(@"select ChiTietSanPham  from tblXuat_GiaCong 
		   where ChiTietSanPham <>'' group by ChiTietSanPham");
            cn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemcbChiTietPhuKien.Items.Add(dt.Rows[i]["ChiTietSanPham"]);
            }
        }
        private void rbDonHang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDonHang.Checked == true)
            {
                rbVatTu.Checked =false;
                rbKhac.Checked = false;
                DonHangTrienKhaiGiaCongNoiBo();
            }
        }

        private void rbVatTu_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVatTu.Checked == true)
            {
                rbDonHang.Checked = false;
                rbKhac.Checked = false;
                reposiVatTu();
            }
        }

        private void rbKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKhac.Checked == true)
            {
                rbDonHang.Checked = false;
                rbVatTu.Checked = false;
                reposiXuatGiaCong();
            }
        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
           
            DocPhieuNhapHangDen();
        }
        private void DocPhieuNhapHangDen()
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select ngaynhan,MaGH,t.BTPT15,t.TRONGLUONG15,IdPSX Iden,''DienGiai,
              c.madh,c.mabv Masp,c.sanpham Tenquicach,c.soluongsx Soluong,
		      t.IDSP,t.chitietsanpham ChiTietSanPham,0 SoLuongGC,t.donvi dvt,''MaDeNghiGC,
		      cast('' as Date) NgayGioiHan,
		      cast('' as Date) NgayDi,c.ngoaiquang,
		      ''SoQuiDoi,1.00 TyLeQuiDoi,''DonViQuiDoi
		      from tbl15 t left outer join
		      tblchitietkehoach c on c.IDSP=t.IDSP where MaGH like N'" + txtMaNhap.Text + "'");
            XReportNhanHangDen nhapHangDen = new XReportNhanHangDen();
            nhapHangDen.DataSource = dt;
            nhapHangDen.DataMember = "Table";
            nhapHangDen.CreateDocument(false);
            nhapHangDen.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuXuatIn.Text;
            PrintTool tool = new PrintTool(nhapHangDen.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void MaNhapKho()
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
                txtMaNhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void UpdateMaNhapKhoHangDen()
        {
            int[] listRowList = gridView3.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gridView3.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update tbl15 set MaGH = '{0}' where Num = '{1}'",
                txtMaNhap.Text,
                rowData["Num"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DonHangTrienKhaiGiaCongNoiBo();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView3.GetFocusedDisplayText();
            txtMaNhap.Text = gridView3.GetFocusedRowCellDisplayText(manhaphangden_grid);
        }

        private void gridControl5_Click_1(object sender, EventArgs e)
        {

        }

        private void txtTaoMaNhap_Click(object sender, EventArgs e)
        {
            MaNhapKho();
            UpdateMaNhapKhoHangDen();
        }
    }
}
