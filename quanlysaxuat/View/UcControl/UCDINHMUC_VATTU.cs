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
using quanlysanxuat.View;

namespace quanlysanxuat
{

    public partial class UCDINHMUC_VATTU : UserControl
    {
       
        FtpClient ftpClient; 
        public UCDINHMUC_VATTU()
        {
            InitializeComponent();
            ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }
        string Gol = "";
        //SqlCommand cmd;
        private void LoadDM_SP_ALL()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("SELECT Iden,Tenquicach, Codedetail, madh, Khachhang, MaPo, MaSP,dvt, "
         + "  Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, "
         +"   Code, usd, tygia, ghichu, nguoithaydoi, thoigianthaydoi, pheduyet, Trangthai, "
         +"  (case when pheduyet = N'Đã phê duyệt' then 'TRUE' else 'FALSE' end) Muc,   "
         +"  Tenkhachhang,Masp_KH, MaKH FROM dbo.tblDHCT");
            // gridControl2.DataSource = Connect.laybang("SELECT Iden,Tenquicach,DHCT.Codedetail,DHCT.madh,DHCT.Khachhang,DHCT.MaPo,DHCT.MaSP,dvt,SPCT.Mact,SPCT.Ten_ct,SPCT.Soluong_CT, "
            //+ "Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, "
            //+ "DHCT.Code, usd, tygia, ghichu, DHCT.nguoithaydoi, DHCT.thoigianthaydoi, pheduyet, Trangthai, "
            //+ "(case when pheduyet = N'Đã phê duyệt' then 'TRUE' else 'FALSE' end) Muc,  "
            //+ "Tenkhachhang,Masp_KH,DHCT.MaKH,DH.Diengiai FROM dbo.tblDHCT DHCT "
            //+ "left outer join tblSANPHAM_CT SPCT on DHCT.MaSP = SPCT.Masp "
            //+ "left outer join tblDONHANG DH on DHCT.madh = DH.madh where DHCT.madh like N'" + cbMaDH.Text + "'");
            Connect.dongketnoi();
        }
        private void LoadDM_VATTU_ALL()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang("SELECT VT.Mavattu,VT.Iden,VT.CodeVatllieu,VT.NVKD,VT.madh,Masp, "
        + "  Tenquicachsp,Ma_CT,Ten_CT,Soluong_CT,Ten_vattu,SL_vattucan,Donvi_vattu,Ghichu_CT,Ghichu_DH,Ngaylap_DM,Nguoilap_DM,Soluongsanpham, VT.Duyetsanxuat, "
        + "  VT.Nguoiduyet, VT.Ngayduyet FROM tblvattu_dauvao VT  left join tblDONHANG DH  on  VT.madh = DH.madh where  "
        + " Ngaylap_DM between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by Ngaylap_DM Desc");
        }
        private void LoadDANHMUC_SANPHAM()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("SELECT DHCT.Iden,Tenquicach,DHCT.Codedetail, "
           +"DHCT.madh,DHCT.Khachhang,DHCT.MaPo,DHCT.MaSP,dvt,SPCT.Mact,SPCT.Ten_ct,SPCT.Soluong_CT, "
           +"Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, "
           +"DHCT.Code, usd, tygia, ghichu, DHCT.nguoithaydoi, DHCT.thoigianthaydoi, pheduyet, Trangthai, "
           +"(case when pheduyet = N'Đã phê duyệt' then 'TRUE' else 'FALSE' end) Muc,  "
           +"Tenkhachhang,Masp_KH,DHCT.MaKH,DH.Diengiai FROM dbo.tblDHCT DHCT "
           +"left outer join tblSANPHAM_CT SPCT on DHCT.MaSP = SPCT.Masp "
           +"left outer join tblDONHANG DH on DHCT.madh = DH.madh where DHCT.madh like N'" + lookupPSX.Text + "'");
            Connect.dongketnoi();
        }
        private void LoadMaVatTu()
        {
            
        }
        private void LoadDanhMuc_DINHMUCVATTU()
        {
            ketnoi Connenct = new ketnoi();
            gridControl1.DataSource = Connenct.laybang("SELECT Mavattu,CodeVatllieu,VT.Iden,VT.NVKD,VT.madh,Masp,Tenquicachsp,Ma_CT,Ten_CT,Soluong_CT,Ten_vattu, "
              + " SL_vattucan,Donvi_vattu,Ghichu_CT,Ghichu_DH,Ngaylap_DM,Nguoilap_DM,Soluongsanpham,DH.Duyetsanxuat "
              +" FROM tblvattu_dauvao VT  left join tblDONHANG DH  on  VT.madh = DH.madh where  "
              + " VT.madh like N'" + lookupPSX.Text+"'");
        }

        private void UCDINHMUC_VATTU_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            LoadMaDH();
            txtUser.Text = Login.Username;
            Autocomplete_Vattu();
            gridlookupPSX();
        }

        private void gridlookupPSX()//gridlookup vật tư sử dụng
        {
            ketnoi Connect = new ketnoi();
            gridLookMavattu.Properties.DataSource = Connect.laybang("select * from tblDM_VATTU order by Ngaylap desc");
            gridLookMavattu.Properties.DisplayMember = "Ma_vl";
            gridLookMavattu.Properties.ValueMember = "Ma_vl";
            gridLookMavattu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookMavattu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookMavattu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void gridLookMavattu_EditValueChanged(object sender, EventArgs e)//lấy tên vật liệu theo gridlookup mã vật liệu
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtTenVatlieu.Text = gridView3.GetFocusedRowCellDisplayText(Tenvatlieu_grid3);
        }
        private void LoadMaDH() {
            ketnoi Connect = new ketnoi();
            lookupPSX.Properties.DataSource = Connect.laybang("select madh,Khachhang,nguoithaydoi from tblDHCT order by thoigianthaydoi DESC");
            lookupPSX.Properties.DisplayMember = "madh";
            lookupPSX.Properties.ValueMember = "madh";
            lookupPSX.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            lookupPSX.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            lookupPSX.Properties.ImmediatePopup = true;
        }
        private void Autocomplete_Vattu()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Ten_vattu from tblvattu_dauvao", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtTenVatlieu.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtNVKD.Text = gridView1.GetFocusedRowCellDisplayText(nvkd_grid1);
            gridLookMavattu.Text = gridView1.GetFocusedRowCellDisplayText(Mavatlieu_grid1);
            txtidPSX.Text = gridView1.GetFocusedRowCellDisplayText(Idpsx_grid1);
            txtmasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txttensp.Text = gridView1.GetFocusedRowCellDisplayText(Tensp_grid1);
            txtMaCT.Text = gridView1.GetFocusedRowCellDisplayText(Machitiet_grid1);
            txtTenCT.Text = gridView1.GetFocusedRowCellDisplayText(Tenchitiet_grid1);
            txtSL_CT.Text = gridView1.GetFocusedRowCellDisplayText(SL_CT_grid1);
            txtQC_CT.Text = gridView1.GetFocusedRowCellDisplayText(QC_chitiet_grid1);
            txtSL_DH.Text = gridView1.GetFocusedRowCellDisplayText(SL_DH_grid1);
            txtTenVatlieu.Text = gridView1.GetFocusedRowCellDisplayText(Ten_vt_grid1);
            txtSL_VT.Text = gridView1.GetFocusedRowCellDisplayText(SoluongDMVT_grid1);
            txtDonvi_sp.Text = gridView1.GetFocusedRowCellDisplayText(Donvi_vt_grid1);
            txtId.Text = gridView1.GetFocusedRowCellDisplayText(Code_grid1);
            txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
        }
        private void cbMaDH_EditValueChanged(object sender, EventArgs e)
        {
            LoadDANHMUC_SANPHAM();
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMadh.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(madh_gl);
        }
        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtMadh.Text = gridView2.GetFocusedRowCellDisplayText(Madh_grid1);
            txtDonvi_sp.Text= gridView2.GetFocusedRowCellDisplayText(donvitinh_grid2);
            txtNVKD.Text= gridView2.GetFocusedRowCellDisplayText(nvkd_grid2);
            dpNgayLap.Text= gridView2.GetFocusedRowCellDisplayText(thoigiancapnhat_grid2);
            txtmasp.Text = gridView2.GetFocusedRowCellDisplayText(masp_grid2);
            txttensp.Text = gridView2.GetFocusedRowCellDisplayText(tenquicach_grid2);
            txtSL_DH.Text = gridView2.GetFocusedRowCellDisplayText(Soluong_DH_grid2);
            txtMaCT.Text = gridView2.GetFocusedRowCellDisplayText(mact_grid2);
            txtTenCT.Text= gridView2.GetFocusedRowCellDisplayText(TenCT_grid2);
            txtSL_CT.Text = gridView2.GetFocusedRowCellDisplayText(Soluong_CT_grid2);
            txtNgoaiQuan.Text= gridView2.GetFocusedRowCellDisplayText(ngoaiquang_grid2);
            txtGhiChuDH.Text= gridView2.GetFocusedRowCellDisplayText(diengiaidonhang_grid2);
            txtGhichu_CT.Text= gridView2.GetFocusedRowCellDisplayText(ghichuCT_grid2);
            txtidPSX.Text = gridView2.GetFocusedRowCellDisplayText(Iden_grid2);  
        }
       
        private void LookupMaSP()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT CodeVatllieu,VT.madh,Masp,Tenquicachsp,Ma_CT,Ten_CT,Soluong_CT,Ten_vattu,SL_vattucan,Donvi_vattu,Ghichu_CT,Ghichu_DH,Ngaylap_DM,Nguoilap_DM,Soluongsanpham, VT.Duyetsanxuat, "
             + "VT.Nguoiduyet, VT.Ngayduyet FROM tblvattu_dauvao VT  left join tblDONHANG DH  on  VT.madh = DH.madh "
             + "FROM tblvattu_dauvao where  "
             + "Masp like N'" + txtmasp.Text + "'");
            kn.dongketnoi();
        }
        private void btnfresh_Click(object sender, EventArgs e)
        {LoadDANHMUC_SANPHAM();LoadDanhMuc_DINHMUCVATTU(); }
        private void btnDonHang_Click(object sender, EventArgs e)
        {LoadDM_SP_ALL();}

        private void btnDinhMuc_VatLieu_Click(object sender, EventArgs e)
        {LoadDM_VATTU_ALL();}

        private void cbMaDH_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (gridLookMavattu.Text != "")
            {
                SqlConnection con = new SqlConnection();
                decimal soluongDH = Convert.ToDecimal(txtSL_DH.Text);             
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblvattu_dauvao (Mavattu,Iden,NVKD,madh,Masp,Tenquicachsp,Soluongsanpham,Donvi_sanpham, "
                       + " Ma_CT,Ten_CT,Soluong_CT,Ten_vattu, SL_vattucan, Donvi_vattu, Ghichu_CT, Ghichu_DH,Ngaylap_DM,Nguoilap_DM,QC_CT) values "
                       + " (@Mavattu,@Iden,@NVKD,@madh, @Masp, @Tenquicachsp, @Soluongsanpham, @Donvi_sanpham, "
                       + " @Ma_CT, @Ten_CT,@Soluong_CT, @Ten_vattu, @SL_vattucan, @Donvi_vattu, @Ghichu_CT, @Ghichu_DH,GetDate(),@Nguoilap_DM,@QC_CT) ", con))
                    {
                        cmd.Parameters.Add("@Mavattu", SqlDbType.NVarChar).Value = gridLookMavattu.Text;
                        cmd.Parameters.Add("@Iden", SqlDbType.NVarChar).Value = txtidPSX.Text;
                        cmd.Parameters.Add("@NVKD", SqlDbType.NVarChar).Value = txtNVKD.Text;
                        cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = txtMadh.Text;
                        cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                        cmd.Parameters.Add("@Tenquicachsp", SqlDbType.NVarChar).Value = txttensp.Text;
                        cmd.Parameters.Add("@Soluongsanpham", SqlDbType.Int).Value = soluongDH;
                        cmd.Parameters.Add("@Donvi_sanpham", SqlDbType.NVarChar).Value = txtDonvi_sp.Text;
                        cmd.Parameters.Add("@Ma_CT", SqlDbType.NVarChar).Value = txtMaCT.Text;
                        cmd.Parameters.Add("@Ten_CT", SqlDbType.NVarChar).Value = txtTenCT.Text;
                        cmd.Parameters.Add("@Soluong_CT", SqlDbType.Int).Value = txtSL_CT.Text;
                        cmd.Parameters.Add("@Ten_vattu", SqlDbType.NVarChar).Value = txtTenVatlieu.Text;
                        cmd.Parameters.Add("@SL_vattucan", SqlDbType.Int).Value = txtSL_VT.Text;
                        cmd.Parameters.Add("@Donvi_vattu", SqlDbType.NVarChar).Value = cbdonvi_vatlieu.Text;
                        cmd.Parameters.Add("@Ghichu_CT", SqlDbType.NVarChar).Value = txtGhichu_CT.Text;
                        cmd.Parameters.Add("@Ghichu_DH", SqlDbType.NVarChar).Value = txtGhiChuDH.Text;
                        cmd.Parameters.Add("@Nguoilap_DM", SqlDbType.NVarChar).Value = txtUser.Text;
                        cmd.Parameters.Add("@QC_CT", SqlDbType.Int).Value = txtQC_CT.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    LoadDanhMuc_DINHMUCVATTU();                  
                }
                else
                {
                    MessageBox.Show("Cần kiểm tra nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        private void btnsua_Click(object sender, EventArgs e)
        {        
            if (gridLookMavattu.Text != "")
            {
                SqlConnection con = new SqlConnection();
                decimal soluongDH = Convert.ToDecimal(txtSL_DH.Text);
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("update  tblvattu_dauvao set nvkd=@nvkd,Iden=@Iden,Mavattu=@Mavattu,madh=@madh,Masp=@Masp,Tenquicachsp=@Tenquicachsp,Soluongsanpham=@Soluongsanpham,Donvi_sanpham=@Donvi_sanpham, "
                       +" Ma_CT=@Ma_CT,Ten_CT=@Ten_CT,Soluong_CT=@Soluong_CT,Ten_vattu=@Ten_vattu, SL_vattucan=@SL_vattucan, "
                       + " Donvi_vattu=@Donvi_vattu, Ghichu_CT=@Ghichu_CT, Ghichu_DH=@Ghichu_DH,Ngaylap_DM=GetDate(),Nguoilap_DM=@Nguoilap_DM,QC_CT=@QC_CT where CodeVatllieu like '"+txtId.Text+"'", con))
                    {
                        cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value =txtNVKD.Text ;
                        cmd.Parameters.Add("@Iden", SqlDbType.Int).Value = txtidPSX.Text;
                        cmd.Parameters.Add("@Mavattu", SqlDbType.NVarChar).Value = gridLookMavattu.Text;
                        cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = txtMadh.Text;
                        cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                        cmd.Parameters.Add("@Tenquicachsp", SqlDbType.NVarChar).Value = txttensp.Text;
                        cmd.Parameters.Add("@Soluongsanpham", SqlDbType.Int).Value = soluongDH;
                        cmd.Parameters.Add("@Donvi_sanpham", SqlDbType.NVarChar).Value = txtDonvi_sp.Text;
                        cmd.Parameters.Add("@Ma_CT", SqlDbType.NVarChar).Value = txtMaCT.Text;
                        cmd.Parameters.Add("@Ten_CT", SqlDbType.NVarChar).Value = txtTenCT.Text;
                        cmd.Parameters.Add("@Soluong_CT", SqlDbType.Int).Value = txtSL_CT.Text;
                        cmd.Parameters.Add("@Ten_vattu", SqlDbType.NVarChar).Value = txtTenVatlieu.Text;
                        cmd.Parameters.Add("@SL_vattucan", SqlDbType.Int).Value = txtSL_VT.Text;
                        cmd.Parameters.Add("@Donvi_vattu", SqlDbType.NVarChar).Value = cbdonvi_vatlieu.Text;
                        cmd.Parameters.Add("@Ghichu_CT", SqlDbType.NVarChar).Value = txtGhichu_CT.Text;
                        cmd.Parameters.Add("@Ghichu_DH", SqlDbType.NVarChar).Value = txtGhiChuDH.Text;                     
                        cmd.Parameters.Add("@QC_CT", SqlDbType.Int).Value = txtQC_CT.Text;
                        cmd.Parameters.Add("@Nguoilap_DM", SqlDbType.NVarChar).Value = txtUser.Text;
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
        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (txttrangthaiDH.Text != "")
                MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txtTenVatlieu.Text != "" && MessageBox.Show("Bạn muốn xóa CT: '" + txtTenVatlieu.Text + "'", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi knn = new ketnoi();
                int kq = knn.xulydulieu("delete from tblvattu_dauvao where CodeVatllieu like '" + txtId.Text + "' and Duyetsanxuat is null");                
                if (kq > 1)
                {
                    MessageBox.Show("XÓA CHI TIẾT ĐƠN HÀNG '" + txtTenVatlieu.Text + "' THÀNH CÔNG", "THÔNG BÁO");
                }
                LoadDanhMuc_DINHMUCVATTU();
            }
        }
        private void txtmasp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSL_DH_TextChanged(object sender, EventArgs e)
        {
            if (txtSL_DH.Text == "")txtSL_DH.Text = "0";
        }

        private void txtSL_CT_TextChanged(object sender, EventArgs e)
        {
            if (txtSL_CT.Text == "")txtSL_CT.Text = "0";
        }

        private void txtQC_CT_TextChanged(object sender, EventArgs e)
        {
            if (txtQC_CT.Text == "")txtQC_CT.Text = "0";
        }

        private void txtSL_VT_TextChanged(object sender, EventArgs e)
        { if (txtSL_VT.Text == "") txtSL_VT.Text = "0"; }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadMaDH(); LoadDANHMUC_SANPHAM(); LoadDanhMuc_DINHMUCVATTU();
        }

        private void cbMaDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDANHMUC_SANPHAM(); LoadDanhMuc_DINHMUCVATTU();
        }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadDANHMUC_SANPHAM(); LoadDanhMuc_DINHMUCVATTU();
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
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void addMavt_Click(object sender, EventArgs e)
        {
            frmThemDMvattu fThemDMVT = new frmThemDMvattu();
            fThemDMVT.ShowDialog();
            gridlookupPSX();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtLocal.Text = openFileDialog1.FileName;
                    string fullFileName = openFileDialog1.FileName;
                    string fileName = openFileDialog1.SafeFileName;
                    ftpClient.upload("/sanxuat_sanxuat/DM_PHIEU_SANXUAT/" + fileName, fullFileName);
                    if (ftpClient.message == "success")
                    {
                        MessageBox.Show(ftpClient.pathFileName);
                    }
                    else
                    {
                        MessageBox.Show(ftpClient.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btnDanhMucSP_Click(object sender, EventArgs e)
        {
            frmDinhMucVLChoDonHang danhMucSanPham = new frmDinhMucVLChoDonHang();
            danhMucSanPham.Show();
        }
    }
}
