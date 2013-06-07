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
        public static Camera Camera;

        private readonly DebugComponent _debugComponent;
        private readonly EntityComponent _entityComponent;
        private readonly TerrainComponent _terrainComponent;
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
            _debugComponent = new DebugComponent();
            _entityComponent = new EntityComponent();
            _terrainComponent = new TerrainComponent(); //Completely temporary
        }

        protected override void LoadContent()
        {
            Camera = new Camera(GraphicsDevice.Viewport.Bounds);

            _debugComponent.LoadContent(this);

            _entityComponent.LoadContent(this);

            _terrainComponent.LoadContent(this);
            _terrainComponent.Generate();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (Input.IsKeyPressed(Keys.Escape))
                Exit();

            if (Input.IsKeyPressed(Keys.F3))
                _debugEnabled = !_debugEnabled;

            World.Step(gameTime.ElapsedGameTime.Milliseconds*0.001f);
            _entityComponent.Update(gameTime);
            _debugComponent.Update();
            _terrainComponent.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _terrainComponent.Draw();
            _entityComponent.Draw();

            if (_debugEnabled)
                _debugComponent.Draw();

            base.Draw(gameTime);
        }
    }
}