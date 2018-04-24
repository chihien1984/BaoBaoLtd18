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
using System.IO;
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class frmHangTonNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmHangTonNhap()
        {
            InitializeComponent();
        }
       
        private void LayManhapkho()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'PN-'+CONCAT('',RIGHT(CONCAT('0000',ISNULL(right(max(Maphieunhap),4),0) + 1),4)) from tblSanpham_nhap", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaphieunhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void Phieunhap(object sender, EventArgs e)
        {
            LayManhapkho();
        }
        private void UpdateMaxNgayLap()//Ghi ngày lập lớn nhất vào danh mục 
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("update tblSanpham_tonkho set NgayNhapMax=NK.Ngaylap "
               +" from (select Masp,max(Ngaylap) Ngaylap from tblSanpham_nhap group by Masp) NK "
               +" where NK.Masp = tblSanpham_tonkho.Masp");
            kn.dongketnoi();
        }
        private void ListNK_NhapKho()//So Nhat Ky Nhap kho
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_nhap where Ngaylap "
            + " between '" + dptu_ngay.Value.ToString("MM/dd/yyyy") + "' and '" + dpden_ngay.Value.ToString("MM/dd/yyyy") + "' order by Maphieunhap ASC");
            kn.dongketnoi();
        }
        private void NhapKhoThem()//So Nhat Ky Nhap kho them
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_nhap where Maphieunhap like N'"+txtMaphieunhap.Text+"' order by idspnhap ASC");
            kn.dongketnoi(); gridView2.ExpandAllGroups();
        }
        private void NhapKhoSua()//So Nhat Ky Nhap kho
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_nhap where idspnhap like N'" +txtId.Text+ "'");
            kn.dongketnoi(); gridView2.ExpandAllGroups();
        }
        private void ListDMNhapKho(object sender, EventArgs e)//Danh mục vật tư nhập kho
        {
            ListNK_NhapKho(); gridView2.ExpandAllGroups();
        }

        private void Tinhtylenhap()//tinh ty le nhap kho
        {
            try
            {
                float SoNhap = float.Parse(txtSonhap.Text);
                float SLDH = float.Parse(txtSLDH.Text);
                float Tyle = SoNhap / SLDH * 100;
                txttyle.Text = Convert.ToString(Tyle);
            }
            catch (Exception)
            {
               
            }
        }

        private void txtSLDH_TextChanged(object sender, EventArgs e)
        {
            Tinhtylenhap();
        }
      
        private void UCNhapKho_TP_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Login.Username;
            GridlookupEditSP();
            gridView2.ExpandAllGroups();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, gridLookSanPham.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void btnLayOut_Sanpham(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void Binding_NKNhap(object sender, EventArgs e)//Binding nhập vật tư
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtId.Text = gridView2.GetFocusedRowCellDisplayText(idspnhap_grid);
            txtMaphieunhap.Text= gridView2.GetFocusedRowCellDisplayText(Maphieunhap_grid);
            txtMasanpham.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid);
            txtSanpham.Text= gridView2.GetFocusedRowCellDisplayText(Tensanpham_grid);
            txtSonhap.Text= gridView2.GetFocusedRowCellDisplayText(Sonhap_grid);
            txtDonvi.Text= gridView2.GetFocusedRowCellDisplayText(Donvi_grid);
            txtDienGiai.Text= gridView2.GetFocusedRowCellDisplayText(Diengiai_grid);
            dpNgayLap.Text = gridView2.GetFocusedRowCellDisplayText(Ngaylap_grid);
            txtSLDH.Text = gridView2.GetFocusedRowCellDisplayText(soluongdh);
            txttyle.Text = gridView2.GetFocusedRowCellDisplayText(Tyle_grid1);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }
        private void Lookup()
        {

        }
        private void BindingEdit(object sender, EventArgs e)//Binding Danh mục vật tư
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMasanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_gl);
            txtSanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tensp_gl);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }
        private void TenNCC(object sender, EventArgs e) //Lấy danh sách tên nhà cung cấp khi mã thay đổi
        {

        }
        private void GridlookupEditSP()//Danh mục xuất nhập tồn kho sản phẩm
        {
            ketnoi Connect = new ketnoi();
            gridLookSanPham.Properties.DataSource = Connect.laybang("select * from tblSanpham_tonkho order by idtpnk desc");
            gridLookSanPham.Properties.DisplayMember = "Masp";
            gridLookSanPham.Properties.ValueMember = "Masp";
            gridLookSanPham.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookSanPham.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookSanPham.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void btnDMVatTu_Click(object sender, EventArgs e)
        {
            frmThemDMvattu fDMVatTu = new frmThemDMvattu();
            fDMVatTu.ShowDialog(); GridlookupEditSP();
        }
        /// <summary>
        /// Cập nhật dữ liệu tồn kho cuối kỳ theo từng mã hàng/ không sử dụng cập nhật tồn kho cuối kỳ toàn bộ
        /// </summary>
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblSanpham_tonkho set TonCuoi=" + txtTonCuoi.Text + " where Masp like N'" + txtMasanpham.Text + "'");
            kn.dongketnoi();
        }
        private void Ghi(object sender, EventArgs e)//Ghi vật liệu phụ nhập kho
        {
            try
            {
                double SLDH = double.Parse(txtSLDH.Text);
                if (txtMaphieunhap.Text == "") { MessageBox.Show("Vui lòng chọn phiếu nhập","THÔNG BÁO"); return; }
                else if (txtMasanpham.Text == "") { MessageBox.Show("Vui lòng chọn mã sản phẩm", "THÔNG BÁO"); return; }
                else if (dpNgayLap.Text == "") { MessageBox.Show("Vui lòng chọn ngày lập","THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect); cn.Open();      
                    SqlCommand cmd = new SqlCommand("insert into tblSanpham_nhap  (Maphieunhap,Ngaylap,Masp,Tensanpham,Sonhap,Donvi,Nguoilap,Ngayghi,Diengiai,SLDonhang,Tyle) "
                    + " values(@Maphieunhap,@Ngaylap,@Masp,@Tensanpham,@Sonhap,@Donvi,@Nguoilap,GetDate(),@Diengiai,@SLDonhang,@Tyle)", cn);
                    cmd.Parameters.Add(new SqlParameter("@Maphieunhap", SqlDbType.NVarChar)).Value = txtMaphieunhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tensanpham", SqlDbType.NVarChar)).Value = txtSanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Sonhap", SqlDbType.NVarChar)).Value = txtSonhap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@SLDonhang", SqlDbType.NVarChar)).Value = SLDH;
                    cmd.Parameters.Add(new SqlParameter("@Tyle", SqlDbType.Float)).Value = txttyle.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    UpdateNhapKho();//group số lượng nhập kho                   
                    GridlookupEditSP();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                    UpdateMaxNgayLap();//Cap nhat ngay ton kho cao nhat
                    NhapKhoThem();//Load lại danh mục nhập kho
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }
        private void Sua(object sender, EventArgs e)//Sửa vật liệu phụ nhập kho
        {
            try
            {
                double SLDH = double.Parse(txtSLDH.Text);
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                SqlCommand cmd = new SqlCommand("update tblSanpham_nhap "
                +" set Maphieunhap=@Maphieunhap,Ngaylap=@Ngaylap,Masp=@Masp,Tensanpham=@Tensanpham, "
                +" Sonhap=@Sonhap,Donvi=@Donvi,Nguoilap=@Nguoilap,Ngayghi=GetDate(),Diengiai=@Diengiai,SLDonhang=@SLDonhang,Tyle=@Tyle  where idspnhap like @idspnhap", cn);
                cmd.Parameters.Add(new SqlParameter("@idspnhap", SqlDbType.BigInt)).Value = txtId.Text;
                cmd.Parameters.Add(new SqlParameter("@Maphieunhap", SqlDbType.NVarChar)).Value = txtMaphieunhap.Text;
                cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasanpham.Text;
                cmd.Parameters.Add(new SqlParameter("@Tensanpham", SqlDbType.NVarChar)).Value = txtSanpham.Text;
                cmd.Parameters.Add(new SqlParameter("@Sonhap", SqlDbType.Float)).Value = txtSonhap.Text;
                cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                cmd.Parameters.Add(new SqlParameter("@SLDonhang", SqlDbType.NVarChar)).Value = SLDH;
                cmd.Parameters.Add(new SqlParameter("@Tyle", SqlDbType.Float)).Value = txttyle.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                UpdateNhapKho();//group số lượng nhập kho
                GridlookupEditSP();//Load danh mục xuất nhập tồn
                DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                TruTonKho();//Trừ lấy số tồn kho
                UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                UpdateMaxNgayLap();//Cap nhat ngay ton kho cao nhat
                NhapKhoSua();//Load lại danh mục nhập kho
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }
        private void Xoa(object sender, EventArgs e)//Xóa vật liệu phụ nhập kho
        {
            UpdateZero();//Trả giá trị xuất hàng hóa về 0
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete from tblSanpham_nhap  where idspnhap like " + txtId.Text + "");
            kn.dongketnoi();
            UpdateNhapKho();//group số lượng nhập kho
            GridlookupEditSP();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh tồn kho
            TruTonKho();//Trừ lấy số tồn kho
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
            UpdateMaxNgayLap();//Cập nhật ngày tồn kho mới nhất
            NhapKhoThem();//Load lại danh mục nhập kho
        }
        #region Trả giá trị xuất hàng hóa về 0 trước khi xóa để tồn kho đúng
        private void UpdateZero()
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblSanpham_nhap set TongNhap=0 where Mavlphu like N'" + txtMasanpham.Text + "' ");
            kn.dongketnoi();
        }
        #endregion
        #region Cập nhật tổng số lượng nhập từ bảng nhập vào Danh mục tồn kho (Nhập - Xuất - Tồn)
        private void UpdateNhapKho()
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateNhap_Sanpham", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
        }
        #endregion
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
            dt = kn.laybang("select * from ViewNhapKhoThanhPham where Maphieunhap like N'" + txtMaphieunhap.Text + "' ");
            XRNhapThanhPham RPNhapThanhPham = new XRNhapThanhPham();
            RPNhapThanhPham.DataSource = dt;
            RPNhapThanhPham.DataMember = "Table";
            RPNhapThanhPham.CreateDocument(false);
            RPNhapThanhPham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaphieunhap.Text;
            PrintTool tool = new PrintTool(RPNhapThanhPham.PrintingSystem);
            RPNhapThanhPham.ShowPreviewDialog();
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
                    txtTonCuoi.Text = "Lệch";
                }
            }
            catch (Exception)
            { }
        }
        private void Masanpham_Change(object sender, EventArgs e)//Thay đổi Mã vật tư
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
            if (txtMasanpham.Text == "")
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
                cmd = new SqlCommand("select isnull(Tondau,0),isnull(Nhap,0),isnull(Xuat,0),isnull(Quydoi,0),isnull(Donvi,0) from tblSanpham_tonkho where Masp like N'" + txtMasanpham.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTonDau.Text = Convert.ToString(reader[0]);
                    txtNhap.Text = Convert.ToString(reader[1]);
                    txtXuat.Text = Convert.ToString(reader[2]);
                    txtQuyDoi.Text = Convert.ToString(reader[3]);
                    txtDvTonDau.Text = Convert.ToString(reader[4]);
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
                SqlCommand queryCM = new SqlCommand("UpdateTonCuoi_SanPham", cn);
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

        }

        private void btnDM_VatlieuPhu_Click(object sender, EventArgs e)
        {
            frmDM_VATLIEUPHU fVatLieuPhu = new frmDM_VATLIEUPHU();
            fVatLieuPhu.ShowDialog();
            GridlookupEditSP();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void btnDM_Hangtonkho_Click(object sender, EventArgs e)
        {
            frmDM_HangTon fHangTon = new frmDM_HangTon();
            fHangTon.ShowDialog();
            GridlookupEditSP();
        }
    }
}
