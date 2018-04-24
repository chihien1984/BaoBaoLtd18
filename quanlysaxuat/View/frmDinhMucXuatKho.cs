using DevExpress.XtraPrinting;
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
    public partial class frmDinhMucXuatKho : Form
    {
        public frmDinhMucXuatKho()
        {
            InitializeComponent();
        }

        private void frmDinhMucXuatKho_Load(object sender, EventArgs e)
        {
            dpDinhMucVatTuDHTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDinhMucVatTuDHDen.Text = DateTime.Now.ToString();
            DocDSDinhMucTheoNgay();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView1.GetFocusedDisplayText();
            txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(masp_grid1);
            txtMaDonHang.Text = gridView1.GetFocusedRowCellDisplayText(madh_grid1);
            txtTenSanPham.Text = gridView1.GetFocusedRowCellDisplayText(tensp_grid1);
        }

        private void btnDinhMucNguyenVatLieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from PHIEUSANXUAT where madh like N'" + txtMaDonHang.Text + "'");
            XRPhieuSX_DaDuyet xrPSX_VatTu = new XRPhieuSX_DaDuyet();
            xrPSX_VatTu.DataSource = dt;
            xrPSX_VatTu.DataMember = "Table";
            xrPSX_VatTu.CreateDocument(false);
            xrPSX_VatTu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(xrPSX_VatTu.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnDocDSDMVatTu_Click(object sender, EventArgs e)
        {
            DocDSDinhMucTheoNgay();
        }
        private void DocDSDinhMucTheoNgay()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang(@"select DV.*, CT.ngoaiquang,T.TonCuoi from tblvattu_dauvao DV 
             left outer join tblDHCT CT on DV.Iden=CT.Iden
			 left outer join tblDM_VATTU T
			 on T.Ma_vl=DV.Mavattu where convert(Date,Ngaylap_DM,101) 
             between '" + dpDinhMucVatTuDHTu.Value.ToString("yyyy/MM/dd") + "' and '" + dpDinhMucVatTuDHDen.Value.ToString("yyyy/MM/dd") + "' order by CodeVatllieu DESC");
            gridView1.ExpandAllGroups();
        }
        private void btnDocDSDinhMucVatTu_Click(object sender, EventArgs e)
        {
            DocDSDinhMucVatLieu();
        }
        private void DocDSDinhMucVatLieu()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang(@"
             select DV.*, CT.ngoaiquang,T.TonCuoi from tblvattu_dauvao DV 
             left outer join tblDHCT CT on DV.Iden=CT.Iden
			 left outer join tblDM_VATTU T
			 on T.Ma_vl=DV.Mavattu order by CodeVatllieu DESC");
            gridView1.ExpandAllGroups();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}
