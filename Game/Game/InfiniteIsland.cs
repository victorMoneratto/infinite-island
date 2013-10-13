using System;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math.Geometry;
using InfiniteIsland.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    internal class InfiniteIsland : Game
    {
        public static readonly Random Random = new Random();

        public static bool IsPaused;
        public SpriteBatch SpriteBatch;
        private Texture2D _pauseFilter;
        public GameState GameState;

        public InfiniteIsland()
        {
            Content.RootDirectory = "Content";
            new GraphicsDeviceManager(this)
            {
                //PreferredBackBufferWidth = 1920, PreferredBackBufferHeight = 1080,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                PreferMultiSampling = true,
                SynchronizeWithVerticalRetrace = true,
                //IsFullScreen = true
            };
        }

        //Replacement for Initialize method, it is so that we can call it after LoadContent
        private void Setup()
        {
            Input.Mouse.Limits = new BoundingLimits
            {
                Left = 2f,
                Right = GraphicsDevice.Viewport.Width - 2f,
                Up = 2f,
                Down = GraphicsDevice.Viewport.Height - 2f
            };
            GameState = new Play(this);
            GameState.LoadContent();
        }

        protected override void LoadContent()
        {
            Cursor.LoadContent(Content);
            Entities.LoadContent(Content);
            Terrain.LoadContent(Content);
            HUD.LoadContent(Content);
            Background.LoadContent(Content);

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _pauseFilter = new Texture2D(GraphicsDevice, 1, 1);
            _pauseFilter.SetData(new[] {new Color(0f, 0f, 0f, .4f)});

            base.LoadContent();

            Setup();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(IsActive);

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                Exit();
            if (Input.Keyboard.IsKeyTyped(Keys.Z))
                IsPaused = !IsPaused;

            if (IsPaused)
            {
                base.Update(gameTime);
                return;
            }

            Wait.Update(gameTime);

            GameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameState.Draw(SpriteBatch);
            if (IsPaused)
            {
                SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                SpriteBatch.Draw(_pauseFilter, GraphicsDevice.Viewport.Bounds, Color.White);
                SpriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}