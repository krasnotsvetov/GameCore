using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial01
{
    public class Entity
    {
        //информации о мире, где живёт сущность, поле предназначенное только для чтения
        readonly World world;

        //Позиция
        public Vector2 Position;

        //Угол поворота
        public float Angle;

        //Кол-во жизней
        public int Health;

        //Кол-во боезопаса
        public int Ammo;

        //Кол-во очков
        public int Score;

        //Состояние сущности
        public EntityState State;


        public BoundingBox boundingBox;


        public Entity(World world)
        {
            //Запомним для нашей сущности "игру"
            this.world = world;
        }


        /// <summary>
        /// Метод для нанесения урона этой сущности
        /// </summary>
        /// <param name="attacker">Аттакующая сущность</param>
        /// <param name="damage">Количество нанесенного урона</param>
        public virtual void Damage(Entity attacker, int damage)
        {

        }


        /// <summary>
        /// Метод, вызываемый при пересечении сущностей
        /// </summary>
        /// <param name="attacker">Аттакующая сущность</param>
        /// <param name="damage">Количество нанесенного урона</param>
        public virtual void Touch(Entity other)
        {

        }

        /// <summary>
        /// Метод для убийства сущности
        /// </summary>
        /// <param name="killer">Убийца сущности</param>
        public virtual void Kill(Entity killer)
        {
            world.Kill(this);
        }


        /// <summary>
        ///Обновление логики сущности, виртуальной метод для наследования. См "Наследование", "Полиморфизм"
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        ///Отрисовка сущности, виртуальной метод для наследования. См "Наследование", "Полиморфизм"
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }


    //Смотри "Атрибуты"
    [Flags]
    //Смотри "Перечисления"
    public enum EntityState
    {
        None = 0x00,
        Dead = 0x01,
        Invisible = 0x02
    }
}
