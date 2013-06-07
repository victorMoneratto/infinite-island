using System.Collections.Generic;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Terrain
{
    public class TerrainComponent
    {
        private readonly TerrainGenerator _generator = new TerrainGenerator();
        private readonly List<Chunk> _loadedChunks = new List<Chunk>();

        private Effect _terrainEffect;
        private SpriteBatch _spriteBatch;

        //Temporary and should be removed
        private Texture2D _tempTexture;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _tempTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            _tempTexture.SetData(new[] { Color.White });

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _terrainEffect = game.Content.Load<Effect>("TerrainEffect");
        }

        public void Generate()
        {
            _loadedChunks.Add(new Chunk(Vector2.UnitY * 2, _generator.Generate()));
        }

        public void Update()
        {
            for (int i = 0; i < _loadedChunks.Count; i++)
            {
                if (_loadedChunks[i].LastVertice.X.ToPixels() < InfiniteIsland.Camera.TopLeft.X)
                    _loadedChunks.RemoveAt(i);
                else break;
            }

            if (_loadedChunks[_loadedChunks.Count - 1].LastVertice.X.ToPixels() <= InfiniteIsland.Camera.BottomRight.X)
                _loadedChunks.Add(new Chunk(_loadedChunks[_loadedChunks.Count - 1].LastVertice, _generator.Generate(),
                                            true));
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                                InfiniteIsland.Camera.CalculateViewMatrix(Vector2.One));

            //_effect.Parameters["heightmap"].SetValue(_heightMap);
            //_effect.CurrentTechnique.Passes[0].Apply();
            //_spriteBatch.Draw(_tempTexture, _destinationRect, Color.White);

            foreach (Chunk chunk in _loadedChunks)
            {
                _spriteBatch.Draw(_tempTexture, chunk.Rect.ToPixels(), Color.White);
            }

            _spriteBatch.End();
        }
    }
}