using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Component
{
    public class CameraOperator
    {
        public readonly Camera Camera;

        public CameraOperator(Rectangle viewportBounds)
        {
            Camera = new Camera();
            Camera.Setup(new Vector2(viewportBounds.Width, viewportBounds.Height));
            //Camera.Limits.Down = (TerrainChunk.VerticalPosition + TerrainChunk.Dimensions.Y).ToPixels();
        }

        public void Update(GameTime gameTime, Entities entities, float factor)
        {
            float y = MathHelper.Max(50, entities.Player.Body.Torso.Position.Y.ToPixels() - 100f);
            Camera.Viewport.Center =
                new Vector2(
                    entities.Player.Body.Torso.Position.X.ToPixels() - (2f*factor - 1)*.4f*Camera.Viewport.Dimensions.X,
                    y);

            Camera.Viewport.Pivot = new Vector2(factor, .5f);

            Vector2 mousePosition = Camera.PositionOnWorld(Input.Mouse.Position);
            float rotationFactor = (Camera.Viewport.Center.X - mousePosition.X.ToPixels())/
                                   (2*Camera.Viewport.Dimensions.X);
            Camera.Viewport.Rotation = -rotationFactor*(MathHelper.PiOver4*.25f);

            float scaleFactor = 1 - Input.Mouse.Position.Y/(Camera.Viewport.Dimensions.Y*.5f);
            Camera.Viewport.Scale = new Vector2(1 + scaleFactor*4e-2f);
        }
    }
}