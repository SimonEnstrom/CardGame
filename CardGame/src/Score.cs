using Microsoft.Xna.Framework.Graphics;
using System;

namespace CardGame
{
    class Scoreboard
    {
        private int m_Score;
        public Scoreboard() { m_Score = 0; }

       public void Draw(SpriteBatch sb, SpriteFont sf)
        {
            sb.DrawString(sf, "Score: " + m_Score.ToString(), new Microsoft.Xna.Framework.Vector2(325, 30), Microsoft.Xna.Framework.Color.BlanchedAlmond);
        }

        public void win()
        {
            m_Score ++;
        }

        public void Lose()
        {
            m_Score --;
        }
    }
}

