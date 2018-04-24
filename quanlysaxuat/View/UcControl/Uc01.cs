using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Threading;
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class Uc01 : UserControl
    {
        public Uc01()
        {
            InitializeComponent();
        }
    
        public static string THONGTIN_MOI;
        SqlCommand cmd;
        private void hieuhaiso()
        {
            double SOLUONG_HT = double.Parse(txtTongSLHT.Text);
            double SOLUONGSANXUAT = double.Parse(txtsluongsx.Text);
            double SOLUONGCONLAI = SOLUONG_HT - SOLUONGSANXUAT;
            txtsoluongchuaxuat.Text = Convert.ToString(SOLUONGCONLAI);
        }
        private void LOAD_ALL()
        {        }
        private void LOAD_TRE()
        {        }
        private void LOAD_TIME()
        { 
        }
        private void LOAD_TIME_DMGIAOHANG()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(" SELECT *  FROM dbo.tbl01 where  convert(Date, ngaynhan, 103)   between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void LOAD_DMGIAOHANG_CODE()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("SELECT *  FROM dbo.tbl01 where IDSP like '" + txtCode.Text + "'");
            kn.dongketnoi();
        }
        private void LOAD_MAGIAOHANG()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("SELECT *  FROM dbo.tbl01 where MaGH like '" + txtMagiaohang.Text + "'");
            kn.dongketnoi();
        }
        private void LOADGridLookupEdit()
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            CbMadhLook.Properties.DataSource = Connect.laybang("select  Donvisp,madh,SPLR,SLSPLR,nvkd,ngaytrienkhai,IDSP,sanpham,Mact,Ten_ct,So_CT,soluongyc,tonkho, "
                      + " soluongsx, ngoaiquang, Maubv,mabv, donvi, daystar, dayend, "
                      + " KetThucTo1,BTPT1,TRONGLUONG1, khachhang, xeploai,MaPo,LoaiDH, Ghichu from tblchitietkehoach where madh is not null and KetThucTo1 is not null "
                      + " and convert(Date, ngaytrienkhai, 103)   between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            CbMadhLook.Properties.DisplayMember = "madh";
            CbMadhLook.Properties.ValueMember = "madh";
            CbMadhLook.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            CbMadhLook.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            CbMadhLook.Properties.ImmediatePopup = true; Connect.dongketnoi();
        }
        private void LOAD_DMBOPHAN()
        {
            ketnoi Connect = new ketnoi();
            cbBP_NHAN.DataSource = Connect.laybang("select To_bophan from tblPHONGBAN ");
            cbBP_NHAN.DisplayMember = "To_bophan";
            cbBP_NHAN.ValueMember = "To_bophan"; Connect.dongketnoi();
        }
        private void LOAD_TOSX()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Tenpb from Admin where Taikhoan LIKE N'" + txtnguoidangnhap.Text + "' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtBP_SX.Text = Convert.ToString(reader[0]);
            reader.Close();
            con.Close();
        }
        private void LOADAD_MAGH()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Top 1 REPLACE(convert(nvarchar,GetDate(),11),'/','') +'-'+convert(nvarchar,(DATEPART(HH,GetDate())))+':'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMagiaohang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void GhiMaPGH()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.xulydulieu("update tbl01 set MaGH='" + txtMagiaohang.Text + "' where Num like '" + txtId.Text + "'");
            LOAD_TIME_DMGIAOHANG();Connect.dongketnoi();
        }
        private void HuyMaPGH()
        {
            ketnoi Connect = new ketnoi();
            gridControl2.DataSource = Connect.xulydulieu("update tbl01 set MaGH ='' where Num like '" + txtId.Text + "' ");
            LOAD_TIME_DMGIAOHANG(); Connect.dongketnoi();
        }
        private void Save_DLSX()
        {
            try
            {
                if (txtCode.Text != "" && txtKinhdoanh.Text != "" && CbMadhLook.Text != "")/// insert cách truyền tham số parameters
                {
                    SqlConnection cn = new SqlConnection();
                    decimal Soluongsanxuat = Convert.ToDecimal(txtsluongsx.Text);
                    decimal SoLuongHoanThanh = Convert.ToDecimal(SOLUONGTT.Text);
                    decimal SLSP_HT = Convert.ToDecimal(txtSoluong_SPHT.Text);
                    cn.ConnectionString = Connect.mConnect;
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO tbl01(Mact,So_CT,SL_CTHT,TL_CTHT,Maubv,LoaiDH,MaPo,IDSP,ngaynhan,nvkd,madh,sanpham,"
                    + "chitietsanpham,cdthanhpham,soluongsx,ngoaiquang,mabv,donvi,daystar,dayend,BTPT01,"
                    + "TRONGLUONG01,khachhang,xeploai,ghichu,TenBPnhan,MaSQL,TenSQL,Donvisp,SoluongSP)"
                    + "values (@Mact,@So_CT,@SL_CTHT,@TL_CTHT,@Maubv,@LoaiDH,@MaPo,@IDSP,GETDATE(),@nvkd,@madh,@sanpham,"
                    + "@chitietsanpham,@cdthanhpham,@soluongsx,@ngoaiquang,@mabv,@donvi,@daystar,@dayend,@BTPT01,"
                    + "@TRONGLUONG01,@khachhang,@xeploai,@ghichu,@TenBPnhan,@MaSQL,@TenSQL,@Donvisp,@SoluongSP)", cn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    try
                    {
                        if (dpbatdau.Text == "")

                            command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = DBNull.Value;
                        else
                            command.Parameters.Add(new SqlParameter("@daystar", SqlDbType.Date)).Value = dpbatdau.Text;
                        if (dpketthuc.Text == "")

                            command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = DBNull.Value;
                        else
                            command.Parameters.Add(new SqlParameter("@dayend", SqlDbType.Date)).Value = dpketthuc.Text;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    command.Parameters.Add(new SqlParameter("@IDSP", SqlDbType.NVarChar)).Value = txtCode.Text;
                    command.Parameters.Add(new SqlParameter("@Mact", SqlDbType.NVarChar)).Value = txtMact.Text;
                    command.Parameters.Add(new SqlParameter("@So_CT", SqlDbType.NVarChar)).Value = txtSLCt.Text;
                    command.Parameters.Add(new SqlParameter("@SL_CTHT", SqlDbType.NVarChar)).Value = txtSLct_HT.Text;
                    command.Parameters.Add(new SqlParameter("@TL_CTHT", SqlDbType.NVarChar)).Value = txtTLCT_HT.Text;
                    command.Parameters.Add(new SqlParameter("@Maubv", SqlDbType.NVarChar)).Value = txtmau_banve.Text;
                    command.Parameters.Add(new SqlParameter("@LoaiDH", SqlDbType.NVarChar)).Value = txtloaiDH.Text;
                    command.Parameters.Add(new SqlParameter("@MaPo", SqlDbType.NVarChar)).Value = txtmapo.Text;
                    command.Parameters.Add(new SqlParameter("@nvkd", SqlDbType.NVarChar)).Value = txtKinhdoanh.Text;
                    command.Parameters.Add(new SqlParameter("@madh", SqlDbType.NVarChar)).Value = CbMadhLook.Text;
                    command.Parameters.Add(new SqlParameter("@sanpham", SqlDbType.NVarChar)).Value = txtsanpham.Text;
                    command.Parameters.Add(new SqlParameter("@chitietsanpham", SqlDbType.NVarChar)).Value = txtSanpham_hoanthanh.Text;
                    command.Parameters.Add(new SqlParameter("@cdthanhpham", SqlDbType.NVarChar)).Value = cdthanhpham.Text;
                    command.Parameters.Add(new SqlParameter("@soluongsx", SqlDbType.Int)).Value = Soluongsanxuat;
                    command.Parameters.Add(new SqlParameter("@ngoaiquang", SqlDbType.NVarChar)).Value = txtngoaiquang.Text;
                    command.Parameters.Add(new SqlParameter("@mabv", SqlDbType.NVarChar)).Value = txtMaSP.Text;
                    command.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonvi.Text;
                    command.Parameters.Add(new SqlParameter("@BTPT01", SqlDbType.NVarChar)).Value = SoLuongHoanThanh;
                    command.Parameters.Add(new SqlParameter("@TRONGLUONG01", SqlDbType.Float)).Value = TRONGLUONGTT.Text;
                    command.Parameters.Add(new SqlParameter("@khachhang", SqlDbType.NVarChar)).Value = txtkhachhang.Text;
                    command.Parameters.Add(new SqlParameter("@xeploai", SqlDbType.NVarChar)).Value = txtxeploai.Text;
                    command.Parameters.Add(new SqlParameter("@ghichu", SqlDbType.NVarChar)).Value = txtghichu.Text;
                    command.Parameters.Add(new SqlParameter("@TenBPnhan", SqlDbType.NVarChar)).Value = cbBP_NHAN.Text;
                    command.Parameters.Add(new SqlParameter("@MaSQL", SqlDbType.NVarChar)).Value = txtnguoidangnhap.Text;
                    command.Parameters.Add(new SqlParameter("@TenSQL", SqlDbType.NVarChar)).Value = txtBP_SX.Text;
                    command.Parameters.Add(new SqlParameter("@Donvisp", SqlDbType.NVarChar)).Value = txtDVSP.Text;
                    command.Parameters.Add(new SqlParameter("@SoluongSP", SqlDbType.Int)).Value = SLSP_HT;
                    adapter.Fill(dt);
                    LOAD_DMGIAOHANG_CODE(); SOLUONGTT.ResetText(); TRONGLUONGTT.ResetText(); TRONGLUONGTT.Text = "0";
                    cn.Close();
                }
            }
            catch
            {
                    MessageBox.Show("Không Thành Công", "THÔNG BÁO");
            }  
        }

        private void CAPNHAT_HETHONG()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Connect.mConnect;
                conn.Open();
                DataSet ds = SqlHelper.ExecuteDataset(conn, "[T01_UPDATE]");
                MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG");
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công");
            }
            LOAD_TIME(); LOADGridLookupEdit();
        }

        private void XOA_DLSX()
        {
            if (txtId.Text != "")
            {
                ketnoi kn = new ketnoi();
                int kq = kn.xulydulieu("UPDATE tbl01 SET BTPT01 = 0,TRONGLUONG01 = 0  Where Num ='" + txtId.Text + "'");
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Connect.mConnect;
                conn.Open();
                DataSet ds = SqlHelper.ExecuteDataset(conn, "[T01_UPDATE]");
                gridControl2.DataSource = kn.xulydulieu("DELETE tbl01 Where Num ='" + txtId.Text + "'");
                if (kq < 0)
                { MessageBox.Show("Không Thành Công", "THÔNG BÁO"); }kn.dongketnoi();
            }
            LOADGridLookupEdit();
            LOAD_TIME_DMGIAOHANG();
        }
        private void LOAD_PRINT_PHIEUGH()
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from tbl01 where MaGH  like N'" + txtMagiaohang.Text + "'");
            XRPGH_NOIBO XtrGIAOHANG = new XRPGH_NOIBO();
            XtrGIAOHANG.DataSource = dt;
            XtrGIAOHANG.DataMember = "Table";
            XtrGIAOHANG.ShowPreviewDialog(); kn.dongketnoi();
        }
        private void Bindin_GridLookEdit()
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtCode.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Code_Look);
            txtmapo.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Mapo_Look);
            txtkhachhang.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Khachhang_Look);
            txtKinhdoanh.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Kinhdoanh_look);
            txtxeploai.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Xeploai_Look);
            txtloaiDH.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(LoaiDH_Look);
            dpbatdau.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Batdau_Look);
            dpketthuc.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Ketthuc_Look);
            txtMaSP.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_Look);
            txtsanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Sanpham_look);
            txtMact.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(MaCt_Look);
            txtSanpham_hoanthanh.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(SPLR_HT_look);
            txtSoluong_SPHT.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Soluong_SPHT_look);
            txtSLCt.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(SLCt_Look);
            txtdonvi.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Donvi_Look);
            txtmau_banve.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Maubv_Look);
            txtngoaiquang.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Ngoaiquan_Look);
            txtghichu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(ghichu_Look);
            txtsluongsx.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(SLSX_Look);
            txtTongSLHT.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(TongSLHT_Look);
            txtTongTLHT.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(TongTLHT_Look);
            txtDVSP.Text= gridLookUpEdit1View.GetFocusedRowCellDisplayText(donviSP_HT);
            hieuhaiso();
        }
        private void Binding_GIAOHANG() {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtId.Text = gridView2.GetFocusedRowCellDisplayText(Id_grid2);
            txtCode.Text = gridView2.GetFocusedRowCellDisplayText(IDSP_grid2);
            txtmapo.Text = gridView2.GetFocusedRowCellDisplayText(Mapo_grid2);
            txtkhachhang.Text = gridView2.GetFocusedRowCellDisplayText(Khachhang_grid2);
            txtKinhdoanh.Text = gridView2.GetFocusedRowCellDisplayText(Kinhdoanh_grid2);
            txtxeploai.Text = gridView2.GetFocusedRowCellDisplayText(Xeploai_grid2);
            txtloaiDH.Text = gridView2.GetFocusedRowCellDisplayText(Loaidonhang_grid2);
            dpbatdau.Text = gridView2.GetFocusedRowCellDisplayText(Ngaybatdau_grid2);
            dpketthuc.Text = gridView2.GetFocusedRowCellDisplayText(Ngayketthuc_grid2);
            txtMaSP.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
            txtsanpham.Text = gridView2.GetFocusedRowCellDisplayText(Sanpham_grid2);
            txtMact.Text = gridView2.GetFocusedRowCellDisplayText(Machitiet_grid2);
            txtSanpham_hoanthanh.Text = gridView2.GetFocusedRowCellDisplayText(Sanpham_grid2);
            txtSoluong_SPHT.Text = gridView2.GetFocusedRowCellDisplayText(SoluongSP_grid2);
            txtSLCt.Text = gridView2.GetFocusedRowCellDisplayText(Sochitiet_grid2);
            txtdonvi.Text = gridView2.GetFocusedRowCellDisplayText(DonviCT_grid2);
            txtmau_banve.Text = gridView2.GetFocusedRowCellDisplayText(Maubv_grid2);
            txtngoaiquang.Text = gridView2.GetFocusedRowCellDisplayText(Ngoaiquan_grid2);
            txtghichu.Text = gridView2.GetFocusedRowCellDisplayText(Ghichu_grid2);
            txtsluongsx.Text = gridView2.GetFocusedRowCellDisplayText(SLCTSX_grid2);
            txtDVSP.Text = gridView2.GetFocusedRowCellDisplayText(DonviSP_grid2);
        }

        private void BindingDL_TIENDO()
        {
            hieuhaiso(); LOAD_DMGIAOHANG_CODE();
        }
        private void FORMATTONG_SLHT()
        {
            if (txtTongSLHT.Text == "")
            {
                txtTongSLHT.Text = "0";
            }
            txtTongSLHT.Text = string.Format("{0:0,0}", decimal.Parse(txtTongSLHT.Text));
            txtTongSLHT.SelectionStart = txtTongSLHT.Text.Length;
        }
        private void FORMAT_SLCONLAI()
        {
            if (txtsoluongchuaxuat.Text == "")
            {
                txtsoluongchuaxuat.Text = "0";
            }
            txtsoluongchuaxuat.Text = string.Format("{0:0,0}", decimal.Parse(txtsoluongchuaxuat.Text));
            txtsoluongchuaxuat.SelectionStart = txtsoluongchuaxuat.Text.Length;

        }
        private void FORMAT_SLGH()
        {
            if (SOLUONGTT.Text == "")
            {
                SOLUONGTT.Text = "0";
            }
            SOLUONGTT.Text = string.Format("{0:0,0}", decimal.Parse(SOLUONGTT.Text));
            SOLUONGTT.SelectionStart = SOLUONGTT.Text.Length;
        }
        private void FORMAT_SLSX()
        {
            if (txtsluongsx.Text == "")
            {
                txtsluongsx.Text = "0";
            }
            txtsluongsx.Text = string.Format("{0:0,0}", decimal.Parse(txtsluongsx.Text));
            txtsluongsx.SelectionStart = txtsluongsx.Text.Length;
        }
        private void FORMAT_SLSPHT()
        {
            if (txtSoluong_SPHT.Text == "")
            {
                txtSoluong_SPHT.Text = "0";
            }
            txtSoluong_SPHT.Text = string.Format("{0:0,0}", decimal.Parse(txtSoluong_SPHT.Text));
            txtSoluong_SPHT.SelectionStart = txtSoluong_SPHT.Text.Length;
        }
        private void Binding_DLGH(object Sender,EventArgs e)
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtId.Text = gridView2.GetFocusedRowCellDisplayText(Id_grid2);
            txtMagiaohang.Text = gridView2.GetFocusedRowCellDisplayText(Magh_grid2);
            KhoaGiaoDich();
        }
        private void KhoaGiaoDich()
        {
            if (txtMagiaohang.Text == "")
            {
                btnGhimaGH.Enabled = true;
                btnLayMaGH.Enabled = true;
                btnHuyMaGH.Enabled = true;
            }
            else if(txtMagiaohang.Text != "")
            {
                btnGhimaGH.Enabled = false;
                btnLayMaGH.Enabled = false;
                btnHuyMaGH.Enabled = false;
            }
        }
        private void EXPORT_DLSX()
        {
        }
        private void SHOW_TIENDO()
        {
            //frmPr01 Pr01 = new frmPr01();
            //Pr01.Show();
        }
        private void SHOW_THONGKE()
        {
            //frmTK01 TK01 = new frmTK01();
            //TK01.Show();
        }
        private void SHOWPRINTPRIVIEW()
        { gridControl2.ShowPrintPreview(); }

        private void Uc01_Load(object sender, EventArgs e)// FORM LOAD
        {
            LOAD_TRE();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtnguoidangnhap.Text = Login.Username;
            LOADGridLookupEdit();
            LOAD_DMBOPHAN();
            LOAD_TOSX();
        }
        private void btnLUU_DLSX(object sender, EventArgs e)
        {
            Save_DLSX();
        }

        private void btnCAPNHAT_HETHONG(object sender, EventArgs e)
        {
            //CAPNHAT_HETHONG();
        }
        private void btnXOADL_DAGHI(object sender, EventArgs e)
        {
            XOA_DLSX();
        }
        private void PrintPHIEU_GH(object sender, EventArgs e)
        {
            LOAD_PRINT_PHIEUGH();
        }
        private void BINDING_LOOKDHTRIENKHAI(object sender, EventArgs e)
        {
            Bindin_GridLookEdit();
        }
        private void BINDINGDL_TIENDOSX(object sender, EventArgs e)
        {
            BindingDL_TIENDO();
        }
        private void LOADDM_GIAOHANG(object sender, EventArgs e)
        {
            LOAD_TIME_DMGIAOHANG(); gridView2.ExpandAllGroups();
        }

        private void LOAD_DMSANXUAT_Code(object sender, EventArgs e)
        {
            LOAD_DMGIAOHANG_CODE();
        }

        private void BINDINGDL_GIAOHANG(object sender, EventArgs e)
        {
            Binding_GIAOHANG();
        }

        private void GOI_TIENDOSX(object sender, EventArgs e)
        {
            SHOW_TIENDO();
        }

        private void GOI_THONGKE(object sender, EventArgs e)
        { SHOW_THONGKE(); }

        private void LOADTIME_TIENDO(object sender, EventArgs e)
        {
            LOAD_TIME();
        }

        private void LOADTIME_TIENDO1(object sender, EventArgs e)
        {
            LOAD_TIME();
        }

        private void LOADTRE_TIENDO(object sender, EventArgs e)
        {
            LOAD_TRE();
        }

        private void EXPORT_DLSX(object sender, EventArgs e)
        {
            EXPORT_DLSX();
        }

        private void LOADALL_TIENDO(object sender, EventArgs e)
        { LOAD_ALL(); }
        private void SEARCHLOOKUPEDIT_PHIEUSX(object sender, EventArgs e)
        { LOADGridLookupEdit(); }

        private void LAYMAPHIEU_GIAOHANG(object sender, EventArgs e)
        { LOADAD_MAGH(); }

        private void FORMATTONG_SLHT(object sender, EventArgs e)
        { FORMATTONG_SLHT(); }

        private void GHIMAPHIEU_GIAOHANG(object sender, EventArgs e)
        {
            GhiMaPGH();
        }

        private void HUYMAPHIEUGIAOHANG(object sender, EventArgs e)
        {
            HuyMaPGH();
        }

        private void FORMAT_SLCONLAI(object sender, EventArgs e)
        {
            FORMAT_SLCONLAI();
        }

        private void FORMA_SLGH(object sender, EventArgs e)
        {
            FORMAT_SLGH();
        }

        private void FORMAT_SLSX(object sender, EventArgs e)
        {
            FORMAT_SLSX();
        }

        private void SOLUONGTT_Click(object sender, EventArgs e)
        {
            FORMAT_SLGH();
        }
        private void SHOWPRINT_PRIIVEW(object sender, EventArgs e) { SHOWPRINTPRIVIEW(); }
        private void FORMAT_SLSPHT(object sender, EventArgs e) { FORMAT_SLSPHT(); }
    }
}

