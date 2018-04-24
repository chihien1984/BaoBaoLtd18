using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;


namespace quanlysanxuat
{
    public partial class Pj_chung : DevExpress.XtraEditors.XtraForm
    {
        public string m_connect = Connect.mConnect;
        SqlConnection con = null;
        public delegate void NewHome();
        public event NewHome OnNewHome;
        public Pj_chung()
        {
            InitializeComponent();
            try
            {
                SqlClientPermission ss = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
                ss.Demand();
            }
            catch (Exception)
            {

                throw;
            }
            SqlDependency.Stop(m_connect);
            SqlDependency.Start(m_connect);
            con = new SqlConnection(m_connect);
        }

        public void RowsColor()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //int val = Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value.ToString());
                if (dataGridView1.Rows[i].Cells[12].Value.ToString().Trim() == "Đỏ")
                {
                    dataGridView1.Rows[i].Cells[12].Style.BackColor = Color.OrangeRed;
                    //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                    //dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                }
                
            }
        }
        public void Form1_OnNewHome()
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)//tab
            {
                NewHome dd = new NewHome(Form1_OnNewHome);
                i.BeginInvoke(dd, null);
                return;
            }
            LoadData();
        }
        void LoadData()
        {
            DataTable dt = new DataTable();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("DATA_UPDATECHITIET", con);
            cmd.Notification = null;
            SqlDependency de = new SqlDependency(cmd);
            de.OnChange += new OnChangeEventHandler(de_OnChange);
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            dataGridView1.DataSource = dt;
            //string m_connect = ketnoi.mConnect;
            //dataGridView1.DataSource = SqlHelper.ExecuteDataset(m_connect, "T01_DH").Tables[0];
        }
        public void de_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency de = sender as SqlDependency;
            de.OnChange -= de_OnChange;
            if (OnNewHome != null)
            {
                OnNewHome();
            }
        }
        private void Refresh_data_Load(object sender, EventArgs e)
        {
            OnNewHome += new NewHome(Form1_OnNewHome);//tab
            LoadData();//load data vao datagrid
            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            FormBorderStyle = FormBorderStyle.Fixed3D;
            WindowState = FormWindowState.Maximized;
        }
        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {

        }
        private void ShowMain()
        {
        }
        private void main_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(ShowMain)); //Tạo luồng mới 
            thread.Start();//Khởi chạy luồng
            this.Dispose();
        }
        private void chitiet_Click(object sender, EventArgs e)
        {
            //frmxemtiendo fChung = new frmxemtiendo();
            //fChung.Show();
        }
        private void thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
