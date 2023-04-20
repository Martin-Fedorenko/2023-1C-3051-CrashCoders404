using System;
using System.Collections.Generic;
using BepuPhysics.CollisionDetection.CollisionTasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP
{
    /// <summary>
    ///     Esta es la clase principal del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer mas ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);
            // Para que el juego sea pantalla completa se puede usar Graphics IsFullScreen.
            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = true;
        }

        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Effect Effect { get; set; }

        private Model Piso { get; set; }
        private Model Paredes { get; set; }
        private Model AutoDeportivo { get; set; }
        private Model AutoDeCombate { get; set; }
        private Model Arbol { get; set; }
        private Model Rock9 { get; set; }
        private Model Rock2 { get; set; }
        private Model Grass { get; set; }
        private Model Column { get; set; }
        private Model Ramp { get; set; }

        private Matrix View { get; set; }
        private Matrix Projection { get; set; }
        private Matrix AutoWorld { get; set; }
        private Matrix Auto2World { get; set; }
        private Matrix Auto3World { get; set; }
        private Matrix Auto4World { get; set; }
        private Matrix Auto5World { get; set; }
        private Matrix Auto6World { get; set; }
        private Matrix Auto7World { get; set; }

        private Matrix PisoWorld { get; set; }
        private Matrix ParedWorld { get; set; }
        private Matrix ArbolWorld { get; set; }
        private Matrix Arbol2World { get; set; }
        private Matrix Arbol3World { get; set; }
        private Matrix Arbol4World { get; set; }
        private Matrix Arbol5World { get; set; }
        private Matrix Rock1World { get; set; }
        private Matrix Rock2World { get; set; }
        private Matrix Rock3World { get; set; }
        private Matrix Rock4World { get; set; }
        private Matrix Rock5World { get; set; }
        private Matrix Rock6World { get; set; }

        private Matrix Grass1World { get; set; }
        private Matrix Grass2World { get; set; }
        private Matrix Grass3World { get; set; }
        private Matrix Grass4World { get; set; }
        private Matrix Grass5World { get; set; }
        private Matrix Grass6World { get; set; }

        private Matrix Column1World { get; set; }
        private Matrix Column2World { get; set; }
        
        private Matrix Ramp1World { get; set; }




        //radio de la arena= = 500 unidades aprox.

        protected override void Initialize()
        {

            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;


            Rock1World = Matrix.CreateScale(0.025f) * Matrix.CreateTranslation(-50, 0, 30);
            Rock2World = Matrix.CreateScale(0.035f) * Matrix.CreateTranslation(40, 0, 50);
            Rock3World = Matrix.CreateScale(0.035f) * Matrix.CreateTranslation(15, 0, 90);
            Rock4World = Matrix.CreateScale(0.035f) * Matrix.CreateTranslation(100, 0, -30);

            ArbolWorld = Matrix.CreateScale(0.25f) * Matrix.CreateTranslation(50, 0, -50);
            Arbol2World = Matrix.CreateScale(0.35f) * Matrix.CreateTranslation(20, 0, 10);
            Arbol3World = Matrix.CreateScale(0.20f) * Matrix.CreateTranslation(-40, 0, 50);
            Arbol4World = Matrix.CreateScale(0.27f) * Matrix.CreateTranslation(-40, 0, 320);
            Arbol5World = Matrix.CreateScale(0.32f) * Matrix.CreateTranslation(130, 0, -280);

            AutoWorld = Matrix.CreateScale(5);
            Auto2World = Matrix.CreateScale(5) * Matrix.CreateRotationY(MathF.PI / 2) * Matrix.CreateTranslation(-80, 0, 70);
            Auto3World = Matrix.CreateScale(5) * Matrix.CreateRotationY(MathF.PI * 3 / 2) * Matrix.CreateTranslation(0, 0, 250);
            Auto4World = Matrix.CreateScale(5) * Matrix.CreateRotationY(MathF.PI) * Matrix.CreateTranslation(-240, 0, 70);
            Auto5World = Matrix.CreateScale(0.35F) * Matrix.CreateTranslation(220, 0, 140);
            Auto6World = Matrix.CreateScale(0.35F) * Matrix.CreateTranslation(110, 0, -170);
            Auto7World = Matrix.CreateScale(0.35F) * Matrix.CreateTranslation(0, 0, -115);

            Grass1World = Matrix.CreateTranslation(-330, 0, -40);
            Grass2World = Matrix.CreateTranslation(-320, 0, -40);
            Grass3World = Matrix.CreateTranslation(-310, 0, -40);
            Grass4World = Matrix.CreateTranslation(-330, 0, -30);
            Grass5World = Matrix.CreateTranslation(-320, 0, -30);
            Grass6World = Matrix.CreateTranslation(-310, 0, -30);

            Column1World = Matrix.CreateScale(0.5f) * Matrix.CreateRotationX(-MathF.PI/2) * Matrix.CreateTranslation(350,0,120);
            Column2World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(-350, 0, 120);

            Ramp1World = Matrix.CreateScale(0.25f) * Matrix.CreateRotationX(-MathF.PI/2) * Matrix.CreateTranslation(-150, 0, 180);


            PisoWorld = Matrix.CreateScale(10);
            ParedWorld = Matrix.CreateScale(25);

            View = Matrix.CreateLookAt(new Vector3(0, 600, 450), Vector3.Zero, Vector3.Up);
            Projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 1500);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Piso = Content.Load<Model>(ContentFolder3D + "arena/plano");
            Paredes = Content.Load<Model>(ContentFolder3D + "arena/arena");
            AutoDeportivo = Content.Load<Model>(ContentFolder3D + "Derby/RacingCarA/RacingCar");
            AutoDeCombate = Content.Load<Model>(ContentFolder3D + "Derby/CombatVehicle/Vehicle");
            Arbol = Content.Load<Model>(ContentFolder3D + "arboles/arbolSinHojas/tree_winter");
            Rock9 = Content.Load<Model>(ContentFolder3D + "Rocks/Rock9");
            Rock2 = Content.Load<Model>(ContentFolder3D + "Rocks/Rock2");
            Column = Content.Load<Model>(ContentFolder3D + "column/column");
            Grass = Content.Load<Model>(ContentFolder3D + "grass/Low Grass");
            Ramp = Content.Load<Model>(ContentFolder3D + "ramps/ramp");



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

            this.dibujarEscenario();
            this.dibujarDetalles();
            this.dibujarAutos();
        }


        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }

        public void dibujarEscenario()
        {
            this.dibujar(PisoWorld, Piso, Color.IndianRed);
            this.dibujar(ParedWorld, Paredes, Color.White);
        }

        public void dibujarDetalles()
        {
            this.dibujar(Grass1World, Grass, Color.ForestGreen);
            this.dibujar(Grass2World, Grass, Color.ForestGreen);
            this.dibujar(Grass3World, Grass, Color.ForestGreen);
            this.dibujar(Grass4World, Grass, Color.ForestGreen);
            this.dibujar(Grass5World, Grass, Color.ForestGreen);
            this.dibujar(Grass6World, Grass, Color.ForestGreen);

            this.dibujar(Rock1World, Rock9, Color.Gray);
            this.dibujar(Rock2World, Rock2, Color.Gray);
            this.dibujar(Rock3World, Rock9, Color.Gray);
            this.dibujar(Rock4World, Rock2, Color.Gray);


            this.dibujar(ArbolWorld, Arbol, Color.SandyBrown);
            this.dibujar(Arbol2World, Arbol, Color.SandyBrown);
            this.dibujar(Arbol3World, Arbol, Color.SandyBrown);
            this.dibujar(Arbol4World, Arbol, Color.SandyBrown);
            this.dibujar(Arbol5World, Arbol, Color.SandyBrown);

            this.dibujar(Column1World, Column, Color.White);
            this.dibujar(Column2World, Column, Color.White);

            this.dibujar(Ramp1World, Ramp, Color.Black);
        }

        public void dibujarAutos()
        {
            this.dibujar(AutoWorld, AutoDeportivo, Color.Black);
            this.dibujar(Auto2World, AutoDeportivo, Color.Black);
            this.dibujar(Auto3World, AutoDeportivo, Color.Black);
            this.dibujar(Auto4World, AutoDeportivo, Color.Black);
            this.dibujar(Auto5World, AutoDeCombate, Color.DarkSlateGray);
            this.dibujar(Auto6World, AutoDeCombate, Color.DarkSlateGray);
            this.dibujar(Auto7World, AutoDeCombate, Color.DarkSlateGray);
        }


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

    }
}