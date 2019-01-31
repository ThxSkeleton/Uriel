using OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using PixelFormat = OpenGL.PixelFormat;

namespace Uriel.DataTypes
{
    public class PngTexture
    {
        private readonly string filePath;

        private Bitmap bitmap;
        private PixelFormat format;

        public PngTexture(string filePath)
        {
            this.filePath = filePath;
        }

        public void Load()
        {
            Stream imageStreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            var bitmapSource = decoder.Frames[0];
            var bitsPerPixel = bitmapSource.Format.BitsPerPixel;
            this.format = BppToPixelFormat(bitsPerPixel);
            this.bitmap = BitmapFromSource(bitmapSource);
            this.HeightInPixels = bitmapSource.PixelHeight;
            this.WidthInPixels = bitmapSource.PixelWidth;
            this.Loaded = true;
        }

        private PixelFormat BppToPixelFormat(int bpp)
        {
            if (bpp == 24)
            {
                return PixelFormat.Bgr;
            }
            else if (bpp == 32)
            {
                return PixelFormat.Bgra;
            }
            else
            {
                throw new InvalidOperationException(String.Format("Cannot Convert BPP {0} from {1} into a PixelFormat.", bpp, this.filePath));
            }
        }


        public void Create()
        {
            TextureName = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, TextureName);
            GlErrorLogger.Check();

            //Gl.TextureParameter(TextureName, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            //GlErrorLogger.Check();
            //Gl.TextureParameter(TextureName, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            //GlErrorLogger.Check();

            //Gl.TextureParameter(TextureName, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            //GlErrorLogger.Check();
            //Gl.TextureParameter(TextureName, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            //GlErrorLogger.Check();

            GlErrorLogger.Check();
            Gl.BindTexture(TextureTarget.Texture2d, TextureName);

            BitmapData data = null;

            try
            {
                data = bitmap.LockBits(new Rectangle(0, 0, WidthInPixels, HeightInPixels), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
                Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, WidthInPixels, HeightInPixels, 0, this.format, PixelType.UnsignedByte, data.Scan0);
            }
            finally
            {
                if (data != null)
                {
                    bitmap.UnlockBits(data);
                }
            }

            GlErrorLogger.Check();

            Gl.GenerateMipmap(TextureTarget.Texture2d);

            this.Created = true;
        }

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        public uint TextureName { get; private set; }

        public int WidthInPixels { get; private set; }

        public int HeightInPixels { get; private set; }

        public bool Loaded { get; private set; }

        public bool Created { get; private set; }
    }
}
