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
using System.IO;
using iText.StyledXmlParser.Jsoup;
using quanlysanxuat.Report;
using quanlysanxuat.Model;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class UcQA : DevExpress.XtraEditors.XtraForm
    {
        public UcQA()
        {
            InitializeComponent();
        }

        //formload 
        private void UcQA_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy"); 
            dpDen.Text = DateTime.Now.ToString();
            dpTu1.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen1.Text = DateTime.Now.ToString();
            dpTu2.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen2.Text = DateTime.Now.ToString();
            dpTuTKTheoTo.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDenTKTheoTo.Text = DateTime.Now.ToString();
            dpTuNgayTKTo.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDenNgayTKTo.Text = DateTime.Now.ToString();
            SetRoile();
            this.gvChatLuongHoatDong.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvChatLuongHoatDong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gvNoiDungLoi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvNoiDungLoi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gvNoiPhatSinhLoi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvNoiPhatSinhLoi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //THChatLuongHoatDong();
            THPhanLoaiChatLuong();
            THNoiDungChatLuong();//
            THNoiPhatSinhLoi();//Danh sach noi phat sinh kiem tra QC
            THChatLuongHoatDong();
        }
        private void SetRoile()
        {
            if (ClassUser.User=="99999"|| (ClassUser.User == "01882")|| (Login.role == "00770"))
            {
                btnCapNhatChatLuongHoatDong.Enabled = true;
                btnReportKhacPhucPhongNgua.Enabled = true;
                btnTaoMoi.Enabled = true;
                btnThem.Enabled = true;
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
                //Thêm nơi phát sinh
                btnTaoMoiNoiPhatSinhLoi.Enabled = true;
                btnThemMoiNoiPhatSinhLoi.Enabled = true;
                btnCapNhatNoiPhatSinhLoi.Enabled = true;
                btnXoaNoiPhatSinhLoi.Enabled = true;
            }
            else if (Login.role == "2042")
            {
                btnReportKhacPhucPhongNgua.Enabled = true;
            }
            else
            {
                btnCapNhatChatLuongHoatDong.Enabled = false;
                btnReportKhacPhucPhongNgua.Enabled = false;
                btnTaoMoi.Enabled = false;
                btnThem.Enabled = false;
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;
                btnTaoMoiNoiPhatSinhLoi.Enabled = false;
                btnThemMoiNoiPhatSinhLoi.Enabled = false;
                btnCapNhatNoiPhatSinhLoi.Enabled = false;
                btnXoaNoiPhatSinhLoi.Enabled = false;
            }
        }
        // Thể hiện nội dung chất lượng
        private void THNoiDungChatLuong()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select ID,MaLoi,NoiDungLoi,NguoiLap,NgayLap from ChatLuongNoiDung");
            grNoiDungLoi.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
            this.gvNoiDungLoi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        // Thể hiện nội dung chất lượng
        private void THNoiDungChatLuongMoi()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select top 0 ID,MaLoi,NoiDungLoi,NguoiLap,NgayLap from ChatLuongNoiDung");
            grNoiDungLoi.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
            this.gvNoiDungLoi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }
        private void THPhanLoaiChatLuong()
        {
            repositoryItemComboBoxPhanLoai.Items.Clear();
            repositoryItemComboBoxPhanLoai.Items.Add("");
            repositoryItemComboBoxPhanLoai.Items.Add("NCR");
        }
        private void CapNhatChatLuongHoatDong()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvChatLuongHoatDong.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChatLuongHoatDong.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update ChatLuongHoatDong set 
					     ChiTietSanPham = N'{0}',
					     NoiDungLoi = N'{1}',
					     ThongTinLoi = N'{2}',
					     NoiLap = N'{3}',
					     SoLuongSanXuat = '{4}',
					     SoLuongKiem = '{5}',
					     SoLuongLoi = '{6}',
					     SoLuongDonHang = '{7}',
					     LoaiDeNghi = '{8}',
                         NguoiHieuChinh = N'{9}',
                         NgayHieuChinh = GetDate()
                         where ID like '{10}'",
                        rowData["ChiTietSanPham"], rowData["NoiDungLoi"], 
                        rowData["ThongTinLoi"], rowData["NoiLap"], 
                        rowData["SoLuongSanXuat"], rowData["SoLuongKiem"],
                        rowData["SoLuongLoi"], rowData["SoLuongDonHang"],
                        rowData["LoaiDeNghi"],
                        Login.Username,
                        rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THChatLuongHoatDong();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void XoaChatLuongHoatDong()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvChatLuongHoatDong.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChatLuongHoatDong.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update ChatLuongHoatDong set 
					     IsDelete = 1,NguoiHieuChinh = N'{0}'
                         NgayHieuChinh = GetDate()
                         where ID like '{1}'",
                        rowData["NguoiHieuChinh"],
                        rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THChatLuongHoatDong();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void btnCapNhatChatLuongHoatDong_Click(object sender, EventArgs e)
        {
            CapNhatChatLuongHoatDong();
        }

        private void btnXoaChatLuongHoatDong_Click(object sender, EventArgs e)
        {
            XoaChatLuongHoatDong();
        }

        private void btnReportKhacPhucPhongNgua_Click(object sender, EventArgs e)
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from ViewChatLuongHoatDong where 
                                   LoaiDeNghi like 'NCR' and ID like '{0}'", iD);
            ReportKhacPhucPhongNgua kppn = new ReportKhacPhucPhongNgua();
            kppn.DataSource = Function.GetDataTable(sqlQuery);
            kppn.DataMember = "Table";
            kppn.CreateDocument(false);
            kppn.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = "NCR-No-" + iD;
            PrintTool tool = new PrintTool(kppn.PrintingSystem);
            kppn.ShowPreviewDialog();
            Function.Disconnect();
        }
        string iD;
        string maDonHang;
        string maSanPham;
        private void Binding(object sender, EventArgs e)
        {
            
        }
     
        private async void THChatLuongHoatDong()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select row_number() 
                     over(order by ID desc) as STT,*
					 from ChatLuongHoatDong where
					 NgayLap between '{0}' and '{1}'
					 and IsDelete is null order by ID desc",
                         dpTu2.Value.ToString("yyyy-MM-dd"),
                         dpDen2.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() =>
                {
                    grChatLuongHoatDong.DataSource = null;
                    grChatLuongHoatDong.DataSource = Function.GetDataTable(sqlQuery);
                }));
            });
        }

        //function convert Tag html to text
        public static String html2text(String html)
        {
            return Jsoup.Parse(html).Text();
        }
        private void Export(object sender, EventArgs e) { gvChatLuongHoatDong.ShowPrintPreview(); }
        private void ATCHOTEN_QC()
        {
            //string ConString = Connect.mConnect;
            //using (SqlConnection con = new SqlConnection(ConString))
            //{
            //    SqlCommand cmd = new SqlCommand("select HoTen from tblDSNHANVIEN where MaBP like N'TQLCL'", con);
            //    con.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            //    while (reader.Read())
            //    {
            //        MyCollection.Add(reader.GetString(0));
            //    }
            //    txtNguoiKiem.AutoCompleteCustomSource = MyCollection;
            //    con.Close();
            //}
        }
        private void ATC_NOIDUNG()
        {
            //string ConString = Connect.mConnect;
            //using (SqlConnection con = new SqlConnection(ConString))
            //{
            //    SqlCommand cmd = new SqlCommand("select NoidungQC from tblchitietkehoach where QC is not null", con);
            //    con.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            //    while (reader.Read())
            //    {
            //        MyCollection.Add(reader.GetString(0));
            //    }
            //    txtNoidung.AutoCompleteCustomSource = MyCollection;
            //    con.Close();
            //}
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            //string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{
            //    MessageBox.Show("Mã sản không khớp đúng");
            //}
        }
        private void Goibanve(object sender, EventArgs e)
        {
            //frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            //f2.Show();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //frmQAControl.Madh = txtMadh.Text;
            //frmQAControl fQA = new frmQAControl();
            //fQA.ShowDialog();
        }
        private void Layout_QTSX()
        {
            //string pat = string.Format(@"{0}\QTSX-{1}.PDF", this.txtPath_QTSX.Text, txtMasp.Text);
            //if (File.Exists(pat))
            //{
            //    System.Diagnostics.Process.Start(pat);
            //}
            //else
            //{ MessageBox.Show("QTSX chưa có trong hệ thống", "Liên hệ QA"); }
        }
        private void txtQuiTrinh_Click(object sender, EventArgs e)
        {
            //frmLoading f2 = new frmLoading(txtQuiTrinh.Text, txtPath_QTSX.Text);
            //f2.Show();
        }

        private void btnExportChatLuongHoatDong_Click(object sender, EventArgs e)
        {
            this.gvChatLuongHoatDong.OptionsSelection.MultiSelectMode 
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grChatLuongHoatDong.ShowPrintPreview();
            this.gvChatLuongHoatDong.OptionsSelection.MultiSelectMode
              = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(maSanPham, path.pathbanve);
            f2.Show();
        }

        private void btnLenhSanXuatDaKyCamKet_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(maDonHang, path.pathkinhdoanh);
            f2.Show();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            THNoiDungChatLuongMoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into ChatLuongNoiDung 
                    (MaLoi,NoiDungLoi,NguoiLap,NgayLap)
                    values (N'{0}',N'{1}',N'{2}',GetDate())", 
                    rowData["MaLoi"], rowData["NoiDungLoi"],Login.Username);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiDungChatLuong();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update ChatLuongNoiDung 
                    set MaLoi=N'{0}',NoiDungLoi=N'{1}',NguoiLap=N'{2}',NgayLap=GetDate() where ID like '{3}'",
                    rowData["MaLoi"], rowData["NoiDungLoi"], Login.Username,rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiDungChatLuong();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiDungLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiDungLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from ChatLuongNoiDung where ID like '{0}'",rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiDungChatLuong();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnTraCuuNhatKy_Click(object sender, EventArgs e)
        {
            THChatLuongHoatDong();
        }
        //The hien noi phat sinh loi
        private void THNoiPhatSinhLoi()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select ID,Ma,Ten,ToThucHien,Nguoi,Ngay from NoiKiemTra");
            grNoiPhatSinhLoi.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }
        //Tao mới nơi phát sinh loi
        private void TaoMoiNoiPhatSinhLoi()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select top 0 ID,Ma,Ten,ToThucHien,Nguoi,Ngay from NoiKiemTra");
            grNoiPhatSinhLoi.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
        }

        private void btnThemMoiNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiPhatSinhLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiPhatSinhLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into NoiKiemTra 
                        (Ma,Ten,ToThucHien,Nguoi,Ngay)
                        values (N'{0}',N'{1}',N'{2}',N'{3}',GetDate())",
                    rowData["Ma"], rowData["Ten"],
                    rowData["ToThucHien"], 
                    Login.Username);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiPhatSinhLoi();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnCapNhatNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiPhatSinhLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiPhatSinhLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update NoiKiemTra set Ma=N'{0}',Ten=N'{1}',
                    ToThucHien=N'{2}',Nguoi=N'{3}',Ngay=Getdate()
                    where ID like '{4}'", rowData["Ma"], rowData["Ten"],
                    rowData["ToThucHien"],
                    Login.Username,rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiPhatSinhLoi();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnXoaNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvNoiPhatSinhLoi.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvNoiPhatSinhLoi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete NoiKiemTra where ID like '{0}'",rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                THNoiPhatSinhLoi();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnExportNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            grNoiPhatSinhLoi.ShowPrintPreview();
        }

        private void btnTaoMoiNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select top 0 ID,Ma,Ten,ToThucHien,Nguoi,Ngay from NoiKiemTra");
            grNoiPhatSinhLoi.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();
            this.gvNoiPhatSinhLoi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        }

        private void btnTraCuuNoiPhatSinhLoi_Click(object sender, EventArgs e)
        {
            THNoiPhatSinhLoi();
        }

        private void btnExportTheoSanPham_Click(object sender, EventArgs e)
        {
            grThongKeLoiTheoSanPham.ShowPrintPreview();
        }

        private void btnThongKeTheoNhom_Click(object sender, EventArgs e)
        {
            grThongKeLoiTheoNhom.ShowPrintPreview();
        }

        private void btnThongKeLoiTheoSanPham_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format("execute TyLeLoiTapTrungTheoNhom '{0}','{1}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grThongKeLoiTheoSanPham.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();
        }

        private void btnThongKeLoiTheoNhom_Click(object sender, EventArgs e)
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format("execute TyLeLoiTapTrungTheoDang '{0}','{1}'",
             dpTu1.Value.ToString("yyyy-MM-dd"),
             dpDen1.Value.ToString("yyyy-MM-dd"));
            grThongKeLoiTheoNhom.DataSource = Function.GetDataTable(sqlQuery);
            Function.Disconnect();
        }

        private void btnTraCuuTheoTo_Click(object sender, EventArgs e)
        {
            THThongKeTheoTo();
        }
        private async void THThongKeTheoTo()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoi Nhom,MaDonHang,MaSanPham,ChiTietSanPham,
		        MaLoi,NoiLap,NoiDungLoi,NgayLap,Loi,
		        SoLuongKiem,SoLuongLoi 
		        from viewNhatKyChatLuong 
		        where NgayLap between '{0}' and '{1}'",
                dpTuTKTheoTo.Value.ToString("MM-dd-yyyy"),
                dpDenTKTheoTo.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grThongKeTheoTo.DataSource = null;
                    grThongKeTheoTo.DataSource = Function.GetDataTable(sqlQuery);
                }));
            });
            gvThongKeTheoTo.Columns["Nhom"].GroupIndex = 0;
            gvThongKeTheoTo.ExpandAllGroups();
        }

        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            gvThongKeTheoTo.Columns["Nhom"].GroupIndex = -1;
            gvThongKeTheoTo.ExpandAllGroups();
        }

        private void btnTongHopTheoTo_Click(object sender, EventArgs e)
        {
            THTongHopTheoTo();
        }
        private async void THTongHopTheoTo()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute TyLeLoiTapTrungTheoTo '{0}','{1}'",
                dpTuNgayTKTo.Value.ToString("MM-dd-yyyy"),
                dpDenNgayTKTo.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grTongHopTo.DataSource = null;
                    grTongHopTo.DataSource = Function.GetDataTable(sqlQuery);
                }));
            });
            gvTongHopTo.Columns["Nhom"].GroupIndex = 0;
            gvTongHopTo.ExpandAllGroups();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gvTongHopTo.Columns["Nhom"].GroupIndex = -1;
        }

        private void btnExportTongHop_Click(object sender, EventArgs e)
        {
            grTongHopTo.ShowPrintPreview();
        }

        private void gvChatLuongHoatDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvChatLuongHoatDong.GetRowCellValue(gvChatLuongHoatDong.FocusedRowHandle, gvChatLuongHoatDong.Columns["ID"]) == null)
                return;
            else
            {

                iD = gvChatLuongHoatDong.GetRowCellValue(gvChatLuongHoatDong.FocusedRowHandle, gvChatLuongHoatDong.Columns["ID"]).ToString();
                maDonHang = gvChatLuongHoatDong.GetRowCellValue(gvChatLuongHoatDong.FocusedRowHandle, gvChatLuongHoatDong.Columns["MaDonHang"]).ToString();
                maSanPham = gvChatLuongHoatDong.GetRowCellValue(gvChatLuongHoatDong.FocusedRowHandle, gvChatLuongHoatDong.Columns["MaSanPham"]).ToString();
            }
        }
    }
}
