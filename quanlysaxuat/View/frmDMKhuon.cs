using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmDMKhuon : Form
    {
        public frmDMKhuon()
        {
            InitializeComponent();
        }

        private void frmDMKhuon_Load(object sender, EventArgs e)
        {
            txtMember.Text = Login.Username;
            DocDSKhuon();
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            NumMax();
        }

        private void DocDSKhuon()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblDM_KHUON order by right(MaKhuon,5) Desc");
            kn.dongketnoi();
        }
        private void DocDSKhuonMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select top 0 * from tblDM_KHUON");
            kn.dongketnoi();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            DocDSKhuonMoi();
            this.gridView2.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            NumMax();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DocDSKhuon();
            this.gridView2.OptionsView.NewItemRowPosition
              = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblDM_KHUON 
                        (MaKhuon,TenKhuon,
                        DacDiemKhuon,SoLuong,
                        UserCreate,DateCreate)
                        values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',GetDate())",
                    rowData["MaKhuon"],rowData["TenKhuon"],
                    rowData["DacDiemKhuon"], rowData["SoLuong"],
                    txtMember.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDSKhuon();
                NumMax();
                con.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show("Reason"+ex, "Error");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDM_KHUON 
                        set MaKhuon = N'{0}',TenKhuon = N'{1}',
                        DacDiemKhuon = N'{2}',SoLuong = N'{3}',
                        UserCreate = N'{4}',DateCreate=GetDate() where KhuonID = '{5}'",
                    rowData["MaKhuon"], rowData["TenKhuon"],
                    rowData["DacDiemKhuon"], rowData["SoLuong"],
                    txtMember.Text,rowData["KhuonID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDSKhuon();
                NumMax();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Reason" + ex);
            }
        }
    


        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblDM_KHUON 
                    where KhuonID='{0}'",
                    rowData["KhuonID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDSKhuon();
                NumMax();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Reason" + ex);
            }
        }
        private void NumMax()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select 
                        Right('00000'+cast(max(right(MaKhuon,5)+1) as nvarchar),5)
                        NumMax from tblDM_KHUON", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaxNum.Text = Convert.ToString(reader[0]);
             reader.Close();
        }
        private void btnSoKeTiep_Click(object sender, EventArgs e)
        {
            NumMax();
        }
    }
}

