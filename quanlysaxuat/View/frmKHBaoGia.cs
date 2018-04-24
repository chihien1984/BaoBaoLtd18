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

namespace quanlysanxuat
{
    public partial class frmKHBaoGia : DevExpress.XtraEditors.XtraForm
    {
        public frmKHBaoGia()
        {
            InitializeComponent();
        }
        private void ListKhachhang()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khachhang");
            kn.dongketnoi();
        }
        private void ListKhachhangthem()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khachhang where makh like N'"+txtMaKH.Text+"'");
            kn.dongketnoi();
        }
        private void ListKhachhangsua()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khachhang where id like '"+txtid.Text+"'");
            kn.dongketnoi();
        }
        private void Listkhachhang(object sender,EventArgs e) 
        {
            ListKhachhang();
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaNCC = txtMaKH.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select makh from tblz_khachhang", con);
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
        private void them(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenKH.Text == "") { MessageBox.Show("Tên sản phẩm", "THÔNG BÁO"); return; }
                else if (kiemtratontai()) { MessageBox.Show("Sản phẩm tồn tại", "THÔNG BÁO"); return; ;}
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_khachhang(makh,tenkh,diachi,nguoilap,ngaylap) "
                    + " values(@makh,@tenkh,@diachi,@nguoilap,GetDate())", cn);
                    cmd.Parameters.Add(new SqlParameter("@makh", SqlDbType.NVarChar)).Value = txtMaKH.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenkh", SqlDbType.NVarChar)).Value = txtTenKH.Text;
                    cmd.Parameters.Add(new SqlParameter("@diachi", SqlDbType.NVarChar)).Value = txtDiachi.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close(); 
                    ListKhachhangthem();  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm không thành công", "thông báo");
            }
        }

        private void sua(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenKH.Text == "") { MessageBox.Show("Tên sản phẩm", "THÔNG BÁO"); return; }               
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblz_khachhang set "
                    + " makh=@makh,tenkh=@tenkh,diachi=@diachi,nguoilap=@nguoilap,ngaylap=GetDate() where id like '"+txtid.Text+"'", cn);
                    cmd.Parameters.Add(new SqlParameter("@makh", SqlDbType.NVarChar)).Value = txtMaKH.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenkh", SqlDbType.NVarChar)).Value = txtTenKH.Text;
                    cmd.Parameters.Add(new SqlParameter("@diachi", SqlDbType.NVarChar)).Value = txtDiachi.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    ListKhachhangsua();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm không thành công", "thông báo");
            }
        }

        private void xoa(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete from tblz_khachhang where id like '"+txtid.Text+"'");
            kn.dongketnoi();
            ListKhachhang();
        }


        private void Binding(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMaKH.Text=gridView2.GetFocusedRowCellDisplayText(Makhach_grid2);
            txtTenKH.Text=gridView2.GetFocusedRowCellDisplayText(tenkhach_grid2);
            txtDiachi.Text=gridView2.GetFocusedRowCellDisplayText(Diachi_grid2);
        }
     
        private void frmKHBaoGia_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Login.Username;
        }
    }
}