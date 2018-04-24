using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace quanlysanxuat
{
    public partial class UcDANHMUC_DONHANG_DANGSANXUAT : UserControl
    {
        public UcDANHMUC_DONHANG_DANGSANXUAT()
        {
            InitializeComponent();
        }

        private void UcDANHMUC_DONHANG_DANGSANXUAT_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy"); dpden_ngay.Text = DateTime.Now.ToString();
            LOAD_ALL();
        }
        private void LOAD_ALL()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM TINHHINH_DH_DANGSANXUAT ");
        }
        private void LOAD_TRE()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("SELECT * FROM TINHHINH_DH_DANGSANXUAT WHERE STATUS <> 'HOAN THANH'");
        }
        private void LOAD_TIME()
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang(" SELECT * FROM TINHHINH_DH_DANGSANXUAT WHERE convert(Date,ngaytrienkhai,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
        }
        private bool _working = false;
        private void view_RowStyle(object sender, RowStyleEventArgs e)
        {
           /* if (_working) return;

            var view = sender as GridView;
            if (view != null)
            {
                int lastRowIndex = (view.GridControl.DataSource as BindingSource).Count;
                if (view.IsRowVisible(lastRowIndex) == RowVisibleState.Visible)
                {
                    _working = true;
                    //go get more rows.
                    _working = false;
                }
            }*/
        }
        private void btnLoadALL(object sender, EventArgs e)
        {
            LOAD_ALL();
        }
        private void btnLoadTre(object sender, EventArgs e)
        {
            LOAD_TRE();
        }
        private void btnLoadTime(object sender, EventArgs e)
        {
            LOAD_TIME();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtMasp.Text = gridView1.GetFocusedRowCellDisplayText(Masp_grid1);
            cbMaPSX.Text = gridView1.GetFocusedRowCellDisplayText(madh);
            //.Text = gridView1.GetFocusedRowCellDisplayText(madh);
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("Mã sản không khớp đúng");
            }
        }
        private void Layout_PSX()//Hàm gọi phiếu sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_PSX.Text, cbMaPSX.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã phiếu sản xuất này chưa đúng"); }

        }
        private void Layout_KHSX()//Hàm  gọi kế hoạch sản xuất
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_KHSX.Text, cbMaPSX.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("Hiện mã kế hoạch này chưa đúng"); }

        }
        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void LoadLayout_PSX(object sender, EventArgs e)//Sự kiện gọi phiếu sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_PSX.Text);
            f2.Show();
        }
        private void LoadLayout_KHSX(object sender, EventArgs e)//Sự kiện gọi kế hoạch sản xuất 
        {
            frmLoading f2 = new frmLoading(cbMaPSX.Text, txtPath_KHSX.Text);
            f2.Show();
        }
        private void Export(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void dpden_ngay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
