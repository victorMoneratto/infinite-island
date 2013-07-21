using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland
{
    /// <summary>
    ///     Handle XNA input, must be updated and can be accessed statically
    /// </summary>
    public static class Input
    {
        private static KeyboardState _prevKeyboardState;
        private static KeyboardState _currKeyboardState;

        private static MouseState _prevMouseState;
        private static MouseState _currMouseState;

        public static void Update()
        {
            _prevKeyboardState = _currKeyboardState;
            _currKeyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            _prevMouseState = _currMouseState;
            _currMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public static class Keyboard
        {
            /// <summary>
            ///     Check if key is being pressed right now
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyDown(Keys key)
            {
                return _currKeyboardState.IsKeyDown(key);
            }

            /// <summary>
            ///     Check if key is being not pressed right now
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyUp(Keys key)
            {
                return !IsKeyDown(key);
            }

            /// <summary>
            ///     Check if key has just being pressed
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyTyped(Keys key)
            {
                return _prevKeyboardState.IsKeyUp(key) && _currKeyboardState.IsKeyDown(key);
            }
        }

        public static class Mouse
        {
            public enum MouseButton
            {
                Left,
                Middle,
                Right
            }

            private static bool IsButtonDown(MouseButton mouseButton, MouseState mouseState)
            {
                switch (mouseButton)
                {
                    case MouseButton.Left:
                        return _currMouseState.LeftButton == ButtonState.Pressed;

                    case MouseButton.Middle:
                        return _currMouseState.MiddleButton == ButtonState.Pressed;

                    case MouseButton.Right:
                        return _currMouseState.RightButton == ButtonState.Pressed;

                    default:
                        return false;
                }
            }

            public static bool IsButtonDown(MouseButton mouseButton)
            {
                return IsButtonDown(mouseButton, _currMouseState);
            }

            public static bool IsButtonUp(MouseButton mouseButton)
            {
                return !IsButtonDown(mouseButton, _currMouseState);
            }

            public static bool IsButtonClicked(MouseButton mouseButton)
            {
                return IsButtonDown(mouseButton, _currMouseState) && !IsButtonDown(mouseButton, _prevMouseState);
            }
        }
    }
}