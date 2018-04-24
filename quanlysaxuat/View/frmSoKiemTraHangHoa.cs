using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using quanlysanxuat.Report;

namespace quanlysanxuat.View
{
    public partial class frmSoKiemTraHangHoa : Form
    {
        public frmSoKiemTraHangHoa()
        {
            InitializeComponent();
        }

        private void frmSoKiemTraHangHoa_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = Phieu_kiem;
            dpstar.Text = DateTime.Today.ToString("01/MM/yyyy");
            dpend.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ItemNgoaiQuan_TinhNang_MucDoAnToan();
            ItemKiemTraKichThuoc();
            MucDo();
            DocDSBPSanXuat();//Đọc danh sách phòng ban lấy bộ phận sản xuất
            DanhMucDHTrienKhai();
            loadItemCbOK();
            loadItemCbNG();
            TraCuuBaoCaoKiemTraHangHoa();
            txtNguoiKiemTra.Text = Login.Username;
          
            cbKiemTraKichThuoc();
            KhoiTaoLaiGiaTriForm();
            cbKiemTraMucDo();
        }
        #region khởi tạo lại giá trị trên form
       
        private void KhoiTaoLaiGiaTriForm()
        {
            ItemNgoaiQuan_TinhNang_MucDoAnToan();
            ItemKiemTraKichThuoc();
            dpNgayHieuLuc.Format = DateTimePickerFormat.Custom;
            dpNgayHieuLuc.CustomFormat = " ";

            dpNgaySanXuat.Format = DateTimePickerFormat.Custom;
            dpNgaySanXuat.CustomFormat = " ";

            dpNgayKiemTra.Format = DateTimePickerFormat.Custom;
            dpNgayKiemTra.CustomFormat = " ";

            dpNgayDuyetMau.Format = DateTimePickerFormat.Custom;
            dpNgayDuyetMau.CustomFormat = " ";

            dpNgayDuyetMauMau.Format = DateTimePickerFormat.Custom;
            dpNgayDuyetMauMau.CustomFormat = " ";

            dpNgayXemXet.Format = DateTimePickerFormat.Custom;
            dpNgayXemXet.CustomFormat = " ";

            dpNgayDuyet.Format = DateTimePickerFormat.Custom;
            dpNgayDuyet.CustomFormat = " ";

            txtSanPham.Text = "";
            txtKhachHang.Text = "";
            txtSoLuong.Text = "";
            txtSoPO.Text = "";

            txtPhanXuong.Text = "";
            txtSoBaoCao.Text = "";
            ckChungLoai_Khung_OK.Checked = false;

            ckChungLoai_Khung_NG.Checked = false;


   
            ckChungLoai_Hardware_OK.Checked = false; 


             ckChungLoai_Hardware_NG.Checked = false; 


    
           ckChungLoai_Ghe_OK.Checked = false; 


           ckChungLoai_Ghe_NG.Checked = false; 

           ckShippingmark_Layoutthung_NG.Checked = false; 


            ckShippingmark_Layoutthung_OK.Checked = false; 


             ckShippingmark_TemNhan_OK.Checked = false; 

           ckShippingmark_TemNhan_NG.Checked = false; 


           ckShippingmark_Barcode_OK.Checked = false; 

   
            ckShippingmark_Barcode_NG.Checked = false; 

            
            ckShippingmark_HDLapRap_OK.Checked = false; 

         
            ckShippingmark_HDLapRap_NG.Checked = false;

            cbShippingmark_MucDo.Text = "";
            txtShippingmark_SoLuong.Text = "";

            cbKiemTra_KichThuoc_MucDo.Text = "";


            txtKiemTra_KichThuoc_SoLuongMau.Text = "";

            txtOnSiteTest_DoDayXi_TieuChuan.Text = "";
            cbOnSiteTest_LapRap_MucDoS3.Text = "";

            txtOnSiteTest_DoDayXi_ThucTe.Text = "";
            txtOnSiteTest_LapRap_SoLuongMau.Text = "";

           ckOnSiteTest_DoDayXiMa_OK.Checked = false; 


            ckOnSiteTest_DoDayXiNG.Checked = false; 


            ckOnSiteTest_LapRap_OK.Checked = false; 



            ckOnSiteTest_LapRap_NG.Checked = false; 


          ckMauChuan_MauDuyet.Checked = false; 

            ckMauChuan_MauDoiChung.Checked = false; 

           ckMauChuan_MauMau.Checked = false; 

            
            txtNguoiDuyetMau.Text = "";
            txtNguoiDuyetMauMau.Text = "";
            txtGhiChu.Text = "";

            ckKetQua_Dat.Checked = false; 

           ckKetQuaKhongDat.Checked = false; 


            txtNguoiKiemTra.Text = "";
         
            txtNguoiXemXet.Text = "";
       
            txtNguoiDuyet.Text = "";
    
            cbShippingmark_MucDo.Text = "";
            txtShippingmark_SoLuong.Text = "";
            cbKiemTra_KichThuoc_MucDo.Text = "";
            txtKiemTra_KichThuoc_SoLuongMau.Text = "";
            txtMucDo_SoLuongMau.Text = "";
        }
        private void dpNgayHieuLuc_ValueChanged(object sender, EventArgs e)
        {
            dpNgayHieuLuc.CustomFormat = null;
            dpNgayHieuLuc.Format = DateTimePickerFormat.Short;
        }

        private void dpNgaySanXuat_ValueChanged(object sender, EventArgs e)
        {
            dpNgaySanXuat.CustomFormat = null;
            dpNgaySanXuat.Format = DateTimePickerFormat.Short;
        }

        private void dpNgayKiemTra_ValueChanged(object sender, EventArgs e)
        {
            dpNgayKiemTra.CustomFormat = null;
            dpNgayKiemTra.Format = DateTimePickerFormat.Short;
        }

