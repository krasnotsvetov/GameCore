using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial03
{
    public class Triangle
    {

        VertexPosition[] vertices = new VertexPosition[3];
        int[] indices = new int[3];
        public Vector3 a;
        public Vector3 b;
        public Vector3 c;
        BasicEffect basicEffect;
        GraphicsDevice device;

        public Triangle(GraphicsDevice device, Vector3 a, Vector3 b, Vector3 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            this.device = device;
            basicEffect = new BasicEffect(device);


            vertices[0].Position = a;
            vertices[1].Position = b;
            vertices[2].Position = c;

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
        }


        public void Draw(Matrix View, Matrix Projection)
        {
            basicEffect.World = Matrix.Identity;
            basicEffect.View = View;
            basicEffect.Projection = Projection;
            basicEffect.CurrentTechnique.Passes[0].Apply();

            device.DrawUserIndexedPrimitives<VertexPosition>(PrimitiveType.TriangleList, vertices, 0, vertices.Count(), indices, 0, 1);
        }
    }
}
