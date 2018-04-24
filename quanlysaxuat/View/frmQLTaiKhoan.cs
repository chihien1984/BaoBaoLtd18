using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using quanlysanxuat.Model;

namespace quanlysanxuat
{
    public partial class frmQLTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        SANXUATDbContext db = new SANXUATDbContext();
        public frmQLTaiKhoan()
        {
            InitializeComponent();
        }
       
        //formload
        public static string Key = "";
        private void UcTAIKHOAN_Load(object sender, EventArgs e)
        {
            ThUserName();
        }
        private void ThUserName()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select Iden,Id,Email,FirstName,LastName,UserName,
                Application,AppPasswordHash from AspNetUsers");
            grUserName.DataSource = Function.GetDataTable(sqlQuery);
        }
        private void btnXacThuc_Click(object sender, EventArgs e)
        {
            try {
                Function.ConnectSanXuat();
                string sqlQuery = string.Format(@"update AspNetUsers 
                    set AppPasswordHash= N'{0}',Application= N'{1}'
                    where UserName like '{2}'",
                    Mahoa.Encrypt(txtPasswordHashNew.Text.Trim()),
                    txtApplication.Text,
                    txtUserName.Text);
                grUserName.DataSource = Function.GetDataTable(sqlQuery);
                ThUserName();
                MessageBox.Show("[Success]", "Thong bao [Message]",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch{
                MessageBox.Show("khong thanh cong[UnSuccess]", "Thong bao [Message]",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                Function.ConnectSanXuat();
                string sqlQuery = string.Format(@"delete from AspNetUsers 
                    where UserName like '{0}'", txtUserName.Text);
                grUserName.DataSource = Function.GetDataTable(sqlQuery);
                ThUserName();
                MessageBox.Show("[Success]", "Thong bao [Message]",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("khong thanh cong[UnSuccess]", "Thong bao [Message]",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ThDanhSachTo()
        {
            /*
            cbBoPhan.Properties.Items.Clear();
            ketnoi cn = new ketnoi();
            var dt = cn.laybang(@"select ToThucHien from tblResources");
            cn.dongketnoi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbBoPhan.Properties.Items.Add(dt.Rows[i]["ToThucHien"]);
            }
            */
        }

        private void ListDMTK_DangNhap()
        {
            ketnoi kn = new ketnoi();
            grUserName.DataSource = kn.laybang("select * from Admin where Taikhoan like N'" + txtUserName.Text + "'");
            kn.dongketnoi();
        }
        private void ListDMTaiKhoan(object sender, EventArgs e)
        {
            ThDanhMucTaiKhoan();
        }
        private void ThDanhMucTaiKhoan()
        {
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select * from AspNetUsers 
                order by Iden desc");
            grUserName.DataSource = Function.GetDataTable(sqlQuery);
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }


        private void Exportsx_Click(object sender, EventArgs e)
        {
            grUserName.ShowPrintPreview();
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            //var tuple = Model.TablePermission.Instance.GetAllTableMenu(MainDev.Instance.Ribbon);
            //treeList1.StateImageList = tuple.Item2;
            //treeList1.DataSource = tuple.Item1;
            //treeList1.ExpandAll();
        }

        private void sANXUATDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
        private async void TheHienUser()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from AspNetUsers");
                Invoke((Action)(() => {
                    grPhanQuyenTruyCapWeb.DataSource = Model.Function.GetDataTable(sqlQuery);
                  //  this.gvRole.OptionsSelection.MultiSelectMode
                  //= DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                }));
            });
        }
        private async void TheHienRoles()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from AspNetRoles order by Id desc");
                Invoke((Action)(() => {
                    grRole.DataSource = Model.Function.GetDataTable(sqlQuery);
                  //  this.gvRole.OptionsSelection.MultiSelectMode
                  //= DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                }));
            });
        }
        private async void TheHienUserRoles()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select a.*,c.Name,
                Email,b.FirstName,b.LastName
                from AspNetUserRoles a
                left outer join AspNetUsers b
                on a.UserId=b.Id
                left outer join AspNetRoles c
                on a.RoleId=c.Id");
                Invoke((Action)(() => {
                    grUserRoles.DataSource = Model.Function.GetDataTable(sqlQuery);
                    this.gvUserRoles.OptionsSelection.MultiSelectMode
                  = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }));
            });
        }

        private void ntnTraCuuUserRole_Click(object sender, EventArgs e)
        {
            TheHienUserRoles();
        }

        private void btnThemRole_Click(object sender, EventArgs e)
        {
            ThemRoles();
        }
        private async void ThemRoles()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"insert into AspNetRoles (id,Name) 
                values ((select Max(Id)+1 from AspNetRoles),N'{0}')", txtQuyen.Text);
                Invoke((Action)(() => {
                    grRole.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            TheHienRoles();
        }

        private void btnSuaRole_Click(object sender, EventArgs e)
        {
            XoaRoles();
        }
        string idRole;
        private async void XoaRoles()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"delete from AspNetRoles where Id like '{0}'", idRole);
                Invoke((Action)(() => {
                    grRole.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            TheHienRoles();
        }
        private void btnXoaRole_Click(object sender, EventArgs e)
        {
            XoaRoles();
        }

        private void grRole_Click(object sender, EventArgs e)
        {
            string gol = "";
            gol = gvRole.GetFocusedDisplayText();
            idRole = gvRole.GetFocusedRowCellDisplayText(colId1);
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            ThemQuyenNguoiDung();
            TheHienUserRolesTheoID();
        }
        private async void ThemQuyenNguoiDung()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                try
                {
                    string sqlQuery = string.Format(@"insert into
                 AspNetUserRoles (UserId,RoleId)
                 values ('{0}','{1}')", idNguoiDung, "3");
                    Invoke((Action)(() => {
                        grUserRoles.DataSource = Model.Function.GetDataTable(sqlQuery);
                    }));
                }
                catch 
                {
                    MessageBox.Show("Error", "Error");
                }
            });
        }
        string idNguoiDung;
        private void grPhanQuyenTruyCapWeb_Click(object sender, EventArgs e)
        {
            string gol = "";
            gol = gvPhanQuyenTruyCapWeb.GetFocusedDisplayText();
            idNguoiDung = gvPhanQuyenTruyCapWeb.GetFocusedRowCellDisplayText(colId);
            TheHienUserRolesTheoID();
        }
        private async void TheHienUserRolesTheoID()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select a.*,c.Name,
                 Email,b.FirstName,b.LastName
                 from AspNetUserRoles a
                 left outer join AspNetUsers b
                 on a.UserId=b.Id
                 left outer join AspNetRoles c
                 on a.RoleId=c.Id where 
                 b.Id like '{0}'", idNguoiDung);
                Invoke((Action)(() => {
                    grUserRoles.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void grPhanQuyenTruyCapWeb_DoubleClick(object sender, EventArgs e)
        {
            ThemQuyenNguoiDung();
            TheHienUserRolesTheoID();
        }

        private void btnXoaQuyen_Click(object sender, EventArgs e)
        {
            XoaUserRoles();
            TheHienUserRoles();
        }
        private async void XoaUserRoles()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvUserRoles.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvUserRoles.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from AspNetUserRoles where UserId like '{0}'",
                        rowData["UserId"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }
        private async void CapNhatUserRoles()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                int[] listRowList = this.gvUserRoles.GetSelectedRows();
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvUserRoles.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"",
                        rowData["UserId"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ");
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TheHienAspUser();
        }
        private async void TheHienAspUser()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from AspNetUsers");
                Invoke((Action)(() => {
                    grPhanQuyenTruyCapWeb.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
        string iD; 
        private void gvUserName_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvUserName.GetRowCellValue(gvUserName.FocusedRowHandle, gvUserName.Columns["Iden"]) == null)
                return;
            iD = gvUserName.GetRowCellValue(gvUserName.FocusedRowHandle, gvUserName.Columns["Iden"]).ToString();
            txtUserName.Text = gvUserName.GetRowCellValue(gvUserName.FocusedRowHandle, gvUserName.Columns["UserName"]).ToString();
            txtPasswordHashOld.Text = gvUserName.GetRowCellValue(gvUserName.FocusedRowHandle, gvUserName.Columns["AppPasswordHash"]).ToString();
        }
    }
}