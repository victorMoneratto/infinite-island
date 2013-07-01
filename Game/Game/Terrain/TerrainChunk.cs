using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Terrain
{
    internal class TerrainChunk
    {
        private readonly Body _body;

        private readonly float[] _heights;
        private readonly RectangleF _rect;

        private Texture2D _heightmap;

        public TerrainChunk(Vector2 bodyPosition,
                            float width,
                            float height,
                            float[] heights,
                            GraphicsDevice graphicsDevice)
        {
            _heights = heights;
            _rect = new RectangleF(bodyPosition, new Vector2(width, height));
            _body = BodyFactory.CreateBody(InfiniteIsland.World, bodyPosition);

            for (int i = 1, length = _heights.Length; i < length; i++)
            {
                FixtureFactory.AttachEdge(
                    start:
                        new Vector2(
                        x: (i - 1)/(length - 1f)*width,
                        y: heights[i - 1]*height),
                    end:
                        new Vector2(
                        x: (i)/(length - 1f)*width,
                        y: heights[i]*height),
                    body: _body);
            }

            BuildHeightmapTexture(graphicsDevice);
        }

        public RectangleF Rect
        {
            get { return _rect; }
        }

        public Vector2 LastVertex
        {
            get
            {
                return new Vector2(_body.Position.X + _rect.Dimensions.X,
                                   _body.Position.Y + _heights[_heights.Length - 1]*_rect.Dimensions.Y);
            }
        }

        public Texture2D Heightmap
        {
            get { return _heightmap; }
        }

        public int VerticesCount
        {
            get { return _heights.Length; }
        }

        private void BuildHeightmapTexture(GraphicsDevice graphicsDevice)
        {
            _heightmap = new Texture2D(graphicsDevice, _heights.Length, 1, false, SurfaceFormat.Single);
            _heightmap.SetData(_heights);
        }

        public void RemoveFromWorld()
        {
            InfiniteIsland.World.RemoveBody(_body);
        }
    }
}