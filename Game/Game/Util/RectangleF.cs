using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Util
{
    internal struct RectangleF
    {
        public readonly Vector2 Dimensions;
        public readonly Vector2 TopLeft;

        public RectangleF(Vector2 topLeft, Vector2 dimensions)
        {
            TopLeft = topLeft;
            Dimensions = dimensions;
        }
    }
}