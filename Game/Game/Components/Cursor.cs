using FarseerPhysics.Dynamics.Joints;
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

        private readonly Color _fillColor = Color.White;
        //private FixedMouseJoint _mouseJoint;
        private Vector2 _scale = Vector2.One;

        public float Radius
        {
            get { return _cursorTexture.Width*_scale.X; }
            set { _scale = new Vector2(value/_cursorTexture.Width); }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

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
                color: _fillColor,
                rotation: 0,
                origin: _cursorCenter,
                scale: .8f*_scale,
                effects: SpriteEffects.None,
                layerDepth: 0);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            //if (Input.Mouse.IsButtonClicked(Input.Mouse.MouseButton.Left))
            //{
            //    Fixture fixture;
            //    if ((fixture = InfiniteIsland.World.TestPoint(mousePosition)) != null)
            //    {
            //    }
            //}
        }

        public static void LoadContent(ContentManager content)
        {
            _cursorTexture = content.Load<Texture2D>("img/cursor");
            _cursorCenter = new Vector2(_cursorTexture.Width/2f, _cursorTexture.Height/2f);
        }
    }
}