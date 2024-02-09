using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThreadProject
{
    internal class Tree : GameObject
    {
        private Texture2D[] trees;
        private Random rnd = new Random();


        public Tree()
        {
            scale = 2;
            position = new Vector2(400, 200);
        }

        public override void LoadContent(ContentManager content)
        {
            //trees = new Texture2D[2];
            //trees[0] = content.Load<Texture2D>("tree-light-green-isaiah658");
            //trees[1] = content.Load<Texture2D>("tree-dark-green-isaiah658");

            sprite = content.Load<Texture2D>("tree-dark-green-isaiah658");
        }

        public override void Update(GameTime gameTime)
        {
        }

       
    }
}
