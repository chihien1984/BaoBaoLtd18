using DevExpress.XtraEditors.Repository;
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
    public partial class frmCVKhuon : Form
    {
        public frmCVKhuon()
        {
            InitializeComponent();
        }
        private void DocCVKhuon()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT ID,SanPham,TenKhuon,NgayYC
                ,ThietKeBD,ThietKeKT,PhoiBD,PhoiKT
                ,GiaCongBD,GiaCongKT,LapRapBD,LapRapKT,ThuKhuonBD
                ,ThuKhuonKT,ChinhKhuonBD,ChinhKhuonKT,NgayNghiemThu,KetQua
                ,GhiChu,NguoiGhi,NgayGhi,NguoiSua,NgaySua FROM dbo.tblCVKhuon 
                where ketqua is null or KetQua=''");
            kn.dongketnoi();
        }
        private void DocTatCa()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT ID,SanPham,TenKhuon,NgayYC
                ,ThietKeBD,ThietKeKT,PhoiBD,PhoiKT
                ,GiaCongBD,GiaCongKT,LapRapBD,LapRapKT,ThuKhuonBD
                ,ThuKhuonKT,ChinhKhuonBD,ChinhKhuonKT,NgayNghiemThu,KetQua
                ,GhiChu,NguoiGhi,NgayGhi,NguoiSua,NgaySua FROM dbo.tblCVKhuon 
                where NgayGhi between '" + dptu_ngay.Value.ToString("yyyy-MM-dd") + "' and '" + dpden_ngay.Value.ToString("yyyy-MM-dd") + "'");
            kn.dongketnoi();
        }
        private void btnDSCongViecKhuon_Click(object sender, EventArgs e)
        {
            this.gridView2.OptionsView.NewItemRowPosition =
              DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.gridView2.OptionsSelection.MultiSelectMode
             = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            DocCVKhuon();
        }
        private void btnDocTatCa_Click(object sender, EventArgs e)
        {
            DocTatCa();
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
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblCVKhuon 
                            (SanPham,TenKhuon,NgayYC,NguoiGhi,NgayGhi) 
                            VALUES (N'{0}',N'{1}',N'{2}',N'{3}',GetDate())",
                          rowData["SanPham"], 
                          rowData["TenKhuon"],
                          rowData["NgayYC"]==DBNull.Value?"":
                          Convert.ToDateTime(rowData["NgayYC"]).ToString("yyyy-MM-dd"),
                          txtMember.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocCVKhuon();
        }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
}

        private void btnSua_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"update tblCVKhuon 
                           set SanPham=N'{0}',TenKhuon=N'{1}',NgayYC=N'{2}'
                            ,ThietKeBD=N'{3}',ThietKeKT=N'{4}',PhoiBD=N'{5}',PhoiKT=N'{6}'
                            ,GiaCongBD=N'{7}',GiaCongKT=N'{8}',LapRapBD=N'{9}',LapRapKT=N'{10}',ThuKhuonBD=N'{11}'
                            ,ThuKhuonKT=N'{12}',ChinhKhuonBD=N'{13}',ChinhKhuonKT=N'{14}',NgayNghiemThu=N'{15}',KetQua=N'{16}'
                            ,GhiChu=N'{17}',NguoiSua=N'{18}',NgaySua=GetDate() where ID ='{19}'",
                          rowData["SanPham"],
                          rowData["TenKhuon"],
                          rowData["NgayYC"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayYC"]).ToString("yyyy-MM-dd"),
                          rowData["ThietKeBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ThietKeBD"]).ToString("yyyy-MM-dd"),
                          rowData["ThietKeKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ThietKeKT"]).ToString("yyyy-MM-dd"),
                          rowData["PhoiBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["PhoiBD"]).ToString("yyyy-MM-dd"),
                          rowData["PhoiKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["PhoiKT"]).ToString("yyyy-MM-dd"),
                          rowData["GiaCongBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["GiaCongBD"]).ToString("yyyy-MM-dd"),
                          rowData["GiaCongKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["GiaCongKT"]).ToString("yyyy-MM-dd"),
                          rowData["LapRapBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["LapRapBD"]).ToString("yyyy-MM-dd"),
                          rowData["LapRapKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["LapRapKT"]).ToString("yyyy-MM-dd"),
                          rowData["ThuKhuonBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ThuKhuonBD"]).ToString("yyyy-MM-dd"),
                          rowData["ThuKhuonKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ThuKhuonKT"]).ToString("yyyy-MM-dd"),
                          rowData["ChinhKhuonBD"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ChinhKhuonBD"]).ToString("yyyy-MM-dd"),
                          rowData["ChinhKhuonKT"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["ChinhKhuonKT"]).ToString("yyyy-MM-dd"),
                          rowData["NgayNghiemThu"] == DBNull.Value ? "" : Convert.ToDateTime(rowData["NgayNghiemThu"]).ToString("yyyy-MM-dd"),
                          rowData["KetQua"],
                          rowData["GhiChu"],
                          txtMember.Text,
                          rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
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
                    string strQuery = string.Format(@"delete from tblCVKhuon where ID ='{0}'",
                          rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); DocTatCa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }

       

        private void btnTaomoi_Click(object sender, EventArgs e)
        {
            DSThemMoi();
            this.gridView2.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView2.OptionsSelection.MultiSelectMode
             = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        private void DSThemMoi()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"SELECT TOP 1 SanPham='',
            TenKhuon='',cast(NgayYC as date) NgayYC FROM dbo.tblCVKhuon");
            kn.dongketnoi();
        }
    
        private void frmCVKhuon_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtMember.Text = Login.Username;
            this.gridView2.OptionsSelection.MultiSelectMode 
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            DocCVKhuon();
        }
    }
}
