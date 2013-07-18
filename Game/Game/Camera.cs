using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game
{
    //This class' atributes should be redesigned for duplicate data removal and matrix buffering
    public static class Camera
    {
        private static Vector2 _center;
        private static Vector2 _dimensions;
        private static float _rotation;
        private static Vector2 _position;
        private static float _zoom = 1f;

        public static void Setup(Rectangle dimensions)
        {
            _dimensions = new Vector2(dimensions.Right, dimensions.Bottom);
            _center = new Vector2(_dimensions.X * .5f, _dimensions.Y * .5f);

            //TODO: this, but better
            MaxBottom = 3 * 64 + 100;
        }

        /// <summary>
        ///     Camera's top-left coordinate (in pixels)
        /// </summary>
        public static Vector2 Position
        {
            get { return _position; }
            set
            {
                if (MaxBottom.HasValue)
                    if (value.Y + _dimensions.Y > MaxBottom)
                        value = new Vector2(value.X, MaxBottom.Value - _dimensions.Y);
                _position = value;
            }
        }

        /// <summary>
        ///     Camera's zoom factor
        /// </summary>
        public static float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        /// <summary>
        ///     Camera's rotation factor (in radians)
        /// </summary>
        public static float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        ///     Camera's bottom-right position of camera.
        /// </summary>
        public static Vector2 BottomRight
        {
            get
            {
                return new Vector2(x: _position.X + _dimensions.X * Zoom,
                                   y: _position.Y + _dimensions.Y * Zoom);
            }
        }

        public static Vector2 Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        public static float? MaxBottom { get; set; }

        /// <summary>
        ///     Calculate the resulting camera matrix for translation, rotation and scale
        /// </summary>
        /// <param name="parallax">Parallax factor, higher values result in higher velocities</param>
        /// <returns>Camera transform matrix</returns>
        public static Matrix CalculateTransformMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-_position * parallax, 0)) *
                   Matrix.CreateTranslation(new Vector3(-_center, 0)) *
                   Matrix.CreateRotationZ(_rotation) *
                   Matrix.CreateScale(_zoom, _zoom, 1) *
                   Matrix.CreateTranslation(new Vector3(_center, 0));
        }

        /// <summary>
        /// Centers the camera on a given position
        /// </summary>
        /// <param name="position">The position to center</param>
        public static void LookAt(Vector2 position)
        {
            Camera.Position = position - new Vector2(_dimensions.X * .5f, _dimensions.Y * .5f);
        }
    }
}