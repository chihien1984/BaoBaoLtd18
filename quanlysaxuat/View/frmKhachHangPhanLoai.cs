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
    public partial class frmKhachHangPhanLoai : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangPhanLoai()
        {
            InitializeComponent();
        }
        //formload
        private void frmKhachHangPhanLoai_Load(object sender, EventArgs e)
        {
            ThPhanLoaiKhachHang();
        }
        private async void ThPhanLoaiKhachHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,MaLoai,
                                TenLoai,NoiDung,DiemSo,
                                NguoiLap,NgayLap,
                                NguoiHieuChinh,NgayHieuChinh
                                from KhachHangPhanLoai");
                    Invoke((Action)(() => {
                    grKhachHangPhanLoai.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        int iD;
        string maLoai;
        string tenLoai;
        string noiDung;
        int diemSo;
        private void gvKhachHangPhanLoai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["ID"])==null)
            {
                return;
            }
            else { 
            iD = (int)gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["ID"]);
            maLoai = (string)gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["MaLoai"]);
            tenLoai= (string)gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["TenLoai"]);
            noiDung= (string)gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["NoiDung"]);
            diemSo= (int)gvKhachHangPhanLoai.GetRowCellValue(gvKhachHangPhanLoai.FocusedRowHandle, gvKhachHangPhanLoai.Columns["DiemSo"]);
            txtMaLoai.Text = maLoai;
            txtTenLoai.Text = tenLoai;
            txtNoiDung.Text = noiDung;
            }
        }

        private void ntnTraCuu_Click(object sender, EventArgs e)
        {
            ThPhanLoaiKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            CapNhat();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }
        private async void Them()
        {
            if (txtMaLoai.Text == "")
            { MessageBox.Show("Ma loai null", "Null"); return; }
            if (txtTenLoai.Text == "")
            { MessageBox.Show("Ten loai null", "Null"); return; }
            if (txtNoiDung.Text == "")
            { MessageBox.Show("Noi dung null", "Null"); return; }
            else
            {
                Model.Function.ConnectSanXuat();
                await Task.Run(() =>
                {
                    string sqlQuery = string.Format(@"insert into KhachHangPhanLoai 
                        (MaLoai,TenLoai,
                        NoiDung,DiemSo,
                        NguoiLap,NgayLap) values 
                        (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',GetDate())",txtMaLoai.Text,txtTenLoai.Text,
                        txtNoiDung.Text,txtDiemSo.Text,MainDev.username);
                    Invoke((Action)(() => {
                        grKhachHangPhanLoai.DataSource = Model.Function.GetDataTable(sqlQuery);
                        ThPhanLoaiKhachHang();
                    }));
                });
            }
        }
        private async void CapNhat()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"update KhachHangPhanLoai 
                        set MaLoai = N'{0}',TenLoai = N'{1}',
                        NoiDung = N'{2}',DiemSo = N'{3}',
                        NguoiHieuChinh=N'{4}',NgayHieuChinh = GetDate()
                        where ID like '{5}'",
                        txtMaLoai.Text,txtTenLoai.Text,
                        txtNoiDung.Text,txtDiemSo.Text,MainDev.username,iD);
                Invoke((Action)(() => {
                    grKhachHangPhanLoai.DataSource = Model.Function.GetDataTable(sqlQuery);
                    ThPhanLoaiKhachHang();
                }));
            });
        }
        private async void Xoa()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"delete from KhachHangPhanLoai 
					where ID like '{0}'", iD);
                Invoke((Action)(() => {
                    grKhachHangPhanLoai.DataSource = Model.Function.GetDataTable(sqlQuery);
                    ThPhanLoaiKhachHang();
                }));
            });
        }

    }
}
