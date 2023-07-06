﻿using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using BepuPhysics.CollisionDetection.CollisionTasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using TGC.MonoGame.TP.Viewer.Gizmos;
using TGC.MonoGame.TP.Cameras;
using TGC.MonoGame.TP.Geometries;
using System.Threading;

namespace TGC.MonoGame.TP //porq no puedo usar follow camera?
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
    private Camera Camera { get; set; }
    private Gizmos gizmos;
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; set; }
    private Effect Effect { get; set; }
    private Effect EscenarioShader { get; set; }
    private Effect DetallesShader { get; set; }
    private Effect AutoShader { get; set; }
    private Effect BlurShader { get; set; }
    private Effect IluminacionShader { get; set; }
    

    private Autos autos;
    private Detalles detalles;
    private Escenario escenario;
    private PowerUps powerUps;
    //private SistemaDeVida vida;

    // Modelos
    //private Model Piso { get; set; }
    private QuadPrimitive Piso { get; set; }
    private QuadPrimitive Pared { get; set; }
    private Model Column { get; set; }
    private Model Ramp { get; set; }
    private Model Platform { get; set; }
    private Model AutoDeportivo { get; set; }
    private Model AutoDeCombate { get; set; }
    private Model Tree { get; set; }
    private Model Rock1 { get; set; }
    private Model Rock5 { get; set; }
    private Model Rock10 { get; set; }
    private Model Tire { get; set; }
    private Model CajaTurbo { get; set; }
    private Model CajaAmetralladora { get; set; }
    private Model CajaMisil { get; set; }
    private Model Misil { get; set; }
    private Model Bala { get; set; }

    // Matrices de Mundo
    private Matrix View { get; set; }
    private Matrix Projection { get; set; }

    // Bones
    private Matrix[] relativeMatrices;

    // Cámara
    private Vector3 posicionTarget = new Vector3(0, 0, 0);
    private Vector3 posicionCamara = new Vector3(-250, 250, -100);

    //Colisiones
    private Boolean collided = false;

    //Pantalla de carga
    public const int ST_PRESENTACION = 0;
    public const int ST_JUEGO = 1;
    public const int ST_CONTROLES = 2;
    public const int ST_COUNTDOWN_3 = 6;
    public const int ST_COUNTDOWN_2 = 7;
    public const int ST_COUNTDOWN_1 = 8;
    public const int ST_COUNTDOWN_GO = 9;
    public const int ST_ENDGAME = -1;
    public const int ST_VICTORIA = 10;
    public const int ST_DERROTA = 11;
    public SpriteFont font;
    public int status = ST_PRESENTACION;


    //Musica
    private Song SongGame { get; set; }
    private Song SongMenu { get; set; }
    private Song SongCountdown { get; set; }
    private Song Winner { get; set; }
    private Song GameOver { get; set; }
  
    //Sonidos
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect BulletSound { get; set; }
    private SoundEffect PickUpGunSound { get; set; }
    private SoundEffect RocketSound { get; set; }
    private SoundEffect PickUpRocketSound { get; set; }
    private SoundEffect BoostSound { get; set; }
    private SoundEffect CarCrash { get; set; }
    private SoundEffect VidaPerdida { get; set; }
    private SoundEffect ExplosionMisil { get; set; }


    //Texturas
    private Texture2D TexturaPiso;
    private Texture2D TexturaRampa;
    private Texture2D TexturaPared;
    private Texture2D TexturaPlataforma;
    private Texture2D TexturaColumna;
    private Texture2D TexturaRoca;
    private Texture2D TexturaArbol;
    private Texture2D TexturaTire1;
    private Texture2D TexturaTire2;
    private Texture2D TexturaMenu;
    private Texture2D TexturaPowerUp;
    private Texture2D Noise;
    private Texture2D TexturaAuxiliar;

    //efectos
    private RenderTargetCube EnvironmentMapRenderTarget { get; set; }

    private RenderTarget2D FirstPassBloomRenderTarget;
    private RenderTarget2D SecondPassBloomRenderTarget;
    private RenderTarget2D MainSceneRenderTarget;
    private FullScreenQuad FullScreenQuad;


    //HUD
    private float totalGameTime;
    private float countdownStart;
    private Vector2 autoPos;
    private bool dibujarGizmos = false;
    private Vector2 tamanioPantalla;
    private Texture2D Corazon1;
    private Texture2D Corazon2;
    private Texture2D Corazon3;
    private Texture2D Corazon4;

    //Variables
    private  Vector3 lightPosition = new Vector3(0.0f,100.0f,0.0f);
    private float timerMenu = 0f;

    protected override void Initialize()
    {
      gizmos = new Gizmos();

      SpriteBatch = new SpriteBatch(GraphicsDevice);
      

      // Dimensiones de la pantalla
      Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
      Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
      Graphics.ApplyChanges();

      var rasterizerState = new RasterizerState();
      rasterizerState.CullMode = CullMode.None;
      GraphicsDevice.RasterizerState = rasterizerState;

      detalles = new Detalles();
      escenario = new Escenario();
      powerUps = new PowerUps();
      autos = new Autos();


      escenario.Initialize();
      detalles.Initialize();
      powerUps.Initialize(GraphicsDevice);
      autos.Initialize();


      //CAMARA
      //Camera = new SimpleCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(40, 60, 150), 55, 0.4f);
      // Cámara con vista isométrica
      View = Matrix.CreateLookAt(posicionCamara, autos.posAutoPrincipal(), Vector3.Up);
      Projection = Matrix.CreateOrthographic(400, 300, -80, 1000);
      
      base.Initialize();
    }

    protected override void LoadContent()
    {

      // Create a render target for the scene
      EnvironmentMapRenderTarget = new RenderTargetCube(GraphicsDevice, 2048, false,
      SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);
      GraphicsDevice.BlendState = BlendState.Opaque;

      FullScreenQuad = new FullScreenQuad(GraphicsDevice);

      MainSceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
        GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0,
        RenderTargetUsage.DiscardContents);
      FirstPassBloomRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
        GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0,
        RenderTargetUsage.DiscardContents);
      SecondPassBloomRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
        GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None, 0,
        RenderTargetUsage.DiscardContents);

      gizmos.LoadContent(GraphicsDevice, Content);
      SpriteBatch = new SpriteBatch(GraphicsDevice);
      Piso = new QuadPrimitive(GraphicsDevice);
      Pared = new QuadPrimitive(GraphicsDevice);

      font = Content.Load<SpriteFont>(ContentFolderSpriteFonts + "Mogathe/MogatheSpurs");

      //Escenario
      //Piso = Content.Load<Model>(ContentFolder3D + "Arena/Plano"); //No tiene textura incluida
      //Pared = Content.Load<Model>(ContentFolder3D + "Arena/Arena"); //No tiene textura incluida
      Column = Content.Load<Model>(ContentFolder3D + "Platforms/Column/Column"); //Tiene textura incluida pero HAY QUE ARREGLAR O CAMBIAR EL MODELO
      Ramp = Content.Load<Model>(ContentFolder3D + "Platforms/Ramps/Ramp"); //No tiene textura incluida
      Platform = Content.Load<Model>(ContentFolder3D + "Platforms/Cubo/Cube"); //No tiene textura incluida

      //Autos
      AutoDeportivo = Content.Load<Model>(ContentFolder3D + "Derby/RacingCar/RacingCar"); //Tiene textura incluida
      AutoDeCombate = Content.Load<Model>(ContentFolder3D + "Derby/CombatVehicle/Vehicle"); //Tiene textura incluida

      //Detalles
      Tree = Content.Load<Model>(ContentFolder3D + "Decoration/ArbolSinHojas/TreeWinter"); //No tiene textura incluida
      Rock1 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock1"); //No tiene textura incluida
      Rock5 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock5"); //No tiene textura incluida
      Rock10 = Content.Load<Model>(ContentFolder3D + "Decoration/Rocks/Rock10"); //No tiene textura incluida
      Tire = Content.Load<Model>(ContentFolder3D + "Decoration/Tire/Tire"); //No tiene textura incluida

      //PowerUps
      CajaAmetralladora = Content.Load<Model>(ContentFolder3D + "PowerUps/Ametralladora/Ametralladora");
      CajaMisil = Content.Load<Model>(ContentFolder3D + "PowerUps/Misil/Misil");
      CajaTurbo = Content.Load<Model>(ContentFolder3D + "PowerUps/PowerUpRayo/PowerUpRayo");
      Misil = Content.Load<Model>(ContentFolder3D + "PowerUps/Misil/misilModel");
      Bala = Content.Load<Model>(ContentFolder3D + "PowerUps/Ametralladora/balaModel");

      //Efectos
      Noise = Content.Load<Texture2D>(ContentFolderTextures + "perlin");
      TexturaAuxiliar = Content.Load<Texture2D>(ContentFolderTextures + "lava");
      Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader"); //aca
      EscenarioShader = Content.Load<Effect>(ContentFolderEffects + "EscenarioShader");
      DetallesShader = Content.Load<Effect>(ContentFolderEffects + "DetallesShader");
      AutoShader = Content.Load<Effect>(ContentFolderEffects + "AutoShader");
      BlurShader = Content.Load<Effect>(ContentFolderEffects + "GaussianBlur");

      BlurShader.Parameters["screenSize"]?.SetValue(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

      AutoShader.Parameters["environmentMap"]?.SetValue(EnvironmentMapRenderTarget);

      AutoShader.Parameters["TexturaRuido"]?.SetValue(Noise);
      AutoShader.Parameters["TexturaAuxiliar"]?.SetValue(TexturaAuxiliar);

      //Iluminacion
      AutoShader.Parameters["ambientColor"]?.SetValue(Color.White.ToVector3());
      AutoShader.Parameters["diffuseColor"]?.SetValue(Color.White.ToVector3());
      AutoShader.Parameters["specularColor"]?.SetValue(Color.White.ToVector3());

      AutoShader.Parameters["KAmbient"]?.SetValue(0.1f);
      AutoShader.Parameters["KDiffuse"]?.SetValue(0.5f);
      AutoShader.Parameters["KSpecular"]?.SetValue(0.5f);
      AutoShader.Parameters["shininess"]?.SetValue(2f);

      //Música y sonido
      BulletSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-ametralladora");
      PickUpGunSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-ametralladora");
      RocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-rocket");
      PickUpRocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-rocket");
      BoostSound = Content.Load<SoundEffect>(ContentFolderSounds + "boost-effect");
      CarCrash = Content.Load<SoundEffect>(ContentFolderSounds + "car-crash");
      VidaPerdida = Content.Load<SoundEffect>(ContentFolderSounds + "damage-received");
      ExplosionMisil = Content.Load<SoundEffect>(ContentFolderSounds + "explosion-misil");
      CarCrash = Content.Load<SoundEffect>(ContentFolderSounds + "car-crash");

      //Textura
      TexturaPiso = Content.Load<Texture2D>(ContentFolderTextures + "sand");
      TexturaPared = Content.Load<Texture2D>(ContentFolderTextures + "color");
      TexturaColumna = Content.Load<Texture2D>(ContentFolderTextures + "displacement");
      TexturaRampa = Content.Load<Texture2D>(ContentFolderTextures + "color");
      TexturaPlataforma = Content.Load<Texture2D>(ContentFolderTextures + "colorPlatform");
      TexturaRoca = Content.Load<Texture2D>(ContentFolderTextures + "rock");
      TexturaArbol = Content.Load<Texture2D>(ContentFolderTextures + "tree");
      TexturaTire1 = Content.Load<Texture2D>(ContentFolderTextures + "tire1");
      TexturaTire2 = Content.Load<Texture2D>(ContentFolderTextures + "tire2");
      TexturaMenu = Content.Load<Texture2D>(ContentFolderTextures + "backMenu");
      TexturaPowerUp = Content.Load<Texture2D>(ContentFolderTextures + "gold");
      Corazon1 = Content.Load<Texture2D>(ContentFolderTextures + "Vida/1 Corazon");
      Corazon2 = Content.Load<Texture2D>(ContentFolderTextures + "Vida/1 Corazon 2");
      Corazon3 = Content.Load<Texture2D>(ContentFolderTextures + "Vida/1 Corazon 3");
      Corazon4 = Content.Load<Texture2D>(ContentFolderTextures + "Vida/1 Corazon 4");
      

      escenario.LoadContent(Piso, Pared, Column, Ramp, Platform, TexturaPiso, TexturaPared, TexturaColumna, TexturaRampa, TexturaPlataforma);
      detalles.LoadContent(Tree, Rock1, Rock5, Rock10, Tire, TexturaRoca, TexturaRoca, TexturaTire1, TexturaTire2);
      powerUps.LoadContent(CajaAmetralladora, CajaMisil, CajaTurbo, Misil, Bala, BulletSound, PickUpGunSound, RocketSound, PickUpRocketSound,
                           BoostSound, TexturaPowerUp,ExplosionMisil);
      autos.LoadContent(AutoDeportivo, AutoDeCombate, AutoShader,CarCrash);


      //Musica
      SongGame = Content.Load<Song>(ContentFolderMusic + "trap-movement");
      SongMenu = Content.Load<Song>(ContentFolderMusic + "music-menu");
      SongCountdown = Content.Load<Song>(ContentFolderMusic + "countdown-start");
      Winner = Content.Load<Song>(ContentFolderMusic + "winner");
      GameOver = Content.Load<Song>(ContentFolderMusic + "gameOver");

      base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        status = ST_ENDGAME;
      }
      switch (status)
      {
        case ST_PRESENTACION:
        timerMenu = (float)gameTime.TotalGameTime.TotalSeconds;

          if(!(MediaPlayer.State == MediaState.Playing))
          {
            MediaPlayer.Play(SongMenu);
          }

          if (Keyboard.GetState().IsKeyDown(Keys.Enter))
          {
            countdownStart = 0;
            status = ST_COUNTDOWN_3;
            MediaPlayer.Stop();
            MediaPlayer.Play(SongCountdown);
            countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
          }
          if (Keyboard.GetState().IsKeyDown(Keys.G))
          {
            countdownStart = 0;
            status = ST_COUNTDOWN_3;
            MediaPlayer.Stop();
            MediaPlayer.Play(SongCountdown);
            countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
            autos.vidaProtagonista = 10000;
          }
          if (Keyboard.GetState().IsKeyDown(Keys.C))
          {
            status = ST_CONTROLES;
          }
          break;

        case ST_CONTROLES:
          if (Keyboard.GetState().IsKeyDown(Keys.B))
          {
            status = ST_PRESENTACION;
          }
          break;

        case ST_COUNTDOWN_3:
        countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (countdownStart > 1)
        {
          status = ST_COUNTDOWN_2;
          break;
        }
        break;

        case ST_COUNTDOWN_2:
        countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(countdownStart > 2)
        {
          status = ST_COUNTDOWN_1;
          break;
        }
        break;

        case ST_COUNTDOWN_1:
        countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(countdownStart > 3)
        {
          status = ST_COUNTDOWN_GO;
          break;
        }
        break;

        case ST_COUNTDOWN_GO:
        countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(countdownStart > 4)
        {
          status = ST_JUEGO;
          break;
        }
        break;

        case ST_JUEGO:
          if(!(MediaPlayer.State == MediaState.Playing))
          {
            MediaPlayer.Play(SongGame);
          }
          var Time = (float)gameTime.TotalGameTime.TotalSeconds;
          totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
          autos.Update(gameTime,powerUps,escenario,detalles);
          powerUps.Update(gameTime, autos, detalles, escenario);

          detalles.Update(gameTime,autos);
          escenario.Update(gameTime, autos);

          //vida.Update(autos, powerUps);

          View = Matrix.CreateLookAt(posicionCamara + autos.posAutoPrincipal(), autos.posAutoPrincipal(), Vector3.Up);
          //Camera.Update(gameTime);
          gizmos.UpdateViewProjection(View, Projection);


          //SOL "FIJO"
          //var lightPosition = new Vector3(autos.posAutoPrincipal().X, autos.posAutoPrincipal().Y+20f, autos.posAutoPrincipal().Z);

            // Set the light position and camera position
            // These change every update so we need to set them on every update call
            AutoShader.Parameters["farosPosition"]?.SetValue(autos.posAutoPrincipal() + autos.CarDirection * 250f ); //+ Vector3.UnitY * 50f
            AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);
            AutoShader.Parameters["eyePosition"]?.SetValue(posicionCamara+autos.posAutoPrincipal());
            AutoShader.Parameters["carDirection"]?.SetValue(autos.directionAutoPrincipal());
            AutoShader.Parameters["Time"]?.SetValue(Time);
            //Console.WriteLine(autos.directionAutoPrincipal());

            if(autos.muereProta())
            {
              MediaPlayer.Stop();
              MediaPlayer.Play(GameOver);
              //autos.vidaProtagonista = 100;
              status = ST_DERROTA;
              break;
            }
          break;
          case ST_DERROTA:
          if(!(MediaPlayer.State == MediaState.Playing))
          {
            MediaPlayer.Play(GameOver);
          }

          if (Keyboard.GetState().IsKeyDown(Keys.Enter))
          {
            
            //this.UnloadContent();
            Thread.Sleep(500);
            autos.iniciarPartida();
            powerUps.iniciarPartida();
            SpriteBatch.Begin();
            dibujarCorazones(tamanioPantalla);
            SpriteBatch.End();
            View = Matrix.CreateLookAt(posicionCamara, autos.posAutoPrincipal(), Vector3.Up);
            Projection = Matrix.CreateOrthographic(400, 300, -80, 1000);
            totalGameTime = 0;
            status = ST_PRESENTACION;
            
            MediaPlayer.Stop();
            MediaPlayer.Play(SongMenu);
            break;
          }
          break;

        case ST_ENDGAME:
          Exit();
          break;
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.LightYellow);
      GraphicsDevice.DepthStencilState = DepthStencilState.Default; //sin esto los autos se ven translucidos y el piso tambien
      Vector2 tamanioPantalla = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

      switch (status)
      {
        case ST_COUNTDOWN_3:          
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("3", tamanioPantalla.Y * 0.3f, 10);
          break;

        case ST_COUNTDOWN_2:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("2", tamanioPantalla.Y * 0.3f, 10);
          break;

        case ST_COUNTDOWN_1:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("1", tamanioPantalla.Y * 0.3f, 10);
          break;

        case ST_COUNTDOWN_GO:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("GO!", tamanioPantalla.Y * 0.3f, 10);
          break;

        case ST_PRESENTACION: 
          autos.dibujarAutosMenu(View,Projection,AutoShader,timerMenu);
          escenario.dibujarPiso(View,Projection,AutoShader);
          DrawCenterTextY("CRASH CODERS 404 ", tamanioPantalla.X * 0f, 4);
          DrawCenterTextY("C -> CONTROLES", tamanioPantalla.X * 0.2f, 2);
          DrawCenterTextY("G -> GOD MODE", tamanioPantalla.X * 0.3f, 2);
          DrawCenterTextY("ENTER -> COMENZAR", tamanioPantalla.X * 0.4f, 2);
          DrawRightText("ESC -> SALIR", tamanioPantalla.X * 0.6f, 1);
          break;

        case ST_CONTROLES:
          DrawCenterTextY("CONTROLES", tamanioPantalla.Y * 0f, 4);
          DrawCenterTextY("W -> AVANZAR", tamanioPantalla.Y * 0.2f, 3);
          DrawCenterTextY("S -> RETROCEDER", tamanioPantalla.Y * 0.3f, 3);
          DrawCenterTextY("D -> ROTAR DERECHA", tamanioPantalla.Y * 0.4f, 3);
          DrawCenterTextY("A -> ROTAR IZQUIERDA", tamanioPantalla.Y * 0.5f, 3);
          DrawCenterTextY("SPACE -> SALTAR", tamanioPantalla.Y * 0.65f, 3);
          DrawRightText("B -> VOLVER AL MENU", tamanioPantalla.Y *0.95f, 1);
          break;

        case ST_DERROTA:          
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("GAME OVER", tamanioPantalla.Y *0.5f, 10);
          DrawRightText("Has Sobrevivido " + ((int)totalGameTime).ToString() + " Segundos",tamanioPantalla.Y *0.95f, 1);
          break;

        case ST_JUEGO:
          SpriteBatch.Begin();
          SpriteBatch.DrawString(font, "Tiempo:" + ((int)totalGameTime).ToString(), new Vector2(tamanioPantalla.X * 0f, tamanioPantalla.Y * 0f), Color.WhiteSmoke);
          SpriteBatch.DrawString(font, "Bajas:" + (autos.getAutoBajas().ToString()), new Vector2(tamanioPantalla.X * 0f, tamanioPantalla.Y * 0.1f), Color.WhiteSmoke);
          SpriteBatch.DrawString(font, "Velocidad:" + (autos.autoSpeed().ToString()), new Vector2(tamanioPantalla.X * 0.5f, tamanioPantalla.Y * 0f), Color.WhiteSmoke);
          SpriteBatch.DrawString(font, "PowerUp:" + (powerUps.powerUpActual()), new Vector2(tamanioPantalla.X * 0f, tamanioPantalla.Y * 0.9f), Color.WhiteSmoke);
          dibujarCorazones(tamanioPantalla);
          
          #region Pass 1-6

          for (var face = CubeMapFace.PositiveX; face <= CubeMapFace.NegativeZ; face++)
          {
               
                GraphicsDevice.SetRenderTarget(EnvironmentMapRenderTarget, face);
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
                
                escenario.dibujarEscenario(View, Projection, AutoShader, true, "Luz");
                detalles.dibujarDetalles(View, Projection, AutoShader, "Luz");
                powerUps.dibujarPowerUps(View, Projection, AutoShader,"Luz");
                autos.dibujarAutos(View, Projection, AutoShader, "Luz"); 
          }
          #endregion

          #region Pass 7


            GraphicsDevice.SetRenderTarget(MainSceneRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            escenario.dibujarEscenario(View, Projection, AutoShader, true, "Luz");
            detalles.dibujarDetalles(View, Projection, AutoShader, "Luz");
            powerUps.dibujarPowerUps(View, Projection, AutoShader, "Luz");
            
            autos.dibujarAutos(View, Projection, AutoShader, "Reflejo");

          #endregion

          #region Pass 8
            GraphicsDevice.SetRenderTarget(FirstPassBloomRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            autos.dibujarAutos(View, Projection, AutoShader, "Bloom");
            escenario.dibujarEscenario(View, Projection, AutoShader, true, "BloomNegro");
            detalles.dibujarDetalles(View, Projection, AutoShader, "BloomNegro");
            powerUps.dibujarPowerUps(View, Projection, AutoShader, "BloomPowerUp");
            
          #endregion

          #region Pass 9

            var bloomTexture = FirstPassBloomRenderTarget;
            var finalBloomRenderTarget = SecondPassBloomRenderTarget;
            var PassCount = 2;
            for (var index = 0; index < PassCount; index++)
            {
                GraphicsDevice.SetRenderTarget(finalBloomRenderTarget);
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

                BlurShader.Parameters["baseTexture"].SetValue(bloomTexture);
                FullScreenQuad.Draw(BlurShader, "Blur");

                if (index != PassCount - 1)
                {
                    var auxiliar = bloomTexture;
                    bloomTexture = finalBloomRenderTarget;
                    finalBloomRenderTarget = auxiliar;
                }
            }

            #endregion


          #region Pass 10
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            
            AutoShader.Parameters["baseTexture"]?.SetValue(MainSceneRenderTarget);
            AutoShader.Parameters["bloomTexture"]?.SetValue(finalBloomRenderTarget);

            FullScreenQuad.Draw(AutoShader, "Integrar");
          #endregion

          /*escenario.dibujarEscenario(View, Projection, AutoShader);
          detalles.dibujarDetalles(View, Projection, AutoShader);
          powerUps.dibujarPowerUps(View, Projection, AutoShader);
          autos.dibujarAutos(View, Projection, AutoShader);*/

          autos.dibujarBoundingBoxes(gizmos); //OBB de autos deportivos bien ubicadas
          detalles.dibujarBoundingBoxes(gizmos); //BB de arboles bien ubicadas
          escenario.dibujarBoundingBoxes(gizmos); //BB de plataformas bien ubicadas
          powerUps.dibujarBoundingBoxes(gizmos); //BB Bien ubicadas

          if(dibujarGizmos)
          {
            gizmos.Draw();
          }
          SpriteBatch.End(); //si lo ponemos antes de dibujar los modelos, los autos y el piso se dibujan translucidos 
          break;
      }
    }
    protected override void UnloadContent()
    {
      gizmos.Dispose();
      Content.Unload();
      base.UnloadContent();
    }

    public void DrawCenterText(string msg, float escala)
    {
      var W = GraphicsDevice.Viewport.Width;
      var H = GraphicsDevice.Viewport.Height;
      var size = font.MeasureString(msg) * escala;
      SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
      Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, (H - size.Y) / 2, 0));
      SpriteBatch.DrawString(font, msg, new Vector2(0, 0), Color.YellowGreen);
      SpriteBatch.End();
    }

    public void DrawCenterTextY(string msg, float Y, float escala)
    {
      var W = GraphicsDevice.Viewport.Width;
      var H = GraphicsDevice.Viewport.Height;
      var size = font.MeasureString(msg) * escala;
      SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
      Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, Y, 0));
      SpriteBatch.DrawString(font, msg, new Vector2(0, 0), Color.YellowGreen);
      SpriteBatch.End();
    }

    public void DrawRightText(string msg, float Y, float escala)
    {
      var W = GraphicsDevice.Viewport.Width;
      var H = GraphicsDevice.Viewport.Height;
      var size = font.MeasureString(msg) * escala;
      SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
          Matrix.CreateScale(escala) * Matrix.CreateTranslation(W - size.X - 20, Y, 0));
      SpriteBatch.DrawString(font, msg, new Vector2(0, 0), Color.YellowGreen);
      SpriteBatch.End();
    }

    public void dibujarCorazones(Vector2 tamanioPantalla)
    {
      
      //CORREGIR DISPOSE DEJA CUADRADO NEGRO
      if(autos.vidaProtagonista >= 100){
        
        SpriteBatch.Draw(Corazon4, new Vector2(tamanioPantalla.X * 0.90f, tamanioPantalla.Y * 0.9f), Color.WhiteSmoke);
      }

      if(autos.vidaProtagonista >= 75)
      {
        //Corazon4.Dispose();
        
        SpriteBatch.Draw(Corazon3, new Vector2(tamanioPantalla.X * 0.85f, tamanioPantalla.Y * 0.9f), Color.WhiteSmoke);
      }
      if(autos.vidaProtagonista >= 50)
      {
        //Corazon3.Dispose();
       
        SpriteBatch.Draw(Corazon2, new Vector2(tamanioPantalla.X * 0.80f, tamanioPantalla.Y * 0.9f), Color.WhiteSmoke);
      }
      if(autos.vidaProtagonista >= 25)
      {
        //Corazon2.Dispose();
        SpriteBatch.Draw(Corazon1, new Vector2(tamanioPantalla.X * 0.75f, tamanioPantalla.Y * 0.9f), Color.WhiteSmoke);
      }

      }


  }
}
