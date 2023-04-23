using System;
using System.Collections.Generic;
using BepuPhysics.CollisionDetection.CollisionTasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP
{
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        public TGCGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Effect Effect { get; set; }

        // Modelos
        private Model Piso { get; set; }
        private Model Pared { get; set; }
        private Model Column { get; set; }
        private Model Ramp { get; set; }
        private Model Platform { get; set; }
        private Model Cube { get; set; }
        private Model AutoDeportivo { get; set; }
        private Model AutoDeCombate { get; set; }
        private Model Tree { get; set; }
        private Model Rock1 { get; set; }

        // Matrices de Mundo
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }

        private Matrix PisoWorld { get; set; }
        private Matrix ParedWorld { get; set; }

        private Matrix AutoPrincipalWorld { get; set; }
        private Matrix Auto1World { get; set; }
        private Matrix Auto2World { get; set; }
        private Matrix Auto3World { get; set; }
        private Matrix Auto4World { get; set; }
        private Matrix Auto5World { get; set; }
        private Matrix AutoCombate1World { get; set; }
        private Matrix AutoCombate2World { get; set; }
        private Matrix AutoCombate3World { get; set; }

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

        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI / 2;

        protected override void Initialize()
        {
            // Dimensiones de la pantalla
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.ApplyChanges();

            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            inicializarEscenario();
            inicializarPlataformas();
            inicializarAutos();
            inicializarDetalles();

            View = Matrix.CreateLookAt(new Vector3(-550, 1000, -200), Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 1500);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Piso = Content.Load<Model>(ContentFolder3D + "Arena/Plano");
            Pared = Content.Load<Model>(ContentFolder3D + "Arena/Arena");

            Column = Content.Load<Model>(ContentFolder3D + "Platforms/Column/Column");
            Ramp = Content.Load<Model>(ContentFolder3D + "Platforms/Ramps/Ramp");
            Platform = Content.Load<Model>(ContentFolder3D + "Platforms/Cubo/Cube");

            AutoDeportivo = Content.Load<Model>(ContentFolder3D + "Derby/RacingCar/RacingCar");
            AutoDeCombate = Content.Load<Model>(ContentFolder3D + "Derby/CombatVehicle/Vehicle");

            Tree = Content.Load<Model>(ContentFolder3D + "Decoration/ArbolSinHojas/TreeWinter");
            Rock1 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock1");

            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            dibujarEscenario();
            dibujarPlataformas();
            dibujarAutos();
            dibujarDetalles();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }

        // Funciones Auxiliares
        public void dibujar(Matrix matrizMundo, Model modelo, Color color)
        {
            foreach (var mesh in modelo.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            Effect.Parameters["View"].SetValue(View);
            Effect.Parameters["Projection"].SetValue(Projection);
            Effect.Parameters["DiffuseColor"].SetValue(color.ToVector3());

            foreach (var mesh in modelo.Meshes)
            {
                Effect.Parameters["World"].SetValue(matrizMundo);
                mesh.Draw();
            }
        }

        public void inicializarEscenario()
        {
            PisoWorld = Matrix.CreateScale(30, 0, 30);
            ParedWorld = Matrix.CreateScale(30, 50, 30);
        }
        public void dibujarEscenario()
        {
            dibujar(PisoWorld, Piso, Color.LightGoldenrodYellow);
            dibujar(ParedWorld, Pared, Color.Wheat);
        }

        public void inicializarPlataformas()
        {
            Column1World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -450);
            Column2World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -350);
            Column3World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140, 0, -450);
            Column4World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140, 0, -350);

            Ramp1World = Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-80, 0, -390);
            Ramp2World = Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(230, 0, -400);

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
        }
        public void dibujarPlataformas()
        {
            dibujar(Column1World, Column, Color.SandyBrown);
            dibujar(Column2World, Column, Color.SandyBrown);
            dibujar(Column3World, Column, Color.SandyBrown);
            dibujar(Column4World, Column, Color.SandyBrown);

            dibujar(Ramp1World, Ramp, Color.Gray);
            dibujar(Ramp2World, Ramp, Color.Gray);

            dibujar(Platform1World, Platform, Color.DarkSalmon);

            dibujar(Column5World, Column, Color.SandyBrown);
            dibujar(Column6World, Column, Color.SandyBrown);
            dibujar(Column7World, Column, Color.SandyBrown);
            dibujar(Column8World, Column, Color.SandyBrown);

            dibujar(Platform2World, Platform, Color.DarkSalmon);

            dibujar(Column9World, Column, Color.SandyBrown);
            dibujar(Column10World, Column, Color.SandyBrown);

            dibujar(Ramp3World, Ramp, Color.Gray);
            dibujar(Ramp4World, Ramp, Color.Gray);

            dibujar(Platform3World, Platform, Color.DarkSalmon);

            dibujar(Ramp5World, Ramp, Color.DarkGray);
            dibujar(Ramp6World, Ramp, Color.DarkGray);
            dibujar(Ramp7World, Ramp, Color.DarkGray);
            dibujar(Ramp8World, Ramp, Color.DarkGray);

            dibujar(Platform4World, Platform, Color.DarkGray);

            dibujar(Ramp9World, Ramp, Color.DarkGray);
            dibujar(Ramp10World, Ramp, Color.DarkGray);
            dibujar(Ramp11World, Ramp, Color.DarkGray);
            dibujar(Ramp12World, Ramp, Color.DarkGray);

            dibujar(Platform5World, Platform, Color.DarkGray);
        }

        public void inicializarAutos()
        {
            AutoPrincipalWorld = Matrix.CreateScale(5) * Matrix.CreateTranslation(0, 0, 0);

            Auto1World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(0, 0, 50);
            Auto2World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(20, 0, 50);
            Auto3World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(40, 0, 50);
            Auto4World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-20, 0, 50);
            Auto5World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-40, 0, 50);

            AutoCombate1World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, 100);
            AutoCombate2World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(35, 0, 100);
            AutoCombate3World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(-35, 0, 100);
        }
        public void dibujarAutos()
        {
            dibujar(AutoPrincipalWorld, AutoDeportivo, Color.DarkRed);

            dibujar(Auto1World, AutoDeportivo, Color.Black);
            dibujar(Auto2World, AutoDeportivo, Color.Black);
            dibujar(Auto3World, AutoDeportivo, Color.Black);
            dibujar(Auto4World, AutoDeportivo, Color.Black);
            dibujar(Auto5World, AutoDeportivo, Color.Black);

            dibujar(AutoCombate1World, AutoDeCombate, Color.DarkGray);
            dibujar(AutoCombate2World, AutoDeCombate, Color.DarkGray);
            dibujar(AutoCombate3World, AutoDeCombate, Color.DarkGray);
        }

        public void inicializarDetalles()
        {
            Tree1World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(25, 0, 350);
            Tree2World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-25, 0, 350);
            Tree3World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(0, 0, 300);
            Tree4World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(0, 0, 375);

            Tree5World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(325, 0, -250);
            Tree6World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(275, 0, -250);
            Tree7World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(300, 0, -200);
            Tree8World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(300, 0, -275);

            Tree9World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-325, 0, -250);
            Tree10World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-275, 0, -250);
            Tree11World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-300, 0, -200);
            Tree12World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-300, 0, -275);

            Rock1World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -250);
            Rock2World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(25, 0, -250);
            Rock3World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(50, 0, -250);
            Rock4World = Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(75, 0, -250);

            Rock5World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 100);
            Rock6World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 125);
            Rock7World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 150);
            Rock8World = Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(350, 0, 175);

            BrokenColumn1World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationY(-MathF.PI/6) * Matrix.CreateTranslation(450,0,250);
            BrokenColumn2World = Matrix.CreateScale(0.7f) * Matrix.CreateRotationY(MathF.PI/6) * Matrix.CreateTranslation(-230, 0, -290);

            Column11World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-500,0,0);
            Column12World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(436, 0, -80);
        }
        public void dibujarDetalles()
        {
            dibujar(Tree1World, Tree, Color.Black);
            dibujar(Tree2World, Tree, Color.Black);
            dibujar(Tree3World, Tree, Color.Black);
            dibujar(Tree4World, Tree, Color.Black);

            dibujar(Tree5World, Tree, Color.Black);
            dibujar(Tree6World, Tree, Color.Black);
            dibujar(Tree7World, Tree, Color.Black);
            dibujar(Tree8World, Tree, Color.Black);

            dibujar(Tree9World, Tree, Color.Black);
            dibujar(Tree10World, Tree, Color.Black);
            dibujar(Tree11World, Tree, Color.Black);
            dibujar(Tree12World, Tree, Color.Black);

            dibujar(Rock1World, Rock1, Color.Gray);
            dibujar(Rock2World, Rock1, Color.Gray);
            dibujar(Rock3World, Rock1, Color.Gray);
            dibujar(Rock4World, Rock1, Color.Gray);

            dibujar(Rock5World, Rock1, Color.Gray);
            dibujar(Rock6World, Rock1, Color.Gray);
            dibujar(Rock7World, Rock1, Color.Gray);
            dibujar(Rock8World, Rock1, Color.Gray);

            dibujar(BrokenColumn1World, Column, Color.SandyBrown);
            dibujar(BrokenColumn2World, Column, Color.SandyBrown);

            dibujar(Column11World, Column, Color.SandyBrown);
            dibujar(Column12World, Column, Color.SandyBrown);
        }
    }
}