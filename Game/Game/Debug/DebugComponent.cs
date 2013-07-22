using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Debug
{
    public class DebugComponent : IUpdateable, IRenderable
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;

        private Viewport _viewport;

        public DebugComponent(Game game, World world)
        {
            _debugView = new DebugViewXNA(world);
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
            _viewport = game.GraphicsDevice.Viewport;
        }

        public void Update(GameTime gameTime)
        {
            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: Camera.Viewport.TopLeft.X.ToMeters(),
                right: (Camera.Viewport.TopLeft.X + _viewport.Width).ToMeters(),
                bottom: (Camera.Viewport.TopLeft.Y + _viewport.Height).ToMeters(),
                top: Camera.Viewport.TopLeft.Y.ToMeters(),
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}