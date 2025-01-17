using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.Viewer.Gizmos;

namespace TGC.MonoGame.TP
{
  public class PowerUps
  {
    public PowerUps() { }
    // Modelos
    private Model CajaTurbo { get; set; }
    private Model CajaAmetralladora { get; set; }
    private Model CajaMisil { get; set; }
    private Model Misil { get; set; }
    private Model Bala { get; set; }

    private Texture2D TexturaPowerUp { get; set; }


    // Matrices
    private Matrix[] AmetralladorasWorld;
    private Matrix[] TurbosWorld;
    private Matrix[] MisilesWorld;
    private Matrix MisilWorld;
    public Matrix[] BalasWorld;

    //Sonido  
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect BulletSound { get; set; }
    private SoundEffect PickUpGunSound { get; set; }
    private SoundEffect RocketSound { get; set; }
    private SoundEffect PickUpRocketSound { get; set; }
    private SoundEffect BoostSound { get; set; }
    private SoundEffect ExplosionMisil { get; set; }

    private int agarrarAmetralladora = 0;
    private bool tieneAmetalladora;
    private int agarrarLanzaCohetes = 0;
    private bool tieneLanzaCohetes;
    private int agarrarTurbo = 0;
    private bool tieneTurbo;

    //Posiciones
    private Vector3 AmetralladoraPos = new Vector3(70, 20, -390);
    private Vector3 Ametralladora1Pos = new Vector3(0, 20, 650);
    private Vector3 Ametralladora2Pos = new Vector3(-210, 20, 210);
    private Vector3 Ametralladora3Pos = new Vector3(210, 35, 300);
    private Vector3 Ametralladora4Pos = new Vector3(-435, 35, -195);
    private Vector3 Ametralladora5Pos = new Vector3(600, 20, 80);

    private Vector3 MisilPos = new Vector3(35, 20, -305);
    private Vector3 Misil1Pos = new Vector3(-250, 20, 80);
    private Vector3 Misil2Pos = new Vector3(-510, 20, 570);
    private Vector3 Misil3Pos = new Vector3(250, 35, 260);
    private Vector3 Misil4Pos = new Vector3(-475, 20, -155);
    private Vector3 Misil5Pos = new Vector3(490, 20, -350);
        
    private Vector3 TurboPos = new Vector3(140, 20, -480);
    private Vector3 Turbo1Pos = new Vector3(-400, 35, -235);
    private Vector3 Turbo2Pos = new Vector3(-310, 20, 290);
    private Vector3 Turbo3Pos = new Vector3(170, 35, 330);
    private Vector3 Turbo4Pos = new Vector3(-125, 20, -240);
    private Vector3 Turbo5Pos = new Vector3(525, 20, 630);

    // Colliders
    private BoundingBox[] collidersAmetralladoras;
    private BoundingBox[] collidersTurbos;
    private BoundingBox[] collidersMisiles;
    private BoundingBox MisilPowerUP;
    private Matrix MisilBBPower;
    public OrientedBoundingBox colliderMisil;
    private BoundingBox BalasPowerUp;
    public OrientedBoundingBox[] collidersBalas;
    private Matrix BalasBBPower;
    private BoundingBox PowerUpBox;
    private List<int> collidedindexAmetralladora;
    private List<int> collidedindexMisil;
    private List<int> collidedindexTurbo;
    private float[] TimersTurbos;
    private float[] TimersAmetralladoras;
    private float[] TimersMisiles;
    //Bones
    private Matrix[] relativeMatrices;

    // Variables
    enum PowerUp
    {
      Turbo,
      Ametralladora,
      Misil,
      None
    }

    private PowerUp currentPowerUp = PowerUp.None;
    private float powerUpTimer = 0f;
    private int ametralladoraCounter = 0;
    private float ametralladoraCooldown = 0f;
    public float recorridoMisil = 0f;
    public float[] recorridoBalas = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
    private Vector3 misilPos = new Vector3(0, 0, 0);
    private Vector3[] balasPos;
    private float misilRot = 0f;
    private float[] balasRot = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
    private float cuartoDeVuelta = MathF.PI / 2;
    private float totalGameTime = 0f;
    
