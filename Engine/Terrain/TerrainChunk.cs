using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Terrain
{
    public class TerrainChunk
    {
        public const int HeightCount = 10;
        public const float VerticalPosition = 3f;
        public static readonly Vector2 Dimensions = new Vector2(5000f.ToMeters(), 150f.ToMeters());

        private readonly Body _body;
        private readonly RectangleF _bounds;
        private readonly Texture2D _heightmap;
        private Fixture[] _fixtures;

        private float[] _heights;

        public TerrainChunk(float horizonalPosition,
                            float[] heights,
                            GraphicsDevice graphicsDevice,
                            World world,
                            float? firstHeight = null)
        {
            _body = BodyFactory.CreateBody(world, new Vector2(horizonalPosition, VerticalPosition));
            _bounds = new RotatableRectangleF(Dimensions) {TopLeft = _body.Position};
            _heightmap = new Texture2D(graphicsDevice, HeightCount, 1, false, SurfaceFormat.Single);

            if (firstHeight.HasValue)
                heights[0] = (firstHeight.Value - VerticalPosition)/Dimensions.Y;
            Heights = heights;
        }

        public RectangleF Bounds
        {
            get { return _bounds; }
        }

        public Vector2 BodyPosition
        {
            get { return _body.Position; }
            set
            {
                _body.Position = value;
                _bounds.TopLeft = value;
            }
        }

        public float[] Heights
        {
            get { return _heights; }
            set
            {
                _heights = value;

                if (_fixtures != null)
                {
                    foreach (Fixture fixture in _fixtures)
                    {
                        _body.DestroyFixture(fixture);
                    }
                }

                _fixtures = new Fixture[_heights.Length - 1];
                for (int i = 1, length = _heights.Length; i < length; i++)
                {
                    _fixtures[i - 1] =
                        FixtureFactory.AttachEdge(
                            start:
                                new Vector2(
                                x: (i - 1)/(length - 1f)*Dimensions.X,
                                y: _heights[i - 1]*Dimensions.Y),
                            end:
                                new Vector2(
                                x: (i)/(length - 1f)*Dimensions.X,
                                y: _heights[i]*Dimensions.Y),
                            body: _body);
                }
                _heightmap.SetData(_heights);
            }
        }

        public Vector2 LastVertex
        {
            get
            {
                return new Vector2(_body.Position.X + _bounds.Dimensions.X,
                                   _body.Position.Y + _heights[_heights.Length - 1]*_bounds.Dimensions.Y);
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
    }
}