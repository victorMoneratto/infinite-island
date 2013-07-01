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
        public static readonly World World = new World(new Vector2(0, 20));

        private readonly Debug _debug;
        private readonly EntitiesManager _entitiesManager;
        private readonly TerrainManager _terrainManager;
        private readonly Input _input;

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
                    SynchronizeWithVerticalRetrace = true
                };

            Content.RootDirectory = "Content";

            _input = new Input();
            _debug = new Debug();
            _entitiesManager = new EntitiesManager();
            _terrainManager = new TerrainManager();
        }

        protected override void LoadContent()
        {
            Camera.Setup(GraphicsDevice.Viewport.Bounds);

            _debug.LoadContent(this);

            _entitiesManager.LoadContent(this);

            _terrainManager.LoadContent(this);
            _terrainManager.Generate();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (Input.IsKeyPressed(Keys.Escape))
                Exit();

            if (Input.IsKeyPressed(Keys.F3))
                _debugEnabled = !_debugEnabled;

            if (Input.IsKeyDown(Keys.Up))
                Camera.Zoom += 1f*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.IsKeyDown(Keys.Down))
                Camera.Zoom -= 1f*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.IsKeyDown(Keys.Left))
                Camera.Rotation += MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            if (Input.IsKeyDown(Keys.Right))
                Camera.Rotation -= MathHelper.Pi*(gameTime.ElapsedGameTime.Milliseconds*1e-3f);

            World.Step(gameTime.ElapsedGameTime.Milliseconds*0.001f);
            _entitiesManager.Update(gameTime);
            _debug.Update();
            _terrainManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _terrainManager.Draw();
            _entitiesManager.Draw();

            if (_debugEnabled)
                _debug.Draw();

            base.Draw(gameTime);
        }
    }
}