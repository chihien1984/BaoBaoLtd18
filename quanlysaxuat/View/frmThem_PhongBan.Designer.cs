namespace quanlysanxuat
{
    partial class frmThemPhongBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemPhongBan));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnGhi = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnTraCuu = new DevExpress.XtraEditors.SimpleButton();
            this.lbNguoi_Dung = new System.Windows.Forms.Label();
            this.ntnNew = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BoPhan_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaBoPhan_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NguoiLap_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayGhi_grid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.exporttoxls;
            this.btnExport.Location = new System.Drawing.Point(201, 1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 31);
            this.btnExport.TabIndex = 1245;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.editnames;
            this.btnSua.Location = new System.Drawing.Point(64, 1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(62, 31);
            this.btnSua.TabIndex = 1243;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnGhi
            // 
            this.btnGhi.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGhi.Appearance.Options.UseFont = true;
            this.btnGhi.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.save;
            this.btnGhi.Location = new System.Drawing.Point(1, 1);
            this.btnGhi.Name = "btnGhi";
            this.btnGhi.Size = new System.Drawing.Size(58, 31);
            this.btnGhi.TabIndex = 1242;
            this.btnGhi.TabStop = false;
            this.btnGhi.Text = "Ghi";
            this.btnGhi.Click += new System.EventHandler(this.btnGhi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.snapdeletelist;
            this.btnXoa.Location = new System.Drawing.Point(130, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(66, 31);
            this.btnXoa.TabIndex = 1244;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnTraCuu);
            this.panelControl1.Controls.Add(this.lbNguoi_Dung);
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnSua);
            this.panelControl1.Controls.Add(this.btnGhi);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 33);
            this.panelControl1.TabIndex = 1246;
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnThoat.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.close;
            this.btnThoat.Location = new System.Drawing.Point(768, 1);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(31, 30);
            this.btnThoat.TabIndex = 1251;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnTraCuu.ImageOptions.SvgImage = global::quanlysanxuat.Properties.Resources.actions_zoom1;
            this.btnTraCuu.Location = new System.Drawing.Point(282, 1);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(85, 31);
            this.btnTraCuu.TabIndex = 1248;
            this.btnTraCuu.TabStop = false;
            this.btnTraCuu.Text = "Tra cứu";
            this.btnTraCuu.Click += new System.EventHandler(this.BtnList_CongDoan_Click);
            // 
            // lbNguoi_Dung
            // 
            this.lbNguoi_Dung.AutoSize = true;
            this.lbNguoi_Dung.Location = new System.Drawing.Point(672, 4);
            this.lbNguoi_Dung.Name = "lbNguoi_Dung";
            this.lbNguoi_Dung.Size = new System.Drawing.Size(19, 13);
            this.lbNguoi_Dung.TabIndex = 1250;
            this.lbNguoi_Dung.Text = "    ";
            // 
            // ntnNew
            // 
            this.ntnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ntnNew.ImageOptions.Image")));
            this.ntnNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.ntnNew.Location = new System.Drawing.Point(0, 31);
            this.ntnNew.Name = "ntnNew";
            this.ntnNew.Size = new System.Drawing.Size(17, 21);
            this.ntnNew.TabIndex = 1250;
            this.ntnNew.Click += new System.EventHandler(this.ntnNew_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 33);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit2});
            this.gridControl1.Size = new System.Drawing.Size(800, 417);
            this.gridControl1.TabIndex = 1249;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID_grid1,
            this.BoPhan_grid1,
            this.MaBoPhan_grid1,
            this.NguoiLap_grid1,
            this.NgayGhi_grid1});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.Gold;
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[TRANGTHAI] <> \'\'";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // ID_grid1
            // 
            this.ID_grid1.AppearanceHeader.Options.UseTextOptions = true;
            this.ID_grid1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID_grid1.Caption = "ID";
            this.ID_grid1.FieldName = "ID";
            this.ID_grid1.Name = "ID_grid1";
            this.ID_grid1.OptionsColumn.AllowEdit = false;
            this.ID_grid1.Visible = true;
            this.ID_grid1.VisibleIndex = 5;
            this.ID_grid1.Width = 32;
            // 
            // BoPhan_grid1
            // 
            this.BoPhan_grid1.AppearanceHeader.Options.UseTextOptions = true;
            this.BoPhan_grid1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.BoPhan_grid1.Caption = "Bộ phận";
            this.BoPhan_grid1.FieldName = "BoPhan";
            this.BoPhan_grid1.Name = "BoPhan_grid1";
            this.BoPhan_grid1.Visible = true;
            this.BoPhan_grid1.VisibleIndex = 2;
            this.BoPhan_grid1.Width = 297;
            // 
            // MaBoPhan_grid1
            // 
            this.MaBoPhan_grid1.AppearanceHeader.Options.UseTextOptions = true;
            this.MaBoPhan_grid1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaBoPhan_grid1.Caption = "Mã bộ phận";
            this.MaBoPhan_grid1.FieldName = "MaBoPhan";
            this.MaBoPhan_grid1.Name = "MaBoPhan_grid1";
            this.MaBoPhan_grid1.Visible = true;
            this.MaBoPhan_grid1.VisibleIndex = 1;
            this.MaBoPhan_grid1.Width = 83;
            // 
            // NguoiLap_grid1
            // 
            this.NguoiLap_grid1.AppearanceHeader.Options.UseTextOptions = true;
            this.NguoiLap_grid1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NguoiLap_grid1.Caption = "Người lập";
            this.NguoiLap_grid1.FieldName = "NguoiLap";
            this.NguoiLap_grid1.Name = "NguoiLap_grid1";
            this.NguoiLap_grid1.OptionsColumn.AllowEdit = false;
            this.NguoiLap_grid1.Visible = true;
            this.NguoiLap_grid1.VisibleIndex = 3;
            this.NguoiLap_grid1.Width = 99;
            // 
            // NgayGhi_grid1
            // 
            this.NgayGhi_grid1.AppearanceHeader.Options.UseTextOptions = true;
            this.NgayGhi_grid1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgayGhi_grid1.Caption = "Ngày lập";
            this.NgayGhi_grid1.FieldName = "NgayGhi";
            this.NgayGhi_grid1.Name = "NgayGhi_grid1";
            this.NgayGhi_grid1.OptionsColumn.AllowEdit = false;
            this.NgayGhi_grid1.Visible = true;
            this.NgayGhi_grid1.VisibleIndex = 4;
            this.NgayGhi_grid1.Width = 129;
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.Mask.EditMask = "99/99/00";
            this.repositoryItemDateEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // frmThemPhongBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ntnNew);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmThemPhongBan.IconOptions.Icon")));
            this.Name = "frmThemPhongBan";
            this.Text = "DANH MỤC PHÒNG BAN";
            this.Load += new System.EventHandler(this.frmPhongBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnGhi;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTraCuu;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ID_grid1;
        private DevExpress.XtraGrid.Columns.GridColumn BoPhan_grid1;
        private DevExpress.XtraGrid.Columns.GridColumn MaBoPhan_grid1;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiLap_grid1;
        private DevExpress.XtraGrid.Columns.GridColumn NgayGhi_grid1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraEditors.SimpleButton ntnNew;
        private System.Windows.Forms.Label lbNguoi_Dung;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
    }
}