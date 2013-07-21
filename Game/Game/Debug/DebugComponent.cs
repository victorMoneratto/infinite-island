using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNAGameConsole;

namespace InfiniteIsland.Debug
{
    public class DebugComponent : IUpdateable, IRenderable
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;
        
        //public static? How ugly.
        public static GameConsole Console;

        private Viewport _viewport;

        public DebugComponent(Game game, World world, SpriteBatch spriteBatch)
        {
            //Console = new GameConsole(game, spriteBatch);
            _debugView = new DebugViewXNA(world);
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
            _viewport = game.GraphicsDevice.Viewport;
        }

        public void Update(GameTime gameTime)
        {
            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: Camera.Position.X.ToMeters(),
                right: (Camera.Position.X + _viewport.Width).ToMeters(),
                bottom: (Camera.Position.Y + _viewport.Height).ToMeters(),
                top: Camera.Position.Y.ToMeters(),
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}