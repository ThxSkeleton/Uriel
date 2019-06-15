namespace Uriel.DataTypes
{
    public class ShaderVersionUtilites
    {
        public static string VersionTag(ShaderVersion vertexShaderVersion)
        {
            if (vertexShaderVersion == ShaderVersion.Version150Compatability)
            {
                return "#version 150 compatibility\n";
            }
            else if (vertexShaderVersion == ShaderVersion.Version300Core)
            {
                return "#version 300 core\n";
            }
            else
            {
                throw new System.InvalidOperationException("Unsupported Shader Type.");
            }
        }
    }

    public enum ShaderVersion
    {
        Version150Compatability,
        Version300Core
    }
}
