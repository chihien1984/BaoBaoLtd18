using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;


namespace quanlysanxuat
{
    public partial class frmXuatKho_GiaCong : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatKho_GiaCong()
        {
            InitializeComponent();
        }
        
        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        private void trongluongthucxuat()
        {
            try                
            {
               if( txttongtl_xuat.Text != "0")
                {
                    decimal tongtrongluong = decimal.Parse(txttongtl_xuat.Text);
                    decimal trongluongbaobi = decimal.Parse(txttlbaobi.Text);
                    decimal trongluongthuc = tongtrongluong - trongluongbaobi;
                    txttl_thucxuat.Text = Convert.ToString(trongluongthuc);
                }
            }
            catch (Exception)
            {}
        }
        public static string GiaCong;
        public void LoadGiaCongNgoai() {
            ketnoi knn = new ketnoi();
            cbTen_Giacong.DataSource = knn.laybang("select TenDVGC from tblDS_GIACONG");
            cbTen_Giacong.ValueMember = "TenDVGC";
            cbTen_Giacong.DisplayMember = "TenDVGC";
            cbTen_Giacong.SelectedIndex = -1;
            knn.dongketnoi();
        }

        public void LoadGCNoiBo()
        {
            ketnoi knn = new ketnoi();
            cbTen_Giacong.DataSource = knn.laybang("select To_bophan from tblPHONGBAN");
            cbTen_Giacong.ValueMember = "To_bophan";
            cbTen_Giacong.DisplayMember = "To_bophan";knn.dongketnoi();
        }
        private void frmXuatKho_GiaCong_Load(object sender, EventArgs e) ////  FROM LOAD
        {
            LoadGiaCongNgoai(); PhongBanLoad();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
           
                 
        }

