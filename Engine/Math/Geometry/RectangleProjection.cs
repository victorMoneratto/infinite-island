using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Math.Geometry
{
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
                return new RectangleF(dimensions) {TopLeft = topLeft};
            }
        }
    }
}