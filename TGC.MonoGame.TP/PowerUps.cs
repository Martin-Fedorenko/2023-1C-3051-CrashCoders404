using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

    // Matrices
    private Matrix[] AmetralladorasWorld;
    private Matrix[] TurbosWorld;
    private Matrix[] MisilesWorld;

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
    private BoundingBox PowerUpBox;
    private List<int> collidedindexAmetralladora;
    private List<int> collidedindexMisil;
    private List<int> collidedindexTurbo;
    //Bones
    private Matrix[] relativeMatrices;

    // Variables
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
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(TurboPos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Turbo1Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Turbo2Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(Turbo3Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(Turbo4Pos),
      Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(Turbo5Pos)
      };
    }

    public void LoadContent(Model cajaAmetralladora, Model cajaMisil, Model cajaTurbo)
    {
      CajaAmetralladora = cajaAmetralladora;
      CajaMisil = cajaMisil;
      CajaTurbo = cajaTurbo;

      //Collisions
      PowerUpBox = BoundingVolumesExtensions.CreateAABBFrom(CajaAmetralladora);
      PowerUpBox = BoundingVolumesExtensions.Scale(PowerUpBox,new Vector3(0.1f, 3.5f, 0.1f));
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

    }

    public void Update(GameTime gameTime, OrientedBoundingBox autoCollider)
    {
      var Rotation = (float)gameTime.ElapsedGameTime.TotalSeconds;

      for(int index = 0 ; index < AmetralladorasWorld.Length; index++)
      {
          AmetralladorasWorld[index] = Matrix.CreateRotationX(Rotation*1.2f) * AmetralladorasWorld[index];
      }
      for(int index = 0 ; index < MisilesWorld.Length; index++)
      {
          MisilesWorld[index] = Matrix.CreateRotationX(Rotation*1.2f) * MisilesWorld[index];
      }
      for(int index = 0 ; index < TurbosWorld.Length; index++)
      {
          TurbosWorld[index] = Matrix.CreateRotationX(Rotation*1.2f) * TurbosWorld[index];
      }

      for (var index = 0; index < collidersAmetralladoras.Length; index++)
      {
        if (autoCollider.Intersects(collidersAmetralladoras[index]))
        {
          collidedindexAmetralladora.Add(index);
        }
      }
      
      for (var index = 0; index < collidersMisiles.Length; index++)
      {
        if (autoCollider.Intersects(collidersMisiles[index]))
        {
          collidedindexMisil.Add(index);
        }
      }

      for (var index = 0; index < collidersTurbos.Length; index++)
      {
        if (autoCollider.Intersects(collidersTurbos[index]))
        {
          collidedindexTurbo.Add(index);
        }
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

      for(int index = 0; index < AmetralladorasWorld.Length; index++)
      {
        if(!collidedindexAmetralladora.Contains(index))
        {
          dibujar(view, projection, effect, AmetralladorasWorld[index], CajaAmetralladora, Color.Red);
        }
      }
      for(int index = 0; index < MisilesWorld.Length; index++)
      {
        if(!collidedindexMisil.Contains(index))
        {
          dibujar(view, projection, effect, MisilesWorld[index], CajaMisil, Color.Yellow);
        }
      }
      for(int index = 0; index < TurbosWorld.Length; index++)
      {
        if(!collidedindexTurbo.Contains(index))
        {
          dibujar(view, projection, effect, TurbosWorld[index], CajaTurbo, Color.Blue);
        }
      }

    }

    public void dibujarBoundingBoxes(Gizmos gizmos)
    {
      for(int index = 0; index < collidersAmetralladoras.Length; index++)
        {
          gizmos.DrawCube((collidersAmetralladoras[index].Max + collidersAmetralladoras[index].Min) / 2f,collidersAmetralladoras[index].Max - collidersAmetralladoras[index].Min,Color.Red);
        }

        for(int index = 0; index < collidersMisiles.Length; index++)
        {
          gizmos.DrawCube((collidersMisiles[index].Max + collidersMisiles[index].Min) / 2f,collidersMisiles[index].Max - collidersMisiles[index].Min,Color.Red);
        }
        
        for(int index = 0; index < collidersTurbos.Length; index++)
        {
          gizmos.DrawCube((collidersTurbos[index].Max + collidersTurbos[index].Min) / 2f,collidersTurbos[index].Max - collidersTurbos[index].Min,Color.Red);
        }
    }
  }
}
