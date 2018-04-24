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
    public partial class frmChildNode : DevExpress.XtraEditors.XtraForm
    {
        private string sanphamid;
        private string maloai;
        private string tenloai;
        private string parentnode;
        private string mucdocon;
        private string masanpham;
        private string tensanpham;

        public frmChildNode(string sanphamid, string maloai, string tenloai, 
            string parentnode, string mucdo,string masanpham,string tensanpham)
        {
            InitializeComponent();
            this.sanphamid = sanphamid;
            this.maloai = maloai;
            this.tenloai = tenloai;
            this.parentnode = parentnode;
            this.mucdocon = mucdo;
            this.masanpham = masanpham;
            this.tensanpham = tensanpham;
        }

        #region formload
        private void frmChildNode_Load(object sender, EventArgs e)
        {
            txtIDSanPham.Text = sanphamid;
            txtMaCha.Text = maloai;
            txtTenCha.Text = tenloai;
            txtParentNode.Text = parentnode;

            txtMucDo.Text = mucdocon;
            TheHienDanhSachSanPham();
            TheHienDanhSachTheoSanPhamID();
            gvChildTreeList.Appearance.Row.Font = new Font("Segoe UI", 8f);
            DonViChiTiet();
        }
        #endregion
        private void DonViChiTiet()
        {
            repositoryItemComboBoxDonViChiTiet.Items.Add("Cái");
            repositoryItemComboBoxDonViChiTiet.Items.Add("Bộ");
            repositoryItemComboBoxDonViChiTiet.Items.Add("Chiếc");
        }
        private void TheHienDanhSachSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select s.Masp,Tensp from tblSANPHAM s 
                inner join tblSANPHAM_CT c on s.Code=c.SanPhamID");
            cbMaSanPham.Properties.DataSource = kn.laybang(sqlQuery);
            cbMaSanPham.Properties.DisplayMember = "Masp";
            cbMaSanPham.Properties.ValueMember = "Masp";
            kn.dongketnoi();
        }
        private void grlSanPham_EditValueChanged(object sender, EventArgs e)
        {
            TheHienDanhSachChiTietCungLoai();
        }
        private void TheHienDanhSachChiTietCungLoai()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Mact MaLoai,
                Ten_ct TenLoai,Soluong_CT SoLuong,
                Chatlieu_chitiet DienGiai,DonVi from tblSANPHAM_CT 
                where Masp like N'{0}'", cbMaSanPham.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }
        private void TheHienDanhSachTheoSanPhamID()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Mact MaLoai,
                Ten_ct TenLoai,Soluong_CT SoLuong,
                Chatlieu_chitiet DienGiai,DonVi from tblSANPHAM_CT 
                where SanPhamID like N'{0}'", txtIDSanPham.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvChildTreeList.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void TaoMoiChiTiet()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select Mact MaLoai,
                Ten_ct TenLoai,Soluong_CT SoLuong,
                Chatlieu_chitiet DienGiai,DonVi from tblSANPHAM_CT
                where Masp like N'{0}'",
                txtMaCha.Text);
            grChildTreeList.DataSource = kn.laybang(sqlQuery);
            kn.dongketnoi();
        }

        private void btnTaoMoiChildNode_Click(object sender, EventArgs e)
        {
            TaoMoiChiTiet();
        }

        private void btnSaveNodeTreeList_Click(object sender, EventArgs e)
        {
            try
            {
                int[] listRowList = this.gvChildTreeList.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvChildTreeList.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into tblSanPhamTreeList 
                            (IDSanPham,MucDo,MaLoai,
                            TenLoai,SoLuong,DienGiai,ParentID,
                            NguoiLap,MaChiTiet,TenChiTiet,SoChiTiet,MaSanPham,TenSanPham,DonVi,NgayLap)
					        values('{0}','{1}',N'{2}',
					        N'{3}','{4}',N'{5}',N'{6}',
                            N'{7}',N'{8}',N'{9}','{10}',N'{11}',N'{12}',N'{13}',GetDate())",
                            txtIDSanPham.Text, txtMucDo.Text, rowData["MaLoai"],
                            rowData["TenLoai"], rowData["SoLuong"],
                            rowData["DienGiai"], txtParentNode.Text,
                            Login.Username, rowData["MaLoai"], rowData["TenLoai"], 
                            rowData["SoLuong"],masanpham,tensanpham,rowData["DonVi"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Success", "Mission");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
    }
}
