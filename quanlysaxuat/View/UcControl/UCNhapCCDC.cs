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
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class UCNhapCCDC : DevExpress.XtraEditors.XtraForm
    {
        public UCNhapCCDC()
        {
            InitializeComponent();
        }
        private void UcNHAPVATTU_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Login.Username;
            GridlookupEditMaVatTu();
            CbNghiepvu.Items.Add("Tồn kho");
            CbNghiepvu.Items.Add("Xuất trực tiếp");
            Vb_XuatTT();//An chuc nang xuat truc tiep
            DocNhapKhoTheoNgay();
        }
        public void LoadDataVattu()
        { }
        private void LayManhapkho()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 'PN '+
                  REPLACE(convert(nvarchar,GetDate(),11),'/','') 
                 +'-'+convert(nvarchar,(DATEPART(HH,GetDate())))+
                 ':'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaphieunhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void Phieunhap(object sender, EventArgs e)
        {
            LayManhapkho();
        }

        private void ListNK_NhapKho()//Lấy danh mục nhập vật tư
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select N.*,DM.Tenvlphu from tblNHAP_VATLIEUPHU N 
            left outer join tblDM_VATLIEUPHU DM on DM.Mavlphu=N.Mavlphu order by SUBSTRING(Manhap,4,6) ASC,SUBSTRING(Manhap,11,3) ASC");
            kn.dongketnoi();
        }
        private void ListNK_NhapKhoThem()//Lay danh muc vat tu nhap kho them
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select N.*,DM.Tenvlphu from tblNHAP_VATLIEUPHU N "
            + "left outer join tblDM_VATLIEUPHU DM on DM.Mavlphu=N.Mavlphu where Manhap like N'" + txtMaphieunhap.Text+ "'");
            kn.dongketnoi();
        }
        private void ListDMNhapVatTu(object sender, EventArgs e)//Danh mục vật tư nhập kho
        {
            ListNK_NhapKho();
        }
        private void DocNhapKhoTheoNgay()//Lay danh muc vat tu nhap kho them
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select N.*,DM.Tenvlphu from tblNHAP_VATLIEUPHU N 
            left outer join tblDM_VATLIEUPHU DM on DM.Mavlphu=N.Mavlphu where N.Ngaynhap 
            between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by SUBSTRING(Manhap,4,6) ASC,SUBSTRING(Manhap,11,3) ASC");
            kn.dongketnoi();
        }
        private void btnDocNhapCCDCTheoNgay_Click(object sender, EventArgs e)
        {
            DocNhapKhoTheoNgay();
        }
      
        
        
        private void Binding_NKNhap(object sender, EventArgs e)//Binding nhập vật tư
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtId.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMaphieunhap.Text= gridView2.GetFocusedRowCellDisplayText(Manhap_grid2);
            dpNgayLap.Text = gridView2.GetFocusedRowCellDisplayText(Ngaynhap_grid2);   
            txtMavatlieu.Text= gridView2.GetFocusedRowCellDisplayText(Mavlphu_grid2);
            txtSonhap.Text= gridView2.GetFocusedRowCellDisplayText(Soluong_grid2);
            txtDonvi.Text= gridView2.GetFocusedRowCellDisplayText(Donvi_grid2);
            txtNhacungcap.Text= gridView2.GetFocusedRowCellDisplayText(Nguoigiao_grid2);
            txtDienGiai.Text= gridView2.GetFocusedRowCellDisplayText(Diengiai_grid2);
            txtTenVatLieu.Text = gridView2.GetFocusedRowCellDisplayText(Tenvatlieuphu_grid2);
            txtNguoiNhan.Text = gridView2.GetFocusedRowCellDisplayText(Nguoinhan_grid2);
            txtNoinhan.Text = gridView2.GetFocusedRowCellDisplayText(Noinhan_grid2);
            txtTemp.Text=gridView2.GetFocusedRowCellDisplayText(Tamnhap_grid2);
            txtXuatTT.Text=gridView2.GetFocusedRowCellDisplayText(Xuattructiep_grid2);
            CbNghiepvu.Text=gridView2.GetFocusedRowCellDisplayText(LoaiNV_grid2);
            txtBPSuDung.Text = gridView2.GetFocusedRowCellDisplayText(Bophansudung_grid2);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();//Tra ve zero
            TruTonKho();//Tru Ton kho
            Vb_XuatTT();//An Chuc Nang xuat truc tiep
        }
        private void Lookup()
        {

        }
        private void BindingEditMavatTu(object sender, EventArgs e)//Binding Danh mục vật tư
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMavatlieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Mavlphu_gl);
            txtTenVatLieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenvlphu_gl);
            NhapXuatTon(); 
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }
        private void TenNCC(object sender, EventArgs e) //Lấy danh sách tên nhà cung cấp khi mã thay đổi
        {
    
        }
        private void GridlookupEditMaVatTu()//Danh mục xuất nhập tồn kho vật tư
        {
            ketnoi Connect = new ketnoi();
            gridLookUpEditVatTu.Properties.DataSource = Connect.laybang("select * from tblDM_VATLIEUPHU");
            gridLookUpEditVatTu.Properties.DisplayMember = "Mavlphu";
            gridLookUpEditVatTu.Properties.ValueMember = "Mavlphu";
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
        /// <summary>
        /// Cập nhật dữ liệu tồn kho cuối kỳ theo từng mã hàng/ không sử dụng cập nhật tồn kho cuối kỳ toàn bộ
        /// </summary>
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATLIEUPHU set TonCuoi=" + txtTonCuoi.Text + " where Mavlphu like N'" + txtMavatlieu.Text + "'");
            kn.dongketnoi();
        }
        private void Ghi(object sender, EventArgs e)//Cập nhật nhập kho vật liệu phụ
        {
            NhapCCDC();
        }

        private void NhapCCDC()//Hàm nhập
        {
            try
            {
                if (txtMavatlieu.Text == "") { MessageBox.Show("Mã phiếu nhập không bỏ rỗng"); return; }
                else if (txtTenVatLieu.Text == "") { MessageBox.Show("Tên vặt tư không bỏ trống", "THÔNG BÁO"); return; }
                else if (txtMaphieunhap.Text == "") { MessageBox.Show("Mã phiếu nhập rỗng", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblNHAP_VATLIEUPHU  (Manhap,Ngaynhap,Mavlphu,Soluong,Dongia,Thanhtien,"
                    + " Donvi,Nguoilap,Nguoigiao,Diengiai,Nguoinhan,Ngayghi,Noinhan,Nhaptam,XuatTT,LoaiNV,BpSuDung) "
                    + " values(@Manhap,@Ngaynhap,@Mavlphu,@Soluong,@Dongia,@Thanhtien, "
                    + " @Donvi,@Nguoilap,@Nguoigiao,@Diengiai,@Nguoinhan,GetDate(),@Noinhan,@Nhaptam,@XuatTT,@LoaiNV,@BpSuDung)", cn);
                    cmd.Parameters.Add(new SqlParameter("@Manhap", SqlDbType.NVarChar)).Value = txtMaphieunhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaynhap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSonhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Dongia", SqlDbType.Float)).Value = txtDongia.Text;
                    cmd.Parameters.Add(new SqlParameter("@Thanhtien", SqlDbType.Float)).Value = txtThanhtien.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoigiao", SqlDbType.NVarChar)).Value = txtNhacungcap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = txtNoinhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nhaptam", SqlDbType.Float)).Value = txtTemp.Text;
                    cmd.Parameters.Add(new SqlParameter("@XuatTT", SqlDbType.Float)).Value = txtXuatTT.Text;
                    cmd.Parameters.Add(new SqlParameter("@LoaiNV", SqlDbType.NVarChar)).Value = CbNghiepvu.Text;
                    cmd.Parameters.Add(new SqlParameter("@BpSuDung", SqlDbType.NVarChar)).Value = txtBPSuDung.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    UpdateNhapKho();//group số lượng nhập kho                   
                    GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    ListNK_NhapKhoThem();//Load lại danh mục nhập kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }
        private void ValueChange_ThucNhap(object Sender, EventArgs e) { ThucNhapKho();}//Sự kiện trừ số xuất sử dụng
        private void ThucNhapKho()//Hàm trừ tạm nhập trừ xuất
        {
            if (txtTemp.Text == "")
            {
                txtTemp.Text = "0";
            }
            if (txtXuatTT.Text == "")
            {
                txtXuatTT.Text = "0";
            }

            try
            {
                double TamNhap = double.Parse(txtTemp.Text);
                double Xuat = double.Parse(txtXuatTT.Text);
                double ThucNhap = TamNhap - Xuat;
                txtSonhap.Text = Convert.ToString(ThucNhap);
                if (double.Parse(txtSonhap.Text)<0)
                {
                    txtSonhap.Text = "0";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Nhập số từ {0...9}","THÔNG BÁO");
            }
            
        }

        private void Sua(object sender, EventArgs e)//Sửa vật liệu phụ nhập kho
        {
            try
            {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblNHAP_VATLIEUPHU "
                    + "set Manhap=@Manhap,Ngaynhap=@Ngaynhap,Mavlphu=@Mavlphu,Soluong=@Soluong,Dongia=@Dongia,Thanhtien=@Thanhtien, "
                    + "Donvi=@Donvi,Nguoilap=@Nguoilap,Nguoigiao=@Nguoigiao,Diengiai=@Diengiai,Nguoinhan=@Nguoinhan,Ngayghi=GetDate(),Noinhan=@Noinhan, "
                    + "Nhaptam=@Nhaptam,XuatTT=@XuatTT,LoaiNV=@LoaiNV,BpSuDung=@BpSuDung "
                    + "where id like @id", cn);
                    cmd.Parameters.Add(new SqlParameter("@id",SqlDbType.NVarChar)).Value=txtId.Text;
                    cmd.Parameters.Add(new SqlParameter("@Manhap", SqlDbType.NVarChar)).Value = txtMaphieunhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaynhap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSonhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Dongia", SqlDbType.Float)).Value = txtDongia.Text;
                    cmd.Parameters.Add(new SqlParameter("@Thanhtien", SqlDbType.Float)).Value = txtThanhtien.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoigiao", SqlDbType.NVarChar)).Value = txtNhacungcap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = txtNoinhan.Text;

                    cmd.Parameters.Add(new SqlParameter("@Nhaptam", SqlDbType.Float)).Value = txtTemp.Text;
                    cmd.Parameters.Add(new SqlParameter("@XuatTT", SqlDbType.Float)).Value = txtXuatTT.Text;
                    cmd.Parameters.Add(new SqlParameter("@LoaiNV", SqlDbType.NVarChar)).Value = CbNghiepvu.Text;
                    cmd.Parameters.Add(new SqlParameter("@BpSuDung", SqlDbType.NVarChar)).Value = txtBPSuDung.Text;

                    cmd.ExecuteNonQuery();
                    cn.Close();
                    UpdateNhapKho();//group số lượng nhập kho
                    GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    ListNK_NhapKhoThem();//Load lại danh mục nhập kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công" + ex, "thông báo");
            }        
        }
        private void Xoa(object sender, EventArgs e)//Xóa vật liệu phụ nhập kho
        {
            UpdateZero();//Trả giá trị xuất hàng hóa về 0
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete from tblNHAP_VATLIEUPHU  where id like " + txtId.Text + "");
            kn.dongketnoi();
            UpdateNhapKho();//group số lượng nhập kho
            GridlookupEditMaVatTu();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
            TruTonKho();//Trừ lấy số tồn kho
            ListNK_NhapKho();//Load lại danh mục nhập kho
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
        }
        private void UpdateZero()//Trả giá trị xuất hàng hóa về 0
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATLIEUPHU set TongNhap=0 where Mavlphu like N'" + txtMavatlieu.Text + "' ");
            kn.dongketnoi();
        }
        void UpdateNhapKho()
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateNhap_VatTuphu", cn);
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
           
        }

        private void BtnExport()
        {
            gridControl2.ShowPrintPreview();
        }

        private void BtnXuatPhieu_Click(object sender, EventArgs e)// Xuất phiếu nhập kho vật tư
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewNhapKho_VTPhu where Manhap like N'" + txtMaphieunhap.Text + "' ");
            XRNhapvatlieuphu RpNhapVTPhu = new XRNhapvatlieuphu();
            RpNhapVTPhu.DataSource = dt;
            RpNhapVTPhu.DataMember = "Table";
            RpNhapVTPhu.CreateDocument(false);
            RpNhapVTPhu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaphieunhap.Text;
            PrintTool tool = new PrintTool(RpNhapVTPhu.PrintingSystem);
            RpNhapVTPhu.ShowPreviewDialog();
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
                if (TonCuoi < 0)
                {
                    txtTonCuoi.Text = "false";
                }
            }
            catch (Exception)
            { MessageBox.Show("Error: {0...9}", "THÔNG BÁO"); }
        }
        private void Mavattu_Change(object sender, EventArgs e)//Thay đổi Mã vật tư
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
            if (txtMavatlieu.Text == "")
            {
                txtTonDau.Text = "0";
                txtNhap.Text = "0";
                txtXuat.Text = "0";
                txtTonCuoi.Text = "0";
            }
        }

        private void DMNhapXuatTon()//Lấy số lượng nhập xuất tồn từ Danh mục vật tư theo mã vật tư
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select isnull(Soluong,0),isnull(TongNhap,0),isnull(TongXuat,0) from tblDM_VATLIEUPHU where Mavlphu like N'" + txtMavatlieu.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTonDau.Text = Convert.ToString(reader[0]);
                    txtNhap.Text = Convert.ToString(reader[1]);
                    txtXuat.Text = Convert.ToString(reader[2]);
                    //txtTonCuoi.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception)
            {
            }
        }
        private void UpdateTonCuoi(object sender, EventArgs e)// Update tồn cuối
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateTonCuoi_VatTuphu", cn);
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

        private void btnDM_VatlieuPhu_Click(object sender, EventArgs e)
        {
            frmDM_VATLIEUPHU fVatLieuPhu = new frmDM_VATLIEUPHU();
            fVatLieuPhu.ShowDialog();
            GridlookupEditMaVatTu();
        }
        private void TinhThanhTien()//Tính thanh tien
        {
            try
            {
                double SL = double.Parse(txtTemp.Text);
                double DG = double.Parse(txtDongia.Text);
                double TT = SL * DG;
                txtThanhtien.Text = Convert.ToString(TT);
            }
            catch{}
        }
        private void Export_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void txtSonhap_TextChanged_1(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtDongia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void CbNghiepvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vb_XuatTT();
            if (CbNghiepvu.Text=="Tồn kho")
            {
                lbSoXuat.Visible = false; txtXuatTT.Visible = false;
            }
        }
        private void Vb_XuatTT()//an chuc nang xuat truc tiep
        {
            if (CbNghiepvu.Text == "Xuất trực tiếp")
            {
                lbSoXuat.Visible = true; txtXuatTT.Visible = true;
            }
            if (CbNghiepvu.Text.CompareTo("") == 0)
            {
                lbSoXuat.Visible = false; txtXuatTT.Visible = false;
            }
        }

        private void BtnThongKeSuDung_Click(object sender, EventArgs e)//Thống kê số tiền mỗi bộ phận sử dụng vật liệu phụ
        {
            ListNK_ThongKe();
            gridView1.ExpandAllGroups();
        }
        private void ListNK_ThongKe()//Lấy danh mục nhập vật tư
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select N.*,DM.Tenvlphu from tblNHAP_VATLIEUPHU N "
            + "left outer join tblDM_VATLIEUPHU DM on DM.Mavlphu=N.Mavlphu where Ngaynhap "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' "
            + " order by SUBSTRING(Manhap,4,6) ASC,SUBSTRING(Manhap,11,3) ASC");
            kn.dongketnoi();
        }

        private void BtnThongke_Click(object sender, EventArgs e)
        {
            gridView1.ShowPrintPreview();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GridlookupEditMaVatTu();
        }

     
    }
}
