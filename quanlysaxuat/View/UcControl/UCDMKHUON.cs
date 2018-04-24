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
    public partial class UCDMKHUON : DevExpress.XtraEditors.XtraForm
    {
        public UCDMKHUON()
        {
            InitializeComponent();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtmasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Liên hệ BP.Kỹ Thuật Áp Mã SP");
            }
        }
        private void SukienGoiMASP_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void btnDMkhuon_Click(object sender, EventArgs e)
        {
            ketnoi Connect = new ketnoi();
            gridControl1.DataSource = Connect.laybang("SELECT Ngaykh_cat,Ngaylukho,Ngaycat_Hoanthanh,Ngayrap_Hoanthanh,BPsudung,Ngaymuon,Ngaytra,Ma_khuon,DM.Mact,DM.Masp,SP.Tensp,Manhom_khuon,Ten_khuon,Dacdiem_khuon,Soluong_khuon, "
                   + " Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, DM.Manv, Ghichu,GETDATE() as Today, DM.Ngaycat_Hoanthanh, DM.Ngayrap_Hoanthanh "
                   + " , DM.Mabp, DM.BPsudung, DM.Nguoiluukhuon, DM.Vitrikhuon, DM.Tinhtrang_khuon, DM.CodeMK from tblDM_KHUON DM left "
                   + " join tblSANPHAM_CT CT on DM.Mact = CT.Mact join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp"
                   + " and convert(Date,Ngaybatdau,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' ");
            gridView1.ExpandAllGroups();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = ""; Gol = gridView1.GetFocusedDisplayText();
            txtmasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);           
        }

        private void UCDMKHUON_Load(object sender, EventArgs e)
        {
           
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
        }
    }
}
