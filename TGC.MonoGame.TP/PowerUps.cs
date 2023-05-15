using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.Samples.Geometries;

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
    private Matrix PowerTurboWorld { get; set; }
    private Matrix PowerTurboWorld1 { get; set; }
    private Matrix PowerTurboWorld2 { get; set; }
    private Matrix PowerTurboWorld3 { get; set; }
    private Matrix PowerTurboWorld4 { get; set; }

    private Matrix PowerAmetralladoraWorld { get; set; }
    private Matrix PowerAmetralladoraWorld1 { get; set; }
    private Matrix PowerAmetralladoraWorld2 { get; set; }
    private Matrix PowerAmetralladoraWorld3 { get; set; }
    private Matrix PowerAmetralladoraWorld4 { get; set; }

    private Matrix PowerMisilWorld { get; set; }
    private Matrix PowerMisilWorld1 { get; set; }
    private Matrix PowerMisilWorld2 { get; set; }
    private Matrix PowerMisilWorld3 { get; set; }
    private Matrix PowerMisilWorld4 { get; set; }

    // Colliders
    private BoundingBox[] collidersAmetralladoras;
    private BoundingBox[] collidersTurbos;
    private BoundingBox[] collidersMisiles;
    private CubePrimitive cubo1;
    private Matrix cubo1World;
    private BoundingBox colliderCubo1;

    private CubePrimitive cubo2;
    private Matrix cubo2World;
    private BoundingBox colliderCubo2;

    //Bones
    private Matrix[] relativeMatrices;

    // Variables
    private float mediaVuelta = MathF.PI;
    private float cuartoDeVuelta = MathF.PI / 2;
    private GraphicsDevice gD;

    public void Initialize(GraphicsDevice graphicsDevice)
    {
      // PowerAmetralladora
      PowerAmetralladoraWorld = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(70, 90, -390);
      PowerAmetralladoraWorld1 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 55, 100);
      PowerAmetralladoraWorld2 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 115, 235);
      PowerAmetralladoraWorld3 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(210, 35, 300);
      PowerAmetralladoraWorld4 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-435, 35, -195);

      // PowerMisil
      PowerMisilWorld = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(70, 90, -370);
      PowerMisilWorld1 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 55, 80);
      PowerMisilWorld2 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 115, 215);
      PowerMisilWorld3 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(210, 35, 280);
      PowerMisilWorld4 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-435, 35, -175);

      // PowerTurbo
      PowerTurboWorld = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(70, 90, -410);
      PowerTurboWorld1 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 55, 120);
      PowerTurboWorld2 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-250, 115, 255);
      PowerTurboWorld3 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(210, 35, 320);
      PowerTurboWorld4 = Matrix.CreateScale(0.2f, 0.1f, 0.1f) * Matrix.CreateRotationZ(-mediaVuelta / 2) * Matrix.CreateTranslation(-435, 35, -215);

      // Colliders
      gD = graphicsDevice; //Solo para probar lo de cambiar el color al cubo1 en Update

      cubo1 = new CubePrimitive(graphicsDevice, 5, Color.Aqua);
      cubo1World = Matrix.CreateScale(5f, 5f, 5f) * Matrix.CreateTranslation(0, 0, 0);
      colliderCubo1 = crearBoxCollider(cubo1World);

      cubo2 = new CubePrimitive(graphicsDevice, 5, Color.Red);
      cubo2World = Matrix.CreateScale(5f, 5f, 5f) * Matrix.CreateTranslation(-20f, 0, 0);
      colliderCubo2 = crearBoxCollider(cubo2World);

      collidersAmetralladoras = new BoundingBox[]
      {
      };

      collidersTurbos = new BoundingBox[]
      {
      };

      collidersMisiles = new BoundingBox[]
      {
      };
    }

    public void LoadContent(Model cajaAmetralladora, Model cajaMisil, Model cajaTurbo)
    {
      CajaAmetralladora = cajaAmetralladora;
      CajaMisil = cajaMisil;
      CajaTurbo = cajaTurbo;
    }

    public void Update(GameTime gameTime, OrientedBoundingBox autoCollider)
    {
      if (colliderCubo1.Intersects(colliderCubo2))
      {
        cubo1 = new CubePrimitive(gD, 5, Color.Blue);
      }

      if (autoCollider.Intersects(colliderCubo2)) // No anda
      {
        cubo1 = new CubePrimitive(gD, 5, Color.Green);
      }

      for (var index = 0; index < collidersAmetralladoras.Length; index++)
      {
        if (autoCollider.Intersects(collidersAmetralladoras[index]))
        {
        }
      }

      for (var index = 0; index < collidersTurbos.Length; index++)
      {
        if (autoCollider.Intersects(collidersTurbos[index]))
        {
        }
      }

      for (var index = 0; index < collidersMisiles.Length; index++)
      {
        if (autoCollider.Intersects(collidersMisiles[index]))
        {
        }
      }
    }

    public BoundingBox crearBoxCollider(Matrix worldMatrix)
    {
      BoundingBox colliderCubo = BoundingVolumesExtensions.FromMatrix(worldMatrix);
      colliderCubo = BoundingVolumesExtensions.Scale(colliderCubo, new Vector3(5, 5, 5));
      return colliderCubo;
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
      // Los neumáticos azules son los turbos
      dibujar(view, projection, effect, PowerTurboWorld, CajaTurbo, Color.Blue);
      dibujar(view, projection, effect, PowerTurboWorld1, CajaTurbo, Color.Blue);
      dibujar(view, projection, effect, PowerTurboWorld2, CajaTurbo, Color.Blue);
      dibujar(view, projection, effect, PowerTurboWorld3, CajaTurbo, Color.Blue);
      dibujar(view, projection, effect, PowerTurboWorld4, CajaTurbo, Color.Blue);
      // Los neumáticos rojos son las ametralladoras
      dibujar(view, projection, effect, PowerAmetralladoraWorld, CajaAmetralladora, Color.Red);
      dibujar(view, projection, effect, PowerAmetralladoraWorld1, CajaAmetralladora, Color.Red);
      dibujar(view, projection, effect, PowerAmetralladoraWorld2, CajaAmetralladora, Color.Red);
      dibujar(view, projection, effect, PowerAmetralladoraWorld3, CajaAmetralladora, Color.Red);
      dibujar(view, projection, effect, PowerAmetralladoraWorld4, CajaAmetralladora, Color.Red);
      // Los neumáticos amarillos son los misiles
      dibujar(view, projection, effect, PowerMisilWorld, CajaMisil, Color.Yellow);
      dibujar(view, projection, effect, PowerMisilWorld1, CajaMisil, Color.Yellow);
      dibujar(view, projection, effect, PowerMisilWorld2, CajaMisil, Color.Yellow);
      dibujar(view, projection, effect, PowerMisilWorld3, CajaMisil, Color.Yellow);
      dibujar(view, projection, effect, PowerMisilWorld4, CajaMisil, Color.Yellow);

      cubo1.Draw(cubo1World, view, projection);
      cubo2.Draw(cubo2World, view, projection);
    }
  }
}
