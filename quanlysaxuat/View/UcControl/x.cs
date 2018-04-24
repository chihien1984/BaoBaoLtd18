using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace quanlysanxuat
{
    public partial class UCDMSanPham_CT : DevExpress.XtraEditors.XtraForm
    {
        public UCDMSanPham_CT()
        {
            InitializeComponent();
        }
        Clsketnoi knn = new Clsketnoi();

        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        private void DocDSSanPham()
        {
            ketnoi kn = new ketnoi();
            grcSanPham.DataSource = kn.laybang(
          @"select Code,case when t.Masp<>'' then 'x' end CoChiTiet,
            case when g.IDSanPham<>'' then 'x' end CoCum,
			s.Masp,Tensp,Kichthuoc,Vatlieu, 
            Dacdiem, Ngaylap, KH.MKH, KH.TenKH, s.manv,
            hotennv from tblSANPHAM s 
			left outer join tblKHACHHANG KH 
            on s.Makh = KH.MKH left outer join
			(select Masp from tblSANPHAM_CT group by Masp)t
			on s.Masp=t.Masp
			left outer join
			(select IDSanPham from tblSanPhamGroup group by IDSanPham)g
			on g.IDSanPham=s.Code 
			order by Code DESC");
            kn.dongketnoi();
        }

        private void DocDSChiTietSanPhamTheoMa()
        {
            ketnoi kn = new ketnoi();
            string strSql = string.Format(@"select DonVi,MaLapGhep,MaCum,TenCum,SoLuongCum,
                                  Code SanPhamID,ChiTetSPID,CT.Masp,SP.Tensp,Mact,Ten_ct, 
                                  Soluong_CT, Chatlieu_chitiet, Ngaycapnhat, 
                                  CT.hotennv  from tblSANPHAM_CT CT, tblSANPHAM SP 
                                  where CT.Masp = SP.Masp and CT.Masp like N'{0}' 
                                  order by Code DESC, ChiTetSPID ASC", txtMaSanPham.Text);
            grChiTietSanPham.DataSource = kn.laybang(strSql);
            kn.dongketnoi();
            this.gvChiTietSanPham.OptionsSelection.MultiSelectMode =
            DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        private void DocTatCaChiTietSanPham()
        {
            ketnoi kn = new ketnoi();
            grChiTietSanPham.DataSource = kn.laybang(@"select DonVi,MaLapGhep,MaCum,TenCum,SoLuongCum,
             Code SanPhamID,ChiTetSPID,c.Masp,s.Tensp,Mact,Ten_ct, 
             Soluong_CT, Chatlieu_chitiet, Ngaycapnhat, 
             c.hotennv  from tblSANPHAM_CT c inner join
             tblSANPHAM s
             on c.Masp = s.Masp 
             order by Code DESC, ChiTetSPID ASC");
            kn.dongketnoi();
            gvChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gvChiTietSanPham.OptionsSelection.MultiSelectMode =
            DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        #region formload
        private void UCDMSanPham_CT_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpDen.Text = DateTime.Now.ToString();
            this.grSanPham.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.grChiTiet.Appearance.Row.Font = new Font("Times New Roman", 7f);
            this.gvChiTietSanPham.Appearance.Row.Font = new Font("Times New Roman", 7f);
            DocDSSanPham();
            DocTatCaChiTietSanPham();
            DocDSSanPhamVaoGridLooKup();
            TaoMoiChiTiet();
            grChiTiet.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            TaoMoiCongDoan();
            ShowMetail();
        }
        #endregion
        private void ShowMetail()
        {
            repositoryItemComboBoxVatLieu.Items.Clear();
            ketnoi kn = new ketnoi();
            string sqlQuery = 
                string.Format(@"select TenVatlieu from tblVatLieuSanPham");
            var data = kn.laybang(sqlQuery);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                repositoryItemComboBoxVatLieu.Items.Add(data.Rows[i]["TenVatlieu"]);
            }
            kn.dongketnoi();
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {
            DocDSSanPham();
        }


        private void btnfresh_Click(object sender, EventArgs e)
        {
            DocDSChiTietSanPhamTheoMa();
            UCDMSanPham_CT_Load(sender, e);
        }


        #region Kiểm tra trùng mã chi tiết
        private bool kiemtramachitiet()
        {
            bool tatkt = false;
            string MaSP = txtMaSanPham.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select Mact from tblSANPHAM_CT", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (MaSP == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        #endregion

        private void btnthem_Click(object sender, EventArgs e)
        {
            //if (txtMasp_CT.Text == "" && txtTen_CT.Text == "")
            //{
            //    MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (kiemtramachitiet())
            //{
            //    MessageBox.Show("Mã '" + txtMasp_CT.Text + "' đã tồn tại, Không thể thêm mã CT trùng");
            //}
            //else
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = new SqlCommand("insert into tblSANPHAM_CT "
            //            + " (Ma_nhom_sp, Masp, Mact, Ten_ct, Soluong_CT, Chatlieu_chitiet, Ngaycapnhat,Manv,hotennv) "
            //            + " values(@Ma_nhom_sp, @Masp, @Mact, @Ten_ct, @Soluong_CT, @Chatlieu_chitiet, GetDate(),@Manv,@hotennv)", con))
            //        {
            //            cmd.Parameters.Add("@Ma_nhom_sp", SqlDbType.NVarChar).Value = txtmanhom.Text;
            //            cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
            //            cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
            //            cmd.Parameters.Add("@Ten_ct", SqlDbType.NVarChar).Value = txtTen_CT.Text;
            //            cmd.Parameters.Add("@Soluong_CT", SqlDbType.Int).Value = txtsoluong_CT.Text;
            //            cmd.Parameters.Add("@Chatlieu_chitiet", SqlDbType.NVarChar).Value = txtChatlieu_CT.Text;
            //            cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtmanv_lapCT.Text;
            //            cmd.Parameters.Add("@hotennv", SqlDbType.NVarChar).Value = txttennv_lapCT.Text;
            //            cmd.ExecuteNonQuery();
            //        }
            //        txtmasp_TextChanged(sender,e); con.Close();
            //    }
            //}
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = Connect.mConnect;
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //    using (SqlCommand cmd = new SqlCommand("update tblSANPHAM_CT set Ma_nhom_sp=@Ma_nhom_sp, "
            //           +" Masp = @Masp, Mact = @Mact, Ten_ct = @Ten_ct, "
            //           +" Soluong_CT = @Soluong_CT, Chatlieu_chitiet = @Chatlieu_chitiet, "
            //           + " Ngaycapnhat = GetDate(),Manv=@Manv,hotennv=@hotennv where Mact like N'"+ txtMasp_CT.Text+ "'", con))
            //    {
            //        cmd.Parameters.Add("@Ma_nhom_sp", SqlDbType.NVarChar).Value = txtmanhom.Text;
            //        cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
            //        cmd.Parameters.Add("@Mact", SqlDbType.NVarChar).Value = txtMasp_CT.Text;
            //        cmd.Parameters.Add("@Ten_ct", SqlDbType.NVarChar).Value = txtTen_CT.Text;
            //        cmd.Parameters.Add("@Soluong_CT", SqlDbType.Int).Value = txtsoluong_CT.Text;
            //        cmd.Parameters.Add("@Chatlieu_chitiet", SqlDbType.NVarChar).Value = txtChatlieu_CT.Text;
            //        cmd.Parameters.Add("@Manv", SqlDbType.NVarChar).Value = txtmanv_lapCT.Text;
            //        cmd.Parameters.Add("@hotennv", SqlDbType.NVarChar).Value = txttennv_lapCT.Text;
            //        cmd.ExecuteNonQuery();
            //    }
            //    ketnoi kn = new ketnoi();DocDSChiTietSanPhamTheoMa();
            //    //gridControl2.DataSource = kn.laybang(" select Ma_nhom_sp,Masp,Mact,Ten_ct, "
            //    //                                    +" Soluong_CT, Chatlieu_chitiet, Ngaycapnhat, "
            //    //                                    +" Manv, hotennv from tblSANPHAM_CT where Masp like N'"+txtmasp_changer.Text+"'");
            //    con.Close();
            //}          
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            //if (txtMasp_CT.Text != "" && MessageBox.Show("Bạn muốn xoa chi tiết " + txtMasp_CT.Text + " có tên" + txttensp.Text + " ", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    ketnoi kn = new ketnoi();
            //    gridControl2.DataSource = kn.xulydulieu("Delete tblSANPHAM_CT where Mact like N'" + txtMasp_CT.Text + "'");
            //}
            //DocDSChiTietSanPhamTheoMa();
            //ketnoi conn = new ketnoi();
            //gridControl2.DataSource = conn.laybang(" select Ma_nhom_sp,Masp,Mact,Ten_ct, "
            //                                    + " Soluong_CT, Chatlieu_chitiet, Ngaycapnhat, "
            //                                    + " Manv, hotennv from tblSANPHAM_CT where Masp like N'" + txtmasp_changer.Text + "'");
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {

        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            //gridControl2.ShowPrintPreview();
        }

        private void txtsoluong_CT_TextChanged(object sender, EventArgs e)
        {
            //if (txtsoluong_CT.Text=="") txtsoluong_CT.Text ="0" ;
        }

        private void txtmasp_TextChanged(object sender, EventArgs e)//xem các mục chi tiết của từng sản phẩm
        {

            //ketnoi kn = new ketnoi();
            //gridControl2.DataSource = kn.laybang(" select Ma_nhom_sp,Masp,Mact,Ten_ct, "
            //                                    + " Soluong_CT, Chatlieu_chitiet, Ngaycapnhat,"
            //                                    + " Manv, hotennv from tblSANPHAM_CT where Masp like N'" + txtmasp.Text+"'");
        }

        private void show_CTsanpham_Click(object sender, EventArgs e)
        {
            DocTatCaChiTietSanPham();
        }

        private void txtmasp_changer_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXemBV_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            //frmLoading f2 = new frmLoading(txtmasp.Text, txtPath_MaSP.Text);
            //f2.Show();
        }

        private void GhiCum()
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    int[] listRowList = this.gvPhanCum.GetSelectedRows();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gvPhanCum.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@" insert into tblSanPhamGroup 
                         (MaCum,TenCum,SoLuongCum,IDSanPham) 
                         values(N'{0}',N'{1}','{2}','{3}')",
                         rowData["MaCum"], rowData["TenCum"],
                         rowData["SoLuongCum"], txtSanPhanID.Text);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();

                    DocDSChiTietSanPhamTheoMa();
                    DocDSSanPham();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã chi tiết trùng" + ex, "Thông báo");
                }
            }
        }
        private void SuaCum()
        {
            try
            {
                int[] listRowList = this.gvPhanCum.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhanCum.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSanPhamGroup  set 
                         MaCum=N'{0}',TenCum=N'{1}',SoLuongCum='{2}' where ID like '{3}'",
                     rowData["MaCum"],
                     rowData["TenCum"],
                     rowData["SoLuongCum"],
                     rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();

                DocDSChiTietSanPhamTheoMa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Thông báo");
            }
        }
        private void SuaCumTrongChiTiet()
        {
            try
            {
                int[] listRowList = this.gvPhanCum.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhanCum.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSANPHAM_CT set MaCum=g.MaCum,
                        TenCum=g.TenCum,SoLuongCum=g.SoLuongCum 
                        from tblSANPHAM_CT d inner join 
                        (select IDSanPham,MaCum,TenCum,SoLuongCum,ID 
                        from tblSanPhamGroup)g
                        on d.IDSanPhamCum=g.ID
                        where g.IDSanPham like '{0}'",
                     rowData["IDSanPham"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                DocDSChiTietSanPhamTheoMa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Thông báo");
            }
        }

        private void XoaCum()
        {

            try
            {
                int[] listRowList = this.gvPhanCum.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhanCum.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from tblSanPhamGroup where ID like '{0}'",
                     rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                TheHienCumTheoMa();
                DocDSChiTietSanPhamTheoMa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Thông báo");
            }

        }
        private void XoaCumTrongChiTiet()
        {
            try
            {
                int[] listRowList = this.gvPhanCum.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhanCum.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update tblSANPHAM_CT set MaCum='',
                        TenCum='',SoLuongCum='' where IDSanPhamCum like '{0}'",
                     idsanphamcum);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lý do:" + ex, "Thông báo");
            }
        }
        private void btnGhiCum_Click(object sender, EventArgs e)
        {
            GhiCum();
            TheHienCumTheoMa();
        }
        private void btnSuaCum_Click(object sender, EventArgs e)
        {
            SuaCum();
            SuaCumTrongChiTiet();
            TheHienCumTheoMa();
        }

        private void btnXoaCum_Click(object sender, EventArgs e)
        {  XoaCumTrongChiTiet();//Cập nhật cụm bên chi tiết sản phẩm về giá trị null
           XoaCum();
           TheHienCumTheoMa();
        }
        #region 
        private void TheHienCumTheoMa()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from tblSanPhamGroup where IDSanPham like '{0}'",
                txtSanPhanID.Text);
            grPhanCum.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvPhanCum.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void TaoMoiCongDoan()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select top 0 * from tblSanPhamGroup");
            grPhanCum.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();

        }
        #endregion
        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    int[] listRowList = this.grChiTiet.GetSelectedRows();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.grChiTiet.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"insert into tblSANPHAM_CT 
				            (Mact,Masp,Ten_ct,
				            Soluong_CT,Chatlieu_chitiet,
				            Ngaycapnhat,Hotennv,SanPhamID,MaLapGhep,DonVi) 
				            values (N'{0}',N'{1}',N'{2}',
				            N'{3}',N'{4}',
				            N'{5}',N'{6}',N'{7}',N'{8}',N'{9}')",
                                rowData["MaChiTiet"], txtMaSanPham.Text, rowData["TenChiTiet"],
                                rowData["SoLuongChiTiet"],
                                rowData["VatLieuChiTiet"], dpNgayGhi.Value.ToString("MM-dd-yyyy"),
                                Login.Username, txtSanPhanID.Text,
                                rowData["MaLapGhep"], rowData["DonVi"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    gvChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                    DocDSChiTietSanPhamTheoMa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã chi tiết trùng" + ex, "Thông báo");
                }
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    int[] listRowList = this.gvChiTietSanPham.GetSelectedRows();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gvChiTietSanPham.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblSANPHAM_CT set 
				            Mact=N'{0}',Ten_ct=N'{1}',
				            Soluong_CT=N'{2}',Chatlieu_chitiet = N'{3}',
				            Ngaycapnhat=GetDate(),Hotennv = N'{4}',
                            MaLapGhep=N'{5}',DonVi=N'{6}'
                            where ChiTetSPID = N'{7}'",
                                rowData["Mact"], rowData["Ten_ct"],
                                rowData["Soluong_CT"],
                                rowData["Chatlieu_chitiet"],
                                Login.Username, rowData["MaLapGhep"],
                                rowData["DonVi"], rowData["ChiTetSPID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    gvChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                    DocDSChiTietSanPhamTheoMa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã chi tiết trùng" + ex, "Thông báo");
                }
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    int[] listRowList = this.gvChiTietSanPham.GetSelectedRows();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gvChiTietSanPham.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"delete from tblSANPHAM_CT where ChiTetSPID like '{0}'",
                            rowData["ChiTetSPID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    gvChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                    DocDSChiTietSanPhamTheoMa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã chi tiết trùng", "Thông báo");
                }
            }
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, txtPath_MaSP.Text);
            f2.Show();
        }

        private void grcSanPham_Click(object sender, EventArgs e)
        {
            string point = "";
            point = grSanPham.GetFocusedDisplayText();
            txtSanPhanID.Text = grSanPham.GetFocusedRowCellDisplayText(idsanpham_col1);
            txtMaSanPham.Text = grSanPham.GetFocusedRowCellDisplayText(masp_col1);
            txtSanPham.Text = grSanPham.GetFocusedRowCellDisplayText(tensanpham_col1);
            txtChatLieuSanPham.Text = grSanPham.GetFocusedRowCellDisplayText(chatlieu_col1);
            DocDSChiTietSanPhamTheoMa();
            TheHienCumTheoMa();
        }
        private void TaoMoiChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select top 0 DonVi,MaLapGhep,MaChiTiet,
			 TenChiTiet,SoLuongChiTiet,
              VatLieuChiTiet,MaSanPham
			  from TempApMaChiTiet");
            grcChiTiet.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }

        private void DocDSSanPhamVaoGridLooKup()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select c.Masp,s.Tensp from
                (select Masp from tblSANPHAM_CT group by Masp)c
                left outer join
                (select Masp,Tensp from tblSANPHAM)s
                on c.Masp=s.Masp");
            grlSanPham.Properties.DataSource = kn.laybang(sqlStr);
            grlSanPham.Properties.DisplayMember = "Masp";
            grlSanPham.Properties.ValueMember = "Masp";
            kn.dongketnoi();
        }

        private void btnTraCuuSanPhamChiTiet_Click(object sender, EventArgs e)
        {
            DocTatCaChiTietSanPham();
        }

        #region Ghi dữ liệu chi tiết sản phẩm vào bản tạm cập nhật mã chi tiết cho bản tạm gọi bản tạm lên
        private void grlSanPham_EditValueChanged(object sender, EventArgs e)
        {
            //Đọc dữ liệu từ chi tiết sản phẩm lên bảng tạm
            DocSanPhamTuongTuVaoBangTam();
        }

        #endregion
        #region
        private void btnChayMaChiTiet_Click(object sender, EventArgs e)
        {
            //Xóa bảng tạm củ
            XoaBangTam();
            //Ghi dữ liệu từ grChiTiet vào bản tạm
            GhiSanPhamCungLoaiVaoBanTam();
            //Cập nhật mã chi tiết
            ChayMaChiTiet();//Cập nhật mã vào bảng tạm
                            //Đọc lại nội dung file tạm
            DocChiTietCungLoaiTam();//Đọc bảng tạm lên
        }
        private void XoaBangTam()
        {
            ketnoi kn = new ketnoi();
            var kq = kn.xulydulieu(@"truncate table TempApMaChiTiet");
            kn.dongketnoi();
        }
        private void DocSanPhamTuongTuVaoBangTam()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select MaChiTiet='',''DonVi,MaLapGhep,Ten_ct TenChiTiet,Mact,
            Soluong_CT SoLuongChiTiet,Chatlieu_chitiet VatLieuChiTiet
            from tblSANPHAM_CT where Masp=N'{0}'", grlSanPham.Text);
            grcChiTiet.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void ChayMaChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update TempApMaChiTiet set 
                MaChiTiet = MaSanPham+'-'+cast(MaSo as nvarchar) from
                (select ID,row_number() over 
                (partition by MaSanPham order by (select 1))as MaSo
                from TempApMaChiTiet)t where t.ID=TempApMaChiTiet.ID");
            var kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }

        private void GhiSanPhamCungLoaiVaoBanTam()
        {
            if (txtMaSanPham.Text == "") { MessageBox.Show("Chưa chọn mã sản phẩm", "Mess"); return; }
            else
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Connect.mConnect;
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                for (int i = 0; i < grChiTiet.DataRowCount; i++)
                {
                    string sqlQuery = string.Format(@"insert into TempApMaChiTiet 
								  (MaSanPham,TenChiTiet,SoLuongChiTiet,VatLieuChiTiet) 
								  values(N'{0}',N'{1}',N'{2}',N'{3}')",
                                      txtMaSanPham.Text,
                                      grChiTiet.GetRowCellValue(i, "TenChiTiet").ToString(),
                                      grChiTiet.GetRowCellValue(i, "SoLuongChiTiet").ToString(),
                                      grChiTiet.GetRowCellValue(i, "VatLieuChiTiet").ToString());

                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }
        private void DocChiTietCungLoaiTam()
        {
            ketnoi kn = new ketnoi();
            string sqlStr = string.Format(@"select ''DonVi,''MaLapGhep,MaChiTiet,
			 TenChiTiet,SoLuongChiTiet,
              VatLieuChiTiet,MaSanPham
			  from TempApMaChiTiet",
                grlSanPham.Text);
            grcChiTiet.DataSource = kn.laybang(sqlStr);
            kn.dongketnoi();
        }
        #endregion

        private void btnTaoMoiDSDinhMucCongDoan_Click(object sender, EventArgs e)
        {
            TaoMoiChiTiet();
        }

        private void btnTraCuuDSSanPham_Click(object sender, EventArgs e)
        {
            DocDSSanPham();
        }

        private void grChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GhiVaoBangTamSelectChange();
        }



        private void GhiVaoBangTamSelectChange()
        {
            int i = 0;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Connect.mConnect;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            while (i < grChiTiet.DataRowCount)
            {
                string sqlQuery = string.Format(@"insert into TempApMaChiTiet 
								  (TenChiTiet,SoLuongChiTiet,VatLieuChiTiet) 
								  values(N'{0}',N'{1}',N'{2}')",
                                  grChiTiet.GetRowCellValue(i, "TenChiTiet").ToString(),
                                  grChiTiet.GetRowCellValue(i, "SoLuongChiTiet").ToString(),
                                  grChiTiet.GetRowCellValue(i, "VatLieuChiTiet").ToString());
                i++;
                SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                cmd.ExecuteNonQuery();
            }
            cn.Close();
        }

        private void grcChiTiet_MouseMove(object sender, MouseEventArgs e)
        {
            if (grChiTiet.GetSelectedRows().Count() >= 1)
            {
                btnGhi.Enabled = true;
            }
            else
            {
                btnGhi.Enabled = false;
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnCapNhatCum.Enabled = false;
            }
        }

        private void grcChiTietSanPham_MouseMove(object sender, MouseEventArgs e)
        {
            if (gvChiTietSanPham.GetSelectedRows().Count() >= 1)
            {
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnCapNhatCum.Enabled = true;
            }
            else
            {
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
                btnGhi.Enabled = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.gvChiTietSanPham.OptionsSelection.MultiSelectMode =
                DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grChiTietSanPham.ShowPrintPreview();
        }

        private void btnTraCongSuatChuyen_Click(object sender, EventArgs e)
        {

        }
        private void CongSuatChuyen()
        {
            ketnoi kn = new ketnoi();
            grCongSuatChuyen.DataSource = kn.laybang(@"");
            kn.dongketnoi();
        }
        private void btnTaoMoiCum_Click(object sender, EventArgs e)
        {
            TaoMoiCongDoan();
        }
        private string idsanphamcum;
        private void grPhanCum_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvPhanCum.GetFocusedDisplayText();
            txtMaCum.Text = gvPhanCum.GetFocusedRowCellDisplayText(macum_);
            txtTenCum.Text = gvPhanCum.GetFocusedRowCellDisplayText(tencum_);
            txtSoLuongCum.Text = gvPhanCum.GetFocusedRowCellDisplayText(soluongcum_);
            idsanphamcum = gvPhanCum.GetFocusedRowCellDisplayText(idsanphamcum_);
        }

        private void btnCapNhatCum_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Chưa có thông tin cụm", "Thông báo");
                return;
            }
            else
            {
                try
                {
                    int[] listRowList = this.gvChiTietSanPham.GetSelectedRows();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DataRow rowData;
                    for (int i = 0; i < listRowList.Length; i++)
                    {
                        rowData = this.gvChiTietSanPham.GetDataRow(listRowList[i]);
                        string strQuery = string.Format(@"update tblSANPHAM_CT set 
                                MaCum=N'{0}',TenCum=N'{1}',
                                SoLuongCum='{2}',IDSanPhamCum='{3}'
                                where ChiTetSPID like '{4}'",
                                txtMaCum.Text, txtTenCum.Text,
                                txtSoLuongCum.Text, idsanphamcum,
                                rowData["ChiTetSPID"]);
                        SqlCommand cmd = new SqlCommand(strQuery, con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    gvChiTietSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
                    DocDSChiTietSanPhamTheoMa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã chi tiết trùng" + ex, "Thông báo");
                }
            }
        }

        private void btnVatLieu_Click(object sender, EventArgs e)
        {
            frmThemVatLieu themVatlieu = new frmThemVatLieu();
            themVatlieu.ShowDialog();
            ShowMetail();
        }
    }
}
