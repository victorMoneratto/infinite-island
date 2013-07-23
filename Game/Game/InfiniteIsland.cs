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
        private readonly World _world = new World(new Vector2(0, 40));
        
        private SpriteBatch _spriteBatch;

        private Player _player;

        private Entities _entities;
        private Debug _debug;
        private Terrain _terrain;
        private Background _background;

#if DEBUG
        private bool _debugEnabled = true;
#else
        private bool _debugEnabled;
#endif

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

            _debug = new Debug(this, _world);
            _player = new Player(_world, Content);
            _entities = new Entities(_player);
            _background = new Background(this);
            _terrain = new Terrain(this, _world);

            Camera.Setup(new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height));
            Camera.Limits.Down = (TerrainChunk.VerticalPosition + TerrainChunk.Dimensions.Y).ToPixels();
            Camera.Viewport.Pivot = Camera.Viewport.Dimensions*.3f;
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                Exit();

            if (Input.Keyboard.IsKeyTyped(Keys.F3))
                _debugEnabled = !_debugEnabled;

            if (Input.Keyboard.IsKeyDown(Keys.Up))
                Camera.Viewport.Scale += Vector2.One * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Down))
                Camera.Viewport.Scale -= Vector2.One * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Left))
                Camera.Viewport.Rotation += MathHelper.Pi * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Right))
                Camera.Viewport.Rotation -= MathHelper.Pi * (gameTime.ElapsedGameTime.Milliseconds * 1e-3f);

            _world.Step(gameTime.ElapsedGameTime.Milliseconds*1e-3f);
            _background.Update(gameTime);
            _terrain.Update(gameTime);
            _entities.Update(gameTime);
            _debug.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _background.Draw(_spriteBatch);
            _terrain.Draw(_spriteBatch);
            _entities.Draw(_spriteBatch);

            if (_debugEnabled)
                _debug.Draw(_spriteBatch);
            
        }
    }
}