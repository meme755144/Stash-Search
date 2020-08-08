using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stash_Search
{
    public partial class InfoListView : UserControl
    {
        public InfoListView()
        {
            InitializeComponent();
        }

        public int ItemCount
        {
            get { return flowLayoutPanel1.Controls.Count; }
        }

        public void ViewClear()
        {
            flowLayoutPanel1.Controls.Clear();
        }


        public void AddListItem(ListItem aListItem)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                int TempItemHeight = aListItem.Height;
                this.SetBounds(this.Location.X, this.Location.Y - TempItemHeight, this.Width, this.Height + TempItemHeight);
            }

            aListItem.Button_PanelControl.Click += new EventHandler(InfoListViewChangeHeightEvent);
            aListItem.Button_Cancel.Click += new EventHandler(RemoveListItemEvent);
            flowLayoutPanel1.Controls.Add(aListItem);
        }

        private void InfoListViewChangeHeightEvent(object sender, EventArgs e)
        {
            //Control control = (Control)sender;
            //if (control.Tag.ToString() == "Up")
            //{
            //    this.SetBounds(this.Location.X, this.Location.Y + (ItemCount - 2) * 300, this.Width, this.Height - (ItemCount - 2) * 300);
            //}
            //else
            //{
            //    this.SetBounds(this.Location.X, this.Location.Y - (ItemCount - 2) * 300, this.Width, this.Height + (ItemCount - 2) * 300);
            //}
        }

        private void RemoveListItemEvent(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            RemoveListItem((ListItem)control.Parent.Parent);
        }

        public void RemoveListItem(ListItem aListItem)
        {
            aListItem.BlockUnfold = true;
            flowLayoutPanel1.Controls.Remove(aListItem);

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                int TempItemHeight = aListItem.Height;
                this.SetBounds(this.Location.X, this.Location.Y + TempItemHeight, this.Width, this.Height - TempItemHeight);
            }
        }
    }
}
