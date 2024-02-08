using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using static ThreadProject.Button;
using System.Drawing.Imaging;
using System.Threading;

namespace ThreadProject
{
    internal class Worker : GameObject
    {
        private float speed;
        private Vector2 structure;

        private int moveSpeed = 5;
        private int workSpeed = 1;
        private int workerLevel = 1;
        static public int workerCost = 10;
        static private int testLock;
        private Texture2D gold;
        private bool idle = true;
        private string profession;

        public int WorkerCost
        {
            get { return workerCost; }
            set { workerCost = value; }
        }


        public Worker()
        {
            position = new Vector2(0, 0);
            scale = 3;
            speed = 0.00007f;
            active = true;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarf-male-base");
            gold = content.Load<Texture2D>("Gold");
            //rectangleForButtons = new Rectangle((int)position.X, (int)position.Y, sprite.Width / 2, sprite.Height / 2);

            PositionUpdate();
        }

        /// <summary>
        /// Updates the position of the buandries of the button - where the button can be clicked
        /// </summary>
        public void PositionUpdate()
        {
            minPosition.X = position.X - (sprite.Width / 4);
            minPosition.Y = position.Y - (sprite.Height / 4);
            maxPosition.X = position.X + (sprite.Width * scale / 4);
            maxPosition.Y = position.Y + (sprite.Height * scale / 4);
        }

        /// <summary>
        /// Checks if the cursor is on an object and turns it gray 
        /// </summary>
        public void MouseOnButton()
        {

            if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
            {
                colorCode = Color.LightGray;
            }
            else
            {
                colorCode = Color.White;
            }
        }

        /// <summary>
        /// Checks if the mouse is pressed on a pressabled object
        /// </summary>
        public void MousePressed()
        {
            if (!active)
            {
                return;
            }
            if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
            {
                JobAcquired();
            }
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

            PositionUpdate();
            MouseOnButton();
            mouseState = Mouse.GetState();

            if (active)
            {
                if (mouseState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
                {
                    MousePressed();
                }
                newState = mouseState;
            }
        }

        public void ChopWood()
        {
            structure = new Vector2(1500, 500);
            WoodLocking(testLock);
        }

        public void MineGold()
        {
            structure = new Vector2(1500, 100);
        }

        public void Move(Vector2 structurePos)
        {
            Vector2 directionMove = Vector2.Normalize(structurePos - position); //Vi normalizer vectoren fordi eller ville bulleten bevæge sig hurtigere, når den bevæger sig skråt
            position += directionMove * speed;
        }

        public void GoldLocking(object ob)
        {
            lock (ob)
            {
                UI_Manager.goldAmount -= workerCost;
                //testLock++;
            }
        }
        public void WoodLocking(object ob)
        {
            lock (ob)
            {
                UI_Manager.woodAmount += 20;
                //testLock++;
            }
        }

        public void JobAcquired()
        {
            profession = "WoodCutting";
            idle = false;
        }

        public void Working(object ob)
        {
            while (true)
            {
                while (idle)
                {
                    Thread.Sleep(100);
                }

                if (profession == "WoodCutting")
                {
                    structure = new Vector2(1500, 500);
                }
                else if (profession == "GoldMining")
                {
                    structure = new Vector2(1500, 100);
                }

                while (position != structure)
                {
                    Move(structure);
                }

                Structures.Enter();

                structure = new Vector2(10, 10);

                while (position != structure)
                {
                    Move(structure);
                }
            }
        }
    }
}
