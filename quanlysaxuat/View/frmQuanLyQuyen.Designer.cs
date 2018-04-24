
namespace quanlysanxuat
{
    partial class frmQuanLyQuyen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyQuyen));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grUser = new DevExpress.XtraGrid.GridControl();
            this.gvUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplication = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPasswork = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAdd = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEdit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDelete = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDisable = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMenu = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentMenu = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnSearchWorkLocation = new DevExpress.XtraEditors.SimpleButton();
            this.grBoPhanPhuTrach = new DevExpress.XtraGrid.GridControl();
            this.gvBoPhanPhuTrach = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWorkLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnLamMoi = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveWorkLocation = new DevExpress.XtraBars.BarButtonItem();
            this.btnQuanLyTaiKhoan = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoaLocation = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grBoPhanPhuTrach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoPhanPhuTrach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 29);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grUser);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(985, 633);
            this.splitContainerControl1.SplitterPosition = 405;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // grUser
            // 
            this.grUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grUser.Location = new System.Drawing.Point(0, 0);
            this.grUser.MainView = this.gvUser;
            this.grUser.Name = "grUser";
            this.grUser.Size = new System.Drawing.Size(405, 633);
            this.grUser.TabIndex = 0;
            this.grUser.UseEmbeddedNavigator = true;
            this.grUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUser});
            this.grUser.Click += new System.EventHandler(this.grUser_Click_1);
            // 
            // gvUser
            // 
            this.gvUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.colApplication,
            this.colPasswork});
            this.gvUser.GridControl = this.grUser;
            this.gvUser.Name = "gvUser";
            this.gvUser.OptionsView.ColumnAutoWidth = false;
            this.gvUser.OptionsView.ShowAutoFilterRow = true;
            this.gvUser.OptionsView.ShowGroupPanel = false;
            this.gvUser.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvUser_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "UserName";
            this.gridColumn1.FieldName = "UserName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 52;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "LastName";
            this.gridColumn2.FieldName = "LastName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 108;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "FirstName";
            this.gridColumn3.FieldName = "FirstName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 48;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Roile";
            this.gridColumn4.FieldName = "Id";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 24;
            // 
            // colApplication
            // 
            this.colApplication.AppearanceHeader.Options.UseTextOptions = true;
            this.colApplication.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colApplication.Caption = "Application";
            this.colApplication.FieldName = "Application";
            this.colApplication.Name = "colApplication";
            this.colApplication.Visible = true;
            this.colApplication.VisibleIndex = 4;
            this.colApplication.Width = 29;
            // 
            // colPasswork
            // 
            this.colPasswork.AppearanceHeader.Options.UseTextOptions = true;
            this.colPasswork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPasswork.Caption = "AppPassWordHash";
            this.colPasswork.FieldName = "AppPasswordHash";
            this.colPasswork.Name = "colPasswork";
            this.colPasswork.Visible = true;
            this.colPasswork.VisibleIndex = 5;
            this.colPasswork.Width = 101;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.btnSearchWorkLocation);
            this.splitContainerControl2.Panel2.Controls.Add(this.grBoPhanPhuTrach);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(574, 633);
            this.splitContainerControl2.SplitterPosition = 290;
            this.splitContainerControl2.TabIndex = 1;
            // 
            // treeList1
            // 
            this.treeList1.CaptionHeight = 2;
            this.treeList1.ColumnPanelRowHeight = 1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDescription,
            this.colAdd,
            this.colEdit,
            this.colDelete,
            this.colDisable,
            this.colMenu,
            this.colParentMenu});
            this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.KeyFieldName = "Menu";
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.treeList1.ParentFieldName = "ParentMenu";
            this.treeList1.Size = new System.Drawing.Size(574, 290);
            this.treeList1.TabIndex = 0;
            this.treeList1.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.treeList1_BeforeCheckNode);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList1_CellValueChanging);
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDescription.AppearanceHeader.Options.UseFont = true;
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.Caption = "Danh mục[Menu]";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowFocus = false;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 0;
            this.colDescription.Width = 256;
            // 
            // colAdd
            // 
            this.colAdd.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAdd.AppearanceHeader.Options.UseFont = true;
            this.colAdd.AppearanceHeader.Options.UseTextOptions = true;
            this.colAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAdd.Caption = "Thêm[AllowAddNew]";
            this.colAdd.FieldName = "AllowAddNew";
            this.colAdd.Name = "colAdd";
            this.colAdd.Visible = true;
            this.colAdd.VisibleIndex = 1;
            this.colAdd.Width = 42;
            // 
            // colEdit
            // 
            this.colEdit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colEdit.AppearanceHeader.Options.UseFont = true;
            this.colEdit.AppearanceHeader.Options.UseTextOptions = true;
            this.colEdit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEdit.Caption = "Sửa[AllowEdit]";
            this.colEdit.FieldName = "AllowEdit";
            this.colEdit.Name = "colEdit";
            this.colEdit.Visible = true;
            this.colEdit.VisibleIndex = 2;
            this.colEdit.Width = 33;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Xóa[AllowDelete]";
            this.colDelete.FieldName = "AllowDelete";
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 3;
            this.colDelete.Width = 50;
            // 
            // colDisable
            // 
            this.colDisable.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDisable.AppearanceHeader.Options.UseFont = true;
            this.colDisable.AppearanceHeader.Options.UseTextOptions = true;
            this.colDisable.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDisable.Caption = "Cấm [Disable]";
            this.colDisable.FieldName = "Disable";
            this.colDisable.Name = "colDisable";
            this.colDisable.Visible = true;
            this.colDisable.VisibleIndex = 4;
            this.colDisable.Width = 169;
            // 
            // colMenu
            // 
            this.colMenu.Caption = "colMenu";
            this.colMenu.FieldName = "Menu";
            this.colMenu.Name = "colMenu";
            // 
            // colParentMenu
            // 
            this.colParentMenu.Caption = "ParentMenu";
            this.colParentMenu.FieldName = "ParentMenu";
            this.colParentMenu.Name = "colParentMenu";
            // 
            // btnSearchWorkLocation
            // 
            this.btnSearchWorkLocation.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchWorkLocation.ImageOptions.Image")));
            this.btnSearchWorkLocation.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearchWorkLocation.Location = new System.Drawing.Point(-1, 1);
            this.btnSearchWorkLocation.Name = "btnSearchWorkLocation";
            this.btnSearchWorkLocation.Size = new System.Drawing.Size(23, 21);
            this.btnSearchWorkLocation.TabIndex = 2;
            this.btnSearchWorkLocation.Click += new System.EventHandler(this.btnSearchWorkLocation_Click);
            // 
            // grBoPhanPhuTrach
            // 
            this.grBoPhanPhuTrach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoPhanPhuTrach.Location = new System.Drawing.Point(0, 0);
            this.grBoPhanPhuTrach.MainView = this.gvBoPhanPhuTrach;
            this.grBoPhanPhuTrach.Name = "grBoPhanPhuTrach";
            this.grBoPhanPhuTrach.Size = new System.Drawing.Size(574, 337);
            this.grBoPhanPhuTrach.TabIndex = 1;
            this.grBoPhanPhuTrach.UseEmbeddedNavigator = true;
            this.grBoPhanPhuTrach.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBoPhanPhuTrach});
            // 
            // gvBoPhanPhuTrach
            // 
            this.gvBoPhanPhuTrach.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWorkLocation,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn5,
            this.gridColumn6});
            this.gvBoPhanPhuTrach.GridControl = this.grBoPhanPhuTrach;
            this.gvBoPhanPhuTrach.Name = "gvBoPhanPhuTrach";
            this.gvBoPhanPhuTrach.OptionsSelection.MultiSelect = true;
            this.gvBoPhanPhuTrach.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvBoPhanPhuTrach.OptionsView.ColumnAutoWidth = false;
            this.gvBoPhanPhuTrach.OptionsView.ShowAutoFilterRow = true;
            this.gvBoPhanPhuTrach.OptionsView.ShowGroupPanel = false;
            // 
            // colWorkLocation
            // 
            this.colWorkLocation.Caption = "WorkLocations";
            this.colWorkLocation.FieldName = "ToThucHien";
            this.colWorkLocation.Name = "colWorkLocation";
            this.colWorkLocation.Visible = true;
            this.colWorkLocation.VisibleIndex = 1;
            this.colWorkLocation.Width = 201;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Mã tổ";
            this.gridColumn7.FieldName = "Ten_Nguonluc";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 254;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Enable";
            this.gridColumn8.FieldName = "AllowCheck";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 41;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "IDResources";
            this.gridColumn5.FieldName = "ResourceID";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 38;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "IDLocationUser";
            this.gridColumn6.FieldName = "ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnLamMoi,
            this.btnSave,
            this.barButtonItem3,
            this.btnQuanLyTaiKhoan,
            this.btnSaveWorkLocation,
            this.btnXoaLocation});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 6;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLamMoi),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveWorkLocation),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnQuanLyTaiKhoan),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnXoaLocation)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Caption = "Làm mới";
            this.btnLamMoi.Id = 0;
            this.btnLamMoi.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLamMoi.ImageOptions.SvgImage")));
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnLamMoi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLamMoi_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Lưu lại thông tin phân quyền";
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnSaveWorkLocation
            // 
            this.btnSaveWorkLocation.Caption = "Lưu vị trí làm việc";
            this.btnSaveWorkLocation.Id = 4;
            this.btnSaveWorkLocation.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSaveWorkLocation.ImageOptions.SvgImage")));
            this.btnSaveWorkLocation.Name = "btnSaveWorkLocation";
            this.btnSaveWorkLocation.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSaveWorkLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveWorkLocation_ItemClick);
            // 
            // btnQuanLyTaiKhoan
            // 
            this.btnQuanLyTaiKhoan.Caption = "Quản lý tài khoản";
            this.btnQuanLyTaiKhoan.Id = 3;
            this.btnQuanLyTaiKhoan.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.allowuserstoeditranges;
            this.btnQuanLyTaiKhoan.Name = "btnQuanLyTaiKhoan";
            this.btnQuanLyTaiKhoan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnQuanLyTaiKhoan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnQuanLyTaiKhoan_ItemClick);
            // 
            // btnXoaLocation
            // 
            this.btnXoaLocation.Caption = "Xóa Location";
            this.btnXoaLocation.Id = 5;
            this.btnXoaLocation.ImageOptions.Image = global::quanlysanxuat.Properties.Resources.delete_16x16;
            this.btnXoaLocation.Name = "btnXoaLocation";
            this.btnXoaLocation.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnXoaLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoaLocation_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(985, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 662);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(985, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 633);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(985, 29);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 633);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // frmQuanLyQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 662);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmQuanLyQuyen";
            this.Text = "Quản lý quyền";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grBoPhanPhuTrach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoPhanPhuTrach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUser;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAdd;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDelete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDisable;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnLamMoi;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMenu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentMenu;
        private DevExpress.XtraBars.BarButtonItem btnQuanLyTaiKhoan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl grBoPhanPhuTrach;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBoPhanPhuTrach;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkLocation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraBars.BarButtonItem btnSaveWorkLocation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btnSearchWorkLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colApplication;
        private DevExpress.XtraGrid.Columns.GridColumn colPasswork;
        private DevExpress.XtraBars.BarButtonItem btnXoaLocation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}