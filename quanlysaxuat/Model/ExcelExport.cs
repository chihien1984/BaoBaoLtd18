using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace quanlysanxuat
{
    public static class ExcelExport
    {
        public static void DataTableToExcel(System.Data.DataTable dt, string filename)
        {
            string ExcelPathx = AppDomain.CurrentDomain.BaseDirectory + "\\FILE TRIEN KHAI.xlsm";
            if (dt.Rows.Count == 0) { return; }
            Excel.Application App = null;
            Excel.Workbook Wb;
            Excel.Worksheet Ws;
            int isExcelOpen = 0;
            try
            {
                App = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                App = new Excel.Application();
                isExcelOpen = 1;
            }
            //catch(Exception){}
            //finally
            //{
            //    App = new Excel.Application();
            //    isExcelOpen = 1;
            //}
            //oXL.Visible = true;
            Wb = (Excel.Workbook)(App.Workbooks.Open(ExcelPathx, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
            //Wb = (Excel.Workbook)(App.Workbooks.Add(Missing.Value));
            Ws = (Excel.Worksheet)Wb.Worksheets["Sheet"];
            //Ws = (Excel.Worksheet)Wb.ActiveSheet;
            try
            {
                //Ws = (Excel.Worksheet)App.Worksheets.Add();

                // Xử lý tiêu đề cột

                int rowCount = dt.Rows.Count;
                int colCount = dt.Columns.Count;
                int c = 0;
                //int r = 0;

                //Excel.Range HeaderRow = Ws.get_Range("A1");

                //foreach (System.Data.DataColumn dc in dt.Columns)
                //{
                //    HeaderRow.get_Offset(0, r).Value2 = dc.ColumnName;
                //    r++;
                //}

                //HeaderRow.EntireRow.Font.Bold = true;
                //HeaderRow.EntireRow.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // Xử lý data-> mảng 2 chiều

                object[,] rowData = new object[rowCount, colCount];

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        if (IsNumeric(row[col].GetType().ToString()))
                        {
                            rowData[c, col] = System.Convert.ToDouble(row[col].ToString());
                        }
                        else
                        {
                            rowData[c, col] = row[col].ToString();
                        }
                    }
                    c++;
                }
                //xóa mảng trước khi pass
                //Ws.get_Range("A2:AZ600").get_Resize(rowCount, colCount).Value2 = rowData; Xóa mảng trùng với mảng copy lên excel
                Ws.get_Range("A2:AZ600").Cells.ClearContents();
                // Paste mảng vào excel

                Ws.get_Range("A2").get_Resize(rowCount, colCount).Value2 = rowData;
                //Ws.get_Range("A1").get_Resize(1, colCount).EntireColumn.AutoFit();
                // Giãn cột

                // Lưu file

                string ExcelPath = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0}.xlsm", "FILE TRIEN KHAI");

                if (System.IO.File.Exists(ExcelPath))
                {
                    System.IO.File.Delete(ExcelPath);
                }

                Wb.SaveAs(ExcelPath, AccessMode: Excel.XlSaveAsAccessMode.xlShared);
                Wb.Close();
                //App.Quit();
                if (isExcelOpen == 1) { App.Quit(); }
                dt.Dispose();
              

            }
            catch
            {
                //throw ex;
            }

            // Dọn rác
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Ws);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            System.Threading.Thread.Sleep(400);
        }

        private static bool IsNumeric(string stype)
        {
            //return stype != null && "Byte,Decimal,Double,Int16,Int32,Int64,SByte,Single,UInt16,UInt32,UInt64,".Contains("stype");
            string stringToCheck = stype;
            string[] stringArray = { "System.Byte", "System.Decimal", "System.Double", "System.Int16", "System.Int32", "System.Int64", "System.Single", "System.UInt16", "System.UInt32", "System.UInt64" };
            foreach (string x in stringArray)
            {
                if (stringToCheck.Contains(x))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
