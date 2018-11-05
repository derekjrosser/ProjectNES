using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectNES
{
    public class Player
    {
        public Point position;
        public float gravity = 0f;
        public float gravityMax = 1f;
        public Texture2D playerTex;
        public int tileSize = 8;
        public SpriteBatch _s;
        public int movespeed = 1;


        public void Draw()
        {
            _s.Draw(playerTex, new Rectangle(position.X, position.Y, tileSize, tileSize), Color.White);
        }
    }
}
