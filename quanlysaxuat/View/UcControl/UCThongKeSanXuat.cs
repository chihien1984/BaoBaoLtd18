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

namespace quanlysanxuat
{
    public partial class UCThongKeSanXuat : DevExpress.XtraEditors.XtraForm
    {
        public UCThongKeSanXuat()
        {
            InitializeComponent();
        }
       

        private void LoadTanSuatDatHang(object sender, EventArgs e)//Thống ke tần suất đặt hàng
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = Connect.mConnect;
                bool flag = sqlConnection.State == ConnectionState.Closed;
                if (flag)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand("TANSUAT_TK", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dptu_ngay.Text;
                sqlCommand.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpden_ngay.Text;
                sqlCommand.Parameters.Add(new SqlParameter("@sonho", SqlDbType.Float)).Value = txtSonho.Text;
                sqlCommand.Parameters.Add(new SqlParameter("@solon", SqlDbType.Float)).Value = txtSolon.Text;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                gridControl2.DataSource = dataTable;
                sqlConnection.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi số lớn hoặc số nhỏ rỗng", "BÁO");
            }
        }
        private void LOAD_DMChiTiet()
        {
            ketnoi ketnoi = new ketnoi();
            gridControl2.DataSource = ketnoi.laybang(string.Concat(new string[]
			{
				"select * from THONGKETONGDONHANG where daystar between '",
				dptu_ngay.Value.ToString("yyyy/MM/dd"),
				"' and  '",
				dpden_ngay.Value.ToString("yyyy/MM/dd"),
				"'"
			}));
            ketnoi.dongketnoi();
        }

        private void BAOCAOTHONGKE_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool flag = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
            if (flag)
            {
                e.Handled = true;
            }
        }
        private void SLden_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool flag = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
            if (flag)
            {
                e.Handled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private DataSet Timkiem(string IDSP)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            string cmdText = "[COUNT_TONGDONHANG]";
            SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@BatDauSTAR", SqlDbType.Date).Value = dptu_ngay.Value;
            sqlCommand.Parameters.Add("@BatDauEND", SqlDbType.Date).Value = dpden_ngay.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "ds");
            sqlConnection.Close();
            return dataSet;
        }
        private void Search_Click(object sender, EventArgs e)//Tổng kết đơn hàng
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            bool flag = sqlConnection.State == ConnectionState.Closed;
            if (flag)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
            {
                "SELECT * FROM [dbo].[THONGKETONGDONHANG] where daystar BETWEEN '",
                dptu_ngay.Value.ToString("MM/dd/yyyy"),
                "' AND '",
                dpden_ngay.Value.ToString("MM/dd/yyyy"),
                "'"
            }), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl3.DataSource = dataTable;
            sqlConnection.Close();
            DataSet dataSet = new DataSet();
            dataSet = Timkiem(dpden_ngay.Text);
            gridControl1.DataSource = dataSet.Tables["ds"];
            gridView3.ExpandAllGroups();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            bool flag = sqlConnection.State == ConnectionState.Closed;
            if (flag)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
			{
				"SELECT * FROM [dbo].[DONHANG_NHO_HON1000] where daystar BETWEEN '",
				dptu_ngay.Value.ToString("MM/dd/yyyy"),
				"' AND '",
				dpden_ngay.Value.ToString("MM/dd/yyyy"),
				"'"
			}), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
            sqlConnection.Close();
            DataSet dataSet = new DataSet();
            dataSet = Timkiem(dpden_ngay.Text);
            gridControl1.DataSource = dataSet.Tables["ds"];
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            bool flag = sqlConnection.State == ConnectionState.Closed;
            if (flag)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
			{
				"SELECT * FROM [dbo].[DONHANG_1000_5000] where daystar BETWEEN '",
				dptu_ngay.Value.ToString("MM/dd/yyyy"),
				"' AND '",
				dpden_ngay.Value.ToString("MM/dd/yyyy"),
				"'"
			}), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
            sqlConnection.Close();
            DataSet dataSet = new DataSet();
            dataSet = Timkiem(dpden_ngay.Text);
            gridControl1.DataSource = dataSet.Tables["ds"];
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            bool flag = sqlConnection.State == ConnectionState.Closed;
            if (flag)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
			{
				"SELECT * FROM [dbo].[DONHANG_5000_10000] where daystar BETWEEN '",
				dptu_ngay.Value.ToString("MM/dd/yyyy"),
				"' AND '",
				dpden_ngay.Value.ToString("MM/dd/yyyy"),
				"'"
			}), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
            sqlConnection.Close();
            DataSet dataSet = new DataSet();
            dataSet = Timkiem(dpden_ngay.Text);
            gridControl1.DataSource = dataSet.Tables["ds"];
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = Connect.mConnect;
            bool flag = sqlConnection.State == ConnectionState.Closed;
            if (flag)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(string.Concat(new string[]
			{
				"SELECT * FROM [dbo].[DONHANG_LON_HON10000] where daystar BETWEEN '",
				dptu_ngay.Value.ToString("MM/dd/yyyy"),
				"' AND '",
				dpden_ngay.Value.ToString("MM/dd/yyyy"),
				"'"
			}), sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
            sqlConnection.Close();
            DataSet dataSet = new DataSet();
            dataSet = Timkiem(dpden_ngay.Text);
            gridControl1.DataSource = dataSet.Tables["ds"];
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            gridView3.ExpandAllGroups();
        }
        private void ExpTongKet(object sender, EventArgs e)// Xuất file
        {
            gridControl1.ShowPrintPreview();
        }
        private void ExpChiTiet(object sender, EventArgs e)// Xuất file
        {
            gridControl2.ShowPrintPreview();
        }
        private void ExpTuanXuat(object sender, EventArgs e)// Xuất file
        {
            gridControl3.ShowPrintPreview();
        }
    }
}
