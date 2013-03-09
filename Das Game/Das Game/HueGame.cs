using DasGame.Component;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DasGame
{
    public class HueGame : Game
    {
        public static readonly World World = new World(new Vector2(0, 20));

        private readonly Input _input;
        public readonly DebugComponent DebugComponent;
        public readonly EntityComponent EntityComponent;
        public readonly FloorComponent FloorComponent;

        public HueGame()
        {
            new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720,
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
            DebugComponent.LoadContent(this);
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

            World.Step(gameTime.ElapsedGameTime.Milliseconds*0.001f);

            EntityComponent.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);

            DebugComponent.Draw();
            EntityComponent.Draw();
            FloorComponent.Draw();

            base.Draw(gameTime);
        }
    }
}