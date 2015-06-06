using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSRotoZoomer.BitmapMappers
{
    public class Bitmap32BppToUint32ArrayMapper : IMapBitmapToInt32Array
    {
        public unsafe void Map(Bitmap srcImage, IList<uint> sourcePixels)
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
    }
}