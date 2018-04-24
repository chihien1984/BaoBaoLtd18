using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace quanlysanxuat.Report
{
    public partial class ReportBaoCaoTuan : DevExpress.XtraReports.UI.XtraReport
    {
		string year;
		string weeklyReport;
		string boPhanBaoCao;
        public ReportBaoCaoTuan(string year,string weeklyReport,string boPhanBaoCao)
        {    
			this.year = year;this.weeklyReport = weeklyReport;this.boPhanBaoCao = boPhanBaoCao;
			InitializeComponent();
		}

        private void ReportBaoCaoTuan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
			View.frmBaoCaoTuan baoCaoTuan = new View.frmBaoCaoTuan();
			//View.frmBaoCaoTuan baoCaoTuan = new View.frmBaoCaoTuan(year,weeklyReport,boPhanCaoCao);
            Model.Function.ConnectSanXuat();
            //Quá hạn vượt vòng kiểm soát
            string sqlQuery1 = string.Format(@"select BoPhanBaoCao,NoiDung,Thang,Tuan,Nam,
	            Max(NhiemVu)NhiemVu,
	            Max(NhiemVu)/cast((select 
	            sum(NhiemVu)TongNhiemVu from
	            (select NoiDung,Thang,Tuan,Nam,
	            Max(NhiemVu)NhiemVu
	            from BaoCaoToChiTietSoview 
	            where NhiemVu <>''
	            group by Tuan,Thang,Nam,NoiDung)a) as float)*100 TyLe
		            from BaoCaoToChiTietSoview 
	            where NhiemVu <>''
				and NoiDung like N'Quá hạn vượt vòng kiểm soát'
	            and BoPhanBaoCao like '{0}'
	            and Tuan like '{1}' 
	            and Nam like '{2}'
	            group by Tuan,Thang,Nam,NoiDung,BoPhanBaoCao",
				boPhanBaoCao, 
				weeklyReport,
				year);
			//Quá hạn trong vòng kiểm soát
			string sqlQuery2 = string.Format(@"select BoPhanBaoCao,NoiDung,Thang,Tuan,Nam,
	            Max(NhiemVu)NhiemVu,
	            Max(NhiemVu)/cast((select 
	            sum(NhiemVu)TongNhiemVu from
	            (select NoiDung,Thang,Tuan,Nam,
	            Max(NhiemVu)NhiemVu
	            from BaoCaoToChiTietSoview 
	            where NhiemVu <>''
	            group by Tuan,Thang,Nam,NoiDung)a) as float)*100 TyLe
		            from BaoCaoToChiTietSoview 
	            where NhiemVu <>''
				and NoiDung like N'Quá hạn vượt vòng kiểm soát'
				and BoPhanBaoCao like '{0}'
	            and Tuan like '{1}' 
	            and Nam like '{2}'
	            group by Tuan,Thang,Nam,NoiDung,BoPhanBaoCao",
				boPhanBaoCao,
				weeklyReport,
				year);
			//Hoàn thành
			string sqlQuery3 = string.Format(@"select BoPhanBaoCao,NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu,
				Max(NhiemVu)/cast((select 
				sum(NhiemVu)TongNhiemVu from
				(select NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu
				from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				group by Tuan,Thang,Nam,NoiDung)a) as float)*100 TyLe
					from BaoCaoToChiTietSoview 
				where NhiemVu <>'' 
				and NoiDung like N'Hoàn thành'				
				and BoPhanBaoCao like '{0}'
	            and Tuan like '{1}' 
	            and Nam like '{2}'
	            group by Tuan,Thang,Nam,NoiDung,BoPhanBaoCao",
				boPhanBaoCao,
				weeklyReport,
				year);
			//Đơn hàng đang thực hiện
			string sqlQuery4 = string.Format(@"select BoPhanBaoCao,NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu,
				Max(NhiemVu)/cast((select 
				sum(NhiemVu)TongNhiemVu from
				(select NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu
				from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				group by Tuan,Thang,Nam,NoiDung)a) as float)*100 TyLe
					from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				and NoiDung like N'Đang thực hiện'
				and BoPhanBaoCao like '{0}'
	            and Tuan like '{1}' 
	            and Nam like '{2}'
	            group by Tuan,Thang,Nam,NoiDung,BoPhanBaoCao",
				boPhanBaoCao,
				weeklyReport,
				year);
			// Rủi ro đơn hàng sắp thực hiện
			string sqlQuery5 = string.Format(@"select BoPhanBaoCao,NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu,
				Max(NhiemVu)/cast((select 
				sum(NhiemVu)TongNhiemVu from
				(select NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu
				from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				group by Tuan,Thang,Nam,NoiDung)a) as float)*100 TyLe
					from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				and NoiDung like N'Rủi ro trong các đơn hàng được lên kế hoạch sắp tới (Rủi ro tiềm ẩn)'
				and BoPhanBaoCao like '{0}'
	            and Tuan like '{1}' 
	            and Nam like '{2}'
	            group by Tuan,Thang,Nam,NoiDung,BoPhanBaoCao",
				boPhanBaoCao,
				weeklyReport,
				year);
			//Nhiệm vụ tổng cộng
			string sqlQuery6 = string.Format(@"select 
				sum(NhiemVu)TongNhiemVu from
				(select NoiDung,Thang,Tuan,Nam,
				Max(NhiemVu)NhiemVu
				from BaoCaoToChiTietSoview 
				where NhiemVu <>''
				group by Tuan,Thang,Nam,NoiDung)a");
			var dt1 = Model.Function.GetDataTable(sqlQuery1);
			var dt2 = Model.Function.GetDataTable(sqlQuery2);
			var dt3 = Model.Function.GetDataTable(sqlQuery3);
			var dt4 = Model.Function.GetDataTable(sqlQuery4);
			var dt5 = Model.Function.GetDataTable(sqlQuery5);
			var dt6 = Model.Function.GetDataTable(sqlQuery6);

            if (dt1.Rows.Count > 0|| dt2.Rows.Count > 0|| 
				dt3.Rows.Count > 0|| dt4.Rows.Count > 0|| 
				dt5.Rows.Count > 0||dt6.Rows.Count>0)
            {
				//Quá hạn vượt vòng kiểm soát
				//NoiDung.DataBindings.Add("Text", dt, "NoiDung");
				//SoNhiemVu.DataBindings.Add("Text", dt, "NhiemVu", "{0:0,0}");
	
                xrTableCellNVVuotVongKiemSoat.DataBindings.Add("Text", dt1, "NhiemVu");
                xrTableCellTyLeVuotVongKiemSoat.DataBindings.Add("Text", dt1, "TyLe");
				//Quá hạn trong vòng kiểm soát
	
				xrTableCellNVTrongVongKiemSoat.DataBindings.Add("Text", dt2, "NhiemVu");
				xrTableCellTyLeTrongVongKiemSoat.DataBindings.Add("Text", dt2, "TyLe");
				//Hoàn thành
		
				xrTableCellNVHoanThanh.DataBindings.Add("Text", dt3, "NhiemVu");
				xrTableCellTyLeHoanThanh.DataBindings.Add("Text", dt3, "TyLe");
				//Đơn hàng đang thực hiện

				xrTableCellNVDangThucHien.DataBindings.Add("Text", dt4, "NhiemVu");
				xrTableCellTyLeDangThucHien.DataBindings.Add("Text", dt4, "TyLe");
				// Rủi ro đơn hàng sắp thực hiện
			
				xrTableCellNVRuiRo.DataBindings.Add("Text", dt5, "NhiemVu");
				xrTableCellTyLeRuiRo.DataBindings.Add("Text", dt5, "TyLe");
				// Tổng cộng
				xrTableCellNVTongCong.DataBindings.Add("Text", dt6, "TongNhiemVu");
			}
		}
    }
}
