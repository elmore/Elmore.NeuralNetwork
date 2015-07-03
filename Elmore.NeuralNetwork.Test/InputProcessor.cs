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
    }
}