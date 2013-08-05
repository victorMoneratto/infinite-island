using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Components
{
    internal class Cursor : Engine.IDrawable
    {
        private readonly Texture2D _cursorTexture;
        private readonly Vector2 _cursorCenter;
        public Cursor(Game game)
        {
            _cursorTexture = game.Content.Load<Texture2D>("img/cursor");
            _cursorCenter = new Vector2(_cursorTexture.Width/2f, _cursorTexture.Height/2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_cursorTexture, Input.Mouse.Position, null, Color.Black, 0, _cursorCenter, .8f*Vector2.One, SpriteEffects.None, 0);
            spriteBatch.Draw(_cursorTexture, Input.Mouse.Position, null, Color.White, 0, _cursorCenter, .6f*Vector2.One, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}