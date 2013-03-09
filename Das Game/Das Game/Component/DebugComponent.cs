using DasGame.Util;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework;

namespace DasGame.Component
{
    public class DebugComponent
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _debugProjection;

        public DebugComponent()
        {
            _debugView = new DebugViewXNA(HueGame.World);
        }

        public void LoadContent(Game game)
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