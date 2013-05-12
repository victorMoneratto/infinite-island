using FarseerPhysics.DebugViews;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Visual
{
    public class DebugComponent
    {
        private readonly DebugViewXNA _debugView;

        public DebugComponent()
        {
            _debugView = new DebugViewXNA(InfiniteIsland.World);
        }
        private Viewport _viewport;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
            _viewport = game.GraphicsDevice.Viewport;
        }

        private Matrix _debugProjection;

        public void Update()
        {
            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: MeasureUtil.ToMeters(InfiniteIsland.Camera.Position.X),
                right: MeasureUtil.ToMeters(InfiniteIsland.Camera.Position.X + _viewport.Width),
                bottom: MeasureUtil.ToMeters(InfiniteIsland.Camera.Position.Y + _viewport.Height),
                top: MeasureUtil.ToMeters(InfiniteIsland.Camera.Position.Y),
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw()
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}