using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;

namespace InfiniteIsland.Components
{
    internal class Cursor : IDrawable
    {
        private static Vector2 _cursorCenter;
        private static Texture2D _cursorTexture;

        private Vector2 _scale = Vector2.One;

        public static void LoadContent(ContentManager content)
        {
            _cursorTexture = content.Load<Texture2D>("img/cursor");
            _cursorCenter = new Vector2(_cursorTexture.Width / 2f, _cursorTexture.Height / 2f);
        }

        public float Radius
        {
            get { return _cursorTexture.Width*_scale.X; }
            set { _scale = new Vector2(value/_cursorTexture.Width); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                texture: _cursorTexture,
                position: Input.Mouse.Position,
                sourceRectangle: null,
                color: Color.Black,
                rotation: 0,
                origin: _cursorCenter,
                scale: _scale,
                effects: SpriteEffects.None,
                layerDepth: 0);

            spriteBatch.Draw(
                texture: _cursorTexture,
                position: Input.Mouse.Position,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0,
                origin: _cursorCenter,
                scale: .8f*_scale,
                effects: SpriteEffects.None,
                layerDepth: 0);

            spriteBatch.End();
        }
    }
}