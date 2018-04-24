using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmThem_Phong_Ban : Form
    {
        public frmThem_Phong_Ban()
        {
            InitializeComponent();
        }

        private void frmPhong_Ban_Load(object sender, EventArgs e)
        {
            DanhSachPhongBan();
        }
        void DanhSachPhongBan()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblPHONGBAN_TK");
            kn.dongketnoi();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
