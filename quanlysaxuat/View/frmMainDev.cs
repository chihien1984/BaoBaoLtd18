using quanlysanxuat.View;
using quanlysanxuat.View.UcControl;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using quanlysanxuat.Model;
using System.Data.Entity.Migrations;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace quanlysanxuat
{
    [System.Runtime.InteropServices.Guid("108ED720-51E6-46BF-8DBD-2FEE18D07A66")]
    public partial class MainDev : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static string username;
        public static string department;
        SANXUATDbContext baobao = new SANXUATDbContext();
        public string CurrentVersion //kiem tra Version
        {
            get
            {
                return System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public MainDev()
        {
            InitializeComponent();
        }

        public void OpenForm(Type typeform)
        {
            foreach (var frm in MdiChildren.Where(frm => frm.GetType() == typeform))
            {
                frm.Activate();
                return;
            }
            var form = (Form)(Activator.CreateInstance(typeform));
            BeginInvoke(new Action(() =>
            {
                form.MdiParent = this;
                form.Show();
            }));
        }
        #region formload
        private void MainDev_Load(object sender, EventArgs e)
        {
            RoleLogin();
            brUser.Caption = Login.Username;
            DocTenBoPhanTheoMa();
            username = brUser.Caption;
            department = brDepartment.Caption;
            //SaveRibbon();
            LoadRights();
        }
        #endregion
        private void Message(object sender, EventArgs e){
            MessageBox.Show("say simble","messa");
        }
        private void RoleLogin()
        {
            if (ClassUser.User=="99999"|| ClassUser.User == "00288")
            {
                btnPermission.Visibility= BarItemVisibility.Always;
            }
            else
            {
                btnPermission.Visibility = BarItemVisibility.Never;
            }
        }
        private void SaveRibbon()
        {  
            foreach (RibbonPage page in ribbonControl1.Pages)
            {
                var pageF = new tblFunction();
                pageF.Menu = page.Name;
                pageF.Application = "AD";
                pageF.Description = page.Text;
                pageF.ParentMenu = null;
                baobao.tblFunctions.AddOrUpdate(pageF);
                foreach (RibbonPageGroup pageGroup in page.Groups)
                {
                    var pageGroupF = new tblFunction();
                    pageGroupF.Menu = pageGroup.Name;
                    pageGroupF.Application = "AD";
                    pageGroupF.Description = pageGroup.Text;
                    pageGroupF.ParentMenu = page.Name;
                    baobao.tblFunctions.AddOrUpdate(pageGroupF);
                    foreach (BarItemLink barItemLink in pageGroup.ItemLinks)
                    {
                        var barItemLinkF = new tblFunction();
                        barItemLinkF.Menu = barItemLink.Item.Name;
                        barItemLinkF.Application = "AD";
                        barItemLinkF.Description = barItemLink.Caption;
                        barItemLinkF.ParentMenu = pageGroup.Name;
                        baobao.tblFunctions.AddOrUpdate(barItemLinkF);
                    }
                }
                baobao.SaveChanges();
            }
        }
        private void LoadRights()
        {
            var dt = (from a in baobao.tblUserFunctions
                      join b in baobao.tblFunctions
                      on a.Menu equals b.Menu
                      where b.Application == "AD" && a.User == ClassUser.User
                      select a).ToList();
            foreach (RibbonPage page in ribbonControl1.Pages)
            {
                foreach (var items in dt)
                {
                    if (items.Menu == page.Name && items.Disable == true)
                    {
                        page.Visible = false;
                    }
                }
                foreach (RibbonPageGroup pageGroup in page.Groups)
                {
                    foreach (var items in dt)
                    {
                        if (items.Menu == pageGroup.Name && items.Disable == true)
                        {
                            pageGroup.Enabled = false;
                        }
                    }
                    foreach (BarItemLink barItemLink in pageGroup.ItemLinks)
                    {
                        foreach (var items in dt)
                        {
                            if (items.Menu == barItemLink.Item.Name && items.Disable == true)
                            {
                                barItemLink.Item.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        private void DocTenBoPhanTheoMa()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select Tenpb from Admin where  Taikhoan like N'{0}'", Login.Username);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                brDepartment.Caption = reader[0].ToString();
            }
            con.Close();
        }

       
        private void MainDev_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Bạn muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }



        private void donhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcDONDH));
        }


        private void taikhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmQuanLyQuyen));
        }
       
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcBaocaoxuatkho));
        }

      

        private void btndulieu_donggoi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenForm(typeof(UcXUATKHO11));
        }

     

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            OpenForm(typeof(UcXUATKHO11));
        }


        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)//ĐÓNG TAB
        {

            if (MessageBox.Show("Bạn muốn đóng", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DevExpress.XtraTab.XtraTabControl xtab = (DevExpress.XtraTab.XtraTabControl)sender;
                int i = xtab.SelectedTabPageIndex;
                xtab.TabPages.RemoveAt(xtab.SelectedTabPageIndex);// Đóng cho mở trở lại
            }
        }

       
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcTongKetXuatKho_Gia));
        }

        private void btnDM_DONHANG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UCDANHMUC_DONHANG));
        }

       
        private void btnKhokhuon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhuonMau khuonMau = new KhuonMau();
            khuonMau.ShowDialog();
        }

        private void barKehoach_VT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcKEHOACH_VATTU));
        }

        private void barLaymaDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

            OpenForm(typeof(UcLayMaDatHangVatTu));
        }


        private void btnLapPhieuMuaVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrintDatMuaVatTu DatmuaVatTu = new frmPrintDatMuaVatTu();
            DatmuaVatTu.Show();
        }

        private void barDANHMUC_SANPHAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcDM_SANPHAM));
        }

        private void btnKeHoach_Vattu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcDM_VATTUSUDUNG));
        }

        private void btnLapPhieuXK_giaoHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            OpenForm(typeof(UcLapPhieuXuatKho));
        }

        private void btnLapPhieuXuat_GH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcLapPhieuXuatKho));
        }


     
       
        private void btnDM_KHUON_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            OpenForm(typeof(UCDMKHUON));
        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            Login flogin = new Login();
            flogin.Show();
        }

        private void BtnnhapkhoVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcNHAPVATTU));
        }

        private void barXuatKhoVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcXUAT_VATTU));
        }

        private void barKehoachQAQC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcQA));
        }

        private void barQcQa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmQAControl fQA = new frmQAControl();
            fQA.ShowDialog();
        }

        private void btnTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmRPNhapXuatVatTu fBaoCaoKho = new frmRPNhapXuatVatTu();
            //fBaoCaoKho.ShowDialog();
            OpenForm(typeof(frmRPNhapXuatVatTu));
        }

        private void brTienDoVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrVatTu prVatTu = new frmPrVatTu();
            prVatTu.Show();
        }

        private void btnXuatNhapTonVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmRPNhapXuatVatTu fBaoCaoKho = new frmRPNhapXuatVatTu();
            fBaoCaoKho.Show();
        }

        private void btnTienDoVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrVatTu prVatTu = new frmPrVatTu();
            prVatTu.Show();
        }

        private void btnXuatNhapTon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmRPNhapXuatVatTu fBaoCaoKho = new frmRPNhapXuatVatTu();
            fBaoCaoKho.Show();
        }

        private void btnTienDoVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrVatTu prVatTu = new frmPrVatTu();
            prVatTu.Show();
        }


        private void btnThongKeDonHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            OpenForm(typeof(UCThongKeSanXuat));
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string pat = string.Format(@"{0}\{1}.exe", AppDomain.CurrentDomain.BaseDirectory, "update");
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
        }

        private void btnDM_TonvlPhu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDM_VATLIEUPHU DMVLP = new frmDM_VATLIEUPHU();
            DMVLP.ShowDialog();
        }

        private void btnNhapVLPhu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(NhapVatLieuPhu_UC));
        }

        private void btnXuatVLPhu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UCXuatCCDC));
        }
        private void BtnVatlieuphutonkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDM_VATLIEUPHU fm = new frmDM_VATLIEUPHU();
            fm.ShowDialog();
        }


        private void BtnTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDM_HangTon fHangTon = new frmDM_HangTon();
            fHangTon.ShowDialog();
        }

        private void BtnSPNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //OpenForm(typeof(UCNhapKho_TP));
        }

        private void BtnSPXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //OpenForm(typeof(UCXuatKho_TP));
        }


        private void barButtonItem137_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSlideShow SlideShow = new frmSlideShow();
            SlideShow.Show();
        }


        private void BtnDowload_Update_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\ftpUpLoad\Win7FTP.exe");
        }

        private void barButtonItem140_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
       
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Win7FTP.exe");
        }
        public static UcDinhMucCongDoan CongDoan;
        private void BtnCongSuat_DinhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }


        public static frmDM_CONGDOAN DMCongDoan;
        private void BtnCapnhat_CD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DMCongDoan == null || DMCongDoan.IsDisposed)
            {
                DMCongDoan = new frmDM_CONGDOAN();
                DMCongDoan.Show();
                frmDM_CONGDOAN.ActiveForm.TopMost = true;
            }
        }

        private void BtnHoTro_LapLich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTinh_nguon_luc_san_xuat tinh_Nguon_Luc_San_Xuat = new frmTinh_nguon_luc_san_xuat();
            tinh_Nguon_Luc_San_Xuat.ShowDialog();
        }


        private void btnSoNhatKyCD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UCNHATKYCONGVIEC));
        }

        public static frmGhiMaNguyenCong GhiMaNguyenCong;
        private void btnNguyenCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnCapNhatNguyenCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void btnGhiDoanhSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            OpenForm(typeof(UCBanHang));
        }

        private void btnThanhPhamTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDM_HangTon ThanhPhamTon = new frmDM_HangTon();
            ThanhPhamTon.Show();
        }

        private void btnLichSanXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        public static frmCVKhuon cVKhuon;
        private void btnCVKhuon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cVKhuon == null || cVKhuon.IsDisposed)
            {
                cVKhuon = new frmCVKhuon();
                cVKhuon.Show();
            }
        }

        private void btnDonHangTre_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDonHangChuaGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBCHangTre bCHangTre = new frmBCHangTre();
            bCHangTre.Show();
        }

        private void btnKPPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhacPhucPhongNgua khacPhucPhongNgua = new frmKhacPhucPhongNgua();
            khacPhucPhongNgua.Show();
        }

        private void btnKPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnWebPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://erp.baobao.vn/");
        }

        private void btnLapDinhMucVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCapNhatDinhMucVatLieu dinhMucVatLieu = new frmCapNhatDinhMucVatLieu();
            dinhMucVatLieu.ShowDialog();
        }

        private void btnDinhMucDonHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDinhMucVLChoDonHang dinhMucDonHang = new frmDinhMucVLChoDonHang();
            dinhMucDonHang.Show();
        }

        private void btnDinhMucXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDinhMucXuatKho dinhMucXuatKho = new frmDinhMucXuatKho();
            dinhMucXuatKho.Show();
        }

        private void btnHuong_Dan_Su_Dung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start(@"\\192.168.1.22\iso_giamsat\TAI LIEU ISO 2018");
        }

        private void btnKiemTraHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSoKiemTraHangHoa soKiemTraHangHoa = new frmSoKiemTraHangHoa();
            soKiemTraHangHoa.Show();
        }

        private void btnQuanLyGiaCongNgoai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmquan_ly_gia_cong quanLyGiaCong = new frmquan_ly_gia_cong();
            quanLyGiaCong.Show();
        }

        private void btnThuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTinh_DinhMuc_Thuong tinh_DinhMuc_Thuong = new frmTinh_DinhMuc_Thuong();
            tinh_DinhMuc_Thuong.Show();
        }

        private void barButtonItem142_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem145_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnQuanLyLuongKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            OpenForm(typeof(TinhLuongKhoan_uc));
        }

        private void btnQLDinhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(frmDM_CONGDOAN));
        }

       

        private void btnLayBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTinh_nguon_luc_san_xuat tinh_Nguon_Luc_San_Xuat = new frmTinh_nguon_luc_san_xuat();
            tinh_Nguon_Luc_San_Xuat.Show();
        }

        private void btnQuanLyKhuon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //KhuonMau khuonMau = new KhuonMau();
            //khuonMau.ShowDialog();
            OpenForm(typeof(KhuonMau));
        }

        private void btnDonHangConLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDonHangConLai donHangConLai = new frmDonHangConLai();
            donHangConLai.ShowDialog();
        }

        private void btnNguyenCongSanXuatChiTietSanPham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(ApDinhMucChiTietSanPham_UC));
        }

        private void btnBaoCaoNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBaoCaoSanXuatMoiNgay baoCaoSanXuatMoiNgay = new frmBaoCaoSanXuatMoiNgay();
            baoCaoSanXuatMoiNgay.Show();
        }

      

        private void btnTrienKhaiKeHoachSanXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(TrienKhaiKeHoachSanXuat_UC));
        }

        private void btnNhanKeHoach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {}

        private void btnKhungThoiGian_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhungThoiGianLamViec khungThoiGianLamViec = new frmKhungThoiGianLamViec();
            khungThoiGianLamViec.ShowDialog();
        }

        private void barButtonItem151_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(TrienKhaiKeHoachBaoCaoGiaoNhanUC));
        }

        private void btnTreeList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(ucproductionTreeList));
        }

        private void btnPhanCumCongDoanSanXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(ucproductionTreeList));
        }

        private void btnDinhMucVatLieuDonVi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {   
            OpenForm(typeof(frmCapNhatDinhMucVatLieu));
        }


        private void btnDangKyCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(ucChamCongDangKyCa));
        }

        private void bbtnKhoVatTuChiNhanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UCNhapKhoVatTuChiNhanh));
        }

        private void btnTonKhoChiNhanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnQuanLyBienBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UserControlBienBan));
        }

        private void btnTHDinhMucCongDoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UcDinhMucCongDoan));
        }

        private void barButtonItem155_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhuCauNhanCong nhanCong = new frmNhuCauNhanCong();
            nhanCong.ShowDialog();
        }

        private void btnNhuCauMay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(CongSuatMayUserControl));
        }
        private void barBaoCaoSanXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(UserControlThongKeSanXuat));
        }

        private void btnBaoCaoBanHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(UCDANHMUC_DONHANG));
        }

        private void btnEditPassUserName_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUserNameEditPass passEdit = new frmUserNameEditPass();
            passEdit.ShowDialog();
        }

        private void barQLUserName_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OpenForm(typeof(frmQLTaiKhoan));
        }

        private void btnNhapKhoHangTon_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmHangTonNhap));
        }

        private void btnXuatKhoHangTon_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmHangTonXuat));
        }

        private void btnthem_dmsanpham_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmCapNhatDanhMucSanPham));
        }

        private void barButtonItemDanhMucMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(CongSuatMayUserControl));
        }

        private void barButtonItemDinhMucCongDoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(UcDinhMucCongDoan));
        }

        private void btnQLKhoKhuon_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void btnCapNhatBOMSanPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(TreeListBOM));
        }

        private void barXuatKhoVatTu_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(UcXUAT_VATTU));
        }

        private void bBKiemkhoVT_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmDinhMucVatTu));
        }

        private void barQHKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmKhachHangSoGiaoDich));
        }

        private void barBaoCaoTuan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmBaoCaoTuan));
        }

        private void btnChiDao_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmBaoCaoTuanChiDao));
        }

        private void barButtonItemKiemTraSanPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmKiemTraSanPham));
        }

        private void barKetQuaKiemTraSanPham_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmKetQuaKiemTraSanPham));
        }
    }
}