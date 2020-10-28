
using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace PsdToSpine
{
    public static class LayerExtensions
    { 
        public static Bitmap GetBitmap(this IImageSource imageSource)
        {
            if (imageSource.HasImage == false)
                return null;

            byte[] data = imageSource.MergeChannels();
            var channelCount = imageSource.Channels.Length;
            var pitch = imageSource.Width * imageSource.Channels.Length;
            var w = imageSource.Width;
            var h = imageSource.Height;

            //var format = channelCount == 3 ? TextureFormat.RGB24 : TextureFormat.ARGB32;
            //var tex = new Texture2D(w, h, format, false);
            var colors = new Color[data.Length / channelCount];


            var k = 0;

            var bitmap = new System.Drawing.Bitmap(w, h);
            try
            {

                for (var y = h - 1; y >= 0; --y)
                {
                    for (var x = 0; x < pitch; x += channelCount)
                    {
                        var n = x + y * pitch;


                        var b = data[n++];
                        var g = data[n++];
                        var r = data[n++];
                        var a = channelCount == 4 ? (byte)System.Math.Round(data[n++] / 255f * imageSource.Opacity * 255f) : (byte)System.Math.Round(imageSource.Opacity * 255f);


                        var color = Color.FromArgb(a, r, g, b);

                        bitmap.SetPixel(x, y, color);
                        Console.Write('.');
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e);

            }


            return bitmap;

            //BitmapSource

            //if (channelCount == 4)
            //    return BitmapSource.Create(imageSource.Width, imageSource.Height, 96, 96, PixelFormats.Bgra32, null, data, pitch);
            //return BitmapSource.Create(imageSource.Width, imageSource.Height, 96, 96, PixelFormats.Bgr24, null, data, pitch);
        }
    }
}