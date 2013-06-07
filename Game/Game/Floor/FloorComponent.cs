using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace InfiniteIsland.Game.Floor
{
    public class FloorComponent
    {
        private readonly List<Chunk> _loadedChunks = new List<Chunk>();

        public void Generate()
        {
            var gen = new FloorGenerator();
            _loadedChunks.Add(new Chunk(Vector2.UnitY*8, new FloorGenerator().Generate()));
            _loadedChunks.Add(new Chunk(_loadedChunks[0].LastVertice, new FloorGenerator().Generate(), true));
        }

        public void Update()
        {
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