using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProject
{
    internal class Worker : GameObject
    {
        private float speed;
        private Vector2 structure = new Vector2 (1000, 1000);

        public Worker()
        {
            position = new Vector2(0, 0);
            scale = 3;
            speed = 2;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarf-male-base");
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }

        public void Move()
        {
            Vector2 directionMove = Vector2.Normalize(structure - position); //Vi normalizer vectoren fordi eller ville bulleten bevæge sig hurtigere, når den bevæger sig skråt
            position += directionMove * speed;
        }
    }
}
