using System;
using FarseerPhysics.Dynamics;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Terrain : IUpdateable, IDrawable
    {
        private readonly Texture2D _blankTexture;
        private readonly TerrainChunk[] _chunks;

        private readonly Effect _terrainEffect;

        public Terrain(Game game, World world)
        {
            _chunks = new TerrainChunk[game.GraphicsDevice.Viewport.Width/(int) TerrainChunk.Dimensions.X + 1];
            _chunks[0] = new TerrainChunk(0, Noise.Generate(TerrainChunk.HeightCount), game.GraphicsDevice, world);
            for (int i = 1; i < _chunks.Length; i++)
            {
                _chunks[i] = new TerrainChunk(_chunks[i - 1].LastVertex.X, Noise.Generate(TerrainChunk.HeightCount),
                                              game.GraphicsDevice, world, _chunks[i - 1].LastVertex.Y);
            }

            _blankTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] {Color.White});

            _terrainEffect = game.Content.Load<Effect>("Terrain");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _terrainEffect,
                              InfiniteIsland.Camera.CalculateTransformMatrix(Vector2.One));

            foreach (TerrainChunk chunk in _chunks)
            {
                _terrainEffect.Parameters["heightmap"].SetValue(chunk.Heightmap);
                _terrainEffect.Parameters["verticesCount"].SetValue(chunk.VerticesCount);
                spriteBatch.Draw(_blankTexture, chunk.Bounds.ToPixels(), Color.White);
            }

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            TerrainChunk firstChunk = _chunks[0];
            if (firstChunk.LastVertex.X.ToPixels() < InfiniteIsland.Camera.Viewport.Projection.BoundingBox.TopLeft.X)
            {
                firstChunk.BodyPosition = _chunks[_chunks.Length - 1].LastVertex;
                firstChunk.Heights = Noise.Generate(TerrainChunk.HeightCount);

                Array.Copy(_chunks, 1, _chunks, 0, _chunks.Length - 1);
                _chunks[_chunks.Length - 1] = firstChunk;
            }
        }
    }
}