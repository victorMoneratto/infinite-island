namespace InfiniteIsland.Engine.Math
{
    public delegate float ScaleFunc(float progress);
    public delegate T LerpFunc<T>(T start, T end, float progress);

    public class Tween<T> where T : struct
    {
        public T Value { get { return _value; } }
        public TweenState State { get; private set; }

        private readonly LerpFunc<T> _lerpFunc;
        private float _currentTime;
        private float _duration;
        private ScaleFunc _scaleFunc;
        private TweenState _state = TweenState.Stopped;

        private T _start;
        private T _end;
        private T _value;

        public Tween(LerpFunc<T> lerpFunc)
        {
            _lerpFunc = lerpFunc;
        }

        public void Start(T start, T end, float duration, ScaleFunc scaleFunc)
        {
            _currentTime = 0f;
            _duration = duration;
            _scaleFunc = scaleFunc;
            _state = TweenState.Running;

            _start = start;
            _end = end;

            UpdateValue();
        }

        public void Update(float elapsedTime)
        {
            if (_state != TweenState.Running) return;

            _currentTime += elapsedTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                _state = TweenState.Stopped;
            }

            UpdateValue();
        }

        private void UpdateValue()
        {
            _value = _lerpFunc(_start, _end, _scaleFunc(_currentTime/_duration));
        }

        public enum TweenState
        {
            Running, Paused, Stopped
        }
    }

    //public class FloatTween : Tween<float>
    //{
    //}
}