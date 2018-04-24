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

namespace quanlysanxuat
{

    public partial class frmPrVatTu : DevExpress.XtraEditors.XtraForm
    {
        public frmPrVatTu()
        {
            InitializeComponent();
        }
        public static string madh;
        private void ListVTNhapKho()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from PHANTICH_TIENDOVATTU where madh like N'"+txtMadh.Text+"'"); 
            kn.dongketnoi();
        }
        private void LisNhapKhoVT()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(""); 
            kn.dongketnoi();
        }
        private void List(object sender,EventArgs e)
        {
            DM_MuaVatTu();
        }
        private void DM_MuaVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang(@"select * from PHANTICH_TIENDOVATTU 
            WHERE cast(Ngaylap_DM as date) 
            between '"+dptu_ngay.Value.ToString("yyyy/MM/dd")+"' and '"+dpden_ngay.Value.ToString("yyyy/MM/dd")+"' order by  Ngaylap_DM DESC");
            kn.dongketnoi();
            //gridView1.ExpandAllGroups();
        }

        private void listChTietNhap(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblNHAP_VATTU where idvattu like '"+txtDinhMucID.Text+"'");
            gridView2.ExpandAllGroups();
            kn.dongketnoi();
        }
        private void Binding(object sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(madh_grid1);
            txtDinhMucID.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            txtMaSP.Text = gridView1.GetFocusedRowCellDisplayText(MaSP_grid1);
        }
        public static string donHangID { get; set; }
       

        private void frmPrVatTu_Load(object sender, EventArgs e)
        {
            if (Login.Username == "Lê Thị Diểm Trinh")
            {
                btnCapNhat.Visible = true;
            }
            txtMember.Text = Login.Username;
            txtDonHangID.Text = donHangID;
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            txtMadh.Text = madh;
            ListVTNhapKho();
            btnMuaNgoai_Click(sender, e);
            btnDungTonKho_Click(sender, e);
            DM_MuaVatTu();

        }

        private void btnExTienDoVatTu_Click(object sender, EventArgs e)
        {
            gridView1.ShowPrintPreview();
        }

        private void btnChiTieNhap_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select VT.*,Ten_vattu from tblNHAP_VATTU VT "
            + " left outer join tblvattu_dauvao DV on VT.idvattu=DV.Codevatllieu where convert(Date,Ngay_lap,103) "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by Ngay_lap DESC");
            gridView2.ExpandAllGroups();
            kn.dongketnoi();
        }

        private void btnTonkho_Click(object sender, EventArgs e)
        {
            frmRPNhapXuatVatTu fBaoCaoKho = new frmRPNhapXuatVatTu();
            fBaoCaoKho.Show();
        }

        private void btnXemBV_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSP.Text, txtPath_MaSP.Text);
            f2.Show();
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
                int[] listRowList = this.gridView1.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView1.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblvattu_dauvao 
                            set SL_vattuve='{0}',
                            Ngayve_TT='{1}',
                            nguoinhap=N'{2}',
                            ngaynhap=GetDate()
                            where CodeVatllieu='{3}'",
                          rowData["Thucnhap"],
                          rowData["Ngaynhap"] == DBNull.Value ? "":Convert.ToDateTime(rowData["Ngaynhap"]).ToString("yyyy-MM-dd"),
                          txtMember.Text,
                          rowData["CodeVatllieu"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
            con.Close();
            btnMuaNgoai_Click( sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo");
            }
        }

        private void btnMuaNgoai_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select * from PHANTICH_TIENDOVATTU 
            WHERE SL_vattumua >0 and cast(Ngaylap_DM as date) 
            between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by  Ngaylap_DM DESC");
            kn.dongketnoi();
        }

        private void btnDungTonKho_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl4.DataSource = kn.laybang(@"select * from PHANTICH_TIENDOVATTU 
            WHERE SL_vattumua <=0 and cast(Ngaylap_DM as date) 
            between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by  Ngaylap_DM DESC");
            kn.dongketnoi();
        }
    }
}