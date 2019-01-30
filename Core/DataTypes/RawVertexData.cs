namespace Uriel.DataTypes
{
    public class RawVertexData
    {
        public float[] positions { get; set; }

        public uint[] indexes { get; set; }

        public float[] colors { get; set; }

        public float[] textures { get; set; }

        public static RawVertexData NonIndexed
        {
            get
            {
                return new RawVertexData()
                {
                    positions = _ArrayPosition_NonIndexed,
                    colors = _ArrayColor_NonIndexed,
                    textures = _ArrayTex_NonIndexed,
                    indexes = null
                };
            }
        }

        public static RawVertexData Indexed
        {
            get
            {
                return new RawVertexData()
                {
                    positions = _ArrayPosition_Indexed,
                    colors = _ArrayColor_Indexed,
                    textures = _ArrayTex_Indexed,
                    indexes = _ArrayIndex
                };
            }
        }


        #region Common Data

        /// <summary>
        /// Vertex position array.
        /// </summary>
        private static readonly float[] _ArrayPosition_Indexed = new float[] {
            -1.0f, -1.0f,
            1.0f, -1.0f,
            -1.0f, 1.0f,
            1.0f, 1.0f
        };

        private static readonly float[] _ArrayPosition_NonIndexed = new float[] {
            -1.0f, -1.0f,
            1.0f, -1.0f,
            -1.0f, 1.0f,

            1.0f, -1.0f,
            -1.0f, 1.0f,
            1.0f, 1.0f
        };

        private static readonly float[] _ArrayColor_Indexed = new float[] {
            0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            0.2f, 0.2f, 0.2f,
        };

        private static readonly float[] _ArrayColor_NonIndexed = new float[] {
            0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f,

            0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            0.2f, 0.2f, 0.2f,
        };


        /// <summary>
        /// texture Coordinates array
        /// </summary>
        private static readonly float[] _ArrayTex_Indexed = new float[] {
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, -1.0f,
            1.0f, -1.0f,
        };


        /// <summary>
        /// texture Coordinates array
        /// </summary>
        private static readonly float[] _ArrayTex_NonIndexed = new float[] {
            -1.0f, -1.0f,
             1.0f, -1.0f,
            -1.0f, 1.0f,

            1.0f, -1.0f,
            -1.0f, 1.0f,
            1.0f, 1.0f,
        };

        /// <summary>
        /// Vertex Index array.
        /// </summary>
        private static readonly uint[] _ArrayIndex = new uint[] {
            0, 1, 2,
            3, 2, 1,
        };

        #endregion

    }
}
