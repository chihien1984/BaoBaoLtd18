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

namespace quanlysanxuat
{
    public partial class frmZ_Doituong : DevExpress.XtraEditors.XtraForm
    {
        public frmZ_Doituong()
        {
            InitializeComponent();
        }
        private void list_zkhaibao()//Danh sách z khởi tạo trọng tháng
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khaibao where convert(date,ngay,101) between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "'");
            kn.dongketnoi();
        }
        private void list_zkhaibaothem()//Danh sách khởi tạo z theo mã z thêm vào
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khaibao where maz like N'"+txtMaz.Text+"'");
            kn.dongketnoi();
        }
        private void list_zkhaibaosua()//Danh sách khởi tạo theo id z sửa
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang("select * from tblz_khaibao where id like N'" + txtid.Text + "'");
            kn.dongketnoi();
        }
        private void list_kytinhgia()//Danh sách tổng hợp kỳ đối tượng chi phí
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select KyCP.*,kb.masp,kb.tenz from tblz_doitongtaphopchiphi KyCP "
            +" left outer join tblz_khaibao kb on KyCP.Maz = kb.maz");
            kn.dongketnoi();
        }
        private void List_Kygia(object sender, EventArgs e) //Show kỳ chi phí
        { 
            list_kytinhgia(); 
        }
        private void list_kytinhgiaMaz()//Danh sách tổng hợp kỳ đối tượng chi phí thao Mã z
        {
            ketnoi kn = new ketnoi();
            gridControl1.DataSource = kn.laybang("select KyCP.*,kb.masp,kb.tenz from tblz_doitongtaphopchiphi KyCP "
            + " left outer join tblz_khaibao kb on KyCP.Maz = kb.maz where KyCP.Maz like N'" + txtMaz.Text + "'");
            kn.dongketnoi();
        }
        private void Themkytinhgia()//add Mã z vào list kỳ tính giá
        {
            try
            {
                if (txtMaz.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtKychiphi.Text == "") { MessageBox.Show("Sản phẩm tính giá", "THÔNG BÁO");return;}
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_doitongtaphopchiphi (Maz,Kytinhgia,ngaylap,nguoilap,idz)"
                    + " values(@Maz,@Kytinhgia,GetDate(),@nguoilap,@idz)", cn);
                    cmd.Parameters.Add(new SqlParameter("@maz", SqlDbType.NVarChar)).Value = txtMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@Kytinhgia", SqlDbType.NVarChar)).Value = txtKychiphi.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.Parameters.Add(new SqlParameter("@idz", SqlDbType.BigInt)).Value = txtid.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_kytinhgiaMaz();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm kỳ chi phí không thành công", "thông báo");
            }
        }
        private void btnChuyenkytinhgia_Click(object sender, EventArgs e)
        {
            Addkychiphi(); Themkytinhgia();
        }
        private void List_zkhaibao(object sender,EventArgs e)//Sự kiện load danh sách khởi tạo z
        {
            list_zkhaibao();
        }
        private void Them(object sender,EventArgs e)// Thêm 
        {
            try
            {
                if (txtMaz.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenz.Text == "") { MessageBox.Show("Sản phẩm tính giá", "THÔNG BÁO"); return; }
                else if (kiemtratontai()) { MessageBox.Show("Sản phẩm tồn tại", "THÔNG BÁO"); return;}
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblz_khaibao (maz,tenz,donvi,masp,ngay,nguoilap)"
                    + " values(@maz,@tenz,@donvi,@masp,GetDate(),@nguoilap)", cn);
                    cmd.Parameters.Add(new SqlParameter("@maz", SqlDbType.NVarChar)).Value = txtMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenz", SqlDbType.NVarChar)).Value = txtTenz.Text;
                    cmd.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonviz.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = glMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_zkhaibaothem();Binding(sender, e);
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
                if (txtMaz.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenz.Text == "") { MessageBox.Show("Tên vật tư", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update tblz_khaibao set maz=@maz,tenz=@tenz,donvi=@donvi,masp=@masp,ngay=GetDate() "
                    +" ,nguoilap=@nguoilap where id like '" + txtid.Text + "'", cn);
                    cmd.Parameters.Add(new SqlParameter("@maz", SqlDbType.NVarChar)).Value = txtMaz.Text;
                    cmd.Parameters.Add(new SqlParameter("@tenz", SqlDbType.NVarChar)).Value = txtTenz.Text;
                    cmd.Parameters.Add(new SqlParameter("@donvi", SqlDbType.NVarChar)).Value = txtdonviz.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = glMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtUser.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_zkhaibaothem();
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
                if (txtMaz.Text == "") { MessageBox.Show("Mã không bỏ trống"); return; }
                else if (txtTenz.Text == "") { MessageBox.Show("Tên khong duoc bo trong", "THÔNG BÁO"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from tblz_khaibao where id like '"+txtid.Text+"'", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_zkhaibao();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa không thành công", "thông báo");
            }
        }
        private void Binding(object  sender,EventArgs e)//Bing từ grid lên textbox
        {
            string Gol = "";
            Gol = gridView2.GetFocusedDisplayText();
            txtid.Text = gridView2.GetFocusedRowCellDisplayText(id_grid2);
            txtMaz.Text = gridView2.GetFocusedRowCellDisplayText(Maz_grid2);
            txtTenz.Text = gridView2.GetFocusedRowCellDisplayText(TenloaiZ_grid2);
            txtdonviz.Text = gridView2.GetFocusedRowCellDisplayText(Donvi_grid2);
            glMasp.Text = gridView2.GetFocusedRowCellDisplayText(Masp_grid2);
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView1.GetFocusedDisplayText();
            txtidtonghopcp.Text = gridView1.GetFocusedRowCellDisplayText(id_grid1);
            txtMaz.Text = gridView1.GetFocusedRowCellDisplayText(Maz_grid1);

        }
        private bool kiemtratontai()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string Maz = txtMaz.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select maz from tblz_khaibao", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Maz == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }


        private void AddCode_Z()//Lấy mã phiếu nhập kho
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("SELECT 'SP'+CONCAT('',RIGHT(CONCAT('00000',ISNULL(right(max(Maz),5),0) + 1),5)) from tblz_doitongtaphopchiphi", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtMaz.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        private void GridEditSanPham()//Danh mục xuất nhập tồn kho vật tư
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            glMasp.Properties.DataSource = Connect.laybang("select Masp,Tensp,Makh,TenKH from tblSANPHAM SP left join tblKHACHHANG KH on SP.Makh=KH.MKH where Tensp is not null and Tensp <>'' order by Makh ASC");
            glMasp.Properties.DisplayMember = "Masp";
            glMasp.Properties.ValueMember = "Masp";
            glMasp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            glMasp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            glMasp.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        private void glMasp_EditValueChanged(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridView3.GetFocusedDisplayText();
            txtTenz.Text = gridView3.GetFocusedRowCellDisplayText(Tensp_look);
        }
        private void AutoSanPhamGiaThanh()// Autocomplete tenz đã nhập
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select tenz from tblz_khaibao", con);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtTenz.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void btnMaZ_Click(object sender, EventArgs e)
        {
            AddCode_Z();
        }
        private void Addkychiphi()//Lấy KỲ TÍNH GIÁ
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select Top 1 REPLACE(convert(nvarchar,GetDate(),11),'/','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtKychiphi.Text = Convert.ToString(reader[0]);
            reader.Close();
        }

        private void btnKychiphi_Click(object sender, EventArgs e)//ADD KỲ TÍNH GIÁ VÀO BẢNG TÍNH GIÁ
        {
            Addkychiphi();
        }
        private void btnxoadoituongz_Click(object sender, EventArgs e)
        {
            try
            {
                if (kiemtraCP()) { MessageBox.Show("Mã đã tính chi phí không xóa"); return;}
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from tblz_doitongtaphopchiphi where id like '" + txtidtonghopcp.Text + "'", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    list_kytinhgia();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa không thành công", "thông báo");
            }
        }
        private bool kiemtraCP()// kiểm tra tồn tại mã
        {
            bool tatkt = false;
            string Maz = txtMaz.Text;
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            SqlCommand cmd = new SqlCommand("select id_z from tblz_dmchiphi", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Maz == dr.GetString(0))
                {
                    tatkt = true;
                    break;
                }
            }
            con.Close();
            return tatkt;
        }
        public static string Username = "";
        private void frmZ_Doituong_Load(object sender, EventArgs e)// from load
        {
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            txtUser.Text = Username;
            GridEditSanPham();
            AutoSanPhamGiaThanh();
        }
    }
}
