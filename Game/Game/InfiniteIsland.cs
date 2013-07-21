using FarseerPhysics.Dynamics;
using InfiniteIsland.Background;
using InfiniteIsland.Debug;
using InfiniteIsland.Entity;
using InfiniteIsland.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    public class InfiniteIsland : Game
    {
        
        private readonly World _world = new World(new Vector2(0, 40));
        private Player _player;

        private DebugComponent _debug;
        private EntityComponent _entity;
        private TerrainComponent _terrain;
        private BackgroundComponent _background;

        private SpriteBatch _spriteBatch;

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

            _debug = new DebugComponent(this, _world, _spriteBatch);
            _player = new Player(_world, Content);
            _entity = new EntityComponent(_player, this);
            _background = new BackgroundComponent(this);
            _terrain = new TerrainComponent(this, _world);

            Camera.Setup(GraphicsDevice.Viewport.Bounds);
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

            _world.Step(gameTime.ElapsedGameTime.Milliseconds*0.001f);
            _background.Update(gameTime);
            _terrain.Update(gameTime);
            _entity.Update(gameTime);
            _debug.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _background.Draw(_spriteBatch);
            _terrain.Draw(_spriteBatch);
            _entity.Draw(_spriteBatch);

            if (_debugEnabled)
                _debug.Draw(_spriteBatch);
        }
    }
}