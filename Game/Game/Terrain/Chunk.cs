using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;

namespace InfiniteIsland.Game.Floor
{
    class Chunk
    {
        private float widthMeters = 5000f.ToMeters();
        private float heightMeters = 100f.ToMeters();
        private readonly RectangleF _rect;
        public RectangleF Rect { get { return _rect; } }
        private readonly Vector2[] _vertices;
        private Body _body;

        

        public Chunk(Vector2 bodyPosition, float[] heights, bool shouldLink = false)
        {
            _rect = new RectangleF(bodyPosition, new Vector2(widthMeters, heightMeters));
            _body = BodyFactory.CreateBody(InfiniteIsland.World, bodyPosition);
            int length = heights.Length;
            _vertices = new Vector2[length];
            int i = 0;
            if (shouldLink)
            {
                bodyPosition = Vector2.Zero;
                ++i;
            }
            for (; i < length; i++)
            {
                _vertices[i] = new Vector2((float)i / (length-1), heights[i]);
                if (i > 0)
                {
                    FixtureFactory.AttachEdge(start: new Vector2(_vertices[i - 1].X * _rect.Dimensions.X, _vertices[i - 1].Y * _rect.Dimensions.Y),
                                              end: new Vector2(_vertices[i].X * _rect.Dimensions.X, _vertices[i].Y * _rect.Dimensions.Y), body: _body);
                }
            }
        }

        public Vector2 LastVertice
        {
            get
            {
                return new Vector2(_vertices[_vertices.Length - 1].X * widthMeters + _body.Position.X,
                    (_vertices[_vertices.Length - 1].Y * heightMeters) + _body.Position.Y);
            }
        }

    }
}