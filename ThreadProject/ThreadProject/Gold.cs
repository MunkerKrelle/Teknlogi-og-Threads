using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadProject
{
/// <summary>
/// The Gold class loads the gold mine sprite.
/// </summary>
    internal class Gold : GameObject
    {/// <summary>
    /// Loads the sprite for the gold mine
    /// </summary>
    /// <param name="content"></param> 
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Minerals");
        }

        /// <summary>
        /// Draws the gold mine sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(1100, 50), Color.White);
        }
    }
}
