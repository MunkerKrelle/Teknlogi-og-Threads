using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace ThreadProject
{
    internal class UI_Manager
    {
        private int goldAmount;
        private int woodAmount;
        private int workerPrice;
        private int warriorPrice;
        private Texture2D goldSprite;
        private Texture2D woodSprite;
        private SpriteFont uiFont;
        Thread uiThread;


        public int GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }

        public UI_Manager()
        {
            uiThread = new Thread(() => ThreadGold());
            uiThread.IsBackground = true;
        }

        public void ThreadGold()
        {
            while (true)
            {
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Q))
                {
                    goldAmount += 10;
                }
                else if (keyState.IsKeyDown(Keys.B))
                {
                    goldAmount -= 10;
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            goldSprite = content.Load<Texture2D>("Gold");
            woodSprite = content.Load<Texture2D>("Wood");

            uiFont = content.Load<SpriteFont>("File");
        }

        public void DrawGold(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goldSprite, new Vector2(30, 850), Color.White);
            spriteBatch.DrawString(uiFont, "" + goldAmount, new Vector2(70, 880), Color.White);
        }

        public void Start()
        {
            uiThread.Start();
        }
    }
}
