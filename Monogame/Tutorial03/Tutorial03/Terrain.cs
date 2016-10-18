using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial03
{
    public class Terrain
    {

        Effect effect;

        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        private Matrix world;
        private GraphicsDevice device;

        MultiTexturedVertexDeclaration[] vertices;
        int[] indices;
        int width;
        int height;
        Texture2D baseTextureA;
        Texture2D baseTextureB;

        public Terrain(GraphicsDevice device, Effect terrainEffect, Texture2D texture,Texture2D baseTextureA, Texture2D baseTextureB, float size, float heightC = 6f)
        {
            this.baseTextureA = baseTextureA;
            this.baseTextureB = baseTextureB;
            width = texture.Width;
            height = texture.Height;
            this.device = device;
            effect = terrainEffect;
            Color[] color = new Color[width * height];

            texture.GetData<Color>(color);

            indices = new int[(width - 1) * (height - 1) * 6];
            vertices = new MultiTexturedVertexDeclaration[width * height];

                

            int num = 0;
            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    // LL --- LR
                    // |----/--|
                    // |-- /---|
                    // |--/----|
                    // |-/-----|
                    // UL---- UR
                    int ll = i * width + j;
                    int lr= i * width + j + 1;
                    int ul = (i + 1) * width + j;
                    int ur = (i + 1) * width + j + 1;
                    indices[num++] = lr;
                    indices[num++] = ul;
                    indices[num++] = ll;

                    indices[num++] = ul;
                    indices[num++] = lr;
                    indices[num++] = ur;
                }
            }

            float maxY = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    vertices[i * width + j].Position = new Vector3(j * size, color[i * width + j].R / heightC, i * size);
                    vertices[i * width + j].TextureCoordinate.X = (float)j / 30.0f;
                    vertices[i * width + j].TextureCoordinate.Y = (float)i / 30.0f;

                    maxY = Math.Max(maxY, vertices[i * width + j].Position.Y);
                }
            }


            for (int i = 0; i < indices.Length / 3; i++)
            {
                int a = indices[i * 3];
                int b = indices[i * 3 + 1];
                int c = indices[i * 3 + 2];

                Vector3 side1 = vertices[a].Position - vertices[c].Position;
                Vector3 side2 = vertices[a].Position - vertices[b].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

 


                vertices[a].Normal += normal;
                vertices[b].Normal += normal;
                vertices[c].Normal += normal;

                vertices[a].TextureWeights.X = vertices[a].Position.Y / maxY;
                vertices[a].TextureWeights.Y = 1 - vertices[a].TextureWeights.X;

                vertices[b].TextureWeights.X = vertices[a].Position.Y / maxY;
                vertices[b].TextureWeights.Y = 1 - vertices[a].TextureWeights.X;

                vertices[c].TextureWeights.X = vertices[a].Position.Y / maxY;
                vertices[c].TextureWeights.Y = 1 - vertices[a].TextureWeights.X;

            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();

            world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z) * Matrix.CreateTranslation(Position);
        }



        public void Draw(Matrix view, Matrix projection)
        {
            effect.Parameters["BaseTextureA"].SetValue(baseTextureA);
            effect.Parameters["BaseTextureB"].SetValue(baseTextureB);
            effect.Parameters["World"].SetValue(Matrix.CreateTranslation(10, -10, 10));
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["LightDirection"].SetValue(new Vector3(-0.5f, -1, -0.5f));
            //device.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.WireFrame};
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserIndexedPrimitives<MultiTexturedVertexDeclaration>(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
            }
        }
    }
}
