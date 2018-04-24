using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraReports.UI;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

namespace quanlysanxuat
{
    public partial class UcBaocaoxuatkho : DevExpress.XtraEditors.XtraForm
    {
        public UcBaocaoxuatkho()
        {
            InitializeComponent();
        }
        private void Load_TongXuatKho(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO11 where ngaytrienkhai between '" + dpTu.Value.ToString("yyyy/MM/dd") + "' and '" + dpDen.Value.ToString("yyyy/MM/dd") + "'");
        }
        private void Load_CTXuatKho(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tbl11 where ngaynhan between '" + dpTu.Value.ToString("yyyy/MM/dd") + "' and '" + dpDen.Value.ToString("yyyy/MM/dd") + "'");
        }
        private void Load_XuatKhoIdsp(object sender, EventArgs e)
        {
            THXuatKhoTheoNhom();
        }
        private void THXuatKhoTheoNhom()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select mabv+''+chitietsanpham IDNhom,* from tbl11 
                                              where ngaynhan between '{0}' and '{1}'",
                                              dpTu.Value.ToString("yyyy-MM-dd"),
                                              dpDen.Value.ToString("yyy-MM-dd"));
            grThongKeNhom.DataSource = Model.Function.GetDataTable(sqlQuery);
            gvThongKeNhom.ExpandAllGroups();
            tabControl1.SelectedTab = tabPageThongKeNhom;
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            //string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{
            //    MessageBox.Show("Mã sản không khớp đúng");
            //}
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            //string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, cbMaPSX.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{ MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }

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
            //frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            //f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            //frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_PSX.Text);
            //f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            //frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_KHSX.Text);
            //f2.Show();
        }
        //formload
        private void UcBaocaoxuatkho_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy"); dpDen.Text = DateTime.Now.ToString();
            THXuatKhoTheoNhom();
        }
        
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView2.GetFocusedDisplayText();
            //txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid1);
            //cbMaPSX.Text = gridView2.GetFocusedRowCellDisplayText(madh);
            //txtIdsp.Text= gridView2.GetFocusedRowCellDisplayText(Id_grid1);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
        private void btnExportChiTiet_Click(object sender, EventArgs e)
        {
            gridView5.Columns["MaGH"].GroupIndex = -1;
            gridControl4.ShowPrintPreview();
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            grThongKeNhom.ShowPrintPreview();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView3.GetFocusedDisplayText();
            //txtMasp.Text = gridView3.GetFocusedRowCellDisplayText(Masp_grid2);
            //cbMaPSX.Text = gridView3.GetFocusedRowCellDisplayText(Madh_grid2);
            //txtIdsp.Text = gridView3.GetFocusedRowCellDisplayText(IdKH_grid2);
        }

        private void btnTongSLXuatKho_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select T11.*,PT.KetThucTo11,PT.tyle from tbl11 T11 left outer join
            PHANTICH_TIENDO11 PT on T11.IDSP=PT.IDSP where 
            ngaynhan between '" + dpTu.Value.ToString("yyyy/MM/dd") + "' and '" + dpDen.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
            gridView5.Columns["MaGH"].GroupIndex = 0;
            gridView5.ExpandAllGroups();
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView5.GetFocusedDisplayText();
            //txtMasp.Text = gridView5.GetFocusedRowCellDisplayText(Masp_grid5);
            //cbMaPSX.Text = gridView5.GetFocusedRowCellDisplayText(madh_grid5);
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            //string Gol = "";
            //Gol = gridView4.GetFocusedDisplayText();
            //txtMasp.Text = gridView4.GetFocusedRowCellDisplayText(Masp_grid5);
            //cbMaPSX.Text = gridView4.GetFocusedRowCellDisplayText(madh_grid5);
        }

        private void btnThongKeSoLuongGiao_Click(object sender, EventArgs e)
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"execute SoDatHangSoSanhSoGiao '{0}','{1}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grThongKeSoGiao.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }

        private void btnExportThongKeSoXuat_Click(object sender, EventArgs e)
        {
            grThongKeSoGiao.ShowPrintPreview();
        }
    }
}