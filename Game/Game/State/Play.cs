using FarseerPhysics.Dynamics;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace InfiniteIsland.State
{
    public class Play : GameState
    {
        private const float LinearDistortion = -.12f;
        private const float CubicDistortion = .2f;

        public readonly Background Background;
        public readonly CameraOperator CameraOperator;
        public readonly Cursor Cursor;
        public readonly Debug Debug;
        public readonly Entities Entities;
        public readonly HUD HUD;
        public readonly Terrain Terrain;
        public readonly World World = new World(new Vector2(0, 30));

        public int Coins;
        public float Factor;
        private SoundEffect _coinConsumeSound;

        private Effect _coinsFX;
        private RenderTarget2D _coinsRT;

        private bool _factorIsChanging;
        private SoundEffect _hurtSound;
        private Effect _postFX;
        private RenderTarget2D _postRT;

        public Play(Game game) : base(game)
        {
            Cursor = new Cursor();
            Debug = new Debug(game, World, this);
            Entities = new Entities(World, this);
            Terrain = new Terrain(game.GraphicsDevice, World);
            CameraOperator = new CameraOperator(game.GraphicsDevice.Viewport.Bounds);
            HUD = new HUD();
            Background =
                new Background(new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height));

            Wait.Until(time => Tweening.Tween(
                start: 0f,
                end: 1f,
                progress: time.Alive/2f,
                step: value => Factor = value,
                scale: TweenScales.Quadratic));
        }

        public void LowerFactor()
        {
            _hurtSound.Play(1f, 0f, 0);
            if (!_factorIsChanging)
            {
                _factorIsChanging = true;
                float factor = Factor;
                Wait.Until(time =>
                    Tweening.Tween(
                        start: factor,
                        end: factor - 1/5f,
                        progress: time.Alive/.4f,
                        step: value => Factor = value,
                        scale: TweenScales.Quadratic),
                    () => _factorIsChanging = false);
            }
        }

        public void RaiseFactor()
        {
            ++Coins;
            _coinConsumeSound.Play(1f, 1f, 0f);
            if (!_factorIsChanging)
            {
                if (Factor <= .9f)
                {
                    float factor = Factor;
                    Wait.Until(time =>
                        Tweening.Tween(
                            start: factor,
                            end: factor + 1/10f,
                            progress: time.Alive/.2f,
                            step: value => Factor = value,
                            scale: TweenScales.Quadratic),
                        () => _factorIsChanging = false);
                }
            }
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
            _coinConsumeSound = Game.Content.Load<SoundEffect>("sfx/coin");
            _hurtSound = Game.Content.Load<SoundEffect>("sfx/sword03");
            _transitionFilter = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _transitionFilter.SetData(new[]{Color.Black});

            MediaPlayer.Play(Game.Content.Load<Song>("bgm/chaotic"));
            MediaPlayer.IsRepeating = true;
        }

        private float _colorFactor = 0f;
        private Texture2D _transitionFilter;

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

            if (Factor < -.1f && _colorFactor == 0)
            {
                Wait.Until(time =>
                {
                    _colorFactor = time.Alive;
                    return _colorFactor >= 1;
                },
                () =>
                {
                    InfiniteIsland.GameState = new HighScore(Game, Coins);
                    InfiniteIsland.GameState.LoadContent();
                });
                MediaPlayer.Stop();
            }
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
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, _coinsFX);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null);

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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(_transitionFilter, Game.GraphicsDevice.Viewport.Bounds, Color.White * _colorFactor);
            spriteBatch.End();

            HUD.Draw(spriteBatch, Coins);
            Debug.Draw(spriteBatch, CameraOperator.Camera);
        }
    }
}