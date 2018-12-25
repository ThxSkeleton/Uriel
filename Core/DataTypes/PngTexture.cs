using OpenGL;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace Uriel.DataTypes
{
    public class PngTexture
    {
        private readonly string filePath;
        private BitmapFrame bitmapSource;

        public PngTexture(string filePath)
        {
            this.filePath = filePath;
        }

        public void Load()
        {
            Stream imageStreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            this.bitmapSource = decoder.Frames[0];
            this.HeightInPixels = this.bitmapSource.PixelHeight;
            this.WidthInPixels = this.bitmapSource.PixelWidth;
            this.Loaded = true;
        }

        public void Create()
        {
            TextureName = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, TextureName);
            GlErrorLogger.Check();

            Gl.TextureParameter(TextureName, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GlErrorLogger.Check();
            Gl.TextureParameter(TextureName, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GlErrorLogger.Check();

            Gl.TextureParameter(TextureName, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GlErrorLogger.Check();
            Gl.TextureParameter(TextureName, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GlErrorLogger.Check();

            byte[] asBytes = BitmapSourceToArray(bitmapSource);

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(asBytes.Length);
            Marshal.Copy(asBytes, 0, unmanagedPointer, asBytes.Length);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba8, bitmapSource.PixelWidth, bitmapSource.PixelHeight, 0, PixelFormat.Bgra, PixelType.UnsignedByte, unmanagedPointer);
            GlErrorLogger.Check();
            Gl.BindTexture(TextureTarget.Texture2d, TextureName);


            GlErrorLogger.Check();
            Marshal.FreeHGlobal(unmanagedPointer);

            this.Created = true;
        }

        private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];

            bitmapSource.CopyPixels(pixels, stride, 0);

            return pixels;
        }

        public uint TextureName { get; private set; }

        public int WidthInPixels { get; private set; }

        public int HeightInPixels { get; private set; }

        public bool Loaded { get; private set; }

        public bool Created { get; private set; }
    }
}
