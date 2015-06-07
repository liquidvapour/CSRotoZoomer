using System.Collections.Generic;
using System.Drawing;

namespace CSRotoZoomer
{
    public class BitmapToUint32ArrayMapper
    {
        private readonly IEnumerable<BitmapLoadStrategy> _strategies;

        public BitmapToUint32ArrayMapper(IEnumerable<BitmapLoadStrategy> strategies)
        {
            _strategies = strategies;
        }

        public uint[] MapToUint32ArrayFrom(Bitmap srcImage)
        {
            var sourcePixels = new uint[srcImage.Width*srcImage.Height];

            EnumerableExtentions.FirstInXWhere(_strategies, x => x.HasPixelFormatOf(srcImage.PixelFormat))
                // read the initial pixels into the srcpixel array. This makes it possible to perform an in-place rendering to avoid memory trashing
                .UseMapperTo.Map(srcImage, sourcePixels);

            return sourcePixels;
        }
    }

}