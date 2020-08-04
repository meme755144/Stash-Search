using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Stash_Search.Properties;

namespace Stash_Search
{
    public partial class GridSetting : Form
    {
        public GridSetting()
        {
            InitializeComponent();
            StashType.SelectedItem = StashType.Items[0];
        }

        API.IniManagerAPI iniManagerAPI = new API.IniManagerAPI(SettingFilePath);
        static string SettingFilePath = System.Environment.CurrentDirectory + @"\setting.ini";

        private void GridSetting_Load(object sender, EventArgs e)
        {
            string Setting_Location_X = "";
            string Setting_Location_Y = "";
            string Setting_Width = "";
            string Setting_Height = "";

            Setting_Location_X = iniManagerAPI.ReadIniFile("Grid", "Location.X");
            Setting_Location_Y = iniManagerAPI.ReadIniFile("Grid", "Location.Y");
            Setting_Width = iniManagerAPI.ReadIniFile("Grid", "Width");
            Setting_Height = iniManagerAPI.ReadIniFile("Grid", "Height");

            if (Setting_Location_X != "" && Setting_Location_Y != "" && Setting_Width != "" && Setting_Height != "")
            {
                this.Location = new Point(int.Parse(Setting_Location_X), int.Parse(Setting_Location_Y));
                this.Width = int.Parse(Setting_Width);
                this.Height = int.Parse(Setting_Height);
            }
        }

        #region Move 按鈕事件
        bool IsMoveType = false;
        int CurrentPositionX;
        int CurrentPositionY;

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMoveType = true;
                CurrentPositionX = e.X;
                CurrentPositionY = e.Y;
            }
        }
        private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveType)
            {
                Location = new Point(Left + e.X - CurrentPositionX, Top + e.Y - CurrentPositionY);
            }
        }
        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMoveType = false;
            }
        }

        #endregion

        #region Title Move 按鈕事件

        private void TitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMoveType = true;
                CurrentPositionX = e.X;
                CurrentPositionY = e.Y;
            }
        }
        private void TitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveType)
            {
                Location = new Point(Left + e.X - CurrentPositionX, Top + e.Y - CurrentPositionY);
            }
        }
        private void TitlePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMoveType = false;
            }
        }

        #endregion

        #region Corner 按鈕事件
        bool IsCornerType = false;

        private void CornerButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        }
        private void CornerButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsCornerType = true;
            }
        }
        private void CornerButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCornerType)
            {
                this.Size = new Size(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
                CornerButton.Invalidate();
                CloseButton.Invalidate();
                ShowGrid.Invalidate();
            }
        }
        private void CornerButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsCornerType = false;
            }
        }
        private void CornerButton_LocationChanged(object sender, EventArgs e)
        {
            CornerButton.Refresh();
        }

        #endregion

        #region Grid 顯示事件
        private void ShowGrid_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White, 2.0f);

            float PanelWidth = ShowGrid.Width - 5;
            float PanelHeight = ShowGrid.Height - 5;

            float CellCountX = 0.0f;
            float CellCountY = 0.0f;

            switch (StashType.SelectedIndex)
            {
                case 0:
                    CellCountX = 12;
                    CellCountY = 12;
                    break;
                case 1:
                    CellCountX = 24;
                    CellCountY = 24;
                    break;
            }

            float CellWidth = PanelWidth / CellCountX;
            float CellHeight = PanelHeight / CellCountY;

            for (int x = 0; x <= CellCountX; x++)
            {
                //g.DrawLine(p, x * CellWidth,0.0f, x * CellWidth, CellHeight * CellCountY);
                g.DrawLine(p, x * CellWidth, 0.0f, x * CellWidth, PanelHeight);
            }

            for (int y = 0; y <= CellCountY; y++)
            {
                //g.DrawLine(p, 0, y * CellHeight, CellWidth * CellCountX, y * CellHeight);
                g.DrawLine(p, 0, y * CellHeight, PanelWidth, y * CellHeight);
            }
        }

        private void ShowGrid_SizeChanged(object sender, EventArgs e)
        {
            ShowGrid.Refresh();
        }

        private void StashType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid.Invalidate();
        }

        #endregion

        #region Close 按鈕事件

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            Bitmap picture = new Bitmap(CloseButton.Image);
            picture = ChangeColorBrightness(picture, 0.3f);
            CloseButton.Image = picture;
        }
        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.Image = Resources.Close;
        }
        private void CloseButton_LocationChanged(object sender, EventArgs e)
        {
            CloseButton.Refresh();
        }

        public static Bitmap ChangeColorBrightness(Bitmap bmp, float correctionFactor)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            float correctionFactortemp = correctionFactor;
            if (correctionFactor < 0)
            {
                correctionFactortemp = 1 + correctionFactor;
            }
            for (int counter = 1; counter < rgbValues.Length; counter++)
            {
                float color = (float)rgbValues[counter];
                if (correctionFactor < 0)
                {
                    color *= (int)correctionFactortemp;
                }
                else
                {
                    color = (255 - color) * correctionFactor + color;
                }
                rgbValues[counter] = (byte)color;
            }
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }



        #endregion

        private void SaveButton_Click(object sender, EventArgs e)
        {
            iniManagerAPI.WriteIniFile("Grid", "Location.X", Location.X.ToString());
            iniManagerAPI.WriteIniFile("Grid", "Location.Y", Location.Y.ToString());
            iniManagerAPI.WriteIniFile("Grid", "Width", Width.ToString());
            iniManagerAPI.WriteIniFile("Grid", "Height", Height.ToString());

        }


    }
}
