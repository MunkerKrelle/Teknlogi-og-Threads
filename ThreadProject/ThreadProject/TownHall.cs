using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace ThreadProject
{
    internal class TownHall : GameObject
    {
        public Button[] buildWorker = new Button[2];
        private Worker[] workerArray = new Worker[10];
        private int workerCount = 0;
        static readonly object lockObject = new object();

        private Vector2 buttonPos1;
        private Vector2 buttonPos2;

        public TownHall()
        {
            position = new Vector2(200, GameWorld.ScreenSize.Y / 2);
            active = true;
        }
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarven home");

            GameWorld.InstantiateGameObject(buildWorker[0] = new Button(new Vector2(-500, -500), "Buy Worker", ThreadForWorkers));
            GameWorld.InstantiateGameObject(buildWorker[1] = new Button(new Vector2(-500, -500), "Expand Mine", UpgradeMine));

            PositionUpdate();
        }

        /// <summary>
        /// Updates the position of the boundaries of the button - where the button can be clicked
        /// </summary>
        public void PositionUpdate()
        {
            minPosition.X = position.X - (sprite.Width / 2);
            minPosition.Y = position.Y - (sprite.Height / 2);
            maxPosition.X = position.X + (sprite.Width / 2);
            maxPosition.Y = position.Y + (sprite.Height / 2);
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
                //buttonSound.Play();
                colorCode = Color.Yellow;
                TownHallButtons();
            }
        }

        public override void Update(GameTime gameTime)
        {
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

        public void TownHallButtons()
        {
            buildWorker[0].Position = new Vector2(position.X, position.Y - 50);
            buildWorker[1].Position = new Vector2(position.X, position.Y + 50);
            active = false;
        }

        public void ThreadForWorkers()
        {
            buildWorker[0].Position = new Vector2(-500, -500);
            buildWorker[1].Position = new Vector2(-500, -500);

            Thread WorkerThread = new Thread(BuyWorker);
            WorkerThread.IsBackground = true;
            WorkerThread.Start();

            active = true;
        }

        public void BuyWorker()
        {
            workerArray[workerCount] = new Worker();
            workerArray[workerCount].Position = new Vector2(position.X + 200, position.Y);
            GameWorld.InstantiateGameObject(workerArray[workerCount]);
            workerArray[workerCount].GoldLocking(lockObject);
            workerArray[workerCount].Working();
        }

        public void UpgradeMine()
        {
            buildWorker[0].Position = new Vector2(-500, -500);
            buildWorker[1].Position = new Vector2(-500, -500);

            Worker.WoodLocking(GameWorld.lockObjectWood);
            Structure.UpgradeMine();

            active = true;
        }
    }
}
