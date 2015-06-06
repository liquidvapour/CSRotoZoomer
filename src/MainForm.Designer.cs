namespace CSRotoZoomer
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this._animTimer = new System.Windows.Forms.Timer(this.components);
            this._renderDestination = new System.Windows.Forms.Panel();
            this._statusBar = new System.Windows.Forms.StatusStrip();
            this._fpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._canvasSizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._imageSizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._imageInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._fpsTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlTools = new System.Windows.Forms.Panel();
            this.tkbDeltaGama = new System.Windows.Forms.TrackBar();
            this.lblDeltaGama = new System.Windows.Forms.Label();
            this.txtDeltaGama = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._mainMenu.SuspendLayout();
            this._statusBar.SuspendLayout();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbDeltaGama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this._mainMenu.Location = new System.Drawing.Point(0, 0);
            this._mainMenu.Name = "_mainMenu";
            this._mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this._mainMenu.Size = new System.Drawing.Size(861, 28);
            this._mainMenu.TabIndex = 1;
            this._mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.openImageToolStripMenuItem.Text = "&Open Image...";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // _openImageDialog
            // 
            this._openImageDialog.Filter = "Image files (jpg/bmp/gif/png)|*.jpg;*.bmp;*.gif;*.png|All files|*.*";
            this._openImageDialog.InitialDirectory = "c:\\";
            this._openImageDialog.Title = "Open an image file to use in the routine";
            // 
            // _animTimer
            // 
            this._animTimer.Interval = 10;
            this._animTimer.Tick += new System.EventHandler(this._animTimer_Tick);
            // 
            // _renderDestination
            // 
            this._renderDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._renderDestination.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this._renderDestination.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._renderDestination.Location = new System.Drawing.Point(4, 34);
            this._renderDestination.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._renderDestination.Name = "_renderDestination";
            this._renderDestination.Size = new System.Drawing.Size(650, 556);
            this._renderDestination.TabIndex = 3;
            this._renderDestination.Visible = false;
            // 
            // _statusBar
            // 
            this._statusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this._statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fpsLabel,
            this._canvasSizeLabel,
            this._imageSizeLabel,
            this._imageInfoLabel});
            this._statusBar.Location = new System.Drawing.Point(0, 590);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this._statusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._statusBar.Size = new System.Drawing.Size(861, 22);
            this._statusBar.TabIndex = 4;
            // 
            // _fpsLabel
            // 
            this._fpsLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._fpsLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._fpsLabel.Name = "_fpsLabel";
            this._fpsLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // _canvasSizeLabel
            // 
            this._canvasSizeLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._canvasSizeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._canvasSizeLabel.Name = "_canvasSizeLabel";
            this._canvasSizeLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // _imageSizeLabel
            // 
            this._imageSizeLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._imageSizeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._imageSizeLabel.Name = "_imageSizeLabel";
            this._imageSizeLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // _imageInfoLabel
            // 
            this._imageInfoLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._imageInfoLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this._imageInfoLabel.Name = "_imageInfoLabel";
            this._imageInfoLabel.Size = new System.Drawing.Size(829, 17);
            this._imageInfoLabel.Spring = true;
            this._imageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _fpsTimer
            // 
            this._fpsTimer.Interval = 500;
            this._fpsTimer.Tick += new System.EventHandler(this._fpsTimer_Tick);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.txtDeltaGama);
            this.pnlTools.Controls.Add(this.lblDeltaGama);
            this.pnlTools.Controls.Add(this.tkbDeltaGama);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTools.Location = new System.Drawing.Point(661, 28);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(200, 562);
            this.pnlTools.TabIndex = 0;
            // 
            // tkbDeltaGama
            // 
            this.tkbDeltaGama.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "DeltaGamma", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tkbDeltaGama.Location = new System.Drawing.Point(3, 54);
            this.tkbDeltaGama.Maximum = 360;
            this.tkbDeltaGama.Name = "tkbDeltaGama";
            this.tkbDeltaGama.Size = new System.Drawing.Size(185, 56);
            this.tkbDeltaGama.TabIndex = 0;
            // 
            // lblDeltaGama
            // 
            this.lblDeltaGama.AutoSize = true;
            this.lblDeltaGama.Location = new System.Drawing.Point(15, 29);
            this.lblDeltaGama.Name = "lblDeltaGama";
            this.lblDeltaGama.Size = new System.Drawing.Size(87, 17);
            this.lblDeltaGama.TabIndex = 1;
            this.lblDeltaGama.Text = "Delta Gama:";
            // 
            // txtDeltaGama
            // 
            this.txtDeltaGama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "DeltaGamma", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDeltaGama.Location = new System.Drawing.Point(108, 26);
            this.txtDeltaGama.Name = "txtDeltaGama";
            this.txtDeltaGama.Size = new System.Drawing.Size(80, 22);
            this.txtDeltaGama.TabIndex = 2;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(CSRotoZoomer.RotoZoomerViewModel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 612);
            this.Controls.Add(this._renderDestination);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this._mainMenu);
            this.Controls.Add(this._statusBar);
            this.MainMenuStrip = this._mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# RotoZoomer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this._statusBar.ResumeLayout(false);
            this._statusBar.PerformLayout();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbDeltaGama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip _mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog _openImageDialog;
		private System.Windows.Forms.Timer _animTimer;
		private System.Windows.Forms.Panel _renderDestination;
		private System.Windows.Forms.StatusStrip _statusBar;
		private System.Windows.Forms.ToolStripStatusLabel _fpsLabel;
		private System.Windows.Forms.ToolStripStatusLabel _canvasSizeLabel;
		private System.Windows.Forms.ToolStripStatusLabel _imageSizeLabel;
		private System.Windows.Forms.ToolStripStatusLabel _imageInfoLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Timer _fpsTimer;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.TrackBar tkbDeltaGama;
        private System.Windows.Forms.TextBox txtDeltaGama;
        private System.Windows.Forms.Label lblDeltaGama;
	}
}

