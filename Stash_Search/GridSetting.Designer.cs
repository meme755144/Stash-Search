namespace Stash_Search
{
    partial class GridSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridSetting));
            this.ShowGrid = new System.Windows.Forms.Panel();
            this.Label_StashType = new System.Windows.Forms.Label();
            this.StashType = new System.Windows.Forms.ComboBox();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.PictureBox();
            this.MoveButton = new System.Windows.Forms.PictureBox();
            this.CornerButton = new System.Windows.Forms.PictureBox();
            this.TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CornerButton)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowGrid
            // 
            resources.ApplyResources(this.ShowGrid, "ShowGrid");
            this.ShowGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShowGrid.Name = "ShowGrid";
            this.ShowGrid.SizeChanged += new System.EventHandler(this.ShowGrid_SizeChanged);
            this.ShowGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowGrid_Paint);
            // 
            // Label_StashType
            // 
            resources.ApplyResources(this.Label_StashType, "Label_StashType");
            this.Label_StashType.Name = "Label_StashType";
            // 
            // StashType
            // 
            this.StashType.BackColor = System.Drawing.Color.LightGray;
            this.StashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.StashType, "StashType");
            this.StashType.FormattingEnabled = true;
            this.StashType.Items.AddRange(new object[] {
            resources.GetString("StashType.Items"),
            resources.GetString("StashType.Items1")});
            this.StashType.Name = "StashType";
            this.StashType.SelectedIndexChanged += new System.EventHandler(this.StashType_SelectedIndexChanged);
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.DimGray;
            this.TitlePanel.Controls.Add(this.SaveButton);
            this.TitlePanel.Controls.Add(this.CloseButton);
            this.TitlePanel.Controls.Add(this.MoveButton);
            this.TitlePanel.Controls.Add(this.StashType);
            this.TitlePanel.Controls.Add(this.Label_StashType);
            resources.ApplyResources(this.TitlePanel, "TitlePanel");
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseDown);
            this.TitlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseMove);
            this.TitlePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseUp);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.TabStop = false;
            this.CloseButton.LocationChanged += new System.EventHandler(this.CloseButton_LocationChanged);
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseEnter += new System.EventHandler(this.CloseButton_MouseEnter);
            this.CloseButton.MouseLeave += new System.EventHandler(this.CloseButton_MouseLeave);
            // 
            // MoveButton
            // 
            resources.ApplyResources(this.MoveButton, "MoveButton");
            this.MoveButton.Image = global::Stash_Search.Properties.Resources.Grid;
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.TabStop = false;
            this.MoveButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButton_MouseDown);
            this.MoveButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveButton_MouseMove);
            this.MoveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButton_MouseUp);
            // 
            // CornerButton
            // 
            resources.ApplyResources(this.CornerButton, "CornerButton");
            this.CornerButton.Image = global::Stash_Search.Properties.Resources.Corner;
            this.CornerButton.Name = "CornerButton";
            this.CornerButton.TabStop = false;
            this.CornerButton.LocationChanged += new System.EventHandler(this.CornerButton_LocationChanged);
            this.CornerButton.Paint += new System.Windows.Forms.PaintEventHandler(this.CornerButton_Paint);
            this.CornerButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CornerButton_MouseDown);
            this.CornerButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CornerButton_MouseMove);
            this.CornerButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CornerButton_MouseUp);
            // 
            // GridSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.ShowGrid);
            this.Controls.Add(this.CornerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GridSetting";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Green;
            this.Load += new System.EventHandler(this.GridSetting_Load);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CornerButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox CornerButton;
        private System.Windows.Forms.Panel ShowGrid;
        private System.Windows.Forms.Label Label_StashType;
        private System.Windows.Forms.ComboBox StashType;
        private System.Windows.Forms.PictureBox MoveButton;
        private System.Windows.Forms.PictureBox CloseButton;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Button SaveButton;
    }
}