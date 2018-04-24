using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class UcNHAPVATTU : DevExpress.XtraEditors.XtraForm
    {
        public UcNHAPVATTU()
        {
            InitializeComponent(); 
        }
        //formload
        private void UcNHAPVATTU_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.ToString();
            //dpNhapKhoTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            //dpNhapKhoDen.Text = dpDen.Text = DateTime.Now.ToString();
            GridlookupEditMaVatTu();
            DocKeHoachVatTuTheoNgay();
            DocDSVatTuNhapKhoTheoNgay();
            TinhTrangVatTu();
            ViTriKho();
        }
        private void TinhTrangVatTu()
        {
            cbTinhTrang.Items.Add("Đạt nhập kho");
            cbTinhTrang.Items.Add("Chưa đạt chờ xử lý");
        }
        private void ViTriKho()
        {
            cbDiaDiemKho.Items.Add("Kho-Bao-Bao");
            cbDiaDiemKho.Items.Add("Kho-CN-Bao-Bao");
        }
        private void LayManhapkho()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 'PN '+
                REPLACE(convert(nvarchar,GetDate(),11),'/','') 
                +'-'+replace(left(CONVERT(time, GetDate()),8),':','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuNhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void Phieunhap(object sender, EventArgs e)
        {
            LayManhapkho();
        }
        #region Kế hoạch vật tư theo đơn hàng
        private void DocKeHoachVatTu()
        {
            ketnoi kn = new ketnoi();
            grKeHoachVatTu.DataSource = kn.laybang(@"select * from tblvattu_dauvao 
            order by Ngaylap_DM DESC");
            kn.dongketnoi();
        }
        private void ListDMDinhMucVatTu(object sender,EventArgs e)
        {
            DocKeHoachVatTu();
        }
        private void DocKeHoachVatTuTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from tblvattu_dauvao 
            where cast(Ngaylap_DM as Date) 
            between '{0}' and '{1}' order by Ngaylap_DM DESC",
            dpTu.Value.ToString("yyyy-MM-dd"),
            dpDen.Value.ToString("yyyy-MM-dd"));
            grKeHoachVatTu.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }

        private void btnDocKeHoachVatTuTheoNgay_Click(object sender, EventArgs e)
        {
            DocKeHoachVatTuTheoNgay();
        }
        #endregion

        #region Danh sach nhập kho vật tư
        private void DocDSVatTuNhapKho()
        {
            ketnoi kn = new ketnoi();
            grNhapVatTu.DataSource = kn.laybang(@"select VT.*,Ten_vattu from tblNHAP_VATTU VT 
            left outer join tblvattu_dauvao DV on VT.idvattu=DV.Codevatllieu 
            order by SUBSTRING(So_chung_tu,4,6) ASC,SUBSTRING(So_chung_tu,11,3) ASC");
            kn.dongketnoi();
        }
        private void ListDMNhapVatTu(object sender, EventArgs e) 
        {
            DocDSVatTuNhapKho();
        }
        
        private void DocDSVatTuNhapKhoTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select VT.*, Ten_vattu from tblNHAP_VATTU VT
             left outer join tblvattu_dauvao DV on VT.idvattu = DV.Codevatllieu where convert(Date, Ngay_lap, 103)
             between '{0}' and '{1}' order by SUBSTRING(So_chung_tu, 4, 6) ASC, SUBSTRING(So_chung_tu, 11, 3) ASC", 
            dpTu.Value.ToString("yyyy-MM-dd"), 
            dpDen.Value.ToString("yyyy-MM-dd"));
            grNhapVatTu.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void btnDocNhapKhoTheoNgay_Click(object sender, EventArgs e)
        {
            DocDSVatTuNhapKhoTheoNgay();
        }
        #endregion 
        private string idkehoachvattu; 
        private void Binding_KHVatTu(object sender, EventArgs e)//Binding kế hoạch vật tư
        {
            string Gol = "";
            Gol = gvKeHoachVatTu.GetFocusedDisplayText();
            idkehoachvattu = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Code_grid1);
            txtMavatlieu.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Mavattu_grid1);
            txtTenVatLieu.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Ten_vt_grid1);
            txtMaDonHang.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Madh_grid1);
            txtMaDeNghi.Text= gvKeHoachVatTu.GetFocusedRowCellDisplayText(MaDNVATTU_grid1);
            txtSoDeNghi.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(SoLuongvattumua_grid1);
            txtDonViDeNghi.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Donvivattumua_grid1);
            txtSLDinhMuc.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(SoluongDMVT_grid1);
            txtDonviDinhMuc.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(Donvivtdm_grid1);
            txtNCC.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(NCC_grid1);
            txtMaNCC.Text = gvKeHoachVatTu.GetFocusedRowCellDisplayText(MaNCC_grid1);
            txtDienGiai.Text= gvKeHoachVatTu.GetFocusedRowCellDisplayText(Tensp_grid1)+"; "+ gvKeHoachVatTu.GetFocusedRowCellDisplayText(Madh_grid1);
            NhapXuatTon(); DMNhapXuatTon(); travezero(); TruTonKho();
            if (idkehoachvattu == "")
            {
                idkehoachvattu = "0";
            }
        }
        private void Binding_NKNhap(object sender,EventArgs e)//Binding nhập vật tư
        {
            string Gol = "";
            Gol = gvNhapVatTu.GetFocusedDisplayText();
            idkehoachvattu = gvNhapVatTu.GetFocusedRowCellDisplayText(idvattu_grid2);
            txtId.Text = gvNhapVatTu.GetFocusedRowCellDisplayText(id_grid2);
            txtMaPhieuNhap.Text= gvNhapVatTu.GetFocusedRowCellDisplayText(Sochungt_grid2);
            gridLookUpEditVatTu.Text = gvNhapVatTu.GetFocusedRowCellDisplayText(Mavatlieu_grid2);
            txtMavatlieu.Text= gvNhapVatTu.GetFocusedRowCellDisplayText(Mavatlieu_grid2);

            dpNgayLap.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Ngaychungtu_grid2);
            txtSLDinhMuc.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(SLdenghi_grid2);
            txtDonviDinhMuc.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Donvidenghi_grid2);
            txtNCC.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(NCC_grid2);
            txtTenVatLieu.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Tenvatlieu_grid2);
            txtDienGiai.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Diengiai_grid2);
            txtSoDeNghi.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(SLdenghi_grid2);
            txtDonViDeNghi.Text = gvNhapVatTu.GetFocusedRowCellDisplayText(Donvidenghi_grid2);
            txtSoNhap.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(SLthucnhap_grid2);
            txtDonViNhap.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Donvinhap_grid2);
            txtMaNCC.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(MaNCC_grid2);
            txtMaDeNghi.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(Madenghi_grid2);
            txtMaDonHang.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(MaPSX_grid2);
            txtSoQuyDoi.Text=gvNhapVatTu.GetFocusedRowCellDisplayText(SLtinhgia_grid2);
            txtDonViQuyDoi.Text = gvNhapVatTu.GetFocusedRowCellDisplayText(Donvitinhgia_grid2);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero(); 
            TruTonKho();
        }
        private void Lookup ()
        {

        }
        private void BindingEditMavatTu(object sender, EventArgs e)//Binding Danh mục vật tư
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMavatlieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colMaVatTu_);
            txtTenVatLieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colTenVatTu_);
            txtMaNCC.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colMaNhaCungCap);
            RefreshSl();
            NhapXuatTon(); 
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }
        private void TenNCC(object sender, EventArgs e) //Lấy danh sách tên nhà cung cấp khi mã thay đổi
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Ten_NCC from tblDM_NCC_VATTU where Ma_NCC like N'"+txtMaNCC.Text+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtNCC.Text = Convert.ToString(reader[0]);
            reader.Close(); con.Close();
        }
        private void GridlookupEditMaVatTu()//Danh mục xuất nhập tồn kho vật tư
        {
            ketnoi Connect = new ketnoi();
            string sqlQuery = string.Format(@"select Ma_vl,
                        isnull(TonDau,0)TonDau,isnull(TongNhap,0)TongNhap,
                        isnull(TongXuat,0)TongXuat,
                        isnull(TonCuoi,0)TonCuoi,b.Ma_NCC,b.Ten_NCC,Ten_vat_lieu
                        from vwDanhMucVatTuChinh a left outer join
				        (select Ma_NCC,Ten_NCC from tblDM_NCC_VATTU)b
				        on a.MaNCC=b.Ma_NCC");
            gridLookUpEditVatTu.Properties.DataSource = Connect.laybang(sqlQuery);
            gridLookUpEditVatTu.Properties.DisplayMember = "Ma_vl";
            gridLookUpEditVatTu.Properties.ValueMember = "Ma_vl";
            gridLookUpEditVatTu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookUpEditVatTu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookUpEditVatTu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void btnDMVatTu_Click(object sender, EventArgs e)
        {
            frmThemDMvattu fDMVatTu = new frmThemDMvattu();
            fDMVatTu.ShowDialog(); GridlookupEditMaVatTu();
        }
       
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATTU set TonCuoi="+txtTonCuoi.Text+" where Ma_vl like N'"+txtMavatlieu.Text+"'");
            kn.dongketnoi();
        }
        #region Ghi nhập lớn nhất vào danh sách vật tư tồn kho
        private void UpdateMaxNgayNhapVatTu()
        {
            ketnoi kn = new ketnoi();
            grNhapVatTu.DataSource = kn.laybang(@"update tblDM_VATTU set MaxNgayNhap=NHAP.NgayNhap from
            (select Ma_vat_lieu, Max(Ngay_lap) NgayNhap from tblNHAP_VATTU group by Ma_vat_lieu)NHAP
            where tblDM_VATTU.Ma_vl = NHAP.Ma_vat_lieu");
            kn.dongketnoi();
        }
        #endregion
        private void Ghi(object sender,EventArgs e)
        { 
            try
            {
                if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
                { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
                else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                    && string.IsNullOrEmpty(txtTenVatLieu.Text)
                    && string.IsNullOrEmpty(txtMaPhieuNhap.Text))
                {MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblNHAP_VATTU "
                    + " (idvattu,Nguoilap,Ngay_lap,Ngay_chung_tu,So_chung_tu "
                    + " ,Ma_de_nghi,Ma_vat_lieu,Ten_vat_lieu ,Dien_giai,SL_de_nghi,Donvi_denghi "
                    + " ,SL_thuc_nhap,Donvinhap,SL_tinh_gia,Don_vi,MaPSX,DinhMuc ,Donvi_dinhmuc,NCC,Ma_NCC,TinhTrang) "
                    + " values(@idvattu,@Nguoilap,GetDate(),@Ngay_chung_tu,@So_chung_tu "
                    + " ,@Ma_de_nghi,@Ma_vat_lieu,@Ten_vat_lieu ,@Dien_giai,@SL_de_nghi,@Donvi_denghi "
                    + " ,@SL_thuc_nhap,@Donvinhap,@SL_tinh_gia,@Don_vi ,@MaPSX,@DinhMuc,@Donvi_dinhmuc,@NCC,@Ma_NCC,@TinhTrang)", con);
                    cmd.Parameters.Add(new SqlParameter("@idvattu", SqlDbType.BigInt)).Value = idkehoachvattu;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = Login.Username;
                    cmd.Parameters.Add(new SqlParameter("@Ngay_chung_tu", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@So_chung_tu", SqlDbType.NVarChar)).Value = txtMaPhieuNhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_de_nghi", SqlDbType.NVarChar)).Value = txtMaDeNghi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_vat_lieu", SqlDbType.NVarChar)).Value = gridLookUpEditVatTu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ten_vat_lieu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Dien_giai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@SL_de_nghi", SqlDbType.Float)).Value = double.Parse(txtSoDeNghi.Text).ToString();
                    cmd.Parameters.Add(new SqlParameter("@Donvi_denghi", SqlDbType.NVarChar)).Value = txtDonViDeNghi.Text;
                    cmd.Parameters.Add(new SqlParameter("@SL_thuc_nhap", SqlDbType.Float)).Value = double.Parse(txtSoNhap.Text).ToString();
                    cmd.Parameters.Add(new SqlParameter("@Donvinhap", SqlDbType.NVarChar)).Value = txtDonViNhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@SL_tinh_gia", SqlDbType.Float)).Value = txtSoQuyDoi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Don_vi", SqlDbType.NVarChar)).Value = txtDonViQuyDoi.Text;
                    cmd.Parameters.Add(new SqlParameter("@MaPSX", SqlDbType.NVarChar)).Value = txtMaDonHang.Text;
                    cmd.Parameters.Add(new SqlParameter("@DinhMuc", SqlDbType.Float)).Value = double.Parse(txtSLDinhMuc.Text).ToString();
                    cmd.Parameters.Add(new SqlParameter("@Donvi_dinhmuc", SqlDbType.NVarChar)).Value = txtDonviDinhMuc.Text;
                    cmd.Parameters.Add(new SqlParameter("@NCC", SqlDbType.NVarChar)).Value = txtNCC.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_NCC", SqlDbType.NVarChar)).Value = txtMaNCC.Text;
                    cmd.Parameters.Add(new SqlParameter("@TinhTrang", SqlDbType.NVarChar)).Value = cbTinhTrang.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grNhapVatTu.DataSource = dt;
                    UpdateNhapKho();//group số lượng nhập kho                   
                    GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                    UpdateMaxNgayNhapVatTu();
                    DocDSVatTuNhapKho();//Load lại danh mục nhập kho

                } 
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            } 
        }

        private void Sua(object sender, EventArgs e)
        {
            try
            {
                if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
                { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
                else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                    && string.IsNullOrEmpty(txtTenVatLieu.Text)
                    && string.IsNullOrEmpty(txtMaPhieuNhap.Text))
                { MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }

                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("update tblNHAP_VATTU "
                + " set idvattu=@idvattu,Nguoilap=@Nguoilap,Ngay_lap=GetDate(), "
                +" @Ngay_chung_tu=@Ngay_chung_tu,So_chung_tu=@So_chung_tu "
                +" ,Ma_de_nghi=@Ma_de_nghi,Ma_vat_lieu=@Ma_vat_lieu, "
                +" Ten_vat_lieu=@Ten_vat_lieu,Dien_giai=@Dien_giai, "
                +" SL_de_nghi=@SL_de_nghi,Donvi_denghi=@Donvi_denghi "
                +" ,SL_thuc_nhap=@SL_thuc_nhap,Donvinhap=@Donvinhap, "
                +" SL_tinh_gia=@SL_tinh_gia,Don_vi=@Don_vi, "
                +" MaPSX=@MaPSX,DinhMuc=@DinhMuc,Donvi_dinhmuc=@Donvi_dinhmuc, "
                + " NCC=@NCC,Ma_NCC=@Ma_NCC,TinhTrang=@TinhTrang where id like " + txtId.Text + "", con);
                cmd.Parameters.Add(new SqlParameter("@idvattu", SqlDbType.BigInt)).Value = idkehoachvattu;
                cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = Login.Username;
                cmd.Parameters.Add(new SqlParameter("@Ngay_chung_tu", SqlDbType.Date)).Value =dpNgayLap.Text ;
                cmd.Parameters.Add(new SqlParameter("@So_chung_tu", SqlDbType.NVarChar)).Value = txtMaPhieuNhap.Text;
                cmd.Parameters.Add(new SqlParameter("@Ma_de_nghi", SqlDbType.NVarChar)).Value = txtMaDeNghi.Text;
                cmd.Parameters.Add(new SqlParameter("@Ma_vat_lieu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                cmd.Parameters.Add(new SqlParameter("@Ten_vat_lieu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                cmd.Parameters.Add(new SqlParameter("@Dien_giai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                cmd.Parameters.Add(new SqlParameter("@SL_de_nghi", SqlDbType.Float)).Value = double.Parse(txtSoDeNghi.Text).ToString();
                cmd.Parameters.Add(new SqlParameter("@Donvi_denghi", SqlDbType.NVarChar)).Value = txtDonViDeNghi.Text;
                cmd.Parameters.Add(new SqlParameter("@SL_thuc_nhap", SqlDbType.Float)).Value = double.Parse(txtSoNhap.Text).ToString();
                cmd.Parameters.Add(new SqlParameter("@Donvinhap", SqlDbType.NVarChar)).Value = txtDonViNhap.Text;
                cmd.Parameters.Add(new SqlParameter("@SL_tinh_gia", SqlDbType.Float)).Value = txtSoQuyDoi.Text;
                cmd.Parameters.Add(new SqlParameter("@Don_vi", SqlDbType.NVarChar)).Value = txtDonViQuyDoi.Text;
                cmd.Parameters.Add(new SqlParameter("@MaPSX", SqlDbType.NVarChar)).Value = txtMaDonHang.Text;
                cmd.Parameters.Add(new SqlParameter("@DinhMuc", SqlDbType.Float)).Value = double.Parse(txtSLDinhMuc.Text).ToString();
                cmd.Parameters.Add(new SqlParameter("@Donvi_dinhmuc", SqlDbType.NVarChar)).Value = txtDonviDinhMuc.Text;
                cmd.Parameters.Add(new SqlParameter("@NCC", SqlDbType.NVarChar)).Value = txtNCC.Text;
                cmd.Parameters.Add(new SqlParameter("@Ma_NCC", SqlDbType.NVarChar)).Value = txtMaNCC.Text;
                cmd.Parameters.Add(new SqlParameter("@TinhTrang", SqlDbType.NVarChar)).Value = cbTinhTrang.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grNhapVatTu.DataSource = dt;
                UpdateNhapKho();//group số lượng nhập kho
                GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
                DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                TruTonKho();//Trừ lấy số tồn kho
                UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                UpdateMaxNgayNhapVatTu();
                DocDSVatTuNhapKho();//Load lại danh mục nhập kho
               
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            } 
        }
        private void Xoa(object sender, EventArgs e)
        {
            if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
            { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
            else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                && string.IsNullOrEmpty(txtTenVatLieu.Text)
                && string.IsNullOrEmpty(txtMaPhieuNhap.Text))
            { MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }

            UpdateZero();//Trả giá trị nhập hóa về 0
            ketnoi kn = new ketnoi();
            grNhapVatTu.DataSource = kn.xulydulieu("delete tblNHAP_VATTU  where id like " + txtId.Text + "");
            kn.dongketnoi();
            UpdateNhapKho();//group số lượng nhập kho
            GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
            TruTonKho();//Trừ lấy số tồn kho
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
            UpdateMaxNgayNhapVatTu();
            DocDSVatTuNhapKho();//Load lại danh mục nhập kho
        }
        private void UpdateZero()//Trả giá trị xuất hàng hóa về 0
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATTU set TongNhap=0 where Ma_vl like N'" + txtMavatlieu.Text + "' ");
            kn.dongketnoi();
        }
        private void UpdateNhapKho()
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateNhap_VatTu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
        }

        private void RefreshSl() 
        {
            if(txtSLDinhMuc.Text=="")txtSLDinhMuc.Text="0";
            txtDonviDinhMuc.Clear();
            txtSLDinhMuc.Text = "0";
            txtSoDeNghi.Text = "0";
            txtDonViDeNghi.Clear();
            txtSoNhap.Text = "0";
            txtSoQuyDoi.Text = "0";
            txtSoQuyDoi.Text = "0";
        }

        private void BtnExport()
        {
            grNhapVatTu.ShowPrintPreview();
        }
        private void BtnXuatPhieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from viewNhapKho_VT where So_chung_tu like N'{0}' ", txtMaPhieuNhap.Text);
            dt = kn.laybang(sqlQuery);
            XRNhapKho RpNhapVT = new XRNhapKho();
            RpNhapVT.DataSource = dt;
            RpNhapVT.DataMember = "Table";
            RpNhapVT.CreateDocument(false);
            RpNhapVT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuNhap.Text;
            PrintTool tool = new PrintTool(RpNhapVT.PrintingSystem);
            RpNhapVT.ShowPreviewDialog();
          
        }
        private void TruTonKho()//Hàm tính tồn kho
        {
            try
            {
                double TonDau = double.Parse(txtTonDau.Text);
                double Nhap = double.Parse(txtNhap.Text);
                double Xuat = double.Parse(txtXuat.Text);
                double TonCuoi = TonDau + Nhap - Xuat;
                txtTonCuoi.Text = Convert.ToString(TonCuoi);
                if (TonCuoi<0)
                {
                    txtTonCuoi.Text = "Lệch";
                }
            }
            catch (Exception)
            { }
        }
        private void Mavattu_Change(object sender,EventArgs e)//Thay đổi Mã vật tư
        {
            DMNhapXuatTon(); NhapXuatTon(); travezero(); TruTonKho();
        }
        private void travezero()
        {
            if (txtTonDau.Text == "")
            {
                txtTonDau.Text = "0";
            }
            if (txtNhap.Text == "")
            {
                txtNhap.Text = "0";
            }
            if (txtXuat.Text == "")
            {
                txtXuat.Text = "0";
            }
        }
        private void NhapXuatTon()//Nếu giá trị ô Mã vật tư rỗng thì Nhập xuất tồn trả về Zero
        {
            if (txtMavatlieu.Text=="")
            {
                txtTonDau.Text = "0";
                txtNhap.Text = "0";
                txtXuat.Text = "0";
                txtTonCuoi.Text = "0";
            }
        }

        private void DMNhapXuatTon()//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd = new SqlCommand("select isnull(Soluong_ton,0),isnull(TongNhap,0),isnull(TongXuat,0) from tblDM_VATTU where Ma_vl like N'" + txtMavatlieu.Text + "'", con);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        reader.Read();
            //        txtTonDau.Text = Convert.ToString(reader[0]);
            //        txtNhap.Text = Convert.ToString(reader[1]);
            //        txtXuat.Text = Convert.ToString(reader[2]);
            //        //txtTonCuoi.Text = Convert.ToString(reader[3]);
            //    }
            //    con.Close();
            //}
            //catch (Exception)
            //{
            //}
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlQuery = string.Format(@"select
                        isnull(TonDau,0)TonDau,isnull(TongNhap,0)TongNhap,
                        isnull(TongXuat,0)TongXuat,isnull(TonCuoi,0)TonCuoi,
                        Ma_vl,b.Ma_NCC,b.Ten_NCC,Ten_vat_lieu
                        from vwDanhMucVatTuChinh a left outer join
				        (select Ma_NCC,Ten_NCC from tblDM_NCC_VATTU)b
				        on a.MaNCC=b.Ma_NCC where
                        Ma_vl like N'{0}'", txtMavatlieu.Text);
                cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTonDau.Text = Convert.ToString(reader[0]);
                    txtNhap.Text = Convert.ToString(reader[1]);
                    txtXuat.Text = Convert.ToString(reader[2]);
                    txtTonCuoi.Text = Convert.ToString(reader[3]);
                    txtMaNCC.Text = Convert.ToString(reader[5]);
                    txtNCC.Text = Convert.ToString(reader[6]);
                }
                con.Close();
            }
            catch { };
        }
        private void UpdateTonCuoi(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateTonCuoi_VatTu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Trừ tồn thành công"); 
            }
            catch (Exception)
            { MessageBox.Show("Trừ tồn không công"); }
        }

        private void txtSoNhap_TextChanged(object sender, EventArgs e)
        {
            //double Thucnhap = double.Parse(txtSoNhap.Text);
            //double Nhap = double.Parse(txtNhap.Text);
            //double Tongnhap = Thucnhap + Nhap;
            //txtNhap.Text = Convert.ToString(Tongnhap);
        }
        #region Phân bổ vật tư nhập cho định mức đơn hàng
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GridlookupEditMaVatTu();
        }

        private void btnDonHangDaTinhDinhMuc_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + txtMaDonHang.Text + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();
        }
    }
}
