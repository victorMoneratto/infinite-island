using FarseerPhysics.Dynamics;
using InfiniteIsland.Components;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    internal class InfiniteIsland : Game
    {
        public static World World = new World(new Vector2(0, 40));
        
        public static Debug Debug;
        public static Terrain Terrain;
        public static Entities Entities;
        public static Camera Camera;
        public static Cursor Cursor;
        public static Background Background;

        public static bool IsPaused;

        private SpriteBatch _spriteBatch;
        private Texture2D _pauseFilter;

        public InfiniteIsland()
        {
            Content.RootDirectory = "Content";
            new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720,
                    PreferMultiSampling = true,
                    SynchronizeWithVerticalRetrace = true,
                    //IsFullScreen = true
                };
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Debug = new Debug(this, _spriteBatch);
            Terrain = new Terrain(GraphicsDevice);
            Entities = new Entities();
            Cursor = new Cursor();
            Background = new Background();

            Camera = new Camera();
            Camera.Setup(new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height));
            Camera.Limits.Down = (TerrainChunk.VerticalPosition + TerrainChunk.Dimensions.Y).ToPixels();
            Camera.Viewport.Pivot = Camera.Viewport.Dimensions*.3f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Entities.LoadContent(Content);
            Background.LoadContent(Content);
            Cursor.LoadContent(Content);
            Terrain.LoadContent(Content);

            _pauseFilter = new Texture2D(GraphicsDevice, 1, 1);
            _pauseFilter.SetData(new[] { new Color(0f, 0f, 0f, .4f) });

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                IsPaused = !IsPaused;

            if (IsPaused)
            {
                base.Update(gameTime);
                return;
            }

            if (Input.Keyboard.IsKeyDown(Keys.Up))
                Camera.Viewport.Scale += Vector2.One*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Down))
                Camera.Viewport.Scale -= Vector2.One*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Left))
                Camera.Viewport.Rotation += MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Right))
                Camera.Viewport.Rotation -= MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            World.Step(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            Debug.Update(gameTime);
            Entities.Update(gameTime);
            Terrain.Update(gameTime);
            Background.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            Background.Draw(_spriteBatch);
            Terrain.Draw(_spriteBatch);
            Entities.Draw(_spriteBatch);
            Debug.Draw(_spriteBatch);
            Cursor.Draw(_spriteBatch);

            if (IsPaused)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                _spriteBatch.Draw(_pauseFilter, GraphicsDevice.Viewport.Bounds, Color.White);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}