using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Terrain
{
    internal class Chunk
    {
        private readonly Body _body;
        private readonly RectangleF _rect;

        private readonly Vector2[] _vertices;
        private readonly float _heightMeters = 100f.ToMeters();
        private readonly float _widthMeters = 5000f.ToMeters();


        public Chunk(Vector2 bodyPosition, float[] heights, bool shouldLink = false)
        {
            _rect = new RectangleF(bodyPosition, new Vector2(_widthMeters, _heightMeters));
            _body = BodyFactory.CreateBody(InfiniteIsland.World, bodyPosition);

            int length = heights.Length;
            _vertices = new Vector2[length];

            for (int i = shouldLink? 1 : 0; i < length; i++)
            {
                _vertices[i] = new Vector2((float) i/(length - 1), heights[i]);
                if (i > 0)
                {
                    FixtureFactory.AttachEdge(
                        start: new Vector2(_vertices[i - 1].X*_rect.Dimensions.X, _vertices[i - 1].Y*_rect.Dimensions.Y),
                        end: new Vector2(_vertices[i].X*_rect.Dimensions.X, _vertices[i].Y*_rect.Dimensions.Y),
                        body: _body);
                }
            }
        }

        public RectangleF Rect
        {
            get { return _rect; }
        }

        public Vector2 LastVertice
        {
            get
            {
                return new Vector2(_vertices[_vertices.Length - 1].X*_widthMeters + _body.Position.X,
                                   (_vertices[_vertices.Length - 1].Y*_heightMeters) + _body.Position.Y);
            }
        }
    }
}