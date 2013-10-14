using FarseerPhysics.Dynamics;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace InfiniteIsland.State
{
    public class Play : GameState
    {
        public const float LinearDistortion = -.12f, CubicDistortion = .2f;
        public readonly Background Background;
        public readonly CameraOperator CameraOperator;
        public readonly Cursor Cursor;
        public readonly Debug Debug;
        public readonly Entities Entities;
        public readonly HUD HUD;
        public readonly Terrain Terrain;
        public readonly World World = new World(new Vector2(0, 30));

        public int Coins;
        public float Factor = 1f;

        private Effect _coinsFX;
        private RenderTarget2D _coinsRT;
        private Effect _postFX;
        private RenderTarget2D _postRT;

        public Play(Game game) : base(game)
        {
            Cursor = new Cursor();
            Debug = new Debug(game, World);
            Entities = new Entities(World, this);
            Terrain = new Terrain(game.GraphicsDevice, World);
            CameraOperator = new CameraOperator(game.GraphicsDevice.Viewport.Bounds);
            HUD = new HUD();
            Background =
                new Background(new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));
            
        }

        public override void LoadContent()
        {
            _postRT = new RenderTarget2D(
                graphicsDevice: Game.GraphicsDevice,
                width: Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                height: Game.GraphicsDevice.PresentationParameters.BackBufferHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None);
            _postFX = Game.Content.Load<Effect>("Post");

            _coinsRT = new RenderTarget2D(
                graphicsDevice: Game.GraphicsDevice,
                width: Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                height: Game.GraphicsDevice.PresentationParameters.BackBufferHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None);
            _coinsFX = Game.Content.Load<Effect>("Coins");
            MediaPlayer.Play(Game.Content.Load<Song>("bgm/chaotic"));
            MediaPlayer.IsRepeating = true;
        }

        public override void Update(GameTime gameTime)
        {
            World.Step(gameTime.ElapsedGameTime.Milliseconds*1e-3f);
            Debug.Update(gameTime);
            Background.Update(gameTime);
            Cursor.Update(gameTime, CameraOperator, World);
            Entities.Update(gameTime, CameraOperator, World);
            Terrain.Update(gameTime, CameraOperator);
            CameraOperator.Update(gameTime, Entities, Factor);
            HUD.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Render revealed coins to own buffer
            Game.GraphicsDevice.SetRenderTarget(_coinsRT);
            Game.GraphicsDevice.Clear(Color.Transparent);
            Entities.Coins.Draw(spriteBatch, CameraOperator, true); //<= Coins

            //Render all the rest
            Game.GraphicsDevice.SetRenderTarget(_postRT);
            Game.GraphicsDevice.Clear(Background.SkyColor);
            Background.Draw(spriteBatch, CameraOperator.Camera);
            Terrain.Draw(spriteBatch, CameraOperator.Camera);
            Entities.Draw(spriteBatch, CameraOperator);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, _coinsFX);

            //Creating objects every frame? That makes me sad ):
            _coinsFX.Parameters["center"].SetValue(
                Input.Mouse.Position/
                new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width,
                    Game.GraphicsDevice.Viewport.Bounds.Height));

            _coinsFX.Parameters["aspectRatio"].SetValue(
                (float) Game.GraphicsDevice.Viewport.Bounds.Width/
                Game.GraphicsDevice.Viewport.Bounds.Height);

            spriteBatch.Draw(_coinsRT, Game.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();
            Cursor.Draw(spriteBatch, CameraOperator.Camera);

            Game.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _postFX);
            _postFX.Parameters["k"].SetValue(LinearDistortion);
            _postFX.Parameters["kcube"].SetValue(CubicDistortion);
            spriteBatch.Draw(_postRT, Vector2.Zero, Color.White);
            spriteBatch.End();

            HUD.Draw(spriteBatch, Coins);
            Debug.Draw(spriteBatch, CameraOperator.Camera);
        }
    }
}