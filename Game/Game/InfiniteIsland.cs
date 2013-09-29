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
        public static readonly World World = new World(new Vector2(0, 30));
        public static readonly Random Random = new Random();

        public static int Coins;
        public static float Factor = 1f;
        public static bool IsPaused;

        private Texture2D _pauseFilter;
        private Effect _postFX, _coinsFX;
        private RenderTarget2D _postRT, _coinsRT;
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

            Debug.Instance = new Debug(this, _spriteBatch);
            Entities.Instance = new Entities();
            Terrain.Instance = new Terrain(GraphicsDevice);
            CameraOperator.Instance = new CameraOperator(GraphicsDevice.Viewport.Bounds);
            HUD.Instance = new HUD();
            Cursor.Instance = new Cursor();
            Background.Instance = new Background(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
        }

        protected override void LoadContent()
        {
            Entities.LoadContent(Content);
            Terrain.LoadContent(Content);
            HUD.LoadContent(Content);
            Background.LoadContent(Content);
            Cursor.LoadContent(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _postRT = new RenderTarget2D(
                graphicsDevice: GraphicsDevice,
                width: GraphicsDevice.PresentationParameters.BackBufferWidth,
                height: GraphicsDevice.PresentationParameters.BackBufferHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None);
            _postFX = Content.Load<Effect>("Post");

            _coinsRT = new RenderTarget2D(
                graphicsDevice: GraphicsDevice,
                width: GraphicsDevice.PresentationParameters.BackBufferWidth,
                height: GraphicsDevice.PresentationParameters.BackBufferHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None);
            _coinsFX = Content.Load<Effect>("Coins");

            _pauseFilter = new Texture2D(GraphicsDevice, 1, 1);
            _pauseFilter.SetData(new[] {new Color(0f, 0f, 0f, .4f)});

            Setup();

            base.LoadContent();
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

            World.Step(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            Entities.Instance.Update(gameTime);
            Terrain.Instance.Update(gameTime);
            CameraOperator.Instance.Update(gameTime);
            HUD.Instance.Update(gameTime);
            Background.Instance.Update(gameTime);
            Cursor.Instance.Update(gameTime);
            Debug.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Render revealed coins to a separate Render Target
            GraphicsDevice.SetRenderTarget(_coinsRT);
            GraphicsDevice.Clear(Color.Transparent);
            Entities.Instance.Coins.Draw(_spriteBatch, true);//<= Coins

            //Render all in-game stuff
            GraphicsDevice.SetRenderTarget(_postRT);
            GraphicsDevice.Clear(Background.Instance.SkyColor);
            Background.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);
            Terrain.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);
            Entities.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, _coinsFX);
            _spriteBatch.Draw(_coinsRT, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.End();
            Cursor.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _postFX);
            _spriteBatch.Draw(_postRT, Vector2.Zero, Color.White);
            _spriteBatch.End();

            HUD.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);
            Debug.Instance.Draw(_spriteBatch, CameraOperator.Instance.Camera);

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