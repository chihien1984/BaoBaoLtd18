using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Model;
using quanlysanxuat.Report;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class frmKhacPhucPhongNgua : Form
    {
        public frmKhacPhucPhongNgua()
        {
            InitializeComponent();
        }
        SANXUATDbContext db = new SANXUATDbContext();
        private void DocDeliveryDatHang()
        {
            var dt = db.Delivery_DatHang.ToList();
            gridControl3.DataSource = new BindingList<Delivery_DatHang>(dt);
            dt.Clear();
        }
        private void DocDeliveryNhanVien()
        {
            var dt = db.Delivery_NhanVien.ToList();
            gridControl4.DataSource = new BindingList<Delivery_NhanVien>(dt);
            dt.Clear();
        }
  

        private void btnDocTatCaNCR_Click(object sender, EventArgs e)
        {
            DocTatCaNCR();
        }
       
        private void Export_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }

        #region Đọc đơn hàng chi tiết vào columedit gridview đơn hàng
        private void LoadItemDatHang()
        {
            ketnoi Connect = new ketnoi();
            repositoryItemDSDatHang.DataSource = Connect.laybang(@"select Iden,madh +';'
                 + MaSP +'; '+Tenquicach+'; '+ngoaiquang +'; '
                 + Khachhang Thongtin from tblDHCT");
            repositoryItemDSDatHang.DisplayMember = "Iden";
            repositoryItemDSDatHang.ValueMember = "Iden";
            repositoryItemDSDatHang.NullText = @"Chọn id sản phẩm";
            Iden.ColumnEdit = repositoryItemDSDatHang;
            Connect.dongketnoi();
        }
        #endregion

        #region Đọc mã nhân viên vào columedit gridview nhân viên
        private void LoadItemNhanVien()
        {
            var dt = db.tblDSNHANVIENs.OrderBy(p=>p.ID).ToList();
            repositoryItemDSNhanVien.DataSource = dt;
            repositoryItemDSNhanVien.ValueMember = "ID";
            repositoryItemDSNhanVien.DisplayMember = "ID";
            repositoryItemDSNhanVien.NullText = @"Chọn id nhân viên";
            colID.ColumnEdit = repositoryItemDSNhanVien;
        }
        #endregion

        private void frmCVKhuon_Load(object sender, EventArgs e)
        {
            txtMember.Text = Login.Username;
            txtMemberCar.Text = Login.Username;
            LoadItemDatHang();
            DocDeliveryDatHang();
            LoadItemNhanVien();
            DocDeliveryNhanVien();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            dpChiPhiNhanVienTu.Text= DateTime.Now.ToString("01/MM/yyyy");
            dpChiPhiNhanVienDen.Text= DateTime.Now.ToString();
            dpCarTuNgay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpCarDenNgay.Text = DateTime.Now.ToString();
            this.gridView2.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            DocTatCaCar();
            DocTatCaNCR();
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView3.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
      
        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Iden")
            {
                var value = gridView3.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDHCTs.FirstOrDefault(x => x.Iden == (long)value);
                if (dt != null)
                {
                    gridView3.SetRowCellValue(e.RowHandle, "Thongtin", 
                     dt.madh+';'
                    +dt.MaSP+';'
                    +dt.Tenquicach+';'
                    +dt.ngoaiquang);
                }
            }
        }
        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ID")
            {
                var value = gridView4.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblDSNHANVIENs.FirstOrDefault(x => x.ID == (int)value);
                if (dt != null)
                {
                    gridView4.SetRowCellValue(e.RowHandle, "Sothe", dt.Sothe);
                    gridView4.SetRowCellValue(e.RowHandle, "HoTen", dt.HoTen);
                }
            }
        }
        private void DocTatCaCar()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select CarID,SBB,NgayPhatHanh,TaiLieu,MoTaVanDe,
						PhanTichNguyenNhan,HanhDongKPPN,NgayHoanThanh,
						NguoiThucHien,NguoiDuyet,GiamSat,NgayGiamSat,
						YKienBoSung,KetQuaThoaMan,KetQuaKhongThoaMan,
						GhiChuCarMoi,NguoiGhi,NgayGhi from tblCAR");
            kn.dongketnoi();
            this.gridView1.OptionsSelection.MultiSelectMode
               = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
        private void btnDocCar_Click(object sender, EventArgs e)
        {
            DocTatCaCar();
        }
        private void DocCarTuDen()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select CarID,SBB,NgayPhatHanh,TaiLieu,MoTaVanDe,
						PhanTichNguyenNhan,HanhDongKPPN,NgayHoanThanh,
						NguoiThucHien,NguoiDuyet,GiamSat,NgayGiamSat,
						YKienBoSung,KetQuaThoaMan,KetQuaKhongThoaMan,
						GhiChuCarMoi,NguoiGhi,NgayGhi from tblCAR where 
                        NgayPhatHanh between '{0}' and '{1}'",
                        dptu_ngay.Value.ToString("MM-dd-yyyy"),
                        dpden_ngay.Value.ToString("MM-dd-yyyy"));
            gridControl1.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            this.gridView1.OptionsSelection.MultiSelectMode
               = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }

        private void btnDocCarTuDen_Click(object sender, EventArgs e)
        {
            DocCarTuDen();
        }

        private void PrintCar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@" select CarID,SBB,NgayPhatHanh,TaiLieu,MoTaVanDe,
						PhanTichNguyenNhan,HanhDongKPPN,NgayHoanThanh,
						NguoiThucHien,NguoiDuyet,GiamSat,NgayGiamSat,
						YKienBoSung,KetQuaThoaMan,KetQuaKhongThoaMan,
						GhiChuCarMoi,NguoiGhi,NgayGhi from tblCAR where CarID='" + txtCarID.Text + "'");
            XRCar xRCar = new XRCar();
            xRCar.DataSource = dt;
            xRCar.DataMember = "Table";
            xRCar.ShowPreviewDialog();
        }
        private void btnGhiCAR_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblCAR 
                                (SBB,NgayPhatHanh,TaiLieu,
						        MoTaVanDe,PhanTichNguyenNhan,HanhDongKPPN,
						        NgayHoanThanh,NguoiThucHien,NguoiDuyet,
						        GiamSat,NgayGiamSat,YKienBoSung,
						        KetQuaThoaMan,KetQuaKhongThoaMan,GhiChuCarMoi,
						        NguoiGhi,NgayGhi) values(N'{0}',N'{1}',N'{2}',
						        N'{3}',N'{4}',N'{5}',N'{6}',
						        N'{7}',N'{8}',N'{9}',N'{10}',
						        N'{11}',N'{12}',N'{13}',
						        N'{14}',N'{15}',GetDate())",
                              rowData ["SBB"],
                              rowData["NgayPhatHanh"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayPhatHanh"]).ToString("yyyy-MM-dd"),
                              rowData ["TaiLieu"],
                              rowData ["MoTaVanDe"],
                              rowData ["PhanTichNguyenNhan"],
                              rowData ["HanhDongKPPN"],
                               rowData["NgayHoanThanh"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayHoanThanh"]).ToString("yyyy-MM-dd"),
                              rowData ["NguoiThucHien"],
                              rowData ["NguoiDuyet"],
                              rowData ["GiamSat"],
                                rowData["NgayGiamSat"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayGiamSat"]).ToString("yyyy-MM-dd"),
                              rowData ["YKienBoSung"],
                              rowData ["KetQuaThoaMan"],
                              rowData ["KetQuaKhongThoaMan"],
                              rowData ["GhiChuCarMoi"],
                              txtMember.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocTatCaCar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        //public static DateTime dateNow;
        //public DateTime dateTimeNow()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = Connect.mConnect;
        //    if (con.State == ConnectionState.Closed)
        //        con.Open();
        //    cmd = new SqlCommand(@"select cast(GetDate() as Date)", con);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //        Convert.ToDateTime(reader[0]);
        //    reader.Close();
        //    return dateTimeNow();
        //}
        private void btnSuaCAR_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblCAR 
                            set SBB=N'{0}',NgayPhatHanh=N'{1}',TaiLieu=N'{2}',
						    MoTaVanDe=N'{3}',PhanTichNguyenNhan=N'{4}',HanhDongKPPN=N'{5}',
						    NgayHoanThanh=N'{6}',NguoiThucHien=N'{7}',NguoiDuyet=N'{8}',
						    GiamSat=N'{9}',NgayGiamSat=N'{10}',YKienBoSung=N'{11}',
						    KetQuaThoaMan=N'{12}',KetQuaKhongThoaMan=N'{13}',GhiChuCarMoi=N'{14}',
						    NguoiGhi=N'{15}',NgayGhi=GetDate() where CarID=N'{16}'",
                              rowData ["SBB"],
                              rowData["NgayPhatHanh"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayPhatHanh"]).ToString("yyyy-MM-dd"),
                              rowData ["TaiLieu"],
                              rowData ["MoTaVanDe"],
                              rowData ["PhanTichNguyenNhan"],
                              rowData ["HanhDongKPPN"],
                               rowData["NgayHoanThanh"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayHoanThanh"]).ToString("yyyy-MM-dd"),
                              rowData ["NguoiThucHien"],
                              rowData ["NguoiDuyet"],
                              rowData ["GiamSat"],
                                rowData["NgayGiamSat"] == DBNull.Value ? ""
                              : Convert.ToDateTime(rowData["NgayGiamSat"]).ToString("yyyy-MM-dd"),
                              rowData ["YKienBoSung"],
                              rowData ["KetQuaThoaMan"],
                              rowData ["KetQuaKhongThoaMan"],
                              rowData ["GhiChuCarMoi"],
                              txtMember.Text,
                              rowData ["CarID"]);
                              SqlCommand cmd = new SqlCommand(strQuery, con);
                              cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCaCar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnXoaCAR_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblCAR where CarID=N'{0}'",
                          rowData ["CarID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCaCar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnExportCAR_Click(object sender, EventArgs e)
        {
            this.gridView1.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gridControl1.ShowPrintPreview();            
        }

        private void btnTaoMoiCAR_Click(object sender, EventArgs e)
        {
            DocCarMoi();
            this.gridView1.OptionsView.NewItemRowPosition =
               DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsSelection.MultiSelectMode
              = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
        private void DocCarMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select top 0 SBB='',
                        NgayPhatHanh=GetDate(),TaiLieu='',MoTaVanDe='',
						PhanTichNguyenNhan='',HanhDongKPPN='',NgayHoanThanh=GETDATE(),
						NguoiThucHien='',NguoiDuyet='',GiamSat='',NgayGiamSat=GETDATE(),
						YKienBoSung='',KetQuaThoaMan='',KetQuaKhongThoaMan='',
						GhiChuCarMoi='',NguoiGhi='' from tblCAR");
            kn.dongketnoi();
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
    
        private void DocTatCaNCR()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select C.ThietHaiCongTy,N.ThietHaiNhanVien,
                C.ThietHaiCongTy+N.ThietHaiNhanVien ThietHai,R.*
                from tblNCR R left outer join
                (select NcrID,Sum(ChiPhiDonHang) ThietHaiCongTy
                from NcrDatHang group by NcrID) C on R.NcrID=C.NcrID
                left outer join 
                (select NcrID,sum(ChiPhiNhanVien) ThietHaiNhanVien
                from NcrNhanVien group by NcrID) N on C.NcrID=N.NcrID");
                        //where NgayLap between '" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "' and '" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "'");
            kn.dongketnoi();
            this.gridView2.OptionsSelection.MultiSelectMode
               = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            this.gridView2.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
        private void btnTaomoiNCR_Click(object sender, EventArgs e)
        {
            this.gridView2.OptionsView.NewItemRowPosition =
               DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView2.OptionsSelection.MultiSelectMode
               = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            DocNCRMoi();

            this.gridView2.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
        private void DocNCRMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select Top 0 cast(NgayLap as date)NgayLap,SoNcr='',SBB='',
                        KhongPhuHop='',NguyenNhan='',KhacPhuc='',PhongNgua='',
						CaiTien='',KetQua='',GhiChu='',SoNcr='',NguoiLap='',HinhThucXuLy='',
                        cast(NgayPhaiHoanThanh as date) NgayPhaiHoanThanh,
                        cast(NgayGiamSat as date) NgayGiamSat,ThoaMan='',
                        KhongThoaMan='',NcrMoi='' from tblNCR");
            kn.dongketnoi();
        }

        private void btnThemNCR_Click(object sender, EventArgs e)
        {
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
                    string strQuery = string.Format(@"insert into tblNCR 
                       (SBB,KhongPhuHop,
                        NguyenNhan,KhacPhuc,PhongNgua,
						CaiTien,KetQua,GhiChu,
                        NguoiGhi,SoNcr,NguoiLap,
                        HinhThucXuLy,ThoaMan,KhongThoaMan,
                        NcrMoi,
                        NgayLap,NgayPhaiHoanThanh,
                        NgayGiamSat,Ngayghi)
                        values(N'{0}',N'{1}',N'{2}',
						N'{3}',N'{4}',N'{5}',
						N'{6}',N'{7}',N'{8}',
                        N'{9}',N'{10}',N'{11}',
                        N'{12}',N'{13}',N'{14}',
                        '{15}','{16}','{17}',GetDate())",
                         rowData ["SBB"],
                         rowData ["KhongPhuHop"],
                         rowData ["NguyenNhan"],
                         rowData ["KhacPhuc"],
                         rowData ["PhongNgua"],
                         rowData["CaiTien"],
                         rowData["KetQua"],
                         rowData["GhiChu"],
                         txtMember.Text,
                          rowData["SoNcr"],
                          rowData["NguoiLap"],
                          rowData["HinhThucXuLy"],
                          rowData["ThoaMan"],
                          rowData["KhongThoaMan"],
                          rowData["NcrMoi"],
                          rowData["NgayLap"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayLap"]).ToString("yyyy-MM-dd"),
                          rowData["NgayPhaiHoanThanh"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayPhaiHoanThanh"]).ToString("yyyy-MM-dd"),
                          rowData["NgayGiamSat"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayGiamSat"]).ToString("yyyy-MM-dd"));
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCaNCR();
        }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
}

        private void btnSuaNCR_Click(object sender, EventArgs e)
        {
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
                    string strQuery = string.Format(@"update tblNCR 
                        set NgayLap=N'{0}',SBB=N'{1}',KhongPhuHop=N'{2}',
                        NguyenNhan=N'{3}',KhacPhuc=N'{4}',PhongNgua=N'{5}',
						CaiTien=N'{6}',KetQua=N'{7}',GhiChu=N'{8}',
                        NguoiGhi=N'{9}',Ngayghi=GetDate(),
                        SoNcr=N'{10}',NguoiLap=N'{11}',
                        HinhThucXuly=N'{12}',NgayPhaiHoanThanh=N'{13}',
                        NgayGiamSat=N'{14}',ThoaMan=N'{15}',
                        KhongThoaMan=N'{16}',NcrMoi=N'{17}'
                        where NcrID='{18}'",
                         rowData["NgayLap"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayLap"]).ToString("yyyy-MM-dd"),
                         rowData["SBB"],
                         rowData["KhongPhuHop"],
                         rowData["NguyenNhan"],
                         rowData["KhacPhuc"],
                         rowData["PhongNgua"],
                         rowData["CaiTien"],
                         rowData["KetQua"],
                         rowData["GhiChu"],
                         txtMember.Text,
                          rowData["SoNcr"],
                          rowData["NguoiLap"],
                          rowData["HinhThucXuly"],
                          rowData["NgayPhaiHoanThanh"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayPhaiHoanThanh"]).ToString("yyyy-MM-dd"),
                          rowData["NgayGiamSat"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayGiamSat"]).ToString("yyyy-MM-dd"),
                          rowData["ThoaMan"],
                          rowData["KhongThoaMan"],
                          rowData["NcrMoi"],
                          rowData["NcrID"]
                          );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCaNCR();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnXoaNCR_Click(object sender, EventArgs e)
        {
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
                    string strQuery = string.Format(@"delete from tblNCR where NcrID='{0}'",
                          rowData["NcrID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCaNCR();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnDocNCR_Click(object sender, EventArgs e)
        {
            DocTatCaNCR();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView2.GetFocusedDisplayText();
            txtNcrID.Text = gridView2.GetFocusedRowCellDisplayText(ncrID_);
            txtSoBB.Text = gridView2.GetFocusedRowCellDisplayText(SoBB_);
            DocChiPhiDonHang();
            DocChiPhiNhanVien();
        }

        private void btnTaoBienBan_Click(object sender, EventArgs e)
        {
            this.gridView2.OptionsView.NewItemRowPosition =
              DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }
       private void TaoMoiBienBan()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select C.ThietHaiCongTy,N.ThietHaiNhanVien,
                C.ThietHaiCongTy+N.ThietHaiNhanVien ThietHai,R.*
                from tblNCR R left outer join
                (select NcrID,Sum(ChiPhiDonHang) ThietHaiCongTy
                from NcrDatHang group by NcrID) C on R.NcrID=C.NcrID
                left outer join 
                (select NcrID,sum(ChiPhiNhanVien) ThietHaiNhanVien
                from NcrNhanVien group by NcrID) N on C.NcrID=N.NcrID WHERE NcrID='" + txtNcrID.Text+"'");
            kn.dongketnoi();
        }
        
        #region Cập nhật chi phí đơn hàng lỗi
        private void btnCapNhatChiPhiDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView3.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into NcrDatHang 
                        (NcrID,DonHangID,ChiPhiDonHang,Thongtin,Kg,Cai)
                         values('{0}','{1}','{2}',N'{3}',N'{4}',N'{5}')", 
                          txtNcrID.Text,
                          gridView3.GetRowCellValue(i, "Iden"),
                          gridView3.GetRowCellValue(i, "ChiPhiDonHang"),
                          gridView3.GetRowCellValue(i, "Thongtin"),
                          gridView3.GetRowCellValue(i, "Kg"),
                          gridView3.GetRowCellValue(i, "Cai"));
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocChiPhiDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

       
        private void btnSuaChiPhiDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView3.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update NcrDatHang 
                          set DonHangID='{0}',ChiPhiDonHang ='{1}',Thongtin =N'{2}',Kg=N'{3}',Cai=N'{4}'
                          where NcrDatHangID='{5}'",
                          rowData["Iden"],
                          rowData["ChiPhiDonHang"],
                          rowData["Thongtin"],
                          rowData["Kg"],
                          rowData["Cai"],
                          rowData["NcrDatHangID"]
                          );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocChiPhiDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnXoaChiPhiDonHang_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView3.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView3.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from NcrDatHang 
                        where NcrDatHangID ='{0}'",
                        rowData["NcrDatHangID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocChiPhiDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        private void DocChiPhiDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select NcrDatHangID,NcrID,
                DonHangID Iden,ChiPhiDonHang,Thongtin,Kg,Cai from 
                NcrDatHang where NcrID=" + txtNcrID.Text + "");
            kn.dongketnoi();
            this.gridView3.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        #endregion 



        #region Cập nhật chi phí nhân viên
        private void btnCapNhatChiPhiNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView4.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into NcrNhanVien 
                          (NcrID,NhanVienID,ChiPhiNhanVien,NoiDungViPham,Sothe,HoTen) 
                          values ('{0}','{1}',N'{2}',N'{3}',N'{4}',N'{5}')",
                          txtNcrID.Text,
                          gridView4.GetRowCellValue(i, "ID"),
                          gridView4.GetRowCellValue(i, "ChiPhiNhanVien"),
                          gridView4.GetRowCellValue(i, "NoiDungViPham"),
                          gridView4.GetRowCellValue(i, "Sothe"),
                          gridView4.GetRowCellValue(i, "HoTen")
                          );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocChiPhiNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnSuaChiPhiNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView4.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update NcrNhanVien 
                          set NhanVienID='{0}',Sothe='{1}',HoTen=N'{2}',
                          ChiPhiNhanVien='{3}',
                          NoiDungViPham=N'{4}'
                          where NcrNhanVienID='{5}'",
                          rowData["ID"],
                          rowData["Sothe"],
                          rowData["HoTen"],
                          rowData["ChiPhiNhanVien"],
                          rowData["NoiDungViPham"],
                          rowData["NcrNhanVienID"]
                          );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocChiPhiNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnXoaChiPhiNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView4.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView4.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from NcrNhanVien 
                        where NcrNhanVienID ='{0}'",
                        rowData["NcrNhanVienID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocChiPhiNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }
        private void DocChiPhiNhanVien()
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select NcrNhanVienID,
                        Sothe,HoTen,NcrID,NhanVienID ID,
                        ChiPhiNhanVien,NoiDungViPham 
                        from NcrNhanVien where NcrID = " + txtNcrID.Text + "");
            kn.dongketnoi();
            this.gridView4.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        #endregion

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            TaoMoiBienBan();
        }

        private void PrintNCR_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select R.*,D.Cai,D.Kg,D.Thongtin,D.ChiPhiDonHang from tblNCR R
                inner join NcrDatHang D
                on R.NcrID=D.NcrID where R.NcrID='"+txtNcrID.Text+"'");
            ReportKhacPhucPhongNgua xRNcr = new ReportKhacPhucPhongNgua();
            xRNcr.DataSource = dt;
            xRNcr.DataMember = "Table";
            xRNcr.CreateDocument(false);
            xRNcr.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtNcrID.Text;
            PrintTool tool = new PrintTool(xRNcr.PrintingSystem);
            xRNcr.ShowPreviewDialog();
            kn.dongketnoi();     
        }

        private void btnBaoCaoChiPhiNV_Click(object sender, EventArgs e)
        {
            BaoCaoChiPhiNhanVien();
        }
        private void BaoCaoChiPhiNhanVien()
        {
            ketnoi kn = new ketnoi();
            gridControl5.DataSource = kn.laybang(@"SELECT NhanVienID,HoTen,Sothe,ChiPhiNhanVien,NoiDungViPham,SBB,SoNcr,
                KhongPhuHop,HinhThucXuLy FROM tblNCR R
                INNER JOIN NcrNhanVien N
                ON R.NcrID=N.NcrID where 
                NgayLap between '"+dpChiPhiNhanVienTu.Value.ToString("MM/dd/yyyy")+"' and '"+dpChiPhiNhanVienDen.Value.ToString("MM/dd/yyyy")+ "'");
            kn.dongketnoi();
            gridView6.ExpandAllGroups();
        }

        private void btnExpChiPhiNV_Click(object sender, EventArgs e)
        {
            gridControl5.ShowPrintPreview();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol;
            Gol = gridView1.GetFocusedDisplayText();
            txtCarID.Text = gridView1.GetFocusedRowCellDisplayText(carID_grid1);
        }

        private void btnBienBanViPham_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(
                @"SELECT NhanVienID,HoTen,Sothe,ChiPhiNhanVien,NoiDungViPham,SBB,SoNcr,
                KhongPhuHop,HinhThucXuLy FROM tblNCR R
                INNER JOIN NcrNhanVien N
                ON R.NcrID=N.NcrID where SBB='" + txtSoBB.Text + "'");
            XRBienBan xRBienBan = new XRBienBan();
            xRBienBan.DataSource = dt;
            xRBienBan.DataMember = "Table";
            xRBienBan.CreateDocument(false);
            xRBienBan.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtSoBB.Text;
            PrintTool tool = new PrintTool(xRBienBan.PrintingSystem);
            tool.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void btnDSNhanVien_Click(object sender, EventArgs e)
        {
            frmDanhSachNV danhSachNV = new frmDanhSachNV();
            danhSachNV.Show();
        }

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    PrintableComponentLink pcl1 = new PrintableComponentLink(new PrintingSystem());
        //    pcl1.Component = gridControl1;
        //    pcl1.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = "test123";
        //    pcl1.CreateDocument();
        //    PrintTool tool = new PrintTool(pcl1.PrintingSystem);
        //    tool.ShowPreviewDialog();
        //}
    }
}
