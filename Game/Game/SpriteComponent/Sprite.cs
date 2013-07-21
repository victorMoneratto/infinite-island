using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.SpriteComponent
{
    public class Sprite<T> where T : struct
    {
        private readonly Dictionary<T?, Animation> _animations = new Dictionary<T?, Animation>();
        private readonly Vector2 _center;
        private readonly Point _dimensions;
        private readonly Texture2D _spriteSheet;

        private T? _animationKey;
        private Animation _currentAnimation;
        private Vector2 _position;
        private float _rotation;

        public Sprite(Texture2D spriteSheet, Point dimensions)
        {
            _dimensions = dimensions;
            _spriteSheet = spriteSheet;

            _center = new Vector2(_dimensions.X/2f, _dimensions.Y/2f);
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public T? AnimationKey
        {
            get { return _animationKey; }
            set
            {
                if (value == null
                    || (_animationKey != null && _animationKey.Equals(value))) return;

                Animation animation;

                if (!_animations.TryGetValue(value, out animation))
                {
                    _currentAnimation = null;
                    return;
                }

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
                    x: points[i].X*_dimensions.X,
                    y: points[i].Y*_dimensions.Y,
                    width: _dimensions.X,
                    height: _dimensions.Y);
            }
            _animations.Add(key, new Animation(rectangles));
        }

        //TODO refactor function
        public void RegisterAnimation(T key, int firstSprite, int frameCount)
        {
            int columns = _spriteSheet.Width/_dimensions.X;
            var spritePoints = new Point[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                spritePoints[i] = new Point((firstSprite + i)%columns, (firstSprite + i)/columns);
            }
            RegisterAnimation(key, spritePoints);
        }

        public void RegisterAnimation(T key, int frameCount, Point first)
        {
            int columns = _spriteSheet.Width/_dimensions.X;
            var points = new Point[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                points[i] = new Point((first.X + i)%columns, first.Y + (first.X + i)/columns);
            }

            RegisterAnimation(key, points);
        }

        public void Update(GameTime gameTime)
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool flipHorizontal = false)
        {
            if (_currentAnimation != null)
            {
                spriteBatch.Draw(
                    texture: _spriteSheet,
                    position: _position,
                    sourceRectangle: _currentAnimation.CurrentFrame,
                    color: Color.White,
                    rotation: _rotation,
                    origin: _center,
                    scale: 1,
                    effects: flipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    layerDepth: 0);
            }
        }
    }
}