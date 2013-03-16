using DasGame.Component;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DasGame
{
    public class HueGame : Game
    {
        public static readonly World World = new World(new Vector2(0, 40));
        public static Camera Camera;

        public readonly DebugComponent DebugComponent;
        public readonly EntityComponent EntityComponent;
        public readonly FloorComponent FloorComponent;
        private readonly Input _input;

        private bool _debugEnabled = false;

        public HueGame()
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
            DebugComponent = new DebugComponent();
            EntityComponent = new EntityComponent();
            FloorComponent = new FloorComponent(); //Completely temporary
        }

        protected override void LoadContent()
        {
            Camera = new Camera(GraphicsDevice.Viewport);
#if DEBUG
            DebugComponent.LoadContent(this);
#endif
            EntityComponent.LoadContent(this);
            FloorComponent.LoadContent(this);
            FloorComponent.Generate();
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

            EntityComponent.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            FloorComponent.Draw();
            EntityComponent.Draw();
#if DEBUG
            if (_debugEnabled)
                DebugComponent.Draw();
#endif

            base.Draw(gameTime);
        }
    }
}