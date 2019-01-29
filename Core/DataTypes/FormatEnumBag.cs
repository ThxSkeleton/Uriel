using OpenGL;
using System;
using System.Collections.Generic;

namespace Uriel.DataTypes
{
    public class FormatEnumBag
    {
        public InternalFormat InternalFormat { get; set; }

        public OpenGL.PixelFormat PixelFormat { get; set; }

        public OpenGL.PixelType PixelType { get; set; }

        public string DisplayString()
        {
            return String.Format("{0}_{1}_{2}", InternalFormat, PixelFormat, PixelType);
        }
    }

}
