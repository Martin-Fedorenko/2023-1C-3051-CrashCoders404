using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP
{
    public class Detalles
    {
        public Detalles() {}

        private Model Tree { get; set; }
        private Model Rock1 { get; set; }
        private Model Rock5 { get; set; }
        private Model Rock10 { get; set; }
        private Model Tire { get; set; }

        private Matrix Tree1World { get; set; }
        private Matrix Tree2World { get; set; }
        private Matrix Tree3World { get; set; }
        private Matrix Tree4World { get; set; }
        private Matrix Tree5World { get; set; }
        private Matrix Tree6World { get; set; }
        private Matrix Tree7World { get; set; }
        private Matrix Tree8World { get; set; }
        private Matrix Tree9World { get; set; }
        private Matrix Tree10World { get; set; }
        private Matrix Tree11World { get; set; }
        private Matrix Tree12World { get; set; }

        private Matrix Rock1World { get; set; }
        private Matrix Rock2World { get; set; }
        private Matrix Rock3World { get; set; }
        private Matrix Rock4World { get; set; }
        private Matrix Rock5World { get; set; }
        private Matrix Rock6World { get; set; }
        private Matrix Rock7World { get; set; }
        private Matrix Rock8World { get; set; }
        private Matrix Rock9World { get; set; }
        private Matrix Rock10World { get; set; }
        private Matrix Rock11World { get; set; }
        private Matrix Rock12World { get; set; }
        private Matrix Rock13World { get; set; }
        private Matrix Rock14World { get; set; }
        private Matrix Rock15World { get; set; }
        private Matrix Rock16World { get; set; }
        private Matrix Rock17World { get; set; }
        private Matrix Rock18World { get; set; }
        private Matrix Rock19World { get; set; }
        private Matrix Rock20World { get; set; }
        private Matrix Rock21World { get; set; }
        private Matrix Rock22World { get; set; }
        private Matrix Rock23World { get; set; }
        private Matrix Rock24World { get; set; }
        private Matrix Rock25World { get; set; }
        private Matrix Rock26World { get; set; }
        private Matrix Rock27World { get; set; }
        private Matrix Rock28World { get; set; }
        private Matrix Rock29World { get; set; }
        private Matrix Rock30World { get; set; }
        private Matrix Rock31World { get; set; }
        private Matrix Rock32World { get; set; }

        // "Tire" del lado más cerca del origen de la rampa Rampa1World
        private Matrix Tire1world { get; set; }

        private Matrix Tire2world { get; set; }
        private Matrix Tire2world1 { get; set; }

        private Matrix Tire3world { get; set; }
        private Matrix Tire3world1 { get; set; }
        private Matrix Tire3world2 { get; set; }

        private Matrix Tire4world { get; set; }
        private Matrix Tire4world1 { get; set; }
        private Matrix Tire4world2 { get; set; }
        private Matrix Tire4world3 { get; set; }

        // "Tire" del lado más lejos del origen de la rampa Rampa1World
        private Matrix Tire5world { get; set; }

        private Matrix Tire6world { get; set; }
        private Matrix Tire6world1 { get; set; }

        private Matrix Tire7world { get; set; }
        private Matrix Tire7world1 { get; set; }
        private Matrix Tire7world2 { get; set; }
        private Matrix Tire8world { get; set; }
        private Matrix Tire8world1 { get; set; }
        private Matrix Tire8world2 { get; set; }
        private Matrix Tire8world3 { get; set; }

        // "Tire" del lado más lejos del origen de la rampa Rampa2World
        private Matrix Tire9world { get; set; }

        private Matrix Tire10world { get; set; }
        private Matrix Tire10world1 { get; set; }

        private Matrix Tire11world { get; set; }
        private Matrix Tire11world1 { get; set; }
        private Matrix Tire11world2 { get; set; }

        private Matrix Tire12world { get; set; }
        private Matrix Tire12world1 { get; set; }
        private Matrix Tire12world2 { get; set; }
        private Matrix Tire12world3 { get; set; }

        // "Tire" del lado más cerca del origen de la rampa Rampa2World
        private Matrix Tire13world { get; set; }

        private Matrix Tire14world { get; set; }
        private Matrix Tire14world1 { get; set; }

        private Matrix Tire15world { get; set; }
        private Matrix Tire15world1 { get; set; }
        private Matrix Tire15world2 { get; set; }

        private Matrix Tire16world { get; set; }
        private Matrix Tire16world1 { get; set; }
        private Matrix Tire16world2 { get; set; }
        private Matrix Tire16world3 { get; set; }

        // "Tire" del lado más cerca del lejos de la rampa Rampa3World
        private Matrix Tire17world { get; set; }

        private Matrix Tire18world { get; set; }
        private Matrix Tire18world1 { get; set; }

        private Matrix Tire19world { get; set; }
        private Matrix Tire19world1 { get; set; }
        private Matrix Tire19world2 { get; set; }

        // "Tire" del lado más cerca del lejos de la rampa Rampa3World
        private Matrix Tire20world { get; set; }

        private Matrix Tire21world { get; set; }
        private Matrix Tire21world1 { get; set; }

        private Matrix Tire22world { get; set; }
        private Matrix Tire22world1 { get; set; }
        private Matrix Tire22world2 { get; set; }

        // Variables
        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI / 2;

        //Bones
        private Matrix[] relativeMatrices;
        
      public void Initialize()
        {
            //Arboles piedras
            Tree1World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(30, 0, 350);
            Tree2World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(-25, 0, 350);
            Tree3World = Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(55, 0, 405);
            Tree4World = Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(-60, 0, 430);

            Tree5World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(325, 0, -250);
            Tree6World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(275, 0, -250);
            Tree7World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(300, 0, -200);
            Tree8World = Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(300, 0, -275);

            Tree9World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(-325, 0, -250);
            Tree10World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(-275, 0, -250);
            Tree11World = Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(-300, 0, -200);
            Tree12World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-300, 0, -275);

            Rock1World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -250);
            Rock2World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(25, 0, -250);
            Rock3World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(50, 0, -250);
            Rock4World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(75, 0, -250);

            Rock5World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 100);
            Rock6World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 125);
            Rock7World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 150);
            Rock8World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 175);

            Rock9World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(15, 0, 350);
            Rock10World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(-15, 0, 375);
            Rock11World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(15, 0, 400);
            Rock12World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(-15, 0, 425);

            Rock13World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-450, 0, 350);
            Rock14World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-460, 0, 375);
            Rock15World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-450, 0, 400);
            Rock16World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-460, 0, 425);
            Rock17World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-480, 0, 350);
            Rock18World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-490, 0, 375);
            Rock19World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-480, 0, 400);
            Rock20World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-490, 0, 425);
            Rock21World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-510, 0, 350);
            Rock22World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-520, 0, 375);
            Rock23World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-510, 0, 400);
            Rock24World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-520, 0, 425);

            Rock25World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-225, 0, 625);
            Rock26World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-255, 0, 650);
            Rock27World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-225, 0, 675);
            Rock28World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-255, 0, 700);
            Rock29World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-285, 0, 625);
            Rock30World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-315, 0, 650);
            Rock31World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-285, 0, 675);
            Rock32World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(-315, 0, 700);

            // "Tire" del lado más cerca del origen de la rampa Rampa1World
            Tire1world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-130, 5, -360);

            Tire2world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-110, 5, -360);
            Tire2world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-110, 10, -360);

            Tire3world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 5, -360);
            Tire3world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 10, -360);
            Tire3world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 15, -360);

            Tire4world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 5, -360);
            Tire4world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 10, -360);
            Tire4world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 15, -360);
            Tire4world3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 20, -360);

            // "Tire" del lado más lejos del origen de la rampa Rampa1World
            Tire5world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-130, 5, -450);

            Tire6world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-110, 5, -450);
            Tire6world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-110, 10, -450);

            Tire7world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 5, -450);
            Tire7world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 10, -450);
            Tire7world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-90, 15, -450);

            Tire8world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 5, -450);
            Tire8world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 10, -450);
            Tire8world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 15, -450);
            Tire8world3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-70, 20, -450);

            // "Tire" del lado más lejos del origen de la rampa Rampa2World
            Tire9world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(270, 5, -430);

            Tire10world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(250, 5, -430);
            Tire10world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(250, 10, -430);

            Tire11world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 5, -430);
            Tire11world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 10, -430);
            Tire11world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 15, -430);

            Tire12world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 5, -430);
            Tire12world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 10, -430);
            Tire12world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 15, -430);
            Tire12world3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 20, -430);

            // "Tire" del lado más cerca del origen de la rampa Rampa2World
            Tire13world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(270, 5, -340);

            Tire14world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(250, 5, -340);
            Tire14world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(250, 10, -340);

            Tire15world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 5, -340);
            Tire15world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 10, -340);
            Tire15world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(230, 15, -340);

            Tire16world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 5, -340);
            Tire16world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 10, -340);
            Tire16world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 15, -340);
            Tire16world3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(210, 20, -340);

            // "Tire" del lado más lejos del origen de la rampa Rampa3World
            Tire17world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 5, -35);

            Tire18world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 5, -15);
            Tire18world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 10, -15);

            Tire19world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 5, 5);
            Tire19world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 10, 5);
            Tire19world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-280, 15, 5);

            // "Tire" del lado más cerca del origen de la rampa Rampa3World
            Tire20world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 5, -35);

            Tire21world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 5, -15);
            Tire21world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 10, -15);

            Tire22world = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 5, 5);
            Tire22world1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 10, 5);
            Tire22world2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(-190, 15, 5);
        }

        public void LoadContent(Model tree, Model rock1, Model rock5, Model rock10, Model tire)
        {
        
            Tree = tree;
            Rock1 = rock1;
            Rock5 = rock5;
            Rock10 = rock10;
            Tire = tire;

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


        public void dibujarArboles(Matrix view,Matrix projection,Effect effect,Matrix matrizMundo, Model modelo, Color color)//con el otro metodo por alguna razon el modelo del arbol no se dibuja
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

            foreach (var mesh in modelo.Meshes)
            {
                effect.Parameters["World"].SetValue(matrizMundo);
                mesh.Draw();
            }
        }
        public void dibujarDetalles(Matrix view,Matrix projection,Effect effect)
        {
            dibujarArboles(view,projection,effect,Tree1World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree2World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree3World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree4World, Tree, Color.Black);

            dibujarArboles(view,projection,effect,Tree5World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree6World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree7World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree8World, Tree, Color.Black);

            dibujarArboles(view,projection,effect,Tree9World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree10World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree11World, Tree, Color.Black);
            dibujarArboles(view,projection,effect,Tree12World, Tree, Color.Black);

            dibujar(view,projection,effect,Rock1World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock2World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock3World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock4World, Rock1, Color.Gray);

            dibujar(view,projection,effect,Rock5World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock6World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock7World, Rock1, Color.Gray);
            dibujar(view,projection,effect,Rock8World, Rock1, Color.Gray);

            dibujar(view,projection,effect,Rock9World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock10World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock11World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock12World, Rock5, Color.Gray);

            dibujar(view,projection,effect,Rock13World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock14World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock15World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock16World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock17World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock18World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock19World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock20World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock21World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock22World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock23World, Rock10, Color.Gray);
            dibujar(view,projection,effect,Rock24World, Rock5, Color.Gray);

            dibujar(view,projection,effect,Rock25World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock26World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock27World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock28World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock29World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock30World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock31World, Rock5, Color.Gray);
            dibujar(view,projection,effect,Rock32World, Rock5, Color.Gray);

            // "Tire" del lado más cerca del origen de la rampa Rampa1World
            dibujar(view,projection,effect,Tire1world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire2world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire2world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire3world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire3world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire3world2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire4world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire4world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire4world2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire4world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa1World
            dibujar(view,projection,effect,Tire5world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire6world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire6world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire7world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire7world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire7world2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire8world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire8world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire8world2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire8world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa2World
            dibujar(view,projection,effect,Tire9world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire10world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire10world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire11world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire11world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire11world2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire12world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire12world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire12world2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire12world3, Tire, Color.Gray);

            // "Tire" del lado más cerca del origen de la rampa Rampa2World
            dibujar(view,projection,effect,Tire13world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire14world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire14world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire15world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire15world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire15world2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire16world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire16world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire16world2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire16world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa3World
            dibujar(view,projection,effect,Tire17world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire18world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire18world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire19world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire19world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire19world2, Tire, Color.Black);

            // "Tire" del lado más cerca del origen de la rampa Rampa3World
            dibujar(view,projection,effect,Tire20world, Tire, Color.Black);

            dibujar(view,projection,effect,Tire21world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire21world1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire22world, Tire, Color.Black);
            dibujar(view,projection,effect,Tire22world1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire22world2, Tire, Color.Black);
        }

    }
}