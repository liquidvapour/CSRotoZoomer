using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSRotoZoomer
{
    public class PixelArrayFactory
    {
        public uint[] CreatePixelArrayFrom(Bitmap srcImage)
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
    }
}