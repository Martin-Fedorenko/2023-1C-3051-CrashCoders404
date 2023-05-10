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
        private Model Rock5 { get; set; }
        private Model Rock10 { get; set; }
        private Model Tire { get; set; }

        //MovimientoAuto
        private Vector3 CarPosition;
        private Vector3 CarDirection;
        private float CarSpeed;
        private float CarAcceleration;
        private float Rozamiento;
        private float Rotation;
        private bool ActiveMovement;
        private Boolean onJump;
        private Boolean onDescend;
        private Boolean accelerating;
        private float jumpRotation;
        private float jumpAcceleration;
        private float jumpAngle;
        private float jumpHeight;
        private float maxSpeed;
        private float WheelRotation;
        private ModelBone leftBackWheelBone;
        private ModelBone rightBackWheelBone;
        private ModelBone leftFrontWheelBone;
        private ModelBone rightFrontWheelBone;
        private Matrix leftBackWheelTransform = Matrix.Identity;
        private Matrix rightBackWheelTransform = Matrix.Identity;
        private Matrix leftFrontWheelTransform = Matrix.Identity;
        private Matrix rightFrontWheelTransform = Matrix.Identity;
        private Matrix[] relativeMatrices;

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
        private Vector3 posicionTarget = new Vector3(0, 0, 0);
        private Vector3 posicionCamara = new Vector3(-250, 250, -100);


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

            //Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 1500);

            // Cámara con vista isométrica
            View = Matrix.CreateLookAt(posicionCamara, CarPosition, Vector3.Up);
            Projection = Matrix.CreateOrthographic(400, 300, -80, 1000);

            //MovimientoAuto
            CarSpeed = 0f;
            CarAcceleration = 200f;
            Rozamiento = -100f;
            ActiveMovement = false;
            jumpAngle = MathF.PI / 9;
            jumpAcceleration = 5f;
            onJump = false;
            onDescend = false;
            accelerating = false;
            jumpHeight = 100f;
            maxSpeed = 1000f;

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
            Rock5 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock5");
            Rock10 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock10");
            Tire = Content.Load<Model>(ContentFolder3D + "Decoration/Tire/Tire");

            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");



            leftBackWheelBone = AutoDeportivo.Bones["WheelD"];
            rightBackWheelBone = AutoDeportivo.Bones["WheelC"];
            leftFrontWheelBone = AutoDeportivo.Bones["WheelA"];
            rightFrontWheelBone = AutoDeportivo.Bones["WheelB"];

            leftBackWheelTransform = leftBackWheelBone.Transform;
            rightBackWheelTransform = rightBackWheelBone.Transform;
            leftFrontWheelTransform = leftFrontWheelBone.Transform;
            rightFrontWheelTransform = rightFrontWheelBone.Transform;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CarDirection = AutoPrincipalWorld.Backward;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !onJump)
            {
                if (CarSpeed < maxSpeed) CarSpeed += CarAcceleration * elapsedTime;
                CarPosition += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
                ActiveMovement = true;
                accelerating = true;

                posicionCamara += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !onJump)
            {
                if (CarSpeed > -maxSpeed) CarSpeed -= CarAcceleration * elapsedTime;
                CarSpeed -= CarAcceleration * elapsedTime;
                CarPosition += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
                ActiveMovement = true;
                accelerating = true;

                posicionCamara += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
            }

            if ((keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.W)) || onJump)
            {
                accelerating = false;
                if (CarSpeed > 0)
                {
                    CarSpeed += Rozamiento * elapsedTime;
                    ActiveMovement = true;
                }
                else if (CarSpeed < 0)
                {
                    CarSpeed -= Rozamiento * elapsedTime;
                    ActiveMovement = true;
                }
                CarPosition += CarDirection * CarSpeed * elapsedTime;
                posicionCamara += CarDirection * CarSpeed * elapsedTime;
            }
            //rotar
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if(WheelRotation > -MathF.PI * 1 / 6) WheelRotation -= elapsedTime;
                if(ActiveMovement) Rotation -= elapsedTime;
         //NO DEBERIA PODER GIRAR AL ESTAR QUIETO
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (WheelRotation < MathF.PI * 1 / 6) WheelRotation += elapsedTime;
                if(ActiveMovement) Rotation += elapsedTime;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                WheelRotation = 0f;//cuando soltas W o A el auto y las ruedas siguen recto
            }
            //saltar
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || onJump) && !accelerating)
            {
                onJump = true;
                if (CarSpeed >= 0)
                {
                    if (!onDescend && jumpRotation < jumpAngle) jumpRotation += 0.05f;
                    if (onDescend && jumpRotation > -jumpAngle) jumpRotation -= 0.05f;
                }
                else if (CarSpeed < 0)
                {
                    if (!onDescend && jumpRotation > -jumpAngle) jumpRotation -= 0.05f;
                    if (onDescend && jumpRotation < jumpAngle) jumpRotation += 0.05f;
                }
                if (CarPosition.Y <= jumpHeight && !onDescend)
                {
                    CarPosition += Vector3.UnitY * jumpAcceleration;
                    posicionCamara += Vector3.UnitY * jumpAcceleration;
                }
                else if (CarPosition.Y >= jumpHeight && !onDescend) onDescend = true;
                else if (CarPosition.Y >= 5 && onDescend)
                {
                    CarPosition -= Vector3.UnitY * jumpAcceleration;
                    posicionCamara -= Vector3.UnitY * jumpAcceleration;
                }
                else if (CarPosition.Y <= jumpHeight && onDescend) //debi usar <= en lugar de ==, ya que hay veces que el auto termina parte bajo el suelo, y no permitia otros movimientos del auto
                {
                    jumpRotation = 0f;
                    onDescend = false;
                    onJump = false;
                }
            }

            View = Matrix.CreateLookAt(posicionCamara,CarPosition, Vector3.Up);

            AutoPrincipalWorld =  Matrix.CreateScale(0.1f) *
                                  Matrix.CreateRotationX(-jumpRotation) *
                                  Matrix.CreateRotationY(Rotation) *
                                  Matrix.CreateTranslation(CarPosition);
         
            // Calculate matrices based on the current animation position.
            var wheelRotation = Matrix.CreateRotationY(WheelRotation*2);

            leftFrontWheelBone.Transform = wheelRotation * leftFrontWheelTransform;
            rightFrontWheelBone.Transform = wheelRotation * rightFrontWheelTransform;

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

            relativeMatrices = new Matrix[modelo.Bones.Count];
            modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);

            foreach (var mesh in modelo.Meshes)
            {
                Effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index]*matrizMundo);
                mesh.Draw();
            }
        }
        public void dibujarArboles(Matrix matrizMundo, Model modelo, Color color)//con el otro metodo por alguna razon el modelo del arbol no se dibuja
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
            AutoPrincipalWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(0, 0, 0);

            Auto1World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(0, 0, 70);
            Auto2World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(30, 0, 70);
            Auto3World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(60, 0, 70);
            Auto4World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-30, 0, 70);
            Auto5World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(-60, 0, 70);

            AutoCombate1World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0, 0, 180);
            AutoCombate2World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(50, 0, 180);
            AutoCombate3World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(-50, 0, 180);
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

            BrokenColumn1World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationY(-MathF.PI / 6) * Matrix.CreateTranslation(450, 0, 250);
            BrokenColumn2World = Matrix.CreateScale(0.7f) * Matrix.CreateRotationY(MathF.PI / 6) * Matrix.CreateTranslation(-230, 0, -290);

            Column11World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(-500, 0, 0);
            Column12World = Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(436, 0, -80);
        }
        public void dibujarDetalles()
        {
            dibujarArboles(Tree1World, Tree, Color.Black);
            dibujarArboles(Tree2World, Tree, Color.Black);
            dibujarArboles(Tree3World, Tree, Color.Black);
            dibujarArboles(Tree4World, Tree, Color.Black);

            dibujarArboles(Tree5World, Tree, Color.Black);
            dibujarArboles(Tree6World, Tree, Color.Black);
            dibujarArboles(Tree7World, Tree, Color.Black);
            dibujarArboles(Tree8World, Tree, Color.Black);

            dibujarArboles(Tree9World, Tree, Color.Black);
            dibujarArboles(Tree10World, Tree, Color.Black);
            dibujarArboles(Tree11World, Tree, Color.Black);
            dibujarArboles(Tree12World, Tree, Color.Black);

            dibujar(Rock1World, Rock1, Color.Gray);
            dibujar(Rock2World, Rock1, Color.Gray);
            dibujar(Rock3World, Rock1, Color.Gray);
            dibujar(Rock4World, Rock1, Color.Gray);

            dibujar(Rock5World, Rock1, Color.Gray);
            dibujar(Rock6World, Rock1, Color.Gray);
            dibujar(Rock7World, Rock1, Color.Gray);
            dibujar(Rock8World, Rock1, Color.Gray);

            dibujar(Rock9World, Rock5, Color.Gray);
            dibujar(Rock10World, Rock5, Color.Gray);
            dibujar(Rock11World, Rock5, Color.Gray);
            dibujar(Rock12World, Rock5, Color.Gray);

            dibujar(Rock13World, Rock5, Color.Gray);
            dibujar(Rock14World, Rock10, Color.Gray);
            dibujar(Rock15World, Rock10, Color.Gray);
            dibujar(Rock16World, Rock5, Color.Gray);
            dibujar(Rock17World, Rock5, Color.Gray);
            dibujar(Rock18World, Rock10, Color.Gray);
            dibujar(Rock19World, Rock10, Color.Gray);
            dibujar(Rock20World, Rock5, Color.Gray);
            dibujar(Rock21World, Rock5, Color.Gray);
            dibujar(Rock22World, Rock10, Color.Gray);
            dibujar(Rock23World, Rock10, Color.Gray);
            dibujar(Rock24World, Rock5, Color.Gray);

            dibujar(Rock25World, Rock5, Color.Gray);
            dibujar(Rock26World, Rock5, Color.Gray);
            dibujar(Rock27World, Rock5, Color.Gray);
            dibujar(Rock28World, Rock5, Color.Gray);
            dibujar(Rock29World, Rock5, Color.Gray);
            dibujar(Rock30World, Rock5, Color.Gray);
            dibujar(Rock31World, Rock5, Color.Gray);
            dibujar(Rock32World, Rock5, Color.Gray);

            // "Tire" del lado más cerca del origen de la rampa Rampa1World
            dibujar(Tire1world, Tire, Color.Black);

            dibujar(Tire2world, Tire, Color.Black);
            dibujar(Tire2world1, Tire, Color.Gray);

            dibujar(Tire3world, Tire, Color.Black);
            dibujar(Tire3world1, Tire, Color.Gray);
            dibujar(Tire3world2, Tire, Color.Black);

            dibujar(Tire4world, Tire, Color.Black);
            dibujar(Tire4world1, Tire, Color.Gray);
            dibujar(Tire4world2, Tire, Color.Black);
            dibujar(Tire4world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa1World
            dibujar(Tire5world, Tire, Color.Black);

            dibujar(Tire6world, Tire, Color.Black);
            dibujar(Tire6world1, Tire, Color.Gray);

            dibujar(Tire7world, Tire, Color.Black);
            dibujar(Tire7world1, Tire, Color.Gray);
            dibujar(Tire7world2, Tire, Color.Black);

            dibujar(Tire8world, Tire, Color.Black);
            dibujar(Tire8world1, Tire, Color.Gray);
            dibujar(Tire8world2, Tire, Color.Black);
            dibujar(Tire8world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa2World
            dibujar(Tire9world, Tire, Color.Black);

            dibujar(Tire10world, Tire, Color.Black);
            dibujar(Tire10world1, Tire, Color.Gray);

            dibujar(Tire11world, Tire, Color.Black);
            dibujar(Tire11world1, Tire, Color.Gray);
            dibujar(Tire11world2, Tire, Color.Black);

            dibujar(Tire12world, Tire, Color.Black);
            dibujar(Tire12world1, Tire, Color.Gray);
            dibujar(Tire12world2, Tire, Color.Black);
            dibujar(Tire12world3, Tire, Color.Gray);

            // "Tire" del lado más cerca del origen de la rampa Rampa2World
            dibujar(Tire13world, Tire, Color.Black);

            dibujar(Tire14world, Tire, Color.Black);
            dibujar(Tire14world1, Tire, Color.Gray);

            dibujar(Tire15world, Tire, Color.Black);
            dibujar(Tire15world1, Tire, Color.Gray);
            dibujar(Tire15world2, Tire, Color.Black);

            dibujar(Tire16world, Tire, Color.Black);
            dibujar(Tire16world1, Tire, Color.Gray);
            dibujar(Tire16world2, Tire, Color.Black);
            dibujar(Tire16world3, Tire, Color.Gray);

            // "Tire" del lado más lejos del origen de la rampa Rampa3World
            dibujar(Tire17world, Tire, Color.Black);

            dibujar(Tire18world, Tire, Color.Black);
            dibujar(Tire18world1, Tire, Color.Gray);

            dibujar(Tire19world, Tire, Color.Black);
            dibujar(Tire19world1, Tire, Color.Gray);
            dibujar(Tire19world2, Tire, Color.Black);

            // "Tire" del lado más cerca del origen de la rampa Rampa3World
            dibujar(Tire20world, Tire, Color.Black);

            dibujar(Tire21world, Tire, Color.Black);
            dibujar(Tire21world1, Tire, Color.Gray);

            dibujar(Tire22world, Tire, Color.Black);
            dibujar(Tire22world1, Tire, Color.Gray);
            dibujar(Tire22world2, Tire, Color.Black);

            dibujar(BrokenColumn1World, Column, Color.SandyBrown);
            dibujar(BrokenColumn2World, Column, Color.SandyBrown);

            dibujar(Column11World, Column, Color.SandyBrown);
            dibujar(Column12World, Column, Color.SandyBrown);
        }
    }
}