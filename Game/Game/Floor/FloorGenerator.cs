using System;

namespace InfiniteIsland.Game.Floor
{
    internal class FloorGenerator
    {
        private const int VerticesCount = 100;

        public float[] Generate(int verticesCount = VerticesCount)
        {
            Random random = new Random();
            float[] heights = new float[verticesCount];

            //TODO Replace with simplex noise
            for (int i = 0; i < verticesCount; i++)
                heights[i] = (float) random.NextDouble()/3f;

            return heights;
        }
    }
}