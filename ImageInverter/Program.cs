using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageInverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = ExpandFilePaths(args);

            foreach (String fileName in r)
            {

                var bmp = new Bitmap(fileName);
                ApplyInvert(bmp);
                const string folder = "dark";
                Console.Write(folder);
                var i = new FileInfo(fileName);
                if (i.Directory != null)
                {
                    var di = i.Directory.CreateSubdirectory(folder);
                    var fn = di.FullName + "\\" + i.Name;
                    Console.WriteLine(fn);
                    if (File.Exists(fn))
                    {
                        File.Delete(fn);
                    }
                    bmp.Save(fn);
                }
            }

        }
        public static string[] ExpandFilePaths(string[] args)
        {
            var fileList = new List<string>();

            foreach (var arg in args)
            {
                var substitutedArg = System.Environment.ExpandEnvironmentVariables(arg);

                var dirPart = Path.GetDirectoryName(substitutedArg);
                if (dirPart.Length == 0)
                    dirPart = ".";

                var filePart = Path.GetFileName(substitutedArg);

                foreach (var filepath in Directory.GetFiles(dirPart, filePart))
                    fileList.Add(filepath);
            }

            return fileList.ToArray();
        }
        private static void ApplyInvert(Bitmap bitmapImage)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

        }
    }
}
