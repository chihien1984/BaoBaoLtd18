using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class quytrinhtrahang : Form
    {
        public quytrinhtrahang()
        {
            InitializeComponent();
        }

        public quytrinhtrahang(string Role)
        {
            InitializeComponent();
            if(Role=="1")
            {
                khohang.Enabled = true;
                groupkinhdoanh.Enabled = false;
                grkythuat.Enabled = false;
                quanlysua.Enabled = false;
                grsanxuat.Enabled = false;
                giamsat.Enabled = false;
            }
            else if (Role == "2")
            {
                khohang.Enabled = false;
                groupkinhdoanh.Enabled = true;
                grkythuat.Enabled = false;
                quanlysua.Enabled = false;
                grsanxuat.Enabled = false;
                giamsat.Enabled = false;
            }
            else if (Role == "3")
            {
                khohang.Enabled = false;
                groupkinhdoanh.Enabled = false;
                grkythuat.Enabled = true;
                quanlysua.Enabled = false;
                grsanxuat.Enabled = false;
                giamsat.Enabled = false;
            }
            else if (Role == "4")
            {
                khohang.Enabled = false;
                groupkinhdoanh.Enabled = false;
                grkythuat.Enabled = false;
                quanlysua.Enabled = false;
                grsanxuat.Enabled = false;
                giamsat.Enabled = true;
            }
            else if (Role == "5")
            {
                khohang.Enabled = false;
                groupkinhdoanh.Enabled = false;
                grkythuat.Enabled = false;
                quanlysua.Enabled = false;
                grsanxuat.Enabled = true;
                giamsat.Enabled = false;
            }
            else if (Role == "6")
            {
                khohang.Enabled = false;
                groupkinhdoanh.Enabled = false;
                grkythuat.Enabled = false;
                quanlysua.Enabled = true;
                grsanxuat.Enabled = false;
                giamsat.Enabled = false;
            }
        }

        private void xulyhangtrave_Load(object sender, EventArgs e)
        {

        }

        private void kho_Click(object sender, EventArgs e)
        {
            frmhangtravekho ftrave = new frmhangtravekho();
            ftrave.Show();
        }

        private void t01_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void kythuat_Click(object sender, EventArgs e)
        {
            frmhangtravekythuat fkythuat = new frmhangtravekythuat();
            fkythuat.Show();
        }

        private void giamsat_Click(object sender, EventArgs e)
        {
            kehoachsuahang fsuahang = new kehoachsuahang();
            fsuahang.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmdulieusuahangtrave fdulieusuahang = new frmdulieusuahangtrave();
            fdulieusuahang.Show();
        }

        private void kinhdoanh_Click(object sender, EventArgs e)
        {
            frmgiamsathangtrave fgs = new frmgiamsathangtrave();
            fgs.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmgiamsathangtrave fgs = new frmgiamsathangtrave();
            fgs.Show();
        }
    }
}
