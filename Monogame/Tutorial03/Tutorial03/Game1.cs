using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial03.Cameras;

namespace Tutorial03
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Terrain terrain;
        Camera camera;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        Model model;
        BasicEffect effect;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            terrain = new Terrain(GraphicsDevice, Content.Load<Effect>("terrainEffect"), Content.Load<Texture2D>("austra"), Content.Load<Texture2D>("Ground"), Content.Load<Texture2D>("Ground2") , 1f, 5f);
            model = Content.Load<Model>("cube");
            effect = new BasicEffect(GraphicsDevice);
            camera = new MoveableCamera(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        float rotation = -0.5f;

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                camera.Update(gameTime);
            }
           // rotation += 0.001f;
            //camera.UpdateViewMatrix(Vector3.Transform(new Vector3(50, 50, 50), Matrix.CreateRotationY(rotation)), new Vector3(60, 30, 60), Vector3.Up);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                return;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (var m in model.Meshes)
            {
                foreach (BasicEffect e in m.Effects)
                {
                    e.View = camera.View;
                    e.Projection = camera.Projection;
                    m.Draw();
                }
            }

            terrain.Draw(camera.View, camera.Projection);


            base.Draw(gameTime);
        }
    }
}
