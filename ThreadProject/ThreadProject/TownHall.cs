using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            //Structures.level++;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(500, 400), Color.White);
        }
    }
}
