using FarseerPhysics.DebugViews;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Visual
{
    public class DebugComponent
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;

        private Viewport _viewport;

        public DebugComponent()
        {
            _debugView = new DebugViewXNA(InfiniteIsland.World);
        }

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
            _viewport = game.GraphicsDevice.Viewport;
        }

        public void Update()
        {
            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: InfiniteIsland.Camera.Position.X.ToMeters(),
                right: (InfiniteIsland.Camera.Position.X + _viewport.Width).ToMeters(),
                bottom: (InfiniteIsland.Camera.Position.Y + _viewport.Height).ToMeters(),
                top: InfiniteIsland.Camera.Position.Y.ToMeters(),
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw()
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}