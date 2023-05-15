using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;

namespace TGC.MonoGame.TP
{
  public class Escenario
  {
    public Escenario() { }
    private Model Piso { get; set; }
    private Model Pared { get; set; }
    private Model Column { get; set; }
    private Model Ramp { get; set; }
    private Model Platform { get; set; }
    private Model Cube { get; set; }


    private Matrix PisoWorld { get; set; }
    private Matrix ParedWorld { get; set; }
    private Matrix[] PlatformWorld { get; set; }
    private Matrix[] RampWorld { get; set; }
  private Matrix[] ColumnWorld { get; set; }

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

     public Vector3  PisoPosition = new Vector3(30, 0, 30);
     public Vector3  ParedPosition = new Vector3(30, 50, 30);
     public Vector3  Platform1Position = new Vector3(70, 64, -390);
     public Vector3  Platform2Position = new Vector3(-250, 30, 100);
     public Vector3  Platform3Position = new Vector3(-250, 93, 235);
     public Vector3  Platform4Position = new Vector3(210, 0, 300);
     public Vector3  Platform5Position = new Vector3(-435, 0, -195);
     public Vector3  Column1Position = new Vector3(0, 0, -450);
     public Vector3  Column2Position = new Vector3(0, 0, -350);
     public Vector3  Column3Position = new Vector3(140, 0, -450);
     public Vector3  Column4Position = new Vector3(140, 0, -350);
     public Vector3  Column5Position = new Vector3(-170, 0, 50);
     public Vector3  Column6Position = new Vector3(-330, 0, 50);
     public Vector3  Column7Position = new Vector3(-170, 0, 175);
     public Vector3  Column8Position = new Vector3(-330, 0, 175);
     public Vector3  Column9Position = new Vector3(-170, 0, 290);
     public Vector3  Column10Position = new Vector3(-330, 0, 290);
     public Vector3  Column11Position = new Vector3(-500, 0, 0);
     public Vector3  Column12Position = new Vector3(436, 0, -80);
     public Vector3  BrokenColumn1Position = new Vector3(450, 0, 250);
     public Vector3  BrokenColumn2Position = new Vector3(-230, 0, -290);

     public Vector3  Ramp1Position = new Vector3(-80, 0, -380);
     public Vector3  Ramp2Position = new Vector3(230, 0, -395);
     public Vector3  Ramp3Position = new Vector3(-250, 0, -10);
     public Vector3  Ramp4Position = new Vector3(-300, 35, 130);
     public Vector3  Ramp5Position = new Vector3(185, 0, 243);     
     public Vector3  Ramp6Position = new Vector3(235, 0, 359);
     public Vector3  Ramp7Position = new Vector3(155, 0, 325);
     public Vector3  Ramp8Position = new Vector3(265, 0, 274);
     public Vector3  Ramp9Position = new Vector3(-461, 0, -254);
     public Vector3  Ramp10Position = new Vector3(-410, 0, -136);
     public Vector3  Ramp11Position = new Vector3(-495, 0, -169);
     public Vector3  Ramp12Position = new Vector3(-376, 0, -221);


    // Variables
    private float mediaVuelta = MathF.PI;
    private float cuartoDeVuelta = MathF.PI / 2;

    //Bones
    private Matrix[] relativeMatrices;

    //Colisiones
    private BoundingBox ColumnBox;
    private BoundingBox PlatformBox;
    private BoundingBox[] ColumnBoxes;
    private BoundingBox[] PlatformBoxes;
    private BoundingBox[] RampBoxes;

