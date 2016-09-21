using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameCoreTutorial00
{
    /// <summary>
    /// Основной класс нашей игры.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //SpriteBatch - класс, позволяющий отрисовывать спрайты. Создадим переменную типа SpriteBatch
        SpriteBatch spriteBatch;

        //Объявим глобальную переменную типа Texture2D для хранения информации о нашей текстуре.
        Texture2D texture;


        //Конструктор
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
          
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// Загрузка графического контета игры. Метод будет вызван один раз.
        /// </summary>
        protected override void LoadContent()
        {
            // Создание экземпляра класс SpriteBatсh.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("island");
        }

        /// <summary>
        /// Выгружаем контент, который создали во время игры, без использование ContentManager
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Реализация игровой логики должны быть в данном методе.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
               
        }

        /// <summary>
        /// Метод для рендера
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            ///Обязательно вызвать spriteBatch.Begin() до того, как будет вызван SpriteBatch.Draw()
            ///Все спрайты будут отрисовывать внутри spriteBatch.Begin() и spriteBatch.End() с параметрами,
            ///указанными в spriteBatch.Draw()
            

            spriteBatch.Begin();

            //Отрисовываем нашу текстуру
            //1-ый аргумент, ссылка на текстуру для отрисовки
            //2-ой аргумент, позиция, где отрисовывать
            //    позиция это структура типа Vector2, имеющая  констркутор Vector2(float x, float y), где x и y - координаты.
            //    Отрисовка происходит относительно левого верхнего угла.
            //3-ий аргумент, цвет, на который будет умножен каждый пиксель текстуры.

            spriteBatch.Draw(texture, new Vector2(0, 50), Color.White);
            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
