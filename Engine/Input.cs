using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Engine
{
    /// <summary>
    ///     Handle XNA input, must be updated and can be accessed statically
    /// </summary>
    public static class Input
    {
        public static KeyboardState PreviousKeyboardState { get; set; }
        public static KeyboardState CurrentKeyboardState { get; set; }

        public static MouseState PreviousMouseState { get; set; }
        public static MouseState CurrentMouseState { get; set; }

        public static void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            Mouse.Position = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public static class Keyboard
        {
            /// <summary>
            ///     Check if key is been pressed
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyDown(Keys key)
            {
                return CurrentKeyboardState.IsKeyDown(key);
            }

            /// <summary>
            ///     Check if key is not been pressed
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyUp(Keys key)
            {
                return !IsKeyDown(key);
            }

            /// <summary>
            ///     Check if key has just been pressed
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyTyped(Keys key)
            {
                return PreviousKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key);
            }

            /// <summary>
            ///     Check if key has just been released
            /// </summary>
            /// <param name="key">Key to check</param>
            public static bool IsKeyReleased(Keys key)
            {
                return CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
            }
        }

        public static class Mouse
        {
            /// <summary>
            /// 
            /// </summary>
            public static Vector2 Position { get; set; }

            /// <summary>
            /// Check if the button is being pressed
            /// </summary>
            /// <param name="mouseButton">Button to check</param>
            /// <param name="mouseState">State to check</param>
            /// <returns>True if button is down on the given State, false otherwise</returns>
            private static bool IsButtonDown(MouseButton mouseButton, MouseState mouseState)
            {
                switch (mouseButton)
                {
                    case MouseButton.Left:
                        return mouseState.LeftButton == ButtonState.Pressed;

                    case MouseButton.Middle:
                        return mouseState.MiddleButton == ButtonState.Pressed;

                    case MouseButton.Right:
                        return mouseState.RightButton == ButtonState.Pressed;

                    default:
                        return false;
                }
            }

            /// <summary>
            /// Check if button is been pressed
            /// </summary>
            /// <param name="mouseButton">Button to check</param>
            /// <returns>True if button is down, false otherwise</returns>
            public static bool IsButtonDown(MouseButton mouseButton)
            {
                return IsButtonDown(mouseButton, CurrentMouseState);
            }

            /// <summary>
            /// Check if button is not been pressed
            /// </summary>
            /// <param name="mouseButton">Button to check</param>
            /// <returns>True if button is up, false otherwise</returns>
            public static bool IsButtonUp(MouseButton mouseButton)
            {
                return !IsButtonDown(mouseButton, CurrentMouseState);
            }

            /// <summary>
            /// Check if button has just been pressed
            /// </summary>
            /// <param name="mouseButton"></param>
            /// <returns></returns>
            public static bool IsButtonClicked(MouseButton mouseButton)
            {
                return IsButtonDown(mouseButton, CurrentMouseState) && !IsButtonDown(mouseButton, PreviousMouseState);
            }

            /// <summary>
            /// Defines mouse button
            /// </summary>
            public enum MouseButton
            {
                /// <summary>
                /// Left mouse button
                /// </summary>
                Left,
                /// <summary>
                /// Right mouse button
                /// </summary>
                Middle,
                /// <summary>
                /// Middle mouse button
                /// </summary>
                Right
            }
        }
    }
}