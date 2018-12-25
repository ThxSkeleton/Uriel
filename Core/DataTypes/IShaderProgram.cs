namespace Uriel
{
    public interface IShaderProgram
    {
        uint ProgramName { get; }

        StandardUniforms StandardUniforms { get; }
    }
}