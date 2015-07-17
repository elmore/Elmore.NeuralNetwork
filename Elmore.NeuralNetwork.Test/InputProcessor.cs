using System;
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

        //public int[] BmpToBinaryArr(string file)
        //{
        //    byte[] arr = BmpToArr(file);

        //    var intArr = new int[arr.Length];

        //    for(var i=0; i<arr.Length; i++)
        //    {
        //        intArr[i] = arr[i] > 100 ? 1 : 0;
        //    }

        //    return intArr;
        //}

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

        //public byte[] BmpToArr(string file)
        //{
        //    string path = FullPath(file);

        //    var bmp = new Bitmap(path);

        //    var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);

        //    // Get the address of the first line.
        //    IntPtr ptr = bmpData.Scan0;

        //    // if the stride is bigger than the width i get an array thats too big
        //    //bmpData.Stride = bmp.Width;

        //    // Declare an array to hold the bytes of the bitmap.
        //    //int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
        //    int bytes = bmp.Width * bmp.Height;
        //    byte[] rgbValues = new byte[bytes];

        //    // Copy the RGB values into the array.
        //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        //    // Unlock the bits.
        //    bmp.UnlockBits(bmpData);

        //    return rgbValues;
        //}
    }
}