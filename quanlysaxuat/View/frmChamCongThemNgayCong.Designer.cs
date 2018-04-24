namespace quanlysanxuat.View
{
    partial class frmChamCongThemNgayCong
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChamCongThemNgayCong));
            this.grThemNgayCong = new DevExpress.XtraGrid.GridControl();
            this.gvThemNgayCong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ngay_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditNgayLam = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditGioVaoGioRa = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxRaVao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEditChiTiet = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemDateEditNgayLam = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnGhiNgay = new DevExpress.XtraEditors.SimpleButton();
            this.btnThemNgay = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grThemNgayCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThemNgayCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditNgayLam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditGioVaoGioRa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRaVao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgayLam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgayLam.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grThemNgayCong
            // 
            this.grThemNgayCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grThemNgayCong.Location = new System.Drawing.Point(2, 2);
            this.grThemNgayCong.MainView = this.gvThemNgayCong;
            this.grThemNgayCong.Name = "grThemNgayCong";
            this.grThemNgayCong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEditChiTiet,
            this.repositoryItemTextEditGioVaoGioRa,
            this.repositoryItemComboBoxRaVao,
            this.repositoryItemDateEditNgayLam,
            this.repositoryItemTextEditNgayLam});
            this.grThemNgayCong.Size = new System.Drawing.Size(639, 313);
            this.grThemNgayCong.TabIndex = 1251;
            this.grThemNgayCong.UseEmbeddedNavigator = true;
            this.grThemNgayCong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvThemNgayCong});
            // 
            // gvThemNgayCong
            // 
            this.gvThemNgayCong.Appearance.Row.Options.UseTextOptions = true;
            this.gvThemNgayCong.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvThemNgayCong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ngay_,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.Gold;
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[TRANGTHAI] <> \'\'";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.gvThemNgayCong.FormatRules.Add(gridFormatRule1);
            this.gvThemNgayCong.GridControl = this.grThemNgayCong;
            this.gvThemNgayCong.Name = "gvThemNgayCong";
            this.gvThemNgayCong.OptionsCustomization.AllowFilter = false;
            this.gvThemNgayCong.OptionsSelection.MultiSelect = true;
            this.gvThemNgayCong.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvThemNgayCong.OptionsView.ColumnAutoWidth = false;
            this.gvThemNgayCong.OptionsView.RowAutoHeight = true;
            this.gvThemNgayCong.OptionsView.ShowAutoFilterRow = true;
            this.gvThemNgayCong.OptionsView.ShowFooter = true;
            this.gvThemNgayCong.OptionsView.ShowGroupPanel = false;
            // 
            // ngay_
            // 
            this.ngay_.AppearanceHeader.Options.UseTextOptions = true;
            this.ngay_.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ngay_.Caption = "Ngày";
            this.ngay_.ColumnEdit = this.repositoryItemTextEditNgayLam;
            this.ngay_.DisplayFormat.FormatString = "d";
            this.ngay_.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ngay_.FieldName = "TimeDate";
            this.ngay_.Name = "ngay_";
            this.ngay_.Visible = true;
            this.ngay_.VisibleIndex = 1;
            this.ngay_.Width = 145;
            // 
            // repositoryItemTextEditNgayLam
            // 
            this.repositoryItemTextEditNgayLam.AutoHeight = false;
            this.repositoryItemTextEditNgayLam.Mask.EditMask = "([0-2][0-9]|(3)[0-1])(\\/)(((0)[0-9])|((1)[0-2]))(\\/)\\d{4}";
            this.repositoryItemTextEditNgayLam.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEditNgayLam.Name = "repositoryItemTextEditNgayLam";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Giờ vào | Giờ ra";
            this.gridColumn2.ColumnEdit = this.repositoryItemTextEditGioVaoGioRa;
            this.gridColumn2.DisplayFormat.FormatString = "hh:mm";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "TimeStr";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 93;
            // 
            // repositoryItemTextEditGioVaoGioRa
            // 
            this.repositoryItemTextEditGioVaoGioRa.AutoHeight = false;
            this.repositoryItemTextEditGioVaoGioRa.Mask.EditMask = "[0-1]?[0-9]|[2][0-3]):([0-5][0-9]";
            this.repositoryItemTextEditGioVaoGioRa.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEditGioVaoGioRa.Name = "repositoryItemTextEditGioVaoGioRa";
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Mã vào ra";
            this.gridColumn3.ColumnEdit = this.repositoryItemComboBoxRaVao;
            this.gridColumn3.FieldName = "OriginType";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 68;
            // 
            // repositoryItemComboBoxRaVao
            // 
            this.repositoryItemComboBoxRaVao.AutoHeight = false;
            this.repositoryItemComboBoxRaVao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxRaVao.Name = "repositoryItemComboBoxRaVao";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Mã máy";
            this.gridColumn4.FieldName = "MachineNo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 50;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "WorkCode";
            this.gridColumn5.FieldName = "WorkCode";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 62;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Mask.EditMask = "99/99/00";
            this.repositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoEditChiTiet
            // 
            this.repositoryItemMemoEditChiTiet.Name = "repositoryItemMemoEditChiTiet";
            // 
            // repositoryItemDateEditNgayLam
            // 
            this.repositoryItemDateEditNgayLam.AutoHeight = false;
            this.repositoryItemDateEditNgayLam.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEditNgayLam.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEditNgayLam.CalendarTimeProperties.TouchUIMaxValue = new System.DateTime(9999, 12, 31, 0, 0, 0, 0);
            this.repositoryItemDateEditNgayLam.Mask.EditMask = "([0-2][0-9]|(3)[0-1])(\\/)(((0)[0-9])|((1)[0-2]))(\\/)\\d{4}";
            this.repositoryItemDateEditNgayLam.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemDateEditNgayLam.Name = "repositoryItemDateEditNgayLam";
            // 
            // txtUserID
            // 
            this.txtUserID.Enabled = false;
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(46, 1);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(81, 26);
            this.txtUserID.TabIndex = 1253;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1252;
            this.label1.Text = "Mã số";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnXoa);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnGhiNgay);
            this.panelControl1.Controls.Add(this.btnThemNgay);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtUserID);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(643, 29);
            this.panelControl1.TabIndex = 1254;
            // 
            // btnXoa
            // 
            this.btnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.Image")));
            this.btnXoa.Location = new System.Drawing.Point(553, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(51, 27);
            this.btnXoa.TabIndex = 1252;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(615, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 27);
            this.btnClose.TabIndex = 1252;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGhiNgay
            // 
            this.btnGhiNgay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGhiNgay.ImageOptions.Image")));
            this.btnGhiNgay.Location = new System.Drawing.Point(496, 1);
            this.btnGhiNgay.Name = "btnGhiNgay";
            this.btnGhiNgay.Size = new System.Drawing.Size(55, 27);
            this.btnGhiNgay.TabIndex = 1254;
            this.btnGhiNgay.Text = "Lưu";
            this.btnGhiNgay.Click += new System.EventHandler(this.btnGhiNgay_Click);
            // 
            // btnThemNgay
            // 
            this.btnThemNgay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThemNgay.ImageOptions.Image")));
            this.btnThemNgay.Location = new System.Drawing.Point(404, 1);
            this.btnThemNgay.Name = "btnThemNgay";
            this.btnThemNgay.Size = new System.Drawing.Size(89, 27);
            this.btnThemNgay.TabIndex = 1254;
            this.btnThemNgay.Text = "Thêm ngày";
            this.btnThemNgay.Click += new System.EventHandler(this.btnThemNgay_Click);
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(213, 1);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(190, 26);
            this.txtName.TabIndex = 1253;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1252;
            this.label2.Text = "Tên nhân viên";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grThemNgayCong);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 29);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(643, 317);
            this.panelControl2.TabIndex = 1255;
            // 
            // frmChamCongThemNgayCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 346);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChamCongThemNgayCong";
            this.Text = "frmChamCongThemNgayCong";
            this.Load += new System.EventHandler(this.frmChamCongThemNgayCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grThemNgayCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThemNgayCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditNgayLam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditGioVaoGioRa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRaVao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgayLam.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditNgayLam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grThemNgayCong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvThemNgayCong;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEditChiTiet;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn ngay_;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btnThemNgay;
        private DevExpress.XtraEditors.SimpleButton btnGhiNgay;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditGioVaoGioRa;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxRaVao;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditNgayLam;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditNgayLam;
    }
}