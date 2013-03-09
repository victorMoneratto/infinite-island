using Microsoft.Xna.Framework.Input;

namespace DasGame
{
    /// <summary>
    /// Handle XNA input, must be updated and can be accessed statically
    /// </summary>
    internal class Input
    {
        private static KeyboardState _currentState;
        private static KeyboardState _prevState;

        public Input()
        {
            _currentState = Keyboard.GetState();
            _prevState = _currentState;
        }

        public void Update()
        {
            _prevState = _currentState;
            _currentState = Keyboard.GetState();
        }

        /// <summary>
        ///     Check if key is being pressed right now
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        /// <summary>
        ///     Check if key is being not pressed right now
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool IsKeyUp(Keys key)
        {
            return _currentState.IsKeyUp(key);
        }

        /// <summary>
        ///     Check if key has just being pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool IsKeyPressed(Keys key)
        {
            return _prevState.IsKeyUp(key) && _currentState.IsKeyDown(key);
        }

        /// <summary>
        ///     Check if key has just being released
        /// </summary>
        /// <param name="key">Key to check</param>
        public static bool IsKeyReleased(Keys key)
        {
            return _prevState.IsKeyDown(key) && _currentState.IsKeyUp(key);
        }
    }
}