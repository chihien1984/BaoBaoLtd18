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
using DevExpress.XtraReports.UI;

namespace quanlysanxuat
{
    public partial class UCXuatCCDC : DevExpress.XtraEditors.XtraForm
    {
        public UCXuatCCDC()
        {
            InitializeComponent();
        }
        public static string Username;
        private void UcXUAT_VATTU_Load(object sender, EventArgs e)
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            GridLookupTonKho();
            DSXuatKhoCCDCTheoNgay();
            DSBoPhanNhanVatTu();
            DocNguoiNhanTheoBoPhan();
            AutoNguoNhan();
        }
        private void XuatKho_Click(object sender, EventArgs e)
        {
            ListXuatKho();
        }
        private async void ListXuatKho()//Danh mục Xuất kho
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select row_number() 
                OVER (PARTITION BY Maphieuxuat order by (select 1)) as STT,x.*,
                case when TenVatLieuPhu<>'' 
                then TenVatLieuPhu else TenVatLieu end Tenvlphu
                from tblXUAT_VATLIEUPHU x left outer join 
                (select Tenvlphu TenVatLieu,Mavlphu from tblDM_VATLIEUPHU) b on x.Mavlphu=b.Mavlphu  
                 order by Ngaylap desc,SUBSTRING(Maphieuxuat,4,6) ASC,SUBSTRING(Maphieuxuat,11,3) ASC");
                Invoke((Action)(() => {
                    grSoXuatKhoVatLieuPhu.DataSource =
                    Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private async void DSXuatKhoCCDCTheoNgay()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select row_number() OVER 
                        (PARTITION BY Maphieuxuat order by (select 1)) as STT,x.*,
                        case when TenVatLieuPhu<>'' 
                        then TenVatLieuPhu else TenVatLieu end Tenvlphu
                        from tblXUAT_VATLIEUPHU x left outer join 
                        (select Tenvlphu TenVatLieu,Mavlphu from tblDM_VATLIEUPHU) b 
                        on x.Mavlphu=b.Mavlphu where X.Ngaylap 
                        between '{0}' and '{1}' order by Ngaylap desc,SUBSTRING(Maphieuxuat,4,6) ASC,
                        SUBSTRING(Maphieuxuat,11,3) ASC",
                      dptu_ngay.Value.ToString("yyyy-MM-dd"),
                      dpden_ngay.Value.ToString("yyyy-MM-dd"));
                Invoke((Action)(() => {
                    grSoXuatKhoVatLieuPhu.DataSource =
                    Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void btnDocXuatCCDCTheoNgay_Click(object sender, EventArgs e)
        {
            DSXuatKhoCCDCTheoNgay();
        }
        private void GridLookupTonKho()//Danh mục hàng tồn trong kho
        {
            ketnoi Connect = new ketnoi();
            gridLookUpEditVatTu.Properties.DataSource = Connect.laybang("select * from tblDM_VATLIEUPHU");
            gridLookUpEditVatTu.Properties.DisplayMember = "Mavlphu";
            gridLookUpEditVatTu.Properties.ValueMember = "Mavlphu";
            gridLookUpEditVatTu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            gridLookUpEditVatTu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            gridLookUpEditVatTu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        string iD;
        private void Binding_XuatKho(object sender, EventArgs e)//Binding xuất vật tư
        {
            string Gol = "";
            Gol = gvSoXuatKhoVatLieuPhu.GetFocusedDisplayText();
            iD = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(id_grid2);
            txtMaxuat.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Maphieuxuat_grid2);
            txtMavatlieu.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Mavlphu_grid2);
            dpNgayLap.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Ngaylap_grid2);
            txtTenVatLieu.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Tenvatlieu_grid2);
            txtDienGiai.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Diengiai_grid2);
            txtSoxuat.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Soluong_grid2);
            txtDonvi.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Donvi_grid2);
            txtNguoiNhan.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Nguoinhan_grid2);
            cbBoPhan.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(Noinhan_grid2);
            txtLyDoXuatKho.Text = gvSoXuatKhoVatLieuPhu.GetFocusedRowCellDisplayText(colLyDoXuatKho);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }
        private void BindingEditMavatTu(object sender, EventArgs e)//Binding tên vật tư theo mã vật tư
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMavatlieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Mavlphu_gl);
            txtTenVatLieu.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenvlphu_gl);
            NhapXuatTon();
            DMNhapXuatTon();
            travezero();
            TruTonKho();
        }

        private void LayMaPhieuXuat(object sender, EventArgs e)//Lấy Mã Phiếu Xuất kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select Top 1 'PX '+REPLACE(convert(nvarchar,GetDate(),11),'/','') 
                +'-'+convert(nvarchar,(DATEPART(HH,GetDate())))+':'+convert(nvarchar,DATEPART(MI,GetDate()))");
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaxuat.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        
        // Cập nhật dữ liệu tồn kho cuối kỳ theo từng mã hàng/ không sử dụng cập nhật tồn kho cuối kỳ toàn bộ
        private void UpdateTonKho()//Cập nhật dữ liệu tồn kho
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"update tblDM_VATLIEUPHU set Toncuoi = '{0}' where Mavlphu like N'{1}'",
                txtTonCuoi.Text,
                txtMavatlieu.Text);
            int kq = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
        }
        private void Them(object sender, EventArgs e) //Ghi dữ liệu xuất kho
        {
            try
            {
                if (txtMavatlieu.Text == "")
                {
                    MessageBox.Show("Mã vật liệu rỗng","Thông báo"); 
                    return;
                }
                else if (txtTenVatLieu.Text == "")
                {
                    MessageBox.Show("Tên vật liệu rỗng","Thông báo");
                    return;
                }
                else if (txtMaxuat.Text == "") 
                {
                    MessageBox.Show("Ma phiếu xuất rỗng", "Thông báo");
                    return; 
                }
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into tblXUAT_VATLIEUPHU "
                    + " (Maphieuxuat,Ngaylap,Mavlphu,Soluong, "
                    + " Donvi,Noinhan,Nguoinhan,Nguoilap,Diengiai,Ngayghi,TenVatLieuPhu,LyDoXuatKho) "
                    + " values(@Maphieuxuat,@Ngaylap,@Mavlphu,@Soluong, "
                    + " @Donvi,@Noinhan,@Nguoinhan,@Nguoilap,@Diengiai,GetDate(),@TenVatLieuPhu,@LyDoXuatKho)", con);
                    cmd.Parameters.Add(new SqlParameter("@Maphieuxuat", SqlDbType.NVarChar)).Value = txtMaxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSoxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = cbBoPhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = MainDev.username;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenVatLieuPhu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@LyDoXuatKho", SqlDbType.NVarChar)).Value = txtLyDoXuatKho.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grSoXuatKhoVatLieuPhu.DataSource = dt;
                    UpdateXuatKho();//group số lượng xuất kho                   
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    DSXuatKhoCCDCTheoNgay();//Load lại danh mục nhập kho
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông báo");
            }
        }

        private void Sua(object sender, EventArgs e)//Sửa dữ liệu xuất kho
        {
            try
            {
                if (txtMavatlieu.Text != "" && txtTenVatLieu.Text != "" && txtMaxuat.Text != "")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = Connect.mConnect;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("update tblXUAT_VATLIEUPHU "
                    + "set Maphieuxuat=@Maphieuxuat,Ngaylap=@Ngaylap,Mavlphu=@Mavlphu,Soluong=@Soluong, "
                    + " Donvi=@Donvi,Noinhan=@Noinhan,Nguoinhan=@Nguoinhan,Nguoilap=@Nguoilap,Diengiai=@Diengiai,Ngayghi=GetDate(),TenVatLieuPhu=@TenVatLieuPhu,LyDoXuatKho=@LyDoXuatKho where id like @id", con);
                    cmd.Parameters.Add(new SqlParameter("@Ngaylap", SqlDbType.Date)).Value = dpNgayLap.Text;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = iD;
                    cmd.Parameters.Add(new SqlParameter("@Maphieuxuat", SqlDbType.NVarChar)).Value = txtMaxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Mavlphu", SqlDbType.NVarChar)).Value = txtMavatlieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@Soluong", SqlDbType.Float)).Value = txtSoxuat.Text;
                    cmd.Parameters.Add(new SqlParameter("@Donvi", SqlDbType.NVarChar)).Value = txtDonvi.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noinhan", SqlDbType.NVarChar)).Value = cbBoPhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoinhan", SqlDbType.NVarChar)).Value = txtNguoiNhan.Text;
                    cmd.Parameters.Add(new SqlParameter("@Nguoilap", SqlDbType.NVarChar)).Value = MainDev.username;
                    cmd.Parameters.Add(new SqlParameter("@Diengiai", SqlDbType.NVarChar)).Value = txtDienGiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenVatLieuPhu", SqlDbType.NVarChar)).Value = txtTenVatLieu.Text;
                    cmd.Parameters.Add(new SqlParameter("@LyDoXuatKho", SqlDbType.NVarChar)).Value = txtLyDoXuatKho.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grSoXuatKhoVatLieuPhu.DataSource = dt;
                    UpdateXuatKho();//group số lượng Xuất kho                   
                    GridLookupTonKho();//Load danh mục xuất nhập tồn
                    DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
                    TruTonKho();//Trừ lấy số tồn kho
                    DSXuatKhoCCDCTheoNgay();//Load lại danh mục xuất vật tư phụ
                    UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
                }
            }
            catch
            {
                MessageBox.Show("Không thành công", "Thông báo");
            }
        }

        private void Xoa(object sender, EventArgs e)// Xóa dữ liệu xuất kho
        {
            UpdateZero();//Trả giá trị xuất hàng hóa về 0
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"delete from tblXUAT_VATLIEUPHU  where id like '{0}'", iD);
            grSoXuatKhoVatLieuPhu.DataSource = kn.xulydulieu(sqlQuery);
            kn.dongketnoi();
            ListXuatKho();
            UpdateXuatKho();//group số lượng nhập kho
            GridLookupTonKho();//Load danh mục xuất nhập tồn
            DMNhapXuatTon();//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
            TruTonKho();//Trừ lấy số tồn kho
            UpdateTonKho();//Cập nhật tồn kho vào danh mục xuất nhập tồn
        }
        private void UpdateZero()//Trả giá trị xuất hàng hóa về 0
        {
            ketnoi kn = new ketnoi();
            int kq = kn.xulydulieu("update tblDM_VATLIEUPHU set TongXuat=0 where Mavlphu like N'" + txtMavatlieu.Text + "' ");
            kn.dongketnoi();
        }
        private void Export(object sender, EventArgs e)//Xuất dữ liệu xuất kho
        {
            grSoXuatKhoVatLieuPhu.ShowPrintPreview();
        }

        private void Clear(object sender, EventArgs e)//Xóa dữ liệu trong các ô textbox
        {}
       

        private void LapPhieuXuat(object sender, EventArgs e)//print phieu xuat kho
        {
            DataTable dt = new DataTable();
            ketnoi kn = new ketnoi();
            dt = kn.laybang("select * from ViewXuatKho_VTPhu where Maphieuxuat like N'" + txtMaxuat.Text + "'");
            XRXuatvatlieuphu XuatKhophu = new XRXuatvatlieuphu();
            XuatKhophu.DataSource = dt;
            XuatKhophu.DataMember = "Table";
            XuatKhophu.ShowPreviewDialog();
            kn.dongketnoi();
        }

        private void UpdateXuatKho()//Group mã đã xuất kho
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateXuat_VatTuphu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            { }
        }

        private void TruTonKho()//Hàm tính tồn kho
        {
            try
            {
                double TonDau = double.Parse(txtTonDau.Text);
                double Nhap = double.Parse(txtNhap.Text);
                double Xuat = double.Parse(txtXuat.Text);
                double TonCuoi = TonDau + Nhap - Xuat;
                txtTonCuoi.Text = Convert.ToString(TonCuoi);
            }
            catch (Exception)
            { }
        }

        private void travezero()//Đưa giá trị Zezo vào các textbox
        {
            if (txtTonDau.Text == "")
            {
                txtTonDau.Text = "0";
            }
            if (txtNhap.Text == "")
            {
                txtNhap.Text = "0";
            }
            if (txtXuat.Text == "")
            {
                txtXuat.Text = "0";
            }
        }

        private void NhapXuatTon()//Nếu mã vật liệu rỗng tồn đầu nhập xuất tồn cũng rỗng
        {
            if (txtMavatlieu.Text == "")
            {
                txtTonDau.Text = "0";
                txtNhap.Text = "0";
                txtXuat.Text = "0";
                txtTonCuoi.Text = "0";
            }
        }

        private void DMNhapXuatTon()//Lấy số lượng nhập xuất tồn từ Danh mục vật tư
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select isnull(Soluong,0),isnull(TongNhap,0),isnull(TongXuat,0),isnull(Toncuoi,0) from tblDM_VATLIEUPHU where Mavlphu like N'" + txtMavatlieu.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTonDau.Text = Convert.ToString(reader[0]);
                    txtNhap.Text = Convert.ToString(reader[1]);
                    txtXuat.Text = Convert.ToString(reader[2]);
                    txtTonCuoi.Text = Convert.ToString(reader[3]);
                }
                con.Close();
            }
            catch { };
        }

        private void Mavattu_Change(object sender, EventArgs e)//Thay đổi Mã vật tư
        {
            NhapXuatTon(); DMNhapXuatTon(); travezero(); TruTonKho();
        }

        private void UpdateTonCuoi(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                SqlCommand queryCM = new SqlCommand("UpdateTonCuoi_VatTuphu", cn);
                queryCM.CommandType = CommandType.StoredProcedure;
                cn.Open();
                queryCM.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Trừ tồn thành công");
            }
            catch (Exception)
            { MessageBox.Show("Trừ tồn không thành công"); }
        }

        private void btnDMVatlieuPhu(object sender, EventArgs e)//Mở danh mục vật liệu phụ add thông tin
        {
            frmDM_VATLIEUPHU fDMVatTu = new frmDM_VATLIEUPHU();
            fDMVatTu.ShowDialog(); 
            GridLookupTonKho();
        }
        private void AutoNguoNhan()// Autocomplete người nhận từ dữ liệu đã nhập
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select Nguoinhan from tblXUAT_VATLIEUPHU where Nguoinhan  <>''", con);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtNguoiNhan.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void AutoToNhan()//Autocomplete Nơi nhận từ dữ liệu đã nhập
        {
            string ConString = Connect.mConnect;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("select Noinhan from tblXUAT_VATLIEUPHU where Noinhan <>''", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                cbBoPhan.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }

        private void btnDMVatTuphu_Click(object sender, EventArgs e)
        {
            frmDM_VATLIEUPHU fVatLieuPhu = new frmDM_VATLIEUPHU();
            fVatLieuPhu.ShowDialog();
            GridLookupTonKho();
        }

        private void btnXuatvlphu_Click(object sender, EventArgs e)
        {
            gvSoXuatKhoVatLieuPhu.ShowPrintPreview();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GridLookupTonKho();
        }

        private void btnThemBoPhanNhanVatTu_Click(object sender, EventArgs e)
        {
            frmDMBoPhanNhanVatTu dMBoPhanNhanVatTu = new frmDMBoPhanNhanVatTu();
            dMBoPhanNhanVatTu.ShowDialog();
            DSBoPhanNhanVatTu();
        }
        private void DocNguoiNhanTheoBoPhan()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select NguoiDaiDien from 
                DSBoPhanNhanVatTu where TenBoPhanNhan like N'" + cbBoPhan.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtNguoiNhan.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void DSBoPhanNhanVatTu()
        {
            ketnoi kn = new ketnoi();
            cbBoPhan.DataSource = kn.laybang("select TenBoPhanNhan from DSBoPhanNhanVatTu");
            cbBoPhan.ValueMember = "TenBoPhanNhan";
            cbBoPhan.DisplayMember = "TenBoPhanNhan";
        }

        private void cbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocNguoiNhanTheoBoPhan();
        }
    }
}
