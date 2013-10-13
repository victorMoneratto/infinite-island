using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Component;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Math.Geometry;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    public class Coins
    {
        private readonly List<Coin> _coins = new List<Coin>(20);
        private float _milis;

        public void Update(GameTime gameTime, CameraOperator cameraOperator, Entities entities, World world)
        {
            foreach (Coin coin in _coins)
            {
                coin.Update(gameTime);
            }

            _milis += gameTime.ElapsedGameTime.Milliseconds*1e-3f;
            if (_milis > .5f)
            {
                --_milis;

                RotatableRectangleF cameraViewport = cameraOperator.Camera.Viewport;
                if (_coins.Count == _coins.Capacity)
                    Remove(_coins[0], world);

                //select a way to launch
                if (InfiniteIsland.Random.Next(2) == 0)
                {
                    //Launch from the left
                    _coins.Add(
                        new Coin(
                            position:
                                (cameraViewport.TopLeft +
                                 new Vector2(0,
                                     (float) InfiniteIsland.Random.NextDouble()*
                                     cameraViewport.Dimensions.Y*.75f)).ToMeters(),
                            linearVelocity:
                                entities.Player.Body.Wheel.LinearVelocity*
                                new Vector2(1.5f + (float) InfiniteIsland.Random.NextDouble()/2.5f,
                                    -2*(float) InfiniteIsland.Random.NextDouble()), world: world));
                }
                else
                {
                    //Launch from the right
                    _coins.Add(
                        new Coin(
                            position:
                                (cameraViewport.TopRight +
                                new Vector2(0, (float) InfiniteIsland.Random.NextDouble()*cameraViewport.Dimensions.Y*.75f)).ToMeters(),
                            linearVelocity: 
                                entities.Player.Body.Wheel.LinearVelocity*
                                (new Vector2(
                                    x: 1.5f + (float) InfiniteIsland.Random.NextDouble()/2.5f,
                                    y:-2*(float) InfiniteIsland.Random.NextDouble()) - Vector2.UnitX),
                            world: world));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator, bool revealCoins)
        {
            if (revealCoins)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                    cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
                foreach (Coin coin in _coins)
                {
                    coin.Sprite.Key = coin.Type;
                    Vector2 badOffset = Vector2.Zero;
                    if (coin.Type == Coin.AnimationKeys.Bad)
                    {
                        badOffset = new Vector2(
                            x: (float) (InfiniteIsland.Random.NextDouble() - .5f)*15,
                            y: (float) (InfiniteIsland.Random.NextDouble() - .5f)*15);
                        coin.Sprite.Body.Center += badOffset;
                    }

                    coin.Sprite.Body.Pivot -= Vector2.One*3;
                    coin.Sprite.Body.Center -= Vector2.One*3;
                    coin.Draw(spriteBatch);
                    coin.Sprite.Body.Center += Vector2.One*3;
                    coin.Sprite.Body.Pivot += Vector2.One*3;
                    coin.Sprite.Body.Center -= badOffset;
                }
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                    cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
                foreach (Coin coin in _coins)
                {
                    coin.Sprite.Key = Coin.AnimationKeys.Outline;
                    coin.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }

        public void Remove(Coin coin, World world)
        {
            world.RemoveBody(coin.Body);
            _coins.Remove(coin);
        }

        public static void LoadContent(ContentManager content)
        {
            Coin.LoadContent(content);
        }
    }

    public class Coin
    {
        private const float Scale = 2f;
        private static Animation _animation;

        public readonly Body Body;
        public readonly Sprite Sprite;
        public readonly string Type;

        public Coin(Vector2 position, Vector2 linearVelocity, World world)
        {
            Body = BodyFactory.CreateCircle(
                world: world,
                radius: (_animation.MaxDimensions.X/2f).ToMeters()*Scale,
                density: 1f,
                position: position,
                userData: this);
            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 1.1f;
            Body.LinearVelocity = linearVelocity;

            Sprite = new Sprite(_animation)
            {
                Body =
                {
                    Scale = new Vector2(Scale),
                }
            };
            Type = InfiniteIsland.Random.Next(2) == 0 ? AnimationKeys.Bad : AnimationKeys.Good;
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Body.Center = Body.Position.ToPixels();
            Sprite.Body.Rotation = Body.Rotation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _animation = content.Load<Animation>("sprite/coin");
        }

        public struct AnimationKeys
        {
            public const string Outline = "outline";
            public const string Good = "good";
            public const string Bad = "bad";
        }
    }
}