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

        public void AddListItem(ListItem aListItem)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                int TempItemHeight = aListItem.Height;
                this.SetBounds(this.Location.X, this.Location.Y - TempItemHeight, this.Width, this.Height + TempItemHeight);
            }
            
            flowLayoutPanel1.Controls.Add(aListItem);
        }

        
    }
}
