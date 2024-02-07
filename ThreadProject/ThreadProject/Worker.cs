using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ThreadProject
{
    internal class Worker : GameObject
    {
        private int moveSpeed = 5;
        private int workSpeed = 1;
        private int workerLevel = 1;
        static public int workerCost = 100;
        static private int test;


        public int WorkerCost
        {
            get { return workerCost; }
            set { workerCost = value; }
        }

        public Worker() 
        {
           
            //position = new Vector2(position.X, position.Y);
            Position = new Vector2(GameWorld.mouseState.Position.X, GameWorld.mouseState.Position.Y);
            //position = new Vector2(300, 300);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarf-male-base");
            
        }

        public override void Update(GameTime gameTime)
        {
            //test++;
            /*Random random1 = new Random();
            Random random2 = new Random();
            position.X = random1.Next(100, 400);
            position.Y = random2.Next(100, 400);*/
            //position.Y++;
            
            //mouseState.Position.X; 
            //mouseState.Position.Y
            //position = new Vector2(GameWorld.mouseState.Position.X, GameWorld.mouseState.Position.Y);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(sprite, Position, Color.White);
        }

        public void ThreadTesting(object ob)
        {
            lock (ob) 
            {
            test++;
            }
        }
    }
}
