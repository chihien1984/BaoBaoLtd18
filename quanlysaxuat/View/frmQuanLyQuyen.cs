using quanlysanxuat.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlysanxuat.Model;
using DevExpress.XtraGrid;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace quanlysanxuat
{
    public partial class frmQuanLyQuyen : DevExpress.XtraEditors.XtraForm
    {
        SANXUATDbContext _baobao = new SANXUATDbContext();
        public frmQuanLyQuyen()
        {
            InitializeComponent();
            //treeList1.CustomDrawNodeCell += TreeList1_CustomDrawNodeCell;
            treeList1.CellValueChanging += treeList1_CellValueChanging;
        }

        private void treeList1_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            e.Node.SetValue(e.Column, e.Value);
            SetCheckChillNode(e.Node, e.Column, (bool)e.Value);
            SetCheckParentNode(e.Node, e.Column, (bool)e.Value);
        }
        private void SetCheckChillNode(TreeListNode node, TreeListColumn col, bool check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i][col] = check;
                SetCheckChillNode(node.Nodes[i], col, check);
            }
        }
        private void SetCheckParentNode(TreeListNode node, TreeListColumn col, bool check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    var state = (bool)node.ParentNode.Nodes[i][col];
                    if (!check.Equals(state))
                    {
                        b = true;
                        break;
                    }
                }
                bool bb = !b && check;
                node.ParentNode[col] = bb;
                SetCheckParentNode(node.ParentNode, col, check);
            }
        }
        private void TreeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
        }
        //formload
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            gvBoPhanPhuTrach.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            grUser.DataSource = _baobao.AspNetUsers.
                Where(x=>x.AppPasswordHash != null).ToList();
        }

        private bool OneOfChildsIsChecked(TreeListNode node)
        {
            bool result = false;
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    result = true;
                }
            }
            return result;
        }
        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node.Checked)
            {
                node.UncheckAll();
            }
            else
            {
                node.CheckAll();
            }
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                if (oneOfChildIsChecked)
                {
                    node.CheckState = CheckState.Checked;
                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {}
        //Focused table user
        string userName;
        private void gvUser_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]) == null)
                return;
            else
            {
                var user = gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]);
                userName = user.ToString();
                var dt = from a in _baobao.tblFunctions.Where(x => x.Application == "AD")
                         join b in _baobao.tblUserFunctions.Where(x => x.User == user.ToString())
                         on a.Menu equals b.Menu
                         into ab
                         from bc in ab.DefaultIfEmpty()
                         select new userRinght
                         {
                             Menu = a.Menu,
                             Description = a.Description,
                             AllowAddNew = bc.AllowAddNew == null ? false : bc.AllowAddNew,
                             AllowEdit = bc.AllowEdit == null ? false : bc.AllowEdit,
                             AllowDelete = bc.AllowDelete == null ? false : bc.AllowDelete,
                             Disable = bc.Disable == null ? false : bc.Disable,
                             ParentMenu = a.ParentMenu
                         };
                treeList1.DataSource = dt.ToList();
                treeList1.ExpandAll();
                ThWorkLocationUserName();
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var user = gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]);
            var node = treeList1.GetNodeList();
            for (int i = 0; i < node.Count; i++)
            {
                var tbl = new tblUserFunction();
                tbl.User = user.ToString();
                tbl.Menu = node[i].GetDisplayText(colMenu);
                tbl.SetTime = DateTime.Now;
                tbl.AllowAddNew = (bool)node[i].GetValue(colAdd);
                tbl.AllowEdit = (bool)node[i].GetValue(colEdit);
                tbl.AllowDelete = (bool)node[i].GetValue(colDelete);
                tbl.Disable = (bool)node[i].GetValue(colDisable);
                _baobao.tblUserFunctions.AddOrUpdate(tbl);
            }
            _baobao.SaveChanges();
            XtraMessageBox.Show("Thao tac thanh cong[Success]", "Thong bao [Message]",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void grUser_Click(object sender, EventArgs e)
        {

        }

        private void btnThemTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmQLTaiKhoan dangky = new frmQLTaiKhoan();
            dangky.ShowDialog();
        }

        private void grUser_Click_1(object sender, EventArgs e)
        { }

        private void btnSaveWorkLocation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
             try
              {
                  SqlConnection con = new SqlConnection();
                  con.ConnectionString = Connect.mConnect;
                  if (con.State == ConnectionState.Closed)
                      con.Open();
                  var user = gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]);
                  DataRow rowData;
                  int[] listRowList = this.gvBoPhanPhuTrach.GetSelectedRows();
                  for (int i = 0; i < listRowList.Length; i++)
                  {
                      rowData = gvBoPhanPhuTrach.GetDataRow(listRowList[i]);
                      var db = new tblResourcesUser();
                      db.UserName = user.ToString();
                      db.WorkLocation = rowData["ToThucHien"].ToString();
                      db.ID = (int)rowData["ResourceID"];
                      _baobao.tblResourcesUsers.AddOrUpdate(db);
                  }
                  _baobao.SaveChanges();
                  XtraMessageBox.Show("Thao tac thanh cong[Success]", "Thong bao [Message]",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              catch
              {
               XtraMessageBox.Show("Thao tac thanh cong[Success]", "Thong bao [Message]",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
             */

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            var user = gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]);
            int[] listRowList = this.gvBoPhanPhuTrach.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvBoPhanPhuTrach.GetDataRow(listRowList[i]);
                string strQuery = string.Format(
                        @"insert into tblResourcesUser 
                        (UserName,WorkLocation,IDWorkLocation)
                        values ('{0}',N'{1}','{2}')",
                        user, rowData["ToThucHien"], rowData["ResourceID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ThWorkLocationUserName();
        }

        private void btnSearchWorkLocation_Click(object sender, EventArgs e)
        {
            TheHienWorkLocation();
        }
        private void TheHienWorkLocation()
        {
            //grBoPhanPhuTrach.DataSource = _baobao.tblResources.ToList();//
            Function.ConnectSanXuat();
            string SqlQuery = string.Format(@"select * from tblResources");
            grBoPhanPhuTrach.DataSource = Function.GetDataTable(SqlQuery);
            Function.Disconnect();
        }
        private void ThWorkLocationUserName()
        {
            //grBoPhanPhuTrach.DataSource = from a in _baobao.tblResourcesUsers
            //                              where a.UserName == userName
            //                              select new tblResource
            //                              {
            //                                  ToThucHien = a.WorkLocation,
            //                                  ResourceID = (int)a.IDWorkLocation
            //                              };
            Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select ID,WorkLocation ToThucHien,IDWorkLocation ResourceID 
                from tblResourcesUser 
                where UserName like '{0}'", userName);
            grBoPhanPhuTrach.DataSource = Function.GetDataTable(sqlQuery);

        }

        private void btnQuanLyTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmQLTaiKhoan qLTaiKhoan = new frmQLTaiKhoan();
            qLTaiKhoan.ShowDialog();
            grUser.DataSource = _baobao.AspNetUsers.ToList();
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MainDev main = new MainDev();
            grUser.DataSource = _baobao.AspNetUsers.ToList();
        }

        private void btnXoaLocation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            var user = gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]);
            int[] listRowList = this.gvBoPhanPhuTrach.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvBoPhanPhuTrach.GetDataRow(listRowList[i]);
                string strQuery = string.Format(
                        @"delete from tblResourcesUser where ID like '{0}'",
                        rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ThWorkLocationUserName();
        }
    }
    public class userRinght
    {
        public string Menu { get; set; }
        public string Description { get; set; }
        public bool? AllowAddNew { get; set; }
        public bool? AllowEdit { get; set; }
        public bool? AllowDelete { get; set; }
        public bool? Disable { get; set; }
        public string ParentMenu { get; set; }
    }
}