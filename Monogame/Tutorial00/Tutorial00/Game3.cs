using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameCoreTutorial00
{
    /// <summary>
    /// Основной класс нашей игры.
    /// </summary>
    public class Game3 : Game
    {
        GraphicsDeviceManager graphics;

        //SpriteBatch - класс, позволяющий отрисовывать спрайты. Создадим переменную типа SpriteBatch
        SpriteBatch spriteBatch;

        //переменная для хранения угла поворота картинки.
        float rotation = MathHelper.PiOver4;

        //переменная для хранения коэфициента масштабирования по оси X и Y
        Vector2 scale = new Vector2(0.5f, 0.5f);


        //Объявим глобальную переменную типа Texture2D для хранения информации о нашей текстуре.
        Texture2D texture;


        //Конструктор
        public Game3()
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


            //ВАЖНО, для того, чтобы работала сортировка спрайтов надо в spriteBatch.Begin() передать SpriteSortMode.BackToFront или SpriteSortMode.FrontToBack

            spriteBatch.Begin(SpriteSortMode.BackToFront);

            //Отрисовываем нашу текстуру
            //1-ый аргумент, ссылка на текстуру для отрисовки
            //2-ой аргумент, позиция, где отрисовывать
            //    позиция это структура типа Vector2, имеющая  констркутор Vector2(float x, float y), где x и y - координаты.
            //    Отрисовка происходит относительно левого верхнего угла.
            //3-ий аргумент, SourceRectangle. Какой участок из текстуры будем отрисовывать. Полезно, когда у вас "атлас текстур".
            //    new Rectangle(x, y, width, height). x, y координаты внутри текстуры относительно левого верхнего угла текстуры. width/ height -
            //    ширина и высота участка, которй вырежим относительно позиции x и y
            //4-ый аргумент, цвет
            //5-ый аргумент, угол поворота
            //6-ой аргумент, позиция относительно которой будет происходить вращение и масш
            //6-ой аргумент, коэфициент масштабирования
            //7-ой аргумент, отражение по оси X и Y, SpriteEffects.None означает, что нет отражения
            //8-ой аргумент, уровень слоя. См. Game3.cs

            spriteBatch.Draw(texture, new Vector2(100, 100), new Rectangle(0, 0, texture.Width, texture.Height),
                Color.Yellow, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(texture, new Vector2(100, 100), new Rectangle(0, 0, texture.Width, texture.Height),
              Color.Red, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);

            spriteBatch.Draw(texture, new Vector2(100, 100), new Rectangle(0, 0, texture.Width, texture.Height),
              Color.Blue, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.2f);


            //В режиме BackToFront мы увидим красный спрайт(он обладает наименьшим layerDepth == 0f)
            //В режиме FrontToBack мы увидим желтый спрайт(он обладает наибольшим layerDpth == 0.5f). Попробуйте!
            //Посмотрите, что будет если убрать SpriteSortMode из spriteBatch.Begin()

            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
