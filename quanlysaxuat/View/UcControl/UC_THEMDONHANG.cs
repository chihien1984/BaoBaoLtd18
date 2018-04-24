using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

using DevExpress.XtraPrinting.BarCode;
using quanlysanxuat.View;

namespace quanlysanxuat
{
    public partial class UC_THEMDONHANG : UserControl
    {
        Clsketnoi knn = new Clsketnoi();

        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        Clsketnoi connect = new Clsketnoi();
        public UC_THEMDONHANG()
        {
            InitializeComponent();
        }
        private string username;
        private void MaNV()//Lấy mã nhân viên
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Sothe from tblDSNHANVIEN where HoTen like N'" + txtuser.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMa_user.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void DocDonHangTheoNgay()//Load danh mục sản phẩm chi tiết
        {
            DateTime ketthuc = dpdenngay.Value.AddDays(1);
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT case when MaLapGhep is null then 'LT' else MaLapGhep end MaLapGhep,
				DHCT.Thoigian_Thuc,
                DHCT.Code,DHCT.Iden,
				case when SPCT.Mact is null then DHCT.MaSP else SPCT.Mact end Mact,
				case when SPCT.Ten_ct is null then Tenquicach else SPCT.Ten_ct end Ten_ct,
                DHCT.pheduyet,SPCT.Soluong_CT,SPCT.Chatlieu_chitiet,
                Tenquicach,DHCT.madh,DHCT.Khachhang,
                case when DH.MaPO ='' or DH.MaPO is null then DHCT.MaPo else DH.MaPO end MaPo,
                DHCT.MaSP,dvt, 
                Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, 
                ghichu, DHCT.nguoithaydoi, DHCT.thoigianthaydoi, case when IdPSX >0 then 'x' end DaChia,
                DHCT.MaKH, DH.LoaiDH, DH.PhanloaiKH,DH.Diengiai 
                FROM dbo.tblDHCT DHCT left outer join tblSANPHAM_CT SPCT
                ON DHCT.MaSP = SPCT.Masp 
                join tblDONHANG DH on DH.madh = DHCT.madh 
				left outer join 
				(select IdPSX from tblchitietkehoach where IdPSX
				is not null group by IdPSX)c
				on Iden=c.IdPSX
                where  cast(DHCT.thoigianthaydoi as date)
                between '{0}' and '{1}' order by madh DESC,Iden DESC",
                dptungay.Value.ToString("MM-dd-yyyy"),
                dpdenngay.Value.ToString("MM-dd-yyyy"));
            gridControl2.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

       
        private void Load_SPTRIENKHAI()//load danh mục sản phẩm sản xuất
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select MaLapGhep,Trangthai,Donvisp,IDSP,
                        CT.IdPSX,CT.nvkd,CT.SPLR,CT.SLSPLR,ngaytrienkhai,CT.madh,CT.LoaiDH,mabv,sanpham,Maubv, 
                      Mact, Ten_ct, So_CT, ChatlieuCT, soluongyc, tonkho, soluongsx, 
                      ngoaiquang, donvi, daystar, dayend, MaKH, CT.khachhang, xeploai, 
                    Ghichu, CT.MaPo, TrangThai, CT.Diengiai,CT.NguoiTao,CT.NgayTao 
                    from tblchitietkehoach CT left join tblDONHANG DH on CT.madh = DH.madh  where CT.ngaytrienkhai 
                      between '{0}' and '{1}' order by IDSP DESC",
                      dptungay.Value.ToString("yyyy/MM/dd"),
                      dpdenngay.Value.ToString("yyyy/MM/dd"));
            gridControl3.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        //Đọc đơn hàng đã phân rã triển khai
        private void LOAD_DHTK()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select MaLapGhep,Trangthai,Donvisp,IDSP,
                        IdPSX,nvkd,ngaytrienkhai,SPLR,SLSPLR,madh,LoaiDH,mabv,sanpham,Maubv, 
                      Mact, Ten_ct, So_CT, ChatlieuCT, soluongyc, tonkho, soluongsx, 
                      ngoaiquang, donvi, daystar, dayend, MaKH, khachhang, xeploai, Ghichu, MaPo, 
                    TrangThai, Diengiai,NguoiTao,NgayTao from 
                      tblchitietkehoach where convert(Date, ngaytrienkhai, 103) 
                    between '{0}' and '{1}'  and madh like N'{2}' order by IDSP DESC",
                      dptungay.Value.ToString("yyyy-MM-dd"),
                      dpdenngay.Value.ToString("yyyy/MM/dd"),
                      cbMaDH.Text);
            gridControl3.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void btnDMDHSX_Click(object sender, EventArgs e)
        {
            Load_SPTRIENKHAI();
        }

        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            DocDonHangTheoNgay();
        }
        private void TruTonkho()
        {
            double SOLUONG_DH = double.Parse(txtSLDH.Text);
            double SOLUONG_TONKHO = double.Parse(txtTONKHO.Text);
            double SLSANXUAT = SOLUONG_DH - SOLUONG_TONKHO;
            txtSLSP_SX.Text = Convert.ToString(SLSANXUAT);
        }
        private void TichSLCT_SX()
        {
            double SOCTSP = double.Parse(txtSoChiTiet.Text);
            double SL_SPSX = double.Parse(txtSLSP_SX.Text);
            double SLCT_SX = SL_SPSX * SOCTSP;
            txtSLCT_SX.Text = Convert.ToString(SLCT_SX);
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMauBV.Text == "")
                {
                    MessageBox.Show("Không có mã sản phẩm vui lòng cung cấp mã", "Thông báo");
                    return;
                }
                else if (cbMaDH.Text == "")
                {
                    MessageBox.Show("Không có mã đơn hàng", "Thông báo");
                    return;
                }
                else if (txtMaLapGhep.Text == "")
                {
                    MessageBox.Show("Mã lắp ghép không được để trống", "Thông báo");
                    return;
                }
                {
                    decimal SOLUONG_DH = Convert.ToDecimal(txtSLDH.Text);
                    decimal SOLUONG_TONKHO = Convert.ToDecimal(txtTONKHO.Text);
                    Decimal SOLUONG_SANXUAT = Convert.ToDecimal(txtSLCT_SX.Text);
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblchitietkehoach "
                    + " (Donvisp,CodePSX,IdPSX,SPLR,SLSPLR,nvkd,NguoiTao,NgayTao,ngaytrienkhai,madh,LoaiDH,mabv,sanpham,Maubv, "
                    + " Mact, Ten_ct,So_CT,ChatlieuCT, soluongyc, tonkho, soluongsx, "
                    + " ngoaiquang, donvi, daystar, dayend,MaKH,khachhang, xeploai,Ghichu,MaPo,TrangThai,Diengiai,MaLapGhep) "
                    + " values(@Donvisp,@CodePSX,@IdPSX,@SPLR,@SLSPLR,@nvkd,@NguoiTao,GetDate(),GetDate(),@madh,@LoaiDH,@mabv,@sanpham,@Maubv, "
                    + " @Mact, @Ten_ct,@So_CT,@ChatlieuCT,@soluongyc,@tonkho,@soluongsx, "
                    + " @ngoaiquang,@donvi,GetDate(),@dayend,@MaKH,@khachhang,@xeploai,@Ghichu,@MaPo,@TrangThai,@Diengiai,@MaLapGhep)", con);
                    cmd.Parameters.Add("@Donvisp", SqlDbType.NVarChar).Value = txtDonviSP.Text;
                    cmd.Parameters.Add("@CodePSX", SqlDbType.Int).Value = txtCodePSX.Text;
                    cmd.Parameters.Add("@IdPSX", SqlDbType.Int).Value = txtIDDH.Text;
                    cmd.Parameters.Add("@SPLR", SqlDbType.NVarChar).Value = txtTenspLaprap.Text;
                    cmd.Parameters.Add("@SLSPLR", SqlDbType.Int).Value = txtSLSP_SX.Text;
                    cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = txtNVKD.Text;
                    cmd.Parameters.Add("@NguoiTao", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbMaDH.Text;
                    cmd.Parameters.Add("@LoaiDH", SqlDbType.NVarChar).Value = txtLoaiDH.Text;
                    cmd.Parameters.Add("@mabv", SqlDbType.NVarChar).Value = txtmasp.Text;
                    cmd.Parameters.Add("@sanpham", SqlDbType.NVarChar).Value = txttenCTSP.Text;
                    cmd.Parameters.Add("@Maubv", SqlDbType.NVarChar).Value = txtMauBV.Text;
                    cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
                    cmd.Parameters.Add("@Ten_ct", SqlDbType.NVarChar).Value = txtTen_CT.Text;
                    cmd.Parameters.Add("@So_CT", SqlDbType.Int).Value = txtSoChiTiet.Text;
                    cmd.Parameters.Add("@ChatlieuCT", SqlDbType.NVarChar).Value = txtChatlieu_CT.Text;
                    cmd.Parameters.Add("@soluongyc", SqlDbType.Int).Value = SOLUONG_DH;
                    cmd.Parameters.Add("@tonkho", SqlDbType.Int).Value = SOLUONG_TONKHO;
                    cmd.Parameters.Add("@soluongsx", SqlDbType.Int).Value = SOLUONG_SANXUAT;
                    cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtNgoaiQuan.Text;
                    cmd.Parameters.Add("@donvi", SqlDbType.NVarChar).Value = txtDonviCT.Text;
                    //cmd.Parameters.Add("@daystar", SqlDbType.Date).Value = dpNgaybatdau.Text;
                    cmd.Parameters.Add("@dayend", SqlDbType.Date).Value = dpngayketthuc.Text;
                    cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar).Value = txtMaKH.Text;
                    cmd.Parameters.Add("@khachhang", SqlDbType.NVarChar).Value = txtTenKhachhang.Text;
                    cmd.Parameters.Add("@xeploai", SqlDbType.NVarChar).Value = txtLoaiKH.Text;
                    cmd.Parameters.Add("@Ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                    cmd.Parameters.Add("@MaPo", SqlDbType.NVarChar).Value = txtMaPo.Text;
                    cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = txt_GhiSX.Text;
                    cmd.Parameters.Add("@Diengiai", SqlDbType.NVarChar).Value = txtdiengiaiDH.Text;
                    cmd.Parameters.Add("@MaLapGhep", SqlDbType.NVarChar).Value = txtMaLapGhep.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl3.DataSource = dt;
                    LOAD_DHTK();
                    DocDonHangTheoMaDonHang();
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông báo");
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNVKD.Text == "" && cbMaDH.Text == "")
                {
                    MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    decimal SOLUONG_DH = Convert.ToDecimal(txtSLDH.Text);
                    decimal SOLUONG_TONKHO = Convert.ToDecimal(txtTONKHO.Text);
                    Decimal SOLUONG_SANXUAT = Convert.ToDecimal(txtSLCT_SX.Text);
                    con.ConnectionString = GetMConnect();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("update tblchitietkehoach "
                            + " set Donvisp=@Donvisp,CodePSX=@CodePSX,IdPSX=@IdPSX,SPLR=@SPLR,SLSPLR=@SLSPLR,nvkd=@nvkd,ngaytrienkhai=GetDate(),madh=@madh,LoaiDH=@LoaiDH,mabv=@mabv,sanpham=@sanpham,Maubv=@Maubv, "
                            + " Mact=@Mact, Ten_ct=@Ten_ct,So_CT=@So_CT,ChatlieuCT=@ChatlieuCT, soluongyc=@soluongyc, tonkho=@tonkho, soluongsx=@soluongsx, "
                            + " ngoaiquang=@ngoaiquang, donvi=@donvi, daystar=@daystar, "
                            + " dayend=@dayend,MaKH=@MaKH, khachhang=@khachhang, xeploai=@xeploai,Ghichu=@Ghichu,MaPo=@MaPo,Diengiai=@Diengiai,NguoiTao=@NguoiTao,NgayTao=GetDate(),MaLapGhep=@MaLapGhep where IDSP like  '" + txtCode.Text + "'", con))
                        {
                            cmd.Parameters.Add("@Donvisp", SqlDbType.NVarChar).Value = txtDonviSP.Text;
                            cmd.Parameters.Add("@CodePSX", SqlDbType.NVarChar).Value = txtCodePSX.Text;
                            cmd.Parameters.Add("@IdPSX", SqlDbType.NVarChar).Value = txtIDDH.Text;
                            cmd.Parameters.Add("@SPLR", SqlDbType.NVarChar).Value = txtTenspLaprap.Text;
                            cmd.Parameters.Add("@SLSPLR", SqlDbType.Int).Value = txtSLSP_SX.Text;
                            cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = txtNVKD.Text;
                            cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbMaDH.Text;
                            cmd.Parameters.Add("@LoaiDH", SqlDbType.NVarChar).Value = txtLoaiDH.Text;
                            cmd.Parameters.Add("@mabv", SqlDbType.NVarChar).Value = txtmasp.Text;
                            cmd.Parameters.Add("@sanpham", SqlDbType.NVarChar).Value = txttenCTSP.Text;
                            cmd.Parameters.Add("@Maubv", SqlDbType.NVarChar).Value = txtMauBV.Text;
                            cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
                            cmd.Parameters.Add("@Ten_ct", SqlDbType.NVarChar).Value = txtTen_CT.Text;
                            cmd.Parameters.Add("@So_CT", SqlDbType.Int).Value = txtSoChiTiet.Text;
                            cmd.Parameters.Add("@ChatlieuCT", SqlDbType.NVarChar).Value = txtChatlieu_CT.Text;
                            cmd.Parameters.Add("@soluongyc", SqlDbType.Int).Value = SOLUONG_DH;
                            cmd.Parameters.Add("@tonkho", SqlDbType.Int).Value = SOLUONG_TONKHO;
                            cmd.Parameters.Add("@soluongsx", SqlDbType.Int).Value = SOLUONG_SANXUAT;
                            cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtNgoaiQuan.Text;
                            cmd.Parameters.Add("@donvi", SqlDbType.NVarChar).Value = txtDonviCT.Text;
                            cmd.Parameters.Add("@daystar", SqlDbType.Date).Value = dpNgaybatdau.Text;
                            cmd.Parameters.Add("@dayend", SqlDbType.Date).Value = dpngayketthuc.Text;
                            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar).Value = txtMaKH.Text;
                            cmd.Parameters.Add("@khachhang", SqlDbType.NVarChar).Value = txtTenKhachhang.Text;
                            cmd.Parameters.Add("@xeploai", SqlDbType.NVarChar).Value = txtLoaiKH.Text;
                            cmd.Parameters.Add("@Ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                            cmd.Parameters.Add("@MaPo", SqlDbType.NVarChar).Value = txtMaPo.Text;
                            cmd.Parameters.Add("@Diengiai", SqlDbType.NVarChar).Value = CBLoaiSP.Text;
                            cmd.Parameters.Add("@NguoiTao", SqlDbType.NVarChar).Value = username;
                            cmd.Parameters.Add("@MaLapGhep", SqlDbType.NVarChar).Value = txtMaLapGhep.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close(); Load_SPTRIENKHAI();
                        DocDonHangTheoMaDonHang();
                    }
                }
            }
            catch { MessageBox.Show("Không thành công"); }
        }

        private static string GetMConnect()
        {
            return Connect.mConnect;
        }
        public static string donHangID;
        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtDonviSP.Text = gridView2.GetFocusedRowCellDisplayText(Donvi_grid2);
            txtCodePSX.Text = gridView2.GetFocusedRowCellDisplayText(CodePSX_grid2);
            txtIDDH.Text = gridView2.GetFocusedRowCellDisplayText(IdPSX_grid2);
            cbMaDH.Text = gridView2.GetFocusedRowCellDisplayText(madh_grid2);
            txtTenKhachhang.Text = gridView2.GetFocusedRowCellDisplayText(Khachhang_grid2);
            txtMaPo.Text = gridView2.GetFocusedRowCellDisplayText(MaPo_grid2);
            txtmasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            txtTenspLaprap.Text = gridView2.GetFocusedRowCellDisplayText(Tensp_grid2);
            txttenCTSP.Text = gridView2.GetFocusedRowCellDisplayText(Tenct_grid2);
            txtDonviCT.Text = gridView2.GetFocusedRowCellDisplayText(Donvi_grid2);
            txtSLDH.Text = gridView2.GetFocusedRowCellDisplayText(SoluongDH_grid2);
            txtTONKHO.Text = gridView2.GetFocusedRowCellDisplayText(Tonkho_grid2);
            txtMauBV.Text = gridView2.GetFocusedRowCellDisplayText(Maubv_grid2);
            dpngayketthuc.Text = gridView2.GetFocusedRowCellDisplayText(ngaygiao_grid2);
            txtNgoaiQuan.Text = gridView2.GetFocusedRowCellDisplayText(Ngoaiquan_grid2);
            txtGhiChu.Text = gridView2.GetFocusedRowCellDisplayText(Ghichu_grid2);
            txtNVKD.Text = gridView2.GetFocusedRowCellDisplayText(nvkd_grid2);
            dpNgayLap_DH.Text = gridView2.GetFocusedRowCellDisplayText(Ngaylap_grid2);
            txtMaKH.Text = gridView2.GetFocusedRowCellDisplayText(Makh_grid2);
            txtMasp_CT.Text = gridView2.GetFocusedRowCellDisplayText(Mact_grid2);
            txtTen_CT.Text = gridView2.GetFocusedRowCellDisplayText(Tenct_grid2);
            txtSoChiTiet.Text = gridView2.GetFocusedRowCellDisplayText(Soluongct_grid2);
            txtChatlieu_CT.Text = gridView2.GetFocusedRowCellDisplayText(Chatlieuct_grid2);
            txtLoaiDH.Text = gridView2.GetFocusedRowCellDisplayText(loaiDH_grid2);
            txtLoaiKH.Text = gridView2.GetFocusedRowCellDisplayText(LoaiKH_grid2);
            txtdiengiaiDH.Text = gridView2.GetFocusedRowCellDisplayText(diengiai_grid2);
            txtMaLapGhep.Text = gridView2.GetFocusedRowCellDisplayText(malapghep_grid2);
            TruTonkho();
            TichSLCT_SX();
            LOAD_DHTK();
            QuiTrinhSanXuat();
            ChonChiTietSanPham();
            donHangID = txtIDDH.Text;
        }
        public void DocLichSanXuatQuaCacBoPhan()
        {
            frmLichSanXuatHangDenCacBoPhan lich =
                new frmLichSanXuatHangDenCacBoPhan();
            lich.ShowDialog();
        }
        #region nếu chi tiết sản phẩm null 
        private void ChonChiTietSanPham()
        {
            if (txttenCTSP.Text == "" || txtSoChiTiet.Text == "0")
            {
                txttenCTSP.Text = txtTenspLaprap.Text;
                txtSoChiTiet.Text = "1";
            }
            if (txtMasp_CT.Text == "")
            {
                txtMasp_CT.Text = txtmasp.Text;
            }
        }
        #endregion


        private void BINDING_TRIENKHAI()
        {
            Gol = gridView3.GetFocusedDisplayText();
            txtCodePSX.Text = gridView3.GetFocusedRowCellDisplayText(IdPSX_grid3);
            txtIDDH.Text = gridView3.GetFocusedRowCellDisplayText(IdPSX_grid3);
            txtCode.Text = gridView3.GetFocusedRowCellDisplayText(Code_grid3);
            txtNVKD.Text = gridView3.GetFocusedRowCellDisplayText(nvkd_grid3);
            dpngayghi.Text = gridView3.GetFocusedRowCellDisplayText(ngaylap_grid3);
            cbMaDH.Text = gridView3.GetFocusedRowCellDisplayText(Madh_grid3);
            txtmasp.Text = gridView3.GetFocusedRowCellDisplayText(Masp_grid3);
            txtTenspLaprap.Text = gridView3.GetFocusedRowCellDisplayText(tensanpham_grid3);
            txttenCTSP.Text = gridView3.GetFocusedRowCellDisplayText(tenCTsp_grid3);
            txtTen_CT.Text = gridView3.GetFocusedRowCellDisplayText(tenCTsp_grid3);
            txtMasp_CT.Text = gridView3.GetFocusedRowCellDisplayText(Machitiet_grid3);
            txtSoChiTiet.Text = gridView3.GetFocusedRowCellDisplayText(soluongct_grid3);
            txtChatlieu_CT.Text = gridView3.GetFocusedRowCellDisplayText(chatlieuCT_grid3);
            txtSLDH.Text = gridView3.GetFocusedRowCellDisplayText(soluongDH_grid3);
            txtTONKHO.Text = gridView3.GetFocusedRowCellDisplayText(tonkho_grid3);
            txtSLCT_SX.Text = gridView3.GetFocusedRowCellDisplayText(soluongsanxuat_grid3);
            txtNgoaiQuan.Text = gridView3.GetFocusedRowCellDisplayText(ngoaiquan_grid3);
            txtDonviCT.Text = gridView3.GetFocusedRowCellDisplayText(donvi_grid3);
            dpNgaybatdau.Text = gridView3.GetFocusedRowCellDisplayText(ngaybatdau_grid3);
            dpngayketthuc.Text = gridView3.GetFocusedRowCellDisplayText(ngayketthuc_grid3);
            txtMaKH.Text = gridView3.GetFocusedRowCellDisplayText(makhachhang_grid3);
            txtMauBV.Text = gridView3.GetFocusedRowCellDisplayText(MauBV_grid3);
            txtMaPo.Text = gridView3.GetFocusedRowCellDisplayText(MaPo_grid3);
            txtLoaiDH.Text = gridView3.GetFocusedRowCellDisplayText(LoaiDH_grid3);
            txtTenKhachhang.Text = gridView3.GetFocusedRowCellDisplayText(tenkhachhang_grid3);
            txtLoaiKH.Text = gridView3.GetFocusedRowCellDisplayText(xeploai_grid3);
            txtGhiChu.Text = gridView3.GetFocusedRowCellDisplayText(ghichu_grid3);
            txtdiengiaiDH.Text = gridView3.GetFocusedRowCellDisplayText(diengiai_grid1);
            txtDonviSP.Text = gridView3.GetFocusedRowCellDisplayText(donviSP_grid3);
            txt_GhiSX.Text = gridView3.GetFocusedRowCellDisplayText(trangthai_grid3);
            txtMaLapGhep.Text = gridView3.GetFocusedRowCellDisplayText(malapghep_grd3);
            QuiTrinhSanXuat();
        }
        private void gridControl3_Click(object sender, EventArgs e)
        {
            BINDING_TRIENKHAI();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {

        }
        private bool kiemtratontai01()// check ton tai T01
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl01", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai02()// check ton tai T02
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl02", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai03()// check ton tai T03
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl03", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai04()// check ton tai T04
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl04", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai05()// check ton tai T05
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl05", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai06()// check ton tai T06
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl06", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai07()// check ton tai T07
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl07", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai08()// check ton tai T08
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl08", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai09()// check ton tai T09
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl09", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai10()// check ton tai T10
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl10", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai11()// check ton tai T11
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl11", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai12()// check ton tai T12
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl12", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai13()// check ton tai T13
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl13", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai14()// check ton tai T14
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl14", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai15()// check ton tai T15
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl15", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai16()// check ton tai T16
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl16", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai17()// check ton tai T17
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl17", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        private bool kiemtratontai18()// check ton tai T18
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl18", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }

        private bool kiemtratontai19()// check ton tai T19
        {
            bool tatkt = false;
            string idsp = txtCode.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP from tbl19", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (idsp == Convert.ToString(dr[0]))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (kiemtratontai01()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 1", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai02()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 3", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai03()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 4", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai04()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 5", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai05()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 6", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai06()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 7", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai07()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 8", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai08()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 9", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai09()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 10", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai10()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 11", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai11()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 12", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai12()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 13", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai13()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 14", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai14()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 15", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai15()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 16", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai16()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 17", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai17()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 18", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai18()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 19", "KHÔNG THỂ XÓA"); return; }
            else if (kiemtratontai19()) { MessageBox.Show("Đơn hàng này đã được sản xuất tại tổ 19", "KHÔNG THỂ XÓA"); return; }
            else
            {
                ketnoi connect = new ketnoi();
                gridControl3.DataSource = connect.laybang("delete from tblchitietkehoach where IDSP like N'" + txtCode.Text + "'");
                connect.dongketnoi();
                Load_SPTRIENKHAI();
                DocDonHangTheoMaDonHang();
            }
        }

        private void txtSLDH_TextChanged(object sender, EventArgs e)
        {
            if (txtSLDH.Text == "")
            {
                txtSLDH.Text = "0";
            }
            txtSLDH.Text = string.Format("{0:0,0}", decimal.Parse(txtSLDH.Text));
            txtSLDH.SelectionStart = txtSLDH.Text.Length;
        }

        private void txtTONKHO_TextChanged(object sender, EventArgs e)
        {
            if (txtTONKHO.Text == "")
            {
                txtTONKHO.Text = "0";
            }
            txtTONKHO.Text = string.Format("{0:0,0}", decimal.Parse(txtTONKHO.Text));
            txtTONKHO.SelectionStart = txtTONKHO.Text.Length; TruTonkho(); TichSLCT_SX();
        }

        private void txtSLSX_TextChanged(object sender, EventArgs e)
        {
            if (txtSLCT_SX.Text == "")
            {
                txtSLCT_SX.Text = "0";
            }
            txtSLCT_SX.Text = string.Format("{0:0,0}", decimal.Parse(txtSLCT_SX.Text));
            txtSLCT_SX.SelectionStart = txtSLCT_SX.Text.Length;
        }

        private void txtSoChiTiet_TextChanged(object sender, EventArgs e)
        {
            if (txtSoChiTiet.Text == "")
            {
                txtSoChiTiet.Text = "0";
            }
            TruTonkho(); TichSLCT_SX();
        }

        private void cbMaDH_SX_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmXacNhanVatTu.madh = cbMaDH.Text;
        }
        private void DocDonHangTheoMaDonHang()
        {
            DateTime ketthuc = dpdenngay.Value.AddDays(1);
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT case when MaLapGhep is null then 'LT' else MaLapGhep end MaLapGhep,
				DHCT.Thoigian_Thuc,
                DHCT.Code,DHCT.Iden,
				case when SPCT.Mact is null then DHCT.MaSP else SPCT.Mact end Mact,
				case when SPCT.Ten_ct is null then Tenquicach else SPCT.Ten_ct end Ten_ct,
                DHCT.pheduyet,SPCT.Soluong_CT,SPCT.Chatlieu_chitiet,
                Tenquicach,DHCT.madh,DHCT.Khachhang,
                case when DH.MaPO ='' or DH.MaPO is null then DHCT.MaPo else DH.MaPO end MaPo,
                DHCT.MaSP,dvt, 
                Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, 
                ghichu, DHCT.nguoithaydoi, DHCT.thoigianthaydoi, case when IdPSX >0 then 'x' end DaChia,
                DHCT.MaKH, DH.LoaiDH, DH.PhanloaiKH,DH.Diengiai 
                FROM dbo.tblDHCT DHCT left outer join tblSANPHAM_CT SPCT
                ON DHCT.MaSP = SPCT.Masp 
                join tblDONHANG DH on DH.madh = DHCT.madh 
				left outer join 
				(select IdPSX from tblchitietkehoach where IdPSX
				is not null group by IdPSX)c
				on Iden=c.IdPSX
                where DHCT.madh like N'{0}' order by madh DESC,Iden DESC",
                cbMaDH.Text);
            gridControl2.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }
        #region
        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #endregion
        private void cbMaDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            DocDonHangTheoMaDonHang();
        }

        private void btnUpCDSP_Click(object sender, EventArgs e)
        {
            txttenCTSP.Text = txtTenspLaprap.Text;
        }

        private void btnXacThuc_Click(object sender, EventArgs e)
        {
            frmXacNhanVatTu fXacNhanVatTu = new frmXacNhanVatTu();
            fXacNhanVatTu.ShowDialog();
        }
        private void QuiTrinhSanXuat()//Them QTSX vao muc ghi chu
        {
            if ((txtQTSanXuat.Text).Length > 2)
            {
                txtGhiChu.Text = txtGhiChu.Text + ";#CÓ QUY TRÌNH SẢN XUẤT";
            }
        }
        private void txtQTSanXuat_TextChanged(object sender, EventArgs e)
        {
            QuiTrinhSanXuat();
        }
        private void txtmasp_TextChanged(object sender, EventArgs e)//lookup mã quy trình theo mã sản phẩm
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select masoqt from tblQUYTRINH_SANXUAT where masp like N'" + txtmasp.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtQTSanXuat.Text = Convert.ToString(reader[0]);
                }
                else
                {
                    txtQTSanXuat.Text = "";
                }
                con.Close();
            }
            catch (Exception) { }
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
        private void Layout_QTSX()//Goi quy trinh san xuat
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_QTSX.Text, txtQuiTrinh.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("HIEN CHUA CO QUY TRINH NAY", "THONG BAO"); }
        }
        private void Layout_QuiTrinhSX(object sender, EventArgs e)//Gọi qui trình sản xuất
        {
            frmLoading f2 = new frmLoading(txtQuiTrinh.Text, txtPath_QTSX.Text);
            f2.Show();
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
                MessageBox.Show("SAN PHAM CHUA CO TRONG HE THONG");
            }
        }
        private void Layout_BanVe(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + cbMaDH.Text + "'");
            RpPhieusanxuat rpPHIEUSANXUAT = new RpPhieusanxuat();
            rpPHIEUSANXUAT.DataSource = dt;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDH.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            kn.dongketnoi();
        }
        private void ListDMDH()
        {
            ketnoi kn = new ketnoi();
            cbMaDH.DataSource = kn.laybang("select madh from tblDONHANG where cast(Ngaydh as date) between '" + dptungay.Value.ToString("yyyy/MM/dd") + "' and '" + dpdenngay.Value.ToString("yyyy/MM/dd") + "'");
            cbMaDH.DisplayMember = "madh";
            cbMaDH.ValueMember = "madh";
            kn.dongketnoi();
        }
        #region Formload sự kiện formload
        private void UC_THEMDONHANG_Load(object sender, EventArgs e)// Sự kiện formload
        {
            username = MainDev.username;
            frmXacNhanVatTu.madh = cbMaDH.Text;
            dptungay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpdenngay.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            ListDMDH();
            DocDonHangTheoMaDonHang();
            this.gridView2.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.gridView1.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.gridView3.Appearance.Row.Font = new Font("Times New Roman", 7f);
            //this.gridView5.Appearance.Row.Font = new Font("Times New Roman", 7f);
            AddMaLapRap();
        }
        private void AddMaLapRap()
        {
            txtMaLapGhep.DisplayMember = "Text";
            txtMaLapGhep.ValueMember = "Value";
            var items = new[] {
                new { Value = "CT", Text = "CT" },
                new { Value = "LC", Text = "LC" },
                new { Value = "LT", Text = "LT" }
            };
            txtMaLapGhep.DataSource = items;
        }
        private void txtMaLapGhep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiDH = txtMaLapGhep.Text;
            switch (loaiDH)
            {
                case "CT":
                    lbMaLap.Text = "Chi tiết";
                    break;
                case "LC":
                    lbMaLap.Text = "Lắp cụm";
                    break;
                case "LT":
                    lbMaLap.Text = "Lắp tổng";
                    break;
                default:
                    MessageBox.Show("Không hợp lệ", "Message");
                    break;
            }
        }

        #endregion
        private void btnPrintPhieuCD_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from tblDHCT where Iden like '" + txtIDDH.Text + "'");
            XRPhieuthuthapcd PhieuCD = new XRPhieuthuthapcd();
            PhieuCD.DataSource = dt;
            PhieuCD.DataMember = "Table";
            PhieuCD.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnVatTu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + cbMaDH.Text + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMaDH.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void gridControl3_KeyDown(object sender, KeyEventArgs e)
        {
            frmPrVatTu.madh = cbMaDH.Text;
            if (e.Control && e.KeyCode == Keys.A)
            {
                frmPrVatTu VatTu = new frmPrVatTu();
                VatTu.ShowDialog();
            }
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }

        private void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            ShowSave();
        }

        private void ShowSave()
        {
            btnGhi.Enabled = true;
            btnExport.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnExportsx.Enabled = false;
            btnExportPhieuSanXuat.Enabled = true;
        }

        private void ShowEdit()
        {
            btnxoa.Enabled = true;
            btnsua.Enabled = true;
            btnExportsx.Enabled = true;
            btnGhi.Enabled = false;
            btnExport.Enabled = false;
            btnExportPhieuSanXuat.Enabled = false;
        }

        private void gridControl3_MouseMove(object sender, MouseEventArgs e)
        {
            ShowEdit();
        }

        private void btnloadgrid1_Click(object sender, EventArgs e)
        {
            DocDSDonHang();
        }
        private void DocDSDonHang()
        {
            ketnoi Connect = new ketnoi();
            string sqlStr = string.Format(@"select Code,DH.Ngaydh,DH.madh,LoaiDH,nvkd,Khachhang,Diachi, MaPO, 
                                    PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, 
                                    thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri 
                                    from tblDHCt group by madh) CT on DH.madh = CT.madh 
                                    where cast(Ngaydh as Date) 
                                    between '{0}'
                                    and '{1}' order by DH.Ngaydh DESC",
                                    dptungay.Value.ToString("yyyy-MM-dd"),
                                    dpdenngay.Value.ToString("yyyy-MM-dd"));
            gridControl1.DataSource = Connect.laybang(sqlStr);
            Connect.dongketnoi();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gridView1.GetFocusedDisplayText();
            string maDonHang = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT case when MaLapGhep is null then 'LT' else MaLapGhep end MaLapGhep,
				DHCT.Thoigian_Thuc,
                DHCT.Code,DHCT.Iden,
				case when SPCT.Mact is null then DHCT.MaSP else SPCT.Mact end Mact,
				case when SPCT.Ten_ct is null then Tenquicach else SPCT.Ten_ct end Ten_ct,
                DHCT.pheduyet,SPCT.Soluong_CT,SPCT.Chatlieu_chitiet,
                Tenquicach,DHCT.madh,DHCT.Khachhang,
                case when DH.MaPO ='' or DH.MaPO is null then DHCT.MaPo else DH.MaPO end MaPo,
                DHCT.MaSP,dvt, 
                Mau_banve, Tonkho, Soluong, ngaygiao, ngoaiquang, 
                ghichu, DHCT.nguoithaydoi, DHCT.thoigianthaydoi, case when IdPSX >0 then 'x' end DaChia,
                DHCT.MaKH, DH.LoaiDH, DH.PhanloaiKH,DH.Diengiai 
                FROM dbo.tblDHCT DHCT left outer join tblSANPHAM_CT SPCT
                ON DHCT.MaSP = SPCT.Masp 
                join tblDONHANG DH on DH.madh = DHCT.madh 
				left outer join 
				(select IdPSX from tblchitietkehoach where IdPSX
				is not null group by IdPSX)c
				on Iden=c.IdPSX
                where DHCT.madh like N'{0}' 
                order by madh DESC,Iden DESC",
                     maDonHang);
            gridControl2.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        public XRBarCode CreateQRCodeBarCode(string BarCodeText)
        {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to QRCode.
            barCode.Symbology = new QRCodeGenerator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 150;

            // If the AutoModule property is set to false, uncomment the next line.
            barCode.AutoModule = true;
            // barcode.Module = 3;

            // Adjust the properties specific to the bar code type.
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.AlphaNumeric;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;

            return barCode;
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            DocLichSanXuatQuaCacBoPhan();
        }

        private void btnExportPhieuSanXuat_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            btnExportPhieuSanXuat.Enabled = true;
        }

        private void btnChiTietPhieuSanXuat_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"Select * from tblDHCT");
            gridControl4.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void btnExprotChiTietDonHang_Click(object sender, EventArgs e)
        {
            gridControl4.ShowPrintPreview();
        }

       
    }
}
