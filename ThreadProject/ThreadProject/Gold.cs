using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadProject
{
    internal class Gold : Resource
    {
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Minerals");
        }
        
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(1100, 50), Color.White);
        }
    }
}
