namespace quanlysanxuat
{
    partial class gridcontrol_and_LookupEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gridcontrol_and_LookupEdit));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Mavlphu_gd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Mavlphu_repos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Dongia_repos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Soluong_gd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Dongia_gd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.thanhtien_gd = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItem});
            this.gridControl1.Size = new System.Drawing.Size(800, 450);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Mavlphu_gd,
            this.Soluong_gd,
            this.Dongia_gd,
            this.thanhtien_gd});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // Mavlphu_gd
            // 
            this.Mavlphu_gd.Caption = "MA VAT TU";
            this.Mavlphu_gd.ColumnEdit = this.repositoryItem;
            this.Mavlphu_gd.FieldName = "Mavlphu";
            this.Mavlphu_gd.Name = "Mavlphu_gd";
            this.Mavlphu_gd.Visible = true;
            this.Mavlphu_gd.VisibleIndex = 0;
            this.Mavlphu_gd.Width = 107;
            // 
            // repositoryItem
            // 
            this.repositoryItem.AutoHeight = false;
            this.repositoryItem.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItem.Name = "repositoryItem";
            this.repositoryItem.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Mavlphu_repos,
            this.Dongia_repos});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Mavlphu_repos
            // 
            this.Mavlphu_repos.Caption = "MÃ VẬT TƯ";
            this.Mavlphu_repos.FieldName = "Mavlphu";
            this.Mavlphu_repos.Name = "Mavlphu_repos";
            this.Mavlphu_repos.Visible = true;
            this.Mavlphu_repos.VisibleIndex = 0;
            // 
            // Dongia_repos
            // 
            this.Dongia_repos.Caption = "ĐƠN GIÁ";
            this.Dongia_repos.FieldName = "Dongia";
            this.Dongia_repos.Name = "Dongia_repos";
            this.Dongia_repos.Visible = true;
            this.Dongia_repos.VisibleIndex = 1;
            // 
            // Soluong_gd
            // 
            this.Soluong_gd.Caption = "SỐ LƯỢNG";
            this.Soluong_gd.FieldName = "Soluong";
            this.Soluong_gd.Name = "Soluong_gd";
            this.Soluong_gd.Visible = true;
            this.Soluong_gd.VisibleIndex = 1;
            this.Soluong_gd.Width = 319;
            // 
            // Dongia_gd
            // 
            this.Dongia_gd.Caption = "ĐƠN GIÁ";
            this.Dongia_gd.FieldName = "Dongia";
            this.Dongia_gd.Name = "Dongia_gd";
            this.Dongia_gd.Visible = true;
            this.Dongia_gd.VisibleIndex = 2;
            this.Dongia_gd.Width = 356;
            // 
            // thanhtien_gd
            // 
            this.thanhtien_gd.Caption = "THÀNH TIỀN";
            this.thanhtien_gd.Name = "thanhtien_gd";
            this.thanhtien_gd.Visible = true;
            this.thanhtien_gd.VisibleIndex = 3;
            // 
            // gridcontrol_and_LookupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "gridcontrol_and_LookupEdit";
            this.Text = "gridcontrol_and_LookupEdit";
            this.Load += new System.EventHandler(this.gridcontrol_and_LookupEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Mavlphu_gd;
        private DevExpress.XtraGrid.Columns.GridColumn Soluong_gd;
        private DevExpress.XtraGrid.Columns.GridColumn Dongia_gd;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItem;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn Mavlphu_repos;
        private DevExpress.XtraGrid.Columns.GridColumn Dongia_repos;
        private DevExpress.XtraGrid.Columns.GridColumn thanhtien_gd;
    }
}