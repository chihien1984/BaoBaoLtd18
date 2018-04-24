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

namespace quanlysanxuat.View.UcControl
{
    public partial class frmDinhMucVatTu : DevExpress.XtraEditors.XtraForm
    {
        public frmDinhMucVatTu()
        {
            InitializeComponent();
        }

        private void btnTraCuuDinhMuc_Click(object sender, EventArgs e)
        {
            TraCuuDinhMucDonVi();
        }

        private void UCDinhMucVatTu_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Today.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Today.ToString("dd-MM-yyyy");
            TraCuuDinhMucDonVi();
            ThDinhMucVatLieuDonHang();
        }
        private void TraCuuDinhMucDonVi()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select * from DinhMucVatTu where TenSP <>'' 
                        order by  SanPhamID Desc");
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            grDinhMucVatTuDonVi.DataSource = dt;
            gvDinhMucVatTuDonVi.ExpandAllGroups();
        }



        private void btnExportDinhMuc_Click(object sender, EventArgs e)
        {
            gvDinhMucVatTuDonVi.ShowPrintPreview();
        }

        private void grDinhMucVatTuDonVi_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabPageDinhMucDonHang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
         
        }

        private void btnDeNghiVatTuTheoNgay_Click(object sender, EventArgs e)
        {
            ThDinhMucVatLieuDonHang();
        }
        private async void ThDinhMucVatLieuDonHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from tblvattu_dauvao 
                      where Ngaylap_DM between '{0}' and '{1}'",
                      dpTu.Value.ToString("yyyy-MM-dd"),
                      dpDen.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grDinhMucVatLieuDonHang.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gvDinhMucVatLieuDonHang.ShowPrintPreview();
        }
    }
}
