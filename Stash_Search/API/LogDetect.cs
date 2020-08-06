using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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

        public string LogFormat(string LogStr)
        {
            try
            {
                Regex InfoRegex = new Regex(@"\[INFO Client [a-zA-Z0-9]*\]");

                string[] StringList = InfoRegex.Split(LogStr);
                if (StringList.Length == 2)
                {
                    string TimeStamp = StringList[0];
                    string Info = StringList[1].Trim();
                    string[] MessageList = Info.Split(new char[] { ':' }, 2);
                    string BuyerName = MessageList[0].Split(' ').Last();

                    string SellerFrom = MessageList[0].Replace(BuyerName, "").Trim();
                    string Language = "";
                    switch (SellerFrom)
                    {
                        case "@來自":
                        case "@From":
                            Language = "English";
                            break;
                        case "@Von":
                            Language = "Français|Deutsch";
                            break;
                        case "@De":
                            Language = "Português Brasileiro|Español";
                            break;
                        case "@От кого":
                            Language = "Русский";
                            break;
                        case "@จาก":
                            Language = "ไทย";
                            break;
                        case "@수신":
                            Language = "한국어";
                            break;
                        default:
                            Language = "Unknown";
                            return "";
                    }

                    Regex MsgRegex = new Regex(@"\(.+[0-9]\)|\(.+\)");

                    MatchCollection sdd = MsgRegex.Matches(MessageList[1]);
                    string StashInfo = "";
                    foreach (var item in sdd)
                    {
                        if (Language == "한국어")
                        {
                            if (new string[] { "보관함 탭", "위치", "왼쪽", "상단" }.All(x => item.ToString().Contains(x)))
                            {
                                StashInfo = item.ToString();
                            }
                        }
                        else
                        {
                            StashInfo = item.ToString();
                        }
                    }

                    string StashName = "";
                    string Position_Left = "";
                    string Position_Top = "";

                    string[] StashDataList = StashInfo.Split(new char[] { ',', ';', ':' });

                    switch (Language)
                    {

                        case "English":
                            StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("stash tab ", "").Trim()).Trim();
                            Position_Left = StashDataList[2].Replace("left", "").Trim();
                            Position_Top = StashDataList[3].Replace("top", "").Trim().Replace(")", "");
                            break;
                        case "Français|Deutsch":
                            if (StashDataList[0].Contains("onglet de réserve"))
                            {
                                StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("onglet de réserve ", "").Trim()).Trim();
                                Position_Left = StashDataList[1].Replace("e en partant de la gauche", "").Trim();
                                Position_Top = StashDataList[2].Replace("e en partant du haut", "").Trim().Replace(")", "");
                            }
                            else
                            {
                                StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("Truhenfach ", "").Trim()).Trim();
                                Position_Left = StashDataList[2].Replace(" von links", "").Trim();
                                Position_Top = StashDataList[3].Replace(" von oben", "").Trim().Replace(")", "");
                            }
                            break;
                        case "Português Brasileiro|Español":
                            if (StashDataList[0].Contains("aba do baú"))
                            {
                                StashName = StringRemoveHeadAndEnd(StashDataList[1]).Trim();
                                Position_Left = StashDataList[3].Replace("esquerda", "").Trim();
                                Position_Top = StashDataList[4].Replace("topo", "").Trim().Replace(")", "");
                            }
                            else
                            {
                                StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("pestaña de alijo ", "").Trim()).Trim();
                                Position_Left = StashDataList[2].Replace("izquierda", "").Trim();
                                Position_Top = StashDataList[3].Replace("arriba", "").Trim().Replace(")", "");
                            }
                            break;
                        case "Русский":
                            StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("секция ", "").Trim()).Trim();
                            Position_Left = StashDataList[2].Replace("столбец", "").Trim();
                            Position_Top = StashDataList[3].Replace("ряд", "").Trim().Replace(")", "");
                            break;
                        case "ไทย":
                            StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("stash tab ", "").Trim()).Trim();
                            Position_Left = StashDataList[2].Replace("ซ้าย", "").Trim();
                            Position_Top = StashDataList[3].Replace("บน", "").Trim().Replace(")", "");
                            break;
                        case "한국어":
                            StashName = StringRemoveHeadAndEnd(StashDataList[0].Replace("보관함 탭 ", "").Trim()).Trim();
                            Position_Left = StashDataList[2].Replace("왼쪽", "").Trim();
                            Position_Top = StashDataList[3].Replace("상단", "").Trim().Replace(")", "");
                            break;
                    }


                }
            }
            catch (Exception)
            {
                return "";
            }


            //Info.Split()
            return "";
        }

        private string StringRemoveHeadAndEnd(string Value)
        {
            string Result = "";
            Result = Value.Substring(2);
            Result = Result.Substring(0, Result.Length - 1);
            return Result;
        }

        private string StringRemoveHeadAndEnd(string Value, int StartPosition)
        {
            string Result = "";
            Result = Value.Substring(StartPosition);
            Result = Result.Substring(0, Result.Length - 1);
            return Result;
        }
    }

    public class BuyerInfo
    {
        public string Buyer { get; set; }
        public string League { get; set; }
        public string ItemName { get; set; }
        public List<string> Price { get; set; }
        public string StashName { get; set; }
        public Point Location { get; set; }
    }

}
