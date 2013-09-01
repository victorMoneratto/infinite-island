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

        public static Debug Debug;
        public static Entities Entities;
        public static Terrain Terrain;
        public static CameraOperator CameraOperator;
        public static Hud Hud;
        public static Cursor Cursor;
        public static Background Background;

        public static int Coins;
        public static float Factor = 1f;
        public static bool IsPaused;

        private Texture2D _pauseFilter;
        private Effect _postFx;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _spriteBatch;
        //To be removed as soon as default values are set
        private float k = -.12f, kcube = +.2f;

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

            Debug = new Debug(this, _spriteBatch);
            Entities = new Entities();
            Terrain = new Terrain(GraphicsDevice);
            CameraOperator = new CameraOperator(GraphicsDevice.Viewport.Bounds);
            Hud = new Hud();
            Cursor = new Cursor();
            Background = new Background();
        }

        protected override void LoadContent()
        {
            Entities.LoadContent(Content);
            Terrain.LoadContent(Content);
            Hud.LoadContent(Content);
            Background.LoadContent(Content);
            Cursor.LoadContent(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _renderTarget = new RenderTarget2D(
                graphicsDevice: GraphicsDevice,
                width: GraphicsDevice.PresentationParameters.BackBufferWidth,
                height: GraphicsDevice.PresentationParameters.BackBufferHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None);
            _postFx = Content.Load<Effect>("Post");

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

            Debug.Update(gameTime);
            Entities.Update(gameTime);
            Terrain.Update(gameTime);
            CameraOperator.Update(gameTime);
            Hud.Update(gameTime);
            Background.Update(gameTime);
            Cursor.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Background.SkyColor);
            
            Background.Draw(_spriteBatch, CameraOperator.Camera);
            Terrain.Draw(_spriteBatch, CameraOperator.Camera);
            Entities.Draw(_spriteBatch, CameraOperator.Camera);

            _spriteBatch.Begin();
            SpriteFont font = Content.Load<SpriteFont>("Bauhaus");
            _spriteBatch.DrawString(font, "K: " + k, 100 * Vector2.UnitX, Color.White);
            _spriteBatch.DrawString(font, "KCube: " + kcube, 500 * Vector2.UnitX, Color.White);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            if (Input.Keyboard.IsKeyDown(Keys.Up))
                k += gameTime.TotalGameTime.Milliseconds*1e-5f;
            if (Input.Keyboard.IsKeyDown(Keys.Down))
                k -= gameTime.TotalGameTime.Milliseconds*1e-5f;
            if (Input.Keyboard.IsKeyDown(Keys.Left))
                kcube += gameTime.TotalGameTime.Milliseconds*1e-5f;
            if (Input.Keyboard.IsKeyDown(Keys.Right))
                kcube -= gameTime.TotalGameTime.Milliseconds*1e-5f;

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _postFx);
            _postFx.Parameters["k"].SetValue(k);
            _postFx.Parameters["kcube"].SetValue(kcube);
            _spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
            _spriteBatch.End();

            Hud.Draw(_spriteBatch, CameraOperator.Camera);
            Debug.Draw(_spriteBatch, CameraOperator.Camera);
            Cursor.Draw(_spriteBatch, CameraOperator.Camera);

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