using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //GridSetting gridSettingForm = new GridSetting();
            //gridSettingForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //1.選擇加密類型
            string myHashName = "SHA1";
            //2.選擇加密檔案
            String myFile = @"E:\Path of Exile\logs\Client.txt";

            //若檔案不存在則離開
            if (!File.Exists(myFile))
            {
                return;
            }
            //3.建立HashAlgorithm類別
            using (HashAlgorithm ha = HashAlgorithm.Create(myHashName))
            {
                //4.開啟檔案
                using (Stream myStream = new FileStream(myFile, FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
                {
                    //5.產生加密的Code
                    byte[] myHash = ha.ComputeHash(myStream);
                    //6.顯示雜湊值
                    //依檔案建立空字串
                    StringBuilder NewHashCode = new StringBuilder(myHash.Length);
                    //轉換成加密的Code
                    foreach (byte AddByte in myHash)
                    {
                        NewHashCode.AppendFormat("{0:X2}", AddByte);
                    }
                    label1.Text = NewHashCode.ToString();
                }
            }
        }
    }
}
