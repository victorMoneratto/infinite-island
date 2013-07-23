namespace InfiniteIsland.Engine.Math.Geometry
{
    public class BoundingLimits
    {
        private float? _down;
        private float? _left;
        private float? _right;
        private float? _up;

        public float? Up
        {
            get { return _up; }
            set
            {
                if (!value.HasValue || !_up.HasValue || value.Value < _up.Value)
                    _up = value;
            }
        }

        public float? Down
        {
            get { return _down; }
            set
            {
                if (!value.HasValue || !_down.HasValue || value.Value > _down.Value)
                    _down = value;
            }
        }

        public float? Left
        {
            get { return _left; }
            set
            {
                if (!value.HasValue || !_left.HasValue || value.Value < _left.Value)
                    _left = value;
            }
        }

        public float? Right
        {
            get { return _right; }
            set
            {
                if (!value.HasValue || !_right.HasValue || value.Value < _right.Value)
                    _right = value;
            }
        }
    }
}