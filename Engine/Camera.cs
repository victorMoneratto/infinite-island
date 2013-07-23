using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public class Camera
    {
        public BoundingLimits Limits = new BoundingLimits();
        public RotatableRectangleF Viewport { get; set; }

        public void Setup(Vector2 dimensions)
        {
            Viewport = new RotatableRectangleF(dimensions);
        }

        /// <summary>
        ///     Calculate the resulting camera matrix for translation, rotation and scale
        /// </summary>
        /// <param name="parallax">Parallax factor, higher values result in higher velocities</param>
        /// <returns>Camera transform matrix</returns>
        public Matrix CalculateTransformMatrix(Vector2 parallax)
        {
            RectangleF boundingBox = Viewport.Projection.BoundingBox;
            if (Limits.Up.HasValue && boundingBox.Up < Limits.Up.Value)
                Viewport.Center += Vector2.UnitY*(Limits.Up.Value - boundingBox.Up);

            if (Limits.Down.HasValue && boundingBox.Down > Limits.Down.Value)
                Viewport.Center -= Vector2.UnitY*(boundingBox.Down - Limits.Down.Value);

            if (Limits.Left.HasValue && boundingBox.Left < Limits.Left.Value)
                Viewport.Center += Vector2.UnitX*(Limits.Left.Value - boundingBox.Left);

            if (Limits.Right.HasValue && boundingBox.Right > Limits.Right.Value)
                Viewport.Center -= Vector2.UnitX*(boundingBox.Right - Limits.Right.Value);

            return Matrix.CreateTranslation(new Vector3(-Viewport.TopLeft*parallax, 0))*
                   Matrix.CreateTranslation(new Vector3(-Viewport.Pivot, 0))*
                   Matrix.CreateRotationZ(Viewport.Rotation)*
                   Matrix.CreateScale(Viewport.Scale.X, Viewport.Scale.Y, 1)*
                   Matrix.CreateTranslation(new Vector3(Viewport.Pivot, 0));
        }
    }
}