using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Threading;

namespace quanlysanxuat
{
    public partial class CAPNHATTHONGBAO : DevExpress.XtraEditors.XtraForm
    {
        public CAPNHATTHONGBAO()
        {
            InitializeComponent();
        }
        private DataTable laydulieu(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void loadData()
        {
            gridControl1.DataSource = laydulieu("select * from tblTHONGTIN");
        }
        private void CAPNHATTHONGBAO_Load(object sender, EventArgs e)
        {         
            loadData();
            ngaythongbao.Enabled = true; noidungthongbao.Enabled = true; nguoithongbao.Enabled = true;
        }


        private void sothongbao_MouseClick(object sender, MouseEventArgs e)
        {
            ngaythongbao.Enabled = true; noidungthongbao.Enabled = true; nguoithongbao.Enabled = true;
            sothongbao.Focus();
            noidungthongbao.Clear();
            nguoithongbao.Clear(); 
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (ngaythongbao.Text != "" && noidungthongbao.Text != "")/// insert truyền tham số parameters
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tblTHONGTIN(Ngaytb,noidung,nguoithongbao)"
                + "values (@Ngaytb,@noidung,@nguoithongbao)", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (ngaythongbao.Text == "")

                        command.Parameters.Add(new SqlParameter("@Ngaytb", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@Ngaytb", SqlDbType.Date)).Value = ngaythongbao.Text;

                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@noidung", SqlDbType.NVarChar)).Value = noidungthongbao.Text;
                command.Parameters.Add(new SqlParameter("@nguoithongbao", SqlDbType.NVarChar)).Value = nguoithongbao.Text;
                adapter.Fill(dt);
                loadData();
                MessageBox.Show("GHI THÔNG BÁO THÀNH CÔNG LÊN HỆ THỐNG", "THÔNG BÁO");
                cn.Close();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            ngaythongbao.Enabled = true; noidungthongbao.Enabled = true; nguoithongbao.Enabled = true;
            if (ngaythongbao.Text != "" && noidungthongbao.Text != "")
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("UPDATE tblTHONGTIN SET noidung =N'" + noidungthongbao.Text + "',Ngaytb ='"+ngaythongbao.Value.ToString("yyyy/MM/dd") +"',nguoithongbao =N'" + nguoithongbao.Text + "'  Where SoTB ='" + sothongbao.Text + "'");
                if (kq > 0)
                {
                    MessageBox.Show("SỬA THÀNH CÔNG", "THÔNG BÁO");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Không Thành Công", "THÔNG BÁO");
                }
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            ngaythongbao.Enabled = true; noidungthongbao.Enabled = true; nguoithongbao.Enabled = true;
            if (sothongbao.Text != "")
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("delete from tblTHONGTIN Where SoTB ='" + sothongbao.Text + "'");
                if (kq > 0)
                {
                    MessageBox.Show("XOÁ THÔNG BÁO THÀNH CÔNG", "THÔNG BÁO");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Không Thành Công ?");
                }
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            sothongbao.Text = gridView1.GetFocusedRowCellDisplayText("SoTB");
            ngaythongbao.Text= gridView1.GetFocusedRowCellDisplayText("Ngaytb");
            noidungthongbao.Text = gridView1.GetFocusedRowCellDisplayText("noidung");
            nguoithongbao.Text= gridView1.GetFocusedRowCellDisplayText("nguoithongbao");
        }
    }
}
