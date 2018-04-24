using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.XtraPrinting;

namespace quanlysanxuat
{
    public partial class UcLapPhieuXuatKho : DevExpress.XtraEditors.XtraForm
    {
        public UcLapPhieuXuatKho()
        {
            InitializeComponent();
        }
        private void trongluongthucxuat()
        {
            try
            {
                if (txtTONGTL_XUAT.Text != "0")
                {
                    decimal tongtrongluong = decimal.Parse(txtTONGTL_XUAT.Text);
                    decimal trongluongbaobi = decimal.Parse(txtTL_Bi.Text);
                    decimal trongluongthuc = tongtrongluong - trongluongbaobi;
                    txtTL_XUAT.Text = Convert.ToString(trongluongthuc);
                }
            }
            catch (Exception)
            { }
        }
        private void TheHienDanhSachDonHangTrienKhaiTatCaCongDoan()
        {
            ketnoi con = new ketnoi();
            string sqlStr = string.Format(@"select a.IDTrienKhai,b.GhichuXuatKho,
             BatDau daystar,KetThuc dayend,a.KhoGiaoHang Diachi_giaohang,b.PhanloaiKH,
             a.IDTrienKhai IDSP,IDChiTietDonHang IdPSX,a.MaPo,
             b.Masp_KH,b.Tenkhachhang,b.Soluong,b.nguoithaydoi,NgayLap ngaytrienkhai,
             MaDonHang madh,b.Mau_banve Maubv,MaSanPham mabv,TenSanPham SPLR,
             SoLuongDonHang SLSPLR,a.TenChiTiet sanpham,SoChiTiet So_CT,
             SoLuongChiTietDonHang soluongyc,a.TonKho,SoLuongYCSanXuat soluongsx,DonViChiTiet donvi,DonViSanPham Donvisp,
             NgoaiQuan ngoaiquang,a.GhiChu ghichu,b.LoaiDH,b.Khachhang,ID,ParentID,MaCongDoan,TenCongDoan,c.SoLuongGiao,c.NgayGiao
             from TrienKhaiKeHoachSanXuat a
			 left outer join
			 (select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)c
		     on a.IDTrienKhai=c.IDTrienKhai
             left outer join
             (select Iden,GhichuXuatKho,PhanloaiKH,Masp_KH,
			 Tenkhachhang,Soluong,nguoithaydoi,Mau_banve,LoaiDH,Khachhang from tblDHCT)b
             on a.IDChiTietDonHang=b.Iden
             where a.NgayLap between '{0}' and '{1}' 
             and MaCongDoan like 'GHA'
             order by NgayLap desc",
             dptu_ngay.Value.ToString("yyyy/MM/dd"),
             dpden_ngay.Value.ToString("yyyy/MM/dd"));
            grDonhangTrienKhai.DataSource = con.laybang(sqlStr);
            con.dongketnoi();
        }
        private void TheHienDanhSachDonHangTrienKhaiCongDoan()
        {
            ketnoi con = new ketnoi();
            string sqlStr = string.Format(@"select a.IDTrienKhai,b.GhichuXuatKho,
             BatDau daystar,KetThuc dayend,a.KhoGiaoHang Diachi_giaohang,b.PhanloaiKH,a.IDTrienKhai IDSP,IDChiTietDonHang IdPSX,a.MaPo,
             b.Masp_KH,b.Tenkhachhang,b.Soluong,b.nguoithaydoi,NgayLap ngaytrienkhai,
             MaDonHang madh,b.Mau_banve Maubv,MaSanPham mabv,TenSanPham SPLR,SoLuongDonHang SLSPLR,a.TenChiTiet sanpham,SoChiTiet So_CT,
             SoLuongChiTietDonHang soluongyc,a.TonKho,SoLuongYCSanXuat soluongsx,DonViChiTiet donvi,DonViSanPham Donvisp,
             NgoaiQuan ngoaiquang,a.GhiChu ghichu,b.LoaiDH,b.Khachhang,ID,ParentID,MaCongDoan,TenCongDoan,c.SoLuongGiao,c.NgayGiao
             from TrienKhaiKeHoachSanXuat a
			 left outer join
			 (select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)c
		     on a.IDTrienKhai=c.IDTrienKhai
             left outer join
             (select Iden,GhichuXuatKho,PhanloaiKH,Masp_KH,
			 Tenkhachhang,Soluong,nguoithaydoi,Mau_banve,LoaiDH,Khachhang from tblDHCT)b
             on a.IDChiTietDonHang=b.Iden
             where a.NgayLap between '{0}' and '{1}' 
             and MaCongDoan like 'GHA'
             order by NgayLap desc",
             dptu_ngay.Value.ToString("yyyy/MM/dd"),
             dpden_ngay.Value.ToString("yyyy/MM/dd"));
            grDonhangTrienKhai.DataSource = con.laybang(sqlStr);
            con.dongketnoi();
        }
        private void btnTraCuuTheoCongDoanXuat_Click(object sender, EventArgs e)
        {
            TheHienDanhSachDonHangTrienKhaiCongDoan();
        }
        private void TheHienDonHangXuatKhoTheoMaDonHang()
        {
            ketnoi Connect = new ketnoi();
                string sqlQuery = 
                string.Format(@"select * from tbl11 where cast(ngaynhan as Date)
                                              between '{0}' and '{1}' and MaGH <>''
                                              and MaGH like N'{2}'
                                              order by left(MaGH,6) ASC,SoChungTu_XK ASC", 
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"),
                txtMaPhieuXuatHang.Text);
            grSoXuatKho.DataSource = Connect.laybang(sqlQuery);
            gvSoXuatKho.ExpandAllGroups();
            Connect.dongketnoi();
        }
        private void ListXuatKhoAll()
        {
            ketnoi Connect = new ketnoi();
            string sqlQuery = string.Format(@"select KetQua QCPass,
            BaoCaoSo,* from tbl11 T left outer join 
            tblKiemTraHangHoa H
            on T.Num=H.DonHangID 
            where convert(Date,ngaynhan,103)
            between '{0}' and '{1}' order by left(MaGH,6) DESC,SoChungTu_XK ASC,Num DESC",
            dptu_ngay.Value.ToString("yyyy-MM-dd"),
            dpden_ngay.Value.ToString("yyyy-MM-dd"));
            grSoXuatKho.DataSource = Connect.laybang(sqlQuery);
            gvSoXuatKho.ExpandAllGroups();
            Connect.dongketnoi();
        }
        private void LAY_MACODE_XUATKHO()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            SqlCommand cmd = new SqlCommand();
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select REPLACE(convert(nvarchar,GetDate(),11),'/','')+CONVERT(nvarchar,DatePart(HH,GETDATE()))+CONVERT(nvarchar,DatePart(MINUTE,GETDATE())) as PXK", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuXuatHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void CapNhat_TongHopBaoBi()//Cap nhat bao bi xuat kho
        {
            ketnoi Connect = new ketnoi();
            var kq =
                Connect.xulydulieu("update tbl11 set TongCongBaoBi= N'" + txtTong_BaoBi.Text + "' where MaGH like N'" + txtMaPhieuXuatHang.Text + "' ");
            Connect.dongketnoi();
        }
        private void CapNhat_GhichuXuat()//Cập nhật ghi chú xuất kho
        {
            ketnoi kn = new ketnoi();
            var kq = kn.xulydulieu("update tbl11 set Ketqua_GD = N'" + txtGhichuXuatKho.Text + "' where MaGH like N'" + txtMaPhieuXuatHang.Text + "' ");
            kn.dongketnoi();
        }
        private void Huy_XuatKho()//Huy giao hang xuat kho
        {
            ketnoi kn = new ketnoi();
            var kq = kn.xulydulieu("delete tbl11 where Num like '" + txtNum.Text + "' ");
            kn.dongketnoi();
        }
    
