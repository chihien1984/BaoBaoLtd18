using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class frmThemVatLieu : DevExpress.XtraEditors.XtraForm
    {
        public frmThemVatLieu()
        {
            InitializeComponent();
        }
        private void LOAD_DMVATLIEU()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tblVatLieuSanPham");
        }
        private void THEM_VATLIEU(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("insert into tblVatLieuSanPham (Mavatlieu,TenVatlieu) values(N'" + txtMaVatLieu.Text + "',N'" + txtTenVatLieu.Text + "')");
            LOAD_DMVATLIEU();
        }
        private void SUA_VATLIEU(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("update tblVatLieuSanPham set Mavatlieu=N'" + txtMaVatLieu.Text + "',TenVatlieu=N'" + txtTenVatLieu.Text + "' where Code '" + txtCode.Text + "' ");
            LOAD_DMVATLIEU();
        }
        private void XOA_VATLIEU(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("delete tblVatLieuSanPham where Code like  '" + txtCode.Text + "'");
            LOAD_DMVATLIEU();
        }
        private void BINDING(object sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMaVatLieu.Text = gridView1.GetFocusedRowCellDisplayText(Mavatlieu_grid);
            txtTenVatLieu.Text = gridView1.GetFocusedRowCellDisplayText(TenVatLieu_grid);
            txtCode.Text = gridView1.GetFocusedRowCellDisplayText(Code_grid);
        }
        private void FrmThemVatLieu_Load(object sender, EventArgs e)
        {
            LOAD_DMVATLIEU(); dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
        }

        private void FrmThemVatLieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
    }
}
