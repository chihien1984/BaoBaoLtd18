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
    public partial class ApDinhMucChiTietSanPham_UC : DevExpress.XtraEditors.XtraForm
    {
        public ApDinhMucChiTietSanPham_UC()
        {
            InitializeComponent();
        }
        private string username;
        #region formLoad
        private void frmChiTietSanPham_Load(object sender, EventArgs e)
        {
            username = Login.Username;
            DocChiTietSanPhamTheoMa();
            QuyenCapNhatDinhMucQuaChiTiet();
            DocMaNguyenCong();
            DocMaNguyenCongChiTietSanPham();
            DocChiTietSanPhamTheoMaSanPham();
            DocDSSanPham();
            TaoCongDoanMoi();
            this.grChiTietSanPham.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.grDinhMucCongDoan.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.grDinhMucChiTietSanPham.Appearance.Row.Font = new Font("Times New Roman", 7f);
            DocToThucHienNguyenCongSanXuat();
            DocToThucHienDinhMucCongChiTiet();
        }
        #endregion
        private void QuyenCapNhatDinhMucQuaChiTiet()
        {
            if (Login.role == "1")
            {
                btnDowAllResourceToDetaiResource.Enabled = true;
            }
            else
            {
                btnDowAllResourceToDetaiResource.Enabled = false;
            }
        }
        private void DocChiTietSanPhamTheoMaSanPham()
        {
            repositoryItemCBChiTietSanPham.Items.Clear();
            repositoryItemCBChiTietSanPham.NullText = @"Chitiết";
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select t.Masp,case when p.Ten_ct is null 
                then t.Tensp else p.Ten_ct end Tensp,
                case when p.Soluong_CT is null then 1 else p.Soluong_CT  end Soluong_CT,
				case when p.Chatlieu_chitiet is null 
				then t.Vatlieu else p.Chatlieu_chitiet end Chatlieu_chitiet,ChiTetSPID,Code
				from (select Code,s.Tensp,Vatlieu,
				case when s.Masp is null then c.Masp else s.Masp end Masp
                from tblSANPHAM s
                full outer join
                (select Masp from tblSANPHAM_CT
                group by Masp)c
                on s.Masp=c.Masp)t left outer join
                tblSANPHAM_CT p on
                t.Masp=p.Masp
                where t.Masp like N'{0}'
				order by ChiTetSPID DESC",txtMaSanPham.Text);
            var dt = kn.laybang(sqlStr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemCBChiTietSanPham.Items.Add(dt.Rows[i]["Tensp"]);
            }
            kn.dongketnoi();
        }
        private void DocMaNguyenCong()
        {
            repositoryItemMaNguyenCong.Items.Clear();
            repositoryItemMaNguyenCong.NullText = @"Nguyên công";
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select Ten_Nguonluc,Ma_Nguonluc from tblResources");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemMaNguyenCong.Items.Add(dt.Rows[i]["Ma_Nguonluc"]);
            }
            kn.dongketnoi();
        }
        private void DocMaNguyenCongChiTietSanPham()
        {
            repositoryItemMaCong2.Items.Clear();
            repositoryItemMaCong2.NullText = @"Nguyên công";
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select Ten_Nguonluc,Ma_Nguonluc from tblResources");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemMaCong2.Items.Add(dt.Rows[i]["Ma_Nguonluc"]);
            }
            kn.dongketnoi();
        }
        #region
        private void DocToThucHienNguyenCongSanXuat()
        {
            repositoryItemcbToThucHien.Items.Clear();
            repositoryItemCBChiTietSanPham.NullText = @"Tổ thực hiện";
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select distinct Tenpb  from Admin where GiaoNhan = 1");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemcbToThucHien.Items.Add(dt.Rows[i]["Tenpb"]);
            }
            kn.dongketnoi();
        }
        private void DocToThucHienDinhMucCongChiTiet()
        {
            repositoryItemCBToThucHienDMCong.Items.Clear();
            repositoryItemCBChiTietSanPham.NullText = @"Tổ thực hiện";
            ketnoi kn = new ketnoi();
            var dt = kn.laybang(@"select Tenpb from Admin where GiaoNhan = 1");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                repositoryItemCBToThucHienDMCong.Items.Add(dt.Rows[i]["Tenpb"]);
            }
            kn.dongketnoi();
        }

        #endregion
        #region Đọc tất cả chi tiết sản phẩm trường hợp không có chi tiết thì sản phẩm se là chi tiết
        private void DocTatCaChiTietSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = 
                string.Format(@"select case when MaLapGhep is null then 'LT' else MaLapGhep end MaLapGhep,
                t.Masp,case when p.Ten_ct is null 
                then t.Tensp else p.Ten_ct end Tensp,
                case when p.Soluong_CT is null then 1 else p.Soluong_CT  end Soluong_CT,
				case when p.Chatlieu_chitiet is null 
				then t.Vatlieu else p.Chatlieu_chitiet end Chatlieu_chitiet,
				case when p.ChiTetSPID is null then 'sp'+cast(Code as nvarchar)
				else cast(p.ChiTetSPID as nvarchar) end ChiTetSPID
				from (select Code,s.Tensp,Vatlieu, 
				case when s.Masp is null then c.Masp else s.Masp end Masp
                from tblSANPHAM s
                full outer join
                (select Masp from tblSANPHAM_CT
                group by Masp)c
                on s.Masp=c.Masp)t left outer join
                tblSANPHAM_CT p on
                t.Masp=p.Masp order by ChiTetSPID DESC");
            grcChiTietSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        #endregion
        private void DocChiTietSanPhamTheoMa()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select case when MaLapGhep is null then 'LT' else MaLapGhep end MaLapGhep,
                t.Masp,case when p.Ten_ct is null 
                then t.Tensp else p.Ten_ct end Tensp,
                case when p.Soluong_CT is null then 1 else p.Soluong_CT  end Soluong_CT,
				case when p.Chatlieu_chitiet is null 
				then t.Vatlieu else p.Chatlieu_chitiet end Chatlieu_chitiet,ChiTetSPID,Code
				from (select Code,s.Tensp,Vatlieu,
				case when s.Masp is null then c.Masp else s.Masp end Masp
                from tblSANPHAM s
                full outer join
                (select Masp from tblSANPHAM_CT
                group by Masp)c
                on s.Masp=c.Masp)t left outer join
                tblSANPHAM_CT p on
                t.Masp=p.Masp
                where t.Masp like '{0}'
				order by ChiTetSPID DESC", txtMaSanPham.Text);
            grcChiTietSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        private void DocDSDinhMucCongDoan()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select id,Tencondoan,Dinhmuc,
                Tothuchien,NguyenCong,ThuTuCongDoan,
                SoChiTiet
                from tblDMuc_LaoDong where
                Tencondoan <>'' and Dinhmuc <>''");
            grcDinhMucCongDoan.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void DocDSDinhMucCongDoanTheoMa()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select id,Tencondoan,Dinhmuc,
                Tothuchien,NguyenCong,ThuTuCongDoan,
                SoChiTiet
                from tblDMuc_LaoDong where
                Tencondoan <>'' and Dinhmuc <>'' and Masp like '{0}'", txtMaSanPham.Text);
            grcDinhMucCongDoan.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void DocDSDinhMucChiTietSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT MaLapGhep,ToThucHien,ID,Masp,Tensp,
                IDCongDoan,SanPhamID,
                DinhMuc,Nguoilap,Ngaylap,ChiTietSanPham,
                Tencondoan,NguyenCong,SoChiTiet,
                SoChiTietSanPham,ThuTuCongDoan,ToThucHien
                FROM ChiTietSanPhamDinhMuc where Masp <> ''");
            grcDinhMucChiTietSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void DocDSSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select Code SanPhamID,Masp,Tensp,
				case when l.MaSanPham <>'' then 'x' end  CoDinhMuc
				from tblSANPHAM
				left outer join
				(select Masp MaSanPham from tblDMuc_LaoDong group by Masp) l on
				l.MaSanPham=tblSANPHAM.Masp order by Code desc");
            grlSanPhamDS.Properties.DataSource = kn.laybang(sqlStr);
            grlSanPhamDS.Properties.DisplayMember = "Masp";
            grlSanPhamDS.Properties.ValueMember = "Masp";
            kn.dongketnoi();
        }
        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtChiTietSanPham.Text=="")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");return ;
            }
            else
            {
            int[] listRowList = this.grDinhMucCongDoan.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.grDinhMucCongDoan.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into ChiTietSanPhamDinhMuc 
                    (Masp,ChiTietSanPham,SoChiTietSanPham,
                    NguyenCong,Tencondoan,DinhMuc,
                    SoChiTiet,ThuTuCongDoan,
                    Nguoilap,Ngaylap,IDCongDoan,SanPhamID,Tensp,ToThucHien,MaLapGhep)
                    values (N'{0}',N'{1}','{2}',
                    N'{3}',N'{4}','{5}',
                    '{6}','{7}',
                    N'{8}',N'{9}','{10}','{11}',N'{12}',N'{13}',N'{14}')",
                    txtMaSanPham.Text, txtChiTietSanPham.Text, txtSoChiTiet.Text,
                    rowData["NguyenCong"], 
                    rowData["Tencondoan"],
                    rowData["Dinhmuc"],
                    rowData["SoChiTiet"],
                    rowData["ThuTuCongDoan"],
                    username, 
                    dpNgayGhi.Value.ToString("MM-dd-yyyy"), 
                    rowData["id"],
                    txtSanPhanID.Text,
                    txtSanPham.Text,
                    rowData["ToThucHien"],
                    txtMaLapGhep.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                DocDSChiTietSanPhamDinhMucTheoMa();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo"); return;
            }
            else
            {
                int[] listRowList = this.grDinhMucChiTietSanPham.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.grDinhMucChiTietSanPham.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update ChiTietSanPhamDinhMuc 
                    set SoChiTiet ='{0}',
                    NguyenCong= N'{1}',
                    Tencondoan= N'{2}',
                    DinhMuc='{3}',
                    SoChiTietSanPham = '{4}',
                    ThuTuCongDoan = '{5}',
                    Nguoilap = N'{6}',
                    Ngaylap = N'{7}',
                    ChiTietSanPham = N'{8}',ToThucHien = N'{9}',MaLapGhep=N'{10}'
                    where ID like {11}",
                    rowData["SoChiTiet"],
                    rowData["NguyenCong"], 
                    rowData["Tencondoan"],
                    rowData["DinhMuc"],
                    rowData["SoChiTietSanPham"],
                    rowData["ThuTuCongDoan"],
                    username,
                    dpNgayGhi.Value.ToString("MM-dd-yyyy"),
                    rowData["ChiTietSanPham"],
                    rowData["ToThucHien"],
                    rowData["MaLapGhep"],
                    rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSChiTietSanPhamDinhMucTheoMa();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           int[] listRowList = this.grDinhMucChiTietSanPham.GetSelectedRows();
           SqlConnection con = new SqlConnection();
           con.ConnectionString = Connect.mConnect;
           if (con.State == ConnectionState.Closed)
               con.Open();
           DataRow rowData;
           for (int i = 0; i < listRowList.Length; i++)
           {
               rowData = this.grDinhMucChiTietSanPham.GetDataRow(listRowList[i]);
               string strQuery = string.Format(@"delete from ChiTietSanPhamDinhMuc where ID like '{0}'",
                rowData["ID"]);
               SqlCommand cmd = new SqlCommand(strQuery, con);
               cmd.ExecuteNonQuery();
           }
           con.Close();
           DocDSChiTietSanPhamDinhMucTheoMa();
         
        }

        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoMoiDSDinhMucCongDoan_Click(object sender, EventArgs e)
        {
            TaoCongDoanMoi();
        }
        private void TaoCongDoanMoi()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select top 0 id,Tencondoan,Dinhmuc,
                Tothuchien,NguyenCong,ThuTuCongDoan,
                SoChiTiet
                from tblDMuc_LaoDong where
                Tencondoan <>'' and Dinhmuc <>''");
            grcDinhMucCongDoan.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void btnDocDSChiTietSanPham_Click(object sender, EventArgs e)
        {
            DocTatCaChiTietSanPham();
        }
        //Bindin Sản phẩm và chi tiết sản phẩm
        private void grcChiTietSanPham_Click(object sender, EventArgs e)
        {
            string point = "";
            point = grChiTietSanPham.GetFocusedDisplayText();
            txtChiTietSanPham.Text = grChiTietSanPham.GetFocusedRowCellDisplayText(chitietsanpham_grchitiet);
            txtChiTietID.Text = grChiTietSanPham.GetFocusedRowCellDisplayText(chitietID_grchitiet);
            txtSoChiTiet.Text = grChiTietSanPham.GetFocusedRowCellDisplayText(sochitiet_grchitiet);
            txtChatLieuChiTiet.Text = grChiTietSanPham.GetFocusedRowCellDisplayText(vatlieu_grchitiet);
            txtMaLapGhep.Text= grChiTietSanPham.GetFocusedRowCellDisplayText(malapghep_grchitiet);
            DocDSChiTietSanPhamDinhMucTheoMa();
            DocCongSuatChuyenTheoMaSanPham();
        
        }

        private void grlSanPhamDS_EditValueChanged(object sender, EventArgs e)
        {
            BinDingMaSanPham();
        }
        private void BinDingMaSanPham()
        {
            string point = "";
            point = grlDSSanPham.GetFocusedDisplayText();
            txtMaSanPham.Text = grlDSSanPham.GetFocusedRowCellDisplayText(MaSanPhamgridLook);
            txtSanPhanID.Text = grlDSSanPham.GetFocusedRowCellDisplayText(SanPhamIDgridLook);
            txtSanPham.Text= grlDSSanPham.GetFocusedRowCellDisplayText(TenSanPhamgridLook);
            DocChiTietSanPhamTheoMa();
            DocDSDinhMucCongDoanTheoMa();
            DocDSChiTietSanPhamDinhMucTheoMa();
            DocCongSuatChuyenTheoMaSanPham();
        }

        private void grlSanPhamDS_Click(object sender, EventArgs e)
        {
            BinDingMaSanPham();
        }

        private void btnChiTietSanPhamDinhMuc_Click(object sender, EventArgs e)
        {
            DocDSChiTietSanPhamDinhMuc();           
        }
        private void DocDSChiTietSanPhamDinhMuc()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT MaLapGhep,ToThucHien,ID,Masp,Tensp,
                IDCongDoan,SanPhamID,
                DinhMuc,Nguoilap,Ngaylap,ChiTietSanPham,
                Tencondoan,NguyenCong,SoChiTiet,
                SoChiTietSanPham,ThuTuCongDoan 
                FROM ChiTietSanPhamDinhMuc where Masp <> '' order by Ngayghi DESC,Masp ASC, ThuTuCongDoan ASC");
            grcDinhMucChiTietSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void DocDSChiTietSanPhamDinhMucTheoMa()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"SELECT MaLapGhep,ToThucHien,ID,Masp,Tensp,
                IDCongDoan,SanPhamID,
                DinhMuc,Nguoilap,Ngaylap,ChiTietSanPham,
                Tencondoan,NguyenCong,SoChiTiet,
                SoChiTietSanPham,ThuTuCongDoan 
                FROM ChiTietSanPhamDinhMuc where Masp = N'{0}' and Masp <> '' 
                order by Ngayghi DESC,Masp ASC, ThuTuCongDoan ASC",
                txtMaSanPham.Text);
            grcDinhMucChiTietSanPham.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
            grDinhMucChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
       

        private void btnDowAllResourceToDetaiResource_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"insert into ChiTietSanPhamDinhMuc
                (Macongdoan,SanPhamID,Masp,Tensp,ChiTietSanPham,SoChiTietSanPham,Tencondoan,
                Dinhmuc,Tothuchien,BoPhanThucHien,Macv,NguyenCong,Soluong,Nguoilap,Ngaylap,
                Ngayghi,Xacnhan,Dongia_CongDoan,Trangthai,
                DonGia_ApDung,DonGiaThuongPhanBo,NgayApDung,HeSoDinhMuc,
                NguoiHC_CV,NgayHC_CV,NguoiGhiDGThuong,NgayGhiDGThuong,
                TrungCongDoan,SoChiTiet,PhuKienSanPham,SoLuongPhuKien,
                NgayTaoPhuKien,NguoiTaoPhuKien,ThuTuCongDoan,IDCongDoan)
                SELECT Macongdoan,SanPhamID,Masp,
                Tensp,ChiTietSanPham,SoChiTietSanPham,Tencondoan,
                Dinhmuc,Tothuchien,BoPhanThucHien,Macv,
                NguyenCong,Soluong,Nguoilap,Ngaylap,
                Ngayghi,Xacnhan,Dongia_CongDoan,Trangthai,
                DonGia_ApDung,DonGiaThuongPhanBo,NgayApDung,HeSoDinhMuc,
                NguoiHC_CV,NgayHC_CV,NguoiGhiDGThuong,NgayGhiDGThuong,
                TrungCongDoan,SoChiTiet,PhuKienSanPham,SoLuongPhuKien,
                NgayTaoPhuKien,NguoiTaoPhuKien,ThuTuCongDoan,id
                FROM tblDMuc_LaoDong
                where tblDMuc_LaoDong.Masp 
                not in (select Masp from ChiTietSanPhamDinhMuc 
                where Dinhmuc>1 group by Masp)");
            var kq = kn.xulydulieu(sqlStr);
            kn.dongketnoi();
            if ((int)kq>1)
            {
                MessageBox.Show("Success","Message");
            }
            else
            {
                MessageBox.Show("Erorr", "Message");
            }
        }

        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {
            DocChiTietSanPhamTheoMaSanPham();
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void btnDocCongSuatChuyen_Click(object sender, EventArgs e)
        {
            DocTatCongSuatChuyen();
        }

        private void DocTatCongSuatChuyen()
        {
            ketnoi kn = new ketnoi();
            grCongSuatChuyen.DataSource = kn.laybang(@"select max(id)id,Masp,
				max(ThuTuCongDoan)ThuTuCongDoan,max(ChiTietSanPham)ChiTietSanPham,
				max(CongSuatChuyen)CongSuatChuyen,NguyenCong 
				from ChiTietSanPhamDinhMuc where 
				SoChiTietSanPham >0 
				and ThuTuCongDoan >0 and NguyenCong<>''
				group by NguyenCong,Masp order by ThuTuCongDoan ASC");
            kn.dongketnoi();
            gvCongSuatChuyen.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void DocCongSuatChuyenTheoMaSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select max(id)id,Masp,
				max(ThuTuCongDoan)ThuTuCongDoan,max(ChiTietSanPham)ChiTietSanPham,
				max(CongSuatChuyen)CongSuatChuyen,NguyenCong  
				from ChiTietSanPhamDinhMuc where 
				SoChiTietSanPham >0 
				and ThuTuCongDoan >0 and NguyenCong<>''  and Masp like N'{0}'
				group by NguyenCong,Masp order by ThuTuCongDoan ASC", txtMaSanPham.Text);
            grCongSuatChuyen.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvCongSuatChuyen.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }

        private void btnGhiDinhMucChuyen_Click(object sender, EventArgs e)
        {
            int[] listRowList = this.gvCongSuatChuyen.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvCongSuatChuyen.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"update ChiTietSanPhamDinhMuc set CongSuatChuyen ='{0}' 
				 where id like '{1}'",rowData["CongSuatChuyen"],
                 rowData["id"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocDSChiTietSanPhamDinhMucTheoMa();
        }

        private void grcDinhMucCongDoan_MouseMove(object sender, MouseEventArgs e)
        {
            HidenSaveNguyenCong();
        }
        private void HidenSaveNguyenCong()
        {
            if (grDinhMucCongDoan.GetSelectedRows().Count()>=1)
            {
                btnGhi.Enabled = true;
            }
            else
            {
                btnGhi.Enabled = false;
            }
        }
        private void grcChiTietSanPham_MouseMove(object sender, MouseEventArgs e)
        {
            HidenSavePhuKienBBA();
        }
        private void HidenSavePhuKienBBA()
        {
            if (grChiTietSanPham.GetSelectedRows().Count() >= 1)
            {
                btnGhiPhuKien.Enabled = true;
            }
            else
            {
                btnGhiPhuKien.Enabled = false;
            }
        }

        private void btnGhiPhuKien_Click(object sender, EventArgs e)
        {
            if (txtChiTietSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo"); return;
            }
            else
            {
                int[] listRowList = this.grChiTietSanPham.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.grChiTietSanPham.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into ChiTietSanPhamDinhMuc
                    (Masp,Tensp,ChiTietSanPham,
                     SoChiTietSanPham,Nguoilap,Ngaylap,SanPhamID,MaLapGhep)
                    values (N'{0}',N'{1}',N'{2}',N'{3}',
                    N'{4}','{5}','{6}','{7}')",
                    txtMaSanPham.Text,txtSanPham.Text, txtChiTietSanPham.Text, txtSoChiTiet.Text,
                    username,
                    dpNgayGhi.Value.ToString("MM-dd-yyyy"),
                    txtSanPhanID.Text,rowData["MaLapGhep"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                grDinhMucCongDoan.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                DocDSChiTietSanPhamDinhMucTheoMa();
            }
        }

        private void ckCheckPhuKien_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCheckPhuKien.Checked == true)
            {
                grChiTietSanPham.OptionsSelection.MultiSelectMode
                    = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            }
            else if (ckCheckPhuKien.Checked == false)
            {
                grChiTietSanPham.OptionsSelection.MultiSelectMode
                  = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            }
        }

        private void btnExportCongDoanSanPham_Click(object sender, EventArgs e)
        {
            grDinhMucChiTietSanPham.OptionsSelection.MultiSelectMode
                 = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grDinhMucChiTietSanPham.ShowPrintPreview();
        }

        private void btnExportCongSuatChuyen_Click(object sender, EventArgs e)
        {
            gvCongSuatChuyen.OptionsSelection.MultiSelectMode
                = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grCongSuatChuyen.ShowPrintPreview();
        }

        private void btnThemMaCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
            DocMaNguyenCong();
        }
    }
}
