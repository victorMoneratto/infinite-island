using System.Globalization;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Hud : IUpdateable, IDrawable
    {
        private static Sprite _scoreSprite;
        private static SpriteFont _scoreFont;
        private static Vector2 _scoreTextPosition;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            string scoreText = InfiniteIsland.Coins.ToString(CultureInfo.InvariantCulture);

            spriteBatch.Begin();

            _scoreSprite.Draw(spriteBatch);

            spriteBatch.DrawString(
                spriteFont:_scoreFont,
                text:scoreText,
                color: Color.White*.8f,
                position: _scoreTextPosition);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            _scoreFont = content.Load<SpriteFont>("Bauhaus");

            _scoreSprite = new Sprite(content.Load<Animation>("sprite/coin"));
            _scoreSprite.Body.Pivot = Vector2.Zero;
            _scoreSprite.Tint = Color.White*.8f;

            _scoreTextPosition = new Vector2(_scoreSprite.Animation.MaxDimensions.X, 0);
        }
    }
}