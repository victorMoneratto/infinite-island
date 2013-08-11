using InfiniteIsland.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Cursor : IUpdateable, IDrawable
    {
        private static Vector2 _cursorCenter;
        private static Texture2D _cursorTexture;

        public float Alpha = .75f;
        private Vector2 _scale = Vector2.One;

        public float Radius
        {
            get { return _cursorTexture.Width*_scale.X; }
            set { _scale = new Vector2(value/_cursorTexture.Width); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(
                texture: _cursorTexture,
                position: Input.Mouse.Position,
                sourceRectangle: null,
                color: new Color(0f, 0, 0f, Alpha),
                rotation: 0,
                origin: _cursorCenter,
                scale: _scale,
                effects: SpriteEffects.None,
                layerDepth: 0);

            spriteBatch.Draw(
                texture: _cursorTexture,
                position: Input.Mouse.Position,
                sourceRectangle: null,
                color: new Color(1f, 1f, 1f, Alpha),
                rotation: 0,
                origin: _cursorCenter,
                scale: .8f*_scale,
                effects: SpriteEffects.None,
                layerDepth: 0);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = InfiniteIsland.Camera.PositionOnWorld(Input.Mouse.Position);
            float diff = (InfiniteIsland.Camera.Viewport.Center.X -
                            mousePosition.X)/
                           (2*InfiniteIsland.Camera.Viewport.Dimensions.X);
            InfiniteIsland.Camera.Viewport.Rotation = diff * MathHelper.PiOver4 / 5f;

            float diff1 = (mousePosition.Y/InfiniteIsland.Camera.Viewport.Center.Y);
            InfiniteIsland.Camera.Viewport.Scale = new Vector2(1 + diff1*5e-4f);
        }

        public static void LoadContent(ContentManager content)
        {
            _cursorTexture = content.Load<Texture2D>("img/cursor");
            _cursorCenter = new Vector2(_cursorTexture.Width/2f, _cursorTexture.Height/2f);
        }
    }
}