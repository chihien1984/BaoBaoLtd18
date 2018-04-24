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
using System.IO;

namespace quanlysanxuat
{
    public partial class frmQAControl : DevExpress.XtraEditors.XtraForm
    {
        public frmQAControl()
        {
            InitializeComponent();
        }
        public static string Madh = "";
        private void ThongTinQADH()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select Ngaytrienkhai,QC,NoidungQC,NgayQC,QA,Donvisp,IDSP,  "
                       + " CT.IdPSX,CT.nvkd,CT.SPLR,CT.SLSPLR,ngaytrienkhai,CT.madh,CT.LoaiDH,mabv,sanpham,Maubv, "
                       + " Mact, Ten_ct, So_CT, ChatlieuCT, soluongyc, tonkho, soluongsx, "
                       + " ngoaiquang, donvi, daystar, dayend, MaKH, CT.khachhang, xeploai, Ghichu, CT.MaPo, "
                       + " TrangThai, DH.Diengiai from tblchitietkehoach CT left join tblDONHANG DH on CT.madh = DH.madh where CT.madh like N'" + txtMadh.Text + "'");
            kn.dongketnoi();
        }
        private void ListAllQA(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select CT.madh+CT.mabv as IdGroup,Ngaytrienkhai,QC,NoidungQC,NgayQC,QA,Donvisp,IDSP,CT.IdPSX,CT.nvkd,CT.SPLR,CT.SLSPLR,ngaytrienkhai,CT.madh,CT.LoaiDH,mabv,sanpham,Maubv, "
                       + " Mact, Ten_ct, So_CT, ChatlieuCT, soluongyc, tonkho, soluongsx, "
                       + " ngoaiquang, donvi, daystar, dayend, MaKH, CT.khachhang, xeploai, Ghichu, CT.MaPo, "
                       + " TrangThai, DH.Diengiai from tblchitietkehoach CT left join tblDONHANG DH on CT.madh = DH.madh   where CT.ngaytrienkhai  "
                       + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by CT.ngaytrienkhai DESC");
            kn.dongketnoi();
            gridView3.ExpandAllGroups();
        }
        private void QAControl_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            txtMadh.Text = Madh;
            ThongTinQADH();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView3.GetFocusedDisplayText();
            txtMabv.Text = gridView3.GetFocusedRowCellDisplayText(Masp_grid3);
            txtMadh.Text = gridView3.GetFocusedRowCellDisplayText(Madh_grid3);
        }

        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMabv.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Bản vẽ chưa có trong hệ thống", "Liên hệ kế hoạch");
            }
        }

        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, txtMadh.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Đơn hàng chưa có trong hệ thống","Liên hệ kế hoạch"); }
        }

        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {}
        private void Layout_QTSX()
        {
            string pat = string.Format(@"{0}\QTSX-{1}.PDF", this.txtPath_QTSX.Text, txtMabv.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("QTSX chưa có trong hệ thống","Liên hệ QA"); }
        }
        private void btnQuiTrinhSX_Click(object sender, EventArgs e)//Gọi qui trình sản xuất
        {
            frmLoading f2 = new frmLoading(txtQuiTrinh.Text, txtPath_QTSX.Text);
            f2.Show();
        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMabv.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            frmLoading f2 = new frmLoading(txtMadh.Text, txtPath_PSX.Text);
            f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            frmLoading f2 = new frmLoading(txtMadh.Text, txtPath_PSX.Text);
            f2.Show();
        }
    }
}