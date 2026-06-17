#region Namespaces
using System.IO;
using System.Windows.Media.Imaging;
#endregion

namespace RevitSpeckleExporter.Resources
{
    internal static class IconHelper
    {
        internal static BitmapSource ToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = ms;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                return img;
            }
        }
    }
}
