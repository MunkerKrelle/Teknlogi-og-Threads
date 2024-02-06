using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProject
{
    internal class GameObject
    {
        protected Vector2 position;
        protected Texture2D sprite;
        private Texture2D background;
        protected SpriteFont font;
        protected Vector2 scale;
        private Vector2 backgroundScale;
        protected Vector2 origin;
        private Texture2D mine;
        private Vector2 mineScale;

        public virtual void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarven home");
            background = content.Load<Texture2D>("grass");
            backgroundScale = new Vector2(5f,3f);
            position = new Vector2(650, 325);
            //origin = spritesize / 2

            mine = content.Load<Texture2D>("Health");
            mineScale = new Vector2(0.6f);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(mine, new Vector2(1250,550), null, Color.White, 0, origin, mineScale, SpriteEffects.None, 1f);
            //spriteBatch.Draw(background, position, null, Color.White, 0, new Vector2(0, 0), backgroundScale, SpriteEffects.None, 1f);
        }
    }
}
