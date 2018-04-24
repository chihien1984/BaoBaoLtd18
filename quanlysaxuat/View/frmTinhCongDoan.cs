using DevExpress.XtraPrinting;
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
    public partial class frmTinhCongDoan : DevExpress.XtraEditors.XtraForm
    {
        public frmTinhCongDoan()
        {
            InitializeComponent();
        }
        public static string maSanPham;
        public static string soLuongSX;
        public static string maDonHang;
        public static string donHangID;
        public static string keHoachID;
        public static string sanPham;
       
        private void frmTinhCongDoan_Load(object sender, EventArgs e)
        {
            txtMaSanPham.Text =maSanPham;
            txtMaDonHang.Text = maDonHang;
            txtSoLuong.Text = soLuongSX;
            txtKeHoachID.Text = keHoachID;
            txtDonHangID.Text = donHangID;
            txtSanPham.Text = sanPham;
            txtNguoiDung.Text = Login.Username;
            DocDanhSachCongDoan();
        }
        private void DocDanhSachCongDoan()
        {

            //try
            //{
            //    {
            //        SqlConnection kn = new SqlConnection(Connect.mConnect);
            //        kn.Open();
            //        SqlCommand cmd = new SqlCommand("DSCongDoan_LenMay", kn);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add(new SqlParameter("@soLuong", SqlDbType.Float)).Value = double.Parse(txtSoLuong.Text);
            //        cmd.Parameters.Add(new SqlParameter("@maSanPham", SqlDbType.NVarChar)).Value = txtMaSanPham.Text;
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        gridControl1.DataSource = dt;
            //        kn.Close();
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show("Lỗi lý do:"+ex.Message); }
            double soLuong = double.Parse(txtSoLuong.Text);
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select id,Masp,Tensp, "
            +" Macongdoan,Tencondoan,Dinhmuc,HeSoDinhMuc, '"+ soLuong+ "'*HeSoDinhMuc/isnull(HeSoDinhMuc,1)/ "
			+" (case when Dinhmuc >=1 then Dinhmuc end) as TG_DUTINH, "
            +" '"+soLuong+"' * HeSoDinhMuc/isnull(HeSoDinhMuc,1)/ "
			+" ((case when Dinhmuc >=1 then Dinhmuc end)/8) as GIOTRONGNGAY, "
			+" Tothuchien,Macv,Dongia_CongDoan,NguyenCong from tblDMuc_LaoDong where Masp like '" + txtMaSanPham.Text+ "'  order by Macongdoan asc");
            kn.dongketnoi(); 
        }

        private void btnGhiDuLieu_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"insert into tblCalender_Product
                            (DonHangID,KeHoachID,Madh,
                             Masp,Soluong_DonHang,
                             CongDoan,TG_DUTINH,Thoigian_Dinhmuc,
                             NguoiTao,SanPham,CongDoanID,HeSoDinhMuc,NguyenCong,Ma_CongDoan,NgayTao)
                             VALUES ('{0}','{1}',N'{2}',N'{3}','{4}',N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',{10},{11},N'{12}',N'{13}',GetDate())",
                             txtDonHangID.Text,
                             txtKeHoachID.Text,
                             txtMaDonHang.Text,
                             txtMaSanPham.Text,
                             double.Parse(txtSoLuong.Text),
                             rowData["Tencondoan"],
                             rowData["TG_DUTINH"],
                             rowData["Dinhmuc"],
                             txtNguoiDung.Text,
                             txtSanPham.Text,
                             rowData["id"],
                             rowData["HeSoDinhMuc"], rowData["NguyenCong"], rowData["Macongdoan"]);         
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
            }
                con.Close();
                DocDanhSachTGLenMayTheoMaSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
            }
        }

        private void btnDocToanBoTGLenMay_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from SoGhiTG_SanXuat");
            kn.dongketnoi();
            gridView2.Columns["Sanpham"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
        }
        private void DocDanhSachTGLenMayTheoNgay() {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from SoGhiTG_SanXuat
                             where NgayTao between '" + dpTuNgay.Value.ToString("yyyy/MM/d")+"' and '"+dpDenNgay.Value.ToString("yyyy/mm/dd")+"'");
            kn.dongketnoi();
            gridView2.Columns["Sanpham"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
        }
        private void DocDanhSachTGLenMayTheoMaSP()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * from SoGhiTG_SanXuat
							 where DonHangID like '" + txtDonHangID.Text+"'");
            kn.dongketnoi();
            gridView2.Columns["Sanpham"].GroupIndex = 0;
            gridView2.ExpandAllGroups();
        }

        private void btnCapNhatNgay_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"update tblCalender_Product set GioBatDau='{0}',BatDau='{1}',GioKetThuc='{2}',KetThuc='{3}',
                                    GioKetThucDuTinh='{4}',KetThucDuTinh='{5}',NguoiCapNhat=N'{6}',TrangThai=N'{7}',LyDo=N'{8}',
                                    NgayCapNhat=GetDate() where NguonLucID='{9}'",
                             rowData["GioBatDau"], 
                             rowData["BatDau"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["BatDau"]).ToString("yyyy-MM-dd"),
                             rowData["GioKetThuc"],
                             rowData["KetThuc"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["KetThuc"]).ToString("yyyy-MM-dd"),
                             rowData["GioKetThucDuTinh"],
                             rowData["KetThucDuTinh"],
                             txtNguoiDung.Text,
                             rowData["TrangThai"],
                             rowData["LyDo"],
                              rowData["NguonLucID"]
                             );
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDanhSachTGLenMayTheoMaSP();
                }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex.Message);
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
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format("delete from tblCalender_Product where NguonLucID ='{0}'",
                    rowData["NguonLucID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDanhSachTGLenMayTheoMaSP();
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
            txtMaDonHang.Text = gridView2.GetFocusedRowCellDisplayText(maDH_grid2);
            txtMaSanPham.Text = gridView2.GetFocusedRowCellDisplayText(maSP_grid2);
            txtSoLuong.Text = gridView2.GetFocusedRowCellDisplayText(soLuong_grid2);
            txtDonHangID.Text = gridView2.GetFocusedRowCellDisplayText(DonHangID_grid2);
            txtKeHoachID.Text = gridView2.GetFocusedRowCellDisplayText(KeHoachID_grid2);
            txtSanPham.Text=gridView2.GetFocusedRowCellDisplayText(sanPham_grid2);
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {//Event save gridView 2
                case Keys.S:
                    if (e.Control) { btnCapNhatNgay.PerformClick();}break;
            }
            switch (e.KeyCode)
            {
                //Event save gridView 2
                case Keys.Delete:
                    if (e.Control) { btnXoa.PerformClick(); }
                    break;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.gridView1.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            PrintableComponentLink pl = new PrintableComponentLink(new PrintingSystem());
            pl.Component=gridControl1;
            pl.CreateMarginalHeaderArea += new CreateAreaEventHandler(Pl_CreateReportHeaderArea);
            pl.CreateDocument();
            pl.ShowPreviewDialog();
            this.gridView1.OptionsSelection.MultiSelectMode
         = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
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
    }
}
