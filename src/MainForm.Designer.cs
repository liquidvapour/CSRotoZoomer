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
            this._toolsPanel = new System.Windows.Forms.Panel();
            this._zoomCounterTextBox = new System.Windows.Forms.TextBox();
            this._zoomCounterLabel = new System.Windows.Forms.Label();
            this._deltaGamaTextBox = new System.Windows.Forms.TextBox();
            this._deltaGamaLabel = new System.Windows.Forms.Label();
            this._deltaGamaTrackBar = new System.Windows.Forms.TrackBar();
            this._bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._mainMenu.SuspendLayout();
            this._statusBar.SuspendLayout();
            this._toolsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._deltaGamaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSource)).BeginInit();
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
            this._renderDestination.Margin = new System.Windows.Forms.Padding(4);
            this._renderDestination.Name = "_renderDestination";
            this._renderDestination.Size = new System.Drawing.Size(563, 556);
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
            // _toolsPanel
            // 
            this._toolsPanel.Controls.Add(this._zoomCounterTextBox);
            this._toolsPanel.Controls.Add(this._zoomCounterLabel);
            this._toolsPanel.Controls.Add(this._deltaGamaTextBox);
            this._toolsPanel.Controls.Add(this._deltaGamaLabel);
            this._toolsPanel.Controls.Add(this._deltaGamaTrackBar);
            this._toolsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._toolsPanel.Location = new System.Drawing.Point(574, 28);
            this._toolsPanel.Name = "_toolsPanel";
            this._toolsPanel.Size = new System.Drawing.Size(287, 562);
            this._toolsPanel.TabIndex = 0;
            // 
            // _zoomCounterTextBox
            // 
            this._zoomCounterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._zoomCounterTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "ZoomCounter", true));
            this._zoomCounterTextBox.Location = new System.Drawing.Point(123, 110);
            this._zoomCounterTextBox.Name = "_zoomCounterTextBox";
            this._zoomCounterTextBox.Size = new System.Drawing.Size(152, 22);
            this._zoomCounterTextBox.TabIndex = 4;
            // 
            // _zoomCounterLabel
            // 
            this._zoomCounterLabel.AutoSize = true;
            this._zoomCounterLabel.Location = new System.Drawing.Point(15, 113);
            this._zoomCounterLabel.Name = "_zoomCounterLabel";
            this._zoomCounterLabel.Size = new System.Drawing.Size(102, 17);
            this._zoomCounterLabel.TabIndex = 3;
            this._zoomCounterLabel.Text = "Zoom Counter:";
            // 
            // _deltaGamaTextBox
            // 
            this._deltaGamaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._deltaGamaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._bindingSource, "DeltaGamma", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._deltaGamaTextBox.Location = new System.Drawing.Point(123, 26);
            this._deltaGamaTextBox.Name = "_deltaGamaTextBox";
            this._deltaGamaTextBox.Size = new System.Drawing.Size(152, 22);
            this._deltaGamaTextBox.TabIndex = 2;
            // 
            // _deltaGamaLabel
            // 
            this._deltaGamaLabel.AutoSize = true;
            this._deltaGamaLabel.Location = new System.Drawing.Point(15, 29);
            this._deltaGamaLabel.Name = "_deltaGamaLabel";
            this._deltaGamaLabel.Size = new System.Drawing.Size(87, 17);
            this._deltaGamaLabel.TabIndex = 1;
            this._deltaGamaLabel.Text = "Delta Gama:";
            // 
            // _deltaGamaTrackBar
            // 
            this._deltaGamaTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._deltaGamaTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this._bindingSource, "DeltaGamma", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._deltaGamaTrackBar.Location = new System.Drawing.Point(3, 54);
            this._deltaGamaTrackBar.Maximum = 720;
            this._deltaGamaTrackBar.Name = "_deltaGamaTrackBar";
            this._deltaGamaTrackBar.Size = new System.Drawing.Size(272, 56);
            this._deltaGamaTrackBar.TabIndex = 0;
            // 
            // _bindingSource
            // 
            this._bindingSource.DataSource = typeof(CSRotoZoomer.RotoZoomerViewModel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 612);
            this.Controls.Add(this._renderDestination);
            this.Controls.Add(this._toolsPanel);
            this.Controls.Add(this._mainMenu);
            this.Controls.Add(this._statusBar);
            this.MainMenuStrip = this._mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# RotoZoomer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this._statusBar.ResumeLayout(false);
            this._statusBar.PerformLayout();
            this._toolsPanel.ResumeLayout(false);
            this._toolsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._deltaGamaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource _bindingSource;
        private System.Windows.Forms.Panel _toolsPanel;
        private System.Windows.Forms.TrackBar _deltaGamaTrackBar;
        private System.Windows.Forms.TextBox _deltaGamaTextBox;
        private System.Windows.Forms.Label _deltaGamaLabel;
        private System.Windows.Forms.TextBox _zoomCounterTextBox;
        private System.Windows.Forms.Label _zoomCounterLabel;
	}
}

