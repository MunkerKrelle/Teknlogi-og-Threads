using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadProject
{
    internal class Gold : Resource
    {
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Minerals");
            scale = new Vector2(0.1f);
        }
        
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(1450, 650), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(sprite, new Vector2(1450, 750), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(sprite, new Vector2(1350, 650), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(sprite, new Vector2(1350, 750), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0.1f);

        }
    }
}
