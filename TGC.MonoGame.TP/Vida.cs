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
  public class SistemaDeVida
  {
    public int[] vidaAutos;
    public int vidaProtagonista = 100;
    private int cantidadEnemigos = 8;
    private bool godMode = false;

    private Autos Autos;
    private PowerUps PowerUps;
    private Random unRandom = new Random();

    public void Initialize()
    {
        vidaAutos = new int[cantidadEnemigos];

        for(int index = 0; index < cantidadEnemigos; index++)
        {
            vidaAutos[index] = 100;
        }
    }

    public void Update(Autos autos, PowerUps powerUps)
    {
        Autos = autos;
        PowerUps = powerUps;


        for (var index = 0; index < autos.CollideCars.Length; index++)
        {
            if (autos.getAutoPrincipalBox().Intersects(autos.CollideCars[index]))
            {
                vidaProtagonista -= 25;
                autos.AutosPosiciones[index] = RandomPosition(unRandom);
                vidaAutos[index] = 100;
            }
        }

        for (int i = 0; i < powerUps.BalasWorld.Length; i++)
        {
            if (powerUps.recorridoBalas[i] > 0f)
            {
                for (var index = 0; index < autos.getPosAutos().Length; index++)
                {
                    if(powerUps.collidersBalas[i].Intersects(autos.getPosAutos()[index]))
                    {
                        powerUps.recorridoBalas[i] = 0f;
                        vidaAutos[index] -= 100;

                        if(vidaAutos[index] <= 0)
                        {
                            autos.AutosPosiciones[index] = RandomPosition(unRandom);
                            vidaAutos[index] = 100;
                        }
                    }
                }
            }
        }

        if (powerUps.recorridoMisil > 0f)
            {
                for (var index = 0; index < autos.getPosAutos().Length; index++)
                {
                    if(powerUps.colliderMisil.Intersects(autos.getPosAutos()[index]))
                    {
                        powerUps.recorridoMisil = 0f;
                        vidaAutos[index] -= 100;

                        if(vidaAutos[index] < 0)
                        {
                            autos.AutosPosiciones[index] = RandomPosition(unRandom);
                            vidaAutos[index] = 100;
                        }
                    }
                }
            }
    }


    public bool muereProta()
    {
        return vidaProtagonista <= 0 ? true : false;
    }

    public void pierdeVidaProta(int cantidad)
    {
        vidaProtagonista -= cantidad;
    }

    public int getVidaProta()
    {
        return vidaProtagonista;
    }

    private Vector3 RandomPosition(Random random)
    {
        return new Vector3((float)random.NextDouble() * 100f - 50f, 5f, (float)random.NextDouble() * 700f - 350f);
    }
    // AJUSTAR VECTOR RANDOM A UNA POSICIÓN CERCA DE UN BORDE 
  }
}