        private void dpNgayDuyetMau_ValueChanged(object sender, EventArgs e)
        {
            dpNgayDuyetMau.CustomFormat = null;
            dpNgayDuyetMau.Format = DateTimePickerFormat.Short;
        }

        private void dpNgayDuyetMauMau_ValueChanged(object sender, EventArgs e)
        {
            dpNgayDuyetMauMau.CustomFormat = null;
            dpNgayDuyetMauMau.Format = DateTimePickerFormat.Short;
        }

        private void dpNgayXemXet_ValueChanged(object sender, EventArgs e)
        {
            dpNgayXemXet.CustomFormat = null;
            dpNgayXemXet.Format = DateTimePickerFormat.Short;
        }
        #endregion
        private void dpNgayDuyet_ValueChanged(object sender, EventArgs e)
        {
            dpNgayDuyet.CustomFormat = null;
            dpNgayDuyet.Format = DateTimePickerFormat.Short;
        }
        private void cbKiemTraMucDo()
        {
            repositoryItemCBKTMucDo.Items.Clear();
               ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select distinct NoiDungMucDo 
                from tblKiemTraHangHoa_NQDoAnToan 
				where NoiDungMucDo !=''");
            kn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemCBKTMucDo.Items.Add(dt.Rows[i]["NoiDungMucDo"]);
            }
        }
        private void cbKiemTraKichThuoc()
        {
            repositoryItemcbNoiDungKTKichThuoc.Items.Clear();
               ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select NoiDung_KTKichThuoc
                from tblKiemTraHangHoa_KTKichThuocNoiDung 
                where NoiDung_KTKichThuoc !=''");
            kn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemcbNoiDungKTKichThuoc.Items.Add(dt.Rows[i]["NoiDung_KTKichThuoc"]);
            }
        }
        private void Xoa_NoiDungKichThuoc_Cu()
        {
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"truncate table tblKiemTraHangHoa_KTKichThuocNoiDung");
            int dt = kn.xulydulieu(strQuery);
            kn.dongketnoi();
        }

        private void ThemMoi_NoiDungKichThuoc()
        {
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@"insert into 
                tblKiemTraHangHoa_KTKichThuocNoiDung (NoiDung_KTKichThuoc) 
                (select Distinct NoiDung_KTKichThuoc from tblKiemTraHangHoa_KTKichThuoc)");
            int dt = kn.xulydulieu(strQuery);
            kn.dongketnoi();
        }

        #region CHỌN BỘ PHẬN CẦN KIỂM TRA
        private void DocDSBPSanXuat()
        {
            ketnoi kn = new ketnoi();
            cbDocDSBPSanXuat.DataSource = kn.laybang(@"select Matable from 
                tblPHONGBAN where KiemTra='QC'");
            cbDocDSBPSanXuat.ValueMember = "Matable";
            cbDocDSBPSanXuat.DisplayMember = "Matable";
            kn.dongketnoi();
        }
        private void BoPhaKiem()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Chuc_nang from 
                tblPHONGBAN where KiemTra='QC' and Matable=N'"+cbDocDSBPSanXuat.Text+"' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtPhanXuong.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void cbDocDSBPSanXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DanhMucDHTrienKhai();
            BoPhaKiem();
        }
        #endregion
        private void TraCuuBaoCaoKiemTraHangHoa()//Danh mục báo cáo kiểm tra hàng hóa
        {
            ketnoi kn = new ketnoi();
            string strQuery = string.Format(@" select *
                        FROM tblKiemTraHangHoa where NgayKiemTra
                        between '{0}' and '{1}' order by KiemTraHangID DESC",
                        dpstar.Value.ToString("yyyy-MM-dd"),
                        dpend.Value.ToString("yyyy-MM-dd"));
            gridControl3.DataSource = kn.laybang(strQuery);
            kn.dongketnoi();
        }
        private void btnTraCuSoKiemTraHang_Click(object sender, EventArgs e)
        {
            TraCuuBaoCaoKiemTraHangHoa();
        }
        private void loadItemCbOK()
        {
            repositoryItemComboBox3.Items.Add("OK");
            repositoryItemComboBox3.Items.Add("");
        }
        private void loadItemCbNG()
        {
            repositoryItemComboBox4.Items.Add("NG");
            repositoryItemComboBox4.Items.Add("");
        }

        private void DanhMucDHTrienKhai()
        {
            ketnoi cnn = new ketnoi();
            string banDuLieu = txtBangDuLieu.Text;
            string strQuery = string.Format(@"select * from " + cbDocDSBPSanXuat.Text + "('{0}','{1}')",
                 dpstar.Value.ToString("yyyy-MM-dd"),
                 dpend.Value.ToString("yyyy-MM-dd"));
            lkID_LookUp.Properties.DataSource = cnn.laybang(strQuery);
            lkID_LookUp.Properties.DisplayMember = "IDNoiKiem";
            lkID_LookUp.Properties.ValueMember = "IDNoiKiem";
            cnn.dongketnoi();
        }
        private void btnrefresh_lookup_Click(object sender, EventArgs e)
        {
            DanhMucDHTrienKhai();
            KhoiTaoLaiGiaTriForm();
        }
        private void MucDo()
        {
            for (int i = 1; i <= 4; i++)
            {
                cbKiemTra_KichThuoc_MucDo.Items.Add("S" + i);
                cbShippingmark_MucDo.Items.Add("S" + i);
                cbOnSiteTest_LapRap_MucDoS3.Items.Add("S" + i);
            }
        }

        private void ItemNgoaiQuan_TinhNang_MucDoAnToan()//Tạo mới kiểm tra mức độ an toàn
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select top 0 
                NoiDungMucDo,Crt,Maj,Min
                from tblKiemTraHangHoa_NQDoAnToan");
            kn.dongketnoi();
        }
        private void ItemKiemTraKichThuoc()//Tạo mới Kiểm tra kích thước
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select top 0 NoiDung_KTKichThuoc='',
                YeuCau='',ThucTe='',
                KetQua_OK='',KetQua_NG=''
                from tblKiemTraHangHoa_KTKichThuocNoiDung");
            kn.dongketnoi();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            BinDingBaoCao();//Ghi nội dung nút trên form
            BinDingDoAnToan();//Ghi nội dung kiểm tra tính năng mức độ an toàn
            BinDingKTKichThuoc();//Ghi nội dung kiểm tra kích thước
        }
        private void BinDingDoAnToan()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select KiemTraHangID,
                NoiDungMucDo,Crt,Maj,Min,BaoCaoSo
                from tblKiemTraHangHoa_NQDoAnToan 
                where BaoCaoSo='" + txtSoBaoCao.Text + "'");
            kn.dongketnoi();
        }
        private void BinDingKTKichThuoc()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select KiemTraHangID,
                NoiDung_KTKichThuoc,YeuCau,ThucTe,KetQua_OK,KetQua_NG,BaoCaoSo
                from tblKiemTraHangHoa_KTKichThuoc where
                BaoCaoSo='" + txtSoBaoCao.Text + "'");
            kn.dongketnoi();
        }
        private void BinDingBaoCao()
        {
            string point = "";
            point = gridView3.GetFocusedDisplayText();
            txtSanPham.Text = gridView3.GetFocusedRowCellDisplayText(TenSanPham_col3);
            txtKhachHang.Text = gridView3.GetFocusedRowCellDisplayText(KhachHang_col3);
            txtSoLuong.Text = gridView3.GetFocusedRowCellDisplayText(SoLuong_col3);
            txtSoPO.Text = gridView3.GetFocusedRowCellDisplayText(SoPO_col3);
            dpNgayHieuLuc.Text = gridView3.GetFocusedRowCellDisplayText(NgayHieuLuc_col3);
            dpNgaySanXuat.Text = gridView3.GetFocusedRowCellDisplayText(NgaySanXuat_col3);
            dpNgayKiemTra.Text = gridView3.GetFocusedRowCellDisplayText(NgayKiemTra_col3);
            txtPhanXuong.Text = gridView3.GetFocusedRowCellDisplayText(PhanXuong_col3);
            txtSoBaoCao.Text = gridView3.GetFocusedRowCellDisplayText(BaoCaoSo_col3);
            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_Khung_col3) == "OK")
            { ckChungLoai_Khung_OK.Checked = true; }
            else { ckChungLoai_Khung_OK.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_Khung_col3) == "NG")
            { ckChungLoai_Khung_NG.Checked = true; }
            else { ckChungLoai_Khung_NG.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_HardWare_col3) == "OK")
            { ckChungLoai_Hardware_OK.Checked = true; }
            else { ckChungLoai_Hardware_OK.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_HardWare_col3) == "NG")
            { ckChungLoai_Hardware_NG.Checked = true; }
            else { ckChungLoai_Hardware_NG.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_Ghe_col3) == "OK")
            { ckChungLoai_Ghe_OK.Checked = true; }
            else { ckChungLoai_Ghe_OK.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Chung_Loai_Ghe_col3) == "NG")
            { ckChungLoai_Ghe_NG.Checked = true; }
            else { ckChungLoai_Ghe_NG.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Layout_col3) == "NG")
            { ckShippingmark_Layoutthung_NG.Checked = true; }
            else { ckShippingmark_Layoutthung_NG.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Layout_col3) == "OK")
            { ckShippingmark_Layoutthung_OK.Checked = true; }
            else { ckShippingmark_Layoutthung_OK.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Tem_col3) == "OK")
            { ckShippingmark_TemNhan_OK.Checked = true; }
            else { ckShippingmark_TemNhan_OK.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Tem_col3) == "NG")
            { ckShippingmark_TemNhan_NG.Checked = true; }
            else { ckShippingmark_TemNhan_NG.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Barcode_col3) == "OK")
            { ckShippingmark_Barcode_OK.Checked = true; }
            else { ckShippingmark_Barcode_OK.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_Barcode_col3) == "NG")
            { ckShippingmark_Barcode_NG.Checked = true; }
            else { ckShippingmark_Barcode_NG.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_HDLapRap_col3) == "OK")
            { ckShippingmark_HDLapRap_OK.Checked = true; }
            else { ckShippingmark_HDLapRap_OK.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(Shipingmark_HDLapRap_col3) == "NG")
            { ckShippingmark_HDLapRap_NG.Checked = true; }
            else { ckShippingmark_HDLapRap_NG.Checked = false; }

            cbShippingmark_MucDo.Text = gridView3.GetFocusedRowCellDisplayText(Shipingmark_MucDo_col3);
            txtShippingmark_SoLuong.Text = gridView3.GetFocusedRowCellDisplayText(Shipingmark_SoLuongMau_col3);
            //Mức độ lỗi từ I-100%
            if (gridView3.GetFocusedRowCellDisplayText(LoiMucDo_col3)=="I")
            {
                ckMucDo_I.Checked = true;
            }
            else { ckMucDo_I.Checked = false; }
            if (gridView3.GetFocusedRowCellDisplayText(LoiMucDo_col3) == "II")
            {
                ckMucDo_II.Checked = true;
            }
            else { ckMucDo_II.Checked = false; }
            if (gridView3.GetFocusedRowCellDisplayText(LoiMucDo_col3) == "III")
            {
                ckMucDo_III.Checked = true;
            }
            else { ckMucDo_III.Checked = false; }
            if (gridView3.GetFocusedRowCellDisplayText(LoiMucDo_col3) == "100")
            {
                ckMucDo_100.Checked = true;
            }
            else { ckMucDo_100.Checked = false; }

            txtKiemTra_KichThuoc_SoLuongMau.Text = gridView3.GetFocusedRowCellDisplayText(LoiMucDo_SLMau_col3);

            txtOnSiteTest_DoDayXi_TieuChuan.Text = gridView3.GetFocusedRowCellDisplayText(OnsiteTest_XiMaTieuChuan_col3);
            cbOnSiteTest_LapRap_MucDoS3.Text = gridView3.GetFocusedRowCellDisplayText(OnsiteTest_LRap_MucDo_col3);

            txtOnSiteTest_DoDayXi_ThucTe.Text = gridView3.GetFocusedRowCellDisplayText(OnsiteTest_XiMaThucTe_col3);
            txtOnSiteTest_LapRap_SoLuongMau.Text = gridView3.GetFocusedRowCellDisplayText(OnsiteTest_LRap_SLMau_col3);

            if (gridView3.GetFocusedRowCellDisplayText(OnsiteTest_KetQua_col3) == "OK")
            { ckOnSiteTest_DoDayXiMa_OK.Checked = true; }
            else { ckOnSiteTest_DoDayXiMa_OK.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(OnsiteTest_KetQua_col3) == "NG")
            { ckOnSiteTest_DoDayXiNG.Checked = true; }
            else { ckOnSiteTest_DoDayXiNG.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(OnsiteTest_KetQua_col3) == "OK")
            { ckOnSiteTest_LapRap_OK.Checked = true; }
            else { ckOnSiteTest_LapRap_OK.Checked = false; }



            if (gridView3.GetFocusedRowCellDisplayText(OnsiteTest_KetQua_col3) == "NG")
            { ckOnSiteTest_LapRap_NG.Checked = true; }
            else { ckOnSiteTest_LapRap_NG.Checked = false; }


            if (gridView3.GetFocusedRowCellDisplayText(MauChuan_LoaiMau_col3) == "Có mẫu chuẩn")
            { ckMauChuan_MauDuyet.Checked = true; }
            else { ckMauChuan_MauDuyet.Checked = false; }

        
            if (gridView3.GetFocusedRowCellDisplayText(MauChuan_MauDoiChung_col3) == "Có mẫu đối chứng")
            { ckMauChuan_MauDoiChung.Checked = true; }
            else { ckMauChuan_MauDoiChung.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(MauChuan_MMau_col3) == "Có mẫu màu")
            { ckMauChuan_MauMau.Checked = true; }
            else { ckMauChuan_MauMau.Checked = false; }

            dpNgayDuyet.Text = gridView3.GetFocusedRowCellDisplayText(MauChuan_NgayDuyet_col3);
            dpNgayDuyetMauMau.Text = gridView3.GetFocusedRowCellDisplayText(MauChuan_NgayDuyetMau_col3);
            txtNguoiDuyetMau.Text = gridView3.GetFocusedRowCellDisplayText(MauChuan_NguoiDuyet_col3);
            txtNguoiDuyetMauMau.Text = gridView3.GetFocusedRowCellDisplayText(MauChuan_NguoiDuyetMMau_col3);
            txtGhiChu.Text = gridView3.GetFocusedRowCellDisplayText(GhiChu_col3);

            if (gridView3.GetFocusedRowCellDisplayText(KetQua_col3) == "Đạt")
            { ckKetQua_Dat.Checked = true; }
            else { ckKetQua_Dat.Checked = false; }

            if (gridView3.GetFocusedRowCellDisplayText(KetQua_col3) == "Không đạt")
            { ckKetQuaKhongDat.Checked = true; }
            else { ckKetQuaKhongDat.Checked = false; }


            txtNguoiKiemTra.Text = gridView3.GetFocusedRowCellDisplayText(KiemTra_Nguoi_col3);
            dpNgayKiemTra.Text = gridView3.GetFocusedRowCellDisplayText(NgayKiemTra_col3);
            txtNguoiXemXet.Text = gridView3.GetFocusedRowCellDisplayText(XemXet_Nguoi_col3);
            dpNgayXemXet.Text = gridView3.GetFocusedRowCellDisplayText(XemXet_Ngay_col3);
            txtNguoiDuyet.Text = gridView3.GetFocusedRowCellDisplayText(Duyet_Nguoi_col3);
            dpNgayDuyet.Text = gridView3.GetFocusedRowCellDisplayText(Duyet_Ngay_col3);
            cbShippingmark_MucDo.Text = gridView3.GetFocusedRowCellDisplayText(Shipingmark_MucDo_col3);
            txtShippingmark_SoLuong.Text = gridView3.GetFocusedRowCellDisplayText(Shipingmark_SoLuongMau_col3);
            cbKiemTra_KichThuoc_MucDo.Text = gridView3.GetFocusedRowCellDisplayText(KTKThuoc_MucDo_col3);
            txtKiemTra_KichThuoc_SoLuongMau.Text = gridView3.GetFocusedRowCellDisplayText(KTKThuoc_SLMau_col3);
            txtMucDo_SoLuongMau.Text= gridView3.GetFocusedRowCellDisplayText(LoiMucDo_SLMau_col3);
        }
        private void ckChungLoai_Khung_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Khung_OK.Checked == true)
            {
                ckChungLoai_Khung_NG.Checked = false;
               
            }
        }

        private void ckChungLoai_Khung_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Khung_NG.Checked == true)
            {
                ckChungLoai_Khung_OK.Checked = false;
                
            }
        }

        private void ckChungLoai_Hardware_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Hardware_OK.Checked == true)
            {
                ckChungLoai_Hardware_NG.Checked = false;
       
            }
        }

        private void ckChungLoai_Hardware_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Hardware_NG.Checked == true)
            {
                ckChungLoai_Hardware_OK.Checked = false;
       
            }
        }

        private void ckChungLoai_Ghe_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Ghe_OK.Checked == true)
            {
                ckChungLoai_Ghe_NG.Checked = false;
           
            }
        }

        private void ckChungLoai_Ghe_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChungLoai_Ghe_NG.Checked == true)
            {
                ckChungLoai_Ghe_OK.Checked = false;

            }
        }

        private void ckShippingmark_Layoutthung_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_Layoutthung_OK.Checked == true)
            {
                ckShippingmark_Layoutthung_NG.Checked = false;
              
            }
        }

        private void ckShippingmark_Layoutthung_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_Layoutthung_NG.Checked == true)
            {
                ckShippingmark_Layoutthung_OK.Checked = false;
               
            }
        }

        private void ckShippingmark_TemNhan_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_TemNhan_OK.Checked == true)
            {
                ckShippingmark_TemNhan_NG.Checked = false;
      
            }
        }

        private void ckShippingmark_TemNhan_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_TemNhan_NG.Checked == true)
            {
                ckShippingmark_TemNhan_OK.Checked = false;
               
            }
        }

        private void ckShippingmark_Barcode_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_Barcode_OK.Checked == true)
            {
                ckShippingmark_Barcode_NG.Checked = false;
              
            }
        }

        private void ckShippingmark_Barcode_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_Barcode_NG.Checked == true)
            {
                ckShippingmark_Barcode_OK.Checked = false;
              
            }
        }

        private void ckShippingmark_HDLapRap_OK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_HDLapRap_OK.Checked == true)
            {
                ckShippingmark_HDLapRap_NG.Checked = false;
               
            }
        }

        private void ckShippingmark_HDLapRap_NG_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShippingmark_HDLapRap_NG.Checked == true)
            {
                ckShippingmark_HDLapRap_OK.Checked = false;
             
            }
        }

        private void ckMucDo_I_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMucDo_I.Checked == true)
            {
                ckMucDo_II.Checked = false;
                ckMucDo_III.Checked = false;
                ckMucDo_100.Checked = false;
               
            }
        }

        private void ckMucDo_II_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMucDo_II.Checked == true)
            {
                ckMucDo_I.Checked = false;
                ckMucDo_III.Checked = false;
                ckMucDo_100.Checked = false;
            }
        }

        private void ckMucDo_III_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMucDo_III.Checked == true)
            {
                ckMucDo_I.Checked = false;
                ckMucDo_II.Checked = false;
                ckMucDo_100.Checked = false;
            }
        }

        private void ckMucDo_100_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMucDo_100.Checked == true)
            {
                ckMucDo_II.Checked = false;
                ckMucDo_III.Checked = false;
                ckMucDo_I.Checked = false;
            }
        }

        private void ckOnSiteTest_DoDayXiMa_OK_CheckedChanged(object sender, EventArgs e)
        {
           
            if (ckOnSiteTest_DoDayXiMa_OK.Checked == true)
            {
                ckOnSiteTest_DoDayXiNG.Checked = false;
            }
        }

        private void ckOnSiteTest_DoDayXiNG_CheckedChanged(object sender, EventArgs e)
        {            
            if (ckOnSiteTest_DoDayXiNG.Checked == true)
            {
                ckOnSiteTest_DoDayXiMa_OK.Checked = false;
            }           
        }

        private void ckOnSiteTest_LapRap_OK_CheckedChanged(object sender, EventArgs e)
        {
         
            if (ckOnSiteTest_LapRap_OK.Checked == true)
            {
                ckOnSiteTest_LapRap_NG.Checked = false;
            }
            
        }

        private void ckOnSiteTest_LapRap_NG_CheckedChanged(object sender, EventArgs e)
        {
           
            if (ckOnSiteTest_LapRap_NG.Checked == true)
            {
                ckOnSiteTest_LapRap_OK.Checked = false;
            }
            
        }

        private void ckKetQua_Dat_CheckedChanged(object sender, EventArgs e)
        {
            if (ckKetQua_Dat.Checked == true)
            {
                ckKetQuaKhongDat.Checked = false;
            }
        }

        private void ckKetQuaKhongDat_CheckedChanged(object sender, EventArgs e)
        {
            if (ckKetQuaKhongDat.Checked == true)
            {
                ckKetQua_Dat.Checked = false;
            
            }
        }
        #region Ghi nội dung chi tiết lỗi mục 3
        private void LoiNgoaiQuanTinhNang_Ghi()
        {
            if (txtSoBaoCao.Text == "")
            { MessageBox.Show("Mã báo cáo không hợp lệ", "Thông báo"); return; }

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    string strQuery = string.Format(@"insert into tblKiemTraHangHoa_NQDoAnToan
                    (NoiDungMucDo,Crt,Maj,Min,BaoCaoSo) 
                    values(N'{0}','{1}','{2}','{3}','{4}')",
                        gridView1.GetRowCellValue(i, "NoiDungMucDo").ToString(),
                        gridView1.GetRowCellValue(i, "Crt").ToString(),
                        gridView1.GetRowCellValue(i, "Maj").ToString(),
                        gridView1.GetRowCellValue(i, "Min").ToString(),
                        txtSoBaoCao.Text
                        );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lý do:" + ex, "Thông báo"); }
        }
        #endregion
        #region Ghi nội dung lỗi mục 4
        private void KiemTraKichThuoc_Ghi()
        {
            if (txtSoBaoCao.Text == "")
            { MessageBox.Show("Mã báo cáo không hợp lệ", "Thông báo"); return; }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                for (int i = 0; i < gridView2.DataRowCount; i++)
                {
                    string strQuery = string.Format(@"insert into tblKiemTraHangHoa_KTKichThuoc 
                    (NoiDung_KTKichThuoc,YeuCau,ThucTe,KetQua_OK,KetQua_NG,BaoCaoSo)
                    values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}','{5}')",
                        gridView2.GetRowCellValue(i, "NoiDung_KTKichThuoc"),
                        gridView2.GetRowCellValue(i, "YeuCau"),
                        gridView2.GetRowCellValue(i, "ThucTe"),
                        gridView2.GetRowCellValue(i, "KetQua_OK"),
                        gridView2.GetRowCellValue(i, "KetQua_NG"),
                        txtSoBaoCao.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lý do:" + ex, "Thông báo"); }
        }
        #endregion
        private void BaoCaoKiemTra_Ghi()
        {
            var ngayHieuLuc = dpNgayHieuLuc.Value.ToString("yyyy-MM-dd");
            var ngaySanXuat = dpNgaySanXuat.Value.ToString("yyyy-MM-dd");
            var ngayKiemTra = dpNgayKiemTra.Value.ToString("yyyy-MM-dd");
            var ngayDuyetMau = dpNgayDuyetMau.Value.ToString("yyyy-MM-dd");
            var ngayDuyetMM = dpNgayDuyetMauMau.Value.ToString("yyyy-MM-dd");

            var ngayLapPhieuKiem = dpNgayLapPhieuKiem.Value.ToString("yyyy-MM-dd");
            var ngayXemXet = dpNgayXemXet.Value.ToString("yyyy-MM-dd");
            var ngayDuyet = dpNgayDuyet.Value.ToString("yyyy-MM-dd");
            double mucDo_SoLuongMau = txtMucDo_SoLuongMau.Text==""?0: Convert.ToDouble(txtMucDo_SoLuongMau.Text);
            double shippingmark_SoLuong= txtShippingmark_SoLuong.Text==""?0: Convert.ToDouble(txtShippingmark_SoLuong.Text);
            double kiemTra_KichThuoc_SoLuongMau =txtKiemTra_KichThuoc_SoLuongMau.Text==""?0: Convert.ToDouble(txtKiemTra_KichThuoc_SoLuongMau.Text);
            double soLuong = txtSoLuong.Text==""?0: Convert.ToDouble(txtSoLuong.Text);
            int[] listRowList = this.gridView2.GetSelectedRows();
            if (txtSoBaoCao.Text == "")
            { MessageBox.Show("Số báo cáo chưa có", "Thông báo"); return; }
            if (txtSanPham.Text == "")
            { MessageBox.Show("Sản phẩm chưa có", "Thông báo"); return; }
            try
            {
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gridView2.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"insert into tblKiemTraHangHoa
                        (DonHangID,
                        SoPO,SoLuong,NgaySanXuat,NgayKiemTra,
                        TenSanPham,MaSanPham,KhachHang,PhanXuong,BaoCaoSo,CongDoanKiem,
                        Chung_Loai_Khung,Chung_Loai_HardWare,Chung_Loai_Ghe,
                        Shipingmark_Layout,
                        Shipingmark_Tem,Shipingmark_Barcode,Shipingmark_HDLapRap,
                        LoiMucDo,LoiMucDo_SLMau,
                        KTKThuoc_MucDo,KTKThuoc_SLMau,
                        OnsiteTest_XiMaTieuChuan,OnsiteTest_XiMaThucTe,OnsiteTest_KetQua,
                        OnsiteTest_LRap_MucDo,OnsiteTest_LRap_SLMau,OnsiteTest_LRap_KetQua,
                
                        MauChuan_MauDuyet,MauChuan_MauDoiChung,MauChuan_MMau,
                        MauChuan_NgayDuyetMau,MauChuan_NgayDuyetMMau,MauChuan_NguoiDuyetMau,MauChuan_NguoiDuyetMMau,
                
                        GhiChu,
                        KetQua,
                        KiemTra_Nguoi,KiemTra_Ngay,NgayHieuLuc,
                        Shipingmark_SoLuongMau,Shipingmark_MucDo)
                        values
                        (
                        '{0}'
                        ,N'{1}','{2}','{3}','{4}'
                        ,N'{5}','{6}','{7}',N'{8}','{9}',N'{10}',
                        '{11}','{12}','{13}',
                        '{14}',
                        '{15}','{16}','{17}',
                        '{18}','{19}',
                        '{20}','{21}',
                        '{22}','{23}','{24}',
                        '{25}','{26}','{27}',
                        N'{28}',N'{29}',N'{30}',
                        '{31}',N'{32}',N'{33}',N'{34}',

                        N'{35}',
                        N'{36}',
                        N'{37}',N'{38}','{39}',
                        '{40}',N'{41}')",
                            lkID_LookUp.Text,
                            txtSoPO.Text,soLuong, ngaySanXuat, ngayKiemTra,
                            txtSanPham.Text, txtBanVe.Text, txtKhachHang.Text, txtPhanXuong.Text, txtSoBaoCao.Text, txtCongDoanKiemTra.Text,
                            ckChungLoai_Khung_OK.Checked == true ? "OK" : ckChungLoai_Khung_NG.Checked == true ? "NG" : "", ckChungLoai_Hardware_OK.Checked == true ? "OK" : ckChungLoai_Hardware_NG.Checked == true ? "NG" : "", ckChungLoai_Ghe_OK.Checked == true ? "OK" : ckChungLoai_Ghe_NG.Checked == true ? "NG" : "",
                            ckShippingmark_Layoutthung_OK.Checked == true ? "OK" : ckShippingmark_Layoutthung_NG.Checked == true ? "NG" : "",
                            ckShippingmark_TemNhan_OK.Checked == true ? "OK" : ckShippingmark_TemNhan_NG.Checked == true ? "NG" : "", ckShippingmark_Barcode_OK.Checked == true ? "OK" : ckShippingmark_Barcode_NG.Checked == true ? "NG" : "",
                            ckShippingmark_HDLapRap_OK.Checked == true ? "OK" : ckShippingmark_HDLapRap_NG.Checked == true ? "NG" : "",
                            ckMucDo_I.Checked == true ? "I" : ckMucDo_II.Checked == true ? "II" : ckMucDo_III.Checked == true ? "III" : ckMucDo_100.Checked == true ? "100" : "", mucDo_SoLuongMau,
                            cbKiemTra_KichThuoc_MucDo.Text,kiemTra_KichThuoc_SoLuongMau,
                            txtOnSiteTest_DoDayXi_TieuChuan.Text, txtOnSiteTest_DoDayXi_ThucTe.Text, ckOnSiteTest_DoDayXiMa_OK.Checked == true ? "OK" : ckOnSiteTest_DoDayXiNG.Checked == true ? "NG" : "",
                            cbOnSiteTest_LapRap_MucDoS3.Text, txtOnSiteTest_LapRap_SoLuongMau.Text, ckOnSiteTest_LapRap_OK.Checked == true ? "OK" : ckOnSiteTest_LapRap_NG.Checked == true ? "NG" : "",
                            ckMauChuan_MauDuyet.Checked == true ? "Có mẫu chuẩn" : "", ckMauChuan_MauDoiChung.Checked == true ? "Có mẫu đối chứng" : "", ckMauChuan_MauMau.Checked == true ? "Có mẫu màu" : "",
                            ngayDuyetMau, ngayDuyetMM, txtNguoiDuyetMau.Text, txtNguoiDuyetMauMau.Text,
                            txtGhiChu.Text,
                            ckKetQua_Dat.Checked == true ? "Đạt" : ckKetQuaKhongDat.Checked == true ? "Không đạt" : "",
                            txtNguoiKiemTra.Text, ngayLapPhieuKiem,ngayHieuLuc, 
                            shippingmark_SoLuong,cbShippingmark_MucDo.Text);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    LoiNgoaiQuanTinhNang_Ghi();//Ghi dữ liệu nội dung số 3
                    KiemTraKichThuoc_Ghi();//Ghi dữ liệu vào nội dung số 4
                    UpDateKiemTraHangHoa_NQDoAnToan();//Update ID cua kiem tra hang hoa sang tblKTKichThuoc (Noi dung 3)
                    UpDateKiemTraHangHoa_KTKichThuoc();//Update ID cua kiem tra hang hoa sang tblKTKichThuoc (Noi dung 4)
                    TraCuuBaoCaoKiemTraHangHoa();
                    cbKiemTraMucDo();
                    cbKiemTraKichThuoc();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lý do" + ex, "Thông báo"); }
        }
        private void btnKiemTraDat_Click(object sender, EventArgs e)
        {
            BaoCaoKiemTra_Ghi();//Ghi dữ liệu vào Sổ kiểm tra hàng hóa(Số Báo cáo - primary Key)
            Xoa_NoiDungKichThuoc_Cu();
            ThemMoi_NoiDungKichThuoc();
        }
        private void UpDateKiemTraHangHoa_NQDoAnToan()//Update ID cua kiem tra hang hoa sang tblKTKichThuoc(Noi dung 3)
        {
            ketnoi cnn = new ketnoi();
            int kq = cnn.xulydulieu(@"update tblKiemTraHangHoa_NQDoAnToan
                set KiemTraHangID=tblKiemTraHangHoa.KiemTraHangID
                from tblKiemTraHangHoa,tblKiemTraHangHoa_NQDoAnToan
                where tblKiemTraHangHoa_NQDoAnToan.BaoCaoSo=tblKiemTraHangHoa.BaoCaoSo
                and tblKiemTraHangHoa_NQDoAnToan.BaoCaoSo='" + txtSoBaoCao.Text + "'");
            cnn.dongketnoi();
        }
        private void UpDateKiemTraHangHoa_KTKichThuoc()//Update ID cua kiem tra hang hoa sang tblKTKichThuoc (Noi dung 4)
        {
            ketnoi cnn = new ketnoi();
            int kq = cnn.xulydulieu(@"update tblKiemTraHangHoa_KTKichThuoc 
                set KiemTraHangID=tblKiemTraHangHoa.KiemTraHangID
                from tblKiemTraHangHoa,tblKiemTraHangHoa_KTKichThuoc
                where tblKiemTraHangHoa.BaoCaoSo=tblKiemTraHangHoa_KTKichThuoc.BaoCaoSo
                and tblKiemTraHangHoa_KTKichThuoc.BaoCaoSo='" + txtSoBaoCao.Text + "'");
            cnn.dongketnoi();
        }
      
        private void KiemTraTrung()
        {
            ketnoi cnn = new ketnoi();
            int kq = cnn.xulydulieu("");
        }
        private void GhiKetQuaKiemVaoSoGiaCong()
        {
            ketnoi cnn = new ketnoi();
            string strCK = ckKetQua_Dat.Checked == true ? "OK" : ckKetQuaKhongDat.Checked == true ? "NO" : "";
            int kq = cnn.xulydulieu("update tbl11 set QCPass='" + strCK + "'where Num='" + lkID_LookUp.Text + "'");
            cnn.dongketnoi();
            if (kq > 0) { MessageBox.Show("Success"); }
        }
        private void GhiKetQuaKiemVaoSoXuatHang()
        {
            ketnoi cnn = new ketnoi();
            string strCK = ckKetQua_Dat.Checked == true ? "OK" : ckKetQuaKhongDat.Checked == true ? "NO" : "";
            int kq = cnn.xulydulieu("update tblXuat_GiaCong set QCPass='" + strCK + "' where SoGiaCongID='" + lkID_LookUp.Text + "'");
            cnn.dongketnoi();
            if (kq > 0) { MessageBox.Show("Success"); }
        }

        private void btnLaySoBaoCao_Click(object sender, EventArgs e)
        {
            if (txtPhanXuong.Text == "")
            {
                MessageBox.Show("Chưa cung cấp phân xưởng kiểm tra", "Thông báo");
                return;
            }
            if (txtCongDoanKiemTra.Text == "")
            {
                MessageBox.Show("Chưa cung cấp công đoạn kiểm tra", "Thông báo");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(@"DECLARE @d DATE= GetDate()
                SELECT 'BCKT'+Right(CONVERT(nvarchar(10), @d, 112),6)+'-'+
                replace(left(CONVERT(time, GetDate()),8),':','')", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    txtSoBaoCao.Text = Convert.ToString(reader[0]);
                reader.Close();
            }
        }

        private void gridLookUpEditDonHangTrienKhai_EditValueChanged(object sender, EventArgs e)
        {
            string point = "";
            point = gridLookUpEdit1View.GetFocusedDisplayText();
            txtSanPham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(ChiTietSanPham_look);
            txtKhachHang.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(khachhang_look);
            txtSoPO.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(MaPo_look);
            txtBanVe.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(mabv_look);
            txtSoLuong.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(soluongyc_look);
            txtCongDoanKiemTra.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(sanpham_look);
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtBanVe.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {
            BanTamBaoCao();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select * from KiemTraHang_vw where SoBaoCao='" + txtSoBaoCao.Text+"' ");
            XRKiemTraHangHoa rpKiemHang = new XRKiemTraHangHoa();
            rpKiemHang.DataSource = dt;
            rpKiemHang.DataMember = "Table";
            rpKiemHang.CreateDocument(false);
            rpKiemHang.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtSoBaoCao.Text;
            PrintTool tool = new PrintTool(rpKiemHang.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void BanTamBaoCao()
        {
            ketnoi kn = new ketnoi();
            string strQurey = string.Format(@"truncate table KiemTraHang_temp;
                insert into KiemTraHang_temp (NoiDungMucDo,Crt,Maj,Min,SoBaoCao,NoiDung_KTKichThuoc,
	                YeuCau,ThucTe,KetQua_OK,KetQua_NG)
	                select * from 
                (SELECT
                  NoiDungMucDo,Crt,Maj,Min,t2.BaoCaoSo SoBaoCao,
                  NoiDung_KTKichThuoc,YeuCau,ThucTe,KetQua_OK,KetQua_NG
                FROM
                  (SELECT
                    ROW_NUMBER() OVER (ORDER BY BaoCaoSo) AS ROW,
	                KiemTraHangID,NoiDungMucDo,Crt,Maj,Min,BaoCaoSo
                  FROM tblKiemTraHangHoa_NQDoAnToan where BaoCaoSo='{0}') t1
	                full outer JOIN
                  (SELECT
                    ROW_NUMBER() OVER (ORDER BY BaoCaoSo) AS ROW,
	                KiemTraHangID,NoiDung_KTKichThuoc,YeuCau,ThucTe,KetQua_OK,KetQua_NG,BaoCaoSo
                  FROM tblKiemTraHangHoa_KTKichThuoc where BaoCaoSo='{0}') t2
	                ON t1.ROW = t2.ROW)t", txtSoBaoCao.Text);
            int kq = kn.xulydulieu(strQurey);
            //MessageBox.Show("" + kq);
            kn.dongketnoi();
        }


        private void btngui_file_duyet_Click(object sender, EventArgs e)
        {
            FtpClient ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");

            //Mở tập tin máy Local
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "Desktop\\";
            openFileDialog1.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Chuyển tập tin từ local lên máy remote
                    string fullFileName = openFileDialog1.FileName;
                    string fileName = openFileDialog1.SafeFileName;
                    ftpClient.upload("QUAN_LY_CHAT_LUONG/" + fileName, fullFileName);
                    ftpClient.rename("QUAN_LY_CHAT_LUONG/" + fileName,txtSoBaoCao.Text+".PDF");
                    if (ftpClient.message == "xong")
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
                    MessageBox.Show("Lỗi lý do: " + ex.Message);
                }
            }
        }

        private void btnDocHoSoChatLuong_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtSoBaoCao.Text,txtPathHoSo.Text);//\\Server\kythuat\DM_SANPHAM
            f2.Show();
        }
        
        private void ShowOnSiteTest()
        {
            if (txtOnSiteTest_DoDayXi_TieuChuan.Text != "" &&
              txtOnSiteTest_DoDayXi_ThucTe.Text != "")
            {
                ckOnSiteTest_DoDayXiMa_OK.Enabled = true;
                ckOnSiteTest_DoDayXiNG.Enabled = true;
            }
            else
            {
                ckOnSiteTest_DoDayXiMa_OK.Enabled = false;
                ckOnSiteTest_DoDayXiNG.Enabled = false;
            }
        }
        private void ShowOnSiteTestMucDo()
        {
            if (txtOnSiteTest_LapRap_SoLuongMau.Text != "")
            {
                ckOnSiteTest_LapRap_OK.Enabled = true;
                ckOnSiteTest_LapRap_NG.Enabled = true;
            }
            else
            {
                ckOnSiteTest_LapRap_OK.Enabled = false;
                ckOnSiteTest_LapRap_NG.Enabled = false;
            }
        }
        private void txtOnSiteTest_LapRap_SoLuongMau_TextChanged(object sender, EventArgs e)
        {
            ShowOnSiteTestMucDo();
        }
        private void txtOnSiteTest_DoDayXi_TieuChuan_TextChanged(object sender, EventArgs e)
        {
            ShowOnSiteTest();
        }

        private void txtOnSiteTest_DoDayXi_ThucTe_TextChanged(object sender, EventArgs e)
        {
            ShowOnSiteTest();
        }

        private void cbOnSiteTest_LapRap_MucDoS3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnMoHoSo_Click(object sender, EventArgs e)
        {
            FtpClient ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
            ftpClient.directoryListDetailed("QUAN_LY_CHAT_LUONG/");
        }
    }
}