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
using System.Configuration;

namespace quanlysanxuat
{
    public partial class frmThemNCC : DevExpress.XtraEditors.XtraForm
    {
        public frmThemNCC()
        {
            InitializeComponent();
        }
       
        private void LoadDMNCC()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblDM_NCC_VATTU order by Ngaycapnhat_NCC desc"); 
            kn.dongketnoi();
        }

        private void LoadDMNCC(object sender, EventArgs e)
        {
            LoadDMNCC();
        }
        private void Cleartextbox()
        {
          txtMaNCC.Clear();
          txtTenNCC.Clear();
          txtDiachiNCC.Clear();
          txtDienthoai.Clear();
          txtfax.Clear();
          txtemail.Clear();
          txtNguoigd.Clear();
          txtLoaiNCC.Clear();
          txtNguoigd.Clear();
        }
        private void btnallDSnhanvien_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select MKH,TenKH,Diachi,Sodienthoai, "
                                      + " Fax, Email, Nguoi_gd, Phanloai_KH, Manv, Ngaycapnhat "
                                      + " from tblDM_NCC_VATTU"); 
            kn.dongketnoi();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = true; txtTenNCC.Enabled = true;
            txtDiachiNCC.Enabled = true; txtNguoigd.Enabled = true; txtDienthoai.Enabled = true; txtfax.Enabled = true;
            txtemail.Enabled = true; txtLoaiNCC.Enabled = true; txtNguoigd.Enabled = true;
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaNCC = txtMaNCC.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Ma_NCC from tblDM_NCC_VATTU", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaNCC == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "")
            {MessageBox.Show("Thêm Mã", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);return;}
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Thêm tên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);return;
            }
            if (kiemtratontai())
            { MessageBox.Show("Mã số '" + txtMaNCC.Text + "' đã tồn tại, Không thể thêm mã trùng"); return; }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblDM_NCC_VATTU (Ma_NCC,Ten_NCC,Diachi_NCC,Dt_NCC "
                                   +",Fax_NCC,Email_NCC,Nguoigd_NCC ,Phanloai_NCC,Nguoilapdm_NCC ,Ngaycapnhat_NCC) "
                                   + " values(@Ma_NCC,@Ten_NCC,@Diachi_NCC,@Dt_NCC "
                                   + ",@Fax_NCC,@Email_NCC,@Nguoigd_NCC, @Phanloai_NCC, @Nguoilapdm_NCC, GetDate())", con))
                    {
                        cmd.Parameters.Add("@Ma_NCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                        cmd.Parameters.Add("@Ten_NCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.Add("@Diachi_NCC", SqlDbType.NVarChar).Value = txtDiachiNCC.Text;
                        cmd.Parameters.Add("@Dt_NCC", SqlDbType.NVarChar).Value = txtDienthoai.Text;
                        cmd.Parameters.Add("@Fax_NCC", SqlDbType.NVarChar).Value = txtfax.Text;
                        cmd.Parameters.Add("@Email_NCC", SqlDbType.NVarChar).Value = txtemail.Text;
                        cmd.Parameters.Add("@Nguoigd_NCC", SqlDbType.NVarChar).Value = txtNguoigd.Text;
                        cmd.Parameters.Add("@Phanloai_NCC", SqlDbType.NVarChar).Value = txtLoaiNCC.Text;
                        cmd.Parameters.Add("@Nguoilapdm_NCC", SqlDbType.NVarChar).Value = txtUser.Text;
                        cmd.ExecuteNonQuery();
                    }
                    LoadDMNCC();
                    con.Close(); Cleartextbox();
                }
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text != "" && txtTenNCC.Text != "")
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update tblDM_NCC_VATTU set Ma_NCC=@Ma_NCC,Ten_NCC=@Ten_NCC,Diachi_NCC=@Diachi_NCC,Dt_NCC=@Dt_NCC "
                                   + ",Fax_NCC=@Fax_NCC,Email_NCC=@Email_NCC,Nguoigd_NCC=@Nguoigd_NCC ,Phanloai_NCC=@Phanloai_NCC,Nguoilapdm_NCC=@Nguoilapdm_NCC ,Ngaycapnhat_NCC=GetDate() where idNCC like N'" + txtid.Text + "'", con))
                    {
                        cmd.Parameters.Add("@Ma_NCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                        cmd.Parameters.Add("@Ten_NCC", SqlDbType.NVarChar).Value = txtTenNCC.Text;
                        cmd.Parameters.Add("@Diachi_NCC", SqlDbType.NVarChar).Value = txtDiachiNCC.Text;
                        cmd.Parameters.Add("@Dt_NCC", SqlDbType.NVarChar).Value = txtDienthoai.Text;
                        cmd.Parameters.Add("@Fax_NCC", SqlDbType.NVarChar).Value = txtfax.Text;
                        cmd.Parameters.Add("@Email_NCC", SqlDbType.NVarChar).Value = txtemail.Text;
                        cmd.Parameters.Add("@Nguoigd_NCC", SqlDbType.NVarChar).Value = txtNguoigd.Text;
                        cmd.Parameters.Add("@Phanloai_NCC", SqlDbType.NVarChar).Value = txtLoaiNCC.Text;
                        cmd.Parameters.Add("@Nguoilapdm_NCC", SqlDbType.NVarChar).Value = txtNguoigd.Text;
                        cmd.ExecuteNonQuery();
                    }
                    LoadDMNCC();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete tblDM_NCC_VATTU where idNCC like " + txtid.Text + " ");
            kn.dongketnoi();
            LoadDMNCC();
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
 
        }
        private void BindingNCC(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(idNCC_grid);
            txtMaNCC.Text = gridView2.GetFocusedRowCellDisplayText(MaNCC_grid);
            txtNguoigd.Text = gridView2.GetFocusedRowCellDisplayText(NguoigdNCC_grid);
            txtTenNCC.Text = gridView2.GetFocusedRowCellDisplayText(TenNCC_grid);
            txtDiachiNCC.Text = gridView2.GetFocusedRowCellDisplayText(DiachiNCC_grid);
            txtDienthoai.Text = gridView2.GetFocusedRowCellDisplayText(DtNCC_grid);
            txtfax.Text = gridView2.GetFocusedRowCellDisplayText(FaxNCC_grid);
            txtemail.Text = gridView2.GetFocusedRowCellDisplayText(EmailNCC_grid);
            txtLoaiNCC.Text = gridView2.GetFocusedRowCellDisplayText(PhanloaiNCC_grid);    
        }

        private void UcThemNCC_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString(); txtUser.Text = Login.Username;
        }

        private void btnfresh_Click(object sender, EventArgs e)
        {
            Cleartextbox();
        }
    }
}
