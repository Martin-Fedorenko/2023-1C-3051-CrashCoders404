using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP
{
    public class Autos
    {

        public Autos() {}
        private Model AutoDeportivo { get; set; }
        private Model AutoDeCombate { get; set; }
        //MovimientoAuto
        private Vector3 CarDirection;
        private float CarSpeed;
        private float CarAcceleration;
        private float Rozamiento;
        private float Rotation;
        private bool ActiveMovement;
        private Boolean onJump;
        private Boolean onDescend;
        private Boolean accelerating;
        private float jumpRotation;
        private float jumpAcceleration;
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

        // Variables
        private float mediaVuelta = MathF.PI;
        private float cuartoDeVuelta = MathF.PI / 2;
        private Vector3 AutoPrincipalPos = new Vector3(0, 0, 0);
        private Vector3 Auto1Pos = new Vector3(0, 0, 70);
        private Vector3 Auto2Pos =new Vector3( 30, 0, 70);
        private Vector3 Auto3Pos = new Vector3(60, 0, 70);
        private Vector3 Auto4Pos = new Vector3 (-30, 0, 70);
        private Vector3 Auto5Pos = new Vector3(-60, 0, 70);
        private Vector3 Auto6Pos = new Vector3(0, 0, 180);
        private Vector3 Auto7Pos = new Vector3(50, 0, 180);
        private Vector3 Auto8Pos = new Vector3(-50, 0, 180);        
        private Vector3 Desplazamiento;
       
        
      public void Initialize()
     {
       //MovimientoAuto
            CarSpeed = 0f;
            CarAcceleration = 200f;
            Rozamiento = -100f;
            ActiveMovement = false;
            jumpAngle = MathF.PI / 9;
            jumpAcceleration = 5f;
            onJump = false;
            onDescend = false;
            accelerating = false;
            jumpHeight = 100f;
            maxSpeed = 2000f;

             //Rotaciones de Ruedas
            WheelRotationPrincipal = 0f;
            WheelRotation1 = 0f;
            WheelRotation2 = 0f;
            WheelRotation3 = 0f;
            WheelRotation4 = 0f;
            WheelRotation5 = 0f;
            WheelRotation6 = 0f;
            WheelRotation7 = 0f;
            WheelRotation8 = 0f;

            AutoPrincipalWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(AutoPrincipalPos);

            Auto1World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto1Pos);
            Auto2World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto2Pos);
            Auto3World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto3Pos);
            Auto4World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto4Pos);
            Auto5World = Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(mediaVuelta) * Matrix.CreateTranslation(Auto5Pos);

            AutoCombate1World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto6Pos);
            AutoCombate2World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto7Pos);
            AutoCombate3World = Matrix.CreateScale(0.007f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Auto8Pos);
        }

        public void LoadContent(Model Auto1,Model Auto2)
        {
            AutoDeportivo = Auto1;
            AutoDeCombate = Auto2;

            leftBackWheelBone = AutoDeportivo.Bones["WheelD"];
            rightBackWheelBone = AutoDeportivo.Bones["WheelC"];
            leftFrontWheelBone = AutoDeportivo.Bones["WheelA"];
            rightFrontWheelBone = AutoDeportivo.Bones["WheelB"];

            leftBackWheelTransform = leftBackWheelBone.Transform;
            rightBackWheelTransform = rightBackWheelBone.Transform;
            leftFrontWheelTransform = leftFrontWheelBone.Transform;
            rightFrontWheelTransform = rightFrontWheelBone.Transform;


        }

      public void Update(GameTime gameTime)
      {
            var keyboardState = Keyboard.GetState();
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CarDirection = AutoPrincipalWorld.Backward;
            Desplazamiento = Vector3.Zero;
           
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !onJump)
            {
                if (CarSpeed < maxSpeed) CarSpeed += CarAcceleration * elapsedTime;
                Desplazamiento += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
                ActiveMovement = true;
                accelerating = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !onJump)
            {
                if (CarSpeed > -maxSpeed) CarSpeed -= CarAcceleration * elapsedTime;
                Desplazamiento += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
                ActiveMovement = true;
                accelerating = true; 
            }

            if ((keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.W)) || onJump)
            {
                accelerating = false;
                if (CarSpeed > 0)
                {
                    CarSpeed += Rozamiento * elapsedTime;
                    ActiveMovement = true;
                }
                else if (CarSpeed < 0)
                {
                    CarSpeed -= Rozamiento * elapsedTime;
                    ActiveMovement = true;
                }
                Desplazamiento += CarDirection * CarSpeed * elapsedTime;
            
            }
            //rotar
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if(CarSpeed >= 0 && WheelRotationPrincipal > -MathF.PI * 1 / 6) WheelRotationPrincipal -= elapsedTime;
                else if (CarSpeed < 0 && WheelRotationPrincipal < MathF.PI * 1 / 6) WheelRotationPrincipal += elapsedTime;
                if(ActiveMovement) Rotation -= elapsedTime;
         //NO DEBERIA PODER GIRAR AL ESTAR QUIETO
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (CarSpeed >= 0 && WheelRotationPrincipal < MathF.PI * 1 / 6) WheelRotationPrincipal += elapsedTime;
                else if (CarSpeed < 0 && WheelRotationPrincipal > -MathF.PI * 1 / 6) WheelRotationPrincipal -= elapsedTime;
                if(ActiveMovement) Rotation += elapsedTime;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                WheelRotationPrincipal = 0f;//cuando soltas W o A el auto y las ruedas siguen recto
            }
            //saltar
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || onJump) && !accelerating)
            {
                onJump = true;
                if (CarSpeed >= 0)
                {
                    if (!onDescend && jumpRotation < jumpAngle) jumpRotation += 0.05f;
                    if (onDescend && jumpRotation > -jumpAngle) jumpRotation -= 0.05f;
                }
                else if (CarSpeed < 0)
                {
                    if (!onDescend && jumpRotation > -jumpAngle) jumpRotation -= 0.05f;
                    if (onDescend && jumpRotation < jumpAngle) jumpRotation += 0.05f;
                }
                if (AutoPrincipalPos.Y <= jumpHeight && !onDescend)
                {
                    Desplazamiento += Vector3.UnitY * jumpAcceleration;
                 
                }
                else if (AutoPrincipalPos.Y >= jumpHeight && !onDescend) onDescend = true;
                else if (AutoPrincipalPos.Y >= 5 && onDescend)
                {
                    Desplazamiento -= Vector3.UnitY * jumpAcceleration;
                  
                }
                else if (AutoPrincipalPos.Y <= jumpHeight && onDescend) //debi usar <= en lugar de ==, ya que hay veces que el auto termina parte bajo el suelo, y no permitia otros movimientos del auto
                {
                    jumpRotation = 0f;
                    onDescend = false;
                    onJump = false;
                }
            }
            AutoPrincipalPos += Desplazamiento;
            AutoPrincipalWorld = Matrix.CreateScale(0.1f) *
                                  Matrix.CreateRotationX(-jumpRotation) *
                                  Matrix.CreateRotationY(Rotation*2) *
                                  Matrix.CreateTranslation(AutoPrincipalPos);
      }
      public void dibujarAuto(Matrix view,Matrix projection,Effect effect,Model modelo,Color color,float WheelRot,Matrix matrizMundo)
        {
            foreach (var mesh in AutoDeportivo.Meshes)
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
                relativeMatrices[rightFrontWheelBone.Index] = Matrix.CreateRotationY(WheelRot) * rightFrontWheelTransform;
                relativeMatrices[leftFrontWheelBone.Index] = Matrix.CreateRotationY(WheelRot) * leftFrontWheelTransform;
                effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index]*matrizMundo);
                mesh.Draw();
            }
        }

        public void dibujarAutos(Matrix view,Matrix projection,Effect effect)
        {
            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Red,WheelRotationPrincipal,AutoPrincipalWorld);

            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Black,WheelRotation1,Auto1World);
            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Black,WheelRotation2,Auto2World);
            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Black,WheelRotation3,Auto3World);
            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Black,WheelRotation4,Auto4World);
            dibujarAuto(view, projection, effect, AutoDeportivo, Color.Black,WheelRotation5,Auto5World);

            dibujarAuto(view, projection, effect, AutoDeCombate, Color.DarkGray,WheelRotation6,AutoCombate1World);
            dibujarAuto(view, projection, effect, AutoDeCombate, Color.DarkGray,WheelRotation7,AutoCombate2World);
            dibujarAuto(view, projection, effect, AutoDeCombate, Color.DarkGray,WheelRotation8,AutoCombate3World);
        }

        public Vector3 posAutoPrincipal()
        {
            return AutoPrincipalPos;
        }
    }
}