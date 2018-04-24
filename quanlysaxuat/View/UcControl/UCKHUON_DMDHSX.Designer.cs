namespace quanlysanxuat
{
    partial class UCKHUON_DMDHSX
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCKHUON_DMDHSX));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.show_CTsanpham = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCalcEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemCalcEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemCalcEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemFontEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.btnrefresh = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.label39 = new System.Windows.Forms.Label();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.btnexport = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.show_CTsanpham);
            this.groupControl3.Controls.Add(this.gridControl3);
            this.groupControl3.Controls.Add(this.btnrefresh);
            this.groupControl3.Controls.Add(this.simpleButton3);
            this.groupControl3.Controls.Add(this.label39);
            this.groupControl3.Controls.Add(this.dptu_ngay);
            this.groupControl3.Controls.Add(this.dpden_ngay);
            this.groupControl3.Controls.Add(this.label40);
            this.groupControl3.Controls.Add(this.btnexport);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1363, 444);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "QUẢN LÝ DANH MỤC KHUÔN SẢN PHẨM SẢN XUẤT ";
            // 
            // show_CTsanpham
            // 
            this.show_CTsanpham.Appearance.ForeColor = System.Drawing.Color.Green;
            this.show_CTsanpham.Appearance.Options.UseForeColor = true;
            this.show_CTsanpham.Image = ((System.Drawing.Image)(resources.GetObject("show_CTsanpham.Image")));
            this.show_CTsanpham.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.show_CTsanpham.Location = new System.Drawing.Point(0, 20);
            this.show_CTsanpham.Name = "show_CTsanpham";
            this.show_CTsanpham.Size = new System.Drawing.Size(19, 19);
            this.show_CTsanpham.TabIndex = 1039;
            this.show_CTsanpham.Click += new System.EventHandler(this.show_CTsanpham_Click);
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl3.Location = new System.Drawing.Point(2, 20);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit4,
            this.repositoryItemCalcEdit5,
            this.repositoryItemCalcEdit6,
            this.repositoryItemFontEdit2});
            this.gridControl3.Size = new System.Drawing.Size(1359, 422);
            this.gridControl3.TabIndex = 1038;
            this.gridControl3.UseEmbeddedNavigator = true;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.GreenYellow;
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[SOLUONGTP] >= [soluongsx]";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.gridView3.FormatRules.Add(gridFormatRule1);
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.MultiSelect = true;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.ShowAutoFilterRow = true;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemCalcEdit4
            // 
            this.repositoryItemCalcEdit4.AutoHeight = false;
            this.repositoryItemCalcEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit4.Name = "repositoryItemCalcEdit4";
            // 
            // repositoryItemCalcEdit5
            // 
            this.repositoryItemCalcEdit5.AutoHeight = false;
            this.repositoryItemCalcEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit5.Name = "repositoryItemCalcEdit5";
            // 
            // repositoryItemCalcEdit6
            // 
            this.repositoryItemCalcEdit6.AutoHeight = false;
            this.repositoryItemCalcEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit6.Name = "repositoryItemCalcEdit6";
            // 
            // repositoryItemFontEdit2
            // 
            this.repositoryItemFontEdit2.AutoHeight = false;
            this.repositoryItemFontEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit2.Name = "repositoryItemFontEdit2";
            // 
            // btnrefresh
            // 
            this.btnrefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnrefresh.Image")));
            this.btnrefresh.Location = new System.Drawing.Point(1060, 0);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(73, 18);
            this.btnrefresh.TabIndex = 1020;
            this.btnrefresh.Text = "Xem trễ";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(982, 0);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 18);
            this.simpleButton3.TabIndex = 1026;
            this.simpleButton3.Text = "Xem";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.ForeColor = System.Drawing.Color.Indigo;
            this.label39.Location = new System.Drawing.Point(673, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(47, 13);
            this.label39.TabIndex = 1023;
            this.label39.Text = "Từ ngày";
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(724, 0);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(99, 18);
            this.dptu_ngay.TabIndex = 1024;
            this.dptu_ngay.TabStop = false;
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(883, 0);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(93, 18);
            this.dpden_ngay.TabIndex = 1025;
            this.dpden_ngay.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.ForeColor = System.Drawing.Color.Indigo;
            this.label40.Location = new System.Drawing.Point(828, 3);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(54, 13);
            this.label40.TabIndex = 1022;
            this.label40.Text = "Đến ngày";
            // 
            // btnexport
            // 
            this.btnexport.Image = ((System.Drawing.Image)(resources.GetObject("btnexport.Image")));
            this.btnexport.Location = new System.Drawing.Point(1134, 0);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(69, 18);
            this.btnexport.TabIndex = 1021;
            this.btnexport.Text = "Export";
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "gridColumn25";
            this.gridColumn25.Name = "gridColumn25";
            // 
            // UCKHUON_DMDHSX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl3);
            this.Name = "UCKHUON_DMDHSX";
            this.Size = new System.Drawing.Size(1363, 444);
            this.Load += new System.EventHandler(this.UCKHUON_DMDHSX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnrefresh;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private System.Windows.Forms.Label label40;
        private DevExpress.XtraEditors.SimpleButton btnexport;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit2;
        private DevExpress.XtraEditors.SimpleButton show_CTsanpham;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
    }
}
