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
using DevExpress.Xpf.Grid;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.XtraPrinting;
using quanlysanxuat.Model;
using quanlysanxuat.View;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    public partial class UcDONDH : DevExpress.XtraEditors.XtraForm
    {

        public UcDONDH()
        {
            InitializeComponent();
        }
        string Gol = "";
        SqlCommand cmd;
        #region Tính thành tiền
        private void tich()
        {
            try
            {
                double SL = txtsoluong.Text == null ? 0 : double.Parse(txtsoluong.Text);
                double DG = txtdongia.Text == null ? 0 : double.Parse(txtdongia.Text);
                double TT = SL * DG;
                txtthanhtien.Text = TT.ToString();
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion
        #region tính ngoại tệ
        private void quydoingoaite()
        {
            try
            {
                double Soluongngoaite = double.Parse(txtsoluongngoaite.Text);
                double tygia = double.Parse(txttygia.Text);
                double DG = Soluongngoaite * tygia;
                txtdongia.Text = Convert.ToString(DG);
            }
            catch { return; }
        }
        #endregion



        #region Đọc đơn hàng theo người phụ trách kinh doanh
        private void DocDonDatHang()
        {
            if (ClassUser.User == "99999")
            {
                DocTatCaDonDatHang();
            }
            else
            {
                DocDonDatHangTheoNguoiPhuTrachKinhDoanh();
            }
        }
        private void DocDonDatHangTheoNguoiPhuTrachKinhDoanh()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Code, DH.Ngaydh, DH.madh, LoaiDH, nvkd, Khachhang, Diachi, MaPO,
                                   PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi,
                                   thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri
                                   from tblDHCt group by madh) CT on DH.madh = CT.madh
                                   where nvkd like N'{0}' and cast(Ngaydh as Date) 
                                   between '{1}'
                                   and '{2}' order by DH.Ngaydh DESC",
                                   Login.Username,
                                   dptu_ngay.Value.ToString("yyyy-MM-dd"),
                                   dpden_ngay.Value.ToString("yyyy-MM-dd"));
            gridControl1.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void DocTatCaDonDatHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Code, DH.Ngaydh, DH.madh, LoaiDH, nvkd, Khachhang, Diachi, MaPO,
                                       PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi,
                                       thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri
                                       from tblDHCt group by madh) CT on DH.madh = CT.madh
                                       where cast(Ngaydh as Date) 
                                       between '{0}'
                                       and '{1}' order by DH.Ngaydh DESC",
                                   dptu_ngay.Value.ToString("yyyy-MM-dd"),
                                   dpden_ngay.Value.ToString("yyyy-MM-dd"));
            gridControl1.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        #endregion
        /// <summary>
        /// gọi chi tiết đơn đặt hàng
        /// </summary>

        #region Đọc chi tiết đơn hàng
        private void DocChiTietDonHang()
        {
            if (ClassUser.User == "99999")
            {
                DocChiTietDonhangTheoNgay();
            }
            else
            {
                DocChiTietDonhangTheoNguoiPhuTrach();
            }
        }
        private void DocChiTietDonhangTheoNguoiPhuTrach()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select case when b.IDChiTietDonHang >0 then 'x' else '' end DaChia,
             CT.GhichuXuatKho, CT.Diachi_giaohang, CT.Iden, 
             CT.Codedetail, CT.Tenquicach, CT.madh, CT.Khachhang, CT.MaPo, CT.MaSP, CT.dvt, CT.Mau_banve,
             CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd,
             CT.tygia, CT.Code, CT.nguoithaydoi, CT.thoigianthaydoi, CT.pheduyet, CT.Trangthai,
             (case when CT.pheduyet = N'Đã phê duyệt' then 'True' end) Muc,CT.Tenkhachhang,
             CT.Masp_KH,CT.MaKH, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai]
             from tblDHCT CT 
			 left outer join tblDONHANG DH on CT.madh = DH.madh
			 left outer join
			 (select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)b
			 on CT.Iden=b.IDChiTietDonHang
             where CT.nguoithaydoi like N'{0}' and 
             cast(CT.thoigianthaydoi as Date) between '{1}' and '{2}'",
             MainDev.username,
             dpNgayChiTietTu.Value.ToString("yyyy-MM-dd"),
             dpNgayChiTietDen.Value.ToString("yyyy-MM-dd"));
            gridControl2.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void DocChiTietDonhangTheoNgay()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@" select case when b.IDChiTietDonHang >0 then 'x' else '' end DaChia,CT.GhichuXuatKho, CT.Diachi_giaohang, CT.Iden, 
             CT.Codedetail, CT.Tenquicach, CT.madh, CT.Khachhang, CT.MaPo, CT.MaSP, CT.dvt, CT.Mau_banve,
             CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd,
             CT.tygia, CT.Code, CT.nguoithaydoi, CT.thoigianthaydoi, CT.pheduyet, CT.Trangthai,
             (case when CT.pheduyet = N'Đã phê duyệt' then 'True' end) Muc,CT.Tenkhachhang,
             CT.Masp_KH,CT.MaKH, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai]
             from tblDHCT CT 
			 left outer join tblDONHANG DH on CT.madh = DH.madh
			 left outer join
			 (select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)b
			 on CT.Iden=b.IDChiTietDonHang
             where cast(CT.thoigianthaydoi as Date) between '{0}' and '{1}'",
             dpNgayChiTietTu.Value.ToString("yyyy-MM-dd"),
             dpNgayChiTietDen.Value.ToString("yyyy-MM-dd"));
            gridControl2.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        #endregion
        /// <summary>
        /// Gọi đơn đặt hàng mới khi ghi vào hệ thống
        /// </summary>
        private void loadData1()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" select Code,Ngaydh,madh,LoaiDH,nvkd,MaPo,Khachhang,Diachi, "
                                    + "PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, "
                                    + "thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri "
                                    + "from tblDHCt group by madh) CT on DH.MADDH = CT.madh where nvkd like N'" + MainDev.username + "' and Code like '" + txtcodeview.Text + "' order by Ngaydh Desc");
            kn.dongketnoi();
        }
        /// <summary>
        /// Gọi chi tiết đơn đặt hàng
        /// </summary>
        private void loadData2()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select CT.GhichuXuatKho,CT.Iden,CT.Codedetail,CT.Tenquicach,CT.madh,CT.Khachhang,CT.MaSP,CT.dvt,CT.Mau_banve, "
            + " CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd, "
            + " CT.tygia, CT.Code, CT.pheduyet, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai], "
            + " DH.[nguoithaydoi], DH.[thoigianthaydoi] "
            + " from tblDHCT CT, tblDONHANG DH where CT.Code = DH.Code and nvkd like N'" + MainDev.username + "'and Code like '" + txtcodeview.Text + "'");
            kn.dongketnoi();
        }

        /// <summary>
        /// Gọi đơn hàng theo tên nhân viên
        /// </summary>
        private void LoadData1_nhanvienkinhdoanh()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select Code,Ngaydh,MADDH,LoaiDH,nvkd,MaPo,Khachhang,Diachi, "
                                     + "PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, "
                                     + "thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri "
                                     + "from tblDHCt group by madh) CT on DH.MADDH = CT.madh where nvkd like N'" + MainDev.username + "' and Code like '" + txtcodeview.Text + "' order by Ngaydh Desc");
            kn.dongketnoi();
        }
        /// <summary>
        /// gọi chi tiết đơn đặt hàng theo từng nhân viên kinh doanh
        /// </summary>
        private void LoadData2_nhanvienkinhdoanh()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select CT.GhichuXuatKho,CT.Iden,CT.Codedetail,CT.Tenquicach,CT.madh,CT.Khachhang,CT.MaSP,CT.dvt,CT.Mau_banve, "
            + " CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd, "
            + " CT.tygia, CT.Code, CT.pheduyet, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai], "
            + " DH.[nguoithaydoi], DH.[thoigianthaydoi] "
            + " from tblDHCT CT, tblDONHANG DH where CT.Code = DH.Code and nvkd like N'" + MainDev.username + "'and Code like '" + txtcodeview.Text + "'");
            kn.dongketnoi();
        }
        /// <summary>
        /// khóa sửa xóa nếu tài khoản nhân viên
        /// </summary>
        private void khoataikhoanuser()
        {

        }
        private void mokhoa()

        {

        }

        private void khoadangnhap()
        {
            //if (Username == "Nguyễn Mộng Trinh" || Username == "Nguyễn Nhật Phương Tùng")
            //{
            //    btnxoachitiet.Enabled = true; btnupdate.Enabled = true;                          
            //}          
        }

        private void khoasuaxoa()
        {

        }

        private void getdatehethong()// Lấy ngày hệ thống
        {

        }

        private void ngaydonhang()//Lấy ngày lập đơn đặt hàng
        {

        }

        private void thoigianthaydoidonhang()
        {

        }

        #region formLoad
        private void UcDONDH_Load(object sender, EventArgs e)
        {
            txtSoLuonDonHangTrongNgay.Text = "01";//Gán đơn hàng là 1
            this.treeListProductionStagesPlan.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 8f);
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 8f);
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 8f);
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.AddDays(1).ToString();
            dpNgayChiTietTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpNgayChiTietDen.Text = DateTime.Now.AddDays(1).ToString();
            TH();

        }
        private async void TH()
        {
            await Task.Run(() =>
            {
                Invoke((Action)(() =>
                {
                    LoadMaDH();
                    DM_KHACHHANG();
                    atcNgoaiquan();
                    TenSP_MaSP();
                    AutocompleTenHang_KH();
                    AutocompleMa_KH();
                    AutoComplete_MASPKH();
                    AutoComplete_TenQCSP();
                    DocGoiYMaDonDatHang();
                    DocDonDatHang();
                    DocChiTietDonHang();
                    AddItemCBMaBuHang();
                }));
            });
        }
        #endregion


        /// <summary>
        /// Tự động bắt tên gọi của khách hàng
        /// </summary>
        private void AutoComplete_TenQCSP()
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Tenquicach from tblDHCT where nguoithaydoi like N'" + MainDev.username + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtTenHang_KH.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        /// <summary>
        /// Tìm mã sản phẩm thông tin mã sản phẩm trong danh mục sản phẩm
        /// </summary>
        private void LoadMaDH()
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            LookupCheck_Masp.Properties.DataSource = Connect.laybang(
                        @"select SP.Masp,SP.Tensp,
                        Makh,TenKH,Toncuoi from tblSANPHAM SP left join tblKHACHHANG
                        KH on SP.Makh=KH.MKH left outer join tblSanpham_tonkho T
						on SP.Masp=T.Masp where 
                        SP.Tensp is not null and SP.Tensp <>''
						order by Makh ASC");
            LookupCheck_Masp.Properties.DisplayMember = "Masp";
            LookupCheck_Masp.Properties.ValueMember = "Masp";
            LookupCheck_Masp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            LookupCheck_Masp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            LookupCheck_Masp.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void BINDING_MAKH()//BINDING DU LIEU MA KHI KHACH HANG THAY DOI
        {

        }
        /// <summary>
        /// truy vấn loại khách hàng theo tên khách hàng
        /// </summary>
        private void BINDING_LOAIKH()//BINDING XEPLOAI KH KHI KHACH HANG THAY DOI
        {
            string sqlQuery = string.Format(@"select TenKH,
            case when DiemBQ is null then '' else DiemBQ end DGDiemBQ,
		    case when Loai is null then '#' else Loai end DGLoai
		    from (select TenKH,DiemBQ,
			case when DiemBQ >= 3.8 then 'Trung-Thanh'
			when DiemBQ < 3.8 and DiemBQ>=2 then 'Tiem-Nang'
			when DiemBQ < 2 then 'Da-mua'
			end Loai from tblKHACHHANG a
			left outer join
			(select CongTyLienHe,
			sum(DiemBQ)/count(DiemBQ) DiemBQ
			from KhachHangHoSo group by CongTyLienHe)b
			on a.TenKH=b.CongTyLienHe)c
			where TenKH like N'{0}'", CbKhachhang.Text);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                cbloaiKH.Text = Convert.ToString(reader[2]);
            //txtDiaChi_GiaoHang.Text = Convert.ToString(reader[1]);//Dia chi giao hang
            reader.Close();
            con.Close();
        }

        /// <summary>
        /// sự kiện combobox khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbKhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {
            BINDING_LOAIKH(); DNMaKH();
        }

        private void Bindin()
        {

        }

        private void AutoComPlete_CheckMasp()//autocomplete Mã sản phẩm
        {

        }

        private void LookSanpham()//look sản phẩm từ check mã sản phẩm
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select  ISNULL(Tensp,'')+' ('+ISNULL(Kichthuoc,'')+')'  from tblSANPHAM where Masp like N'" + LookupCheck_Masp.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtQuycachchitiet.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }

        private void AutocompleTenHang_KH()//Lấy tên hàng theo khách hàng
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Masp from tblSANPHAM where ISNULL(Tensp,'')+' ('+ISNULL(Kichthuoc,'')+')' like N'" + txtQuycachchitiet.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtmasp.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }
        private void AutocompleMa_KH()//Lấy tên hàng theo khách hàng
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Masp_KH from tblDHCT where Masp_KH <>''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    Collection.Add(reader.GetString(0));
                }
                txtMasp_KH.AutoCompleteCustomSource = Collection;
                con.Close();
            }
        }
        private void AutoComplete_MASPKH()//Lấy tên hàng theo khách hàng
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strQuery = string.Format(@"select Tenkhachhang from tblDHCT where nguoithaydoi like N'{0}'", MainDev.username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtTenHang_KH.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        #region Tao mới mã đơn hàng gợi ý
        private void DocGoiYMaDonDatHang()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Connect.mConnect;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            SqlCommand cmd = new SqlCommand(@"select REPLACE(convert(nvarchar,GetDate(),11),'/','')", cn);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                txtMaDonHang.Text = read[0].ToString();
            }
            read.Close();
            cn.Close();
        }
        #endregion


        private void SELECT_TENSANPHAM()
        {
            //CbQuycachchitiet.DataSource = kn.laydulieu("select ISNULL(Tensp,'')+' ('+ISNULL(Kichthuoc,'')+')' as SANPHAM from tblSANPHAM");
            //CbQuycachchitiet.DisplayMember = "SANPHAM";
            //CbQuycachchitiet.ValueMember = "SANPHAM";
        }
        //Thể hiện danh mục khách hàng
        private void DM_KHACHHANG()
        {
            ketnoi connect = new ketnoi();
            CbKhachhang.Properties.DataSource = connect.laybang(@"select TenKH,
            case when DiemBQ is null then '' else DiemBQ end DGDiemBQ,
		    case when Loai is null then '#' else Loai end DGLoai
		    from (select TenKH,DiemBQ,
			case when DiemBQ >= 3.8 then 'Trung-Thanh'
			when DiemBQ < 3.8 and DiemBQ>=2 then 'Tiem-Nang'
			when DiemBQ < 2 then 'Da-mua'
			end Loai from tblKHACHHANG a
			left outer join
			(select CongTyLienHe,
			sum(DiemBQ)/count(DiemBQ) DiemBQ
			from KhachHangHoSo group by CongTyLienHe)b
			on a.TenKH=b.CongTyLienHe)c");
            CbKhachhang.Properties.DisplayMember = "TenKH";
            CbKhachhang.Properties.ValueMember = "TenKH";
            CbKhachhang.EditValue = CbKhachhang.Properties.GetKeyValueByDisplayValue(0);
            connect.dongketnoi();
        }
        private void cbmadh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbmadh.Text = string.Format("PSX ", decimal.Parse(cbmadh.Text));
            //cbmadh.SelectionStart = cbmadh.Text.Length;
        }

        private void txtcode_TextChanged(object sender, EventArgs e) // CHẠY MÃ ĐƠN HÀNG THEO Code
        {

        }

        private void chaymadhtheocode()//CHẠY MÃ ĐƠN HÀNG THEO Code ĐƠN HÀNG
        {

            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = Connect.mConnect;
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            //string sqlQuery = string.Format();
            //cmd = new SqlCommand(sqlQuery, con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //if (reader.Read())
            //    txtMaDonHang.Text = Convert.ToString(reader[0]);
            //reader.Close();
            //con.Close();


            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"Select TOP 1 madh From DONDATHANG where nvkd
                like N'{0}' and Code like '{1}' order by Code ASC", MainDev.username, txtDatHangID.Text);
            cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaDonHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        /// <summary>
        /// /tự động lấy dữ liệu các trường
        /// </summary>
        private void autocomple()// Complete CHI TIẾT SẢN PHẨM
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select sanpham from "
                    + " (select sanpham from tblchitietkehoach where sanpham is not null) DH union all "
                    + " (select Tenquicach as sanpham from tblDHCT)", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtQuycachchitiet.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        private void autocomplecb()// autocomplete khách hàng
        {
            //string ConString = Connect.mConnect;
            //using (SqlConnection con = new SqlConnection(ConString))
            //{
            //    SqlCommand cmd = new SqlCommand("select TenKH from tblKHACHHANG", con);
            //    con.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
            //    while (reader.Read())
            //    {
            //        Collection.Add(reader.GetString(0));
            //    }
            //    CbKhachhang.AutoCompleteCustomSource = Collection;
            //    con.Close();
            //}
        }
        private void AutoMaSanPham()// danh Mục Mã Sản phẩm
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Masp from tblSANPHAM", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    Collection.Add(reader.GetString(0));
                }
                txtmasp.AutoCompleteCustomSource = Collection;
                con.Close();
            }
        }

        private void atcghichuchitet()// ghi chú chi tiết
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select ghichu from tblDHCT", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection GCCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    GCCollection.Add(reader.GetString(0));
                }
                txtghichu.AutoCompleteCustomSource = GCCollection;
                con.Close();
            }
        }

        private void atcNgoaiquan()//ngoaiquang
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select distinct ngoaiquang from tblDHCT where ngoaiquang is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection NQCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    NQCollection.Add(reader.GetString(0));
                }
                txtngoaiquan.AutoCompleteCustomSource = NQCollection;
                con.Close();
            }
        }

        private void Binding()//Binding GridView 1
        {
            txtQuycachchitiet.DataBindings.Clear();
            txtQuycachchitiet.DataBindings.Add("text", gridControl2.DataSource, "Tenquicach");
            txtsoluong.DataBindings.Clear();
            txtsoluong.DataBindings.Add("text", gridControl2.DataSource, "Soluong");
            txtdongia.DataBindings.Clear();
            txtdongia.DataBindings.Add("text", gridControl2.DataSource, "dongia");
            txtthanhtien.DataBindings.Clear();
            txtthanhtien.DataBindings.Add("text", gridControl2.DataSource, "thanhtien");
            dpngaygiao.DataBindings.Clear();
            dpngaygiao.DataBindings.Add("text", gridControl2.DataSource, "ngaygiao");
            txtngoaiquan.DataBindings.Clear();
            txtngoaiquan.DataBindings.Add("text", gridControl2.DataSource, "ngoaiquang");
            txtsoluongngoaite.DataBindings.Clear();
            txtsoluongngoaite.DataBindings.Add("text", gridControl2.DataSource, "usd");
            txttygia.DataBindings.Clear();
            txttygia.DataBindings.Add("text", gridControl2.DataSource, "tygia");
            txtghichu.DataBindings.Clear();
            txtghichu.DataBindings.Add("text", gridControl2.DataSource, "ghichu");
        }
        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BINDING_DHCT();
            //chaymadhtheocode();//CHẠY MÃ ĐƠN HÀNG THEO CODE
        }
        private void Binding1()//Binding Gridview 2
        {

        }
        private void gridControl1_Click(object sender, EventArgs e) //Binding GridView 1
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtDatHangID.Text = gridView1.GetFocusedRowCellDisplayText(code_grid1);
            txtMaDonHang.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
            CbKhachhang.Text = gridView1.GetFocusedRowCellDisplayText(KhachHang_grid1);
            //txtnvkd.Text = gridView1.GetFocusedRowCellDisplayText(nvkd_grid1);
            cbloaiDH.Text = gridView1.GetFocusedRowCellDisplayText(loaidh_grid1);
            cbloaiKH.Text = gridView1.GetFocusedRowCellDisplayText(loaikhachhang_grid2);
            txtmapo.Text = gridView1.GetFocusedRowCellDisplayText(MaPO_grid1);
            txtghichudonhang.Text = gridView1.GetFocusedRowCellDisplayText(Diengiai_grid1);
            cbMadonhang_Xoa.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
            txtcodeview.Text = gridView1.GetFocusedRowCellDisplayText(code_grid1);
            txtgiatridonhang.Text = gridView1.GetFocusedRowCellDisplayText(Trigia_grid1);
            txttrangthaiDH.Text = gridView1.GetFocusedRowCellDisplayText(pheduyetdonhang_grid1);
            txtmapo.Text = gridView1.GetFocusedRowCellDisplayText(MaPO_grid1);
            txtDC_DatHang.Text = gridView1.GetFocusedRowCellDisplayText(Diachi_grid1);
            txtMaDonHang.Focus();
            chaymadhtheocode();//CHẠY MÃ ĐƠN HÀNG THEO CODE
            LoadDHCT_Code();
        }

        private void BINDING_DHCT()
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtGhiChuXuatKho.Text = gridView2.GetFocusedRowCellDisplayText(GhichuXuatKho_grid2);
            dpNgayLap_DH.Text = gridView2.GetFocusedRowCellDisplayText(Ngaylap_grid2);
            CbKhachhang.Text = gridView2.GetFocusedRowCellDisplayText(khachhangchitiet_grid2);
            txtMaDonHang.Text = gridView2.GetFocusedRowCellDisplayText(Madh_grid2);
            txtDonHangID.Text = gridView2.GetFocusedRowCellDisplayText(idchitietdonhang_grid2);
            txtctspview.Text = gridView2.GetFocusedRowCellDisplayText(machitiet_grid2);
            txtQuycachchitiet.Text = gridView2.GetFocusedRowCellDisplayText(tenquicach_grid2);
            txtsoluong.Text = gridView2.GetFocusedRowCellDisplayText(Soluong_grid2);
            txtdongia.Text = gridView2.GetFocusedRowCellDisplayText(dongia_grid2);
            txtthanhtien.Text = gridView2.GetFocusedRowCellDisplayText(thanhtien_grid2);
            dpngaygiao.Text = gridView2.GetFocusedRowCellDisplayText(ngaygiaohang_grid2);
            txtsoluongngoaite.Text = gridView2.GetFocusedRowCellDisplayText(ngoaite_grid2);
            txttygia.Text = gridView2.GetFocusedRowCellDisplayText(tygia_grid2);
            txtmasp.Text = gridView2.GetFocusedRowCellDisplayText(masp_grid2);
            txtngoaiquan.Text = gridView2.GetFocusedRowCellDisplayText(ngoaiquang_grid2);
            txtmau_banve.Text = gridView2.GetFocusedRowCellDisplayText(maubanve_grid2);
            txtghichu.Text = gridView2.GetFocusedRowCellDisplayText(ghichu_grid2);
            txtdonvi.Text = gridView2.GetFocusedRowCellDisplayText(donvitinh_grid2);
            cbloaiDH.Text = gridView2.GetFocusedRowCellDisplayText(loaidonhang_grid2);
            cbloaiKH.Text = gridView2.GetFocusedRowCellDisplayText(loaikhachhang_grid2);
            txtghichudonhang.Text = gridView2.GetFocusedRowCellDisplayText(diengiaidonhang_grid2);
            txttrangthaiDH.Text = gridView2.GetFocusedRowCellDisplayText(Pheduyet_grid2);
            txtTenHang_KH.Text = gridView2.GetFocusedRowCellDisplayText(TenHang_KH_grid2);
            txtMasp_KH.Text = gridView2.GetFocusedRowCellDisplayText(MaSP_KH_grid2);
            txtDiaChi_GiaoHang.Text = gridView2.GetFocusedRowCellDisplayText(Diachigiaohang_grid2);
            txtdongia.Text = gridView2.GetFocusedRowCellDisplayText(dongia_grid2);
            txtthanhtien.Text = gridView2.GetFocusedRowCellDisplayText(thanhtien_grid2);
            //LookupCheck_Masp.Text = gridView2.GetFocusedRowCellDisplayText(masp_grid2);
            khoadangnhap(); txtTenHang_KH.Focus();
            khoasuaxoa();
            mokhoa();
        }
        private string idchitietdonhang;
        private void gridControl2_Click(object sender, EventArgs e)//Binding du lieu chi tiet
        {
            //if (string.IsNullOrEmpty(txtQuycachchitiet.Text))
            //    {BINDING_DHCT();}
            //else{MessageBox.Show("Bạn chưa làm mới");}
            idchitietdonhang = gridView2.GetFocusedRowCellDisplayText(idchitietdonhang_grid2);
            if (ckCumChiTiet.Checked == true)
            {
                TheHienTinhTrangDonHangTatCaChiTiet();
            }
            else
            {
                TheHienTinhTrangDonHang();
            }
        }
        //Thể hiện tình trạng của đơn hàng
        private void TheHienTinhTrangDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
            case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
				(select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat
               where IDChiTietDonHang like N'{0}' and MaCongDoan in('BTN','GHA'))a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai order by NgayLap desc", idchitietdonhang);
            treeListProductionStagesPlan.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStagesPlan.ForceInitialize();
            treeListProductionStagesPlan.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            //treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 6f);
        }
        private void TheHienTinhTrangDonHangTatCaChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select b.SoLuongGiao,b.NgayGiao,
            case when isnull(b.SoLuongGiao,0)>=a.SoLuongYCSanXuat then N'Hoàn thành'
                 when GetDate()<BatDau and isnull(b.SoLuongGiao,0) < 1 then N'Chưa khởi động'
                 when cast(GetDate() as date)>= KetThuc and isnull(b.SoLuongGiao,0)<SoLuongYCSanXuat then N'Quá hạn'
                 when GetDate()<KetThuc and isnull(b.SoLuongGiao,0)>0 then N'Đang thực hiện'
            end TinhTrang,a.* from
				(select ID,ParentID,IDTrienKhai,IDChiTietDonHang,
               MaDonHang,MaPo,MaSanPham,
               TenSanPham,TenLoai,SoLuongDonHang,
               DonViSanPham,DonViChiTiet,SoLuongChiTietDonHang,TonKho,
               SoLuongYCSanXuat,ToThucHien,BatDau,KetThuc,MaCongDoan,
               TenCongDoan,SoThuTu,
               NgayLap,NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,NgoaiQuan,GhiChu
               from TrienKhaiKeHoachSanXuat
               where IDChiTietDonHang like N'{0}')a 
				left outer join
				(select IDTrienKhai,sum(SoGiao)SoLuongGiao,Max(NgayGiao)NgayGiao
			    from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
				where IDTrienKhai>0
			    group by IDTrienKhai
						 union all 
						 select IDTrienKhai,sum(BTPT11)SoLuongGiao,max(ngaynhan)NgayGiao from tbl11 
						 where IDTrienKhai <>''
						 group by IDtrienKhai)b
			    on a.IDTrienKhai=b.IDTrienKhai order by NgayLap desc", idchitietdonhang);
            treeListProductionStagesPlan.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStagesPlan.ForceInitialize();
            treeListProductionStagesPlan.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListProductionStagesPlan.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
            //treeListProductionStagesPlan.Appearance.Row.Font = new Font("Segoe UI", 6f);
        }
        private void LoadDHCT_Code()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select case when b.IDChiTietDonHang >0 then 'x' else '' end DaChia,CT.GhichuXuatKho, CT.Diachi_giaohang, CT.Iden, 
             CT.Codedetail, CT.Tenquicach, CT.madh, CT.Khachhang, CT.MaPo, CT.MaSP, CT.dvt, CT.Mau_banve,
             CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd,
             CT.tygia, CT.Code, CT.nguoithaydoi, CT.thoigianthaydoi, CT.pheduyet, CT.Trangthai,
             (case when CT.pheduyet = N'Đã phê duyệt' then 'True' end) Muc,CT.Tenkhachhang,
             CT.Masp_KH,CT.MaKH, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai]
             from tblDHCT CT 
			 left outer join tblDONHANG DH on CT.madh = DH.madh
			 left outer join
			 (select IDChiTietDonHang from TrienKhaiKeHoachSanXuat group by IDChiTietDonHang)b
			 on CT.Iden=b.IDChiTietDonHang
            where CT.Code = DH.Code and DH.Code like N'{0}'", txtcodeview.Text);
            gridControl2.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void txtcodeview_EditValueChanged(object sender, EventArgs e)
        {
            LoadDHCT_Code();
        }
        private bool KiemtramaDH()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaDH = txtMaDonHang.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select madh from tblDONHANG", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaDH == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        /// <summary>
        /// Ghi đơn hàng vào sổ đơn hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addmadh_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemtramaDH())
                {
                    MessageBox.Show("Mã '" + txtMaDonHang.Text + "' đã có, Không thể thêm chi tiết trùng");
                }
                else if (CbKhachhang.Text != "" && cbloaiDH.Text != "" && cbloaiKH.Text != "")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT into tblDONHANG (Diachi,MaPO,madh,Ngaydh,LoaiDH,nvkd,Khachhang,PhanloaiKH,Diengiai) "
                                                             + "values(@Diachi,@MaPO,@madh,Getdate(),@LoaiDH,@nvkd,@Khachhang,@PhanloaiKH,@Diengiai)", con))
                        {
                            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDC_DatHang.Text;
                            cmd.Parameters.Add("@MaPO", SqlDbType.NVarChar).Value = txtmapo.Text;
                            cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = txtMaDonHang.Text;
                            cmd.Parameters.Add("@LoaiDH", SqlDbType.NVarChar).Value = cbloaiDH.Text;
                            cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = MainDev.username;
                            cmd.Parameters.Add("@Khachhang", SqlDbType.NVarChar).Value = CbKhachhang.Text;
                            cmd.Parameters.Add("@PhanloaiKH", SqlDbType.NVarChar).Value = cbloaiKH.Text;
                            cmd.Parameters.Add("@Diengiai", SqlDbType.NVarChar).Value = txtghichudonhang.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close(); addmadh.Enabled = false;
                        LoadDHGHI();
                        Gol = gridView1.GetFocusedDisplayText();
                        txtDatHangID.Text = gridView1.GetFocusedRowCellDisplayText(code_grid1);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadDHGHI()
        {
            ketnoi Connect = new ketnoi();
            gridControl1.DataSource = Connect.laybang(@"select Code,Ngaydh,DH.madh,LoaiDH,nvkd,Khachhang,Diachi, MaPO, 
                                    PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, 
                                    thoigianthaydoi from tblDONHANG DH left join (select madh, sum(thanhtien) as Giatri 
                                    from tblDHCt group by madh) CT on DH.madh = CT.madh 
                                    where nvkd like N'" + MainDev.username + "' and  DH.madh like N'" + txtMaDonHang.Text + "' order by Ngaydh Desc");
            Connect.dongketnoi();
        }

        private void LoadDONDATHANG()//Load du lieu dơn đặt hàng
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select Code,Ngaydh,MADDH,LoaiDH,nvkd,Khachhang,Diachi, 
                           PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, 
                           thoigianthaydoi from tblDONHANG DH left outer join (select madh, sum(thanhtien) as Giatri 
                           from tblDHCt group by madh) CT on DH.MADDH = CT.madh 
                           where  Code like N'" + txtDatHangID.Text + "' order by Ngaydh Desc");
            kn.dongketnoi();
        }

        private void Maxcode()// lấy số lớn nhất trong danh sách
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select TOP 1 max(Code) as code From DONDATHANG where nvkd like N'" + MainDev.username + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtDatHangID.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void maxcodeview()//  LẤY MÃ CAO NHẤT XEM
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select TOP 1 max(Code) as code From DONDATHANG where nvkd like N'" + MainDev.username + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtcodeview.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void insertmadhauto()//THÊM MÃ ĐƠN HÀNG TỰ ĐỘNG
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu(" update tblDONHANG set tblDONHANG.madh=DONDATHANG.[MAĐH],tblDONHANG.MADDH=DONDATHANG.[MAĐH] from tblDONHANG,DONDATHANG  "
                        + " where tblDONHANG.Code = DONDATHANG.Code "); kn.dongketnoi();
        }
        #region Ghi chi tiết đơn hàng vào sổ chi tiết đơn hàng

        private void btaddchitiet_Click(object sender, EventArgs e)
        {
            //if (txttrangthaiDH.Text != "") { MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);}
            if (txtMaDonHang.Text == "") { MessageBox.Show("Mã đơn hàng không được bỏ trống", "Message"); return; }
            else if (CbKhachhang.Text == "") { MessageBox.Show("Khách hàng không được bỏ trống", "Message"); return; }
            else if (cbloaiDH.Text == "") { MessageBox.Show("Loại đơn hàng không được bỏ trống", "Message"); return; }
            else if (cbloaiKH.Text == "") { MessageBox.Show("Loại khách hàng không được bỏ trống", "Message"); return; }
            else if (txtQuycachchitiet.Text == "") { MessageBox.Show("Qui cách chi tiết không được bỏ trống", "Message"); return; }
            else if (txtsoluong.Text == "") { MessageBox.Show("Số lượng không được bỏ trống", "Message"); return; }
            else if (txtdongia.Text == "") { MessageBox.Show("đơn giá không được bỏ trống", "Message"); return; }
            else if (txtthanhtien.Text == "") { MessageBox.Show("Thành tiền không được bỏ trống", "Message"); return; }
            else if (txtdonvi.Text == "") { MessageBox.Show("Đơn vị không được bỏ trống", "Message"); return; }
            else if (txtmasp.Text == "") { MessageBox.Show("Mã sản phẩm không được bỏ trống", "Message"); return; }
            else if (txtDiaChi_GiaoHang.Text == "") { MessageBox.Show("Địa điểm giao hàng cho khách không được bỏ trống", "Message"); return; }

            try
            {
                {
                    SqlConnection con = new SqlConnection();
                    decimal soluong = Convert.ToDecimal(txtsoluong.Text);
                    decimal dongia = Convert.ToDecimal(txtdongia.Text);
                    decimal thanhtien = Convert.ToDecimal(txtthanhtien.Text);
                    decimal ngoaite = Convert.ToDecimal(txtsoluongngoaite.Text);
                    decimal tygia = Convert.ToDecimal(txttygia.Text);
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("insert into tblDHCT (MaPo,GhichuXuatKho,Diachi_giaohang,Code,madh,Khachhang,Tenquicach,Masp,dvt, "
                               + " Mau_banve, Soluong, dongia, thanhtien, ngaygiao,ngoaiquang,usd,tygia, ghichu,nguoithaydoi,thoigianthaydoi,Tenkhachhang,Masp_KH,Tonkho) "
                               + " values(@MaPo,@GhichuXuatKho,@Diachi_giaohang,@Code, @madh, @Khachhang, @Tenquicach,@Masp, @dvt, "
                               + " @Mau_banve, @Soluong, @dongia, @thanhtien, @ngaygiao,@ngoaiquang,@usd,@tygia, @ghichu,@nguoithaydoi,GetDate(),@Tenkhachhang,@Masp_KH,@Tonkho) ", con))
                        {
                            cmd.Parameters.Add("@MaPo", SqlDbType.NVarChar).Value = txtmapo.Text;
                            cmd.Parameters.Add("@GhichuXuatKho", SqlDbType.NVarChar).Value = txtGhiChuXuatKho.Text;
                            cmd.Parameters.Add("@Diachi_giaohang", SqlDbType.NVarChar).Value = txtDiaChi_GiaoHang.Text;
                            cmd.Parameters.Add("@Code", SqlDbType.Int).Value = txtDatHangID.Text;
                            cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = txtMaDonHang.Text;
                            cmd.Parameters.Add("@Khachhang", SqlDbType.NVarChar).Value = CbKhachhang.Text;
                            cmd.Parameters.Add("@Tenquicach", SqlDbType.NVarChar).Value = txtQuycachchitiet.Text;
                            cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                            cmd.Parameters.Add("@dvt", SqlDbType.NVarChar).Value = txtdonvi.Text;
                            cmd.Parameters.Add("@Mau_banve", SqlDbType.NVarChar).Value = txtmau_banve.Text;
                            cmd.Parameters.Add("@Soluong", SqlDbType.NVarChar).Value = soluong;
                            cmd.Parameters.Add("@dongia", SqlDbType.NVarChar).Value = dongia;
                            cmd.Parameters.Add("@thanhtien", SqlDbType.NVarChar).Value = thanhtien;
                            cmd.Parameters.Add("@ngaygiao", SqlDbType.Date).Value = dpngaygiao.Text;
                            cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtngoaiquan.Text;
                            cmd.Parameters.Add("@usd", SqlDbType.Float).Value = ngoaite;
                            cmd.Parameters.Add("@tygia", SqlDbType.Float).Value = tygia;
                            cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtghichu.Text;
                            cmd.Parameters.Add("@nguoithaydoi", SqlDbType.NVarChar).Value = MainDev.username;
                            cmd.Parameters.Add("@Tenkhachhang", SqlDbType.NVarChar).Value = txtTenHang_KH.Text;
                            cmd.Parameters.Add("@Masp_KH", SqlDbType.NVarChar).Value = txtMasp_KH.Text;
                            cmd.Parameters.Add("@Tonkho", SqlDbType.Float).Value = txtTonKho.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        updatemachitiet();
                        txtQuycachchitiet.ResetText();
                        txtsoluong.Text = "0";
                        txtdongia.Text = "0";
                        txtthanhtien.Text = "0";
                        txtdonvi.Text = "";
                        txtmasp.ResetText();
                        txtmau_banve.ResetText();
                        txtghichu.Text = "";
                        txtQuycachchitiet.Focus();
                        txtngoaiquan.ResetText();
                        LoadDHCT_Code();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công lý do" + ex, "Thông Báo");
            }
        }
        #endregion
        private void LoadDHCT_THEM()//Load Danh mục đơn hàng chi tiết theo Code
        {

            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("SELECT GhichuXuatKho,Iden,Codedetail,Tenquicach,madh,Khachhang,MaPo,MaSP,dvt, "
              + "Mau_banve, Tonkho, Soluong, dongia, thanhtien, ngaygiao, ngoaiquang, "
              + "Code, usd, tygia, ghichu, nguoithaydoi, thoigianthaydoi, pheduyet, Trangthai,(case when pheduyet = N'Đã phê duyệt' then 'TRUE' end) Muc,Tenkhachhang,Masp_KH,MaKH FROM dbo.tblDHCT "
              + "where nguoithaydoi like N'" + MainDev.username + "' and Code like '" + txtDatHangID + "'"); Connect.dongketnoi();
        }

        private void updatemachitiet()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("update tblDHCT set tblDHCT.Codedetail=INDONHANG.MACT "
                                                  + "from tblDHCT, INDONHANG  where tblDHCT.Iden = INDONHANG.Iden"); kn.dongketnoi();
        }

        private void btndelete_Click(object sender, EventArgs e)//Delete don dat hang
        {
            if (cbMadonhang_Xoa.Text != "" && MessageBox.Show("BẠN CHẮC MUỐN HỦY ĐƠN HÀNG có số Code ##'" + txtcodeview.Text + "'", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi kn = new ketnoi();
                int kq1 = kn.xulydulieu("delete tblDHCT where Code like '" + txtcodeview.Text + "'");
                if (kq1 > 0)
                {
                    MessageBox.Show("XOÁ THÀNH CÔNG", "THÔNG BÁO");
                    viewdetail_Click(sender, e);
                    btnalldonhang_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Không có chi tiết cần xóa");
                    viewdetail_Click(sender, e);
                    btnalldonhang_Click(sender, e);
                }
                kn.dongketnoi();
            }
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttrangthaiDH.Text != "")
                    MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (cbMadonhang_Xoa.Text != "" && MessageBox.Show("Bạn muốn xóa CT: '" + txtQuycachchitiet.Text + "'", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ketnoi knn = new ketnoi();
                    int kq = knn.xulydulieu("delete tblDHCT where Iden like '" + txtDonHangID.Text + "' and pheduyet is null");
                    if (kq > 1)
                    {
                        MessageBox.Show("XÓA CHI TIẾT ĐƠN HÀNG '" + cbMadonhang_Xoa.Text + "' THÀNH CÔNG", "THÔNG BÁO");
                    }
                    LoadDHCT_Code();
                    knn.dongketnoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thành công" + ex, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void save_Click(object sender, EventArgs e) // refresh cac textbox
        {
            txtDonHangID.ResetText(); txtMaDonHang.ResetText(); CbKhachhang.ResetText(); txtmapo.ResetText();
            cbloaiDH.ResetText(); cbloaiKH.ResetText(); txtQuycachchitiet.ResetText(); txtngoaiquan.ResetText(); txtghichudonhang.ResetText();
            txtsoluong.Text = "0"; txtdongia.Text = "0"; txtthanhtien.Text = "0"; txtdonvi.ResetText(); txtgiatridonhang.ResetText();
            txtmasp.ResetText(); txtmau_banve.ResetText(); txtghichu.ResetText(); txtTenHang_KH.ResetText(); txtMasp_KH.ResetText(); CbKhachhang.Enabled = true; txtmapo.Enabled = true;
            cbloaiDH.Enabled = true; cbloaiKH.Enabled = true;
            addmadh.Enabled = true;
            //UcDONDH_Load(sender, e);
        }
        private void txtdongia_TextChanged(object sender, EventArgs e)
        {
            if (txtdongia.Text == "")
            {
                txtdongia.Text = "0";
            }
            //txtdongia.Text = string.Format("{0:0,0}", float.Parse(txtdongia.Text));
            //txtdongia.SelectionStart = txtdongia.Text.Length;
            tich();
        }
        private void txtthanhtien_TextChanged(object sender, EventArgs e)
        {
            if (txtthanhtien.Text == "")
            {
                txtthanhtien.Text = "0";
            }
            txtthanhtien.Text = string.Format("{0:0,0}", decimal.Parse(txtthanhtien.Text));
            txtthanhtien.SelectionStart = txtthanhtien.Text.Length;
        }
        private void txtsoluong_TextChanged(object sender, EventArgs e)
        {

            if (txtsoluong.Text == "")
            {
                txtsoluong.Text = "0";
            }
            tich();
            //txtsoluong.Text = string.Format("{0:#,#}", decimal.Parse(txtsoluong.Text));
            //txtsoluong.SelectionStart = txtsoluong.Text.Length;
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void txtdongia_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtsoluong_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtthanhtien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void btnalldonhang_Click(object sender, EventArgs e)// show all đơn đặt hàng
        {
            DocDonDatHang();
        }

        private void viewdetail_Click(object sender, EventArgs e)// Show all chi tiết đơn hàng
        {
            DocChiTietDonHang();
        }

        private void txtgiatridonhang_TextChanged(object sender, EventArgs e)
        {
            txtgiatridonhang.Text = string.Format("{0:0,0}", decimal.Parse(txtgiatridonhang.Text));
            txtgiatridonhang.SelectionStart = txtgiatridonhang.Text.Length;
        }

        private void txtgiatridonhang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        public static string madonHang;
        private void printDH_Click(object sender, EventArgs e) // Print đơn đặt hàng
        {
            if (txtMaDonHang.Text == null) { MessageBox.Show("Chưa chọn mã đơn hàng", "Message");return; }
            madonHang = txtMaDonHang.Text;
            Function.ConnectSanXuat();//Mo ket noi
            string strQuery = string.Format(@"select * from
							(select * from INDONHANG    
							where madh like N'{0}')a
							inner join
							(select * from tblDONHANG 
							where madh like N'{0}')b
							on a.madh=b.madh", txtMaDonHang.Text);
            var dataTable = Function.GetDataTable(strQuery);
            if (dataTable.Rows.Count < 1) return;
            Rpdondathang nhapKho = new Rpdondathang();
            nhapKho.DataSource = dataTable;
            nhapKho.DataMember = "Table";
            nhapKho.CreateDocument(false);
            nhapKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(nhapKho.PrintingSystem);
            nhapKho.ShowPreviewDialog();
            Function.Disconnect();
        }
        private void PrintPSX_Click(object sender, EventArgs e) // Print phiếu sản xuất
        {
            if (txtMaDonHang.Text == null) { MessageBox.Show("Chưa chọn mã đơn hàng", "Message"); return; }
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"SELECT * from IN_LENHSANXUAT where madh like N'{0}'", txtMaDonHang.Text);
            var dataTable = Function.GetDataTable(strQuery);
            if (dataTable.Rows.Count < 1) return;
            RpPhieusanxuat rpPHIEUSANXUAT = new RpPhieusanxuat();
            rpPHIEUSANXUAT.DataSource = dataTable;
            rpPHIEUSANXUAT.DataMember = "Table";
            rpPHIEUSANXUAT.CreateDocument(false);
            rpPHIEUSANXUAT.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT.PrintingSystem);
            rpPHIEUSANXUAT.ShowPreviewDialog();
            Function.Disconnect();
        }

        private void printPSX_VT_Click(object sender, EventArgs e)
        {
            if (txtMaDonHang.Text == null) { MessageBox.Show("Chưa chọn mã đơn hàng", "Message"); return; }
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"select * from PHIEUSANXUAT where madh like N'{0}'", txtMaDonHang.Text);
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = Function.GetDataTable(strQuery);
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Function.Disconnect();
        }
        private void XatraReportCong(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();
            string strQuery = string.Format(@"select * from ViewDINHMUC_LAODONG where madh like N'{0}'", txtMaDonHang.Text);
            XtraReportDMCong XtraRPCong = new XtraReportDMCong();
            ReportPrintTool tool = new ReportPrintTool(XtraRPCong);
            XtraRPCong.DataSource = Function.GetDataTable(strQuery);
            XtraRPCong.DataMember = "Table";
            XtraRPCong.ShowPreviewDialog();
            Function.Disconnect();
        }

        private void exportDH_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void Exportsx_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }

        private void cbmadhview_SelectedIndexChanged(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select CT.GhichuXuatKho,CT.Iden,CT.Codedetail,CT.Tenquicach,CT.madh,CT.Khachhang,CT.MaSP,CT.dvt,CT.Mau_banve, 
             CT.Tonkho, CT.Soluong, CT.dongia, CT.thanhtien, CT.ngaygiao, CT.ghichu, CT.ngoaiquang, CT.usd, 
             CT.tygia, CT.Code, CT.pheduyet, DH.nvkd, DH.[LoaiDH], DH.[PhanloaiKH],DH.[Diengiai], 
             DH.[nguoithaydoi], DH.[thoigianthaydoi] from tblDHCT CT, tblDONHANG DH where CT.Code = DH.Code and CT.madh like N'" + cbMadonhang_Xoa.Text + "' and Duyetsanxuat =''");
            kn.dongketnoi();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttrangthaiDH.Text != "")
                    MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(txttrangthaiDH.Text) && txtQuycachchitiet.Text != "" && MessageBox.Show("Bạn muốn sửa CT '" + txtQuycachchitiet.Text + "'", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection();
                    decimal soluong = Convert.ToDecimal(txtsoluong.Text);
                    decimal dongia = Convert.ToDecimal(txtdongia.Text);
                    decimal thanhtien = Convert.ToDecimal(txtthanhtien.Text);
                    decimal ngoaite = Convert.ToDecimal(txtsoluongngoaite.Text);
                    decimal tygia = Convert.ToDecimal(txttygia.Text);
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("update tblDHCT set GhichuXuatKho=@GhichuXuatKho,Tenquicach =@Tenquicach,Masp=@Masp,MaPo=@MaPo,dvt=@dvt, "
                               + " Mau_banve=@Mau_banve, Soluong=@Soluong, dongia=@dongia, thanhtien=@thanhtien, "
                               + " ngaygiao=@ngaygiao,ngoaiquang=@ngoaiquang,usd=@usd,tygia=@tygia, ghichu=@ghichu, "
                               + " nguoithaydoi=@nguoithaydoi,Thoigian_Thuc=GetDate(),Tenkhachhang=@Tenkhachhang, "
                               + " Masp_KH=@Masp_KH,Diachi_giaohang=@Diachi_giaohang,Tonkho=@Tonkho where Iden like N'" + txtDonHangID.Text + "'", con))
                        {
                            cmd.Parameters.Add("@GhichuXuatKho", SqlDbType.NVarChar).Value = txtGhiChuXuatKho.Text;
                            cmd.Parameters.Add("@Tenquicach", SqlDbType.NVarChar).Value = txtQuycachchitiet.Text;
                            cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                            cmd.Parameters.Add("@MaPo", SqlDbType.NVarChar).Value = txtmapo.Text;
                            cmd.Parameters.Add("@dvt", SqlDbType.NVarChar).Value = txtdonvi.Text;
                            cmd.Parameters.Add("@Mau_banve", SqlDbType.NVarChar).Value = txtmau_banve.Text;
                            cmd.Parameters.Add("@Soluong", SqlDbType.NVarChar).Value = soluong;
                            cmd.Parameters.Add("@dongia", SqlDbType.NVarChar).Value = dongia;
                            cmd.Parameters.Add("@thanhtien", SqlDbType.NVarChar).Value = thanhtien;
                            cmd.Parameters.Add("@ngaygiao", SqlDbType.Date).Value = dpngaygiao.Text;
                            cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtngoaiquan.Text;
                            cmd.Parameters.Add("@usd", SqlDbType.Float).Value = ngoaite;
                            cmd.Parameters.Add("@tygia", SqlDbType.Float).Value = tygia;
                            cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtghichu.Text;
                            cmd.Parameters.Add("@nguoithaydoi", SqlDbType.NVarChar).Value = MainDev.username;
                            cmd.Parameters.Add("@Tenkhachhang", SqlDbType.NVarChar).Value = txtTenHang_KH.Text;
                            cmd.Parameters.Add("@Masp_KH", SqlDbType.NVarChar).Value = txtMasp_KH.Text;
                            cmd.Parameters.Add("@Diachi_giaohang", SqlDbType.NVarChar).Value = txtDiaChi_GiaoHang.Text;
                            cmd.Parameters.Add("@Tonkho", SqlDbType.Float).Value = txtTonKho.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close(); LoadDHCT_Code();
                    }
                }
                else
                {
                    MessageBox.Show("Kiểm tra nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch { MessageBox.Show("Không thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void txtsoluongngoaite_TextChanged(object sender, EventArgs e)// quy doi ngoai te
        {
            if (txtsoluongngoaite.Text == "")
            {
                txtsoluongngoaite.Text = "0";
            }
            else
            {
                txtsoluongngoaite.Text = string.Format("{0}", decimal.Parse(txtsoluongngoaite.Text));
            }
        }

        private void txttygia_TextChanged(object sender, EventArgs e) // quy doi ngoai te
        {
            if (txttygia.Text == "")
            {
                txttygia.Text = "0";
            }
            txttygia.Text = string.Format("{0:0,0}", decimal.Parse(txttygia.Text));
            txttygia.SelectionStart = txttygia.Text.Length;
            quydoingoaite();
        }

        private void btnusertaikhoan_Click(object sender, EventArgs e)
        {
            frmQLTaiKhoan taikhoan = new frmQLTaiKhoan();
            taikhoan.Show();
        }

        private void btnduyet_Click(object sender, EventArgs e)
        {

        }

        private void btnsuadonhang_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttrangthaiDH.Text != "")
                    MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (txtMaDonHang.Text != "")
                {
                    SqlConnection con = new SqlConnection();
                    decimal soluong = Convert.ToDecimal(txtsoluong.Text);
                    decimal dongia = Convert.ToDecimal(txtdongia.Text);
                    decimal thanhtien = Convert.ToDecimal(txtthanhtien.Text);
                    decimal ngoaite = Convert.ToDecimal(txtsoluongngoaite.Text);
                    decimal tygia = Convert.ToDecimal(txttygia.Text);
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("update tblDONHANG set Diachi=@Diachi,madh=@madh,LoaiDH=@LoaiDH, "
                           + " nvkd=@nvkd,MaPO=@MaPO,Khachhang=@Khachhang,PhanloaiKH=@PhanloaiKH,Diengiai=@Diengiai,Thoigian_Thuc=GetDate() where Code = '" + txtDatHangID.Text + "'", con))
                        {
                            cmd.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDC_DatHang.Text;
                            cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = txtMaDonHang.Text;
                            cmd.Parameters.Add("@LoaiDH", SqlDbType.NVarChar).Value = cbloaiDH.Text;
                            cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = MainDev.username;
                            cmd.Parameters.Add("@MaPO", SqlDbType.NVarChar).Value = txtmapo.Text;
                            cmd.Parameters.Add("@Khachhang", SqlDbType.NVarChar).Value = CbKhachhang.Text;
                            cmd.Parameters.Add("@PhanloaiKH", SqlDbType.NVarChar).Value = cbloaiKH.Text;
                            cmd.Parameters.Add("@Diengiai", SqlDbType.NVarChar).Value = txtghichudonhang.Text;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        ketnoi kn = new ketnoi();
                        gridControl2.DataSource = kn.xulydulieu("update tblDHCT set madh=N'" + txtMaDonHang.Text + "',Khachhang=N'" + CbKhachhang.Text + "',Diengiai=N'" + txtghichudonhang.Text + "', "
                          + "LoaiDH=N'" + cbloaiDH.Text + "',PhanloaiKH=N'" + cbloaiKH.Text + "',MaPo=N'" + txtmapo.Text + "' where Code like '" + txtDatHangID.Text + "'");
                        gridControl1.DataSource = kn.laybang("select Code,Ngaydh,DH.madh,LoaiDH,nvkd,MaPO,Khachhang,Diachi, "
                          + " PhanloaiKH, NgayBD, NgayKT, CT.Giatri, NgayGH, HanTT, Diengiai, Duyetsanxuat, nguoithaydoi, "
                          + " thoigianthaydoi from tblDONHANG DH left outer join (select madh, sum(thanhtien) as Giatri "
                          + " from tblDHCt group by madh) CT on DH.MADDH = CT.madh "
                          + " where  Code like N'" + txtDatHangID.Text + "' order by Ngaydh Desc");
                        kn.dongketnoi();
                        LoadDHCT_Code();
                    }
                }
            }
            catch { MessageBox.Show("Không thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnhuydonhang_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttrangthaiDH.Text == "Đã phê duyệt")
                    MessageBox.Show("Đơn hàng Đã duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (txttrangthaiDH.Text != "Đã phê duyệt" && MessageBox.Show("Bạn muốn hủy đơn hàng '" + txtMaDonHang.Text + "'", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ketnoi ConnectDH = new ketnoi();
                    gridControl1.DataSource = ConnectDH.xulydulieu("delete tblDONHANG where Code like '" + txtDatHangID.Text + "' and Duyetsanxuat is null");
                    ketnoi ConnectCT = new ketnoi();
                    gridControl2.DataSource = ConnectCT.xulydulieu("delete tblDHCT where Code like '" + txtDatHangID.Text + "' and pheduyet is null");
                    ConnectDH.dongketnoi();
                }
                DocDonDatHang();
                DocChiTietDonhangTheoNgay();
            }
            catch { MessageBox.Show("Không thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        private void txtnvkd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtmasp_TextChanged(object sender, EventArgs e)
        {
            //TenSP_MaSP();
        }
        private void TenSP_MaSP()//định nghĩa tên sản phẩm theo mã sản phẩm
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select ISNULL(Tensp,'')+' ('+ISNULL(Kichthuoc,'')+')' as TenQuiCach,Masp from tblSANPHAM where Masp like N'" + txtmasp.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtQuycachchitiet.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }

        private void DNMaKH()//Load MaKH theo tên khách hàng
        {
            string sqlQuery = string.Format(@"select MKH from tblKHACHHANG where TenKH like N'{0}'",
                CbKhachhang.Text);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaKH.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btndmkhachhang_Click(object sender, EventArgs e)
        {
            frmThem_KH ThemKH = new frmThem_KH();
            ThemKH.ShowDialog();
        }

        private void txtkhachhang_TextChanged(object sender, EventArgs e)
        {
            DNMaKH();
        }

        private void txtngoaiquan_TextChanged(object sender, EventArgs e)
        {
            //txtghichu.Text = txtngoaiquan.Text;
        }

        private void btnrefresh_fomload_Click(object sender, EventArgs e)
        {
            DocGoiYMaDonDatHang();
        }
        private void SELECT_MASP()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Masp from tblSANPHAM where ISNULL(Tensp,'')+' ('+ISNULL(Kichthuoc,'')+')' like N'" + txtQuycachchitiet.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtmasp.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void CbQuycachchitiet_SelectedIndexChanged(object sender, EventArgs e)
        { SELECT_MASP(); }

        private void AutoTenSanPham()
        {

        }

        private void txtCheck_Masp_TextChanged(object sender, EventArgs e)//Look sản phẩm theo mã sản phẩm
        {
            LookSanpham();
        }

        private void LookupCheck_Masp_EditValueChanged(object sender, EventArgs e)
        {
            Gol = glKiemTraMaSanPham.GetFocusedDisplayText();
            txtQuycachchitiet.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtTenHang_KH.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtmasp.Text = LookupCheck_Masp.Text;
            txtMasp_KH.Text = LookupCheck_Masp.Text;
            //LoadDinhMucLaoDong();
        }

        private void btnbup_Click(object sender, EventArgs e)
        {
            txtTenHang_KH.Text = txtQuycachchitiet.Text;
        }
        private void btnDowMaspKH_Click(object sender, EventArgs e)
        {
            txtQuycachchitiet.Text = string.Format(@"{0} {1}", this.txtQuycachchitiet.Text, this.txtMasp_KH.Text);
            txtTenHang_KH.Text = string.Format(@"{0} {1}", this.txtTenHang_KH.Text, this.txtMasp_KH.Text);
        }
        Path path = new Path();
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", path.pathbanve, txtmasp.Text);
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
            frmLoading f2 = new frmLoading(txtmasp.Text, path.pathbanve);
            f2.Show();
        }
        private void THEM_MaspKH()//Chuyển mã sản phẩm khách hàng xuống tên sản phẩm
        {
            string APMA_SPKH_QCTT = string.Format(@"{0} {1}", this.txtQuycachchitiet.Text, this.txtMasp_KH.Text);
            string APMA_SPKH_TENKH = string.Format(@"{0} {1}", this.txtTenHang_KH.Text, this.txtMasp_KH.Text);
            txtQuycachchitiet.Text = APMA_SPKH_QCTT.ToString(); txtTenHang_KH.Text = APMA_SPKH_TENKH.ToString();
        }
        private void SK_THEMMASPKH(object Sender, EventArgs e) { THEM_MaspKH(); }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        { }

        private void btndowTenKH_Tengoi_Click(object sender, EventArgs e)
        { txtQuycachchitiet.Text = txtTenHang_KH.Text; }

        private void txtQuycachchitiet_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select MaSP from tblDHCT where Tenquicach  like N'" + txtQuycachchitiet.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtmasp.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }

        private void btmMtg(object sender, EventArgs e)
        {
            frmThemCongDoan fMtg = new frmThemCongDoan();
            fMtg.ShowDialog();
            LoadDinhMucLaoDong();
        }
        private void LoadDinhMucLaoDong()
        {
            ketnoi kn = new ketnoi();
            treeListProductionStagesPlan.DataSource =
                kn.laybang("select * from tblDMuc_LAODONG where Masp like N'" + LookupCheck_Masp.Text + "' ");
            kn.dongketnoi();
        }
        private void LoadDinhMucLaoDongAll(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            treeListProductionStagesPlan.DataSource = kn.laybang("select * from tblDMuc_LAODONG");
            kn.dongketnoi();
        }

        private void txtsoluongngoaite_TextChanged_1(object sender, EventArgs e)//Ty gia ngoai te
        {
            quydoingoaite();
        }

        private void txttygia_TextChanged_1(object sender, EventArgs e)
        {
            if (txttygia.Text == "")
            {
                txttygia.Text = "0";
            }
            txttygia.Text = string.Format("{0:0,0}", decimal.Parse(txttygia.Text));
            txttygia.SelectionStart = txttygia.Text.Length;
            quydoingoaite();
        }

        private void BtnKhokhuon_Click(object sender, EventArgs e)//FORM TIM KHUON
        {
            KhuonMau.maSP = LookupCheck_Masp.Text;
            KhuonMau VTKhuon = new KhuonMau();
            VTKhuon.ShowDialog();
        }

        private void BtnKhovattu_Click(object sender, EventArgs e)//FORM KHO VAT TU
        {
            frmPrVatTu fVatTu = new frmPrVatTu();
            fVatTu.ShowDialog();
        }

        private void BtnKhotp_Click(object sender, EventArgs e)//KHO THANH PHAM
        {
            frmDM_HangTon frmHangTon = new frmDM_HangTon();
            frmHangTon.ShowDialog();
        }

        private void BtnPrintDT_Nangluc_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("SELECT * from IN_LENHSANXUAT where Iden like N'" + txtDonHangID.Text + "'");
            XRDieuTraNangLuc XRDieuTraNangLuc = new XRDieuTraNangLuc();
            XRDieuTraNangLuc.DataSource = dt;
            XRDieuTraNangLuc.DataMember = "Table";
            XRDieuTraNangLuc.CreateDocument(false);
            XRDieuTraNangLuc.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(XRDieuTraNangLuc.PrintingSystem);
            XRDieuTraNangLuc.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void txtSoLuonDonHangTrongNgay_TextChanged(object sender, EventArgs e)
        {
            TaoMoiMaDonHang();
        }
        private void TaoMoiMaDonHang()
        {
            if (txtSoLuonDonHangTrongNgay.Text == "")
            {
                txtSoLuonDonHangTrongNgay.Text = "01";
            }
            if (txtSoBuHang.Text == "")
            {
                txtSoBuHang.Text = "01";
            }
            string soLuongDonHang = "0" + txtSoLuonDonHangTrongNgay.Text;
            string soBuHang = "0" + txtSoBuHang.Text;
            string newSoLuongDonHang = soLuongDonHang.Substring(soLuongDonHang.Length - 2, 2);
            string newSoBuHang = soBuHang.Substring(soBuHang.Length - 2, 2);
            if (txtSoLuonDonHangTrongNgay.TextLength > 2)
            {
                MessageBox.Show("Bạn nhập quá ba số vui lòng nhập số từ 01-99", "Message");
                txtSoLuonDonHangTrongNgay.Text = "01";
                return;
            }
            if (txtSoBuHang.TextLength > 2)
            {
                MessageBox.Show("Bạn nhập quá ba số vui lòng nhập số từ 01-10", "Message");
                txtSoBuHang.Text = "01";
                return;
            }
            if (txtSoLuonDonHangTrongNgay.Text != "" && cbMaBuHang.Text == "")
            {
                txtMaDonHang.Clear();
                DocGoiYMaDonDatHang();
                txtMaDonHang.Text = txtMaDonHang.Text + "-" + newSoLuongDonHang;
            }
            if (txtSoLuonDonHangTrongNgay.Text != "" && cbMaBuHang.Text != "")
            {
                txtMaDonHang.Clear();
                DocGoiYMaDonDatHang();
                txtMaDonHang.Text = txtMaDonHang.Text + "-" + newSoLuongDonHang + "-" + cbMaBuHang.Text;
            }
            if (txtSoLuonDonHangTrongNgay.Text != "" && cbMaBuHang.Text != "" && cbMaBuHang.Text != "SX" && txtSoBuHang.Text != "")
            {
                txtSoBuHang.ReadOnly = false;
                txtMaDonHang.Clear();
                DocGoiYMaDonDatHang();
                txtMaDonHang.Text = txtMaDonHang.Text + "-" + newSoLuongDonHang + "-" + cbMaBuHang.Text + newSoBuHang;
            }
            if (cbMaBuHang.Text == "SX")
            {
                txtSoBuHang.ReadOnly = true;
            }
        }
        private void txtSoLuonDonHangTrongNgay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void cbMaBuHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtSoLuonDonHangTrongNgay.Text = "01";
            if (txtSoLuonDonHangTrongNgay.Text == "")
            {
                MessageBox.Show("Vui lòng cung cấp số đơn hàng của ngày", "Message"); return;
            }
            else
            {
                string loaiDH = cbMaBuHang.Text;
                switch (loaiDH)
                {
                    case "SX":
                        lbTheHienBuHang.Text = "Sản xuất";
                        break;
                    case "BH":
                        lbTheHienBuHang.Text = "Bù hàng";
                        break;
                    case "TT":
                        lbTheHienBuHang.Text = "Thay thế";
                        break;
                    case "DT":
                        lbTheHienBuHang.Text = "Dự trữ";
                        break;
                    case "MU":
                        lbTheHienBuHang.Text = "Hàng mẫu";
                        break;
                    case "NB":
                        lbTheHienBuHang.Text = "Nội bộ";
                        break;
                    default:
                        MessageBox.Show("Không hợp lệ", "Message");
                        break;
                }
                TaoMoiMaDonHang();
            }
        }

        private void ckChinhSuaMaDonHang_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChinhSuaMaDonHang.Checked == true && txtMaDonHang.Text != "")
            {
                txtMaDonHang.ReadOnly = false;
            }
            else
            {
                txtMaDonHang.ReadOnly = true;
            }
        }
        private void AddItemCBMaBuHang()
        {
            cbMaBuHang.DisplayMember = "Text";
            cbMaBuHang.ValueMember = "Value";
            var items = new[]
           {new { Text = "SX", Value = "SX"},
            new { Text = "BH", Value = "BH"},
            new { Text = "TT", Value = "TT"},
            new { Text = "MU", Value = "MU"},
            new { Text = "DT", Value = "DT"},
            new { Text = "NB", Value = "NB" }};
            cbMaBuHang.DataSource = items;
        }

        private void txtSoBuHang_TextChanged(object sender, EventArgs e)
        {
            TaoMoiMaDonHang();
        }

        private void txtSoBuHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnBaoCaoTinhHinhDonHang_Click(object sender, EventArgs e)
        {
            treeListProductionStagesPlan.ShowPrintPreview();
        }

        private void LookupCheck_Masp_Click(object sender, EventArgs e)
        {
            Gol = glKiemTraMaSanPham.GetFocusedDisplayText();
            txtQuycachchitiet.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtmasp.Text = LookupCheck_Masp.Text;
            txtMasp_KH.Text = LookupCheck_Masp.Text;
            txtTenHang_KH.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
        }

        private void LookupCheck_Masp_EditValueChanged(object sender, KeyEventArgs e)
        {
            Gol = glKiemTraMaSanPham.GetFocusedDisplayText();
            txtQuycachchitiet.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtTenHang_KH.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtmasp.Text = LookupCheck_Masp.Text;
            txtMasp_KH.Text = LookupCheck_Masp.Text;
        }

        private void LookupCheck_Masp_EditValueChanged(object sender, KeyPressEventArgs e)
        {
            Gol = glKiemTraMaSanPham.GetFocusedDisplayText();
            txtQuycachchitiet.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtTenHang_KH.Text = glKiemTraMaSanPham.GetFocusedRowCellDisplayText(Tensp_look);
            txtmasp.Text = LookupCheck_Masp.Text;
            txtMasp_KH.Text = LookupCheck_Masp.Text;
        }
        string FormatMoney(object money)
        {
            string str = money.ToString();
            string pattern = @"(?<a>\d*)(?<b>\d{3})*";
            Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
            StringBuilder sb = new StringBuilder();
            foreach (Capture i in m.Groups["b"].Captures)
            {
                sb.Insert(0, "," + i.Value);
            }
            sb.Insert(0, m.Groups["a"].Value);
            return sb.ToString().Trim(',');
        }
        private void txtsoluong_KeyUp(object sender, KeyEventArgs e)
        {
            string str = txtsoluong.Text;
            int start = txtsoluong.Text.Length - txtsoluong.SelectionStart;
            str = str.Replace(",", "");
            txtsoluong.Text = FormatMoney(str);
            txtsoluong.SelectionStart = -start + txtsoluong.Text.Length;
        }
        private void txtdongia_KeyUp(object sender, KeyEventArgs e)
        {
            string str = txtdongia.Text;
            int start = txtdongia.Text.Length - txtdongia.SelectionStart;
            str = str.Replace(",", "");
            txtdongia.Text = FormatMoney(str);
            txtdongia.SelectionStart = -start + txtdongia.Text.Length;
        }

        private void btnLenhSanXuatDaKyCamKet_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtMaDonHang.Text, path.pathkinhdoanh);
            f2.Show();
        }

        private void btnNhuCauNhanCong_Click(object sender, EventArgs e)
        {
            frmNhuCauNhanCong nhanCong = new frmNhuCauNhanCong();
            nhanCong.ShowDialog();
        }

        private void btnThemDanhGiaKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHangTinhHuong dgKH = new frmKhachHangTinhHuong();
            dgKH.ShowDialog();
            DM_KHACHHANG();
        }
    }
}
