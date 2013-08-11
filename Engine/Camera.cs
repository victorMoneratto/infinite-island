using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public class Camera
    {
        public BoundingLimits Limits = new BoundingLimits();

        private RotatableRectangleF _viewport;

        public RotatableRectangleF Viewport
        {
            get
            {
                RectangleF boundingBox = _viewport.Projection.BoundingBox;
                if (Limits.Up.HasValue && boundingBox.Up < Limits.Up.Value)
                    _viewport.Center += Vector2.UnitY*(Limits.Up.Value - boundingBox.Up);

                if (Limits.Down.HasValue && boundingBox.Down > Limits.Down.Value)
                    _viewport.Center -= Vector2.UnitY*(boundingBox.Down - Limits.Down.Value);

                if (Limits.Left.HasValue && boundingBox.Left < Limits.Left.Value)
                    _viewport.Center += Vector2.UnitX*(Limits.Left.Value - boundingBox.Left);

                if (Limits.Right.HasValue && boundingBox.Right > Limits.Right.Value)
                    _viewport.Center -= Vector2.UnitX*(boundingBox.Right - Limits.Right.Value);

                return _viewport;
            }
            set { _viewport = value; }
        }

        public void Setup(Vector2 dimensions)
        {
            Viewport = new RotatableRectangleF(dimensions);
        }

        public Vector2 PositionOnWorld(Vector2 position)
        {
            return Vector2.Transform(
                position: position,
                matrix: Matrix.Invert(CalculateTransformMatrix(Vector2.One))).ToMeters();
        }

        public Vector2 PositionOnScreen(Vector2 position)
        {
            return Vector2.Transform(
                position: position,
                matrix: CalculateTransformMatrix(Vector2.One));
        }

        /// <summary>
        ///     Calculate the resulting camera matrix for translation, rotation and scale
        /// </summary>
        /// <param name="parallax">Parallax factor, higher values result in higher velocities</param>
        /// <returns>Camera transform matrix</returns>
        public Matrix CalculateTransformMatrix(Vector2 parallax)
        {
            RectangleF boundingBox = Viewport.Projection.BoundingBox;

            return Matrix.CreateTranslation(new Vector3(-Viewport.TopLeft*parallax, 0))*
                   Matrix.CreateTranslation(new Vector3(-Viewport.Pivot, 0))*
                   Matrix.CreateRotationZ(Viewport.Rotation)*
                   Matrix.CreateScale(Viewport.Scale.X, Viewport.Scale.Y, 1)*
                   Matrix.CreateTranslation(new Vector3(Viewport.Pivot, 0));
        }
    }
}