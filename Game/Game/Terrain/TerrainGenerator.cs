using System;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Terrain
{
    internal class TerrainGenerator
    {
        private const int VerticesCount = 30;
        private readonly Random _random = new Random();

        public TerrainChunk Generate(GraphicsDevice graphicsDevice,
                                     Vector2 bodyPosition,
                                     float? firstVertexHeight = null,
                                     int verticesCount = VerticesCount)
        {
            float[] heights = new float[verticesCount];

            //TODO: Replace with simplex noise
            for (int i = 0; i < verticesCount; i++)
                heights[i] = (float) _random.NextDouble()/3f;

            //TODO: Design and replace it
            Vector2 dimensions = new Vector2(5000f.ToMeters(), 100f.ToMeters());

            if (firstVertexHeight.HasValue)
                heights[0] = (firstVertexHeight.Value - bodyPosition.Y)/dimensions.Y;

            return new TerrainChunk(bodyPosition, dimensions.X, dimensions.Y, heights, graphicsDevice);
        }
    }
}