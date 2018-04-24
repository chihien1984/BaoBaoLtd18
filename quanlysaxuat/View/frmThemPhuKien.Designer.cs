namespace quanlysanxuat
{
    partial class frmThemPhuKien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemPhuKien));
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaCT_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenCT_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoLuong_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NguoiLap_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayLap_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ID_Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColMaNguonluc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTenNguonLuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.BtnGhi = new DevExpress.XtraBars.BarButtonItem();
            this.BtnSua = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.BtnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.LableUser = new DevExpress.XtraBars.BarStaticItem();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LbUser = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 20);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1});
            this.gridControl2.Size = new System.Drawing.Size(950, 438);
            this.gridControl2.TabIndex = 1217;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaCT_Col,
            this.TenCT_Col,
            this.SoLuong_Col,
            this.NguoiLap_Col,
            this.NgayLap_Col,
            this.ID_Col});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // MaCT_Col
            // 
            this.MaCT_Col.Caption = "MÃ PHỤ KIỆN";
            this.MaCT_Col.FieldName = "MaCT";
            this.MaCT_Col.Name = "MaCT_Col";
            this.MaCT_Col.Visible = true;
            this.MaCT_Col.VisibleIndex = 1;
            this.MaCT_Col.Width = 105;
            // 
            // TenCT_Col
            // 
            this.TenCT_Col.Caption = "TÊN LOẠI PHỤ KIỆN";
            this.TenCT_Col.FieldName = "TenCT";
            this.TenCT_Col.Name = "TenCT_Col";
            this.TenCT_Col.Visible = true;
            this.TenCT_Col.VisibleIndex = 2;
            this.TenCT_Col.Width = 373;
            // 
            // SoLuong_Col
            // 
            this.SoLuong_Col.Caption = "SỐ LƯỢNG";
            this.SoLuong_Col.FieldName = "SoLuong";
            this.SoLuong_Col.Name = "SoLuong_Col";
            this.SoLuong_Col.Visible = true;
            this.SoLuong_Col.VisibleIndex = 3;
            this.SoLuong_Col.Width = 65;
            // 
            // NguoiLap_Col
            // 
            this.NguoiLap_Col.Caption = "NGƯỜI LẬP";
            this.NguoiLap_Col.FieldName = "NguoiLap";
            this.NguoiLap_Col.Name = "NguoiLap_Col";
            this.NguoiLap_Col.OptionsColumn.AllowEdit = false;
            this.NguoiLap_Col.OptionsColumn.AllowFocus = false;
            this.NguoiLap_Col.Visible = true;
            this.NguoiLap_Col.VisibleIndex = 4;
            this.NguoiLap_Col.Width = 106;
            // 
            // NgayLap_Col
            // 
            this.NgayLap_Col.Caption = "NGÀY LẬP";
            this.NgayLap_Col.FieldName = "NgayLap";
            this.NgayLap_Col.Name = "NgayLap_Col";
            this.NgayLap_Col.OptionsColumn.AllowEdit = false;
            this.NgayLap_Col.OptionsColumn.AllowFocus = false;
            this.NgayLap_Col.Visible = true;
            this.NgayLap_Col.VisibleIndex = 5;
            this.NgayLap_Col.Width = 94;
            // 
            // ID_Col
            // 
            this.ID_Col.Caption = "ID";
            this.ID_Col.FieldName = "ID";
            this.ID_Col.Name = "ID_Col";
            this.ID_Col.OptionsColumn.AllowEdit = false;
            this.ID_Col.OptionsColumn.AllowFocus = false;
            this.ID_Col.Visible = true;
            this.ID_Col.VisibleIndex = 6;
            this.ID_Col.Width = 77;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.PopupView = this.gridView3;
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColMaNguonluc,
            this.ColTenNguonLuc});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // ColMaNguonluc
            // 
            this.ColMaNguonluc.Caption = "MÃ NGUỒN LỰC";
            this.ColMaNguonluc.FieldName = "Ma_NguonLuc";
            this.ColMaNguonluc.Name = "ColMaNguonluc";
            this.ColMaNguonluc.Visible = true;
            this.ColMaNguonluc.VisibleIndex = 0;
            // 
            // ColTenNguonLuc
            // 
            this.ColTenNguonLuc.Caption = "TÊN NGUỒN LỰC";
            this.ColTenNguonLuc.FieldName = "Ten_Nguonluc";
            this.ColTenNguonLuc.Name = "ColTenNguonLuc";
            this.ColTenNguonLuc.Visible = true;
            this.ColTenNguonLuc.VisibleIndex = 1;
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
            this.BtnGhi,
            this.BtnSua,
            this.BtnRefresh,
            this.BtnXoa,
            this.LableUser,
            this.barHeaderItem1});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnGhi),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnSua),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnXoa)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // BtnGhi
            // 
            this.BtnGhi.Caption = "Lưu";
            this.BtnGhi.Id = 0;
            this.BtnGhi.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnGhi.ImageOptions.Image")));
            this.BtnGhi.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnGhi.ImageOptions.LargeImage")));
            this.BtnGhi.Name = "BtnGhi";
            this.BtnGhi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGhi_ItemClick);
            // 
            // BtnSua
            // 
            this.BtnSua.Caption = "Sửa";
            this.BtnSua.Id = 1;
            this.BtnSua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSua.ImageOptions.Image")));
            this.BtnSua.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnSua.ImageOptions.LargeImage")));
            this.BtnSua.Name = "BtnSua";
            this.BtnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSua_ItemClick);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Caption = "Refresh";
            this.BtnRefresh.Id = 2;
            this.BtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnRefresh.ImageOptions.Image")));
            this.BtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnRefresh.ImageOptions.LargeImage")));
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnRefresh_ItemClick);
            // 
            // BtnXoa
            // 
            this.BtnXoa.Caption = "Xoa";
            this.BtnXoa.Id = 3;
            this.BtnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnXoa.ImageOptions.Image")));
            this.BtnXoa.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnXoa.ImageOptions.LargeImage")));
            this.BtnXoa.Name = "BtnXoa";
            this.BtnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnXoa_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(954, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 484);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(954, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 460);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(954, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 460);
            // 
            // LableUser
            // 
            this.LableUser.Caption = "User";
            this.LableUser.Id = 6;
            this.LableUser.Name = "LableUser";
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "barHeaderItem1";
            this.barHeaderItem1.Id = 7;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "NGÀY LẬP";
            this.gridColumn13.FieldName = "Ngaycapnhat";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "NGƯỜI LẬP";
            this.gridColumn12.FieldName = "Hotennv";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "SỐ LƯỢNG";
            this.gridColumn11.FieldName = "Soluong_CT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 54;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "TÊN CHI TIẾT";
            this.gridColumn10.FieldName = "Ten_ct";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 212;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "MÃ CHI TIẾT";
            this.gridColumn5.FieldName = "Mact";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 73;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "TÊN SẢN PHẨM";
            this.gridColumn9.FieldName = "Tensp";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 163;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "MÃ SẢN PHẨM";
            this.gridColumn8.FieldName = "Masp";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 95;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 76;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.LbUser);
            this.groupControl1.Controls.Add(this.gridControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 24);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(954, 460);
            this.groupControl1.TabIndex = 1222;
            // 
            // LbUser
            // 
            this.LbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbUser.AutoSize = true;
            this.LbUser.Location = new System.Drawing.Point(919, 3);
            this.LbUser.Name = "LbUser";
            this.LbUser.Size = new System.Drawing.Size(29, 13);
            this.LbUser.TabIndex = 1218;
            this.LbUser.Text = "User";
            // 
            // frmThemPhuKien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 484);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmThemPhuKien";
            this.Text = "DANH MỤC PHỤ KIỆN";
            this.Load += new System.EventHandler(this.frmThemPhuKien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn ColMaNguonluc;
        private DevExpress.XtraGrid.Columns.GridColumn ColTenNguonLuc;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem BtnGhi;
        private DevExpress.XtraBars.BarButtonItem BtnSua;
        private DevExpress.XtraBars.BarButtonItem BtnRefresh;
        private DevExpress.XtraBars.BarButtonItem BtnXoa;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraBars.BarStaticItem LableUser;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraGrid.Columns.GridColumn MaCT_Col;
        private DevExpress.XtraGrid.Columns.GridColumn TenCT_Col;
        private DevExpress.XtraGrid.Columns.GridColumn SoLuong_Col;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiLap_Col;
        private DevExpress.XtraGrid.Columns.GridColumn NgayLap_Col;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label LbUser;
        private DevExpress.XtraGrid.Columns.GridColumn ID_Col;

    }
}