using System;
using System.Collections.Generic;
using Uriel.DataTypes;
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

        public int Location_iCursorPosition { get; set; }
    
        public int Location_iCursorMovement { get; set; }


        public void ValidateAllPresent(List<KnownFragmentShaderUniform> expectedUniforms)
        {
            foreach(var uniform in expectedUniforms)
            {
                ValidateSingle(uniform);
            }
        }

        private void ValidateSingle(KnownFragmentShaderUniform uniform)
        {
            if (uniform == KnownFragmentShaderUniform.Dimension)
            {
                LocationValidation.ValidateSingle(Location_iResolution, nameof(Location_iResolution), false);
            }
            else if (uniform == KnownFragmentShaderUniform.Time)
            {
                LocationValidation.ValidateSingle(Location_iTime, nameof(Location_iTime), false);
            }
            else if (uniform == KnownFragmentShaderUniform.Mouse)
            {
                LocationValidation.ValidateSingle(Location_iMouse, nameof(Location_iMouse), false);
            }
            else if (uniform == KnownFragmentShaderUniform.Texture)
            {
                LocationValidation.ValidateSingle(Location_iTexture, nameof(Location_iTexture), false);
            }
            else if (uniform == KnownFragmentShaderUniform.CursorPosition)
            {
                LocationValidation.ValidateSingle(Location_iCursorPosition, nameof(Location_iCursorPosition), false);
            }
            else if (uniform == KnownFragmentShaderUniform.CursorMovement)
            {
                LocationValidation.ValidateSingle(Location_iCursorMovement, nameof(Location_iCursorMovement), false);
            }
        }

    }
}
