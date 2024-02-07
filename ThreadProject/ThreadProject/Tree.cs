using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadProject
{
    internal class Tree : Resource
    {
        private Texture2D[] trees;
        private Random rnd = new Random();


        public Tree()
        {
            position.X = rnd.Next(10, 200);
            position.Y = rnd.Next(600, 800);
        }

        public override void LoadContent(ContentManager content)
        {
            trees = new Texture2D[2];
            trees[0] = content.Load<Texture2D>("tree-light-green-isaiah658");
            trees[1] = content.Load<Texture2D>("tree-dark-green-isaiah658");
        }

        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(trees[0], position, null, Color.White, 0, origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(trees[1], position, null, Color.White, 0, origin, 1f, SpriteEffects.None, 1f);
        }
    }
}
