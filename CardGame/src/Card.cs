using Microsoft.Xna.Framework;

public enum Type
{
    DIAMOND,
    HEARTS,
    SPADES,
    CLUBS
}


public struct Card
{
    public Type m_Type;
    public int m_Value;

    public Card(int val, Type type)
    {
        m_Value = val;
        m_Type = type;
    }

    public static Card Empty { get { return new Card(13, Type.HEARTS); } }

    public static Vector2 m_TextureSize { get { return new Vector2(43, 64); } }

}