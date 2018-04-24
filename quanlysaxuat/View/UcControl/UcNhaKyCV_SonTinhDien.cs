using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Threading;

namespace quanlysanxuat
{
    public partial class UcNhaKyCV_SonTinhDien : UserControl
    {
        Clsketnoi knn = new Clsketnoi();
        
        string Gol = "";
        SqlCommand cmd;
        int vitri = 0;
        public UcNhaKyCV_SonTinhDien()
        {
            InitializeComponent();
        }
        public void LoadGrid1()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" select CTKH.IDSP,nvkd,madh,sanpham,T10.SOLUONGTP,T10.TRONGLUONGTP,soluongyc,tonkho, "
                       + " soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, BatDauTo1, "
                       + " KetThucTo1, khachhang, xeploai, Ghichu from tblchitietkehoach CTKH left join "
                       + " (select IDSP, sum(BTPT10) as SOLUONGTP, sum(TRONGLUONG10) AS TRONGLUONGTP from tbl10 group by IDSP) AS T10 "
                       + " ON CTKH.IDSP = T10.IDSP where madh is not null and dayend is not null");
        }
        public void LoadGrid2()//Hiện dữ liệu đã ghi vào sổ nhật ký công việc
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.laybang("Select * from [dbo].[NKCVT10] where convert(Date,Ngay,103) between '" + dateTimePickerTU.Value.ToString("yyyy/MM/dd") + "' "
                                                + "and '" + dateTimePickerDEN.Value.ToString("yyyy/MM/dd") + "' order by Ngay,Hoten,NgayCapNhat ASC");
        }
        private void txtHOTENNV_TextChanged(object sender, EventArgs e)// LẤY MÃ NHÂN VIÊN THEO HỌ VÀ TÊN
        {
            SelectMaNV();
        }
        private void SelectMaNV()//Lấy mã số nhân viên theo họ tên nhân viên
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Sothe from tblDSNHANVIEN where HoTen like N'%" + txtHOTENNV.Text + "%'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaNV.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void AutoCompleteCDSP()// AUTOCOMPLETE CONG DOAN SAN PHAM
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Congdoan from [dbo].[tblNHATKY_T10] where  Congdoan is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtCDSP.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void SelectSoluongDH()//                                                             Select số lượng đơn hàng theo sản phẩm
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select SLSP from tblNHATKY_T10 where Ten_quicach like N'" + txtsp.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtsoluongKH.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void selectMadh_sanpham()//                                                            Select Mã đơn hàng, sản phẩm
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Madh from tblNHATKY_T10 where Ten_quicach like N'" + txtsp.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtmadh.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void TimeNow()
        {
            string GioBatDau = DateTime.Today.ToString("7:30 dd/MM/yyyy");
            string GioKetThuc = DateTime.Today.ToString("16:30 dd/MM/yyyy");
            dateTimePickerbatdau.Text = GioBatDau;
            dateTimePickerketthuc.Text = GioKetThuc;
        }
        private void Autocomplete_TenQuicachSP()//Autocomplete tên qui cách sản phẩm
        {
            string ConString = Connect.mConnect;
            string MaBP = Convert.ToString(cbMaBP.Text);
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select distinct Ten_quicach from NKCVT10 where Ten_quicach != ''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtsp.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
       
        private void Tosanxuat()// LẤY TỔ SẢN XUẤT THEO MÃ TỔ
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select To_bophan FROM tblPHONGBAN where Ma_bophan LIKE '%" + txtmatosanxuat.Text + "%' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txttosanxuat.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void AutoCompleteNhanVien()// ATUCOMPLETE TÊN NHÂN VIÊN
        {
            string ConString = Connect.mConnect;
            string MaBP = Convert.ToString(cbMaBP.Text);
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select DISTINCT HoTen from tblDSNHANVIEN where MaBP like N'" + MaBP + "%' and HoTen is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtHOTENNV.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void UcNhaKyCV_SonTinhDien_Load(object sender, EventArgs e)
        {
            DM_PHONGBAN(); DM_MaDH();
            txtnguoidangnhap.Text = Login.Username; AutoCompleteNhanVien(); AutoCompleteCDSP(); Tosanxuat();
            dateTimePickerTU.Text = DateTime.Now.ToString("01/MM/yyyy"); dateTimePickerDEN.Text = DateTime.Now.ToString();
            Autocomplete_TenQuicachSP(); 
        }

        private void DM_PHONGBAN()
        {
            ketnoi kn = new ketnoi();
            cbMaBP.DataSource = kn.laybang("select distinct Ma_bophan from tblPHONGBAN");
            cbMaBP.ValueMember = "Ma_bophan";
            cbMaBP.DisplayMember = "Ma_bophan";
            cbMaBP.Text = "T06";
            kn.dongketnoi();
        }

        private void DM_MaDH()
        {
            ketnoi kn = new ketnoi();
            txtmadh.DataSource = kn.laybang("select distinct madh from tblchitietkehoach");
            txtmadh.DisplayMember = "madh";
            txtmadh.ValueMember = "madh";
            kn.dongketnoi();
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            if (txtHOTENNV.Text != "")
            {
                SqlConnection con = new SqlConnection();
                string time = "'" + dpbatdau.ToString() + "'";
                decimal SoluongKH = Convert.ToDecimal(txtsoluongKH.Text);
                decimal Soluongsanxuat = Convert.ToDecimal(txtsoluongsanxuat.Text);
                decimal SoluongCD = Convert.ToDecimal(txtsoluongCD.Text);
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tblNHATKY_T10] "
                        + " (Code,Ngay,Manv,Hoten,Ten_quicach,SLSP,SLCD,Congdoan,Madh, "
                        + " SL_SanXuat,GioLam_BatDau,GioLam_KetThuc,MaBP,TenBP,ChuyenGiao,NgayCapNhat) "
                        + " values (@Code,@Ngay,@Manv,@Hoten,@Ten_quicach,@SLSP,@SLCD,@Congdoan,@Madh, "
                        + " @SL_SanXuat,@GioLam_BatDau,@GioLam_KetThuc,@MaBP,@TenBP,@ChuyenGiao,GetDate())", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("Code", SqlDbType.Int)).Value = txtIDSP.Text;
                        cmd.Parameters.Add(new SqlParameter("@Ngay", SqlDbType.Date)).Value = dateTimePickerketthuc.Text;
                        cmd.Parameters.Add(new SqlParameter("@Manv", SqlDbType.NVarChar)).Value = txtMaNV.Text;
                        cmd.Parameters.Add(new SqlParameter("@Hoten", SqlDbType.NVarChar)).Value = txtHOTENNV.Text;
                        cmd.Parameters.Add(new SqlParameter("@Ten_quicach", SqlDbType.NVarChar)).Value = txtsp.Text;
                        cmd.Parameters.Add(new SqlParameter("@SLSP", SqlDbType.Int)).Value = SoluongKH;
                        cmd.Parameters.Add(new SqlParameter("@SLCD", SqlDbType.Int)).Value = SoluongCD;
                        cmd.Parameters.Add(new SqlParameter("@Congdoan", SqlDbType.NVarChar)).Value = txtCDSP.Text;
                        cmd.Parameters.Add(new SqlParameter("@Madh", SqlDbType.NVarChar)).Value = txtmadh.Text;
                        cmd.Parameters.Add(new SqlParameter("@SL_SanXuat", SqlDbType.Int)).Value = Soluongsanxuat;
                        cmd.Parameters.Add(new SqlParameter("@GioLam_BatDau", SqlDbType.DateTime)).Value = dateTimePickerbatdau.Text;
                        cmd.Parameters.Add(new SqlParameter("@GioLam_KetThuc", SqlDbType.DateTime)).Value = dateTimePickerketthuc.Text;
                        cmd.Parameters.Add(new SqlParameter("@MaBP", SqlDbType.NVarChar)).Value = cbMaBP.Text;
                        cmd.Parameters.Add(new SqlParameter("@TenBP", SqlDbType.NVarChar)).Value = txtTenBP.Text;
                        cmd.Parameters.Add(new SqlParameter("@ChuyenGiao", SqlDbType.NVarChar)).Value = cbChuyenGiao.Text;
                        cmd.ExecuteNonQuery();
                        LoadGrid2();
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                        txtsoluongsanxuat.Text = "0"; txtCDSP.ResetText(); txtCDSP.Focus(); AutoCompleteCDSP();
                    }
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập Họ Tên NV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnsua_Click_1(object sender, EventArgs e)
        {
            if (txtHOTENNV.Text != "")
            {
                SqlConnection con = new SqlConnection();
                string time = "'" + dpbatdau.ToString() + "'";
                decimal SoluongKH = Convert.ToDecimal(txtsoluongKH.Text);
                decimal Soluongsanxuat = Convert.ToDecimal(txtsoluongsanxuat.Text);
                decimal SoluongCD = Convert.ToDecimal(txtsoluongCD.Text);
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE [dbo].[tblNHATKY_T10] SET "
                        + " Code=@Code, Ngay=@Ngay,Manv=@Manv,Hoten=@Hoten, "
                        + " Ten_quicach=@Ten_quicach,SLSP=@SLSP,SLCD=@SLCD,Congdoan=@Congdoan,Madh=@Madh, "
                        + " SL_SanXuat=@SL_SanXuat,GioLam_BatDau=@GioLam_BatDau, "
                        + " GioLam_KetThuc=@GioLam_KetThuc,MaBP=@MaBP,TenBP=@TenBP, "
                        + " ChuyenGiao=@ChuyenGiao,NgayCapNhat=GetDate() WHERE IdenKey LIKE '" + txtIdekey.Text + "'", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.Int)).Value = txtIDSP.Text;
                        cmd.Parameters.Add(new SqlParameter("@Ngay", SqlDbType.Date)).Value = dateTimePickerketthuc.Text;
                        cmd.Parameters.Add(new SqlParameter("@Manv", SqlDbType.NVarChar)).Value = txtMaNV.Text;
                        cmd.Parameters.Add(new SqlParameter("@Hoten", SqlDbType.NVarChar)).Value = txtHOTENNV.Text;
                        cmd.Parameters.Add(new SqlParameter("@Ten_quicach", SqlDbType.NVarChar)).Value = txtsp.Text;
                        cmd.Parameters.Add(new SqlParameter("@SLSP", SqlDbType.Int)).Value = SoluongKH;
                        cmd.Parameters.Add(new SqlParameter("@SLCD", SqlDbType.Int)).Value = SoluongCD;
                        cmd.Parameters.Add(new SqlParameter("@Congdoan", SqlDbType.NVarChar)).Value = txtCDSP.Text;
                        cmd.Parameters.Add(new SqlParameter("@Madh", SqlDbType.NVarChar)).Value = txtmadh.Text;
                        cmd.Parameters.Add(new SqlParameter("@SL_SanXuat", SqlDbType.Int)).Value = Soluongsanxuat;
                        cmd.Parameters.Add(new SqlParameter("@GioLam_BatDau", SqlDbType.DateTime)).Value = dateTimePickerbatdau.Text;
                        cmd.Parameters.Add(new SqlParameter("@GioLam_KetThuc", SqlDbType.DateTime)).Value = dateTimePickerketthuc.Text;
                        cmd.Parameters.Add(new SqlParameter("@MaBP", SqlDbType.NVarChar)).Value = cbMaBP.Text;
                        cmd.Parameters.Add(new SqlParameter("@TenBP", SqlDbType.NVarChar)).Value = txtTenBP.Text;
                        cmd.Parameters.Add(new SqlParameter("@ChuyenGiao", SqlDbType.NVarChar)).Value = cbChuyenGiao.Text;
                        cmd.ExecuteNonQuery();
                        ketnoi knn = new ketnoi();
                        LoadGrid2();
                        /* gridControl2.DataSource = knn.laybang("SELECT Code,Ngay,Manv,Hoten,Ten_quicach,SLSP "
                                         + ", SLCD, Congdoan, Madh, SL_SanXuat, GioLam_BatDau "
                                         + ", GioLam_KetThuc, MaBP, TenBP, ChuyenGiao, NgayCapNhat "
                                         + ", IdenKey, GIOLAM, CONG_SUAT_GIO FROM [dbo].[NKCVT10]  "
                                         + " where convert(varchar,Ngay,103) like CONVERT(varchar,'"+dateTimePickerbatdau.Text+"',103) and Hoten like N'" + txtHOTENNV.Text + "'"); */
                        txtsoluongsanxuat.Text = "0"; txtCDSP.ResetText(); txtIDSP.Focus(); AutoCompleteCDSP();
                        //SET FOCUS KHI SỬA THÔNG TIN
                        gridView2.FocusedRowHandle = vitri; vitri = gridView2.FocusedRowHandle;
                        gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                        //txtIdekey.Text=gridView2.FocusedRowHandle
                    }
                }
            }
        }

        private void btnxoa_Click_1(object sender, EventArgs e)
        {
            if (txtIdekey.Text != "" && MessageBox.Show("Bạn muốn xoa " + txtHOTENNV.Text + " có Iden" + txtIdekey.Text + " :D", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("delete [dbo].[tblNHATKY_T10] where IdenKey like '" + txtIdekey.Text + "' ");
                ketnoi knn = new ketnoi();
                LoadGrid2();
                gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                /*gridControl2.DataSource = knn.laybang("SELECT Code,Ngay,Manv,Hoten,Ten_quicach,SLSP "
                                        + ", SLCD, Congdoan, Madh, SL_SanXuat, GioLam_BatDau "
                                        + ", GioLam_KetThuc, MaBP, TenBP, ChuyenGiao, NgayCapNhat "
                                        + ", IdenKey, GIOLAM, CONG_SUAT_GIO FROM [dbo].[NKCVT10]  "
                                        + " where convert(varchar,NgayCapNhat,103) like CONVERT(varchar,GetDate(),103) and Hoten like N'" + txtHOTENNV.Text + "' order by Ngay,Hoten,NgayCapNhat ASC ");*/
            }
        }

        private void btnNKCV_Click_1(object sender, EventArgs e)
        {
            LoadGrid2();
        }

        private void btnphantich_Click(object sender, EventArgs e)
        {

        }

        private void btnthongke_Click(object sender, EventArgs e)
        {
            gridControl3.DataSource = knn.laydulieu("select Ngay,Hoten,Manv,Congdoan,Madh,Ten_quicach, "
                                     + " SLSP, SL_SanXuat, GioLam_BatDau, GioLam_KetThuc, GIOLAM, "
                                     + " CONG_SUAT_GIO, PHUT_GOMTGNGHI from [dbo].[NKCVT10] "
                                     + " where Ngay between '" + dateTimePickerTU.Value.ToString("MM/dd/yyy") + "' "
                                     + " and '" + dateTimePickerDEN.Value.ToString("MM/dd/yyy") + "' order by Ngay,Hoten,NgayCapNhat ASC");
        }

        private void Exportsx_Click(object sender, EventArgs e)
        {
            gridView3.ShowPrintPreview();
        }

        private void giaidoanngoaiSX_Click(object sender, EventArgs e)
        {
            txtIDSP.Text = "0"; txtsp.Clear(); dpbatdau.Text = null; dpketthuc.Text = null;
            txtmadh.Text = ""; txtsoluongKH.Text = "0"; txtmadh.Focus();
        }

        private void btndanhsachnhanvien_Click(object sender, EventArgs e)
        {
            frmDanhSachNV fDSNV = new frmDanhSachNV();
            fDSNV.Show();
        }

        private void btnDonHang_Click_1(object sender, EventArgs e)
        {
            LoadGrid1();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtcodechange.Text = gridView1.GetFocusedRowCellDisplayText(idsp);
            txtIDSP.Text = gridView1.GetFocusedRowCellDisplayText(idsp);
            txtmadh.Text = gridView1.GetFocusedRowCellDisplayText(madh);
            dpbatdau.Text = gridView1.GetFocusedRowCellDisplayText(ngaybd);
            dpketthuc.Text = gridView1.GetFocusedRowCellDisplayText(ngaykt);
            txtsp.Text = gridView1.GetFocusedRowCellDisplayText(sanpham);
            txtdonvi.Text = gridView1.GetFocusedRowCellDisplayText(donvi);
            txtsoluongKH.Text = gridView1.GetFocusedRowCellDisplayText(soluongcansanxuat);
            txtSoluong_giaohang.Text = gridView1.GetFocusedRowCellDisplayText(soluongdaxuat);
        }

        private void cbMaBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("SELECT To_bophan FROM tblPHONGBAN where Ma_bophan like N'%" + cbMaBP.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtTenBP.Text = Convert.ToString(reader[0]);
            reader.Close();
            AutoCompleteNhanVien();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtIDSP.Text = gridView2.GetFocusedRowCellDisplayText(Code);
            txtmadh.Text = gridView2.GetFocusedRowCellDisplayText(madonhang);
            txtsp.Text = gridView2.GetFocusedRowCellDisplayText(tenquicach);
            txtsoluongKH.Text = gridView2.GetFocusedRowCellDisplayText(soluongkehoach);
            txtsoluongsanxuat.Text = gridView2.GetFocusedRowCellDisplayText(soluongsanxuat);
            txtHOTENNV.Text = gridView2.GetFocusedRowCellDisplayText(hoten);
            txtCDSP.Text = gridView2.GetFocusedRowCellDisplayText(congdoan);
            dateTimePickerbatdau.Text = gridView2.GetFocusedRowCellDisplayText(giobatdau);
            dateTimePickerketthuc.Text = gridView2.GetFocusedRowCellDisplayText(gioketthuc);
            txtIdekey.Text = gridView2.GetFocusedRowCellDisplayText(Idkey);
        }

        private void txtsoluongsanxuat_TextChanged(object sender, EventArgs e)
        {
            if (txtsoluongsanxuat.Text == "")
            {
                txtsoluongsanxuat.Text = "0";
            }
            else
            {
                txtsoluongsanxuat.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongsanxuat.Text));
                txtsoluongsanxuat.SelectionStart = txtsoluongsanxuat.Text.Length;
            }
        }

        private void dateTimePickerbatdau_ValueChanged(object sender, EventArgs e)   /// KIỂM TRA GIỜ KẾT THÚC THEO GIỜ BẮT ĐẦU
        {
            DoiGio();
        }

        private void DoiGio()//                                                    ĐỔI THỜI GIAN KẾT THÚC
        {
            if (txtIdekey.Text == "")
            {
                string ngaybatdau = dateTimePickerbatdau.Value.ToString("16:30 dd/MM/yyyy");
                dateTimePickerketthuc.Text = ngaybatdau;
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtIDSP.Text = gridView2.GetFocusedRowCellDisplayText(Code);
            txtmadh.Text = gridView2.GetFocusedRowCellDisplayText(madonhang);
            txtsp.Text = gridView2.GetFocusedRowCellDisplayText(tenquicach);
            txtsoluongKH.Text = gridView2.GetFocusedRowCellDisplayText(soluongkehoach);
            txtsoluongsanxuat.Text = gridView2.GetFocusedRowCellDisplayText(soluongsanxuat);
            txtHOTENNV.Text = gridView2.GetFocusedRowCellDisplayText(hoten);
            txtCDSP.Text = gridView2.GetFocusedRowCellDisplayText(congdoan);
            dateTimePickerbatdau.Text = gridView2.GetFocusedRowCellDisplayText(giobatdau);
            dateTimePickerketthuc.Text = gridView2.GetFocusedRowCellDisplayText(gioketthuc);
            txtIdekey.Text = gridView2.GetFocusedRowCellDisplayText(Idkey);
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txtIDSP.Text = gridView2.GetFocusedRowCellDisplayText(Code);
            txtmadh.Text = gridView2.GetFocusedRowCellDisplayText(madonhang);
            txtsp.Text = gridView2.GetFocusedRowCellDisplayText(tenquicach);
            txtsoluongKH.Text = gridView2.GetFocusedRowCellDisplayText(soluongkehoach);
            txtsoluongsanxuat.Text = gridView2.GetFocusedRowCellDisplayText(soluongsanxuat);
            txtHOTENNV.Text = gridView2.GetFocusedRowCellDisplayText(hoten);
            txtCDSP.Text = gridView2.GetFocusedRowCellDisplayText(congdoan);
            dateTimePickerbatdau.Text = gridView2.GetFocusedRowCellDisplayText(giobatdau);
            dateTimePickerketthuc.Text = gridView2.GetFocusedRowCellDisplayText(gioketthuc);
            txtIdekey.Text = gridView2.GetFocusedRowCellDisplayText(Idkey);
            txtHOTENNV.Focus();
        }

        private void txtcodechange_TextChanged(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("SELECT Code,Ngay,Manv,Hoten,Ten_quicach,SLSP "
                                        + ", SLCD, Congdoan, Madh, SL_SanXuat, GioLam_BatDau "
                                        + ", GioLam_KetThuc, MaBP, TenBP, ChuyenGiao, NgayCapNhat "
                                        + ", IdenKey, GIOLAM, CONG_SUAT_GIO FROM NKCVT10 where Code like '" + txtcodechange.Text + "'");
            //gridControl4.DataSource = knn.laydulieu("select Ten_quicach, SLSP, Congdoan, SL_SanXuat, "
            //                             + " GioLam_BatDau, GioLam_KetThuc, GIOLAM, CONG_SUAT_GIO "
            //                             + " from NKCVT10 where Code like '" + txtIDSP.Text + "'");
            //gridControl3.DataSource = knn.laydulieu("select NK.Code,Ten_quicach,NK.Congdoan,SOLUONG_HT from tblNHATKY_T10 NK, "
            // + "(select Code, Congdoan, SUM(SL_SanXuat) as SOLUONG_HT from tblNHATKY_T10 group by Code, Congdoan) NK_T10 "
            // + "WHERE NK.Code = NK_T10.Code");
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            txtHOTENNV.Focus();
        }

        private void txtHOTENNV_TextChanged_1(object sender, EventArgs e)//LẤY MÃ SỐ NHÂN VIÊN THEO HỌ TÊN
        {
            SelectMaNV();
        }

        private void txtsoluongKH_TextChanged(object sender, EventArgs e)
        {
            if (txtsoluongKH.Text == "")
            {
                txtsoluongKH.Text = "0";
            }
            else
            {
                txtsoluongKH.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongKH.Text));
                txtsoluongKH.SelectionStart = txtsoluongKH.Text.Length;
            }
        }

        private void txtmadh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsp_TextChanged(object sender, EventArgs e)
        {
            selectMadh_sanpham(); SelectSoluongDH();
        }

        private void txtmadh_KeyDown(object sender, KeyEventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" select CTKH.IDSP,nvkd,madh,sanpham,T10.SOLUONGTP,T10.TRONGLUONGTP,soluongyc,tonkho, "
                       + " soluongsx, ngoaiquang, mabv, donvi, daystar, dayend, BatDauTo1, "
                       + " KetThucTo1, khachhang, xeploai, Ghichu from tblchitietkehoach CTKH left join "
                       + " (select IDSP, sum(BTPT10) as SOLUONGTP, sum(TRONGLUONG10) AS TRONGLUONGTP from tbl10 group by IDSP) AS T10 "
                       + " ON CTKH.IDSP = T10.IDSP where madh is not null and dayend is not null and madh like N'" + txtmadh.Text + "'");
        }

        private void btnphantich_Click_1(object sender, EventArgs e)
        {

        }

        private void btnthongke_Click_1(object sender, EventArgs e)
        {
            gridControl3.DataSource = knn.laydulieu("select Ngay,Hoten,Manv,Congdoan,Madh,Ten_quicach, "
                                     + " SLSP, SL_SanXuat, GioLam_BatDau, GioLam_KetThuc, GIOLAM, "
                                     + " CONG_SUAT_GIO, PHUT_GOMTGNGHI from [dbo].[NKCVT10] "
                                     + " where Ngay between '" + dateTimePickerTU.Value.ToString("MM/dd/yyy") + "' "
                                     + " and '" + dateTimePickerDEN.Value.ToString("MM/dd/yyy") + "' order by Ngay,Hoten,NgayCapNhat ASC");
        }
    }
}
