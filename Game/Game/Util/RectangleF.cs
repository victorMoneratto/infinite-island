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

    //internal class NewRectangle
    //{
    //    private Vector2 _halfDimension;
    //    private Vector2 _center;
    //    private float _scale;
    //    private float _rotation;

    //    public Vector2 Dimension
    //    {
    //        get { return 2 * _halfDimension; }
    //        set { _halfDimension = value * .5f; }
    //    }

    //    public Vector2 Center
    //    {
    //        get { return _center; }
    //        set { _center = value; }
    //    }

    //    public float Scale
    //    {
    //        get { return _scale; }
    //        set { _scale = value; }
    //    }

    //    public float Rotation
    //    {
    //        get { return _rotation; }
    //        set { _rotation = value; }
    //    }

    //    public float Left { get { return _center.X - _halfDimension.X; } }
    //    public float Right { get { return _center.X + _halfDimension.X; } }
    //    public float Up { get { return _center.Y - _halfDimension.Y; } }
    //    public float Down { get { return _center.Y + _halfDimension.Y; } }

    //    public Vector2 UpperLeft { get { return new Vector2(Left, Up); } }
    //    public Vector2 UpperRight { get { return new Vector2(Right, Up); } }
    //    public Vector2 LowerLeft { get { return new Vector2(Left, Down); } }
    //    public Vector2 LowerRight { get { return new Vector2(Right, Down); } }

    //    public ProjectedRectangle Projected(Matrix transformMatrix)
    //    {
    //        Matrix rectangleMatrix =
    //            Matrix.CreateRotationZ(_rotation) *
    //            Matrix.CreateScale(_scale) *
    //            transformMatrix;

    //        return new ProjectedRectangle(
    //            upperLeft: Vector2.Transform(UpperLeft, rectangleMatrix),
    //            lowerRight: Vector2.Transform(LowerRight, rectangleMatrix));
    //    }

    //    public ProjectedRectangle Projected() { return Projected(Matrix.Identity); }
    //}

    //internal struct ProjectedRectangle
    //{
    //    public readonly Vector2 UpperLeft;
    //    public readonly Vector2 UpperRight;
    //    public readonly Vector2 LowerLeft;
    //    public readonly Vector2 LowerRight;

    //    public ProjectedRectangle(Vector2 upperLeft, Vector2 lowerRight)
    //    {
    //        UpperLeft = upperLeft;
    //        UpperRight = new Vector2(lowerRight.X, upperLeft.Y);
    //        LowerLeft = new Vector2(upperLeft.X, lowerRight.Y);
    //        LowerRight = lowerRight;
    //    }
    //}
}