namespace quanlysanxuat
{
    partial class frmThemVatLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemVatLieu));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.dpden_ngay = new System.Windows.Forms.DateTimePicker();
            this.dptu_ngay = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Mavatlieu_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenVatLieu_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Code_grid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaVatLieu = new System.Windows.Forms.TextBox();
            this.txtTenVatLieu = new System.Windows.Forms.TextBox();
            this.btnsua = new DevExpress.XtraEditors.SimpleButton();
            this.btnxoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnthem = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnExport);
            this.groupControl1.Controls.Add(this.textBox1);
            this.groupControl1.Controls.Add(this.label39);
            this.groupControl1.Controls.Add(this.dpden_ngay);
            this.groupControl1.Controls.Add(this.dptu_ngay);
            this.groupControl1.Controls.Add(this.label40);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.txtCode);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1027, 344);
            this.groupControl1.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(470, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(59, 18);
            this.btnExport.TabIndex = 1039;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(659, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(96, 18);
            this.textBox1.TabIndex = 1038;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Indigo;
            this.label39.Location = new System.Drawing.Point(154, 4);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(40, 11);
            this.label39.TabIndex = 1035;
            this.label39.Text = "Từ ngày";
            // 
            // dpden_ngay
            // 
            this.dpden_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpden_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpden_ngay.Location = new System.Drawing.Point(370, 0);
            this.dpden_ngay.Name = "dpden_ngay";
            this.dpden_ngay.Size = new System.Drawing.Size(93, 18);
            this.dpden_ngay.TabIndex = 1037;
            this.dpden_ngay.TabStop = false;
            // 
            // dptu_ngay
            // 
            this.dptu_ngay.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dptu_ngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptu_ngay.Location = new System.Drawing.Point(217, 0);
            this.dptu_ngay.Name = "dptu_ngay";
            this.dptu_ngay.Size = new System.Drawing.Size(99, 18);
            this.dptu_ngay.TabIndex = 1036;
            this.dptu_ngay.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Indigo;
            this.label40.Location = new System.Drawing.Point(321, 4);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(46, 11);
            this.label40.TabIndex = 1034;
            this.label40.Text = "Đến ngày";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 20);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1023, 322);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.BINDING);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Mavatlieu_grid,
            this.TenVatLieu_grid,
            this.Code_grid});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // Mavatlieu_grid
            // 
            this.Mavatlieu_grid.Caption = "Mã vật liệu";
            this.Mavatlieu_grid.FieldName = "MaVatLieu";
            this.Mavatlieu_grid.Name = "Mavatlieu_grid";
            this.Mavatlieu_grid.Visible = true;
            this.Mavatlieu_grid.VisibleIndex = 0;
            this.Mavatlieu_grid.Width = 188;
            // 
            // TenVatLieu_grid
            // 
            this.TenVatLieu_grid.Caption = "Tên vật liệu";
            this.TenVatLieu_grid.FieldName = "TenVatlieu";
            this.TenVatLieu_grid.Name = "TenVatLieu_grid";
            this.TenVatLieu_grid.Visible = true;
            this.TenVatLieu_grid.VisibleIndex = 1;
            this.TenVatLieu_grid.Width = 562;
            // 
            // Code_grid
            // 
            this.Code_grid.Caption = "Code";
            this.Code_grid.FieldName = "Code";
            this.Code_grid.Name = "Code_grid";
            this.Code_grid.Visible = true;
            this.Code_grid.VisibleIndex = 2;
            this.Code_grid.Width = 70;
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(70, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(74, 18);
            this.txtCode.TabIndex = 1018;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(626, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1016;
            this.label2.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1016;
            this.label1.Text = "Iden";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 1017;
            this.label4.Text = "Tên Vật liệu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1016;
            this.label3.Text = "Mã vật liệu";
            // 
            // txtMaVatLieu
            // 
            this.txtMaVatLieu.Location = new System.Drawing.Point(70, 3);
            this.txtMaVatLieu.Name = "txtMaVatLieu";
            this.txtMaVatLieu.Size = new System.Drawing.Size(74, 21);
            this.txtMaVatLieu.TabIndex = 1018;
            // 
            // txtTenVatLieu
            // 
            this.txtTenVatLieu.Location = new System.Drawing.Point(217, 3);
            this.txtTenVatLieu.Name = "txtTenVatLieu";
            this.txtTenVatLieu.Size = new System.Drawing.Size(538, 21);
            this.txtTenVatLieu.TabIndex = 1018;
            // 
            // btnsua
            // 
            this.btnsua.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsua.Appearance.Options.UseFont = true;
            this.btnsua.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsua.ImageOptions.Image")));
            this.btnsua.Location = new System.Drawing.Point(927, 3);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(45, 18);
            this.btnsua.TabIndex = 1021;
            this.btnsua.Text = "Sửa";
            this.btnsua.Click += new System.EventHandler(this.SUA_VATLIEU);
            // 
            // btnxoa
            // 
            this.btnxoa.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Appearance.Options.UseFont = true;
            this.btnxoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnxoa.ImageOptions.Image")));
            this.btnxoa.Location = new System.Drawing.Point(976, 3);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(46, 18);
            this.btnxoa.TabIndex = 1023;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.Click += new System.EventHandler(this.XOA_VATLIEU);
            // 
            // btnthem
            // 
            this.btnthem.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthem.Appearance.Options.UseFont = true;
            this.btnthem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnthem.ImageOptions.Image")));
            this.btnthem.Location = new System.Drawing.Point(869, 3);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(55, 18);
            this.btnthem.TabIndex = 1025;
            this.btnthem.TabStop = false;
            this.btnthem.Text = "Thêm";
            this.btnthem.Click += new System.EventHandler(this.THEM_VATLIEU);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnthem);
            this.panelControl1.Controls.Add(this.btnxoa);
            this.panelControl1.Controls.Add(this.btnsua);
            this.panelControl1.Controls.Add(this.txtTenVatLieu);
            this.panelControl1.Controls.Add(this.txtMaVatLieu);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1027, 28);
            this.panelControl1.TabIndex = 2;
            // 
            // frmThemVatLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 372);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmThemVatLieu";
            this.Text = "DANH MỤC VẬT LIỆU SẢN PHẨM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemVatLieu_FormClosing);
            this.Load += new System.EventHandler(this.FrmThemVatLieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn Mavatlieu_grid;
        private DevExpress.XtraGrid.Columns.GridColumn TenVatLieu_grid;
        private DevExpress.XtraGrid.Columns.GridColumn Code_grid;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.DateTimePicker dpden_ngay;
        private System.Windows.Forms.DateTimePicker dptu_ngay;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaVatLieu;
        private System.Windows.Forms.TextBox txtTenVatLieu;
        private DevExpress.XtraEditors.SimpleButton btnsua;
        private DevExpress.XtraEditors.SimpleButton btnxoa;
        private DevExpress.XtraEditors.SimpleButton btnthem;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}