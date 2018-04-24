using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class frmDM_CONGDOAN : DevExpress.XtraEditors.XtraForm
    {
        public frmDM_CONGDOAN()
        {
            InitializeComponent();
        }
        private void ListDM_CONGDOANSX(string masanpham)//Danh mục công đoan sản xuất
        {
            ketnoi kn = new ketnoi();
            grDanhMucCongDoan.DataSource = kn.laybang(@"select DonGiaHeSo,HeSo,ChiTietSanPham,SoChiTietSanPham,
                        case when TrungCongDoan='x' 
                        then 0 else SoChiTiet*Dongia_CongDoan end  DonGiaBoSanPham,
				SoChiTiet,TrungCongDoan,NguyenCong,id,Ngayghi,LD.Masp,LD.Tensp,Macongdoan,Tencondoan,Dinhmuc,
				Dongia_CongDoan,Tothuchien,Nguoilap,LD.Ngaylap,Trangthai,LD.DonGia_ApDung,
				LD.NgayApDung from tblDMuc_LaoDong LD left outer join tblSANPHAM SP
				on LD.Masp=SP.Masp");
            kn.dongketnoi();
            gvDanhMucCongDoan.ExpandAllGroups();
        }

        private void DSNguyenCong()//Danh Nguyen công
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select ChiTietSanPham,SoChiTietSanPham,SanPhamID,MaSP,SanPham,MaNC,
				 ChiTietNC,DinhMuc,DonGia,NguyenCong,SoChiTiet,
				 Trangthai='',Tothuchien='',NguoiLap,NgayLap from NguyenCong
				 where MaSP=N'" + txtMasp.Text + "'");
            kn.dongketnoi();
        }
        private void DMCD_MaSP()//Danh mục công đoạn sản phẩm theo mã sản phẩm
        {
            ketnoi kn = new ketnoi();
            grDanhMucCongDoan.DataSource = kn.laybang(@"select ChiTietSanPham,SoChiTietSanPham,case when TrungCongDoan='x' 
                        then 0 else SoChiTiet * Dongia_CongDoan end  DonGiaBoSanPham, * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "'");
            kn.dongketnoi();
            gvDanhMucCongDoan.ExpandAllGroups();
        }
        private void ListDM_CONGDOANTHEM()
        {
            ketnoi kn = new ketnoi();
            grDanhMucCongDoan.DataSource = kn.laybang(@"select ChiTietSanPham,SoChiTietSanPham,case when TrungCongDoan='x' 
                        then 0 else SoChiTiet * Dongia_CongDoan end  DonGiaBoSanPham, * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "'");
            kn.dongketnoi();
            gvDanhMucCongDoan.ExpandAllGroups();
        }

        private void ListDM_CONGDOANSUA()
        {
            ketnoi kn = new ketnoi();
            grDanhMucCongDoan.DataSource = kn.laybang(@"select ChiTietSanPham,SoChiTietSanPham,case when TrungCongDoan='x' 
                        then 0 else SoChiTiet*Dongia_CongDoan end  DonGiaBoSanPham,* from tblDMuc_LaoDong where id like '" + txtId.Text + "'");
            kn.dongketnoi();
            gvDanhMucCongDoan.ExpandAllGroups();
        }

        private void LisDM_CONGDOAN(object sender, EventArgs e)
        {
            string Masanpham = txtMasp.Text;
            ListDM_CONGDOANSX(Masanpham);
        }

        private void BtnList_Sanpham_Click(object sender, EventArgs e)
        {
            //ListDM_Sanpham();
        }

        private void BtnChuyen_Click(object sender, EventArgs e)//Chuyển mã sản phẩm sang danh mục định mức
        {
            try
            {
                if (txtMasp.Text == "") { MessageBox.Show("CHƯA CÓ MÃ KHÔNG THỂ LẬP ĐỊNH MỨC", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblTemp(Ngayghi,Masp,Tensp,Nguoilap,Ngaylap,Tuychon) "
                    + " values(@Ngayghi,@Masp,@Tensp,@Nguoilap,GetDate(),@Tuychon)", cn);
                    cmd.Parameters.Add(new SqlParameter("@Ngayghi", SqlDbType.Date)).Value = dpNgaylap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tensp", SqlDbType.NVarChar)).Value = txtSanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    //cmd.Parameters.Add(new SqlParameter("@Tuychon", SqlDbType.NVarChar)).Value = CBTuyChon.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close(); DMCD_MaSP();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }

        private void Them(object sender, EventArgs e)
        {
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Mã không được trống", "THÔNG BÁO"); return;
            }
            if (txtSanpham.Text == "")
            {
                MessageBox.Show("Sản phẩm không bỏ trống", "THÔNG BÁO"); return;
            }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblDMuc_LaoDong 
					 (SanPhamID,Masp,Tensp,Macongdoan,Tencondoan,Dinhmuc,
					  Dongia_CongDoan,Trangthai,Tothuchien,SoChiTiet,ChiTietSanPham,SoChiTietSanPham,Nguoilap,Ngayghi,Ngaylap) 
					  VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}','{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}','{12}',GetDate(),GetDate())",
                    rowData["SanPhamID"],
                    rowData["Masp"],
                    txtSanpham.Text,
                    rowData["Macongdoan"],
                    rowData["Tencondoan"],
                    rowData["Dinhmuc"],
                    rowData["Dongia_CongDoan"],
                    rowData["Trangthai"],
                    rowData["Tothuchien"],
                    rowData["SoChiTiet"],
                    rowData["ChiTietSanPham"],
                    rowData["SoChiTietSanPham"],
                    txtUser.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                GridLook_SanPhamChuaLapDM();
                GridLook_SanPham_DaLapDM();
                ListDM_CONGDOANTHEM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void Sua(object sender, EventArgs e)
        {
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Mã sản phẩm trông", "THÔNG BÁO"); return;
            }
            if (txtSanpham.Text == "")
            {
                MessageBox.Show("Sản phẩm không bỏ trống", "THÔNG BÁO");
            }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvDanhMucCongDoan.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucCongDoan.GetDataRow(listRowList[i]);
                    Console.WriteLine(rowData["id"]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong set Masp=N'{0}',Tensp=N'{1}',Trangthai=N'{2}',
					Macongdoan=N'{3}',Tencondoan=N'{4}',Dinhmuc='{5}',Dongia_CongDoan='{6}',Nguoilap=N'{7}',
					Tothuchien=N'{8}',SoChiTiet='{9}',TrungCongDoan='{10}',NguyenCong='{11}',ChiTietSanPham=N'{12}',SoChiTietSanPham='{13}',HeSo='{14}',Ngayghi=GetDate() where id ='{15}'",
                    rowData["Masp"],
                    rowData["Tensp"],
                    rowData["Trangthai"],
                    rowData["Macongdoan"],
                    rowData["Tencondoan"],
                    rowData["Dinhmuc"],
                    rowData["Dongia_CongDoan"],
                    txtUser.Text,
                    rowData["Tothuchien"],
                    rowData["SoChiTiet"],
                    rowData["TrungCongDoan"],
                    rowData["NguyenCong"],
                    rowData["ChiTietSanPham"],
                    rowData["SoChiTietSanPham"],
                    rowData["HeSo"],
                    rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatDonGiaHeSo();
                ListDM_CONGDOANTHEM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        //Cập nhật đơn giá hệ số mới = đơn giá củ /1.3
        private void CapNhatDonGiaHeSo()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvDanhMucCongDoan.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucCongDoan.GetDataRow(listRowList[i]);
                    Console.WriteLine(rowData["id"]);
                    string strQuery = string.Format(@"update tblDMuc_LaoDong 
                        set HeSo='{0}',
                        DonGiaHeSo =(Dongia_CongDoan/'{0}')
                        where id ='{1}'",
                        txtHeSoDonGia.Text, rowData["id"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void DocDSNguonLuc()//GridlookupEdit Repository chọn mã nguồn lực trong gridcontrol Áp mã nguồn lực
        {
            ketnoi kn = new ketnoi();
            repositoryItemGridLookUpNguyenCong.DataSource = kn.laybang(@"SELECT Ma_Nguonluc,
                Ten_Nguonluc,Nguoi,Ngay FROM tblResources");
            repositoryItemGridLookUpNguyenCong.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpNguyenCong.ValueMember = "Ma_Nguonluc";
            kn.dongketnoi();
        }
        private void Xoa(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvDanhMucCongDoan.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDanhMucCongDoan.GetDataRow(listRowList[i]);
                    Console.WriteLine(rowData["id"]);
                    string strQuery = string.Format(@"DELETE from tblDMuc_LaoDong  where id ='{0}'", rowData["id"]);
                    Console.WriteLine(strQuery);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                ListDM_CONGDOANTHEM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void Export(object sender, EventArgs e)
        {
            grDanhMucCongDoan.ShowPrintPreview();
        }
        //Danh muc san pham chua lap dinh muc
        #region  DANH MỤC SẢN PHẨM CẦN LẬP ĐỊNH MỨC
        private void GridLook_SanPhamChuaLapDM()
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            GridLook_SPChuaLapDM.Properties.DataSource = Connect.laybang(@"select Code,S.Masp,Tensp,Hotennv,Ngaylap,L.Masp DaApCong 
				   from tblSANPHAM S left outer join
				   (select Masp from tblDMuc_LaoDong group by Masp)L
				   on S.Masp=L.Masp");
            GridLook_SPChuaLapDM.Properties.DisplayMember = "Masp";
            GridLook_SPChuaLapDM.Properties.ValueMember = "Masp,";
            GridLook_SPChuaLapDM.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            GridLook_SPChuaLapDM.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            GridLook_SPChuaLapDM.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        #endregion
        #region DANH MỤC SẢN PHẨM ĐÃ CÓ NGUYÊN CÔNG DO TỔ TRƯỞNG LẬP
        //Danh muc san pham da lap dinh muc
        private void GridLook_SanPham_DaLapDM()
        {
            //DataTable Table = new DataTable();
            //ketnoi Connect = new ketnoi();
            //GridLookSPDaLapDM.Properties.DataSource = Connect.laybang(@"SELECT SP.Masp,Tensp, 
            //     KH.TenKH,DM.Masp SPAPCD FROM tblSANPHAM SP left outer join tblKHACHHANG KH 
            //     on SP.Makh = KH.MKH LEFT OUTER JOIN 
            //     (select Masp from tblDMuc_LaoDong GROUP BY MASP) DM 
            //     on SP.Masp = DM.Masp where DM.Masp is not null");
            //GridLookSPDaLapDM.Properties.DisplayMember = "Masp";
            //GridLookSPDaLapDM.Properties.ValueMember = "Masp";
            //GridLookSPDaLapDM.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //GridLookSPDaLapDM.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            //GridLookSPDaLapDM.Properties.ImmediatePopup = true;
            //Connect.dongketnoi();
        }
        #endregion

        #region DANH MỤC BỘ PHẬN PHÒNG BAN
        private void ListDM_Bophan()//Lay danh muc phong ban
        {
            ketnoi kn = new ketnoi();
            cbTothuchien.DataSource = kn.laybang("select To_bophan from tblPHONGBAN");
            cbTothuchien.DisplayMember = "To_bophan";
            cbTothuchien.ValueMember = "To_bophan";
        }
        #endregion
        #region LẤY MÃ PHÒNG BAN
        private void MaPhongBan()//Lay ma phong ban
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select Ma_bophan from tblPHONGBAN where To_bophan like N'" + cbTothuchien.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtMaBP.Text = Convert.ToString(reader[0]);
                }
                con.Close();
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region BINDING PHÂN QUYỀN SỬA XÓA
        private void BinDing(object sender, EventArgs e)
        {
            //string point;
            //point = gridView1.GetFocusedDisplayText();
            //txtId.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            //txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            //txtSanpham.Text = gridView1.GetFocusedRowCellDisplayText(Tensp_grid1);
            //if (txtSanpham.Text == "")
            //{
            //    txtMasp_TextChanged(sender, e);
            //}
            //gridView1.ExpandAllGroups();
            //if (txtTrangThai.Text == "locked")
            //{
            //    btnSua.Enabled = false; btnXoa.Enabled = false;
            //}
            //else
            //{
            //    btnSua.Enabled = true; btnXoa.Enabled = true;
            //}
        }
        #endregion
        private void BindingNguyenCongSanPham(object sender, EventArgs e)
        {
            //string Gol;
            //Gol = gridView2.GetFocusedDisplayText();
            //txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(maSanPham_grid2);
            //txtSanpham.Text = gridView2.GetFocusedRowCellDisplayText(sanPham_grid2);
            //DMCD_MaSP();
        }

        #region 
        private void LookupCheck_Masp_EditValueChanged(object sender, EventArgs e)//Sự kiện change 
        {
            BinDingCheckMa();
        }

        private void BinDingCheckMa()//Check ma san pham, ten san pham
        {
            string point;
            point = gridView3.GetFocusedDisplayText();
            txtSanPhamID.Text = gridView3.GetFocusedRowCellDisplayText(sanPhamID_col_look);
            txtMasp.Text = gridView3.GetFocusedRowCellDisplayText(Masp_col_look);
            txtSanpham.Text = gridView3.GetFocusedRowCellDisplayText(Tensp_col_look);
        }
        #endregion

        private void cbTothuchien_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaPhongBan();
        }
        private void BtnXuatPhieu_Click(object sender, EventArgs e)//Xuat Phieu dinh muc cong doan san xuat
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "'");
            XRCongDoan CongDoan = new XRCongDoan();
            CongDoan.DataSource = dt;
            CongDoan.DataMember = "Table";
            CongDoan.ShowPreviewDialog();
            kn.dongketnoi();
        }
        //formload
        private void frmDM_CONGDOAN_Load(object sender, EventArgs e) // Form Load
        {
            txtUser.Text = Login.Username;
            LoadTempCongDoan();
            if (Login.role == "39" || Login.role == "1")
            {
                BtnGhepMaCDoan.Visible = true;
                btnSua.Visible = true;
                btnThem.Visible = true;
                btnXoa.Visible = true;
                btnLuuDonGiaSP.Visible = true;
                phanbo_pg.PageVisible = true;
            }
            gvDanhMucCongDoan.ExpandAllGroups();
            ListDM_Bophan();//Lay danh muc phong ban
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            GridLook_SanPhamChuaLapDM();//Danh muc san pham chua tinh dinh muc
            GridLook_SanPham_DaLapDM();//Danh muc san pham da tinh dinh muc
            AddMaBoPhan();
            CbxNhanVien_Resource();//Hiển thị danh sách phòng ban thông kê vào công đoạn sản phẩm
            GiaThamChieu();
            DanhMucGiaSanPham();
            gvDanhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView6.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            repositoryItemComboBoxTrangThai.Items.Add("Đang áp dụng");
            repositoryItemComboBoxTrangThai.Items.Add("Cũ");
            DocDSNguonLuc();
            DocDonDatHangTheoNgay();//Đọc đơn đặt hàng
            gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvDanhMucCongDoan.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gridView2.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gridView3.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gridView4.Appearance.Row.Font = new Font("Segoe UI", 8f);
        }

        private void CbxNhanVien_Resource()//Combobox chọn mã nguồn lực trong gridcontrol Áp mã nguồn lực
        {
            repositoryItemComboBoxToThucHien.Items.Clear();
            ketnoi cn = new ketnoi();
            dynamic dt = cn.laybang(@"SELECT BoPhan FROM tblPHONGBAN_TK");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemComboBoxToThucHien.Items.Add(dt.Rows[i]["BoPhan"]);
            }
        }
        #region Goi lại danh muc sản phẩm đã áp mã
        private void GridLookSPDaLapDM_EditValueChanged(object sender, EventArgs e)
        {
            //ketnoi kn = new ketnoi();
            //gridControl2.DataSource = kn.laybang(@"select *
            //    from NguyenCong where Masp like N'" + GridLookSPDaLapDM.Text+"'");
            //kn.dongketnoi();
        }
        #endregion
        private void AddMaBoPhan()
        {
            ketnoi kn = new ketnoi();
            repositoryItemComboBoxToThucHien.Items.Add(kn.laybang("select To_bophan Tothuchien from tblPHONGBAN").ToString());
            kn.dongketnoi();
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //this.gridView2.OptionsView.NewItemRowPosition
            //     = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            LoadTempCongDoan();
        }
        private void LoadTempCongDoan()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select Top 0 * from tblTempCongDoan");
            kn.dongketnoi();
        }
        private void AddMa_CongDoan()
        {
            //ketnoi kn = new ketnoi();
            //gridControl2.DataSource = kn.laybang(@"select 
            //    Masp + '/CD' + right(concat('000', convert(nvarchar, row_number() over 
            //    (partition by Masp order by(select 1)))), 3) as Mact,Trangthai
            //    from tblDMuc_LaoDong where Masp like N'"+ GridLookSPDaLapDM.Text+"' Masp is not null ");
            //kn.dongketnoi();
        }
        //Add mã công đoạn sản xuất
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //    if (view == null) return;
            //    string columnName = e.Column.Name.ToString();
            //    Console.WriteLine(columnName+ e.RowHandle);
            //    var TenCongDoan = this.getDataValue(view.GetRowCellValue(e.RowHandle, "Tencondoan").ToString());
            //    var valueCode = txtMasp.Text + "CD" + gridView1.RowCount;

            //    view.SetRowCellValue(e.RowHandle, "gridColumn1", valueCode);
            //    if (columnName == "Tencondoan" || columnName == "Dinhmuc" || columnName == "Dongia_CongDoan")
            //    {
            //        Console.WriteLine(TenCongDoan);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error ");
            //}
        }

        private double getDataValue(string tmp)
        {
            try
            {
                return Convert.ToDouble(tmp);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)//Hủy temp
        {

        }

        private void Unlocked()//Cập nhật unlock cho table định mức công đoạn
        {

        }
        private void BtnKhoa_Click(object sender, EventArgs e)//Cập nhật locked cho table định mức công đoạn
        {

        }

        private void btnXemBV_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnXuatvlphu_Click(object sender, EventArgs e)
        {
            grDanhMucCongDoan.ShowPrintPreview();
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //gridView1.SetRowCellValue(1, gridView1.Columns[2], "fadsfsadfds");
        }

        //Cập nhật mã công đoạn vào bản tạm trước khi ghi vào dữ danh mục công đoạn
        private void UpDateMaCongDoan()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"update tblTempCongDoan set Macongdoan=C.MaCD
				from(select id,Masp + '/CD' + right(concat('000', convert(nvarchar, row_number() over 
				(partition by SanPhamID order by(select 1)))), 3)MaCD from tblTempCongDoan)C
				where tblTempCongDoan.ID=C.ID");
            kn.dongketnoi();
        }

        private void BtnGhepMaCDoan_Click(object sender, EventArgs e)
        {
            //MaChiTiet();
            //DSNguyenCong();
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("TRUNCATE table tblTempCongDoan");
            kn.dongketnoi();
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Mã sản phẩm không bỏ trống", "THÔNG BÁO"); return;
            }
            if (txtSanpham.Text == "")
            {
                MessageBox.Show("Sản phẩm không bỏ trống", "THÔNG BÁO"); return;
            }

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblTempCongDoan (
			            Macongdoan,Tencondoan,Dinhmuc,
                        Dongia_CongDoan,Tothuchien,Trangthai,SanPhamID,Masp,SoChiTiet,ChiTietSanPham,SoChiTietSanPham)
                    VALUES (N'{0}',N'{1}','{2}','{3}',N'{4}',N'{5}','{6}',N'{7}','{8}','{9}','{10}') ",
                    rowData["Macongdoan"],
                    rowData["Tencondoan"],
                    rowData["Dinhmuc"],
                    rowData["Dongia_CongDoan"],
                    rowData["Tothuchien"],
                    rowData["Trangthai"],
                    txtSanPhamID.Text,
                    txtMasp.Text,
                    rowData["SoChiTiet"],
                    rowData["ChiTietSanPham"],
                    rowData["SoChiTietSanPham"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                UpDateMaCongDoan();
                con.Close();
                CategoryTemp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do :" + ex.Message);
            }
        }
        private void CategoryTemp()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from tblTempCongDoan");
            kn.dongketnoi();
        }
        private void txtMasp_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //	SqlConnection con = new SqlConnection();
            //	con.ConnectionString = Connect.mConnect;
            //	if (con.State == ConnectionState.Closed)
            //		con.Open();
            //	SqlCommand cmd = new SqlCommand(@"select SanPham,Masp  
            //	from NguyenCong where MaSP = N'" + txtMasp.Text + "' group by MaSP,SanPham", con);
            //	SqlDataReader reader = cmd.ExecuteReader();
            //	if (reader.Read())
            //		txtSanpham.Text = Convert.ToString(reader[0]);
            //	reader.Close(); DSNguyenCong();
            //}
            //catch (Exception ex)
            //{
            //	MessageBox.Show("Lý do: "+ex.Message);
            //}
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            frmThemPhongBan PBTK = new frmThemPhongBan();
            PBTK.Member = txtUser.Text;
            PBTK.ShowDialog();
        }

        private void btnDonGiaSanPham_Click(object sender, EventArgs e)
        {
            DanhMucGiaSanPham();
        }
        private void DanhMucGiaSanPham()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select Code,SP.Masp,Tensp,DonGia_ApDung,
					case when NgayApDung='1900-01-01' then '' 
					else NgayApDung end NgayApDung,dongia DGDN,GiaNgay from tblSANPHAM SP,
					(select MaSP,dongia,NG.GiaNgay from
					(select MaSP,max(thoigianthaydoi)GiaNgay
					from tblDHCT where (MaSP <>'' and dongia >1)
					group by MaSP)NG
					left outer join
					(select dongia,thoigianthaydoi from tblDHCT)DG 
					on NG.GiaNgay=DG.thoigianthaydoi) DG where SP.Masp=DG.MaSP");
            kn.dongketnoi();
        }
        private void BtnDMSanPham_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select Code,Masp,Tensp,DonGia_ApDung,
					case when NgayApDung='1900-01-01' then '' 
					else NgayApDung end NgayApDung,DonGia_ApDung DGDN,NgayApDung GiaNgay
					from tblSANPHAM");
            kn.dongketnoi();
        }
        private void btnLuuDonGiaSP_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView6.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView6.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSANPHAM Set 
						 DonGia_ApDung='{0}',NgayApDung='{1}' WHERE Code ='{2}'",
                     rowData["DonGia_ApDung"],
                     rowData["NgayApDung"] == DBNull.Value ? ""
                     : Convert.ToDateTime(rowData["NgayApDung"]).ToString("yyyy-MM-dd"),
                         rowData["Code"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                    DanhMucGiaSanPham();
                    PhanBoDonGiaCD();
                    DanhMucCongDoan();
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void PhanBoDonGiaCD()
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu(@"update tblDMuc_LaoDong set  DonGia_ApDung=DG.DonGiaCD,
				NgayApDung=DG.NgayApDung 
				from tblDMuc_LaoDong LD,
				(select id,LD.Masp,SP.DonGia_ApDung,SP.NgayApDung,
				cast(Dongia_CongDoan/DG.DonGiaGroup*SP.DonGia_ApDung as decimal) DonGiaCD
				from tblDMuc_LaoDong LD 
				left outer join 
				(select Masp,sum(Dongia_CongDoan)DonGiaGroup from tblDMuc_LaoDong group by Masp)DG
				on DG.Masp=LD.Masp
				left outer join tblSANPHAM SP
				on LD.Masp=SP.Masp)DG
				WHERE LD.id=DG.id");
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            string Poi = "";
            Poi = gridView6.GetFocusedDisplayText();
            txtMasp.Text = gridView6.GetFocusedRowCellDisplayText(Masp_gridsp);
            txtSanpham.Text = gridView6.GetFocusedRowCellDisplayText(Tensp_gridsp);
            DanhMucCongDoan();
            GiaThamChieu();
        }
        private void DanhMucCongDoan()
        {
            ketnoi kn = new ketnoi();
            grDanhMucCongDoan.DataSource = kn.laybang(@"select DonGiaHeSo,HeSo,ChiTietSanPham,SoChiTietSanPham,
				case when TrungCongDoan='x' 
                then 0 else SoChiTiet*Dongia_CongDoan end  DonGiaBoSanPham,SoChiTiet,
                TrungCongDoan,NguyenCong,id,Ngayghi,LD.Masp,LD.Tensp,Macongdoan,Tencondoan,Dinhmuc,
				Dongia_CongDoan,Tothuchien,Nguoilap,LD.Ngaylap,Trangthai,LD.DonGia_ApDung,
				LD.NgayApDung from tblDMuc_LaoDong LD left outer join tblSANPHAM SP
				on LD.Masp=SP.Masp where LD.Masp = N'" + txtMasp.Text + "' ");
            kn.dongketnoi();
            gvDanhMucCongDoan.ExpandAllGroups();
        }
        private void GiaThamChieu()
        {
            ketnoi Connect = new ketnoi();
            gridLookUpGiaDoiChieu.Properties.DataSource = Connect.laybang(@"SELECT Masp,Tenquicach,dongia,thoigianthaydoi
				FROM tblDHCT where (MaSP <>'' and dongia >1) and Masp like '" + txtMasp.Text + "'");
            gridLookUpGiaDoiChieu.Properties.DisplayMember = "Masp";
            gridLookUpGiaDoiChieu.Properties.ValueMember = "Masp";
            gridLookUpGiaDoiChieu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookUpGiaDoiChieu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookUpGiaDoiChieu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void gridLookUpGiaDoiChieu_EditValueChanged_1(object sender, EventArgs e)
        {
            string Poi = "";
            Poi = gridView7.GetFocusedDisplayText();
            txtDonGiaDeNghi.Text = gridView7.GetFocusedRowCellDisplayText(dongia_look);
            GiaThamChieu();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            ketnoi Connect = new ketnoi();
            gridLookUpGiaDoiChieu.Properties.DataSource = Connect.laybang(@"SELECT Masp,Tenquicach,dongia,thoigianthaydoi
				FROM tblDHCT where (MaSP <>'' and dongia >1)");
            gridLookUpGiaDoiChieu.Properties.DisplayMember = "Masp";
            gridLookUpGiaDoiChieu.Properties.ValueMember = "Masp";
            gridLookUpGiaDoiChieu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookUpGiaDoiChieu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookUpGiaDoiChieu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }

        private void btnDSNguyenCong_Click(object sender, EventArgs e)
        {
            CategoryTemp();
        }

        private void btnNguyenCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
            DocDSNguonLuc();
        }
        #region
        private void DocDonDatHangTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Iden,madh,C.Masp,Cast(thoigianthaydoi as date) Ngaylap,case when D.Masp<>'' then 'x' end CoDinhMuc,
				Tenquicach,dvt,Soluong,ngaygiao,
				Khachhang,Mau_banve,Tonkho,ghichu,
				ngoaiquang,pheduyet,Diengiai,nguoithaydoi,
				DonGiaKhoan,TongTienKhoan,NgayLapGiaKhoan,NguoiGhiGiaKhoan from tblDHCT C
                left outer join(select distinct(Masp) from tblDMuc_LaoDong) D
				on D.MaSP=C.MaSP
                where convert( datetime,thoigianthaydoi) 
                between '{0}' and '{1}' order by Iden Desc",
                dptu_ngay.Value.ToString("yyyy-MM-dd"),
                dpden_ngay.Value.ToString("yyyy-MM-dd"));
            gridControl4.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        #endregion
        private void btnDonHang_TrienKhai_Click(object sender, EventArgs e)
        {
            DocDonDatHangTheoNgay();
        }

        private void btnCapNhatGiaKhoan_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gridView4.GetSelectedRows();
            if (listRowList.Count() < 1)
            {
                MessageBox.Show("Chưa check", "Thông báo"); return;
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblDHCT set DonGiaKhoan='{0}',
                            TongTienKhoan=Soluong*{0},
                            NgayLapGiaKhoan=GetDate(),
                            NguoiGhiGiaKhoan='{1}' where Iden='{2}'",
                    rowData["DonGiaKhoan"],
                    txtUser.Text,
                    rowData["Iden"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDonDatHangTheoNgay();
            }
        }

        private void btnCapNhatDonGiaCongMoi_Click(object sender, EventArgs e)
        {
            CapNhatDonGiaHeSo();
            ListDM_CONGDOANTHEM();
        }

        private void btnThemToThucHienCongDoan_Click(object sender, EventArgs e)
        {
            frmThemPhongBan PBTK = new frmThemPhongBan();
            PBTK.Member = txtUser.Text;
            PBTK.ShowDialog();
        }
    }
}
