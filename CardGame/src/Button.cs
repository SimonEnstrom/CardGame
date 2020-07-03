using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Button
{
    Texture2D m_Texture;
    Rectangle m_Bounds;
    Input.InputModule m_Ipm;

    public Button(string texture, Vector2 pos, Input.InputModule ipm, ContentManager cManager)
    {
        m_Texture = cManager.Load<Texture2D>(texture);    
        m_Bounds = new Rectangle((int)pos.X, (int)pos.Y, m_Texture.Width, m_Texture.Height);
        m_Ipm = ipm;
    }

    public bool Click()
    {
        if (m_Bounds.Contains(m_Ipm.MousePos()) && m_Ipm.MouseButtonPressed(Input.MouseButton.Left))
            return true;
        return false;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(m_Texture, m_Bounds.Location.ToVector2(), Color.White);
    }
}