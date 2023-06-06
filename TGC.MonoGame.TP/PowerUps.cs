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

    // Matrices
    private Matrix[] AmetralladorasWorld;
    private Matrix[] TurbosWorld;
    private Matrix[] MisilesWorld;
    private Matrix MisilWorld;
    private Matrix[] BalasWorld;

    //Sonido  
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect BulletSound { get; set; }
    private SoundEffect PickUpGunSound { get; set; }
    private SoundEffect RocketSound { get; set; }
    private SoundEffect PickUpRocketSound { get; set; }
    private SoundEffect BoostSound { get; set; }

    private int agarrarAmetralladora = 0;
    private bool tieneAmetalladora;
    private int agarrarLanzaCohetes = 0;
    private bool tieneLanzaCohetes;
    private int agarrarTurbo = 0;
    private bool tieneTurbo;

    //Posiciones
    private Vector3 AmetralladoraPos = new Vector3(70, 90, -390);
    private Vector3 Ametralladora1Pos = new Vector3(0, 20, 650);
    private Vector3 Ametralladora2Pos = new Vector3(-210, 115, 210);
    private Vector3 Ametralladora3Pos = new Vector3(210, 35, 300);
    private Vector3 Ametralladora4Pos = new Vector3(-435, 35, -195);
    private Vector3 Ametralladora5Pos = new Vector3(600, 20, 80);

    private Vector3 MisilPos = new Vector3(70, 90, -370);
    private Vector3 Misil1Pos = new Vector3(-250, 55, 80);
    private Vector3 Misil2Pos = new Vector3(-510, 20, 570);
    private Vector3 Misil3Pos = new Vector3(210, 35, 280);
    private Vector3 Misil4Pos = new Vector3(-435, 35, -175);
    private Vector3 Misil5Pos = new Vector3(490, 20, -350);

    private Vector3 TurboPos = new Vector3(70, 90, -410);
    private Vector3 Turbo1Pos = new Vector3(-230, 27, -40);
    private Vector3 Turbo2Pos = new Vector3(-310, 115, 290);
    private Vector3 Turbo3Pos = new Vector3(210, 35, 320);
    private Vector3 Turbo4Pos = new Vector3(-435, 35, -215);
    private Vector3 Turbo5Pos = new Vector3(90, 20, -90);

    // Colliders
    private BoundingBox[] collidersAmetralladoras;
    private BoundingBox[] collidersTurbos;
    private BoundingBox[] collidersMisiles;
    private BoundingBox MisilPowerUP;
    private Matrix MisilBBPower;
    private OrientedBoundingBox colliderMisil;
    private BoundingBox BalasPowerUp;
    private OrientedBoundingBox[] collidersBalas;
    private Matrix BalasBBPower;
    private BoundingBox PowerUpBox;
    private List<int> collidedindexAmetralladora;
    private List<int> collidedindexMisil;
    private List<int> collidedindexTurbo;
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
    private float recorridoMisil = 0f;
    private float[] recorridoBalas = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
    private Vector3 misilPos = new Vector3(0, 0, 0);
    private Vector3[] balasPos;
    private float misilRot = 0f;
    private float[] balasRot = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
    private float mediaVuelta = MathF.PI;
    private float cuartoDeVuelta = MathF.PI / 2;

    public void Initialize(GraphicsDevice graphicsDevice)
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
    }


    public void LoadContent(Model cajaAmetralladora, Model cajaMisil, Model cajaTurbo, Model misil, Model bala, 
                            SoundEffect bulletSound, SoundEffect pickUpGunSound, SoundEffect pickUpRocketSound, SoundEffect rocketSound, 
                            SoundEffect boostSound)
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
      MisilPowerUP = BoundingVolumesExtensions.Scale(MisilPowerUP, new Vector3(.1f, .1f, .1f));
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
      var Rotation = (float)gameTime.ElapsedGameTime.TotalSeconds;
      OrientedBoundingBox autoCollider = autos.GetAutoPrincipalBox();

      tieneAmetalladora = false;
      tieneLanzaCohetes = false;
      tieneTurbo = false;

      for (int index = 0; index < AmetralladorasWorld.Length; index++)
      {
        AmetralladorasWorld[index] = Matrix.CreateRotationX(Rotation * 1.2f) * AmetralladorasWorld[index];
      }
      for (int index = 0; index < MisilesWorld.Length; index++)
      {
        MisilesWorld[index] = Matrix.CreateRotationX(Rotation * 1.2f) * MisilesWorld[index];
      }
      for (int index = 0; index < TurbosWorld.Length; index++)
      {
        TurbosWorld[index] = Matrix.CreateRotationX(Rotation * 1.2f) * TurbosWorld[index];
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
          Instance = PickUpGunSound.CreateInstance();
          Instance.Play();
          agarrarAmetralladora = 1;
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
                dispararBala(autos.AutoPrincipalPos, autos.Rotation, ametralladoraCounter);
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
          dispararMisil(autos.AutoPrincipalPos, autos.Rotation);
          Instance = PickUpRocketSound.CreateInstance();
          Instance.Play();
          currentPowerUp = PowerUp.None;
        }
      }

      if (recorridoMisil > 0f)
      {
        recorridoMisil -= elapsedTime;
        misilPos += MisilWorld.Up * 750f * elapsedTime;
        MisilWorld = Matrix.CreateScale(0.05f, 0.05f, 0.05f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(misilRot) * Matrix.CreateTranslation(misilPos) * Matrix.CreateTranslation(0f, 10f, 0f);
        colliderMisil = OrientedBoundingBox.FromAABB(new BoundingBox(MisilPowerUP.Min + misilPos + (MisilWorld.Up * 150)  - new Vector3(0f,190f,0f), MisilPowerUP.Max + misilPos + (MisilWorld.Up * 150) - new Vector3(0f,190f,0f)));
        colliderMisil.Rotate(Matrix.CreateRotationY(misilRot));

        //Colisiones con otros auto
        for (var index = 0; index < autos.getPosAutos().Length; index++)
          {
            if(colliderMisil.Intersects(autos.getPosAutos()[index]))
            {
              recorridoMisil = 0f;
              autos.getVidaAutos()[index] -= 50;
            }
          }

          //Colisiones con detalles
          for (var index = 0; index < detalles.getTreeBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getTreeBoxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getTireBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getTireBoxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock1Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock1Boxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock5Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock5Boxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < detalles.getRock10Boxes().Length; index++)
          {
            if(colliderMisil.Intersects(detalles.getRock10Boxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          
          //Colisiones con escenario
          for (var index = 0; index < escenario.getColumnBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(escenario.getColumnBoxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < escenario.getParedBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(escenario.getParedBoxes()[index]))
            {
              recorridoMisil = 0f;
            }
          }
          for (var index = 0; index < escenario.getRampBoxes().Length; index++)
          {
            if(colliderMisil.Intersects(escenario.getRampBoxes()[index]))
            {
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
          balasPos[i] += BalasWorld[i].Up * 750f * elapsedTime;
          BalasWorld[i] = Matrix.CreateScale(0.07f, 0.07f, 0.07f) * Matrix.CreateRotationZ(-cuartoDeVuelta) * Matrix.CreateRotationY(balasRot[i]) * Matrix.CreateTranslation(balasPos[i]) * Matrix.CreateTranslation(0f, 10f, 0f);
          collidersBalas[i] = OrientedBoundingBox.FromAABB(new BoundingBox(BalasPowerUp.Min + balasPos[i] + (BalasWorld[i].Up * 40) - new Vector3(0f,30f,0f), BalasPowerUp.Max + balasPos[i] + (BalasWorld[i].Up * 40) - new Vector3(0f,50f,0f)));
          collidersBalas[i].Rotate(Matrix.CreateRotationZ(-cuartoDeVuelta));
          collidersBalas[i].Rotate(Matrix.CreateRotationY(balasRot[i]));

          //Colisiones con otros auto
          for (var index = 0; index < autos.getPosAutos().Length; index++)
          {
            if(collidersBalas[i].Intersects(autos.getPosAutos()[index]))
            {
              recorridoBalas[i] = 0f;
              autos.getVidaAutos()[index] -= 10;
            }
          }

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
          for (var index = 0; index < escenario.getRampBoxes().Length; index++)
          {
            if(collidersBalas[i].Intersects(escenario.getRampBoxes()[index]))
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
        if (autoCollider.Intersects(collidersAmetralladoras[index]) && !collidedindexAmetralladora.Contains(index))
        {
          if (currentPowerUp == PowerUp.None)
          {
            collidedindexAmetralladora.Add(index);
            currentPowerUp = PowerUp.Ametralladora;
            powerUpTimer = 20f;
            ametralladoraCounter = 20;
          }
        }
      }

      for (var index = 0; index < collidersMisiles.Length; index++)
      {
        if (autoCollider.Intersects(collidersMisiles[index]) && !collidedindexMisil.Contains(index))
        {
          if (currentPowerUp == PowerUp.None)
          {
            collidedindexMisil.Add(index);
            currentPowerUp = PowerUp.Misil;
          }
        }
      }

      for (var index = 0; index < collidersTurbos.Length; index++)
      {
        if (autoCollider.Intersects(collidersTurbos[index]) && !collidedindexTurbo.Contains(index))
        {
          if (currentPowerUp == PowerUp.None)
          {
            collidedindexTurbo.Add(index);
            currentPowerUp = PowerUp.Turbo;
          }
        }

        if (!tieneAmetalladora) {agarrarAmetralladora = 0;}
        if (!tieneLanzaCohetes) {agarrarLanzaCohetes = 0;}
        if (!tieneTurbo) {agarrarTurbo = 0;}

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
      effect.Parameters["DiffuseColor"].SetValue(color.ToVector3());

      relativeMatrices = new Matrix[modelo.Bones.Count];
      modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);


      foreach (var mesh in modelo.Meshes)
      {
        effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
        mesh.Draw();
      }
    }

    public void dibujarPowerUps(Matrix view, Matrix projection, Effect effect)
    {

      for (int index = 0; index < AmetralladorasWorld.Length; index++)
      {
        if (!collidedindexAmetralladora.Contains(index))
        {
          dibujar(view, projection, effect, AmetralladorasWorld[index], CajaAmetralladora, Color.Red);
        }
      }

      for (int index = 0; index < MisilesWorld.Length; index++)
      {
        if (!collidedindexMisil.Contains(index))
        {
          dibujar(view, projection, effect, MisilesWorld[index], CajaMisil, Color.Yellow);
        }
      }

      for (int index = 0; index < TurbosWorld.Length; index++)
      {
        if (!collidedindexTurbo.Contains(index))
        {
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

    public String powerUpActual(){
      return currentPowerUp.ToString();
    }
  }
}
