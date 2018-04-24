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
    public partial class frmKhachHangLienHe : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangLienHe()
        {
            InitializeComponent();
        }
        //formload
        private void frmKhachHangLienHe_Load(object sender, EventArgs e)
        {
            THThongTinNguoiLienHe();
            ThDanhMucCongTy();
        }
        private async void ThDanhMucCongTy() {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select TenKhachHang from KhachHangThongTin");
                Invoke((Action)(() => {
                    cbCongTy.DataSource = Model.Function.GetDataTable(sqlQuery);
                    cbCongTy.DisplayMember ="TenKhachHang";
                    cbCongTy.ValueMember = "TenKhachHang";
                }));
            });
        }

        private async void ThDMNguoiLienHe()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,NguoiLienHe,DiaChi,
					Email,DienThoai,ChucDanh,EmailCaNhan,CongTy,ThanhPho,GhiChu
					from KhachHangLienHe order by ID desc");
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

        private async void THThongTinNguoiLienHe()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,NguoiLienHe,DiaChi,
					Email,DienThoai,ChucDanh,EmailCaNhan,CongTy,ThanhPho,GhiChu
					from KhachHangLienHe order by ID desc");
                Invoke((Action)(() => {
                    grKhachHangLienHe.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void Them()
        {
            if (txtNguoiLienHe.Text == "") 
            { MessageBox.Show("Contact null","Null"); return; }
            if(txtDiaChi.Text == "")
            { MessageBox.Show("address null","Null"); return; }
            if(txtDienThoai.Text == "")
            { MessageBox.Show("phone null","Null"); return; }
            if (txtEmailCongViec.Text == "")
            { MessageBox.Show("Email null", "Null"); return; }
            else { 
                string sqlQuery = string.Format(@"insert into KhachHangLienHe 
				   (NguoiLienHe,DiaChi,
                    Email,DienThoai,
                    NguoiLap,ChucDanh,
                    EmailCaNhan,CongTy,
                    GhiChu,NgayLap) 
					values (N'{0}',N'{1}',
                            N'{2}',N'{3}',
                            N'{4}',N'{5}',
                            N'{6}',N'{7}',
                            N'{8}',GetDate())",
                    txtNguoiLienHe.Text,txtDiaChi.Text,
                    txtEmailCongViec.Text,txtDienThoai.Text,
                    MainDev.username,txtChucDanh.Text,
                    txtEmailCaNhan.Text,cbCongTy.Text,
                    txtGhiChuSoThich.Text);
                    grKhachHangLienHe.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinNguoiLienHe();
            }
        }
        private async void CapNhat()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"update KhachHangLienHe set 
					NguoiLienHe = N'{0}',DiaChi = N'{1}',
					Email = N'{2}',DienThoai = N'{3}',
					NguoiHieuChinh = N'{4}',ChucDanh=N'{5}',
                    EmailCaNhan=N'{6}',CongTy=N'{7}',
                    GhiChu=N'{8}',NgayHieuChinh=GetDate()
					where ID like '{9}'", 
                    txtNguoiLienHe.Text,txtDiaChi.Text,
                    txtEmailCongViec.Text,txtDienThoai.Text,
                    MainDev.username,txtChucDanh.Text,txtEmailCaNhan.Text,
                    cbCongTy.Text,txtGhiChuSoThich.Text,iD);
                Invoke((Action)(() => {
                    grKhachHangLienHe.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinNguoiLienHe();
                }));
            });
        }
        private async void Xoa()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"delete from KhachHangLienHe 
					where ID like '{0}'",iD);
                Invoke((Action)(() => {
                    grKhachHangLienHe.DataSource = Model.Function.GetDataTable(sqlQuery);
                    THThongTinNguoiLienHe();
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
            THThongTinNguoiLienHe();
        }
        int iD;
        string nguoiLienHe;
        string diaChi;
        string email;
        string soDienThoai;
        string chucDanh;
        string emailCaNhan;
        string congTy;
        string ghiChu;
        private void gvKhachHangLienHe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["NguoiLienHe"]) == null) return;
            if (gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["ChucDanh"]) == null) return;
            if (gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["EmailCaNhan"]) == null) return;
            if (gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["CongTy"]) == null) return;
            if (gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["GhiChu"]) == null) return;
            else {
                iD = (int)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["ID"]);
                nguoiLienHe = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["NguoiLienHe"]);
                diaChi = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["DiaChi"]);
                email = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["Email"]);
                soDienThoai = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["DienThoai"]);
                chucDanh = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["ChucDanh"]);
                emailCaNhan = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["EmailCaNhan"]);
                congTy = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["CongTy"]);
                ghiChu = (string)gvKhachHangLienHe.GetRowCellValue(gvKhachHangLienHe.FocusedRowHandle, gvKhachHangLienHe.Columns["GhiChu"]);
                txtNguoiLienHe.Text = nguoiLienHe;
                txtDiaChi.Text = diaChi;
                txtEmailCongViec.Text = email;
                txtDienThoai.Text = soDienThoai;
                txtChucDanh.Text = chucDanh;
                txtEmailCaNhan.Text = emailCaNhan;
                cbCongTy.Text = congTy;
                txtGhiChuSoThich.Text = ghiChu;
            }
        }
    }
}