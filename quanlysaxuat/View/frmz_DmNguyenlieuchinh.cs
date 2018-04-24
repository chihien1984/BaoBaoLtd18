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
    public partial class frmz_DmNguyenlieuchinh : DevExpress.XtraEditors.XtraForm
    {
        public frmz_DmNguyenlieuchinh()
        {
            InitializeComponent();
        }
        private void Listvatlieu()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_danhmucgiavatlieu");
            kn.dongketnoi();
        }
        private void Listvatlieuthem()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_danhmucgiavatlieu where Madongiavl like N'" + txtMavatlieu.Text + "'");
            kn.dongketnoi();
        }
        private void Listvatlieusua()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_danhmucgiavatlieu where id like '" + txtid.Text + "'");
            kn.dongketnoi();
        }
        private void Listkhachhang(object sender, EventArgs e)
        {
            Listvatlieu();
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaNCC = txtMavatlieu.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Madongiavl from tblz_danhmucgiavatlieu where Madongiavl is not null", con);
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
                if (txtMavatlieu.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenvatlieu.Text == "") { MessageBox.Show("Tên không bỏ trống", "THÔNG BÁO"); return; }
                else if (kiemtratontai()) { MessageBox.Show("Sản phẩm tồn tại", "THÔNG BÁO"); return; ;}
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_danhmucgiavatlieu(Madongiavl,Tendongiavl,Dongia,Nguoilap,Ngaylap) "
                    + " values(@Madongiavl,@Tendongiavl,@Dongia,@Nguoilap,GetDate())", cn);
                    cmd.Parameters.Add(new SqlParameter("@Madongiavl", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tendongiavl", SqlDbType.NVarChar)).Value = txtTenvatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Dongia", SqlDbType.NVarChar)).Value = txtDongia.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Listvatlieuthem();
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
                if (txtMavatlieu.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenvatlieu.Text == "") { MessageBox.Show("Tên sản phẩm", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblz_danhmucgiavatlieu set Madongiavl=@Madongiavl,Tendongiavl=@Tendongiavl,Dongia=@Dongia,Nguoilap=@Nguoilap,Ngaylap=GetDate() "
                    + "  where id like '" + txtid.Text + "'", cn);
                    cmd.Parameters.Add(new SqlParameter("@Madongiavl", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tendongiavl", SqlDbType.NVarChar)).Value = txtTenvatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Dongia", SqlDbType.NVarChar)).Value = txtDongia.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    Listvatlieusua();
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
            gridControl2.DataSource = kn.xulydulieu("delete from tblz_danhmucgiavatlieu where id like '" + txtid.Text + "'");
            kn.dongketnoi();
            Listvatlieu();
        }


        private void Binding(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMavatlieu.Text = gridView2.GetFocusedRowCellDisplayText(Madongiavl_grid2);
            txtTenvatlieu.Text = gridView2.GetFocusedRowCellDisplayText(Tendongiavl_grid2);
            txtDongia.Text = gridView2.GetFocusedRowCellDisplayText(Dongia_grid2);
        }
        private void btnLay_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'VT'+CONCAT('',RIGHT(CONCAT('00000',ISNULL(right(max(Madongiavl),5),0) + 1),5)) from tblz_danhmucgiavatlieu", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMavatlieu.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void AutoTenvatlieu()// Autocomplete noi dung chi phi đã nhập
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                {
                    SqlCommand cmd = new SqlCommand("select Tendongiavl from tblz_danhmucgiavatlieu", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    txtTenvatlieu.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                }
            }
            catch
            { }
            
        }
        public static string Username = "";
        private void frmz_DmNguyenlieuchinh_Load(object sender, EventArgs e)
        {
            AutoTenvatlieu();//Autocomplete Tên Vật liệu 
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Username;
        }
    }
}