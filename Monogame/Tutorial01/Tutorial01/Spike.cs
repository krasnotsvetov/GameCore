using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial01
{
    public class Spike : Entity
    {
        public Texture2D texture;

        public Spike(World world) : base(world)
        {
            //Загружаем нашу текстуру.
            //Возьмем нашу "игру", о которой знает world, и возьмем у ней ContentManager
            texture = world.game.Content.Load<Texture2D>("spike");
        }


        //См. информацию о ключевом слове override
        public override void Damage(Entity attacker, int damage)
        {
            this.Health -= damage;
            if (Health <= 0)
            {
                Kill(attacker);
            }
            base.Damage(attacker, damage);
        }

        public override void Kill(Entity killer)
        {
            State = EntityState.Dead;
            base.Kill(killer);
        }


        public override void Touch(Entity other)
        {
            if (!(other is Spike))
            {
                other.Kill(this);
            }
            base.Touch(other);
        }


        public override void Update(GameTime gameTime)
        {
           
            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            boundingBox.Min = new Vector3(Position, 0);
            boundingBox.Max = new Vector3(Position + new Vector2(texture.Width, texture.Height), 0);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //отрисовываем наш объект
            spriteBatch.Draw(texture, Position, Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
