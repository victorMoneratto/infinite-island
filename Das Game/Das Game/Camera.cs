using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game
{
    public class Camera
    {
        private Viewport _viewport;

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            Origin = new Vector2(viewport.Width*.5f, viewport.Height*.5f);
            Zoom = 1f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix CalculateViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0))*
                   Matrix.CreateTranslation(new Vector3(-Origin, 0))*
                   Matrix.CreateRotationZ(Rotation)*
                   Matrix.CreateScale(Zoom, Zoom, 1)*
                   Matrix.CreateTranslation(new Vector3(Origin, 0));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_viewport.Width*.5f, _viewport.Height*.5f);
        }
    }
}