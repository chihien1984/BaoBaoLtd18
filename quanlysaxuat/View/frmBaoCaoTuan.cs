using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using quanlysanxuat.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysanxuat.View
{
    public partial class frmBaoCaoTuan : DevExpress.XtraEditors.XtraForm
    {
        string year;
        string weeklyReport;
        string boPhanCaoCao;
        string CodeReport;
        public frmBaoCaoTuan()
        {
            InitializeComponent();
        }

        //gvUser.GetRowCellValue(gvUser.FocusedRowHandle, gvUser.Columns["UserName"]) == null;
        //formload
        private void frmBaoCaoTuan_Load(object sender, EventArgs e)
        {
            dpFrom.Text = DateTime.Now.ToString("01-MM-yyyy");
            dpEnd.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ThBaoCaoBoPhanBaoCao();
            ThSoBaoCaoToChiTiet();
        }
        private void CreateCodeReport()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Connect.mConnect;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(@"select Top 1 
				REPLACE(convert(nvarchar,GetDate(),11),'/','') 
				+replace(replace(left(CONVERT(time, GetDate()),12),':',''),'.','')", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                txtCodeReport.Text = Convert.ToString(reader[0]);
            reader.Close();
        }
        
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            ThSoBaoCaoToChiTiet();
        }
        private async void ThSoBaoCaoToChiTiet()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select ID,CodeReport,
				ToBaoCao,Tuan,Nam,Thang,DateMin,DateMax,NoiDung,ChiSo,NguoiBaoCao,
				NgayGhi,NgayHieuChinh,QHTrongVongKiemSoat,QHVuotVongKiemSoat,
				ChuanBiThucHien,DangThucHien,RuiRoTiemAn,HoanThanh from BaoCaoToNoiDung");
                Invoke((Action)(() =>
                {
                    grBaoCaoToChiTietSo.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
        }
        private void ThBaoCaoBoPhanBaoCao()
        {
            Model.Function.ConnectSanXuat();
          
                string sqlQuery = string.Format(@"select Ten_Nguonluc from tblResources");
                    cbToBaoCao.DataSource = Model.Function.GetDataTable(sqlQuery);
                    cbToBaoCao.DisplayMember = "Ten_Nguonluc";
                    cbToBaoCao.ValueMember = "Ten_Nguonluc";
                    //cbToBaoCao.Text = "--Select--";
        }
        private void ThBaoCaoThongKe()
        {
            //Model.Function.ConnectSanXuat();
            //await Task.Run(() =>
            //{
            //    string sqlQuery = string.Format(@"select ID,ID IDChiSo,NoiDung,NoiDungChiTiet,NhiemVu from BaoCaoToChiSo");
            //    Invoke((Action)(() =>
            //    {
            //        grBaoCaoThongKe.DataSource = Model.Function.GetDataTable(sqlQuery);
            //    }));
            //});
            //gvBaoCaoThongKe.Columns["NoiDung"].GroupIndex = 0;
            //gvBaoCaoThongKe.ExpandAllGroups();
            //gvBaoCaoThongKe.Appearance.Row.Font = new Font("Segoe UI", 8f);
            //gvBaoCaoThongKe.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThQHTrongVongKiemSoat()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'",lbQHTrongVongKiemSoat.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grTrongVongKiemSoat.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvTrongVongKiemSoat.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvTrongVongKiemSoat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThQHVuotVongKiemSoat()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbQHVuotVongKiemSoat.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grQHVuotVongKiemSoat.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvQHVuotVongKiemSoat.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvQHVuotVongKiemSoat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThThucHienTrongTuanToi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbThucHienTrongTuanToi.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grThucHienTrongTuanToi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvThucHienTrongTuanToi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvThucHienTrongTuanToi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThDangThucHien()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbDangThucHien.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grDangThucHien.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvDangThucHien.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvDangThucHien.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThRuiRoSapToi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbRuiRo.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grRuiRoSapToi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvRuiRoSapToi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvRuiRoSapToi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThVanDeChatLuong()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbVanDeChatLuong.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grVanDeChatLuong.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvVanDeChatLuong.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvVanDeChatLuong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThCaiTien()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbDeXuatCaiTien.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grDeXuatCaiTien.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvDeXuatCaiTien.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvDeXuatCaiTien.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThThucHien5S()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbThucHanh5S.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grThucHien5S.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvThucHien5S.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvThucHien5S.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThTangChiPhi()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbLamTangChiPhi.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grTangChiPhi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvTangChiPhi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvTangChiPhi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThHoanThanh()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"execute BaoCaoQHTrongVongKiemSoat 
                    N'{0}','{1}','{2}'", lbLamTangChiPhi.Text,
                    dpFrom.Value.ToString("MM-dd-yyyy"),
                    dpEnd.Value.ToString("MM-dd-yyyy"));
                Invoke((Action)(() =>
                {
                    grHoanThanh.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvHoanThanh.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvHoanThanh.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private void gvTrongVongKiemSoat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvTrongVongKiemSoat.GetRowCellValue(gvTrongVongKiemSoat.FocusedRowHandle, gvTrongVongKiemSoat.Columns["NhiemVu"]) == null)
            {
                return;
            }
            else
            {
                txtQuaHanTrongVongKiemSoat.Text = gvTrongVongKiemSoat.GetRowCellValue(gvTrongVongKiemSoat.FocusedRowHandle, gvTrongVongKiemSoat.Columns["NhiemVu"]).ToString();
            }
            gvTrongVongKiemSoat.SelectAll();
        }

        private void gvQHVuotVongKiemSoat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            if (gvQHVuotVongKiemSoat.GetRowCellValue(gvQHVuotVongKiemSoat.FocusedRowHandle, gvQHVuotVongKiemSoat.Columns["NhiemVu"]) == null)
            {
                return;
            }
            else
            {
                txtQHVuotVongKiemSoat.Text = gvQHVuotVongKiemSoat.GetRowCellValue(gvQHVuotVongKiemSoat.FocusedRowHandle, gvQHVuotVongKiemSoat.Columns["NhiemVu"]).ToString();
            }
            gvQHVuotVongKiemSoat.SelectAll();
        }

        private void gvThucHienTrongTuanToi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvThucHienTrongTuanToi.GetRowCellValue(gvThucHienTrongTuanToi.FocusedRowHandle, gvThucHienTrongTuanToi.Columns["NhiemVu"])==null)
            {
                return;
            }
            else
            {
                txtThucHienTrongTuanToi.Text = gvThucHienTrongTuanToi.GetRowCellValue(gvThucHienTrongTuanToi.FocusedRowHandle, gvThucHienTrongTuanToi.Columns["NhiemVu"]).ToString();
            }
            gvThucHienTrongTuanToi.SelectAll();
        }

        private void gvDangThucHien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDangThucHien.GetRowCellValue(gvDangThucHien.FocusedRowHandle, gvDangThucHien.Columns["NhiemVu"]) == null)
            {
                return;
            }
            else
            {
                txtDangThucHien.Text = gvDangThucHien.GetRowCellValue(gvDangThucHien.FocusedRowHandle, gvDangThucHien.Columns["NhiemVu"]).ToString();
            }
            gvDangThucHien.SelectAll();
        }
        private void gvHoanThanh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvHoanThanh.GetRowCellValue(gvHoanThanh.FocusedRowHandle, gvHoanThanh.Columns["NhiemVu"]) == null)
            {
                return;
            }
            else
            {
                txtHoanThanh.Text = gvHoanThanh.GetRowCellValue(gvHoanThanh.FocusedRowHandle, gvHoanThanh.Columns["NhiemVu"]).ToString();
            }
            gvHoanThanh.SelectAll();
        }
        private void gvRuiRoSapToi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvRuiRoSapToi.GetRowCellValue(gvRuiRoSapToi.FocusedRowHandle, gvRuiRoSapToi.Columns["NhiemVu"]) == null)
            {
                return;
            }
            else{txtRuiRoDangThucHien.Text = gvRuiRoSapToi.GetRowCellValue(gvRuiRoSapToi.FocusedRowHandle, gvRuiRoSapToi.Columns["NhiemVu"]).ToString();}
            gvRuiRoSapToi.SelectAll();
        }
   
        private void gvDeXuatCaiTien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvDeXuatCaiTien.SelectAll();
        }

        private void gvThucHien5S_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvThucHien5S.SelectAll();
        }

        private void gvVanDeChatLuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvVanDeChatLuong.SelectAll();
        }

        private void gvTangChiPhi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvTangChiPhi.SelectAll();
        }
        private void ThemChiSo()
        {
            //gvBaoCaoThongKe.Columns["NoiDung"].GroupIndex = -1;
            //gvBaoCaoThongKe.ExpandAllGroups();
            //try
            //{
            //    int[] listRowList = this.gvBaoCaoThongKe.GetSelectedRows();
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open(); DataRow rowData;
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gvBaoCaoThongKe.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"insert into BaoCaoToChiSoSo
            //            (NoiDung,NoiDungChiTiet,
            //             NhiemVu,BoPhanBaoCao,
            //             DateFrom,DateEnd,
            //             NgayGhi,NguoiGhi,IDChiSo)
            //             values(N'{0}',N'{1}',
            //             N'{2}',N'{3}',
            //             N'{4}',N'{5}',
            //             GetDate(),N'{6}','{7}')",
            //             rowData["NoiDung"], rowData["NoiDungChiTiet"],
            //             rowData["NhiemVu"], cbToBaoCao.Text,
            //             dpFrom.Value.ToString("MM-dd-yyyy"),
            //             dpEnd.Value.ToString("MM-dd-yyyy"),
            //             MainDev.username, rowData["IDChiSo"]);
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    ThBaoCaoThongKe();
            //    con.Close(); MessageBox.Show("Success", "Mission");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lý do" + ex, "error");
            //}
        }

        private void ThemQuaHanTrongVongKiemSoat()
        {
            string boPhan = cbToBaoCao.SelectedValue.ToString();
            try
            {
                int[] listRowList = this.gvTrongVongKiemSoat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTrongVongKiemSoat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}',N'{10}','{11}','{12}')",
                         lbQHTrongVongKiemSoat.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtQuaHanTrongVongKiemSoat.Text,
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemQuaHanVuotVongKiemSoat()
        {
            try
            {
                int[] listRowList = this.gvQHVuotVongKiemSoat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvQHVuotVongKiemSoat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}','{12}')",
                         lbQHVuotVongKiemSoat.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtQHVuotVongKiemSoat.Text,
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ChuanBiThucHienTrongTuanToi()
        {
            try
            {
                int[] listRowList = this.gvThucHienTrongTuanToi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThucHienTrongTuanToi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}',
                        '{11}','{12}')",
                         lbThucHienTrongTuanToi.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtThucHienTrongTuanToi.Text, 
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemDangThucHien()
        {
            try
            {
                int[] listRowList = this.gvDangThucHien.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDangThucHien.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}',
                        '{10}','{11}','{12}')",
                         lbDangThucHien.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtDangThucHien.Text,
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemRuiRoSapToi()
        {
            try
            {
                int[] listRowList = this.gvRuiRoSapToi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvRuiRoSapToi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}','{12}')",
                         lbRuiRo.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtRuiRoDangThucHien.Text,
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemHoanThanh()
        {
            try
            {
                int[] listRowList = this.gvHoanThanh.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvHoanThanh.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,NhiemVu,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}','{12}')",
                         lbHoanThanh.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, txtHoanThanh.Text, 
                         lbIDTienDo.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemVanDeChatLuong()
        {
            try
            {
                int[] listRowList = this.gvVanDeChatLuong.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvVanDeChatLuong.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}')",
                         lbVanDeChatLuong.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, 
                         lbIDChatLuong.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void ThemVanDeCaiTien()
        {
            try
            {
                int[] listRowList = this.gvDeXuatCaiTien.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDeXuatCaiTien.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}')",
                         lbDeXuatCaiTien.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, lbIDCaiTien.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void ThemVanDe5S()
        {
            try
            {
                int[] listRowList = this.gvThucHien5S.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThucHien5S.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}')",
                         lbThucHanh5S.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, lbIDTieuChuan5S.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void LamTangChiPhi()
        {
            try
            {
                int[] listRowList = this.gvTangChiPhi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTangChiPhi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"insert into BaoCaoToChiTietSo 
                        (NoiDung,NoiDungChiTiet,
                        NguyenNhan,DeXuatBienPhapKPCaiTien,
                        NguoiThucHien,NgayHoanThanh,
                        BoPhanBaoCao,DateFrom,DateEnd,
                        NgayGhi,NguoiGhi,IDNoiDung,CodeReport)
                        values
                       (N'{0}',N'{1}',
                        N'{2}',N'{3}',
                        N'{4}',N'{5}',
                        N'{6}',N'{7}',N'{8}',
                        GetDate(),N'{9}','{10}','{11}')",
                         lbLamTangChiPhi.Text,
                         rowData["NoiDungChiTiet"], rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, lbIDViPhamTangChiPhi.Text,txtCodeReport.Text);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
       
        private void btnThem_Click(object sender, EventArgs e)
        {
            CreateCodeReport();//Tạo số báo cáo
            GhiNoiDung();//Ghi nội dung vào báo cáo
            ThemQuaHanTrongVongKiemSoat();//ghi tiến độ quá hạn vào form
            ThemQuaHanVuotVongKiemSoat();//Ghi tiến độ quá hạn vượt vòng 
            ChuanBiThucHienTrongTuanToi();//Ghi chuẩn bị thực hiện
            ThemDangThucHien();//Ghi đang thực hiện
            ThemRuiRoSapToi();//Ghi rủi ro sắp tới
            ThemHoanThanh();//Ghi hoàn thành
            ThemVanDeChatLuong();//Ghi chất lượng
            ThemVanDeCaiTien();//Ghi cải tiến
            ThemVanDe5S();//Ghi thực hành 5S
            LamTangChiPhi();//Ghi vi phạm tăng chi phí
            ThSoBaoCaoToChiTiet();//Show list báo cáo
        }

        DateTime dateFrom; DateTime dateEnd; string month;
        private void gvBaoCaoToChiTietSo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["ToBaoCao"]) == null)
            {
                return;
            }
            else
            {
                boPhanCaoCao = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["ToBaoCao"]).ToString();
                dateFrom = Convert.ToDateTime(gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["DateMin"]));
                dateEnd = (DateTime)gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["DateMax"]);
                txtMonth.Text = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["Thang"]).ToString();
                txtYear.Text = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["Nam"]).ToString();
                txtWeekNumber.Text = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["Tuan"]).ToString();
                txtCodeReport.Text = gvBaoCaoToChiTietSo.GetRowCellValue(gvBaoCaoToChiTietSo.FocusedRowHandle, gvBaoCaoToChiTietSo.Columns["CodeReport"]).ToString();
                month = txtMonth.Text;
                weeklyReport = txtWeekNumber.Text;
                year = txtYear.Text;
                ThQHTrongVongKiemSoatTheoTuan();
                ThQHVuotVongKiemSoatTheoTuan();
                ThThucHienTrongTuanToiTheoTuan();
                ThDangThucHienTheoTuan();
                ThRuiRoSapToiTheoTuan();
                ThVanDeChatLuongTheoTuan();
                ThCaiTienTheoTuan();
                ThThucHien5STheoTuan();
                ThTangChiPhiTheoTuan();
                ThHoanThanhTheoTuan();
            }
        }

        // Thể hiện danh sách tổ theo tuần báo cáo
        private async void ThBaoCaoThongKeTheoTuan()
        {
            //Model.Function.ConnectSanXuat();
            //await Task.Run(() =>
            //{
            //    string sqlQuery = string.Format(@"select NoiDung,NoiDungChiTiet,NhiemVu,
            //        BoPhanBaoCao,DateFrom,DateEnd,ID,IDChiSo
            //        from BaoCaoToChiSoSo 
            //        where DateFrom like '{0}' 
            //        and DateEnd like '{1}' 
            //        and BoPhanBaoCao like N'{2}'",
            //        dateFrom.ToString("yyyy-MM-dd"),
            //        dateEnd.ToString("yyyy-MM-dd"),
            //        boPhanCaoCao);
            //    Invoke((Action)(() =>
            //    {
            //        grBaoCaoThongKe.DataSource = Model.Function.GetDataTable(sqlQuery);
            //    }));
            //});
            //gvBaoCaoThongKe.Columns["NoiDung"].GroupIndex = 0;
            //gvBaoCaoThongKe.ExpandAllGroups();
            //gvBaoCaoThongKe.Appearance.Row.Font = new Font("Segoe UI", 8f);
            //gvBaoCaoThongKe.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThQHTrongVongKiemSoatTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'",
                txtCodeReport.Text,lbQHTrongVongKiemSoat.Text);
                Invoke((Action)(() =>
                {
                    grTrongVongKiemSoat.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvTrongVongKiemSoat.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvTrongVongKiemSoat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThQHVuotVongKiemSoatTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbQHVuotVongKiemSoat.Text);
                Invoke((Action)(() =>
                {
                    grQHVuotVongKiemSoat.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvQHVuotVongKiemSoat.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvQHVuotVongKiemSoat.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThThucHienTrongTuanToiTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbThucHienTrongTuanToi.Text);
                Invoke((Action)(() =>
                {
                    grThucHienTrongTuanToi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvThucHienTrongTuanToi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvThucHienTrongTuanToi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThDangThucHienTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbDangThucHien.Text);
                Invoke((Action)(() =>
                {
                    grDangThucHien.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvDangThucHien.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvDangThucHien.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThRuiRoSapToiTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbRuiRo.Text);
                Invoke((Action)(() =>
                {
                    grRuiRoSapToi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvRuiRoSapToi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvRuiRoSapToi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThVanDeChatLuongTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbVanDeChatLuong.Text);
                Invoke((Action)(() =>
                {
                    grVanDeChatLuong.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvVanDeChatLuong.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvVanDeChatLuong.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThCaiTienTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbDeXuatCaiTien.Text);
                Invoke((Action)(() =>
                {
                    grDeXuatCaiTien.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvDeXuatCaiTien.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvDeXuatCaiTien.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThThucHien5STheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbThucHanh5S.Text);
                Invoke((Action)(() =>
                {
                    grThucHien5S.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvThucHien5S.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvThucHien5S.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThTangChiPhiTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbLamTangChiPhi.Text);
                Invoke((Action)(() =>
                {
                    grTangChiPhi.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvTangChiPhi.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvTangChiPhi.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        private async void ThHoanThanhTheoTuan()
        {
            Model.Function.ConnectSanXuat();
            await Task.Run(() =>
            {
                string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSo 
				where CodeReport like '{0}'
				and NoiDung like N'{1}'", txtCodeReport.Text, lbHoanThanh.Text);
                Invoke((Action)(() =>
                {
                    grHoanThanh.DataSource = Model.Function.GetDataTable(sqlQuery);
                }));
            });
            gvHoanThanh.Appearance.Row.Font = new Font("Segoe UI", 8f);
            gvHoanThanh.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
        }
        //Sửa nội dung
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //SuaChiSo();
            SuaQuaHanTrongVongKiemSoat();
            SuaQuaHanVuotVongKiemSoat();
            SuaBiThucHienTrongTuanToi();
            SuaDangThucHien();
            SuaRuiRoSapToi();
            SuaVanDeChatLuong();
            SuaVanDeCaiTien();
            SuaVanDe5S();
            SuaTangChiPhi();
            SuaHoanThanh();
        }

        private void SuaChiSo()
        {
            //gvBaoCaoThongKe.Columns["NoiDung"].GroupIndex = -1;
            //gvBaoCaoThongKe.ExpandAllGroups();
            //try
            //{
            //    int[] listRowList = this.gvBaoCaoThongKe.GetSelectedRows();
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = Connect.mConnect;
            //    if (con.State == ConnectionState.Closed)
            //        con.Open(); DataRow rowData;
            //    for (int i = 0; i < listRowList.Length; i++)
            //    {
            //        rowData = this.gvBaoCaoThongKe.GetDataRow(listRowList[i]);
            //        string strQuery = string.Format(@"update BaoCaoToChiSoSo
            //             set NhiemVu = N'{0}',BoPhanBaoCao = N'{1}',
            //             DateFrom = N'{2}',DateEnd = N'{3}',
            //             NgaySua = GetDate(),NguoiGhi = N'{4}' where ID like '{5}'",
            //             rowData["NhiemVu"], cbToBaoCao.Text,
            //             dpFrom.Value.ToString("MM-dd-yyyy"),
            //             dpEnd.Value.ToString("MM-dd-yyyy"),
            //             MainDev.username, rowData["ID"]);
            //        SqlCommand cmd = new SqlCommand(strQuery, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //    ThBaoCaoThongKeTheoTuan();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lý do" + ex, "error");
            //}
        }

        private void SuaQuaHanTrongVongKiemSoat()
        {
            string boPhan = cbToBaoCao.SelectedValue.ToString();
            try
            {
                int[] listRowList = this.gvTrongVongKiemSoat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTrongVongKiemSoat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThQHTrongVongKiemSoatTheoTuan();
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex) { MessageBox.Show("Lý do" + ex, "error"); }
        }
        private void SuaQuaHanVuotVongKiemSoat()
        {
            try
            {
                int[] listRowList = this.gvQHVuotVongKiemSoat.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvQHVuotVongKiemSoat.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThQHVuotVongKiemSoatTheoTuan();

                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaBiThucHienTrongTuanToi()
        {
            try
            {
                int[] listRowList = this.gvThucHienTrongTuanToi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThucHienTrongTuanToi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThThucHienTrongTuanToiTheoTuan();

                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaDangThucHien()
        {
            try
            {
                int[] listRowList = this.gvDangThucHien.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDangThucHien.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThDangThucHienTheoTuan();
                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaRuiRoSapToi()
        {
            try
            {
                int[] listRowList = this.gvRuiRoSapToi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvRuiRoSapToi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThRuiRoSapToiTheoTuan();
                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaVanDeChatLuong()
        {
            try
            {
                int[] listRowList = this.gvVanDeChatLuong.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvVanDeChatLuong.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThVanDeChatLuongTheoTuan();
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaVanDeCaiTien()
        {
            try
            {
                int[] listRowList = this.gvDeXuatCaiTien.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvDeXuatCaiTien.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThCaiTienTheoTuan();

                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void SuaVanDe5S()
        {
            try
            {
                int[] listRowList = this.gvThucHien5S.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvThucHien5S.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThThucHien5STheoTuan();
                con.Close(); 
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void SuaTangChiPhi()
        {
            try
            {
                int[] listRowList = this.gvTangChiPhi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTangChiPhi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThTangChiPhiTheoTuan();
                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }

        private void SuaHoanThanh()
        {
            try
            {
                int[] listRowList = this.gvHoanThanh.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvHoanThanh.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"update BaoCaoToChiTietSo 
                        set NoiDungChiTiet = N'{0}',
                        NguyenNhan = N'{1}',DeXuatBienPhapKPCaiTien = N'{2}',
                        NguoiThucHien = N'{3}',NgayHoanThanh = N'{4}',
                        BoPhanBaoCao = N'{5}',DateFrom = N'{6}',DateEnd = N'{7}',
                        NgaySua = GetDate(),NguoiGhi = N'{8}' where ID like '{9}'",
                         rowData["NoiDungChiTiet"],
                         rowData["NguyenNhan"],
                         rowData["DeXuatBienPhapKPCaiTien"],
                         rowData["NguoiThucHien"],
                         rowData["NgayHoanThanh"], cbToBaoCao.Text,
                         dpFrom.Value.ToString("MM-dd-yyyy"),
                         dpEnd.Value.ToString("MM-dd-yyyy"),
                         MainDev.username, rowData["ID"]);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThTangChiPhiTheoTuan();
                con.Close();
                //MessageBox.Show("Success", "Mission");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void XoaSoChiSo()
        {
            try
            {
                int[] listRowList = this.gvTangChiPhi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTangChiPhi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BaoCaoToChiSoSo   
                    where DateFrom like '{0}' 
                    and DateEnd like '{1}' 
                    and BoPhanBaoCao like '{2}' ",
                    dateFrom.ToString("yyyy-MM-dd"),
                    dateEnd.ToString("yyyy-MM-dd"),
                    boPhanCaoCao);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                ThSoBaoCaoToChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void XoaSoNoiDungChiTiet()
        {
            try
            {
                int[] listRowList = this.gvTangChiPhi.GetSelectedRows();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Connect.mConnect;
                if (con.State == ConnectionState.Closed)
                    con.Open(); DataRow rowData;
                for (int i = 0; i < listRowList.Length; i++)
                {
                    rowData = this.gvTangChiPhi.GetDataRow(listRowList[i]);
                    string strQuery = string.Format(@"delete from BaoCaoToChiTietSo   
                    where DateFrom like '{0}' 
                    and DateEnd like '{1}' 
                    and BoPhanBaoCao like N'{2}'",
                    dateFrom.ToString("yyyy-MM-dd"),
                    dateEnd.ToString("yyyy-MM-dd"),
                    boPhanCaoCao);
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.ExecuteNonQuery();
                }
                ThSoBaoCaoToChiTiet();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lý do" + ex, "error");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //XoaSoChiSo();
            XoaSoNoiDungChiTiet();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //string tuanBaoCao = boPhanCaoCao + weeklyReport + year;
            //DataTable dt = new DataTable();
            //ketnoi Connect = new ketnoi();
            //string sqlQuery = string.Format(@"select * from BaoCaoToChiTietSoview
            //    where 
            //    BoPhanBaoCao
            //    +cast(datepart(week,DateFrom) as nvarchar)
            //    +cast(year(DateFrom) as nvarchar) = N'{0}' order by IDNoiDung asc", tuanBaoCao);
            //dt = Connect.laybang(sqlQuery);
            //ReportBaoCaoTuan baoCaoTuan = new ReportBaoCaoTuan(year,weeklyReport,boPhanCaoCao);
            //baoCaoTuan.DataSource = dt;
            //baoCaoTuan.DataMember = "Table";
            //baoCaoTuan.CreateDocument(false);
            //baoCaoTuan.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName = tuanBaoCao;
            //PrintTool tool = new PrintTool(baoCaoTuan.PrintingSystem);
            //baoCaoTuan.ShowPreviewDialog();
            //Connect.dongketnoi();
        }
        private void THPieChart()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"select NoiDung,Max(NhiemVu)NhiemVu 
                from BaoCaoToChiTietSo 
                where NhiemVu is not null
                group by NoiDung");
            var dt = Model.Function.GetDataTable(sqlQuery);
        }

        private void btnGetReport_Click(object sender, EventArgs e)
        {
            ThBaoCaoThongKe();
            ThQHTrongVongKiemSoat();
            ThQHVuotVongKiemSoat();
            ThThucHienTrongTuanToi();
            ThDangThucHien();
            ThRuiRoSapToi();
            ThVanDeChatLuong();
            ThCaiTien();
            ThThucHien5S();
            ThTangChiPhi();
            ThHoanThanh();
        }
        private void GhiNoiDung()
        {
            Model.Function.ConnectSanXuat();
            string sqlQuery = string.Format(@"insert into BaoCaoToNoiDung 
                    (ToBaoCao,DateMin,DateMax,Thang,Tuan,Nam,
                     QHTrongVongKiemSoat,QHVuotVongKiemSoat,ChuanBiThucHien,
                     DangThucHien,RuiRoTiemAn,HoanThanh,
                     NguoiBaoCao,NgayGhi,CodeReport)
                     values (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',
                     N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',
                     N'{12}',GetDate(),N'{13}')", cbToBaoCao.Text,
                 dpFrom.Value.ToString("MM-dd-yyyy"),
                 dpEnd.Value.ToString("MM-dd-yyyy"),
                 txtMonth.Text, txtWeekNumber.Text,txtYear.Text,
                 txtQuaHanTrongVongKiemSoat.Text,txtQHVuotVongKiemSoat.Text,
                 txtThucHienTrongTuanToi.Text,txtDangThucHien.Text, txtRuiRoDangThucHien.Text,
                 txtHoanThanh.Text, MainDev.username,txtCodeReport.Text);
            var kq = Model.Function.GetDataTable(sqlQuery);
            if (kq.Rows.Count > 0)
            { MessageBox.Show("Success text", "Success"); }
        }

        private void dpFrom_ValueChanged(object sender, EventArgs e)
        {
            //hàm này dùng để tính số tuần của 1 ngày cụ thể
            int year = dpFrom.Value.Year;
            int month = dpFrom.Value.Month;
            int day = dpFrom.Value.Day;
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNo = currentCulture.Calendar.GetWeekOfYear(
                  new DateTime(year, month, day),
                  currentCulture.DateTimeFormat.CalendarWeekRule,
                  currentCulture.DateTimeFormat.FirstDayOfWeek);
            txtWeekNumber.Text = weekNo.ToString();
            txtMonth.Text = month.ToString();
            txtYear.Text = year.ToString();
        }
    }
}