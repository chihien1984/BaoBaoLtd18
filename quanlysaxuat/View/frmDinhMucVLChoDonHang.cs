using DevExpress.XtraPrinting;
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
    public partial class frmDinhMucVLChoDonHang : DevExpress.XtraEditors.XtraForm
    {
        public frmDinhMucVLChoDonHang()
        {
            InitializeComponent();
        }
        SANXUATDbContext db = new SANXUATDbContext();
        private void frmDanhMucSanPham_Load(object sender, EventArgs e)
        {   grvChuaMaSP.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gridView1.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gridView2.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gridView3.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gridView4.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gridView5.Appearance.Row.Font = new Font("Times New Roman", 7f);
            txtMember.Text = Login.Username;
            dpDonHangTKTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDonHangTKDen.Text = DateTime.Now.ToString();
            DocDonDatHangTheoNgay();
            LoadItemNhaCungUng();
            DocDinhMucVatLieuDHTheoNgay();
            DocDeNghiVatTuTheoNgay();
            btnCapNhatDinhMucDonHang.Enabled = false;
            BtnXuatPhieuDinhMucQuiDinh.Enabled = false;
            DocDMVatLieuChuaCoMaSanPham();
            DocItemVatLieu();
            gridView2.VisibleColumns[0].Width =10;
            gridView4.VisibleColumns[0].Width =10;
        
        }
        #region Thêm định mức vật tư cho sản phẩm không có mã
        private void AddDinhMuc_SP_KhongMa()
        {
            ketnoi cn = new ketnoi();
        }
        #endregion
        void ShowButton_Save_Edit()
        {
            if (this.gridView3.GetSelectedRows().Count() > 0)
            {
                btnCapNhatDinhMucDonHang.Enabled = true;
            }
            else
            {
                btnCapNhatDinhMucDonHang.Enabled = false;
            }
            if (txtXuatDinhMucDonHang.Text == "")
            {
                BtnXuatPhieuDinhMucQuiDinh.Enabled = false;
            }
            else
            {
                BtnXuatPhieuDinhMucQuiDinh.Enabled = true;
            }
        }
        private void gridControl3_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButton_Save_Edit();
        }
        //Cập nhật nhà cung cấp
        private void LoadItemNhaCungUng()
        {
            ketnoi Connect = new ketnoi();
            repositoryItemCungUng.DataSource = Connect.laybang(@"select * from tblDM_NCC_VATTU order by Ngaycapnhat_NCC desc ");
            repositoryItemCungUng.DisplayMember = "Ten_NCC";
            repositoryItemCungUng.ValueMember = "Ten_NCC";
            repositoryItemCungUng.NullText = @"Chọn nhà cung ứng vật tư";
            Connect.dongketnoi();
        }

        private void DocIdSanPham()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(@"select Code from tblSANPHAM where Masp='" + txtMaSanPham.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtSanPhanID.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void DocDonDatHangTheoNgay()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select V.Iden TrangThai,P.MaSP maDinhMucDonVi,C.* from tblDHCT C
                left outer join(select distinct(Iden) from tblvattu_dauvao) V
                on C.Iden=V.Iden left outer join
				(select distinct (MaSP) from DinhMucVatTu where Masp <>'') P
				on P.MaSP=C.MaSP
                where convert( datetime,thoigianthaydoi) between '" + dpDonHangTKTu.Value.ToString("yyyy/MM/dd") + "' and '" + dpDonHangTKDen.Value.ToString("yyyy/MM/dd") + "' order by Code Desc");
            kn.dongketnoi();
        }
        private void DocTatCaDonDatHang()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select V.Iden TrangThai,P.MaSP maDinhMucDonVi,C.* from tblDHCT C
                left outer join(select distinct(Iden) from tblvattu_dauvao) V
                on C.Iden=V.Iden left outer join
				(select distinct (MaSP) from DinhMucVatTu where Masp <>'') P
				on P.MaSP=C.MaSP
				order by Code Desc");
            kn.dongketnoi();
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView1.GetFocusedDisplayText();
            txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(masp_grid2);
            txtTenSanPham.Text = gridView1.GetFocusedRowCellDisplayText(tenquicach_grid2);
            txtSoLuong.Text = gridView1.GetFocusedRowCellDisplayText(Soluong_DH_grid2);
            txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid2);
            txtDonVi.Text = gridView1.GetFocusedRowCellDisplayText(donvitinh_grid2);
            txtDonHangID.Text = gridView1.GetFocusedRowCellDisplayText(donHangID_grid2);
            txtNVKD.Text = gridView1.GetFocusedRowCellDisplayText(nVKD_col2);
            GanDinhMucVatTuDonHang();//Đọc định mức vật tư cho sản phẩm khi chọn vào đơn hàng triển khai
            DocDinhMucTheoDonHang();//Đọc định mức đã lập
            DocDeNghiVatTuTheoDonHang();//Đọc đề nghị vật tư đã lập
            DocIdSanPham();//Đọc ID sản phẩm tính định mức
            DocItemDanhMucVatTu();//Set trường hợp có mã sản phẩm thì sử dụng định mức, và chưa có mã sản phẩm thì sử dụng bảng không có định mức
            gridView3.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void DocDinhMucTheoDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblvattu_dauvao
            where madh = '" + txtMadh.Text + "'");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }
        #region TRƯỜNG HỢP ĐƠN HÀNG CHƯA CÓ MÃ SẢN PHẨM
        //Trường hợp mã sản phẩm không null thì áp dụng định mức
        //Ngược lại thì đọc danh mục vật tư
        private void DocItemDanhMucVatTu()
        {
            if (txtMaSanPham.Text == "")
            {
                chuacoMaSP.PageVisible = true;
                coMaSP.PageVisible = false;
            }
            else
            {
                coMaSP.PageVisible = true;
                chuacoMaSP.PageVisible = false;
            }
        }
        //Đọc danh mục vật tư cho đơn hàng chưa có mã sản phẩm
        private void DocDMVatLieuChuaCoMaSanPham()
        {
            var dt = db.Delivery_VatTu.ToList();
            gridControl5.DataSource = new BindingList<Delivery_VatTu>(dt);
            dt.Clear();
        }
        //Đọc Item tên vật tư cho đơn hàng chưa có vật tư
        private void DocItemVatLieu()
        {
            var dt = db.tblDM_VATTU.ToList();
            repositoryItemVatTu.DataSource = dt;
            repositoryItemVatTu.ValueMember = "Ten_vat_lieu";
            repositoryItemVatTu.DisplayMember = "Ten_vat_lieu";
            repositoryItemVatTu.NullText = @"Chọn vật tư";
            ten_gridlook.ColumnEdit = repositoryItemVatTu;
        }
        //Khi thay đổi giá trị trên Item vật tư thì mã vật tư sẽ chạy theo
        private void gridView6_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Ten_vat_lieu")
            {
                var value = grvChuaMaSP.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDM_VATTU.FirstOrDefault(x => x.Ten_vat_lieu == (string)value);
                if (dt != null)
                {
                    grvChuaMaSP.SetRowCellValue(e.RowHandle, "Ma_vl", dt.Ma_vl);
                    grvChuaMaSP.SetRowCellValue(e.RowHandle, "DonVi", dt.Donvi);
                    grvChuaMaSP.SetRowCellValue(e.RowHandle, "id", dt.id);
                }
            }
        }
        private void btnGhiDinhMucChuaCoMaSanPham_Click(object sender, EventArgs e)
        {
            double soLuong = Convert.ToDouble(txtSoLuong.Text);
            if (this.grvChuaMaSP.DataRowCount < 0)
            { MessageBox.Show("Cần có 1 loại vật tư", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                for (int i = 0; i < grvChuaMaSP.DataRowCount; i++)
                {
                    string strQuery = string.Format(@"insert into tblvattu_dauvao 
                        (Iden,madh,Masp,Tenquicachsp,Soluongsanpham,
                        Donvi_sanpham,
                        Mavattu,
                        Ten_vattu,
                        SL_vattucan,
                        Donvi_vattu,
                        Nguoilap_DM,
                        DinhMucDonVi,
                        KhoanMuc,
                        NVKD,
                        DienGiai_CachTinh,
                        Ngaylap_DM)
                        values(N'{0}',N'{1}',N'{2}',N'{3}','{4}',
                               N'{5}',N'{6}',N'{7}','{8}',N'{9}',
                               N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',GetDate())",
                        txtDonHangID.Text, 
                        txtMadh.Text,
                        txtMaSanPham.Text, 
                        txtTenSanPham.Text, 
                        soLuong,
                        txtDonVi.Text,
                        grvChuaMaSP.GetRowCellValue(i, "Ma_vl").ToString(),
                        grvChuaMaSP.GetRowCellValue(i, "Ten_vat_lieu").ToString(),
                        grvChuaMaSP.GetRowCellValue(i, "DinhMucDonHang").ToString(),
                        grvChuaMaSP.GetRowCellValue(i, "DonVi").ToString(),
                        txtMember.Text, grvChuaMaSP.GetRowCellValue(i, "DinhMuc").ToString(),
                        grvChuaMaSP.GetRowCellValue(i, "KhoanMuc").ToString(),
                        txtNVKD.Text, grvChuaMaSP.GetRowCellValue(i, "DienGiai").ToString());
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSDinhMucVLChuaCoMa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do thiếu nội dung:" + ex, "Thông báo");
            }
        }
        #endregion
        private void DocDSDinhMucVLChuaCoMa()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblvattu_dauvao 
            where Iden = '" + txtDonHangID.Text + "' order by CodeVatllieu desc");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }
        private void GanDinhMucVatTuDonHang()
        {
            try
            {
                double soLuong = Convert.ToDouble(txtSoLuong.Text);
                ketnoi kn = new ketnoi();
                gridControl3.DataSource = kn.laybang("select DM.*,SP.Masp,SP.Tensp,DinhMuc * '" + soLuong + "' DinhMucDonHang "
                   + " from DinhMucVatTu DM left outer join "
                   + " tblSANPHAM SP on DM.SanPhamID=SP.Code "
                   + " where SP.Masp= '" + txtMaSanPham.Text + "'");
                kn.dongketnoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Bạn chọn không đúng");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            DocDMVatLieuChuaCoMaSanPham();
        }


        private void btnDonHangTK_Click(object sender, EventArgs e)
        {
            DocDonDatHangTheoNgay();
        }

        private void btnDocTatCaDonDatHang_Click(object sender, EventArgs e)
        {
            DocTatCaDonDatHang();
        }

        private void btnLapDinhMuc_Click(object sender, EventArgs e)
        {
            frmCapNhatDinhMucVatLieu.maSanPham = txtMaSanPham.Text;
            frmCapNhatDinhMucVatLieu.tenSanPham = txtTenSanPham.Text;
            frmCapNhatDinhMucVatLieu.sanPhamID = txtSanPhanID.Text;
            frmCapNhatDinhMucVatLieu.userName = txtMember.Text;
            frmCapNhatDinhMucVatLieu dinhMucVatLieu = new frmCapNhatDinhMucVatLieu();
            dinhMucVatLieu.Show();
        }
        private void DocDSDinhMucVatLieuDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblvattu_dauvao 
            where madh = '" + txtMadh.Text + "' order by CodeVatllieu desc");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }
        private void DocTatCaDinhMucVatLieuDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblvattu_dauvao order by CodeVatllieu DESC");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }

        private void btnCapNhatDinhMucDonHang_Click(object sender, EventArgs e)
        {
            double soLuong = Convert.ToDouble(txtSoLuong.Text);
            if (this.gridView3.GetSelectedRows().Count() < 0)
            { MessageBox.Show("Cần tích chọn", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView3.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblvattu_dauvao 
                    (Iden,madh,Masp,Tenquicachsp,Soluongsanpham,
                    Donvi_sanpham,Mavattu,Ten_vattu,SL_vattucan,
                    Donvi_vattu,Nguoilap_DM,DinhMucDonVi,KhoanMuc,NVKD,DienGiai_CachTinh,Ngaylap_DM)
                    values(N'{0}',N'{1}',N'{2}',N'{3}','{4}',
                           N'{5}',N'{6}',N'{7}','{8}',N'{9}',
                           N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',GetDate())",
                        txtDonHangID.Text,
                        txtMadh.Text,
                        txtMaSanPham.Text,
                        txtTenSanPham.Text,
                        soLuong,
                        txtDonVi.Text,
                        rowData["MaVatTu"],
                        rowData["TenVatTu"],
                        rowData["DinhMucDonHang"],
                        rowData["DonVi"],
                        txtMember.Text,
                        rowData["DinhMuc"],
                        rowData["KhoanMuc"],
                        txtNVKD.Text,
                        rowData["DienGiai"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSDinhMucVatLieuDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnDocTatCaDMNLDonHang_Click(object sender, EventArgs e)
        {
            DocTatCaDinhMucVatLieuDonHang();
        }



        private void btnXoaDMucVatLieuDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format("delete from tblvattu_dauvao where CodeVatllieu ='{0}'",
                       rowData["CodeVatllieu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSDinhMucVatLieuDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void BtnXuatPhieuDinhMucQuiDinh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from PHIEUSANXUAT where madh like N'" + txtXuatDinhMucDonHang.Text + "'");
            XRPhieuSX_DaDuyet xrPSX_VatTu = new XRPhieuSX_DaDuyet();
            xrPSX_VatTu.DataSource = dt;
            xrPSX_VatTu.DataMember = "Table";
            xrPSX_VatTu.CreateDocument(false);
            xrPSX_VatTu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtXuatDinhMucDonHang.Text;
            PrintTool tool = new PrintTool(xrPSX_VatTu.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView2.GetFocusedDisplayText();
            txtXuatDinhMucDonHang.Text = gridView2.GetFocusedRowCellDisplayText(madhCol);
        }

        private void btnLayMaDeNghi_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(@"select 
				CASE WHEN DATEPART(MM,GETDATE())<10 THEN 
				'PDN '+'0'+ CAST(DATEPART(MM,GETDATE())as nvarchar) 
				+RIGHT(CAST(DATEPART(YYYY,GETDATE()) AS nvarchar),2)+'-'
                +RIGHT('00'+Max(RIGHT(MaDN_VATTU,2)),2)
				else 
				'PDN '+ CAST(DATEPART(MM,GETDATE())as nvarchar) 
				+RIGHT(CAST(DATEPART(YYYY,GETDATE()) AS nvarchar),2)+'-'
                +RIGHT('00'+Max(RIGHT(MaDN_VATTU,2)),2)
				END 
				from tblvattu_dauvao where month(Ngaylap_DM)=month(GETDATE()) 
                and YEAR(Ngaynhap)=YEAR(getdate())", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuDeNghi.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnDocDSDeNghiVatTu_Click(object sender, EventArgs e)
        {
            DocTatCaDeNghiVatTu();
        }
        private void DocTatCaDeNghiVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select * from tblvattu_dauvao order by CodeVatllieu DESC");
            kn.dongketnoi();
        }
        private void DocDeNghiVatTuTheoDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select * from tblvattu_dauvao where madh ='" + txtMadh.Text + "'");
            kn.dongketnoi();
        }

        private void btnPrintDeNghiVatTu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT DatHangVatTuDauVao_STT.* FROM DatHangVatTuDauVao_STT where MaDN_VATTU like '" + txtMaPhieuDeNghi.Text + "'");
            XtraReportDeNghiVatTu rpDEXUATVATTU = new XtraReportDeNghiVatTu();
            rpDEXUATVATTU.DataSource = dt;
            rpDEXUATVATTU.DataMember = "Table";
            rpDEXUATVATTU.CreateDocument(false);
            rpDEXUATVATTU.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuDeNghi.Text;
            PrintTool tool = new PrintTool(rpDEXUATVATTU.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnCapNhatDeNghiVatTu_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView4.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblvattu_dauvao set MaDN_VATTU=N'{0}',
                        SL_vattumua='{1}',Dongia='{2}',Donviquydoi=N'{3}',
                        Ngaydat_vattu='{4}',NgayDK_ve='{5}',MaNCC=N'{6}',
                        Ghichu_denghimua=N'{7}' where CodeVatllieu='{8}'",
                        rowData["MaDN_VATTU"],
                        rowData["SL_vattumua"],
                        rowData["Dongia"],
                        rowData["Donviquydoi"],
                        rowData["Ngaydat_vattu"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["Ngaydat_vattu"]).ToString("yyyy-MM-dd"),
                        rowData["NgayDK_ve"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayDK_ve"]).ToString("yyyy-MM-dd"),
                        rowData["MaNCC"],
                        rowData["Ghichu_denghimua"],
                       rowData["CodeVatllieu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDeNghiVatTuTheoDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        //xoa cap nhat
        private void btnXoaDeNghiVatTu_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView4.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblvattu_dauvao 
                       set MaDN_VATTU='' where CodeVatllieu='{0}'",
                       rowData["CodeVatllieu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDeNghiVatTuTheoDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView4.GetFocusedDisplayText();
            txtMaPhieuDeNghi.Text = gridView4.GetFocusedRowCellDisplayText(maDNghi_grid4);
        }

        private void btnNhaCungUng_Click(object sender, EventArgs e)
        {
            frmThemNCC themNCC = new frmThemNCC();
            themNCC.ShowDialog();
            LoadItemNhaCungUng();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void DocDinhMucVatLieuDHTheoNgay()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblvattu_dauvao where Ngaylap_DM between '" + dpDinhMucVatTuDHTu.Value.ToString("MM-dd-yyyy") + "' and '" + dpDinhMucVatTuDHDen.Value.ToString("MM-dd-yyyy") + "' order by CodeVatllieu DESC");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
        }

        private void DocDeNghiVatTuTheoNgay()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select * from tblvattu_dauvao where Ngaylap_DM between '" + dpDeNghiVatTuTu.Value.ToString("MM-dd-yyyy") + "' and '" + dpDeNghiVatTuDen.Value.ToString("MM-dd-yyyy") + "' order by CodeVatllieu DESC");
            kn.dongketnoi();
        }

        private void btnDeNghiVatTuTheoNgay_Click(object sender, EventArgs e)
        {
            DocDeNghiVatTuTheoNgay();
        }

        private void btnDinhMucVatTuDHTheoNgay_Click(object sender, EventArgs e)
        {
            DocDinhMucVatLieuDHTheoNgay();
        }

        private void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            ShowButton_Save_Edit();
        }

        private void btnBuPhuKien_Click(object sender, EventArgs e)
        {
            chuacoMaSP.PageVisible = true;
            coMaSP.PageVisible = false;
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void btnExportDinhMuc_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void btnExportCoDeNghi_Click(object sender, EventArgs e)
        {
            gridControl4.ShowPrintPreview();
        }
    }
}
