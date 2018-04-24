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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace quanlysanxuat.View.UcControl
{
    public partial class TreeListBOM : DevExpress.XtraEditors.XtraForm
    {
        public TreeListBOM()
        {
            InitializeComponent();
        }
        private string maloai;
        private string tenloai;
        private string idsanpham;
        private double mucDoCon;
        private string soluongchitiet;
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            AddNodeParent();
            TheHienTraCuuCongDoanTreeListStages();
        }
        //cấu trúc Chi tiet sản phẩm
        private async void ThTreeListDetail(string query)
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = query;
                Invoke((Action)(() =>
                {
                    treeListProduction.DataSource = Model.Function.GetDataTable(sqlQuery);
                    treeListProduction.ForceInitialize();
                    treeListProduction.ExpandAll();
                    treeListProduction.OptionsSelection.MultiSelect = true;
                }));
            });
        }
        //cấu trúc chi tiết BOM của sản phẩm
        private async void ThTreeListBoM(string query)
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = query;
                Invoke((Action)(() =>
                {
                    treeListPrBOM.DataSource = Model.Function.GetDataTable(sqlQuery);
                    treeListPrBOM.ForceInitialize();
                    treeListPrBOM.ExpandAll();
                    treeListPrBOM.OptionsSelection.MultiSelect = true;
                }));
            });
        }

        #region sự kiện checkedchildNode đồng thời checkedParentNode
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
        private void treeListProductionStages_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
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
        private void treeListProduction_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (checkNode.Checked == true)
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
        }
        #endregion

        #region
        private void AddNodeParent()
        {
            List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                  @"insert into tblSanPhamBOM 
                    (ID,ParentID,IDSanPham,MucDo,
                    MaLoai,TenLoai,SoLuong,
                    MaChiTiet,TenChiTiet,SoChiTiet,
                    MaSanPham,TenSanPham,NguoiLap,NgayLap)
                    values(N'{0}',N'{1}',
	                       N'{2}',N'{3}',
	                       N'{4}',N'{5}',
	                       N'{6}',N'{7}',
	                       N'{8}',N'{9}',
	                       N'{10}',N'{11}',N'{12}',GetDate())",
                    node.GetDisplayText(colID),
                    node.GetDisplayText(colParent),
                    node.GetDisplayText(colIDSanPham),
                    node.GetDisplayText(colMucDo),
                    node.GetDisplayText(colMaLoai),
                    node.GetDisplayText(colTenLoai),
                    node.GetDisplayText(colSoLuong),
                    node.GetDisplayText(colMaChiTiet),
                    node.GetDisplayText(colTenChiTiet),
                    node.GetDisplayText(colSoChiTiet),
                    node.GetDisplayText(colMaSanPham),
                    node.GetDisplayText(colTenSanPham),
                    Login.Username);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        #endregion

        private void btnTraCuuDSSanPham_Click(object sender, EventArgs e)
        {
            TheHienDanhSachSanPham();
        }
        private void TheHienDanhSachSanPham()
        {
            //  ketnoi kn = new ketnoi();
            //  string sqlQuery = string.Format(@"select case when t.IDSanPham 
            //      is not null then 'x' else '' end TreeList,
	        		//* from tblSANPHAM p left outer join
	        		//(select IDSanPham from tblSanPhamTreeList group by IDSanPham)t
	        		//on p.Code=t.IDSanPham");
            //  grSanPham.DataSource =
            //      kn.laybang(sqlQuery);
            //  kn.dongketnoi();
            //  gvSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #region formload
        private void ucproductionTreeList_Load(object sender, EventArgs e)
        {
            TheHienDanhSachSanPham();
            ShowTreListProduction();
            ShowStagesCode();
            TheHienToThucHien();//Thể hiện tổ thực hiện
            treeListProduction.Appearance.Row.Font = new Font("Segoe UI", 7f);
            treeListPrBOM.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        #endregion
        private void grTreeList_Click(object sender, EventArgs e)
        {
        }
        private void grTreeList_DoubleClick(object sender, EventArgs e)
        {
            //frmChildNode childNode = new
            //    frmChildNode(idsanpham, maloai, tenloai, txtParentNode.Text, mucDoCon.ToString());
            //childNode.ShowDialog();
            //ShowTreListProductionfollowProduction();
        }

        private void grSanPham_Click(object sender, EventArgs e)
        {
            string point = "";
            //point = gvSanPham.GetFocusedDisplayText();
            //txtMaSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(masp_sanpham);
            //txtSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(tensanpham_sanpham);
            //txtIDSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(idsanpham_sanpham);
            ShowTreListProductionfollowProduction();
            ShowTreeListfollowIDProductionStages();
        }

        private void ShowTreListProduction()
        {
            string sqlQuery = string.Format(@"select *
                    from tblSanPhamTreeList
                    order by ID desc,NgayLap DESC");
            ThTreeListDetail(sqlQuery);
        }
        #region Tra cứu cây cấu trúc sản phẩm theo ten san pham
        private void ShowTreListProductionfollowProduction()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select *
                    from tblSanPhamTreeList 
                    where IDSanPham like '{0}'", txtIDSanPham.Text);
            treeListProduction.DataSource = kn.laybang(sqlQuery);
            treeListProduction.ForceInitialize();
            treeListProduction.ExpandAll();
            //treeListProduction.BestFitColumns();
            treeListProduction.OptionsSelection.MultiSelect = true;
        }
        #endregion
        string currentGroupName;

        private bool IsCurrentGroupName(string groupName)
        {
            if (currentGroupName != null)
                return currentGroupName.Contains(groupName);
            return false;
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            e.NodeImageIndex = e.Node.HasChildren ? 1 : 2;
            if (treeListProduction.FocusedNode == e.Node) e.NodeImageIndex = 0;
        }

        private void treeList1_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            if (e.Node.Focused)
                e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.Orange), e.StateRect);
            e.DefaultDraw();
        }

        private void btnExtraTreeList_Click(object sender, EventArgs e)
        {
            treeListProduction.ShowPrintPreview();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DeleteTreeList();
            ShowTreListProductionfollowProduction();
            TheHienDanhSachSanPham();
        }

        private void btnTraCuuTreeList_Click_1(object sender, EventArgs e)
        {
            ShowTreListProduction();
        }



        private void btnEditTreeList_Click(object sender, EventArgs e)
        {
            EditTreeList();
            ShowTreListProductionfollowProduction();
        }
        private void ShowTestTreeList()
        {
            //List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            //foreach (TreeListNode node in list)
            //{
            //    MessageBox.Show("" + node.GetDisplayText(idtree), "");
            //}
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            EditTreeList();
        }
        private void EditTreeList()
        {
            //List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = Connect.mConnect;
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            //foreach (TreeListNode node in list)
            //{
            //    string strQuery = string.Format(
            //        @"update tblSanPhamTreeList set
            //                MaLoai=N'{0}',
            //                TenLoai=N'{1}',SoLuong='{2}',DienGiai=N'{3}',
            //                NguoiLap=N'{4}',NgayLap=GetDate(),DonVi=N'{5}' where ID like '{6}'",
            //            node.GetDisplayText(maloaitree),
            //            node.GetDisplayText(tenloaitree),
            //            node.GetDisplayText(soluongtree),
            //            node.GetDisplayText(diengiaitree),
            //            Login.Username,
            //            node.GetDisplayText(donvitree),
            //            node.GetDisplayText(idtree));
            //    SqlCommand cmd = new SqlCommand(strQuery, con);
            //    cmd.ExecuteNonQuery();
            //}
            //con.Close();
        }
        
        private void DeleteTreeList()
        {
            //List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = Connect.mConnect;
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            //foreach (TreeListNode node in list)
            //{
            //    string strQuery = string.Format(
            //          @"delete from 
            //            tblSanPhamTreeList 
            //            where ID like '{0}';
            //            delete from 
            //            tblSanPhamTreeList 
            //            where ParentID like '{0}' 
            //            and (DonGiaCongDoan like '' or DonGiaCongDoan is null)",
            //            node.GetDisplayText(idtree));
            //    SqlCommand cmd = new SqlCommand(strQuery, con);
            //    cmd.ExecuteNonQuery();
            //}
            //con.Close();
        }

        private void treeListProduction_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["ID"]) == null)               return;
            parentID = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["ParentID"]).ToString();
            idSanPham = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["IDSanPham"]).ToString();
            maLoai = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["MaLoai"]).ToString();
            tenLoai = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["TenLoai"]).ToString();
            soLuong = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["SoLuong"]).ToString();
            maChiTiet = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["MaChiTiet"]).ToString();
            tenChiTiet = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["TenChiTiet"]).ToString();
            soChiTiet = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["SoChiTiet"]).ToString();
            maSanPham = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["MaSanPham"]).ToString();
            tenSanPham = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["TenSanPham"]).ToString();
            donVi = treeListProduction.GetRowCellValue(treeListProduction.FocusedNode, treeListProduction.Columns["DonVi"]).ToString();
        }
        string parentID;
        string idSanPham;
        string maLoai;
        string tenLoai;
        string soLuong;
        string maChiTiet;
        string tenChiTiet;
        string soChiTiet;
        string maSanPham;
        string tenSanPham;
        string donVi;
        private void treeListProduction_DoubleClick(object sender, EventArgs e)
        {
            //frmChildNode childNode = new
            //    frmChildNode(idsanpham, maloai, tenloai, txtParentNode.Text,
            //    mucDoCon.ToString(),txtMaSanPham.Text,txtSanPham.Text);
            //childNode.ShowDialog();
            //ShowTreListProductionfollowProduction();
        }

        private void treeListProduction_MouseMove(object sender, MouseEventArgs e)
        {
            //if (treeListProduction.GetAllCheckedNodes().Count()>0)
            //{
            //    btnDelete.Enabled = true;
            //    btnEditTreeList.Enabled = true;
            //}
            //else
            //{
            //    btnDelete.Enabled = false;
            //    btnEditTreeList.Enabled = false;
            //}
        }

        /// <summary>
        /// //////////////// Cập nhật công đoạn cho chi tiết sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnTraCuuTreeList_Click(object sender, EventArgs e)
        {
            TheHienTraCuuCongDoanTreeListStages();
        }
        private void TheHienTraCuuCongDoanTreeListStages()
        {
            string sqlQuery = string.Format(@"select ID,ParentID,MucDo,
                    MaLoai,TenLoai,SoLuong,
                    MaSanPham,TenSanPham,
                    NguoiLap,NgayLap,
                    MaChiTiet,TenChiTiet,
                    SoChiTiet from tblSanPhamBOM
					order by IDSanPham Desc, ThuTu ASC");
            ThTreeListBoM(sqlQuery);
        }
        private void ShowTreeListfollowIDProductionStages()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select case when DonGiaCongDoan>0 then 'x' end CoGia,* from tblSanPhamTreeList
                    where IDSanPham like '{0}' order by ThuTu ASC", txtIDSanPham.Text);
            treeListPrBOM.DataSource =
                kn.laybang(sqlQuery);
            treeListPrBOM.ForceInitialize();
            treeListPrBOM.ExpandAll();
            //treeListProductionStages.BestFitColumns();
            treeListPrBOM.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
        }
        private void treeListCongDoanSanPham_DoubleClick(object sender, EventArgs e)
        {
            frmChildNodeBOM chilBoM = new
            frmChildNodeBOM(idsanpham, maloai, tenloai, txtParentNode.Text,
                mucDoCon.ToString(), soluongchitiet.ToString(),txtMaSanPham.Text,
                txtSanPham.Text,maloai,tenloai,soluongchitiet);
            chilBoM.ShowDialog();
            ShowTreeListfollowIDProductionStages();
        }
        private void EditTreeListStages()
        {
            List<TreeListNode> list = treeListPrBOM.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                    @"update tblSanPhamBOM 
                    set MaLoai=N'{0}',TenLoai=N'{1}',SoLuong='{2}',
                    MaChiTiet=N'{3}',TenChiTiet=N'{4}',
                    SoChiTiet='{5}',MaSanPham=N'{6}',
                    TenSanPham=N'{7}' where ID like '{8}'", 
                    node.GetDisplayText(colBomMa),
                    node.GetDisplayText(colBomTen),
                    node.GetDisplayText(colBomSoLuong),
                    node.GetDisplayText(colBomMaChiTiet),
                    node.GetDisplayText(colBomTenChiTiet),
                    node.GetDisplayText(colBomSoChiTiet),
                    node.GetDisplayText(colBomMaSanPham),
                    node.GetDisplayText(colBomTenSanPham),
                    node.GetDisplayText(colBomID));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        private void CapNhatChiTiet()
        {
            Model.Function.ConnectSanXuat();
            string sql = string.Format(@"update tblSanPhamTreeList set MaChiTiet = N'{0}',TenChiTiet = N'{1}',
                SoLuong='{2}',SoChiTiet='{2}'
                where ParentID ='{3}'",
                txtParentNode.Text);
            var kq = Model.Function.GetDataTable(sql);
        }
        private void DeleteTreeListStages()
        {
            List<TreeListNode> list = treeListPrBOM.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                    @"delete from tblSanPhamBOM where ID like '{0}'",
                    node.GetDisplayText(colBomID));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        private void btnCapNhatCongDoanSanPham_Click(object sender, EventArgs e)
        {
            EditTreeListStages();
            TheHienTraCuuCongDoanTreeListStages();
        }

        private void btnXoaCongDoanSanPham_Click(object sender, EventArgs e)
        {
            DeleteTreeListStages();
            TheHienTraCuuCongDoanTreeListStages();
        }

        private void treeListCongDoanSanPham_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (treeListPrBOM.GetRowCellValue(treeListPrBOM.FocusedNode, treeListPrBOM.Columns[""]) == null)
                return;
            txtParentNode.Text = treeListPrBOM.GetRowCellValue(treeListPrBOM.FocusedNode, treeListPrBOM.Columns[""]).ToString();
            //string point = "";
            //point = treeListPrBOM.GetFocusedDisplayText();
            //txtParentNode.Text = treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesID);//Khi tao them node con thì node hiện hành là node cha
            //maloai = treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesMa);
            //tenloai = treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesTen);
            //idsanpham = treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesIDSanPham);
            //txtMucDoCha.Text = treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesMucDo);
            //soluongchitiet= treeListPrBOM.GetFocusedRowCellDisplayText(treeListStagesSoLuong);
            if (txtMucDoCha.Text == "")
            {
                txtMucDoCha.Text = "0";
            }
            else
            {
                double mucdoCha = Convert.ToInt16(txtMucDoCha.Text);
                mucDoCon = mucdoCha + 1;
            }
        }

        private void treeListProductionStages_MouseMove(object sender, MouseEventArgs e)
        {
            if (treeListPrBOM.GetAllCheckedNodes().Count() > 0)
            {
                btnXoaBOM.Enabled = true;
                btnCapNhatBOM.Enabled = true;
            }
            else
            {
                btnXoaBOM.Enabled = false;
                btnCapNhatBOM.Enabled = false;
            }
        }

        private void treeListProductionStages_MouseHover(object sender, EventArgs e)
        {
            if (treeListPrBOM.GetAllCheckedNodes().Count() > 0)
            {
                btnXoaBOM.Enabled = true;
                btnCapNhatBOM.Enabled = true;
            }
            else
            {
                btnXoaBOM.Enabled = false;
                btnCapNhatBOM.Enabled = false;
            }
        }

        private void btnExportTreeListStages_Click(object sender, EventArgs e)
        {
            treeListPrBOM.ShowPrintPreview();
        }
        private void ShowStagesCode()
        {
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"select * from tblResources order by ResourceID ASC");
            //repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(sqlQuery);
            //repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            //repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            //kn.dongketnoi();
        }

        private void btnInfoDesiger_Click(object sender, EventArgs e)
        {
            Path path = new Path();
            frmLoading f2 = new frmLoading(txtMaSanPham.Text, path.pathbanve);
            f2.Show();
        }

        private void btnThemNguyenCong_Click(object sender, EventArgs e)
        {
            frmResources Resources = new frmResources();
            Resources.ShowDialog();
            ShowStagesCode();
        }
        private void TheHienToThucHien()
        {
            //treeListPrBOM.Items.Clear();
            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            //var data = kn.laybang(sqlQuery);
            //for (int i = 0; i < data.Rows.Count; i++)
            //{
            //    repositoryItemCBToThucHien.Items.Add(data.Rows[i]["ToThucHien"]);
            //}
            //kn.dongketnoi();
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {}

        private void treeListProduction_FocusedNodeChanged_1(object sender, FocusedNodeChangedEventArgs e)
        {

        }
    }
}
