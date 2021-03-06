using System;

namespace InfiniteIsland.Engine.Math
{
    public static class Noise
    {
        private static readonly Random Random = new Random();

        public static float[] Generate(int count)
        {
            var values = new float[count];

            //TODO: Replace with simplex noise
            for (int i = 0; i < count; i++)
                values[i] = (float) Random.NextDouble()/3f;

            return values;
        }
    }
}