using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial01
{
    public class Monster : Entity
    {
        public Texture2D texture;

        public Monster(World world) : base(world)
        {
            //Загружаем нашу текстуру.
            //Возьмем нашу "игру", о которой знает world, и возьмем у ней ContentManager
            texture = world.game.Content.Load<Texture2D>("monster");
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
            base.Touch(other);
        }


        public override void Update(GameTime gameTime)
        {
           
            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.A))
            {
                Position.X -= 100f * delta;
            }

            if (ks.IsKeyDown(Keys.D))
            {
                Position.X += 100f * delta;
            }

            if (ks.IsKeyDown(Keys.W))
            {
                Position.Y -= 100f * delta;
            }

            if (ks.IsKeyDown(Keys.S))
            {
                Position.Y += 100f * delta;
            }

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
