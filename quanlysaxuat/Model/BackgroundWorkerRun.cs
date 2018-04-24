using System;
using System.Linq;

namespace quanlysanxuat
{
    class BackgroundWorkerRun
    {
        // have reference to the class where the worker will be running
        private OpenfilePDF mwc = null;
        //public DevExpress.XtraWaitForm.ProgressPanel proccessBar
        //{
        //    set;
        //    get;
        //}
        //public DevExpress.XtraEditors.SimpleButton btDownload
        //{
        //    set;
        //    get;
        //}

        public string parthbanve {get;set;}
        public string parthquytrinh { get; set;}
     
        public System.Windows.Forms.Form formLoad
        {
            set;
            get;
        }

        public string filename { get; set; }
        public string pathname { get; set; }
        // run the worker
        public void RunMyWorker()
        {
            mwc = new OpenfilePDF();
            mwc.filename = this.filename;
            mwc.pathname = this.pathname;
            System.ComponentModel.BackgroundWorker bgWork = new System.ComponentModel.BackgroundWorker();
            bgWork.DoWork += new System.ComponentModel.DoWorkEventHandler(mwc.runOpen);
            bgWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            bgWork.RunWorkerAsync();
        }
        public void RunMyWorkerXls()
        {
            mwc = new OpenfilePDF();
            mwc.filename = this.filename;
            mwc.pathname = this.pathname;
            System.ComponentModel.BackgroundWorker bgWork = new System.ComponentModel.BackgroundWorker();
            bgWork.DoWork += new System.ComponentModel.DoWorkEventHandler(mwc.RunOpenXLS);
            bgWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            bgWork.RunWorkerAsync();
        }
        public void hideProcessBar()
        {
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.formLoad.Close();
                //proccessBar.Visible = false;
                //btDownload.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
