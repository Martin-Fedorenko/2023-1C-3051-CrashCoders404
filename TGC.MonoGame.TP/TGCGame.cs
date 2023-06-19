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
    private Effect IluminacionShader { get; set; }
    

    private Autos autos;
    private Detalles detalles;
    private Escenario escenario;
    private PowerUps powerUps;

    // Modelos
    private Model Piso { get; set; }
    private Model Pared { get; set; }
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
    public SpriteFont font;
    public int status = ST_PRESENTACION;


    //Musica
    private Song SongGame { get; set; }
    private Song SongMenu { get; set; }
    private Song SongCountdown { get; set; }
  
    //Sonidos
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect BulletSound { get; set; }
    private SoundEffect PickUpGunSound { get; set; }
    private SoundEffect RocketSound { get; set; }
    private SoundEffect PickUpRocketSound { get; set; }
    private SoundEffect BoostSound { get; set; }
    private SoundEffect CarCrash { get; set; }


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


    //HUD
    private float totalGameTime;
    private float countdownStart;
    private Vector2 autoPos;

        protected override void Initialize()
    {
      gizmos = new Gizmos();

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
      gizmos.LoadContent(GraphicsDevice, Content);
      SpriteBatch = new SpriteBatch(GraphicsDevice);

      font = Content.Load<SpriteFont>(ContentFolderSpriteFonts + "CascadiaCode/CascadiaCodePL");

      //Escenario
      Piso = Content.Load<Model>(ContentFolder3D + "Arena/Plano"); //No tiene textura incluida
      Pared = Content.Load<Model>(ContentFolder3D + "Arena/Arena"); //No tiene textura incluida
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
      Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader"); //aca
      EscenarioShader = Content.Load<Effect>(ContentFolderEffects + "EscenarioShader");
      DetallesShader = Content.Load<Effect>(ContentFolderEffects + "DetallesShader");
      AutoShader = Content.Load<Effect>(ContentFolderEffects + "AutoShader");

      // iluminacion
      AutoShader.Parameters["ambientColor"].SetValue(new Vector3(1f, 1f, 1f));
      AutoShader.Parameters["diffuseColor"].SetValue(new Vector3(0.1f, 0.1f, 0.6f));
      AutoShader.Parameters["specularColor"].SetValue(new Vector3(1f, 1f, 1f));

      AutoShader.Parameters["KAmbient"].SetValue(0.1f);
      AutoShader.Parameters["KDiffuse"].SetValue(0.1f);
      AutoShader.Parameters["KSpecular"].SetValue(1f);
      AutoShader.Parameters["shininess"].SetValue(5f);

      //Música y sonido
      BulletSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-ametralladora");
      PickUpGunSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-ametralladora");
      RocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "bullet-rocket");
      PickUpRocketSound = Content.Load<SoundEffect>(ContentFolderSounds + "pickup-rocket");
      BoostSound = Content.Load<SoundEffect>(ContentFolderSounds + "boost-effect");
      CarCrash = Content.Load<SoundEffect>(ContentFolderSounds + "car-crash");

      //Textura
      TexturaPiso = Content.Load<Texture2D>(ContentFolderTextures + "ground");
      TexturaPared = Content.Load<Texture2D>(ContentFolderTextures + "stones");
      TexturaColumna = Content.Load<Texture2D>(ContentFolderTextures + "displacement");
      TexturaRampa = Content.Load<Texture2D>(ContentFolderTextures + "color");
      TexturaPlataforma = Content.Load<Texture2D>(ContentFolderTextures + "colorPlatform");
      TexturaRoca = Content.Load<Texture2D>(ContentFolderTextures + "rock");
      TexturaArbol = Content.Load<Texture2D>(ContentFolderTextures + "tree");
      TexturaTire1 = Content.Load<Texture2D>(ContentFolderTextures + "tire1");
      TexturaTire2 = Content.Load<Texture2D>(ContentFolderTextures + "tire2");
      TexturaMenu = Content.Load<Texture2D>(ContentFolderTextures + "backMenu");
      

      escenario.LoadContent(Piso, Pared, Column, Ramp, Platform, TexturaPiso, TexturaPared, TexturaColumna, TexturaRampa, TexturaPlataforma);
      detalles.LoadContent(Tree, Rock1, Rock5, Rock10, Tire, TexturaRoca, TexturaRoca, TexturaTire1, TexturaTire2);
      powerUps.LoadContent(CajaAmetralladora, CajaMisil, CajaTurbo, Misil, Bala, BulletSound, PickUpGunSound, RocketSound, PickUpRocketSound,
                           BoostSound);
      autos.LoadContent(AutoDeportivo, AutoDeCombate, AutoShader,CarCrash);


      //Musica
      SongGame = Content.Load<Song>(ContentFolderMusic + "trap-movement");
      SongMenu = Content.Load<Song>(ContentFolderMusic + "retro-platforming");
      SongCountdown = Content.Load<Song>(ContentFolderMusic + "countdown-start");

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
          if(!(MediaPlayer.State == MediaState.Playing))
          {
            MediaPlayer.Play(SongMenu);
          }

          if (Keyboard.GetState().IsKeyDown(Keys.Enter))
          {
            status = ST_COUNTDOWN_3;
            MediaPlayer.Stop();
            MediaPlayer.Play(SongCountdown);
            countdownStart += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
          //MediaPlayer.Play(SongGame); No se por qué acá no anda, se corta la música
          break;
        }
        break;

        case ST_JUEGO:
          if(!(MediaPlayer.State == MediaState.Playing))
          {
            //MediaPlayer.Play(SongGame);
          }

          totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
          autos.Update(gameTime,totalGameTime);
          powerUps.Update(gameTime, autos, detalles, escenario);

          detalles.Update(gameTime,autos);
          escenario.Update(gameTime, autos);

          View = Matrix.CreateLookAt(posicionCamara + autos.posAutoPrincipal(), autos.posAutoPrincipal(), Vector3.Up);
          //Camera.Update(gameTime);
          gizmos.UpdateViewProjection(View, Projection);

          //var lightPosition = new Vector3(autos.posAutoPrincipal().X+5f, autos.posAutoPrincipal().Y+10f, autos.posAutoPrincipal().Z+5f);

            // Set the light position and camera position
            // These change every update so we need to set them on every update call
            //AutoShader.Parameters["lightPosition"].SetValue(lightPosition);
            //AutoShader.Parameters["eyePosition"].SetValue(posicionCamara + autos.posAutoPrincipal());

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


      switch (status)
      {
        case ST_COUNTDOWN_3:          
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("3", 300, 10);
          break;

        case ST_COUNTDOWN_2:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("2", 300, 10);
          break;

        case ST_COUNTDOWN_1:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("1", 300, 10);
          break;

        case ST_COUNTDOWN_GO:
          GraphicsDevice.Clear(Color.Black);
          DrawCenterTextY("GO!", 300, 10);
          break;

        case ST_PRESENTACION:
          autos.dibujarAutosMenu(View,Projection,AutoShader);
          DrawCenterTextY("CRASH CODERS 404 ", 100, 4);
          DrawCenterTextY("C -> CONTROLES", 400, 2);
          DrawCenterTextY("G -> GOD MODE", 500, 2);
          DrawCenterTextY("ENTER -> COMENZAR", 600, 2);
          DrawRightText("ESC -> SALIR", 900, 1);
          break;

        case ST_CONTROLES:
          DrawCenterTextY("CONTROLES", 100, 4);
          DrawCenterTextY("W -> AVANZAR", 300, 3);
          DrawCenterTextY("S -> RETROCEDER", 400, 3);
          DrawCenterTextY("D -> ROTAR DERECHA", 500, 3);
          DrawCenterTextY("A -> ROTAR IZQUIERDA", 600, 3);
          DrawCenterTextY("SPACE -> SALTAR", 700, 3);
          DrawRightText("B -> VOLVER AL MENU", 900, 1);

          break;

        case ST_JUEGO:
          SpriteBatch.Begin();
          SpriteBatch.DrawString(font, "Tiempo:" + ((int)totalGameTime).ToString(), new Vector2(10, 10), Color.Black);
          SpriteBatch.DrawString(font, "Velocidad:" + (autos.autoSpeed().ToString()), new Vector2(700, 10), Color.Black);
          SpriteBatch.DrawString(font, "PowerUp:" + (powerUps.powerUpActual()), new Vector2(1250, 900), Color.Black);

          autoPos = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2 - 80);
          if (autos.getVidaProtagonista() > 50)
          {
            SpriteBatch.DrawString(font, autos.getVidaProtagonista().ToString(), autoPos, Color.Green);
          } 
          else if(autos.getVidaProtagonista() <= 50)
          {
            SpriteBatch.DrawString(font, autos.getVidaProtagonista().ToString(), autoPos, Color.Yellow);
          } 
          else if(autos.getVidaProtagonista() <= 20)
          {
            SpriteBatch.DrawString(font, autos.getVidaProtagonista().ToString(), autoPos, Color.Red);
          }

          escenario.dibujarEscenario(View, Projection, EscenarioShader);
          detalles.dibujarDetalles(View, Projection, DetallesShader);
          powerUps.dibujarPowerUps(View, Projection, Effect);
          autos.dibujarAutos(View, Projection, AutoShader);

          autos.dibujarBoundingBoxes(gizmos); //OBB de autos deportivos bien ubicadas
          detalles.dibujarBoundingBoxes(gizmos); //BB de arboles bien ubicadas
          escenario.dibujarBoundingBoxes(gizmos); //BB de plataformas bien ubicadas
          powerUps.dibujarBoundingBoxes(gizmos); //BB Bien ubicadas

          gizmos.Draw();
          SpriteBatch.End(); //si lo ponemos antes de dibujar los mdoelos, los autos y el piso se dibujan translucidos 
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
  }
}
