using FarseerPhysics.DebugViews;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Component
{
    public class DebugComponent
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;

        public DebugComponent()
        {
            _debugView = new DebugViewXNA(InfiniteIsland.World);
        }

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _debugView.LoadContent(game.GraphicsDevice, game.Content);

            _debugProjection = Matrix.CreateOrthographicOffCenter(
                left: 0,
                right: MeasureUtil.ToMeters(game.GraphicsDevice.Viewport.Width),
                bottom: MeasureUtil.ToMeters(game.GraphicsDevice.Viewport.Height),
                top: 0,
                zNearPlane: 0,
                zFarPlane: 1);
        }

        public void Draw()
        {
            _debugView.RenderDebugData(ref _debugProjection);
        }
    }
}