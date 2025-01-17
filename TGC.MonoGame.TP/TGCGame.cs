﻿﻿﻿﻿using System;
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
    private Effect SandShader { get; set; }
    private Effect BlurShader { get; set; }
    

    private Autos autos;
    private Detalles detalles;
    private Escenario escenario;
    private PowerUps powerUps;

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


    // Cámara
    private Vector3 posicionCamara = new Vector3(-250, 250, -100);


    //Pantalla de carga
    public const int ST_PRESENTACION = 0;
    public const int ST_JUEGO = 1;
    public const int ST_CONTROLES = 2;
    public const int ST_COUNTDOWN_3 = 6;
    public const int ST_COUNTDOWN_2 = 7;
    public const int ST_COUNTDOWN_1 = 8;
    public const int ST_COUNTDOWN_GO = 9;
    public const int ST_ENDGAME = -1;
    public const int ST_DERROTA = 11;
    public const int ST_VICTORIA = 12;
    public const int ST_OBJETIVOS = 13;
    public SpriteFont font;    
    public SpriteFont font2;
    public int status = ST_PRESENTACION;


    //Musica
    private Song SongGame { get; set; }
    private Song SongMenu { get; set; }
    private Song SongCountdown { get; set; }
    private Song Winner { get; set; }
    private Song GameOver { get; set; }
  
    //Sonidos
    private SoundEffect BulletSound { get; set; }
    private SoundEffect PickUpGunSound { get; set; }
    private SoundEffect RocketSound { get; set; }
    private SoundEffect PickUpRocketSound { get; set; }
    private SoundEffect BoostSound { get; set; }
    private SoundEffect CarCrash { get; set; }
    private SoundEffect VidaPerdida { get; set; }
    private SoundEffect ExplosionMisil { get; set; }
    private SoundEffect KillEffect { get; set; }


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
    private Texture2D NoiseSand;
    private Texture2D TexturaAuxiliar;
    private Texture2D BoostLogo;
    private Texture2D MiniGunLogo;
    private Texture2D RocketLaucherLogo;
    private Texture2D ClockLogo;
    private Texture2D KillsLogo;
    private Texture2D ObjClockLogo;
    private Texture2D ObjKillsLogo;
    private Texture2D NoPowerUp;

    //efectos
    private RenderTargetCube EnvironmentMapRenderTarget { get; set; }

    private RenderTarget2D FirstPassBloomRenderTarget;
    private RenderTarget2D SecondPassBloomRenderTarget;
    private RenderTarget2D MainSceneRenderTarget;
    private RenderTarget2D PresentacionRenderTarget;
    private FullScreenQuad FullScreenQuad;


    //HUD
    private float totalGameTime;
    private float countdownStart;
    private bool dibujarGizmos = false;
    private Vector2 tamanioPantalla;
    private Texture2D Corazon;
    private Texture2D Logo;
    private Texture2D LogoGameOver;
    private Texture2D LogoVictory;
    private Texture2D LogoGo;
    private Texture2D particula;

    //Variables
    private  Vector3 lightPosition = new Vector3(0.0f,100.0f,0.0f);
    private float timerMenu = 0f;

    //Optimizaciones
    BoundingFrustum boundingFrustum;

    //
    private List<ParticulasExplosion> particulas;

    protected override void Initialize()
    {
      gizmos = new Gizmos();

      SpriteBatch = new SpriteBatch(GraphicsDevice);
      
      particulas = new List<ParticulasExplosion>();
      

      // Dimensiones de la pantalla
      Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
      Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
      Graphics.IsFullScreen = false;
      Graphics.HardwareModeSwitch = true;
      Graphics.ApplyChanges();

      var rasterizerState = new RasterizerState();
      rasterizerState.CullMode = CullMode.CullClockwiseFace;
      GraphicsDevice.RasterizerState = rasterizerState;

      detalles = new Detalles();
      escenario = new Escenario();
      powerUps = new PowerUps();
      autos = new Autos();


      escenario.Initialize();
      detalles.Initialize();
      powerUps.Initialize();
      autos.Initialize();


      //CAMARA
      //Camera = new SimpleCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(40, 60, 150), 55, 0.4f);
      // Cámara con vista isométrica
      View = Matrix.CreateLookAt(posicionCamara, autos.posAutoPrincipal(), Vector3.Up);
      Projection = Matrix.CreateOrthographic(400, 300, -80, 1000);

      boundingFrustum = new BoundingFrustum(View * Projection);
      
      base.Initialize();
    }

    protected override void LoadContent()
    {

      // Create a render target for the scene
      EnvironmentMapRenderTarget = new RenderTargetCube(GraphicsDevice, 256, false,
      SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);
      GraphicsDevice.BlendState = BlendState.Opaque;

      FullScreenQuad = new FullScreenQuad(GraphicsDevice);

      MainSceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
        GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0,
        RenderTargetUsage.DiscardContents);
      PresentacionRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
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
      font2 = Content.Load<SpriteFont>(ContentFolderSpriteFonts + "Mogathe/MogatheSpurs2");

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

      //Música y sonido
      BulletSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-ametralladora");
      PickUpGunSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-ametralladora");
      RocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-rocket");
      PickUpRocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-rocket");
      BoostSound = Content.Load<SoundEffect>(ContentFolderSounds + "boost-effect");
      CarCrash = Content.Load<SoundEffect>(ContentFolderSounds + "car-crash");
      VidaPerdida = Content.Load<SoundEffect>(ContentFolderSounds + "damage-received");
      ExplosionMisil = Content.Load<SoundEffect>(ContentFolderSounds + "explosion-misil");
      KillEffect = Content.Load<SoundEffect>(ContentFolderSounds + "kill-effect");

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
      //TexturaMenu = Content.Load<Texture2D>(ContentFolderTextures + "backMenu");
      TexturaPowerUp = Content.Load<Texture2D>(ContentFolderTextures + "gold");
      Corazon = Content.Load<Texture2D>(ContentFolderTextures + "Vida/1 Corazon");
      Logo = Content.Load<Texture2D>(ContentFolderTextures + "logo");
      LogoVictory = Content.Load<Texture2D>(ContentFolderTextures + "victoria");
      LogoGameOver = Content.Load<Texture2D>(ContentFolderTextures + "gameOver");
      LogoGo = Content.Load<Texture2D>(ContentFolderTextures + "go");
      MiniGunLogo = Content.Load<Texture2D>(ContentFolderTextures + "minigun-logo");
      KillsLogo = Content.Load<Texture2D>(ContentFolderTextures + "kills");
      ClockLogo = Content.Load<Texture2D>(ContentFolderTextures + "clock");
      ObjKillsLogo = Content.Load<Texture2D>(ContentFolderTextures + "Objkills");
      ObjClockLogo = Content.Load<Texture2D>(ContentFolderTextures + "Objclock");
      RocketLaucherLogo = Content.Load<Texture2D>(ContentFolderTextures + "rocketLauncher-logo");
      BoostLogo = Content.Load<Texture2D>(ContentFolderTextures + "boost-logo");
      NoPowerUp = Content.Load<Texture2D>(ContentFolderTextures + "noPowerUp-logo");

      //Efectos
      Noise = Content.Load<Texture2D>(ContentFolderTextures + "perlin");
      NoiseSand = Content.Load<Texture2D>(ContentFolderTextures + "perlinSand");
      TexturaAuxiliar = Content.Load<Texture2D>(ContentFolderTextures + "lava");

      AutoShader = Content.Load<Effect>(ContentFolderEffects + "AutoShader");
      SandShader = Content.Load<Effect>(ContentFolderEffects + "SandShader");
      BlurShader = Content.Load<Effect>(ContentFolderEffects + "GaussianBlur");

      BlurShader.Parameters["screenSize"]?.SetValue(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
      
      SandShader.Parameters["screenSize"]?.SetValue(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
      SandShader.Parameters["TexturaRuido"]?.SetValue(NoiseSand);
      SandShader.Parameters["texturaAuxiliar"]?.SetValue(TexturaPiso);

      AutoShader.Parameters["environmentMap"]?.SetValue(EnvironmentMapRenderTarget);

      AutoShader.Parameters["TexturaRuido"]?.SetValue(Noise);
      AutoShader.Parameters["fuegoTexture"]?.SetValue(TexturaAuxiliar);

      //Iluminacion
      AutoShader.Parameters["ambientColor"]?.SetValue(Color.White.ToVector3());
      AutoShader.Parameters["diffuseColor"]?.SetValue(Color.White.ToVector3());
      AutoShader.Parameters["specularColor"]?.SetValue(Color.White.ToVector3());

      AutoShader.Parameters["KAmbient"]?.SetValue(0.1f);
      AutoShader.Parameters["KDiffuse"]?.SetValue(0.5f);
      AutoShader.Parameters["KSpecular"]?.SetValue(0.5f);
      AutoShader.Parameters["shininess"]?.SetValue(2f);
      //AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);

      //Explosion
      particula = Content.Load<Texture2D>(ContentFolderTextures + "particle");
   

      escenario.LoadContent(Piso, Pared, Column, Ramp, Platform, TexturaPiso, TexturaPared, TexturaColumna, TexturaRampa, TexturaPlataforma);
      detalles.LoadContent(Tree, Rock1, Rock5, Rock10, Tire, TexturaRoca, TexturaRoca, TexturaTire1, TexturaTire2);
      powerUps.LoadContent(CajaAmetralladora, CajaMisil, CajaTurbo, Misil, Bala, BulletSound, PickUpGunSound, RocketSound, PickUpRocketSound,
                           BoostSound, TexturaPowerUp,ExplosionMisil);
      autos.LoadContent(AutoDeportivo, AutoDeCombate, AutoShader,CarCrash, VidaPerdida, KillEffect);


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
            autos.godMode();
          }
          if (Keyboard.GetState().IsKeyDown(Keys.C))
          {
            status = ST_CONTROLES;
          }
          if (Keyboard.GetState().IsKeyDown(Keys.O))
          {
            status = ST_OBJETIVOS;
          }
          break;

        case ST_CONTROLES:
          if (Keyboard.GetState().IsKeyDown(Keys.B))
          {
            status = ST_PRESENTACION;
          }
          break;
        case ST_OBJETIVOS:
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

          detalles.Update(gameTime,autos,escenario);
          escenario.Update(gameTime, autos);

          //vida.Update(autos, powerUps);

          View = Matrix.CreateLookAt(posicionCamara + autos.posAutoPrincipal(), autos.posAutoPrincipal(), Vector3.Up);
          //Camera.Update(gameTime);
          gizmos.UpdateViewProjection(View, Projection);


          //SOL "FIJO"
          //var lightPosition = new Vector3(autos.posAutoPrincipal().X, autos.posAutoPrincipal().Y+20f, autos.posAutoPrincipal().Z);

            // Set the light position and camera position
            // These change every update so we need to set them on every update call
            AutoShader.Parameters["farosPosition"]?.SetValue(autos.posAutoPrincipal() + autos.CarDirection * 25f ); //+ Vector3.UnitY * 50f
            AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);
            AutoShader.Parameters["eyePosition"]?.SetValue(posicionCamara+autos.posAutoPrincipal());
            AutoShader.Parameters["carDirection"]?.SetValue(autos.CarDirection);
            AutoShader.Parameters["Time"]?.SetValue(Time);
            //Console.WriteLine(autos.directionAutoPrincipal());

             particulas = powerUps.particulas();

            boundingFrustum.Matrix = View * Projection;

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
            detalles.iniciarPartida();
            SpriteBatch.Begin();
            dibujarCorazones(tamanioPantalla);
            dibujarPowerUpsLogos(tamanioPantalla);
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

          case ST_VICTORIA:
          if(!(MediaPlayer.State == MediaState.Playing))
          {
            MediaPlayer.Play(Winner);
          }

          if (Keyboard.GetState().IsKeyDown(Keys.Enter))
          {
            
            //this.UnloadContent();
            Thread.Sleep(500);
            autos.iniciarPartida();
            powerUps.iniciarPartida();
            SpriteBatch.Begin();
            dibujarCorazones(tamanioPantalla);
            dibujarPowerUpsLogos(tamanioPantalla);
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
      var Time = (float)gameTime.TotalGameTime.TotalSeconds;
      GraphicsDevice.Clear(Color.LightYellow);
      GraphicsDevice.DepthStencilState = DepthStencilState.Default; //sin esto los autos se ven translucidos y el piso tambien
      Vector2 tamanioPantalla = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
      Console.WriteLine(tamanioPantalla);
      switch (status)
      {
        case ST_COUNTDOWN_3:          
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("3", tamanioPantalla.Y * 0.25f, 1);
          break;

        case ST_COUNTDOWN_2:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("2", tamanioPantalla.Y * 0.25f, 1);
          break;

        case ST_COUNTDOWN_1:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("1", tamanioPantalla.Y * 0.25f, 1);
          break;

        case ST_COUNTDOWN_GO:
          GraphicsDevice.Clear(Color.Black);
          //DrawCenterTextY("GO!", tamanioPantalla.Y * 0.3f, 10);
          SpriteBatch.Begin();
          SpriteBatch.Draw(LogoGo, new Vector2(tamanioPantalla.X * 0.35f, tamanioPantalla.Y*0.3f), Color.WhiteSmoke);
          //SpriteBatch.DrawString(font, "GO", new Vector2(tamanioPantalla.X * 0.32f, tamanioPantalla.Y *0.6f), Color.WhiteSmoke);
          SpriteBatch.End();
          break;

        case ST_PRESENTACION:

            #region Pass 1

            GraphicsDevice.SetRenderTarget(MainSceneRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            //AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);
            escenario.dibujarPiso(View,Projection,AutoShader);
            autos.dibujarAutosMenu(View,Projection,AutoShader,timerMenu);
            
            #endregion

            #region Pass 2

            GraphicsDevice.SetRenderTarget(PresentacionRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            SandShader.Parameters["Time"]?.SetValue(Time);
            FullScreenQuad.Draw(SandShader, "BlowingSand");

            #endregion

            #region Pass 3
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            AutoShader.Parameters["baseTexture"]?.SetValue(MainSceneRenderTarget);
            AutoShader.Parameters["texturaAuxiliar"]?.SetValue(PresentacionRenderTarget);

            FullScreenQuad.Draw(AutoShader, "IntegrarPresentacion");
            
            
            DrawCenterTextY("C -> CONTROLES", tamanioPantalla.X * 0.2f, 0.2f);
            DrawCenterTextY("O -> OBJETIVOS", tamanioPantalla.X * 0.25f, 0.2f);
            DrawCenterTextY("G -> GOD MODE", tamanioPantalla.X * 0.3f, 0.2f);
            DrawCenterTextY("ENTER -> COMENZAR", tamanioPantalla.X * 0.35f, 0.2f);
            DrawCenterTextY("ESC -> SALIR", tamanioPantalla.X*0.4f, 0.2f);
            SpriteBatch.Begin();
            SpriteBatch.Draw(Logo, new Vector2(tamanioPantalla.X * 0.2f, tamanioPantalla.Y*0.1f), Color.WhiteSmoke);
            SpriteBatch.End();
            #endregion

           
            break;

        case ST_CONTROLES:

            #region Pass 1

            GraphicsDevice.SetRenderTarget(MainSceneRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            //AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);
            escenario.dibujarPiso(View,Projection,AutoShader);
            
            #endregion

            #region Pass 2

            GraphicsDevice.SetRenderTarget(PresentacionRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            SandShader.Parameters["Time"]?.SetValue(Time);
            FullScreenQuad.Draw(SandShader, "BlowingSand");

            #endregion

            #region Pass 3
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            AutoShader.Parameters["baseTexture"]?.SetValue(MainSceneRenderTarget);
            AutoShader.Parameters["texturaAuxiliar"]?.SetValue(PresentacionRenderTarget);

            FullScreenQuad.Draw(AutoShader, "IntegrarPresentacion");
            #endregion
            
            DrawCenterTextY("CONTROLES", tamanioPantalla.Y * 0.1f, 0.3f);
            DrawCenterTextY("F -> DISPARAR", tamanioPantalla.Y * 0.3f, 0.2f);
            DrawCenterTextY("W -> AVANZAR", tamanioPantalla.Y * 0.36f, 0.2f);
            DrawCenterTextY("S -> RETROCEDER", tamanioPantalla.Y * 0.42f, 0.2f);
            DrawCenterTextY("D -> ROTAR DERECHA", tamanioPantalla.Y * 0.48f, 0.2f);
            DrawCenterTextY("A -> ROTAR IZQUIERDA", tamanioPantalla.Y * 0.54f, 0.2f);
            DrawCenterTextY("SPACE -> SALTAR", tamanioPantalla.Y * 0.6f, 0.2f);

            DrawCenterTextY("B -> VOLVER AL MENU", tamanioPantalla.Y *0.75f, 0.2f);
          
          
            break;

            case ST_OBJETIVOS:

            #region Pass 1

            GraphicsDevice.SetRenderTarget(MainSceneRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            //AutoShader.Parameters["lightPosition"]?.SetValue(lightPosition);
            escenario.dibujarPiso(View,Projection,AutoShader);
            
            #endregion

            #region Pass 2

            GraphicsDevice.SetRenderTarget(PresentacionRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            SandShader.Parameters["Time"]?.SetValue(Time);
            FullScreenQuad.Draw(SandShader, "BlowingSand");

            #endregion

            #region Pass 3
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            AutoShader.Parameters["baseTexture"]?.SetValue(MainSceneRenderTarget);
            AutoShader.Parameters["texturaAuxiliar"]?.SetValue(PresentacionRenderTarget);

            FullScreenQuad.Draw(AutoShader, "IntegrarPresentacion");
            #endregion

            SpriteBatch.Begin();
            SpriteEffects nuevo = new SpriteEffects();
            SpriteBatch.Draw(ObjClockLogo, new Vector2(tamanioPantalla.X * 0.46f, tamanioPantalla.Y * 0.33f), null,Color.WhiteSmoke*0.7f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
            //SpriteBatch.DrawString(font2, "120", new Vector2(tamanioPantalla.X * 0.505F, tamanioPantalla.Y * 0.37f), Color.Black);
            SpriteBatch.Draw(ObjKillsLogo, new Vector2(tamanioPantalla.X * 0.46f, tamanioPantalla.Y * 0.53f), null, Color.WhiteSmoke*0.7f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
            //SpriteBatch.DrawString(font2, "5", new Vector2(tamanioPantalla.X * 0.51f, tamanioPantalla.Y * 0.57f), Color.Black);
            //SpriteBatch.Draw(Corazon, new Vector2(tamanioPantalla.X * 0.46f, tamanioPantalla.Y * 0.73f), Color.WhiteSmoke);
            SpriteBatch.End();

            DrawCenterTextY("OBJETIVOS", tamanioPantalla.Y * 0.1f, 0.3f);
            DrawCenterTextY("SOBREVIVIR ", tamanioPantalla.Y * 0.25f, 0.2f);
            DrawCenterTextY("ELIMINAR ", tamanioPantalla.Y * 0.45f, 0.2f);
            //DrawCenterTextY("NO PERDER ", tamanioPantalla.Y * 0.65f, 0.2f);
            DrawCenterTextY("B -> VOLVER AL MENU", tamanioPantalla.Y *0.75f, 0.2f);
          
            break;

        case ST_DERROTA:          
          GraphicsDevice.Clear(Color.Black);
          SpriteBatch.Begin();
          SpriteBatch.DrawString(font2, "Has Sobrevivido " + ((int)totalGameTime).ToString() + " Segundos",new Vector2(tamanioPantalla.X * 0.35f, tamanioPantalla.Y * 0.5f), Color.WhiteSmoke);
          SpriteBatch.DrawString(font2, "Has Asesinado " + (autos.getAutoBajas().ToString()) + " Enemigos",new Vector2(tamanioPantalla.X * 0.36f, tamanioPantalla.Y * 0.6f), Color.WhiteSmoke);
          SpriteBatch.Draw(LogoGameOver, new Vector2(tamanioPantalla.X * 0.28f, tamanioPantalla.Y*0.3f), Color.WhiteSmoke);
          SpriteBatch.End();
        break;

        case ST_VICTORIA:
          GraphicsDevice.Clear(Color.Black);
          SpriteBatch.Begin();
          SpriteBatch.Draw(LogoVictory, new Vector2(tamanioPantalla.X * 0.26f, tamanioPantalla.Y*0.3f), Color.WhiteSmoke);
          SpriteBatch.End();
        break;

        case ST_JUEGO:
          SpriteBatch.Begin();
          dibujarPowerUpsLogos(tamanioPantalla);
          SpriteBatch.DrawString(font2, ((int)totalGameTime).ToString(), new Vector2(tamanioPantalla.X * 0.0545f, tamanioPantalla.Y * 0.056f), Color.Black*0.7f);
          SpriteBatch.DrawString(font2, (autos.getAutoBajas().ToString()), new Vector2(tamanioPantalla.X * 0.969f, tamanioPantalla.Y * 0.056f), Color.Black*0.7f);
          dibujarCorazones(tamanioPantalla);
          
          for(int i = 0; i < particulas.Count; i++)
          {
            
            if(particulas[i].viva()) //solo se dibujan aquellas particulas que sigan vivas
            {
              Vector3 posicion3D = GraphicsDevice.Viewport.Project(particulas[i].posicion(),Projection,View,Matrix.Identity); 
              //se pasan las posiciones de cada particula de espacio de mundo (3D) a espacio de pantalla (2D)
              Vector2 posPantalla = new Vector2(posicion3D.X,posicion3D.Y);

              SpriteBatch.Draw(particula,posPantalla, null,particulas[i].color(), 0f, Vector2.Zero,particulas[i].size(),SpriteEffects.None,0f);
            }
          }
         
          #region Pass 1-3

          for (var face = CubeMapFace.PositiveX; face <= CubeMapFace.NegativeZ; face++)
          {
               if(face == CubeMapFace.PositiveX || face == CubeMapFace.NegativeX || face == CubeMapFace.PositiveY){

                GraphicsDevice.SetRenderTarget(EnvironmentMapRenderTarget, face);
                GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
                
                escenario.dibujarEscenario(View, Projection, AutoShader, true, "Luz",boundingFrustum);
                detalles.dibujarDetalles(View, Projection, AutoShader, "Luz",boundingFrustum);
                powerUps.dibujarPowerUps(View, Projection, AutoShader,"Luz",boundingFrustum);
                autos.dibujarAutos(View, Projection, AutoShader, "Luz",boundingFrustum);
                }
          }
          #endregion

          #region Pass 4


            GraphicsDevice.SetRenderTarget(MainSceneRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            escenario.dibujarEscenario(View, Projection, AutoShader, true, "Luz",boundingFrustum);
            detalles.dibujarDetalles(View, Projection, AutoShader, "Luz",boundingFrustum);
            powerUps.dibujarPowerUps(View, Projection, AutoShader, "Luz",boundingFrustum);
            
            autos.dibujarAutos(View, Projection, AutoShader, "Reflejo",boundingFrustum);

          #endregion

          #region Pass 5
            GraphicsDevice.SetRenderTarget(FirstPassBloomRenderTarget);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);
            autos.dibujarAutos(View, Projection, AutoShader, "Bloom",boundingFrustum);
            escenario.dibujarEscenario(View, Projection, AutoShader, true, "BloomNegro",boundingFrustum);
            detalles.dibujarDetalles(View, Projection, AutoShader, "BloomNegro",boundingFrustum);
            powerUps.dibujarPowerUps(View, Projection, AutoShader, "BloomPowerUp",boundingFrustum);
            
          #endregion

          #region Pass 6

            var bloomTexture = FirstPassBloomRenderTarget;
            var finalBloomRenderTarget = SecondPassBloomRenderTarget;
            var PassCount = 1;
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


          #region Pass 7
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1f, 0);

            
            AutoShader.Parameters["baseTexture"]?.SetValue(MainSceneRenderTarget);
            AutoShader.Parameters["bloomTexture"]?.SetValue(finalBloomRenderTarget);

            FullScreenQuad.Draw(AutoShader, "Integrar");
          #endregion

          dibujarGizmos = false;
          if(dibujarGizmos)
          {
          autos.dibujarBoundingBoxes(gizmos); //OBB de autos deportivos bien ubicadas
          detalles.dibujarBoundingBoxes(gizmos); //BB de arboles bien ubicadas
          escenario.dibujarBoundingBoxes(gizmos); //BB de plataformas bien ubicadas
          powerUps.dibujarBoundingBoxes(gizmos); //BB Bien ubicadas

          gizmos.DrawFrustum(View * Projection, Color.Red);

          gizmos.Draw();
          }
          SpriteBatch.End(); //si lo ponemos antes de dibujar los modelos, los autos y el piso se dibujan translucidos 
          
          if(autos.victoriaPorKills() || (int)totalGameTime >= 120)
          {
            status = ST_VICTORIA;
            MediaPlayer.Stop();
          }
          
          
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
      SpriteBatch.DrawString(font, msg, new Vector2(0, 0), Color.OrangeRed);
      SpriteBatch.End();
    }

    public void DrawCenterTextY(string msg, float Y, float escala)
    {
      var W = GraphicsDevice.Viewport.Width;
      var H = GraphicsDevice.Viewport.Height;
      var size = font.MeasureString(msg) * escala;
      SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
      Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, Y, 0));
      SpriteBatch.DrawString(font, msg, new Vector2(0f, 0), Color.OrangeRed);
      SpriteBatch.End();
    }

    public void DrawRightText(string msg, float Y, float escala)
    {
      var W = GraphicsDevice.Viewport.Width;
      var H = GraphicsDevice.Viewport.Height;
      var size = font.MeasureString(msg) * escala;
      SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
          Matrix.CreateScale(escala) * Matrix.CreateTranslation(12*W/20, Y, 0));
      SpriteBatch.DrawString(font, msg, new Vector2(0, 0), Color.OrangeRed);
      SpriteBatch.End();
    }
    public void dibujarCorazones(Vector2 tamanioPantalla)
    {
      
      //CORREGIR DISPOSE DEJA CUADRADO NEGRO
      if(autos.vidaProtagonista >= 100){
        
        SpriteBatch.Draw(Corazon, new Vector2(tamanioPantalla.X * 0.93f, tamanioPantalla.Y * 0.85f), Color.WhiteSmoke*0.5f);
      }

      if(autos.vidaProtagonista >= 75)
      {
        //Corazon4.Dispose();
        
        SpriteBatch.Draw(Corazon, new Vector2(tamanioPantalla.X * 0.88f, tamanioPantalla.Y * 0.85f), Color.WhiteSmoke*0.5f);
      }
      if(autos.vidaProtagonista >= 50)
      {
        //Corazon3.Dispose();
       
        SpriteBatch.Draw(Corazon, new Vector2(tamanioPantalla.X * 0.83f, tamanioPantalla.Y * 0.85f), Color.WhiteSmoke*0.5f);
      }
      if(autos.vidaProtagonista >= 25)
      {
        //Corazon2.Dispose();
        SpriteBatch.Draw(Corazon, new Vector2(tamanioPantalla.X * 0.78f, tamanioPantalla.Y * 0.85f), Color.WhiteSmoke*0.5f);
      }

      }

    public void dibujarPowerUpsLogos(Vector2 tamanioPantalla)
    {
      SpriteEffects nuevo = new SpriteEffects();

      SpriteBatch.Draw(KillsLogo, new Vector2(tamanioPantalla.X * 0.92f, tamanioPantalla.Y * 0.01f), null,
         Color.WhiteSmoke*0.5f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);

      SpriteBatch.Draw(ClockLogo, new Vector2(tamanioPantalla.X * 0.01f, tamanioPantalla.Y * 0.01f), null,
         Color.WhiteSmoke*0.5f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
      
      if(powerUps.getVacio()){
        
        SpriteBatch.Draw(NoPowerUp, new Vector2(tamanioPantalla.X * 0.01f, tamanioPantalla.Y * 0.85f), null,
         Color.WhiteSmoke*0.5f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
      }

      else if(powerUps.getAmetralaldora()){
        
        SpriteBatch.Draw(MiniGunLogo, new Vector2(tamanioPantalla.X * 0.01f, tamanioPantalla.Y * 0.85f), null,
         Color.WhiteSmoke*0.5f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
      }

      else if(powerUps.getMisil()){
        
        SpriteBatch.Draw(RocketLaucherLogo, new Vector2(tamanioPantalla.X * 0.01f, tamanioPantalla.Y * 0.85f), null,
         Color.WhiteSmoke*0.5f, 0f, Vector2.Zero, new Vector2(1f,1f), nuevo,0f);
      }
    }
  }
}