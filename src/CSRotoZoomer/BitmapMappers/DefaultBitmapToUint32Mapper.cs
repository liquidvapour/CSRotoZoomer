using System.Collections.Generic;
using System.Drawing;

namespace CSRotoZoomer.BitmapMappers
{
    public class DefaultBitmapToUint32Mapper : IMapBitmapToInt32Array
    {
        public void Map(Bitmap srcImage, IList<uint> sourcePixels)
        {
            // use slow getpixel method. This is slow because GetPixel is very slow and also the on-the-fly ARGB conversion. 
            for (var i = 0; i < srcImage.Height; i++)
            {
                for (var j = 0; j < srcImage.Width; j++)
                {
                    var pixel = srcImage.GetPixel(j, i);
                    sourcePixels[(i * srcImage.Width) + j] = (uint)pixel.ToArgb();
                }
            }
        }
    }
}