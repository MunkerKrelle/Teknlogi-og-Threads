using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProject
{
    internal class TownHall : GameObject
    {
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarven home");
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(600, 400), Color.White);
        }
    }
}
