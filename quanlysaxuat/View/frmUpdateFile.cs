using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace quanlysanxuat
{
    public partial class frmUpdateFile : DevExpress.XtraEditors.XtraForm
    {
        FtpClient ftpClient;
        public frmUpdateFile()
        {
            InitializeComponent();
            ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }

        private void UpdateFile_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)//Update bản vẽ
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox3.Text = openFileDialog1.FileName;
                    string fullFileName = openFileDialog1.FileName;
                    string fileName = openFileDialog1.SafeFileName;
                    ftpClient.upload("kythuat/DM_SANPHAM/"+fileName, fullFileName);
                    //Console.WriteLine("kythuat/QTRINH_SAN_XUAT/" + fileName);
                    //Console.WriteLine(fullFileName);
                    if (ftpClient.message == "success")
                    {
                        //update path file name database
                        MessageBox.Show(ftpClient.pathFileName);
                    }
                    else
                    {
                        MessageBox.Show(ftpClient.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btnDowload_Click(object sender, EventArgs e)
        {
            //ftpClient.download(textBox1.Text,textBox2.Text);
            /*ftpClient.download("kythuat/DM_SANPHAM/" + textBox1.Text + ".PDF", "D:\\" + textBox1.Text + ".PDF");
            if (ftpClient.message == "success")
            {
                //update path file name database
                MessageBox.Show(ftpClient.pathFileName);
            }
            else
            {
                MessageBox.Show(ftpClient.message);
            }*/
        }

        private void btnUpdateQuiTrinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox3.Text = openFileDialog1.FileName;
                    string fullFileName = openFileDialog1.FileName;
                    string fileName = openFileDialog1.SafeFileName;
                    ftpClient.upload("kythuat/QTRINH_SAN_XUAT/" + fileName, fullFileName);
                    //Console.WriteLine("kythuat/DM_SANPHAM/" + fileName);
                    //Console.WriteLine(fullFileName);
                    if (ftpClient.message == "success")
                    {
                        //update path file name database
                        MessageBox.Show(ftpClient.pathFileName);
                    }
                    else
                    {
                        MessageBox.Show(ftpClient.message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}