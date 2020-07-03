using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
{
    /// <summary>
    /// Defines a accessible mouse button.
    /// </summary>
    enum MouseButton
    {
        Left,
        Right,
        Middle,

        // Special Keys

        X1,
        X2,

        // Amount of Keys

        Keys
    }

    class InputModule
    {

        ButtonState[] m_MouseButton;
        KeyboardState m_Keyboard;
        MouseState m_Mouse;

        bool[] m_KeyboardKeys;
        bool[] m_MouseButtons;

        /// <summary>
        /// Module Initialization.
        /// </summary>
        public InputModule()
        {
            m_Keyboard = new KeyboardState();
            m_Mouse = new MouseState();
            m_MouseButton = new ButtonState[5];

            m_KeyboardKeys = new bool[char.MaxValue];
            m_MouseButtons = new bool[(int)MouseButton.Keys];
        }

        /// <summary>
        ///     Pull next Input Event.
        ///     <para>
        ///         -Keyboard and Mouse 
        ///     </para>
        /// </summary>
        public void Pull_Events()
        {
            m_Keyboard = Keyboard.GetState();
            m_Mouse = Mouse.GetState();

            m_MouseButton[(int)MouseButton.Left] = m_Mouse.LeftButton;
            m_MouseButton[(int)MouseButton.Right] = m_Mouse.RightButton;
            m_MouseButton[(int)MouseButton.Middle] = m_Mouse.MiddleButton;

            m_MouseButton[(int)MouseButton.X1] = m_Mouse.XButton1;
            m_MouseButton[(int)MouseButton.X2] = m_Mouse.XButton2;
        }

        /// <summary>
        /// Check whether or not a specific keyboard Key is being pressed.
        /// </summary>
        /// <param name="key">Monogame Key index</param>
        /// <returns>Current state of Key</returns>
        public bool KeyPress(Keys key)
        {
            return m_Keyboard.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a keyboard key were pressed.
        /// </summary>
        /// <param name="key">Monogame Key index</param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            if (m_Keyboard.IsKeyUp(key))
                m_KeyboardKeys[(int)key] = false;

            if (m_KeyboardKeys[(int)key])
                return false;

            if (KeyPress(key))
                m_KeyboardKeys[(int)key] = true;

            return KeyPress(key);
        }

        /// <summary>
        /// Fetch current Mouse position relative to Viewport.
        /// </summary>
        /// <returns>Current state of Mouse pos</returns>
        public Point MousePos()
        {
            return m_Mouse.Position;
        }

        /// <summary>
        /// Check whether or not a specific mouse button is being pressed.
        /// </summary>
        /// <param name="button">VSL Mouse button index</param>
        /// <returns>Current state of Button</returns>
        public bool MouseButtonPress(MouseButton button)
        {
            return m_MouseButton[(int)button] == ButtonState.Pressed ? true : false;
        }

        /// <summary>
        /// Check if a mouse button were pressed.
        /// </summary>
        /// <param name="button">VSL Mouse button index</param>
        /// <returns></returns>
        public bool MouseButtonPressed(MouseButton button)
        {
            if (m_MouseButton[(int)button] == ButtonState.Released)
                m_MouseButtons[(int)button] = false;

            if (m_MouseButtons[(int)button])
                return false;

            if (MouseButtonPress(button))
                m_MouseButtons[(int)button] = true;

            return MouseButtonPress(button);
        }
    }

}