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
using quanlysanxuat.View;
using System.Data.SqlClient;

namespace quanlysanxuat
{
    public partial class frmDMBoPhanNhanVatTu : DevExpress.XtraEditors.XtraForm
    {
        public frmDMBoPhanNhanVatTu()
        {
            InitializeComponent();
        }

        private void frmDMBoPhanNhanVatTu_Load(object sender, EventArgs e)
        {
            txtMember.Text = Login.Username;
            DocDanhSachBoPhanNhanVatTu();
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void DocDanhSachBoPhanNhanVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from DSBoPhanNhanVatTu");
            kn.dongketnoi();
        }
        private void DocDanhSachBoPhanNhanVatTuMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select top 0 * from DSBoPhanNhanVatTu");
            kn.dongketnoi();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            DocDanhSachBoPhanNhanVatTuMoi();
            this.gridView2.OptionsView.NewItemRowPosition 
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DocDanhSachBoPhanNhanVatTu();
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
                for(int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert 
                    into DSBoPhanNhanVatTu 
                    (TenBoPhanNhan,NguoiDaiDien,UserCreate,CreateDate)
                    values(N'{0}',N'{1}',N'{2}',getdate())",
                    rowData["TenBoPhanNhan"],
                    rowData["NguoiDaiDien"],
                    txtMember.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDanhSachBoPhanNhanVatTu();
                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("Error" ,""+ ex);
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
                for(int i = 0; i < listRowList.Length; i++)
                {
                    rowData = gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update DSBoPhanNhanVatTu 
                    set TenBoPhanNhan=N'{0}',
                    NguoiDaiDienN=N'{1}',
                    UserCreate=N'{2}',
                    CreateDate=GETDATE() 
                    where IDBPNhanVatTu='{3}'",
                    rowData["TenBoPhanNhan"],
                    rowData["NguoiDaiDienN"],
                    txtMember.Text,
                    rowData["IDBPNhanVatTu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDanhSachBoPhanNhanVatTu();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "" + ex);
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
                    string strQuery = string.Format(@"delete from DSBoPhanNhanVatTu 
                    where IDBPNhanVatTu='{0}'",
                    rowData["IDBPNhanVatTu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                DocDanhSachBoPhanNhanVatTu();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "" + ex);
            }
        }
    }
}