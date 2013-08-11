using System;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Components;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    internal class InfiniteIsland : Game
    {
        public static World World = new World(new Vector2(0, 40));
        public static Random Random = new Random();

        public static Debug Debug;
        public static Terrain Terrain;
        public static Entities Entities;
        public static CameraOperator CameraOperator;
        public static Cursor Cursor;
        public static Background Background;

        public static float Factor = 1f;
        public static bool IsPaused;

        private Texture2D _pauseFilter;
        private SpriteBatch _spriteBatch;

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

            Input.Mouse.Limits = new BoundingLimits
                {
                    Left = 2f,
                    Right = GraphicsDevice.Viewport.Width - 2f,
                    Up = 2f,
                    Down = GraphicsDevice.Viewport.Height - 2f
                };

            Debug = new Debug(this, _spriteBatch);
            Terrain = new Terrain(GraphicsDevice);
            Entities = new Entities();
            Cursor = new Cursor();
            Background = new Background();
            CameraOperator = new CameraOperator(GraphicsDevice.Viewport.Bounds);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Entities.LoadContent(Content);
            Background.LoadContent(Content);
            Cursor.LoadContent(Content);
            Terrain.LoadContent(Content);

            _pauseFilter = new Texture2D(GraphicsDevice, 1, 1);
            _pauseFilter.SetData(new[] {new Color(0f, 0f, 0f, .4f)});
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                Exit();
            if (Input.Keyboard.IsKeyTyped(Keys.Z))
                IsPaused = !IsPaused;

            if (IsPaused)
            {
                base.Update(gameTime);
                return;
            }

            World.Step(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            Debug.Update(gameTime);
            CameraOperator.Update(gameTime);
            Entities.Update(gameTime);
            Terrain.Update(gameTime);
            Background.Update(gameTime);
            Cursor.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            Background.Draw(_spriteBatch, CameraOperator.Camera);
            Terrain.Draw(_spriteBatch, CameraOperator.Camera);
            Entities.Draw(_spriteBatch, CameraOperator.Camera);
            Debug.Draw(_spriteBatch, CameraOperator.Camera);

            if (IsPaused)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                _spriteBatch.Draw(_pauseFilter, GraphicsDevice.Viewport.Bounds, Color.White);
                _spriteBatch.End();
            }

            Cursor.Draw(_spriteBatch, CameraOperator.Camera);

            base.Draw(gameTime);
        }
    }
}