using System.Globalization;
using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Hud : IUpdateable, IDrawable
    {
        private static Texture2D _scoreTexture;
        private static SpriteFont _scoreFont;
        private static Vector2 _scoreTextPosition;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            string scoreText = InfiniteIsland.Coins.ToString(CultureInfo.InvariantCulture);

            spriteBatch.Begin();

            spriteBatch.Draw(
                texture: _scoreTexture,
                position: Vector2.Zero,
                sourceRectangle: null,
                color: Color.White*.8f,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);

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
            _scoreTexture = content.Load<Texture2D>("img/coin");
            _scoreFont = content.Load<SpriteFont>("Bauhaus");

            _scoreTextPosition = new Vector2(_scoreTexture.Width, 0);
        }
    }
}