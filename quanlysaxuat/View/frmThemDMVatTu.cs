using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;

namespace quanlysanxuat
{
    public partial class frmThemDMvattu : DevExpress.XtraEditors.XtraForm
    {
        public frmThemDMvattu()
        {
            InitializeComponent();
        }

       
        #region Dọc danh mục vật tư tồn kho
        private void ListDM_Vattu()
        {
            ketnoi kn = new ketnoi();
            grDanhMucHangTonKho.DataSource = 
                kn.laybang("select * from viewThoiHanTonKhoVatTu  order by Ngaylap DESC");
            kn.dongketnoi();
        }
        #endregion
        #region Dọc danh mục vật tư tồn kho theo mã vật tư 
        private void ListDM_VattuThem()
        {
            ketnoi kn = new ketnoi();
            grDanhMucHangTonKho.DataSource = 
                kn.laybang("select * from viewThoiHanTonKhoVatTu where Ma_vl like N'" + txtMavattu.Text+"' ");
            kn.dongketnoi();
        }
        #endregion
        #region sự kiện load danh mục vật tư
        private void LoadDM_NCC(object sender, EventArgs e) { ListDM_Vattu(); }
        #endregion
        #region Dọc danh sách nhà cung cấp lên combobox
        private void LoadCbNhaCC()
        {
            ketnoi Connect = new ketnoi();
            cbTenNCC.DataSource = Connect.laybang("select * from tblDM_NCC_VATTU order by Ngaycapnhat_NCC desc");
            cbTenNCC.DisplayMember = "Ten_NCC";
            cbTenNCC.ValueMember = "Ten_NCC";
            Connect.dongketnoi();
        }
        #endregion
        #region Đọc danh sách chất liệu vật tư lên combobox vật liệu
        private void LoadCbVatlieuVT()
        {
            ketnoi kn = new ketnoi();
            cbchatlieu_vattu.DataSource = kn.laybang("select * from tblDMCL_VATTU order by Ngaylap desc");
            cbchatlieu_vattu.DisplayMember = "Tencl_vattu";
            cbchatlieu_vattu.ValueMember = "Tencl_vattu";
            kn.dongketnoi();
        }
        #endregion
        #region đọc danh sách chất liệu vật tư theo mã chất liệu vật tư
        private void MaCLVT(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand("select Macl_vattu from tblDMCL_VATTU where Tencl_vattu like N'" + cbchatlieu_vattu.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaChatlieuvt.Text = Convert.ToString(reader[0]);
            reader.Close(); con.Close(); LayMaVatTu();
        }
        #endregion

        #region Đọc danh sách nhà cung cấp theo mã nhà cung cấp
        private void MaNCC(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand("select Ma_NCC from tblDM_NCC_VATTU where Ten_NCC like N'" + cbTenNCC.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaNCC.Text = Convert.ToString(reader[0]);
            reader.Close(); con.Close(); LayMaVatTu();
        }
        #endregion
     
        #region kiểm tra mã vật tư có tồn tại trong danh mục
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaVatTu = txtMavattu.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Ma_vl from tblDM_VATTU", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaVatTu == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        #endregion 
        #region Thêm vật tư mới danh mục vật tư
        private void ThemDM_VatTu(object sender, EventArgs e)
        {
            try
            {
                Double TonDau = Convert.ToDouble(txtTonDau.Text);
                if (txtMavattu.Text == "")
                { MessageBox.Show("Mã vật tư", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                else if (kiemtratontai())
                { MessageBox.Show("Mã số '" + txtMaNCC.Text + "' đã tồn tại, Không thể thêm mã trùng"); return; }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblDM_VATTU "
                     + "(Mahieu,Ma_vl,Ma_loai,Kich_thuoc_vl, "
                     + "Ten_vat_lieu,Donvi,Donvi_quidoi, "
                     + "Soluong_ton,MaNCC,Nguoilap ,Ngaylap,ViTri,DinhMucTon) "
                     + "values(@Mahieu,@Ma_vl,@Ma_loai,@Kich_thuoc_vl, "
                     + "@Ten_vat_lieu,@Donvi,@Donvi_quidoi, "
                     + "@Soluong_ton,@MaNCC,@Nguoilap,GetDate(),@ViTri,@DinhMucTon)", con);
                    if (txtTonDau.Text == "")
                    {
                    cmd.Parameters.Add(new SqlParameter("@Soluong_ton", SqlDbType.Int)).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(new SqlParameter("@Mahieu", SqlDbType.NVarChar)).Value = txtMahieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_vl", SqlDbType.NVarChar)).Value = txtMavattu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_loai", SqlDbType.NVarChar)).Value = txtMaChatlieuvt.Text;
                    cmd.Parameters.Add(new SqlParameter("@Kich_thuoc_vl", SqlDbType.NVarChar)).Value = txtDacdiemvt.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ten_vat_lieu", SqlDbType.NVarChar)).Value = txtTenvattu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi_quidoi", SqlDbType.NVarChar)).Value = txtDonviquydoi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong_ton", SqlDbType.Float)).Value = TonDau;
                    cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.NVarChar)).Value = txtMaNCC.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = MainDev.username;
                    cmd.Parameters.Add(new SqlParameter("@ViTri", SqlDbType.NVarChar)).Value = txtViTri.Text;
                    cmd.Parameters.Add(new SqlParameter("@DinhMucTon", SqlDbType.Float)).Value = txtDinhMucTonKho.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grDanhMucHangTonKho.DataSource = dt;
                    con.Close(); ListDM_VattuThem();
                }               
            }
            catch
            { MessageBox.Show("Không thành công","Thông báo"); }
        }
        #endregion
        #region
 
   
        #endregion
       
        #region Hiệu Chỉnh Danh sách vật tư
        private void suaDM_VatTu(object sender, EventArgs  e)//Sửa vật tư
        {
            try 
            {
                Double TonDau = Convert.ToDouble(txtTonDau.Text);
                if (MessageBox.Show("Trong nhập xuất sẽ thay đổi", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("update tblDM_VATTU "
                    + "set Mahieu=@Mahieu,Ma_vl=@Ma_vl,Ma_loai=@Ma_loai,Kich_thuoc_vl=@Kich_thuoc_vl "
                    + ",Ten_vat_lieu=@Ten_vat_lieu,Donvi=@Donvi,Donvi_quidoi=@Donvi_quidoi "
                    + ",Soluong_ton=@Soluong_ton,MaNCC=@MaNCC,Nguoilap=@Nguoilap,Ngaylap=GetDate(),TonCuoi=@TonCuoi,ViTri=@ViTri,DinhMucTon=@DinhMucTon "
                    + " where id like " +iD+ "", con);
                    if (txtTonDau.Text == "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@Soluong_ton", SqlDbType.Int)).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(new SqlParameter("@Mahieu", SqlDbType.NVarChar)).Value = txtMahieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_vl", SqlDbType.NVarChar)).Value = txtMavattu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ma_loai", SqlDbType.NVarChar)).Value = txtMaChatlieuvt.Text;
                    cmd.Parameters.Add(new SqlParameter("@Kich_thuoc_vl", SqlDbType.NVarChar)).Value = txtDacdiemvt.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ten_vat_lieu", SqlDbType.NVarChar)).Value = txtTenvattu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi_quidoi", SqlDbType.NVarChar)).Value = txtDonviquydoi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong_ton", SqlDbType.Float)).Value = TonDau;
                    cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.NVarChar)).Value = txtMaNCC.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = MainDev.username;
                    cmd.Parameters.Add(new SqlParameter("@TonCuoi", SqlDbType.Float)).Value = txtTonCuoi.Text;
                    cmd.Parameters.Add(new SqlParameter("@ViTri", SqlDbType.NVarChar)).Value = txtViTri.Text;
                    cmd.Parameters.Add(new SqlParameter("@DinhMucTon", SqlDbType.Float)).Value = txtDinhMucTonKho.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grDanhMucHangTonKho.DataSource = dt;
                    con.Close();
                    UpdateNhapVT();
                    UpdateXuatVT() ;
                    ListDM_VattuThem();
                }
            }
            catch (Exception ex) { throw ex;}
        }
        #endregion
        #region Cập nhật danh sách vật tư Cập nhật thay đổi vật tư lên nhập kho
        private void UpdateNhapVT()//
        {
            ketnoi kn = new ketnoi();
            grDanhMucHangTonKho.DataSource = kn.xulydulieu("update tblNHAP_VATTU set Ma_vat_lieu=N'"+txtMavattu.Text+"', "
            + " Ten_vat_lieu=N'" + txtTenvattu.Text + "',Donvinhap=N'" + txtDonvi.Text + "' where Ma_vat_lieu like N'" + txtMavatucu.Text + "'"); 
            kn.dongketnoi();
        }
        #endregion
        #region Cập nhật thay đổi vật tư lên xuất kho
        private void UpdateXuatVT()//
        {
            ketnoi kn = new ketnoi();
            grDanhMucHangTonKho.DataSource = kn.xulydulieu("update tblXuatKho set Mavattu=N'" + txtMavattu.Text + "', "
            + " Tenvattu=N'" + txtTenvattu.Text + "',Donvi=N'" + txtDonvi.Text + "' where Mavattu like N'" + txtMavatucu.Text + "'");
            kn.dongketnoi();
        }
        #endregion
        #region Xóa vật tư
        private void xoaDM_VatTu(object sender,EventArgs e)//
        {
            if (KiemtraNhapVT()==true)
            {
                MessageBox.Show("Mã tồn tại trong danh sách nhập kho", "Thông báo");return ;               
            }
            else if (KiemtraXuatVT()==true)
            {
                MessageBox.Show("Mã tồn tại trong danh sách xuất kho", "Thông báo");return;
            }
            else
            {
                ketnoi kn = new ketnoi();
                grDanhMucHangTonKho.DataSource = kn.xulydulieu("Delete from tblDM_VATTU where id like " + iD + "");
                kn.dongketnoi();
                ListDM_Vattu();
            }
        }
        #endregion
        #region Kiểm tra xem mã vật tư có tồn tại trong phần nhập xuất hay không nêu có ko cho xóa
        private bool KiemtraNhapVT()//
        {
            bool tatkt = false;
            string Mavl = txtMavattu.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select Ma_vat_lieu from tblNHAP_VATTU", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Mavl == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        #endregion
        #region Kiểm tra xem mã vật tư có tồn tại trong phần nhập xuất hay không nêu có ko cho xóa
        private bool KiemtraXuatVT()//
        {
            bool tatkt = false;
            string Mavl = txtMavattu.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select Mavattu from tblXuatKho", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Mavl == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        #endregion
        #region binding vật tư sử dụng sửa
        string iD;
        private void Binding(object sender,EventArgs e)//
        {
            string Gol = "";
            Gol = gvDanhMucHangTonKho.GetFocusedDisplayText();
            iD= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(id_grid1);
            txtMaNCC.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(MaNCC_grid);
            txtMaChatlieuvt.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Maloai_grid1);
            txtTenvattu.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Tenvatlieu_grid1);
            txtDacdiemvt.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Kichthuocvl_grid1);
            txtMahieu.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Mahieu_grid1);
            txtMavattu.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Mavl_grid1);
            txtTonDau.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(TonDau_grid1);
            txtDonvi.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Donvi_grid1);
            txtDonviquydoi.Text= gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Donviquidoi_grid1);
            txtMavatucu.Text = gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(Mavl_grid1);
            txtNhap.Text = gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(TongNhap_grid1);
            txtXuat.Text = gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(TongXuat_grid1);
            txtTonCuoi.Text = gvDanhMucHangTonKho.GetFocusedRowCellDisplayText(TonCuoi_grid1);
            Gan_zero(); 
            TruTonKho();
        }
        #endregion
        #region Gán 0 khi giá trị nhập xuất Null
        private void Gan_zero()//
        {
            if (txtNhap.Text == "") { txtNhap.Text = "0"; }
            if (txtXuat.Text == "") { txtXuat.Text = "0"; }
        }
        #endregion
        #region Hàm tính tồn kho
        private void TruTonKho()//
        {
            try
            {
                float TonDau = float.Parse(txtTonDau.Text);
                float Nhap = float.Parse(txtNhap.Text);
                float Xuat = float.Parse(txtXuat.Text);
                float TonCuoi = TonDau + Nhap - Xuat;
                txtTonCuoi.Text = Convert.ToString(TonCuoi);
                if (TonCuoi < 0)
                {
                    txtTonCuoi.Text = "Lệch";
                }
            }
            catch (Exception)
            { }
        }
        #endregion
        #region
        private void frmThemNCC_Load(object sender, EventArgs e)
        {
            LoadCbNhaCC(); LoadCbVatlieuVT();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            ListDM_Vattu();
        }
        #endregion
        private void LayMaVatTuChange(object sender, EventArgs e)
        {
            LayMaVatTu();
        }

        #region 
        private void LayMaVatTu()
        {
            string Mavattu = string.Format(@"{0}-{1}-{2}", txtMaNCC.Text, txtMaChatlieuvt.Text,txtMahieu.Text);
            txtMavattu.Text = Mavattu.ToString();
        }
        #endregion
        #region
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            frmThemNCC fNCC = new frmThemNCC();fNCC.ShowDialog(); LoadCbNhaCC();
        }
        #endregion
        #region
        private void btnThemloaivatlieu_Click(object sender, EventArgs e)
        { frmThemDM_vatlieuvt fchatlieuvt = new frmThemDM_vatlieuvt(); fchatlieuvt.ShowDialog(); LoadCbVatlieuVT(); }
        #endregion
        private void Export_Click(object sender, EventArgs e)
        {
            gvDanhMucHangTonKho.ShowPrintPreview();
        }
        #region Sự kiện tồn đầu
        private void txtTonDau_TextChanged(object sender, EventArgs e)//
        {
            Gan_zero(); TruTonKho();
        }
        #endregion

        private void btnTonKhoThang_Click(object sender, EventArgs e)
        {
            THTonKhoTheoThoiGian();
        }
        private async void THTonKhoTheoThoiGian()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"Execute NhapXuatTonKhoVatTuChinhTheoNgay '{0}','{1}'",
                      dptu_ngay.Value.ToString("yyyy-MM-dd"),
                      dpden_ngay.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grDanhMucHangTonKho.DataSource =
                    Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

    }
}