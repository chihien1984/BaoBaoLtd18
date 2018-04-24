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
    public partial class UcKhuonrap_tk : UserControl
    {
        FtpClient ftpClient;
        public UcKhuonrap_tk()
        {
            InitializeComponent();
            ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }
        Clsketnoi kn = new Clsketnoi();
        string Gol = "";
       
        private void LoadDM_KHUON_RAP()
        {
            ketnoi Connet = new ketnoi();
            gridControl1.DataSource = Connet.laybang("SELECT Ngaykh_cat,Ma_khuon,DM.Mact,DM.Masp,SP.Tensp,Manhom_khuon,Ten_khuon,Dacdiem_khuon,Soluong_khuon, "
                   + " Ngaylap, Ngaybatdau, Ngayhoanthanh, Nguoilap, DM.Manv, Ghichu,GETDATE() as Today, DM.Ngaycat_Hoanthanh, DM.Ngayrap_Hoanthanh "
                   + " , DM.Mabp, DM.BPsudung, DM.Nguoiluukhuon, DM.Vitrikhuon, DM.Tinhtrang_khuon, DM.CodeMK from tblDM_KHUON DM left "
                   + " join tblSANPHAM_CT CT on DM.Mact = CT.Mact join (select Tensp, Masp from tblSANPHAM) SP on CT.Masp = SP.Masp"
                   + " and convert(nvarchar,Ngaybatdau,103) between '" + dptu_ngay.Value.ToString("dd/MM/yyyy") + "' and '" + dpden_ngay.Value.ToString("dd/MM/yyyy") + "'");
        }
        private void btnCapNhat_ngayHT_Click(object sender, EventArgs e)
        {
            if (txtCodeMK.Text != "")/// insert cách truyền tham số parameters
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand command = new SqlCommand("update tblDM_KHUON set Ngayrap_Hoanthanh =@Ngayrap_Hoanthanh where CodeMK like '" + txtCodeMK.Text + "'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                try
                {
                    if (dpNgayRapKhuon_HT.Text == "")
                        command.Parameters.Add(new SqlParameter("@Ngayrap_Hoanthanh", SqlDbType.Date)).Value = DBNull.Value;
                    else
                        command.Parameters.Add(new SqlParameter("@Ngayrap_Hoanthanh", SqlDbType.Date)).Value = dpNgayRapKhuon_HT.Text;
                }
                catch (Exception)
                {
                    throw;
                }
                adapter.Fill(dt); cn.Close(); LoadDM_KHUON_RAP();
            }
        }

        private void UcKhuonrap_tk_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString(); txtuser.Text = Login.Username;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
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
            txtCodeMK.Text = gridView1.GetFocusedRowCellDisplayText(CodeMK_grid1);
        }

        private void btnDMkhuon_Click(object sender, EventArgs e)
        {
            LoadDM_KHUON_RAP();
        }

        private void BtnUpNK_Khuon_Click(object sender, EventArgs e)
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
                    ftpClient.upload("/kythuat/KHUON_MAU/" + fileName, fullFileName);
                    if (ftpClient.message == "success")
                    {
                        MessageBox.Show(ftpClient.pathFileName+"success");
                    }
                    else
                    {
                        MessageBox.Show(ftpClient.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "unsuccessful");
                }
            }
        }

        private void btnDowLoad_Click(object sender, EventArgs e)
        {
            ftpClient.download("/kythuat/KHUON_MAU/NHATKY_RAPKHUON.xlsx", "D:\\NHATKY_RAPKHUON.xlsx");
            if (ftpClient.message == "success")
            {
                MessageBox.Show(ftpClient.pathFileName + "success");
            }
            else
            {
                MessageBox.Show(ftpClient.message + "unsuccessful");
            }
        }
    }
}
