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
        private Model Platform { get; set; }

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

        private Matrix Ramp1World { get; set; }
        private Matrix Ramp2World { get; set; }
        private Matrix Ramp3World { get; set; }
        private Matrix Ramp4World { get; set; }

        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI/2;


        //radio de la arena= = 500 unidades aprox.

        protected override void Initialize()
        {

            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            inicializarArena();
            inicializarAutos();
            inicializarPlataformas();
            

            View = Matrix.CreateLookAt(new Vector3(500, 500, 200), Vector3.Zero, Vector3.Up);
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
            Platform = Content.Load<Model>(ContentFolder3D + "cubo/cube");



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
            dibujarAutos();
            dibujarPlataformas();
        }


        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
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

        public void inicializarArena()
        {
            PisoWorld = Matrix.CreateScale(10);
            ParedWorld = Matrix.CreateScale(25,50,25);
        }

        public void inicializarAutos()
        {
            AutoPrincipalWorld = Matrix.CreateScale(5);

            Auto1World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(0, 0, 50);
            Auto2World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(20, 0, 50);
            Auto3World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(40, 0, 50);
            Auto4World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-20, 0, 50);
            Auto5World = Matrix.CreateScale(5) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-40, 0, 50);

            AutoCombate1World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, 100);
            AutoCombate2World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(35, 0, 100);
            AutoCombate3World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(-35, 0, 100);
        }

        public void inicializarPlataformas()
        {
            Column1World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -450);
            Column2World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, -350);
            Column3World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140 , 0, -450);
            Column4World = Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(140, 0, -350);

            Ramp1World = Matrix.CreateScale(0.45f,0.35f,0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-80,0,-390);
            Ramp2World = Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(230, 0, -400);

            Platform1World = Matrix.CreateScale(100,5,80) * Matrix.CreateTranslation(70,64,-390);

            Column5World = Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 50);
            Column6World = Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 50);
            Column7World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 175);
            Column8World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 175);

            Platform2World = Matrix.CreateScale(100,5, 80) * Matrix.CreateTranslation(-250, 30, 100);

            Column9World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-170, 0, 290);
            Column10World = Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-330, 0, 290);

            Ramp3World = Matrix.CreateScale(0.25f, 0.35f, 0.3f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(-250,0,-10);
            Ramp4World = Matrix.CreateScale(0.4f, 0.15f, 0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(-300, 35, 130);

            Platform3World = Matrix.CreateScale(100, 5, 70) * Matrix.CreateTranslation(-250, 93, 235);


        }

        public void dibujarEscenario()
        {
            dibujar(PisoWorld, Piso, Color.LightGoldenrodYellow);
            dibujar(ParedWorld, Paredes, Color.Wheat);
        }
        
        public void dibujarPlataformas()
        {
            dibujar(Column1World, Column, Color.SandyBrown);
            dibujar(Column2World, Column, Color.SandyBrown);
            dibujar(Column3World, Column, Color.SandyBrown);
            dibujar(Column4World, Column, Color.SandyBrown);

            dibujar(Ramp1World,Ramp,Color.Gray);
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
        }
        public void dibujarDetalles()
        {
            

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




    }
}