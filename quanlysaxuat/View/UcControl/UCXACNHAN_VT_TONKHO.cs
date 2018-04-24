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
using System.IO;
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class UCXACNHAN_VT_TONKHO : UserControl
    {
        public UCXACNHAN_VT_TONKHO()
        {
            InitializeComponent();
        }
        string Gol = "";
        private void UCXACNHAN_VT_TONKHO_Load(object sender, EventArgs e)
        {
            txtUser.Text = Login.Username; LoadMaDH();
            if (Login.Username == "Lê Thị Diểm Trinh"|| Login.Username == "Nguyễn Thành Hòa")
            {
                btncapnhat_tonkho.Visible = true;
            }
            DocDSDinhMucTheoNgay();
        }
        private void LoadDanhMuc_DINHMUCVATTU()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang(@"select DV.*, CT.ngoaiquang,T.TonCuoi from tblvattu_dauvao DV 
             left outer join tblDHCT CT on DV.Iden=CT.Iden
			 left outer join tblDM_VATTU T
			 on T.Ma_vl=DV.Mavattu
             where  madh like N'" + cbMaDH.Text + "' order by CodeVatllieu DESC");
            gridView1.ExpandAllGroups();
        }
        private void btnDocDonHangThoiGian_Click(object sender, EventArgs e)
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
             between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by CodeVatllieu DESC");
            gridView1.ExpandAllGroups();
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
        private void btnShow_VT_Click(object sender, EventArgs e)
        {
            DocDSDinhMucVatLieu();
        }
        private void LoadMaDH()
        {
            ketnoi Connect = new ketnoi();
            cbMaDH.DataSource = Connect.laybang("Select Madh from tblDHCT order by madh DESC");
            cbMaDH.ValueMember = "Madh";
            cbMaDH.DisplayMember = "Madh";
            Connect.dongketnoi();
        }
        private void btncapnhat_tonkho_Click(object sender, EventArgs e)
        {
            if (txttrangthaiDH.Text != "")
                MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (string.IsNullOrEmpty(txttrangthaiDH.Text) && cbMaDH.Text != "" && txtTenVatlieu.Text != "" && txttensp.Text != "")
            {
                SqlConnection con = new SqlConnection();
                //decimal GIAMUA = Convert.ToDecimal(txtGiamua.Text);
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set SL_vattutonkho=@SL_vattutonkho,Kiemkho=@Kiemkho, nguoikiemkho=@nguoikiemkho,ngaykiemkho=GetDate() "
                   + " where CodeVatllieu like '" + txtIden.Text + "' and Duyetsanxuat is null", con))
                    {
                        cmd.Parameters.Add("@SL_vattutonkho", SqlDbType.NVarChar).Value = txtSL_Ton.Text;
                        cmd.Parameters.Add("@nguoikiemkho", SqlDbType.NVarChar).Value = txtUser.Text;
                        cmd.Parameters.Add("@Kiemkho", SqlDbType.NVarChar).Value = cbkiemkho.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close(); LoadDanhMuc_DINHMUCVATTU();
                }
                else
                {
                    MessageBox.Show("Cần kiểm tra nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtIden.Text = gridView1.GetFocusedRowCellDisplayText(Code_grid1);
            cbMaDH.Text = gridView1.GetFocusedRowCellDisplayText(madh_grid1);
            txtmasp.Text= gridView1.GetFocusedRowCellDisplayText(masp_grid1);
            txttensp.Text= gridView1.GetFocusedRowCellDisplayText(tensp_grid1);
            txtQC_CT.Text= gridView1.GetFocusedRowCellDisplayText(QCCT_grid1);
            txtTenVatlieu.Text= gridView1.GetFocusedRowCellDisplayText(Tenloaivt_grid1);
            txtSL_VT_DM.Text= gridView1.GetFocusedRowCellDisplayText(SLvattuDM_grid1);
            txtDonvi_sp.Text = gridView1.GetFocusedRowCellDisplayText(donvisp_grid1);
            cbdonvi_vatlieu.Text = gridView1.GetFocusedRowCellDisplayText(Donvi_VatTu);
            txttrangthaiDH.Text= gridView1.GetFocusedRowCellDisplayText(Pheduyet_grid1);
            dpngaykiem.Text= gridView1.GetFocusedRowCellDisplayText(Ngaykiemkho_grid1);
            cbkiemkho.Text= gridView1.GetFocusedRowCellDisplayText(Kiemkho_grid1);
            txtSL_Ton.Text = gridView1.GetFocusedRowCellDisplayText(SoluongTon_grid1);
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
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, cbMaDH.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }
        }
        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaDH.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã kế hoạch này chưa đúng"); }
        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaDH.Text, txtPath_PSX.Text);
            f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaDH.Text, txtPath_KHSX.Text);
            f2.Show();
        }
   

        private void cbMaDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDanhMuc_DINHMUCVATTU();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
        }

        private void btnDinhMucNguyenVatLieu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from PHIEUSANXUAT where madh like N'" + cbMaDH.Text + "'");
            XRPhieuSX_DaDuyet xrPSX_VatTu = new XRPhieuSX_DaDuyet();
            xrPSX_VatTu.DataSource = dt;
            xrPSX_VatTu.DataMember = "Table";
            xrPSX_VatTu.CreateDocument(false);
            xrPSX_VatTu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDH.Text;
            PrintTool tool = new PrintTool(xrPSX_VatTu.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }

     
    }
}
