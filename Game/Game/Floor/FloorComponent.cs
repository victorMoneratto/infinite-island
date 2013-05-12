using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Floor
{
    public class FloorComponent
    {
        private readonly Body _body = new Body(InfiniteIsland.World);
        private Effect _effect;
        private SpriteBatch _spriteBatch;
        //temp
        private Texture2D _tempTexture;
        private Texture2D _heightMap;
        private const int VerticesCount = 10;

        private Rectangle _destinationRect;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _effect = game.Content.Load<Effect>("FloorEffect");

            _heightMap = new Texture2D(game.GraphicsDevice, VerticesCount, 1, false, SurfaceFormat.Vector2);

            _tempTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            _tempTexture.SetData(new[] { Color.PaleGoldenrod });

            _body.Position = Vector2.UnitY * 8;
            _destinationRect = new Rectangle(
                x: 0,
                y: MeasureUtil.ToPixels(_body.Position.Y),
               width:(int) (VerticesCount * VectorHorizontalDistance) * game.GraphicsDevice.Viewport.Width,
               height: game.GraphicsDevice.Viewport.Height/2);
        }

        private const float VectorHorizontalDistance = .1f;

        public void Generate()
        {
            Random random = new Random();
            Vector2[] verticesUV = new Vector2[VerticesCount];
            Vector2 destinationSize = new Vector2(MeasureUtil.ToMeters(_destinationRect.Width), MeasureUtil.ToMeters(_destinationRect.Height));
            for (int i = 0; i < verticesUV.Length; i++)
            {
                float positionY = (float)random.NextDouble()/5f;//temp random usage.
                verticesUV[i] = new Vector2(i * VectorHorizontalDistance, positionY);
                if (i > 0)
                {
                    FixtureFactory.AttachEdge(start: new Vector2(verticesUV[i - 1].X * destinationSize.X,verticesUV[i - 1].Y * destinationSize.Y),
                                              end: new Vector2(verticesUV[i].X * destinationSize.X, verticesUV[i].Y * destinationSize.Y), body: _body);
                }
            }

            _heightMap.SetData(verticesUV);
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, InfiniteIsland.Camera.CalculateViewMatrix(Vector2.One));
            _effect.Parameters["vectorHorizontalDistance"].SetValue(VectorHorizontalDistance);
            _effect.Parameters["heightmap"].SetValue(_heightMap);
            _effect.CurrentTechnique.Passes[0].Apply();
            _spriteBatch.Draw(_tempTexture, _destinationRect, Color.White);
            _spriteBatch.End();
        }
    }
}