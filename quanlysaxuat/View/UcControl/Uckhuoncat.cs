using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace quanlysanxuat
{
    public partial class Uckhuoncat : UserControl
    {
        public Uckhuoncat()
        {
            InitializeComponent();
        }
        Clsketnoi kn = new Clsketnoi();
        string Gol = "";
     
        private void LoadDM_KHUON()
        {
            ketnoi Connet = new ketnoi();
            gridControl1.DataSource = Connet.laybang("SELECT Ngaykh_cat,Ma_khuon,DM.Mact,DM.Masp,SP.Tensp,Manhom_khuon,Ten_khuon,Dacdiem_khuon,Soluong_khuon, "
                   + " Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, DM.Manv, Ghichu,GETDATE() as Today, DM.Ngaycat_Hoanthanh, DM.Ngayrap_Hoanthanh "
                   + " , DM.Mabp, DM.BPsudung, DM.Nguoiluukhuon, DM.Vitrikhuon, DM.Tinhtrang_khuon, DM.CodeMK from tblDM_KHUON DM left "
                   +" join tblSANPHAM_CT CT on DM.Mact = CT.Mact join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp"
                   + " and convert(nvarchar,Ngaybatdau,103) between '" + dptu_ngay.Value.ToString("dd/MM/yyyy") + "' and '" + dpden_ngay.Value.ToString("dd/MM/yyyy") + "'");
        }
       
        private void Uckhuoncat_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString(); txtuser.Text = Login.Username;
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void btnCapNhat_ngayHT_Click(object sender, EventArgs e)
        {
            if (txtCodeMK.Text != "")/// insert cách truyền tham số parameters
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("update tblDM_KHUON set Ngaycat_Hoanthanh =@Ngaycat_Hoanthanh where CodeMK like '" + txtCodeMK.Text + "'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpNgayCatDayHT.Text == "")
                        command.Parameters.Add(new SqlParameter("@Ngaycat_Hoanthanh", SqlDbType.Date)).Value = DBNull.Value;
                    else
                    command.Parameters.Add(new SqlParameter("@Ngaycat_Hoanthanh", SqlDbType.Date)).Value = dpNgayCatDayHT.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                adapter.Fill(dt);cn.Close(); LoadDM_KHUON();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gridView1.GetFocusedDisplayText();
            txtmasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            txtMasp_CT.Text = gridView1.GetFocusedRowCellDisplayText(Maspct_grid1);
            txtMakhuon.Text = gridView1.GetFocusedRowCellDisplayText(Makhuon_grid1);
            txtDacdiem_khuon.Text = gridView1.GetFocusedRowCellDisplayText(Dacdiemkhuon_grid1);
            txtsoluongkhuon.Text = gridView1.GetFocusedRowCellDisplayText(soluongkhuon_grid1);
            txtNV_lapkhuon.Text = gridView1.GetFocusedRowCellDisplayText(nguoilap_grid1);
            txtmanv_lapkhuon.Text = gridView1.GetFocusedRowCellDisplayText(manv_grid1);
            dpNgaybatdau.Text = gridView1.GetFocusedRowCellDisplayText(ngaybatdau_grid1);
            dpngayketthucKH.Text = gridView1.GetFocusedRowCellDisplayText(NgayKH_grid1);
            dpNgayCatDayHT.Text = gridView1.GetFocusedRowCellDisplayText(ngayCatDayHT_grid1);
            txtGhichu.Text = gridView1.GetFocusedRowCellDisplayText(ghichu_grid1);
            dpNgayLap.Text = gridView1.GetFocusedRowCellDisplayText(ngayghi_grid1);
            txttensp.Text = gridView1.GetFocusedRowCellDisplayText(sanpham_grid1);
            txttenkhuon.Text = gridView1.GetFocusedRowCellDisplayText(Tenkhuon_grid1);
            txtCodeMK.Text= gridView1.GetFocusedRowCellDisplayText(CodeMK_grid1);   
        }

        private void btnDMkhuon_Click(object sender, EventArgs e)
        {
            LoadDM_KHUON();
        }
    }
}
