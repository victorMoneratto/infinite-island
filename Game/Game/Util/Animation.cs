using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Util
{
    internal class Animation
    {
        private readonly Rectangle[] _animationFrames;
        private readonly int _frames;

        private Rectangle _currentFrame;
        private int _currentIndex;
        private int _millisPerFrame;
        private int _millisSinceLastFrame;

        private const int DefaultFps = 15;

        public Animation(params Rectangle[] frames)
        {
            FramesPerSecond = DefaultFps;
            _animationFrames = frames;
            _frames = _animationFrames.Length;
        }

        public int FramesPerSecond
        {
            get { return _millisPerFrame/1000; }
            set { _millisPerFrame = 1000/value; }
        }

        public Rectangle CurrentFrame
        {
            get { return _currentFrame; }
        }

        public void Update(GameTime gameTime)
        {
            _millisSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_millisSinceLastFrame >= _millisPerFrame)
            {
                _millisSinceLastFrame -= _millisPerFrame;
                _currentIndex = (_currentIndex + 1)%_frames;
                _currentFrame = _animationFrames[_currentIndex];
            }
        }
    }
}