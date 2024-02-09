using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ThreadProject
{ /// <summary>
/// The GameWorld Class handles updating the game, gameObjects, arrays, lists etc. Whilst also handling input from the player 
/// through Mousestates. GameWorld also instantiates the locks which are used for syncronizing the threads when they attempt to
/// enter critical regions of code such as resources that are shared.
/// </summary>
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
        /// <summary>
        /// Gets the workerCount variable and sets it
        /// </summary>
        public int WorkerCount
        {
            get { return workerCount; }
            set { workerCount = value; }
        }
        /// <summary>
        /// Constructor which sets the default for the gameWorld, such as screen size, content directory etc.
        /// </summary>
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        }
        /// <summary>
        /// Initializes lists and adds gameObject instances to the list of gameObjects. This is done manually for objects
        /// which are expected to be available at the start, such as townhall, goldmine and tree. 
        /// But new instances of objects such as worker, will be added via the gameObjectsToAdd list and then updated into the 
        /// gameObjects list when possible to prevent list change errors.
        /// </summary>
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
         /// <summary>
         /// In our LoadContent we have a foreach loop which loads the content of all gameObjects that have been added 
         /// to the list of gameObjects.
         /// </summary>
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
        /// <summary>
        /// The update method handles adding and removing of our game objects by running the methods called 
        /// AddNewGameObjects and RemoveGameObjects. It also handles the gameobjects in our gameObject list
        /// and ensures that their update is being run.
        /// </summary>
        /// <param name="gameTime"></param>
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
        /// <summary>
        /// We draw our game objects here when they have been added to the list of gameObjects. aswell as our UI manager.
        /// </summary>
        /// <param name="gameTime"></param>
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
        /// <summary>
        /// This is our second list of gameObjects, which is used as a buffer to add to our main list of gameObjects,
        /// to ensure we dont get errors when adding new gameObjects.
        /// </summary>
        /// <param name="go"></param>
        public static void InstantiateGameObject(GameObject go)
        {
            gameObjectsToAdd.Add(go);
        }

        /// <summary>
        /// Tager listen af gameObjectsToAdd og spawner dem i GameWorld.
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
        /// <summary>
        /// Removes gameObjects in our gameObject list as those have already been added, and sets their bool to
        /// be allowed to be removed.
        /// </summary>
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
