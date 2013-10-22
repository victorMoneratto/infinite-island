using System;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace InfiniteIsland.State
{
    internal class MainMenu : GameState
    {
        private SoundEffect _elements;
        private SpriteFont _font;
        private Song _song;
        private RotatableRectangleF _textBounds;
        private Waiter _waiter;
        private Texture2D _tutorial;

        public MainMenu(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            _song = Game.Content.Load<Song>("bgm/menuBgm");
            _font = Game.Content.Load<SpriteFont>("Bauhaus");
            _elements = Game.Content.Load<SoundEffect>("sfx/elements01");

            _tutorial = Game.Content.Load<Texture2D>("img/tutorial");

            MediaPlayer.Play(_song);
            _textBounds = new RotatableRectangleF(_font.MeasureString("[Enter] to Play"))
            {
                Center = new Vector2(
                    x: .5f * Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                    y: Game.GraphicsDevice.PresentationParameters.BackBufferHeight - 100)
            };

            //Should be disposed :)
            _waiter = Wait.Until(time =>
            {
                _textBounds.Rotation = (float) Math.Sin(time.Alive*MathHelper.TwoPi)*.25f;
                _textBounds.Scale = 1.5f*new Vector2(
                    .5f + (float) Math.Sin((time.Alive*MathHelper.TwoPi)%MathHelper.Pi));
                return false;
            }, null, -1);
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Enter))
            {
                MediaPlayer.Stop();
                InfiniteIsland.GameState = new Play(Game);
                InfiniteIsland.GameState.LoadContent();
                Wait.Waiters.Remove(_waiter);
                _elements.Play(.5f, 0f, 0f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(_tutorial, Vector2.Zero, Color.White);
            spriteBatch.DrawString(
                spriteFont: _font,
                text: "[Enter] to Play",
                position: _textBounds.Center,
                color: Color.WhiteSmoke,
                rotation: _textBounds.Rotation,
                origin: _textBounds.Pivot,
                scale: _textBounds.Scale,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();
        }
    }
}