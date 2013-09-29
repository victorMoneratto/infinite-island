using System;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Terrain : IUpdateable, IDrawable
    {
        public static Terrain Instance;

        private static Effect _terrainEffect;
        private readonly Texture2D _blankTexture;
        private readonly TerrainChunk[] _chunks;

        public Terrain(GraphicsDevice graphics)
        {
            //T_T SAD it doesn't work yet T_T
            _chunks = new TerrainChunk[graphics.Viewport.Width/TerrainChunk.Dimensions.X.ToPixels() + 20];
            _chunks[0] = new TerrainChunk(0, Noise.Generate(TerrainChunk.HeightCount), graphics, InfiniteIsland.World);

            for (int i = 1; i < _chunks.Length; i++)
            {
                _chunks[i] = new TerrainChunk(_chunks[i - 1].LastVertex.X, Noise.Generate(TerrainChunk.HeightCount),
                                              graphics, InfiniteIsland.World, _chunks[i - 1].LastVertex.Y);
            }

            _blankTexture = new Texture2D(graphics, 1, 1);
            _blankTexture.SetData(new[] {Color.White});
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _terrainEffect,
                              camera.CalculateTransformMatrix(Vector2.One));

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
            if (firstChunk.LastVertex.X.ToPixels() <
                CameraOperator.Instance.Camera.Viewport.Projection.BoundingBox.TopLeft.X)
            {
                firstChunk.BodyPosition = _chunks[_chunks.Length - 1].LastVertex;
                firstChunk.Heights = Noise.Generate(TerrainChunk.HeightCount);

                Array.Copy(_chunks, 1, _chunks, 0, _chunks.Length - 1);
                _chunks[_chunks.Length - 1] = firstChunk;
            }
        }

        public static void LoadContent(ContentManager content)
        {
            _terrainEffect = content.Load<Effect>("Terrain");
        }
    }
}