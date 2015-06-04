///////////////////////////////////////////////////////////////////
// C# implementation of old-skool demoscene roto-zoomer effect
// Programmed by Frans 'Otis' Bouma
// http://weblogs.asp.net/fbouma
// 
// Effect invented by Chaos / Sanity in 1989 on the Amiga 500
///////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CSRotoZoomer
{
    public partial class MainForm : Form
    {
        private int _canvasWidth, _canvasHeight, _zoomCounter, _zoomInMax, _zoomOutMax, _imageWidth, _imageHeight;
        private Bitmap _renderCanvas;
        private bool _zoomIn, _resetCanvas;
        private double _gamma, _deltaGamma, _xZoomDelta, _yZoomDelta;
        private string _fpsString, _canvasSizeString, _imageSizeString, _imageInfoString;
        private DateTime _timeCurrentFrame, _timePreviousFrame;

        // coordinate source.
        private readonly double[] _xSourceCoords = { -128, 128, -128 };
        private readonly double[] _ySourceCoords = { -64, -64, 64 };

        // coordinate destination. (for drawing).
        private readonly double[] _xDestinationCoords = { -128, 128, -128 };
        private readonly double[] _yDestinationCoords = { -64, -64, 64 };

        private uint[] _sourcePixels;

        /// <summary>
        ///     CTor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // be sure this dialog uses double buffered rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            _resetCanvas = false;
            _fpsString = string.Empty;
            _canvasSizeString = string.Empty;
            _imageSizeString = string.Empty;
            _imageInfoString = string.Empty;
            _timeCurrentFrame = DateTime.Now;
            _timePreviousFrame = _timeCurrentFrame;
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

            // user has specified a filename, load the image and start the rotozoomer
            InitRotoZoomer(_openImageDialog.FileName);
        }

        /// <summary>
        ///     Initializes the roto zoomer.
        /// </summary>
        private void InitRotoZoomer(string filename)
        {
            _fpsTimer.Enabled = false;
            _animTimer.Enabled = false;
            CreateCanvas();

            // load bitmap specified by the user.
            var srcImage = new Bitmap(filename);
            _imageWidth = srcImage.Width;
            _imageHeight = srcImage.Height;
            _imageInfoString = filename;
            _imageSizeString = string.Format("Image: {0} x {1}", _imageWidth, _imageHeight);

            // initialize our parameters.
            _zoomCounter = 0;
            _zoomIn = false;

            // deltaGamma value, controls rotation.
            _deltaGamma = 2.0;

            // deltas for x and y zooming. If they're not the same the image gets stretched.
            _xZoomDelta = 2.0;
            _yZoomDelta = 1.0;

            // zoom counter values when the zoom action (in/out) will swap. Give this different values for wicked results. 
            _zoomInMax = 200;
            _zoomOutMax = 300;

            if ((_imageWidth == 0) || (_imageHeight == 0))
            {
                throw new ApplicationException("The image you selected has a width and/or height of 0 and isn't usable");
            }

            double imageWidthD = _imageWidth;
            double imageHeightD = _imageHeight;
            if (_xZoomDelta != 0)
            {
                _zoomInMax = (int) (((imageWidthD/2.0)/_xZoomDelta) - 10.0);
            }
            _zoomOutMax = _zoomInMax*5;
            _yZoomDelta = (imageHeightD/imageWidthD)*_xZoomDelta;

            // use slow getpixel method. This is slow because GetPixel is very slow and also the on-the-fly ARGB conversion. 
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            _sourcePixels = CreatePixelArrayFrom(srcImage);

            Cursor = Cursors.Default;
            Application.DoEvents();

            InitializeCoordArrays(imageWidthD, imageHeightD);

            _animTimer.Enabled = true;
            _fpsTimer.Enabled = true;
            _animTimer.Start();
            _fpsTimer.Start();
        }

        private static uint[] CreatePixelArrayFrom(Bitmap srcImage)
        {
            var sourcePixels = new uint[srcImage.Width*srcImage.Height];
            // read the initial pixels into the srcpixel array. This makes it possible to perform an in-place rendering to avoid memory trashing
            switch (srcImage.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    PopulateSourcePixelsFrom32bpp(srcImage, sourcePixels);
                    break;
                case PixelFormat.Format8bppIndexed:
                    PopulateSourcePixelsFrom8bpp(srcImage, sourcePixels);
                    break;
                default:
                    PopulateSourcePixelsDefault(srcImage, sourcePixels);
                    break;
            }
            return sourcePixels;
        }

        private void InitializeCoordArrays(double imageWidthD, double imageHeightD)
        {
            // init coord arrays for the three points we're using to read the source pixels
            // A ---------- B
            // | read direction ->
            // | 
            // |
            // |
            // C
            var halfWidthSrc = imageWidthD/2.0;
            var halfHeightSrc = imageHeightD/2.0;
            _xSourceCoords[0] = -halfWidthSrc;
            _xSourceCoords[1] = halfWidthSrc;
            _xSourceCoords[2] = -halfWidthSrc;
            _ySourceCoords[0] = -halfHeightSrc;
            _ySourceCoords[1] = -halfHeightSrc;
            _ySourceCoords[2] = halfHeightSrc;
        }

        private static unsafe void PopulateSourcePixelsFrom8bpp(Bitmap srcImage, IList<uint> sourcePixels)
        {
            var srcData = srcImage.LockBits(
                new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                ImageLockMode.ReadWrite,
                srcImage.PixelFormat);


            // first convert the palet to uint's (ARGB). This is an operation which needs to be done once, so we don't convert
            // the same color over and over again. 
            var pSrc8bindexed = (byte*) srcData.Scan0;
            var paletteColors = new uint[srcImage.Palette.Entries.Length];
            for (var i = 0; i < srcImage.Palette.Entries.Length; i++)
            {
                paletteColors[i] = (uint) srcImage.Palette.Entries[i].ToArgb();
            }
            // now convert the pixels to uints
            for (var i = 0; i < srcImage.Height; i++)
            {
                for (var j = 0; j < srcImage.Width; j++)
                {
                    sourcePixels[(i * srcImage.Width) + j] = paletteColors[pSrc8bindexed[(i * srcImage.Width) + j]];
                }
            }
            srcImage.UnlockBits(srcData);
        }

        private static void PopulateSourcePixelsDefault(Bitmap srcImage, IList<uint> sourcePixels)
        {
            for (var i = 0; i < srcImage.Height; i++)
            {
                for (var j = 0; j < srcImage.Width; j++)
                {
                    var pixel = srcImage.GetPixel(j, i);
                    sourcePixels[(i * srcImage.Width) + j] = (uint)pixel.ToArgb();
                }
            }
        }

        private static unsafe void PopulateSourcePixelsFrom32bpp(Bitmap srcImage, IList<uint> sourcePixels)
        {
            var srcData = srcImage.LockBits(
                new Rectangle(0, 0, srcImage.Width, srcImage.Height), 
                ImageLockMode.ReadWrite,
                srcImage.PixelFormat);

            var pSrc32bpp = (uint*) srcData.Scan0;
            for (var i = 0; i < srcData.Height; i++)
            {
                for (var j = 0; j < srcData.Width; j++)
                {
                    sourcePixels[(i * srcData.Width) + j] = pSrc32bpp[(i * srcData.Width) + j];
                }
            }
            srcImage.UnlockBits(srcData);
        }

        /// <summary>
        ///     Creates a new render canvas, which will be used to render the picture in for every frame.
        /// </summary>
        private void CreateCanvas()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                return;
            }

            if (_renderCanvas != null)
            {
                _renderCanvas.Dispose();
                _renderCanvas = null;
            }

            // _renderDestination is a panel which isn't visible, and which we use to determine the position of the image we will be creating every frame.
            _canvasWidth = _renderDestination.Width;
            _canvasHeight = _renderDestination.Height;
            _renderCanvas = new Bitmap(_canvasWidth, _canvasHeight);

            var halfWidthDst = _canvasWidth/2.0;
            var halfHeightDst = _canvasHeight/2.0;
            _xDestinationCoords[0] = -halfWidthDst;
            _xDestinationCoords[1] = halfWidthDst;
            _xDestinationCoords[2] = -halfWidthDst;

            _yDestinationCoords[0] = -halfHeightDst;
            _yDestinationCoords[1] = -halfHeightDst;
            _yDestinationCoords[2] = halfHeightDst;

            _canvasSizeString = string.Format("Canvas: {0} x {1}", _canvasWidth, _canvasHeight);
        }

        /// <summary>
        ///     Handles the Tick event of the _animTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void _animTimer_Tick(object sender, EventArgs e)
        {
            if (_resetCanvas)
            {
                CreateCanvas();
                _resetCanvas = false;
            }

            Zoom();
            Rotate();
            Animate();

            Invalidate();
        }

        /// <summary>
        ///     Controls the zoom parameters, used by Animate()
        /// </summary>
        private void Zoom()
        {
            if (_zoomIn)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (_xSourceCoords[i] < 0)
                    {
                        _xSourceCoords[i] += _xZoomDelta;
                    }
                    else
                    {
                        _xSourceCoords[i] -= _xZoomDelta;
                    }
                    if (_ySourceCoords[i] < 0)
                    {
                        _ySourceCoords[i] += _yZoomDelta;
                    }
                    else
                    {
                        _ySourceCoords[i] -= _yZoomDelta;
                    }
                }
                _zoomCounter++;
                if (_zoomCounter > _zoomInMax)
                {
                    _zoomIn = false;
                }
            }
            else
            {
                for (var i = 0; i < 3; i++)
                {
                    if (_xSourceCoords[i] < 0)
                    {
                        _xSourceCoords[i] -= _xZoomDelta;
                    }
                    else
                    {
                        _xSourceCoords[i] += _xZoomDelta;
                    }
                    if (_ySourceCoords[i] < 0)
                    {
                        _ySourceCoords[i] -= _yZoomDelta;
                    }
                    else
                    {
                        _ySourceCoords[i] += _yZoomDelta;
                    }
                }
                _zoomCounter--;
                if (_zoomCounter < -_zoomOutMax)
                {
                    _zoomIn = true;
                }
            }
        }

        /// <summary>
        ///     Animates the actual image. It renders a new destination image into the rendercanvas pixelbuffer and invalidates the
        ///     form so the image gets drawn
        ///     in the paint event handler.
        /// </summary>
        private void Animate()
        {
            _timePreviousFrame = _timeCurrentFrame;
            _timeCurrentFrame = DateTime.Now;

            var xa = _xDestinationCoords[0];
            var xb = _xDestinationCoords[1];
            var xc = _xDestinationCoords[2];
            var ya = _yDestinationCoords[0];
            var yb = _yDestinationCoords[1];
            var yc = _yDestinationCoords[2];
            double canvasWidthD = _canvasWidth;
            double canvasHeightD = _canvasHeight;
            var xab_delta = (xb - xa)/canvasWidthD;
            var yab_delta = (yb - ya)/canvasWidthD;
            var xac_delta = (xc - xa)/canvasHeightD;
            var yac_delta = (yc - ya)/canvasHeightD;

            // transpose the rotating centre to the middle of the picture.
            var x_off = xa + (canvasWidthD*0.5);
            var y_off = ya + (canvasHeightD*0.5);

            // lock the bits so we can grab a 32bit pointer to the buffer which we can use to write to the buffer directly.
            var imageDst = _renderCanvas.LockBits(new Rectangle(0, 0, _canvasWidth, _canvasHeight),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            var scan0 = imageDst.Scan0;

            unsafe
            {
                // grab the destination pointer into the pixelbuffer.
                var pDest = (uint*) scan0;

                for (var i = 0; i < _canvasHeight; i++)
                {
                    for (var j = 0; j < _canvasWidth; j++)
                    {
                        // calculate for the current pixel on (j,i) in the rendercanvas the x, y for the pixel in the source image. 
                        var readX = (int) x_off;
                        var readY = (int) y_off;

                        if (readX < 0)
                        {
                            // clamp
                            readX = (_imageWidth - 1) - (-readX%(_imageWidth - 1));
                        }
                        if (readY < 0)
                        {
                            // clamp
                            readY = (_imageHeight - 1) - (-readY%(_imageHeight - 1));
                        }
                        if (readX >= _imageWidth)
                        {
                            // clamp
                            readX = 0 + (readX%(_imageWidth - 1));
                        }
                        if (readY >= _imageHeight)
                        {
                            // clamp
                            readY = 0 + (readY%(_imageHeight - 1));
                        }
                        // write the pixel
                        pDest[0] = _sourcePixels[(readY*_imageWidth) + readX];
                        // set the offsets to the new sourcepixel coordinates for the next pixel on this rasterline. Remember that these pixels
                        // are read using a rotated rectangle on the sourcepixels and the deltas for x and y are used to determine the next sourcepixel
                        // on a rotated line on the sourcepixels.
                        x_off += xab_delta;
                        y_off += yab_delta;
                        pDest++;
                    }
                    double iD = i;
                    // calculates the new start of the next line of the rendercanvas in the sourcepixel. We apply a little offset to the xac/yac deltas to get
                    // a funny rubber effect. 
                    x_off = xa + (iD*(xac_delta - 0.002*iD)) + (canvasWidthD*0.5);
                    y_off = ya + (iD*(yac_delta + 0.001*iD)) + (canvasHeightD*0.5);
                }
            }

            // done, unlock the buffer so we can render the canvas.
            _renderCanvas.UnlockBits(imageDst);
        }

        /// <summary>
        ///     Rotate the rectangle which is used to read the sourcepixels from the source image.
        /// </summary>
        public void Rotate()
        {
            // first update the angle. only gamma is interesting...
            _gamma = (_gamma + _deltaGamma)%360;
            var gammaRad = (_gamma/360)*2*Math.PI;

            var sinG = Math.Sin(gammaRad);
            var cosG = Math.Cos(gammaRad);

            // now rotate the coords...
            for (var i = 0; i < 3; i++)
            {
                var yn = _ySourceCoords[i];
                var xn = _xSourceCoords[i];

                // easy-does-it rotation using simple euler angle math. 
                _xDestinationCoords[i] = (xn*cosG) - (yn*sinG);
                _yDestinationCoords[i] = (yn*cosG) + (xn*sinG);
            }
        }

        /// <summary>
        ///     Handles the Click event of the _closeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Handles the Resize event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // set a new canvas so the destination render canvas is resized as well.
            _resetCanvas = true;
        }

        /// <summary>
        ///     Handles the Paint event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_renderCanvas == null)
            {
                // no canvas created, no drawing to be done
                return;
            }
            // draw the new frame onto the form.
            e.Graphics.DrawImageUnscaled(_renderCanvas, _renderDestination.Left, _renderDestination.Top);
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
        ///     Handles the Tick event of the _fpsTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void _fpsTimer_Tick(object sender, EventArgs e)
        {
            var millisecondsBetweenFrames = _timeCurrentFrame.Subtract(_timePreviousFrame).Milliseconds;
            if (millisecondsBetweenFrames == 0)
            {
                _fpsString = "0 frames/sec";
            }
            else
            {
                _fpsString = string.Format("{0} frames / sec", (1000.0f/millisecondsBetweenFrames).ToString(".00"));
            }
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
    }
}