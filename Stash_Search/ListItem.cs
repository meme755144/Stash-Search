using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stash_Search.Properties;

namespace Stash_Search
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private void Button_PanelControl_Click(object sender, EventArgs e)
        {
            if (Button_PanelControl.Tag.ToString() == "Down")
            {
                Button_PanelControl.Image = Resources.UpArrow;
                Button_PanelControl.Tag = "Up";

                this.Height = 26;
            }
            else
            {
                Button_PanelControl.Image = Resources.DownArrow;
                Button_PanelControl.Tag = "Down";

                this.Height = 253;
            }
        }

        #region 屬性設定

        private string _ItemName;
        private string _Price;
        private string _StashName;
        private Image _Icon;
        private bool _BlockUnfold = true;

        public int ItemWidth
        {
            get { return ItemPanel.Width; }
        }

        public int ItemHeight
        {
            get { return ItemPanel.Height; }
        }



        [Category("自定義屬性設定")]
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; Label_ItemNameContent.Text = value; }
        }

        [Category("自定義屬性設定")]
        public string Price
        {
            get { return _Price; }
            set { _Price = value; Label_PriceContent.Text = value; }
        }

        [Category("自定義屬性設定")]
        public string StashName
        {
            get { return _StashName; }
            set { _StashName = value; Label_StasNameContent.Text = value; }
        }

        [Category("自定義屬性設定")]
        public Image Icon
        {
            get { return _Icon; }
            set { _Icon = value; Img_ItemIcon.Image = value; }
        }

        [Category("自定義屬性設定")]
        public bool BlockUnfold
        {
            get { return _BlockUnfold; }
            set
            {
                _BlockUnfold = value;

                if (value)
                {
                    Button_PanelControl.Image = Resources.DownArrow;
                    Button_PanelControl.Tag = "Down";

                    this.Height = 253;
                }
                else
                {
                    Button_PanelControl.Image = Resources.UpArrow;
                    Button_PanelControl.Tag = "Up";

                    this.Height = 26;
                }
            }
        }

        #endregion

    }
}
