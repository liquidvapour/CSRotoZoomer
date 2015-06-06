using System.Drawing;
using System.Drawing.Imaging;

namespace CSRotoZoomer
{
    public class BitmapToUint32ArrayMapper
    {
        private readonly Bitmap8bppToUint32ArrayMapper _bitmap8BppToUint32ArrayMapper = new Bitmap8bppToUint32ArrayMapper();
        private readonly Bitmap32BppToUint32ArrayMapper _bitmap32BppToUint32ArrayMapper = new Bitmap32BppToUint32ArrayMapper();
        private readonly DefaultBitmapToUint32Mapper _defaultBitmapToUint32Mapper = new DefaultBitmapToUint32Mapper();

        public uint[] MapToUint32ArrayFrom(Bitmap srcImage)
        {
            var sourcePixels = new uint[srcImage.Width*srcImage.Height];
            // read the initial pixels into the srcpixel array. This makes it possible to perform an in-place rendering to avoid memory trashing
            switch (srcImage.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    _bitmap32BppToUint32ArrayMapper.PopulateSourcePixelsFrom32bpp(srcImage, sourcePixels);
                    break;
                case PixelFormat.Format8bppIndexed:
                    _bitmap8BppToUint32ArrayMapper.PopulateSourcePixelsFrom8bpp(srcImage, sourcePixels);
                    break;
                default:
                    _defaultBitmapToUint32Mapper.PopulateSourcePixelsDefault(srcImage, sourcePixels);
                    break;
            }
            return sourcePixels;
        }
    }
}