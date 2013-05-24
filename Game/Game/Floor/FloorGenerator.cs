namespace InfiniteIsland.Game.Floor
{
    internal class FloorGenerator
    {
        private const int VerticesCount = 400;

        public float[] Generate(int verticesCount = VerticesCount)
        {
            System.Random random = new System.Random();
            var heights = new float[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                heights[i] = (float) random.NextDouble() * .1f;
            }

            return heights;
        }
    }
}