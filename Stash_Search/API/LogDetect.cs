using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Stash_Search.API
{
    class LogDetect
    {
        public string GetFileHash(string HashType, string FilePath)
        {
            //若檔案不存在則離開
            if (!File.Exists(FilePath))
            {
                return "";
            }

            using (HashAlgorithm ha = HashAlgorithm.Create(HashType))
            {
                //4.開啟檔案
                using (Stream myStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] myHash;
                    //5.產生加密的Code
                    try
                    {
                        myHash = ha.ComputeHash(myStream);
                    }
                    catch (NullReferenceException ex)
                    {
                        return "";
                    }

                    //6.顯示雜湊值
                    //依檔案建立空字串
                    StringBuilder NewHashCode = new StringBuilder(myHash.Length);
                    //轉換成加密的Code
                    foreach (byte AddByte in myHash)
                    {
                        NewHashCode.AppendFormat("{0:X2}", AddByte);
                    }

                    return NewHashCode.ToString();
                }
            }
        }

        /// <summary>
        /// 讀取文本文件最後的內容
        /// </summary>
        /// <param name="AFileName">文件名</param>
        /// <param name="ALineCount">行數</param>
        /// <param name="AEncoding">字符編碼</param>
        /// <returns>返回讀取的內容</returns>
        public string ReadLastLine(string AFileName, int ALineCount, Encoding AEncoding)
        {
            if (ALineCount <= 0) return string.Empty;
            if (!File.Exists(AFileName)) return string.Empty; // 文件不存在
            if (AEncoding == null) AEncoding = Encoding.Default;
            using (FileStream vFileStream = new FileStream(AFileName,
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                if (vFileStream.Length <= 0) return string.Empty; // 空文件
                byte[] vBuffer = new byte[0x1000]; // 緩沖區
                int vReadLength; // 讀取到的大小
                int vLineCount = 0; // 讀取的行數
                int vReadCount = 0; // 讀取的次數
                int vScanCount = 0; // 掃描過的字符數
                long vOffset = 0; // 向后讀取的位置
                do
                {
                    vOffset = vBuffer.Length * ++vReadCount;
                    int vSpace = 0; // 偏移超出的空間
                    if (vOffset >= vFileStream.Length) // 超出範圍
                    {
                        vSpace = (int)(vOffset - vFileStream.Length);
                        vOffset = vFileStream.Length;
                    }
                    vFileStream.Seek(-vOffset, SeekOrigin.End); //“SeekOrigin.End”反方向偏移讀取位置

                    vReadLength = vFileStream.Read(vBuffer, 0, vBuffer.Length - vSpace);
                    #region 所讀的緩沖里有多少行
                    for (int i = vReadLength - 1; i >= 0; i--)
                    {
                        if (vBuffer[i] == 10)
                        {
                            if (vScanCount > 0) vLineCount++; // #13#10為回車換行
                            if (vLineCount >= ALineCount) break;
                        }
                        vScanCount++;
                    }
                    #endregion 所讀的緩沖里有多少行
                } while (vReadLength >= vBuffer.Length && vOffset < vFileStream.Length &&
                    vLineCount < ALineCount);

                if (vReadCount > 1) // 讀的次數超過一次，則需重分配緩沖區
                {
                    vBuffer = new byte[vScanCount];
                    vFileStream.Seek(-vScanCount, SeekOrigin.End);
                    vReadLength = vFileStream.Read(vBuffer, 0, vBuffer.Length);
                }
                return AEncoding.GetString(vBuffer, vReadLength - vScanCount, vScanCount);
            }
        }

    }
}
