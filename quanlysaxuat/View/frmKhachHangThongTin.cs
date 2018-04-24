using DevExpress.XtraEditors;
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
    public partial class frmKhachHangThongTin : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangThongTin()
        {
            InitializeComponent();
        }
        //formload
        private void frmKhachHangLienHe_Load(object sender, EventArgs e)
        {
            THThongTinKhachHang();
        }
        private async void ThDMNguoiLienHe()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,NguoiLienHe,DiaChi,
					Email,DienThoai 
					from KhachHangLienHe order by ID desc");
                Invoke((Action)(() => {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void DungChung(string a)
        {
            Model.Function.ConnectSanXuat();
            grKhachHangThongTin.DataSource = Model.Function.GetDataTable(a);
            ThDMNguoiLienHe();
        }
        //private void Them()
        //{
        //    string sqlQuery = string.Format(@"");
        //    DungChung(sqlQuery);
        //}
        //private void CapNhat()
        //{
        //    string sqlQuery = string.Format(@"");
        //    DungChung(sqlQuery);
        //}
        //private void Xoa()
        //{
        //    string sqlQuery = string.Format(@"");
        //    DungChung(sqlQuery);
        //}

        private async void THThongTinKhachHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from KhachHangThongTin order by ID desc");
                Invoke((Action)(() => {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void Them()
        {
            if (txtTenKhachHang.Text == "") 
            { MessageBox.Show("Contact null","Null"); return; }
            if(txtDiaChi.Text == "")
            { MessageBox.Show("address null","Null"); return; }
            if(txtDienThoai.Text == "")
            { MessageBox.Show("phone null","Null"); return; }
            if (txtEmail.Text == "")
            { MessageBox.Show("Email null", "Null"); return; }
            else { 
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"insert into KhachHangThongTin 
                        (TenKhachHang,MaKhachHang,DiaChi,
                        KenhLienHe,DienThoai,
                        Email,NguoiLap,NgayLap)
                        values (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}','{5}',
                        N'{6}',GetDate())",
                        txtTenKhachHang.Text,txtMaKhachHang.Text,txtDiaChi.Text,
                        txtKenhLienHe.Text,txtDienThoai.Text,
                        txtEmail.Text,MainDev.username);
                Invoke((Action)(() => {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinKhachHang();
                }));
            });
            }
        }
        private async void CapNhat()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"update KhachHangThongTin 
                        set TenKhachHang=N'{0}',MaKhachHang=N'{1}',DiaChi=N'{2}',
                        KenhLienHe=N'{3}',DienThoai=N'{4}',
                        Email=N'{5}',NguoiHieuChinh=N'{6}',NgayHieuChin=GetDate() where ID like '{7}')",
                        txtTenKhachHang.Text, txtMaKhachHang.Text,
                        txtDiaChi.Text, txtKenhLienHe.Text, txtDienThoai.Text,
                        txtEmail.Text, MainDev.username,iD);
                Invoke((Action)(() => {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinKhachHang();
                }));
            });
        }
        private async void Xoa()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"delete from KhachHangThongTin where ID like '{0}'", iD);
                Invoke((Action)(() => {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinKhachHang();
                }));
            });
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void btnCapNhật_Click(object sender, EventArgs e)
        {
            CapNhat();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void ntnTraCuu_Click(object sender, EventArgs e)
        {
            THThongTinKhachHang();
        }
        int iD;
        string tenKhachHang;
        string diaChi;
        string email;
        string soDienThoai;
        private void gvKhachHangLienHe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["TenKhachHang"])==null) return;
            else {
                iD = (int)gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["ID"]);
                tenKhachHang = (string)gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["TenKhachHang"]);
                diaChi = (string)gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["DiaChi"]);
                email = (string)gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["Email"]);
                soDienThoai = (string)gvKhachHangThongTin.GetRowCellValue(gvKhachHangThongTin.FocusedRowHandle, gvKhachHangThongTin.Columns["DienThoai"]);
                txtTenKhachHang.Text = tenKhachHang;
                txtDiaChi.Text = diaChi;
                txtEmail.Text = email;
                txtDienThoai.Text = soDienThoai;
            }
        }
    }
}