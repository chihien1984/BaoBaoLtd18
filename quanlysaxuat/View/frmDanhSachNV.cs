using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using quanlysanxuat.View;

namespace quanlysanxuat
{
    public partial class frmDanhSachNV : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachNV()
        {
            InitializeComponent();
        }

        private void frmDanhSachNV_Load(object sender, EventArgs e)//FROM LOAD DANH SACH NHAN VIEN
        {
            txtMember.Text = Login.Username;
            if(Login.role=="39"|| Login.role == "1")
            {
                btnTaoMoi.Visible = true;
                btnLuu.Visible = true;
                btnSua.Visible = true;
                btnXoa.Visible = true;
                btnBo_Phan.Visible = true;
            }
            DSPhongBan();
            btnallDSnhanvien();
        }
    
        private void DSPhongBan() {
            ketnoi Connect = new ketnoi();
            repositoryItemGridLookUpEdit1.DataSource = Connect.laybang(@"select MaBoPhan,BoPhan from tblPHONGBAN_TK");
            repositoryItemGridLookUpEdit1.DisplayMember = "MaBoPhan";
            repositoryItemGridLookUpEdit1.ValueMember = "MaBoPhan";
            repositoryItemGridLookUpEdit1.NullText = null;
            repositoryItemGridLookUpEdit1.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            repositoryItemGridLookUpEdit1.View.OptionsView.ShowAutoFilterRow = true;
            repositoryItemGridLookUpEdit1.EditValueChanged += gridLookupEditTenBP_EditValueChanged;
            repositoryItemGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemGridLookUpEdit1.ImmediatePopup = true;
            Connect.dongketnoi();
        }
        

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void btnDanhSachNV_Click(object sender, EventArgs e)
        {
            btnallDSnhanvien();
        }
        private void btnallDSnhanvien()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select Sothe,HoTen,MaBP,BoPhan,
                    NguoiLap,NgayLap,ID from tblDSNHANVIEN order by  MaBP ASC");
            kn.dongketnoi();
            gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into tblDSNHANVIEN(Sothe,HoTen,MaBP,BoPhan,
                    NguoiLap,NgayLap)
                    VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',GetDate())",
                    rowData["Sothe"],
                    rowData["HoTen"],
                    rowData["MaBP"],
                    rowData["BoPhan"],
                    Member);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery(); 
                }
                btnallDSnhanvien();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void DSNV_THEM()
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select Sothe,HoTen,MaBP,BoPhan,
                    NguoiLap,NgayLap,ID from tblDSNHANVIEN order by  MaBP ASC");
            kn.dongketnoi();
            gridView2.OptionsView.NewItemRowPosition = 
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }


        private void btnTaomoi_Click(object sender, EventArgs e)
        {

        }
        public string Member { set;get; }
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gridView2.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"UPDATE tblDSNHANVIEN 
                        SET Sothe=N'{0}', HoTen=N'{1}', MaBP=N'{2}',BoPhan=N'{3}',
                        NguoiLap=N'{4}',NgayLap=GetDate() WHERE ID='{5}'",
                    rowData["Sothe"],
                    rowData["HoTen"],
                    rowData["MaBP"], 
                    rowData["MaBP"],
                    Member, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery(); btnallDSnhanvien();
                }
            con.Close();
        }
            catch (Exception)
            {
                MessageBox.Show("Lỗi");
            }
}

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                con.Open();
                DataRow rowData;
                int[] listRowList = this.gridView2.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gridView2.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"Delete from tblDSNHANVIEN where ID = '{0}'",
                rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
                btnallDSnhanvien();
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void btnTaoMoi_Click_1(object sender, EventArgs e)
        {
            ketnoi kn = new ketnoi();
            gridControl2.DataSource = kn.laybang(@"select top 0 Sothe='',HoTen='',MaBP='',BoPhan='',
            NguoiLap='',NgayLap,BoPhan='' from tblDSNHANVIEN");
            kn.dongketnoi();
            gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            //gridView2.OptionsBehavior.Editable = true;
        }

        private void gridLookupEditTenBP_EditValueChanged(object sender, EventArgs e)
        {
            //string Gol;
            
            //Gol = gridLookupEditTenBP.GetFocusedDisplayText();
            //Mabp = gridView2.GetFocusedRowCellDisplayText(mabophan_grid2);
            //Tenbp = gridView2.GetFocusedRowCellDisplayText(tenbophan_grid2);
            //Console.WriteLine(Mabp);
        }
        public string Mabp { set; get; }
        public string Tenbp { set; get; }
        private void reposi(string Mabp,string Tenbp)
        {
            string Gol;
            Gol = repositoryItemGridLookUpEdit1View.GetFocusedDisplayText();
            this.Mabp = repositoryItemGridLookUpEdit1View.GetFocusedRowCellDisplayText(MaBoPhan_repoin);
            this.Tenbp = repositoryItemGridLookUpEdit1View.GetFocusedRowCellDisplayText(BoPhan_repoint);
        }

        private void btnBo_Phan_Click(object sender, EventArgs e)
        {
            frmThemPhongBan themPhongBan = new frmThemPhongBan();
            themPhongBan.ShowDialog();
            DSPhongBan();
        }
    }
}
