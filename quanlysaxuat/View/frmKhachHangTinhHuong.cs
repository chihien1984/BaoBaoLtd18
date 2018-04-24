using quanlysanxuat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmKhachHangTinhHuong : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHangTinhHuong()
        {
            InitializeComponent();
        }
        //formload
        private void frmKhachHangTinhHuong_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dpSoChiTiet_Min.Text= DateTime.Now.ToString("01-MM-yyyy");
            dpSoChiTiet_Max.Text= DateTime.Now.ToString("dd-MM-yyyy");
            TaoMoiLichGiaoDich();
            ThDanhSachKhachHang();
            ThDSNguoiLienhe();//DS người liên hệ
            ThDMPhanLoaiKhachHang();//danh sách phân loại khách hàng
            //ThThonTinKhachHang();// Thể hiện thông tin giao dịch khách hàng
            //Them diem dat hang - phan hoi - cong no vao chi tiet
            ThDiemCongNo();
            ThDiemDatHang();
            ThDiemPhanHoi();
            //Them diem dat hang - phan hoi - cong no vao so chi tiet
            ThDiemCongNoSoChiTiet();
            ThDiemDatHangSoChiTiet();
            ThDiemPhanHoiSoChiTiet();
            ThSoGiaoDichTheoThoiGian();//Tra cứu sổ giao dịch theo người đăng nhập
        }
        //Điểm đặt hàng
        private async void ThDiemDatHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemDatHang,TenLoai,NoiDung,DiemSo
                    from KhachHangPhanLoai where 
	                TenLoai like N'Điểm đặt hàng'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDiemDatHang.DataSource = Model.Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDiemDatHang.DisplayMember = "DiemDatHang";
                    repositoryItemGridLookUpEditDiemDatHang.ValueMember = "DiemDatHang";
                }));
            });
        }
        //Điểm công nợ
        private async void ThDiemCongNo()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemCongNo,TenLoai,
                    NoiDung,DiemSo from KhachHangPhanLoai where 
	                TenLoai like N'Công nợ'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDiemCongNo.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDiemCongNo.DisplayMember = "DiemCongNo";
                    repositoryItemGridLookUpEditDiemCongNo.ValueMember = "DiemCongNo";
                }));
            });
        }
        //Điểm phản hồi
        private async void ThDiemPhanHoi()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemPhanHoi,
                    TenLoai,NoiDung,DiemSo from KhachHangPhanLoai where 
	                TenLoai like N'Phản hồi'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDiemPhanHoi.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDiemPhanHoi.DisplayMember = "DiemPhanHoi";
                    repositoryItemGridLookUpEditDiemPhanHoi.ValueMember = "DiemPhanHoi";
                }));
            });
        }

        // Diem dat hang - diem cong no - diem phan hoi 
        //Điểm đặt hàng
        private async void ThDiemDatHangSoChiTiet()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemDatHang,
                    TenLoai,NoiDung,DiemSo
                    from KhachHangPhanLoai where 
	                TenLoai like N'Điểm đặt hàng'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDDH.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDDH.DisplayMember = "DiemDatHang";
                    repositoryItemGridLookUpEditDDH.ValueMember = "DiemDatHang";
                }));
            });
        }
        //Điểm công nợ
        private async void ThDiemCongNoSoChiTiet()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemCongNo,TenLoai,
                    NoiDung,DiemSo from KhachHangPhanLoai where 
	                TenLoai like N'Công nợ'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDCN.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDCN.DisplayMember = "DiemCongNo";
                    repositoryItemGridLookUpEditDCN.ValueMember = "DiemCongNo";
                }));
            });
        }
        //Điểm phản hồ
        private async void ThDiemPhanHoiSoChiTiet()
        {
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select MaLoai DiemPhanHoi,
                    TenLoai,NoiDung,DiemSo from KhachHangPhanLoai where 
	                TenLoai like N'Phản hồi'");
                Invoke((Action)(() =>
                {
                    repositoryItemGridLookUpEditDPH.DataSource = Function.GetDataTable(sqlQuery);
                    repositoryItemGridLookUpEditDPH.DisplayMember = "DiemPhanHoi";
                    repositoryItemGridLookUpEditDPH.ValueMember = "DiemPhanHoi";
                }));
            });
        }

        //Danh sách người liên hệ
        private async void ThDSNguoiLienhe()
        {
            //Model.Function.ConnectSanXuat();
            //await Task.Run(() => {
            //    string sqlQuery = string.Format(@"select * from KhachHangLienHe order by ID desc");
            //    Invoke((Action)(() => {
            //        glNguoiLienHe.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
            //        glNguoiLienHe.Properties.DisplayMember = "NguoiLienHe";
            //        glNguoiLienHe.Properties.ValueMember = "NguoiLienHe";
            //    }));
            //});
        }
        //Danh mục phân loại khách hàng
        private async void ThDMPhanLoaiKhachHang()
        {
            //Model.Function.ConnectSanXuat();
            //await Task.Run(() => {
            //    string sqlQuery = string.Format(@"select ID,MaLoai,
            //        TenLoai,NoiDung,DiemSo,
            //        NguoiLap,NgayLap,
            //        NguoiHieuChinh,NgayHieuChinh
            //        from KhachHangPhanLoai order by ID desc");
            //    Invoke((Action)(() => {
            //        glXepLoaiKhachHang.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
            //        glXepLoaiKhachHang.Properties.DisplayMember="MaLoai";
            //        glXepLoaiKhachHang.Properties.ValueMember="MaLoai";
            //    }));
            //});
        }
        //danh sách công ty
        private void ThDanhSachKhachHang()
        {
            string sqlQuery = string.Format(@"select Khachhang,XepHang,CountMH,DiemDoanhSo,
             case when XepHang<=3 then 5
             when (XepHang <=6 and XepHang>3) and (CountMH >=20 and CountMH <=50) then 4
             when (XepHang <=6 and XepHang>3) and (CountMH >=51 and CountMH <=100) then 3
             when (XepHang <=6 and XepHang>3) and (CountMH >100) then 2
             when (XepHang <=10 and XepHang>6)and (CountMH <=50) then 3
             when (XepHang <=10 and XepHang>6)and (CountMH >50 and CountMH <=100) then 2
             when (XepHang <=10 and XepHang>6)and (CountMH >100) then 1
             else 1
             end DiemDDHangHoa
             from			
             (select	a.Khachhang,XepHang,b.CountMH,
             case when XepHang<=3 then 5 
             when XepHang <=6 and XepHang>3 then 4
             when XepHang <=10 and XepHang>6 then 3
             else 1 end DiemDoanhSo
             from (select  dense_rank() over(order by sum(thanhtien) desc) XepHang,Khachhang,
             sum(thanhtien)Diem
             from tblDHCT where thanhtien > 0
             and thoigianthaydoi between '{0}' and Getdate()
             group by Khachhang)a
             inner join
             (select Khachhang,count(KhachHang)CountMH from 
             (select Khachhang,MaSP,count(*)Dem from tblDHCT 
             where year(thoigianthaydoi)>='{1}' and left(MaSP,'3')<>'TEM'
             group by Khachhang,MaSP)a
             group by a.Khachhang)b
             on a.Khachhang=b.Khachhang)c order by XepHang asc",
             dpTu.Value.ToString("01-01-yyyy"),
             dpTu.Value.ToString("yyyy"));
            gridLookUpEditKhachHang.Properties.DataSource = Function.GetDataTable(sqlQuery);
            gridLookUpEditKhachHang.Properties.DisplayMember = "Khachhang";
            gridLookUpEditKhachHang.Properties.ValueMember = "Khachhang";
            gridLookUpEditKhachHang.EditValue = gridLookUpEditKhachHang.Properties.GetKeyValue(0);
        }
        private void ThDanhGiaDoanhSo()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select x.*,y.CongTy,y.NguoiLienHe from
            (select Khachhang,XepHang,CountMH,DiemDoanhSo,
             case when XepHang<=3 then 5
             when (XepHang <=6 and XepHang>3) and (CountMH >=20 and CountMH <=50) then 4
             when (XepHang <=6 and XepHang>3) and (CountMH >=51 and CountMH <=100) then 3
             when (XepHang <=6 and XepHang>3) and (CountMH >100) then 2
             when (XepHang <=10 and XepHang>6)and (CountMH <=50) then 3
             when (XepHang <=10 and XepHang>6)and (CountMH >50 and CountMH <=100) then 2
             when (XepHang <=10 and XepHang>6)and (CountMH >100) then 1
             else 1
             end DiemDDHangHoa
             from			
             (select	a.Khachhang,XepHang,b.CountMH,
             case when XepHang<=3 then 5 
             when XepHang <=6 and XepHang>3 then 4
             when XepHang <=10 and XepHang>6 then 3
             else 1 end DiemDoanhSo
             from (select  dense_rank() over(order by sum(thanhtien) desc) XepHang,Khachhang,
             sum(thanhtien)Diem
             from tblDHCT where thanhtien > 0
             and thoigianthaydoi between '{0}' and Getdate()
             group by Khachhang)a
             inner join
             (select Khachhang,count(KhachHang)CountMH from 
             (select Khachhang,MaSP,count(*)Dem from tblDHCT 
             where year(thoigianthaydoi)>='{1}' and left(MaSP,'3')<>'TEM'
             group by Khachhang,MaSP)a
             group by a.Khachhang)b
             on a.Khachhang=b.Khachhang)c 
            where Khachhang like N'{2}')x
             left outer join
			 (select CongTy,NguoiLienHe 
			 from KhachHangLienHe)y
			 on x.Khachhang=y.CongTy",
             dpTu.Value.ToString("01-01-yyyy"),
             dpTu.Value.ToString("yyyy"),
             gridLookUpEditKhachHang.Text);
                    cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    txtDiemDoanhSo.Text = Convert.ToString(reader[3]);
                    txtDiemDaDangMatHang.Text = Convert.ToString(reader[4]);
                    txtNguoiLienHe.Text = Convert.ToString(reader[6]);
                    reader.Close();
        }
        private void ThThongTinNguoiGiaoDich()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select top 1 NguoiLienHe 
                    from KhachHangLienHe 
		            where CongTy like 'ANH KHÁNH'
                    order by ID desc");
            var kq = Function.GetDataTable(sqlQuery);
            txtNguoiLienHe.Text = kq.Rows[0]["NguoiLienHe"].ToString();
        }
        //
 

        //Thể hiện danh sách đăng ký thông tin khách hàng
        private async void ThThongTinKhachHang()
        {
            //Model.Function.ConnectSanXuat();
            //await Task.Run(() =>
            //{
            //    string sqlQuery = string.Format(@"select * from KhachHangThongTin order by ID desc");
            //    Invoke((Action)(() => {
            //        grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
            //    }));
            //});
        }
        private async void ThSoGiaoDichKhachHang()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from KhachHangHoSo order by ID desc");
                Invoke((Action)(() =>
                {
                    gridControlSoGiaoDich.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void ThSoGiaoDichTheoThoiGian()
        {
            this.gridViewSoGiaoDich.OptionsSelection.MultiSelectMode =
                DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewSoGiaoDich.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,NgayLap,NoiDungLienHe,CongTyLienHe,
                    NgayGhi,NguoiGhi,
                    KetQuaLienHePhanHoi,DiemDatHang,
                    DiemPhanHoi,DiemCongNo,DiemDH,DiemPH,DiemCN,DiemBQ,
                    DiemDoanhSo,DiemDaDang,ThongTinNguoiLienHe,ThongTinKhachHang
                    from KhachHangHoSo where NgayLap 
                    between '{0}' and '{1}' and 
                    NguoiGhi like N'{2}'",
                    dpSoChiTiet_Min.Value.ToString("MM-dd-yyyy"),
                    dpSoChiTiet_Max.Value.ToString("MM-dd-yyyy"),
                    MainDev.username);
                Invoke((Action)(() =>
                {
                    gridControlSoGiaoDich.DataSource = Function.GetDataTable(sqlQuery);
                }));
            });
        }
        //private async void ThThongTinGiaoDich()
        //{
        //    Model.Function.ConnectSanXuat();
        //    await Task.Run(() =>
        //    {
        //        string sqlQuery = string.Format(@"select dateadd(DAY, nbr - 1, '2021-04-01')Ngay,
        //            '' NoiDungLienHe,'' DiemTiemNang,
        //            '' PhanHoiKhachHang,'' XuLyTinhHuong
        //            from (select row_number() OVER ( ORDER BY c.object_id ) AS Nbr
        //            from sys.columns c) nbrs
        //            where nbr - 1 <= datediff(day, '2021-04-01','2021-04-30')");
        //        Invoke((Action)(() =>
        //        {
        //            gridLookUpEditKhachHang.Properties.DataSource = Model.Function.GetDataTable(sqlQuery);
        //        }));
        //    });
        //}
        private void Them()
        {
            try
            {
                if (gridLookUpEditKhachHang.Text=="")
                {
                    MessageBox.Show("","Ten khach hang trong");return;
                }
                if (txtNguoiLienHe.Text=="")
                {
                    MessageBox.Show("Khong duoc bo trong","Nguoi lien he");return;
                }
                if (txtDiemDoanhSo.Text=="")
                {
                    MessageBox.Show("Khong duoc bo trong","Diem cong no");return;
                }
                if (txtDiemDaDangMatHang.Text=="")
                {
                    MessageBox.Show("Khong duoc bo trong","Diem da dang mat hang");return;
                }
                int[] listRowList = this.gridViewThemGiaoDich.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridViewThemGiaoDich.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into KhachHangHoSo (NoiDungLienHe,KetQuaLienHePhanHoi,
	                    CongTyLienHe,DiemDatHang,DiemPhanHoi,
	                    DiemCongNo,ThongTinNguoiLienHe,
	                    ThongTinKhachHang,NguoiGhi,
                        NgayGhi,NgayLap,
                        DiemDoanhSo,DiemDaDang)
                        values (N'{0}',N'{1}',
	                    N'{2}',N'{3}',N'{4}',
                        N'{5}',N'{6}',N'{7}',N'{8}',GetDate(),'{9}','{10}','{11}')",
                        rowData["NoiDungLienHe"], rowData["KetQuaLienHePhanHoi"],
                        gridLookUpEditKhachHang.Text, rowData["DiemDatHang"], rowData["DiemPhanHoi"],
                        rowData["DiemCongNo"], txtNguoiLienHe.Text,
                        rowData["ThongTinKhachHang"],
                        MainDev.username,
                        Convert.ToDateTime(rowData["Ngay"]).ToString("MM-dd-yyyy"),
                        txtDiemDoanhSo.Text,
                        txtDiemDaDangMatHang.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatDiemSoDatHang();
                CapNhatDiemSoPhanHoi();
                CapNhatDiemSoCongNo();
                CapNhatDiemBinhQuan();
                ThSoGiaoDichKhachHang();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void CapNhatDiemSoDatHang()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update KhachHangHoSo set DiemDH=b.DiemSo
                from KhachHangHoSo a
                inner join
                (select MaLoai,DiemSo from KhachHangPhanLoai)b
                on a.DiemDatHang=b.MaLoai");
            var kq = Function.GetDataTable(sqlQuery);
        }
        private void CapNhatDiemSoPhanHoi()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update KhachHangHoSo set DiemPH=b.DiemSo
                from KhachHangHoSo a
                inner join
                (select MaLoai,DiemSo from KhachHangPhanLoai)b
                on a.DiemPhanHoi=b.MaLoai");
            var kq = Function.GetDataTable(sqlQuery);
        }
        private void CapNhatDiemSoCongNo()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update KhachHangHoSo set DiemCN=b.DiemSo
                from KhachHangHoSo a
                inner join
                (select MaLoai,DiemSo from KhachHangPhanLoai)b
                on a.DiemCongNo=b.MaLoai");
            var kq = Function.GetDataTable(sqlQuery);
        }
        private void CapNhatDiemBinhQuan()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"update KhachHangHoSo set DiemBQ=b.DBQ
		   from (select ID,(DiemDoanhSo+DiemDaDang+DiemPH)/3 DBQ 
		   from KhachHangHoSo)b
		   where KhachHangHoSo.ID=b.ID");
            var kq = Function.GetDataTable(sqlQuery);
        }
        private void CapNhat()
        {
            try
            {
                int[] listRowList = this.gridViewSoGiaoDich.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridViewSoGiaoDich.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"update KhachHangHoSo set NoiDungLienHe = N'{0}',KetQuaLienHePhanHoi = N'{1}',
	                    CongTyLienHe = N'{2}',DiemDatHang = N'{3}',DiemPhanHoi = N'{4}',
	                    DiemCongNo = N'{5}',ThongTinNguoiLienHe = N'{6}',
	                    ThongTinKhachHang = N'{7}',NguoiSua = N'{8}',NgaySua = GetDate() where ID like '{9}'",
                        rowData["NoiDungLienHe"], rowData["KetQuaLienHePhanHoi"],
                        gridLookUpEditKhachHang.Text, rowData["DiemDatHang"], rowData["DiemPhanHoi"],
                        rowData["DiemCongNo"], rowData["ThongTinNguoiLienHe"],
                        rowData["ThongTinKhachHang"], MainDev.username,rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                ThSoGiaoDichKhachHang();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private async void Xoa()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"delete from KhachHangHoSo where ID like '{0}'", iD);
                Invoke((Action)(() =>
                {
                    grKhachHangThongTin.DataSource = Model.Function.GetDataTable(sqlQuery);
                    ThSoGiaoDichKhachHang();
                }));
            });
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Them();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CapNhat();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xoa();
        }
    

        private void gvKhachHangLienHe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //iD = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["ID"]);
            //maHoSo = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["MaHoSo"]);
            //tenHoSo = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["TenHoSo"]);
            //noiDungLienHe = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["NoiDungLienHe"]);
            //ctyLienhe = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["CongTyLienHe"]);
            //diemTiemNang = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["DiemTiemNang"]);
            //maLoai = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["MaLoai"]);
            //trangThai= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["TrangThai"]);
            //trangThaiTiepTheo= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["TrangThaiTiepTheo"]);
            //mucTiemNang= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["MucTiemNang"]);
            //phanHoiKhachHang= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["PhanHoiKhachHang"]);
            //xuLyTinhHuong= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["XuLyTinhHuong"]);
            //khachHang= (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["KhachHang"]);
            //nguoiLienHe = (string)gvKhachHangGiaoDich.GetRowCellValue(gvKhachHangGiaoDich.FocusedRowHandle, gvKhachHangGiaoDich.Columns["NguoiLienHe"]);
        }
        //show danh sach nguoi lien he
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHangLienHe khachHang = new frmKhachHangLienHe();
            khachHang.ShowDialog();
            ThDSNguoiLienhe();
        }
        //show danh muc xep loai
        private void btnThemXepLoaiKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHangPhanLoai phanLoai = new frmKhachHangPhanLoai();
            phanLoai.ShowDialog();
            ThDMPhanLoaiKhachHang();
        }

        private void btnTaoMoiKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhachHangThongTin khachHangThongTin = new frmKhachHangThongTin();
            khachHangThongTin.ShowDialog();
            //ThThonTinKhachHang();
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThDMPhanLoaiKhachHang();
            ThDSNguoiLienhe();
            //ThThonTinKhachHang();
            ThDanhGiaDoanhSo();
        }
        private async void TaoMoiLichGiaoDich()
        {
            this.gridViewThemGiaoDich.OptionsSelection.MultiSelectMode =
                DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewThemGiaoDich.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select dateadd(DAY, nbr - 1, '{0}')Ngay,
                    ''NoiDungLienHe,''KetQuaLienHePhanHoi,
					''DiemDatHang,''DiemPhanHoi,''DiemCongNo,
					''ThongTinNguoiLienHe,''ThongTinKhachHang
                    from (select row_number() OVER ( ORDER BY c.object_id ) AS Nbr
                    from sys.columns c) nbrs
                    where nbr - 1 <= datediff(day, '{0}','{1}')",
                       dpTu.Value.ToString("MM-dd-yyyy"),
                       dpDen.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    gridControlThemGiaoDich.DataSource = Model.Function.GetDataToTable(sqlQuery);
                    gridViewThemGiaoDich.SelectAll();
                }));
            });
        }
        private void btnTaoGiaoDich_Click(object sender, EventArgs e)
        {
            TaoMoiLichGiaoDich();
        }

        private void gridViewSoGiaoDichChiTiet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void gridViewThemGiaoDich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*
            if (ckCheckListTongHop.Checked == true)
            {
            btnTaoToaGiaoHang.Enabled = true;
            this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvTongHopCongDoanSanXuat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = true;
            TheHienSoLuongDonHangTheoTo();
            }
            else
            {
            btnTaoToaGiaoHang.Enabled = false;
            this.gvTongHopCongDoanSanXuat.OptionsSelection.MultiSelectMode
            = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            gvTongHopCongDoanSanXuat.Columns["SoDaSanXuat"].Visible = false;
            }
            */

        }
       
        private void gridViewThemGiaoDich_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SANXUATDbContext db = new SANXUATDbContext();
            if (e.Column.FieldName == "")
            {
                var value = gridViewThemGiaoDich.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.KhachHangPhanLoais.FirstOrDefault(x => x.MaLoai == (string)value);
                if (dt != null)
                {
                    gridViewThemGiaoDich.SetRowCellValue(e.RowHandle, "DiemSo", dt.DiemSo);
                }
            }
        }

        private void btnThemSoChiTiet_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void gridLookUpEditKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            ThDanhGiaDoanhSo();
        }

        private void btnTraCuuSoChiTiet_Click(object sender, EventArgs e)
        {
            ThSoGiaoDichTheoThoiGian();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridViewSoGiaoDich.ShowPrintPreview();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gridViewSoGiaoDich.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridViewSoGiaoDich.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                            @"update KhachHangHoSo set NoiDungLienHe = N'{0}',KetQuaLienHePhanHoi = N'{1}',
	                        CongTyLienHe=N'{2}',DiemDatHang=N'{3}',DiemPhanHoi=N'{4}',
	                        DiemCongNo=N'{5}',ThongTinNguoiLienHe=N'{6}',
	                        ThongTinKhachHang=N'{7}',NguoiSua=N'{8}',NgaySua=GetDate(),
                            DiemDoanhSo=N'{9}',DiemDaDang=N'{10}' where ID like '{11}'",
                        rowData["NoiDungLienHe"], rowData["KetQuaLienHePhanHoi"],
                        rowData["CongTyLienHe"], rowData["DiemDatHang"], rowData["DiemPhanHoi"],
                        rowData["DiemCongNo"], rowData["ThongTinNguoiLienHe"],
                        rowData["ThongTinKhachHang"],MainDev.username,
                        rowData["DiemDoanhSo"], rowData["DiemDaDang"],rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatDiemSoDatHang();
                CapNhatDiemSoPhanHoi();
                CapNhatDiemSoCongNo();
                CapNhatDiemBinhQuan();
                ThSoGiaoDichKhachHang();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnThemNguoiLienHe_Click(object sender, EventArgs e)
        {
            frmKhachHangLienHe thongtinKhachHang = new frmKhachHangLienHe();
            thongtinKhachHang.ShowDialog();
        }

        private void btnThemCongTy_Click(object sender, EventArgs e)
        {
            frmKhachHangThongTin thongTinKhachHang = new frmKhachHangThongTin();
            thongTinKhachHang.ShowDialog();
        }

        private void gridControlSoGiaoDich_Click(object sender, EventArgs e)
        {

        }
        DateTime ngayLap;
        string noiDungLienHe;
        string congtyLienHe;
        string ketquaLienHe;
        string diemDatHang;
        string diemPhanHoi;
        string diemCongNo;
        double diemDoanhSo;
        double diemDaDang;
        int iD;

        private void gridViewSoGiaoDich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle, 
                gridViewSoGiaoDich.Columns["ID"]) == null) return;
            if(gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,
                gridViewSoGiaoDich.Columns["NgayLap"]) == null) return;
            else
            {
                ngayLap=(DateTime)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["NgayLap"]);
                noiDungLienHe=(string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["NoiDungLienHe"]);
                congtyLienHe=(string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["CongTyLienHe"]);
                ketquaLienHe = (string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["KetQuaLienHePhanHoi"]);
                diemDatHang=(string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["DiemDatHang"]);
                diemPhanHoi=(string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["DiemPhanHoi"]);
                diemCongNo=(string)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["DiemCongNo"]);
                diemDoanhSo=(double)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["DiemDoanhSo"]);
                diemDaDang=(double)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle,gridViewSoGiaoDich.Columns["DiemDaDang"]);
                iD=(int)gridViewSoGiaoDich.GetRowCellValue(gridViewSoGiaoDich.FocusedRowHandle, gridViewSoGiaoDich.Columns["ID"]);
            }
        }

        private void btnXoaSoGiaoDich_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gridViewSoGiaoDich.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridViewSoGiaoDich.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                            @"delete from KhachHangHoSo where ID like '{0}'",rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                CapNhatDiemSoDatHang();
                CapNhatDiemSoPhanHoi();
                CapNhatDiemSoCongNo();
                CapNhatDiemBinhQuan();
                ThSoGiaoDichKhachHang();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
    }
}
