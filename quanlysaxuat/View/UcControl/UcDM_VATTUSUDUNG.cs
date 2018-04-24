using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace quanlysanxuat
{
    public partial class UcDM_VATTUSUDUNG : DevExpress.XtraEditors.XtraForm
    {
        public UcDM_VATTUSUDUNG()
        {
            InitializeComponent();
        }
        private void LOADDM_CTSANPHAM()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Donviquydoi,NVKD,NgayDK_ve,MaDN_VATTU,CodeVatllieu,madh,masp,Tenquicachsp,Soluongsanpham,Donvi_sanpham,Ma_CT, "
              + " Ten_CT, QC_CT, Ten_vattu, SL_vattucan, SL_vattutonkho, SL_vattumua, Dongia, Donvi_vattu, NCC, NguoiGD, fax, nhanviendathang, Kiemkho, "
              + " nguoikiemkho, ngaykiemkho, Duyetsanxuat, VAT, Kiemkho, Ghichu_dathangmua, Ghichu_denghimua, DK_TCmua, quyetdinh "
              + " FROM tblvattu_dauvao");
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, cbMaPSX.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }

        }
        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {
            //string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaPSX.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{ MessageBox.Show("Hiện mã kế hoạch này chưa đúng"); }

        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_PSX.Text);
            f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_PSX.Text);
            f2.Show();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txtTensp.Text = gridView1.GetFocusedRowCellDisplayText(Tensp_grid1);
            cbMaPSX.Text= gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
        }

        private void btnShow_VT_Click(object sender, EventArgs e)
        {
            LOADDM_CTSANPHAM();
        }
    }
}
