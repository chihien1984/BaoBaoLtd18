using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using quanlysanxuat.View;

namespace quanlysanxuat
{
    public partial class UcXUAT_VATTU : DevExpress.XtraEditors.XtraForm
    {
        public UcXUAT_VATTU()
        {
            InitializeComponent();
        }
        //formload
        private void UcXUAT_VATTU_Load(object sender, EventArgs e)
        {
            gvDinhMucVatLieu.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvSoXuatKho.Appearance.Row.Font = new Font("Segoe UI", 7f);
            dpXuatKhoTu.Text = DateTime.Now.ToString("01/MM/yyyy"); dpXuatKhoDen.Text = DateTime.Now.ToString();
            dpKeHoachVatTuTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpKeHoachVatTuDen.Text = DateTime.Now.ToString() ;
            txtUser.Text = Login.Username;
            GridLookupTonKho();
            DSBoPhanNhanVatTu();
            DocDSKeHoachVatTuTheoNgay();
            DSXuatKhoTheoNgay();
            DocNguoiNhanTheoBoPhan();
            AutoNguoNhan();
        }
        #region Danh mục kế hoạch vật tư
        private void ListKeHoachVT()
        {
            ketnoi kn = new ketnoi();
            grDinhMucVatLieu.DataSource = kn.laybang("select * from tblvattu_dauvao order by Ngaylap_DM DESC");
            kn.dongketnoi();
        }

