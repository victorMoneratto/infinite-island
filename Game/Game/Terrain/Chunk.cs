using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Terrain
{
    internal class Chunk
    {
        private readonly Body _body;
        private readonly float _heightMeters = 100f.ToMeters();
        private readonly float[] _heights;
        private readonly RectangleF _rect;
        private readonly float _widthMeters = 5000f.ToMeters();
        private Texture2D _heightmap;

        public Chunk(Vector2 bodyPosition, float[] heights, GraphicsDevice graphicsDevice, bool shouldLink = false)
        {
            _heights = heights;

            _rect = new RectangleF(bodyPosition, new Vector2(_widthMeters, _heightMeters));
            _body = BodyFactory.CreateBody(InfiniteIsland.World, bodyPosition);

            //TODO: Replace if with something clever
            if (shouldLink)
                _heights[0] = 0;

            for (int i = 1, length = _heights.Length; i < length; i++)
            {
                FixtureFactory.AttachEdge(
                    start:
                        new Vector2(
                        x: (i - 1) / (length - 1f) * _widthMeters,
                        y: heights[i - 1] * _heightMeters),
                    end:
                        new Vector2(
                        x: (i) / (length - 1f) * _widthMeters,
                        y: heights[i] * _heightMeters),
                    body: _body);
            }
            BuildHeightmapTexture(graphicsDevice);
        }

        private void BuildHeightmapTexture(GraphicsDevice graphicsDevice)
        {
            _heightmap = new Texture2D(graphicsDevice, _heights.Length, 1, false, SurfaceFormat.Single);
            _heightmap.SetData(_heights);
            //using (System.IO.FileStream fstream = System.IO.File.Create(System.DateTime.Now.ToString("mm-ss") + ".jpg"))
            //{
            //    _heightmap.SaveAsJpeg(fstream, _heightmap.Width, _heightmap.Height);
            //}
        }

        public RectangleF Rect
        {
            get { return _rect; }
        }

        public Vector2 LastVertice
        {
            get
            {
                return new Vector2(_body.Position.X + _widthMeters,
                                   (_body.Position.Y + _heights[_heights.Length - 1] * _heightMeters));
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

        public void RemoveFromWorld()
        {
            InfiniteIsland.World.RemoveBody(_body);
        }
    }
}