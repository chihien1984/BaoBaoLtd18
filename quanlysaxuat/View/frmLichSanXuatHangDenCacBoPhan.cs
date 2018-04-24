using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmLichSanXuatHangDenCacBoPhan : Form
    {
        public frmLichSanXuatHangDenCacBoPhan()
        {
            InitializeComponent();
        }

        private void frmLichSanXuatHangDenCacBoPhan_Load(object sender, EventArgs e)
        {
            dptungay.Text = DateTime.Today.ToString("01-MM-yyyy");
            dpdenngay.Text = DateTime.Today.ToString("dd-MM-yyyy");
            DocLichSanXuatQuaCacTo();
            this.gridView1.Appearance.Row.Font = new Font("Times New Roman", 7f);
        }
        private void DocLichSanXuatQuaCacTo()
        {
            string donHangID =UC_THEMDONHANG.donHangID;
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from viewCHITIET_BPSX where DonHangID='{0}'",
                donHangID);
            gridControl1.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void DocToanBoLichSanXuat()
        {
            string donHangID = UC_THEMDONHANG.donHangID;
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from viewCHITIET_BPSX where cast(NgayGhiKeHoach as date) between '{0}' and '{1}'",
                dptungay.Value.ToString("MM-dd-yyyy"),
                dpdenngay.Value.ToString("MM-dd-yyyy"));
            gridControl1.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            DocToanBoLichSanXuat();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
    }
}
