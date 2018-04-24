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
using DevExpress.XtraGrid;

namespace quanlysanxuat
{
    public partial class frmResources : DevExpress.XtraEditors.XtraForm
    {
        public frmResources()
        {
            InitializeComponent();
        }
       
        private void List_NguonLuc()//Hien thi danh sach nguon luc
        {
            ketnoi kn = new ketnoi();
            grDanhMucNguonLuc.DataSource = kn.laybang("select * from tblResources order by ResourceID ASC ");
            kn.dongketnoi();
            this.gvDanhMucNguonLuc.OptionsView.NewItemRowPosition
                = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void List_NguonLucThem()
        {
            ketnoi kn = new ketnoi();
            grDanhMucNguonLuc.DataSource = kn.laybang("select * from tblResources order by ResourceID ASC");
            kn.dongketnoi();
        }
        private string userName;
        private void frmResources_Load(object sender, EventArgs e)
        {
            this.userName = Login.Username;
            List_NguonLuc();
            gvDanhMucNguonLuc.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            TheHienToThucHien();
        }

        private void BtnList_Resource_Click(object sender, EventArgs e)
        {
            List_NguonLuc();
        }

        private void Binding_Grid1(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gvDanhMucNguonLuc.GetFocusedDisplayText();
        }
        private void Them(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvDanhMucNguonLuc.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucNguonLuc.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblResources (Ma_Nguonluc,Ten_Nguonluc,Ngay,Nguoi,ToThucHien) 
                          VALUES (N'{0}',N'{1}',GetDate(),N'{2}',N'{3}')",
                          rowData["Ma_Nguonluc"],
                          rowData["Ten_Nguonluc"],
                          userName, 
                          rowData["ToThucHien"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                List_NguonLuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "Lỗi");
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
                int[] listRowList = this.gvDanhMucNguonLuc.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucNguonLuc.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblResources set 
                        Ma_Nguonluc=N'{0}',
                        Ten_Nguonluc=N'{1}',
                        Ngay=GetDate(),
                        Nguoi=N'{2}',
                        ToThucHien=N'{3}'
                        where ResourceID='{4}'",
                    rowData["Ma_Nguonluc"],
                    rowData["Ten_Nguonluc"],
                    userName,
                    rowData["ToThucHien"],
                    rowData["ResourceID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); List_NguonLuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "Lỗi");
            }
            //focused to row after update
            int id = (int)gvDanhMucNguonLuc.GetRowCellValue(gvDanhMucNguonLuc.FocusedRowHandle, "ID");

            //Then I save the changes to the database using Entity Framework's SaveChanges method and refresh the grid.
            //Finaly I iterate through the GridView's Rows and try to find the row where the selected column is equal to the value I want and put save that value to the GridView's FocusedHandle property.Done.
        }
        public void SelectRow(string pColumnName, int pValue)
        {
            //int rowhandle = GetRowHandleByColumnValue(myView, "ID", pValue);
            //if (rowhandle != GridControl.InvalidRowHandle)
            //{
            //    myView.FocusedRowHandle = rowhandle;
            //    return;
            //}
        }

        private int GetRowHandleByColumnValue(DevExpress.XtraGrid.Views.Grid.GridView view, string ColumnFieldName, object value)
        {
            int result = GridControl.InvalidRowHandle;
            for (int i = 0; i < view.RowCount; i++)
                if (view.GetRowCellValue(i, ColumnFieldName).Equals(value))
                    return i;
            return result;
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
                int[] listRowList = this.gvDanhMucNguonLuc.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucNguonLuc.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblResources where ResourceID='{0}'",
                          rowData["ResourceID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); List_NguonLuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do"+ex, "Lỗi");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            grDanhMucNguonLuc.DataSource = kn.laybang(@"select Top 0 * from tblResources");
            kn.dongketnoi();
              this.gvDanhMucNguonLuc.OptionsView.NewItemRowPosition =
              DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
        private void TheHienToThucHien()
        {
            repositoryItemComboBox1.Items.Clear();
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select ToThucHien from tblResources");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemComboBox1.Items.Add(dt.Rows[i]["ToThucHien"]);
            }
            kn.dongketnoi();
      
        }
    }
}