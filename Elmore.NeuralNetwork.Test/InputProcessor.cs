using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Elmore.NeuralNetwork.Test
{
    public class InputProcessor
    {
        private readonly string _basePath;

        public InputProcessor(string basePath)
        {
            _basePath = basePath;
        }

        public string FullPath(string filename)
        {
            return Path.Combine(_basePath, filename);
        }

        public byte[] JpgToBinaryArr(string file)
        {
            string path = FullPath(file);

            Image img = Image.FromFile(path);

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                
                return ms.ToArray();
            }
        }

        public int[] BmpToBinaryArr(string file)
        {
            string path = FullPath(file);

            var bmp = new Bitmap(path);

            var arr = new int[bmp.Width*bmp.Height];

            int i=0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    arr[i] = bmp.GetPixel(y, x).R > 100 ? 0 : 1;

                    i++;
                }
            }

            return arr;
        }
    }
}