using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Debug : Engine.Interface.IUpdateable, Engine.Interface.IDrawable
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;

        private Viewport _viewport;

        public Debug(Game game, World world)
        {
            _debugView = new DebugViewXNA(world);
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
            _viewport = game.GraphicsDevice.Viewport;
        }

        public void Update(GameTime gameTime)
        {
            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: InfiniteIsland.Camera.Viewport.TopLeft.X.ToMeters(),
                right: (InfiniteIsland.Camera.Viewport.TopLeft.X + _viewport.Width).ToMeters(),
                bottom: (InfiniteIsland.Camera.Viewport.TopLeft.Y + _viewport.Height).ToMeters(),
                top: InfiniteIsland.Camera.Viewport.TopLeft.Y.ToMeters(),
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}