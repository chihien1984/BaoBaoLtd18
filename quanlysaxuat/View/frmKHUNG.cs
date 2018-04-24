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
    public partial class frmKHUNG : Form
    {
        public frmKHUNG()
        {
            InitializeComponent();
        }
        private void LOAD_ALL()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select PT.*,VT.Thucnhap,VT.Ngaynhap,Ten_vattu from 
            (select madh + mabv Ma_PT, *from PHANTICH_TIENDO07) PT
            left outer join
            (select Madh + Masp Ma_VT, Thucnhap, Ngaynhap, Ten_vattu from PHANTICH_TIENDOVATTU) VT
            on PT.Ma_PT = VT.Ma_VT
            where Ma_PT is not null and Ma_VT is not null");
            gridView1.ExpandAllGroups();
        }
        private void LOAD_TRE()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select PT.*,VT.Thucnhap,VT.Ngaynhap,Ten_vattu from 
            (select madh+mabv Ma_PT,* from PHANTICH_TIENDO07) PT
            left outer join
            (select Madh+Masp Ma_VT,Thucnhap,Ngaynhap,Ten_vattu from PHANTICH_TIENDOVATTU) VT
            on PT.Ma_PT=VT.Ma_VT
            where Ma_PT is not null and Ma_VT is not null and PT.STATUS <> 'HOAN THANH'");
            gridView1.ExpandAllGroups();
        }
        private void LOAD_TIME()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(@"select PT.*,VT.Thucnhap,VT.Ngaynhap,Ten_vattu from 
            (select madh+mabv Ma_PT,* from PHANTICH_TIENDO07) PT
            left outer join
            (select Madh+Masp Ma_VT,Thucnhap,Ngaynhap,Ten_vattu from PHANTICH_TIENDOVATTU) VT
            on PT.Ma_PT=VT.Ma_VT
            where Ma_PT is not null and Ma_VT is not null and convert(Date,PT.ngaytrienkhai,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
            gridView1.ExpandAllGroups();
        }

        private void frmKHUNG_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            ketnoi kn = new ketnoi();
            LOAD_TIME();
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LOAD_ALL();
        }

        private void btnLoadTime_Click(object sender, EventArgs e)
        {
            LOAD_TRE();
        }
    }
}