        private void GhiDL_XuatKho()
        {
            if (txtTenHang_KH.Text != "" && txtMaPhieuXuatHang.Text != "")
            {
                SqlConnection cn = new SqlConnection();
                decimal Soluongsanxuat = Convert.ToDecimal(txtSLSP_DH.Text);
                //decimal SoLuongHoanThanh = Convert.ToDecimal(SOLUONGTT.Text);
                //decimal SLSP_HT = Convert.ToDecimal(txtSoluong_SPHT.Text);
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tbl11(IdPSX,IDSP,So_CT,Maubv,LoaiDH,MaPo,ngaynhan,nvkd,madh,sanpham,"
                + " chitietsanpham,cdthanhpham,soluongsx,ngoaiquang,mabv,donvi,daystar,dayend,BTPT11,"
                + " TRONGLUONG11,khachhang,ghichu,Donvisp,SoluongSP, "
                + " MaGH,SL_LR,SL_DH,SL_TONKHO,SL_SANXUAT,xeploai, "
                + " TongSL_Xuat, TongTL_Xuat, SL_Bi, TL_Bi, Loai_Bi, Diengiai, "
                + " MaSP_Khach, TenSP_KH, Diachi_KH, fax, SoDienThoai, Ngaytrienkhai, Ngaysua,Lydo_Xuatkho,SoChungTu_XK,IDTrienKhai,ID,ParentID)"
                + " values (@IdPSX,@IDSP,@So_CT,@Maubv,@LoaiDH,@MaPo,@ngaynhan,@nvkd,@madh,@sanpham,"
                + " @chitietsanpham,@cdthanhpham,@soluongsx,@ngoaiquang,@mabv,@donvi,@daystar,@dayend,@BTPT11,"
                + " @TRONGLUONG11,@khachhang,@ghichu,@Donvisp,@SoluongSP, "
                + " @MaGH,@SL_LR, @SL_DH, @SL_TONKHO, @SL_SANXUAT, @xeploai, "
                + " @TongSL_Xuat, @TongTL_Xuat, @SL_Bi, @TL_Bi, @Loai_Bi, @Diengiai, "
                + " @MaSP_Khach, @TenSP_KH, @Diachi_KH, @fax, @SoDienThoai, @Ngaytrienkhai, GetDate(),"
                + " @Lydo_Xuatkho,@SoChungTu_XK,@IDTrienKhai,@ID,@ParentID)", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpBatDau.Text == "")
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dpBatDau.Text;
                    if (dpKetThuc.Text == "")
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpKetThuc.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@IdPSX", SqlDbType.BigInt)).Value = txtIdPSX.Text;
                command.Parameters.Add(new SqlParameter("@ngaynhan", SqlDbType.Date)).Value = dpNgayXuat.Text;
                command.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.NVarChar)).Value = txtIdKH.Text;
                command.Parameters.Add(new SqlParameter("@So_CT", SqlDbType.NVarChar)).Value = txtSoCT.Text;
                command.Parameters.Add(new SqlParameter("@Maubv", SqlDbType.NVarChar)).Value = txtmau_banve.Text;
                command.Parameters.Add(new SqlParameter("@LoaiDH", SqlDbType.NVarChar)).Value = txtLoaiDH.Text;
                command.Parameters.Add(new SqlParameter("@MaPo", SqlDbType.NVarChar)).Value = txtMaPO.Text;
                command.Parameters.Add(new SqlParameter("@nvkd", SqlDbType.NVarChar)).Value = txtPhuTrachKinhDoanh.Text;
                command.Parameters.Add(new SqlParameter("@madh", SqlDbType.NVarChar)).Value = cbmadh.Text;
                command.Parameters.Add(new SqlParameter("@sanpham", SqlDbType.NVarChar)).Value = txtchitietSP.Text;
                command.Parameters.Add(new SqlParameter("@chitietsanpham", SqlDbType.NVarChar)).Value = txtTenQC_sanpham.Text;
                command.Parameters.Add(new SqlParameter("@cdthanhpham", SqlDbType.NVarChar)).Value = CbCongDoan.Text;
                command.Parameters.Add(new SqlParameter("@soluongsx", SqlDbType.Int)).Value = Soluongsanxuat;
                command.Parameters.Add(new SqlParameter("@ngoaiquang", SqlDbType.NVarChar)).Value = txtngoaiquan.Text;
                command.Parameters.Add(new SqlParameter("@mabv", SqlDbType.NVarChar)).Value = txtMasp.Text;
                command.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonviCT.Text;
                command.Parameters.Add(new SqlParameter("@BTPT11", SqlDbType.Int)).Value = int.Parse(txtSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@TRONGLUONG11", SqlDbType.Float)).Value = double.Parse(txtTL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@khachhang", SqlDbType.NVarChar)).Value = cbTen_KhachHang.Text;
                command.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar)).Value = txtGhiChu.Text;
                command.Parameters.Add(new SqlParameter("@Donvisp", SqlDbType.NVarChar)).Value = txtDVSPLR.Text;
                command.Parameters.Add(new SqlParameter("@SoluongSP", SqlDbType.Int)).Value = int.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@MaGH", SqlDbType.NVarChar)).Value = txtMaPhieuXuatHang.Text;
                command.Parameters.Add(new SqlParameter("@SL_LR", SqlDbType.Float)).Value = double.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@SL_DH", SqlDbType.Float)).Value = double.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@SL_TONKHO", SqlDbType.Float)).Value = double.Parse(txtTonKho.Text);
                command.Parameters.Add(new SqlParameter("@SL_SANXUAT", SqlDbType.Float)).Value = double.Parse(txtSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@xeploai", SqlDbType.NVarChar)).Value = txtLoaiKH.Text;
                command.Parameters.Add(new SqlParameter("@TongSL_Xuat", SqlDbType.Float)).Value = double.Parse(txtTONGSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@TongTL_Xuat", SqlDbType.Float)).Value = double.Parse(txtTONGTL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@SL_Bi", SqlDbType.NVarChar)).Value = txtSL_Bi.Text;
                command.Parameters.Add(new SqlParameter("@TL_Bi", SqlDbType.NVarChar)).Value = txtTL_Bi.Text;
                command.Parameters.Add(new SqlParameter("@Loai_Bi", SqlDbType.NVarChar)).Value = cbloaibi.Text;
                command.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                command.Parameters.Add(new SqlParameter("@MaSP_Khach", SqlDbType.NVarChar)).Value = txtMasp_KH.Text;
                command.Parameters.Add(new SqlParameter("@TenSP_KH", SqlDbType.NVarChar)).Value = txtTenHang_KH.Text;
                command.Parameters.Add(new SqlParameter("@Diachi_KH", SqlDbType.NVarChar)).Value = txtdiachi_KhachHang.Text;
                command.Parameters.Add(new SqlParameter("@fax", SqlDbType.NVarChar)).Value = txtfax.Text;
                command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.Int)).Value = int.Parse(txtdienthoai.Text);
                command.Parameters.Add(new SqlParameter("@Ngaytrienkhai", SqlDbType.Date)).Value = dpNgayTK.Text;
                command.Parameters.Add(new SqlParameter("@Lydo_Xuatkho", SqlDbType.NVarChar)).Value = txtLyDoXuat.Text;
                command.Parameters.Add(new SqlParameter("@SoChungTu_XK", SqlDbType.Int)).Value = int.Parse(txtSo_ChungTu.Text);
                command.Parameters.Add(new SqlParameter("@IDTrienKhai", SqlDbType.NVarChar)).Value = idtrienkhai;
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar)).Value = id;
                command.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.NVarChar)).Value = parentid;
                adapter.Fill(dt);
                cn.Close();
                CapNhat_TongHopBaoBi();
                CapNhat_GhichuXuat();
                TheHienDonHangXuatKhoTheoMaDonHang();
                //CapNhatDH_HeThong();
                //CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
                //CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                //CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
                //CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                
            }
            else
            {
                MessageBox.Show("Mã phiếu xuất,tên khách hàng các số lượng không được rỗng", "THÔNG BÁO");
            }
        }
        private void SuaDuLieu()
        {
            if (txtTenHang_KH.Text != "" && txtMaPhieuXuatHang.Text != "" && txtTONGSL_XUAT.Text != "" && txtSL_XUAT.Text != "" &&
            txtTONGTL_XUAT.Text != "" && txtTL_XUAT.Text != "" && txtTL_Bi.Text != "" && txtSL_Bi.Text != "")
            {
                SqlConnection cn = new SqlConnection();
                decimal Soluongsanxuat = Convert.ToDecimal(txtSLSP_DH.Text);
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("UPDATE tbl11 set ngaynhan=@ngaynhan,IDSP=@IDSP,So_CT=@So_CT,Maubv=@Maubv,LoaiDH=@LoaiDH,MaPo=@MaPo,nvkd=@nvkd,madh=@madh,sanpham=@sanpham,"
                + " chitietsanpham=@chitietsanpham,cdthanhpham=@cdthanhpham,soluongsx=@soluongsx,ngoaiquang=@ngoaiquang,mabv=@mabv,donvi=@donvi,daystar=@daystar,dayend=@dayend,BTPT11=@BTPT11,"
                + " TRONGLUONG11=@TRONGLUONG11,khachhang=@khachhang,ghichu=@ghichu,Donvisp=@Donvisp,SoluongSP=@SoluongSP, "
                + " MaGH=@MaGH,SL_LR=@SL_LR,SL_DH=@SL_DH,SL_TONKHO=@SL_TONKHO,SL_SANXUAT=@SL_SANXUAT,xeploai=@xeploai, "
                + " TongSL_Xuat=@TongSL_Xuat, TongTL_Xuat=@TongTL_Xuat, SL_Bi=@SL_Bi, TL_Bi=@TL_Bi, Loai_Bi=@Loai_Bi, Diengiai=@Diengiai, "
                + " MaSP_Khach=@MaSP_Khach, TenSP_KH=@TenSP_KH, Diachi_KH=@Diachi_KH, fax=@fax," +
                " SoDienThoai=@SoDienThoai, Ngaytrienkhai=@Ngaytrienkhai, Ngaysua=GetDate()," +
                " Lydo_Xuatkho=@Lydo_Xuatkho,SoChungTu_XK=@SoChungTu_XK,IDTrienKhai=@IDTrienKhai,ID=@ID,ParentID=@ParentID  where Num like '" + txtNum.Text + "'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpBatDau.Text == "")
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dpBatDau.Text;
                    if (dpKetThuc.Text == "")

                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpKetThuc.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                command.Parameters.Add(new SqlParameter("@ngaynhan", SqlDbType.Date)).Value = dpNgayXuat.Text;
                command.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.NVarChar)).Value = txtIdKH.Text;
                command.Parameters.Add(new SqlParameter("@So_CT", SqlDbType.NVarChar)).Value = txtSoCT.Text;
                command.Parameters.Add(new SqlParameter("@Maubv", SqlDbType.NVarChar)).Value = txtmau_banve.Text;
                command.Parameters.Add(new SqlParameter("@LoaiDH", SqlDbType.NVarChar)).Value = txtLoaiDH.Text;
                command.Parameters.Add(new SqlParameter("@MaPo", SqlDbType.NVarChar)).Value = txtMaPO.Text;
                command.Parameters.Add(new SqlParameter("@nvkd", SqlDbType.NVarChar)).Value = txtPhuTrachKinhDoanh.Text;
                command.Parameters.Add(new SqlParameter("@madh", SqlDbType.NVarChar)).Value = cbmadh.Text;
                command.Parameters.Add(new SqlParameter("@sanpham", SqlDbType.NVarChar)).Value = txtchitietSP.Text;
                command.Parameters.Add(new SqlParameter("@chitietsanpham", SqlDbType.NVarChar)).Value = txtTenQC_sanpham.Text;
                command.Parameters.Add(new SqlParameter("@cdthanhpham", SqlDbType.NVarChar)).Value = CbCongDoan.Text;
                command.Parameters.Add(new SqlParameter("@soluongsx", SqlDbType.Int)).Value = Soluongsanxuat;
                command.Parameters.Add(new SqlParameter("@ngoaiquang", SqlDbType.NVarChar)).Value = txtngoaiquan.Text;
                command.Parameters.Add(new SqlParameter("@mabv", SqlDbType.NVarChar)).Value = txtMasp.Text;
                command.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonviCT.Text;
                command.Parameters.Add(new SqlParameter("@BTPT11", SqlDbType.Int)).Value = int.Parse(txtSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@TRONGLUONG11", SqlDbType.Float)).Value = double.Parse(txtTL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@khachhang", SqlDbType.NVarChar)).Value = cbTen_KhachHang.Text;
                command.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar)).Value = txtGhiChu.Text;
                command.Parameters.Add(new SqlParameter("@Donvisp", SqlDbType.NVarChar)).Value = txtDVSPLR.Text;
                command.Parameters.Add(new SqlParameter("@SoluongSP", SqlDbType.Int)).Value = int.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@MaGH", SqlDbType.NVarChar)).Value = txtMaPhieuXuatHang.Text;
                command.Parameters.Add(new SqlParameter("@SL_LR", SqlDbType.Float)).Value = double.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@SL_DH", SqlDbType.Float)).Value = double.Parse(txtSLSP_DH.Text);
                command.Parameters.Add(new SqlParameter("@SL_TONKHO", SqlDbType.Float)).Value = double.Parse(txtTonKho.Text);
                command.Parameters.Add(new SqlParameter("@SL_SANXUAT", SqlDbType.Float)).Value = double.Parse(txtSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@xeploai", SqlDbType.NVarChar)).Value = txtLoaiKH.Text;
                command.Parameters.Add(new SqlParameter("@TongSL_Xuat", SqlDbType.Float)).Value = double.Parse(txtTONGSL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@TongTL_Xuat", SqlDbType.Float)).Value = double.Parse(txtTONGTL_XUAT.Text);
                command.Parameters.Add(new SqlParameter("@SL_Bi", SqlDbType.NVarChar)).Value = txtSL_Bi.Text;
                command.Parameters.Add(new SqlParameter("@TL_Bi", SqlDbType.NVarChar)).Value = txtTL_Bi.Text;
                command.Parameters.Add(new SqlParameter("@Loai_Bi", SqlDbType.NVarChar)).Value = cbloaibi.Text;
                command.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                command.Parameters.Add(new SqlParameter("@MaSP_Khach", SqlDbType.NVarChar)).Value = txtMasp_KH.Text;
                command.Parameters.Add(new SqlParameter("@TenSP_KH", SqlDbType.NVarChar)).Value = txtTenHang_KH.Text;
                command.Parameters.Add(new SqlParameter("@Diachi_KH", SqlDbType.NVarChar)).Value = txtdiachi_KhachHang.Text;
                command.Parameters.Add(new SqlParameter("@fax", SqlDbType.NVarChar)).Value = txtfax.Text;
                command.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.Int)).Value = int.Parse(txtdienthoai.Text);
                command.Parameters.Add(new SqlParameter("@Ngaytrienkhai", SqlDbType.Date)).Value = dpNgayTK.Text;
                command.Parameters.Add(new SqlParameter("@Lydo_Xuatkho", SqlDbType.NVarChar)).Value = txtLyDoXuat.Text;
                command.Parameters.Add(new SqlParameter("@SoChungTu_XK", SqlDbType.Int)).Value = int.Parse(txtSo_ChungTu.Text);
                command.Parameters.Add(new SqlParameter("@IDTrienKhai", SqlDbType.NVarChar)).Value = idtrienkhai;
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar)).Value = id;
                command.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.NVarChar)).Value = parentid;
                adapter.Fill(dt);
                cn.Close();
                CapNhat_TongHopBaoBi();
                CapNhat_GhichuXuat();
                TheHienDonHangXuatKhoTheoMaDonHang();
                //CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
                //CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                //CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
                //CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                //CapNhatDH_HeThong();
            }
            else
            {
                MessageBox.Show("Mã phiếu xuất,tên khách hàng các số lượng không được rỗng", "THÔNG BÁO");
            }
        }

        private void GhiDuLieu_XuatKho(object sender, EventArgs e)//Ghi du lieu xuat
        {
            GhiDL_XuatKho();
        }
        private void SuaDulieu_DaXuat(object sender, EventArgs e)//Sua du lieu xuat
        {
            SuaDuLieu();
        }
        private void HuyDulieu_xuat(object sender, EventArgs e)//Huy du lieu xuat
        {
            CapNhatSL_VeKhong();
            //CapNhatDH_HeThong();
            //CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
            //CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
            //CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
            //CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
            Huy_XuatKho();
            TheHienDonHangXuatKhoTheoMaDonHang();
        }
        private void CapNhatSL_VeKhong()//Cap nhat so luong ve 0 truoc roi xoa du lieu
        {
            ketnoi Connect = new ketnoi();
            string sqlQuery = string.Format(@"update tbl11 set MaGH = N'Huy', BTPT11 = '0',
                TRONGLUONG11 = '0'  where Num like '{0}'", txtNum.Text);
            var kq = Connect.xulydulieu(sqlQuery);
            Connect.dongketnoi();
        }
        private void UpdateSoLuongGiaoVeZeroDelete()
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet set SoGiao='0' where ID like '{0}'",
            //        idsogiaohang, idtrienkhai, idchitietdonhang);
            //    SqlCommand cmd = new SqlCommand(sqlQuery, con);
            //    cmd.ExecuteNonQuery();
            //    CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
            //    CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
            //    CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
            //    CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
            //                     //CapNhatPhanTichKeHoachSanXuat();//Cập nhật phân tích đơn hàng vào sổ kế hoạch
            //    Delete();//Xóa luôn
            //    TheHienChiTietSoLuongGiaoTheoIDTrienKhai();
            //    MessageBox.Show("Success", "Message");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error" + ex, "Message");
            //    return;
            //}
        }
        private void LOAD_DONDATHANG(object sender, EventArgs e)//Lấy thông tin lệnh đặt hàng
        {
            TheHienDanhSachDonHangTrienKhaiTatCaCongDoan();
        }

        private void LOAD_DONHANG_XUATKHO(object sender, EventArgs e)//Lấy thông tin đơn hàng xuất kho
        {
            ListXuatKhoAll();
        }

        private void LAY_MACODE_XUATKHO(object sender, EventArgs e)//Lấy mã code xuất kho
        {
            LAY_MACODE_XUATKHO();
        }
        private void btnthemmonhang_Click(object sender, EventArgs e)// Ghi Du Lieu Xuat Kho Giao Hang
        {
            GhiDL_XuatKho();
        }
        #region formload
        private void UcLapPhieuXuatKho_Load(object sender, EventArgs e)
        {

            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            ListXuatKhoAll();
            this.gvDonHangTrienKhai.Appearance.Row.Font = new Font("Segoe UI", 8f);
            this.gvSoXuatKho.Appearance.Row.Font = new Font("Segoe UI", 8f);
            BINDING_MA_DONHANG();
            DM_KHACHHANG();
            TheHienDanhSachDonHangTrienKhaiCongDoan();
        }
        #endregion
        private void DM_KHACHHANG()
        {
            ketnoi connect = new ketnoi();
            cbTen_KhachHang.DataSource = connect.laybang("select TenKH from tblKHACHHANG");
            cbTen_KhachHang.DisplayMember = "TenKH";
            cbTen_KhachHang.ValueMember = "TenKH";
            connect.dongketnoi();
        }

        private void BINDING_MA_DONHANG()
        {
            ketnoi Connect = new ketnoi();
            cbmadh.DataSource = Connect.laybang("select madh,thoigianthaydoi from tblDHCT order by thoigianthaydoi DESC");
            cbmadh.DisplayMember = "madh";
            cbmadh.ValueMember = "madh";
            Connect.dongketnoi();
        }
        private void cbTen_KhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DIACHI_KH(); DienThoai_KH(); Fax_KH();
            }
            catch { }
        }
        private void DIACHI_KH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            SqlCommand cmd = new SqlCommand();
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("SELECT Diachi from tblKHACHHANG where TenKH like N'" + cbTen_KhachHang.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtdiachi_KhachHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void DienThoai_KH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            SqlCommand cmd = new SqlCommand();
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("SELECT Sodienthoai from tblKHACHHANG where TenKH like N'" + cbTen_KhachHang.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtdienthoai.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void Fax_KH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            SqlCommand cmd = new SqlCommand();
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("SELECT Fax from tblKHACHHANG where TenKH like N'" + cbTen_KhachHang.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtfax.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private string idtrienkhai;
        private string id;
        private string parentid;
        private string idchitietdonhang;
        private void BINDING_DONDATHANG(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gvDonHangTrienKhai.GetFocusedDisplayText();
            txtGhichuXuatKho.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(GhichuXuatKho_grid1);
            dpNgayTK.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Ngaytk_grid1);
            cbmadh.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Mapsx_grid1);
            txtMasp.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Masp_grid1);
            txtTenQC_sanpham.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Sanphamlaprap_grid1);
            txtSLSP_DH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongDH_grid1) == "" ? "0" : gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongDH_grid1);
            txtDVSPLR.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(DonviSPLR_grid1) == "" ? "0" : gvDonHangTrienKhai.GetFocusedRowCellDisplayText(DonviSPLR_grid1);
            txtSoCT.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Soct_grid1) == "" ? "0" : gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Soct_grid1);
            txtchitietSP.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Chitiet_grid1);
            txtSL_CTDH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongSX_grid1) == "" ? "0" : gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongSX_grid1);
            txtdonviCT.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Donvi_grid1);

            txtMasp_KH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(MaspKhachHang_grid1);
            txtTenHang_KH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Tenspkhachhang_grid1);

            txtmau_banve.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Maubv_grid1);
            txtTonKho.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongTon_grid1) == "" ? "0" : gvDonHangTrienKhai.GetFocusedRowCellDisplayText(SoluongTon_grid1);
            txtIdPSX.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(idchitietdonhang_grid1);
            txtIdKH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(idtrienkhai_grid);

            txtMaPO.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(MaPo_grid1);
            dpBatDau.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(DayStar_grid1);
            dpKetThuc.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(DayEnd_grid1);
            txtngoaiquan.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Ngoaiquan_grid1);
            txtGhiChu.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Ghichu_grid1);
            txtLoaiDH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(LoaiDH_grid1);
            cbTen_KhachHang.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(khachhang_grid1);
            txtPhuTrachKinhDoanh.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Kinhdoanh_grid1);
            txtLoaiKH.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(LoaiKH_grid1);
            txtdiachi_KhachHang.Text = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(Diachigiaohang_grid1);
            idtrienkhai = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(idtrienkhai_grid1);
            idchitietdonhang = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(idchitietdonhang_grid1);
            id = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(id_grid1);
            parentid = gvDonHangTrienKhai.GetFocusedRowCellDisplayText(parentid_grid1);
        }

        private void BINDING_DONHANG_XUAT_KHO(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gvSoXuatKho.GetFocusedDisplayText();
            txtGhichuXuatKho.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(ghichuxuatkho_grid2);
            txtNum.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Num_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(Num_grid2);
            txtIdKH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(IdKH_grid2);
            txtMaPhieuXuatHang.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(MaPXK_grid2);
            dpNgayXuat.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(ngayxuat_grid2);
            cbmadh.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Madh_grid2);
            txtMasp.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Masp_grid2);
            txtMasp_KH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(MaspKH_grid2);
            txtTenQC_sanpham.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SPLR_grid2);
            txtTenHang_KH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TenspXuatKho_grid2);
            txtchitietSP.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(ChiTiet_grid2);
            txtSoCT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SoCT_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SoCT_grid2);
            //txtSL_CTDH.Text = gridView2.GetFocusedRowCellDisplayText(so);
            txtdonviCT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(DonviCT_grid2);
            txtMaPO.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(MaPO_grid2);
            //txtSLSP_DH.Text = gridView2.GetFocusedRowCellDisplayText();
            txtDVSPLR.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(dvSPLR_grid2);
            txtTonKho.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLtonkho_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLtonkho_grid2);
            txtmau_banve.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(MauBV_grid2);
            cbTen_KhachHang.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Khachhang_grid2);
            txtdiachi_KhachHang.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(DiachiKH_grid2);
            txtfax.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(fax_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(fax_grid2);
            txtdienthoai.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Dienthoai_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(Dienthoai_grid2);
            dpNgayTK.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Ngaytk_grid2);
            txtLoaiDH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(LoaiDH_grid2);
            CbCongDoan.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(CongDoan_grid2);
            txtTONGSL_XUAT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TongSLXuat_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(TongSLXuat_grid2);
            txtTONGTL_XUAT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TongTLXuat_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(TongTLXuat_grid2);
            txtSL_XUAT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLThucXuat_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLThucXuat_grid2);
            txtTL_XUAT.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TLThucXuat_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(TLThucXuat_grid2);
            txtTL_Bi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TLBi_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(TLBi_grid2);
            txtSL_Bi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLBi_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLBi_grid2);
            cbloaibi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(LoaiBi_grid2);
            txtGhiChu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Ghichu_grid2);
            txtDienGiai.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(DienGiai_grid2);
            txtPhuTrachKinhDoanh.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(KD_grid2);
            txtLyDoXuat.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Lydoxuat_grid2);
            txtSo_ChungTu.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SoChungTu_grid2);
            txtTong_BaoBi.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(TongCongBaoBi_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(TongCongBaoBi_grid2);
            txtSLSP_DH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLDH_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLDH_grid2);
            txtSL_CTDH.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLCT_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLCT_grid2);
            txtTonKho.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(SLtonkho_grid2) == "" ? "0" : gvSoXuatKho.GetFocusedRowCellDisplayText(SLtonkho_grid2);
            dpBatDau.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(NgayBD_grid2);
            dpKetThuc.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(NgayKT_grid2);
            txtngoaiquan.Text = gvSoXuatKho.GetFocusedRowCellDisplayText(Ngoaiquan_grid2);
            idtrienkhai = gvSoXuatKho.GetFocusedRowCellDisplayText(idtrienkhai_grid2);
            idchitietdonhang = gvSoXuatKho.GetFocusedRowCellDisplayText(idchitietdonhang_grid2);
            //txtLyDoXuat.Text = "Xuất kho giao hàng";
            CbCongDoan.Text = "TP";
        }

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void txtdienthoai_TextChanged(object sender, EventArgs e)
        {
            if (txtdienthoai.Text == "")
                txtdienthoai.Text = string.Format("0");
            double.Parse(txtdienthoai.Text);
            trongluongthucxuat();
        }

        private void txtTONGSL_XUAT_TextChanged(object sender, EventArgs e)
        {
            if (txtTONGSL_XUAT.Text == "")
                txtTONGSL_XUAT.Text = string.Format("0");
            double.Parse(txtTONGSL_XUAT.Text);
            trongluongthucxuat();
            txtSL_XUAT.Text = txtTONGSL_XUAT.Text;
        }

        private void txtTONGTL_XUAT_TextChanged(object sender, EventArgs e)
        {
            if (txtTONGSL_XUAT.Text == "")
                txtTONGSL_XUAT.Text = string.Format("0");
            double.Parse(txtTONGSL_XUAT.Text);
            txtTL_XUAT.Text = txtTONGTL_XUAT.Text;
            trongluongthucxuat();
        }

        private void txtTL_Bi_TextChanged(object sender, EventArgs e)
        {
            if (txtTONGSL_XUAT.Text == "")
                txtTONGSL_XUAT.Text = string.Format("0");
            double.Parse(txtTONGSL_XUAT.Text);
            trongluongthucxuat();
        }

        private void btncapnhatht_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Connect.mConnect;
                conn.Open();
                DataSet ds = SqlHelper.ExecuteDataset(conn, "[T11_UPDATE]");
                MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG");
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công");
            }
        }
        private void CapNhatDH_HeThong()
        {
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = Connect.mConnect;
            //conn.Open();
            //DataSet ds = SqlHelper.ExecuteDataset(conn, "[T11_UPDATE]");
            //conn.Close();

            try
            {
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(txtUpdateProc.Text, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@KeHoachID", SqlDbType.Float)).Value = txtIdKH.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }


        }
        private void btndowTenKH_Tengoi_Click(object sender, EventArgs e)
        {
            txtTenHang_KH.Text = txtTenQC_sanpham.Text;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTenHang_KH.Text = txtchitietSP.Text;
        }

        private void btninphieu_xuatkhoGiaoHang_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewXuatKhoGiaoHang "
                   + " where MaGH like N'" + txtMaPhieuXuatHang.Text + "' ");
            XtraReportXuatKho Rpxuatkho = new XtraReportXuatKho();
            Rpxuatkho.DataSource = dt;
            Rpxuatkho.DataMember = "Table";
            Rpxuatkho.CreateDocument(false);
            Rpxuatkho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuXuatHang.Text;
            PrintTool tool = new PrintTool(Rpxuatkho.PrintingSystem);
            Rpxuatkho.ShowPreviewDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtTONGSL_XUAT.ResetText(); txtSL_XUAT.ResetText();
            txtTONGTL_XUAT.ResetText(); txtTL_XUAT.ResetText();
            txtTL_Bi.ResetText(); txtSL_Bi.ResetText();
            cbloaibi.ResetText(); txtMaPhieuXuatHang.ResetText();
            txtDienGiai.ResetText(); txtLyDoXuat.ResetText();
        }
        Path path;//Hàm gọi path bản vẽ
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {

            string pat = string.Format(@"{0}\{1}.pdf", path.pathbanve, txtMasp.Text);
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
            Path path = new Path();
            string pat = string.Format(@"{0}\{1}.PDF", path, cbmadh.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }
        }
        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {
            //string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaDH.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{ MessageBox.Show("Hiện mã kế hoạch này chưa đúng"); }
        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMasp.Text, path.pathbanve);
            f2.ShowDialog();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(cbmadh.Text, path.pathkinhdoanh);
            f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(cbmadh.Text, path.pathbanve);
            f2.Show();
        }

        private void btnDienGiai_QuaTongDH_Click(object sender, EventArgs e)
        {
            txtTong_BaoBi.Text = txtDienGiai.Text;
        }

        private void btnExSoXuatKho_Click(object sender, EventArgs e)
        {
            grSoXuatKho.ShowPrintPreview();
        }
        //txtIdPSX.Text= gridView1.GetFocusedRowCellDisplayText(IdPSX_grid1);
        //    txtIdKH.Text= gridView1.GetFocusedRowCellDisplayText(idtrienkhai_grid);
        //proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
        private void CapNhatSoGiaoVaoSoTrienKhai()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("CapNhatSoTrienKhai", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idtrienkhai", SqlDbType.Int).Value = idtrienkhai;
            cmd.ExecuteNonQuery();

        }
        //proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
        private void CapNhatCumChiTiet()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("CapNhatCumChiTiet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idchitietdonhang", SqlDbType.Int).Value =idchitietdonhang;
            cmd.ExecuteNonQuery();
        }
        //proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
        private void CapNhatThanhPhamLapGhep()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("CapNhatThanhPhamLapGhep", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idchitietdonhang", SqlDbType.Int).Value = idchitietdonhang;
            cmd.ExecuteNonQuery();
        }
        //proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
        private void CapNhatMoiNgay()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("CapNhatSoLuongMoiNgay", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idtrienkhai", SqlDbType.Int).Value = idtrienkhai;
            cmd.ExecuteNonQuery();
        }

        private void txtLenhSanXuat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where madh like N'" + cbmadh.Text + "'");
            RpPhieusanxuat rpPHIEUSANXUAT = new RpPhieusanxuat();
            rpPHIEUSANXUAT.DataSource = dt;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbmadh.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnExportDonHangTrienKhai_Click(object sender, EventArgs e)
        {
            grDonhangTrienKhai.ShowPrintPreview();
        }

        private void grDonhangTrienKhai_DoubleClick(object sender, EventArgs e)
        {
            TheHienPhieuXuat();
        }
        private void TheHienPhieuXuat()
        {
           string a= idtrienkhai;
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select * from tbl11
            where
            IDTrienKhai like '{0}'
			order by left(MaGH,6) desc,
			SoChungTu_XK ASC,Num desc", idtrienkhai);
            grSoXuatKho.DataSource = Model.Function.GetDataTable(sqlQuery);
            gvSoXuatKho.ExpandAllGroups();
        }
    }
}