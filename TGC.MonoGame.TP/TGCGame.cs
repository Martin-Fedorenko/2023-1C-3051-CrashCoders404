﻿﻿using System;
using System.Collections.Generic;
using BepuPhysics.CollisionDetection.CollisionTasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;

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
        private Effect TextureShader { get; set; }

        private Autos autos;
        private Detalles detalles;
        private Escenario escenario;

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
        private Matrix[] relativeMatrices;

        // Matrices de Mundo
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }

        //Camara
        private Vector3 posicionTarget = new Vector3(0, 0, 0);
        private Vector3 posicionCamara = new Vector3(-250, 250, -100);

        //Colisiones
        private BoundingBox AutoPrincipalBox;
        private BoundingBox Auto1Box;
        private Boolean collided;


        protected override void Initialize()
        {
            // Dimensiones de la pantalla
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.ApplyChanges();

            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            autos = new Autos();
            detalles = new Detalles();
            escenario = new Escenario();

            escenario.Initialize();
            autos.Initialize();
            detalles.Initialize();

            //Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 1500);

            // Cámara con vista isométrica
            View = Matrix.CreateLookAt(posicionCamara, autos.posAutoPrincipal(), Vector3.Up);
            Projection = Matrix.CreateOrthographic(400, 300, -80, 1000);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Piso = Content.Load<Model>(ContentFolder3D + "Arena/Plano"); //No tiene textura incluida
            Pared = Content.Load<Model>(ContentFolder3D + "Arena/Arena"); //No tiene textura incluida
            Column = Content.Load<Model>(ContentFolder3D + "Platforms/Column/Column"); //Tiene textura incluida pero HAY QUE ARREGLAR O CAMBIAR EL MODELO
            Ramp = Content.Load<Model>(ContentFolder3D + "Platforms/Ramps/Ramp"); //No tiene textura incluida
            Platform = Content.Load<Model>(ContentFolder3D + "Platforms/Cubo/Cube"); //No tiene textura incluida
            escenario.LoadContent(Piso,Pared,Column,Ramp,Platform,Cube);

            AutoDeportivo = Content.Load<Model>(ContentFolder3D + "Derby/RacingCar/RacingCar"); //Tiene textura incluida
            AutoDeCombate = Content.Load<Model>(ContentFolder3D + "Derby/CombatVehicle/Vehicle"); //Tiene textura incluida


            Tree = Content.Load<Model>(ContentFolder3D + "Decoration/ArbolSinHojas/TreeWinter"); //No tiene textura incluida
            Rock1 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock1"); //No tiene textura incluida
            Rock5 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock5"); //No tiene textura incluida
            Rock10 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock10"); //No tiene textura incluida
            Tire = Content.Load<Model>(ContentFolder3D + "Decoration/Tire/Tire"); //No tiene textura incluida
            detalles.LoadContent(Tree,Rock1,Rock5,Rock10,Tire);

            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
            TextureShader = Content.Load<Effect>(ContentFolderEffects + "TextureShader");
            autos.LoadContent(AutoDeportivo,AutoDeCombate,TextureShader);

            AutoPrincipalBox = BoundingVolumesExtensions.CreateAABBFrom(AutoDeportivo);
            Auto1Box = BoundingVolumesExtensions.CreateAABBFrom(AutoDeportivo);
            AutoPrincipalBox = BoundingVolumesExtensions.Scale(AutoPrincipalBox,0.1f);
            Auto1Box = BoundingVolumesExtensions.Scale(Auto1Box,0.1f);
            AutoPrincipalBox = new BoundingBox(AutoPrincipalBox.Min + autos.posAutoPrincipal(), AutoPrincipalBox.Max + autos.posAutoPrincipal());
            Auto1Box = new BoundingBox(Auto1Box.Min + autos.posAuto1(), Auto1Box.Max + autos.posAuto1());


            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            collided = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            autos.Update(gameTime);
            View = Matrix.CreateLookAt(posicionCamara + autos.posAutoPrincipal(),autos.posAutoPrincipal(), Vector3.Up);

            AutoPrincipalBox = new BoundingBox(AutoPrincipalBox.Min + autos.increment() , AutoPrincipalBox.Max + autos.increment());
            
            collided = AutoPrincipalBox.Intersects(Auto1Box);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            escenario.dibujarEscenario(View,Projection,Effect);
            autos.dibujarAutos(View,Projection,TextureShader,collided);
            detalles.dibujarDetalles(View,Projection,Effect);
        }
        protected override void UnloadContent()
        {
            Content.Unload();
            base.UnloadContent();
        }
    }
}