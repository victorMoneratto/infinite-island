using FarseerPhysics.Dynamics;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
namespace InfiniteIsland.Game.Floor
{
    public class FloorComponent
    {
        public void Generate()
        {
            var gen = new FloorGenerator();
            new Chunk(Vector2.UnitY*8, gen.Generate());
        }

        public void Draw()
        {
            //_spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
            //                   InfiniteIsland.Camera.CalculateViewMatrix(Vector2.One));
            //_effect.Parameters["heightmap"].SetValue(_heightMap);
            //_effect.CurrentTechnique.Passes[0].Apply();
            //_spriteBatch.Draw(_tempTexture, _destinationRect, Color.White);
            //_spriteBatch.End();
        }
    }
}