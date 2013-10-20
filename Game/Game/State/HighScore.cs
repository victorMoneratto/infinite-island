using System.Runtime.InteropServices;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.State
{
    internal class HighScore : GameState
    {
        private string _highScore;
        private SpriteFont _font;

        public HighScore(Game game, int highScore) : base(game)
        {
            _highScore = highScore.ToString();
        }

        public override void LoadContent()
        {
            Game.Content.Load<SoundEffect>("sfx/applause").Play();
            _font = Game.Content.Load<SpriteFont>("Bauhaus");

            Vector2 center = new Vector2(
                x:Game.GraphicsDevice.Viewport.Bounds.Width/2f,
                y:Game.GraphicsDevice.Viewport.Bounds.Height/2f);

            //_stringPosition = center - new Vector2(.5f, 1f) * _font.MeasureString("You Scored");
            _stringPosition = center*new Vector2(.1f, .5f);

            _coin = new Sprite(Game.Content.Load<Animation>("sprite/coin"))
            {
                Key = "good",
                Body =
                {
                    Left = _stringPosition.X + 75,
                    Up = _stringPosition.Y + 50,
                    Scale = new Vector2(2)
                }
            };

            _highScorePosition = _coin.Body.Center + new Vector2(75, -70);

            Wait.Until(time =>
            {
                _coin.Body.Rotation = time.Alive * MathHelper.Pi;
                return false;
            });
            _orderPosition = 2*center - _font.MeasureString("[Enter] to Restart");
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Enter))
            {
                InfiniteIsland.GameState = new Play(Game);
                InfiniteIsland.GameState.LoadContent();
            }
        }

        private Vector2 _orderPosition;
        private Vector2 _highScorePosition;
        private Vector2 _stringPosition;
        private Sprite _coin;

        public override void Draw(SpriteBatch spriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.DrawString(_font, "You Scored", _stringPosition, Color.Orange, -MathHelper.PiOver4/2f, Vector2.Zero, 1.5f*Vector2.One, SpriteEffects.None, 0f);
            _coin.Key = "outline";
            _coin.Draw(spriteBatch);
            _coin.Key = "good";
            _coin.Body.Pivot -= new Vector2(3);
            _coin.Body.Center -= new Vector2(3);
            _coin.Draw(spriteBatch);
            _coin.Body.Pivot += new Vector2(3);
            _coin.Body.Center += new Vector2(3);
            spriteBatch.DrawString(_font, _highScore, _highScorePosition, Color.AntiqueWhite, -MathHelper.PiOver4 / 2f, Vector2.Zero, 2*Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, "[Enter] to Restart", _orderPosition, Color.White);
            spriteBatch.End();
        }
    }
}