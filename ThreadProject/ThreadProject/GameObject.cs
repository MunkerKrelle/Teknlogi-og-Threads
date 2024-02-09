using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace ThreadProject
{
    public class GameObject
    {
        protected Vector2 position;
        protected Texture2D sprite;
        private Texture2D background;
        protected SpriteFont font;
        protected float scale = 1;
        private Vector2 backgroundScale;
        protected Vector2 origin;
        private Texture2D mine;
        private Vector2 mineScale;
        protected bool shouldBeRemoved;

        /// <summary>
        /// Used to remove our buttons after being pressed
        /// </summary>
        public bool ShouldBeRemoved { get { return shouldBeRemoved; } }

        protected Color colorCode = Color.White;
        protected Vector2 minPosition;
        protected Vector2 maxPosition;

        public MouseState mouseState;
        public MouseState newState;
        public bool active;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// NOT USED???
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            //sprite = content.Load<Texture2D>("dwarven home");
            //background = content.Load<Texture2D>("grass");
            //backgroundScale = new Vector2(5f,3f);
            ////position = new Vector2(650, 325);
            ////origin = spritesize / 2

            mine = content.Load<Texture2D>("Health");
            mineScale = new Vector2(0.6f);
            font = content.Load<SpriteFont>("File");
        }
        
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws our sprites for the tree and the town hall
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(sprite.Width/2, sprite.Height/2);

            spriteBatch.Draw(sprite, position, null, colorCode, 0, origin, scale, SpriteEffects.None, 1f);
            //spriteBatch.Draw(mine, new Vector2(1250,550), null, Color.White, 0, origin, mineScale, SpriteEffects.None, 1f);
            //spriteBatch.Draw(background, position, null, Color.White, 0, new Vector2(0, 0), backgroundScale, SpriteEffects.None, 1f);
        }
    }
}
