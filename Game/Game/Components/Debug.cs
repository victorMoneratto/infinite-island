using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InfiniteIsland.Engine.Math;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Debug : IUpdateable, IDrawable
    {
        private readonly DebugViewXNA _debugView;
        private Matrix _projection;
        private Matrix _view;

        public Debug(Game game, World world)
        {
            _debugView = new DebugViewXNA(world);
            _debugView.LoadContent(game.GraphicsDevice, game.Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _debugView.RenderDebugData(ref _projection, ref _view);
        }

        public void Update(GameTime gameTime)
        {
            _projection = Matrix.CreateOrthographicOffCenter(
                left: 0,
                right: InfiniteIsland.Camera.Viewport.Dimensions.X.ToMeters(),
                bottom: InfiniteIsland.Camera.Viewport.Dimensions.Y.ToMeters(),
                top: 0,
                zNearPlane: 0,
                zFarPlane: 1);

            _view = Matrix.CreateTranslation(new Vector3(-InfiniteIsland.Camera.Viewport.TopLeft.ToMeters(), 0)) *
                   Matrix.CreateTranslation(new Vector3(-InfiniteIsland.Camera.Viewport.Pivot.ToMeters(), 0)) *
                   Matrix.CreateRotationZ(InfiniteIsland.Camera.Viewport.Rotation) *
                   Matrix.CreateScale(InfiniteIsland.Camera.Viewport.Scale.X, InfiniteIsland.Camera.Viewport.Scale.Y, 1) *
                   Matrix.CreateTranslation(new Vector3(InfiniteIsland.Camera.Viewport.Pivot.ToMeters(), 0));
        }
    }
}