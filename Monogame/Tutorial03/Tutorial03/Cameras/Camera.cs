using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial03.Cameras
{
    public class Camera
    {
        public Matrix View, Projection;

        protected GraphicsDevice device;


        public Camera(GraphicsDevice device)
        {

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, device.Viewport.AspectRatio, 0.0001f, 100000f);
            this.device = device;
            UpdateViewMatrix(new Vector3(50, 30, 50), new Vector3(80, 10, 80), Vector3.Up);
        }

        public void UpdateViewMatrix(Vector3 position, Vector3 target, Vector3 up)
        {
            View = Matrix.CreateLookAt(position, target, up);
        }

        public virtual void Update(GameTime gameTime)
        {

        }


    }
}
