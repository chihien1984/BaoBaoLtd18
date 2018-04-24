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
    public partial class ucproductionTreeList : DevExpress.XtraEditors.XtraForm
    {
        public ucproductionTreeList()
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
            ShowTreListProductionfollowProduction();
            TheHienDanhSachSanPham();
        }
        #region
        private void AddNodeParent()
        {
            try
            {
                int[] listRowList = this.gvSanPham.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvSanPham.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(
                        @"insert into tblSanPhamTreeList 
                          (IDSanPham,MucDo,MaLoai,
                          TenLoai,SoLuong,DienGiai,
                          ParentID,MaSanPham,TenSanPham,
                         MaChiTiet,TenChiTiet,SoChiTiet,
                         NguoiLap,DonVi,NgayLap)
					      values('{0}','{1}',N'{2}',
					      N'{3}','{4}',N'{5}',
                          '{6}',N'{7}',N'{8}',
                         N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',GetDate());
                          update tblSanPhamTreeList set ParentID='{0}'+ID 
                          where IDSanPham='{0}' and ParentID = '0'",
                            rowData["Code"], txtMucDo.Text, rowData["Masp"],
                            rowData["Tensp"], txtSoLuong.Text, rowData["Vatlieu"],
                            "0", rowData["Masp"], rowData["Tensp"],
                            rowData["Masp"], rowData["Tensp"], txtSoLuong.Text,
                            Login.Username, cbDonVi.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Massage");
            }
        }
        #endregion

        private void btnTraCuuDSSanPham_Click(object sender, EventArgs e)
        {
            TheHienDanhSachSanPham();
        }
        private void TheHienDanhSachSanPham()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select case when t.IDSanPham 
                is not null then 'x' else '' end TreeList,
				* from tblSANPHAM p left outer join
				(select IDSanPham from tblSanPhamTreeList group by IDSanPham)t
				on p.Code=t.IDSanPham");
            grSanPham.DataSource =
                kn.laybang(sqlQuery);
            kn.dongketnoi();
            gvSanPham.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        #region formload
        private void ucproductionTreeList_Load(object sender, EventArgs e)
        {
            TheHienDanhSachSanPham();
            ShowTreListProduction();
            ShowStagesCode();
            TheHienToThucHien();//Thể hiện tổ thực hiện
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
            point = gvSanPham.GetFocusedDisplayText();
            txtMaSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(masp_sanpham);
            txtSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(tensanpham_sanpham);
            txtIDSanPham.Text = gvSanPham.GetFocusedRowCellDisplayText(idsanpham_sanpham);

            ShowTreListProductionfollowProduction();
            ShowTreeListfollowIDProductionStages();
        }
        private void ShowTreListProduction()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select *
                    from tblSanPhamTreeList 
                    order by ID desc,NgayLap DESC");
            treeListProduction.DataSource = kn.laybang(sqlQuery);
            treeListProduction.ForceInitialize();
            treeListProduction.ExpandAll();
            //treeListProduction.BestFitColumns();
            treeListProduction.OptionsSelection.MultiSelect = true;
            //treeList1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
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
            List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            foreach (TreeListNode node in list)
            {
                MessageBox.Show("" + node.GetDisplayText(idtree), "");
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            EditTreeList();
        }
        private void EditTreeList()
        {
            List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                    @"update tblSanPhamTreeList set
                            MaLoai=N'{0}',
                            TenLoai=N'{1}',SoLuong='{2}',DienGiai=N'{3}',
                            NguoiLap=N'{4}',NgayLap=GetDate(),DonVi=N'{5}' where ID like '{6}'",
                        node.GetDisplayText(maloaitree),
                        node.GetDisplayText(tenloaitree),
                        node.GetDisplayText(soluongtree),
                        node.GetDisplayText(diengiaitree),
                        Login.Username,
                        node.GetDisplayText(donvitree),
                        node.GetDisplayText(idtree));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        
        private void DeleteTreeList()
        {
            List<TreeListNode> list = treeListProduction.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"delete from 
                        tblSanPhamTreeList 
                        where ID like '{0}';
                        delete from 
                        tblSanPhamTreeList 
                        where ParentID like '{0}' 
                        and (DonGiaCongDoan like '' or DonGiaCongDoan is null)",
                        node.GetDisplayText(idtree));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        private void treeListProduction_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string point = "";
            point = treeListProduction.GetFocusedDisplayText();
            txtParentNode.Text = treeListProduction.GetFocusedRowCellDisplayText(idtree);//Khi tao them node con thì node hiện hành là node cha
            maloai = treeListProduction.GetFocusedRowCellDisplayText(maloaitree);
            tenloai = treeListProduction.GetFocusedRowCellDisplayText(tenloaitree);
            idsanpham = treeListProduction.GetFocusedRowCellDisplayText(idsanphamtree);
            txtMucDoCha.Text = treeListProduction.GetFocusedRowCellDisplayText(mucdotree);
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
        private void treeListProduction_DoubleClick(object sender, EventArgs e)
        {
            frmChildNode childNode = new
                frmChildNode(idsanpham, maloai, tenloai, txtParentNode.Text,
                mucDoCon.ToString(),txtMaSanPham.Text,txtSanPham.Text);
            childNode.ShowDialog();
            ShowTreListProductionfollowProduction();
        }

        private void treeListProduction_MouseMove(object sender, MouseEventArgs e)
        {
            if (treeListProduction.GetAllCheckedNodes().Count()>0)
            {
                btnDelete.Enabled = true;
                btnEditTreeList.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnEditTreeList.Enabled = false;
            }
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
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select case when DonGiaCongDoan>0 then 'x' end CoGia,* from tblSanPhamTreeList
				order by IDSanPham Desc, ThuTu ASC");
            treeListProductionStages.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStages.ForceInitialize();
            treeListProductionStages.ExpandAll();
            //treeListProductionStages.BestFitColumns();
            treeListProductionStages.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
        }
        private void ShowTreeListfollowIDProductionStages()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select case when DonGiaCongDoan>0 then 'x' end CoGia,* from tblSanPhamTreeList
                    where IDSanPham like '{0}' order by ThuTu ASC", txtIDSanPham.Text);
            treeListProductionStages.DataSource =
                kn.laybang(sqlQuery);
            treeListProductionStages.ForceInitialize();
            treeListProductionStages.ExpandAll();
            //treeListProductionStages.BestFitColumns();
            treeListProductionStages.OptionsSelection.MultiSelect = true;
            kn.dongketnoi();
        }
        private void treeListCongDoanSanPham_DoubleClick(object sender, EventArgs e)
        {
            //sự kiện double click show form thêm công đoạn chi tiết sản phẩm
            frmChildNodeStages childNodeStages = new
            frmChildNodeStages(idsanpham, maloai, tenloai, txtParentNode.Text,
                mucDoCon.ToString(), soluongchitiet.ToString(),txtMaSanPham.Text,
                txtSanPham.Text,maloai,tenloai,soluongchitiet);
            childNodeStages.ShowDialog();
            ShowTreeListfollowIDProductionStages();
        }
        private void EditTreeListStages()
        {
            List<TreeListNode> list = treeListProductionStages.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                    @"update tblSanPhamTreeList set
                            MaLoai=N'{0}',
                            TenLoai=N'{1}',
                            DinhMucCongDoan='{2}',ToThucHien=N'{3}',ThuTu='{4}',
                            DienGiaiCongDoan=N'{5}',SoLanCongDoan=N'{6}',MaCong=N'{7}',
                            NguoiLap=N'{8}',NgayLap=GetDate(),MaSanPham=N'{9}',TenSanPham= N'{10}',TenCong=N'{11}',
                            MaChiTiet=N'{12}',TenChiTiet=N'{13}',SoChiTiet='{14}',DonVi='{15}' where ID like '{16}'",
                        node.GetDisplayText(treeListStagesMa),
                        node.GetDisplayText(treeListStagesTen),
                        node.GetDisplayText(treeListStagesDinhMuc),
                        node.GetDisplayText(treeListStagesToThucHien),
                        node.GetDisplayText(treeListStagesThuTu),
                        node.GetDisplayText(treeListStagesDienGiaiCongDoan),
                        node.GetDisplayText(treeListStagesSoLanLam),
                        node.GetDisplayText(treeListStagesMaGiaiDoan),
                       Login.Username,
                       node.GetDisplayText(treeListStagesMaSanPham),
                       node.GetDisplayText(treeListStagesSanPham),
                       node.GetDisplayText(treeListStagesTenCongDoan),
                       node.GetDisplayText(treeListStagesMa),
                       node.GetDisplayText(treeListStagesTen),
                       node.GetDisplayText(treeListStagesSoChiTiet),
                       node.GetDisplayText(treeListStagesDonVi),
                       node.GetDisplayText(treeListStagesID));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            CapNhatChiTiet();
            ShowTreeListfollowIDProductionStages();
        }
        private void CapNhatChiTiet()
        {
            Model.Function.ConnectSanXuat();
            string sql = string.Format(@"update tblSanPhamTreeList set MaChiTiet = N'{0}',TenChiTiet = N'{1}',
                SoLuong='{2}',SoChiTiet='{2}'
                where ParentID ='{3}'",
                txtMaChiTietCanSua.Text,
                txtChiTietCanSua.Text,
                txtSoLuongChiTietCanSua.Text,
                txtParentNode.Text);
            var kq = Model.Function.GetDataTable(sql);
        }
        private void DeleteTreeListStages()
        {
            List<TreeListNode> list = treeListProductionStages.GetAllCheckedNodes();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            foreach (TreeListNode node in list)
            {
                string strQuery = string.Format(
                      @"delete from 
                        tblSanPhamTreeList 
                        where ID like '{0}';
                        delete from 
                        tblSanPhamTreeList 
                        where ParentID like '{0}' and MaCong <>'' and (DonGiaCongDoan like '' or DonGiaCongDoan is null) ",
                        node.GetDisplayText(idtree));
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            ShowTreeListfollowIDProductionStages();
        }
        private void btnCapNhatCongDoanSanPham_Click(object sender, EventArgs e)
        {
            EditTreeListStages();
        }

        private void btnXoaCongDoanSanPham_Click(object sender, EventArgs e)
        {
            DeleteTreeListStages();
        }

        private void treeListCongDoanSanPham_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string point = "";
            point = treeListProductionStages.GetFocusedDisplayText();
            txtParentNode.Text = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesID);//Khi tao them node con thì node hiện hành là node cha
            maloai = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesMa);
            tenloai = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesTen);
            idsanpham = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesIDSanPham);
            txtMucDoCha.Text = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesMucDo);
            soluongchitiet= treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesSoLuong);
            txtSoLuongChiTietCanSua.Text= treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesSoLuong);
            txtChiTietCanSua.Text= treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesTen);
            txtMaChiTietCanSua.Text = treeListProductionStages.GetFocusedRowCellDisplayText(treeListStagesMaChiTiet);
            //txtIDSanPham.Text = idsanpham;
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
        {// sự kiện rê chuột vào lưới nêu trong lưới có check thì bật các nut cho xóa sửa ngược lại thì không cho
            if (treeListProductionStages.GetAllCheckedNodes().Count() > 0)
            {
                btnDeleteTreeListStages.Enabled = true;
                btnEditTreeListStages.Enabled = true;
            }
            else
            {
                btnDeleteTreeListStages.Enabled = false;
                btnEditTreeListStages.Enabled = false;
            }
        }

        private void treeListProductionStages_MouseHover(object sender, EventArgs e)
        {
            //nêu trong lưới có check thì bật các nut cho xóa sửa ngược lại thì không cho
            if (treeListProductionStages.GetAllCheckedNodes().Count() > 0)
            {
                btnDeleteTreeListStages.Enabled = true;
                btnEditTreeListStages.Enabled = true;
            }
            else
            {
                btnDeleteTreeListStages.Enabled = false;
                btnEditTreeListStages.Enabled = false;
            }
        }

        private void btnExportTreeListStages_Click(object sender, EventArgs e)
        {
            treeListProductionStages.ShowPrintPreview();
        }
        private void ShowStagesCode()
        {
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select * from tblResources order by ResourceID ASC");
            repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(sqlQuery);
            repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            kn.dongketnoi();
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
            repositoryItemCBToThucHien.Items.Clear();
            ketnoi kn = new ketnoi();
            string sqlQuery = string.Format(@"select ToThucHien from tblResources");
            var data = kn.laybang(sqlQuery);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                repositoryItemCBToThucHien.Items.Add(data.Rows[i]["ToThucHien"]);
            }
            kn.dongketnoi();

            //ketnoi kn = new ketnoi();
            //string sqlQuery = string.Format(@"select * from tblResources order by ResourceID ASC");
            //repositoryItemGridLookUpEditMaCongDoan.DataSource = kn.laybang(sqlQuery);
            //repositoryItemGridLookUpEditMaCongDoan.DisplayMember = "Ma_Nguonluc";
            //repositoryItemGridLookUpEditMaCongDoan.ValueMember = "Ma_Nguonluc";
            //kn.dongketnoi();
        }

        #region sự kiện checkedchildNode đồng thời checkedParentNode
        //private bool OneOfChildsIsChecked(TreeListNode node)
        //{
        //    bool result = false;
        //    foreach (TreeListNode item in node.Nodes)
        //    {
        //        if (item.CheckState == CheckState.Checked)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
        private void treeListProductionStages_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            //TreeListNode node = e.Node;
            //if (node.Checked)
            //{
            //    node.UncheckAll();
            //}
            //else
            //{
            //    node.CheckAll();
            //}
            //while (node.ParentNode != null)
            //{
            //    node = node.ParentNode;
            //    bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
            //    if (oneOfChildIsChecked)
            //    {
            //        node.CheckState = CheckState.Checked;
            //    }
            //    else
            //    {
            //        node.CheckState = CheckState.Unchecked;
            //    }
            //}
        }
        #endregion

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
