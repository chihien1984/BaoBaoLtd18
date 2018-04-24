using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class UcCapNhatPhieuSanXuat : UserControl
    {
        FtpClient ftpClient;
        public UcCapNhatPhieuSanXuat()
        {
            InitializeComponent();
            ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }

        Clsketnoi knn = new Clsketnoi();
     
        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        Clsketnoi connect = new Clsketnoi();
        private void DocLichSanXuatTheoDonHang()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select * from LichSanXuatTong
	                                       ('{0}','{1}') 
                                           where madh like N'{2}'
	                                       order by NgayLapLich ASC",
                                 dptu_ngay.Value.ToString("MM/dd/yyyy"),
                                 dpden_ngay.Value.ToString("MM/dd/yyyy"), 
                                 cbMa_DH.Text);
            gridControl3.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        #region formload
        private void UcCapNhatPhieuSanXuat_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpden_ngay.Text = DateTime.Today.AddDays(1).ToString("dd-MM-yyyy");
            DocDSMaDonHang();//Đọc mã đơn hàng vào CBDonDatHang
            MaNV();
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 7f);
        }
        #endregion
        private void DocDSMaDonHang()
        {
           ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select madh,max(ngaytrienkhai) ngaytrienkhai
                   from tblchitietkehoach where ngaytrienkhai 
	               between '{0}' and '{1}'  group by 
	               madh ORDER BY ngaytrienkhai DESC", 
                                 dptu_ngay.Value.ToString("MM-dd-yyyy"),
                                 dpden_ngay.Value.ToString("MM-dd-yyyy"));
                    cbMa_DH.DataSource = kn.laybang(sqlStr);
                    cbMa_DH.DisplayMember = "madh";
                    cbMa_DH.ValueMember = "madh";
            kn.dongketnoi();
        }
        private void cbMa_DH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocLichSanXuatTheoDonHang();
        }
        private void cbMa_DH_KeyPress(object sender, KeyPressEventArgs e)
        {
            DocLichSanXuatTheoDonHang();
        }
        private void PhanQuyenXoa()
        {
            if (Login.role == "1")
            {
                btnxoa.Enabled = true;
            }
            else
            {
                btnxoa.Enabled = false;
            }
        }
        private void btnDMDHSX_Click(object sender, EventArgs e)
        {
            DocLichSanXuatTheoNgay();
        }
        private void DocLichSanXuatTheoNgay()
        {
           ketnoi kn = new ketnoi();
           string sqlStr = string.Format(@"select * from LichSanXuatTong('{0}','{1}') order by NgayLapLich ASC",
                                    dptu_ngay.Value.ToString("MM/dd/yyyy"),
                                    dpden_ngay.Value.ToString("MM/dd/yyyy"));
           gridControl3.DataSource = kn.laybang(sqlStr);
           kn.dongketnoi();
        }

       
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

        private void btnGhi_KHSX_Click(object sender, EventArgs e)
        {
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("UPDATE tblchitietkehoach SET ngaytrienkhai=@ngaytrienkhai,KetThucTo_khuon=@KetThucTo_khuon, "
                    + "KetThucTo1=@KetThucTo1,KetThucToRapLD=@KetThucToRapLD,"
                    + "KetThucTo2=@KetThucTo2,KetThucTo3=@KetThucTo3, "
                    + "KetThucTo4=@KetThucTo4,KetThucTo5=@KetThucTo5, "
                    + "KetThucTo6=@KetThucTo6,KetThucTo7=@KetThucTo7, "
                    + "KetThucTo8=@KetThucTo8,KetThucTo9=@KetThucTo9, "
                    + "KetThucTo10=@KetThucTo10,KetThucTo11=@KetThucTo11, "
                    + "KetThucTo12=@KetThucTo12,KetThucTo14=@KetThucTo14, "
                    + "KetThucTo15=@KetThucTo15,KetThucTo16=@KetThucTo16, "
                    + "KetThucTo17=@KetThucTo17,KetThucTo18=@KetThucTo18, "
                    + "KetThucTo19=@KetThucTo19,KetThucTo20=@KetThucTo20, "
                    + "KetThucTo21=@KetThucTo21, "
                    + "KetThucToAnMon=@KetThucToAnMon, "
                    + "KetThucTo_HanBam=@KetThucTo_HanBam, NguoiLapLich = N'"+Login.Username+ "',NgayLapLich=GetDate() WHERE IDSP = '" + txtCode.Text + "'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataTable dt = new System.Data.DataTable();
                command.Parameters.Add(new SqlParameter("@ngaytrienkhai", SqlDbType.Date, 50)).Value = dpNgayLapKeHoach.Text;
                try
                {
                    if (KetThucTo_khuon.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo_khuon", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo_khuon", SqlDbType.Date, 50)).Value = KetThucTo_khuon.Text;
                    if (KetThuc1.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo1", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo1", SqlDbType.Date, 50)).Value = KetThuc1.Text;
                    if (KetThucToRapLD.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucToRapLD", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucToRapLD", SqlDbType.Date, 50)).Value = KetThucToRapLD.Text;
                    if (KetThucDap2.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo2", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo2", SqlDbType.Date, 50)).Value = KetThucDap2.Text;        
                    if (KetThuc3.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo3", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo3", SqlDbType.Date, 50)).Value = KetThuc3.Text;                   
                    if (KetThuc4.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo4", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo4", SqlDbType.Date, 50)).Value = KetThuc4.Text;
                    if (KetThuc5.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo5", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo5", SqlDbType.Date, 50)).Value = KetThuc5.Text;               
                    if (KetThuc6.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo6", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo6", SqlDbType.Date, 50)).Value = KetThuc6.Text;       
                    if (KetThuc7.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo7", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo7", SqlDbType.Date, 50)).Value = KetThuc7.Text;
                    if (KetThuc8.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo8", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo8", SqlDbType.Date, 50)).Value = KetThuc8.Text;
                    if (KetThuc9.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo9", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo9", SqlDbType.Date, 50)).Value = KetThuc9.Text;     
                    if (KetThuc10.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo10", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo10", SqlDbType.Date, 50)).Value = KetThuc10.Text;                  
                    if (KetThuc11.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo11", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo11", SqlDbType.Date, 50)).Value = KetThuc11.Text;            
                    if (KetThuc12.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo12", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo12", SqlDbType.Date, 50)).Value = KetThuc12.Text;   
                    if (KetThuc14.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo14", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo14", SqlDbType.Date, 50)).Value = KetThuc14.Text;
                    if (KetThuc15.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo15", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo15", SqlDbType.Date, 50)).Value = KetThuc15.Text;
                    if (KetThuc16.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo16", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo16", SqlDbType.Date, 50)).Value = KetThuc16.Text;         
                    if (KetThuc17.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo17", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo17", SqlDbType.Date, 50)).Value = KetThuc17.Text;
                    if (KetThuc18.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo18", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo18", SqlDbType.Date, 50)).Value = KetThuc18.Text;
                    if (KetThuc19.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo19", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo19", SqlDbType.Date, 50)).Value = KetThuc19.Text;
                    if (KetThuc20.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo20", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo20", SqlDbType.Date, 50)).Value = KetThuc20.Text;
                    if (KetThuc21.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo21", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo21", SqlDbType.Date, 50)).Value = KetThuc21.Text;
                    if (KetThucTo_HanBam.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucTo_HanBam", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucTo_HanBam", SqlDbType.Date, 50)).Value = KetThucTo_HanBam.Text;
                    if (KetThucToAnMon.Text == "")
                        command.Parameters.Add(new SqlParameter("@KetThucToAnMon", SqlDbType.Date, 50)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@KetThucToAnMon", SqlDbType.Date, 50)).Value = KetThucToAnMon.Text;
                }
                catch { MessageBox.Show("Không thành công", "Thông báo");}
                adapter.Fill(dt); Ghi_TrangThai();
                DocLichSanXuatTheoDonHang();
                cn.Close();
            }
        }
        private void Ghi_TrangThai()//Cap nhat trang thai 'LapKeHoach' columns trang thai tblchitietkehoach
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblchitietkehoach set Trangthai=N'LapKeHoach' where IDSP like '" + txtCode.Text+ "'");
            kn.dongketnoi();
        }
        private void gridControl3_Click(object sender, EventArgs e)
        {
            Gol = gridView3.GetFocusedDisplayText();
            txtIdPSX.Text=gridView3.GetFocusedRowCellDisplayText(IdPSX);
            txtSOCHITIET.Text = gridView3.GetFocusedRowCellDisplayText(Soluongct_grid);
            txtDONVISP_DONHANG.Text= gridView3.GetFocusedRowCellDisplayText(donvisp_grid3);
            KetThucTo_khuon.Text= gridView3.GetFocusedRowCellDisplayText(Catkhuon_grid);
            txtCode.Text = gridView3.GetFocusedRowCellDisplayText(Code_Gridsp);
            cbMa_DH.Text= gridView3.GetFocusedRowCellDisplayText(Madh_grid);
            txtMa_SP.Text= gridView3.GetFocusedRowCellDisplayText(Masp_grid);
            txtTENGOISP_DONHANG.Text= gridView3.GetFocusedRowCellDisplayText(TengoispDH_grid);
            txtKinhdoanh.Text= gridView3.GetFocusedRowCellDisplayText(Kinhdoanh_grid);
            txtTenSP.Text= gridView3.GetFocusedRowCellDisplayText(Tenquicach_grid);
            txtSLSP_DH.Text= gridView3.GetFocusedRowCellDisplayText(SoluongspDonhang_grid);
            txtSL_DH.Text= gridView3.GetFocusedRowCellDisplayText(SoluongDH_grid);
            txtSL_TonKho.Text= gridView3.GetFocusedRowCellDisplayText(Soluongtonkho_grid);
            txtSL_SX.Text= gridView3.GetFocusedRowCellDisplayText(Soluongsx_grid);
            txtNgoaiQuan.Text= gridView3.GetFocusedRowCellDisplayText(Ngoaiquan_grid);
            txtDonvi.Text= gridView3.GetFocusedRowCellDisplayText(Donvi_grid);
            txtGhiChu.Text= gridView3.GetFocusedRowCellDisplayText(ghichu_grid);
            dpNgaybatdau.Text= gridView3.GetFocusedRowCellDisplayText(Ngaybatdau_grid);
            dpngayketthuc.Text= gridView3.GetFocusedRowCellDisplayText(Ngaykethuc_grid);
            dpngayghi.Text= gridView3.GetFocusedRowCellDisplayText(Ngay_TK_grid);
            KetThuc1.Text= gridView3.GetFocusedRowCellDisplayText(Dap1_grid);
            KetThucDap2.Text= gridView3.GetFocusedRowCellDisplayText(Dap2_grid);
            KetThuc3.Text = gridView3.GetFocusedRowCellDisplayText(To3_grid);
            KetThuc4.Text = gridView3.GetFocusedRowCellDisplayText(To4_grid);
            KetThuc5.Text = gridView3.GetFocusedRowCellDisplayText(To5_grid);
            KetThuc6.Text = gridView3.GetFocusedRowCellDisplayText(To6_grid);
            KetThuc7.Text = gridView3.GetFocusedRowCellDisplayText(To7_grid);
            KetThuc8.Text = gridView3.GetFocusedRowCellDisplayText(To8_grid);
            KetThuc9.Text = gridView3.GetFocusedRowCellDisplayText(To9_grid);
            KetThuc10.Text = gridView3.GetFocusedRowCellDisplayText(To10_grid);
            KetThuc11.Text = gridView3.GetFocusedRowCellDisplayText(To11_grid);
            KetThuc12.Text = gridView3.GetFocusedRowCellDisplayText(To12_grid);
            KetThuc14.Text = gridView3.GetFocusedRowCellDisplayText(To14_grid);
            KetThuc15.Text = gridView3.GetFocusedRowCellDisplayText(To15_grid);
            KetThuc16.Text = gridView3.GetFocusedRowCellDisplayText(To16_grid);
            KetThuc17.Text = gridView3.GetFocusedRowCellDisplayText(To17_grid);
            KetThuc18.Text = gridView3.GetFocusedRowCellDisplayText(To18_grid);
            KetThuc19.Text= gridView3.GetFocusedRowCellDisplayText(To19_grid);
            KetThuc20.Text = gridView3.GetFocusedRowCellDisplayText(To20_grid);
            KetThuc21.Text = gridView3.GetFocusedRowCellDisplayText(To21_grid);
            KetThucTo_HanBam.Text= gridView3.GetFocusedRowCellDisplayText(ToBamHan_grid);
            KetThucToAnMon.Text= gridView3.GetFocusedRowCellDisplayText(ToAnMon_grid);
            KetThuc21.Text = gridView3.GetFocusedRowCellDisplayText(To21_grid);
            KetThucToRapLD.Text = gridView3.GetFocusedRowCellDisplayText(RapLD_grid);
        }

        private void btnfresh_Click(object sender, EventArgs e)
        {
            DocLichSanXuatTheoDonHang();
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }
        private void Xuatphieu()
        {
        }
        private void Xuat_kehoachsx(System.Data.DataTable dt, string filename)//Xuất phiếu
        {
            if (dt.Rows.Count == 0) { return; }
            Excel.Application App = null;
            Excel.Workbook Wb;
            Excel.Worksheet Ws;
            int isExcelOpen = 0;
            try
            {
                App = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                App = new Excel.Application();
                isExcelOpen = 1;
            }

            //oXL.Visible = true;

            Wb = (Excel.Workbook)(App.Workbooks.Add(Missing.Value));
            Ws = (Excel.Worksheet)Wb.ActiveSheet;

            try
            {
                Ws = (Excel.Worksheet)App.Worksheets.Add();

                // Xử lý tiêu đề cột

                int rowCount = dt.Rows.Count;
                int colCount = dt.Columns.Count;
                int c = 0;
                int r = 0;

                Excel.Range HeaderRow = Ws.get_Range("A1");

                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    HeaderRow.get_Offset(0, r).Value2 = dc.ColumnName;
                    r++;
                }

                HeaderRow.EntireRow.Font.Bold = true;
                HeaderRow.EntireRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // Xử lý data-> mảng 2 chiều

                object[,] rowData = new object[rowCount, colCount];

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        if (IsNumeric(row[col].GetType().ToString()))
                        {
                            rowData[c, col] = System.Convert.ToDouble(row[col].ToString());
                        }
                        else
                        {
                            rowData[c, col] = row[col].ToString();
                        }

                    }
                    c++;
                }

                // Paste mảng vào excel

                Ws.get_Range("A2").get_Resize(c, colCount).Value2 = rowData;
                Ws.get_Range("A1").get_Resize(1, colCount).EntireColumn.AutoFit();

                // Lưu file

                string ExcelPath = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0}.xlsx", filename);

                if (System.IO.File.Exists(ExcelPath))
                {
                    System.IO.File.Delete(ExcelPath);
                }

                Wb.SaveAs(ExcelPath, AccessMode: Excel.XlSaveAsAccessMode.xlShared);
                Wb.Close();
                if (isExcelOpen == 1) { App.Quit(); }
                dt.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Dọn rác
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Ws);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);        
        }

        private static bool IsNumeric(string stype)
        {
            //return stype != null && "Byte,Decimal,Double,Int16,Int32,Int64,SByte,Single,UInt16,UInt32,UInt64,".Contains("stype");
            string stringToCheck = stype;
            string[] stringArray = { "System.Byte", "System.Decimal", "System.Double", "System.Int16", "System.Int32", "System.Int64", "System.Single", "System.UInt16", "System.UInt32", "System.UInt64" };
            foreach (string x in stringArray)
            {
                if (stringToCheck.Contains(x))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnxoa_Click(object sender, EventArgs e)//Huy trang thai
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblchitietkehoach set Trangthai='' where IDSP like '" + txtCode.Text+ "'");
            kn.dongketnoi();
            DocLichSanXuatTheoDonHang();
            kn.dongketnoi();
        }

        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMa_SP.Text);
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
            frmLoading f2 = new frmLoading(txtMa_SP.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void txtXuat_kehoachsx_Click(object sender, EventArgs e)
        {
            string tenfiletuyy = "FILE TRIEN KHAI";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select IDSP,CT.nvkd,Convert(nvarchar,ngaytrienkhai,101) ngaytrienkhai,CT.madh,mabv,sanpham, "
                   +" SPLR,SLSPLR,So_CT,soluongyc,tonkho,  "
                   +" soluongsx,ngoaiquang,donvi,Convert(nvarchar,daystar,101)daystar,Convert(nvarchar,dayend,101)dayend,CT.khachhang, "
                   +" Convert(nvarchar,KetThucTo_khuon,101)KetThucTo_khuon,Convert(nvarchar,KetThucTo17,101)KetThucTo17,Convert(nvarchar,KetThucTo4,101)KetThucTo4, "
                   +" Convert(nvarchar,KetThucTo1,101)KetThucTo1,Convert(nvarchar,KetThucTo2,101)KetThucTo2,Convert(nvarchar,KetThucToRapLD,101)KetThucToRapLD,Convert(nvarchar,KetThucTo3,101)KetThucTo3,Convert(nvarchar,KetThucTo5,101)KetThucTo5, "
                   +" Convert(nvarchar,KetThucToAnMon,101)KetThucToAnMon,Convert(nvarchar,KetThucTo_HanBam,101)KetThucTo_HanBam,Convert(nvarchar,KetThucTo6,101)KetThucTo6,Convert(nvarchar,KetThucTo8,101)KetThucTo8,Convert(nvarchar,KetThucTo9,101)KetThucTo9, "
                   +" Convert(nvarchar,KetThucTo10,101)KetThucTo10,Convert(nvarchar,KetThucTo12,101)KetThucTo12,Convert(nvarchar,KetThucTo14,101)KetThucTo14,Convert(nvarchar,KetThucTo7,101)KetThucTo7,Convert(nvarchar,KetThucTo18,101)KetThucTo18, "
                   +" Convert(nvarchar,KetThucTo19,101)KetThucTo19,Convert(nvarchar,KetThucTo11,101)KetThucTo11,Convert(nvarchar,KetThucTo15,101)KetThucTo15,Convert(nvarchar,KetThucTo16,101)KetThucTo16,Convert(nvarchar,KetThucTo20,101)KetThucTo20,Convert(nvarchar,KetThucTo21,101)KetThucTo21,xeploai,Ghichu, "
                   + " DH.Diengiai,Maubv,Donvisp,ChatlieuCT,IdPSX "
                   +" from tblchitietkehoach CT left outer join tblDONHANG DH on CT.madh = DH.madh "
                   +" where CT.madh like N'" + cbMa_DH.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            ExcelExport.DataTableToExcel(dt, tenfiletuyy);
            DocLichSanXuatTheoDonHang();
            con.Close();
            string pat = AppDomain.CurrentDomain.BaseDirectory +"\\FILE TRIEN KHAI.xlsm";
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, cbMa_DH.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Chưa có lưu trên hệ thống"); }

        }
        private void btnLayout_PSX_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(cbMa_DH.Text, txtPath_PSX.Text);
            f2.Show();
        }

        private void btnKhuon_Click(object sender, EventArgs e)
        {
            KhuonMau.maSP = txtMa_SP.Text;
            KhuonMau VTKhuon = new KhuonMau();
            VTKhuon.ShowDialog();
        }

        private void btnVatTu_Click(object sender, EventArgs e)
        {
            frmPrVatTu.madh = cbMa_DH.Text;
            frmPrVatTu fVatTu = new frmPrVatTu();
            fVatTu.ShowDialog();
        }

        private void btnCongdoan_Click(object sender, EventArgs e)
        {
            frmDM_CONGDOAN DMCONGDOAN = new frmDM_CONGDOAN();
            DMCONGDOAN.ShowDialog();
        }

        private void gridControl3_KeyDown(object sender, KeyEventArgs e)
        {
            frmPrVatTu.madh = cbMa_DH.Text;
            if (e.Control && e.KeyCode == Keys.A)
            {
                frmPrVatTu VatTu = new frmPrVatTu();
                VatTu.ShowDialog();
            }
        }

        private void BtnUpServer_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtLocal.Text = openFileDialog1.FileName;
                    string fullFileName = openFileDialog1.FileName;
                    string fileName = openFileDialog1.SafeFileName;
                    ftpClient.upload("sanxuat_sanxuat/DM_KEHOACH_SANXUAT/" + fileName, fullFileName);
                    if (ftpClient.message == "success")
                    {
                        MessageBox.Show(ftpClient.pathFileName);
                    }
                    else
                    {
                        MessageBox.Show(ftpClient.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            ketnoi Connect = new ketnoi();
            dt = Connect.laybang("select * from PHIEUSANXUAT where madh like N'" + cbMa_DH.Text + "'");
            XRPhieuSX_DaDuyet rpPHIEUSANXUAT_Duyet = new XRPhieuSX_DaDuyet();
            rpPHIEUSANXUAT_Duyet.DataSource = dt;
            rpPHIEUSANXUAT_Duyet.DataMember = "Table";
            rpPHIEUSANXUAT_Duyet.CreateDocument(false);
            rpPHIEUSANXUAT_Duyet.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = cbMa_DH.Text;
            PrintTool tool = new PrintTool(rpPHIEUSANXUAT_Duyet.PrintingSystem);
            rpPHIEUSANXUAT_Duyet.ShowPreviewDialog();
            Connect.dongketnoi();

        }

        private void BtnDuKienKeHoach_Click(object sender, EventArgs e)
        { Cat(); Dap(); Tien(); Bulon(); Han(); Mai(); STD(); DG(); }
        private void Cat()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'"+txtMaNL_Cat.Text+"' AND IdDonHang like '"+txtIdPSX.Text+"' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc4.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void Dap()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLDap.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc1.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void Tien()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLTien.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc12.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void Bulon()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLBulon.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc14.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void Han()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLHan.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc8.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void Mai()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLMai.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc9.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void STD()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLSon.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc10.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }
        private void DG()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT idDonHang,Ma_NguonLuc,MIN(BatDau)BatDau,MAX(KetThuc)KetThuc FROM  tblCalender_Product "
                 + " WHERE Ma_NguonLuc LIKE N'" + txtMa_NLDongGoi.Text + "' AND IdDonHang like '" + txtIdPSX.Text + "' GROUP BY IdDonHang,Ma_NguonLuc", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    KetThuc11.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch (Exception) { }
        }

        private void btnExportDonHangTrienKhai_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void btnBaoCaoSanLuongHoanThanh_Click(object sender, EventArgs e)
        {
            quanlysanxuat.View.BaoCaoSanLuongHoanThanh_UC baoCaoSanLuongHoanThanh 
                = new View.BaoCaoSanLuongHoanThanh_UC();
            baoCaoSanLuongHoanThanh.Show();
        }
    }
}