        private void KeHoachVatTu_Click(object sender, EventArgs e)
        {
            ListKeHoachVT();
        }

       
        private async void DocDSKeHoachVatTuTheoNgay()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select a.*,b.SoXuat from tblvattu_dauvao a
				left outer join (select Madh,Mavattu,sum(Soluongxuat)SoXuat 
				from tblXuatKho group by Madh,Mavattu)b
				on a.madh=b.Madh and a.Mavattu=b.Mavattu
				where cast (Ngaylap_DM as Date)
                between '{0}' and '{1}'
				order by Ngaylap_DM DESC",
                dpKeHoachVatTuTu.Value.ToString("yyyy-MM-dd"),
                dpKeHoachVatTuDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grDinhMucVatLieu.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            }
          );
         
        }
        private void btnDocKeHoachVatTuTheoNgay_Click(object sender, EventArgs e)
        {
            DocDSKeHoachVatTuTheoNgay();
        }
        #endregion

        #region Danh mục Xuất kho
        private void ListXuatKho()
        {
            ketnoi kn = new ketnoi();
            grSoXuatKho.DataSource = kn.laybang("select * from tblXuatKho order by SUBSTRING(Maxuat,4,6) ASC,SUBSTRING(Maxuat,11,3) ASC");
            kn.dongketnoi();
        }
        private void XuatKho_Click(object sender,EventArgs e)
        {
            ListXuatKho();
        }
        private async void DSXuatKhoTheoNgay()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from tblXuatKho
                    where Ngayxuat between '{0}' and '{1}'
                    order by SUBSTRING(Maxuat,4,6) ASC,SUBSTRING(Maxuat,11,3) ASC",
                dpXuatKhoTu.Value.ToString("yyyy-MM-dd"),
                dpXuatKhoDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grSoXuatKho.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            }
          );
        }
        private void btnDocXuatKhoVatTuTheoNgay_Click(object sender, EventArgs e)
        {
            DSXuatKhoTheoNgay();
        }
        #endregion

        private void ListNhapHangGhi()
        {
            ketnoi kn = new ketnoi();
            grDinhMucVatLieu.DataSource = kn.laybang("select * from tblXuatKho where Maxuat like N'"+txtMaxuat.Text+"'");
            kn.dongketnoi();
        }
        private void GridLookupTonKho()//Danh mục hàng tồn trong kho
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
        private void Binding_KHVatTu(object sender, EventArgs e)//Binding kế hoạch vật tư
        {
            string Gol = "";
            Gol = gvDinhMucVatLieu.GetFocusedDisplayText();
            txtidvattu.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(idvt_grid1);
            txtMavatlieu.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Mavattu_grid1);
            txtTenVatLieu.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Ten_vt_grid1);
            txtMaDonHang.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Madh_grid1);
            txtMaDeNghi.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(MaDNVATTU_grid1);
            txtSoDeNghi.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(SoLuongvattumua_grid1);
            txtDonViDeNghi.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Donvivattumua_grid1);
            txtSLDinhMuc.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(SoluongDMVT_grid1);
            txtDonviDinhMuc.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Donvivtdm_grid1);
            txtNCC.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(NCC_grid1);
            txtMaNCC.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(MaNCC_grid1);
            txtMasp.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Masp_grid1);
            txtTensp.Text = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Tensp_grid1);
            txtDienGiai.Text= gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Tensp_grid1)+
                "; "+ gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Madh_grid1);
            NhapXuatTon(); DMNhapXuatTon(); travezero(); TruTonKho();
            if (txtidvattu.Text=="")
            {
                txtidvattu.Text = "0";
            }
            //gridLookUpEditVatTu.Properties.DisplayMember = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Mavattu_grid1);
            //gridLookUpEditVatTu.Properties.ValueMember = gvDinhMucVatLieu.GetFocusedRowCellDisplayText(Mavattu_grid1);
        }
        private void Binding_XuatKho(object sender, EventArgs e)//Binding xuất vật tư
        {
            string Gol = "";
            Gol = gvSoXuatKho.GetFocusedDisplayText();
            txtidvattu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(idvt_grid2);
            txtId.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(id_grid2);
            txtMaxuat.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Sochungt_grid2);
            gridLookUpEditVatTu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Sochungt_grid2);

            txtMavatlieu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Mavatlieu_grid2);
            dpNgayLap.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Ngaychungtu_grid2);
            txtTenVatLieu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Tenvatlieu_grid2);
            txtDienGiai.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Diengiai_grid2);
            txtSoxuat.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLthucnhap_grid2);
            txtDonvi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Donvinhap_grid2);
            txtMaDeNghi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Madenghi_grid2);
            txtMaDonHang.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(MaPSX_grid2);
            txtNguoiNhan.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Nguoinhan_grid2);
            cbBoPhan.Text= gvSoXuatKho.GetFocusedRowCellDisplayText(Noinhan_grid2);
            //txtSLDinhMuc.Text = gridView2.GetFocusedRowCellDisplayText(SLdenghi_grid2);
            //txtDonviDinhMuc.Text = gridView2.GetFocusedRowCellDisplayText(Donvidenghi_grid2);
            //txtNCC.Text = gridView2.GetFocusedRowCellDisplayText(NCC_grid2);
            //txtSoDeNghi.Text = gridView2.GetFocusedRowCellDisplayText(SLdenghi_grid2);
            //txtDonViDeNghi.Text = gridView2.GetFocusedRowCellDisplayText(Donvidenghi_grid2);    
            //txtMaNCC.Text = gridView2.GetFocusedRowCellDisplayText(MaNCC_grid2);
            //txtSoQuyDoi.Text = gridView2.GetFocusedRowCellDisplayText(SLtinhgia_grid2);
            //txtDonViQuyDoi.Text = gridView2.GetFocusedRowCellDisplayText(Donvitinhgia_grid2);
            NhapXuatTon(); 
            DMNhapXuatTon(); 
            travezero();
            TruTonKho();
        }
        private void BindingEditMavatTu(object sender, EventArgs e)//Binding tên vật tư theo mã vật tư
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMavatlieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colMaVatTu_);
            txtTenVatLieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colTenVatTu_);
            txtMaNCC.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(colMaNhaCungCap); 
            Refresh();
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }

        private void LayMaPhieuXuat(object sender,EventArgs e)//Lấy Mã Phiếu Xuất kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Top 1 'PX '+REPLACE(convert(nvarchar,GetDate(),11),'/','') "
               + " +'-'+convert(nvarchar,(DATEPART(HH,GetDate())))+':'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaxuat.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        #region Ghi ngày xuất nhập lớn nhất vào danh sách vật tư tồn kho
        private void UpdateMaxNgayXuatVatTu()
        {
            ketnoi kn = new ketnoi();
            grSoXuatKho.DataSource = kn.laybang(@"update tblDM_VATTU set MaxNgayXuat=Xuat.NgayXuat from
			(select Mavattu,Max(Ngayxuat) NgayXuat from tblXuatKho group by Mavattu)Xuat
			where tblDM_VATTU.Ma_vl=Xuat.Mavattu");
            kn.dongketnoi();
        }
        #endregion
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATTU set TonCuoi =" + txtTonCuoi.Text + " where Ma_vl like N'" + txtMavatlieu.Text + "'");
            kn.dongketnoi();
        }
        private void Them(object sender,EventArgs e) //Ghi dữ liệu xuất kho
        {
            try
            {
                if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
                { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
                else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                    && string.IsNullOrEmpty(txtTenVatLieu.Text)
                    && string.IsNullOrEmpty(txtMaxuat.Text))
                { MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblXuatKho "
                    + " (Bophan,idvattu,Maxuat,Ngayxuat,Madenghi,Mavattu,Tenvattu,Soluongxuat, "
                    + " Donvi,Madh,Masp,Sanpham,Nguoilap,Ngaylap,Nguoinhan,Diengiai) "
                    + " values(@Bophan,@idvattu,@Maxuat,@Ngayxuat,@Madenghi,@Mavattu,@Tenvattu,@Soluongxuat, "
                    + " @Donvi,@Madh,@Masp,@Sanpham,@Nguoilap,GetDate(),@Nguoinhan,@Diengiai)", con);
                    cmd.Parameters.Add(new SqlParameter("@Bophan", SqlDbType.NVarChar)).Value = cbBoPhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@idvattu", SqlDbType.BigInt)).Value = txtidvattu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Maxuat", SqlDbType.NVarChar)).Value = txtMaxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngayxuat", SqlDbType.Date)).Value = dpNgayLap.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add(new SqlParameter("@Madenghi", SqlDbType.NVarChar)).Value = txtMaDeNghi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavattu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tenvattu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluongxuat", SqlDbType.Float)).Value =double.Parse(txtSoxuat.Text).ToString();
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Madh", SqlDbType.NVarChar)).Value = txtMaDonHang.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Sanpham", SqlDbType.NVarChar)).Value = txtTensp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grSoXuatKho.DataSource = dt;
                    UpdateXuatKho();//group số lượng xuất kho                   
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateMaxNgayXuatVatTu();//Cập nhật ngày xuất gần nhất
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                    ListXuatKho();//Load lại danh mục nhập kho
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    Update_TonCuoi();//Cập nhật tồn kho cuối kỳ cho danh mục vật tư
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông báo");
            }
        }

        private void Sua(object sender, EventArgs e)//Sửa dữ liệu xuất kho
        {
            try
            {
                if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
                { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
                else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                    && string.IsNullOrEmpty(txtTenVatLieu.Text)
                    && string.IsNullOrEmpty(txtMaxuat.Text))
                { MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("update tblXuatKho "
                    + " set  Bophan=@Bophan,Ngayxuat=@Ngayxuat,Madenghi=@Madenghi,Mavattu=@Mavattu,Tenvattu=@Tenvattu,Soluongxuat=@Soluongxuat, "
                    + " Donvi=@Donvi,Madh=@Madh,Masp=@Masp,Sanpham=@Sanpham,Nguoilap=@Nguoilap,Ngaylap=GetDate(), "
                    +" Nguoinhan=@Nguoinhan,Diengiai=@Diengiai where id like "+txtId.Text+"", con);
                    cmd.Parameters.Add(new SqlParameter("@Bophan", SqlDbType.NVarChar)).Value = cbBoPhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Maxuat", SqlDbType.NVarChar)).Value = txtMaxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngayxuat", SqlDbType.Date)).Value = dpNgayLap.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add(new SqlParameter("@Madenghi", SqlDbType.NVarChar)).Value = txtMaDeNghi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavattu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tenvattu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluongxuat", SqlDbType.Float)).Value = double.Parse(txtSoxuat.Text).ToString();
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Madh", SqlDbType.NVarChar)).Value = txtMaDonHang.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Sanpham", SqlDbType.NVarChar)).Value = txtTensp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grSoXuatKho.DataSource = dt;
                    UpdateXuatKho();//group số lượng nhập kho                   
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateMaxNgayXuatVatTu();
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn       
                    ListXuatKho();//Load lại danh mục nhập kho
                    Update_TonCuoi();//Cập nhật tồn kho cuối kỳ cho danh mục vật tư
                                   
                }
            }
            catch
            {
                Console.WriteLine(MessageBox.Show("Không thành công", "Thông báo"));
            }

   
        }

        private void Xoa(object sender, EventArgs e)// Xóa dữ liệu xuất kho
        {
            if (txtMavatlieu.Text != gridLookUpEditVatTu.Text)
            { MessageBox.Show("Mã vật tư và ô check mã vật tư chưa trùng khớp"); return; }
            else if (string.IsNullOrEmpty(txtMavatlieu.Text)
                && string.IsNullOrEmpty(txtTenVatLieu.Text)
                && string.IsNullOrEmpty(txtMaxuat.Text))
            { MessageBox.Show("Có điều kiện Mã vật liệu hoặc tên vật liệu hoặc Mã phiếu nhập trống rỗng"); return; }

            UpdateZero();//Trả giá trị của mặt hàng hóa đó về 0 sau đó mới xóa hàng hóa đó khỏi danh sách
            ketnoi kn = new ketnoi();
            grSoXuatKho.DataSource = kn.xulydulieu("Delete tblXuatKho where id like "+txtId.Text+"");
            kn.dongketnoi();
            UpdateXuatKho();//group số lượng nhập kho                   
            GridLookupTonKho();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
            TruTonKho();//Trừ lấy số tồn kho
            UpdateMaxNgayXuatVatTu();
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
            ListXuatKho();//Load lại danh mục nhập kho
            Update_TonCuoi();//Cập nhật tồn kho cuối kỳ cho danh mục vật tư
        }
        private void UpdateZero()//Trả giá trị xuất hàng hóa về 0
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATTU set TongXuat=0 where Ma_vl like N'" + txtMavatlieu.Text + "' ");
            kn.dongketnoi();
        }
        private void Export(object sender, EventArgs e)//Xuất dữ liệu xuất kho
        {
            grSoXuatKho.ShowPrintPreview();
        }

        private void Refresh(object sender, EventArgs e)//Làm mới
        {
            if (txtSLDinhMuc.Text == "") txtSLDinhMuc.Text = "0";
            txtDonviDinhMuc.Clear();
            txtSLDinhMuc.Text = "0";
            txtSoDeNghi.Text = "0";
            txtDonViDeNghi.Clear();
            txtSoxuat.Text = "0";
            txtSoQuyDoi.Text = "0";
            txtSoQuyDoi.Text = "0";
        }

        private void Clear(object sender, EventArgs e)//Xóa dữ liệu trong các ô textbox
        {

        }
       

        private void LapPhieuXuat(object sender,EventArgs e)//print phieu xuat kho
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from viewXuatKho_VT where Maxuat like N'" + txtMaxuat.Text + "'");
            XReportXuatKhoVT XuatKho = new XReportXuatKhoVT();
            XuatKho.DataSource = dt;
            XuatKho.DataMember = "Table";
            XuatKho.CreateDocument(false);
            XuatKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaxuat.Text;
            PrintTool tool = new PrintTool(XuatKho.PrintingSystem);
            XuatKho.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void UpdateXuatKho()//Group mã đã xuất kho
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateXuat_VatTu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
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
            }
            catch (Exception)
            { }
        }

        private void travezero()//Đưa giá trị Zezo vào các textbox
        {
            if (txtTonDau.Text=="")
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

        private void NhapXuatTon()
        {
            if (txtMavatlieu.Text == "")
            {
                txtTonDau.Text = "0";
                txtNhap.Text = "0";
                txtXuat.Text = "0";
                txtTonCuoi.Text = "0";
            }
        }

        private void DMNhapXuatTon()//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
        {
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

        private void Mavattu_Change(object sender, EventArgs e)//Thay đổi Mã vật tư
        {
            NhapXuatTon(); 
            DMNhapXuatTon(); 
            travezero();
            TruTonKho();
        }

        private void UpdateTonCuoi(object sender,EventArgs e) 
        {
            Update_TonCuoi();
        }
        void Update_TonCuoi()
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
            { MessageBox.Show("Trừ tồn không thành công"); }
        }

        private void btnDMVatTu_Click(object sender, EventArgs e)
        {
            frmThemDMvattu fDMVatTu = new frmThemDMvattu();
            fDMVatTu.ShowDialog(); GridLookupTonKho();
        }
        private void AutoNguoNhan()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Nguoinhan from tblXuatKho where Nguoinhan  <>''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtNguoiNhan.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        private void DSBoPhanNhanVatTu()
        {
            ketnoi kn = new ketnoi();
            cbBoPhan.DataSource = kn.laybang("select TenBoPhanNhan from DSBoPhanNhanVatTu");
            cbBoPhan.ValueMember = "TenBoPhanNhan";
            cbBoPhan.DisplayMember = "TenBoPhanNhan";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GridLookupTonKho();
            Update_TonCuoi();//Cập nhật lại số tồn kho cuối
        }

        private void btnThemBoPhanNhanVatTu_Click(object sender, EventArgs e)
        {
            frmDMBoPhanNhanVatTu dMBoPhanNhanVatTu = new frmDMBoPhanNhanVatTu();
            dMBoPhanNhanVatTu.ShowDialog();
            DSBoPhanNhanVatTu();
        }

        private void cbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocNguoiNhanTheoBoPhan();
        }
        private void DocNguoiNhanTheoBoPhan()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select NguoiDaiDien from 
                DSBoPhanNhanVatTu where TenBoPhanNhan like N'" + cbBoPhan.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtNguoiNhan.Text = Convert.ToString(reader[0]);
            reader.Close();
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
        string maDonHang;
        string maVatLieu;
        private void gvDinhMucVatLieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void grDinhMucVatLieu_DoubleClick(object sender, EventArgs e)
        {
            if (gvDinhMucVatLieu.GetRowCellValue(gvDinhMucVatLieu.FocusedRowHandle, gvDinhMucVatLieu.Columns["madh"]) == null||
                gvDinhMucVatLieu.GetRowCellValue(gvDinhMucVatLieu.FocusedRowHandle, gvDinhMucVatLieu.Columns["Mavattu"]) == null)
                return;
            else
            {
                maDonHang = gvDinhMucVatLieu.GetRowCellValue(gvDinhMucVatLieu.FocusedRowHandle, gvDinhMucVatLieu.Columns["madh"]).ToString();
                maVatLieu = gvDinhMucVatLieu.GetRowCellValue(gvDinhMucVatLieu.FocusedRowHandle, gvDinhMucVatLieu.Columns["Mavattu"]).ToString();
            }
            ThXuatKhoTheoMaVatTu();
        }
        private async void ThXuatKhoTheoMaVatTu()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from tblXuatKho
                    where Madh like N'{0}' and Mavattu like N'{1}' 
                    order by SUBSTRING(Maxuat,4,6) ASC,SUBSTRING(Maxuat,11,3) ASC",
                maDonHang,
                maVatLieu);
                Invoke((Action)(() => {
                    grSoXuatKho.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            }
          );
            Model.Function.Disconnect();
        }

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
