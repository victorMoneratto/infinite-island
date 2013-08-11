using FarseerPhysics.DebugViews;
using InfiniteIsland.Console;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNAGameConsole;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Debug : IUpdateable, IDrawable
    {
        public GameConsole Console;
        public bool PhysicsDebugEnabled;

        private Matrix _physicsProjection;
        private Matrix _physicsView;
        private readonly DebugViewXNA _physicsDebug;

        public Debug(Game game, SpriteBatch spriteBatch)
        {
            _physicsDebug = new DebugViewXNA(InfiniteIsland.World);
            _physicsDebug.LoadContent(game.GraphicsDevice, game.Content);

            Console = new GameConsole(game, spriteBatch,
                                      new GameConsoleOptions {AnimationSpeed = .2f, OpenOnWrite = false});
            Console.AddCommand(new CameraCommand());
        }

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.F3))
                PhysicsDebugEnabled = !PhysicsDebugEnabled;

            if (!PhysicsDebugEnabled) return;

            _physicsProjection = Matrix.CreateOrthographicOffCenter(
                left: 0,
                right: InfiniteIsland.Camera.Viewport.Dimensions.X.ToMeters(),
                bottom: InfiniteIsland.Camera.Viewport.Dimensions.Y.ToMeters(),
                top: 0,
                zNearPlane: 0,
                zFarPlane: 1);

            _physicsView = Matrix.CreateTranslation(new Vector3(-InfiniteIsland.Camera.Viewport.TopLeft.ToMeters(), 0))*
                           Matrix.CreateTranslation(new Vector3(-InfiniteIsland.Camera.Viewport.Pivot.ToMeters(), 0))*
                           Matrix.CreateRotationZ(InfiniteIsland.Camera.Viewport.Rotation)*
                           Matrix.CreateScale(InfiniteIsland.Camera.Viewport.Scale.X,
                                              InfiniteIsland.Camera.Viewport.Scale.Y, 1)*
                           Matrix.CreateTranslation(new Vector3(InfiniteIsland.Camera.Viewport.Pivot.ToMeters(), 0));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (PhysicsDebugEnabled)
                _physicsDebug.RenderDebugData(ref _physicsProjection, ref _physicsView);
        }
    }
}