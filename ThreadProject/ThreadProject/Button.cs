using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ThreadProject
{
    /// <summary>
    /// The button class is a GameObject that can be clicked on and can then hold and run a specific function
    /// </summary>
    internal class Button : GameObject
    {
        private Vector2 originText;
        public delegate void ButtonFunction();
        public ButtonFunction buttonFunction;

        public string buttonText;

        /// <summary>
        /// Opretter en knap man kan trykke på med musen som kan kører en function
        /// </summary>
        /// <param name="buttonPosition">Knappens position på skærmen</param>
        /// <param name="buttonText">Tekst der bliver skrevet på knappen</param>
        /// <param name="buttonFunction">Hvilken function der bliver kørt når knappen bliver trykket</param>
        public Button(/*Texture2D buttonTexture,*/ Vector2 buttonPosition, string buttonText, ButtonFunction buttonFunction)
        {
            //sprite = buttonTexture; // Can give the buttons unique sprites if required 
            scale = 0.5f;
            position = buttonPosition;
            this.buttonText = buttonText;
            this.buttonFunction = buttonFunction;
            active = true;
    }

        public override void Update(GameTime gameTime)
        {
            MouseOnButton();
            mouseState = Mouse.GetState();

            if (active)
            {
                if (mouseState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
                {
                    MousePressed();
                }
                newState = mouseState;
            }

            PositionUpdate();
        }

        /// <summary>
        /// Loads the necessary sprites for the button and the text
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("File");

            sprite = content.Load<Texture2D>("button");
        }

        /// <summary>
        /// Updates the position of the buandries of the button - where the button can be clicked
        /// </summary>
        public void PositionUpdate()
        {
            minPosition.X = position.X - (sprite.Width / 2);
            minPosition.Y = position.Y - (sprite.Height / 2);
            maxPosition.X = position.X + (sprite.Width/2 * scale);
            maxPosition.Y = position.Y + (sprite.Height/2 * scale);
        }

        /// <summary>
        /// Checks if the cursor is on an object and turns it gray 
        /// </summary>
        public void MouseOnButton()
        {
                if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
                {
                    colorCode = Color.LightGray;
                }
                else
                {
                    colorCode = Color.White;
                }
        }

        /// <summary>
        /// Checks if the mouse is pressed on a pressabled object
        /// </summary>
        public void MousePressed()
        {
            if (!active)
            {
                return;
            }
            if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
            {
                //buttonSound.Play();
                colorCode = Color.Yellow;
                buttonFunction.Invoke(); //Kører functionen som knappen indeholder. For at kunne kører en delegate, kan man bruge .Invoke()
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 fontLength = font.MeasureString(buttonText);

            originText = new Vector2(fontLength.X / 2f, fontLength.Y / 2f);

            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            if (active)
            {
                spriteBatch.Draw(sprite, position, null, colorCode, 0, origin, scale, SpriteEffects.None, 1f);

                spriteBatch.DrawString(font, buttonText, position, Color.Black, 0, originText, 1, SpriteEffects.None, 0.1f);
            }
        }

        public void RemoveObject()
        {
            shouldBeRemoved = true;
        }
    }
}
