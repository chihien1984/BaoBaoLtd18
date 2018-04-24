using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class UCTongHopZ : UserControl
    {
        public UCTongHopZ()
        {
            InitializeComponent();
        }

        private void List_TongDuToan(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select KyCP.*,kb.masp,kb.tenz from tblz_doitongtaphopchiphi KyCP "
            +"left outer join tblz_khaibao kb on KyCP.Maz = kb.maz where convert(date,KyCP.ngaylap,101)  "
            +"between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' "
            + "and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' and z_tong is not null");
            kn.dongketnoi();
        }

        private void List_DuToanChiTiet(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select cp.*,kb.donvi from tblz_dmchiphi cp left outer join "
            +"tblz_khaibao kb on cp.id_z=kb.maz where convert(date,cp.ngaylap,101) "
            +"between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' "
            +"and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void List_DuToanChiTietMaz()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select cp.*,kb.donvi from tblz_dmchiphi cp left outer join "
            + "tblz_khaibao kb on cp.id_z=kb.maz where id_kycp like '" + txtidtonghopcp.Text+ "'");
            kn.dongketnoi();
        }
        private void UCTongHopZ_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
        }

        private void gridControl1_Click(object sender, EventArgs e)//Binding Du toan
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtidtonghopcp.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            txtTenz.Text = gridView1.GetFocusedRowCellDisplayText(Sanpham_grid1);
            txtUser.Text = gridView1.GetFocusedRowCellDisplayText(nguoilap_grid1);
            dpNgaylap.Text = gridView1.GetFocusedRowCellDisplayText(ngaylap_grid1);
            List_DuToanChiTietMaz();
        }

        private void gridControl2_Click(object sender, EventArgs e)//Binding chi tiết dự toán
        {
           
        }

        private void btnXuatDuToan_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnXuatChiTiet_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }
    }
}
