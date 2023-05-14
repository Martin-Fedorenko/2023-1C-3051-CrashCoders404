using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;

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
        private float CarBrakes;
        private float Rozamiento;
        private float Rotation;
        private bool ActiveMovement;
        private Boolean onJump;
        private Boolean onDescend;
        private Boolean accelerating;
        private float jumpRotation;
        private float jumpAcceleration;
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

        private Matrix[] AutosWorld;
        private Vector3[] AutosPosiciones;

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
       
       //Texturas
       private List<Texture2D> ColorTextures { get; set; }

        //Colisiones
        private BoundingBox AutoDeportivoBoxAABB;
        private BoundingBox AutoDeCombateBoxAABB;
        private OrientedBoundingBox AutoPrincipalBox;
        private OrientedBoundingBox[] CollideCars;
        private Boolean collided;
        private int CollisionIndex;
        
      public void Initialize()
     {
       //MovimientoAuto
            CarSpeed = 0f;
            CarAcceleration = 200f;
            CarBrakes = 400f;
            Rozamiento = -500f;
            ActiveMovement = false;
            jumpAngle = MathF.PI / 9;
            jumpAcceleration = 5f;
            gravity = 5f;
            onJump = false;
            onDescend = false;
            accelerating = false;
            jumpHeight = 100f;
            maxSpeed = 2800f;

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


            //Listas
            ColorTextures = new List<Texture2D>();

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

        public void LoadContent(Model Auto1,Model Auto2,Effect effect)
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
            
            //Texturas
            foreach(var mesh in AutoDeportivo.Meshes)
            {
                foreach(var meshPart in mesh.MeshParts)
                {
                    ColorTextures.Add(((BasicEffect)meshPart.Effect).Texture);
                    meshPart.Effect= effect;
                }
            } 

           foreach(var mesh in AutoDeCombate.Meshes)
            {
                foreach(var meshPart in mesh.MeshParts)
                {
                    ColorTextures.Add(((BasicEffect)meshPart.Effect).Texture);
                    meshPart.Effect= effect;
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
            collided = false;
           
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !onJump)
            {
                if (CarSpeed < 0) //De esta manera si estaba yendo hacia atras, frena y luego acelera hacia delante
                {
                    CarSpeed += CarBrakes * elapsedTime;
                }
                else if (CarSpeed < maxSpeed) CarSpeed += CarAcceleration * elapsedTime;
                Desplazamiento += CarDirection * CarSpeed * elapsedTime + CarAcceleration * elapsedTime * elapsedTime * CarDirection / 2f;
                ActiveMovement = true;
                accelerating = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !onJump)
            {
                if (CarSpeed > 0) //De esta manera si estaba yendo hacia delante, frena y luego acelera hacia atras
                {
                    CarSpeed -= CarBrakes * elapsedTime;
                }
                else if (CarSpeed > -maxSpeed) CarSpeed -= CarAcceleration * elapsedTime;
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
                    Desplazamiento -= Vector3.UnitY * gravity;
                  
                }
                else if (AutoPrincipalPos.Y <= jumpHeight && onDescend) //debi usar <= en lugar de ==, ya que hay veces que el auto termina parte bajo el suelo, y no permitia otros movimientos del auto
                {
                    jumpRotation = 0f;
                    onDescend = false;
                    onJump = false;
                }
            }

           // collided = AutoPrincipalBox.Intersects(Auto1Box);
            AutoPrincipalPos += Desplazamiento;

            Vector3 postChoque = CarDirection*CarSpeed/5;
            
            for(var index = 0; index < CollideCars.Length; index++)
            {
                if(AutoPrincipalBox.Intersects(CollideCars[index]))
                {
                  //  collided = true;
                  // CollisionIndex = index;
                    
                    AutoPrincipalPos -= postChoque;
                    AutosPosiciones[index] += postChoque;

                    CarSpeed = 0;
                }
            }


            AutoPrincipalWorld =  Matrix.CreateScale(0.1f) *
                                  Matrix.CreateRotationX(-jumpRotation) *
                                  Matrix.CreateRotationY(Rotation*2) *
                                  Matrix.CreateTranslation(AutoPrincipalPos);

            for(var index = 0; index < CollideCars.Length; index++)
            {
                if(index < 5)
                {
                AutosWorld[index] = Matrix.CreateScale(0.1f) *
                                    Matrix.CreateRotationY(mediaVuelta) *
                                    Matrix.CreateTranslation(AutosPosiciones[index]);
                }
                else
                {
                AutosWorld[index] = Matrix.CreateScale(0.007f) *
                                    Matrix.CreateRotationY(cuartoDeVuelta) *
                                    Matrix.CreateTranslation(AutosPosiciones[index]);
                }

                 CollideCars[index] = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min +  AutosPosiciones[index], AutoDeportivoBoxAABB.Max +  AutosPosiciones[index]));

            }

            AutoPrincipalBox = OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutoPrincipalPos, AutoDeportivoBoxAABB.Max + AutoPrincipalPos));
            AutoPrincipalBox.Rotate(Matrix.CreateRotationX(-jumpRotation)*Matrix.CreateRotationY(Rotation*2));
    
           
      }
      public void dibujarAuto(Matrix view,Matrix projection,Effect effect,Model modelo,float WheelRot,Matrix matrizMundo)
        {
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);

            relativeMatrices = new Matrix[modelo.Bones.Count];
            modelo.CopyAbsoluteBoneTransformsTo(relativeMatrices);
            int index = 0;
            foreach (var mesh in modelo.Meshes)
            {
                relativeMatrices[rightFrontWheelBone.Index] = Matrix.CreateRotationY(WheelRot) * rightFrontWheelTransform;
                relativeMatrices[leftFrontWheelBone.Index] = Matrix.CreateRotationY(WheelRot) * leftFrontWheelTransform;
                effect.Parameters["World"].SetValue(relativeMatrices[mesh.ParentBone.Index]*matrizMundo);
                foreach(var meshPart in mesh.MeshParts)
                {
                    effect.GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
                    effect.GraphicsDevice.Indices = meshPart.IndexBuffer;
                    effect.Parameters["ModelTexture"].SetValue(ColorTextures[index]);
                    foreach(var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        effect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,meshPart.VertexOffset,meshPart.StartIndex,meshPart.PrimitiveCount);
                    }
                }
                mesh.Draw();
                index++;
            }
        }

        public void dibujarAutos(Matrix view,Matrix projection,Effect effect)
        {  
            Model modeloAuto;
            dibujarAuto(view, projection, effect, AutoDeportivo,WheelRotationPrincipal,AutoPrincipalWorld);

            for(int index = 0; index < AutosWorld.Length; index++)
            {
               
                    if(index < 5) modeloAuto = AutoDeportivo;
                    else modeloAuto = AutoDeCombate;
                    dibujarAuto(view,projection,effect,modeloAuto,WheelRotationWorld[index],AutosWorld[index]);
                
            }
        }

        public Vector3 posAutoPrincipal()
        {
            return AutoPrincipalPos;
        }
        public void inicializarBoundingBoxes()
        {
            AutoDeportivoBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeportivo);
            AutoDeportivoBoxAABB = BoundingVolumesExtensions.Scale(AutoDeportivoBoxAABB,0.1f);
            AutoDeCombateBoxAABB = BoundingVolumesExtensions.CreateAABBFrom(AutoDeCombate);
            AutoDeCombateBoxAABB = BoundingVolumesExtensions.Scale(AutoDeCombateBoxAABB,0.007f);
            
            AutoPrincipalBox =  OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + AutoPrincipalPos, AutoDeportivoBoxAABB.Max + AutoPrincipalPos));
            
            CollideCars = new OrientedBoundingBox[] 
            {
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto1Pos, AutoDeportivoBoxAABB.Max + Auto1Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto2Pos, AutoDeportivoBoxAABB.Max + Auto2Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto3Pos, AutoDeportivoBoxAABB.Max + Auto3Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto4Pos, AutoDeportivoBoxAABB.Max + Auto4Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeportivoBoxAABB.Min + Auto5Pos, AutoDeportivoBoxAABB.Max + Auto5Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto6Pos, AutoDeCombateBoxAABB.Max + Auto6Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto7Pos, AutoDeCombateBoxAABB.Max + Auto7Pos)),
            OrientedBoundingBox.FromAABB(new BoundingBox(AutoDeCombateBoxAABB.Min + Auto8Pos, AutoDeCombateBoxAABB.Max + Auto8Pos)),
            };
        }
    }
}