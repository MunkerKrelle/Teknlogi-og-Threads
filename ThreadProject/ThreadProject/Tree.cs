using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThreadProject
{
/// <summary>
/// The Tree class loads the tree sprite and determines its position and scale.
/// </summary>
    internal class Tree : GameObject
    {
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
            sprite = content.Load<Texture2D>("tree-dark-green-isaiah658");
        }
    }
}
