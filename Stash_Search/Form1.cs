using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stash_Search
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        API.LogDetect LogDetect = new API.LogDetect();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string LogFilePath = @"E:\Path of Exile\logs\Client.txt";
            string CurrentHash = LogDetect.GetFileHash("SHA1", LogFilePath);

            BackGroundWorkerInit(CurrentHash, LogFilePath);
        }


        #region BackGroundWorker 設定
        private BackgroundWorker worker;
        //初始化
        private void BackGroundWorkerInit(string InitHash ,string FilePath)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (o, ea) =>
            {
                BackGroundWorker_DoWork<bool>(InitHash, FilePath); // 可以使用泛型
            };
            worker.ProgressChanged += (o, ea) =>
            {
                BackGroundWorker_ProgressChanged<string>(FilePath);
            };

            worker.RunWorkerAsync();
        }

        //背景執行
        private void BackGroundWorker_DoWork<T>(string OriginHash,string FilePath)
        {
            while (true)
            {
                string CurrnetHash = LogDetect.GetFileHash("SHA1", FilePath);

                if (CurrnetHash != OriginHash)
                {
                    worker.ReportProgress(1);
                    OriginHash = CurrnetHash;
                }

                Thread.Sleep(1000);
            }
        }

        //處理進度條更新
        private void BackGroundWorker_ProgressChanged<T>(string FilePath)
        {
            listBox1.Items.Add(LogDetect.ReadLastLine(FilePath, 3, Encoding.UTF8));
            Console.WriteLine(LogDetect.ReadLastLine(FilePath, 3, Encoding.UTF8));
        }
        #endregion





    }
}
