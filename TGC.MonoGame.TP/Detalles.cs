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

        public Matrix Tree1World { get; set; }
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
        private Matrix Tire1World { get; set; }

        private Matrix Tire2World { get; set; }
        private Matrix Tire2World1 { get; set; }

        private Matrix Tire3World { get; set; }
        private Matrix Tire3World1 { get; set; }
        private Matrix Tire3World2 { get; set; }

        private Matrix Tire4World { get; set; }
        private Matrix Tire4World1 { get; set; }
        private Matrix Tire4World2 { get; set; }
        private Matrix Tire4World3 { get; set; }

        // "Tire" del lado más lejos del origen de la rampa Rampa1World
        private Matrix Tire5World { get; set; }

        private Matrix Tire6World { get; set; }
        private Matrix Tire6World1 { get; set; }

        private Matrix Tire7World { get; set; }
        private Matrix Tire7World1 { get; set; }
        private Matrix Tire7World2 { get; set; }

        private Matrix Tire8World { get; set; }
        private Matrix Tire8World1 { get; set; }
        private Matrix Tire8World2 { get; set; }
        private Matrix Tire8World3 { get; set; }

        // "Tire" del lado más lejos del origen de la rampa Rampa2World
        private Matrix Tire9World { get; set; }

        private Matrix Tire10World { get; set; }
        private Matrix Tire10World1 { get; set; }

        private Matrix Tire11World { get; set; }
        private Matrix Tire11World1 { get; set; }
        private Matrix Tire11World2 { get; set; }

        private Matrix Tire12World { get; set; }
        private Matrix Tire12World1 { get; set; }
        private Matrix Tire12World2 { get; set; }
        private Matrix Tire12World3 { get; set; }

        // "Tire" del lado más cerca del origen de la rampa Rampa2World
        private Matrix Tire13World { get; set; }

        private Matrix Tire14World { get; set; }
        private Matrix Tire14World1 { get; set; }

        private Matrix Tire15World { get; set; }
        private Matrix Tire15World1 { get; set; }
        private Matrix Tire15World2 { get; set; }

        private Matrix Tire16World { get; set; }
        private Matrix Tire16World1 { get; set; }
        private Matrix Tire16World2 { get; set; }
        private Matrix Tire16World3 { get; set; }

        // "Tire" del lado más cerca del lejos de la rampa Rampa3World
        private Matrix Tire17World { get; set; }

        private Matrix Tire18World { get; set; }
        private Matrix Tire18World1 { get; set; }

        private Matrix Tire19World { get; set; }
        private Matrix Tire19World1 { get; set; }
        private Matrix Tire19World2 { get; set; }

        // "Tire" del lado más cerca del lejos de la rampa Rampa3World
        private Matrix Tire20World { get; set; }

        private Matrix Tire21World { get; set; }
        private Matrix Tire21World1 { get; set; }

        private Matrix Tire22World { get; set; }
        private Matrix Tire22World1 { get; set; }
        private Matrix Tire22World2 { get; set; }

        public Vector3 Tree1Position = new Vector3 (100,0,0); //30, 0, 350
        public Vector3 Tree2Position = new Vector3 (-25, 0, 350);
        public Vector3 Tree3Position = new Vector3 (55, 0, 405);
        public Vector3 Tree4Position = new Vector3 (-60, 0, 430);
        public Vector3 Tree5Position = new Vector3 (325, 0, -250);
        public Vector3 Tree6Position = new Vector3 (275, 0, -200);
        public Vector3 Tree7Position = new Vector3 (300, 0, -275);
        public Vector3 Tree8Position = new Vector3 (-325, 0, -250);
        public Vector3 Tree9Position = new Vector3 (-325, 0, -250);
        public Vector3 Tree10Position = new Vector3 (-275, 0, -250);
        public Vector3 Tree11Position = new Vector3 (-300, 0, -200);
        public Vector3 Tree12Position = new Vector3 (-300, 0, -275);

        public Vector3 Rock1Position = new Vector3 (0, 0, -250);
        public Vector3 Rock2Position = new Vector3 (25, 0, -250);
        public Vector3 Rock3Position = new Vector3 (50, 0, -250);
        public Vector3 Rock4Position = new Vector3 (75, 0, -250);
        public Vector3 Rock5Position = new Vector3 (350, 0, 100);
        public Vector3 Rock6Position = new Vector3 (350, 0, 125);     
        public Vector3 Rock7Position = new Vector3 (350, 0, 150); 
        public Vector3 Rock8Position = new Vector3 (350, 0, 150);
        public Vector3 Rock9Position = new Vector3 (15, 0, 350);
        public Vector3 Rock10Position = new Vector3 (-15, 0, 375);
        public Vector3 Rock11Position = new Vector3 (15, 0, 400);
        public Vector3 Rock12Position = new Vector3 (-15, 0, 425);
        public Vector3 Rock13Position = new Vector3 (-450, 0, 350);
        public Vector3 Rock14Position = new Vector3 (-460, 0, 375);
        public Vector3 Rock15Position = new Vector3 (-450, 0, 400);
        public Vector3 Rock16Position = new Vector3 (-460, 0, 425);
        public Vector3 Rock17Position = new Vector3 (-480, 0, 350);
        public Vector3 Rock18Position = new Vector3 (-490, 0, 375);
        public Vector3 Rock19Position = new Vector3 (-480, 0, 400);
        public Vector3 Rock20Position = new Vector3 (-490, 0, 425);
        public Vector3 Rock21Position = new Vector3 (-510, 0, 350);
        public Vector3 Rock22Position = new Vector3 (-520, 0, 375);
        public Vector3 Rock23Position = new Vector3 (-510, 0, 400);
        public Vector3 Rock24Position = new Vector3 (-520, 0, 425);
        public Vector3 Rock25Position = new Vector3 (-225, 0, 625);
        public Vector3 Rock26Position = new Vector3 (-255, 0, 650);
        public Vector3 Rock27Position = new Vector3 (-225, 0, 675);
        public Vector3 Rock28Position = new Vector3 (-255, 0, 700);
        public Vector3 Rock29Position = new Vector3 (-285, 0, 625);
        public Vector3 Rock30Position = new Vector3 (-315, 0, 650);
        public Vector3 Rock31Position = new Vector3 (-285, 0, 675);
        public Vector3 Rock32Position = new Vector3 (-315, 0, 700);

        // "Tire" del lado más cerca del origen de la rampa Rampa1Position
        public Vector3 Tire1Position = new Vector3 (-130, 5, -360);

        public Vector3 Tire2Position = new Vector3 (-110, 5, -360);
        public Vector3 Tire2Position1 = new Vector3 (-110, 10, -360);

        public Vector3 Tire3Position = new Vector3 (-90, 5, -360);
        public Vector3 Tire3Position1 = new Vector3 (-90, 10, -360);
        public Vector3 Tire3Position2 = new Vector3 (-90, 15, -360);

        public Vector3 Tire4Position = new Vector3 (-70, 5, -360);
        public Vector3 Tire4Position1 = new Vector3 (-70, 10, -360);
        public Vector3 Tire4Position2 = new Vector3 (-70, 15, -360);
        public Vector3 Tire4Position3 = new Vector3 (-70, 20, -360);

        // "Tire" del lado más lejos del origen de la rampa Rampa1Position
        public Vector3 Tire5Position = new Vector3 (-130, 5, -450);

        public Vector3 Tire6Position = new Vector3 (-110, 5, -450);
        public Vector3 Tire6Position1 = new Vector3 (-110, 10, -450);

        public Vector3 Tire7Position = new Vector3 (-90, 5, -450);
        public Vector3 Tire7Position1 = new Vector3 (-90, 10, -450);
        public Vector3 Tire7Position2 = new Vector3 (-90, 15, -450);
        public Vector3 Tire8Position = new Vector3 (-70, 5, -450);
        public Vector3 Tire8Position1 = new Vector3 (-70, 10, -450);
        public Vector3 Tire8Position2 = new Vector3 (-70, 15, -450);
        public Vector3 Tire8Position3 = new Vector3 (-70, 20, -450);

        // "Tire" del lado más lejos del origen de la rampa Rampa2Position
        public Vector3 Tire9Position = new Vector3 (270, 5, -430);

        public Vector3 Tire10Position = new Vector3 (250, 5, -430);
        public Vector3 Tire10Position1 = new Vector3 (250, 10, -430);

        public Vector3 Tire11Position = new Vector3 (230, 5, -430);
        public Vector3 Tire11Position1 = new Vector3 (230, 10, -430);
        public Vector3 Tire11Position2 = new Vector3 (230, 15, -430);

        public Vector3 Tire12Position = new Vector3 (210, 5, -430);
        public Vector3 Tire12Position1 = new Vector3 (210, 10, -430);
        public Vector3 Tire12Position2 = new Vector3 (210, 15, -430);
        public Vector3 Tire12Position3 = new Vector3 (210, 20, -430);

        // "Tire" del lado más cerca del origen de la rampa Rampa2Position
        public Vector3 Tire13Position = new Vector3 (270, 5, -340);

        public Vector3 Tire14Position = new Vector3 (250, 5, -340);
        public Vector3 Tire14Position1 = new Vector3 (250, 10, -340);

        public Vector3 Tire15Position = new Vector3 (230, 5, -340);
        public Vector3 Tire15Position1 = new Vector3 (230, 10, -340);
        public Vector3 Tire15Position2 = new Vector3 (230, 15, -340);

        public Vector3 Tire16Position = new Vector3 (210, 5, -340);
        public Vector3 Tire16Position1 = new Vector3 (210, 10, -340);
        public Vector3 Tire16Position2 = new Vector3 (210, 15, -340);
        public Vector3 Tire16Position3 = new Vector3 (210, 20, -340);

        // "Tire" del lado más cerca del lejos de la rampa Rampa3Position
        public Vector3 Tire17Position = new Vector3 (-280, 5, -35);

        public Vector3 Tire18Position = new Vector3 (-280, 5, -15);
        public Vector3 Tire18Position1 = new Vector3 (-280, 10, -15);

        public Vector3 Tire19Position = new Vector3 (-280, 5, 5);
        public Vector3 Tire19Position1 = new Vector3 (-280, 10, 5);
        public Vector3 Tire19Position2 = new Vector3 (-280, 15, 5);

        // "Tire" del lado más cerca del lejos de la rampa Rampa3Position
        public Vector3 Tire20Position = new Vector3 (-190, 5, -35);

        public Vector3 Tire21Position = new Vector3 (-190, 5, -15);
        public Vector3 Tire21Position1 = new Vector3 (-190, 10, -15);

        public Vector3 Tire22Position = new Vector3 (-190, 5, 5);
        public Vector3 Tire22Position1 = new Vector3 (-190, 10, 5);
        public Vector3 Tire22Position2 = new Vector3 (-190, 15, 5);


        // Variables
        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI / 2;

        //Bones
        private Matrix[] relativeMatrices;
        
      public void Initialize()
        {
            //Árboles
            Tree1World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree1Position);
            Tree2World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree2Position);
            Tree3World = Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(Tree3Position);
            Tree4World = Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(Tree4Position);

            Tree5World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree5Position);
            Tree6World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree6Position);
            Tree7World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree7Position);
            Tree8World = Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(Tree8Position);

            Tree9World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree9Position);
            Tree10World = Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree10Position);
            Tree11World = Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(Tree11Position);
            Tree12World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree12Position);

            //Piedras
            Rock1World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock1Position);
            Rock2World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock2Position);
            Rock3World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock3Position);
            Rock4World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock4Position);

            Rock5World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock5Position);
            Rock6World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock6Position);
            Rock7World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock7Position);
            Rock8World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock8Position);

            Rock9World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock9Position);
            Rock10World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock10Position);
            Rock11World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock11Position);
            Rock12World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock12Position);

            Rock13World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock13Position);
            Rock14World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock14Position);
            Rock15World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock15Position);
            Rock16World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock16Position);
            Rock17World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock17Position);
            Rock18World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock18Position);
            Rock19World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock19Position);
            Rock20World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock20Position);
            Rock21World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock21Position);
            Rock22World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock22Position);
            Rock23World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock23Position);
            Rock24World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock24Position);

            Rock25World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock25Position);
            Rock26World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock26Position);
            Rock27World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock27Position);
            Rock28World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock28Position);
            Rock29World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock29Position);
            Rock30World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock30Position);
            Rock31World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock31Position);
            Rock32World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock32Position);

            // "Tire" del lado más cerca del origen de la rampa Rampa1World
            Tire1World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire1Position);

            Tire2World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire2Position);
            Tire2World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire2Position1);

            Tire3World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire3Position);
            Tire3World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire3Position1);
            Tire3World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire3Position2);

            Tire4World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire4Position);
            Tire4World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire4Position1);
            Tire4World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire4Position2);
            Tire4World3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire4Position3);

            // "Tire" del lado más lejos del origen de la rampa Rampa1World
            Tire5World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire5Position);

            Tire6World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire6Position);
            Tire6World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire6Position1);

            Tire7World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire7Position);
            Tire7World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire7Position1);
            Tire7World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire7Position2);

            Tire8World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire8Position);
            Tire8World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire8Position1);
            Tire8World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire8Position2);
            Tire8World3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire8Position3);

            // "Tire" del lado más lejos del origen de la rampa Rampa2World
            Tire9World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire9Position);

            Tire10World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire10Position);
            Tire10World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire10Position1);

            Tire11World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire11Position);
            Tire11World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire11Position1);
            Tire11World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire11Position2);

            Tire12World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire12Position);
            Tire12World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire12Position1);
            Tire12World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire12Position2);
            Tire12World3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire12Position3);

            // "Tire" del lado más cerca del origen de la rampa Rampa2World
            Tire13World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire13Position);

            Tire14World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire14Position);
            Tire14World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire14Position1);

            Tire15World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire15Position);
            Tire15World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire15Position1);
            Tire15World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire15Position2);

            Tire16World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire16Position);
            Tire16World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire16Position1);
            Tire16World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire16Position2);
            Tire16World3 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire16Position3);

            // "Tire" del lado más lejos del origen de la rampa Rampa3World
            Tire17World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire17Position);

            Tire18World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire18Position);
            Tire18World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire18Position1);

            Tire19World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire19Position);
            Tire19World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire19Position1);
            Tire19World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire19Position2);

            // "Tire" del lado más cerca del origen de la rampa Rampa3World
            Tire20World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire20Position);

            Tire21World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire21Position);
            Tire21World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire21Position1);

            Tire22World = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire22Position);
            Tire22World1 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire22Position1);
            Tire22World2 = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(Tire22Position2);

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
            dibujar(view,projection,effect,Tire1World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire2World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire2World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire3World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire3World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire3World2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire4World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire4World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire4World2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire4World3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa1World
            dibujar(view,projection,effect,Tire5World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire6World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire6World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire7World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire7World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire7World2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire8World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire8World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire8World2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire8World3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa2World
            dibujar(view,projection,effect,Tire9World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire10World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire10World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire11World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire11World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire11World2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire12World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire12World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire12World2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire12World3, Tire, Color.Gray);

            // "Tire" del lado más cerca del origen de la rampa Rampa2World
            dibujar(view,projection,effect,Tire13World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire14World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire14World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire15World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire15World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire15World2, Tire, Color.Black);

            dibujar(view,projection,effect,Tire16World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire16World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire16World2, Tire, Color.Black);
            dibujar(view,projection,effect,Tire16World3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa3World
            dibujar(view,projection,effect,Tire17World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire18World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire18World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire19World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire19World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire19World2, Tire, Color.Black);

            // "Tire" del lado más cerca del origen de la rampa Rampa3World
            dibujar(view,projection,effect,Tire20World, Tire, Color.Black);

            dibujar(view,projection,effect,Tire21World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire21World1, Tire, Color.Gray);

            dibujar(view,projection,effect,Tire22World, Tire, Color.Black);
            dibujar(view,projection,effect,Tire22World1, Tire, Color.Gray);
            dibujar(view,projection,effect,Tire22World2, Tire, Color.Black);
        }

    }
}