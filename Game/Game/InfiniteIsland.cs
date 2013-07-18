using FarseerPhysics.Dynamics;
using InfiniteIsland.Game.Entity;
using InfiniteIsland.Game.Terrain;
using InfiniteIsland.Game.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Game
{
    public class InfiniteIsland : Microsoft.Xna.Framework.Game
    {
        public static readonly World World = new World(new Vector2(0, 40));

        public static readonly Debug Debug = new Debug();
        public static readonly EntitiesManager EntitiesManager = new EntitiesManager();
        public static readonly TerrainManager TerrainManager = new TerrainManager();

#if DEBUG
        private bool _debugEnabled = true;
#else
        private bool _debugEnabled;
#endif

        public InfiniteIsland()
        {
            new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720,
                    PreferMultiSampling = true,
                    SynchronizeWithVerticalRetrace = true,
                };

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            Camera.Setup(GraphicsDevice.Viewport.Bounds);

            Debug.LoadContent(this);

            EntitiesManager.LoadContent(this);

            TerrainManager.LoadContent(this);
            TerrainManager.Generate();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (Input.Keyboard.IsKeyTyped(Keys.Escape))
                Exit();

            if (Input.Keyboard.IsKeyTyped(Keys.F3))
                _debugEnabled = !_debugEnabled;

            if (Input.Keyboard.IsKeyDown(Keys.Up))
                Camera.Zoom += 1f*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Down))
                Camera.Zoom -= 1f*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Left))
                Camera.Rotation += MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.Keyboard.IsKeyDown(Keys.Right))
                Camera.Rotation -= MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            World.Step(gameTime.ElapsedGameTime.Milliseconds*0.001f);
            EntitiesManager.Update(gameTime);
            Debug.Update();
            TerrainManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            TerrainManager.Draw();
            EntitiesManager.Draw();

            if (_debugEnabled)
                Debug.Draw();

            base.Draw(gameTime);
        }
    }
}