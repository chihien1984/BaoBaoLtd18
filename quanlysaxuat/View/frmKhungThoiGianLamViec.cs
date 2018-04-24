using DevExpress.ExpressApp.Win.Editors;
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
    public partial class frmKhungThoiGianLamViec : Form
    {
        public frmKhungThoiGianLamViec()
        {
            InitializeComponent();
        }
        #region fromload
        private void frmKhungThoiGianLamViec_Load(object sender, EventArgs e)
        {
            DocDSKhungThoiGianLamViec();
        }
        #endregion
        private void DocDSKhungThoiGianLamViec()
        {
            ketnoi kn = new ketnoi();
            grKhungThoiGianLamViec.DataSource = kn.laybang(@"select * from tblKhungThoiGianLamViec order by ID desc");
            kn.dongketnoi();
            this.gvKhungThoiGianLamViec.OptionsSelection.MultiSelectMode =
            DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvKhungThoiGianLamViec.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gvKhungThoiGianLamViec.OptionsView.NewItemRowPosition =
       DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void TaoMoiDanhSachThoiGianLamViec()
        {
            ketnoi kn = new ketnoi();
            grKhungThoiGianLamViec.DataSource = 
                kn.laybang(@"select top 0 * from tblKhungThoiGianLamViec");
            kn.dongketnoi();
        }
        private void Them()
        {
            try
            {
            DataRow rowData;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int[] listRowList = gvKhungThoiGianLamViec.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = gvKhungThoiGianLamViec.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"insert into tblKhungThoiGianLamViec 
			         (Ma,TenGoi,Tu,Den,HeSo,NguoiGhi,NgayGhi)
			            values(N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        GetDate())",
                rowData["Ma"], rowData["TenGoi"],
                rowData["Tu"], rowData["Den"],
                rowData["HeSo"],Login.Username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhungThoiGianLamViec();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:"+ex, "Message");
            }
        }

        private void Sua()
        {
            DataRow rowData;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int[] listRowList = gvKhungThoiGianLamViec.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = gvKhungThoiGianLamViec.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update tblKhungThoiGianLamViec 
			        set Ma=N'{0}',TenGoi=N'{1}',
                        Tu='{2}',
                        Den='{3}',HeSo='{4}',
                        NguoiGhi=N'{5}',
                        NgayGhi=GetDate() where ID='{6}'",
                rowData["Ma"], rowData["TenGoi"],
                rowData["Tu"], rowData["Den"],
                rowData["HeSo"],Login.Username,rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhungThoiGianLamViec();
        }
        private void Xoa()
        {
            DataRow rowData;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int[] listRowList = gvKhungThoiGianLamViec.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = gvKhungThoiGianLamViec.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"delete from tblKhungThoiGianLamViec 
			        where ID like '{0}'", rowData["ID"],
                Login.Username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSKhungThoiGianLamViec();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiDanhSachThoiGianLamViec();
            this.gvKhungThoiGianLamViec.OptionsSelection.MultiSelectMode =
             DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvKhungThoiGianLamViec.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private void ShowSaveEditDelete()
        {
            if (gvKhungThoiGianLamViec.SelectedRowsCount>=1)
            {
                btnThem.Visible = true;
                btnSua.Visible = true;
                btnXoa.Visible = true;
            }
            else
            {
                btnSua.Visible = true;
                btnXoa.Visible = true;
            }
        }

        private void grKhungThoiGianLamViec_MouseMove(object sender, MouseEventArgs e)
        {
            ShowSaveEditDelete();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Sua();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            DocDSKhungThoiGianLamViec();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            grKhungThoiGianLamViec.ShowPrintPreview();
        }
    }
}
