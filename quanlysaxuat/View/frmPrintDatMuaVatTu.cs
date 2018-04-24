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
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class frmPrintDatMuaVatTu : DevExpress.XtraEditors.XtraForm
    {
        Clsketnoi knn = new Clsketnoi();
        string Gol = "";
        //SqlCommand cmd;
        public frmPrintDatMuaVatTu()
        {
            InitializeComponent();
        }

        private void frmPrintDatMuaVatTu_Load(object sender, EventArgs e)
        {
            MaDatVatTu(); dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
        }
        private void btnCapnhatDatVatTu_Click(object sender, EventArgs e)
        {
            MaDatVatTu();
        }
        private void MaDatVatTu()
        {
            cbMaDH_Vattu.DataSource = knn.laydulieu("select Purchase_order from tblvattu_dauvao where Purchase_order is not null group by Purchase_order");
            cbMaDH_Vattu.DisplayMember = "Purchase_order";
            cbMaDH_Vattu.ValueMember = "Purchase_order";
        }
        private void txtCodePO_TextChanged(object sender, EventArgs e)//CHẠY MÃ ĐẶT HÀNG MUA VẬT TƯ THEO MÃ CODE
        {       
        }

        private void btnlaymadonhang_Click(object sender, EventArgs e)
        {
            if (txttrangthaiDH.Text != "")
                MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txtTenloaiVL.Text != "")
            {
                SqlConnection con = new SqlConnection();  //decimal GIAMUA = Convert.ToDecimal(txtGiamua.Text)
                var mConnect = Connect.mConnect;
                con.ConnectionString = mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set NgayDinhViDatVatTu =GETDATE(),Purchase_order =@Purchase_order , "
                    + "Ghichu_dathangmua=@Ghichu_dathangmua,SL_tinhgia=@SL_tinhgia,DK_TCmua=@DK_TCmua, "
                    + "Dongia=@Dongia,NCC=@NCC,NguoiGD=@NguoiGD,fax=@fax "
                    + "where CodeVatllieu like N'" + txtCode.Text + "'", con))
                    {
                        cmd.Parameters.Add("@Purchase_order", SqlDbType.NVarChar).Value = cbMaDH_Vattu.Text;
                        cmd.Parameters.Add("@Ghichu_dathangmua", SqlDbType.NVarChar).Value = txtghichudathang.Text;
                        cmd.Parameters.Add("@DK_TCmua", SqlDbType.NVarChar).Value = txtTCDieuKienMua.Text;
                        cmd.Parameters.Add("@Dongia", SqlDbType.Int).Value = txtDonGia.Text;
                        cmd.Parameters.Add("@NCC", SqlDbType.NVarChar).Value = txtNCC.Text;
                        cmd.Parameters.Add("@NguoiGD", SqlDbType.NVarChar).Value = txtnguoigiaodich.Text;
                        cmd.Parameters.Add("@fax", SqlDbType.NVarChar).Value = txtfax.Text;
                        cmd.Parameters.Add("@SL_tinhgia", SqlDbType.Float).Value = txtSL_tinhgia.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close(); LoadDanhMuc_DINHMUCVATTUALL();
                }
                else
                {
                    MessageBox.Show("Cần kiểm tra nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void themmadatvattu_tudong()//MÃ ĐẶT VẬT TƯ TỰ ĐỘNG
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu(" update tblvattu_dauvao set tblvattu_dauvao.CodePO = DatHangVatTuDauVao.Purchase_order "
                        //+ " tblvattu_dauvao.CodePO = DatHangVatTuDauVao.CodePO "
                        + " from tblvattu_dauvao, DatHangVatTuDauVao where tblvattu_dauvao.CodeVatllieu = DatHangVatTuDauVao.CodeVatllieu");
        }
        private void btnloadgridview2_Click(object sender, EventArgs e)
        {
        }
        private void MaPOMax()// LẤY SỐ LỚN NHẤT MÃ PO (ĐƠN ĐẶT HÀNG VẬT TƯ)
        {
           
        }

        private void dpNgaydatvattu_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtcodePOthemVT_TextChanged(object sender, EventArgs e)
        {
            
        }



        private void btnthemvattudachon_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu(" update tblvattu_dauvao set Ngaydat_vattu = GetDate(), Purchase_order = N'"+cbMaDH_Vattu.Text+"' "
                        + " where CodeVatllieu like N'" + txtMaPhieuDexuat.Text + "'");
        }

        private void viewdh_Click(object sender, EventArgs e)
        {

        }

        private void cbmapo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnthemsaudachon_Click(object sender, EventArgs e)
        {
           
        }

        private void btnbotmucchitiet_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu(" update tblvattu_dauvao set Ngaydat_vattu = GetDate(), Purchase_order = N'NULL' "
                        + " where MaDN_VATTU like N'" + txtMaPhieuDexuat.Text + "'");
            LoadDanhMuc_DINHMUCVATTUALL();
        }
        private void btnxoaTOA_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu(" update tblvattu_dauvao set Ngaydat_vattu = GetDate(), Purchase_order = N'NULL' "
                        + " where Purchase_order like N'" + txtMaPhieuDexuat.Text + "'");
            LoadDanhMuc_DINHMUCVATTUALL();
        }
        private void btnindenghivattu_Click(object sender, EventArgs e) //PRINT ĐỀ NGHỊ VẬT TƯ
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT DatHangVatTuDauVao_STT.* FROM DatHangVatTuDauVao_STT where MaDN_VATTU like '" + txtMaPhieuDexuat.Text + "'");
            XtraReportDeNghiVatTu rpDEXUATVATTU = new XtraReportDeNghiVatTu();
            rpDEXUATVATTU.DataSource = dt;
            rpDEXUATVATTU.DataMember = "Table";
            rpDEXUATVATTU.CreateDocument(false);
            rpDEXUATVATTU.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuDexuat.Text;
            PrintTool tool = new PrintTool(rpDEXUATVATTU.PrintingSystem);
            rpDEXUATVATTU.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnprintdondathang_Click(object sender, EventArgs e)//PRINT ĐƠN ĐẶT HÀNG 
        {
            DataTable dt = new DataTable();
            ketnoi kn1 = new ketnoi();
            dt = kn1.laybang("SELECT DatHangVatTuDauVao_STT.* FROM DatHangVatTuDauVao_STT where Purchase_order like '"+cbMaDH_Vattu.Text+"'");
            RpDatHangVatTu rpDatHangVatTu = new RpDatHangVatTu();
            rpDatHangVatTu.DataSource = dt;
            rpDatHangVatTu.DataMember = "Table";
            rpDatHangVatTu.ShowPreviewDialog();
            kn1.dongketnoi();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            cbMaDH_Vattu.Text = gridView1.GetFocusedRowCellDisplayText(MaDatHangVatTu_grid1);
            txtnguoigiaodich.Text =gridView1.GetFocusedRowCellDisplayText(NguoiGD_grid1);
            txtdonvivattu.Text = gridView1.GetFocusedRowCellDisplayText(Donvivt_grid1);
            txtSLcandat.Text= gridView1.GetFocusedRowCellDisplayText(SLVT_Can_grid1);
            txtsoluongmua.Text = gridView1.GetFocusedRowCellDisplayText(SLVT_mua_grid1);
            dpNgayVeVatTu.Text = gridView1.GetFocusedRowCellDisplayText(Ngayve_DK_grid1);
            txtMaPhieuDexuat.Text = gridView1.GetFocusedRowCellDisplayText(Madenghi_VT_grid1);       
            txtTenloaiVL.Text = gridView1.GetFocusedRowCellDisplayText(Tenvattu_grid1);
            txtDonGia.Text= gridView1.GetFocusedRowCellDisplayText(Dongiamua_grid1);
            txtNCC.Text = gridView1.GetFocusedRowCellDisplayText(Nhacungcap_grid1);
            txtfax.Text = gridView1.GetFocusedRowCellDisplayText(Dienthoai_grid1);           
            cbquyetdinhvattu.Text = gridView1.GetFocusedRowCellDisplayText(quyetdinh_grid1);
            txtnguoikiem.Text = gridView1.GetFocusedRowCellDisplayText(Nguoikiem_grid1);
            dpngaykiem.Text = gridView1.GetFocusedRowCellDisplayText(Ngaykiem_grid1);
            cbkiemkho.Text = gridView1.GetFocusedRowCellDisplayText(kiemkho_grid1);            
            txttrangthaiDH.Text = gridView1.GetFocusedRowCellDisplayText(Duyetsx_grid1);           
            txtCode.Text= gridView1.GetFocusedRowCellDisplayText(Code_grid1);
            txtTCDieuKienMua.Text= gridView1.GetFocusedRowCellDisplayText(Ghichu_DKMua_grid1);
            txtghichudathang.Text= gridView1.GetFocusedRowCellDisplayText(GhichuDathang_grid1);
            txtSL_tinhgia.Text = gridView1.GetFocusedRowCellDisplayText(SL_tinhgia_grid1);
            cbmadh.Text= gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
        }

        private void LoadDanhMuc_DINHMUCVATTUALL()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang("select NgayDK_ve,NVKD,Purchase_order,MaDN_VATTU,CodeVatllieu,madh,masp,Tenquicachsp,Soluongsanpham,Donvi_sanpham,Ma_CT, "
              + " Ten_CT, QC_CT, Ten_vattu, SL_vattucan, SL_vattutonkho, SL_vattumua, Dongia, Donvi_vattu,SL_tinhgia, NCC, NguoiGD, fax, nhanviendathang, Kiemkho, "
              + " nguoikiemkho, ngaykiemkho, Duyetsanxuat, VAT, Kiemkho, Ghichu_dathangmua, Ghichu_denghimua, DK_TCmua, quyetdinh "
              + " FROM tblvattu_dauvao where  convert(Date,Ngaylap_DM,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
        }
        private void btnShow_VT_Click(object sender, EventArgs e)
        {
            LoadDanhMuc_DINHMUCVATTUALL();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void printPSX_VT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + cbmadh.Text + "'");
            XRPhieuSX_DaDuyet xrPSX_VatTu = new XRPhieuSX_DaDuyet();
            xrPSX_VatTu.DataSource = dt;
            xrPSX_VatTu.DataMember = "Table";
            xrPSX_VatTu.CreateDocument(false);
            xrPSX_VatTu.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbmadh.Text;
            PrintTool tool = new PrintTool(xrPSX_VatTu.PrintingSystem);
            tool.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void txtSL_tinhgia_TextChanged(object sender, EventArgs e)
        {
            if (txtSL_tinhgia.Text == "") { txtSL_tinhgia.Text = "0";} txtSL_tinhgia.Text = string.Format("{0:0,0}", decimal.Parse(txtSL_tinhgia.Text));
        }
    }
}
