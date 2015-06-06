using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSRotoZoomer
{
    public class Bitmap8bppToUint32ArrayMapper
    {
        public unsafe void PopulateSourcePixelsFrom8bpp(Bitmap srcImage, IList<uint> sourcePixels)
        {
            var srcData = srcImage.LockBits(
                new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                ImageLockMode.ReadWrite,
                srcImage.PixelFormat);

            // first convert the palet to uint's (ARGB). This is an operation which needs to be done once, so we don't convert
            // the same color over and over again. 
            var paletteColors = new uint[srcImage.Palette.Entries.Length];
            for (var i = 0; i < srcImage.Palette.Entries.Length; i++)
            {
                paletteColors[i] = (uint) srcImage.Palette.Entries[i].ToArgb();
            }
            // now convert the pixels to uints
            var pSrc8bindexed = (byte*)srcData.Scan0;
            for (var i = 0; i < srcImage.Height; i++)
            {
                for (var j = 0; j < srcImage.Width; j++)
                {
                    sourcePixels[(i * srcImage.Width) + j] = paletteColors[pSrc8bindexed[(i * srcImage.Width) + j]];
                }
            }
            srcImage.UnlockBits(srcData);
        }
    }
}