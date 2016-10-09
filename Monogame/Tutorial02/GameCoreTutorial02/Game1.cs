using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameCoreTutorial02
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Класс со спрайтовой анимацией, смотри реализацию.
        SpriteAnimation spriteAnimation;

        //Model - класс для хранения 3д геометрии.
        Model model;

        //Мировая матрица куба
        Matrix world = Matrix.CreateTranslation(Vector3.Zero);

        //Начальная позиция камеры.
        Vector3 cameraPosition = new Vector3(0, 2, 3);

        //Матрица вида и проекции 
        //http://www.3dgep.com/understanding-the-view-matrix/
        Matrix view;

        //google it : projection matrix
        //https://habrahabr.ru/post/252771/
        Matrix projection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Создаем перспективную матрицу проекции
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2,
                GraphicsDevice.Viewport.AspectRatio, 0.001f, 1000f);

            //и матрицу вида(позиция камеры, точка, в которую смотрит камера, и Vector.Up камеры)
            view = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0),
            Vector3.Up);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //Загружаем куб, по аналогии с текстурой
            model = Content.Load<Model>("cube");
            

            //Создаем объект спрайтовой анимации
            spriteAnimation = new SpriteAnimation(Content.Load<Texture2D>("spriteAnim"),
                new Vector2(100, 100), 200, 100);


        }

        protected override void UnloadContent()
        {
        }


        //угол поворота кубика
        float rotation = 0;

        //угол поворота камеры
        float cameraRotation = 0;

        protected override void Update(GameTime gameTime)
        {

            //количество секунд с прошлого кадра
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;

            //поворачиваем кубик
            rotation += 2f * delta;

            //поворачиваем камеру
            //Для этого нам лишь надо, чтобы позиция камеры изменялась(камера всех будет смотреть в заданную точку - target, у нас же это (0, 0, 0))
            //Поэтому наша камера будет бегать по окружности вокруг объекта
            cameraRotation += 2f * delta;

            //Поворачиваем изначальный вектор позициии вокруг оси OY на угол cameraRotation
            cameraPosition = Vector3.Transform(new Vector3(0, 2, 3), 
                Matrix.CreateRotationY(cameraRotation));


            //Пересоздаем мировую матрицу куба
            world = Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(Vector3.Zero);

            //Пересоздаем матрицу вида
            view = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0),
            Vector3.Up);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //Обновляем спрайтовую анимацию
            spriteAnimation.Update(gameTime);
            spriteAnimation.Position.X -= 10f * delta;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //Рисуем спрайтовую анимацию. Выполняем наша соглашение, что вызывающая стороная будет делать spriteBatch.Begin(), spriteBatch.End().
            spriteBatch.Begin();
            spriteAnimation.Draw(gameTime, spriteBatch);
            spriteBatch.End();



            //Рисуем кубик. Надо нарисовать все коллекцию мешев внутри модели
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //Задаем параметры для эффекта: мировую матрицу, матрицу вида и проекции
                    //Так же включаем освещение.
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                    mesh.Draw();
                }
            }


            base.Draw(gameTime);
        }
    }
}
