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

            animationContent.Texture = context.BuildAndLoadAsset<TextureContent, TextureContent>(
                sourceAsset: new ExternalReference<TextureContent>(textureFileName),
                processorName: "TextureProcessor");

            //Get dimensions and add animations
            Vector2 dimensions = Vector2.Zero;
            input = input.GetChild("animation");
            animationContent.Animations = new Dictionary<string, Rectangle[]>();

            foreach (Tag animation in input.GetChildren(false))
            {
                IList<Tag> frames = animation.GetChildren("frame", false);
                var rectangles = new Rectangle[frames.Count];

                for (int i = 0; i < rectangles.Length; ++i)
                {
                    rectangles[i] = new Rectangle(
                        x: (int)frames[i].Attributes["left"],
                        y: (int)frames[i].Attributes["top"],
                        width: (int)frames[i].Attributes["width"],
                        height: (int)frames[i].Attributes["height"]);

                    dimensions.X = MathHelper.Max(dimensions.X, (int)frames[i].Attributes["width"]);
                    dimensions.Y = MathHelper.Max(dimensions.Y, (int)frames[i].Attributes["height"]);
                }

                animationContent.Animations[animation.Name] = rectangles;
                animationContent.MaxDimensions = dimensions;
            }

            return animationContent;
        }
    }
}