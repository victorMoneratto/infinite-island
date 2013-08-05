using FarseerPhysics.Dynamics;
using InfiniteIsland.Components;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    public class InfiniteIsland : Game
    {
        public static readonly Camera Camera = new Camera();
        public static readonly World World = new World(new Vector2(0, 40));

        private SpriteBatch _spriteBatch;

        private Player _player;

        private Entities _entities;
        private Debug _debug;
        private Terrain _terrain;
        private Background _background;
        private Cursor _cursor;

        public InfiniteIsland()
        {
            Content.RootDirectory = "Content";
            new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720,
                    PreferMultiSampling = true,
                    SynchronizeWithVerticalRetrace = true,
                };
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _debug = new Debug(this, _spriteBatch, World);
            _player = new Player(World, Content);
            _entities = new Entities(_player);
            _terrain = new Terrain(this, World);
            _background = new Background(this);
            _cursor = new Cursor(this);

            Camera.Setup(new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height));
            Camera.Limits.Down = (TerrainChunk.VerticalPosition + TerrainChunk.Dimensions.Y).ToPixels();
            Camera.Viewport.Pivot = Camera.Viewport.Dimensions * .3f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Debug.Console.Opened)
                return;

            Input.Update();

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                Exit();

            if (Input.Keyboard.IsKeyTyped(Keys.F3))
                _debug.PhysicsDebugEnabled = !_debug.PhysicsDebugEnabled;

            if (Input.Keyboard.IsKeyDown(Keys.Up))
                Camera.Viewport.Scale += Vector2.One * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Down))
                Camera.Viewport.Scale -= Vector2.One * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Left))
                Camera.Viewport.Rotation += MathHelper.Pi * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Right))
                Camera.Viewport.Rotation -= MathHelper.Pi * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            World.Step(gameTime.ElapsedGameTime.Milliseconds * 1e-3f);
            _background.Update(gameTime);
            _terrain.Update(gameTime);
            _entities.Update(gameTime);
            _debug.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _background.Draw(_spriteBatch);
            _terrain.Draw(_spriteBatch);
            _entities.Draw(_spriteBatch);
            _debug.Draw(_spriteBatch);
            _cursor.Draw(_spriteBatch);

            base.Draw(gameTime);

        }
    }
}