    public void Initialize()
    {
      //Arena
      PisoWorld = Matrix.CreateScale(30, 0, 30);
      ParedWorld = Matrix.CreateScale(30, 50, 30);

      //Plataformas
      ColumnWorld = new Matrix[]
      {
        Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column1Position),
        Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column2Position),
        Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column3Position),
        Matrix.CreateScale(0.35f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column4Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column5Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.15f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column6Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column7Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column8Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column9Position),
        Matrix.CreateScale(0.35f, 0.35f, 0.5f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column10Position),
        Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column11Position),
        Matrix.CreateScale(0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Column12Position),

        Matrix.CreateScale(0.6f) * Matrix.CreateRotationY(-MathF.PI / 6) * Matrix.CreateTranslation(BrokenColumn1Position),
        Matrix.CreateScale(0.7f) * Matrix.CreateRotationY(MathF.PI / 6) * Matrix.CreateTranslation(BrokenColumn2Position),
      };


      PlatformWorld = new Matrix[]
      {
        Matrix.CreateScale(100, 5, 80) * Matrix.CreateTranslation(Platform1Position),
        Matrix.CreateScale(100, 5, 80) * Matrix.CreateTranslation(Platform2Position),
        Matrix.CreateScale(100, 5, 70) * Matrix.CreateTranslation(Platform3Position),
        Matrix.CreateScale(50, 10, 50) * Matrix.CreateTranslation(Platform4Position),
        Matrix.CreateScale(50, 10, 50) * Matrix.CreateTranslation(Platform5Position)
      };
     

      RampWorld = new Matrix[]
      {
      Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp1Position),
        Matrix.CreateScale(0.45f, 0.35f, 0.65f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Ramp2Position),
        Matrix.CreateScale(0.25f, 0.35f, 0.3f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp3Position),
        Matrix.CreateScale(0.4f, 0.15f, 0.6f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp4Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp5Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Ramp6Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp7Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Ramp8Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp9Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Ramp10Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateTranslation(Ramp11Position),
        Matrix.CreateScale(0.1f, 0.52f, 0.1f) * Matrix.CreateRotationX(-cuartoDeVuelta) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Ramp12Position)
      };
    }


    public void LoadContent(Model piso, Model pared, Model column, Model ramp, Model platform, Model cube)
    {
      Piso = piso;
      Pared = pared;
      Column = column;
      Ramp = ramp;
      Platform = platform;
      Cube = cube;


      Vector3 correctorPosicionBoxColumnas = new Vector3(0f,0f,0f);
        
      ColumnBox = BoundingVolumesExtensions.CreateAABBFrom(Column);
      ColumnBox = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.01f,0.5f,0.01f));

      ColumnBoxes = new BoundingBox[]
      {
        new BoundingBox(ColumnBox.Min  , ColumnBox.Max  ),
        new BoundingBox(ColumnBox.Min + Column1Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column1Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column2Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column2Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column3Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column3Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column4Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column4Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column5Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column5Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column6Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column6Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column7Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column7Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column8Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column8Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column9Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column9Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column10Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column10Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column11Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column11Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + Column12Position -correctorPosicionBoxColumnas, ColumnBox.Max + Column12Position - correctorPosicionBoxColumnas),

        new BoundingBox(ColumnBox.Min + BrokenColumn1Position -correctorPosicionBoxColumnas, ColumnBox.Max + BrokenColumn1Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox.Min + BrokenColumn2Position -correctorPosicionBoxColumnas, ColumnBox.Max + BrokenColumn2Position - correctorPosicionBoxColumnas)
      };


      Vector3 correctorPosicionBoxPlataformas = new Vector3(0f,0f,0f);
        
      PlatformBox = BoundingVolumesExtensions.CreateAABBFrom(Platform);
      PlatformBox = BoundingVolumesExtensions.Scale(PlatformBox,new Vector3(0.01f,0.5f,0.01f));

      PlatformBoxes = new BoundingBox[]
      {
        new BoundingBox(ColumnBox.Min  , ColumnBox.Max  ),
        new BoundingBox(ColumnBox.Min + Platform1Position -correctorPosicionBoxPlataformas, ColumnBox.Max + Platform1Position - correctorPosicionBoxPlataformas),
        new BoundingBox(ColumnBox.Min + Platform2Position -correctorPosicionBoxPlataformas, ColumnBox.Max + Platform2Position - correctorPosicionBoxPlataformas),
        new BoundingBox(ColumnBox.Min + Platform3Position -correctorPosicionBoxPlataformas, ColumnBox.Max + Platform3Position - correctorPosicionBoxPlataformas),
        new BoundingBox(ColumnBox.Min + Platform4Position -correctorPosicionBoxPlataformas, ColumnBox.Max + Platform4Position - correctorPosicionBoxPlataformas),
        new BoundingBox(ColumnBox.Min + Platform5Position -correctorPosicionBoxPlataformas, ColumnBox.Max + Platform5Position - correctorPosicionBoxPlataformas)
        

      };
    }

    public Boolean DetectorDeColisionesDeEscenario(GameTime gameTime, OrientedBoundingBox autoCollider)
        {
            for(int index = 0; index < ColumnBoxes.Length; index++)
            {
                if(autoCollider.Intersects(ColumnBoxes[index]))
                {
                    return true;
                }
            }

            
            return false;

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

    public void dibujarEscenario(Matrix view, Matrix projection, Effect effect)
    {
      //Arena
      dibujar(view, projection, effect, PisoWorld, Piso, Color.LightGoldenrodYellow);
      dibujar(view, projection, effect, ParedWorld, Pared, Color.Wheat);

      //Plataformas
      dibujar(view, projection, effect, Column1World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column2World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column3World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column4World, Column, Color.SandyBrown);

      dibujar(view, projection, effect, Ramp1World, Ramp, Color.Gray);
      dibujar(view, projection, effect, Ramp2World, Ramp, Color.Gray);

      dibujar(view, projection, effect, Platform1World, Platform, Color.DarkSalmon);

      dibujar(view, projection, effect, Column5World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column6World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column7World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column8World, Column, Color.SandyBrown);

      dibujar(view, projection, effect, Platform2World, Platform, Color.DarkSalmon);

      dibujar(view, projection, effect, Column9World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column10World, Column, Color.SandyBrown);

      dibujar(view, projection, effect, Ramp3World, Ramp, Color.Gray);
      dibujar(view, projection, effect, Ramp4World, Ramp, Color.Gray);

      dibujar(view, projection, effect, Platform3World, Platform, Color.DarkSalmon);

      dibujar(view, projection, effect, Ramp5World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp6World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp7World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp8World, Ramp, Color.DarkGray);

      dibujar(view, projection, effect, Platform4World, Platform, Color.DarkGray);

      dibujar(view, projection, effect, Ramp9World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp10World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp11World, Ramp, Color.DarkGray);
      dibujar(view, projection, effect, Ramp12World, Ramp, Color.DarkGray);

      dibujar(view, projection, effect, Platform5World, Platform, Color.DarkGray);

      //Columnas Individuales
      dibujar(view, projection, effect, BrokenColumn1World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, BrokenColumn2World, Column, Color.SandyBrown);

      dibujar(view, projection, effect, Column11World, Column, Color.SandyBrown);
      dibujar(view, projection, effect, Column12World, Column, Color.SandyBrown);
    }
  }
}
