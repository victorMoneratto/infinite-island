using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Console;
using InfiniteIsland.Engine.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameConsole;

namespace InfiniteIsland.Components
{
    public class Debug : Engine.IUpdateable, Engine.IDrawable
    {
        //static but needs initialization, yeah, I feel bad about it.
        public static GameConsole Console;
        private readonly DebugViewXNA _physicsDebug;
        private Matrix _physicsProjection;
        private Matrix _physicsView;

        public Debug(Game game, SpriteBatch spriteBatch, World world)
        {
            _physicsDebug = new DebugViewXNA(world);
            _physicsDebug.LoadContent(game.GraphicsDevice, game.Content);

            Console = new GameConsole(game, spriteBatch, new GameConsoleOptions {AnimationSpeed = .2f});
            Console.AddCommand(new CameraCommand());
        }

        public bool PhysicsDebugEnabled { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (PhysicsDebugEnabled)
                _physicsDebug.RenderDebugData(ref _physicsProjection, ref _physicsView);
        }

        public void Update(GameTime gameTime)
        {
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
    }
}