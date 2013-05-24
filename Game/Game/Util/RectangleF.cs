using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Util
{
    struct RectangleF
    {
        public Vector2 Position;
        public Vector2 Dimensions;

        public RectangleF(Vector2 position, Vector2 dimensions)
        {
            this.Position = position;
            this.Dimensions = dimensions;
        }
    }
}
