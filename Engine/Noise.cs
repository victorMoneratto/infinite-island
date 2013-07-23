using System;

namespace Engine
{
    public static class Noise
    {
        private static readonly Random Random = new Random();

        public static float[] Generate(int count)
        {
            float[] values = new float[count];

            //TODO: Replace with simplex noise
            for (int i = 0; i < count; i++)
                values[i] = (float) Random.NextDouble()/3f;

            return values;
        }
    }
}
