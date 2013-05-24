using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Game.Floor
{
    internal class Chunk
    {
        private readonly float _heightMeters = 100f.ToMeters();
        private readonly float _widthMeters = 10000f.ToMeters();

        private readonly Body _body;
        private readonly RectangleF _rect;
        private readonly Vector2[] _vertices;


        public Chunk(Vector2 bodyPosition, params float[] heights)
        {
            _rect = new RectangleF(bodyPosition, new Vector2(_widthMeters, _heightMeters));
            _body = BodyFactory.CreateBody(InfiniteIsland.World, bodyPosition);
            int length = heights.Length;
            _vertices = new Vector2[length];
            for (int i = 0; i < length; i++)
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
    }
}