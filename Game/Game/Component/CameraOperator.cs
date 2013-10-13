using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Component
{
    public class CameraOperator
    {
        public Camera Camera;

        public CameraOperator(Rectangle viewportBounds)
        {
            Camera = new Camera();
            Camera.Setup(new Vector2(viewportBounds.Width, viewportBounds.Height));
            Camera.Limits.Down = (TerrainChunk.VerticalPosition + TerrainChunk.Dimensions.Y).ToPixels();
        }

        public void Update(GameTime gameTime, Entities entities, float factor)
        {
            Camera.Viewport.Center =
                entities.Player.Body.Torso.Position.ToPixels() -
                (2f*factor - 1)*new Vector2(.4f*Camera.Viewport.Dimensions.X, 1f);

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