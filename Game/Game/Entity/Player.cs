using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Physics;
using InfiniteIsland.Engine.Terrain;
using InfiniteIsland.Engine.Visual;
using InfiniteIsland.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Entity
{
    public class Player
    {
        private const float MaxSpeed = 40f;
        private static Sprite _sprite;

        private static SoundEffect _coinConsumeSound, _jumpSound, _hurtSound;

        public readonly HumanoidBody Body;
        private readonly Play _play;
        public Player(World world, Play play)
        {
            _play = play;
            Body = new HumanoidBody(
                world: world,
                dimensions: _sprite.Animation.MaxDimensions.ToMeters()*.9f,
                userData: this);

            Body.Torso.OnCollision += OnCollision;
            Body.Wheel.OnCollision += Wheel_OnCollision;

            Body.Motor.MotorSpeed = MaxSpeed;
            //_sprite.Key = AnimationKeys.Walk;
        }

        private bool _canJump = true;

        bool Wheel_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            return fixtureB.Body.UserData is TerrainChunk;
        }

        private bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            Coin coin = f2.UserData as Coin;
            if (coin == null)
                return false;

            if (coin.Type == Coin.AnimationKeys.Good)
            {
                _play.RaiseFactor();
            }
            else
            {
                _play.LowerFactor();
            }
            _play.Entities.Coins.Remove(coin, _play.World);
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Space) && _canJump)
            {
                _canJump = false;
                Wait.Until(time => time.Alive > .75f, () => _canJump = true);
                _jumpSound.Play(1f, 1f, 0f);
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -30));
            }

            _sprite.Update(gameTime);
            _sprite.Body.TopRight = Body.TopMiddle.ToPixels() + new Vector2(_sprite.Body.Dimensions.X/2f, 0);
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void LoadContent(ContentManager content)
        {
            
            _jumpSound = content.Load<SoundEffect>("sfx/jump");
            _sprite = new Sprite(content.Load<Animation>("sprite/kraemer")){FramesPerSecond = 13};
        }

        //private struct AnimationKeys
        //{
        //    public const string Walk = "walk";
        //    public const string Stand = "stand";
        //    public const string Jump = "jump";
        //}
    }
}