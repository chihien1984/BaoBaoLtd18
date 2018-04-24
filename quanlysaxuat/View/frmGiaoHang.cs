using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmGiaoHang : Form
    {
        public frmGiaoHang(string idtrienkhai, string parentid, string childid,
            string masanpham, string tensanpham, string machitiet, string tenchitiet,
            string sochitiet, string congdoan, string tothuchien, string idchitietdonhang)
        {
            this.idchitietdonhang = idchitietdonhang;
            this.idtrienkhai = idtrienkhai;
            this.parentid = parentid;
            this.childid = childid;
            this.masanpham = masanpham;
            this.tensanpham = tensanpham;
            this.machitiet = machitiet;
            this.tenchitiet = tenchitiet;
            this.sochitiet = sochitiet;
            this.congdoan = congdoan;
            this.tothuchien = tothuchien;
            InitializeComponent();
        }
        private string idchitietdonhang;
        private string idtrienkhai;
        private string parentid;
        private string childid;
        private string masanpham;
        private string tensanpham;
        private string machitiet;
        private string tenchitiet;
        private string sochitiet;
        private string ngayghi;
        private string tothuchien;
        private string congdoan;

        #region formload
        private void frmGiaoHang_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtCongDoanThucHien.Text = congdoan;
            txtToThucHien.Text = tothuchien;
            txtMaSanPham.Text = masanpham;
            txtSanPham.Text = tensanpham;
            txtMaChiTiet.Text = machitiet;
            txtTenChiTiet.Text = tenchitiet;
            DocToGiaoDen();//Đọc tổ giao đến vào combobox
            DieuKienGhi();//Kiểm tra diều kiện để ghi dữ liệu nếu đúng là bộ phận đăng nhập cho ghi sửa xóa ngược lại khóa ẩn đi
            txtSoLuongGiao.Focus();
            TheHienChiTietSoLuongGiaoTheoIDTrienKhai();//Thể hiện chi tiết số lượng
                                                       //DieuKienSuaXoa();//ràng buộc sửa xóa
            TheHienDanhSachCacChiTietCungLoai();//Thể hiện danh sách các chi tiết cùng loại
            gvChiTietCungLoai.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvSoChiTietGiaoHang.Appearance.Row.Font = new Font("Segoe UI", 7f);
            gvChiTietCungLoai.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #endregion
        private void txtToThucHien_TextChanged(object sender, EventArgs e)
        {
            DieuKienGhi();
        }


        //Ràng buộc điều kiện ghi dữ liệu
        private void DieuKienGhi()
        {
            if (tothuchien.Trim() == Login.dePartMent.Trim() || Login.role == "1")
            {
                lbThongBao.Text = "Bạn đã chọn đúng. Bạn có quyền cập nhật số lượng giao hàng.";
                lbThongBao.ForeColor = System.Drawing.Color.Green;
                btnGhi.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnTraCuuTatCa.Visible = true;
            }
            else
            {
                lbThongBao.Text = "Tên đăng nhập và tổ sản xuất chưa trùng khớp.";
                lbThongBao.ForeColor = System.Drawing.Color.Red;
                btnGhi.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnTraCuuTatCa.Enabled = false;
            }
        }
        //Ghi số lượng giao hàng vào sổ chi tiết giao hàng TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
        //tổng cộng số lượng chi tiết ghi vào sổ kế hoạch triển khai - TrienKhaiKeHoachSanXuat
        private void btnCapNhatSoLuongGiaoHang_Click(object sender, EventArgs e)
        {
            InsertQualitySuccess();
        }
        private void InsertQualitySuccess()
        {
            double.Parse(txtSoLuongGiao.Text.Replace(".", ""));
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sqlQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
					(NgayGiao,SoGiao,ToThucHien,
					NguoiGhiGiao,ToNhan,DienGiai,
					IDChiTietDonHang,IDTrienKhai,NgayGhiGiao) values
					('{0}','{1}',N'{2}',
					N'{3}',N'{4}',N'{5}','{6}','{7}',GetDate())",
                    dpNgayGiaoHang.Value.ToString("MM-dd-yyyy"),
                   double.Parse(txtSoLuongGiao.Text.Replace(".", "")), txtToThucHien.Text,
                    Login.Username, cbToGiaoDen.Text, txtGhiChu.Text,
                    idchitietdonhang, idtrienkhai);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                //CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
                //CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                //CapNhatThanhPhamLapGhep();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                //CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                MessageBox.Show("Success", "Message");
                this.Close();
                TheHienChiTietSoLuongGiaoTheoIDTrienKhai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Message");
                return;
            }
        }
        private void DocToGiaoDen()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from TrienKhaiKeHoachSanXuat
				  where IDChiTietDonHang like '{0}' and ToThucHien <>''
                  group by ToThucHien", idchitietdonhang, txtToThucHien.Text);
            cbToGiaoDen.DataSource = kn.laybang(sqlQuery);
            cbToGiaoDen.DisplayMember = "ToThucHien";
            cbToGiaoDen.ValueMember = "ToThucHien";
            kn.dongketnoi();

        }
        private void CapNhatSoLuongTong()//tổng cộng số lượng chi tiết ghi vào sổ kế hoạch triển khai - TrienKhaiKeHoachSanXuat
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat
						 set SoLuongGiao = g.SoGiao,NgayGiao=g.NgayGiao
						 from (select IDTrienKhai,max(NgayGiao)NgayGiao,sum(SoGiao)SoGiao 						 
						 from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet group by IDTrienKhai)g
						 inner join TrienKhaiKeHoachSanXuat t
						 on t.IDTrienKhai=g.IDTrienKhai where g.IDTrienKhai like '{0}'", idtrienkhai);
            kn.dongketnoi();
        }

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
            cmd.Parameters.Add("@idchitietdonhang", SqlDbType.Int).Value = idchitietdonhang;
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

        private void CapNhatSoLuongChiTietNgay()//Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuat set 
					Date01=n01,
					Date02=n02,
					Date03=n03,
					Date04=n04,
					Date05=n05,
					Date06=n06,
					Date07=n07,
					Date08=n08,
					Date09=n09,
					Date10=n10,
					Date11=n11,
					Date12=n12,
					Date13=n13,
					Date14=n14,
					Date15=n15,
					Date16=n16,
					Date17=n17,
					Date18=n18,
					Date19=n19,
					Date20=n20,
					Date21=n21,
					Date22=n22,
					Date23=n23,
					Date24=n24,
					Date25=n25,
					Date26=n26,
					Date27=n27,
					Date28=n28,
					Date29=n29,
					Date30=n30,
					Date31=n31
					from TrienKhaiKeHoachSanXuat a inner join
					(select IDTrienKhai,
					sum(case when day(NgayGiao)=01 then SoGiao end) n01,
					sum(case when day(NgayGiao)=02 then SoGiao end) n02,
					sum(case when day(NgayGiao)=03 then SoGiao end) n03,
					sum(case when day(NgayGiao)=04 then SoGiao end) n04,
					sum(case when day(NgayGiao)=05 then SoGiao end) n05,
					sum(case when day(NgayGiao)=06 then SoGiao end) n06,
					sum(case when day(NgayGiao)=07 then SoGiao end) n07,
					sum(case when day(NgayGiao)=08 then SoGiao end) n08,
					sum(case when day(NgayGiao)=09 then SoGiao end) n09,
					sum(case when day(NgayGiao)=10 then SoGiao end) n10,
					sum(case when day(NgayGiao)=11 then SoGiao end) n11,
					sum(case when day(NgayGiao)=12 then SoGiao end) n12,
					sum(case when day(NgayGiao)=13 then SoGiao end) n13,
					sum(case when day(NgayGiao)=14 then SoGiao end) n14,
					sum(case when day(NgayGiao)=15 then SoGiao end) n15,
					sum(case when day(NgayGiao)=16 then SoGiao end) n16,
					sum(case when day(NgayGiao)=17 then SoGiao end) n17,
					sum(case when day(NgayGiao)=18 then SoGiao end) n18,
					sum(case when day(NgayGiao)=19 then SoGiao end) n19,
					sum(case when day(NgayGiao)=20 then SoGiao end) n20,
					sum(case when day(NgayGiao)=21 then SoGiao end) n21,
					sum(case when day(NgayGiao)=22 then SoGiao end) n22,
					sum(case when day(NgayGiao)=23 then SoGiao end) n23,
					sum(case when day(NgayGiao)=24 then SoGiao end) n24,
					sum(case when day(NgayGiao)=25 then SoGiao end) n25,
					sum(case when day(NgayGiao)=26 then SoGiao end) n26,
					sum(case when day(NgayGiao)=27 then SoGiao end) n27,
					sum(case when day(NgayGiao)=28 then SoGiao end) n28,
					sum(case when day(NgayGiao)=29 then SoGiao end) n29,
					sum(case when day(NgayGiao)=30 then SoGiao end) n30,
					sum(case when day(NgayGiao)=31 then SoGiao end) n31
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet
					group by IDTrienKhai)b
					on a.IDTrienKhai=b.IDTrienKhai where a.IDTrienKhai like '{0}'", idtrienkhai);
            int kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtSoLuongGiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //    e.Handled = true;
        }
        private void txtSoLuongGiao_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuongGiao.Text == "")
            {
                txtSoLuongGiao.Text = "0";
            }
            DieuKienGhi();
        }
        private void txtSoLuongGiao_KeyUp(object sender, KeyEventArgs e)
        {
            string str = txtSoLuongGiao.Text;
            int start = txtSoLuongGiao.Text.Length - txtSoLuongGiao.SelectionStart;
            str = str.Replace(".", "");
            txtSoLuongGiao.Text = FormatMoney(str);
            txtSoLuongGiao.SelectionStart = -start + txtSoLuongGiao.Text.Length;
        }
        string FormatMoney(object money)
        {
            string str = money.ToString();
            string pattern = @"(?<a>\d*)(?<b>\d{3})*";
            Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
            StringBuilder sb = new StringBuilder();
            foreach (Capture i in m.Groups["b"].Captures)
            {
                sb.Insert(0, "." + i.Value);
            }
            sb.Insert(0, m.Groups["a"].Value);
            return sb.ToString().Trim('.');
        }

        private void TheHienChiTietSoLuongGiaoTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select g.ID,g.IDTrienKhai,
					g.NgayGiao,SoGiao,g.ToThucHien,
					NguoiGhiGiao,g.ToNhan,DienGiai,
					g.IDChiTietDonHang,g.IDTrienKhai,
					NgayGhiGiao,t.MaDonHang,t.MaSanPham,
					t.TenSanPham,t.TenChiTiet,t.TenCongDoan,
					t.IDChiTietDonHang,t.MaPo
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet g
					inner join TrienKhaiKeHoachSanXuat t
					on g.IDTrienKhai=t.IDTrienKhai ");
            grSoChiTietGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }

        private void TheHienSoLuongGiaoTheoTo()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select g.ID,g.IDTrienKhai,
					g.NgayGiao,SoGiao,g.ToThucHien,
					NguoiGhiGiao,g.ToNhan,DienGiai,
					g.IDChiTietDonHang,g.IDTrienKhai,
					NgayGhiGiao,t.MaDonHang,t.MaSanPham,
					t.TenSanPham,t.TenChiTiet,t.TenCongDoan,
					t.IDChiTietDonHang,t.MaPo
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet g
					inner join TrienKhaiKeHoachSanXuat t
					on g.IDTrienKhai=t.IDTrienKhai 
					where g.ToThucHien like N'{0}' 
					and t.IDChiTietDonHang like '{1}' and g.NgayGiao between '{2}' and '{3}' order by ID desc",
                    tothuchien, idchitietdonhang,
                    dpTu.Value.ToString("yyyy-MM-dd"),
                    dpDen.Value.ToString("yyyy-MM-dd"));
            grSoChiTietGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }

        private void TheHienChiTietSoLuongGiaoTheoIDTrienKhai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select g.ID,g.IDTrienKhai,
					g.NgayGiao,SoGiao,g.ToThucHien,
					NguoiGhiGiao,g.ToNhan,DienGiai,
					g.IDChiTietDonHang,g.IDTrienKhai,
					NgayGhiGiao,t.MaDonHang,t.MaSanPham,
					t.TenSanPham,t.TenChiTiet,t.TenCongDoan,
					t.IDChiTietDonHang,t.MaPo
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet g
					inner join TrienKhaiKeHoachSanXuat t
					on g.IDTrienKhai=t.IDTrienKhai where t.IDTrienKhai like '{0}'", idtrienkhai);
            grSoChiTietGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void btnTraCuuTatCa_Click(object sender, EventArgs e)
        {
            TheHienSoLuongGiaoTheoTo();
        }
        private string idsogiaohang;
        private void grSoChiTietGiaoHang_Click(object sender, EventArgs e)
        {

            string point;
            point = gvSoChiTietGiaoHang.GetFocusedDisplayText();
            idsogiaohang = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(idsogiaohang_col);
            idtrienkhai = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(idtrienkhai_col);
            txtMaSanPham.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(masanpham_col);
            txtSanPham.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(tenquicach_col);
            idchitietdonhang = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(idchitietdonhang_col);
            txtTenChiTiet.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(chitietsanpham_col);
            txtCongDoanThucHien.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(congdoan_col);
            txtToThucHien.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(tothuchien_col);
            cbToGiaoDen.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(tonhan_col) == "" ? "xx" : gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(tonhan_col);
            txtGhiChu.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(ghichu_col);
            dpNgayGiaoHang.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(ngaygiao_col);
            txtSoLuongGiao.Text = gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(sogiao_col) == "" ? "0" :
            gvSoChiTietGiaoHang.GetFocusedRowCellDisplayText(sogiao_col);
            btnGhi.Enabled = false;
            DieuKienSuaXoa();//ràng buộc sửa xóa
        }
        /*Ràng buộc sự kiện thêm sửa xóa: 
		 khi người dùng muốn sửa xóa số lượng đã giao thì không được ghi
		Trường hợp người dùng muốn thêm mới dữ liệu giao hàng phải chọn từ kế hoạch giao */
        private void DieuKienSuaXoa()
        {
            if ((Convert.ToDouble(idsogiaohang) > 0 && txtToThucHien.Text == Login.dePartMent) || Login.role == "1")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UpdateSoLuongGiao();
        }
        /* cập nhật số lượng khi người dùng thay đổi 
		   vào sổ giao hàng sao đó tổng hợp lại ghi vào sổ triển khai */
        private void UpdateSoLuongGiao()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
						set NgayGiao='{0}',SoGiao='{1}',ToNhan=N'{2}',
						DienGiai=N'{3}',NguoiGhiGiao=N'{4}',
						NgayGhiGiao = GetDate() where ID like '{5}'",
                    dpNgayGiaoHang.Value.ToString("MM-dd-yyyy"),
                    double.Parse(txtSoLuongGiao.Text.Replace(".", "")), cbToGiaoDen.Text, txtGhiChu.Text,
                    Login.Username, idsogiaohang, idtrienkhai);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                

                
                CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
                CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                CapNhatThanhPhamLapGhep();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                                 //CapNhatPhanTichKeHoachSanXuat();//Cập nhật phân tích đơn hàng vào sổ kế hoạch
                MessageBox.Show("Success", "Message");
                TheHienChiTietSoLuongGiaoTheoIDTrienKhai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Message");
                return;
            }
            gvSoChiTietGiaoHang.AddNewRow();
            gvSoChiTietGiaoHang.FocusedColumn = gvSoChiTietGiaoHang.Columns["ID"];
            gvSoChiTietGiaoHang.ShowEditor();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            UpdateSoLuongGiaoVeZeroDelete();
        }
        //Trước khi xóa cập nhật số giao hàng =0
        //Cập nhật lại số lượng giao hàng của công đoạn
        //Cập nhật số lượng chi tiết sản phẩm
        //Cật nhật số lượng sản phẩm hoàn thành
        private void UpdateSoLuongGiaoVeZeroDelete()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sqlQuery = string.Format(@"update TrienKhaiKeHoachSanXuatGiaoNhanChiTiet set SoGiao='0' where ID like '{0}'",
                    idsogiaohang, idtrienkhai, idchitietdonhang);
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
                CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
                CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
                CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                                 //CapNhatPhanTichKeHoachSanXuat();//Cập nhật phân tích đơn hàng vào sổ kế hoạch
                Delete();//Xóa luôn
                TheHienChiTietSoLuongGiaoTheoIDTrienKhai();
                MessageBox.Show("Success", "Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Message");
                return;
            }
        }
        /* update TrienKhaiKeHoachSanXuat
						 set SoLuongGiao = g.SoGiao,NgayGiao=g.NgayGiao
						 from (select IDTrienKhai,sum(SoGiao)SoGiao,max(NgayGiao)NgayGiao						 
						 from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet group by IDTrienKhai)g
						 inner join TrienKhaiKeHoachSanXuat t
						 on t.IDTrienKhai=g.IDTrienKhai where g.IDTrienKhai like '{1}';

							update TrienKhaiKeHoachSanXuat set SoLuongGiao=g.SoLuongGiao from 
							TrienKhaiKeHoachSanXuat inner join
							(select ParentID,min(SoLuongGiao)SoLuongGiao,
							MaChiTiet,TenChiTiet 
							from TrienKhaiKeHoachSanXuat where MaChiTiet <>'' 
							and TenChiTiet <>''
							group by ParentID,MaChiTiet,TenChiTiet)g
							on TrienKhaiKeHoachSanXuat.ID=g.ParentID 
							where IDChiTietDonHang like '{2}';

					update TrienKhaiKeHoachSanXuat set SoLuongGiao=g.SoLuongGiao from 
					TrienKhaiKeHoachSanXuat inner join
					(select ParentID,min(SoLuongGiao)SoLuongGiao,
					MaChiTiet,TenChiTiet 
					from TrienKhaiKeHoachSanXuat  where MaCongDoan like 'TPN'
					group by ParentID,MaChiTiet,TenChiTiet)g
					on TrienKhaiKeHoachSanXuat.ID=g.ParentID 
					where IDChiTietDonHang like '{2}' and MucDo = 0*/
        //Sau khi update dữ liệu về 0 thì thực hiện xóa luôn dòng
        private void Delete()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"delete from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
					where ID like '{0}'", idsogiaohang);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
        }

        //Cập nhật tiến độ sản xuất vào sổ kế hoạch
        private void CapNhatPhanTichKeHoachSanXuat()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("phantichTrienKhaiKeHoachSanXuat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idchitietdonhang", SqlDbType.Int).Value = idchitietdonhang;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void grChiTietCungLoai_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvChiTietCungLoai.SelectedRowsCount > 0)
            {
                btnGhiChiTietCungLoai.Visible = true;
                //btnGhi.Enabled = false;
                //btnSua.Enabled = false;
                //btnXoa.Enabled = false;
            }
            else
            {
                btnGhiChiTietCungLoai.Visible = false;
                //btnGhi.Enabled = true;
                //btnSua.Enabled = true;
                //btnXoa.Enabled = true;
            }
        }
        private void TheHienDanhSachCacChiTietCungLoai()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select TenChiTiet,SoChiTiet,
							SoLuongYCSanXuat,''SoGiao,IDTrienKhai from TrienKhaiKeHoachSanXuat 
							where IDChiTietDonHang like '{0}' and
							TenChiTiet like N'{1}' and 
							ToThucHien like N'{2}'",
                            idchitietdonhang, txtTenChiTiet.Text, txtToThucHien.Text);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grChiTietCungLoai.DataSource = dt;
        }

        private void btnGhiChiTietCungLoai_Click(object sender, EventArgs e)
        {
            GhiChiTietCungLoai();
        }
        private void GhiChiTietCungLoai()
        {
            int[] listRowList = gvChiTietCungLoai.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvChiTietCungLoai.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"insert into TrienKhaiKeHoachSanXuatGiaoNhanChiTiet 
					(NgayGiao,SoGiao,ToThucHien,
					NguoiGhiGiao,ToNhan,DienGiai,
					IDChiTietDonHang,IDTrienKhai,NgayGhiGiao) values
					('{0}','{1}',N'{2}',
					N'{3}',N'{4}',N'{5}',
					 '{6}','{7}',GetDate());
						 update TrienKhaiKeHoachSanXuat
						 set SoLuongGiao = g.SoGiao,NgayGiao=g.NgayGiao,TinhTrangNgung=g.DienGiai
						 from (select IDTrienKhai,max(NgayGiao)NgayGiao,sum(SoGiao)SoGiao,Max(DienGiai)DienGiai					 
						 from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet group by IDTrienKhai union all 
						  select IDTrienKhai,max(ngaynhan)NgayGiao,sum(BTPT11)Sogiao,''DienGiai from tbl11 
						 where IDTrienKhai is not null
						 group by IDtrienKhai)g
						 inner join TrienKhaiKeHoachSanXuat t
						 on t.IDTrienKhai=g.IDTrienKhai where g.IDTrienKhai like '{7}'",
                    dpNgayGiaoHang.Value.ToString("yyyy-MM-dd"),
                    rowData["SoGiao"], txtToThucHien.Text,
                    Login.Username, cbToGiaoDen.Text, txtGhiChu.Text,
                    idchitietdonhang, rowData["IDTrienKhai"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            //CapNhatSoGiaoVaoSoTrienKhai();//proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
            CapNhatCumChiTiet();//proc Cập nhật cụm chi tiết - TrienKhaiKeHoachSanXuat
            CapNhatThanhPhamLapGhep();//proc Cập nhật thành phẩm lấp gép - TrienKhaiKeHoachSanXuat
            CapNhatMoiNgay();//proc Câp nhat so luong chi tiet moi ngay trong thang- TrienKhaiKeHoachSanXuat
                             //CapNhatPhanTichKeHoachSanXuat();//Cập nhật phân tích đơn hàng vào sổ kế hoạch
            TheHienChiTietSoLuongGiaoTheoTo();
        }
        //proc Cập nhật số lượng giao hang vao so trien khai ke hoach san xuat
        private void CapNhatSoGiaoCungLoaiVaoSoTrienKhai()
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
        private void TheHienChiTietSoLuongGiaoTheoTo()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select g.ID,g.IDTrienKhai,
					g.NgayGiao,SoGiao,g.ToThucHien,
					NguoiGhiGiao,g.ToNhan,DienGiai,
					g.IDChiTietDonHang,g.IDTrienKhai,
					NgayGhiGiao,t.MaDonHang,t.MaSanPham,
					t.TenSanPham,t.TenChiTiet,t.TenCongDoan,
					t.IDChiTietDonHang,t.MaPo
					from TrienKhaiKeHoachSanXuatGiaoNhanChiTiet g
					inner join TrienKhaiKeHoachSanXuat t
					on g.IDTrienKhai=t.IDTrienKhai where g.ToThucHien like '{0}'", Login.Username);
            grSoChiTietGiaoHang.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
    }
}
