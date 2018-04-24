using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class frmChiTietSanXuat : DevExpress.XtraEditors.XtraForm
    {
        private string maDonHang;
        private string maSanPham;
        private string tenSanPham;
        private string userName;
        private string donHangID;
        private string keHoachID;
        private string soLuong;
        private string thongBao;
        private string tableID;
        public frmChiTietSanXuat(string maDonHang, string maSanPham, string tenSanPham,
            string userName, string donHangID,
            string keHoachID, string soLuong, string thongBao, string tableID)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
            this.maSanPham = maSanPham;
            this.tenSanPham = tenSanPham;
            this.userName = userName;
            this.donHangID = donHangID;
            this.keHoachID = keHoachID;
            this.soLuong = soLuong;
            this.thongBao = thongBao;
            this.tableID = tableID;
        }
        #region formload Dọc form chi tiết sản xuất - hứa hẹn ngày về vật tư - thực tế nhập kho vật tư - Khuôn gá kiểm tra sản xuất
        private void frmChiTietSanXuat_Load(object sender, EventArgs e)
        {
            txtNguoiDung.Text = userName;
            txtMaDonHang.Text = maDonHang;
            txtMaSanPham.Text = maSanPham;
            txtSanPham.Text = tenSanPham;
            txtDonHangID.Text = donHangID;
            txtKeHoachID.Text = keHoachID;
            txtSoLuong.Text = soLuong;
            lbThongBao.Text = thongBao;
            txtTableID.Text = tableID;
            txtMaDH_NguyenCong.Text = maDonHang;
            txtmaSanPhamInNguyenCong.Text = maSanPham;
            txtSoLuong_NguyenCong.Text = soLuong;
            //DSGioLenMay();
            //DSVatTu();
            //DSKhuon();
            XRNguyenCong.maDonHang = txtMaDonHang.Text;
            XRNguyenCong.soLuongDonHang = txtSoLuong.Text;
            ToThucHien_NguyenCong();
            GiaoNhanfield();
            SoGiaoHang();
            ChiTietNguyenCong();
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //DocKeHoachVatTu();
            //DocNhapKhoVatTu();
            //DocKhuonMauSanXuat();
            //DocPhuKienVatTu();
        }
        #endregion
        private void DocPhuKienVatTu()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select idPSX,IDSP,sanpham,soluongsx,
                    HanDen,ToGiaoTruoc,SoHoanThanh
                    from calenderdp_dauvaohanpat_func() 
                    where Handen is not null and idPSX = '{0}'",this.donHangID);
            gridControl1.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        void ChiTietNguyenCong()
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang(@"select Masp,Tensp,Tencondoan,TrungCongDoan,
                Tothuchien,id,Dinhmuc,'" + txtSoLuong_NguyenCong.Text+"' SoluongSanXuat from tblDMuc_LaoDong where Masp='" + txtmaSanPhamInNguyenCong.Text+"'");
            kn.dongketnoi();
        }
        private void DocKeHoachVatTu()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlStr = string.Format(@"select OSTag from VatTuKeHoachGruop_fuc('{0}')", txtDonHangID.Text);
                cmd = new SqlCommand(sqlStr, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtVatTuKeHoach.Text = reader.GetString(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        private void DocNhapKhoVatTu()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlStr = string.Format(@"select OSTag from VatTuNhapKhoGroup_func('{0}')",
                    txtDonHangID.Text);
                cmd = new SqlCommand(sqlStr, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtThuTeNhapKho.Text = reader.GetString(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        private void DocKhuonMauSanXuat()
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    string sqlStr = string.Format(@"select OSTag from KhuonSanPhamGroup_func() 
            //        where MaSanPham like '{0}'", txtMaSanPham.Text);
            //    cmd = new SqlCommand(sqlStr, con);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        reader.Read();
            //        txtKhuonGaSanXuat.Text = reader.GetString(0);
            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi lý do:" + ex.Message);
            //}


        }


        void SelectDateNgayTrienKhai()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlStr = string.Format(@"select Ngaytrienkhai from tblchitietkehoach
                    where madh ='{0}' and soluongsx='{1}'", maDonHang, soLuong);
                cmd = new SqlCommand(sqlStr, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtVatTuKeHoach.Text = reader.GetString(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        void ToThucHien_NguyenCong()
        {
            ketnoi kn = new ketnoi();
            cbToThucHien_NguyenCong.DataSource = kn.laybang(@"select distinct Tothuchien from tblDMuc_LaoDong 
                where Tothuchien !='' order by Tothuchien DESC");
            cbToThucHien_NguyenCong.DisplayMember = "Tothuchien";
            cbToThucHien_NguyenCong.ValueMember = "Tothuchien";
            kn.dongketnoi();
        }
        
        #region Lấy MaView,Ma_bophan,fieldSL,fieldTL,Matable from tblPHONGBAN
        private void GiaoNhanfield()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand(@"select fieldSL,fieldTL,CapNhatHT 
                    from tblPHONGBAN where Matable like N'" + txtTableID.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtfieldSL.Text = reader.GetString(0);
                    txtfieldTL.Text = reader.GetString(1);
                    txtUpdateProc.Text = reader.GetString(2);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        #endregion
    
        private void DSGioLenMay()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select HeSoDinhMuc,CongDoan,TG_DUTINH,Thoigian_Dinhmuc,
                    GioBatDau,BatDau,KetThucDuTinh,GioKetThuc,KetThuc,
                    NguoiCapNhat,NguoiTao,NgayCapNhat,NgayTao,ngayThucTe
                    from SoGhiTG_SanXuat where DonHangID like '" + txtDonHangID.Text+"'");
            kn.dongketnoi();
        }

        //private void DSVatTu()
        //{
        //    ketnoi kn = new ketnoi();
        //    gridControl3.DataSource = kn.laybang(@"select Ten_vattu,SL_vattucan,SL_vattumua,
        //            Thucnhap,Ngaynhap,Donvi_vattu,SubQty 
        //            from PHANTICH_TIENDOVATTU where iden like '" + txtDonHangID.Text+"'");
        //    kn.dongketnoi();
        //}
        //private void DSKhuon()
        //{
        //    ketnoi kn = new ketnoi();
        //    gridControl1.DataSource = kn.laybang(@"select Ma_khuon,Ten_khuon,Ghichu,Nguoilap,Ngaylap 
        //            from tblDM_KHUON where Masp like '"+txtMaSanPham.Text+"'");
        //    kn.dongketnoi();
        //}

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DateTime batDau = DateTime.Parse(dpTu.Text);
            DateTime ketThuc = DateTime.Parse(dpDen.Text);
            string maDH = txtMaDH_NguyenCong.Text;
            string maSanPham = txtMaSanPham.Text;
            double soLuongSanXuat = double.Parse(txtSoLuong_NguyenCong.Text);
            string toThucHien = cbToThucHien_NguyenCong.Text;
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select * from XuatCongDoan_func('"+batDau+"','"+ketThuc+"',N'"+maDH+"',N'"+maSanPham+"',N'"+toThucHien+"','"+ soLuongSanXuat + "')");
            XRNguyenCong nguyenCong = new XRNguyenCong();
            nguyenCong.DataSource = dt;
            nguyenCong.DataMember = "Table";
            nguyenCong.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void SoGiaoHang()
        {
            ketnoi kn = new ketnoi();
            string sqlStr =string.Format(@"select donvi,chitietsanpham,IDSP,Num,ngaynhan," + txtfieldSL.Text + " BTP," + txtfieldTL.Text + " TRONGLUONG,MaSQL from " + txtTableID.Text + " where IDSP like '" + txtKeHoachID.Text + "'");
            gridControl2.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void Pl_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick brick1 = e.Graph.DrawString(txtMaSanPham.Text, Color.Black,
            new RectangleF(0, 0, 620, 20), DevExpress.XtraPrinting.BorderSide.None);
            brick1.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
            brick1.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            TextBrick brick2 = e.Graph.DrawString(txtSanPham.Text, Color.Black,
            new RectangleF(40, 40, 620, 20), DevExpress.XtraPrinting.BorderSide.None);
            brick2.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            brick2.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
        }

        private void cbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
   
        }

        private void btnSubmitDonHang_Click(object sender, EventArgs e)
        {
            txtNguoiDung.Text = userName;
            txtMaDonHang.Text = maDonHang;
            txtMaSanPham.Text = maSanPham;
            txtSanPham.Text = tenSanPham;
            txtDonHangID.Text = donHangID;
            txtKeHoachID.Text = keHoachID;
            txtSoLuong.Text = soLuong;
            lbThongBao.Text = thongBao;
            txtTableID.Text = tableID;
            //DSGioLenMay();
            //DSVatTu();
            //DSKhuon();
            XRNguyenCong.maDonHang = txtMaDonHang.Text;
            XRNguyenCong.soLuongDonHang = txtSoLuong.Text;
            ToThucHien_NguyenCong();
         
            GiaoNhanfield();
            SoGiaoHang();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoLuongGiao.Text))
            {
                MessageBox.Show("Số lượng giao không được bỏ trống."); return ;
            }
            if (string.IsNullOrEmpty(txtTrongLuongGiao.Text))
            {
                MessageBox.Show("Trọng lượng không được bỏ trống."); return;
            }
            else
            {
            try { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("insert into "+txtTableID.Text+" (IDSP,ngaynhan,"+txtfieldSL.Text+","+txtfieldTL.Text+ ",DonHangID, MaSQL,chitietsanpham,donvi) values(@IDSP,GetDate(), @BTP, @TrongLuong, @DonHangID, @MaSQL,@chitietsanpham,@donvi)", con);
            cmd.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.NVarChar)).Value = txtKeHoachID.Text;
            cmd.Parameters.Add(new SqlParameter("@BTP", SqlDbType.Float)).Value = double.Parse(txtSoLuongGiao.Text);
            cmd.Parameters.Add(new SqlParameter("@TrongLuong", SqlDbType.Float)).Value = double.Parse(txtTrongLuongGiao.Text);
            cmd.Parameters.Add(new SqlParameter("@DonHangID", SqlDbType.NVarChar)).Value = txtDonHangID.Text;
            cmd.Parameters.Add(new SqlParameter("@MaSQL", SqlDbType.NVarChar)).Value = txtNguoiDung.Text;
            cmd.Parameters.Add(new SqlParameter("@chitietsanpham", SqlDbType.NVarChar)).Value = txtChiTietPhuKien.Text;
            cmd.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtDonViChiTiet.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            con.Close();
            UpdateSoLuongSanXuatQuaKeHoach();
            SoGiaoHang();
            }
            catch(Exception ex) { MessageBox.Show("Error:" + ex.Message); }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoLuongGiao.Text))
            {
                MessageBox.Show("Số lượng giao không được bỏ trống."); return;
            }
            if (string.IsNullOrEmpty(txtTrongLuongGiao.Text))
            {
                MessageBox.Show("Trọng lượng không được bỏ trống."); return;
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("update " + txtTableID.Text + " set IDSP=@IDSP," + txtfieldSL.Text + "=@BTP," + txtfieldTL.Text + "=@TrongLuong,DonHangID=@DonHangID, MaSQL=@MaSQL WHERE Num ='" + txtSoGiaoHangID.Text + "'", con);
                    cmd.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.BigInt)).Value = txtKeHoachID.Text;
                    cmd.Parameters.Add(new SqlParameter("@BTP", SqlDbType.Int)).Value = double.Parse(txtSoLuongGiao.Text);
                    cmd.Parameters.Add(new SqlParameter("@TrongLuong", SqlDbType.Float)).Value = double.Parse(txtTrongLuongGiao.Text);
                    cmd.Parameters.Add(new SqlParameter("@DonHangID", SqlDbType.BigInt)).Value = txtDonHangID.Text;
                    cmd.Parameters.Add(new SqlParameter("@MaSQL", SqlDbType.NVarChar)).Value = txtNguoiDung.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    con.Close();
                    UpdateSoLuongSanXuatQuaKeHoach();
                    SoGiaoHang();
                }
                catch (Exception ex) { MessageBox.Show("Error:" + ex.Message); }
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            CapNhatSoLuongVeKhong();
            UpdateSoLuongSanXuatQuaKeHoach();
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("DELETE from "+txtTableID.Text+" Where Num ='" + txtSoGiaoHangID.Text + "'");      
            kn.dongketnoi();
            SoGiaoHang();
        }
        private void CapNhatSoLuongVeKhong()
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("UPDATE "+txtTableID.Text+" SET " + txtfieldSL.Text + " = 0," + txtfieldTL.Text + " = 0  Where Num ='" + txtSoGiaoHangID.Text + "'");
            kn.dongketnoi();
        }
        private void UpdateSoLuongSanXuatQuaKeHoach()
        {
            try
            {
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(txtUpdateProc.Text, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@KeHoachID", SqlDbType.Float)).Value = txtKeHoachID.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtSoGiaoHangID.Text = gridView2.GetFocusedRowCellDisplayText(SoGiaoHang_grid2);
            txtSoLuongGiao.Text= gridView2.GetFocusedRowCellDisplayText(SoLuongGiao_grid2);
            txtTrongLuongGiao.Text= gridView2.GetFocusedRowCellDisplayText(trongLuongGiao_grid2);
            txtChiTietPhuKien.Text = gridView2.GetFocusedRowCellDisplayText(chitiet_grid2); ;
            txtDonViChiTiet.Text = gridView2.GetFocusedRowCellDisplayText(donvi_grid2); ;
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void btnDocDS_CongDoan_DonHang_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView5.GetFocusedDisplayText();
            cbToThucHien_NguyenCong.Text = gridView5.GetFocusedRowCellDisplayText(toThucHien_col5);
        }

        private void btnCancel_form_Click(object sender, EventArgs e)
             => this.Dispose();

        private void btnKeHoachVatTu_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + txtMaDonHang.Text + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaDonHang.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();
        }

        private void btnSoNhapKho_Click(object sender, EventArgs e)
        {

        }

        private void btnSoKhuon_Click(object sender, EventArgs e)
        {
            DocDSKhuonSanPham();
        }
        private void DocDSKhuonSanPham()
            {
                ketnoi kn = new ketnoi();
                string sqlStr = string.Format(@"select k.*,v.ViTri,v.NoiMuon,
                v.NguoiMuon,NgayMuon from SanPhamKhuon k left outer join
                (select max(ID)ID,max(NgayMuon)NgayMuon,MaKhuon,
				max(NoiMuon)NoiMuon,max(NguoiMuon)NguoiMuon,
                max(ViTri)ViTri
                from tblKhuon_Xuat_Nhap
                group by MaKhuon)v
                on k.MaKhuon=v.MaKhuon");
                grcKhuonSanPham.DataSource = kn.laybang(sqlStr);
                kn.dongketnoi();
            }

        private void btnTraCuuPhuKienSanPham_Click(object sender, EventArgs e)
        {
            DocPhuKienVatTu();
        }
    }
}