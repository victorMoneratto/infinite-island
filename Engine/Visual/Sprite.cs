using System;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Visual
{
    public class Sprite
    {
        public Animation Animation;
        public RotatableRectangleF Body;
        public SpriteEffects Flip;
        public Color Tint;
        private Vector2 _centerOffset;

        private Rectangle[] _frames;
        private int _index;
        private string _key;

        private int _millisPerFrame;
        private int _millisSinceLastFrame;

        public Sprite(Animation animation)
        {
            Animation = animation;
            Body = new RotatableRectangleF(Animation.MaxDimensions);

            Flip = SpriteEffects.None;
            Tint = Color.White;
            FramesPerSecond = 30;

            if (Animation.Animations.Count == 1)
            {
                var keys = new string[1];
                Animation.Animations.Keys.CopyTo(keys, 0);
                Key = keys[0];
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                if (value != _key)
                {
                    _key = value;
                    _frames = Animation.Animations[_key];
                    _index = 0;
                    CalculateOffset();
                }
            }
        }

        public int FramesPerSecond
        {
            get { return 1000/_millisPerFrame; }
            set
            {
                if (value != 0)
                    _millisPerFrame = 1000/value;
            }
        }

        public void Update(GameTime gameTime)
        {
            _millisSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_millisSinceLastFrame >= _millisPerFrame)
            {
                _millisSinceLastFrame -= _millisPerFrame;
                _index = (_index + 1)%_frames.Length;
                CalculateOffset();
            }
        }

        private void CalculateOffset()
        {
            _centerOffset = (Animation.MaxDimensions - new Vector2(_frames[_index].Width, _frames[_index].Height))/2f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_frames == null)
                throw new Exception("A valid animation key must be set");
            spriteBatch.Draw(
                texture: Animation.Texture,
                position: Body.Center + _centerOffset,
                color: Tint,
                sourceRectangle: _frames[_index],
                rotation: Body.Rotation,
                origin: Body.Pivot,
                scale: Body.Scale,
                effects: Flip,
                layerDepth: 0f);
        }
    }
}