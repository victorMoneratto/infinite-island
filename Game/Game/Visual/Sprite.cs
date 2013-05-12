using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Visual
{
    internal class Sprite<T> where T : struct
    {
        private readonly Dictionary<T?, Animation> _animations = new Dictionary<T?, Animation>();
        private readonly Vector2 _center;
        private readonly int _height;
        private readonly Texture2D _spriteSheet;
        private readonly int _width;

        private T? _animationKey;
        private Animation _currentAnimation;
        private Vector2 _position;

        public Sprite(Texture2D spriteSheet, int columns, int rows)
        {
            OffsetFromCenter = Vector2.Zero;
            _spriteSheet = spriteSheet;

            _width = spriteSheet.Width / columns;
            _height = spriteSheet.Height / rows;
            _center = new Vector2(_width / 2f, _height / 2f);
        }

        public Vector2 OffsetFromCenter { get; set; }

        public T? AnimationKey
        {
            get { return _animationKey; }
            set
            {
                if (value == null
                    || (_animationKey != null && _animationKey.Equals(value))) return;

                Animation animation;

                if (!_animations.TryGetValue(value, out animation)) return;

                _animationKey = value;
                _currentAnimation = animation;
            }
        }

        public void RegisterAnimation(T key, params Point[] points)
        {
            var rectangles = new Rectangle[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                rectangles[i] = new Rectangle(
                    x: points[i].X * _width,
                    y: points[i].Y * _height,
                    width: _width,
                    height: _height);
            }
            _animations.Add(key, new Animation(rectangles));
        }

        public void RegisterAnimation(T key, int row)
        {
            int columns = _spriteSheet.Width / _width;
            var rectangles = new Rectangle[columns];
            for (int i = 0; i < columns; i++)
            {
                rectangles[i] = new Rectangle(
                    x: i * _width,
                    y: row * _height,
                    width: _width,
                    height: _height);
            }
            _animations.Add(key, new Animation(rectangles));
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            _currentAnimation.Update(gameTime);
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch, bool flipHorizontal)
        {
            spriteBatch.Draw(
                texture: _spriteSheet,
                position: _position + OffsetFromCenter,
                sourceRectangle: _currentAnimation.CurrentFrame,
                color: Color.White,
                rotation: 0,
                origin: _center,
                scale: 1,
                effects: flipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layerDepth: 0);
        }
    }
}