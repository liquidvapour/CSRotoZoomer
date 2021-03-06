using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSRotoZoomer
{
    public interface IRotoZoomer
    {
        double DeltaGamma { get; set; }
        int ZoomCounter { get; }
        int ZoomInMax { get; set; }
        int ZoomOutMax { get; set; }
        bool ZoomIn { get; }
        double XZoomDelta { get; set; }
        double YZoomDelta { get; }

        /// <summary>
        ///     Initializes the roto zoomer.
        /// </summary>
        void Initialize(Bitmap srcImage);

        void Update(double deltaTimeInSeconds);

        /// <summary>
        ///     Rotate the rectangle which is used to read the sourcepixels from the source image.
        /// </summary>
        void Rotate(double dt);

        void RenderUsing(Graphics graphics);
        void ResizeCanvas(Rectangle clientRectangle);
    }

    public class RotoZoomer : IRotoZoomer
    {
        private int _canvasWidth, _canvasHeight;
        private Bitmap _renderCanvas;
        private double _gamma;
        private int _imageWidth, _imageHeight;

        // coordinate source.
        private readonly double[] _xSourceCoords = { -128, 128, -128 };
        private readonly double[] _ySourceCoords = { -64, -64, 64 };

        // coordinate destination. (for drawing).
        private readonly double[] _xDestinationCoords = { -128, 128, -128 };
        private readonly double[] _yDestinationCoords = { -64, -64, 64 };

        private uint[] _sourcePixels;
        private readonly IMap<Bitmap, uint[]> _bitmapToUint32ArrayMapper;
        private Rectangle _renderDestination;
        private bool _resetCanvas;

        public double DeltaGamma { get; set; }
        public int ZoomCounter { get; private set; }
        public int ZoomInMax { get; set; }
        public int ZoomOutMax { get; set; }
        public bool ZoomIn { get; private set; }
        public double XZoomDelta { get; set; }
        public double YZoomDelta { get; private set; }

        /// <summary>
        ///     CTor
        /// </summary>
        /// <param name="bitmapToUint32ArrayMapper"></param>
        public RotoZoomer(IMap<Bitmap, uint[]> bitmapToUint32ArrayMapper)
        {
            _bitmapToUint32ArrayMapper = bitmapToUint32ArrayMapper;

            // deltaGamma value, controls rotation.
            DeltaGamma = 60.0;
            // initialize our parameters.
            ZoomCounter = 0;
            ZoomIn = false;


            // deltas for x and y zooming. If they're not the same the image gets stretched.
            XZoomDelta = 2.0;
            YZoomDelta = 1.0;

            // zoom counter values when the zoom action (in/out) will swap. Give this different values for wicked results. 
            ZoomInMax = 200;
            ZoomOutMax = 300;
        }

        public void Initialize(Bitmap srcImage)
        {
            CreateCanvas();

            _imageWidth = srcImage.Width;
            _imageHeight = srcImage.Height;

            if ((_imageWidth == 0) || (_imageHeight == 0))
            {
                throw new ApplicationException("The image you selected has a width and/or height of 0 and isn't usable");
            }

            double imageWidthD = _imageWidth;
            double imageHeightD = _imageHeight;
            if (XZoomDelta != 0)
            {
                ZoomInMax = (int) (((imageWidthD/2.0)/XZoomDelta) - 10.0);
            }
            ZoomOutMax = ZoomInMax*5;
            YZoomDelta = (imageHeightD/imageWidthD)*XZoomDelta;

            _sourcePixels = _bitmapToUint32ArrayMapper.MapToUint32ArrayFrom(srcImage);

            InitializeCoordArrays(imageWidthD, imageHeightD);

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

        /// <summary>
        ///     Creates a new render canvas, which will be used to render the picture in for every frame.
        /// </summary>
        private void CreateCanvas()
        {
            if (_renderDestination == Rectangle.Empty) throw new InvalidOperationException("Please call ResizeCanval before Initializing.");

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

            
        }


        public void Update(double deltaTimeInSeconds)
        {
            if (_resetCanvas)
            {
                CreateCanvas();
                _resetCanvas = false;
            }

            Zoom(deltaTimeInSeconds);
            Rotate(deltaTimeInSeconds);
            Animate();
        }

        /// <summary>
        ///     Controls the zoom parameters, used by Animate()
        /// </summary>
        /// <param name="deltaTimeInSeconds"></param>
        private void Zoom(double deltaTimeInSeconds)
        {
            var xZoomDeltaThisFrame = XZoomDelta*deltaTimeInSeconds;
            var yZoomDeltaThisFrame = YZoomDelta*deltaTimeInSeconds;
            if (ZoomIn)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (_xSourceCoords[i] < 0)
                    {
                        _xSourceCoords[i] += xZoomDeltaThisFrame;
                    }
                    else
                    {
                        _xSourceCoords[i] -= xZoomDeltaThisFrame;
                    }
                    if (_ySourceCoords[i] < 0)
                    {
                        _ySourceCoords[i] += yZoomDeltaThisFrame;
                    }
                    else
                    {
                        _ySourceCoords[i] -= yZoomDeltaThisFrame;
                    }
                }
                ZoomCounter++;
                if (ZoomCounter > ZoomInMax)
                {
                    ZoomIn = false;
                }
            }
            else
            {
                for (var i = 0; i < 3; i++)
                {
                    if (_xSourceCoords[i] < 0)
                    {
                        _xSourceCoords[i] -= xZoomDeltaThisFrame;
                    }
                    else
                    {
                        _xSourceCoords[i] += xZoomDeltaThisFrame;
                    }
                    if (_ySourceCoords[i] < 0)
                    {
                        _ySourceCoords[i] -= yZoomDeltaThisFrame;
                    }
                    else
                    {
                        _ySourceCoords[i] += yZoomDeltaThisFrame;
                    }
                }
                ZoomCounter--;
                if (ZoomCounter < -ZoomOutMax)
                {
                    ZoomIn = true;
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
        public void Rotate(double dt)
        {
            Debug.Print("deltaTimeInSeconds: {0}", dt);
            // first update the angle. only gamma is interesting...
            _gamma = (_gamma + (DeltaGamma * dt))%360;
            Debug.Print("new gamma: {0}", _gamma);
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

        public void RenderUsing(Graphics graphics)
        {
            if (_renderCanvas == null)
            {
                // no canvas created, no drawing to be done
                return;
            }

            graphics.DrawImageUnscaled(_renderCanvas, _renderDestination.Left, _renderDestination.Top);

        }

        public void ResizeCanvas(Rectangle clientRectangle)
        {
            _resetCanvas = true;
            _renderDestination = clientRectangle;
        }
    }
}