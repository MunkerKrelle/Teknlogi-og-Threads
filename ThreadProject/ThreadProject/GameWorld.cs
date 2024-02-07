using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace ThreadProject
{ 
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static List<GameObject> gameObjects;
        private static List<GameObject> gameObjectsToAdd;
        private Button chopTreesButton;
        private SpriteFont testFont;

        private static Vector2 screenSize;
        public static Vector2 ScreenSize { get => screenSize; }

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
            gameObjects.Add(new Worker());
            gameObjects.Add(new Gold());
            gameObjects.Add(new Tree());
            gameObjects.Add(chopTreesButton = new Button(new Vector2 (100,100), "", ChopTree));

            base.Initialize();
        }
         
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var item in gameObjects)
            {
                item.LoadContent(Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var item in gameObjects)
            {
                item.Update(gameTime);
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

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void ChopTree()
        {
            chopTreesButton.buttonText = "You are chopping trees";
        }
    }
}
