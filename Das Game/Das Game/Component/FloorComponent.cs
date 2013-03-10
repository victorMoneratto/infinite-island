using System;
using DasGame.Util;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DasGame.Component
{
    public class FloorComponent
    {
        private readonly Body _body = new Body(HueGame.World);
        private Effect _effect;
        private Texture2D _moss;
        private SpriteBatch _spriteBatch;
        private Vector2 _viewport;
        private Vector2[] _vertices;

        //temp
        readonly float _fixedDistance = MeasureUtil.ToMeters(80);
        private const int VerticesCount = 20;
        //                                /\
        //                                ||
        //Oh, I'm sorry did I break your concentration?                    
        //don't change this without changing the Effect
        //I DARE YOU! I DOUBLE DARE YOU, MOTHERFUCKER!
        

        public void Generate()
        {
            _body.FixtureList.Clear();
            Random random = new Random();
            Vertices vertices = new Vertices(VerticesCount);
            _vertices = new Vector2[vertices.Capacity];
            for (int i = 0; i < vertices.Capacity; ++i)
            {
                vertices.Add(new Vector2(i * _fixedDistance,(float) random.NextDouble()/2f));
                _vertices[i] = new Vector2(MeasureUtil.ToPixels(vertices[i].X), MeasureUtil.ToPixels(vertices[i].Y) + 400);
            }
            for (int i = 0; i < vertices.Capacity - 1; ++i)
            {
                FixtureFactory.AttachEdge(vertices[i], vertices[i + 1], _body);
            }
            _body.Position = new Vector2(0, MeasureUtil.ToMeters(400));
        }

        public void LoadContent(Game game)
        {
            
            _viewport = new Vector2(
                x: game.GraphicsDevice.Viewport.Width,
                y: game.GraphicsDevice.Viewport.Height);

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _effect = game.Content.Load<Effect>("DasEffect");
            _effect.Parameters["viewport"].SetValue(_viewport);
            _effect.Parameters["fixedDistance"].SetValue(MeasureUtil.ToPixels(_fixedDistance));

            _moss = game.Content.Load<Texture2D>("moss");
            _tempTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            _tempTexture.SetData(new[]{Color.White});
        }

        private Texture2D _tempTexture;

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            _effect.Parameters["terrain"].SetValue(_vertices);
            _effect.CurrentTechnique.Passes[0].Apply();
            //_spriteBatch.Draw(_moss, Vector2.Zero, Color.White);
            _spriteBatch.Draw(_tempTexture, new Rectangle(0, 0, (int)_viewport.X, (int)_viewport.Y), Color.White);
            _spriteBatch.End();
        }
    }
}