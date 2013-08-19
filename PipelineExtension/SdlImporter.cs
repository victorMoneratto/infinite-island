using Ikayzo.SDL;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace PipelineExtension
{
    [ContentImporter(".sdl", DisplayName = "Simple Declarative Language")]
    internal class SDLImporter : ContentImporter<Tag>
    {
        public override Tag Import(string filename, ContentImporterContext context)
        {
            return new Tag("root").ReadFile(filename);
        }
    }
}