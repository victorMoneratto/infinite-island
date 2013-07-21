using Microsoft.Xna.Framework;

namespace InfiniteIsland.Util
{
    internal struct RectangleF
    {
        public Vector2 Dimensions;
        public Vector2 TopLeft;

        public RectangleF(Vector2 topLeft, Vector2 dimensions)
        {
            TopLeft = topLeft;
            Dimensions = dimensions;
        }

        public Vector2 BottomRight
        {
            get { return TopLeft + Dimensions; }
        }
    }

    internal class NewRectangleF
    {
        private Vector2 _halfDimensions;
        public Vector2 Center { get; set; }

        public Vector2 Dimensions
        {
            get { return 2 * _halfDimensions; }
            set { _halfDimensions = .5f * value; }
        }

        public float Left { get { return Center.X - _halfDimensions.X; } }
        public float Right { get { return Center.X + _halfDimensions.X; } }
        public float Up { get { return Center.Y - _halfDimensions.Y; } }
        public float Down { get { return Center.Y + _halfDimensions.Y; } }

        public Vector2 TopLeft
        {
            get { return Center - _halfDimensions; }
        }

        public Vector2 BottomRight
        {
            get { return Center + _halfDimensions; }
        }
    }
}