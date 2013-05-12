using FarseerPhysics.Dynamics;
using InfiniteIsland.Game.Entity;
using InfiniteIsland.Game.Floor;
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
        private readonly FloorComponent _floorComponent;
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
            _floorComponent = new FloorComponent(); //Completely temporary
        }

        protected override void LoadContent()
        {
            Camera = new Camera(GraphicsDevice.Viewport);
            _debugComponent.LoadContent(this);
            _entityComponent.LoadContent(this);
            _floorComponent.LoadContent(this);
            _floorComponent.Generate();
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _floorComponent.Draw();
            _entityComponent.Draw();
            if (_debugEnabled)
                _debugComponent.Draw();

            base.Draw(gameTime);
        }
    }
}