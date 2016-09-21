using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameCoreTutorial00
{
    /// <summary>
    /// Основной класс нашей игры.
    /// </summary>
    public class Game5 : Game
    {
        GraphicsDeviceManager graphics;

        //SpriteBatch - класс, позволяющий отрисовывать спрайты. Создадим переменную типа SpriteBatch
        SpriteBatch spriteBatch;

        //переменная для хранения угла поворота картинки.
        float rotation = MathHelper.PiOver4;

        //переменная для хранения коэфициента масштабирования по оси X и Y
        Vector2 scale = new Vector2(0.5f, 0.5f);

        //переменная для хранения позиции
        Vector2 position = new Vector2(100, 100);


        //Объявим глобальную переменную типа Texture2D для хранения информации о нашей текстуре.
        Texture2D texture;


        //Конструктор
        public Game5()
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

            graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            //Не забываем!!! Для применения изменений.
            graphics.ApplyChanges();

            //Убрав комментарий у строчки ниже, игра запустится в полноэкранном режиме.
            //graphics.IsFullScreen = true;


            //Сделаем курсор мыши видимым
            IsMouseVisible = true;


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
            //Узнаем сколько времени прошло с предыдушего кадра
            //gameTime.ElepsedGameTime возращает объект, описывающий различными характеристиками сколько времени
            //прошло с предыдущего кадра. Нам интересно TotalMilliseconds, количество мс. И переведем его в секунды.
            //Важно, если бы мы взяли .TotalSeconds, мы бы получали всегда 0, так как между каждым кадром проходит 0,016 секунд
            //при 60 фпс
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;


            //Получаем состояние клавиатуры
            KeyboardState ks = Keyboard.GetState();

            //Проверим нажата ли какая-либо кнопка

            if (ks.IsKeyDown(Keys.Q))
            {
                //изменяем rotation

                //0.5f - скорость, delta - время. Получаем расстояние в углах.
                rotation += 0.5f * delta;

                //строчка выше эквивалентна rotation = rotation + 0.5f * delta;
            }


            //Получим состояние мышки

            MouseState ms = Mouse.GetState();
           
            //Проверим нажата ли левая кнопка мыши

            if (ms.LeftButton == ButtonState.Pressed)
            {
                //вращаем в обратную сторону
                rotation -= 0.5f * delta;
            }

            //узнаем позицию мышки и поместим туда остров(Не забываем про origin, он смещает место отрисовки спрайта)
            position = ms.Position.ToVector2();

            
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

            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height),
                Color.Yellow, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.5f);
            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
