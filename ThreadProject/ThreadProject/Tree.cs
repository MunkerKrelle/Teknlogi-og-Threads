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
{/// <summary>
/// The Tree class loads the tree sprite and determines its position and scale.
/// </summary>
    internal class Tree : GameObject
    {
        private Texture2D[] trees;
        private Random rnd = new Random();

        /// <summary>
        /// the constructor sets the scale and position of the tree
        /// </summary>
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
