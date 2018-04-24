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
    public partial class frmHieuChinhMaCongViec : Form
    {
        public frmHieuChinhMaCongViec()
        {
            InitializeComponent();
        }
       
        public static string maSanPham;
        private void frmHieuChinhMaCongViec_Load(object sender, EventArgs e)
        {
            txtNguoiDung.Text = Login.Username;
            txtMaSanPham.Text = maSanPham;
            DocDanhSachDinhMuc();
            DocDanhMucNguonLuc();
        }
        private void DocDanhMucNguonLuc()
        {
            ketnoi Connect = new ketnoi();
            repositoryItemGridLookUpEdit1.DataSource = Connect.laybang("select Ma_Nguonluc,Ten_Nguonluc from tblResources");
            repositoryItemGridLookUpEdit1.DisplayMember = "Ten_Nguonluc";
            repositoryItemGridLookUpEdit1.ValueMember = "Ten_Nguonluc";
            repositoryItemGridLookUpEdit1.NullText = null;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowAutoFilterRow = true;
            repositoryItemGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemGridLookUpEdit1.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void DocDanhSachDinhMucTheoTen()
        {
            ketnoi kn = new ketnoi();
                gridControl1.DataSource = kn.laybang(@"select id,Masp,Tensp,Macongdoan,
            Tencondoan,Dinhmuc,Macv,HeSoDinhMuc, NguyenCong,
            Dongia_CongDoan, NguoiHC_CV, NgayHC_CV
            from tblDMuc_LaoDong where Masp='" + txtMaSanPham.Text+"'");
            kn.dongketnoi();
            gridView1.Columns["Tensp"].GroupIndex = 0;
            gridView1.ExpandAllGroups();
        }
        private void DocDanhSachDinhMuc()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select id,Masp,Tensp,Macongdoan,
            Tencondoan,Dinhmuc,Macv,HeSoDinhMuc, NguyenCong,
            Dongia_CongDoan, NguoiHC_CV, NgayHC_CV
            from tblDMuc_LaoDong");
            kn.dongketnoi();
            gridView1.Columns["Tensp"].GroupIndex = 0;
            gridView1.ExpandAllGroups();
        }

        private void btnHieuChinh_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set Macv='{0}',
                             NguoiHC_CV=N'{1}',NgayHC_CV=GetDate(),HeSoDinhMuc='{2}',NguyenCong=N'{3}'
                             where id='{4}'",
                             rowData["Macv"],
                             txtNguoiDung.Text,
                             rowData["HeSoDinhMuc"],
                             rowData["NguyenCong"],
                             rowData["id"]
                             );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDanhSachDinhMucTheoTen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void btnDocDanhSachDinhMuc_Click(object sender, EventArgs e)
        {
            DocDanhSachDinhMuc();
            DocDanhMucNguonLuc();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(maSanPham_grid);
        }
     
        private void btnNguyenCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
                Resources.ShowDialog(); DocDanhMucNguonLuc();
        }
    }
}
