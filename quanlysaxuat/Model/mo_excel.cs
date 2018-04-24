using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlysanxuat
{
    class mo_excel
    {
        private System.Data.OleDb.OleDbConnection con;
        private String sConnectionString;
        public mo_excel(String file_readed)
        {
            sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                   "Data Source=" + file_readed + ";" +
                   "Extended Properties=Excel 8.0";
        }

        public System.Data.DataTable get_data_from_excell()
        {
            System.Data.DataTable bang = new System.Data.DataTable();
            try
            {

                con = new System.Data.OleDb.OleDbConnection(sConnectionString);
                con.Open();
                System.Data.OleDb.OleDbCommand lenh = new System.Data.OleDb.OleDbCommand("SELECT * FROM [Sheet1$]", con);
                System.Data.OleDb.OleDbDataAdapter thich_ung = new System.Data.OleDb.OleDbDataAdapter();
                thich_ung.SelectCommand = lenh;
                thich_ung.Fill(bang);
                con.Close();
            }
            catch
            {
            }
            return bang;
        }
    }
}
