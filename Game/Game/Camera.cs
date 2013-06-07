using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game
{
    public class Camera
    {
        private Vector2 _center;
        private Point _dimensions;
        private float _rotation;
        private Vector2 _topLeft;
        private float _zoom;

        public Camera(Rectangle dimensions)
        {
            _dimensions = new Point(dimensions.Right, dimensions.Bottom);
            Center = new Vector2(_dimensions.X*.5f, _dimensions.Y*.5f);
            Zoom = 1.0f;
        }

        /// <summary>
        ///     Camera's top-left coordinate (in pixels)
        /// </summary>
        public Vector2 TopLeft
        {
            get { return _topLeft; }
            set { _topLeft = value; }
        }

        /// <summary>
        ///     Camera's center coordinate (in pixels)
        /// </summary>
        public Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        /// <summary>
        ///     Camera's zoom factor
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        /// <summary>
        ///     Camera's rotation factor (in radians)
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        ///     Camera's bottom-right position of camera.
        /// </summary>
        public Vector2 BottomRight
        {
            get { return new Vector2(_topLeft.X + _dimensions.X*Zoom, _topLeft.Y + _dimensions.Y*Zoom); }
        }

        /// <summary>
        /// Calculate the resulting camera matrix for translation, rotation and scale
        /// </summary>
        /// <param name="parallax">Parallax factor, higher values result in higher velocities</param>
        /// <returns></returns>
        public Matrix CalculateViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-_topLeft*parallax, 0))*
                   Matrix.CreateTranslation(new Vector3(-_center, 0))*
                   Matrix.CreateRotationZ(_rotation)*
                   Matrix.CreateScale(_zoom, _zoom, 1)*
                   Matrix.CreateTranslation(new Vector3(_center, 0));
        }

        public void LookAt(Vector2 position)
        {
            TopLeft = position - new Vector2(_dimensions.X*.5f, _dimensions.Y*.5f);
        }
    }
}