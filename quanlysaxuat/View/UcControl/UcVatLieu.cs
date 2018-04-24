using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat
{
    public partial class UcVatLieu : DevExpress.XtraEditors.XtraForm
    {
        public UcVatLieu()
        {
            InitializeComponent();
        }
        private void LOAD_DMVATLIEU()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select * from tblVatLieuSanPham");
        }
        private void THEM_VATLIEU(object sender,EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("insert into tblVatLieuSanPham (Mavatlieu,TenVatlieu) values(N'"+txtMaVatLieu.Text+"',N'"+txtTenVatLieu.Text+"')");
        }
        private void SUA_VATLIEU(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("update tblVatLieuSanPham set Mavatlieu='"+txtMaVatLieu.Text+"',TenVatlieu='"+txtTenVatLieu.Text+"' where Code '"+txtCode.Text+"' ");
        }
        private void XOA_VATLIEU(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("delete tblVatLieuSanPham where Code like  '"+txtCode.Text+"'");
        }
        private void UcVatLieu_Load(object sender, EventArgs e)
        {
            LOAD_DMVATLIEU();
        }
    }
}
