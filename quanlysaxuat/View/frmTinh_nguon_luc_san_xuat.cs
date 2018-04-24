using DevExpress.XtraPrinting;
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
using quanlysanxuat.Report;

namespace quanlysanxuat.View
{
	public partial class frmTinh_nguon_luc_san_xuat : Form
	{
		public frmTinh_nguon_luc_san_xuat()
		{
			InitializeComponent();
		}

		private void frmTinh_nguon_luc_san_xuat_Load(object sender, EventArgs e)
		{
			txtMember.Text = Login.Username;
			PhanQuyen();
			Thang();
			dpDonHangTKTu.Text = DateTime.Now.ToString("01/MM/yyyy");
			dpDonHangTKDen.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
			dptu.Text = DateTime.Now.ToString("01/01/yyyy");
			dpden.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
			cbthang.Text = DateTime.Now.Month.ToString();
			
			DocDSNguonLuc();
			this.gridView2.Appearance.Row.Font = new Font("Times New Roman", 7f);
			this.gridView1.Appearance.Row.Font = new Font("Times New Roman", 7f);
			this.gridView3.Appearance.Row.Font = new Font("Times New Roman", 7f);
			this.gridView5.Appearance.Row.Font = new Font("Times New Roman", 7f);
            DocDonDatHangTheoNgay();
        }
		private void PhanQuyen()
		{
			if (Login.role == "1039" || Login.role == "1")
			{
				btnCapNhatDinhMucDonHang.Enabled = true;
			}
			if (Login.role == "1")
			{
				btnDeleteAllTemp.Enabled = true;
			}
			else
			{
				btnCapNhatDinhMucDonHang.Enabled = false;
				btnDeleteAllTemp.Enabled = false;
			}
		}
		private void Thang()
		{
			for (int i = 1; i <= 12; i++)
			{
				cbthang.Items.Add(i.ToString());
			}
		}
		private void DocTatCaDonDatHang()
		{
			ketnoi kn = new ketnoi();
			gridControl1.DataSource = kn.laybang(@"select D.Masp TrangThai,madh,C.Masp,
				Tenquicach,dvt,Soluong,ngaygiao,
				Khachhang,Mau_banve,Tonkho,ghichu,
				ngoaiquang,pheduyet,Diengiai,nguoithaydoi,Iden,TrangThai from tblDHCT C
				left outer join(select distinct(Masp) from tblDMuc_LaoDong) D
				on D.MaSP=C.MaSP
				order by Code Desc");
			kn.dongketnoi();
		}
		private void DocDonDatHangTheoNgay()
		{
			ketnoi kn = new ketnoi();
			gridControl1.DataSource = kn.laybang(@"select case when D.Masp<>'' then 'x' end CoDinhMuc,
				case when P.DonHangID <>'' then 'x' end DaChiaNguonLuc,madh,C.Masp,
				Tenquicach,dvt,Soluong,ngaygiao,
				Khachhang,Mau_banve,Tonkho,ghichu,
				ngoaiquang,pheduyet,Diengiai,nguoithaydoi,Iden from tblDHCT C
				left outer join(select distinct(Masp) from tblDMuc_LaoDong) D
				on D.MaSP=C.MaSP left outer join
				(select DonHangID from tblCalender_Product group by DonHangID)P
				on C.Iden=P.DonHangID
				where convert( datetime,thoigianthaydoi) 
				between '" + dpDonHangTKTu.Value.ToString("yyyy-MM-dd") + "' and '" + dpDonHangTKDen.Value.ToString("yyyy-MM-dd") + "' order by Code Desc");
			kn.dongketnoi();
		}
		#region Function đọc công suất sản xuất của đơn hàng theo IDSelect
		private void TinhLichSanXuatChiTiet()
		{
			ketnoi kn = new ketnoi();
			string sqlQuery = string.Format(@"select c.MaPo,madh,Tenquicach,
					p.* from LichSanXuatChiTiet_func('{0}','{1}','{2}','{3}')p
					left outer join tblDHCT c on
					p.DonHangID=c.Iden
					where IDChon = "+lbPointSave.Text+"",
				dpNam.Value.ToString("yyyy"),
				cbthang.Text,
				txtGioLamViec.Text,
				txtMaSanPham.Text);//('2019',06,12,'HOW-ST-1207')
			gridControl2.DataSource = kn.laybang(sqlQuery);
			kn.dongketnoi();
		}
		#endregion
		#region Function đọc tất cả công suất sản xuất của đơn hàng theo IDSelect
		private void DocTatCaCongSuatSanXuat()
		{
			ketnoi kn = new ketnoi();
			string sqlQuery = string.Format(@"select c.MaPo,madh,Tenquicach,
					p.* from LichSanXuatChiTiet_func('{0}','{1}','{2}','{3}') p
					left outer join tblDHCT c on
					p.DonHangID=c.Iden",
				dpNam.Value.ToString("yyyy"),
				cbthang.Text,
				txtGioLamViec.Text,
				"%");//('2019',06,12,'HOW-ST-1207')
			gridControl2.DataSource = kn.laybang(sqlQuery);
			kn.dongketnoi();
		}
		#endregion
		private void btnDocTatCaDonDatHang_Click(object sender, EventArgs e)
		{
			DocTatCaDonDatHang();
		}

		private void btnDonHang_TrienKhai_Click(object sender, EventArgs e)
		{
			DocDonDatHangTheoNgay();
		}
		private void DocNguyenCong()
		{
			ketnoi kn = new ketnoi();
			gridControl4.DataSource = kn.laybang(@"select ChiTietSanPham,
				SoChiTietSanPham,SoChiTiet,TrungCongDoan,NguyenCong,
				Masp,Tensp,NguyenCong,Tencondoan,Dinhmuc,id
				from tblDMuc_LaoDong where
				Masp ='" + txtMaSanPham.Text + "'");
			kn.dongketnoi();
			gridView4.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
		}
		private void DocMaBangID()
		{
			SqlConnection con = new SqlConnection();
			con.ConnectionString = Connect.mConnect;
			if (con.State == ConnectionState.Closed)
				con.Open();
			SqlCommand cmd = new SqlCommand(@"select Top 1 
				REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.Read())
				lbPointSave.Text = Convert.ToString(reader[0]);
			reader.Close();
		}


		private void gridControl1_Click(object sender, EventArgs e)
		{
			DocMaBangID();//Hàm tạo ID cho công suất
			string point = "";
			point = gridView1.GetFocusedDisplayText();
			txtMadh.Text = gridView1.GetFocusedRowCellDisplayText(Madh_col1);
			txtMaSanPham.Text = gridView1.GetFocusedRowCellDisplayText(masp_col1);
			txtTenSanPham.Text = gridView1.GetFocusedRowCellDisplayText(sanpham_col1);
			txtSoLuongDonHang.Text = gridView1.GetFocusedRowCellDisplayText(Soluong_DH_col1);
			txtDonVi.Text = gridView1.GetFocusedRowCellDisplayText(donvitinh_col1);
			txtDonHangID.Text = gridView1.GetFocusedRowCellDisplayText(donHangID_col1);
			txtTrangThaiDaPhanBo.Text = gridView1.GetFocusedRowCellDisplayText(daChiaNguonLuc_col1);
			/*Ghi dữ liệu vào bảng tạm tính 
			(Ghi số lượng đơn hàng x số chi tiết = số lượng chi tiết đơn hàng)
			Ghi Ép công suất = 1 để khi chia kế hoạch có thể tăng công suất đơn hàng lên*/
			txtSoLuongTonKho.Text = "0";
			TinhTruTonKho();//tinh so luong can lam
			// neu nhu tinh trang phan bo khong co thi load moi
			if (txtTrangThaiDaPhanBo.Text == "")
			{
				GhiVaoBanTam();//Ghi dữ liệu từ bảng ChiTietCong vào bảng tạm
			    /*Đọc dữ liệu từ bản tạm 
				DocDanhSach tính lịch(từ function tblCalender_Product_Temp) 
				tính số lượng bình quân cho mỗi ngày*/
				TinhLichSanXuatChiTiet();
				DocDSCalenderProductTheoID();//Đọc danh sách nguồn lực chi tiết theo ID
				DocNguyenCong();//Đọc nguyên công sản xuất theo từng mã sản phẩm
				DSNguonLucTheoDonHangID();//Đọc danh sách nguồn lực đã phân bổ
				UnCheckGrid();
			}
			else
			{
				//Load Lại đơn hàng từ Nguồn lực đã phân bổ
				LoadLaiDuLieuDaGhi();
				//doc thang cua don hang dang phan do
				DocThangCuaDonHangDaPhanBo();
				//Tính lại nguồn lực đã phân bổ
				TinhLichSanXuatChiTiet();
				DSNguonLucTheoDonHangID();
				DocKeHoachSanXuatNgayTheoDonHang();
			}
		}
		//Ham tinh so luong can lam (SoLuongCanLam=SoLuongDonHang-TonKho)
		private void TinhTruTonKho()
		{
			try
			{
				double soLuongDonHang = double.Parse(txtSoLuongDonHang.Text);
				double soLuongTonKho = double.Parse(txtSoLuongTonKho.Text);
				double soluongCanLam = soLuongDonHang - soLuongTonKho;
				txtSoLuongCanLam.Text = soluongCanLam.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error" + ex, "Message"); return;
			}
		}
		//Doc don hang da phan bo nguon luc
		private void DocDSDonHangDaPhanBoNguonLuc()
		{

		}
		private void gridControl1_DoubleClick(object sender, EventArgs e)
		{
			GhiVaoBanTam();
			//Đọc dữ liệu từ bản tạm 
			//DocDanhSach tính lịch(từ function tblCalender_Product_Temp) 
			//+ tính số lượng bình quân cho mỗi ngày
			TinhLichSanXuatChiTiet();
			DocDSCalenderProductTheoID();//Đọc danh sách nguồn lực chi tiết theo ID
			DocNguyenCong();//Đọc nguyên công sản xuất theo từng mã sản phẩm
			DSNguonLucTheoDonHangID();//Đọc danh sách nguồn lực đã phân bổ
			UnCheckGrid();
		}
		//Doc thang cua dong hang da phan bo
		private void DocThangCuaDonHangDaPhanBo()
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection con = new SqlConnection();
			con.ConnectionString = Connect.mConnect;
			if (con.State == ConnectionState.Closed)
				con.Open();
			cmd = new SqlCommand(@"select month(BatDau) 
			from tblCalender_Product
			where DonHangID =" + txtDonHangID.Text + "", con);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.Read())
				cbthang.Text = Convert.ToString(reader[0]);
			reader.Close();
		}
		private void DocDSCalenderProduct()
		{
			ketnoi kn = new ketnoi();
			gridControl3.DataSource = kn.laybang(@"select * from tblCalender_Product order by KetThuc ASC");
			kn.dongketnoi();
		}

		private void DocDSCalenderProductTheoID()
		{
			ketnoi kn = new ketnoi();
			gridControl3.DataSource = kn.laybang(@"select * from tblCalender_Product 
				where DonHangID='" + txtDonHangID.Text + "' order by KetThuc DESC");
			kn.dongketnoi();
			gridView3.ExpandAllGroups();
		}

		private void DSNguonLucTheoNgay()
		{
			ketnoi kn = new ketnoi();
			gridControl3.DataSource = kn.laybang(@"SELECT * FROM tblCalender_Product where 
				NgayLap between '" + dptu.Value.ToString("yyyy-MM-dd") + "' and '" + dpden.Value.ToString("yyyy-MM-dd") + "' order by KetThuc DESC");
			kn.dongketnoi();
		}

		private void DSNguonLucTheoDonHangID()
		{
			ketnoi kn = new ketnoi();
			gridControl3.DataSource = kn.laybang(@"select madh +'-'+C.MaSP+'-'+C.Tenquicach
				ThonTin,C.madh,C.MaSP,
				P.* from tblCalender_Product P
				left outer join tblDHCT C
				on P.DonHangID=C.Iden where 
				DonHangID=" + txtDonHangID.Text + " order by KetThuc ASC");
			kn.dongketnoi();
			gridView3.ExpandAllGroups();
		}
		private void DSNguonLucTheoDonHangTheoThoiGian()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"select madh +'-'+C.MaSP+'-'+C.Tenquicach
				ThonTin,C.madh,C.MaSP,
				P.* from tblCalender_Product P
				left outer join tblDHCT C
				on P.DonHangID=C.Iden where P.NgayLap 
				between '{0}' and '{1}' order by KetThuc ASC",
				dptu.Value.ToString("yyyy-MM-dd"),
				dpden.Value.ToString("yyyy-MM-dd"));
			gridControl3.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			gridView3.ExpandAllGroups();
		}
		private void btnTraCuuDSNguonLuc_Click(object sender, EventArgs e)
		{
			DSNguonLucTheoDonHangTheoThoiGian();
		}
		private void btnXoaNguonLucDaChia_Click(object sender, EventArgs e)
		{
			XoaBanGhiProductCu();
			DSNguonLucTheoDonHangID();
			DocDonDatHangTheoNgay();
		}

		private void btnGhi_Click(object sender, EventArgs e)
		{
			try
			{
				SqlConnection con = new SqlConnection();
				con.ConnectionString = Connect.mConnect;
				if (con.State == ConnectionState.Closed)
					con.Open();
				DataRow rowData;
				int[] listRowList = this.gridView4.GetSelectedRows();
				for (int i = 0; i < listRowList.Length; i++)
				{
					rowData = this.gridView4.GetDataRow(listRowList[i]);
					string strQuery = string.Format(@"update tblDMuc_LaoDong 
						set NguyenCong=N'{0}',
						SoChiTiet=N'{1}',
						TrungCongDoan=N'{2}',
						NguoiHC_CV=N'{3}',
						NgayHC_CV=GetDate(),
						ChiTietSanPham=N'{4}'
						where id='{5}'",
						rowData["NguyenCong"],
						rowData["SoChiTiet"],
						rowData["TrungCongDoan"],
						txtMember.Text,
						rowData["ChiTietSanPham"],
						rowData["id"]);
					SqlCommand cmd = new SqlCommand(strQuery, con);
					cmd.ExecuteNonQuery();
				}
				con.Close();
				DocNguyenCong();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi lý do :" + ex.Message);
			}
		}

		private void DocNguyenCongTheoNgay()
		{
			ketnoi kn = new ketnoi();
			gridControl4.DataSource = kn.laybang(@"select ChiTietSanPham,
				SoChiTietSanPham,Masp,Tensp,NguyenCong,
				Tencondoan,Dinhmuc,id,
				NguoiHC_CV,NgayHC_CV,TrungCongDoan
				from tblDMuc_LaoDong cast(NgayHC_CV as Date) 
				between '" + dpnguyenCong_BD.Value.ToString("MM-dd-yyyy") + "'  and '" + dpnguyenCong_KT.Value.ToString("MM-dd-yyyy") + "'");
			kn.dongketnoi();
		}
		private void btnTraCuNguyenCong_Click(object sender, EventArgs e)
		{
			DocNguyenCongTheoNgay();
		}
		private void btnThemNguyenCong_Click(object sender, EventArgs e)
		{
			frmResources Resources = new frmResources();
			Resources.ShowDialog();
			DocDSNguonLuc();
		}
		private void DocDSNguonLuc()//GridlookupEdit Repository chọn mã nguồn lực trong gridcontrol Áp mã nguồn lực
		{
			ketnoi kn = new ketnoi();
			repository.DataSource = kn.laybang(@"SELECT Ma_Nguonluc,
				Ten_Nguonluc,Nguoi,Ngay FROM tblResources");
			repository.DisplayMember = "Ma_Nguonluc";
			repository.ValueMember = "Ma_Nguonluc";
			kn.dongketnoi();
		}



		private void btnDonHang_TrienKhai_Click_1(object sender, EventArgs e)
		{
			DocDonDatHangTheoNgay();
		}


		//Load lại dữ liệu đã phân bổ vào Calender_product
		private void LoadLaiDuLieuDaGhi()
		{
			//truncate table tblCalender_Product_Temp;
			ketnoi kn = new ketnoi();
			string sqlQuery = string.Format(@"
			insert into tblCalender_Product_Temp
			(DinhMucID,Masp,
			Tencondoan,Dinhmuc,
			EpCongSuat,NguyenCong,
			NgayBatDau,DonHangID,
			SoChiTiet,SoLuongDonHang,SoLuongChiTietDonHang,TonKhoChiTiet,ChiTietSanPham)
			select DinhMucID,Masp,Tencondoan,Dinhmuc,
			EpCongSuat,NguyenCong,BatDau,
			DonHangID,SoChiTiet,SoLuongDonHang,SoLuongChiTietDonHang,TonKhoChiTiet,ChiTietSanPham
			from tblCalender_Product
			where DonHangID='{0}'", txtDonHangID.Text);
			var dt = kn.xulydulieu(sqlQuery);
			kn.dongketnoi();
		}

		//Kiểm tra xem ID đã có trong file tạm hay chưa nếu có thì hỏi có muốn ghi đè không, nếu chưa thì ghi mới
		private void GhiVaoBanTam()
		{
			ketnoi kn = new ketnoi();
			var dt = kn.xulydulieu(@"select count(Distinct(DonHangID)) 
				from tblCalender_Product_Temp where DonHangID='" + txtDonHangID.Text + "'"); //Kiểm tra file tạm
			if (dt > 0)
			{
				DialogResult dialogResult = MessageBox.Show("ID đã có", "Thông báo", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					InsertCalender_Product_Temp();//Hàm ghi vào bảng tạm
				}
				else if (dialogResult == DialogResult.No)
				{
					return;
				}
			}
			else
			{
				InsertCalender_Product_Temp();//Hàm ghi vào bảng tạm
			}
			kn.dongketnoi();
		}
		//Ghi dữ liệu công suất=1 vào file load lên lại để ép công suất lên
		private void InsertCalender_Product_Temp()
		{
			if (txtDonHangID.Text == "")
			{
				MessageBox.Show("Vui lòng chọn vào bảng", "Lỗi chọn lựa"); return;
			}
			else
			{
				try
				{
					double soLuong = double.Parse(txtSoLuongCanLam.Text);
					double soTonKho = double.Parse(txtSoLuongTonKho.Text);
					double soDonHang = double.Parse(txtSoLuongDonHang.Text);
					ketnoi kn = new ketnoi();
					string sqlQuery = string.Format(@"
						insert into tblCalender_Product_Temp
						(DinhMucID, Masp, Tencondoan, Dinhmuc, 
						EpCongSuat, NguyenCong, SoChiTiet, SoLuongChiTietDonHang, 
						NgayBatDau, DonHangID,TonKhoChiTiet,SoLuongDonHang,ChiTietSanPham,
						ThuTuCongDoan,IDChon,NguoiLap,NgayLap)
						(select id DinhMucID, Masp, Tencondoan,
						Dinhmuc, EpCongSuat = 1, NguyenCong, SoChiTiet,
						SoChiTiet * {0} SoLuongChiTietDonHang, '{1}' NgayBatDau,
						{2} DonHangID,{3} TonKhoChiTiet,{4} SoLuongDonHang,ChiTietSanPham,
						ThuTuCongDoan,{5} IDChon,'{6}'NguoiLap,'{7}' NgayLap
						from tblDMuc_LaoDong where Masp like N'{8}'
						and(TrungCongDoan = '' or TrungCongDoan is null) and SoChiTiet>0)",
					soLuong,
					dpNgayBatDau.Value.ToString("yyyy-MM-dd"),
					txtDonHangID.Text,
					soTonKho,
					soDonHang,
					lbPointSave.Text,
					txtMember.Text,
                    dpNgayBatDau.Value.ToString("yyyy-MM-dd"), 
                    txtMaSanPham.Text);
					gridControl2.DataSource = kn.laybang(sqlQuery);
					kn.dongketnoi();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lý do id chọn chưa có" + ex, "Lỗi");
				}
			}
		}
		//Cập nhật ép công suất
		private void UpdateEpCongSuatPrTemp()
		{
			SqlConnection con = new SqlConnection();
			con.ConnectionString = Connect.mConnect;
			if (con.State == ConnectionState.Closed)
				con.Open();
			for (int i = 0; i < gridView2.DataRowCount; i++)
			{
				var batdau = Convert.ToDateTime(gridView2.GetRowCellValue(i, "BatDau")).ToString("yyyy-MM-dd");
				string strQuery = string.Format(@"update tblCalender_Product_Temp
						set EpCongSuat = '{0}',
						NgayBatDau = '{1}',
						SoLuongDonHang = '{2}',
						TonKhoChiTiet= '{3}',
						SoLuongChiTietDonHang=cast('{2}' as float)*cast('{4}' as int)-cast('{3}' as float)
					where ProductTemID = '{5}'",
					gridView2.GetRowCellValue(i, "EpCongSuat").ToString(),
					batdau,
					gridView2.GetRowCellValue(i, "SoLuongDonHang").ToString(),
					gridView2.GetRowCellValue(i, "TonKhoChiTiet").ToString(),
					gridView2.GetRowCellValue(i, "SoChiTiet").ToString(),
					gridView2.GetRowCellValue(i, "ProductTemID").ToString());
				SqlCommand cmd = new SqlCommand(strQuery, con);
				cmd.ExecuteNonQuery();
			}
			con.Close();
			//if (this.gridView2.GetSelectedRows().Count() < 0)
			//{ MessageBox.Show("Cần tích chọn", "Thông báo"); return; }
			//try
			//{
			//    DateTime batdau = DateTime.Parse(dpNgayBatDau.Text);
			//    SqlConnection con = new SqlConnection();
			//    con.ConnectionString = Connect.mConnect;
			//    if (con.State == ConnectionState.Closed)
			//        con.Open();
			//    DataRow rowData;
			//    int[] listRowList = this.gridView2.GetSelectedRows();
			//    for (int i = 0; i < listRowList.Length; i++)
			//    {
			//        rowData = this.gridView2.GetDataRow(listRowList[i]);
			//        string strQuery = string.Format(@"update tblCalender_Product_Temp
			//        set EpCongSuat = '{0}',
			//            NgayBatDau = '{1}',
			//            SoLuongDonHang = '{2}',
			//            TonKhoChiTiet= '{3}',
			//            SoLuongChiTietDonHang ='{2}'-'{3}'
			//            where ProductTemID = '{4}'",
			//                rowData["EpCongSuat"],
			//                batdau,
			//                rowData["SoLuongDonHang"],
			//                rowData["TonKhoChiTiet"],
			//                rowData["ProductTemID"]);
			//        SqlCommand cmd = new SqlCommand(strQuery, con);
			//        cmd.ExecuteNonQuery();
			//    }
			//    con.Close();
			//}
			//catch (Exception ex)
			//{
			//    MessageBox.Show("Lỗi lý do : " + ex, "Thông báo");
			//}
		}

		private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (e.Column == epCongSuat_col2 ||
				e.Column == batDau_col2 || e.Column == TonKhoChiTiet_grid2)
			{
				UpdateEpCongSuatPrTemp();//Cập nhật lại bảng tạm
				TinhLichSanXuatChiTiet();//Load lại lịch chi tiết
			}
		}
		//Trước khi ghi kiểm tra xem don hang da co phan bo hay chua
		//nếu da co phan bo roi thi hoi xem co dong y ghi de lai du lieu khong
		//Nếu không đồng ý thì thoát, nếu đồng ý ghi đè lên bản ghi cũ
		private void btnCapNhatDinhMucDonHang_Click(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection();
			con.ConnectionString = Connect.mConnect;
			if (con.State == ConnectionState.Closed)
				con.Open();
			//kiem tra trung
			string strQuery = string.Format(@"select Count(Distinct DonHangID)
				from tblCalender_Product where DonHangID = " + txtDonHangID.Text + "");
			SqlCommand cmd = new SqlCommand(strQuery, con);
			int dt = Convert.ToInt16(cmd.ExecuteScalar());
			con.Close();
			if (dt == 1)
			{
				//hoi co dong y ghi de khong
				DialogResult dialogResult = MessageBox.Show("Đơn hàng đã tính bạn có muốn ghi đè không", "Thông báo", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
				else if (dialogResult == DialogResult.Yes)
				{
					//xoa ban ghi cu
					XoaBanGhiProductCu();
					//ghi ban ghi moi
					SaveCalenderProduct();
				}
			}
			else if (dt == 0)
			{
				SaveCalenderProduct();
			}
			DocDonDatHangTheoNgay();
		}

		private void XoaBanGhiProductCu()
		{
			ketnoi kn = new ketnoi();
			var dt = kn.xulydulieu(@"delete from tblCalender_Product 
				where DonHangID=" + txtDonHangID.Text + "");
			kn.dongketnoi();
		}
		private void SaveCalenderProduct()
		{

			double soluong = txtSoLuongCanLam.Text == "" ? 0 : double.Parse(txtSoLuongCanLam.Text);
			ketnoi kn = new ketnoi();
			string strQuery = string.Format(@"insert into tblCalender_Product
				(ChiTietSanPham,TonKhoChiTiet,SoLuongDonHang,DonHangID,DinhMucID,Masp,Tencondoan,Dinhmuc,
				NgayCanLam,EpCongSuat,NguyenCong,BatDau,KetThuc,
				Sunday,SanLuongBQ,thang,nam,TrienKhai,ConLai,ThuTuCongDoan,
				date01,date02,date03,date04,date05,
				date06,date07,date08,date09,date10,
				date11,date12,date13,date14,date15,
				date16,date17,date18,date19,date20,
				date21,date22,date23,date24,date25,
				date26,date27,date28,date29,date30,
				date31,SoChiTiet,SoLuongChiTietDonHang,NguoiLap,NgayLap)
				select ChiTietSanPham,TonKhoChiTiet,SoLuongDonHang ,DonHangID,DinhMucID,Masp,Tencondoan,Dinhmuc,
				NgayCanLam,EpCongSuat,NguyenCong,BatDau,KetThuc,Sunday,SanLuongBQ,thang,
				nam,TrienKhai,ConLai,ThuTuCongDoan,
				date01,date02,date03,date04,date05,
				date06,date07,date08,date09,date10,
				date11,date12,date13,date14,date15,
				date16,date17,date18,date19,date20,
				date21,date22,date23,date24,date25,
				date26,date27,date28,date29,date30,
				date31,SoChiTiet,SoLuongChiTietDonHang,N'" + txtMember.Text + "' NguoiLap,GetDate() NgayLap  from LichSanXuatChiTiet_func ('{0}','{1}','{2}','{3}')",
				dpNam.Value.ToString("yyyy"),
				cbthang.Text,
				txtGioLamViec.Text,
				txtMaSanPham.Text
				);
			var kq = kn.xulydulieu(strQuery);
			if (kq > 0) { MessageBox.Show("Đã phân bổ", "Thông báo"); }
			else
			{
				MessageBox.Show("không thành công", "Thông báo");
			}
			kn.dongketnoi();
		}

		private void btnBanVe_Click(object sender, EventArgs e)
		{
			frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
			f2.Show();
		}

		private void btnCheck_Click(object sender, EventArgs e)
		{
			CheckGrid();
		}
		private void CheckGrid()
		{
			this.gridView2.OptionsSelection.MultiSelectMode =
				DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			gridView2.OptionsSelection.CheckBoxSelectorColumnWidth = 20;

		}
		private void UnCheckGrid()
		{
			this.gridView2.OptionsSelection.MultiSelectMode =
				DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
		}
		private void btnPhanBoTuyChon_Click(object sender, EventArgs e)
		{
			CapNhatBangTamDaChon();//Cập nhật tùy chọn x vào bảng tạm de xoa nhung cai chua chọn
			XoaBanTamChuaChon();//Xóa các phần còn lại
			GhiTuyChonDaPhanBo();//Ghi tùy chọn vào bảng phân bổ nguồn lực
			DocDonDatHangTheoNgay();//Đọc danh sách
		}

		private void GhiTuyChonDaPhanBo()
		{
			double soluong = txtSoLuongCanLam.Text == "" ? 0 : double.Parse(txtSoLuongCanLam.Text);
			ketnoi kn = new ketnoi();
			string strQuery = string.Format(@"insert into tblCalender_Product
				(ChiTietSanPham,TonKhoChiTiet,SoLuongDonHang,DonHangID,DinhMucID,Masp,Tencondoan,Dinhmuc,
				NgayCanLam,EpCongSuat,NguyenCong,BatDau,KetThuc,
				Sunday,SanLuongBQ,thang,nam,TrienKhai,ConLai,ThuTuCongDoan,
				date01,date02,date03,date04,date05,
				date06,date07,date08,date09,date10,
				date11,date12,date13,date14,date15,
				date16,date17,date18,date19,date20,
				date21,date22,date23,date24,date25,
				date26,date27,date28,date29,date30,
				date31,SoChiTiet,SoLuongChiTietDonHang,NguoiLap,NgayLap)
				select ChiTietSanPham,TonKhoChiTiet,SoLuongDonHang ,DonHangID,DinhMucID,Masp,Tencondoan,Dinhmuc,
				NgayCanLam,EpCongSuat,NguyenCong,BatDau,KetThuc,Sunday,SanLuongBQ,thang,
				nam,TrienKhai,ConLai,ThuTuCongDoan,
				date01,date02,date03,date04,date05,
				date06,date07,date08,date09,date10,
				date11,date12,date13,date14,date15,
				date16,date17,date18,date19,date20,
				date21,date22,date23,date24,date25,
				date26,date27,date28,date29,date30,
				date31,SoChiTiet,SoLuongChiTietDonHang,'" + txtMember.Text + "' NguoiLap,GetDate() NgayLap  from LichSanXuatChiTiet_func ('{0}','{1}','{2}','{3}')",
				dpNam.Value.ToString("yyyy"),
				cbthang.Text,
				txtGioLamViec.Text,
				txtMaSanPham.Text
				);
			var kq = kn.xulydulieu(strQuery);
			if (kq > 0) { MessageBox.Show("Đã phân bổ", "Thông báo"); }
			else
			{
				MessageBox.Show("không thành công", "Thông báo");
			}
			kn.dongketnoi();
		}
		private void XoaTuyChonDaPhanBoCu()
		{
			ketnoi kn = new ketnoi();
			var dt = kn.xulydulieu(@"");
			kn.dongketnoi();
		}

		private void XoaBanGhiDaPhanBoCuTuyChon()
		{
			ketnoi kn = new ketnoi();
			var dt = kn.xulydulieu(@"delete from tblCalender_Product 
				where DonHangID =" + txtDonHangID.Text + "");
			kn.dongketnoi();
		}
		private void CapNhatBangTamDaChon()
		{
			SqlConnection con = new SqlConnection();
			con.ConnectionString = Connect.mConnect;
			if (con.State == ConnectionState.Closed)
				con.Open();
			DataRow rowData;
			int[] listRowList = this.gridView2.GetSelectedRows();
			for (int i = 0; i < listRowList.Length; i++)
			{
				rowData = this.gridView2.GetDataRow(listRowList[i]);
				string strQuery = string.Format(@"update tblCalender_Product_Temp set XoaPhanBoTuyChon='x' where ProductTemID = '{0}'",
				rowData["ProductTemID"]);
				SqlCommand cmd = new SqlCommand(strQuery, con);
				cmd.ExecuteNonQuery();
			}
			con.Close();
		}
		private void XoaBanTamChuaChon()
		{
			ketnoi kn = new ketnoi();
			int kq = kn.xulydulieu(@"delete from tblCalender_Product_Temp where XoaPhanBoTuyChon is null");
			kn.dongketnoi();
		}

		private void btnExportLichSanXuatChiTiet_Click(object sender, EventArgs e)
		{
			gridControl3.ShowPrintPreview();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"select N'Đơn hàng: '+ c.madh+char(13)+
			N'; Sản phẩm'+c.Tenquicach+
			char(13)+N'; Số lượng:'+cast(c.Soluong as nvarchar) ThongTin,
			n.ProDucID,n.NguyenCong,n.Tencondoan,
			p.* from
			(select t.ProDucID,t.DonHangID,t.NguyenCong,t.Tencondoan,t.BatDau,e.KetThuc from
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			Min(BatDau)BatDau from tblCalender_Product
			group by DonHangID,NguyenCong)t
			left outer join
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			max(KetThuc)KetThuc from tblCalender_Product
			group by DonHangID,NguyenCong)e
			on t.ProDucID=e.ProDucID)n
			left outer join  
			(select ProDucID,DinhMucID,DonHangID,Masp,Tencondoan,Dinhmuc,NgayCanLam,
			EpCongSuat,NguyenCong,Sunday,SanLuongBQ,thang,nam,TrienKhai,
			ConLai,date01,date02,date03,date04,date05,date06,date07,date08,date09,
			date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
			date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
			date30,date31,NguoiLap,NgayLap,SoChiTiet,SoLuongChiTietDonHang,TonKhoChiTiet,
			SoLuongDonHang from tblCalender_Product) p
			on p.ProDucID=n.ProDucID
			left outer join 
			(select Iden,madh,Tenquicach,Soluong from tblDHCT) c
			on p.DonHangID=c.Iden where NgayLap 
				between '{0}' and '{1}'",
				dptu.Value.ToString("yyyy-MM-dd"),
				dpden.Value.ToString("yyyy-MM-dd"));
			gridControl5.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			gridView5.ExpandAllGroups();
		}

		private void simpleButton3_Click(object sender, EventArgs e)
		{
			gridControl5.ShowPrintPreview();
		}

		private void ntndocKeHoachHangNgay_Click(object sender, EventArgs e)
		{
			DocKeHoachSanXuatTheoNgay();
		}
		private void DocKeHoachSanXuatTheoNgay()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"select p.NgayLap,p.NguoiLap,
			p.ProDucID,p.NguyenCong,N'Đơn hàng: '+ c.madh+char(13)+'; '+c.Tenquicach+
			char(13)+N'; '+cast(format(p.SoLuongDonHang,'#,#') as nvarchar)
			+N';Chi tiết'+cast(SoChiTiet as nvarchar)+
			N'; Cần làm: '+cast(format(p.SoLuongChiTietDonHang,'#,#') as nvarchar)
			ThongTinSanXuat,
			N'Công: '+ p.Tencondoan +
			N'; Định mức: '+ cast(Dinhmuc as nvarchar)+
			N'; BQ/Ngày: '+cast(format(SanLuongBQ,'#,#') as nvarchar)NangLucSanXuat,
			n.BatDau,n.KetThuc,
			date01,date02,date03,date04,date05,date06,date07,date08,date09,
			date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
			date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
			date30,date31 from
			(select t.ProDucID,t.DonHangID,t.NguyenCong,t.Tencondoan,t.BatDau,e.KetThuc from
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			Min(BatDau)BatDau from tblCalender_Product
			group by DonHangID,NguyenCong)t
			left outer join
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			max(KetThuc)KetThuc from tblCalender_Product
			group by DonHangID,NguyenCong)e
			on t.ProDucID=e.ProDucID)n
			left outer join  
			(select ProDucID,DinhMucID,DonHangID,Masp,Tencondoan,Dinhmuc,NgayCanLam,
			EpCongSuat,NguyenCong,Sunday,SanLuongBQ,thang,nam,TrienKhai,
			ConLai,date01,date02,date03,date04,date05,date06,date07,date08,date09,
			date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
			date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
			date30,date31,NguoiLap,NgayLap,SoChiTiet,SoLuongChiTietDonHang,TonKhoChiTiet,
			SoLuongDonHang from tblCalender_Product) p
			on p.ProDucID=n.ProDucID
			left outer join 
			(select Iden,madh,Tenquicach,Soluong from tblDHCT) c
			on p.DonHangID=c.Iden where p.NgayLap 
								between '{0}' and '{1}'",
				dpDonHangTKTu.Value.ToString("yyyy-MM-dd"),
				dpDonHangTKDen.Value.ToString("yyyy-MM-dd"));
			gridControl6.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			gridView6.ExpandAllGroups();
		}
		private void DocKeHoachSanXuatNgayTheoDonHang()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"select p.NgayLap,p.NguoiLap,
			p.ProDucID,p.NguyenCong,N'Đơn hàng: '+ c.madh+char(13)+'; '+c.Tenquicach+
			char(13)+N'; '+cast(format(p.SoLuongDonHang,'#,#') as nvarchar)
			+N';Chi tiết'+cast(SoChiTiet as nvarchar)+
			N'; Cần làm: '+cast(format(p.SoLuongChiTietDonHang,'#,#') as nvarchar)
			ThongTinSanXuat,
			N'Công: '+ p.Tencondoan +
			N'; Định mức: '+ cast(Dinhmuc as nvarchar)+
			N'; BQ/Ngày: '+cast(format(SanLuongBQ,'#,#') as nvarchar)NangLucSanXuat,
			n.BatDau,n.KetThuc,
			date01,date02,date03,date04,date05,date06,date07,date08,date09,
			date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
			date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
			date30,date31 from
			(select t.ProDucID,t.DonHangID,t.NguyenCong,t.Tencondoan,t.BatDau,e.KetThuc from
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			Min(BatDau)BatDau from tblCalender_Product
			group by DonHangID,NguyenCong)t
			left outer join
			(select max(ProDucID)ProDucID,DonHangID,NguyenCong,max(Tencondoan)Tencondoan,
			max(KetThuc)KetThuc from tblCalender_Product
			group by DonHangID,NguyenCong)e
			on t.ProDucID=e.ProDucID)n
			left outer join  
			(select ProDucID,DinhMucID,DonHangID,Masp,Tencondoan,Dinhmuc,NgayCanLam,
			EpCongSuat,NguyenCong,Sunday,SanLuongBQ,thang,nam,TrienKhai,
			ConLai,date01,date02,date03,date04,date05,date06,date07,date08,date09,
			date10,date11,date12,date13,date14,date15,date16,date17,date18,date19,
			date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,
			date30,date31,NguoiLap,NgayLap,SoChiTiet,SoLuongChiTietDonHang,TonKhoChiTiet,
			SoLuongDonHang from tblCalender_Product) p
			on p.ProDucID=n.ProDucID
			left outer join 
			(select Iden,madh,Tenquicach,Soluong from tblDHCT) c
			on p.DonHangID=c.Iden where c.madh ='{0}'", txtMadh.Text);
			gridControl6.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			gridView6.ExpandAllGroups();
		}
		private void btnXatKeHoachHangNgay_Click(object sender, EventArgs e)
		{
			gridControl6.ShowPrintPreview();
		}

		private void btnXuatKeHoachSanXuatNgay_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			ketnoi kn = new ketnoi();
			dt = kn.laybang(@"select * from vw_KeHoachNgayCongDoanCuoi where madh= N'" + txtMadh.Text + "' order by KetThuc ASC");
			XRLichSanXuaTheoNgay rpKiemHang = new XRLichSanXuaTheoNgay();
			rpKiemHang.DataSource = dt;
			rpKiemHang.DataMember = "Table";
			rpKiemHang.CreateDocument(false);
			rpKiemHang.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMadh.Text;
			PrintTool tool = new PrintTool(rpKiemHang.PrintingSystem);
			tool.ShowPreviewDialog();
			kn.dongketnoi();
		}
		private void gridControl3_Click(object sender, EventArgs e)
		{
			string point = "";
			point = gridView3.GetFocusedDisplayText();
			txtProDucID.Text = gridView3.GetFocusedRowCellDisplayText(ProDucID_col3);
		}
		private void btnXoaChiTiet_Click(object sender, EventArgs e)
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"delete from tblCalender_Product
				where ProDucID='{0}'", txtProDucID.Text);
			int kq = kn.xulydulieu(sqlStr);
			kn.dongketnoi();
			DocDSCalenderProductTheoID();
		}

