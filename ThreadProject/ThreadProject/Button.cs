using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace ThreadProject
{
    internal class Button : GameObject
    {
        private Vector2 minPosition;
        private Vector2 maxPosition;
        private Rectangle rectangleForButtons;
        private Color colorCode = Color.White;
        private Vector2 originText;
        public bool active = true;
        public delegate void ButtonFunction();
        public ButtonFunction buttonFunction;
        private SoundEffect buttonSound;

        public MouseState mouseState;
        public MouseState newState;

        public string buttonText;
        public string currentButtonText;
        public Vector2 ButtonPosition
        {
            get { return position; }
            set { position = value; }
        }

        //protected Vector2 SpriteSize
        //{
        //    get
        //    {
        //        return new Vector2(sprite.Width * scale, sprite.Height * scale);
        //    }
        //}

        /// <summary>
        /// Opretter en knap man kan trykke på med musen som kan kører en function
        /// </summary>
        /// <param name="buttonPosition">Knappens position på skærmen</param>
        /// <param name="buttonText">Tekst der bliver skrevet på knappen</param>
        /// <param name="buttonFunction">Hvilken function der bliver kørt når knappen bliver trykket</param>
        public Button(/*Texture2D buttonTexture,*/ Vector2 buttonPosition, string buttonText, ButtonFunction buttonFunction)
        {
            //sprite = buttonTexture;
            scale = 2;
            position = buttonPosition;
            this.buttonText = buttonText;
            this.buttonFunction = buttonFunction;
            currentButtonText = buttonText;
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
        }

        public override void LoadContent(ContentManager content)
        {
            //buttonSound = content.Load<SoundEffect>("Sound\\questClick");

            font = content.Load<SpriteFont>("File");

            sprite = content.Load<Texture2D>("tree-light-green-isaiah658");

            rectangleForButtons = new Rectangle((int)position.X, (int)position.Y, sprite.Width / 2, sprite.Height / 2);

            PositionUpdate();
        }

        /// <summary>
        /// Updates the position of the buandries of the button - where the button can be clicked
        /// </summary>
        public void PositionUpdate()
        {
            minPosition.X = position.X - (sprite.Width / 2);
            minPosition.Y = position.Y - (sprite.Height / 2);
            maxPosition.X = position.X + (sprite.Width * scale);
            maxPosition.Y = position.Y + (sprite.Height * scale);
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

            spriteBatch.Draw(sprite, position, null, colorCode, 0, origin, scale, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, buttonText, position, Color.Black, 0, originText, 1, SpriteEffects.None, 0.1f);
            spriteBatch.DrawString(font, $"({mouseState.X},{mouseState.Y})", new Vector2 (100,100), colorCode);
            spriteBatch.DrawString(font, $"{minPosition},{maxPosition}", new Vector2(100, 200), colorCode);
        }
    }
}
