using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Math.Geometry
{
    public class RectangleF
    {
        protected Vector2 HalfDimensions;

        public RectangleF(Vector2 dimensions)
        {
            Dimensions = dimensions;
            Scale = Vector2.One;
            Pivot = HalfDimensions;
        }

        public Vector2 Center { get; set; }

        public Vector2 Pivot { get; set; }

        public Vector2 Scale { get; set; }

        public Vector2 Dimensions
        {
            get { return 2*HalfDimensions; }
            set { HalfDimensions = .5f*value; }
        }

        public Vector2 TopLeft
        {
            get { return Center - HalfDimensions; }
            set { Center = value + HalfDimensions; }
        }

        public Vector2 TopRight
        {
            get { return Center + new Vector2(HalfDimensions.X, -HalfDimensions.Y); }
            set { Center = value - new Vector2(HalfDimensions.X, -HalfDimensions.Y); }
        }

        public Vector2 BottomLeft
        {
            get { return Center + new Vector2(-HalfDimensions.X, HalfDimensions.Y); }
            set { Center = value - new Vector2(-HalfDimensions.X, HalfDimensions.Y); }
        }

        public Vector2 BottomRight
        {
            get { return Center + HalfDimensions; }
            set { Center = value - HalfDimensions; }
        }

        public float Up
        {
            get { return Center.Y - HalfDimensions.Y; }
            set { Center += Vector2.UnitY*(value + HalfDimensions.Y); }
        }

        public float Down
        {
            get { return Center.Y + HalfDimensions.Y; }
            set { Center += Vector2.UnitY*(value - HalfDimensions.Y); }
        }

        public float Left
        {
            get { return Center.X - HalfDimensions.X; }
            set { Center += Vector2.UnitX*(value + HalfDimensions.X); }
        }

        public float Right
        {
            get { return Center.X + HalfDimensions.X; }
            set { Center += Vector2.UnitX*(value - HalfDimensions.Y); }
        }

        public RectangleProjection Projection
        {
            get
            {
                Matrix transform =
                    Matrix.CreateTranslation(-new Vector3(TopLeft + Pivot, 0))*
                    Matrix.CreateScale(new Vector3(Scale, 0))*
                    Matrix.CreateTranslation(new Vector3(TopLeft + Pivot, 0));
                return new RectangleProjection(this, transform);
            }
        }
    }
}