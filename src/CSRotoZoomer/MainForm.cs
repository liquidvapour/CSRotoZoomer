///////////////////////////////////////////////////////////////////
// C# implementation of old-skool demoscene roto-zoomer effect
// Programmed by Frans 'Otis' Bouma
// http://weblogs.asp.net/fbouma
// 
// Effect invented by Chaos / Sanity in 1989 on the Amiga 500
///////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CSRotoZoomer
{
    public partial class MainForm : Form
    {
        private readonly IRotoZoomer _rotoZoomer;
        private string _fpsString, _canvasSizeString, _imageSizeString, _imageInfoString;
        private DateTime _timeCurrentFrame, _timePreviousFrame;
        private Bitmap _srcImage;

        public MainForm(IRotoZoomer rotoZoomer)
        {
            InitializeComponent();

            // be sure this dialog uses double buffered rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, false);

            _rotoZoomer = rotoZoomer;

            _bindingSource.DataSource = new RotoZoomerViewModel(_rotoZoomer);
            
            _fpsString = string.Empty;
            _canvasSizeString = string.Empty;
            _imageSizeString = string.Empty;
            _imageInfoString = string.Empty;
            _timeCurrentFrame = DateTime.Now;
            _timePreviousFrame = _timeCurrentFrame;

            ResizeCanvas();
        }

        private RotoZoomerViewModel ViewModel
        {
            get { return (RotoZoomerViewModel)_bindingSource.DataSource; }
        }

        /// <summary>
        ///     Handles the Click event of the openImageToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = _openImageDialog.ShowDialog(this);
            if (result != DialogResult.OK)
            {
                return;
            }

            _fpsTimer.Enabled = false;
            _animTimer.Enabled = false;

            Cursor = Cursors.WaitCursor;
            //Application.DoEvents();
            // user has specified a filename, load the image and start the rotozoomer
            var filename = _openImageDialog.FileName;
            _srcImage = new Bitmap(filename);

            ThreadPool.QueueUserWorkItem(InitRotoZoomer, _srcImage);
        }


        private void InitRotoZoomer(object _)
        {
            _rotoZoomer.InitRotoZoomer(_srcImage);

            
            // Called on another thread so needs to BeginInvoke starting 
            // animation so it happens on the UI thread.
            BeginInvoke(new Action(StartAnamating));
        }

        private void StartAnamating()
        {
            _imageInfoString = _openImageDialog.FileName;
            _imageSizeString = string.Format("Image: {0} x {1}", _srcImage.Width, _srcImage.Height);

            _srcImage.Dispose();
            _srcImage = null;

            Cursor = Cursors.Default;
            
            _animTimer.Enabled = true;
            _fpsTimer.Enabled = true;
            _animTimer.Start();
            _fpsTimer.Start();
        }

        /// <summary>
        ///     Handles the Tick event of the _animTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void _animTimer_Tick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) return;

            _timePreviousFrame = _timeCurrentFrame;
            _timeCurrentFrame = DateTime.Now;

            var dt = (_timeCurrentFrame - _timePreviousFrame).TotalMilliseconds * 0.001;

            _rotoZoomer.Update(dt);

            ViewModel.OnUpdate();
            Invalidate();
        }

        /// <summary>
        ///     Handles the Resize event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizeCanvas();
        }

        private void ResizeCanvas()
        {
            if (_rotoZoomer == null) return;

            _rotoZoomer.ResizeCanvas(_renderDestination.Bounds);
            _canvasSizeString = string.Format("Canvas: {0} x {1}", _renderDestination.Width, _renderDestination.Height);
        }

        /// <summary>
        ///     Handles the Paint event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // draw the new frame onto the form.
            _rotoZoomer.RenderUsing(e.Graphics);
            UpdateStatusBar();
        }

        /// <summary>
        ///     Updates the status bar elements
        /// </summary>
        private void UpdateStatusBar()
        {
            _fpsLabel.Text = _fpsString;
            _canvasSizeLabel.Text = _canvasSizeString;
            _imageSizeLabel.Text = _imageSizeString;
            _imageInfoLabel.Text = _imageInfoString;
        }


        /// <summary>
        ///     Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Handles the Tick event of the _fpsTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void _fpsTimer_Tick(object sender, EventArgs e)
        {
            var millisecondsBetweenFrames = _timeCurrentFrame.Subtract(_timePreviousFrame).Milliseconds;
            _fpsString = millisecondsBetweenFrames == 0 
                ? "0 frames/sec" 
                : string.Format("{0} frames / sec", (1000.0f / millisecondsBetweenFrames).ToString(".00"));
        }
    }
}