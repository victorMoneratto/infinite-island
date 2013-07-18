using System.Collections.Generic;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Terrain
{
    public class TerrainManager
    {
        private readonly List<TerrainChunk> _chunks = new List<TerrainChunk>();
        private readonly TerrainGenerator _generator = new TerrainGenerator();
        private GraphicsDevice _graphicsDevice;

        private SpriteBatch _spriteBatch;

        //Temporary and should be removed
        private Texture2D _tempTexture;
        private Effect _terrainEffect;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _graphicsDevice = game.GraphicsDevice;
            _tempTexture = new Texture2D(_graphicsDevice, 1, 1);
            _tempTexture.SetData(new[] {Color.White});

            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _terrainEffect = game.Content.Load<Effect>("Terrain");
        }

        public void Generate()
        {
            _chunks.Add(
                _generator.Generate(
                    graphicsDevice: _graphicsDevice,
                    bodyPosition: Vector2.UnitY*3
                    )
                );
        }

        public void Update()
        {
            if (_chunks[0].LastVertex.X.ToPixels() < Camera.Position.X)
            {
                _chunks[0].RemoveFromWorld();
                _chunks.RemoveAt(0);
            }

            if (_chunks[_chunks.Count - 1].LastVertex.X.ToPixels() <= Camera.BottomRight.X)
            {
                _chunks.Add(
                    _generator.Generate(
                        graphicsDevice: _graphicsDevice,
                        bodyPosition: new Vector2(_chunks[_chunks.Count - 1].Rect.BottomRight.X, 3),
                        firstVertexHeight: _chunks[_chunks.Count - 1].LastVertex.Y
                        )
                    );
            }
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _terrainEffect,
                               Camera.CalculateTransformMatrix(Vector2.One));

            foreach (TerrainChunk chunk in _chunks)
            {
                _terrainEffect.Parameters["heightmap"].SetValue(chunk.Heightmap);
                _terrainEffect.Parameters["verticesCount"].SetValue(chunk.VerticesCount);
                _spriteBatch.Draw(_tempTexture, chunk.Rect.ToPixels(), Color.White);
            }

            _spriteBatch.End();
        }
    }
}