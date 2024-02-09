using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThreadProject
{/// <summary>
/// The GameObject class is the main class our gameObjects inherit from, and they use base variables like sprite, position, 
/// scale, origin etc.
///
/// </summary>
    public class GameObject
    {
        protected Vector2 position;
        protected Texture2D sprite;
        protected SpriteFont font;
        protected float scale = 1;
        protected Vector2 origin;
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
        /// Placeholder for future LoadContent
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
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
        }
    }
}
