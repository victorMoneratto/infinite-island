using Microsoft.Xna.Framework;

namespace InfiniteIsland.Util
{
    public class RectangleF
    {
        protected Vector2 HalfDimensions;

        public RectangleF(Vector2 dimensions)
        {
            Dimensions = dimensions;
            Pivot = HalfDimensions;
            Scale = Vector2.One;
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
            set { Center += Vector2.UnitY * (value + HalfDimensions.Y); }
        }

        public float Down
        {
            get { return Center.Y + HalfDimensions.Y; }
            set { Center += Vector2.UnitY * (value - HalfDimensions.Y); }
        }

        public float Left
        {
            get { return Center.X - HalfDimensions.X; }
            set { Center += Vector2.UnitX * (value + HalfDimensions.X); }
        }

        public float Right
        {
            get { return Center.X + HalfDimensions.X; }
            set { Center += Vector2.UnitX * (value - HalfDimensions.Y); }
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

    public class RotatableRectangleF : RectangleF
    {
        private float _rotation;

        public RotatableRectangleF(Vector2 dimensions)
            : base(dimensions)
        {
            Rotation = 0f;
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value % MathHelper.TwoPi; }
        }

        public new RectangleProjection Projection
        {
            get
            {
                Matrix transform =
                    Matrix.CreateTranslation(-new Vector3(TopLeft + Pivot, 0))*
                    Matrix.CreateRotationZ(-Rotation)*
                    Matrix.CreateScale(new Vector3(Vector2.One/Scale, 0))*
                    Matrix.CreateTranslation(new Vector3(TopLeft + Pivot, 0));
                return new RectangleProjection(this, transform);
            }
        }
    }

    public class RectangleProjection
    {
        public Vector2[] Vertices = new Vector2[4];

        public RectangleProjection(RectangleF rectangle, Matrix transform)
        {
            Vertices[0] = Vector2.Transform(rectangle.TopLeft, transform);
            Vertices[1] = Vector2.Transform(rectangle.TopRight, transform);
            Vertices[2] = Vector2.Transform(rectangle.BottomLeft, transform);
            Vertices[3] = Vector2.Transform(rectangle.BottomRight, transform);
        }

        public RectangleF BoundingBox
        {
            get
            {
                Vector2 topLeft = Vertices[0];
                Vector2 dimensions = Vector2.Zero;
                for (int i = 0; i < Vertices.Length; i++)
                {
                    if (Vertices[i].X < topLeft.X)
                        topLeft.X = Vertices[i].X;
                    if (Vertices[i].Y < topLeft.Y)
                        topLeft.Y = Vertices[i].Y;

                    for (int j = i + 1; j < Vertices.Length; j++)
                    {
                        float distance = MathHelper.Distance(Vertices[i].X, Vertices[j].X);
                        if (distance > dimensions.X)
                            dimensions.X = distance;

                        distance = MathHelper.Distance(Vertices[i].Y, Vertices[j].Y);
                        if (distance > dimensions.Y)
                            dimensions.Y = distance;
                    }
                }
                return new RectangleF(dimensions){TopLeft = topLeft};
            }
        }
    }
}