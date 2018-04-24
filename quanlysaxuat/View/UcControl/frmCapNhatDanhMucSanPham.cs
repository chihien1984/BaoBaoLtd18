using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using quanlysanxuat.Models;

namespace quanlysanxuat
{
    public partial class frmCapNhatDanhMucSanPham : DevExpress.XtraEditors.XtraForm
    {
        public frmCapNhatDanhMucSanPham()
        {
            InitializeComponent();
        }
        Clsketnoi knn = new Clsketnoi();
      
        public static string THONGTIN_MOI;
        string Gol = "";
        SqlCommand cmd;
        Clsketnoi connect = new Clsketnoi();
        public void DocDSSanPhamTheoNgay()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select check1,check2,Masp,Tensp,Kichthuoc,Vatlieu, 
              Dacdiem, Ngaylap, KH.MKH, KH.TenKH, sp.manv, hotennv,Code 
              from tblSANPHAM SP left outer join tblKHACHHANG KH 
              on SP.Makh = KH.MKH");
            grDanhMucSanPham.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        #region formload
        private void UcDMSanPham_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            this.gvDanhMucSanPham.Appearance.Row.Font = new Font("Times New Roman", 7f);
            gvDanhMucSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            THDanhSachVatLieu();
            THDanhMucKhachHang();
            THDanhSachSanPham();
        }
        #endregion
        private void GetRole()
        {
            if (Login.role=="1"|| Login.role == "39")
            {
                txtSoThuTu.Enabled = true;
                txtTenSanPham.Enabled = true;
                btnthem.Enabled = true;
                btnxoa.Enabled = true;
                btnsua.Enabled = true;
            }
            else
            {
                txtTenSanPham.Enabled = false;
                btnthem.Enabled = false;
                btnxoa.Enabled = false;
                btnsua.Enabled = false;
                txtSoThuTu.Enabled = false;
            }
        }
        //private void TracKingpdf()
        //{
        //    //File that we will create
        //    string OutputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Events.pdf");

        //    //Standard PDF creation setup
        //    using (FileStream fs = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        using (Document doc = new Document(PageSize.LETTER))
        //        {
        //            using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
        //            {
        //                //Open our document for writing
        //                doc.Open();

        //                //Create an action that points to the built-in app.doc object and calls the getURL method on it
        //                PdfAction act = PdfAction.JavaScript("app.doc.getURL('http://www.google.com/');", writer);

        //                //Set that action as the documents open action
        //                writer.SetOpenAction(act);

        //                //We need to add some content to this PDF to be valid
        //                doc.Add(new Paragraph("Hello"));

        //                //Close the document
        //                doc.Close();
        //            }
        //        }
        //    }
        //}

