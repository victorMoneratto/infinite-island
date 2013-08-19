using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Interface
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Camera camera);
    }

    public static class DrawableExtension
    {
        public static void Draw(this SpriteBatch spriteBatch, IDrawable drawable, Camera camera)
        {
            drawable.Draw(spriteBatch, camera);
        }
    }
}