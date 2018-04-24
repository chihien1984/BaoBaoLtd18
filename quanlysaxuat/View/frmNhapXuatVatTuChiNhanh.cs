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
using System.Data.SqlClient;
using DevExpress.Pdf.Native;
using quanlysanxuat.Model;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Report;
using DevExpress.Persistent.BaseImpl;

namespace quanlysanxuat.View
{
    public partial class frmNhapXuatVatTuChiNhanh : DevExpress.XtraEditors.XtraForm
    {
        public string maphieu;
        public DateTime ngaylap;
        public string tenkho;

        public frmNhapXuatVatTuChiNhanh(string maphieu, DateTime ngaylap, string tenkho)
        {
            InitializeComponent();
            this.maphieu = maphieu;
            this.ngaylap = ngaylap;
            this.tenkho = tenkho;
        }
        //formload
        private void frmNhapXuatVatTu_Load(object sender, EventArgs e)
        {
            if (ngaylap == null)
            {
                dpNgayLap.Text = Convert.ToDateTime(ngaylap).ToString("yyyy-MM-dd");
            }
            else 
            {
                dpNgayLap.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtMaPhieuNhap.Text = maphieu;
            }
            dpTu.Text = DateTime.Today.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            gvPhieuNhapXuat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            TheHienKeHoachVatTu();
            TheHienDanhMucVatTu();
            //TheHienDanhMucVatTuPhu();
            LoaiPhieu();
            TenKho();
            THNhapXuatTon();
            QuyenLapPhieu();
            THNhapXuatTon();
            cbLoaiPhieu.Text = txtMaPhieuNhap.Text == "" ? cbLoaiPhieu.Text : (txtMaPhieuNhap.Text).Substring(0, 2);
            cbTenKho.Text = tenkho;
        }
        private void QuyenLapPhieu()
        {
            if (ClassUser.User== "00574" || ClassUser.User == "00288"|| ClassUser.User == "99999")
            {
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiPhieuNhapXuat();
        }
        private void THNhapXuatTon()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select * from VatTuNhapXuat where 
			    MaChungTu like N'{0}' and Del like 0", maphieu);
            grPhieuNhapXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void TaoMoiPhieuNhapXuat()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select  top 0 ''TenVatTu,''MaVatTu,
                         ''DonVi,cast('' as float) SoLuong,
                    cast('' as float)DonGia,
                    cast('' as float)ThanhTien,
                         ''ChiTietDinhMuc,''DienGiai,
                         ''TenQuiCach,''IDKeHoachVatTu,''IDVatTu,''ChungTuKemTheo,''NguoiGiaoNhan,''DonVi,''SoQuyDoi");
            grPhieuNhapXuat.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }
        private void LoaiPhieu()
        {
            cbLoaiPhieu.Items.Add("PN");
            cbLoaiPhieu.Items.Add("PX");
            cbLoaiPhieu.SelectedIndex = 0;
        }
        private void TenKho()
        {
            cbTenKho.Items.Add("Kho-BaoBao");
            cbTenKho.Items.Add("Kho-CN1-BaoBao-TruongThanh");
            cbTenKho.Items.Add("Kho-CN2-BaoBao");
            cbTenKho.Items.Add("Kho-CN3-BaoBao");
            cbTenKho.Items.Add("Kho-CN4-BaoBao");
            cbTenKho.Items.Add("Kho-CN5-BaoBao");
            cbTenKho.Items.Add("Kho-CN6-BaoBao");
            cbTenKho.SelectedIndex = 1;
        }