        private async void THDanhSachSanPham()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select Case when isnumeric (right(Masp,4)) >0 then right(Masp,4) end TT,
					   Masp,Tensp,Kichthuoc,Vatlieu, 
                       Dacdiem,Ngaylap,a.Makh,case when b.TenKH is null 
					   then c.TenKH else b.TenKH end TenKH,a.manv, 
                       hotennv,Code,check1,check2 from tblSANPHAM a 
					   left outer join tblKHACHHANG b 
                       on a.Makh = b.MKH
					left outer join
					(select a.Makh,b.TenKH from tblSANPHAM a
					left outer join (select right(concat('0000',ID),4)ID,
					MKH,TenKH from tblKHACHHANG)b
					on a.Makh = b.ID where b.TenKH is not null
                    group by a.Makh,b.TenKH)c
					on a.Makh=c.Makh");
                Invoke((Action)(() => {
                    grDanhMucSanPham.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void THDanhMucKhachHang()
        {
            ketnoi kn = new ketnoi();
            cbTenKhachHang.DataSource = kn.laybang("select TenKH from tblKHACHHANG");
            cbTenKhachHang.ValueMember = "TenKH";
            cbTenKhachHang.DisplayMember = "TenKH";
            kn.dongketnoi();
        }


        private void txtTenNV_TextChanged(object sender, EventArgs e)//Select tên nhân viên
        {

        }
        private void MaNV()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select Sothe from tblDSNHANVIEN where HoTen like N'" + Login.Username + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                Login.Username = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void cbtenkh_SelectedIndexChanged(object sender, EventArgs e)//chọn tên khách hàng
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select top 1 right(concat('0000',ID),4)
                    from tblKHACHHANG where TenKH like N'{0}'",cbTenKhachHang.Text);
            cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtCodeKhachHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void cbmakh_SelectedIndexChanged(object sender, EventArgs e)//chọn mã khách hàng
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select TenKH from tblKHACHHANG where MKH like N'%" + cbTenKhachHang.Text + "%'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                cbTenKhachHang.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void LayMaVatlieu()
        {
            
        }
        private void LayMaVatLieu_Click(object sender,EventArgs e)
        {

        }
        private void LoadNhomsanpham()
        {
            //ketnoi kn = new ketnoi();
            //cbnhomsanpham.DataSource= kn.laybang("SELECT distinct(LEFT(Masp,4)) " 
            //    + " Masp from tblSANPHAM where Masp <>'' or Masp is not null order by Masp ASC");
            //cbnhomsanpham.DisplayMember = "Masp";
            //cbnhomsanpham.ValueMember = "Masp";
        }
        private void THDanhSachVatLieu()//DANH SÁCH TÊN VẬT LIỆU
        {
            ketnoi kn = new ketnoi();
            cbTen_vatlieu.DataSource = kn.laybang("select TenVatlieu from tblVatLieuSanPham");
            cbTen_vatlieu.DisplayMember = "TenVatlieu";
            cbTen_vatlieu.ValueMember = "TenVatlieu";
        }
        private void cbnhomsanpham_SelectedIndexChanged(object sender, EventArgs e)//Truy vấn theo nhóm mã sản phẩm
        {
            //  ketnoi kn = new ketnoi();
            //  gridControl1.DataSource = kn.laybang("select Masp,Tensp,Kichthuoc,Vatlieu, "
            //+ "Dacdiem, Ngaylap, KH.MKH, KH.TenKH, sp.manv, hotennv from tblSANPHAM SP, tblKHACHHANG KH "
            //+ "where SP.Makh = KH.MKH 
            //and left(Masp,4) like N'" + cbnhomsanpham.Text+"' order by substring(Masp,6,4) ASC");
        }
        private void btnfresh_Click(object sender, EventArgs e)
        {
            UcDMSanPham_Load(sender,e);
            DocDSSanPhamTheoNgay();
        }
        private void AutoCompleteNhanVien()// ATUCOMPLETE TÊN NHÂN VIÊN
        {
            //string ConString = Connect.mConnect;
            ////string MaBP = Convert.ToString(cbMaBP.Text);
            //using (SqlConnection con = new SqlConnection(ConString))
            //{
            //    SqlCommand cmd = new SqlCommand("select HoTen from tblDSNHANVIEN where HoTen is not null", con);
            //    con.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            //    while (reader.Read())
            //    {
            //        MyCollection.Add(reader.GetString(0));
            //    }
            //    txtMember.AutoCompleteCustomSource = MyCollection;
            //    con.Close();
            //}
        }
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtmasp.ReadOnly = false;txtTenSanPham.Enabled = true;
            cbTen_vatlieu.Enabled = true;
            txtTenSanPham.Clear();     
        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string MaSP = txtmasp.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            SqlCommand cmd = new SqlCommand("select Masp from tblSANPHAM", con);
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
        private void btnthem_Click(object sender, EventArgs e)// THÊM DỮ LIỆU
        {
            if (txtmasp.Text == "" && txtTenSanPham.Text == "")
            {
                MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (kiemtratontai())
            {
                MessageBox.Show("Mã '" + txtmasp.Text + "' đã tồn tại, Không thể thêm chi tiết trùng");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into tblSANPHAM "
                           + "(Masp,Tensp,Vatlieu,Dacdiem,Ngaylap,manv,hotennv,Makh,check1,check2) "
                           + "values(@Masp,@Tensp,@Vatlieu,@Dacdiem,GetDate(),@manv,@hotennv,@Makh,@check1,@check2)", con))
                    {
                        cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                        cmd.Parameters.Add("@Tensp", SqlDbType.NVarChar).Value = txtTenSanPham.Text;
                        //cmd.Parameters.Add("@Kichthuoc", SqlDbType.NVarChar).Value = txtkichthuoc.Text;
                        cmd.Parameters.Add("@Vatlieu", SqlDbType.NVarChar).Value = cbTen_vatlieu.Text;
                        cmd.Parameters.Add("@Dacdiem", SqlDbType.NVarChar).Value = txtDacDiem.Text;
                        cmd.Parameters.Add("@manv", SqlDbType.NVarChar).Value = Login.Username;
                        cmd.Parameters.Add("@hotennv", SqlDbType.NVarChar).Value = Login.Username;
                        cmd.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = txtCodeKhachHang.Text;
                        cmd.Parameters.Add("@check1", SqlDbType.NVarChar).Value = ckQuyTrinhSanXuat.Checked;
                        cmd.Parameters.Add("@check2", SqlDbType.NVarChar).Value = ckDinhMucVatTu.Checked;
                        cmd.ExecuteNonQuery();
                    }
                    DocDSSanPhamTheoNgay();
                    con.Close();
                }
            }
        }
        private void btnsua_Click(object sender, EventArgs e)//SỬA DỮ LIỆU
        {
            if (txtmasp.Text != "" && txtTenSanPham.Text != "")
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE tblSANPHAM "
                           +" set Masp=@Masp,Tensp=@Tensp, "
                           +" Vatlieu=@Vatlieu,Dacdiem=@Dacdiem, "
                           + " Ngaylap=GetDate(),manv=@manv,hotennv=@hotennv,Makh=@Makh,check1 = @check1,check2 = @check2 where Code like '" + txtCode.Text+"'", con))
                    {
                        cmd.Parameters.Add("@Masp", SqlDbType.NVarChar).Value = txtmasp.Text;
                        cmd.Parameters.Add("@Tensp", SqlDbType.NVarChar).Value = txtTenSanPham.Text;
                        //cmd.Parameters.Add("@Kichthuoc", SqlDbType.NVarChar).Value = txtkichthuoc.Text;
                        cmd.Parameters.Add("@Vatlieu", SqlDbType.NVarChar).Value = cbTen_vatlieu.Text;
                        cmd.Parameters.Add("@Dacdiem", SqlDbType.NVarChar).Value = txtDacDiem.Text;
                        cmd.Parameters.Add("@manv", SqlDbType.NVarChar).Value = Login.Username;
                        cmd.Parameters.Add("@hotennv", SqlDbType.NVarChar).Value = Login.Username;
                        cmd.Parameters.Add("@Makh", SqlDbType.NVarChar).Value = txtCodeKhachHang.Text;
                        cmd.Parameters.Add("@check1", SqlDbType.NVarChar).Value = ckQuyTrinhSanXuat.Checked;
                        cmd.Parameters.Add("@check2", SqlDbType.NVarChar).Value = ckDinhMucVatTu.Checked;
                        cmd.ExecuteNonQuery();
                    }
                    DocDSSanPhamTheoNgay();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Cần thêm đủ nội dung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            //if (txtmasp.Text != "" && MessageBox.Show("Bạn muốn xoa " + txtmasp.Text + " có tên" + txttensp.Text + " ", "THÔNG BÁO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    ketnoi kn = new ketnoi();
            //    gridControl1.DataSource = kn.xulydulieu("delete tblSANPHAM where "
            //                    + " Code like '" + txtCode.Text + "'");
            //    cbnhomsanpham_SelectedIndexChanged(sender,e);
            //}
            int[] listRowList = this.gvDanhMucSanPham.GetSelectedRows();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvDanhMucSanPham.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"delete from tblSANPHAM where Code like '{0}'",
                 rowData["Code"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DocTatCaDSSanPham();
        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {
            THDanhSachSanPham();
        }
        private void DocTatCaDSSanPham()
        {
            ketnoi kn = new ketnoi();
            grDanhMucSanPham.DataSource = kn.laybang(@"select check1,check2,Case when ISNUMERIC (right(Masp,4)) >0 then right(Masp,4) 
                       end TT, Masp, Tensp, Kichthuoc, Vatlieu,
                       Dacdiem, Ngaylap, KH.MKH, KH.TenKH, sp.manv,
                       hotennv, Code from tblSANPHAM SP left outer join tblKHACHHANG KH
                       on SP.Makh = KH.MKH ORDER BY TT DESC");
            kn.dongketnoi();
        }
        private void DSKhachHang()
        {
            ketnoi knn = new ketnoi();
            cbTenKhachHang.DataSource = knn.laybang("select TenKH from tblKHACHHANG");
            cbTenKhachHang.ValueMember = "TenKH";
            cbTenKhachHang.DisplayMember = "TenKH";
            knn.dongketnoi();
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            txtCodeKhachHang.Clear(); txtMa_Vatlieu.Clear(); txtmasp.Clear(); txtTenSanPham.Clear(); txtDacDiem.Clear();
        }

        private void btnExportsx_Click(object sender, EventArgs e)
        {
            grDanhMucSanPham.ShowPrintPreview();
        }

        private void btnThem_Nhanvien_Click(object sender, EventArgs e)//Thêm khách hàng
        {
            frmDanhSachNV ThemNhanVien = new frmDanhSachNV();
            DialogResult result = ThemNhanVien.ShowDialog();
            if (result == DialogResult.Cancel)
            {         
                MessageBox.Show("Thêm NV đã đóng");
            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)//Thêm Nhân Viên
        {
            frmThem_KH themKhachHang = new frmThem_KH();
            themKhachHang.ShowDialog();
            DSKhachHang();
        }
        private void ThemVatlieu_Click(object sender,EventArgs e) {
            frmThemVatLieu themVatlieu = new frmThemVatLieu();
            themVatlieu.ShowDialog();
            THDanhSachSanPham();
        }

      
        private void cbTen_vatlieu_SelectedIndexChanged_1(object sender, EventArgs e)//CHỌN MÃ VẬT LIỆU THEO TÊN VẬT LIỆU
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("select top 1 MaVatLieu from tblVatLieuSanPham where TenVatlieu like N'" + cbTen_vatlieu.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMa_Vatlieu.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void HAPMASP()
        {  string APMASP=string.Format(@"{0}-{1}-{2}",this.txtCodeKhachHang.Text, this.txtMa_Vatlieu.Text, this.txtSoThuTu.Text);
            txtmasp.Text = APMASP.ToString();
        }
        private void SUKIENAPMA(object Sender,EventArgs e) { HAPMASP();  }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            Gol = gvDanhMucSanPham.GetFocusedDisplayText();
            txtmasp.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Masp_grid);
            txtTenSanPham.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Tensp_grid);
            cbTen_vatlieu.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Vatlieu_grid);
            txtDacDiem.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Dacdiem_grid);
            dpNgayLap.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Ngaylap_grid);
            cbTenKhachHang.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Tenkh_grid1);
            txtCodeKhachHang.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Makh_grid1);
            txtCode.Text = gvDanhMucSanPham.GetFocusedRowCellDisplayText(Code_grid);
            if (gvDanhMucSanPham.GetFocusedRowCellDisplayText(colQuyTrinh).ToString()== "Checked") 
            {
                ckQuyTrinhSanXuat.Checked = true;
            }
            else
            {
                ckQuyTrinhSanXuat.Checked = false;
            }
            if (gvDanhMucSanPham.GetFocusedRowCellDisplayText(colDinhMuc)== "Checked")
            {
                ckDinhMucVatTu.Checked = true;
            }
            else
            {
                ckDinhMucVatTu.Checked = false;
            }
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            Path path = new Path();
            frmLoading f2 =
            new frmLoading(txtmasp.Text, path.pathbanve);
            f2.Show();
        }
        private void SukienGoiMASP_Click(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            Path path = new Path();
            frmLoading f2 = 
            new frmLoading(txtmasp.Text, path.pathbanve);
            f2.Show();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            grDanhMucSanPham.DataSource = kn.laybang("select check1,check2,Masp,Tensp,Kichthuoc,Vatlieu, "
          + " Dacdiem, Ngaylap, KH.MKH, KH.TenKH, sp.manv, hotennv, Code from tblSANPHAM SP left outer join tblKHACHHANG KH "
          + " on SP.Makh = KH.MKH where convert(date,Ngaylap,103) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //FTPclient client = new FTPclient("192.168.1.22", "ftpPublic", "ftp#1234");
            //FtpClientForm ftp = new FtpClientForm();
            //ftp.SetFtpClient(client);
            //ftp.Show();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //frmDinhMucVatLieu dinhMucVatLieu = new frmDinhMucVatLieu
            //    (gridView1.GetFocusedRowCellDisplayText(Masp_grid),
            //    gridView1.GetFocusedRowCellDisplayText(Tensp_grid), 
            //   txtCode.Text,txtMember.Text);
            //dinhMucVatLieu.ShowDialog();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void Code_Click(object sender, EventArgs e)
        {

        }
    }
}
