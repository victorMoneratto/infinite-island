using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace InfiniteIsland.Game.Visual
{
    class Layer
    {
        public delegate void UpdateLayer(GameTime gameTime);
        public delegate void DrawLayer(SpriteBatch spriteBatch);
        
        public UpdateLayer Update;
        public DrawLayer Draw;

        public Vector2 Parallax { get; set; }
    }

    static class Layers
    {
        static Layers()
        {
            new List<Layer>()
            {
                new Layer()
                {
                    Update = delegate(GameTime gameTime)
                    {
                    },
                    Draw = delegate(SpriteBatch spriteBatch)
                    {
                    }
                },
            };
        }
    }
}
