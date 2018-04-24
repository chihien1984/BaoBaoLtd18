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
using DevExpress.ClipboardSource.SpreadsheetML;

namespace quanlysanxuat
{
    public partial class frmDM_VATLIEUPHU : DevExpress.XtraEditors.XtraForm
    {
        public frmDM_VATLIEUPHU()
        {
            InitializeComponent();
        }

        private void LisDM_VLPhu()
        {
            //ketnoi kn = new ketnoi();
            //gridControl1.DataSource = kn.laybang("select * from tblDM_VATLIEUPHU order by Mavlphu asc");
            //kn.dongketnoi();
            TheHienDMVatTuPhuTheoNgay();
        }
        private void TheHienDMVatTuPhuTheoNgay()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("TonKhoVatTuPhuTheoThoiGian_proc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = dpTu.Value.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = dpDen.Value.ToString("yyyy-MM-dd");
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            con.Close();
            xtraTabControl1.SelectedTabPage = xtraTabPageDanhMuc;
        } 
        private void BinDing(object sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            //txtid.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            //txtMavlphu.Text = gridView1.GetFocusedRowCellDisplayText(Mavl_grid1);
            //txtMaCu.Text = gridView1.GetFocusedRowCellDisplayText(Mavl_grid1);
            //txtTenvlphu.Text = gridView1.GetFocusedRowCellDisplayText(Tenvatlieu_grid1);
            //txtDonvi.Text = gridView1.GetFocusedRowCellDisplayText(Donvi_grid1);
            //txtTonDau.Text = gridView1.GetFocusedRowCellDisplayText(tondau_grid1)==""?"0":
            //    gridView1.GetFocusedRowCellDisplayText(tondau_grid1);
            //txtNhap.Text = gridView1.GetFocusedRowCellDisplayText(Tongnhap_grid1);
            //txtXuat.Text=gridView1.GetFocusedRowCellDisplayText(Tongxuat_grid1);
            //txtTonCuoi.Text = gridView1.GetFocusedRowCellDisplayText(Toncuoi_grid1);
            Gan_zero();
            TruTonKho();
        }
        private void ListDMVATTU(object sender, EventArgs e)
        {
            TheHienDMVatTuPhuTheoNgay();
        }
        private void Them(object sender,EventArgs e) 
        {
            //double tondau = Double.Parse(txtTonDau.Text);
            //double nhap = Double.Parse(txtNhap.Text);
            //double xuat = Double.Parse(txtXuat.Text);
            //double toncuoi = Double.Parse(txtTonCuoi.Text);

            //try
            //{
            //    {
            //        if (txtMavlphu.Text == "") { MessageBox.Show("Mã không bỏ rỗng"); return; }
            //        else if (txtTenvlphu.Text == "") { MessageBox.Show("Tên vặt tư không bỏ trống"); return; }
            //        else if (kiemtratontai()) { MessageBox.Show("Trùng mã"); return;}
            //        {
            //            SqlConnection cn = new SqlConnection(Connect.mConnect);
            //            cn.Open();
            //            SqlCommand cmd = new SqlCommand("insert into tblDM_VATLIEUPHU (Mavlphu,Tenvlphu,Soluong,Donvi,Nguoilap,Ngaylap,Toncuoi,ViTri) "
            //            + "values(@Mavlphu,@Tenvlphu,@Soluong,@Donvi,@Nguoilap,GetDate(),@Toncuoi,@ViTri)", cn);
            //            cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value =txtMavlphu.Text;       
            //            cmd.Parameters.Add(new SqlParameter("@Tenvlphu", SqlDbType.NVarChar)).Value =txtTenvlphu.Text;
            //            cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = tondau;
            //            cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
            //            cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
            //            cmd.Parameters.Add(new SqlParameter("@Toncuoi", SqlDbType.Float)).Value = txtTonCuoi.Text;
            //            cmd.Parameters.Add(new SqlParameter("@ViTri", SqlDbType.NVarChar)).Value = txtViTri.Text;

            //            cmd.ExecuteNonQuery();
            //            cn.Close();
            //            LisDM_VLPhu();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Không thành công lý do" + ex);
            //}
        }
        //private bool kiemtratontai()// kiểm tra tồn tại mã
        //{
        //    //bool tatkt = false;
        //    //string MaVatTu = txtMavlphu.Text;
        //    //SqlConnection con = new SqlConnection(Connect.mConnect);
        //    //con.Open();
        //    //SqlCommand cmd = new SqlCommand("select Mavlphu from tblDM_VATLIEUPHU", con);
        //    //SqlDataReader dr = cmd.ExecuteReader();
        //    //while (dr.Read())
        //    //{
        //    //    if (MaVatTu == dr.GetString(0))
        //    //    {
        //    //        tatkt = true;
        //    //        break;
        //    //    }
        //    //}
        //    //con.Close();
        //    //return tatkt;
        //}
        private void Sua(object sender, EventArgs e)
        {
            //double tondau = Double.Parse(txtTonDau.Text);
            //double nhap = Double.Parse(txtNhap.Text);
            //double xuat = Double.Parse(txtXuat.Text);
            //double toncuoi = Double.Parse(txtTonCuoi.Text);
            //try
            //{
            //    SqlConnection cn = new SqlConnection(Connect.mConnect);
            //    cn.Open();
            //    SqlCommand cmd = new SqlCommand("update tblDM_VATLIEUPHU set "
            //           +" Mavlphu=@Mavlphu,Tenvlphu=@Tenvlphu,Soluong=@Soluong, "
            //           +" Donvi=@Donvi,Nguoilap=@Nguoilap, "
            //           +" Ngaylap=GetDate(),Toncuoi=@Toncuoi,ViTri= @ViTri where id like @id", cn);
            //    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = txtid.Text;
            //    cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value=txtMavlphu.Text;
            //    cmd.Parameters.Add(new SqlParameter("@Tenvlphu", SqlDbType.NVarChar)).Value=txtTenvlphu.Text;
            //    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value= tondau;
            //    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value=txtDonvi.Text;
            //    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value=txtUser.Text;
            //    cmd.Parameters.Add(new SqlParameter("@Toncuoi", SqlDbType.Float)).Value = toncuoi;
            //    cmd.Parameters.Add(new SqlParameter("@ViTri", SqlDbType.NVarChar)).Value = txtViTri.Text;
            //    cmd.ExecuteNonQuery();
            //    cn.Close();
            //    LisDM_VLPhu(); UpdateNhapVT(); UpdateXuatVT();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Không thành công lý do" + ex);
            //}
        }
        private void UpdateNhapVT()//Cập nhật thay đổi vật tư lên nhập kho
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("update tblNHAP_VATLIEUPHU set Mavlphu=N'" + txtMavlphu.Text + "', "
            //+ " Donvi=N'" + txtDonvi.Text + "' where Mavlphu like N'" + txtMaCu.Text + "'");
            //kn.dongketnoi();
        }
        private void UpdateXuatVT()//Cập nhật thay đổi vật tư lên xuất kho
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("update tblXUAT_VATLIEUPHU set Mavlphu=N'" + txtMavlphu.Text + "', "
            //+ " Donvi=N'" + txtDonvi.Text + "' where Mavlphu like N'" + txtMaCu.Text + "'");
            //kn.dongketnoi();
        }
        //private void Xoa(object sender, EventArgs e)
        //{
        //    if (KiemtraNhapVatTuPhu()==true)
        //    {
        //        MessageBox.Show("Mã tồn tại trong danh sách nhập kho", "Thông báo"); return;
        //    }
        //    else if (KiemtraXuatVatTuPhu()==true)
        //    {
        //        MessageBox.Show("Mã tồn tại trong danh sách xuất kho", "Thông báo"); return;
        //    }
        //    else
        //    try
        //    {
        //        SqlConnection cn = new SqlConnection(Connect.mConnect);
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand("delete from tblDM_VATLIEUPHU where id like @id",cn);
        //        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = txtid.Text;
        //        cmd.ExecuteNonQuery();
        //        cn.Close();
        //        //LisDM_VLPhu();
        //            XoaXuat();
        //            XoaNhap();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Không thành công");
        //    }
        //}
        #region Kiểm tra xem mã vật tư có tồn tại trong phần nhập xuất hay không nêu có ko cho xóa
        //private bool KiemtraNhapVatTuPhu()//
        //{
        //    //bool tatkt = false;
        //    //string Mavl = txtMavlphu.Text;
        //    //SqlConnection con = new SqlConnection(Connect.mConnect);
        //    //SqlCommand cmd = new SqlCommand("select Mavlphu from tblNHAP_VATLIEUPHU", con);
        //    //con.Open();
        //    //SqlDataReader dr = cmd.ExecuteReader();
        //    //while (dr.Read())
        //    //{
        //    //    if (Mavl == dr.GetString(0))
        //    //    {
        //    //        tatkt = true;
        //    //        break;
        //    //    }
        //    //}
        //    //con.Close();
        //    //return tatkt;
        //}
        #endregion
        #region Kiểm tra xem mã vật tư có tồn tại trong phần nhập xuất hay không nêu có ko cho xóa
        //private bool KiemtraXuatVatTuPhu()//
        //{
        //    //bool tatkt = false;
        //    //string Mavl = txtMavlphu.Text;
        //    //SqlConnection con = new SqlConnection(Connect.mConnect);
        //    //SqlCommand cmd = new SqlCommand("select Mavlphu from tblXUAT_VATLIEUPHU", con);
        //    //con.Open();
        //    //SqlDataReader dr = cmd.ExecuteReader();
        //    //while (dr.Read())
        //    //{
        //    //    if (Mavl == dr.GetString(0))
        //    //    {
        //    //        tatkt = true;
        //    //        break;
        //    //    }
        //    //}
        //    //con.Close();
        //    //return tatkt;
        //}
        #endregion
        private void XoaNhap()//Cập nhật thay đổi vật tư lên nhập kho
        {
            //ketnoi kn = new ketnoi();
            //int kq= kn.xulydulieu("delete from tblNHAP_VATLIEUPHU where Mavlphu like  '"+txtMaCu.Text+"'");
            //kn.dongketnoi();
        }
        private void XoaXuat()//Cập nhật thay đổi vật tư lên xuất kho
        {
            //ketnoi kn = new ketnoi();
            //int kq = kn.xulydulieu("delete from tblXUAT_VATLIEUPHU where Mavlphu like  '" + txtMaCu.Text + "'");
            //kn.dongketnoi();
        }
        private void Expo(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
        private void TruTonKho()//Hàm tính tồn kho
        {
            //try
            //{
            //    float TonDau = float.Parse(txtTonDau.Text);
            //    float Nhap = float.Parse(txtNhap.Text);
            //    float Xuat = float.Parse(txtXuat.Text);
            //    float TonCuoi = TonDau + Nhap - Xuat;
            //    txtTonCuoi.Text = Convert.ToString(TonCuoi);
            //    if (TonCuoi < 0)
            //    {
            //        txtTonCuoi.Text = "Lệch";
            //    }
            //}
            //catch (Exception)
            //{ }
        }
        private void Gan_zero()//Gán 0 khi giá trị nhập xuất Null
        {
            //if (txtNhap.Text == "") { txtNhap.Text = "0"; }
            //if (txtXuat.Text == "") { txtXuat.Text = "0"; }
        }
        private void txtTonDau_TextChanged(object sender, EventArgs e)//Trừ tồn kho khi số lượng tồn đầu thay đổi
        {
            Gan_zero(); TruTonKho();
        }
        #region formload
        private void frmDM_VATLIEUPHU_Load(object sender, EventArgs e)// Form load
        {
            //txtUser.Text = Login.Username;
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.ToString();
            //if (Login.role== "2041"|| Login.role == "1")
            //{
            //    btnThem.Enabled = true; btnSua.Enabled = true; btnXoa.Enabled = true;
            //}
            //LisDM_VLPhu();
            TheHienDMVatTuPhuTheoNgay();
            TheHienChiTietNhap();
            TheHienChiTietXuat();
        }
        #endregion
        private void BtnChiTietNhap_Click(object sender, EventArgs e)
        {
            TheHienChiTietNhap();
        }
        private void TheHienChiTietNhap()
        {
         

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("NhapVatLieuPhu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = dpTu.Value.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = dpDen.Value.ToString("yyyy-MM-dd");
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            con.Close();
            xtraTabControl1.SelectedTabPage = xtraTabPageNhap;
        }

        private void BtnChiTietXuat_Click(object sender, EventArgs e)
        {
            TheHienChiTietXuat();
        }
        private void TheHienChiTietXuat()
        {
      

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("XuatVatLieuPhu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = dpTu.Value.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = dpDen.Value.ToString("yyyy-MM-dd");
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl3.DataSource = dt;
            con.Close();
            xtraTabControl1.SelectedTabPage = xtraTabPageXuat;
        }

        private void BtnExNhap_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void BtnXuat_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
