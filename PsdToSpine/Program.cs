using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace PsdToSpine
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "Fighter1.psd";
            var direcrotyName = filename.Replace('.', '-');
            PsdDocument document = PsdDocument.Create(filename);

            if (Directory.Exists(direcrotyName) ==false)
            {
                Directory.CreateDirectory(direcrotyName);
            }
            foreach(var layer in document.Childs)
            {
                var bmap = layer.GetBitmap();
                bmap.Save($"{direcrotyName}/{layer.Name}.png", ImageFormat.Png);

                Console.WriteLine($"{layer.Name}.png saved");
            }


            Console.WriteLine("Hello World!");
        }
    }
}
