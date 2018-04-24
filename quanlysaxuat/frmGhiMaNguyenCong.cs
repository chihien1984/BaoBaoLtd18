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
    public partial class frmGhiMaNguyenCong : Form
    {
        public frmGhiMaNguyenCong()
        {
            InitializeComponent();
        }
        private void DocNguyenCong()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select Masp,Tensp,NguyenCong,Tothuchien,
                Tencondoan,HeSoDinhMuc,Dongia_CongDoan,
                Dinhmuc,id,NguoiHC_CV,NgayHC_CV
                from tblDMuc_LaoDong");
            kn.dongketnoi();
            gridView2.ExpandAllGroups();
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
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong 
                        set NguyenCong=N'{0}',HeSoDinhMuc=N'{1}',
                        NguoiHC_CV=N'{2}',NgayHC_CV=GetDate() where id='{3}'",
                    rowData["NguyenCong"],
                    rowData["HeSoDinhMuc"],
                    txtMemBer.Text,
                    rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocNguyenCong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex.Message);
            }
        }
        private void DocDSNguonLuc()//GridlookupEdit Repository chọn mã nguồn lực trong gridcontrol Áp mã nguồn lực
        {
            ketnoi Connect = new ketnoi();
            repository.DataSource = Connect.laybang(@"SELECT Ma_Nguonluc,
                Ten_Nguonluc,Nguoi,Ngay FROM tblResources");
            repository.DisplayMember = "Ma_Nguonluc";
            repository.ValueMember = "Ma_Nguonluc";
            repository.NullText = null;
            Connect.dongketnoi();
        }
        private void frmGhiMaNguyenCong_Load(object sender, EventArgs e)
        {
            DocNguyenCong();
            DocDSNguonLuc();
        }

        private void btnThemNguyenCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
            DocDSNguonLuc();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView2.GetFocusedDisplayText();
            txtMaSanPham.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid);
            txtNguyenCongID.Text = gridView2.GetFocusedRowCellDisplayText(NguyenCongID_grid);
        }

        private void btnXemBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}
