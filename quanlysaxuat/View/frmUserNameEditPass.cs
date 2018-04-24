using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlysanxuat.Model;
using System.Data.SqlClient;

namespace quanlysanxuat.View
{
    public partial class frmUserNameEditPass : DevExpress.XtraEditors.XtraForm
    {
        public frmUserNameEditPass()
        {
            InitializeComponent();
        }
        //formload
        private void frmUserNameEditPass_Load(object sender, EventArgs e)
        {
            ThThongTinNguoiDangNhap();
        }
        private void ThThongTinNguoiDangNhap (){
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select UserName,LastName,FirstName,AppPasswordHash
                from AspNetUsers where UserName like '{0}'", ClassUser.User);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtUserName.Text= reader[0].ToString();
                txtLastName.Text= reader[1].ToString();
                txtFirstName.Text= reader[2].ToString();
               txtPassWordOld.Text= reader[3].ToString();
            }
            con.Close();
        }

        private void btnCapNhatPassWordMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPass.Text == "") 
                {
                    MessageBox.Show("Mật khẩu mới không được để trống", "Message"); return; 
                }
                else
                { 
                Function.ConnectSanXuat();
                string sqlQuery = string.Format(@"update AspNetUsers set AppPasswordHash = N'{0}' where UserName like N'{1}'",
                   Mahoa.Encrypt(txtNewPass.Text.Trim()), txtUserName.Text.Trim());
                var kq = Function.GetDataTable(sqlQuery);
                    XtraMessageBox.Show("Thao tác thành công[Success]", "Thông báo [Message]",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                XtraMessageBox.Show("Không thành công[UnSuccess]", "Thông báo [Message]",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}