		//Sau khi chia lịch sản xuất chi tiết nguyên công 
		//thì người dùng cập nhật lịch sản xuất chi tiết đến tổ sản xuất
		private void CapNhatLichSXChiTiet_KeHoachTrienKhai()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"insert into tblchitietkehoach
				  (IdPSX,SPLR,
				   SLSPLR,Donvisp,
				   Maubv,nvkd,
				   ngaytrienkhai,madh,
				   mabv,sanpham,
				   NguyenCong,Ten_ct,
				   So_CT,soluongyc,
				   tonkho,soluongsx,
				   ngoaiquang,donvi,
				   daystar,dayend,
				   khachhang,xeploai,
				   Ghichu,Trangthai)
			select DonHangID,Tenquicach,
					SoLuongDonHang,c.dvt,
					Mau_banve,c.nguoithaydoi,
					NgayLap,c.madh,
					p.Masp,Tencondoan+p.ChiTietSanPham ChiTietSanPham,
					NguyenCong,p.ChiTietSanPham,
					p.SoChiTiet,p.SoLuongDonHang,
					p.TonKhoChiTiet,p.SoLuongChiTietDonHang,
					c.ngoaiquang,c.dvt,
					p.BatDau,p.KetThuc,
					c.Khachhang,c.PhanloaiKH,
					c.ghichu,c.Trangthai
								 from LichSanXuatChiTiet_func_group_nguyencong('{0}') p 
								 left outer join tblDHCT c
								 on p.DonHangID=c.Iden
			   where p.DonHangID='{0}'", txtDonHangID.Text);
			int kq = kn.xulydulieu(sqlStr);
			kn.dongketnoi();
		}
		//Sau khi update chi tiết sản phẩm, 
		//người dùng update ngày sản xuất cuối cho các tổ sản xuất
		private void UpdatePhanChiaDonHangChoToSanXuat()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"update tblchitietkehoach set KetThucTo1=dap,KetThucTo8=han,
				KetThucTo9=mai,KetThucTo10=std,
				KetThucTo11=dgo,KetThucTo12=tie,KetThucTo14=bul,KetThucTo18=dao,KetThucTo19=rob
				from (select IDSP,IdPSX,NguyenCong,
				case when NguyenCong='DAP' then dayend end dap,
				case when NguyenCong='HAN' then dayend end han,
				case when NguyenCong='MAI' then dayend end mai,
				case when NguyenCong='STD' then dayend end std,
				case when NguyenCong='DGO' then dayend end dgo,
				case when NguyenCong='TIE' then dayend end tie,
				case when NguyenCong='BUL' then dayend end bul,
				case when NguyenCong='DAO' then dayend end dao,
				case when NguyenCong='ROB' then dayend end rob,
				case when NguyenCong='DUC' then dayend end duc
				from tblchitietkehoach where IdPSX='{0}')g
				where tblchitietkehoach.IDSP=g.IDSP", txtDonHangID.Text);
			int kq = kn.xulydulieu(sqlStr);
			kn.dongketnoi();
		}
		private void btnPhanChiaDonHangQuaToSanXuat_Click(object sender, EventArgs e)
		{
			CapNhatLichSXChiTiet_KeHoachTrienKhai();
			UpdatePhanChiaDonHangChoToSanXuat();
		}

		private void btnExportDonHang_Click(object sender, EventArgs e)
		{
			gridControl1.ShowPrintPreview();
		}
		#region BinDing ID TuyChon nhận sự kiện double dể xóa đi những chọn lựa phân tích không cần thiết 
		private void gridControl2_Click(object sender, EventArgs e)
		{
			string point = "";
			point = gridView2.GetFocusedDisplayText();
			txtPointSave.Text = gridView2.GetFocusedRowCellDisplayText(idselect_col2);
		}
		#endregion
		private void XoaLuaChonKhongCanThiet()
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"delete from ");
			gridControl2.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
		}

		private void btnReadAllCaCalenderTemp_Click(object sender, EventArgs e)
		{
			DocTatCaCongSuatSanXuat();
		}

		private void gridControl2_MouseMove(object sender, MouseEventArgs e)
		{
			if (gridView2.SelectedRowsCount > 1)
			{
				btnPhanBoTuyChon.Enabled = true;
				btnCapNhatDinhMucDonHang.Enabled =false;
			}
			else
			{
				btnCapNhatDinhMucDonHang.Enabled = true;
				btnPhanBoTuyChon.Enabled = false;
			}
		}

		private void btnExportCongSuatCalender_Click(object sender, EventArgs e)
		{
			gridControl2.ShowPrintPreview();
		}

		private void gridControl2_DoubleClick(object sender, EventArgs e)
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"delete from tblCalender_Product_Temp where IDChon like '{0}'",
				txtPointSave.Text);
			gridControl2.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			DocTatCaCongSuatSanXuat();
		}

		private void btnDeleteAllTemp_Click(object sender, EventArgs e)
		{
			ketnoi kn = new ketnoi();
			string sqlStr = string.Format(@"truncate table tblCalender_Product_Temp",
				txtPointSave.Text);
			gridControl2.DataSource = kn.laybang(sqlStr);
			kn.dongketnoi();
			DocTatCaCongSuatSanXuat();
		}
	}
}
