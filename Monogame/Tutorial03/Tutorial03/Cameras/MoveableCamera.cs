using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tutorial03.Cameras
{
    public class MoveableCamera : Camera
    {
        public float TranslationSpeed = 50f;
        public float RotationSpeed = 0.1f;
        public Vector3 Position;
        public float Yaw = 0;
        public float Pitch = 0;


        private MouseState lastMouseState;
        
        public MoveableCamera(GraphicsDevice device) : base(device)
        {
            lastMouseState = Mouse.GetState();
        } 
    


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            `MouseState mouseState = Mouse.GetState();
            float deltaX = mouseState.X - lastMouseState.X;
            float deltaY = mouseState.Y - lastMouseState.Y;
            Yaw -= RotationSpeed * deltaX * elapsedTime;
            Pitch -= RotationSpeed * deltaY * elapsedTime;


            Vector3 translation = new Vector3(0, 0, 0);
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                translation += new Vector3(0, 0, -TranslationSpeed) * elapsedTime;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                translation += new Vector3(0, 0, TranslationSpeed) * elapsedTime;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                translation += new Vector3(TranslationSpeed, 0, 0) * elapsedTime;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                translation += new Vector3(-TranslationSpeed, 0, 0) * elapsedTime;
            }

            Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(Yaw, Pitch, 0);


            Vector3 Up = Vector3.Transform(Vector3.Up, rotationMatrix);

            Position += Vector3.Transform(translation, rotationMatrix);
            Vector3 cameraFinalTarget = Position + Vector3.Transform(new Vector3(0, 0, -1), rotationMatrix);

            View = Matrix.CreateLookAt(Position, cameraFinalTarget, Up);

            Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);
            lastMouseState = Mouse.GetState();
        }
    }
}
