using System.Drawing.Imaging;

namespace CSRotoZoomer.BitmapMappers
{
    public class BitmapLoadStrategy
    {
        public Func<PixelFormat, bool> HasPixelFormatOf { get; set; }
        public IMapBitmapToInt32Array UseMapperTo { get; set; }
    }
}