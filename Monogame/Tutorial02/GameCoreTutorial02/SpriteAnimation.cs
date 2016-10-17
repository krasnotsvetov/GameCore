using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameCoreTutorial02
{
    public class SpriteAnimation
    {
        //атлас кадров
        private Texture2D texture;

        //ширина кадра
        private int width;
        //время на один кадр
        private float frameTime;
        //сколько времени прошло с предыдущего кадра
        private float timeNow;
        //максимальное количество кадров
        private int maxFrameCount = 1;
        //текущий кадр
        private int frame         = 0;
        //позиция, где рисуем спрайтовую анимацию
        public Vector2 Position   = Vector2.Zero;

        public SpriteAnimation(Texture2D texture, Vector2 position, int width, float frameTime)
        {
            this.texture = texture;
            this.width = width;
            this.frameTime = frameTime;
            this.Position = position;
            maxFrameCount = texture.Width / width;
        }

        public void Update(GameTime gameTime)
        {

            //обрабатываем смену кадров
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            timeNow += delta;

            //Проверка на то, что кадр надо сменить
            if (timeNow > frameTime)
            {
                //Update мог "подвиснуть" и надо высчитать сколько кадров дейсвительно надо проскачить, обычно это число == 1
                int count = (int)(timeNow / frameTime);
                //Вычитаем время
                timeNow -= count * frameTime;
                //И обновляем текущий кадр
                frame += count;
                //Не забываем взять по модулю номер кадра, чтобы зациклить анимацию.
                frame %= maxFrameCount;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //Вырезаем из текстуры необходимый кадр
            spriteBatch.Draw(texture, Position,
                new Rectangle(frame * width, 0, width, texture.Height), Color.White);
        }
    }
}
