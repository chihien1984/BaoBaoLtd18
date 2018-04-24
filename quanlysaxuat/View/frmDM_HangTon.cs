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
    public partial class frmDM_HangTon : DevExpress.XtraEditors.XtraForm
    {
        public frmDM_HangTon()
        {
            InitializeComponent();
        }
        private void LisDM_TPNK()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from viewThoiHanTonKho where ngaylap between "
            +" '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, GLookEditSanpham.Text);
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
            frmLoading f2 = new frmLoading(GLookEditSanpham.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void BinDing(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtid.Text = gridView1.GetFocusedRowCellDisplayText(idtpnk_grid1);
            txtMatonkho.Text = gridView1.GetFocusedRowCellDisplayText(maspctton_grid1);
            txtTenSP.Text = gridView1.GetFocusedRowCellDisplayText(tensp_grid1);
            txtTonDau.Text = gridView1.GetFocusedRowCellDisplayText(tondau_grid1);
            txtNhap.Text=gridView1.GetFocusedRowCellDisplayText(nhap_grid1);
            txtXuat.Text=gridView1.GetFocusedRowCellDisplayText(Xuat_grid1);
            txtTonCuoi.Text = gridView1.GetFocusedRowCellDisplayText(toncuoi_grid1);
            txtDonvi.Text = gridView1.GetFocusedRowCellDisplayText(donvi_grid1);
            txttenKhachhang.Text = gridView1.GetFocusedRowCellDisplayText(tenkhach_grid1);
            txtMakhach.Text = gridView1.GetFocusedRowCellDisplayText(Makhach_grid1);
            txtViTri.Text = gridView1.GetFocusedRowCellDisplayText(vitri_grid1);
            txtTrangThai.Text=gridView1.GetFocusedRowCellDisplayText(Makhach_grid1);
            txtQuyDoi.Text = gridView1.GetFocusedRowCellDisplayText(Quydoi_grid1);
            txtMacu.Text = gridView1.GetFocusedRowCellDisplayText(maspctton_grid1);
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            dpNgayLuKho.Text = gridView1.GetFocusedRowCellDisplayText(Maxngaynhap_grid1);
            Gan_zero(); TruTonKho();
        }
        private void ListDM_TPNK(object sender, EventArgs e) { LisDM_TPNK(); }
        private void Them(object sender, EventArgs e)
        {
            try
            {
                {
                    if (txtMatonkho.Text == "") { MessageBox.Show("Mã không bỏ rỗng"); return; }
                    else if (txtTenSP.Text == "") { MessageBox.Show("Tên sản phẩm không bỏ trống"); return; }
                    else if (kiemtratontai()) { MessageBox.Show("Trùng mã"); return; }
                    {
                        SqlConnection cn = new SqlConnection(Connect.mConnect);
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("insert into tblSanpham_tonkho (Masp,Tensp,Donvi,ngaylap,Nguoilap,Tondau,Makhach,Tenkhach,Vitri,Trangthai,Quydoi,Mact,Toncuoi,NgayNhapMax) "
                        + "values(@Masp,@Tensp,@Donvi,GetDate(),@Nguoilap,@Tondau,@Makhach,@Tenkhach,@Vitri,@Trangthai,@Quydoi,@Mact,@Toncuoi,@NgayNhapMax)", cn);
                        cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMatonkho.Text;
                        cmd.Parameters.Add(new SqlParameter("@Tensp", SqlDbType.NVarChar)).Value =  txtTenSP.Text;
                        cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                        cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = username;
                        cmd.Parameters.Add(new SqlParameter("@Tondau", SqlDbType.Float)).Value = txtTonDau.Text;
                        cmd.Parameters.Add(new SqlParameter("@Makhach", SqlDbType.NVarChar)).Value = txtMakhach.Text;
                        cmd.Parameters.Add(new SqlParameter("@Tenkhach", SqlDbType.NVarChar)).Value = txttenKhachhang.Text;
                        cmd.Parameters.Add(new SqlParameter("@Vitri", SqlDbType.NVarChar)).Value = txtViTri.Text;
                        cmd.Parameters.Add(new SqlParameter("@Trangthai", SqlDbType.NVarChar)).Value = txtTrangThai.Text;
                        cmd.Parameters.Add(new SqlParameter("@Quydoi", SqlDbType.Float)).Value = txtQuyDoi.Text;
                        cmd.Parameters.Add(new SqlParameter("@Mact", SqlDbType.NVarChar)).Value = txtMasp.Text;
                        cmd.Parameters.Add(new SqlParameter("@Toncuoi", SqlDbType.Float)).Value = txtTonCuoi.Text;
                        if (dpNgayLuKho.Text==null)
                        {
                            cmd.Parameters.Add(new SqlParameter("@NgayNhapMax", SqlDbType.Date)).Value = DBNull.Value;  
                        }
                        cmd.Parameters.Add(new SqlParameter("@NgayNhapMax", SqlDbType.Date)).Value = dpNgayLuKho.Text;  
                        cmd.ExecuteNonQuery();
                        LisDM_TPNK();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công");
            }
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string Masp = txtMatonkho.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Masp from tblSanpham_tonkho where Masp is not null", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Masp == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool KiemtratontaiNhap()
        {
            bool tatkt = false;
            string Masp = txtMatonkho.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Masp from tblSanpham_nhap", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Masp == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool KiemthatontaiXuat()
        {
            bool tatkt = false;
            string Masp = txtMatonkho.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Masp from tblSanpham_xuat", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Masp == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private void Sua(object sender, EventArgs e)
        {
            if (KiemtratontaiNhap())
            {
                MessageBox.Show("Sản phẩm tồn tại sổ nhập kho");
            }
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                SqlCommand cmd = new SqlCommand("update tblSanpham_tonkho set Masp=@Masp,Tensp=@Tensp,Donvi=@Donvi,ngaylap=GetDate(), "
                +" Nguoilap=@Nguoilap,Tondau=@Tondau,Makhach=@Makhach,Tenkhach=@Tenkhach,Vitri=@Vitri, "
                + " Trangthai=@Trangthai,Quydoi=@Quydoi,Mact=@Mact,Toncuoi=@Toncuoi,NgayNhapMax=@NgayNhapMax where idtpnk like @idtpnk", cn);
                cmd.Parameters.Add(new SqlParameter("@idtpnk", SqlDbType.BigInt)).Value = txtid.Text;
                cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMatonkho.Text;
                cmd.Parameters.Add(new SqlParameter("@Tensp", SqlDbType.NVarChar)).Value = txtTenSP.Text;
                cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = username;
                cmd.Parameters.Add(new SqlParameter("@Tondau", SqlDbType.Float)).Value = txtTonDau.Text;
                cmd.Parameters.Add(new SqlParameter("@Makhach", SqlDbType.NVarChar)).Value = txtMakhach.Text;
                cmd.Parameters.Add(new SqlParameter("@Tenkhach", SqlDbType.NVarChar)).Value = txttenKhachhang.Text;
                cmd.Parameters.Add(new SqlParameter("@Vitri", SqlDbType.NVarChar)).Value = txtViTri.Text;
                cmd.Parameters.Add(new SqlParameter("@Trangthai", SqlDbType.NVarChar)).Value = txtTrangThai.Text;
                cmd.Parameters.Add(new SqlParameter("@Quydoi", SqlDbType.Float)).Value = txtQuyDoi.Text;
                cmd.Parameters.Add(new SqlParameter("@Mact", SqlDbType.NVarChar)).Value = txtMasp.Text;
                cmd.Parameters.Add(new SqlParameter("@Toncuoi", SqlDbType.Float)).Value = txtTonCuoi.Text;
                if (dpNgayLuKho.Text == "")
                {
                    cmd.Parameters.Add(new SqlParameter("@NgayNhapMax", SqlDbType.Date)).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@NgayNhapMax", SqlDbType.Date)).Value = dpNgayLuKho.Text;  
                }
                cmd.ExecuteNonQuery();
                cn.Close();
                UpdateNhapSanPham();
                UpdateXuatSanPham();
                LisDM_TPNK();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công "+ex);
            }
        }


        private void UpdateNhapSanPham()//Cập nhật thay đổi vật tư nhập kho
        {
            ketnoi kn = new ketnoi();
            int kq=kn.xulydulieu("update tblSanpham_nhap set Masp=N'" + txtMatonkho.Text + "', "
            + " Tensanpham=N'" + txtTenSP.Text + "',Donvi=N'" + txtDonvi.Text + "' where Masp like N'" + txtMacu.Text + "'");
            kn.dongketnoi();
        }
        private void UpdateXuatSanPham()//Cập nhật thay đổi vật tư xuất kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblSanpham_xuat set Masp=N'" + txtMatonkho.Text + "', "
            + " Sanpham=N'" + txtTenSP.Text + "',Donvi=N'" + txtDonvi.Text + "' where Masp like N'" + txtMacu.Text + "'");
            kn.dongketnoi();
        }
        private void Xoa(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                SqlCommand cmd = new SqlCommand("delete from tblSanpham_tonkho where idtpnk like @idtpnk", cn);
                cmd.Parameters.Add(new SqlParameter("@idtpnk", SqlDbType.BigInt)).Value = txtid.Text;
                cmd.ExecuteNonQuery();
                LisDM_TPNK();
                cn.Close();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công");
            }
        }
        private void Expo(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private string ghepchuoi()//Hàm tách chuỗi Mã sản phẩm
        {
            string chuoi = txtTenSP.Text;
            string[] chuoi_cat = chuoi.Split(' ');
            string chuoiemail = "";
            int dai = chuoi_cat.Length;
            for (int i = 0; i <= dai - 2; i++)
            {
                chuoiemail += chuoi_cat[i].Substring(0, 1).ToLower();
            }
            return chuoiemail += chuoi_cat[dai - 1].ToLower() + txtMatonkho.ToString();
        }

        private void GhepChuoi(object sender,EventArgs e) 
        {
            txtMatonkho.Text= ghepchuoi();           
        }

        private void btnLayMaSP_Click(object sender, EventArgs e)//Lay ma san pham
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'SP'+CONCAT('',RIGHT(CONCAT('0000',ISNULL(right(max(Masp),4),0) + 1),4)) from tblSanpham_tonkho where left(Masp,2) like 'SP'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMatonkho.Text = Convert.ToString(reader[0]);
            reader.Close();
            txtMasp.Text = txtMatonkho.Text;

        }

        private void LoadDMSP()//List Danh muc san pham
        {
            try
            {
                DataTable Table = new DataTable();
                ketnoi Connect = new ketnoi();
                GLookEditSanpham.Properties.DataSource = Connect.laybang("select Masp,Tensp,Makh,TenKH "
                +" from tblSANPHAM SP left join tblKHACHHANG KH on SP.Makh=KH.MKH where Tensp is not null and Tensp <>'' order by Makh ASC");
                GLookEditSanpham.Properties.DisplayMember = "Masp";
                GLookEditSanpham.Properties.ValueMember = "Masp";
                GLookEditSanpham.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                GLookEditSanpham.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                GLookEditSanpham.Properties.ImmediatePopup = true;
                Connect.dongketnoi();
            }
            catch (Exception)
            {}
        }
        private void TruTonKho()//Hàm tính tồn kho
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
        private void Gan_zero()
        {
            if (txtNhap.Text==""){txtNhap.Text = "0";}
            if (txtXuat.Text == ""){txtXuat.Text = "0";}
        }
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblSanpham_tonkho set TonCuoi=" + txtTonCuoi.Text + " where Masp like N'" + txtMatonkho.Text + "'");
            kn.dongketnoi();
        }

        private void GLookEditSanpham_EditValueChanged(object sender, EventArgs e)//Binding LookuEdit to textbox 
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtTenSP.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tensp_look);
            txtMakhach.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Makh_look);
            txttenKhachhang.Text= gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenkh_look);
        }

        private void CheckMa_CheckedChanged(object sender, EventArgs e)
        {
            if (GLookEditSanpham.Text=="")
            {
                MessageBox.Show("Mã Check rỗng");return ;
            }
            if (CheckMa.Checked == true)
            {
                txtMatonkho.Text = GLookEditSanpham.Text;
                txtMasp.Text = GLookEditSanpham.Text;
            }
            else if (CheckMa.Checked == false)
            {
                txtMatonkho.Text = "";
                txtMasp.Text = "";
            }

        }
        private void txtTonDau_TextChanged_1(object sender, EventArgs e)//Trừ tồn kho
        {
            TruTonKho();
        }

        #region  formload
        private string username;
        private void frmDM_HangTon_Load(object sender, EventArgs e)//FROM LOAD
        {
            username = Login.Username;
            LoadDMSP();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            if (Login.role=="1"||Login.role== "12")
            {
                btnThem.Visible = true;
                btnSua.Visible = true;
                btnXoa.Visible = true;
            }
        }
        #endregion

        private void btnMaSP_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}