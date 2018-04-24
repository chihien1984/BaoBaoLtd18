using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;

namespace quanlysanxuat
{
    public partial class UcTongKetXuatKho_Gia : DevExpress.XtraEditors.XtraForm
    {
        public UcTongKetXuatKho_Gia()
        {
            InitializeComponent();
        }
        private void LOAD_XUATKHO() {
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"select 
			 T11.ngaynhan,T11.MaGH,T11.SoChungTu_XK,T11.madh,T11.MaPo,T11.MaSP_Khach,T11.TenSP_KH, 
             T11.chitietsanpham,T11.mabv,T11.sanpham, T11.SL_DH, TH.Soluong, 
             T11.SL_LR, T11.BTPT11, T11.TRONGLUONG11, TH.dongia, convert(bigint, T11.BTPT11 * TH.dongia) as ThanhTien, 
			 case when (TH.Soluong-BTPT11)>=0 then (BTPT11-TH.Soluong) end Thieu,
		     case when (BTPT11-TH.Soluong)>=0 then (BTPT11-TH.Soluong) end Du,
             T11.IDSP, TH.IdPSX, TH.Iden,T11.nvkd,T11.khachhang,T11.Diachi_KH,donvi,T11.Maubv,
             T11.ngoaiquang,T11.ghichu,T11.Diengiai,T11.TongCongBaoBi from tbl11 T11 left outer join 
             (select CT.IDSP,CT.IdPSX, DHCT.Iden, DHCT.Soluong, DHCT.thanhtien, DHCT.dongia  from tblDHCT DHCT 
             left outer join tblchitietkehoach CT on DHCT.Iden = CT.IdPSX) TH on T11.IDSP = TH.IDSP  
             where MaGH is not null and convert(Date,ngaynhan,101) 
            between '{0}' and '{1}'",
            dptu_ngay.Value.ToString("yyyy/MM/dd"),
            dpden_ngay.Value.ToString("yyyy/MM/dd"));
            gridControl4.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void LOAD_DONGIAXUAT(object sender, EventArgs e)
        {
            LOAD_XUATKHO();
            gridView5.Columns["MaGH"].GroupIndex = 0;
            gridView5.ExpandAllGroups();
        }
        private void UcTongKetXuatKho_Gia_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            LOAD_XUATKHO();
            gridView5.ExpandAllGroups();
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void gridControl4_Click_1(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView5.GetFocusedDisplayText();
            txtIdsp.Text = gridView5.GetFocusedRowCellDisplayText(IDDH_grid5);
            cbMaPSX.Text = gridView5.GetFocusedRowCellDisplayText(Madh_grid5);
            txtMaphieu.Text = gridView5.GetFocusedRowCellDisplayText(MaGH_grid5);
            txtMasp.Text = gridView5.GetFocusedRowCellDisplayText(Masp_grid5);
        }

        private void txtMasp_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbMaPSX_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void dptu_ngay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void dpden_ngay_ValueChanged(object sender, EventArgs e)
        {

        }
        private void LoadDH_CT()//List chi tiết đơn đặt hàng
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("select * from tblDHCT  where convert(Date,thoigianthaydoi,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            Connect.dongketnoi();
        }
        private void btnrefresh_CTDH_Click(object sender, EventArgs e)
        {
            LoadDH_CT();
        }

        private void BtnXuatPhieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewXuatKho_Gia where MaGH like N'" + txtMaphieu.Text + "' ");
            XRGIAXUAT XRXUATKHO_GIA = new XRGIAXUAT();
            XRXUATKHO_GIA.DataSource = dt;
            XRXUATKHO_GIA.DataMember = "Table";
            XRXUATKHO_GIA.ShowPreviewDialog();
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
            { MessageBox.Show("Chưa có phiếu sản xuất trên máy chủ"); }

        }
        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaPSX.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Chưa có phiếu sản xuất trên máy chủ"); }

        }
        private void btnXemBV_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnLayout_PSX_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_PSX.Text);
            f2.Show();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            gridControl4.ShowPrintPreview();
        }

        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            gridView5.Columns["MaGH"].GroupIndex=-1;
        }
    }
}
