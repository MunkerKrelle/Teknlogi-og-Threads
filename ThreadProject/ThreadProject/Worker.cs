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
{/// <summary>
/// Our Worker class is responsible for the worker unit in the game, it handles infomation such as position, scale, price
/// aswell as methods and updates for how to mine gold or chop wood.
/// </summary>
    internal class Worker : GameObject
    {
        private float speed;
        private Vector2 structure;

        private int moveSpeed = 5;
        private int workSpeed = 1;
        private int workerLevel = 1;
        static public int workerCost = 10;
        static private int testLock;
        private float distance;
        private Texture2D gold;
        private bool idle = true;
        private bool atWorkStructure = false;
        private bool atTownHall = false;
        private string profession;
        private Button[] job = new Button[2];

        public int WorkerCost
        {
            get { return workerCost; }
            set { workerCost = value; }
        }

        /// <summary>
        /// The constructor for the worker, sets postion, scale, speed and activates the instance of the worker.
        /// </summary>
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

            //GameWorld.InstantiateGameObject(job[0] = new Button(new Vector2(-500, -500), "Mine Gold", GoldMining));
            //GameWorld.InstantiateGameObject(job[1] = new Button(new Vector2(-500, -500), "Chop Wood", WoodCutting));


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
            if (active)
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
        }

        /// <summary>
        /// Checks if the mouse is pressed on a pressable object
        /// </summary>
        public void MousePressed()
        {
            if (!active)
            {
                return;
            }
            if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
            {
                ChooseJob();
            }
        }
        /// <summary>
        /// Update handles position and mouse clicks for the worker. 
        /// </summary>
        /// <param name="gameTime"></param> 
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

        /// <summary>
        /// moving the worker relative to its direction and speed. We normalize the vector to ensure even speed during diagonal movement
        /// which causes double input.
        /// </summary>
        /// <param name="structurePos"></param> structurePos is the target postion of a position the worker will be moving towards
        public void Move(Vector2 structurePos)
        {
            Vector2 directionMove = Vector2.Normalize(structurePos - position);
            position += directionMove * speed;
        }
        /// <summary>
        /// Here we ensure that only one thread can access the goldAmount variable at a time to prevent race conditions.
        /// </summary>
        /// <param name="ob"></param> The locking object used for securing the gold amount in a multi thread setup.
        public void GoldLocking(object ob)
        {
            lock (ob)
            {
                UI_Manager.goldAmount -= workerCost;
                //testLock++;
            }
        }
        /// <summary>
        /// Here we ensure that only one thread can access the woodAmount variable at a time to prevent race conditions.
        /// </summary>
        /// <param name="ob"></param> The locking object used for securing the wood amount in a multi thread setup.
        static public void WoodLocking(object ob)
        {
            lock (ob)
            {
                UI_Manager.woodAmount -= 50;
                //testLock++;
            }
        }
        /// <summary>
        /// The WoodCutting method is enabled when the player clicks on the corresponding button for cutting wood, 
        /// which afterwards disables clicking, to prevent repeat clicks.
        /// </summary>
        public void WoodCutting()
        {
            job[0].RemoveObject();
            job[1].RemoveObject();
            active = false;
            profession = "WoodCutting";
            idle = false;
        }
        /// <summary>
        /// The GoldMining method is enabled when the player clicks on the corresponding button for mining gold, 
        /// which afterwards disables clicking, to prevent repeat clicks.
        /// </summary>
        public void GoldMining()
        {
            job[0].RemoveObject();
            job[1].RemoveObject();
            profession = "GoldMining";
            idle = false;
        }
     
        public void ChooseJob()
        {
            active = false;
            colorCode = Color.White;
            GameWorld.InstantiateGameObject(job[0] = new Button(new Vector2(position.X, position.Y - 50), "Mine Gold", GoldMining));
            GameWorld.InstantiateGameObject(job[1] = new Button(new Vector2(position.X, position.Y + 50), "Chop Wood", WoodCutting));
        }

        /// <summary>
        /// The working method handles the movement and targeting of the worker in a series of loops. 
        /// The first loop ensures both booleans are false, to prevent lockouts in the while.
        /// Nested within that loop is an idle loop which will sleep the thread running the code for 1/10 of a second,
        /// and repeat this process for aslong as the idle boolean is true.
        /// When it is no longer true, it checks for which profession is selected, and then sets a target position
        /// for the worker to move towards. 
        /// Eventually the worker will get to a specified(gold mine or forest) position 
        /// and if it is mining gold it will enter the semaphore.
        /// if it is chopping wood, it will simply wait 5 seconds.
        /// Finally it will return back to the townhall with either gold or wood, and the player UI updates accordingly
        /// 
        /// </summary>
        public void Working()
        {
            while (true)
            {
                atWorkStructure = false;
                atTownHall = false;
                while (idle)
                {
                    Thread.Sleep(100);
                }

                if (profession == "WoodCutting")
                {
                    structure = new Vector2(400, 200);
                }
                else if (profession == "GoldMining")
                {
                    structure = new Vector2(1500, 350);
                }

                //while (position != structure)
                while(atWorkStructure == false)
                {
                    Move(structure);
                    distance = Vector2.Distance(position, structure);
                    if (distance <= 10)
                    {
                        atWorkStructure = true;
                        if (profession == "GoldMining")
                        {
                            Structure.Enter();
                            structure = new Vector2(250, GameWorld.ScreenSize.Y / 2 + 50);
                        }
                        else if (profession == "WoodCutting") 
                        {
                            Thread.Sleep(5000);
                            structure = new Vector2(250, GameWorld.ScreenSize.Y / 2 + 50);
                        }
                    }
                }

                while (atTownHall == false)
                {
                    Move(structure);
                    distance = Vector2.Distance(position, structure);
                    if (distance <= 10) 
                    {
                        atTownHall = true;
                        if (profession == "WoodCutting")
                        {
                            UI_Manager.woodAmount += 10;
                        }
                        if (profession == "GoldMining")
                        {
                            UI_Manager.goldAmount += 10;
                        }
                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}
