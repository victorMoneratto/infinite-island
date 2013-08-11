using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Visual
{
    internal class Animation
    {
        private const int DefaultFps = 20;
        private readonly Rectangle[] _animationFrames;
        private readonly int _frames;

        private Rectangle _currentFrame;
        private int _currentIndex;
        private int _millisPerFrame;
        private int _millisSinceLastFrame;

        public Animation(params Rectangle[] frames)
        {
            FramesPerSecond = DefaultFps;
            _animationFrames = frames;
            _frames = _animationFrames.Length;
            _currentFrame = _animationFrames[_currentIndex];
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