    //explosion
    private List<ParticulasExplosion> particulasExplosion;
    private int CantidadParticulas = 5000;

    public void Initialize()
    {
      collidedindexAmetralladora = new List<int>();
      collidedindexMisil = new List<int>();
      collidedindexTurbo = new List<int>();

      // PowerAmetralladora
      AmetralladorasWorld = new Matrix[]
      {
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(AmetralladoraPos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Ametralladora1Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Ametralladora2Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Ametralladora3Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Ametralladora4Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Ametralladora5Pos),
      };

      MisilesWorld = new Matrix[]
      {
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(MisilPos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Misil1Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Misil2Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Misil3Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Misil4Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Misil5Pos)
      };

      TurbosWorld = new Matrix[]
      {
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(TurboPos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Turbo1Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Turbo2Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Turbo3Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Turbo4Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(Turbo5Pos)
      };

      MisilWorld = new Matrix();
      MisilWorld = Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f, -10f, -10f);

      BalasWorld = new Matrix[]
      {
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f),
        Matrix.CreateScale(0f, 0f, 0f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f,-10f,-10f)
      };

      balasPos = new Vector3[] {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0)
      };

      TimersTurbos = new float[TurbosWorld.Length];
      TimersAmetralladoras = new float[AmetralladorasWorld.Length];
      TimersMisiles = new float[MisilesWorld.Length];
      
      for(int i =0; i < TurbosWorld.Length; i++)
        TimersTurbos[i] = 0;
      
      for(int i =0; i < AmetralladorasWorld.Length; i++)
        TimersAmetralladoras[i] = 0;
            
      for(int i =0; i < MisilesWorld.Length; i++)
        TimersMisiles[i] = 0;

      particulasExplosion = new List<ParticulasExplosion>();
        
