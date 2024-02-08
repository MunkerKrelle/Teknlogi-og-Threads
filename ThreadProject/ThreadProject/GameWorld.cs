using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace ThreadProject
{ 
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static List<GameObject> gameObjects;
        private static List<GameObject> gameObjectsToAdd;
        private Button chopTreesButton;

        private SpriteFont testFont;
        private UI_Manager myUIManager;

        public static MouseState mouseState;

        private static Vector2 screenSize;
        public static Vector2 ScreenSize { get => screenSize; }
        private Worker[] workerArray = new Worker[10];
        private int workerCount = 0;
        private bool purchaseCoolDown = false;
        private float timeElapsed;
        public static float DeltaTime;
        //private int goldAmount = 500; //temp variable untill jeppe is done
        

        static readonly object lockObjectGold = new object();
        static public readonly object lockObjectWood = new object();

        public int WorkerCount
        {
            get { return workerCount; }
            set { workerCount = value; }
        }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            gameObjectsToAdd = new List<GameObject>();
            gameObjects = new List<GameObject>();
           // gameObjects.Add(new Worker()); //all this is an instance.
            gameObjects.Add(new Gold());
            gameObjects.Add(new TownHall());
            gameObjects.Add(new Tree());
            myUIManager = new UI_Manager();
            //myUIManager.Start();

            base.Initialize();
        }
         
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            testFont = Content.Load<SpriteFont>("File");

            foreach (var item in gameObjects)
            {
                item.LoadContent(Content);
            }

            myUIManager.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeElapsed += DeltaTime;

            KeyboardState keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            AddNewGameObjects();
            RemoveGameObjects();

            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(testFont, $"Mine Level: {Structure.level}", new Vector2(1250, 500), Color.White);

            myUIManager.DrawResource(_spriteBatch);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static void InstantiateGameObject(GameObject go)
        {
            gameObjectsToAdd.Add(go);
        }

        /// <summary>
        /// Tager listen af gameObjectsToAdd og spawner dem i GameWorld
        /// </summary>
        private void AddNewGameObjects()
        {
            foreach (GameObject gameObjectToSpawn in gameObjectsToAdd)
            {
                gameObjectToSpawn.LoadContent(Content);
                gameObjects.Add(gameObjectToSpawn);
            }

            gameObjectsToAdd.Clear();
        }

        private void RemoveGameObjects()
        {
            List<GameObject> gameObjectsToRemove = new List<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                bool shouldRemoveGameObject = go.ShouldBeRemoved;
                if (shouldRemoveGameObject)
                    gameObjectsToRemove.Add(go);
            }

            foreach (GameObject goToRemove in gameObjectsToRemove)
            {
                gameObjects.Remove(goToRemove);
            }
        }
    }
}
