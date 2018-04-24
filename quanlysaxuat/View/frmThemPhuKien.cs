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

namespace quanlysanxuat
{
    public partial class frmThemPhuKien : Form
    {
        public frmThemPhuKien()
        {
            InitializeComponent();
        }
       

        private void frmThemPhuKien_Load(object sender, EventArgs e)
        {
            LbUser.Text = Login.Username; CHITIET_PHUKIEN();
        }

        private void BtnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    string strQuery = string.Format(@"insert into tblPHUKIEN_SP (MaCT,TenCT,SoLuong,NguoiLap,NgayLap) "
                    + " VALUES (N'{0}',N'{1}','{2}',N'{3}',GetDate()) ",
                    rowData["MaCT"],
                    rowData["TenCT"],
                    rowData["SoLuong"],
                    LbUser.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); CHITIET_PHUKIEN();
            }
            catch (Exception)
            {
                MessageBox.Show("Mã chi tiết trùng");
            }
        }
        private void CHITIET_PHUKIEN()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblPHUKIEN_SP");
            kn.dongketnoi();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    string strQuery = string.Format(@"update tblPHUKIEN_SP set MaCT=N'{0}',TenCT=N'{1}',SoLuong='{2}',NguoiLap=N'{3}', "
                           + "NgayLap=GetDate() where ID = '{4}'",
                    rowData["MaCT"],
                    rowData["TenCT"],
                    rowData["SoLuong"],
                    LbUser.Text,
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery(); CHITIET_PHUKIEN();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex.Message);
            } 
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    Console.WriteLine(rowData["ID"]);
                    string strQuery = string.Format(@"DELETE from tblPHUKIEN_SP  where ID ='{0}'", rowData["ID"]);
                    Console.WriteLine(strQuery);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery(); CHITIET_PHUKIEN();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex.Message);
            }
        }

        private void BtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CHITIET_PHUKIEN();
        }
    }
}
