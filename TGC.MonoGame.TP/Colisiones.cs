using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Collisions;

namespace TGC.MonoGame.TP
{
    public class Colisiones
    {
        public Colisiones() {}



        //ESCENARIO 
        
        private Model Piso { get; set; }
        private Model Pared { get; set; }
        private Model Column { get; set; }
        private Model Ramp { get; set; }
        private Model Platform { get; set; }
        private Model Cube { get; set; }

        private Escenario escenario;
    

        //DETALLES 
         
        private Model Tree { get; set; }
        private Model Rock1 { get; set; }
        private Model Rock5 { get; set; }
        private Model Rock10 { get; set; }
        private Model Tire { get; set; }
        
        private Detalles detalles;


        private Vector3[] TreesPositions;

        //COLISIONES

        private Autos autos;
        private BoundingBox TreeBoxAABB;
        private BoundingBox Rock1BoxAABB;
        private BoundingBox Rock5BoxAABB;
        private BoundingBox Rock10BoxAABB;
        private BoundingBox TireBoxAABB;

        private BoundingBox Tree1;


        private BoundingBox[] CollideTrees;
        private OrientedBoundingBox[] CollideRocks1;
        private OrientedBoundingBox[] CollideRocks5;
        private OrientedBoundingBox[] CollideRocks10;
        private OrientedBoundingBox[] CollideTires;



    public void Initialize()
    {
        
         TreesPositions = new Vector3[]
            {
            detalles.Tree1Position,
            detalles.Tree2Position,
            detalles.Tree3Position,
            detalles.Tree4Position,
            detalles.Tree5Position,
            detalles.Tree6Position,
            detalles.Tree7Position,
            detalles.Tree8Position,
            detalles.Tree9Position,
            detalles.Tree1Position,
            detalles.Tree11Position,
            detalles.Tree12Position,
            };

            detalles.Initialize();
    }
    public void LoadContent()
    {
        
        detalles.LoadContent(Tree,Rock1,Rock5,Rock10,Tire);
        escenario.LoadContent(Piso,Pared,Column,Ramp,Platform,Cube);

        detalles.Tree1World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(detalles.Tree1Position);
        TreeBoxAABB = BoundingVolumesExtensions.FromMatrix(detalles.Tree1World);
        //Tree1AABB = BoundingVolumesExtensions.Scale(Tree1ABB,0.1f);

        CollideTrees = new BoundingBox[] 
            {
                TreeBoxAABB = BoundingVolumesExtensions.FromMatrix(detalles.Tree1World),
            };
           
        
    }

    public void Upadate(GameTime gameTieme)
    {

         Vector3 postChoque = autos.CarDirection*autos.CarSpeed/5;

    //Colisiones con árboles
         for(var index = 0; index < CollideTrees.Length; index++) 
            {
                if(autos.AutoPrincipalBox.Intersects(CollideTrees[index])) 
                {
                    autos.AutoPrincipalPos -= postChoque;
                    TreesPositions[index] += postChoque;

                    autos.CarSpeed = 0;
                }
            }
    }

    }
}