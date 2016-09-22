using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial01
{
    public class World
    {
        public readonly Game game;

        List<Entity> entities = new List<Entity>();
        List<Entity> toKill = new List<Entity>();

        public World(Game game)
        {
            this.game = game;
        }

        public virtual Entity Spawn(string className)
        {
            return Spawn(className, Vector2.Zero);
        }


        public virtual Entity Spawn(string className, Vector2 position)
        {
            var prms = new object[] { this };
            var entity = (Entity)Activator.CreateInstance(Type.GetType(className), prms);
            entity.Position = position;
            entities.Add(entity);
            return entity;
        }


        public virtual void Update(GameTime gameTime)
        {

            foreach (var entity in entities)
            {
                entity.Update(gameTime);
            }

            foreach (var a in entities)
            {
                foreach (var b in entities)
                {
                    if (!ReferenceEquals(a, b))
                    {
                        if (a.boundingBox.Intersects(b.boundingBox))
                        {
                            a.Touch(b);
                        }
                    }
                }
            }


            entities.RemoveAll(e => toKill.Contains(e));
            toKill.Clear();
        }


        public virtual void Kill(Entity entity)
        {
            toKill.Add(entity);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                entity.Draw(gameTime, spriteBatch);
            }
        }
    }
}
