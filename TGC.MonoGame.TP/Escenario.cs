using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP
{
    public class Escenario
    {
        public Escenario(){}
        private Model Piso { get; set; }
        private Model Pared { get; set; }
        private Model Column { get; set; }
        private Model Ramp { get; set; }
        private Model Platform { get; set; }
        private Model Cube { get; set; }
        private Matrix PisoWorld { get; set; }
        private Matrix ParedWorld { get; set; }
        private Matrix Platform1World { get; set; }
        private Matrix Platform2World { get; set; }
        private Matrix Platform3World { get; set; }
        private Matrix Platform4World { get; set; }
        private Matrix Platform5World { get; set; }

        private Matrix Column1World { get; set; }
        private Matrix Column2World { get; set; }
        private Matrix Column3World { get; set; }
        private Matrix Column4World { get; set; }
        private Matrix Column5World { get; set; }
        private Matrix Column6World { get; set; }
        private Matrix Column7World { get; set; }
        private Matrix Column8World { get; set; }
        private Matrix Column9World { get; set; }
        private Matrix Column10World { get; set; }
        private Matrix Column11World { get; set; }
        private Matrix Column12World { get; set; }
        private Matrix BrokenColumn1World { get; set; }
        private Matrix BrokenColumn2World { get; set; }

        private Matrix Ramp1World { get; set; }
        private Matrix Ramp2World { get; set; }
        private Matrix Ramp3World { get; set; }
        private Matrix Ramp4World { get; set; }
        private Matrix Ramp5World { get; set; }
        private Matrix Ramp6World { get; set; }
        private Matrix Ramp7World { get; set; }
        private Matrix Ramp8World { get; set; }
        private Matrix Ramp9World { get; set; }
        private Matrix Ramp10World { get; set; }
        private Matrix Ramp11World { get; set; }
        private Matrix Ramp12World { get; set; }

        // Variables
        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI / 2;

        //Bones
        private Matrix[] relativeMatrices;

        public void Initialize()
        {
            //Arena
            PisoWorld = Matrix.CreateScale(30, 0, 30);
            ParedWorld = Matrix.CreateScale(30, 50, 30);

            //Plataformas
            Column1World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -450);
            Column2World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -350);
            Column3World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140, 0, -450);
            Column4World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140, 0, -350);

            Ramp1World = Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-80, 0, -380);
            Ramp2World = Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(230, 0, -395);

            Platform1World = Matrix.CreateScale(100, 5, 80) * Matrix.CreateTranslation(70, 64, -390);

            Column5World = Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 50);
            Column6World = Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 50);
            Column7World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 175);
            Column8World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 175);

            Platform2World = Matrix.CreateScale(100, 5, 80) * Matrix.CreateTranslation(-250, 30, 100);

            Column9World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 290);
            Column10World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 290);

            Ramp3World = Matrix.CreateScale(0.25f, 0.35f, 0.3f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(-250, 0, -10);
            Ramp4World = Matrix.CreateScale(0.4f, 0.15f, 0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(-300, 35, 130);

            Platform3World = Matrix.CreateScale(100, 5, 70) * Matrix.CreateTranslation(-250, 93, 235);

            Ramp5World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(185, 0, 243);
            Ramp6World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(235, 0, 359);
            Ramp7World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(155, 0, 325);
            Ramp8World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(265, 0, 274);

            Platform4World = Matrix.CreateScale(50, 10, 50) * Matrix.CreateTranslation(210, 0, 300);

            Ramp9World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(-461, 0, -254);
            Ramp10World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(-410, 0, -136);
            Ramp11World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-495, 0, -169);
            Ramp12World = Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-376, 0, -221);

            Platform5World = Matrix.CreateScale(50, 10, 50) * Matrix.CreateTranslation(-435, 0, -195);

            //Columnas Individuales
            BrokenColumn1World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationY(-MathF.PI / 6) * Matrix.CreateTranslation(450, 0, 250);
            BrokenColumn2World = Matrix.CreateScale(0.7f) * Matrix.CreateRotationY(MathF.PI / 6) * Matrix.CreateTranslation(-230, 0, -290);

            Column11World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-500, 0, 0);
            Column12World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(436, 0, -80);
        }


        public void LoadContent(Model piso, Model pared, Model column, Model ramp, Model platform, Model cube)
        {
            Piso = piso;
            Pared = pared;
            Column = column;
            Ramp = ramp;
            Platform = platform;
            Cube = cube;
        }
        public void dibujar(Matrix view,Matrix projection,Effect effect,Matrix matrizMundo,Model modelo,Color color)
        {
            foreach (var mesh in modelo.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = effect;
                }
            }

            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["DiffuseColor"].SetValue(color.ToVector3());

            relativeMatrices = new Matrix[modelo.Bones.Count];
            modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);


            foreach (var mesh in modelo.Meshes)
            {
                effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
                mesh.Draw();
            }
        }

         public void dibujarEscenario(Matrix view,Matrix projection,Effect effect)
        {
            //Arena
            dibujar(view,projection,effect,PisoWorld, Piso, Color.LightGoldenrodYellow);
            dibujar(view,projection,effect,ParedWorld, Pared, Color.Wheat);

            //Plataformas
            dibujar(view,projection,effect,Column1World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column2World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column3World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column4World, Column, Color.SandyBrown);

            dibujar(view,projection,effect,Ramp1World, Ramp, Color.Gray);
            dibujar(view,projection,effect,Ramp2World, Ramp, Color.Gray);

            dibujar(view,projection,effect,Platform1World, Platform, Color.DarkSalmon);

            dibujar(view,projection,effect,Column5World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column6World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column7World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column8World, Column, Color.SandyBrown);

            dibujar(view,projection,effect,Platform2World, Platform, Color.DarkSalmon);

            dibujar(view,projection,effect,Column9World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column10World, Column, Color.SandyBrown);

            dibujar(view,projection,effect,Ramp3World, Ramp, Color.Gray);
            dibujar(view,projection,effect,Ramp4World, Ramp, Color.Gray);

            dibujar(view,projection,effect,Platform3World, Platform, Color.DarkSalmon);

            dibujar(view,projection,effect,Ramp5World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp6World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp7World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp8World, Ramp, Color.DarkGray);

            dibujar(view,projection,effect,Platform4World, Platform, Color.DarkGray);

            dibujar(view,projection,effect,Ramp9World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp10World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp11World, Ramp, Color.DarkGray);
            dibujar(view,projection,effect,Ramp12World, Ramp, Color.DarkGray);

            dibujar(view,projection,effect,Platform5World, Platform, Color.DarkGray);

            //Columnas Individuales
            dibujar(view,projection,effect,BrokenColumn1World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,BrokenColumn2World, Column, Color.SandyBrown);

            dibujar(view,projection,effect,Column11World, Column, Color.SandyBrown);
            dibujar(view,projection,effect,Column12World, Column, Color.SandyBrown);
        }
    }
}
