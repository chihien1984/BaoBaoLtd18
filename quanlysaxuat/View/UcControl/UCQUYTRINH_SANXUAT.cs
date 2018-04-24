using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;

namespace quanlysanxuat
{
    public partial class UCQUYTRINH_SANXUAT : DevExpress.XtraEditors.XtraUserControl
    {
        public UCQUYTRINH_SANXUAT()
        {
            InitializeComponent();
        }
        private void LISTDANHMUC_SANPHAM()//LIST DANH MUC SAN PHAM SAN XUAT
        {
            DataTable Table = new DataTable();
            ketnoi Connect = new ketnoi();
            LookupCheck_Masp.Properties.DataSource = Connect.laybang("select Masp,Tensp,Makh,TenKH from tblSANPHAM SP "
            +" left join tblKHACHHANG KH on SP.Makh=KH.MKH where Tensp is not null and Tensp <>'' order by Makh ASC");
            LookupCheck_Masp.Properties.DisplayMember = "Masp";
            LookupCheck_Masp.Properties.ValueMember = "Masp";
            LookupCheck_Masp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            LookupCheck_Masp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            LookupCheck_Masp.Properties.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        
        private void ListQuyTrinhSanPham()//Load Quy trinh
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select id,masoqt,ngaybanhanh,QT.masp,SP.Tensp,diengiai,tacnhan, "
            +"nguoilap,ngayghi,KH.TenKH from tblQUYTRINH_SANXUAT QT "
            +"left outer join tblSANPHAM SP on QT.masp=SP.Masp "
            +"left outer join tblKHACHHANG KH on SP.Makh=KH.MKH where ngaybanhanh "
            + " between '" + dptu_ngay.Value.ToString("yyyy/MM/dd") + "' and '" + dpden_ngay.Value.ToString("yyyy/MM/dd") + "' order by id ASC");
            kn.dongketnoi();
        }
        private void ListQuyTrinhSanPhamThem()//Load them quy trinh
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select id,masoqt,ngaybanhanh,QT.masp,SP.Tensp,diengiai,tacnhan, "
            + "nguoilap,ngayghi,KH.TenKH from tblQUYTRINH_SANXUAT QT "
            + "left outer join tblSANPHAM SP on QT.masp=SP.Masp "
            + "left outer join tblKHACHHANG KH on SP.Makh=KH.MKH where masoqt like N'" + txtMaQT.Text + "' order by id ASC");
            kn.dongketnoi();
        }
        private void ListQuyTrinhSanPhamSua()//Load sua quy trinh
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.laybang("select id,masoqt,ngaybanhanh,QT.masp,SP.Tensp,diengiai,tacnhan, "
            + "nguoilap,ngayghi,KH.TenKH from tblQUYTRINH_SANXUAT QT "
            + "left outer join tblSANPHAM SP on QT.masp=SP.Masp "
            + "left outer join tblKHACHHANG KH on SP.Makh=KH.MKH where id like '" + txtid.Text + "' order by id ASC"); 
            kn.dongketnoi();
        }
        private void LISTQUYTRINH_SANPHAM(object sender, EventArgs e) { ListQuyTrinhSanPham();}
        private void Ghi(object sender, EventArgs e)//Ghi vật liệu phụ nhập kho
        {
            try
            {
                if (txtMasp.Text=="") { MessageBox.Show("Chọn mã sản phẩm"); return; }
                else if (dpNgaylap.Text == "") { MessageBox.Show("Chọn ngày lập"); return; }
                else if (txtMaQT.Text == "") { MessageBox.Show("Mã QT rỗng"); return; }
                {
                    SqlConnection cn = new SqlConnection(Connect.mConnect);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into  tblQUYTRINH_SANXUAT (masoqt,masp,ngaybanhanh,nguoilap,ngayghi,diengiai,tacnhan) "
                    + " values(@masoqt,@masp,@ngaybanhanh,@nguoilap,GetDate(),@diengiai,@tacnhan)", cn);
                    cmd.Parameters.Add(new SqlParameter("@masoqt", SqlDbType.NVarChar)).Value = txtMaQT.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@ngaybanhanh", SqlDbType.Date)).Value = dpNgaylap.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtuser.Text;
                    cmd.Parameters.Add(new SqlParameter("@diengiai", SqlDbType.NVarChar)).Value = txtDiengiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@tacnhan", SqlDbType.NVarChar)).Value = txtTask.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close(); 
                    ListQuyTrinhSanPhamThem();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }
        private void Sua(object sender, EventArgs e)//Sửa vật liệu phụ nhập kho
        {
            try
            {
                if (txtMasp.Text == "") { MessageBox.Show("Chọn mã sản phẩm"); return; }
                else if (dpNgaylap.Text == "") { MessageBox.Show("Chọn ngày lập"); return; }
                else if (txtMaQT.Text == "") { MessageBox.Show("Mã rỗng"); return; }
                SqlConnection cn = new SqlConnection(Connect.mConnect);
                cn.Open();
                 {
                    SqlCommand cmd = new SqlCommand("update tblQUYTRINH_SANXUAT "
                    +" set masoqt=@masoqt,masp=@masp,ngaybanhanh=@ngaybanhanh,nguoilap=@nguoilap,ngayghi=GetDate(), "
                    +" diengiai=@diengiai,tacnhan=@tacnhan where id like '" + txtid.Text + "' ", cn);
                    cmd.Parameters.Add(new SqlParameter("@masoqt", SqlDbType.NVarChar)).Value = txtMaQT.Text;
                    cmd.Parameters.Add(new SqlParameter("@masp", SqlDbType.NVarChar)).Value = txtMasp.Text;
                    cmd.Parameters.Add(new SqlParameter("@ngaybanhanh", SqlDbType.Date)).Value = dpNgaylap.Text;
                    cmd.Parameters.Add(new SqlParameter("@nguoilap", SqlDbType.NVarChar)).Value = txtuser.Text;
                    cmd.Parameters.Add(new SqlParameter("@diengiai", SqlDbType.NVarChar)).Value = txtDiengiai.Text;
                    cmd.Parameters.Add(new SqlParameter("@tacnhan", SqlDbType.NVarChar)).Value = txtTask.Text;
                    cmd.ExecuteNonQuery();
                    cn.Close(); 
                    ListQuyTrinhSanPhamSua();
                 }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thành công", "thông báo");
            }
        }
        private void Xoa(object sender, EventArgs e)//Xóa vật liệu phụ nhập kho
        {
            ketnoi kn = new ketnoi();
            gridControl3.DataSource = kn.xulydulieu("delete from tblQUYTRINH_SANXUAT  where id like " + txtid.Text + "");
            kn.dongketnoi(); 
            ListQuyTrinhSanPham();
        }
        private void Binding_NKNhap(object sender, EventArgs e)//Binding Quy trinh san xuat
        {
            string Gol = "";
            Gol = gridView3.GetFocusedDisplayText();
            txtid.Text = gridView3.GetFocusedRowCellDisplayText(id_grid);
            txtMaQT.Text = gridView3.GetFocusedRowCellDisplayText(masoqt_grid);
            txtMasp.Text= gridView3.GetFocusedRowCellDisplayText(masp_grid);
            txtSanpham.Text= gridView3.GetFocusedRowCellDisplayText(tenquycach_grid);
            txtDiengiai.Text= gridView3.GetFocusedRowCellDisplayText(diengiai_grid);
            txtTask.Text= gridView3.GetFocusedRowCellDisplayText(task_grid);
            dpNgaylap.Text= gridView3.GetFocusedRowCellDisplayText(ngaybanhanh_grid);
        }
        private void LookupCheck_Masp_EditValueChanged(object sender, EventArgs e)
        {
            string Gol = "";
            Gol = gridLookUpEdit1View.GetFocusedDisplayText();
            txtMasp.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Masp_look);
            txtSanpham.Text = gridLookUpEdit1View.GetFocusedRowCellDisplayText(Tensp_look);
            if (txtMasp.Text!="")
            {
                txtMaQT.Text = "QTSX-" + txtMasp.Text;
            }
            else { txtMaQT.Text = "";}
            
        }
        private void Layout_QTSX()//Goi quy trinh san xuat
        {
            string pat = string.Format(@"{0}\{1}.PDF", this.txtPath_QTSX.Text, txtMaQT.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            { MessageBox.Show("HIEN CHUA CO QUY TRINH NAY", "THONG BAO"); }
        }
        private void BtnQuiTrinhSX_Click(object sender, EventArgs e)//Gọi qui trình sản xuất
        {
            frmLoading f2 = new frmLoading(txtMaQT.Text, txtPath_QTSX.Text);
            f2.Show();
        }
        private void Layout_Masp()//Hàm gọi bản vẽ sản phẩm
        {
            string pat = string.Format(@"{0}\{1}.pdf", this.txtPath_MaSP.Text, txtMasp.Text);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
            else
            {
                MessageBox.Show("SAN PHAM CHUA CO TRONG HE THONG");
            }
        }
        private void AutoDienGiai()// Autocomplete
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select Distinct diengiai from tblQUYTRINH_SANXUAT", con);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtDiengiai.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void AutoTacNhan()// Autocomplete
        {
            SqlConnection con = new SqlConnection(Connect.mConnect);
            con.Open();
            {
                SqlCommand cmd = new SqlCommand("select Distinct tacnhan from tblQUYTRINH_SANXUAT", con);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                txtTask.AutoCompleteCustomSource = MyCollection;
                con.Close();
            }
        }
        private void BtnGoiBanVe(object sender, EventArgs e)// Sự kiện gọi mã sản phẩm
        {
            frmLoading f2 = new frmLoading(txtMasp.Text, txtPath_MaSP.Text);
            f2.Show();
        }
        private void btnEx_Click(object sender, EventArgs e)
        {
            gridControl3.ShowPrintPreview();
        }
       
        private void UCQUYTRINH_SANXUAT_Load(object sender, EventArgs e)// from load
        {
            LISTDANHMUC_SANPHAM();//List Danh muc san pham san xuat
            txtuser.Text = Login.Username;
            dptu_ngay.Text = DateTime.Now.ToString("01/MM/yyyy");
            dpden_ngay.Text = DateTime.Now.ToString();
            AutoTacNhan(); AutoDienGiai();
          
        }



 

    }
}
