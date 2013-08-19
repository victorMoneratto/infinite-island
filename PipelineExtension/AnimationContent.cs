using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace PipelineExtension
{
    [ContentSerializerRuntimeType("InfiniteIsland.Engine.Visual.Animation, Engine")]
    internal class AnimationContent
    {
        public Dictionary<string, Rectangle[]> Animations;
        public Vector2 MaxDimensions;
        public TextureContent Texture;
    }
}