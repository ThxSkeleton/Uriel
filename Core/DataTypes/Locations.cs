﻿using System;
using System.Collections.Generic;
using Uriel.ShaderTypes;

namespace Uriel
{
    public class LocationValidation
    {
        public static bool Enabled(int Location)
        {
            return Location >= 0;
        }

        public static void ValidateSingle(int value, string name, bool shouldThrow)
        {
            if (Enabled(value))
            {
                StaticLogger.Logger.InfoFormat("{0} is enabled with location {1}", name, value);
            }
            else
            {
                string disabledMessage = string.Format("{0} is disabled.", name);
                StaticLogger.Logger.WarnFormat("{0} is disabled.", name);
                if (shouldThrow)
                {
                    throw new InvalidOperationException(disabledMessage);
                }
            }
        }
    }

    public class VertexLocations
    {
        public int Location_Position { get; set; }

        public int Location_Texture { get; set; }

        public int Location_Color { get; set; }

        public void ValidateAllPresent(VertexFormat expectedVertexLocations)
        {
            if (expectedVertexLocations == VertexFormat.Plain)
            {
                LocationValidation.ValidateSingle(Location_Position, nameof(Location_Position), true);
            }
            else if (expectedVertexLocations == VertexFormat.WithColor)
            {
                LocationValidation.ValidateSingle(Location_Position, nameof(Location_Position), true);
                LocationValidation.ValidateSingle(Location_Color, nameof(Location_Color), true);
            }
            else if (expectedVertexLocations == VertexFormat.WithTexture)
            {
                LocationValidation.ValidateSingle(Location_Position, nameof(Location_Position), true);
                LocationValidation.ValidateSingle(Location_Texture, nameof(Location_Texture), true);
            }
            else if (expectedVertexLocations == VertexFormat.WithColorAndTexture)
            {
                LocationValidation.ValidateSingle(Location_Position, nameof(Location_Position), true);
                LocationValidation.ValidateSingle(Location_Texture, nameof(Location_Texture), true);
                LocationValidation.ValidateSingle(Location_Color, nameof(Location_Color), true);
            }
            else
            {
                throw new InvalidOperationException(expectedVertexLocations + " vertexLocations not supported.");
            }
        }
    }

    public class UniformLocations
    {
        public int Location_iMouse { get; set; }

        public int Location_iTime { get; set; }

        public int Location_iResolution { get; set; }

        public int Location_iTexture { get; set; }

        public void ValidateAllPresent(List<FragmentShaderUniformType> expectedUniforms)
        {
            foreach(var uniform in expectedUniforms)
            {
                ValidateSingle(uniform);
            }
        }

        private void ValidateSingle(FragmentShaderUniformType uniform)
        {
            if (uniform == FragmentShaderUniformType.Dimension)
            {
                LocationValidation.ValidateSingle(Location_iResolution, nameof(Location_iResolution), true);
            }
            else if (uniform == FragmentShaderUniformType.Time)
            {
                LocationValidation.ValidateSingle(Location_iTime, nameof(Location_iTime), true);
            }
            else if (uniform == FragmentShaderUniformType.Mouse)
            {
                LocationValidation.ValidateSingle(Location_iMouse, nameof(Location_iMouse), true);
            }
            else if (uniform == FragmentShaderUniformType.Texture)
            {
                LocationValidation.ValidateSingle(Location_iTexture, nameof(Location_iTexture), true);
            }

        }

    }
}
