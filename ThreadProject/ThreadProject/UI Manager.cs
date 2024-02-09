using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace ThreadProject
{
    /// <summary>
    /// This class is used for managing our resources, making them dynamic to update in sync with the workers delivering resources to our town hall
    /// It's also used for drawing and scaling our resource sprites
    /// </summary>
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
            //Scaling the Wood sprite (Log resource icon) to 10% of original size, as it was too big
            woodScale = new Vector2(0.1f);
        }

        /// <summary>
        /// Loading the files for our resources, Coins and Wood, + the spritefont used to display resource values
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            goldSprite = content.Load<Texture2D>("Gold");
            woodSprite = content.Load<Texture2D>("Wood");
            uiFont = content.Load<SpriteFont>("File");
        }

        /// <summary>
        /// As the name implies this draws our resources, where the wood sprite uses a woodScale
        /// The goldAmount and woodAmount in our DrawStrings are used for making the resource values dynamic to update the accordingly in game
        /// Everything is drawn to the bottom left corner, with the aim of making our UI easier to navigate
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawResource(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goldSprite, new Vector2(30, 850), Color.White);
            spriteBatch.DrawString(uiFont, "" + goldAmount, new Vector2(70, 880), Color.White);
            spriteBatch.Draw(woodSprite, new Vector2(110, 850), null, Color.White, 0, new Vector2(0, 0), woodScale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(uiFont, "" + woodAmount, new Vector2(150, 880), Color.White);
        }
    }
}
