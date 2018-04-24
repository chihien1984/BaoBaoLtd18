using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace quanlysanxuat
{
    class OpenfilePDF
    {
        FtpClient ftpClient;
        public string filename { get; set; }
        public string pathname { get; set; }
        public OpenfilePDF()
        {
            ftpClient = new FtpClient("ftp://192.168.1.22", "ftpPublic", "ftp#1234");
        }

        public void runOpen(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.openPDFBanVe();
        }
        public void RunOpenXLS(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            openPDFFileXLSX();
        }
        //Tao Background vao file pdf khi nguoi dung tai ban ve sử dụng thư viện 
        private void BackgroundPDF(string fileName)
        {
            string pat = string.Format(@"{0}\{1}.PDF", AppDomain.CurrentDomain.BaseDirectory, fileName);
            //string pat1 = string.Format(@"{0}\{1}.ico", AppDomain.CurrentDomain.BaseDirectory, "LogoBaoBao");
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(pat);
            //PdfImage image = PdfImage.FromFile(pat1);
            foreach (PdfPageBase page in pdf.Pages)
            {
                PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                brush.Graphics.SetTransparency(0.3f);
                brush.Graphics.Save();
                brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                brush.Graphics.RotateTransform(-45);
                brush.Graphics.DrawString(XuLyDuLieu.LoaiBoDauTiengViet(Login.Username), new PdfFont(PdfFontFamily.Helvetica, 26), PdfBrushes.Green, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                brush.Graphics.Restore();
                brush.Graphics.SetTransparency(1);
                page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
            }
            pdf.SaveToFile(fileName + ".PDF");
        }
        public void openPDFBanVe()
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c taskkill /f /im FoxitReader.exe";
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(500);//chương trình ngủ
                //Xoa tat cac file pdf trong Appdomain giữ lại 1 file vừa mở
                string[] filePaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.PDF");
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                string fileName = this.filename + ".PDF";//file cần download
                string sourcePath = this.pathname;//Đường dẫn file nguồn remote
                string targetPath = AppDomain.CurrentDomain.BaseDirectory;//Đường dẫn file đích local
                //Combine file và đường dẫn
                //string sourceFile = System.IO.Path.Combine(sourcePath, fileName); //combine file remote
                string sourceFile = string.Format(@"{0}/{1}", sourcePath, fileName);
                sourceFile = sourceFile.Replace(@"\\Server\", "");
                sourceFile = sourceFile.Replace(@"\", "/");
                string destFile = System.IO.Path.Combine(targetPath, fileName);//Combine file local  
                //Console.WriteLine(sourceFile);
                //Console.WriteLine(destFile);
                //Download file từ file nguồn đến file đích
                //System.IO.File.Copy(sourceFile, destFile, true);
                ftpClient.download(sourceFile, destFile);
                BackgroundPDF(this.filename);//Ghi quyền vào file pdf vừa tải
                openpdf();//Mở file lên khi copy xong
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thành công: " + ex.Message);
            }
        }
        private void openpdf()
        {
            string pat = string.Format(@"{0}\{1}.PDF", AppDomain.CurrentDomain.BaseDirectory, this.filename);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
        }

        private void OpenXLSX()
        {
            string pat = string.Format(@"{0}\{1}.xlsx", AppDomain.CurrentDomain.BaseDirectory, this.filename);
            if (File.Exists(pat))
            {
                System.Diagnostics.Process.Start(pat);
            }
        }
        public void openPDFFileXLSX()
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "/c taskkill /f /im FoxitReader.exe";
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(500);//chương trình ngủ
                string fileName = this.filename + ".xlsx";//file cần download
                string sourcePath = this.pathname;//Đường dẫn file nguồn remote
                string targetPath = AppDomain.CurrentDomain.BaseDirectory;//Đường dẫn file đích local
                //Combine file và đường dẫn
                //string sourceFile = System.IO.Path.Combine(sourcePath, fileName); //combine file remote
                string sourceFile = string.Format(@"{0}/{1}", sourcePath, fileName);
                sourceFile = sourceFile.Replace(@"\\Server\", "");
                sourceFile = sourceFile.Replace(@"\", "/");
                string destFile = System.IO.Path.Combine(targetPath, fileName);//Combine file local
                ftpClient.download(sourceFile, destFile);
                OpenXLSX();//Mở file lên khi copy xong
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thành công: " + ex.Message);
            }
        }
    }
}
