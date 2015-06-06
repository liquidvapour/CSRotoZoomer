using System.Collections.Generic;
using System.Drawing;

namespace CSRotoZoomer
{
    public interface IMapBitmapToInt32Array
    {
        void Map(Bitmap srcImage, IList<uint> sourcePixels);
    }
}