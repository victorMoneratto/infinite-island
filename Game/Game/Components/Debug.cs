using FarseerPhysics;
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
        private readonly DebugViewXNA _physicsDebug;
        public GameConsole Console;
        public bool PhysicsDebugEnabled;

        public Debug(Game game, SpriteBatch spriteBatch)
        {
            _physicsDebug = new DebugViewXNA(InfiniteIsland.World);
            _physicsDebug.LoadContent(game.GraphicsDevice, game.Content);
            _physicsDebug.AppendFlags(DebugViewFlags.DebugPanel);

            Console = new GameConsole(
                game,
                spriteBatch,
                new[] {new CameraCommand()},
                new GameConsoleOptions
                    {
                        AnimationSpeed = .2f,
                        OpenOnWrite = false
                    });
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!PhysicsDebugEnabled) return;

            Matrix physicsProjection = Matrix.CreateOrthographicOffCenter(
                left: 0,
                right: camera.Viewport.Dimensions.X.ToMeters(),
                bottom: camera.Viewport.Dimensions.Y.ToMeters(),
                top: 0,
                zNearPlane: 0,
                zFarPlane: 1);

            Matrix physicsView =
                Matrix.CreateTranslation(new Vector3(-camera.Viewport.TopLeft.ToMeters(), 0))*
                Matrix.CreateTranslation(new Vector3(-camera.Viewport.Pivot.ToMeters(), 0))*
                Matrix.CreateRotationZ(camera.Viewport.Rotation)*
                Matrix.CreateScale(camera.Viewport.Scale.X, camera.Viewport.Scale.Y, 1)*
                Matrix.CreateTranslation(new Vector3(camera.Viewport.Pivot.ToMeters(), 0));

            _physicsDebug.RenderDebugData(ref physicsProjection, ref physicsView);
        }

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.F3))
                PhysicsDebugEnabled = !PhysicsDebugEnabled;
        }
    }
}