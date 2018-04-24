using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Report;

namespace quanlysanxuat
{
    public partial class frmBCHangTre : Form
    {
        public frmBCHangTre()
        {
            InitializeComponent();
        }

        private void DocDuLieuTre()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select * 
                from PHANTICH_CHUNG WHERE STATUS !='HOAN THANH'
                ORDER BY IDSP DESC");
            kn.dongketnoi();
        }
        public static int ketQua;
        private void KetQua()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(@"select count(*) from PHANTICH_CHUNG 
                WHERE STATUS !='HOAN THANH'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtTongSoTre.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void TongDonHangTre()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(@"select count(*) from PHANTICH_CHUNG 
                WHERE STATUS =N'Trễ' or STATUS =N'Đỏ'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtDonHangTre.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            KetQua();
            TongDonHangTre();
            int tongTre = Convert.ToInt32(txtTongSoTre.Text);
            int pageSize = Convert.ToInt32(txtPageSize.Text);
            if (i == tongTre/pageSize)
            {
                i = 0;
            }
            i++;
            txtPageNum.Text = i.ToString();
            DuyetTrang();
        }

        private void DuyetTrang()
        {
            try
            {
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("spPhanTrang_Table", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int)).Value = txtPageNum.Text;
                    cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int)).Value = txtPageSize.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    cn.Close();
                }
            }
            catch (Exception) { }
        }
        public static string member;
        private void frmBCHangTre_Load(object sender, EventArgs e)
        {
            KetQua();
            TongDonHangTre();
            int pageSize = 18;
            txtPageSize.Text= Convert.ToString(pageSize);
            timer1_Tick(sender,e);
            btnDocDonHangHienTai_Click(sender,e);
            btnDocDonHangTre_Click(sender,e);
            txtMember.Text = member;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            txtTongSoTre.ForeColor = Color.Blue;
            txtTienDoChung.ForeColor = Color.DarkBlue;
            txtDonHangTre.ForeColor = Color.Blue;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            txtTongSoTre.ForeColor = Color.Red;
            txtTienDoChung.ForeColor = Color.Red;
            txtDonHangTre.ForeColor = Color.Red;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            txtTongSoTre.ForeColor = Color.Red;
            txtTienDoChung.ForeColor = Color.Red;
            txtDonHangTre.ForeColor = Color.Red;
        }

        private void btnDocDonHangHienTai_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"SELECT IDSP,mabv,madh,SPLR,sanpham,soluongyc,BTPT11,
                    KetThucTo11,AllProgress,STATUS,ngoaiquang,khachhang
                    FROM PHANTICH_CHUNG WHERE STATUS <>'HOAN THANH'");
            kn.dongketnoi();
        }
        private void DocDonHangTre()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"SELECT IdPSX,IDSP,DanhGia,mabv,madh,SPLR,sanpham,soluongyc,BTPT11,
                KetThucTo11,AllProgress,STATUS,ngoaiquang
                FROM PHANTICH_CHUNG WHERE IDSP like '"+txtIDSP.Text+"'");
            kn.dongketnoi();
            this.gridView1.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }
        private void btnDocDonHangTre_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"SELECT IdPSX,IDSP,DanhGia,mabv,madh,SPLR,sanpham,soluongyc,BTPT11,
                KetThucTo11,AllProgress,STATUS,ngoaiquang
                FROM PHANTICH_CHUNG WHERE STATUS =N'Trễ' or STATUS =N'Đỏ'");
            kn.dongketnoi();
            this.gridView1.OptionsSelection.MultiSelectMode
           = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtIDSP.Text = gridView1.GetFocusedRowCellDisplayText(sanPhamID_grid);
            txtTienDoChung.Text = gridView1.GetFocusedRowCellDisplayText(tienDoChung_grid1);
            txtIDPSX.Text = gridView1.GetFocusedRowCellDisplayText(idPSX_grid);
        }

        private void btnExportTre_Click(object sender, EventArgs e)
        {
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridView1.OptionsSelection.MultiSelectMode
                 = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridControl1.ShowPrintPreview();
        }

        private void btnExportConLai_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"SELECT IDSP,DanhGia,mabv,madh,SPLR,sanpham,soluongyc,BTPT11,
                    KetThucTo11,AllProgress,STATUS,ngoaiquang
                    FROM PHANTICH_CHUNG WHERE STATUS =N'Trễ' or STATUS =N'Đỏ'");
            RpDonHangTre rpDonHangTre = new RpDonHangTre();
            rpDonHangTre.DataSource = dt;
            rpDonHangTre.DataMember = "Table";
            rpDonHangTre.ShowPreviewDialog();
        }
        private void BtnMoi_Click(object sender, EventArgs e)
        {
            this.gridView1.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

        }
        private void btnGhiBaoCao_Click(object sender, EventArgs e)
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
                    string strQuery = string.Format(@"update tblchitietkehoach 
                            set DanhGia=N'{0}',NguoiDanhGia=N'{1}',
                            NgayDanhGia=GetDate() where IDSP ='{2}'",
                          rowData["DanhGia"],
                          txtMember.Text,
                          rowData["IDSP"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                btnDocDonHangTre_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
            }
        }

        private void btnPrintBaoCaoTre_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang(@"select * from viewDonHangTre where IDSP like '" + txtIDSP.Text+"'");
            RpDonHangTre rpDonHangTre = new RpDonHangTre();
            rpDonHangTre.DataSource = dt;
            rpDonHangTre.DataMember = "Table";
            rpDonHangTre.ShowPreviewDialog();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtIDSP.Text = gridView2.GetFocusedRowCellDisplayText(keHoachID_grid2);
            txtMasp.Text= gridView2.GetFocusedRowCellDisplayText(maSanPham_grid2);
            DocDonHangTre();
        }

        private void btnLayoutSanPham_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
    }
}
