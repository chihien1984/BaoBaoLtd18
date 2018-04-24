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
using System.Configuration;

namespace quanlysanxuat
{
    public partial class frmThem_KH : DevExpress.XtraEditors.XtraForm
    {
        public frmThem_KH()
        {
            InitializeComponent();
        }
        Clsketnoi knn = new Clsketnoi();
     
        public static string THONGTIN_MOI;
        string Gol = ""; 
        private void ThDanhMucKhachHang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select ID,MKH,TenKH,Diachi,Sodienthoai, 
                                      Fax, Email, Nguoi_gd, Phanloai_KH, Manv, Ngaycapnhat,CodeKH 
                                      from tblKHACHHANG");
        }
        private void frmThem_KH_Load(object sender, EventArgs e)
        {
            ThDanhMucKhachHang();
        }

        private void btnfresh_Click(object sender, EventArgs e)
        {
            ThDanhMucKhachHang();
        }

        private void btnallDSnhanvien_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select ID,MKH,TenKH,Diachi,Sodienthoai, 
                                      Fax, Email, Nguoi_gd, Phanloai_KH, Manv, Ngaycapnhat,CodeKH 
                                      from tblKHACHHANG");
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaKhachHang.Text=string.Empty;
            txtTenKhachHang.Text = string.Empty;
            txtMaKhachHang.Enabled = true;
            txtTenKhachHang.Enabled = true;
            txtDiachi_KH.Enabled = true;
            txtNguoiGiaoDich.Enabled = true;
            txtDienThoai.Enabled = true;
            txtfax.Enabled = true;
            txtemail.Enabled = true;
            txtLoai_KH.Enabled = true;
            txtNguoiGiaoDich.Enabled = true;
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaKH = txtMaKhachHang.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select MKH from tblKHACHHANG", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaKH == dr.GetString(0))
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
            if (txtTenKhachHang.Text == "")
            {
                MessageBox.Show("Thêm tên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }         
            else
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblKHACHHANG (TenKH,Diachi,Sodienthoai, "
                                   + " Fax,Email,Nguoi_gd,Phanloai_KH,Ngaycapnhat) "
                                   + " values(@TenKH,@Diachi,@Sodienthoai, "
                                   + " @Fax,@Email,@Nguoi_gd,@Phanloai_KH,GetDate())", con))
                    {
                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                        cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi_KH.Text;
                        cmd.Parameters.Add("@Sodienthoai", SqlDbType.NVarChar).Value = txtDienThoai.Text;
                        cmd.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = txtfax.Text;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtemail.Text;
                        cmd.Parameters.Add("@Nguoi_gd", SqlDbType.NVarChar).Value = txtNguoiGiaoDich.Text;
                        cmd.Parameters.Add("@Phanloai_KH", SqlDbType.NVarChar).Value = txtLoai_KH.Text;
                        cmd.ExecuteNonQuery();
                    }
                    CapNhatMaKhachHang();
                    ThDanhMucKhachHang();
                    con.Close();
                }
            }
        }
        private void CapNhatMaKhachHang()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update tblKHACHHANG set MKH = right(concat('0000',b.ID),4)
				from (select top 1 ID from tblKHACHHANG order by ID desc)b where MKH is null");
            var dt = Model.Function.GetDataTable(sqlQuery);
        }
        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txtMaKhachHang.Text != "" && txtTenKhachHang.Text != "")
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update tblKHACHHANG set TenKH=@TenKH,Diachi=@Diachi,Sodienthoai=@Sodienthoai, "
                                   + " Fax=@Fax, Email=@Email, Nguoi_gd=@Nguoi_gd, Phanloai_KH=@Phanloai_KH, Ngaycapnhat=GetDate() where ID like N'" + txtCode.Text+"'", con))
                    {
                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = txtTenKhachHang.Text;
                        cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiachi_KH.Text;
                        cmd.Parameters.Add("@Sodienthoai", SqlDbType.NVarChar).Value = txtDienThoai.Text;
                        cmd.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = txtfax.Text;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtemail.Text;
                        cmd.Parameters.Add("@Phanloai_KH", SqlDbType.NVarChar).Value = txtLoai_KH.Text;
                        cmd.Parameters.Add("@Nguoi_gd", SqlDbType.NVarChar).Value = txtNguoiGiaoDich.Text;
                        cmd.ExecuteNonQuery();
                    }
                    CapNhatMaKhachHang();
                    ThDanhMucKhachHang();
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
            gridControl2.DataSource = kn.xulydulieu("delete tblKHACHHANG where "
                            + " MKH like '" + txtMaKhachHang.Text + "' and ID like N'" + txtCode.Text + "' ");
            ThDanhMucKhachHang();
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtMaKhachHang.Text= gridView2.GetFocusedRowCellDisplayText(makh_grid);
            txtTenKhachHang.Text=gridView2.GetFocusedRowCellDisplayText(TenKh_grid);
            txtNguoiGiaoDich.Text=gridView2.GetFocusedRowCellDisplayText(nguoigiaodich_grid);
            txtDiachi_KH.Text= gridView2.GetFocusedRowCellDisplayText(Diachi_grid);
            txtDienThoai.Text = gridView2.GetFocusedRowCellDisplayText(sodienthoai_grid);
            txtfax.Text  = gridView2.GetFocusedRowCellDisplayText(fax_grid);
            txtLoai_KH.Text = gridView2.GetFocusedRowCellDisplayText(phanloai_grid);
            dpngay_them.Text = gridView2.GetFocusedRowCellDisplayText(ngaycapnhat_grid);
            txtCode.Text = gridView2.GetFocusedRowCellDisplayText(colIDKhachHang);
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtMaKhachHang.Text = gridView2.GetFocusedRowCellDisplayText(makh_grid);
            txtTenKhachHang.Text = gridView2.GetFocusedRowCellDisplayText(TenKh_grid);
            txtNguoiGiaoDich.Text = gridView2.GetFocusedRowCellDisplayText(nguoigiaodich_grid);
            txtDiachi_KH.Text = gridView2.GetFocusedRowCellDisplayText(Diachi_grid);
            txtDienThoai.Text = gridView2.GetFocusedRowCellDisplayText(sodienthoai_grid);
            txtfax.Text = gridView2.GetFocusedRowCellDisplayText(fax_grid);
            txtLoai_KH.Text = gridView2.GetFocusedRowCellDisplayText(phanloai_grid);
            dpngay_them.Text = gridView2.GetFocusedRowCellDisplayText(ngaycapnhat_grid);
            txtCode.Text = gridView2.GetFocusedRowCellDisplayText(colIDKhachHang);
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}