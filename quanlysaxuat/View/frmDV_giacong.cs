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

namespace quanlysanxuat
{
    public partial class frmDV_giacong : DevExpress.XtraEditors.XtraForm
    {
        public frmDV_giacong()
        {
            InitializeComponent();
        }
        Clsketnoi connect = new Clsketnoi();
        public static string ComboboxdanhsachGC;
        void DocDanhSachDonViGiaCong()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("Select MaDVGC,TenDVGC,DiaChi,Sodienthoai,fax,Id from tblDS_GIACONG");
        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("insert into tblDS_GIACONG (MaDVGC,TenDVGC,DiaChi,Sodienthoai,fax,Nguoilienhe)  "
              + "  values ('" + txtMDVGC.Text + "',N'" + txttenDVGC.Text + "', "
              + " N'" + txtDiaChiGiaCong.Text + "','" + txtDienThoai.Text + "','" + TxtFax.Text + "','" + txtNguoiLienHe.Text + "')");
            if (kq > 0)
            {
                MessageBox.Show("Bạn đã thêm '" + txttenDVGC.Text + "' Vào Danh Mục Gia Công", "Thông báo");
                ketnoi knn = new ketnoi();
                DocDanhSachDonViGiaCong();
                kn.dongketnoi();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại", "Thông báo");
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDS_GIACONG set "
              + " MaDVGC= '" + txtMDVGC.Text + "',TenDVGC=N'" + txttenDVGC.Text + "', "
              + " DiaChi=N'" + txtDiaChiGiaCong.Text + "',Sodienthoai='" + txtDienThoai.Text + "', "
              + " fax='" + TxtFax.Text + "',Nguoilienhe='" + txtNguoiLienHe.Text + "' where Id='" + txtIde.Text + "' ");
            if (kq > 0)
            {
                ketnoi knn = new ketnoi();
                DocDanhSachDonViGiaCong();
                kn.dongketnoi();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại", "Thông báo");
            }
        }
        string Gol = "";
        private void gridControl2_Click(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txttenDVGC.Text = gridView2.GetFocusedRowCellDisplayText(tendvgiacong_col);
            txtMDVGC.Text = gridView2.GetFocusedRowCellDisplayText(madonvi_col);
            txtDiaChiGiaCong.Text = gridView2.GetFocusedRowCellDisplayText(diachi_col);
            txtDienThoai.Text = gridView2.GetFocusedRowCellDisplayText(dienthoai_col);
            TxtFax.Text = gridView2.GetFocusedRowCellDisplayText(fax_col);
            txtIde.Text = gridView2.GetFocusedRowCellDisplayText(id_col);
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.xulydulieu("delete tblDS_GIACONG where Id='" + txtIde.Text + "'");
            DocDanhSachDonViGiaCong();
        }

        private void frmDanhMucDVGiaCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmDV_giacong_Load(object sender, EventArgs e)
        {
            DocDanhSachDonViGiaCong();
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            Gol = gridView2.GetFocusedDisplayText();
            txttenDVGC.Text = gridView2.GetFocusedRowCellDisplayText(tendvgiacong_col);
            txtMDVGC.Text = gridView2.GetFocusedRowCellDisplayText(madonvi_col);
            txtDiaChiGiaCong.Text = gridView2.GetFocusedRowCellDisplayText(diachi_col);
            txtDienThoai.Text = gridView2.GetFocusedRowCellDisplayText(dienthoai_col);
            TxtFax.Text = gridView2.GetFocusedRowCellDisplayText(fax_col);
            txtIde.Text = gridView2.GetFocusedRowCellDisplayText(id_col);
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            gridControl2.ShowPrintPreview();
        }
    }
}