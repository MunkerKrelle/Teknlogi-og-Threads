using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace ThreadProject
{
    internal class UI_Manager
    {
        static public int goldAmount = 50;
        static public int woodAmount = 50;
        private int workerPrice;
        private int warriorPrice;
        private Texture2D goldSprite;
        private Texture2D woodSprite;
        private SpriteFont uiFont;
        private Vector2 woodScale;

        public UI_Manager()
        {
            woodScale = new Vector2(0.1f);
        }

        public void LoadContent(ContentManager content)
        {
            goldSprite = content.Load<Texture2D>("Gold");
            woodSprite = content.Load<Texture2D>("Wood");
            uiFont = content.Load<SpriteFont>("File");
        }

        public void DrawResource(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goldSprite, new Vector2(30, 850), Color.White);
            spriteBatch.DrawString(uiFont, "" + goldAmount, new Vector2(70, 880), Color.White);
            spriteBatch.Draw(woodSprite, new Vector2(110, 850), null, Color.White, 0, new Vector2(0, 0), woodScale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(uiFont, "" + woodAmount, new Vector2(150, 880), Color.White);
        }
    }
}
