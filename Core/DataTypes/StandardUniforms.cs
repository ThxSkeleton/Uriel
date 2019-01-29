namespace Uriel
{
    public class ShaderLocations
    {
        public int LocationPosition { get; set; }

        public int Location_u_time { get; set; }

        public int Location_resolution { get; set; }

        public int LocationTexture { get; set; }

        public int LocationColor { get; set; }

        public static bool Enabled(int Location)
        {
            return Location >= 0;
        }

        public void Validate()
        {
            ValidateSingle(LocationPosition, nameof(LocationPosition));
            ValidateSingle(Location_u_time, nameof(Location_u_time));
            ValidateSingle(Location_resolution, nameof(Location_resolution));
            ValidateSingle(LocationTexture, nameof(LocationTexture));
            ValidateSingle(LocationColor, nameof(LocationColor));
        }

        private void ValidateSingle(int value, string name)
        {
            if (Enabled(value))
            {
                StaticLogger.Logger.InfoFormat("{0} is enabled with location {1}", name, value);
            }
            else
            {
                StaticLogger.Logger.WarnFormat("{0} is disabled.", name);
            }
        }

    }
}