        private void TheHienDanhMucVatTu()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select Ten_vat_lieu TenVatTu,
                    Ma_vl MaVatTu,id IDVatTu,Donvi DonVi from tblDM_VATTU order by id desc");
            repositoryItemGridLookUpEditTenNhanHieu.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditTenNhanHieu.ValueMember = "MaVatTu";
            repositoryItemGridLookUpEditTenNhanHieu.DisplayMember = "MaVatTu";
            Model.Function.Disconnect();
        }
        private void TheHienDanhMucVatTuPhu()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select Tenvlphu TenVatTu,
                    Mavlphu MaVatTu,id IDVatTu,Donvi DonVi from tblDM_VATLIEUPHU order by id desc");
            repositoryItemGridLookUpEditTenNhanHieu.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditTenNhanHieu.ValueMember = "MaVatTu";
            repositoryItemGridLookUpEditTenNhanHieu.DisplayMember = "MaVatTu";
            Model.Function.Disconnect();
        }
        private void TheHienKeHoachVatTu()
        {
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select CodeVatllieu IDKeHoachVatTu,madh,Masp,Tenquicachsp TenQuiCach,
                Soluongsanpham,Mavattu,Ten_vattu,SL_vattucan
                from tblvattu_dauvao 
				where Ngaylap_DM between '{0}' and '{1}'
				order by Ngaylap_DM Desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            repositoryItemGridLookUpEditDinhMuc.DataSource = Model.Function.GetDataTable(sqlQuery);
            repositoryItemGridLookUpEditDinhMuc.ValueMember = "IDKeHoachVatTu";
            repositoryItemGridLookUpEditDinhMuc.DisplayMember = "IDKeHoachVatTu";
            Model.Function.Disconnect();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.maphieu = "";
        }

        private void btnTaoMaPhieu_Click(object sender, EventArgs e)
        {
            LayManhapkho();
        }
        private void LayManhapkho()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select Top 1 '{0}'+left(REPLACE(convert(nvarchar,GetDate(),11),'/',''),4)", cbLoaiPhieu.Text);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaPhieuNhap.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        #region event cell-value-changed
        private void gvPhieuNhapXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {  
            var value = gvPhieuNhapXuat.GetRowCellValue(e.RowHandle, e.Column);
            Model.Function.ConnectSanXuat();//Mo ket noi
            string sqlQuery = string.Format(@"select Tenvlphu TenVatTu,
                    Mavlphu MaVatTu,id IDVatTu,Donvi DonVi 
                    from tblDM_VATLIEUPHU  where Tenvlphu like N'{0}'",value);
            var dtx = Model.Function.GetDataTable(sqlQuery);


            SANXUATDbContext db = new SANXUATDbContext();
            if (e.Column.FieldName == "MaVatTu")
            {
                if (ckVatTuPhu.Checked == true)
                {
                    //List<VatTuPhuViewModels> dt1 = dtx.AsEnumerable().Where(x => x.Field<string>("TenVatTu") == (string)value).
                    //     Select(m => new VatTuPhuViewModels()
                    //     {
                    //         TenVatTu = m.Field<string>("TenVatTu"),
                    //         //IDVatTu = Convert.ToInt32(m.Field<double>("IDVatTu"))
                    //         DonVi = m.Field<string>("DonVi"), 
                    //         MaVatTu = m.Field<string>("MaVatTu")
                    //     }).ToList();
                    //foreach(var item in dt1)
                    //{
                    //     if (dt1 != null)
                    //    {
                    //        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "TenVatTu", item.TenVatTu);
                    //        //gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "MaVatTu", item.MaVatTu);
                    //        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "DonVi", item.DonVi);
                    //    }
                    //}

                    var dt1 = db.tblDM_VATLIEUPHU.FirstOrDefault(x => x.Mavlphu == (string)value);
                    if (dt1 != null)
                    {
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "TenVatTu", dt1.Tenvlphu);
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "IDVatTu", dt1.id);
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "DonVi", dt1.Donvi);
                    }
                }
                else
                {
                    var dt = db.tblDM_VATTU.FirstOrDefault(x => x.Ma_vl == (string)value);
                    if (dt != null)
                    {
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "TenVatTu", dt.Ten_vat_lieu);
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "IDVatTu", dt.id);
                        gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "DonVi", dt.Donvi);
                    }
                }
            }
            if (e.Column.FieldName == "IDKeHoachVatTu")
            {
                var _value = gvPhieuNhapXuat.GetRowCellValue(e.RowHandle, e.Column);
                var dt = db.tblvattu_dauvao
                    .FirstOrDefault(x => x.CodeVatllieu.ToString() == _value.ToString());
                if (dt != null)
                {
                    gvPhieuNhapXuat.SetRowCellValue(e.RowHandle, "DienGiai", dt.madh + ';' + dt.Tenquicachsp + dt.Masp + dt.Soluongsanpham + dt.SL_vattucan);
                }
            }
            //tinh thanh tien
            if (e.Column == soluong_col || e.Column == dongia_col)
            {
                double soluong, dongia, thanhtien;
                soluong = gvPhieuNhapXuat.GetFocusedRowCellValue(soluong_col) == DBNull.Value ? 0 : Convert.ToDouble(gvPhieuNhapXuat.GetFocusedRowCellValue(soluong_col));
                dongia = gvPhieuNhapXuat.GetFocusedRowCellValue(dongia_col) == DBNull.Value ? 0 : Convert.ToDouble(gvPhieuNhapXuat.GetFocusedRowCellValue(dongia_col));
                thanhtien = soluong * dongia;
                gvPhieuNhapXuat.SetFocusedRowCellValue(thanhtien_col, thanhtien);
            }
            gvPhieuNhapXuat.SelectAll();
        }
        #endregion
        private void cbLoaiPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (maphieu == "")
            //{
            //    LayManhapkho();
            //}
            //else
            //{
            //    txtMaPhieuNhap.Text = maphieu;
            //}
            LayManhapkho();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvPhieuNhapXuat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhieuNhapXuat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into VatTuNhapXuat (IDKeHoachVatTu,IDVatTu,MaChungTu,
                            NgayChungTu,MaVatTu,
                            TenVatTu,SoLuong,
                            DonGia,ThanhTien,
                            DienGiai,NguoiLap,TenKho,
                            ChungTuKemTheo,NguoiGiaoNhan,DonVi,SoQuyDoi,NgayLap)
                            values ('{0}','{1}',N'{2}',
                            '{3}',N'{4}',
                            N'{5}','{6}',
                            '{7}','{8}',
                            N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}','{15}',GetDate())",
                       rowData["IDKeHoachVatTu"], rowData["IDVatTu"], txtMaPhieuNhap.Text,
                       dpNgayLap.Value.ToString("yyyy-MM-dd"), rowData["MaVatTu"],
                       rowData["TenVatTu"], rowData["SoLuong"], rowData["DonGia"],
                       rowData["ThanhTien"], rowData["DienGiai"], Login.Username,
                       cbTenKho.Text, rowData["ChungTuKemTheo"],
                       rowData["NguoiGiaoNhan"], rowData["DonVi"], rowData["SoQuyDoi"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvPhieuNhapXuat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhieuNhapXuat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update VatTuNhapXuat set IDKeHoachVatTu='{0}',IDVatTu='{1}',MaChungTu=N'{2}',
                            NgayChungTu='{3}',MaVatTu=N'{4}',
                            TenVatTu=N'{5}',SoLuong='{6}',
                            DonGia='{7}',ThanhTien='{8}',
                            DienGiai=N'{9}',NguoiSua=N'{10}',TenKho=N'{11}',DonVi=N'{12}',
                        ChungTuKemTheo=N'{13}',NguoiGiaoNhan='{14}',SoQuyDoi='{15}',NgaySua=GetDate()
                        where ID like '{16}'",
                       rowData["IDKeHoachVatTu"], rowData["IDVatTu"], txtMaPhieuNhap.Text,
                       dpNgayLap.Value.ToString("yyyy-MM-dd"), rowData["MaVatTu"],
                       rowData["TenVatTu"], rowData["SoLuong"], rowData["DonGia"],
                       rowData["ThanhTien"], rowData["DienGiai"],
                       Login.Username, cbTenKho.Text, rowData["DonVi"],
                       rowData["ChungTuKemTheo"], rowData["NguoiGiaoNhan"],
                       rowData["SoQuyDoi"], rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvPhieuNhapXuat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvPhieuNhapXuat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"update VatTuNhapXuat set Del= 1 where ID like '{0}'",
                       rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void btnPhieu_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhieu.Text == "PX")
            {
                Function.ConnectSanXuat();//Mo ket noi
                string sqlQuery = string.Format(@"select * from NhapXuatKhoCN_vw where left(MaChungTu,2)='PX' 
                        and MaChungTu like  N'{0}'", txtMaPhieuNhap.Text);
                ReportXuatKhoVatTuCN XuatKho = new ReportXuatKhoVatTuCN();
                XuatKho.DataSource = Function.GetDataTable(sqlQuery);
                XuatKho.DataMember = "Table";
                XuatKho.CreateDocument(false);
                XuatKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuNhap.Text;
                PrintTool tool = new PrintTool(XuatKho.PrintingSystem);
                XuatKho.ShowPreviewDialog();
                Function.Disconnect();
            }
            else if (cbLoaiPhieu.Text == "PN")
            {
                Function.ConnectSanXuat();//Mo ket noi
                string sqlQuery = string.Format(@"select * from NhapXuatKhoCN_vw where 
                    left(MaChungTu,2)='PN' and MaChungTu like N'{0}'", txtMaPhieuNhap.Text);
                ReportNhapKhoVatTuCN nhapKho = new ReportNhapKhoVatTuCN();
                nhapKho.DataSource = Function.GetDataTable(sqlQuery);
                nhapKho.DataMember = "Table";
                nhapKho.CreateDocument(false);
                nhapKho.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = txtMaPhieuNhap.Text;
                PrintTool tool = new PrintTool(nhapKho.PrintingSystem);
                nhapKho.ShowPreviewDialog();
                Function.Disconnect();
            }
        }

        private void btnRefreshItem_Click(object sender, EventArgs e)
        {
            TheHienKeHoachVatTu();
            TheHienDanhMucVatTu();
            TheHienDanhMucVatTuPhu();
        }

        private void ckVatTuPhu_CheckedChanged(object sender, EventArgs e)
        {
            if (ckVatTuPhu.Checked == true)
            {
                TheHienDanhMucVatTuPhu();
            }
            if(ckVatTuPhu.Checked == false)
            {
                TheHienDanhMucVatTu();
            }
        }
    }
}