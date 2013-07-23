using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Math.Geometry
{
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
}