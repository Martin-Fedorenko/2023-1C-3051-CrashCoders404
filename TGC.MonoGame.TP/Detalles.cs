using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.Viewer.Gizmos;

namespace TGC.MonoGame.TP
{
    public class Detalles
    {
        public Detalles() {}

        private Autos autos;
        private Model Tree { get; set; }
        private Model Rock1 { get; set; }
        private Model Rock5 { get; set; }
        private Model Rock10 { get; set; }
        private Model Tire { get; set; }

        private Matrix[] TreesWorld;
        private Matrix[] Rocks1World;
        private Matrix[] Rocks5World;
        private Matrix[] Rocks10World;
        private Matrix[] TiresWorld;

        public Vector3 Tree1Position = new Vector3 (30, 0, 350); 
        public Vector3 Tree2Position = new Vector3 (-25, 0, 350);
        public Vector3 Tree3Position = new Vector3 (55, 0, 405);
        public Vector3 Tree4Position = new Vector3 (-60, 0, 430);
        public Vector3 Tree5Position = new Vector3 (325, 0, -250);
        public Vector3 Tree6Position = new Vector3 (275, 0, -200);
        public Vector3 Tree7Position = new Vector3 (300, 0, -275);
        public Vector3 Tree8Position = new Vector3 (-325, 0, -250);
        public Vector3 Tree9Position = new Vector3 (-325, 0, -250);
        public Vector3 Tree10Position = new Vector3 (-275, 0, -250);
        public Vector3 Tree11Position = new Vector3 (-300, 0, -200);
        public Vector3 Tree12Position = new Vector3 (-300, 0, -275);

        public Vector3 Rock1Position = new Vector3 (0, 0, -250);
        public Vector3 Rock2Position = new Vector3 (25, 0, -250);
        public Vector3 Rock3Position = new Vector3 (50, 0, -250);
        public Vector3 Rock4Position = new Vector3 (75, 0, -250);
        public Vector3 Rock5Position = new Vector3 (350, 0, 100);
        public Vector3 Rock6Position = new Vector3 (350, 0, 125);     
        public Vector3 Rock7Position = new Vector3 (350, 0, 150); 
        public Vector3 Rock8Position = new Vector3 (550, 0, 150);
        public Vector3 Rock9Position = new Vector3 (15, 0, 350);
        public Vector3 Rock10Position = new Vector3 (-15, 0, 375);
        public Vector3 Rock11Position = new Vector3 (15, 0, 400);
        public Vector3 Rock12Position = new Vector3 (-15, 0, 425);
        public Vector3 Rock13Position = new Vector3 (-450, 0, 350);
        public Vector3 Rock14Position = new Vector3 (-460, 0, 375);
        public Vector3 Rock15Position = new Vector3 (-450, 0, 400);
        public Vector3 Rock16Position = new Vector3 (-460, 0, 425);
        public Vector3 Rock17Position = new Vector3 (-480, 0, 350);
        public Vector3 Rock18Position = new Vector3 (-490, 0, 375);
        public Vector3 Rock19Position = new Vector3 (-480, 0, 400);
        public Vector3 Rock20Position = new Vector3 (-490, 0, 425);
        public Vector3 Rock21Position = new Vector3 (-510, 0, 350);
        public Vector3 Rock22Position = new Vector3 (-520, 0, 375);
        public Vector3 Rock23Position = new Vector3 (-510, 0, 400);
        public Vector3 Rock24Position = new Vector3 (-520, 0, 425);
        public Vector3 Rock25Position = new Vector3 (-225, 0, 625);
        public Vector3 Rock26Position = new Vector3 (-255, 0, 650);
        public Vector3 Rock27Position = new Vector3 (-225, 0, 675);
        public Vector3 Rock28Position = new Vector3 (-255, 0, 700);
        public Vector3 Rock29Position = new Vector3 (-285, 0, 625);
        public Vector3 Rock30Position = new Vector3 (-315, 0, 650);
        public Vector3 Rock31Position = new Vector3 (-285, 0, 675);
        public Vector3 Rock32Position = new Vector3 (-315, 0, 700);

