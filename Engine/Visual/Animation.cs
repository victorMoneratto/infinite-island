using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Visual
{
    public class Animation
    {
        public Dictionary<string, Rectangle[]> Animations;
        public Vector2 MaxDimensions;
        public Texture2D Texture;
    }
}