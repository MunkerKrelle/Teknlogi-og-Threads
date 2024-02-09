using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace ThreadProject
{
    /// <summary>
    /// Overall this class is used for managing the positioning, visibility and functionality for the buttons and the cursor.
    /// </summary>
    internal class TownHall : GameObject
    {
        // Array til at holde knapper til at bygge arbejdere og udvide minen
        public Button[] buildWorker = new Button[2];

        // Array til at holde arbejdere
        private Worker[] workerArray = new Worker[10];

        // Tæller for antallet af arbejdere
        private int workerCount = 0;

        // Objekt til låsning af tråde
        static readonly object lockObject = new object();

        // Positioner for knapperne
        private Vector2 buttonPos1;
        private Vector2 buttonPos2;

        // Konstruktør
        public TownHall()
        {
            position = new Vector2(200, GameWorld.ScreenSize.Y / 2);
            active = true;
        }

        // Metode til at indlæse knapperne
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("dwarven home");

            
            GameWorld.InstantiateGameObject(buildWorker[0] = new Button(new Vector2(-500, -500), "Buy Worker", ThreadForWorkers));
            GameWorld.InstantiateGameObject(buildWorker[1] = new Button(new Vector2(-500, -500), "Expand Mine", UpgradeMine));

            PositionUpdate();
        }

        /// <summary>
        /// Opdaterer positionen af knappernes grænser - hvor knappen kan klikkes
        /// </summary>
        public void PositionUpdate()
        {
            
            minPosition.X = position.X - (sprite.Width / 2);
            minPosition.Y = position.Y - (sprite.Height / 2);
            maxPosition.X = position.X + (sprite.Width / 2);
            maxPosition.Y = position.Y + (sprite.Height / 2);
        }

        /// <summary>
        /// Tjekker om musen er på en knap og ændrer farven til grå
        /// </summary>
        public void MouseOnButton()
        {
            // Tjekker om musen er over knappen og ændrer farven
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
        /// Tjekker om musen har trykket på en klikbar genstand
        /// </summary>
        public void MousePressed()
        {
            // Tjekker om musen er trykket på knappen
            if (!active)
            {
                return;
            }
            if (mouseState.X > minPosition.X && mouseState.Y > minPosition.Y && mouseState.X < maxPosition.X && mouseState.Y < maxPosition.Y)
            {
                
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

        /// <summary>
        /// Metode til at vise knapperne for bygning af arbejdere og udvidelse af minen
        /// </summary>
        public void TownHallButtons()
        {
            buildWorker[0].Position = new Vector2(position.X, position.Y - 50);
            buildWorker[1].Position = new Vector2(position.X, position.Y + 50);
            active = false;
        }

        /// <summary>
        /// Metode til at starte en tråd for at købe arbejdere
        /// </summary>
        public void ThreadForWorkers()
        {
            buildWorker[0].Position = new Vector2(-500, -500);
            buildWorker[1].Position = new Vector2(-500, -500);

            // Kontrollerer om der er nok guld til at købe en arbejder
            if (UI_Manager.goldAmount >= Worker.workerCost)
            {
                Thread WorkerThread = new Thread(BuyWorker);
                WorkerThread.IsBackground = true;
                WorkerThread.Start();
            }
            active = true;

        }

        /// <summary>
        /// Metode til at købe en arbejder
        /// </summary>
        public void BuyWorker()
        {
            workerArray[workerCount] = new Worker();
            workerArray[workerCount].Position = new Vector2(position.X + 200, position.Y);
            GameWorld.InstantiateGameObject(workerArray[workerCount]);
            workerArray[workerCount].GoldLocking(lockObject);
            workerArray[workerCount].Working();
        }

        /// <summary>
        /// Metode til at udvide minen
        /// </summary>
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

