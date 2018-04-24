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
    public partial class frmHangTonXuat : DevExpress.XtraEditors.XtraForm
    {
        public frmHangTonXuat()
        {
            InitializeComponent();
        }
       

        private void XuatKho_Click(object sender, EventArgs e)
        {
            ListXuatKho();
        }
        private void ListXuatKho()//Danh mục Xuất kho
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_xuat where Ngaylap "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by Maphieuxuat ASC");
            kn.dongketnoi();
        }
        private void XuatKhoThem()//So Nhat Ky Nhap kho them
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_xuat where Maphieuxuat like '"+txtMaphieuxuat.Text+"' order by idspxuat ASC");
            kn.dongketnoi();
        }
        private void XuatKhoSua()//So Nhat Ky Nhap kho
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblSanpham_xuat where idspxuat like N'" + txtId.Text + "'");
            kn.dongketnoi();
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
            frmLoading f2 = new frmLoading(gridLookSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void UpdateMaxNgayLap()//Ghi ngày lập lớn nhất vào danh mục 
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("update tblSanpham_tonkho set NgayNhapMax=XK.Ngaylap "
            +" from (select Masp,max(Ngaylap) Ngaylap from tblSanpham_xuat group by Masp) XK "
            +" where XK.Masp = tblSanpham_tonkho.Masp");
            kn.dongketnoi();
        }
        private void GridLookupTonKho()//Danh mục hàng tồn trong kho
        {
            ketnoi Connect = new ketnoi();
            gridLookSanPham.Properties.DataSource = Connect.laybang("select * from tblSanpham_tonkho");
            gridLookSanPham.Properties.DisplayMember = "Masp";
            gridLookSanPham.Properties.ValueMember = "Masp";
            gridLookSanPham.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookSanPham.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookSanPham.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void Binding_XuatKho(object sender, EventArgs e)//Binding xuất vật tư
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtId.Text = gridView2.GetFocusedRowCellDisplayText(idspxuat_grid2);
            txtMaphieuxuat.Text = gridView2.GetFocusedRowCellDisplayText(Maphieu_grid2);
            txtMasanpham.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            dpNgayLap.Text = gridView2.GetFocusedRowCellDisplayText(Ngaylap_grid2);
            txtTensanpham.Text = gridView2.GetFocusedRowCellDisplayText(Sanpham_grid2);
            txtDienGiai.Text = gridView2.GetFocusedRowCellDisplayText(Diengiai_grid2);
            txtSoxuat.Text = gridView2.GetFocusedRowCellDisplayText(Soxuat_grid2);
            txtDonvi.Text = gridView2.GetFocusedRowCellDisplayText(Donvi_grid2);
            txtNguoiNhan.Text = gridView2.GetFocusedRowCellDisplayText(Nguoinhan_grid2);
            txtNoinhan.Text = gridView2.GetFocusedRowCellDisplayText(Noinhan_grid2);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }

        private void LayMaPhieuXuat(object sender, EventArgs e)//Lấy Mã Phiếu Xuất kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'PX-'+CONCAT('',RIGHT(CONCAT('0000',ISNULL(right(max(Maphieuxuat),4),0) + 1),4)) from tblSanpham_xuat", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaphieuxuat.Text = Convert.ToString(reader[0]);
            reader.Close();
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
        private void Them(object sender, EventArgs e) //Ghi dữ liệu xuất kho
        {
            try
            {
                if (txtMasanpham.Text == ""){MessageBox.Show("Mã vật liệu rỗng", "Thông báo");return;}
                else if (txtTensanpham.Text == ""){MessageBox.Show("Tên vật liệu rỗng", "Thông báo");return;}
                else if (txtMaphieuxuat.Text == "") {MessageBox.Show("Ma phiếu xuất rỗng", "Thông báo");return;}
                else if (dpNgayLap.Text == "") { MessageBox.Show("Ngày lập rỗng", "Thông báo"); return;}
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into tblSanpham_xuat "
                    + " (Maphieuxuat,Ngaylap,Nguoilap,Masp,Sanpham,Soxuat,Donvi,Ngayghi,Diengiai,Nguoinhan,Noinhan) "
                    + " values(@Maphieuxuat,@Ngaylap,@Nguoilap,@Masp,@Sanpham,@Soxuat,@Donvi,GetDate(),@Diengiai,@Nguoinhan,@Noinhan)", con);
                    cmd.Parameters.Add(new SqlParameter("@Maphieuxuat", SqlDbType.NVarChar)).Value = txtMaphieuxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Sanpham", SqlDbType.NVarChar)).Value = txtTensanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soxuat", SqlDbType.Float)).Value = txtSoxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = txtNoinhan.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    UpdateXuatKho();//group số lượng xuất kho                   
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                    UpdateMaxNgayLap();//Cap nhat ngay lon nhat ton kho
                    XuatKhoThem();//Load lại danh mục nhập kho
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
                if (txtMasanpham.Text != "" && txtTensanpham.Text != "" && txtMaphieuxuat.Text != "")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("update tblSanpham_xuat "
                    + "set Maphieuxuat=@Maphieuxuat,Ngaylap=@Ngaylap,Nguoilap=@Nguoilap,Masp=@Masp,Sanpham=@Sanpham,"
                    +"Soxuat=@Soxuat,Donvi=@Donvi,Ngayghi=GetDate(),Diengiai=@Diengiai, "
                    + "Nguoinhan=@Nguoinhan,Noinhan=@Noinhan  where idspxuat like @id", con);
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar)).Value = txtId.Text;
                    cmd.Parameters.Add(new SqlParameter("@Maphieuxuat", SqlDbType.NVarChar)).Value = txtMaphieuxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Sanpham", SqlDbType.NVarChar)).Value = txtTensanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soxuat", SqlDbType.Float)).Value = txtSoxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = txtNoinhan.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    UpdateXuatKho();//group số lượng Xuất kho                   
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                    UpdateMaxNgayLap();//Cap nhat ngay lon nhat ton kho
                    XuatKhoSua();//Load lại danh mục nhập kho
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông báo");
            }
        }

        private void Xoa(object sender, EventArgs e)// Xóa dữ liệu xuất kho
        {
            UpdateZero();//Trả giá trị xuất hàng hóa về 0
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete from tblSanpham_xuat  where idspxuat like " + txtId.Text + "");
            kn.dongketnoi();
            UpdateXuatKho();//group số lượng nhập kho
            GridLookupTonKho();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
            TruTonKho();//Trừ lấy số tồn kho
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
            UpdateMaxNgayLap();//Cap nhat ngay lon nhat ton kho
            XuatKhoThem();
        }
        private void UpdateZero()//Trả giá trị xuất hàng hóa về 0
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblSanpham_tonkho set Xuat=0 where Masp like N'" + txtMasanpham.Text + "' ");
            kn.dongketnoi();
        }
        private void Export(object sender, EventArgs e)//Xuất dữ liệu xuất kho
        {
            gridControl2.ShowPrintPreview();
        }

        private void Clear(object sender, EventArgs e)//Xóa dữ liệu trong các ô textbox
        {

        }
        private void UcXUAT_KHOTP_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Login.Username;
            GridLookupTonKho();
            AutoToNhan();
            AutoNguoNhan();
        }

        private void LapPhieuXuat(object sender, EventArgs e)//print phieu xuat kho
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewXuatKhoThanhPham where Maphieuxuat like N'" + txtMaphieuxuat.Text + "'");
            XRXuatThanhPham RPXuatThanhPham = new XRXuatThanhPham();
            RPXuatThanhPham.DataSource = dt;
            RPXuatThanhPham.DataMember = "Table";
            RPXuatThanhPham.CreateDocument(false);
            RPXuatThanhPham.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaphieuxuat.Text;
            PrintTool tool = new PrintTool(RPXuatThanhPham.PrintingSystem);
            RPXuatThanhPham.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void UpdateXuatKho()//Group mã đã xuất kho
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateXuat_Sanpham", cn);
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

        private void NhapXuatTon()//Nếu mã vật liệu rỗng tồn đầu nhập xuất tồn cũng rỗng
        {
            if (txtMasanpham.Text == "")
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
            catch { };
        }

        private void Masanpham_Change(object sender, EventArgs e)//Thay đổi Mã vật tư
        {
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }

        private void UpdateTonCuoi(object sender, EventArgs e)
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
            { MessageBox.Show("Trừ tồn không thành công"); }
        }

        private void btnDMVatlieuPhu(object sender, EventArgs e)//Mở danh mục vật liệu phụ add thông tin
        {
            frmDM_VATLIEUPHU fDMVatTu = new frmDM_VATLIEUPHU();
            fDMVatTu.ShowDialog();
            GridLookupTonKho();
        }
        private void AutoNguoNhan()// Autocomplete người nhận từ dữ liệu đã nhập
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);    
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select Nguoinhan from tblSanpham_xuat where Nguoinhan  <>''", con);
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
        private void AutoToNhan()//Autocomplete Nơi nhận từ dữ liệu đã nhập
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Noinhan from tblXUAT_VATLIEUPHU where Noinhan <>''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtNoinhan.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        private void btnXuatvlphu_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }

        private void gridLookSanPham_EditValueChanged(object sender, EventArgs e)//Binding change mã sản phẩm, tên sản phẩm
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMasanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_gl);
            txtTensanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tensp_gl);

            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }

        private void BtnSanpham_tonkho_Click(object sender, EventArgs e)
        {
            frmDM_HangTon fHangTon = new frmDM_HangTon();
            fHangTon.ShowDialog();
            GridLookupTonKho();

        }
    }
}
