namespace Uriel
{
    public interface IShaderProgram
    {
        uint ProgramName { get; }
        UniformLocations UniformLocations { get; }
        VertexLocations VertexLocations { get; }
    }
}