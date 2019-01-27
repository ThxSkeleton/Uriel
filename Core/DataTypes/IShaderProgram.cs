namespace Uriel
{
    public interface IShaderProgram
    {
        uint ProgramName { get; }

        ShaderLocations StandardUniforms { get; }
    }
}