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
    public partial class frmz_Taphopchiphi : DevExpress.XtraEditors.XtraForm
    {
        public frmz_Taphopchiphi()
        {
            InitializeComponent();
        }
        private void list()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_dmchiphi");
            kn.dongketnoi();
        }
        private void list_dmchiphi(object sender,EventArgs e) {
            list();
        }
        private void columnlookupEdit()
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd = new SqlCommand("select tenloai from tblz_dmchiphi", con);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        reader.Read();
            //        repositoryItemLookUpEdit1.DataSource = Convert.ToString(reader[0]);
            //        //txtNhap.Text = Convert.ToString(reader[1]);
            //        //txtXuat.Text = Convert.ToString(reader[2]);
            //        //txtTonCuoi.Text = Convert.ToString(reader[3]);
            //    }
            //    con.Close();
            //}
            //catch (Exception)
            //{
            //}
            ketnoi kn = new ketnoi();
            repositoryItemLookUpEdit1.DataSource = kn.laybang("select tenloai from tblz_dmchiphi");
            repositoryItemLookUpEdit1.ValueMember = "tenloai";
            repositoryItemLookUpEdit1.DisplayMember = "tenloai";
        }
        private void frmz_Taphopchiphi_Load(object sender, EventArgs e)
        {

        }
    }
}