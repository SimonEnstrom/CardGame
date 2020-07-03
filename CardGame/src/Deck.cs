using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class Deck
{
    List<Card> m_Deck;
    List<Card> m_DeckDrawn;

    const int m_DeckMaxCap = 52;
    Random m_Rand;
    Texture2D m_TextureAtlas;


    ContentManager m_cM;

    public Deck(ContentManager cManager, Random r) {
        m_Deck = new List<Card>();
        m_DeckDrawn = new List<Card>();
        m_Rand = r;
        m_cM = cManager;

        m_TextureAtlas = cManager.Load<Texture2D>("tinycards");
    }

    public void GenerateNewDeck()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < m_DeckMaxCap / 4; j++)
                m_Deck.Add(new Card(j, (Type)i));

        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        Deck tmp = new Deck(m_cM, m_Rand);
        for (int i = 0; i < m_DeckMaxCap; i++)
        {
            int rand = m_Rand.Next(0, m_DeckMaxCap - i) % 52;

            tmp.m_Deck.Add(m_Deck[rand]);
            m_Deck.RemoveAt(rand);
        }

        int rng = m_Rand.Next(0, m_DeckMaxCap - 1);

        m_Deck = tmp.m_Deck;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(m_TextureAtlas, new Vector2(20, 20), Color.White);
    }

    public void DrawCard(SpriteBatch sb, Card c, Vector2 pos)
    {
        float offsY = Card.m_TextureSize.Y * (int)c.m_Type;
        float offsX = Card.m_TextureSize.X * c.m_Value;

        Rectangle Rect = new Rectangle((int)offsX, (int)offsY, (int)Card.m_TextureSize.X, (int)Card.m_TextureSize.Y);

        sb.Draw(m_TextureAtlas, pos, Rect, Color.White);

    }

    public void DrawDrawnCards(SpriteBatch sb)
    {
        float ysize = 472;
        for (float i = 0; i < m_DeckDrawn.Count; i++)
        {
            if (i % 18 == 0)
                ysize -= Card.m_TextureSize.Y;
            DrawCard(sb, m_DeckDrawn[(int)i], new Vector2((i % 18 * (Card.m_TextureSize.X)) + 13, ysize));
        }
                     
    }

    public Card NextCard()
    {
        if (!EmptyDeck())
        {
            int FirstCardIndex = m_Deck.Count - 1;
            Card tmp = m_Deck[FirstCardIndex];

            m_Deck.RemoveAt(m_Deck.Count - 1);
            m_DeckDrawn.Add(tmp);
            return tmp;
        }

        return Card.Empty;
    }

    public bool EmptyDeck()
    {
        return m_Deck.Count <= 0 ? true : false;
    }

}