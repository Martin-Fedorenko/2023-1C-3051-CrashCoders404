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

    private SistemaDeVida vida { get; set; }

    //MovimientoAuto
    public Vector3 CarDirection;
    public Vector2 CarSpeed;
    public float CarAcceleration;
    private float CarBrakes;
    private float Rozamiento;
    public float Rotation;
    private bool ActiveMovement;
    private Boolean onJump;
    private Boolean accelerating;
    private float jumpRotation;
    private float jumpSpeed;
    private float jumpPower;
    private float gravity;
    private float jumpAngle;
    private float jumpHeight;
    private float maxSpeed;
    private float WheelRotationPrincipal;
    private float WheelRotation1;
    private float WheelRotation2;
    private float WheelRotation3;
    private float WheelRotation4;
    private float WheelRotation5;
    private float WheelRotation6;
    private float WheelRotation7;
    private float WheelRotation8;
    private ModelBone leftBackWheelBone;
    private ModelBone rightBackWheelBone;
    private ModelBone leftFrontWheelBone;
    private ModelBone rightFrontWheelBone;
    private Matrix leftBackWheelTransform = Matrix.Identity;
    private Matrix rightBackWheelTransform = Matrix.Identity;
    private Matrix leftFrontWheelTransform = Matrix.Identity;
    private Matrix rightFrontWheelTransform = Matrix.Identity;

    private float[] WheelRotationWorld;
    private Matrix[] relativeMatrices;

    //Matrices
    private Matrix AutoPrincipalWorld { get; set; }
    private Matrix Auto1World { get; set; }
    private Matrix Auto2World { get; set; }
    private Matrix Auto3World { get; set; }
    private Matrix Auto4World { get; set; }
    private Matrix Auto5World { get; set; }
    private Matrix AutoCombate1World { get; set; }
    private Matrix AutoCombate2World { get; set; }
    private Matrix AutoCombate3World { get; set; }

    private int cantidadEnemigos = 7;
    private Matrix[] AutosWorld;
    public Vector3[] AutosPosiciones;

    private float[] CarsSpeeds;
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
    private Vector3[] DesplazamientoAutos;

    public Vector2[] AutosVelocidades;
    private Boolean enElPiso;
    private Boolean enPlataforma;
    float tiempoEnAire;

    //Texturas
    private List<Texture2D> ColorTextures { get; set; }

    //Colisiones
    private BoundingBox AutoDeportivoBoxAABB;
    private BoundingBox AutoDeCombateBoxAABB;
    private OrientedBoundingBox AutoPrincipalBox;
    private Matrix AutoPrincipalOBBWorld;
    private Matrix[] AutosOBBWorld;
    public OrientedBoundingBox[] CollideCars;
    private Boolean collided;
    private int CollisionIndex;
    private float PreviousSpeed;
    private float pesoAuto;
    private int collidedCars;
    private Boolean turbo;
    private float turboTime;
    private Vector3 direccionPostChoque;
    private Vector3 direccionChoqueBB = Vector3.Zero;
    private float penetration;
    private float alturaPlataforma = 0f;

    Vector3 coreccionAltura = new Vector3(0, 66f, 0); //el centro de la oriented bounding box esta quedando muy arriba
    Vector3 coreccionAlturaAutoCombate = new Vector3(199, 4244f, -443); //(3,-20f,0);

    //Menu
    private Vector3 autoMenuPos = new Vector3(0,0,-130);
    private Matrix autoMenu;
    private Matrix autoMenu2;
    private Matrix autoMenu3;

    //Sonidos
    private SoundEffectInstance Instance { get; set; }
    private SoundEffect CarCrash {get; set;}
    private int acabaDeChocar = 0;
    private bool choco;
    public void Initialize()
    {
      //MovimientoAuto
      CarSpeed = new Vector2(0f,0f);
      AutosVelocidades = new Vector2[]
      {
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f),
            new Vector2(0f,0f)
      };
      

      CarAcceleration = 200f;
      CarBrakes = 400f;
      ActiveMovement = false;
      jumpAngle = MathF.PI / 9;
      jumpSpeed = 30f;
      gravity = 2f;
      onJump = false;
      accelerating = false;
      jumpHeight = 100f;
      maxSpeed = 2800f;
      enElPiso = true;
      enPlataforma = false;
      tiempoEnAire = 0f;
      turbo = false;
      turboTime = 0;
      penetration = 0f;

      //Rotaciones de Ruedas
      WheelRotationWorld = new float[]
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

      CarsSpeeds = new float[]
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
      //Listas
      ColorTextures = new List<Texture2D>();

      autoMenu = Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(-MathF.PI/2) *  Matrix.CreateTranslation(autoMenuPos);
      autoMenu2 = Matrix.CreateScale(0.01f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(0,0,130);
      autoMenu3 = Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(mediaVuelta) *  Matrix.CreateTranslation(0,-100,130);

      AutoPrincipalWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(AutoPrincipalPos);

      AutosPosiciones = new Vector3[]
      {
            Auto1Pos,
            Auto2Pos,
            Auto3Pos,
            Auto4Pos,
            Auto5Pos,
            Auto6Pos,
            Auto7Pos,
            Auto8Pos
      };

      AutosWorld = new Matrix[]
      {
            Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto1Pos),
            Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto2Pos),
            Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto3Pos),
            Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto4Pos),
            Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto5Pos),

            Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto6Pos),
            Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto7Pos),
            Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto8Pos),
      };

    }

    public void LoadContent(Model Auto1, Model Auto2, Effect effect, SoundEffect carCrash)
    {
      AutoDeportivo = Auto1;
      AutoDeCombate = Auto2;
      CarCrash = carCrash;

      leftBackWheelBone = AutoDeportivo.Bones["WheelD"];
      rightBackWheelBone = AutoDeportivo.Bones["WheelC"];
      leftFrontWheelBone = AutoDeportivo.Bones["WheelA"];
      rightFrontWheelBone = AutoDeportivo.Bones["WheelB"];

      leftBackWheelTransform = leftBackWheelBone.Transform;
      rightBackWheelTransform = rightBackWheelBone.Transform;
      leftFrontWheelTransform = leftFrontWheelBone.Transform;
      rightFrontWheelTransform = rightFrontWheelBone.Transform;

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

    public void Update(GameTime gameTime)
    {
      var keyboardState = Keyboard.GetState();
      var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
      CarDirection = AutoPrincipalWorld.Backward;
      Desplazamiento = Vector3.Zero;
      Rozamiento = CarSpeed.X * 0.5f;
      collided = false;
      collidedCars = 0;
      choco = false;

      for(int index = 0 ; index < DesplazamientoAutos.Length; index++)
      {
        DesplazamientoAutos[index] = Vector3.Zero;
      }


      for(int index = 0; index < CarsSpeeds.Length; index++)
      {
        if (CarsSpeeds[index] > 3)
        {
          CarsSpeeds[index] += Rozamiento * elapsedTime;
        }
        else if (CarsSpeeds[index] < -3) //La velocidad nunca acaba siendo menor al |3|, de esta forma podemos saber si el auto se queda "quieto" y podemos evitar que gire sin avanzar/retroceder
        {
          CarsSpeeds[index] -= Rozamiento * elapsedTime;
        }

      }

      if (Keyboard.GetState().IsKeyDown(Keys.W) && !onJump)
      {
        if (CarSpeed.X < 0) //De esta manera si estaba yendo hacia atras, frena y luego acelera hacia delante
        {
          CarSpeed.X += CarBrakes * elapsedTime;
        }
        else if (CarSpeed.X < maxSpeed) CarSpeed.X += CarAcceleration * elapsedTime;
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime + CarAcceleration * elapsedTime * elapsedTime* CarDirection / 2f;
        ActiveMovement = true;
        accelerating = true;

      }
      else if (Keyboard.GetState().IsKeyDown(Keys.S) && !onJump)
      {
        if (CarSpeed.X >= 0) //De esta manera si estaba yendo hacia delante, frena y luego acelera hacia atras
        {
          CarSpeed.X -= CarBrakes * elapsedTime;
        }
        else if (CarSpeed.X > -maxSpeed) CarSpeed.X -= CarAcceleration * elapsedTime;
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime + CarAcceleration * elapsedTime* elapsedTime * CarDirection / 2f;
        ActiveMovement = true;
        accelerating = true;
      }

      if ((keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.W)) || onJump)
      {
        accelerating = false;

        if(CarSpeed.X < 3 && CarSpeed.X > -3)
        {
          CarSpeed.X = 0f;
        }      
        else
        {
          CarSpeed.X -= Rozamiento * elapsedTime;
        }
        Desplazamiento += CarDirection * CarSpeed.X * elapsedTime;
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
          if(MathF.Abs(CarSpeed.X) < 2800)
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


      // collided = AutoPrincipalBox.Intersects(Auto1Box);
      for (var index = 0; index < CollideCars.Length; index++)
      {
        if (index < 5) pesoAuto = 250;
        else pesoAuto = 450;
        if (AutoPrincipalBox.Intersects(CollideCars[index]))
        {
            CollisionIndex = index;
            direccionPostChoque = CarDirection;
            Desplazamiento*=-1;
            CarsSpeeds[CollisionIndex] = CarSpeed.X * 0.5f;
            CarSpeed*=-0.5f;

            
          if(acabaDeChocar == 0)
          {
            acabaDeChocar = 1;
            Instance = CarCrash.CreateInstance();
            Instance.Play();
          }
          choco = true;
        }
      }

      AutoPrincipalPos += Desplazamiento;
      if(CarsSpeeds[CollisionIndex] > 3 || CarsSpeeds[CollisionIndex] < -3){
          DesplazamientoAutos[CollisionIndex] += direccionPostChoque * CarsSpeeds[CollisionIndex] * elapsedTime;
      }

      for(int index = 0; index < DesplazamientoAutos.Length; index++)
        {
          AutosPosiciones[index] += DesplazamientoAutos[index];
        }

      AutoPrincipalWorld =  Matrix.CreateScale(0.1f) *
                            Matrix.CreateRotationX(-jumpRotation) *
                            Matrix.CreateRotationY(Rotation * 2) *
                            Matrix.CreateTranslation(AutoPrincipalPos);

      for (var index = 0; index < CollideCars.Length; index++)
      {
        if (index < 5)
        {
          AutosWorld[index] = Matrix.CreateScale(0.1f) *
                              Matrix.CreateRotationY(mediaVuelta) *
                              Matrix.CreateTranslation(AutosPosiciones[index]);

          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutosPosiciones[index] - coreccionAltura, AutoDeportivoBoxAABB.Max + AutosPosiciones[index] - coreccionAltura));
        }
        else
        {
          AutosWorld[index] = Matrix.CreateScale(0.007f) *
                              Matrix.CreateRotationY(cuartoDeVuelta) *
                              Matrix.CreateTranslation(AutosPosiciones[index]);

          CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + AutosPosiciones[index] - coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + AutosPosiciones[index] - coreccionAlturaAutoCombate));
        }
      }


      AutoPrincipalBox = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutoPrincipalPos - coreccionAltura, AutoDeportivoBoxAABB.Max + AutoPrincipalPos - coreccionAltura));
      AutoPrincipalBox.Rotate(Matrix.CreateRotationX(-jumpRotation) * Matrix.CreateRotationY(Rotation * 2));

      if (!choco) {acabaDeChocar = 0;}


      //LOGICA IA
      
      /*for(int i = 0; i < AutosVelocidades.Length; i++)
      {
        if(AutosPosiciones[i].X != AutoPrincipalPos.X)
        {
          if (AutosVelocidades[i].X < maxSpeed) 
          {
            AutosVelocidades[i].X += CarAcceleration * elapsedTime;
            DesplazamientoAutos[i] += CarDirection * AutosVelocidades[i].X * elapsedTime + CarDirection * elapsedTime * CarDirection / 2f;
            
            AutosPosiciones[i].X += DesplazamientoAutos[i].X;
          }
        }
      }*/
    }

    public void dibujarAuto(Matrix view, Matrix projection, Effect effect, Model modelo, float WheelRot, Matrix matrizMundo)
    {
      effect.Parameters["View"].SetValue(view);
      effect.Parameters["Projection"].SetValue(projection);
      effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Transpose(Matrix.Invert(matrizMundo)));
      

      relativeMatrices = new Matrix[modelo.Bones.Count];
      
      int index = 0;
      foreach (var mesh in modelo.Meshes)
      {
        rightFrontWheelBone.Transform = Matrix.CreateRotationY(WheelRot) * rightFrontWheelTransform;
        leftFrontWheelBone.Transform = Matrix.CreateRotationY(WheelRot) * leftFrontWheelTransform;
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

    public void dibujarAutosMenu(Matrix view, Matrix projection, Effect effect)
    {
      effect.CurrentTechnique = effect.Techniques["Luz"];
       dibujarAuto(view, projection, effect, AutoDeportivo, 0, autoMenu);
       dibujarAuto(view, projection, effect, AutoDeCombate, 0, autoMenu2);
       dibujarAuto(view, projection, effect, AutoDeportivo, 0, autoMenu3);
    }
    public void dibujarAutos(Matrix view, Matrix projection, Effect effect, String tecnica)
    {
      Model modeloAuto;
      effect.CurrentTechnique = effect.Techniques[tecnica];
      dibujarAuto(view, projection, effect, AutoDeportivo, WheelRotationPrincipal, AutoPrincipalWorld);

      //dibujarAuto(view, projection, effect, AutoDeportivo, WheelRotationPrincipal, AutoPrincipalWorld);

      for (int index = 0; index < AutosWorld.Length; index++)
      {

        if (index < 5) modeloAuto = AutoDeportivo;
        else modeloAuto = AutoDeCombate;
        dibujarAuto(view, projection, effect, modeloAuto, WheelRotationWorld[index], AutosWorld[index]);

      }
    }

    public Vector3 posAutoPrincipal()
    {
      return AutoPrincipalPos;
    }

    public Vector3 directionAutoPrincipal()
    {
      var martiz = Matrix.CreateRotationY(Rotation * 2f);
      Vector3 vector = new Vector3(0,0,1);
      Vector3 vector2 = Vector3.Transform(vector, martiz);
      return Vector3.Normalize(vector2);
    }

    public OrientedBoundingBox getAutoPrincipalBox()
    {
      return AutoPrincipalBox;
    }

    public void FrenarAuto()
    {
      CarSpeed.X = 0;
    }
    public float autoSpeed(){
      return CarSpeed.X;
    }
    public float prevSpeed(){
      return PreviousSpeed;
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
    public void chocarTecho()
    {
      CarSpeed.Y = 0f;
    }
    public Vector3 posAutoMenu(){
      return autoMenuPos;
    }
    public void rebotarAuto()
    {
      CarSpeed *= -1;
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

    public int cantEnemigos()
    {
      return cantidadEnemigos;
    }

    public OrientedBoundingBox[] getPosAutos()
    {
      return CollideCars;
    }
    public void inicializarBoundingBoxes()
    {
      AutoDeportivoBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeportivo);
      AutoDeportivoBoxAABB = BoundingVolumesExtensions.Scale(AutoDeportivoBoxAABB, 0.1f);
      AutoDeCombateBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeCombate);
      AutoDeCombateBoxAABB = BoundingVolumesExtensions.Scale(AutoDeCombateBoxAABB, new Vector3(0.0042f, 0.004f, 0.011f));

      AutoPrincipalBox = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min - coreccionAltura, AutoDeportivoBoxAABB.Max - coreccionAltura));

      CollideCars = new OrientedBoundingBox[]
      {
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto1Pos - coreccionAltura, AutoDeportivoBoxAABB.Max + Auto1Pos - coreccionAltura)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto2Pos - coreccionAltura, AutoDeportivoBoxAABB.Max + Auto2Pos - coreccionAltura)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto3Pos - coreccionAltura, AutoDeportivoBoxAABB.Max + Auto3Pos - coreccionAltura)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto4Pos - coreccionAltura, AutoDeportivoBoxAABB.Max + Auto4Pos - coreccionAltura)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto5Pos - coreccionAltura, AutoDeportivoBoxAABB.Max + Auto5Pos - coreccionAltura)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto6Pos- coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + Auto6Pos  - coreccionAlturaAutoCombate)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto7Pos- coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + Auto7Pos - coreccionAlturaAutoCombate)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto8Pos- coreccionAlturaAutoCombate, AutoDeCombateBoxAABB.Max + Auto8Pos - coreccionAlturaAutoCombate)),
      };
    }

    public void dibujarBoundingBoxes(Gizmos gizmos)
    {
      AutoPrincipalOBBWorld = Matrix.CreateScale(AutoPrincipalBox.Extents * 2f) * AutoPrincipalBox.Orientation * Matrix.CreateTranslation(AutoPrincipalBox.Center);
      gizmos.DrawCube(AutoPrincipalOBBWorld, Color.Red);

      for (int index = 0; index < CollideCars.Length; index++)
      {
        Matrix OBBWorld = Matrix.CreateScale(CollideCars[index].Extents * 2f) * CollideCars[index].Orientation * Matrix.CreateTranslation(CollideCars[index].Center);
        gizmos.DrawCube(OBBWorld, Color.Red);
      }
    }

  }
}