        public Vector3[] posicionRuedas;


        // Variables
        private float cuartoDeVuelta = MathF.PI / 2;

        //Bones
        private Matrix[] relativeMatrices;

        //Colisiones
        private BoundingBox TreeBox;
        private BoundingBox TireBox;
        private BoundingBox Rock1Box;
        private BoundingBox Rock5Box;
        private BoundingBox Rock5Box1;
        private BoundingBox Rock5Box2;
        private BoundingBox Rock10Box;
        private BoundingBox[] TreeBoxes;
        private BoundingBox[] Rock1Boxes;
        private BoundingBox[] Rock5Boxes;
        private BoundingBox[] Rock10Boxes;
        private BoundingBox[] TireBoxes;
        private Vector3 correctorPosicionBoxTires = new Vector3(1f,296f,33f);

        //Texturas
        private Texture2D TexturaRoca;
        private Texture2D TexturaArbol;
        private Texture2D TexturaTire1;
        private Texture2D TexturaTire2;
        
      public void Initialize()
        {
            autos = new Autos();

            //Árboles
            TreesWorld = new Matrix[]
            {
            Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree1Position),
            Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree2Position),
            Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(Tree3Position),
            Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(Tree4Position),

            Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree5Position),
            Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree6Position),
            Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree7Position),
            Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(Tree8Position),

            Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree9Position),
            Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(Tree10Position),
            Matrix.CreateScale(0.7f) * Matrix.CreateTranslation(Tree11Position),
            Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(Tree12Position)
            };


            //Piedras
            Rocks1World = new Matrix[]
            {
                Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock1Position), 
                Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock2Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock3Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateRotationY(cuartoDeVuelta) * Matrix.CreateTranslation(Rock4Position),

                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock5Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock6Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock7Position)
            };

            Rocks5World = new Matrix[]
            {
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock8Position),
                Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock9Position),
                Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock10Position),
                Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock11Position),
                Matrix.CreateScale(0.1f) * Matrix.CreateRotationY(MathF.PI / 8) * Matrix.CreateTranslation(Rock12Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock13Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock16Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock17Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock20Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock21Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock24Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock25Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock26Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock27Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock28Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock29Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock30Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock31Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock32Position)
            };

            Rocks10World = new Matrix[]
            {
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock14Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock15Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock18Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock19Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock22Position),
                Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(Rock23Position)
            };


            //Tires
            posicionRuedas = new Vector3[]
            {
                  new Vector3 (600, 5, 600),
                  new Vector3 (600, 5, 660),
                  new Vector3 (570, 10, 600),

                  new Vector3 (540, 5, 600),
                  new Vector3 (570, 5, 660),
                  new Vector3 (540, 5, 660),

                  new Vector3 (630, 5, 585),
                  new Vector3 (660, 5, 570),
                  new Vector3 (690, 5, 555),
                  new Vector3 (690, 5, 525),
                  new Vector3 (690, 5, 495),

                

                  new Vector3 (630, 5, 645),
                  new Vector3 (660, 5, 630),
                  new Vector3 (690, 5, 615),
                  new Vector3 (720, 5, 600),
                  new Vector3 (750, 5, 585),
                  new Vector3 (750, 5, 555),
                  new Vector3 (750, 5, 525),
                  new Vector3 (750, 5, 495),

                //Circulo de ruedas
                  new Vector3 (-400, 5, -500),

                  new Vector3 (-415, 5, -500),
                  new Vector3 (-430, 5, -500),
                  new Vector3 (-385, 5, -500),
                  new Vector3 (-370, 5, -500),
                  new Vector3 (-400, 5, -515),
                  new Vector3 (-400, 5, -530),
                  new Vector3 (-400, 5, -485),
                  new Vector3 (-400, 5, -470),
                
                  new Vector3 (-415, 5, -515),
                  new Vector3 (-415, 5, -485),
                  new Vector3 (-385, 5, -515),
                  new Vector3 (-385, 5, -485),

                //Circulo de ruedas
                  new Vector3 (30, 5, -600),
                  new Vector3 (45, 5, -600),
                  new Vector3 (60, 5, -600),
                  new Vector3 (15, 5, -600),
                  new Vector3 (0, 5, -600),
                  new Vector3 (30, 5, -615),
                  new Vector3 (30, 5, -630),
                  new Vector3 (30, 5, -585),
                  new Vector3 (30, 5, -570),
                  new Vector3 (45, 5, -615),
                  new Vector3 (45, 5, -585),
                  new Vector3 (15, 5, -615),
                  new Vector3 (15, 5, -585),



                //Ruedas dispersadas
                  new Vector3 (550, 5, -600),
                  new Vector3 (593, 5, -584),
                  new Vector3 (524, 5, -600),
                  new Vector3 (534, 5, -643),
                  new Vector3 (579, 5, -530),
                  new Vector3 (587, 5, -605),
                  new Vector3 (591, 5, -564),
            };

            TiresWorld = new Matrix[posicionRuedas.Length];

            for(int i = 0; i < posicionRuedas.Length; i++)
                TiresWorld[i] = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(posicionRuedas[i]);

        }

        public void LoadContent(Model tree, Model rock1, Model rock5, Model rock10, Model tire, Texture2D texturaRoca,
                                Texture2D texturaArbol, Texture2D texturaTire1, Texture2D texturaTire2)
        {
        
            Tree = tree;
            Rock1 = rock1;
            Rock5 = rock5;
            Rock10 = rock10;
            Tire = tire;
            TexturaRoca = texturaRoca;
            TexturaArbol = texturaArbol;
            TexturaTire1 = texturaTire1;
            TexturaTire2 = texturaTire2;

            Vector3 correctorPosicionBoxTree = new Vector3(3695f,350f,5875f);//Sino la bounding box aparecia en posiciones lejanas al modelo, de esta forma la llevo al origen
        

            TreeBox = BoundingVolumesExtensions.CreateAABBFrom(Tree);
            TreeBox = BoundingVolumesExtensions.Scale(TreeBox,new Vector3(0.01f,0.2f,0.01f));

            TreeBoxes = new BoundingBox[]
            {
                new BoundingBox(TreeBox.Min + Tree1Position -correctorPosicionBoxTree, TreeBox.Max + Tree1Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree2Position -correctorPosicionBoxTree, TreeBox.Max + Tree2Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree3Position - correctorPosicionBoxTree , TreeBox.Max + Tree3Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree4Position -correctorPosicionBoxTree, TreeBox.Max + Tree4Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree5Position -correctorPosicionBoxTree, TreeBox.Max + Tree5Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree6Position -correctorPosicionBoxTree, TreeBox.Max + Tree6Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree7Position -correctorPosicionBoxTree, TreeBox.Max + Tree7Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree8Position -correctorPosicionBoxTree, TreeBox.Max + Tree8Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree9Position -correctorPosicionBoxTree, TreeBox.Max + Tree9Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree10Position -correctorPosicionBoxTree, TreeBox.Max + Tree10Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree11Position -correctorPosicionBoxTree, TreeBox.Max + Tree11Position - correctorPosicionBoxTree),
                new BoundingBox(TreeBox.Min + Tree12Position -correctorPosicionBoxTree, TreeBox.Max + Tree12Position - correctorPosicionBoxTree),      
            };

            Vector3 correctorPosicionBoxRock1 = new Vector3(145f, 142f,8f);
            Vector3 correctorPosicionBoxRock12 = new Vector3(135f, 142f,8f);
            Vector3 correctorPosicionBoxRock5 = new Vector3(15f, 43f, 40f);
            Vector3 correctorPosicionBoxRock10 = new Vector3(-43f, 175f, -53f);

            Rock1Box = BoundingVolumesExtensions.CreateAABBFrom(Rock1);
            Rock1Box = BoundingVolumesExtensions.Scale(Rock1Box,new Vector3(0.03f,0.04f,0.05f));

            Rock5Box = BoundingVolumesExtensions.CreateAABBFrom(Rock5);
            Rock5Box1 = BoundingVolumesExtensions.Scale(Rock5Box, new Vector3(0.075f,0.05f,0.07f));
            Rock5Box2 = BoundingVolumesExtensions.Scale(Rock5Box, new Vector3(0.04f,0.03f,0.05f));

            Rock10Box = BoundingVolumesExtensions.CreateAABBFrom(Rock10);
            Rock10Box = BoundingVolumesExtensions.Scale(Rock10Box, new Vector3(0.05f));

            Rock1Boxes = new BoundingBox[] 
            {
                new BoundingBox(Rock1Box.Min + Rock1Position -correctorPosicionBoxRock1, Rock1Box.Max + Rock1Position - correctorPosicionBoxRock1),
                new BoundingBox(Rock1Box.Min + Rock2Position -correctorPosicionBoxRock1, Rock1Box.Max + Rock2Position - correctorPosicionBoxRock1),
                new BoundingBox(Rock1Box.Min + Rock3Position -correctorPosicionBoxRock1, Rock1Box.Max + Rock3Position - correctorPosicionBoxRock1),
                new BoundingBox(Rock1Box.Min + Rock4Position -correctorPosicionBoxRock1, Rock1Box.Max + Rock4Position - correctorPosicionBoxRock1),
                new BoundingBox(Rock1Box.Min + Rock5Position -correctorPosicionBoxRock12, Rock1Box.Max + Rock5Position - correctorPosicionBoxRock12),
                new BoundingBox(Rock1Box.Min + Rock6Position -correctorPosicionBoxRock12, Rock1Box.Max + Rock6Position - correctorPosicionBoxRock12),
                new BoundingBox(Rock1Box.Min + Rock7Position -correctorPosicionBoxRock12, Rock1Box.Max + Rock7Position - correctorPosicionBoxRock12),  
            };

            Rock5Boxes = new BoundingBox[] 
            {
                new BoundingBox(Rock5Box2.Min + Rock8Position - correctorPosicionBoxRock5, Rock5Box2.Max + Rock8Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box1.Min + Rock9Position - correctorPosicionBoxRock5, Rock5Box1.Max + Rock9Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box1.Min + Rock10Position - correctorPosicionBoxRock5, Rock5Box1.Max + Rock10Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box1.Min + Rock11Position - correctorPosicionBoxRock5, Rock5Box1.Max + Rock11Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box1.Min + Rock12Position - correctorPosicionBoxRock5, Rock5Box1.Max + Rock12Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock13Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock13Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock16Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock16Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock17Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock17Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock20Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock20Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock21Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock21Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock24Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock24Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock25Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock25Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock26Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock26Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock27Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock27Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock28Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock28Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock29Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock29Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock30Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock30Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock31Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock31Position - correctorPosicionBoxRock5),
                new BoundingBox(Rock5Box2.Min + Rock32Position -correctorPosicionBoxRock5, Rock5Box2.Max + Rock32Position - correctorPosicionBoxRock5),
            };

            Rock10Boxes = new BoundingBox[]
            {
                new BoundingBox(Rock10Box.Min + Rock14Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock14Position - correctorPosicionBoxRock10),
                new BoundingBox(Rock10Box.Min + Rock15Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock15Position - correctorPosicionBoxRock10),
                new BoundingBox(Rock10Box.Min + Rock18Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock18Position - correctorPosicionBoxRock10),
                new BoundingBox(Rock10Box.Min + Rock19Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock19Position - correctorPosicionBoxRock10),
                new BoundingBox(Rock10Box.Min + Rock22Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock22Position - correctorPosicionBoxRock10),
                new BoundingBox(Rock10Box.Min + Rock23Position -correctorPosicionBoxRock10, Rock10Box.Max + Rock23Position - correctorPosicionBoxRock10),
            };

            

            TireBox = BoundingVolumesExtensions.CreateAABBFrom(Tire);
            TireBox = BoundingVolumesExtensions.Scale(TireBox,new Vector3(0.02f,0.01f,0.04f));

            TireBoxes = new BoundingBox[posicionRuedas.Length];
            
            for(int i = 0; i < posicionRuedas.Length; i++)
            {
                TireBoxes[i] =  new BoundingBox(TireBox.Min + posicionRuedas[i] - correctorPosicionBoxTires, TireBox.Max + posicionRuedas[i] - correctorPosicionBoxTires);
            }

        }
        
        public void Update(GameTime gameTime, Autos auto)
        {
            OrientedBoundingBox autoCollider = auto.getAutoPrincipalBox();
            Vector3 vectorChoque = Vector3.Zero;
            float penetration = 0f;

            for(int index = 0; index < TreeBoxes.Length; index++)
            {
                if(autoCollider.Intersects(TreeBoxes[index],out vectorChoque,out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                   auto.rebotar(vectorChoque,penetration);
                   auto.FrenarAuto();
                }
            }
            
            for(int index = 0; index < Rock1Boxes.Length; index++)
            {
                if(autoCollider.Intersects(Rock1Boxes[index],out vectorChoque,out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                    auto.rebotar(vectorChoque,penetration);
                    auto.FrenarAuto();
                }
            }

            for (int index = 0; index < Rock5Boxes.Length; index++)
            {
                if (autoCollider.Intersects(Rock5Boxes[index], out vectorChoque, out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                    auto.rebotar(vectorChoque, penetration);
                    auto.FrenarAuto();
                }
            }

            for (int index = 0; index < Rock10Boxes.Length; index++)
            {
                if (autoCollider.Intersects(Rock10Boxes[index], out vectorChoque, out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                    auto.rebotar(vectorChoque, penetration);
                    auto.FrenarAuto();
                }
            }

            for (int index = 0; index < TireBoxes.Length; index++)
            {
                if(autoCollider.Intersects(TireBoxes[index],out vectorChoque,out penetration))
                {
                    if(auto.autoSpeed() > 10f) 
                    {
                        auto.audioChoque();
                    }
                    posicionRuedas[index] += -vectorChoque * penetration;
                    
                }
            }

            for(int i = 0; i < posicionRuedas.Length; i++)
                TiresWorld[i] = Matrix.CreateScale(0.02f) * Matrix.CreateRotationX(cuartoDeVuelta) * Matrix.CreateTranslation(posicionRuedas[i]);
            for(int i = 0; i < posicionRuedas.Length; i++)
                TireBoxes[i] =  new BoundingBox(TireBox.Min + posicionRuedas[i] - correctorPosicionBoxTires, TireBox.Max + posicionRuedas[i] - correctorPosicionBoxTires);
                        
        }

        public BoundingBox[] getTireBoxes()
        {
            return TireBoxes;
        }

        public BoundingBox[] getRock1Boxes()
        {
            return Rock1Boxes;
        }

        public BoundingBox[] getRock5Boxes()
        {
            return Rock5Boxes;
        }

        public BoundingBox[] getRock10Boxes()
        {
            return Rock10Boxes;
        }

        public BoundingBox[] getTreeBoxes()
        {
            return TreeBoxes;
        }


        public void dibujar(Matrix view,Matrix projection,Effect effect,Matrix matrizMundo,Model modelo,Texture2D textura)
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


        public void dibujarArboles(Matrix view,Matrix projection,Effect effect,Matrix matrizMundo, Model modelo, Texture2D textura)
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

            foreach (var mesh in modelo.Meshes)
            {
                effect.Parameters["World"].SetValue(matrizMundo);
                effect.Parameters["InverseTransposeWorld"]?.SetValue(Matrix.Invert(Matrix.Transpose(matrizMundo)));
                mesh.Draw();
            }
        }

        
        public void dibujarDetalles(Matrix view,Matrix projection,Effect effect, String tecnica, BoundingFrustum frustum)
        {   

            effect.CurrentTechnique = effect.Techniques[tecnica];
            for(int index = 0; index < TreeBoxes.Length; index++)
            {
                if(frustum.Intersects(TreeBoxes[index]))
                    dibujarArboles(view,projection,effect,TreesWorld[index], Tree, TexturaArbol);
            }


            for(int index = 0; index < Rocks1World.Length; index++)
            {
                if(frustum.Intersects(Rock1Boxes[index]))
                        dibujar(view,projection,effect,Rocks1World[index], Rock1, TexturaRoca);
            }
            for(int index = 0; index < Rocks5World.Length; index++)
            {
                if(frustum.Intersects(Rock5Boxes[index]))
                    dibujar(view,projection,effect,Rocks5World[index], Rock5, TexturaRoca);
            }
            for(int index = 0; index < Rocks10World.Length; index++)
            {
                if(frustum.Intersects(Rock10Boxes[index]))
                    dibujar(view,projection,effect,Rocks10World[index], Rock10, TexturaRoca);
            }

            for (int index = 0; index < TiresWorld.Length; index++)
            {
                if(frustum.Intersects(TireBoxes[index]))
                {
                    dibujar(view,projection,effect,TiresWorld[index], Tire, TexturaTire1);
                }
                    index++;
                
                if(frustum.Intersects(TireBoxes[index]))
                {
                    dibujar(view,projection,effect,TiresWorld[index], Tire, TexturaTire2);
                }
                
            }
        }

        public void dibujarBoundingBoxes(Gizmos gizmos)
        {
            for(int index = 0; index < TreeBoxes.Length; index++)
            {
                gizmos.DrawCube((TreeBoxes[index].Max + TreeBoxes[index].Min) / 2f,TreeBoxes[index].Max - TreeBoxes[index].Min,Color.Red);
            }

            for (int index = 0; index < TireBoxes.Length; index++)
            {
                gizmos.DrawCube((TireBoxes[index].Max + TireBoxes[index].Min) / 2f, TireBoxes[index].Max - TireBoxes[index].Min, Color.Red);
            }

            for (int index = 0; index < Rock1Boxes.Length; index++)
            {
                gizmos.DrawCube((Rock1Boxes[index].Max + Rock1Boxes[index].Min) / 2f, Rock1Boxes[index].Max - Rock1Boxes[index].Min, Color.Red);
            }

            for (int index = 0; index < Rock5Boxes.Length; index++)
            {
                gizmos.DrawCube((Rock5Boxes[index].Max + Rock5Boxes[index].Min) / 2f, Rock5Boxes[index].Max - Rock5Boxes[index].Min, Color.Red);
            }

            for (int index = 0; index < Rock10Boxes.Length; index++)
            {
                gizmos.DrawCube((Rock10Boxes[index].Max + Rock10Boxes[index].Min) / 2f, Rock10Boxes[index].Max - Rock10Boxes[index].Min, Color.Red);
            }

        }

        public Boolean IAchoco(OrientedBoundingBox obb){
            Vector3 vectorChoque = Vector3.Zero;
            float penetration = 0f;
            for (int index = 0; index < TireBoxes.Length; index++)
            {
                if(obb.Intersects(TireBoxes[index],out vectorChoque,out penetration))
                {
                    posicionRuedas[index] += -vectorChoque * penetration;
                }
            }

            for(int i = 0; i < Rock1Boxes.Length; i++)
                {
                    if(obb.Intersects(Rock1Boxes[i]))
                    return true;
                }

            for(int i = 0; i < Rock5Boxes.Length; i++)
                {
                    if(obb.Intersects(Rock5Boxes[i]))
                    return true;
                }
            for(int i = 0; i < Rock10Boxes.Length; i++)
                {
                    if(obb.Intersects(Rock10Boxes[i]))
                    return true;
                }

            for(int i = 0; i < TreeBoxes.Length; i++)
                {
                    if(obb.Intersects(TreeBoxes[i]))
                    return true;
                }

            return false;
        }

    }


}