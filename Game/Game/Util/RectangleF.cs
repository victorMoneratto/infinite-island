using Microsoft.Xna.Framework;

namespace InfiniteIsland.Util
{
    internal class RectangleF
    {

        public RectangleF(Vector2 dimensions)
        {
            Dimensions = dimensions;
        }
        
        public Vector2 Center { get; set; }

        private Vector2 _halfDimensions;
        public Vector2 Dimensions
        {
            get { return 2*_halfDimensions; }
            set { _halfDimensions = .5f*value; }
        }

        public Vector2 TopLeft
        {
            get { return Center - _halfDimensions; }
            set { Center = value + _halfDimensions; }
        }

        public Vector2 BottomRight
        {
            get { return Center + _halfDimensions; }
            set { Center = value - _halfDimensions; }
        }
    }
}