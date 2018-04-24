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
    public partial class frmXacNhanVatTu : DevExpress.XtraEditors.XtraForm
    {
        public frmXacNhanVatTu()
        {
            InitializeComponent();
        }
        public static string madh="";
        private void ListKeHoachVatTuDonHang()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select CodeVatllieu,madh,Ten_vattu,SL_vattucan,SL_vattumua,Donvi_vattu, "
           + " Tenquicachsp,Ngayxacnhan,NgayDK_ve,Ngaylap_DM,Nguoilap_DM from tblvattu_dauvao where madh like N'" + txtMadh.Text + "' order by Ngaylap_DM desc");
            kn.dongketnoi();
        }
        private void ListKeHoachVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select CodeVatllieu,madh,Ten_vattu,SL_vattucan,SL_vattumua,Donvi_vattu, "
           + " Tenquicachsp,Ngayxacnhan,NgayDK_ve,Ngaylap_DM,Nguoilap_DM from tblvattu_dauvao where convert(Date,Ngaylap_DM,101) "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by Ngaylap_DM desc");
            kn.dongketnoi();
        }
        private void ListKHVTChuaXacNhan()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select CodeVatllieu,madh,Ten_vattu,SL_vattucan,SL_vattumua,Donvi_vattu, "
           + " Tenquicachsp,Ngayxacnhan,NgayDK_ve,Ngaylap_DM,Nguoilap_DM from tblvattu_dauvao where convert(Date,Ngaylap_DM,101) "
           + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' "
           + " and Ngayxacnhan is null and NgayDK_ve is not null  order by Ngaylap_DM desc");
            kn.dongketnoi();
        }
        private void ListKehoachVatTu()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select CodeVatllieu,madh,Ten_vattu,SL_vattucan,SL_vattumua,Donvi_vattu, "
           + " Tenquicachsp,Ngayxacnhan,NgayDK_ve,Ngaylap_DM,Nguoilap_DM from tblvattu_dauvao where convert(Date,Ngaylap_DM,101) "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by Ngaylap_DM desc");
            kn.dongketnoi();
        }

        private void ListChuaXacNhan(object sender,EventArgs  e) {ListKHVTChuaXacNhan();}
        private void ListKHVT(object sender, EventArgs e) { ListKehoachVatTu(); }
        private void BindingChuaXacNhan(object sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtidvattu.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMadh.Text = gridView2.GetFocusedRowCellDisplayText(madh_grid2);
            dpNgaydukien.Text = gridView2.GetFocusedRowCellDisplayText(NgayDk_grid2);     
        }

        private void BindingXacNhan(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtidvattu.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(Madh_grid1);
        }

        private void ListDaXacNhan(object sender,EventArgs e)
        {
            ListKeHoachVatTu();
        }

        private void XacNhanNgayVatTu(object sender,EventArgs e)
        {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("update tblvattu_dauvao set Ngayxacnhan=@NgayDK_ve where CodeVatllieu like '"+txtidvattu.Text+"'", con);
                if(dpNgaydukien.Text =="")
                cmd.Parameters.Add(new SqlParameter("@NgayDK_ve", SqlDbType.Date)).Value = DBNull.Value;
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@NgayDK_ve", SqlDbType.Date)).Value = dpNgaydukien.Text;
                } 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                ListKeHoachVatTuDonHang();
        }

        private void frmXacNhanVatTu_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            txtMadh.Text = madh;
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            ListKeHoachVatTuDonHang();
        }
    }
}