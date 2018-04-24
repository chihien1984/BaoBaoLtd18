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
using quanlysanxuat.Model;
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    
    public partial class UcKEHOACH_VATTU : DevExpress.XtraEditors.XtraForm
    {
   
        string Gol = "";
     
        public UcKEHOACH_VATTU()
        {
            InitializeComponent();
        }

        private void LoadDanhMuc_DINHMUCVATTU()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * "
              +" FROM tblvattu_dauvao  where  "
              +" madh like N'" + cbMaDH.Text + "'");
            kn.dongketnoi();
        }
        private void LoadDanhMuc_DINHMUCVATTUALL()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * "
              + " FROM tblvattu_dauvao");
            kn.dongketnoi();
        }
        private void refreshtextbox()// REFRESH LẠI CÁC THÔNG TIN
        {
           
        }
        

        private void Refresh_Click(object sender, EventArgs e)
        {
            
        }

        private void UcKEHOACH_VATTU_Load(object sender, EventArgs e) // FROM LOAD KẾ HOẠCH ĐẶT VẬT TƯ - NGUYÊN LIỆU DÙNG SẢN XUẤT THEO ĐƠN HÀNG
        {
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.ToString();
            txtUser.Text = Login.Username;
            LOADAD_MAGH();
            LoadMaDH();
            ListDMNCC();
            LoadDanhMuc_DINHMUCVATTUALL();
            DocDSKeHoachVatTu();
        }

        private void Autocomple_NCC()
        {
        }
        private void chaymadhtheocode()//Người giao dịch
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select NguoiGD from tblvattu_dauvao where NCC like N'"+cbNCC.Text+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtnguoigiaodich.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void LoadMa_VT()
        {
            ketnoi Connect = new ketnoi();
            cbMaPhieudexuat.DataSource = Connect.laybang(" select Top 1 MaDN_VATTU from tblvattu_dauvao where MaDN_VATTU is not null and MaDN_VATTU <>'' order by CodeVatllieu DESC");
            cbMaPhieudexuat.ValueMember = "MaDN_VATTU";
            cbMaPhieudexuat.DisplayMember = "MaDN_VATTU";
        }
        private void LOADAD_MAGH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select Top 1 'PDN '+REPLACE(convert(nvarchar,GetDate(),12),'/','')+'-'+ convert(nvarchar,(DATEPART(HH,GetDate())))+'.'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                cbMaPhieudexuat.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void LoadMaDH()
        {
            ketnoi Connect = new ketnoi();
            cbMaDH.DataSource = Connect.laybang("Select Madh from tblDHCT order by madh DESC");
            cbMaDH.ValueMember = "Madh";
            cbMaDH.DisplayMember = "Madh";
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LOADAD_MAGH();
        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)// SỰ KIỆN CLICK CHUỘT VÀO GRIDVIEW 1 BINDING DỮ LIỆU LÊN TEXTBOX
        {
            Gol = gridView1.GetFocusedDisplayText();    
        }

        private void txtidsp_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtidentitysanpham_TextChanged(object sender, EventArgs e)//BẮT SỰ IỆN THAY ĐỔI SỐ TEXT IDEN SẢN PHẨM VIEW VẬT LIỆU CHI TIẾT
        { ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT Donviquydoi,CodeVatllieu,Iden,madh,Codedetail "
                + ", Tenquicachsp, Soluongsanpham, Donvi_sanpham, Ten_vattu "
                + ", SL_vattucan, KL_vattucan, SL_vattutonkho, KL_vattutonkho, SL_vattumua, KL_vattumua "
                + ", Donvi_vattu, NCC, NguoiGD, Dongia, Donviquydoi, Ngaydat_vattu, NgayDK_ve, Ngayve_TT, "
                + "SL_vattuve, KL_vattuve, SL_tinhgia, Dvt_gia, Ghichu_dathangmua, Ghichu_denghimua, "
                + "DK_TCmua, VAT, quyetdinh, nhanviendathang, nguoikiemkho, ngaykiemkho, nguoinhap, ngaynhap,fax,NVKD,Kiemkho "
                + " FROM dbo.tblvattu_dauvao where Iden like '" + txtIden.Text + "'");
            kn.dongketnoi();
        }
        private void update_Click(object sender, EventArgs e)// SỮA CHỮA ĐẶT VẬT TƯ MUA HÀNG TỪ FROM SỬA CHỬA 
        {
           
        }
        private void gridControl1_Click_2(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            cbMaDH.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
            cbMaPhieudexuat.Text = gridView1.GetFocusedRowCellDisplayText(Madenghi_VT_grid1);
            txtmasp.Text= gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txttensp.Text= gridView1.GetFocusedRowCellDisplayText(Tensp_grid1);
            txtMact.Text= gridView1.GetFocusedRowCellDisplayText(Tenct_grid1);
            txtTenVatlieu.Text= gridView1.GetFocusedRowCellDisplayText(Tenvattu_grid1);
            txtSL_VT_DM.Text= gridView1.GetFocusedRowCellDisplayText(SLVT_Can_grid1);
            txtSL_Ton.Text= gridView1.GetFocusedRowCellDisplayText(SLVT_Ton_grid1);
            txtSLVT_MUA.Text= gridView1.GetFocusedRowCellDisplayText(SLVT_mua_grid1);
            txtGiamua.Text= gridView1.GetFocusedRowCellDisplayText(Dongiamua_grid1);
            txtQC_CT.Text= gridView1.GetFocusedRowCellDisplayText(Quicachct_grid1);
            cbVAT.Text= gridView1.GetFocusedRowCellDisplayText(VAT_grid1);
            cbNCC.Text= gridView1.GetFocusedRowCellDisplayText(Nhacungcap_grid1);
            txtfax.Text= gridView1.GetFocusedRowCellDisplayText(Dienthoai_grid1);
            txtDonvi_sp.Text= gridView1.GetFocusedRowCellDisplayText(Donvisp_grid1);
            cbdonvi_vatlieu.Text= gridView1.GetFocusedRowCellDisplayText(Donvivt_grid1);
            txtghichudenghi.Text= gridView1.GetFocusedRowCellDisplayText(Ghichudenghi_grid1);
            txtghichudathang.Text= gridView1.GetFocusedRowCellDisplayText(GhichuDathang_grid1);
            txtTCDK_MUA.Text= gridView1.GetFocusedRowCellDisplayText(Ghichu_DKMua_grid1);
            cbquyetdinhvattu.Text= gridView1.GetFocusedRowCellDisplayText(quyetdinh_grid1);
            txtnguoikiem.Text= gridView1.GetFocusedRowCellDisplayText(Nguoikiem_grid1);
            dpngaykiem.Text= gridView1.GetFocusedRowCellDisplayText(Ngaykiem_grid1);
            cbkiemkho.Text= gridView1.GetFocusedRowCellDisplayText(kiemkho_grid1);
            txtIden.Text= gridView1.GetFocusedRowCellDisplayText(Code_grid1);
            txttrangthaiDH.Text= gridView1.GetFocusedRowCellDisplayText(Duyetsx_grid1);
            dpNgayVTVe_DK.Text= gridView1.GetFocusedRowCellDisplayText(Ngayve_DK_grid1);
            txtkinhdoanh.Text= gridView1.GetFocusedRowCellDisplayText(Kinhdoanh_grid1);


        }
        private void btnxoa_Click(object sender, EventArgs e)// XÓA ĐẶT VẬT TƯ MUA HÀNG
        {
           

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void viewdh_Click(object sender, EventArgs e)// SHOW DỮ LIỆU CHI TIẾT ĐƠN ĐẶT HÀNG
        {
         
        }

        private void viewdetail_Click(object sender, EventArgs e)// SHOW DỮ LIỆU ĐẶT VẬT TƯ NGUYÊN LIỆU DÙNG SẢN XUẤT
        {
           
        }

        private void cbmadh_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }
        private void cbbophan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// ĐỊNH DẠNG SỐ LƯỢNG 
        /// <param name="e"></param>

        private void txtSLcandat_TextChanged(object sender, EventArgs e) //ĐỊNH DẠNG DẠNG SỐ SỐ LƯỢNG VẬT TƯ CẦN ĐẶT CHO SẢN PHẨM 
        {
            if (txtSLVT_MUA.Text=="")
            {
                txtSLVT_MUA.Text = "0";
            }
            txtSLVT_MUA.Text = string.Format("{0:0,0}", decimal.Parse(txtSLVT_MUA.Text));
            txtSLVT_MUA.SelectionStart = txtSLVT_MUA.Text.Length;
        }
        private void txtkhoiluongcandat_TextChanged(object sender, EventArgs e)//ĐỊNH DẠNG KHỐI LƯỢNG VẬT TƯ CẦN ĐẶT
        {
            //if (txtkhoiluongcandat.Text == "")
            //{
            //    txtkhoiluongcandat.Text = "0";
            //}
            //txtkhoiluongcandat.Text = string.Format("{0:0,0.0}", decimal.Parse(txtkhoiluongcandat.Text));
            //txtkhoiluongcandat.SelectionStart = txtkhoiluongcandat.Text.Length;
        }

        private void txtsoluongmua_TextChanged(object sender, EventArgs e)//ĐỊNH DẠNG SỐ LƯỢNG MUA
        {
          
        }

        private void txtkhoiluongmua_TextChanged(object sender, EventArgs e)//ĐỊNH DẠNG KHỐI LƯỢNG MUA
        {
          
        }

        private void txtSLtonkho_TextChanged(object sender, EventArgs e)//ĐỊNH DẠNG SỐ LƯỢNG TỒN KHO
        {
           
        }

        private void txtsoluongthucnhap_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txttronluongthucnhap_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void txtdongia_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtKLtonkho_TextChanged(object sender, EventArgs e)//ĐỊNH DẠNG KHỐI LƯỢNG TỒN KHO
        {
          
        }
        /// <summary>
        /// SỰ KIỆN KeyPress ĐỊNH DẠNG CHO NHẬP SỐ
        private void txtSLtonkho_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO SỐ LƯỢNG TỒN KHO
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtKLtonkho_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO KHỐI LƯỢNG TỒN KHO
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtSLcandat_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO SỐ LƯỢNG CẦN ĐẶT
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtkhoiluongcandat_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO KHỐI LƯỢNG TỒN KHO
        {
            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '.'))
                e.Handled = true;
        }

        private void txtsoluongmua_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO SỐ LƯỢNG MUA
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtkhoiluongmua_KeyPress(object sender, KeyPressEventArgs e)// ĐỊNH DẠNG CHỈ CHO NHẬP SỐ VÀO KHỐI LƯỢNG MUA
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtdongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtsoluongsp_TextChanged(object sender, EventArgs e)//IN 
        {

        }

        private void btnindenghivattu_Click(object sender, EventArgs e)// IN ĐỀ NGHỊ ĐẶT VẬT TƯ
        {
            //DataTable dt = new DataTable();
            //ketnoi kn = new ketnoi();
            //dt = kn.laybang("SELECT INDONHANG.*, tblDONHANG.* "
            //            + "FROM  INDONHANG INNER JOIN "
            //            + "tblDONHANG ON INDONHANG.Code = tblDONHANG.Code where INDONHANG.madh like N'" + txtcodevatlieu.Text + "'");
            //RpDEXUATMUAVATTU rpDEXUATVATTU = new RpDEXUATMUAVATTU();
            //rpDEXUATVATTU.DataSource = dt;
            //rpDEXUATVATTU.DataMember = "Table";
            //rpDEXUATVATTU.ShowPreviewDialog();
        }

        private void btnbaocaodatvattu_Click(object sender, EventArgs e)//IN BÁO CÁO CHI TIẾT ĐẶT HÀNG VẬT TƯ
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void btnprintdondathang_Click(object sender, EventArgs e)//IN ĐƠN ĐẶT HÀNG VẬT TƯ
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT INDONHANG.*, tblDONHANG.* "
                        + "FROM  INDONHANG INNER JOIN "
                        + "tblDONHANG ON INDONHANG.Code = tblDONHANG.Code where INDONHANG.madh like N'" + cbMaPhieudexuat.Text + "'");
            RpDatHangVatTu rpDatHangVatTu = new RpDatHangVatTu();
            rpDatHangVatTu.DataSource = dt;
            rpDatHangVatTu.DataMember = "Table";
            rpDatHangVatTu.ShowPreviewDialog();
        }

        private void btnDatVatTu_Click(object sender, EventArgs e)
        {
            frmPrintDatMuaVatTu frPrDatMuaVatTu = new frmPrintDatMuaVatTu();
            frPrDatMuaVatTu.Show();
        }

        private void btnChiTiet_DatVatTu_Click(object sender, EventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }


        private void LoadrefreshDH()
        {
            ketnoi Connect = new ketnoi();
            cbMaPhieudexuat.DataSource = Connect.laybang("Select Madh from tblDHCT order by madh DESC");
            cbMaPhieudexuat.ValueMember = "Madh";
            cbMaPhieudexuat.DisplayMember = "Madh";
        }

        SANXUATDbContext db = new SANXUATDbContext();
       

        private void btnShow_VT_Click(object sender, EventArgs e)
        {
            LoadDanhMuc_DINHMUCVATTUALL();
        }

        private void txtGiamua_TextChanged(object sender, EventArgs e)
        {
            if (txtGiamua.Text == "")
            {
                txtGiamua.Text = "0";
            }
            txtGiamua.Text = string.Format("{0:0,0}", decimal.Parse(txtGiamua.Text));
            txtGiamua.SelectionStart = txtGiamua.Text.Length;
        }

        private void txtQC_CT_TextChanged(object sender, EventArgs e)
        {
            if (txtQC_CT.Text == "") txtQC_CT.Text = "0";
        }

        private void txtSLVT_MUA_TextChanged(object sender, EventArgs e)
        {
            if (txtSLVT_MUA.Text == "") txtSLVT_MUA.Text = "0";
        }

        private void txtSL_VT_DM_TextChanged(object sender, EventArgs e)
        {
            if (txtSL_VT_DM.Text == "") txtSL_VT_DM.Text = "0";
        }

        private void txtSL_Ton_TextChanged(object sender, EventArgs e)
        {
            if (txtSL_Ton.Text == "") txtSL_Ton.Text = "0";
        }

        private void cbMaDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDanhMuc_DINHMUCVATTU();
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadDanhMuc_DINHMUCVATTU();
        }

       

        private void txtNCC_TextChanged(object sender, EventArgs e)
        {
            chaymadhtheocode();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
           
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            frmThemNCC fNCC = new frmThemNCC();
            fNCC.ShowDialog();
            ListDMNCC();
        }
        private void ListDMNCC() //Combobox lấy danh mục nhà cung cấp
        {
            ketnoi Connect = new ketnoi();
            cbNCC.Properties.DataSource = Connect.laybang("select * from tblDM_NCC_VATTU order by Ngaycapnhat_NCC desc ");
            cbNCC.Properties.DisplayMember = "Ten_NCC";
            cbNCC.Properties.ValueMember = "Ten_NCC";
            cbNCC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cbNCC.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            cbNCC.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void LayMaNCC(object sender, EventArgs e) //Lấy mã NCC theo tên nhà cung cấp
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Ma_NCC from tblDM_NCC_VATTU where Ten_NCC like N'" + cbNCC.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaNCC.Text = Convert.ToString(reader[0]);
                //txtMaNCC.Text = Convert.ToString(reader[0]);
                //txtMaNCC.Text = Convert.ToString(reader[0]);
                //txtMaNCC.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }
        private void add_Click_1(object sender, EventArgs e)
        {
            //int d = db.tblvattu_dauvao.Count(p => p.MaDN_VATTU == cbMaPhieudexuat.Text);
            //if (d > 0)
            //{
            //    MessageBox.Show("Mã đề nghị bị trùng.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (string.IsNullOrEmpty(txttrangthaiDH.Text) && cbMaDH.Text == ""
                && txtTenVatlieu.Text == ""
                && txttensp.Text == "") return;
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    decimal GIAMUA = Convert.ToDecimal(txtGiamua.Text);
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set MaNCC=@MaNCC,NgayDK_ve=@NgayDK_ve,Donviquydoi=@Donviquydoi,MaDN_VATTU=@MaDN_VATTU,"
                       + " NCC=@NCC,NguoiGD=@NguoiGD,fax=@fax,Ghichu_denghimua=@Ghichu_denghimua,"
                       + " Ghichu_dathangmua=@Ghichu_dathangmua,DK_TCmua=@DK_TCmua,nhanviendathang=@nhanviendathang,Ngaydat_vattu=Getdate(), "
                       + " SL_vattumua=@SL_vattumua,Dongia=@Dongia,VAT=@VAT,quyetdinh=@quyetdinh "
                       + " where CodeVatllieu like '" + txtIden.Text + "' and Duyetsanxuat is null", con))
                        {
                            cmd.Parameters.Add("@Donviquydoi", SqlDbType.NVarChar).Value = cbDonviquydoi.Text;
                            cmd.Parameters.Add("@MaDN_VATTU", SqlDbType.NVarChar).Value = cbMaPhieudexuat.Text;
                            cmd.Parameters.Add("@NCC", SqlDbType.NVarChar).Value = cbNCC.Text;
                            cmd.Parameters.Add("@NguoiGD", SqlDbType.NVarChar).Value = txtnguoigiaodich.Text;
                            cmd.Parameters.Add("@fax", SqlDbType.NVarChar).Value = txtfax.Text;
                            cmd.Parameters.Add("@Ghichu_denghimua", SqlDbType.NVarChar).Value = txtghichudenghi.Text;
                            cmd.Parameters.Add("@Ghichu_dathangmua", SqlDbType.NVarChar).Value = txtghichudathang.Text;
                            cmd.Parameters.Add("@DK_TCmua", SqlDbType.NVarChar).Value = txtTCDK_MUA.Text;
                            cmd.Parameters.Add("@nhanviendathang", SqlDbType.NVarChar).Value = txtUser.Text;
                            cmd.Parameters.Add("@SL_vattumua", SqlDbType.Float).Value = txtSLVT_MUA.Text;
                            cmd.Parameters.Add("@Dongia", SqlDbType.Float).Value = GIAMUA;
                            cmd.Parameters.Add("@VAT", SqlDbType.NVarChar).Value = cbVAT.Text;
                            cmd.Parameters.Add("@quyetdinh", SqlDbType.NVarChar).Value = cbquyetdinhvattu.Text;
                            cmd.Parameters.Add("@NgayDK_ve", SqlDbType.Date).Value = dpNgayVTVe_DK.Text;
                            cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        LoadDanhMuc_DINHMUCVATTU();
                    }
                }
                catch (Exception ex)
                {
                    object cauBaoLoi = "Lưu không thành công.<br> Lý do:" + ex.Message;
                    MessageBox.Show("" + cauBaoLoi);
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {   
            if (string.IsNullOrEmpty(txttrangthaiDH.Text) && cbMaDH.Text == ""
               && txtTenVatlieu.Text == ""
               && txttensp.Text == "") 
            { MessageBox.Show("Mã đơn hàng, trạng thái ĐH, Tên vật tư, tên sản phẩm chưa có"); return; }
            
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    decimal GIAMUA = Convert.ToDecimal(txtGiamua.Text);
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set MaNCC=@MaNCC,NgayDK_ve=@NgayDK_ve,Donviquydoi=@Donviquydoi,"
                       + " NCC=@NCC,NguoiGD=@NguoiGD,fax=@fax,Ghichu_denghimua=@Ghichu_denghimua,"
                       + " Ghichu_dathangmua=@Ghichu_dathangmua,DK_TCmua=@DK_TCmua,nhanviendathang=@nhanviendathang,Ngaydat_vattu=Getdate(), "
                       + " SL_vattumua=@SL_vattumua,Dongia=@Dongia,VAT=@VAT,quyetdinh=@quyetdinh "
                       + " where CodeVatllieu like '" + txtIden.Text + "' and Duyetsanxuat is null", con))
                        {
                            cmd.Parameters.Add("@Donviquydoi", SqlDbType.NVarChar).Value = cbDonviquydoi.Text;
                            cmd.Parameters.Add("@NCC", SqlDbType.NVarChar).Value = cbNCC.Text;
                            cmd.Parameters.Add("@NguoiGD", SqlDbType.NVarChar).Value = txtnguoigiaodich.Text;
                            cmd.Parameters.Add("@fax", SqlDbType.NVarChar).Value = txtfax.Text;
                            cmd.Parameters.Add("@Ghichu_denghimua", SqlDbType.NVarChar).Value = txtghichudenghi.Text;
                            cmd.Parameters.Add("@Ghichu_dathangmua", SqlDbType.NVarChar).Value = txtghichudathang.Text;
                            cmd.Parameters.Add("@DK_TCmua", SqlDbType.NVarChar).Value = txtTCDK_MUA.Text;
                            cmd.Parameters.Add("@nhanviendathang", SqlDbType.NVarChar).Value = txtUser.Text;
                            cmd.Parameters.Add("@SL_vattumua", SqlDbType.Float).Value = txtSLVT_MUA.Text;
                            cmd.Parameters.Add("@Dongia", SqlDbType.Float).Value = GIAMUA;
                            cmd.Parameters.Add("@VAT", SqlDbType.NVarChar).Value = cbVAT.Text;
                            cmd.Parameters.Add("@quyetdinh", SqlDbType.NVarChar).Value = cbquyetdinhvattu.Text;
                            cmd.Parameters.Add("@NgayDK_ve", SqlDbType.Date).Value = dpNgayVTVe_DK.Text;
                            cmd.Parameters.Add("@MaNCC", SqlDbType.NVarChar).Value = txtMaNCC.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        LoadDanhMuc_DINHMUCVATTU();
                    }
                }
                catch (Exception ex)
                {
                    object cauBaoLoi = "Lưu không thành công.<br> Lý do:" + ex.Message;
                    MessageBox.Show("" + cauBaoLoi);
                }
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
                try
                {       ketnoi kn = new ketnoi();
                        gridControl1.DataSource = kn.xulydulieu("update tblvattu_dauvao set MaDN_VATTU='' where CodeVatllieu like '" + txtIden.Text + "'");
                        kn.dongketnoi();
                        LoadDanhMuc_DINHMUCVATTU();
                }
                catch (Exception ex)
                {
                    object cauBaoLoi = "không thành công.<br> Lý do:" + ex.Message;
                    MessageBox.Show("" + cauBaoLoi);
                }
        }
        private void btnPrintDexuat_VT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT DatHangVatTuDauVao_STT.* FROM DatHangVatTuDauVao_STT where MaDN_VATTU like '" + cbMaPhieudexuat.Text + "'");
            XtraReportDeNghiVatTu rpDEXUATVATTU = new XtraReportDeNghiVatTu();
            rpDEXUATVATTU.DataSource = dt;
            rpDEXUATVATTU.DataMember = "Table";
            rpDEXUATVATTU.CreateDocument(false);
            rpDEXUATVATTU.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaPhieudexuat.Text;
            PrintTool tool = new PrintTool(rpDEXUATVATTU.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void btnDinhMucVatTu_Click(object sender, EventArgs e)
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

        private void btnExpVTu_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }
        private void DocDSKeHoachVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(
             @"select * 
               FROM tblvattu_dauvao 
               where Ngaylap_DM between '"+dpTu.Value.ToString("MM/dd/yyyy")+ "' and '" + dpDen.Value.ToString("MM/dd/yyyy") + "'");
            kn.dongketnoi();
        }
        private void btnDocSearchDSVatTu_Click(object sender, EventArgs e)
        {
            DocDSKeHoachVatTu();
        }
    }
}
