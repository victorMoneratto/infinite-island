using Microsoft.Xna.Framework;

namespace InfiniteIsland
{
    public static class Camera
    {
        private static Vector2 _center;
        private static Vector2 _halfDimensions;

        public static void Setup(Vector2 dimensions)
        {
            Dimensions = dimensions;
            Pivot = _halfDimensions;
            Zoom = Vector2.One;
        }

        /// <summary>
        ///     Camera's center coordinate
        /// </summary>
        public static Vector2 Center
        {
            get { return _center; }
            set
            {
                if (BottomLimit.HasValue)
                    if (value.Y + _halfDimensions.Y > BottomLimit)
                        value = new Vector2(value.X, BottomLimit.Value - _halfDimensions.Y);
                _center = value;
            }
        }

        /// <summary>
        ///     Camera's top-left coordinate
        /// </summary>
        public static Vector2 TopLeft
        {
            get { return _center - _halfDimensions; }
        }

        /// <summary>
        ///     Camera's bottom-right position of camera
        /// </summary>
        public static Vector2 BottomRight
        {
            get { return Center + _halfDimensions; }
        }

        /// <summary>
        ///     Camera's dimensions
        /// </summary>
        public static Vector2 Dimensions
        {
            get { return 2f*_halfDimensions; }
            set { _halfDimensions = .5f*value; }
        }

        /// <summary>
        /// Camera's pivot of rotation
        /// </summary>
        public static Vector2 Pivot { get; set; }

        /// <summary>
        ///     Camera's zoom factor
        /// </summary>
        public static Vector2 Zoom { get; set; }

        /// <summary>
        ///     Camera's rotation angle
        /// </summary>
        public static float Rotation { get; set; }

        /// <summary>
        /// Camera's bottom limit
        /// </summary>
        public static float? BottomLimit { get; set; }

        /// <summary>
        ///     Calculate the resulting camera matrix for translation, rotation and scale
        /// </summary>
        /// <param name="parallax">Parallax factor, higher values result in higher velocities</param>
        /// <returns>Camera transform matrix</returns>
        public static Matrix CalculateTransformMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-TopLeft*parallax, 0))*
                   Matrix.CreateTranslation(new Vector3(-Pivot, 0))*
                   Matrix.CreateRotationZ(Rotation)*
                   Matrix.CreateScale(Zoom.X, Zoom.Y, 1)*
                   Matrix.CreateTranslation(new Vector3(Pivot, 0));
        }
    }
}