//using AForge.Video;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ZXing;

namespace quanlysanxuat.View.UcControl
{
    public partial class ucChamCong : UserControl
    {
        public ucChamCong()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //if (btn_Connect.Text == "Connect")
            //{
            //    stream = new MJPEGStream(txt_url_DroidCam.Text);
            //    stream.NewFrame += stream_NewFrame;
            //    stream.Start();
            //    timer1.Enabled = true;
            //    timer1.Start();
            //    btn_Connect.Text = "Disconnect";
            //}
            //else
            //{
            //    btn_Connect.Text = "Connect";
            //    timer1.Stop();
            //    stream.Stop();
            //}
        }
        //public void stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
        //    pic_cam.Image = bmp;
        //}
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Bitmap img = (Bitmap)pic_cam.Image;
            //if (img != null)
            //{
            //    ZXing.BarcodeReader Reader = new ZXing.BarcodeReader();
            //    Result result = Reader.Decode(img);
            //    try
            //    {
            //        string decoded = result.ToString().Trim();
            //        if (!listBox1.Items.Contains(decoded))
            //        {
            //            listBox1.Items.Insert(0, decoded);
            //        }

            //        img.Dispose();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message + "");
            //    }

            //}
        }
        ///formload
        private void ucChamCong_Load(object sender, EventArgs e)
        {
            dpTu.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpDen.Text = DateTime.Now.ToString("dd-MM-yyyy");
            TheHienDanhMucPhongBan();
            gvChamCong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            //gvChamCong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            ChinhSuaChamCong();
            this.gvChamCong.Appearance.Row.Font = new Font("Segoe UI", 10f);
            this.gvTongHopChamCong.Appearance.Row.Font = new Font("Segoe UI", 10f);
            this.gvDanhSachNhanVien.Appearance.Row.Font = new Font("Segoe UI", 10f);
            this.treeListPhongBan.Appearance.Row.Font = new Font("Segoe UI", 10f);
        }
        //Phan uyen cho phep chinh sua cham cong
        private void ChinhSuaChamCong()
        {
            if (Login.role == "1" || Login.role == "39")
            {
                xtraTabPageChamCong.PageVisible = true;
                btnThemGioCong.Visible = true;
            }
            else
            {
                xtraTabPageChamCong.PageVisible = false;
                btnThemGioCong.Visible = false;
            }
        }
        //Treelist phong ban lam viec
        private void TheHienDanhMucPhongBan()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select RelationID ParentID,LevelID,
                ID,Description from RelationDept");
            treeListPhongBan.DataSource = Model.Function.GetDataTable(sqlQuery);
            treeListPhongBan.ForceInitialize();
            treeListPhongBan.ExpandAll();
            //treeListProductionStagesPlan.BestFitColumns();
            treeListPhongBan.OptionsSelection.MultiSelect = true;
            treeListPhongBan.Appearance.Row.Font = new Font("Segoe UI", 7f);
        }
        private void TheHienDanhSachNhanVienTheoPhongBan()
        {
            /*
            Connect connect = new Connect();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connect.ConnectChamCong;
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sqlQuery = string.Format(@"select a.*,b.* from
                (select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description
                from UserInfo a
                inner join RelationDept b
                on a.UserIDD=b.ID)a
				left outer join
				(select UserFullCode,sum(NgayCong)NgayCong,
				sum(PhutTangCa)PhutTangCa
				from GioCongview where TimeDate between '{0}' and '{1}'
				group by UserFullCode)b
				on a.UserFullCode=b.UserFullCode where UserIDD like '{2}' ", 
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), 
                deptID);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grDanhSachNhanVien.DataSource = dt;
            con.Close();*/

            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"execute TongHopNgayCongTheoPhongBan_proc '{0}','{1}','{2}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), deptID);
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);

        }
        private void TheHienTatCaNhanVien()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select a.*,b.* from
                (select UserFullCode,UserCardNo,
                UserFullName,UserIDD,b.Description
                from UserInfo a
                inner join RelationDept b
                on a.UserIDD=b.ID)a
				left outer join
				(select UserFullCode,sum(NgayCong)NgayCong,
				sum(PhutTangCa)PhutTangCa
				from GioCongview where TimeDate between '{0}' and '{1}'
				group by UserFullCode)b
				on a.UserFullCode=b.UserFullCode",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);
 
        }
        private void TheHienTatCaThoiGianLamViec()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select b.UserFullCode,b.UserCardNo,b.UserFullName,a.* from 
                (select * from CheckInOut where TimeDate 
				between '{0}' and '{1}')a
                left outer join
                (select * from UserInfo) b
                on a.UserEnrollNumber=b.UserEnrollNumber",
                dpTu.Value.ToString("yyyy-MM-dd"), dpDen.Value.ToString("yyyy-MM-dd"));
            grDanhSachNhanVien.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void TheHienThoiGianLamViecTheoTo()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select b.UserFullCode,
                b.UserCardNo,b.UserFullName,c.Description,a.* from 
                (select * from CheckInOut where TimeDate 
				between '{0}' and '{1}')a
                left outer join
                (select * from UserInfo) b
                on a.UserEnrollNumber=b.UserEnrollNumber
					inner join RelationDept c
				on b.UserIDD=c.ID
                where b.UserIDD like '{2}' order by TimeDate desc",
                dpTu.Value.ToString("yyyy-MM-dd"), dpDen.Value.ToString("yyyy-MM-dd"), deptID);
 
            grChamCong.DataSource = Model.Function.GetDataTable(sqlQuery);

        }
        private void TheHienThoiGianLamViecTheoMaNhanVien()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select b.UserFullCode,b.UserCardNo,
                b.UserFullName,c.Description,a.* from 
                (select * from CheckInOut where TimeDate 
				between '{0}' and '{1}')a
                left outer join
                (select * from UserInfo) b
                on a.UserEnrollNumber=b.UserEnrollNumber
					inner join RelationDept c
				on b.UserIDD=c.ID
                where b.UserFullCode like '{2}' order by TimeDate desc",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"), userID);
            grChamCong.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }

        private void btnTraCuuNhanVien_Click(object sender, EventArgs e)
        {
            TheHienTatCaNhanVien();
        }
        private string deptID;
        private string userID;
        private string hoten;
        private void treeListPhongBan_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //string point;
            //point = treeListPhongBan.GetFocusedDisplayText();
            //deptID = treeListPhongBan.GetFocusedRowCellDisplayText(treeListColumnDeptID);

            //TheHienDanhSachNhanVienTheoPhongBan();
            //TheHienThoiGianLamViecTheoTo();
        }

        private void grDanhSachNhanVien_Click(object sender, EventArgs e)
        {
            string point = "";
            point = gvDanhSachNhanVien.GetFocusedDisplayText();
            userID = gvDanhSachNhanVien.GetFocusedRowCellDisplayText(manhanvien_colnv);
            hoten = gvDanhSachNhanVien.GetFocusedRowCellDisplayText(hoten_colnv);
            TheHienThoiGianLamViecTheoMaNhanVien();
            TheHienTongHopChamCongTheoUserID();
            TheHienNgayCongTangCaTheoMaThe();
        }

        private void btnTraCuuNgayCong_Click(object sender, EventArgs e)
        {
            TheHienThoiGianLamViecTheoNgay();
        }
        private void TheHienThoiGianLamViecTheoNgay()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select b.UserFullCode,
                b.UserCardNo,b.UserFullName,c.Description,a.* from 
                (select * from CheckInOut where TimeDate 
				between '{0}' and '{1}')a
                left outer join
                (select * from UserInfo) b
                on a.UserEnrollNumber=b.UserEnrollNumber
					inner join RelationDept c
				on b.UserIDD=c.ID order by TimeDate desc",
                dpTu.Value.ToString("yyyy-MM-dd"), dpDen.Value.ToString("yyyy-MM-dd"));
            grChamCong.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi
        }

        private void btnTraCuuTongHopChamCong_Click(object sender, EventArgs e)
        {
            TheHienChamCongTheoNgay();
        }
        private void TheHienTongHopChamCongTheoUserID()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"execute TongHopChamCongTheoID '{0}','{1}','{2}'",
                dpTu.Value.ToString("yyyy-MM-dd"), dpDen.Value.ToString("yyyy-MM-dd"), userID);
            grTongHopChamCong.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi

            //Model.Function.ConnectChamCong();//Mo ket noi
            //SqlCommand cmd = new SqlCommand("TongHopChamCongTheoID", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@from", SqlDbType.Date).Value = dpTu.Value.ToString("yyyy-MM-dd");
            //cmd.Parameters.Add("@end", SqlDbType.Date).Value = dpDen.Value.ToString("yyyy-MM-dd");
            //cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //grTongHopChamCong.DataSource = dt;
            //con.Close();
        }
        private void TheHienChamCongTheoNgay()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"execute TongHopChamCongTheoNgay '{0}'",
                dpTu.Value.ToString("yyyy-MM-dd"), dpDen.Value.ToString("yyyy-MM-dd"));
            grTongHopChamCong.DataSource = Model.Function.GetDataTable(sqlQuery);
            Model.Function.Disconnect();//Mo ket noi

            //Model.Function.ConnectChamCong();//Mo ket noi
            //SqlCommand cmd = new SqlCommand("TongHopChamCongTheoNgay", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@from", SqlDbType.Date).Value = dpTu.Value.ToString("yyyy-MM-dd");
            //cmd.Parameters.Add("@end", SqlDbType.Date).Value = dpDen.Value.ToString("yyyy-MM-dd");
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //grTongHopChamCong.DataSource = dt;
            //con.Close();
        }

        private void btnThemGioCong_Click(object sender, EventArgs e)
        {

            xtraTabControl1.SelectedTabPage = xtraTabPageChamCong;
            frmChamCongThemNgayCong them = new frmChamCongThemNgayCong(userID, hoten, dpNgayThem.Value);
            them.ShowDialog();
            TheHienThoiGianLamViecTheoMaNhanVien();
            TheHienTongHopChamCongTheoUserID();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connect cn = new Connect();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = cn.ConnectChamCong;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            int[] listRowList = this.gvChamCong.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvChamCong.GetDataRow(listRowList[i]);
                var date = Convert.ToDateTime(rowData["TimeDate"].ToString());
                var time = Convert.ToDateTime(rowData["TimeStr"].ToString());
                DateTime dateTime = date + time.TimeOfDay;
                //MessageBox.Show(Convert.ToDateTime(dateTime).ToString("yyyy/MM/dd"));
                string strQuery = string.Format(@"update CheckInOut set
                        TimeStr = '{0}' where ID like {1}",
                        Convert.ToDateTime(rowData["TimeStr"]).ToString("yyyy-MM-dd HH:mm"),
                        rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienThoiGianLamViecTheoMaNhanVien();
            TheHienTongHopChamCongTheoUserID();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connect cn = new Connect();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = cn.ConnectChamCong;
            if (con.State == ConnectionState.Closed)
                con.Open();
            DataRow rowData;
            int[] listRowList = this.gvChamCong.GetSelectedRows();
            for (int i = 0; i < listRowList.Length; i++)
            {
                rowData = this.gvChamCong.GetDataRow(listRowList[i]);
                string strQuery = string.Format(@"delete from CheckInOut where ID='{0}'",
                rowData["ID"]);
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            TheHienThoiGianLamViecTheoMaNhanVien();
            TheHienTongHopChamCongTheoUserID();
        }

        private void btnExportDanhSachNhanVien_Click(object sender, EventArgs e)
        {
            gvDanhSachNhanVien.ShowPrintPreview();
        }

        private void btnExportTongHopChamCong_Click(object sender, EventArgs e)
        {
            gvTongHopChamCong.ShowPrintPreview();
        }

        private void btnGioCong_Click(object sender, EventArgs e)
        {
            gvChamCong.ShowPrintPreview();
        }

        private void btnTraCuTongHopGioCong_Click(object sender, EventArgs e)
        {
            TheHienTatCaNgayCongTangCa();
        }
        private void TheHienTatCaNgayCongTangCa()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"execute TongHopNgayCong_proc '{0}','{1}'",
                dpTu.Value.ToString("yyyy-MM-dd"),
                dpDen.Value.ToString("yyyy-MM-dd"));
            grNgayCongTangCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void TheHienNgayCongTangCaTheoBoPhan()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select * from GioCongview where DepID like '{0}' 
                order by UserFullCode asc, TimeDate desc", deptID);
            grNgayCongTangCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }
        private void TheHienNgayCongTangCaTheoMaThe()
        {
            Model.Function.ConnectChamCong();//Mo ket noi
            string sqlQuery = string.Format(@"select * from GioCongview where UserFullCode like '{0}' 
               order by TimeDate desc", this.userID);
            grNgayCongTangCa.DataSource = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnExportNgayCong_Click(object sender, EventArgs e)
        {
            grNgayCongTangCa.ShowPrintPreview();
        }

        private void treeListPhongBan_Click(object sender, EventArgs e)
        {
            string point;
            point = treeListPhongBan.GetFocusedDisplayText();
            deptID = treeListPhongBan.GetFocusedRowCellDisplayText(treeListColumnDeptID);

            TheHienDanhSachNhanVienTheoPhongBan();
            TheHienThoiGianLamViecTheoTo();
            TheHienNgayCongTangCaTheoBoPhan();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {

        }
    }
}
