﻿using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Terrain
{
    internal class TerrainChunk
    {
        public const int HeightCount = 10;
        public const float VerticalPosition = 3f;
        public static readonly Vector2 Dimensions = new Vector2(5000f.ToMeters(), 150f.ToMeters());

        private readonly Body _body;
        private Fixture[] _fixtures;

        private float[] _heights;
        private readonly Texture2D _heightmap;

        private RectangleF _rect;

        public TerrainChunk(float horizonalPosition,
                            float[] heights,
                            GraphicsDevice graphicsDevice,
                            World world,
                            float? firstHeight = null)
        {
            _body = BodyFactory.CreateBody(world, new Vector2(horizonalPosition, VerticalPosition));
            _rect = new RectangleF(Dimensions) { TopLeft = _body.Position };
            _heightmap = new Texture2D(graphicsDevice, HeightCount, 1, false, SurfaceFormat.Single);

            if (firstHeight.HasValue)
                heights[0] = (firstHeight.Value - VerticalPosition)/Dimensions.Y;
            Heights = heights;
        }

        public RectangleF Rect
        {
            get { return _rect; }
        }

        public Vector2 BodyPosition
        {
            get { return _body.Position; }
            set
            {
                _body.Position = value;
                _rect.TopLeft = value;
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
    }
}