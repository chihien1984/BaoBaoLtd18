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

namespace quanlysanxuat
{
    public partial class UCz_doitongtaphopchiphi : UserControl
    {
        public UCz_doitongtaphopchiphi()
        {
            InitializeComponent();
        }
        private void Tinhthanhtien()//Hàm tính don gia su dung vat lieu
        {
            try
            {
                double Quicachvl = double.Parse(txtQuicachvt.Text);
                double Dongiavatlieu = double.Parse(txtDongia.Text);
                double Thanhtien = Quicachvl*Dongiavatlieu;
                txtDongmotsanpham.Text = Convert.ToString(Thanhtien);
            }
            catch (Exception)
            { }
        }
        private void list_dmchiphi()//Danh sách z khởi tạo trọng tháng
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select cp.*,kb.donvi from tblz_dmchiphi cp left outer join tblz_khaibao kb on cp.id_z=kb.maz where convert(date,cp.ngaylap,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' "
            +" and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void list_dmchiphiThem()//Danh sách khởi tạo z theo mã z thêm vào
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select cp.*,kb.donvi from tblz_dmchiphi cp left outer join tblz_khaibao kb on cp.id_z=kb.maz where cp.id_z like N'" + txtMaz.Text + "'");
            kn.dongketnoi();
        }
        private void list_dmchiphiSua()//Danh sách khởi tạo theo id z sửa
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select cp.*,kb.donvi from tblz_dmchiphi cp left outer join tblz_khaibao kb on cp.id_z=kb.maz where cp.idcp like N'" + txtid.Text + "'");
            kn.dongketnoi();
        }
        private void List_zkhaibao(object sender, EventArgs e)//Sự kiện load danh sách khởi tạo z
        {
            list_dmchiphi();
        }
        private void Them(object sender, EventArgs e)// Thêm 
        {
            try
            {
                if (cbMacp.Text == "") { MessageBox.Show("Mã không bỏ trống"); return ; }
                else if (txtTenz.Text == "") { MessageBox.Show("Tên tính giá không bỏ trống", "THÔNG BÁO"); return; }
                else if (txtKyCP.Text == "") { MessageBox.Show("Kỳ chi phí không bỏ trống", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_dmchiphi(id_z,masp,tenloai,Macp,Noidungcp, "
                    + " thanhtiensanpham,nguoilap,ngaylap,Kytinhgia,id_kycp,Madongiavl) "
                    +" values(@id_z,@masp,@tenloai,@Macp,@Noidungcp, "
                    + " @thanhtiensanpham,@nguoilap,GetDate(),@Kytinhgia,@id_kycp,@Madongiavl) ", cn);
                    cmd.Parameters.Add(new SqlParameter("@id_z", SqlDbType.NVarChar)).Value = txtMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenloai", SqlDbType.NVarChar)).Value = txtTenz.Text;
                    cmd.Parameters.Add(new SqlParameter("@Macp", SqlDbType.NVarChar)).Value = cbMacp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noidungcp", SqlDbType.NVarChar)).Value = txtNoidungcp.Text;
                    cmd.Parameters.Add(new SqlParameter("@thanhtiensanpham", SqlDbType.Float)).Value = txtDongmotsanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Kytinhgia", SqlDbType.NVarChar)).Value = txtKyCP.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_kycp", SqlDbType.NVarChar)).Value = txtidky.Text;
                    cmd.Parameters.Add(new SqlParameter("@Madongiavl", SqlDbType.NVarChar)).Value = glMavatlieu.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_dmchiphiThem();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm không thành công", "thông báo");
            }
        }
        private void Sua(object sender, EventArgs e)// Sửa
        {
            try
            {
                if (cbMacp.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenz.Text == "") { MessageBox.Show("Tên vật tư", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblz_dmchiphi set id_z=@id_z,masp=@masp,tenloai=@tenloai,Macp=@Macp,Noidungcp=@Noidungcp, "
                          +" thanhtiensanpham=@thanhtiensanpham,nguoilap=@nguoilap,ngaylap=GetDate(),Kytinhgia=@Kytinhgia,id_kycp=@id_kycp, "
                          +" Madongiavl=@Madongiavl where idcp like '" + txtid.Text + "'", cn);
                    cmd.Parameters.Add(new SqlParameter("@id_z", SqlDbType.NVarChar)).Value = txtMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenloai", SqlDbType.NVarChar)).Value = txtTenz.Text;
                    cmd.Parameters.Add(new SqlParameter("@Macp", SqlDbType.NVarChar)).Value = cbMacp.Text;
                    cmd.Parameters.Add(new SqlParameter("@Noidungcp", SqlDbType.NVarChar)).Value = txtNoidungcp.Text;
                    cmd.Parameters.Add(new SqlParameter("@thanhtiensanpham", SqlDbType.Float)).Value = txtDongmotsanpham.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@Kytinhgia", SqlDbType.NVarChar)).Value = txtKyCP.Text;
                    cmd.Parameters.Add(new SqlParameter("@id_kycp", SqlDbType.BigInt)).Value = txtidky.Text;
                    cmd.Parameters.Add(new SqlParameter("@Madongiavl", SqlDbType.NVarChar)).Value = glMavatlieu.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_dmchiphiSua();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sửa không thành công", "thông báo");
            }
        }
        private void Xoa(object sender, EventArgs e)// Xóa 
        {
            try
            {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from tblz_dmchiphi where idcp like '" + txtid.Text + "'", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_dmchiphi();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa không thành công", "thông báo");
            }
        }
        private void Binding(object sender, EventArgs e)//Bing từ grid lên textbox
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(idcp_grid2);
            txtMaz.Text = gridView2.GetFocusedRowCellDisplayText(id_z_grid2);
            cbMacp.Text=gridView2.GetFocusedRowCellDisplayText(macp_grid2);
            txtTenz.Text=gridView2.GetFocusedRowCellDisplayText(tenloai_grid2);
            txtNoidungcp.Text=gridView2.GetFocusedRowCellDisplayText(noidung_grid2);
            txtMasp.Text=gridView2.GetFocusedRowCellDisplayText(masp_grid2);
            txtDongmotsanpham.Text=gridView2.GetFocusedRowCellDisplayText(dongsanpham_grid2);
            txtKyCP.Text = gridView2.GetFocusedRowCellDisplayText(kyz_gl);
            txtidky.Text = gridView2.GetFocusedRowCellDisplayText(idkycp_grid2);
            glKychiphi.Text = gridView2.GetFocusedRowCellDisplayText(tenloai_grid2);
        }
        private void LoadTenz()//Load Tên theo mã Z
        {
            try
            {
                SqlConnection con = new SqlConnection(Connect.mConnect);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select kb.tenz,kb.masp,cp.Kytinhgia from tblz_khaibao kb "
                                    +"left outer join tblz_doitongtaphopchiphi cp on cp.Maz=kb.maz where maz like N'" + txtMaz.Text + "' ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTenz.Text = Convert.ToString(reader[0]);
                    txtMasp.Text = Convert.ToString(reader[1]);
                    txtKyCP.Text = Convert.ToString(reader[2]);
                }
                con.Close();
            }
            catch (Exception) { };
        }
        private void cbMaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTenz();
        }
        private void CbListMaZ()
        {
            //ketnoi kn = new ketnoi();
            //txtMaz.DataSource = kn.laybang("select maz from tblz_khaibao");
            //txtMaz.DisplayMember = "maz";
            //txtMaz.ValueMember = "maz";
            //kn.dongketnoi();
        }
        private void AddCode_Z()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Top 1 'PN '+REPLACE(convert(nvarchar,GetDate(),11),'/','') "
               + " +'-'+convert(nvarchar,(DATEPART(HH,GetDate())))+':'+convert(nvarchar,DATEPART(MI,GetDate()))", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtTenz.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnZkhoitao_Click(object sender, EventArgs e)
        {
            frmZ_Doituong fZDoituong = new frmZ_Doituong();
            fZDoituong.ShowDialog();
            CbListMaZ();
        }
        private void GlEditDoiTuongTinhGia()//Danh mục đối tượng tính z
        {
            ketnoi Connect = new ketnoi();
            glKychiphi.Properties.DataSource = Connect.laybang("select cp.id,kb.maz,masp,kb.tenz,cp.Kytinhgia from tblz_khaibao kb "
            +" left outer join tblz_doitongtaphopchiphi cp on cp.Maz=kb.maz");
            glKychiphi.Properties.DisplayMember = "tenz";
            glKychiphi.Properties.ValueMember = "tenz";
            glKychiphi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glKychiphi.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            glKychiphi.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void BindingLookup()//Chọn Mã chi phí từ 
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMaz.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Maz_gl);
            txtTenz.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tenz_gl);
            txtKyCP.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(kyz_gl);
            txtMasp.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(masp_gl);
            txtidky.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(id_gl);
        }
        private void glKychiphi_EditValueChanged(object sender, EventArgs e)//Chọn danh mục kỳ chi phí
        {
            BindingLookup();
        }
        private void btnCapNhatCP_Click(object sender, EventArgs e)//Tổng hợp chi tiết chi phí update lên Đối tượng từng kỳ chi phí
        {
            try
            {
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                SqlCommand cmd = new SqlCommand("Z_UpdGiaThanh", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idcp", SqlDbType.BigInt)).Value = txtidky.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Tổng hợp chi phí thành công", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Không thành công","Thông báo");
            }
    
        }
        private void ReadonlyNoidung()//Mở nội dung chi phí
        {
            if (glKychiphi.Text !="")
            {
                txtNoidungcp.ReadOnly = false;
            }
        }
        private void AutoNoidungChiPhi()// Autocomplete noi dung chi phi đã nhập
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select Noidungcp from tblz_dmchiphi", con);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtNoidungcp.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void GlDMGianguyenlieu()//Danh mục định mức giá vật liệu sử dụng
        {
            ketnoi Connect = new ketnoi();
            glMavatlieu.Properties.DataSource = Connect.laybang("select * from tblz_danhmucgiavatlieu");
            glMavatlieu.Properties.DisplayMember = "Madongiavl";
            glMavatlieu.Properties.ValueMember = "Madongiavl";
            glMavatlieu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glMavatlieu.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            glMavatlieu.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void txtDonvia_TextChanged(object sender, EventArgs e)//Tinh tien khi nhap lieu
        {
            Tinhthanhtien();
        }
        private void txtQuicachvt_TextChanged(object sender, EventArgs e)
        {
            Tinhthanhtien();
        }
        private void glMavatlieu_EditValueChanged(object sender, EventArgs e)//Binding so tien theo ma vat lieu su dung
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtDongia.Text = gridView1.GetFocusedRowCellDisplayText(Dongia_gl);
            txtNoidungcp.Text = gridView1.GetFocusedRowCellDisplayText(Tendongiavl_gl);
        }
        private void btnDMGiaVatlieu_Click(object sender, EventArgs e)//Goi form them vat tu su dung tinh z
        {
            frmz_DmNguyenlieuchinh fDMGiaNguyenlieu = new frmz_DmNguyenlieuchinh();
            fDMGiaNguyenlieu.ShowDialog();
            GlDMGianguyenlieu();

        }
       
        public static string Username = "";
        private void UCz_doitongtaphopchiphi_Load(object sender, EventArgs e)// from load
        {
            AutoNoidungChiPhi();
            GlDMGianguyenlieu();
            ReadonlyNoidung();
            GlEditDoiTuongTinhGia();
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Username;
        }

        private void glKychiphi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbMacp.Focus();
            }
        }
        private void cbMacp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                glMavatlieu.Focus();
            }
        }
        private void glMavatlieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNoidungcp.Focus();
            }
        }

        private void txtNoidungcp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDongia.Focus();
            }
        }

        private void txtDongia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQuicachvt.Focus();
            }
        }

        private void txtQuicachvt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Focus();
            }
        }

        private void btnNkchiphi_Click(object sender, EventArgs e)
        {
            frmz_Taphopchiphi fNKTapHopCP = new frmz_Taphopchiphi();
            fNKTapHopCP.ShowDialog();
        }

     
    }
}
