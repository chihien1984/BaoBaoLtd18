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
    public partial class frmZ_SaveChiTietGia : DevExpress.XtraEditors.XtraForm
    {
        public frmZ_SaveChiTietGia()
        {
            InitializeComponent();
        }
        private void LoadMaGiaThanh()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tblz_dmchiphi where id_z like '" + glMasp.Text + "'");
            kn.dongketnoi();
        }
        private void btnUpdate_Click(object sender, EventArgs e)//Cap nhat tinh gia moi
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
                    string strQuery = string.Format(@"insert into tblz_ChiTietKyChiPhi(id_z,Kytinhgia,masp,tenloai,Macp,Noidungcp, "
                    +"thanhtiensanpham,nguoilap,ngaylap,id_kycp,Madongiavl) "
                    + "VALUES ('{0}','{1}','{2}',N'{3}','{4}','{5}','{6}',N'{7}','{8}','{9}','{10}')",
                    rowData["id_z"], rowData["Kytinhgia"], rowData["masp"], rowData["tenloai"],
                    rowData["Macp"], rowData["Noidungcp"], rowData["thanhtiensanpham"], rowData["nguoilap"],
                    Convert.ToDateTime(rowData["ngaylap"]).ToString("MM/dd/yyyy"), rowData["id_kycp"], rowData["Madongiavl"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error:"+ex); }
        }
        private void GlEditDanhMucCP()//Danh muc ky chi phi
        {
            ketnoi Connect = new ketnoi();
            glMasp.Properties.DataSource = Connect.laybang("select id_z,masp,tenloai,Macp from tblz_dmchiphi");
            glMasp.Properties.DisplayMember = "id_z";
            glMasp.Properties.ValueMember = "id_z";
            glMasp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glMasp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            glMasp.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void frmZ_SaveChiTietGia_Load(object sender, EventArgs e)// FormLoad
        {
            LoadMaGiaThanh();
            GlEditDanhMucCP();
        }

        private void glMasp_EditValueChanged(object sender, EventArgs e)
        {
            LoadMaGiaThanh();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
    }
}