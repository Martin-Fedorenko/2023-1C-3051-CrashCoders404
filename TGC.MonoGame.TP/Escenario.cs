using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.Viewer.Gizmos;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP
{
  public class Escenario
  {
    public Escenario() { }
    //private Model Piso { get; set; }
    private QuadPrimitive Piso { get; set; }
    private Model Pared { get; set; }
    private Model Column { get; set; }
    private Model Ramp { get; set; }
    private Model Platform { get; set; }


    private Matrix PisoWorld { get; set; }
    private Matrix[] ParedWorld { get; set; }
    private Matrix[] PlatformWorld { get; set; }
    private Matrix[] RampWorld { get; set; }
    private Matrix[] ColumnWorld { get; set; }
    private Matrix[] BrokenColumnWorld { get; set; }

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

    private Vector3  PisoPosition = new Vector3(30, 0, 30);
    private Vector3  ParedPosition = new Vector3(30, 50, 30);
    private Vector3  Platform1Position = new Vector3(70, 64, -390);
    private Vector3  Platform2Position = new Vector3(-250, 30, 100);
    private Vector3  Platform3Position = new Vector3(-250, 93, 235);
    private Vector3  Platform4Position = new Vector3(210, 0, 300);
    private Vector3  Platform5Position = new Vector3(-435, 0, -195);
    private Vector3  Column1Position = new Vector3(0, 0, -450);
    private Vector3  Column2Position = new Vector3(0, 0, -350);
    private Vector3  Column3Position = new Vector3(140, 0, -450);
    private Vector3  Column4Position = new Vector3(140, 0, -350);
    private Vector3  Column5Position = new Vector3(-170, 0, 50);
    private Vector3  Column6Position = new Vector3(-330, 0, 50);
    private Vector3  Column7Position = new Vector3(-170, 0, 175);
    private Vector3  Column8Position = new Vector3(-330, 0, 175);
    private Vector3  Column9Position = new Vector3(-170, 0, 290);
    private Vector3  Column10Position = new Vector3(-330, 0, 290);
    private Vector3  Column11Position = new Vector3(-500, 0, 0);
    private Vector3  Column12Position = new Vector3(436, 0, -80);
    private Vector3  BrokenColumn1Position = new Vector3(450, 0, 250);
    private Vector3  BrokenColumn2Position = new Vector3(-230, 0, -270);
    private Vector3  Ramp1Position = new Vector3(-80, 0, -380);
    private Vector3  Ramp2Position = new Vector3(230, 0, -395);
    private Vector3  Ramp3Position = new Vector3(-250, 0, -10);
    private Vector3  Ramp4Position = new Vector3(-300, 35, 130);
    private Vector3  Ramp5Position = new Vector3(185, 0, 243);     
    private Vector3  Ramp6Position = new Vector3(235, 0, 359);
    private Vector3  Ramp7Position = new Vector3(155, 0, 325);
    private Vector3  Ramp8Position = new Vector3(265, 0, 274);
    private Vector3  Ramp9Position = new Vector3(-461, 0, -254);
    private Vector3  Ramp10Position = new Vector3(-410, 0, -136);
    private Vector3  Ramp11Position = new Vector3(-495, 0, -169);
    private Vector3  Ramp12Position = new Vector3(-376, 0, -221);

    Vector3 vectorChoque = Vector3.Zero;
    float penetration = 0f;


    // Variables
    private float mediaVuelta = MathF.PI;
    private float cuartoDeVuelta = MathF.PI / 2;

    //Bones
    private Matrix[] relativeMatrices;

    //Colisiones
    private BoundingBox ColumnBox;
    private BoundingBox ColumnBox1;
    private BoundingBox ColumnBox2;
    private BoundingBox ColumnBox3;
    private BoundingBox ColumnBox4;
    private BoundingBox BrokenColumnAABB1;
    private BoundingBox BrokenColumnAABB2;
    private BoundingBox PlatformBox;
    private BoundingBox PlatformBox1;
    private BoundingBox PlatformBox2;
    private BoundingBox PlatformBox3;
    private BoundingBox[] ParedBoxes;
    private BoundingBox[] ColumnBoxes;
    private BoundingBox[] BrokenColumnBoxes;
    private BoundingBox[] PlatformBoxes;
    private BoundingBox[] RampBoxes;
    private BoundingBox PisoBox;

    //Texturas

    private Texture2D TexturaPiso;
    private Texture2D TexturaPared;
    private Texture2D TexturaColumna;
    private Texture2D TexturaRampa;
    private Texture2D TexturaPlataforma;
    private float retardoColisionBox;

    private Boolean enPisoOPlataforma;
    public void Initialize(GraphicsDevice graphicsDevice)
    {
      //Arena
      PisoWorld = Matrix.CreateScale(800, 1, 800);
      //ParedWorld = Matrix.CreateScale(30, 0, 30);

      var scale1 = new Vector3(800f, 1f, 200f);
      var scale2 = new Vector3(200f,1f,800f);
      ParedWorld = new Matrix[]
      {
        Matrix.CreateScale(scale1) * Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateTranslation(-Vector3.UnitZ * 800f),
        Matrix.CreateScale(scale1) * Matrix.CreateRotationX(-MathHelper.PiOver2) * Matrix.CreateTranslation(Vector3.UnitZ * 800f),
        Matrix.CreateScale(scale2) * Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(Vector3.UnitX * 800f),
        Matrix.CreateScale(scale2) * Matrix.CreateRotationZ(-MathHelper.PiOver2) * Matrix.CreateTranslation(-Vector3.UnitX * 800f)
      };
      

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
      };

      BrokenColumnWorld = new Matrix[]
      {
        Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(BrokenColumn1Position),
        Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(BrokenColumn2Position),
      };

      BoxPrimitive = new BoxPrimitive(graphicsDevice, Vector3.One, TexturaPlataforma);
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


public void LoadContent(QuadPrimitive piso, Model pared, Model column, Model ramp, Model platform, Texture2D texturaPiso, 
                        Texture2D texturaPared,Texture2D texturaColumna, Texture2D texturaRampa, Texture2D texturaPlataforma)
    {
      Piso = piso;
      Pared = pared;
      Column = column;
      Ramp = ramp;
      Platform = platform;

      TexturaPiso = texturaPiso;
      TexturaPared = texturaPared;
      TexturaColumna = texturaColumna;
      TexturaRampa = texturaRampa;
      TexturaPlataforma = texturaPlataforma;

      PisoBox = new BoundingBox(new Vector3(-20000f, -0.001f, -20000f), new Vector3(20000f, 0f, 20000f));
      //PisoBox = BoundingVolumesExtensions.CreateAABBFrom(Piso);
      //PisoBox = BoundingVolumesExtensions.Scale(PisoBox, new Vector3(20,1,20));
      //PisoBox = new BoundingBox(PisoBox.Min,PisoBox.Max);

      var minVector = Vector3.One * 0.25f;
      ParedBoxes = new BoundingBox[]
      {
        new BoundingBox(new Vector3(-800f, 200f, -800f) - minVector, new Vector3(800f, 0f, -800f) + minVector),
        new BoundingBox(new Vector3(-800f, 200f, 800f) - minVector, new Vector3(800f, 0f, 800f) + minVector),
        new BoundingBox(new Vector3(800f, 200f, -800f) - minVector, new Vector3(800f, 0f, 800f) + minVector),
        new BoundingBox(new Vector3(-800f, 200f, -800f) - minVector, new Vector3(-800f, 0f, 800f) + minVector)
      };

      Vector3 correctorPosicionBoxColumnas = new Vector3(0,-45,98);
      Vector3 correctorPosicionBoxColumnas2 = new Vector3(0,-10,98); //para evitar que la mitad de la BB este bajo el piso
      Vector3 correctorPosicionBoxColumnas3 = new Vector3(0,-25,98);

      ColumnBox = BoundingVolumesExtensions.CreateAABBFrom(Column);
      ColumnBox1 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.3f,1.5f,0.1f));
      ColumnBox2 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.5f, 0.7f, 0.1f));
      ColumnBox3 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.35f, 2f, 0.1f));
      ColumnBox4 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.4f,2.5f,0.1f));



      ColumnBoxes = new BoundingBox[]
      {
        new BoundingBox(ColumnBox1.Min + Column1Position -correctorPosicionBoxColumnas3, ColumnBox1.Max + Column1Position - correctorPosicionBoxColumnas3),
        new BoundingBox(ColumnBox1.Min + Column2Position -correctorPosicionBoxColumnas3, ColumnBox1.Max + Column2Position - correctorPosicionBoxColumnas3),
        new BoundingBox(ColumnBox1.Min + Column3Position -correctorPosicionBoxColumnas3, ColumnBox1.Max + Column3Position - correctorPosicionBoxColumnas3),
        new BoundingBox(ColumnBox1.Min + Column4Position -correctorPosicionBoxColumnas3, ColumnBox1.Max + Column4Position - correctorPosicionBoxColumnas3),
        new BoundingBox(ColumnBox2.Min + Column5Position -correctorPosicionBoxColumnas2, ColumnBox2.Max + Column5Position - correctorPosicionBoxColumnas2),
        new BoundingBox(ColumnBox2.Min + Column6Position -correctorPosicionBoxColumnas2, ColumnBox2.Max + Column6Position - correctorPosicionBoxColumnas2),
        new BoundingBox(ColumnBox3.Min + Column7Position -correctorPosicionBoxColumnas, ColumnBox3.Max + Column7Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox3.Min + Column8Position -correctorPosicionBoxColumnas, ColumnBox3.Max + Column8Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox3.Min + Column9Position -correctorPosicionBoxColumnas, ColumnBox3.Max + Column9Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox3.Min + Column10Position -correctorPosicionBoxColumnas, ColumnBox3.Max + Column10Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox4.Min + Column11Position -correctorPosicionBoxColumnas, ColumnBox4.Max + Column11Position - correctorPosicionBoxColumnas),
        new BoundingBox(ColumnBox4.Min + Column12Position -correctorPosicionBoxColumnas, ColumnBox4.Max + Column12Position - correctorPosicionBoxColumnas)
        };

      Vector3 correctorPosicionBoxBrokenColumnas1 = new Vector3(0,-5,40);
      Vector3 correctorPosicionBoxBrokenColumnas2 = new Vector3(0,-5,40);
      BrokenColumnAABB1 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.3f,0.3f,0.6f));
      BrokenColumnAABB2 = BoundingVolumesExtensions.Scale(ColumnBox,new Vector3(0.4f,0.3f,0.6f));

      BrokenColumnBoxes = new BoundingBox[]
      {
        new BoundingBox(BrokenColumnAABB1.Min + BrokenColumn1Position -correctorPosicionBoxBrokenColumnas1, BrokenColumnAABB1.Max + BrokenColumn1Position - correctorPosicionBoxBrokenColumnas1),
        new BoundingBox(BrokenColumnAABB2.Min + BrokenColumn2Position-correctorPosicionBoxBrokenColumnas2, BrokenColumnAABB2.Max + BrokenColumn2Position - correctorPosicionBoxBrokenColumnas2)
      };
     

      PlatformBox = BoundingVolumesExtensions.CreateAABBFrom(Platform);
      PlatformBox1 = BoundingVolumesExtensions.Scale(PlatformBox,new Vector3(100, 5, 80));
      PlatformBox2 = BoundingVolumesExtensions.Scale(PlatformBox,new Vector3(100, 5, 70));
      PlatformBox3 = BoundingVolumesExtensions.Scale(PlatformBox,new Vector3(50, 10, 50));


      PlatformBoxes = new BoundingBox[]
      {
        new BoundingBox(PlatformBox1.Min + Platform1Position , PlatformBox1.Max + Platform1Position),
        new BoundingBox(PlatformBox1.Min + Platform2Position , PlatformBox1.Max + Platform2Position),
        new BoundingBox(PlatformBox2.Min + Platform3Position , PlatformBox2.Max + Platform3Position),
        new BoundingBox(PlatformBox3.Min + Platform4Position , PlatformBox3.Max + Platform4Position),
        new BoundingBox(PlatformBox3.Min + Platform5Position , PlatformBox3.Max + Platform5Position)
        
      };
    }

    public Boolean Update(GameTime gameTime, Autos auto)
        {
            OrientedBoundingBox autoCollider;
            Vector3 vectorChoque = Vector3.Zero;
            float penetration = 0f;
            enPisoOPlataforma = false;
            autoCollider = auto.getAutoPrincipalBox();


            if(autoCollider.Intersects(PisoBox))
            {
              auto.autoEnPiso();
            }
            else{
              auto.autoNoEnPiso();
            }


          for(int index = 0; index < PlatformBoxes.Length; index++)
            {
                if(autoCollider.Intersects(PlatformBoxes[index]))
                {
                  if(autoCollider.Center.Y > PlatformBoxes[index].Max.Y || index > 2)
                  {
                    auto.autoEnPlataforma(PlatformBoxes[index].Max.Y);
                  }
                  else
                  {
                    auto.chocarTecho();
                  }
                }
                else
                {
                  auto.autoNoEnPlataforma();
                }
            }



            for(int index = 0; index < ColumnBoxes.Length; index++)
            {
                if(autoCollider.Intersects(ColumnBoxes[index],out vectorChoque,out penetration))
                {
                  if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                   auto.rebotar(vectorChoque,penetration);
                   auto.FrenarAuto();
                }
            }

            for(int index = 0; index < BrokenColumnBoxes.Length; index++)
            {
                if(autoCollider.Intersects(BrokenColumnBoxes[index],out vectorChoque,out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                   auto.rebotar(vectorChoque,penetration);
                   auto.FrenarAuto();
                }
            }

            for(int index = 0; index < ParedBoxes.Length; index++)
            {
                if(autoCollider.Intersects(ParedBoxes[index],out vectorChoque,out penetration))
                {
                  if(auto.autoSpeed() > 10f) 
                  {
                    auto.audioChoque();
                  }
                   auto.rebotar(vectorChoque,penetration);
                   auto.FrenarAuto();
                }
            }
            
            return false;

        }
public void dibujar(Matrix view, Matrix projection, Effect effect, Matrix matrizMundo, Model modelo, Texture2D textura)
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
      effect.Parameters["ModelTexture"].SetValue(textura);

      relativeMatrices = new Matrix[modelo.Bones.Count];
      modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);


      foreach (var mesh in modelo.Meshes)
      {
        effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
        effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Invert(Matrix.Transpose(matrizMundo)));
        mesh.Draw();
      }
    }


    public void dibujar(Matrix view, Matrix projection, Effect effect, Matrix matrizMundo, QuadPrimitive quad, Texture2D textura)
    {
        effect.Parameters["View"]?.SetValue(view);
        effect.Parameters["Projection"]?.SetValue(projection);
        effect.Parameters["ModelTexture"]?.SetValue(textura);
        effect.Parameters["World"]?.SetValue(matrizMundo);
        effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Invert(Matrix.Transpose(matrizMundo)));
        quad.Draw(effect);
    }

    public void dibujarEscenario(Matrix view, Matrix projection, Effect effect, Boolean pisoOn)
    {

      effect.CurrentTechnique = effect.Techniques["Luz"];
      //Arena
      if(pisoOn)
        dibujar(view, projection, effect, PisoWorld, Piso, TexturaPiso);
      
      //Paredes
      for(int index = 0; index < ParedWorld.Length; index++)
      {
        dibujar(view,projection,effect,ParedWorld[index], Platform, TexturaPared);
      }

      //Plataformas
      for(int index = 0; index < PlatformWorld.Length; index++)
      {
        dibujar(view,projection,effect,PlatformWorld[index], Platform, TexturaPlataforma);
      }

      //Columnas
      for(int index = 0; index < ColumnWorld.Length; index++)
      {
        dibujar(view,projection,effect,ColumnWorld[index], Column, TexturaColumna);
      }
      
      //ColumnasRotas
      for(int index = 0; index < BrokenColumnWorld.Length; index++)
      {
        dibujar(view,projection,effect,BrokenColumnWorld[index], Column, TexturaColumna);
      }

      //Rampas
      for(int index = 0; index < RampWorld.Length; index++)
      {
        dibujar(view,projection,effect,RampWorld[index], Ramp, TexturaRampa);
      }
      
    }

    public void dibujarBoundingBoxes(Gizmos gizmos)
    {
      for(int index = 0; index < ColumnBoxes.Length; index++)
        {
           gizmos.DrawCube((ColumnBoxes[index].Max + ColumnBoxes[index].Min) / 2f,ColumnBoxes[index].Max - ColumnBoxes[index].Min,Color.Red);
        }

      for(int index = 0; index < PlatformBoxes.Length; index++)
        {
           gizmos.DrawCube((PlatformBoxes[index].Max + PlatformBoxes[index].Min) / 2f,PlatformBoxes[index].Max - PlatformBoxes[index].Min,Color.Red);
        }

       for(int index = 0; index < ParedBoxes.Length; index++)
        {
           gizmos.DrawCube((ParedBoxes[index].Max + ParedBoxes[index].Min) / 2f,ParedBoxes[index].Max - ParedBoxes[index].Min,Color.Red);
        }

        gizmos.DrawCube((PisoBox.Max + PisoBox.Min) / 2f,PisoBox.Max - PisoBox.Min,Color.Red);

        for(int index = 0; index < BrokenColumnBoxes.Length; index++)
        {
           gizmos.DrawCube((BrokenColumnBoxes[index].Max + BrokenColumnBoxes[index].Min) / 2f,BrokenColumnBoxes[index].Max - BrokenColumnBoxes[index].Min,Color.Red);
        }
    }

  public BoundingBox[] getParedBoxes()
  {
    return ParedBoxes;
  }
  public BoundingBox[] getRampBoxes()
  {
    return ParedBoxes;
  }
  public BoundingBox[] getColumnBoxes()
  {
    return ColumnBoxes;
  }


  }
}