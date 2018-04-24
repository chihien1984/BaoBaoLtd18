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

namespace quanlysanxuat
{
    public partial class frmThemDM_vatlieuvt : DevExpress.XtraEditors.XtraForm
    {
        public frmThemDM_vatlieuvt()
        {
            InitializeComponent();
        }
      
        private void ListDMCL_vattu()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM tblDMCL_VATTU");
            kn.dongketnoi();
        }
        private void LoadDMCL_VatTu(object sender, EventArgs e)
        {
            ListDMCL_vattu();
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string Machatlieu = txtMaVatLieu.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Macl_vattu from tblDMCL_VATTU", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Machatlieu == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private void them(object sender,EventArgs e)
        {
            if (txtMaVatLieu.Text == "")
            {
                MessageBox.Show("Mã số rỗng"); return;
            }
            if (kiemtratontai())
            { MessageBox.Show("Mã số '" + txtMaVatLieu.Text + "' tồn tại, Chọn mã khác"); return; }
            else 
            { 
                ketnoi kn = new ketnoi();
                gridControl1.DataSource = kn.laybang("insert into tblDMCL_VATTU (Macl_vattu,Tencl_vattu,Nguoilap,Ngaylap) "
                +" values(N'"+txtMaVatLieu.Text+"',N'"+txtTenVatLieu.Text+"',N'"+txtUser.Text+"',Getdate())");
                kn.dongketnoi(); ListDMCL_vattu();
            }
        }
        private void sua(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("update tblDMCL_VATTU set Macl_vattu=N'" + txtMaVatLieu.Text + "', "
            + " Tencl_vattu=N'" + txtTenVatLieu.Text + "', Nguoilap=N'" + txtUser.Text + "',Ngaylap=Getdate() where id like "+txtid.Text+"");
            kn.dongketnoi();
            ListDMCL_vattu();
        }
        private void xoa(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("DELETE tblDMCL_VATTU where id like "+txtid.Text+"");
            kn.dongketnoi(); ListDMCL_vattu();
        }
        private void frmThemDM_vatlieuvt_Load(object sender, EventArgs e)
        {
            dpden_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString(); txtUser.Text = Login.Username;
        }
        private void Binding(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtid.Text = gridView1.GetFocusedRowCellDisplayText(id_grid);
            txtMaVatLieu.Text = gridView1.GetFocusedRowCellDisplayText(Maclvattu_grid);
            txtTenVatLieu.Text = gridView1.GetFocusedRowCellDisplayText(Tenclvattu_grid);
        }

    }
}