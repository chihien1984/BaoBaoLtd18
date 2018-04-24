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
    public partial class frmThemPhongBan : DevExpress.XtraEditors.XtraForm
    {
        public string Member { set; get; }
        public frmThemPhongBan()
        {
            InitializeComponent();
        }
        private void DanhMucPhongBan()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select ID,BoPhan,MaBoPhan,NguoiLap,
                NgayGhi from tblPHONGBAN_TK");
            kn.dongketnoi();
            gridView1.OptionsView.NewItemRowPosition =
          DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        //Truyền tham chiếu tên người dùng sang from frmPhongBan
       
        private void frmPhongBan_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            lbNguoi_Dung.Text= Login.Username;
            //if (Login.role=="1"|| Login.role == "39")
            //{
            //    btnGhi.Visible = true;btnSua.Visible = true;btnXoa.Visible = true;
            //}
            DanhMucPhongBan();
        }
        private void BtnList_CongDoan_Click(object sender, EventArgs e)
        {
            DanhMucPhongBan();
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblPHONGBAN_TK 
                        (BoPhan,MaBoPhan,NguoiLap,NgayGhi)
                        values(N'{0}',N'{1}',N'{2}',GetDate())",
                          rowData["BoPhan"], 
                          rowData["MaBoPhan"],
                          Login.Username);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DanhMucPhongBan();
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Thông báo");
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
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblPHONGBAN_TK 
                        set BoPhan=N'{0}',MaBoPhan=N'{1}',NguoiLap=N'{2}',NgayGhi=GetDate() where ID='{3}'",
                          rowData["BoPhan"],
                          rowData["MaBoPhan"],
                          lbNguoi_Dung.Text,
                          rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DanhMucPhongBan();
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Thông báo");
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
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"Delete from tblPHONGBAN_TK where ID='{0}'",
                          rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DanhMucPhongBan();
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Thông báo");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.gridView1.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gridControl1.ShowPrintPreview();
            this.gridView1.OptionsSelection.MultiSelectMode
          = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void ntnNew_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.NewItemRowPosition =
               DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select top 0 ID,BoPhan,MaBoPhan,NguoiLap,
                NgayGhi from tblPHONGBAN_TK");
            kn.dongketnoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
