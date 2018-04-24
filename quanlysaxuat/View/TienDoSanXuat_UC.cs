using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class TienDoSanXuat_UC : DevExpress.XtraEditors.XtraForm
    {
        public TienDoSanXuat_UC()
        {
            InitializeComponent();
        }
        private void GoifrmQA(object sender, EventArgs e)
        {
            //frmQAControl.Madh = cbMaPSX.Text;
            frmQAControl fQA = new frmQAControl();
            fQA.ShowDialog();
        }
       
        private void TheHienTienDoTheoThoiGian()
        {
            string sqlQuery = string.Format(@"SELECT * FROM PHANTICH_TIENDO01 
                WHERE cast (ngaytrienkhai as Date)
                between '{0}' and '{1}'",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gridView1.ExpandAllGroups();
        }
        private void LOAD_CTGIAOHANG(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select t.BTPT01,t.ngaynhan,
                k.* from tbl01 t 
                inner join 
                tblchitietkehoach k
                on t.IDSP=k.IDSP 
                where cast(t.ngaynhan as date) 
                between '{0}' and '{1}'
                order by ngaynhan DESC",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            gridControl2.DataSource = kn.laybang(sqlQuery);
            gridView2.ExpandAllGroups();
            kn.dongketnoi(); gridView1.ExpandAllGroups();
        }


        #region formload
        private void frmPr01_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            TheHienTienDoTheoThoiGian();
        }
        #endregion


        private void btnexport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnXemTuNgayDenNgay(object sender, EventArgs e)
        {
            TheHienTienDoTheoThoiGian();
        }

        private void btnexport_Click_1(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
        }
        private void OPENFILE()//Mở folder
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "PDF files (*.PDF)|*.jpg|All files (*.*)|*.*";
            if (oFile.ShowDialog() == DialogResult.OK)
            { txtPath_MaSP.Text = oFile.FileName.Substring(oFile.FileName.LastIndexOf("\\") + 1); }
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMaSanPham.Text);
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

        private void Layout_QTSX()//Goi quy trinh san xuat
        {
            //string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_QTSX.Text, txtQuiTrinh.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{ MessageBox.Show("Quy trình này chưa được lập", "THÔNG BÁO"); }
        }
        private void LayoutQuyTrinhSX(object sender, EventArgs e)//Gọi qui trình sản xuất
        {
            //frmLoading f2 = new frmLoading(txtQuiTrinh.Text, txtPath_QTSX.Text);
            //f2.Show();
        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }
      
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + maDonHang + "'");
            RpPhieusanxuat rpPHIEUSANXUAT = new RpPhieusanxuat();
            rpPHIEUSANXUAT.DataSource = dt;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = maDonHang;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            //frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_KHSX.Text);
            //f2.Show();
        }
        private string maDonHang;
        private void gridControl1_Click(object sender, EventArgs e)//Sự kiện binding textbox
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(mabv_grid);
            maDonHang = gridView1.GetFocusedRowCellDisplayText(madh);
            Load_Sanxuatgiaohang();
        }

        private void txtidsp_TextChanged(object sender, EventArgs e)
        {
            //Load_Sanxuatgiaohang();
        }

        private void Load_Sanxuatgiaohang()//Filter chi tiết dữ liệu giao hàng
        {
            //ketnoi kn = new ketnoi();
            //gridControl2.DataSource = kn.laybang("select * from tbl01 where IDSP like '" + txtKeHoachID.Text + "'");
            //gridView2.ExpandAllGroups();
            //kn.dongketnoi();
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnExportChiTiet_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public static KhuonMau KhuonMau;
        private void VitriKhuon(object sender, EventArgs e)
        {
            KhuonMau.maSP = txtMaSanPham.Text;
            if (KhuonMau == null || KhuonMau.IsDisposed)
                KhuonMau = new KhuonMau();
            KhuonMau.Show();
        }
        private void btnAccept_Click(object sender, EventArgs e)//Event chấp nhận đơn hàng
        {
            try
            {
                //{
                //    SqlConnection cn = new SqlConnection(Connect.mConnect);
                //    cn.Open();
                //    SqlCommand cmd = new SqlCommand("ACCEP1", cn);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = txtUser.Text;
                //    cmd.Parameters.Add(new SqlParameter("@idsp", SqlDbType.BigInt)).Value = txtKeHoachID.Text;
                //    cmd.Parameters.Add(new SqlParameter("@trangthai", SqlDbType.NVarChar)).Value = txtStatus.Text;
                //    cmd.ExecuteNonQuery();
                //    cn.Close();
                //    ListAccept();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void ListAccept()
        {
            //ketnoi kn = new ketnoi();
            //gridControl1.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO1 where madh like N'" + cbMaPSX.Text + "'");
            //kn.dongketnoi();
        }
        #region event host key
        public static frmPrVatTu TienDoVatTu;
        public static frmTinhCongDoan TinhCongDoan;
        private void gridControl1_KeyDown_1(object sender, KeyEventArgs e)//Sự kiện Ctrl+A
        {
            switch (e.KeyCode)
            {
                //Event thời gian lên máy
                case Keys.F1:
                    if (e.Control)
                    {
                        if (TinhCongDoan == null || TinhCongDoan.IsDisposed)
                        {
                            //frmTinhCongDoan.maDonHang = cbMaPSX.Text;
                            //frmTinhCongDoan.soLuongSX = gridView1.GetFocusedRowCellDisplayText(Soluongsx_grid);
                            //frmTinhCongDoan.maSanPham = txtMaSanPham.Text;
                            //frmTinhCongDoan.donHangID = txtDonHangID.Text;
                            //frmTinhCongDoan.keHoachID = txtKeHoachID.Text;

                            //string Gol = "";
                            //Gol = gridView1.GetFocusedDisplayText();
                            //frmTinhCongDoan.sanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
                            //TinhCongDoan = new frmTinhCongDoan();
                            //TinhCongDoan.Show();
                            //frmTinhCongDoan.ActiveForm.TopMost = true;
                        }
                    }
                    break;
                //Event call Tien do vat tu 
                case Keys.F2:
                    if (TienDoVatTu == null || TienDoVatTu.IsDisposed)
                    {
                        //frmPrVatTu.maSanPham = txtMasp.Text;
                        TienDoVatTu = new frmPrVatTu();
                        TienDoVatTu.Show();
                        frmPrVatTu.ActiveForm.TopMost = true;
                    }
                    break;
            }
            //switch (e.KeyCode)
            //{
            //    //event call Tien do vat tu 
            //    case Keys.F2:
            //        if (TienDoVatTu == null || TienDoVatTu.IsDisposed)
            //        {
            //            //frmPrVatTu.maSanPham = txtMasp.Text;
            //            TienDoVatTu = new frmPrVatTu();
            //            TienDoVatTu.Show();
            //            frmPrVatTu.ActiveForm.TopMost = true;
            //        }
            //        break;
            //}
        }
        #endregion
        #region Đọc thông tin chi tiết vật tư, khuôn, thời gian lên máy
        private string IDChiTietDonHang;
        private string IDKeHoach;

        private void DocThongTinChiTiet()
        {
            string soLuong;
            string tenSanPham;
            string thongBao;
            soLuong = gridView1.GetFocusedRowCellDisplayText(Soluongsx_grid);
            tenSanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
            tenSanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
            thongBao = gridView1.GetFocusedRowCellDisplayText(tienDoChung_grid1);
            frmChiTietSanXuat DSChiTietSanXuat = new frmChiTietSanXuat(maDonHang, txtMaSanPham.Text,
            tenSanPham, Login.Username, IDChiTietDonHang, IDKeHoach,
            soLuong, thongBao, txtBoPhan.Text);
            DSChiTietSanXuat.ShowDialog();
            TheHienTienDoTheoThoiGian();
        } 
        #endregion  
        private void button1_Click(object sender, EventArgs e)
        {
            frmTinhCongDoan tinhcongdoan = new frmTinhCongDoan();
            tinhcongdoan.Show();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DocThongTinChiTiet();
        }

        private void btnDinhMucCongDoan_Click(object sender, EventArgs e)
        {
            //frmDinhMucCongDoan dinhMucCongDoan = new frmDinhMucCongDoan(txtDonHangID.Text);
            //dinhMucCongDoan.ShowDialog();
        }
    }
}