        private void AutoComplete()//Autocomplete danh sach don vi gia cong
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select TenDVGC  from tblDS_GIACONG", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                cbTen_Giacong.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void LoadGrid1()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" SELECT IDSP ,ngaynhan ,nvkd ,madh ,tendh ,sanpham  "
                +" , chitietsanpham, cdthanhpham, soluongsx, ngoaiquang, mabv, donvi, daystar "
                +" , dayend, SL_HANGĐEN, TL_HANGDEN, khachhang, xeploai, ghichu, Diengiai "
                + " , Num, MaBPnhan, TenBPnhan, Giaidoan, Ngaylap, SLgiaoGC,TLgiaoGC, MaSQL, TenSQL "
                + "  FROM dbo.viewBTPHANGDEN_GIACONG");
        }

        private void LoadGrid2(){
           ketnoi kn = new ketnoi();
           gridControl7.DataSource = kn.laybang("SELECT (case when SLthucxuat is not null and SLthucxuat<>'' and TLthucxuat is not null and TLthucxuat<>'' "
                   +" then convert(float, SLthucxuat / TLthucxuat)  end) QuiDoi,Code,NgayLapPhieu,MaPhieu,nvkd,madh,sanpham,chitietsanpham,  "
                   +" cdthanhpham, soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, "
                   +" TongSLgiaogiacong, TongTLgiaogiacong, Soluongbaobi, Loaibaobi, "
                   +" TLbaobi, khachhang, SLthucxuat, TLthucxuat, MaGiaCong, TenGiaCong, Id, "
                   +" Num, Deadline FROM tblXuat_GiaCong "
                   +" where NgayLapPhieu between '"+dptu_ngay.Value.ToString("MM/dd/yyyy")+"' "
                   +" and '"+dpden_ngay.Value.ToString("MM/dd/yyyy")+ "' "
                   +" order by NgayLapPhieu, substring(MaPhieu,3,2) ASC ");
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e) // SỰ KIỆN BINDING DỮ LIỆU BÁN THÀNH  PHẨN CHỜ GIA CÔNG LÊN TEXBOX 
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtIDSP.Text = gridView1.GetFocusedRowCellDisplayText(Code1);
            cbmadh.Text = gridView1.GetFocusedRowCellDisplayText(madh2);
            txttensanpham.Text = gridView1.GetFocusedRowCellDisplayText(sanphamg1);
            txtchitietsanpham.Text = gridView1.GetFocusedRowCellDisplayText(chitietspg1);
            txtsoluongDH.Text = gridView1.GetFocusedRowCellDisplayText(Soluongsxg1);
            dpbatdau.Text = gridView1.GetFocusedRowCellDisplayText(ngaybatdaug1);
            dpketthuc.Text = gridView1.GetFocusedRowCellDisplayText(ngayketthucg1);
            txtkinhdoanh.Text = gridView1.GetFocusedRowCellDisplayText(nvkdg1);
            txtghichu.Text = gridView1.GetFocusedRowCellDisplayText(ghichug1);
            txtNum.Text = gridView1.GetFocusedRowCellDisplayText(numg1);
            txtngoaiquan.Text = gridView1.GetFocusedRowCellDisplayText(ngoaiquan1);
            txtSLBanThanhPham.Text = gridView1.GetFocusedRowCellDisplayText(slbanthanhphamg1);
            txtTLbtp.Text = gridView1.GetFocusedRowCellDisplayText(TLbtp1);
            txtmabanve.Text = gridView1.GetFocusedRowCellDisplayText(Mabvg1);
            txtsoluongchuaxuat.Text = "0"; txtslthuc_xuat.Text = "0";
            txttl_thucxuat.Text = "0"; txtsobi.Text = "0";
            txtsoluongdaxuat.Text = "0"; txttlbaobi.Text = "0";
            txttongsl_xuat.Text = "0"; txttongtl_xuat.Text = "0";
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            LoadGrid1();
        }
        private void btnGiacong_ngoai_Click(object sender, EventArgs e)
        {
            LoadGrid2();
        }
        private void LOADAD_MAGH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Top 1 'PX '+REPLACE(convert(nvarchar,GetDate(),12),'/','')+'-'+ convert(nvarchar,(DATEPART(HH,GetDate())))+'.'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMa_phieu.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void btnadd_Click(object sender, EventArgs e)
        {            LOADAD_MAGH();        }

        private void txtsoluongDH_TextChanged(object sender, EventArgs e)
        {
            MaDV_GiaCong(); DCDV_GiaCong(); DienThoai_GiaCong(); fax_GiaCong();
        }

        private void btndanhsachnhanvien_Click(object sender, EventArgs e)
        {
            frmDV_giacong THEMDV_GIACONG = new frmDV_giacong();
            DialogResult result = THEMDV_GIACONG.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("Thêm ĐV Gia công đã đóng");
            }
        }

        private void cbTen_Giacong_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaDV_GiaCong(); DCDV_GiaCong(); DienThoai_GiaCong(); fax_GiaCong(); //truy van thong tin theo tên đv gia công
        }
        private void MaPhieuXuat()//LẤY MÃ PHIẾU XUẤT
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(" select (CASE WHEN (Max(SoPhieu)+1) <10 THEN 'PX0'+ CONVERT(NVARCHAR,Max(SoPhieu)+1) "
                               +"  WHEN(Max(SoPhieu) + 1) >= 10 THEN 'PX' + CONVERT(NVARCHAR, Max(SoPhieu) + 1) END) SoPhieu "
                               +"  from GiacongNgoai_view where MaGiaCong like N'"+ txtMA_GIACONG + "' and Month(NgayLapPhieu)like MONTH(GetDate())", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMa_phieu.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void CAPNHAT_MAPHIEU()
        {
            ketnoi kn = new ketnoi();
            kn.xulydulieu("update tblXuat_GiaCong set tblXuat_GiaCong.MaPhieu=GiacongNgoai_view.MSPhieu "
                      + "from GiacongNgoai_view, tblXuat_GiaCong "
                      + "where tblXuat_GiaCong.Id = GiacongNgoai_view.Id");
        }
        private void MaDV_GiaCong()//MÃ ĐƠN VỊ GIA CÔNG
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select MaDVGC from tblDS_GIACONG "
                +" where TenDVGC like N'" + cbTen_Giacong.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMA_GIACONG.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void DCDV_GiaCong()//ĐỊA CHỈ GIA CÔNG
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select DiaChi from tblDS_GIACONG where TenDVGC like N'" + cbTen_Giacong.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtdiachi_giacong.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void DienThoai_GiaCong()//ĐIỆN THOẠI GIA CÔNG
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Sodienthoai from tblDS_GIACONG where TenDVGC like N'" + cbTen_Giacong.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtdienthoai.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void fax_GiaCong()//FAX GIA CÔNG
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select fax from tblDS_GIACONG where TenDVGC like N'" + cbTen_Giacong.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtfax.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)// TẠO MỚI PHIẾU
        {
            cbmadh.ValueMember = ""; txtIDSP.Clear(); txttensanpham.Clear(); txtchitietsanpham.Clear(); txtsoluongDH.Clear();
            txtkinhdoanh.Clear(); txtghichu.Clear(); txtNum.Clear(); txtngoaiquan.EditValue=string.Empty;
            txtSLBanThanhPham.Text="0"; txtTLbtp.Text = "0";txtMa_phieu.Clear();
            txtmabanve.Clear(); dpbatdau.EditValue= string.Empty;
            dpketthuc.EditValue = string.Empty;btnadd.Enabled=true;
        }

        private void txttongsl_xuat_TextChanged(object sender, EventArgs e)
        {
            if (txttongsl_xuat.Text == "")
            {
                txttongsl_xuat.Text = "0";
            }
            else
            {
                txttongsl_xuat.Text = string.Format("{0:0,0}", decimal.Parse(txttongsl_xuat.Text));
                txttongsl_xuat.SelectionStart = txttongsl_xuat.Text.Length;
            }     
            float soluongtong = float.Parse(txttongsl_xuat.Text);
            txtslthuc_xuat.Text = Convert.ToString(soluongtong);
        }

        private void txtslthuc_xuat_TextChanged(object sender, EventArgs e)
        {
            if (txtslthuc_xuat.Text == "")
            {
                txtslthuc_xuat.Text = "0";
            }
            else
            {
                txtslthuc_xuat.Text = string.Format("{0:0,0}", decimal.Parse(txtslthuc_xuat.Text));
                txtslthuc_xuat.SelectionStart = txtslthuc_xuat.Text.Length;
            }
        }

        private void btnthemmonhang_Click(object sender, EventArgs e)// THÊM CHI TIẾT MÓN VÀO PHIẾU XUẤT
        {
            if (cbTen_Giacong.Text != "" && txtdiachi_giacong.Text != ""
                && txtMA_GIACONG.Text != "" && cbmadh.Text != ""
                && txttensanpham.Text != ""&&txtMa_phieu.Text !="")
            {
                SqlConnection con = new SqlConnection();
                decimal tongsoluongxuat = Convert.ToDecimal(txttongsl_xuat.Text);
                decimal soluong_thucxuat = Convert.ToDecimal(txtslthuc_xuat.Text);
                decimal soluong_baobi = Convert.ToDecimal(txtsobi.Text);
                decimal soluong_DH = Convert.ToDecimal(txtsoluongDH.Text);
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblXuat_GiaCong "
                           + "(Code,NgayLapPhieu,MaPhieu,nvkd,madh,sanpham,chitietsanpham, "
                           + "soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, "
                           + "TongSLgiaogiacong, TongTLgiaogiacong, Soluongbaobi, Loaibaobi, "
                           + "TLbaobi, khachhang, SLthucxuat, TLthucxuat, MaGiaCong, TenGiaCong, "
                           + "Num, Deadline,MaPoKH,PhuongthucGC) "
                           + "values(@Code,GetDate(),@MaPhieu,@nvkd,@madh,@sanpham,@chitietsanpham, "
                           + "@soluongsx, @ngoaiquang, @mabv, @donvi, @daystar, @dayend, "
                           + "@TongSLgiaogiacong, @TongTLgiaogiacong, @Soluongbaobi, @Loaibaobi, "
                           + "@TLbaobi, @khachhang, @SLthucxuat, @TLthucxuat, @MaGiaCong, @TenGiaCong, "
                           + "@Num, @Deadline,@MaPoKH,@PhuongthucGC)", con))
                    {
                        cmd.Parameters.Add("@Code", SqlDbType.Int).Value = txtIDSP.Text;
                        cmd.Parameters.Add("@MaPhieu", SqlDbType.NVarChar).Value = txtMa_phieu.Text;
                        cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = txtkinhdoanh.Text;
                        cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbmadh.Text;
                        cmd.Parameters.Add("@sanpham", SqlDbType.NVarChar).Value = txttensanpham.Text;
                        cmd.Parameters.Add("@chitietsanpham", SqlDbType.NVarChar).Value = txtchitietsanpham.Text;
                        cmd.Parameters.Add("@soluongsx", SqlDbType.Int).Value = soluong_DH;
                        cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtngoaiquan.Text;
                        cmd.Parameters.Add("@mabv", SqlDbType.NVarChar).Value = txtmabanve.Text;
                        cmd.Parameters.Add("@donvi", SqlDbType.NVarChar).Value = txtdonvi.Text;
                        cmd.Parameters.Add("@daystar", SqlDbType.Date).Value = dpbatdau.Text;
                        cmd.Parameters.Add("@dayend", SqlDbType.Date).Value = dpketthuc.Text;
                        cmd.Parameters.Add("@TongSLgiaogiacong", SqlDbType.Int).Value = tongsoluongxuat;
                        cmd.Parameters.Add("@TongTLgiaogiacong", SqlDbType.Float).Value = txttongtl_xuat.Text;
                        cmd.Parameters.Add("@Soluongbaobi", SqlDbType.Int).Value = soluong_baobi;
                        cmd.Parameters.Add("@Loaibaobi", SqlDbType.NVarChar).Value = cbloaibi.Text;
                        cmd.Parameters.Add("@TLbaobi", SqlDbType.Float).Value = txttlbaobi.Text;
                        cmd.Parameters.Add("@khachhang", SqlDbType.NVarChar).Value = txtkhachhang.Text;
                        cmd.Parameters.Add("@SLthucxuat", SqlDbType.Int).Value = soluong_thucxuat;
                        cmd.Parameters.Add("@TLthucxuat", SqlDbType.Float).Value = txttl_thucxuat.Text;
                        cmd.Parameters.Add("@MaGiaCong", SqlDbType.NVarChar).Value = txtMA_GIACONG.Text;
                        cmd.Parameters.Add("@TenGiaCong", SqlDbType.NVarChar).Value = cbTen_Giacong.Text;
                        cmd.Parameters.Add("@Num", SqlDbType.Int).Value = txtNum.Text;
                        cmd.Parameters.Add("@Deadline", SqlDbType.Date).Value = dpDeadline.Text;
                        cmd.Parameters.Add("@MaPoKH", SqlDbType.NVarChar).Value = txtPokhachhang.Text;
                        cmd.Parameters.Add("@PhuongthucGC",SqlDbType.NVarChar).Value=cbgiaiphapGiaCong.Text;
                        cmd.ExecuteNonQuery();
                    }
                    LoadGrid2();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)// XÓA CHI TIẾT MÓN TRONG PHIẾU XUẤT
        {
            ketnoi kn = new ketnoi();
            gridControl7.DataSource = kn.xulydulieu("delete tblXuat_GiaCong where Id like '"+txtId_xoa.Text+"' ");
            LoadGrid2();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            decimal tongsoluongxuat = Convert.ToDecimal(txttongsl_xuat.Text);
            decimal soluong_thucxuat = Convert.ToDecimal(txtslthuc_xuat.Text);
            decimal soluong_baobi = Convert.ToDecimal(txtsobi.Text);
            decimal soluong_DH = Convert.ToDecimal(txtsoluongDH.Text);
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("update tblXuat_GiaCong set "                     
                       + "Code=@Code,NgayLapPhieu=GetDate(),MaPhieu=@MaPhieu,nvkd=@nvkd,madh=@madh,sanpham=@sanpham,chitietsanpham=@chitietsanpham, "
                       + "soluongsx=@soluongsx, ngoaiquang=@ngoaiquang, mabv=@mabv, donvi=@donvi, daystar=@daystar, dayend=@dayend, "
                       + "TongSLgiaogiacong=@TongSLgiaogiacong, TongTLgiaogiacong=@TongTLgiaogiacong, Soluongbaobi=@Soluongbaobi, Loaibaobi=@Loaibaobi, "
                       + "TLbaobi=@TLbaobi, khachhang=@khachhang, SLthucxuat=@SLthucxuat, TLthucxuat=@TLthucxuat, MaGiaCong=@MaGiaCong, TenGiaCong=@TenGiaCong, "
                       + "Num=@Num, Deadline=@Deadline,MaPoKH=@MaPoKH,PhuongthucGC=@PhuongthucGC where Id like '" + txtId_xoa.Text+"'", con))
                {
                    cmd.Parameters.Add("@Code", SqlDbType.Int).Value = txtIDSP.Text;
                    cmd.Parameters.Add("@MaPhieu", SqlDbType.NVarChar).Value = txtMa_phieu.Text;
                    cmd.Parameters.Add("@nvkd", SqlDbType.NVarChar).Value = txtkinhdoanh.Text;
                    cmd.Parameters.Add("@madh", SqlDbType.NVarChar).Value = cbmadh.Text;
                    cmd.Parameters.Add("@sanpham", SqlDbType.NVarChar).Value = txttensanpham.Text;
                    cmd.Parameters.Add("@chitietsanpham", SqlDbType.NVarChar).Value = txtchitietsanpham.Text;
                    cmd.Parameters.Add("@soluongsx", SqlDbType.Int).Value = soluong_DH;
                    cmd.Parameters.Add("@ngoaiquang", SqlDbType.NVarChar).Value = txtngoaiquan.Text;
                    cmd.Parameters.Add("@mabv", SqlDbType.NVarChar).Value = txtmabanve.Text;
                    cmd.Parameters.Add("@donvi", SqlDbType.NVarChar).Value = txtdonvi.Text;
                    cmd.Parameters.Add("@daystar", SqlDbType.Date).Value = dpbatdau.Text;
                    cmd.Parameters.Add("@dayend", SqlDbType.Date).Value = dpketthuc.Text;
                    cmd.Parameters.Add("@TongSLgiaogiacong", SqlDbType.Int).Value = tongsoluongxuat;
                    cmd.Parameters.Add("@TongTLgiaogiacong", SqlDbType.Float).Value = txttongtl_xuat.Text;
                    cmd.Parameters.Add("@Soluongbaobi", SqlDbType.Int).Value = soluong_baobi;
                    cmd.Parameters.Add("@Loaibaobi", SqlDbType.NVarChar).Value = cbloaibi.Text;
                    cmd.Parameters.Add("@TLbaobi", SqlDbType.Float).Value = txttlbaobi.Text;
                    cmd.Parameters.Add("@khachhang", SqlDbType.NVarChar).Value = txtkhachhang.Text;
                    cmd.Parameters.Add("@SLthucxuat", SqlDbType.Int).Value = soluong_thucxuat;
                    cmd.Parameters.Add("@TLthucxuat", SqlDbType.Float).Value = txttl_thucxuat.Text;
                    cmd.Parameters.Add("@MaGiaCong", SqlDbType.NVarChar).Value = txtMA_GIACONG.Text;
                    cmd.Parameters.Add("@TenGiaCong", SqlDbType.NVarChar).Value = cbTen_Giacong.Text;
                    cmd.Parameters.Add("@Num", SqlDbType.Int).Value = txtNum.Text;
                    cmd.Parameters.Add("@Deadline", SqlDbType.Date).Value = dpDeadline.Text;
                    cmd.Parameters.Add("@MaPoKH", SqlDbType.NVarChar).Value = txtPokhachhang.Text;
                    cmd.Parameters.Add("@PhuongthucGC",SqlDbType.NVarChar).Value=cbgiaiphapGiaCong.Text;
                    cmd.ExecuteNonQuery();
                }
                LoadGrid2();
                con.Close();
            }
        }

        private void btninphieu_xuatkhoGCNgoai_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("Select QuiDoi,Code,NgayLapPhieu,MaPhieu,nvkd,madh,sanpham +' '+chitietsanpham as sanpham,cdthanhpham, "
                   + " soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, TongSLgiaogiacong "
                   +" , TongTLgiaogiacong, Soluongbaobi, Loaibaobi, TLbaobi, khachhang, SLthucxuat "
                   +" , TLthucxuat, MaGiaCong, TenGiaCong, Id, Num, Deadline, MaPoKH, STT, DiaChi from viewxuatkho_giacong "
                   +" where MaPhieu like N'"+txtMa_phieu.Text+ "' ");
            Rpxuatkho_giacong Rpxuatkho = new Rpxuatkho_giacong();
            Rpxuatkho.DataSource = dt;
            Rpxuatkho.DataMember = "Table";

            Rpxuatkho.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void gridControl7_Click(object sender, EventArgs e)
        {
            Gol = gridView7.GetFocusedDisplayText();
            txtMa_phieu.Text = gridView7.GetFocusedRowCellDisplayText(maphieu2);
            txtIDSP.Text = gridView7.GetFocusedRowCellDisplayText(Code2);
            cbmadh.Text = gridView7.GetFocusedRowCellDisplayText(madh2);
            txttensanpham.Text = gridView7.GetFocusedRowCellDisplayText(sanpham2);
            txtchitietsanpham.Text = gridView7.GetFocusedRowCellDisplayText(chitietsanpham2);
            txtsoluongDH.Text = gridView7.GetFocusedRowCellDisplayText(soluongsx2);
            dpbatdau.Text = gridView7.GetFocusedRowCellDisplayText(ngaybatdau2);
            dpketthuc.Text = gridView7.GetFocusedRowCellDisplayText(ngayketthuc2);
            txtkinhdoanh.Text = gridView7.GetFocusedRowCellDisplayText(kinhdoanh2);
            txtNum.Text = gridView7.GetFocusedRowCellDisplayText(num2);
            txtngoaiquan.Text = gridView7.GetFocusedRowCellDisplayText(ngoaiquan2);
            txtmabanve.Text = gridView7.GetFocusedRowCellDisplayText(mabv2);
            txtsobi.Text = gridView7.GetFocusedRowCellDisplayText(slbaobi2);
            txttlbaobi.Text = gridView7.GetFocusedRowCellDisplayText(tlbaobi2);
            txtsoluongdaxuat.Text = gridView7.GetFocusedRowCellDisplayText(tongsl_xuatgiacong2);
            txttongtl_xuat.Text = gridView7.GetFocusedRowCellDisplayText(tongtl_xuatGC2);
            txttongsl_xuat.Text = gridView7.GetFocusedRowCellDisplayText(slthucxuat2);
            txttl_thucxuat.Text = gridView7.GetFocusedRowCellDisplayText(tlthucxuat2);
            cbTen_Giacong.Text = gridView7.GetFocusedRowCellDisplayText(TenDVgiacong2);
            txtId_xoa.Text = gridView7.GetFocusedRowCellDisplayText(id2);
            txtPokhachhang.Text = gridView7.GetFocusedRowCellDisplayText(Malo);
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            
        }

        private void PhongBanLoad()                         // lấy dữ liệu phòng ban theo mã phòng ban
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select To_bophan from tblPHONGBAN where Ma_bophan like N'" + txtmatosanxuat.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txttosanxuat.Text = Convert.ToString(reader[0]);
            reader.Close();con.Close();
        }
        private void txttlbaobi_TextChanged(object sender, EventArgs e)
        {
            trongluongthucxuat();
        }

        private void cbTen_Giacong_Click(object sender, EventArgs e)
        {

        }

        private void txtsobi_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cbTen_Giacong_DropDown(object sender, EventArgs e)
        {
        }

        private void cbTen_Giacong_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cbgiaiphapGiaCong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGCNoiBo();
        }

        private void txtsoluongDH_TextChanged_1(object sender, EventArgs e)
        {
            if (txtsoluongDH.Text == "")
            {
                txtsoluongDH.Text = "0";
            }
            else
            {
                txtsoluongDH.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongDH.Text));
                txtsoluongDH.SelectionStart = txtsoluongDH.Text.Length;
            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            gridView7.ShowPrintPreview();
        }
    }
}
