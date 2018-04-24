using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class frmPr_BamHan : DevExpress.XtraEditors.XtraForm
    {
        public frmPr_BamHan()
        {
            InitializeComponent();
        }
        private void LoadGrid1()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO_BAMHAN");
            gridView1.ExpandAllGroups();
        }
        private void LOAD_ALL()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO_BAMHAN");
            gridView1.ExpandAllGroups();
        }
        private void LOAD_TRE()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO_BAMHAN WHERE STATUS <> 'HOAN THANH'");
            gridView1.ExpandAllGroups();
        }
        private void LOAD_TIME()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" SELECT * FROM PHANTICH_TIENDO_BAMHAN WHERE convert(Date,ngaytrienkhai,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi(); gridView1.ExpandAllGroups();
        }
       
        private void frmPr_BamHan_Load(object sender, EventArgs e)
        {
            txtUser.Text = Login.Username;
            LOAD_TRE(); dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
        }

        private void btnloadgrid1_Click(object sender, EventArgs e)
        {
            LOAD_ALL();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            frmPr_BamHan_Load(sender, e);
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnLoadTime_Click(object sender, EventArgs e)
        {
            LOAD_TIME();
        }

        private void btnLoadTre_Click(object sender, EventArgs e)
        {
            LOAD_TRE();
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LOAD_ALL();
        }

        private void btnexport_Click_1(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
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
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaPSX.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã kế hoạch này chưa đúng"); }

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
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_KHSX.Text);
            f2.Show();
        }
       

        private void btnExportChiTiet_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ACCEP1", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@idsp", SqlDbType.BigInt)).Value = txtKeHoachID.Text;
                    cmd.Parameters.Add(new SqlParameter("@trangthai", SqlDbType.NVarChar)).Value = txtStatus.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    ListAccept();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        private void ListAccept()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM PHANTICH_TIENDO1 where madh like N'" + cbMaPSX.Text + "'");
            kn.dongketnoi();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtKeHoachID.Text = gridView1.GetFocusedRowCellDisplayText(KeHoachID_grid);
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(mabv_grid);
            cbMaPSX.Text = gridView1.GetFocusedRowCellDisplayText(madh);
            txtQuiTrinh.Text = gridView1.GetFocusedRowCellDisplayText(quytrinh_grid1);
            txtDonHangID.Text = gridView1.GetFocusedRowCellDisplayText(DonHangID_grid);
            Load_Sanxuatgiaohang();
        }
        private void Load_Sanxuatgiaohang()//Filter chi tiết dữ liệu giao hàng
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblHanBam where IDSP like '" + txtKeHoachID.Text + "'");
            gridView2.ExpandAllGroups();
            kn.dongketnoi();
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
                            frmTinhCongDoan.maDonHang = cbMaPSX.Text;
                            frmTinhCongDoan.soLuongSX = gridView1.GetFocusedRowCellDisplayText(Soluongsx_grid);
                            frmTinhCongDoan.maSanPham = txtMasp.Text;
                            frmTinhCongDoan.donHangID = txtDonHangID.Text;
                            frmTinhCongDoan.keHoachID = txtKeHoachID.Text;

                            string Gol = "";
                            Gol = gridView1.GetFocusedDisplayText();
                            frmTinhCongDoan.sanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
                            TinhCongDoan = new frmTinhCongDoan();
                            TinhCongDoan.Show();
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
        private void DocThongTinChiTiet()
        {
            string soLuong;
            string tenSanPham;
            string thongBao;
            soLuong = gridView1.GetFocusedRowCellDisplayText(Soluongsx_grid);
            tenSanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
            tenSanPham = gridView1.GetFocusedRowCellDisplayText(tensp_grid);
            thongBao = gridView1.GetFocusedRowCellDisplayText(tienDoChung_grid1);
            frmChiTietSanXuat DSChiTietSanXuat = new frmChiTietSanXuat(cbMaPSX.Text, txtMasp.Text,
            tenSanPham, txtUser.Text, txtDonHangID.Text, txtKeHoachID.Text,
            soLuong, thongBao, txtTableID.Text);
            DSChiTietSanXuat.ShowDialog();
            LOAD_TRE();
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
        public static KhuonMau KhuonMau;
        private void btnVTKhuon_Click(object sender, EventArgs e)
        {
            KhuonMau.maSP = txtMasp.Text;
            if (KhuonMau == null || KhuonMau.IsDisposed)
                KhuonMau = new KhuonMau();
            KhuonMau.Show();
        }

        private void BtnGoiQuyTrinh_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtQuiTrinh.Text, txtPath_QTSX.Text);
            f2.Show();
        }
    }
}
