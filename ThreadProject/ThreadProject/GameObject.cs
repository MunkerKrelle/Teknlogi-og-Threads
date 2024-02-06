using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace ThreadProject
{
    internal class GameObject
    {
        protected Vector2 position;
        protected Texture2D sprite;
        private Texture2D background;
        protected SpriteFont font;
        protected float scale;
        private Vector2 backgroundScale;
        protected Vector2 origin;
        private Texture2D mine;
        private Vector2 mineScale;

        protected Vector2 CurrentPosition
        {
            get
            {
                return new Vector2(position.X + (sprite.Width * scale/2), position.Y + (sprite.Height * scale/2));
            }
        }
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 1f);
            //spriteBatch.Draw(mine, new Vector2(1250,550), null, Color.White, 0, origin, mineScale, SpriteEffects.None, 1f);
            //spriteBatch.Draw(background, position, null, Color.White, 0, new Vector2(0, 0), backgroundScale, SpriteEffects.None, 1f);
        }
    }
}
