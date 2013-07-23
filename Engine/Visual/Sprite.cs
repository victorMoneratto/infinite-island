using System.Collections.Generic;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Visual
{
    public class Sprite<T> where T : struct
    {
        private readonly Texture2D _spriteSheet;

        private readonly Dictionary<T?, Animation> _animations = new Dictionary<T?, Animation>();
        private T? _animationKey;
        private Animation _currentAnimation;

        public RotatableRectangleF Body { get; set; }
        public bool FlipHorizontally { get; set; }
        public Color Tint { get; set; }

        public Sprite(Texture2D spriteSheet, Vector2 dimensions)
        {
            Body = new RotatableRectangleF(dimensions);
            _spriteSheet = spriteSheet;
            Tint = Color.White;
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
                    x: (int)(points[i].X*Body.Dimensions.X),
                    y: (int)(points[i].Y*Body.Dimensions.Y),
                    width: (int)Body.Dimensions.X,
                    height: (int)Body.Dimensions.Y);
            }
            _animations.Add(key, new Animation(rectangles));
        }

        //TODO refactor function
        public void RegisterAnimation(T key, int firstSprite, int frameCount)
        {
            int columns = (int)(_spriteSheet.Width / Body.Dimensions.X);
            var spritePoints = new Point[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                spritePoints[i] = new Point((firstSprite + i)%columns, (firstSprite + i)/columns);
            }
            RegisterAnimation(key, spritePoints);
        }

        public void RegisterAnimation(T key, int frameCount, Point first)
        {
            int columns = (int)(_spriteSheet.Width / Body.Dimensions.X);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentAnimation != null)
            {
                spriteBatch.Draw(
                    texture: _spriteSheet,
                    position: Body.Center,
                    sourceRectangle: _currentAnimation.CurrentFrame,
                    color: Tint,
                    rotation: Body.Rotation,
                    origin: Body.Pivot,
                    scale: Body.Scale,
                    effects: FlipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    layerDepth: 0);
            }
        }
    }

    public static class SpriteExtension<T> where T : struct 
    {
        public static void Draw(SpriteBatch spriteBatch, Sprite<T> sprite)
        {
            sprite.Draw(spriteBatch);
        }
    }
}