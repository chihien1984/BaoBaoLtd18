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
    public partial class frmThemCongDoan : DevExpress.XtraEditors.XtraForm
    {
        public frmThemCongDoan()
        {
            InitializeComponent();
        }
      
        private void LoadDMSP()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tblSANPHAM");
            kn.dongketnoi();
        }
        private void LoadDinhMucLaoDong(){
            ketnoi kn = new ketnoi();
            gridControl2.DataSource=kn.laybang("select * from tblDMuc_LaoDong");
            kn.dongketnoi();
        }
        private void LoadDMSP(object sender, EventArgs e) { LoadDMSP(); }
        private void LoadDinhMucLaoDong(object sender, EventArgs e) { LoadDinhMucLaoDong(); }
        private void Them(object sender, EventArgs e)
        {
            try
            {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblDMuc_LaoDong "
                    +" (Masp,Tensp,Macv,Tencv,Tck,Ttn,Tpv,Tnn, "
                    +" Soluong,Tcd,Zson,Zxi,Nguoilap,Ngaylap) "
                    +" values(@Masp,@Tensp,@Macv,@Tencv,@Tck,@Ttn,@Tpv,@Tnn, "
                    +" @Soluong,@Tcd,@Zson,@Zxi,@Nguoilap,GetDate())", con);
                    cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tensp", SqlDbType.NVarChar)).Value = txtSanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@Macv", SqlDbType.NVarChar)).Value = txtMaGiaidoan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tencv", SqlDbType.NVarChar)).Value = txtTenGD.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tck", SqlDbType.Float)).Value = txtTck.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ttn", SqlDbType.Float)).Value = txtTtn.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tpv", SqlDbType.Float)).Value = txtTpv.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tnn", SqlDbType.Float)).Value = txtTnn.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSoluongcd.Text;
                    cmd.Parameters.Add(new SqlParameter("@Tcd", SqlDbType.Float)).Value = txtTcd.Text;
                    cmd.Parameters.Add(new SqlParameter("@Zson", SqlDbType.Float)).Value = txtZGC_son.Text;
                    cmd.Parameters.Add(new SqlParameter("@Zxi", SqlDbType.Float)).Value = txtZGC_Xi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl2.DataSource = dt;
                    con.Close();
                    LoadDinhMucLaoDong(); txtSoluongcd.Clear(); txtTcd.Clear();txtMaGiaidoan.Clear() ; txtTenGD.Focus();
            }
            catch
            { MessageBox.Show("Không thành công", "Thông báo"); }
        }

        private void Sua(object sender,EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("update tblDMuc_LaoDong "
                + " set Masp=@Masp,Tensp=@Tensp,Macv=@Macv,Tencv=@Tencv,Tck=@Tck,Ttn=@Ttn,Tpv=@Tpv,Tnn=@Tnn, "
                + " Soluong=@Soluong,Tcd=@Tcd,Zson=@Zson,Zxi=@Zxi,Nguoilap=@Nguoilap,Ngaylap=GetDate() where id like "+txtid.Text+"", con);
                cmd.Parameters.Add(new SqlParameter("@Masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                cmd.Parameters.Add(new SqlParameter("@Tensp", SqlDbType.NVarChar)).Value = txtSanpham.Text;
                cmd.Parameters.Add(new SqlParameter("@Macv", SqlDbType.NVarChar)).Value = txtMaGiaidoan.Text;
                cmd.Parameters.Add(new SqlParameter("@Tencv", SqlDbType.NVarChar)).Value = txtTenGD.Text;
                cmd.Parameters.Add(new SqlParameter("@Tck", SqlDbType.Float)).Value = txtTck.Text;
                cmd.Parameters.Add(new SqlParameter("@Ttn", SqlDbType.Float)).Value = txtTtn.Text;
                cmd.Parameters.Add(new SqlParameter("@Tpv", SqlDbType.Float)).Value = txtTpv.Text;
                cmd.Parameters.Add(new SqlParameter("@Tnn", SqlDbType.Float)).Value = txtTnn.Text;
                cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSoluongcd.Text;
                cmd.Parameters.Add(new SqlParameter("@Tcd", SqlDbType.Float)).Value = txtTcd.Text;
                cmd.Parameters.Add(new SqlParameter("@Zson", SqlDbType.Float)).Value = txtZGC_son.Text;
                cmd.Parameters.Add(new SqlParameter("@Zxi", SqlDbType.Float)).Value = txtZGC_Xi.Text;
                cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl2.DataSource = dt;
                con.Close(); LoadDinhMucLaoDong();
            }
            catch
            { MessageBox.Show("Không thành công", "Thông báo"); }
        }

        private void Xoa(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("Delete tblDMuc_LaoDong where id like " + txtid.Text + " "); kn.dongketnoi();
            LoadDinhMucLaoDong();
        }

        private void Exprot()
        {

        }
        private void BindingDMSP(object sender,EventArgs e)
        {
            string Gol = "";
            Gol=gridView1.GetFocusedDisplayText();
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid);
            txtSanpham.Text = gridView1.GetFocusedRowCellDisplayText(Tensp_grid); 
            SearchMasp();
        }
        private void SearchMasp()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblDMuc_LaoDong where Masp like N'" + txtMasp.Text + "' ");
            kn.dongketnoi();
        }
        private void BindingDMuc_laodong(object sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            txtSanpham.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            txtMaGiaidoan.Text= gridView2.GetFocusedRowCellDisplayText(Macv_grid2);
            txtTenGD.Text= gridView2.GetFocusedRowCellDisplayText(Tencv_grid2);
            txtSoluongcd.Text= gridView2.GetFocusedRowCellDisplayText(Soluong_grid2);
            txtTcd.Text= gridView2.GetFocusedRowCellDisplayText(Tcd_grid2);
        }
        private void frmThemCongDoan_Load(object sender, EventArgs e)
        {
        }
    }
}