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
  public class Autos
  {
    public Autos() { }
    private Model AutoDeportivo { get; set; }
    private Model AutoDeCombate { get; set; }

    //MovimientoAuto
    private int CollisionIndex;
    private Vector3 direccionPostChoque;
    public Vector3 CarDirection;
    public Vector2 CarSpeed;
    public float CarAcceleration;
    private float CarBrakes;
    private float Rozamiento;
    public float Rotation;
    private bool ActiveMovement;
    private Boolean onJump;
    private float jumpRotation;
    private float jumpSpeed;
    private float gravity;
    private float maxSpeed;
    private float WheelRotationPrincipal;
    private float frontWheelRotation;
    private ModelBone leftBackWheelBone;
    private ModelBone rightBackWheelBone;
    private ModelBone leftFrontWheelBone;
    private ModelBone rightFrontWheelBone;
    private ModelBone wheelDerecha1;
    private ModelBone wheelDerecha2;
    private ModelBone wheelDerecha3;
    private ModelBone wheelIzquierda1;
    private ModelBone wheelIzquierda2;
    private ModelBone wheelIzquierda3;
    private ModelBone wheelIzquierda4;
    private ModelBone wheelDerecha4;
    private Matrix leftBackWheelTransform = Matrix.Identity;
    private Matrix rightBackWheelTransform = Matrix.Identity;
    private Matrix leftFrontWheelTransform = Matrix.Identity;
    private Matrix rightFrontWheelTransform = Matrix.Identity;

    private Matrix wheelDerecha1Transform = Matrix.Identity;
    private Matrix wheelDerecha2Transform = Matrix.Identity;
    private Matrix wheelDerecha3Transform = Matrix.Identity;
    private Matrix wheelDerecha4Transform = Matrix.Identity;

    private Matrix wheelIzquierda1Transform = Matrix.Identity;
    private Matrix wheelIzquierda2Transform = Matrix.Identity;
    private Matrix wheelIzquierda3Transform = Matrix.Identity;
    private Matrix wheelIzquierda4Transform = Matrix.Identity;

    private float[] frontWheelRotationIA;
    private Matrix[] relativeMatrices;

    //Matrices
    private Matrix AutoPrincipalWorld { get; set; }

    private int cantidadEnemigos = 8;

    //matrices y vectores autos IA
    private Matrix[] AutosWorld;
    public Vector3[] AutosPosiciones;
    public Boolean[] permitirMovimiento;
    public Vector3[] AutosDirecciones;
    public float[] AutosRotaciones;
    private float[] CarsSpeeds;
    private Vector3[] DesplazamientoAutos;

    private int BajasBalas = 0;
    private int BajasMisil = 0;
    
    // Variables
    private float mediaVuelta = MathF.PI;
    private float cuartoDeVuelta = MathF.PI / 2;
    public Vector3 AutoPrincipalPos = new Vector3(0, 0, 0);
    private Vector3 Auto1Pos = new Vector3(0, 0, 70);
    private Vector3 Auto2Pos = new Vector3(30, 0, 70);
    private Vector3 Auto3Pos = new Vector3(60, 0, 70);
    private Vector3 Auto4Pos = new Vector3(-30, 0, 70);
    private Vector3 Auto5Pos = new Vector3(-60, 0, 70);
    private Vector3 Auto6Pos = new Vector3(0, 0, 180);
    private Vector3 Auto7Pos = new Vector3(80, 0, 180);
    private Vector3 Auto8Pos = new Vector3(-80, 0, 180);
    private Vector3 Desplazamiento;
    private Boolean enElPiso;
    private Boolean enPlataforma;
    private float tiempoEnAire;
    private Random unRandom = new Random();
    public int vidaProtagonista = 100;
    private int[] vidaAutos;
    private Vector3 posicionEspera = new Vector3(0f,1000f,0f); //para que las boxes de autos que acaban de respawnear no nos golpeen

    //Texturas
    private List<Texture2D> ColorTextures { get; set; }

    //Colisiones
    private BoundingBox AutoDeportivoBoxAABB;
    private BoundingBox AutoDeCombateBoxAABB;
    private OrientedBoundingBox AutoPrincipalBox;
    private Matrix AutoPrincipalOBBWorld;
    public OrientedBoundingBox[] CollideCars;
    private float PreviousSpeed;
    private Boolean turbo;
    private float turboTime;
    private float penetration;
    Vector3 coreccionAltura = new Vector3(0, 66f, 0); //el centro de la oriented bounding box esta quedando muy arriba
    Vector3 coreccionAlturaAutoCombate = new Vector3(199, 4244f, -443); //(3,-20f,0);

    private Vector3[] objetivo;
    private float timerInvencibilidad;
    private Boolean serInvencible;

    //Menu
    private Vector3 autoMenuPos = new Vector3(0,0,-130);
    private Matrix autoMenu;
    private Matrix autoMenu2;
    private Matrix autoMenu3;

    //Sonidos
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect CarCrash {get; set;}
    private SoundEffect VidaPerdida {get; set;}
    private SoundEffect KillEffect {get; set;}
    private int acabaDeChocar = 0;
    private bool choco;

    //IA
    private Vector3[] Spawns;
    private float[] timersRespawn;
    private List<int> autosDestruidos;
    private Boolean[] dissolveActivado;
     private Boolean[] IAenPlataforma;
    private Boolean[] IAenPiso;
    
    //GODMODE
    private Boolean modoDios;

    public void Initialize()
    {
      DesplazamientoAutos = new Vector3[cantidadEnemigos];
      permitirMovimiento = new Boolean[cantidadEnemigos];
      AutosPosiciones = new Vector3[cantidadEnemigos];
      objetivo = new Vector3[cantidadEnemigos];
      AutosWorld = new Matrix[cantidadEnemigos];
      ColorTextures = new List<Texture2D>();
      
      iniciarPartida();
    }

    public void LoadContent(Model Auto1, Model Auto2, Effect effect, SoundEffect carCrash, SoundEffect vidaPerdida,
                            SoundEffect killEffect)
    {
      AutoDeportivo = Auto1;
      AutoDeCombate = Auto2;
      CarCrash = carCrash;
      VidaPerdida = vidaPerdida;
      KillEffect = killEffect;

      leftBackWheelBone = AutoDeportivo.Bones["WheelD"];
      rightBackWheelBone = AutoDeportivo.Bones["WheelC"];
      leftFrontWheelBone = AutoDeportivo.Bones["WheelA"];
      rightFrontWheelBone = AutoDeportivo.Bones["WheelB"];

      leftBackWheelTransform = leftBackWheelBone.Transform;
      rightBackWheelTransform = rightBackWheelBone.Transform;
      leftFrontWheelTransform = leftFrontWheelBone.Transform;
      rightFrontWheelTransform = rightFrontWheelBone.Transform;

      wheelDerecha1 = AutoDeCombate.Bones["Wheel1"];
      wheelDerecha2 = AutoDeCombate.Bones["Wheel2"];
      wheelDerecha3 = AutoDeCombate.Bones["Wheel3"];
      wheelDerecha4 = AutoDeCombate.Bones["Wheel4"];
      
      wheelIzquierda1 = AutoDeCombate.Bones["Wheel5"];
      wheelIzquierda2 = AutoDeCombate.Bones["Wheel6"];
      wheelIzquierda3 = AutoDeCombate.Bones["Wheel7"];
      wheelIzquierda4 = AutoDeCombate.Bones["Wheel8"];

      wheelDerecha1Transform = wheelDerecha1.Transform;
      wheelDerecha2Transform = wheelDerecha2.Transform;
      wheelDerecha3Transform = wheelDerecha3.Transform;
      wheelDerecha4Transform = wheelDerecha4.Transform;
      
      wheelIzquierda1Transform = wheelIzquierda1.Transform;
      wheelIzquierda2Transform = wheelIzquierda2.Transform;
      wheelIzquierda3Transform = wheelIzquierda3.Transform;
      wheelIzquierda4Transform = wheelIzquierda4.Transform;

      //Texturas
      foreach (var mesh in AutoDeportivo.Meshes)
      {
        foreach (var meshPart in mesh.MeshParts)
        {
          ColorTextures.Add(((BasicEffect)meshPart.Effect).Texture);
          meshPart.Effect = effect;
        }
      }

      foreach (var mesh in AutoDeCombate.Meshes)
      {
        foreach (var meshPart in mesh.MeshParts)
        {
          ColorTextures.Add(((BasicEffect)meshPart.Effect).Texture);
          meshPart.Effect = effect;
        }
      }

      this.inicializarBoundingBoxes();

    }

    public void Update(GameTime gameTime,PowerUps powerUps,Escenario escenario, Detalles detalles)
    {
      var keyboardState = Keyboard.GetState();
      var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
      CarDirection = Vector3.Normalize(AutoPrincipalWorld.Backward); //se normalizo porque AutoPrincipalWorld.Backward toma valores como Vector3(0,0,0.1);
      Desplazamiento = Vector3.Zero;
      Rozamiento = CarSpeed.X * 0.5f;
      choco = false;

      if (Keyboard.GetState().IsKeyDown(Keys.W) && !onJump)
      {
        if (CarSpeed.X < 0) //De esta manera si estaba yendo hacia atras, frena y luego acelera hacia delante
        {
          CarSpeed.X += CarBrakes * elapsedTime;
        }
        else if (CarSpeed.X < maxSpeed) 
          CarSpeed.X += CarAcceleration * elapsedTime;
        
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime + CarAcceleration * elapsedTime * elapsedTime* CarDirection  / 2f;
        ActiveMovement = true;
        frontWheelRotation += elapsedTime * CarSpeed.X/10;
      }
      else if (Keyboard.GetState().IsKeyDown(Keys.S) && !onJump)
      {
        if (CarSpeed.X >= 0) //De esta manera si estaba yendo hacia delante, frena y luego acelera hacia atras
        {
          CarSpeed.X -= CarBrakes * elapsedTime;
        }
        else if (CarSpeed.X > -maxSpeed) CarSpeed.X -= CarAcceleration * elapsedTime;
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime + CarAcceleration * elapsedTime * elapsedTime* CarDirection  / 2f;
        ActiveMovement = true;
        frontWheelRotation -= elapsedTime * -CarSpeed.X/10;
   
      }

      if ((keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.W)) || onJump)
      {
        if(CarSpeed.X < 3 && CarSpeed.X > -3)
        {
          CarSpeed.X = 0f;
        }      
        else
        {
          CarSpeed.X -= Rozamiento * elapsedTime;
        }
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime;
        frontWheelRotation += elapsedTime * CarSpeed.X/10;
      }

      if(CarSpeed.X == 0f) ActiveMovement = false;
      //rotar
      if (Keyboard.GetState().IsKeyDown(Keys.D))
      {
        if (CarSpeed.X >= 0 && WheelRotationPrincipal > -MathF.PI * 1 / 6) WheelRotationPrincipal -= elapsedTime;
        else if (CarSpeed.X < 0 && WheelRotationPrincipal < MathF.PI * 1 / 6) WheelRotationPrincipal += elapsedTime;
        if (ActiveMovement) Rotation -= elapsedTime;         //NO DEBERIA PODER GIRAR AL ESTAR QUIETO
      }
      else if (Keyboard.GetState().IsKeyDown(Keys.A))
      {
        if (CarSpeed.X >= 0 && WheelRotationPrincipal < MathF.PI * 1 / 6) WheelRotationPrincipal += elapsedTime;
        else if (CarSpeed.X < 0 && WheelRotationPrincipal > -MathF.PI * 1 / 6) WheelRotationPrincipal -= elapsedTime;
        if (ActiveMovement) Rotation += elapsedTime;
      }
      else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
      {
        WheelRotationPrincipal = 0f;//cuando soltas W o A el auto y las ruedas siguen recto
      }



      //saltar
      if (Keyboard.GetState().IsKeyDown(Keys.Space)  && (enElPiso || enPlataforma))
      {
        CarSpeed.Y = jumpSpeed;
        onJump = true;
        enElPiso = false;
        enPlataforma = false;
      }

      if(!enElPiso && !enPlataforma)
      {
        tiempoEnAire += (float)gameTime.ElapsedGameTime.TotalSeconds;
        CarSpeed.Y -= gravity * tiempoEnAire;
        Desplazamiento.Y += CarSpeed.Y;

            if (CarSpeed.X >= 0)
            {
              if(CarSpeed.Y > 0) jumpRotation += elapsedTime;
              else jumpRotation -= elapsedTime*2;
            }
            else if (CarSpeed.X < 0)
            {
              if(CarSpeed.Y > 0) jumpRotation -= elapsedTime;
              else jumpRotation += elapsedTime*2;
            }
    
      }

      if(turbo){
        turboTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(turboTime < 1f)
        {
          if(MathF.Abs(CarSpeed.X) < 500)
             CarSpeed *= 1.1f;
        }
        else if(turboTime < 2f)
        {
          if(MathF.Abs(CarSpeed.X) > PreviousSpeed) 
          CarSpeed /= 1.1f;
        }
        else{
              CarSpeed.X = PreviousSpeed;
          turbo = false;
          turboTime = 0f;
        }
      }


      //choque autosIA con autoPrincipal
      for (var index = 0; index < cantidadEnemigos; index++)
      {
        if (AutoPrincipalBox.Intersects(CollideCars[index]) && !autosDestruidos.Contains(index))
        {
            if(!modoDios)
            {
              turbo = false;
              
              CollisionIndex = index;
              direccionPostChoque = CarDirection;
              Desplazamiento*=-1;
              CarsSpeeds[CollisionIndex] = 100f;
              CarSpeed*=-0.5f;


              if(!serInvencible)
              {
                if(index < 5)
                  vidaProtagonista -= 25;
                else
                  vidaProtagonista -= 50;

                serInvencible = true;
              }
            }
              vidaAutos[index] = 0;

              dissolveActivado[index] = true;
              timersRespawn[index] = 0f;
              autosDestruidos.Add(index);
            
            
            
            audioChoque();
            if(!modoDios)
              Instance = VidaPerdida.CreateInstance();
            Instance.Play();

        }
      }

        

      for(int i = 0; i < cantidadEnemigos; i++)
      {
        objetivo[i] = AutoPrincipalPos;
        atacarAutoPrincipal(i,elapsedTime);
      }


      

      if (!choco) {acabaDeChocar = 0;}
        
        for (int i = 0; i < powerUps.BalasWorld.Length; i++)
        {
            if (powerUps.recorridoBalas[i] > 0f)
            {
                for (var index = 0; index < cantidadEnemigos; index++)
                {
                    if(powerUps.collidersBalas[i].Intersects(CollideCars[index]) && !autosDestruidos.Contains(index))
                    {
                        powerUps.recorridoBalas[i] = 0f;
                        vidaAutos[index] -= 50;
                        if(vidaAutos[index] <= 0){

                          dissolveActivado[index] = true;
                          timersRespawn[index] = 0f;
                          autosDestruidos.Add(index);

                          BajasBalas++;
                          Instance = KillEffect.CreateInstance();
                          Instance.Play();
                        } 
                    }
                }
            }
        }

        if (powerUps.recorridoMisil > 0f)
            {
                for (var index = 0; index < cantidadEnemigos; index++)
                {
                    if(powerUps.colliderMisil.Intersects(CollideCars[index])&& !autosDestruidos.Contains(index))
                    {
                        powerUps.recorridoMisil = 0f;
                        vidaAutos[index] -= 200;
                        if(vidaAutos[index] <= 0){

                          dissolveActivado[index] = true;
                          timersRespawn[index] = 0f;
                          autosDestruidos.Add(index);

                          BajasMisil++;
                          Instance = KillEffect.CreateInstance();
                          Instance.Play();
                        }
                    }
                }
            }

         for(int i = 0; i < cantidadEnemigos; i++)
        {
            if(escenario.IAchoco(CollideCars[i]) || detalles.IAchoco(CollideCars[i])){
              dissolveActivado[i] = true;
              timersRespawn[i] = 0f;
              autosDestruidos.Add(i);
             }
          
          for(int k = 0; k < escenario.getPlatformBoxes().Length; k++)
          {
            if(CollideCars[i].Intersects(escenario.getPlatformBoxes()[k]))
            {
              IAenPlataforma[i] = true;
              AutosPosiciones[i].Y = escenario.getPlatformBoxes()[k].Max.Y;
            } 
            else
            {
              IAenPlataforma[i] = false;
            }
          }

          if(CollideCars[i].Intersects(escenario.getPisoBox()))
          {
            IAenPiso[i] = true;
            //IAsobrePiso(i);
          }
          else
          {
            IAenPiso[i] = false;
          }
            
        }

      for(int i = 0; i < cantidadEnemigos; i++)
      {
        if(!IAenPiso[i] && !IAenPlataforma[i])
        {
          AutosPosiciones[i].Y -= gravity;
        }
      }  

      for(int i = 0; i < cantidadEnemigos; i++)
      {
        for(int j = 0; j < cantidadEnemigos; j++)
        {
          if(CollideCars[i].Intersects(CollideCars[j]) && CollideCars[i] != CollideCars[j])
          {
            if(i > j)
            {              
              if(i < 5)
                AutosPosiciones[j] += Vector3.Normalize(AutosWorld[i].Backward) * 0.3f;
              else
                AutosPosiciones[j] += Vector3.Normalize(AutosWorld[i].Left) * 0.4f; //es un auto de combate
            }
            else
            {
              if(j < 5)
                AutosPosiciones[i] += Vector3.Normalize(AutosWorld[j].Backward) * 0.3f;
              else
                AutosPosiciones[i] += Vector3.Normalize(AutosWorld[j].Left) * 0.4f; //es un auto de combate
            }
          }
        }
      }
     

      

    

      for(int i = 0; i < autosDestruidos.Count; i++)
        {
          
          if(timersRespawn[autosDestruidos[i]] == 1.0f)
          {
            AutosPosiciones[autosDestruidos[i]] = obtenerSpawn();
            dissolveActivado[autosDestruidos[i]] = false;
            
             
             if(autosDestruidos[i] < 5)
                vidaAutos[autosDestruidos[i]] = 100;
             else
                vidaAutos[autosDestruidos[i]] = 200;
              
             autosDestruidos.RemoveAt(i);
          
          }
          else
          {
            timersRespawn[autosDestruidos[i]] = Math.Min(1.0f, timersRespawn[autosDestruidos[i]] +elapsedTime);
          }
        }

        if(serInvencible && timerInvencibilidad < 2)
        {
          timerInvencibilidad = MathF.Min(2.0f,timerInvencibilidad + elapsedTime);
        }
        else
        {
          serInvencible = false;
          timerInvencibilidad = 0f;
        }


      //ubicacion auto principal
       AutoPrincipalPos += Desplazamiento;
       AutoPrincipalWorld =  Matrix.CreateScale(0.1f) *
                            Matrix.CreateRotationX(-jumpRotation) *
                            Matrix.CreateRotationY(Rotation * 2) *
                            Matrix.CreateTranslation(AutoPrincipalPos);

      AutoPrincipalBox = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutoPrincipalPos - coreccionAltura, AutoDeportivoBoxAABB.Max + AutoPrincipalPos - coreccionAltura));
      AutoPrincipalBox.Rotate(Matrix.CreateRotationX(-jumpRotation) * Matrix.CreateRotationY(Rotation * 2));

      //ubicacion coches IA
      for (var index = 0; index < cantidadEnemigos; index++)
      {
        if(autosDestruidos.Contains(index))
        {
          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + posicionEspera - coreccionAltura, AutoDeportivoBoxAABB.Max + posicionEspera - coreccionAltura));
        }

        else if (index < 5)
        {
          AutosWorld[index] = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateRotationY(AutosRotaciones[index] ) * Matrix.CreateTranslation(AutosPosiciones[index]);
                              
          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutosPosiciones[index] - coreccionAltura, AutoDeportivoBoxAABB.Max + AutosPosiciones[index] - coreccionAltura));
        }
        else
        {
          AutosWorld[index] = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(-cuartoDeVuelta) * Matrix.CreateRotationY(AutosRotaciones[index] ) * Matrix.CreateTranslation(AutosPosiciones[index]);

          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + AutosPosiciones[index] - coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + AutosPosiciones[index] - coreccionAlturaAutoCombate));
        }
        CollideCars[index].Rotate(Matrix.CreateRotationY(AutosRotaciones[index]));
      }
      
      
    
    }

    public void dibujarAutoDeportivo(Matrix view, Matrix projection, Effect effect, Model modelo,float fronRot ,float WheelRot, Matrix matrizMundo, String tecnica)
    {
      effect.Parameters["View"].SetValue(view);
      effect.Parameters["Projection"].SetValue(projection);
      effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Transpose(Matrix.Invert(matrizMundo)));
      

      relativeMatrices = new Matrix[modelo.Bones.Count];
      
      int index = 0;
      foreach (var mesh in modelo.Meshes)
      {
        effect.CurrentTechnique = effect.Techniques[tecnica];

        rightFrontWheelBone.Transform = Matrix.CreateRotationX(fronRot) * Matrix.CreateRotationY(WheelRot) * rightFrontWheelTransform;
        leftFrontWheelBone.Transform =  Matrix.CreateRotationX(fronRot) * Matrix.CreateRotationY(WheelRot) * leftFrontWheelTransform;
        leftBackWheelBone.Transform = Matrix.CreateRotationX(fronRot) * leftBackWheelTransform;
        rightBackWheelBone.Transform = Matrix.CreateRotationX(fronRot) * rightBackWheelTransform;
        modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);

        effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
        foreach (var meshPart in mesh.MeshParts)
        {
         
          effect.GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
          effect.GraphicsDevice.Indices = meshPart.IndexBuffer;
          effect.Parameters["ModelTexture"]?.SetValue(ColorTextures[index]);
          foreach (var pass in effect.CurrentTechnique.Passes)
          {
            pass.Apply();
            effect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
          }
        }
        mesh.Draw();
        index++;
      }
    }
    public void dibujarAutoDeCombate(Matrix view, Matrix projection, Effect effect, Model modelo,float fronRot ,float WheelRot, Matrix matrizMundo, String tecnica)
    {
      effect.CurrentTechnique = effect.Techniques[tecnica];
      effect.Parameters["View"].SetValue(view);
      effect.Parameters["Projection"].SetValue(projection);
      effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Transpose(Matrix.Invert(matrizMundo)));
      

      relativeMatrices = new Matrix[modelo.Bones.Count];
      
      int index = 0;
      foreach (var mesh in modelo.Meshes)
      {
        
        wheelDerecha1.Transform = Matrix.CreateRotationZ(-fronRot) * Matrix.CreateRotationY(WheelRot) * wheelDerecha1Transform;
        wheelDerecha2.Transform =  Matrix.CreateRotationZ(-fronRot) * Matrix.CreateRotationY(WheelRot) * wheelDerecha2Transform;
        wheelDerecha3.Transform = Matrix.CreateRotationZ(-fronRot) * wheelDerecha3Transform;
        //wheelDerecha4.Transform =   Matrix.CreateRotationZ(-fronRot) * wheelDerecha4Transform; por alguna razon no rota sobre su eje
        
        wheelIzquierda1.Transform = Matrix.CreateRotationZ(-fronRot) * Matrix.CreateRotationY(WheelRot) * wheelIzquierda1Transform;
        wheelIzquierda2.Transform =  Matrix.CreateRotationZ(-fronRot) * Matrix.CreateRotationY(WheelRot) * wheelIzquierda2Transform;
        wheelIzquierda3.Transform = Matrix.CreateRotationZ(-fronRot) * wheelIzquierda3Transform;
        //wheelIzquierda4.Transform = Matrix.CreateRotationZ(-fronRot) * wheelIzquierda4Transform;

        modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);

        effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index] * matrizMundo);
        foreach (var meshPart in mesh.MeshParts)
        {
          
          effect.GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
          effect.GraphicsDevice.Indices = meshPart.IndexBuffer;
          effect.Parameters["ModelTexture"]?.SetValue(ColorTextures[index]);
          foreach (var pass in effect.CurrentTechnique.Passes)
          {
            pass.Apply();
            effect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
          }
        }
        mesh.Draw();
        index++;
      }
    }

    public void dibujarAutosMenu(Matrix view, Matrix projection, Effect effect, float timerMenu)
    {
      
       //effect.Parameters["lightPosition"]?.SetValue(new Vector3(0.0f,100.0f,0.0f));
       dibujarAutoDeportivo(view, projection, effect, AutoDeportivo, timerMenu, 0, autoMenu, "Luz");
       dibujarAutoDeCombate(view, projection, effect, AutoDeCombate, timerMenu,0, autoMenu2, "Luz");
       dibujarAutoDeportivo(view, projection, effect, AutoDeportivo, timerMenu ,0, autoMenu3, "Luz");
    }
    public void dibujarAutos(Matrix view, Matrix projection, Effect effect, String tecnica)
    {
      String Tecnica;
      effect.Parameters["colorBloom"]?.SetValue(Color.White.ToVector3());
      
      if(serInvencible)
        effect.Parameters["invencible"].SetValue(true);
      else
        effect.Parameters["invencible"].SetValue(false);
     
      dibujarAutoDeportivo(view, projection, effect, AutoDeportivo,frontWheelRotation, WheelRotationPrincipal, AutoPrincipalWorld, tecnica);


      for (int index = 0; index < cantidadEnemigos; index++)
      {
        effect.Parameters["invencible"].SetValue(false);
        if(dissolveActivado[index]){
          effect.Parameters["tiempoRestante"]?.SetValue(timersRespawn[index]);
          Tecnica = "Dissolve";
          
        }else{
          Tecnica = tecnica;
        }

        //if(!autosDestruidos.Contains(index))
          //{
            if (index < 5)  dibujarAutoDeportivo(view, projection, effect, AutoDeportivo, frontWheelRotationIA[index], 0f,AutosWorld[index], Tecnica);
            else dibujarAutoDeCombate(view, projection, effect, AutoDeCombate, frontWheelRotationIA[index], 0f,AutosWorld[index], Tecnica);
          //}
        }
    }

    public Vector3 posAutoPrincipal()
    {
      return AutoPrincipalPos;
    }

    public OrientedBoundingBox getAutoPrincipalBox()
    {
      return AutoPrincipalBox;
    }

    public void FrenarAuto()
    {
      CarSpeed.X = 0;
    }
    public int autoSpeed(){
      return (int)CarSpeed.X;
    }

    public int getAutoBajas()
    {
      return BajasBalas + BajasMisil;
    }
    public void autoEnPiso()
    {   
        CarSpeed.Y = 0f;
        onJump = false;
        jumpSpeed = 10f;
        jumpRotation = 0;
        tiempoEnAire = 0f;
        AutoPrincipalPos.Y = 0f;
        enElPiso = true;
    }
     public void autoEnPlataforma(float alturaPlataformaIntersectada)
    {
        CarSpeed.Y = 0f;
        onJump = false;
        jumpRotation = 0;
        tiempoEnAire = 0f;
        enPlataforma = true;
        AutoPrincipalPos.Y = alturaPlataformaIntersectada;

    }
    public void autoNoEnPiso()
    {
      enElPiso = false;
    }
    public void autoNoEnPlataforma()
    {
      enPlataforma = false;
    }

    public void rebotar(Vector3 vectorChoque, float penetration){
      AutoPrincipalPos += vectorChoque *penetration;
    }

    public void audioChoque()
    {
      if(acabaDeChocar == 0)
          {
            acabaDeChocar = 1;
            Instance = CarCrash.CreateInstance();
            Instance.Play();
          }
          choco = true;
    }
    public void aplicarTurbo(){

      PreviousSpeed = CarSpeed.X;
      turbo = true;
    }

    public void inicializarBoundingBoxes()
    {
      AutoDeportivoBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeportivo);
      AutoDeportivoBoxAABB = BoundingVolumesExtensions.Scale(AutoDeportivoBoxAABB, 0.1f);
      AutoDeCombateBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeCombate);
      AutoDeCombateBoxAABB = BoundingVolumesExtensions.Scale(AutoDeCombateBoxAABB, new Vector3(0.0042f, 0.004f, 0.011f));

      AutoPrincipalBox = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min - coreccionAltura, AutoDeportivoBoxAABB.Max - coreccionAltura));

      CollideCars = new OrientedBoundingBox[cantidadEnemigos];

      for(int index = 0; index < cantidadEnemigos; index++)
      {
        if(index < 5)
          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutosPosiciones[index] - coreccionAltura, AutoDeportivoBoxAABB.Max + AutosPosiciones[index] - coreccionAltura));
        else
          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + AutosPosiciones[index] - coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + AutosPosiciones[index] - coreccionAlturaAutoCombate));
      };
    }

    public void dibujarBoundingBoxes(Gizmos gizmos)
    {
      AutoPrincipalOBBWorld = Matrix.CreateScale(AutoPrincipalBox.Extents * 2f) * AutoPrincipalBox.Orientation * Matrix.CreateTranslation(AutoPrincipalBox.Center);
      gizmos.DrawCube(AutoPrincipalOBBWorld, Color.Red);

      for (int index = 0; index < cantidadEnemigos; index++)
      {
        Matrix OBBWorld = Matrix.CreateScale(CollideCars[index].Extents * 2f) * CollideCars[index].Orientation * Matrix.CreateTranslation(CollideCars[index].Center);
        gizmos.DrawCube(OBBWorld, Color.Red);
      }
    }

    //aca esta la magia de la rotación
    public void atacarAutoPrincipal(int index,float elapsedTime){
        Vector2 delante = new Vector2(AutosDirecciones[index].X,AutosDirecciones[index].Z); //vector frente de auto
        Vector2 apunta = new Vector2(AutoPrincipalPos.X - AutosPosiciones[index].X,AutoPrincipalPos.Z - AutosPosiciones[index].Z); //vector entre auto principal y IA
       
        delante = Vector2.Normalize(delante);
        apunta = Vector2.Normalize(apunta);

        float diffX = apunta.X - delante.X; //diferencias en X entre vectores
        float diffY = apunta.Y - delante.Y; //diferencias en z entre vectores

        float angle = (float)Math.Atan2(diffY,diffX); //arco tangente de las diferencias en Radianes
        //nos devuelve un angulo teniendo en cuenta todos los cuadrates -> desde -180 a 180 
        //atan devuelve entre -90 y 90 no sirve

        AutosRotaciones[index] = -angle*2; //sin el "*2" giraban muy lento

        if(index < 5) //sus matrices de mundo originalmente apuntan hacia otro lado (right = adelante en el tanque) y (atras = adelante en el auto)
          AutosPosiciones[index] += Vector3.Normalize(AutosWorld[index].Backward) * CarsSpeeds[index] * elapsedTime; 
        else
          AutosPosiciones[index] += Vector3.Normalize(AutosWorld[index].Left) * CarsSpeeds[index] * elapsedTime;

        
        frontWheelRotationIA[index] += elapsedTime;  
    }


    private double RandomPosition(Random random)
    {
        return random.NextDouble();
    }

    public bool muereProta()
    {
        return vidaProtagonista <= 0 ? true : false;
    }

    public Vector3 obtenerSpawn(){
      double aa = RandomPosition(unRandom);
      if(aa < 0.2)
         return Spawns[0];
      else if(aa < 0.4)
         return Spawns[1];
      else if(aa < 0.6)
         return Spawns[2];
      else if(aa < 0.8)
         return Spawns[3];
      else
         return Spawns[4];
    }

 public void iniciarPartida(){
        AutoPrincipalPos = new Vector3(0, 0, 0);
        vidaProtagonista = 100;
        CarDirection = AutoPrincipalWorld.Backward;
        Desplazamiento = Vector3.Zero;
        Rozamiento = CarSpeed.X * 0.5f;
        BajasBalas = 0;
        BajasMisil = 0;

      dissolveActivado = new Boolean[]
      {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false
      };

//MovimientoAuto
      CarSpeed = new Vector2(0f,0f);
      
      CarAcceleration = 50f;
      CarBrakes = 200f;
      ActiveMovement = false;
      jumpSpeed = 30f;
      gravity = 2f;
      onJump = false;
      maxSpeed = 200f;
      enElPiso = true;
      enPlataforma = false;
      tiempoEnAire = 0f;
      turbo = false;
      turboTime = 0f;
      penetration = 0f;
      Rotation = 0f;

      //Rotaciones de Ruedas
      frontWheelRotationIA = new float[]
      {
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
      };
      WheelRotationPrincipal = 0f;
      frontWheelRotation = 0f;

      CarsSpeeds = new float[]
      {
                75f,
                75f,
                75f,
                75f,
                75f,
                50f,
                50f,
                50f,
      };

      DesplazamientoAutos = new Vector3[]{
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero,
        Vector3.Zero
      };

      AutosRotaciones = new float[]{
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f
      };



      //Listas
      

      autoMenu = Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(-MathF.PI/2) *  Matrix.CreateTranslation(autoMenuPos);
      autoMenu2 = Matrix.CreateScale(0.01f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0,0,130);
      autoMenu3 = Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(mediaVuelta) *  Matrix.CreateTranslation(0,-100,130);

      AutoPrincipalWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(AutoPrincipalPos);

      Spawns = new Vector3[]{
        new Vector3(650,0,-400),
        new Vector3(590,0,515),
        new Vector3(-500,0,180), 
        new Vector3(-200,0,-600), 
        new Vector3(230,0,700)
      };

      
      
      for(int i = 0; i < cantidadEnemigos; i++)
      {
        AutosPosiciones[i] = obtenerSpawn();
      }

      for(int i = 0; i < cantidadEnemigos; i++)
      {
        if(i < 5)
          AutosWorld[i] = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(AutosPosiciones[i]);
        else
          AutosWorld[i] = Matrix.CreateScale(0.007f)  * Matrix.CreateTranslation(AutosPosiciones[i]);
      }


      AutosDirecciones = new Vector3[cantidadEnemigos];
      for(int i = 0; i < cantidadEnemigos; i++)
      {
          AutosDirecciones[i] = Vector3.Normalize(AutosWorld[i].Forward);
      }

      vidaAutos = new int[cantidadEnemigos];
      for(int i = 0; i < cantidadEnemigos; i++)
      {
        if(i < 6)
          vidaAutos[i] = 100;
        else
          vidaAutos[i] = 200;
      }  

      timersRespawn = new float[cantidadEnemigos];
      for(int i = 0; i < cantidadEnemigos; i++)
        timersRespawn[i] = 0f;

      autosDestruidos = new List<int>();

      IAenPlataforma = new Boolean[cantidadEnemigos];
        for(int i = 0; i < cantidadEnemigos; i++)
          IAenPlataforma[i] = false;

        IAenPiso = new Boolean[cantidadEnemigos];
        for(int i = 0; i < cantidadEnemigos; i++)
          IAenPiso[i] = true;  
        
        modoDios = false;
        timerInvencibilidad = 0f;
        serInvencible = false;
      }

      public bool victoriaPorKills()
      {
        return (BajasBalas + BajasMisil >= 10);
      }

      public void godMode(){
        modoDios = true;
      }
  }
}