      for(int i = 0; i < CantidadParticulas; i++)
      {
        particulasExplosion.Add(new ParticulasExplosion(Vector3.Zero,Vector3.Zero,1f,0f)); //de esta manera se crea una unica vez la lista
      }

    }


    public void LoadContent(Model cajaAmetralladora, Model cajaMisil, Model cajaTurbo, Model misil, Model bala, 
                            SoundEffect bulletSound, SoundEffect pickUpGunSound, SoundEffect pickUpRocketSound, SoundEffect rocketSound, 
                            SoundEffect boostSound, Texture2D texturaPowerUp, SoundEffect explosionMisil)
    {
      CajaAmetralladora = cajaAmetralladora;
      CajaMisil = cajaMisil;
      CajaTurbo = cajaTurbo;
      Misil = misil;
      Bala = bala;
      BulletSound = bulletSound;
      PickUpGunSound = pickUpGunSound;
      PickUpRocketSound = pickUpRocketSound;
      RocketSound = rocketSound;
      BoostSound = boostSound;
      TexturaPowerUp = texturaPowerUp;
      ExplosionMisil = explosionMisil;



      //Collisions
      PowerUpBox = BoundingVolumesExtensions.CreateAABBFrom(CajaAmetralladora);
      PowerUpBox = BoundingVolumesExtensions.Scale(PowerUpBox, new Vector3(0.1f, 3.5f, 0.1f));
      collidersAmetralladoras = new BoundingBox[]
      {
       new BoundingBox(PowerUpBox.Min + AmetralladoraPos,PowerUpBox.Max + AmetralladoraPos),
       new BoundingBox(PowerUpBox.Min + Ametralladora1Pos,PowerUpBox.Max + Ametralladora1Pos ),
       new BoundingBox(PowerUpBox.Min + Ametralladora2Pos,PowerUpBox.Max + Ametralladora2Pos),
       new BoundingBox(PowerUpBox.Min + Ametralladora3Pos,PowerUpBox.Max + Ametralladora3Pos),
       new BoundingBox(PowerUpBox.Min + Ametralladora4Pos,PowerUpBox.Max + Ametralladora4Pos),
       new BoundingBox(PowerUpBox.Min + Ametralladora5Pos,PowerUpBox.Max + Ametralladora5Pos)
      };

      collidersMisiles = new BoundingBox[]
      {
       new BoundingBox(PowerUpBox.Min + MisilPos,PowerUpBox.Max + MisilPos),
       new BoundingBox(PowerUpBox.Min + Misil1Pos,PowerUpBox.Max + Misil1Pos),
       new BoundingBox(PowerUpBox.Min + Misil2Pos,PowerUpBox.Max + Misil2Pos),
       new BoundingBox(PowerUpBox.Min + Misil3Pos,PowerUpBox.Max + Misil3Pos),
       new BoundingBox(PowerUpBox.Min + Misil4Pos,PowerUpBox.Max + Misil4Pos),
       new BoundingBox(PowerUpBox.Min + Misil5Pos,PowerUpBox.Max + Misil5Pos)
      };

      collidersTurbos = new BoundingBox[]
      {
       new BoundingBox(PowerUpBox.Min + TurboPos,PowerUpBox.Max + TurboPos),
       new BoundingBox(PowerUpBox.Min + Turbo1Pos,PowerUpBox.Max + Turbo1Pos),
       new BoundingBox(PowerUpBox.Min + Turbo2Pos,PowerUpBox.Max + Turbo2Pos),
       new BoundingBox(PowerUpBox.Min + Turbo3Pos,PowerUpBox.Max + Turbo3Pos),
       new BoundingBox(PowerUpBox.Min + Turbo4Pos,PowerUpBox.Max + Turbo4Pos),
       new BoundingBox(PowerUpBox.Min + Turbo5Pos,PowerUpBox.Max + Turbo5Pos)
      };

      MisilPowerUP = BoundingVolumesExtensions.CreateAABBFrom(Misil);
      MisilPowerUP = BoundingVolumesExtensions.Scale(MisilPowerUP, new Vector3(0.1f, 0.1f, 0.1f));
      colliderMisil = OrientedBoundingBox.FromAABB(new BoundingBox(MisilPowerUP.Min - new Vector3(0f,190f,0f), MisilPowerUP.Max - new Vector3(0f,190f,0f)));

      BalasPowerUp = BoundingVolumesExtensions.CreateAABBFrom(bala);
      BalasPowerUp = BoundingVolumesExtensions.Scale(BalasPowerUp, new Vector3(0.15f, 0.05f, 0.15f));
      collidersBalas = new OrientedBoundingBox[]
      {
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f))),
        OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min - new Vector3(0f,190f,0f), BalasPowerUp.Max - new Vector3(0f,190f,0f)))
      };
    }

    public void Update(GameTime gameTime, Autos autos, Detalles detalles, Escenario escenario)
    {
      var keyboardState = Keyboard.GetState();
      var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
      totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
      OrientedBoundingBox autoCollider = autos.getAutoPrincipalBox();

      tieneAmetalladora = false;
      tieneLanzaCohetes = false;
      tieneTurbo = false;

      for (int index = 0; index < AmetralladorasWorld.Length; index++)
      {
        AmetralladorasWorld[index] = Matrix.CreateRotationX(elapsedTime * 1.2f) * AmetralladorasWorld[index] * Matrix.CreateTranslation(new Vector3(0f,MathF.Sin(totalGameTime) *0.15f,0f));
      }
      for (int index = 0; index < MisilesWorld.Length; index++)
      {
        MisilesWorld[index] = Matrix.CreateRotationX(elapsedTime * 1.2f) * MisilesWorld[index] * Matrix.CreateTranslation(new Vector3(0f,MathF.Sin(totalGameTime) *0.15f,0f));;
      }
      for (int index = 0; index < TurbosWorld.Length; index++)
      {
        TurbosWorld[index] = Matrix.CreateRotationX(elapsedTime * 1.2f) * TurbosWorld[index] * Matrix.CreateTranslation(new Vector3(0f,MathF.Sin(totalGameTime) *0.15f,0f));;
      }

      if (currentPowerUp != PowerUp.None)
      {
        powerUpTimer -= elapsedTime;
      }
      if (currentPowerUp == PowerUp.Ametralladora)
      {
        ametralladoraCooldown -= elapsedTime;
        if(agarrarAmetralladora == 0)
        {
          agarrarAmetralladora = 1;
          Instance = PickUpGunSound.CreateInstance();
          Instance.Play();
        }
        tieneAmetalladora = true;
      }
      if (currentPowerUp == PowerUp.Turbo)
      {
          autos.aplicarTurbo();
          currentPowerUp = PowerUp.None;

          if(agarrarTurbo == 0)
          {
            Instance = BoostSound.CreateInstance();
            Instance.Play();
            agarrarTurbo = 1;
          }
          tieneTurbo = true;
      }
      if(currentPowerUp == PowerUp.Misil)
      {
        if(agarrarLanzaCohetes == 0)
          {
            Instance = RocketSound.CreateInstance();
            Instance.Play();
            agarrarLanzaCohetes = 1;
          }
          tieneLanzaCohetes = true;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.F))
      {
        if (currentPowerUp == PowerUp.Ametralladora)
        {
          if (powerUpTimer > 0f)
          {
            if (ametralladoraCounter > 0)
            {
              if (ametralladoraCooldown <= 0f)
              {
                ametralladoraCounter--;
                dispararBala(autos.posAutoPrincipal()+autos.CarDirection*20f, autos.Rotation, ametralladoraCounter);
                ametralladoraCooldown = 0.25f;
                Instance = BulletSound.CreateInstance();
                Instance.Play();
              }
            }
            else
            {
              currentPowerUp = PowerUp.None;
            }
          }
          else
          {
            currentPowerUp = PowerUp.None;
          }
        }
        else if (currentPowerUp == PowerUp.Misil)
        {
          dispararMisil(autos.posAutoPrincipal()+autos.CarDirection*35f , autos.Rotation);
          Instance = PickUpRocketSound.CreateInstance();
          Instance.Play();
          currentPowerUp = PowerUp.None;
        }
      }

      if (recorridoMisil > 0f)
      {
        recorridoMisil -= elapsedTime;
        misilPos += MisilWorld.Up * 5000f * elapsedTime;
        MisilWorld = Matrix.CreateScale(0.05f, 0.05f, 0.05f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(misilRot) * Matrix.CreateTranslation(misilPos) * Matrix.CreateTranslation(0f, 10f, 0f);
        colliderMisil = OrientedBoundingBox.FromAABB(new BoundingBox(MisilPowerUP.Min + misilPos + (MisilWorld.Up * 150)  - new Vector3(0f,190f,0f), MisilPowerUP.Max + misilPos + (MisilWorld.Up * 150) - new Vector3(0f,190f,0f)));
        colliderMisil.Rotate(Matrix.CreateRotationY(misilRot));

          //Colisiones con detalles
          for (var index = 0; index < detalles.getTreeBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getTreeBoxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getTireBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getTireBoxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock1Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock1Boxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock5Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock5Boxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock10Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock10Boxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          
          //Colisiones con escenario
          for (var index = 0; index < escenario.getColumnBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(escenario.getColumnBoxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < escenario.getParedBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(escenario.getParedBoxes()[index]))
            {
              crearExplosion();

              recorridoMisil = 0f;
            }
          }

          
      }
      else
      {
        MisilWorld = Matrix.CreateScale(0.05f, 0.05f, 0.05f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f, -10f, -10f);
        colliderMisil = OrientedBoundingBox.FromAABB(new BoundingBox(MisilPowerUP.Min - new Vector3(0f,300f,0f), MisilPowerUP.Max - new Vector3(0f,300f,0f)));
      }

      for (int i = 0; i < BalasWorld.Length; i++)
      {
        if (recorridoBalas[i] > 0f)
        {
          recorridoBalas[i] -= elapsedTime;
          balasPos[i] += BalasWorld[i].Up * 7500f * elapsedTime;
          BalasWorld[i] = Matrix.CreateScale(0.07f, 0.07f, 0.07f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(balasRot[i]) * Matrix.CreateTranslation(balasPos[i]) * Matrix.CreateTranslation(0f, 10f, 0f);
          collidersBalas[i] = OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min + balasPos[i] + (BalasWorld[i].Up * 40) - new Vector3(0f,30f,0f), BalasPowerUp.Max + balasPos[i] + (BalasWorld[i].Up * 40) - new Vector3(0f,50f,0f)));
          collidersBalas[i].Rotate(Matrix.CreateRotationZ(-cuartoDeVuelta));
          collidersBalas[i].Rotate(Matrix.CreateRotationY(balasRot[i]));

          //Colisiones con detalles
          for (var index = 0; index < detalles.getTreeBoxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(detalles.getTreeBoxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          for (var index = 0; index < detalles.getTireBoxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(detalles.getTireBoxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock1Boxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(detalles.getRock1Boxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock5Boxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(detalles.getRock5Boxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock10Boxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(detalles.getRock10Boxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          
          //Colisiones con escenario
          for (var index = 0; index < escenario.getColumnBoxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(escenario.getColumnBoxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }
          for (var index = 0; index < escenario.getParedBoxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(escenario.getParedBoxes()[index]))
            {
              recorridoBalas[i] = 0f;
            }
          }

        }
        else
        {
          BalasWorld[i] = Matrix.CreateScale(0.1f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateTranslation(-10f, -10f, -10f);
          collidersBalas[i] = OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min + balasPos[i] - new Vector3(0f,300f,0f), BalasPowerUp.Max + balasPos[i] - new Vector3(0f,300f,0f)));
        }
      }

      for (var index = 0; index < collidersAmetralladoras.Length; index++)
      {
        if (autoCollider.Intersects(collidersAmetralladoras[index]) && !collidedindexAmetralladora.Contains(index) && currentPowerUp == PowerUp.None)
        {
            collidedindexAmetralladora.Add(index);
            currentPowerUp = PowerUp.Ametralladora;
            powerUpTimer = 20f;
            ametralladoraCounter = 20;
        }
      }

      for (var index = 0; index < collidersMisiles.Length; index++)
      {
        if (autoCollider.Intersects(collidersMisiles[index]) && !collidedindexMisil.Contains(index) && currentPowerUp == PowerUp.None)
        {
            collidedindexMisil.Add(index);
            currentPowerUp = PowerUp.Misil;
        }
      }

      for (var index = 0; index < collidersTurbos.Length; index++)
      {
        if (autoCollider.Intersects(collidersTurbos[index]) && !collidedindexTurbo.Contains(index) && currentPowerUp == PowerUp.None)
        {
            collidedindexTurbo.Add(index);
            currentPowerUp = PowerUp.Turbo;
        }

        if (!tieneAmetalladora) {agarrarAmetralladora = 0;}
        if (!tieneLanzaCohetes) {agarrarLanzaCohetes = 0;}
        if (!tieneTurbo) {agarrarTurbo = 0;}

      }

      for(int index = 0; index < collidedindexAmetralladora.Count; index++)
      {
        if(TimersAmetralladoras[collidedindexAmetralladora[index]] < 20)
        {
          TimersAmetralladoras[collidedindexAmetralladora[index]] += elapsedTime;
        }
        else
        {
          TimersAmetralladoras[collidedindexAmetralladora[index]] = 0f;
          collidedindexAmetralladora.RemoveAt(index);
        }
      }

      for(int index = 0; index < collidedindexMisil.Count; index++)
      {
        if(TimersMisiles[collidedindexMisil[index]] < 20)
        {
          TimersMisiles[collidedindexMisil[index]] += elapsedTime;
        }
        else
        {
          TimersMisiles[collidedindexMisil[index]] = 0f;
          collidedindexMisil.RemoveAt(index);
        }
      }

      for(int index = 0; index < collidedindexTurbo.Count; index++)
      {
        if(TimersTurbos[collidedindexTurbo[index]] < 20)
        {
          TimersTurbos[collidedindexTurbo[index]] += elapsedTime;
        }
        else
        {
          TimersTurbos[collidedindexTurbo[index]] = 0f;
          collidedindexTurbo.RemoveAt(index);
        }
      }

      for(int i = 0; i < particulasExplosion.Count; i++)
      {
        if(particulasExplosion[i].viva()) //solo se actualizan aquellas particulas que sigan vivas
          particulasExplosion[i].Update(gameTime);
      }
        
    }

    public void dibujar(Matrix view, Matrix projection, Effect effect, Matrix matrizMundo, Model modelo, Color color)
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
      //effect.Parameters["DiffuseColor"].SetValue(color.ToVector3());
      effect.Parameters["ModelTexture"].SetValue(TexturaPowerUp);

      relativeMatrices = new Matrix[modelo.Bones.Count];
      modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);


      foreach (var mesh in modelo.Meshes)
      {
        effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
        effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Invert(Matrix.Transpose(matrizMundo)));
        mesh.Draw();
      }
    }

    public void dibujarPowerUps(Matrix view, Matrix projection, Effect effect, String tecnica, BoundingFrustum frustum)
    {
      //effect.CurrentTechnique = effect.Techniques["Luz"];
      effect.CurrentTechnique = effect.Techniques[tecnica];
      for (int index = 0; index < AmetralladorasWorld.Length; index++)
      {
        if (frustum.Intersects(collidersAmetralladoras[index]) && !collidedindexAmetralladora.Contains(index))
        {
          effect.Parameters["colorBloom"]?.SetValue(Color.Red.ToVector3());
          dibujar(view, projection, effect, AmetralladorasWorld[index], CajaAmetralladora, Color.Red);
        }
      }

      for (int index = 0; index < MisilesWorld.Length; index++)
      {
        if (frustum.Intersects(collidersMisiles[index]) && !collidedindexMisil.Contains(index))
        {
          effect.Parameters["colorBloom"]?.SetValue(Color.Blue.ToVector3());
          dibujar(view, projection, effect, MisilesWorld[index], CajaMisil, Color.Yellow);
        }
      }

      for (int index = 0; index < TurbosWorld.Length; index++)
      {
        if (frustum.Intersects(collidersTurbos[index]) && !collidedindexTurbo.Contains(index))
        {
          effect.Parameters["colorBloom"]?.SetValue(Color.Green.ToVector3());
          dibujar(view, projection, effect, TurbosWorld[index], CajaTurbo, Color.Blue);
        }
      }

      if (recorridoMisil > 0f)
        dibujar(view, projection, effect, MisilWorld, Misil, Color.Blue);

      for (int index = 0; index < BalasWorld.Length; index++)
      {
        if (recorridoBalas[index] > 0f)
        {
          dibujar(view, projection, effect, BalasWorld[index], Bala, Color.Blue);
        }
      }

    }

    public void dibujarBoundingBoxes(Gizmos gizmos)
    {
      for (int index = 0; index < collidersAmetralladoras.Length; index++)
      {
        gizmos.DrawCube((collidersAmetralladoras[index].Max + collidersAmetralladoras[index].Min) / 2f, collidersAmetralladoras[index].Max - collidersAmetralladoras[index].Min, Color.Red);
      }

      for (int index = 0; index < collidersMisiles.Length; index++)
      {
        gizmos.DrawCube((collidersMisiles[index].Max + collidersMisiles[index].Min) / 2f, collidersMisiles[index].Max - collidersMisiles[index].Min, Color.Red);
      }

      for (int index = 0; index < collidersTurbos.Length; index++)
      {
        gizmos.DrawCube((collidersTurbos[index].Max + collidersTurbos[index].Min) / 2f, collidersTurbos[index].Max - collidersTurbos[index].Min, Color.Red);
      }

      MisilBBPower = Matrix.CreateScale(colliderMisil.Extents ) * colliderMisil.Orientation * Matrix.CreateTranslation(colliderMisil.Center );
      gizmos.DrawCube(MisilBBPower , Color.Red);

      
      for (int index = 0; index < collidersBalas.Length; index++)
      {
        BalasBBPower = Matrix.CreateScale(collidersBalas[index].Extents ) * collidersBalas[index].Orientation * Matrix.CreateTranslation(collidersBalas[index].Center );
        gizmos.DrawCube(BalasBBPower , Color.Red);
      }
      
    }

    public void dispararMisil(Vector3 autoPrincipalPos, float rotation)
    {
      MisilWorld = Matrix.CreateScale(0.1f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(rotation * 2f - cuartoDeVuelta) * Matrix.CreateTranslation(autoPrincipalPos) * Matrix.CreateTranslation(0f, 10f, 0f);
      misilPos = autoPrincipalPos;
      misilRot = rotation * 2f - cuartoDeVuelta;
      recorridoMisil = 5f;
    }

    public void dispararBala(Vector3 autoPrincipalPos, float rotation, int indexBala)
    {
      BalasWorld[indexBala] = Matrix.CreateScale(0.1f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(rotation * 2f - cuartoDeVuelta) * Matrix.CreateTranslation(autoPrincipalPos) * Matrix.CreateTranslation(0f, 10f, 0f);
      balasPos[indexBala] = autoPrincipalPos;
      balasRot[indexBala] = rotation * 2f - cuartoDeVuelta;
      recorridoBalas[indexBala] = 5f;
    }

    public void iniciarPartida()
    {
      for(int i =0; i < TurbosWorld.Length; i++)
        TimersTurbos[i] = 0;
      
      for(int i =0; i < AmetralladorasWorld.Length; i++)
        TimersAmetralladoras[i] = 0;
            
      for(int i =0; i < MisilesWorld.Length; i++)
        TimersMisiles[i] = 0;

      currentPowerUp = PowerUp.None;
    }
    public bool getMisil(){
      return currentPowerUp == PowerUp.Misil;
    }
    public bool getAmetralaldora(){
      return currentPowerUp == PowerUp.Ametralladora;
    }
    public bool getTurbo(){
      return currentPowerUp == PowerUp.Turbo;
    }
    public bool getVacio(){
      return currentPowerUp == PowerUp.None;
    }
    public List<ParticulasExplosion> particulas(){
      return particulasExplosion;
    }
    

    public void crearExplosion(){
      Instance = ExplosionMisil.CreateInstance();
      Instance.Play();
      
      Random random = new Random();

      for(int i = 0; i < CantidadParticulas; i++)
      {

        Vector3 velocidad = new Vector3((float) MathF.Cos(MathHelper.ToRadians(random.Next(360))) * random.Next(5,15),(float) MathF.Sin(MathHelper.ToRadians(random.Next(360))) * random.Next(10,20),(float) MathF.Cos(MathHelper.ToRadians(random.Next(360))) * random.Next(5,15));
      
        

        float sizeRandom = (float)random.NextDouble() * 2f; //entre 0 y 2
        float tiempoVidaRandom = (float)random.NextDouble() * 1.5f; //entre 0 y 1.5 
        Vector3 posicionExplosion = misilPos + MisilWorld.Up * 400; //para que la explosion ocurra lo mas cercano al punto de colision
        particulasExplosion[i] =  new ParticulasExplosion(posicionExplosion,velocidad,sizeRandom,tiempoVidaRandom);  
        //se reemplazan las "viejas" particulas por las "nuevas"
      }

    }
  }
}
