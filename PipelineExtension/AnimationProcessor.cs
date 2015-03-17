using System.Collections.Generic;
using Ikayzo.SDL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace PipelineExtension
{
    [ContentProcessor(DisplayName = "Animation - Sprite Sheet")]
    internal class AnimationProcessor : ContentProcessor<Tag, AnimationContent>
    {
        public override AnimationContent Process(Tag input, ContentProcessorContext context)
        {
            var animationContent = new AnimationContent();
            //Load texture
            var textureFileName = input.GetChild("image").Value as string;

            animationContent.Texture =
                context.BuildAndLoadAsset<TextureContent, TextureContent>(
                    new ExternalReference<TextureContent>(textureFileName), "TextureProcessor");

            //Get dimensions and add animations
            var dimensions = Vector2.Zero;
            input = input.GetChild("animation");
            animationContent.Animations = new Dictionary<string, Rectangle[]>();

            foreach (var animation in input.GetChildren(false))
            {
                var frames = animation.GetChildren("frame", false);
                var rectangles = new Rectangle[frames.Count];

                for (var i = 0; i < rectangles.Length; ++i)
                {
                    rectangles[i] = new Rectangle((int) frames[i].Attributes["left"], (int) frames[i].Attributes["top"],
                        (int) frames[i].Attributes["width"], (int) frames[i].Attributes["height"]);

                    dimensions.X = MathHelper.Max(dimensions.X, (int) frames[i].Attributes["width"]);
                    dimensions.Y = MathHelper.Max(dimensions.Y, (int) frames[i].Attributes["height"]);
                }

                animationContent.Animations[animation.Name] = rectangles;
                animationContent.MaxDimensions = dimensions;
            }

            return animationContent;
        }
    }
}