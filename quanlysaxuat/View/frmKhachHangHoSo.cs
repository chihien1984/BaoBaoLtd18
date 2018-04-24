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
    public partial class frmKhachHangHoSo : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangHoSo()
        {
            InitializeComponent();
        }

        private void frmKhachHangHoSo_Load(object sender, EventArgs e)
        {

        }
        private async void ThDMNguoiLienHe()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"");
                Invoke((Action)(() => {
                    grKhachHangLienHe.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void DungChung(string a)
        {
            Model.Function.ConnectSanXuat();
            grKhachHangLienHe.DataSource = Model.Function.GetDataTable(a);
            ThDMNguoiLienHe();
        }
        private void Them()
        {
            string sqlQuery = string.Format(@"");
            DungChung(sqlQuery);
        }
        private void CapNhat()
        {
            string sqlQuery = string.Format(@"");
            DungChung(sqlQuery);
        }
        private void Xoa()
        {
            string sqlQuery = string.Format(@"");
            DungChung(sqlQuery);
        }
    }
}
