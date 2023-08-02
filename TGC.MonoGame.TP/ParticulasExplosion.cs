using System;
using Microsoft.Xna.Framework;

namespace TGC.MonoGame.TP
{
  public class ParticulasExplosion
  {
    private Vector3 Posicion;
    private Vector3 Velocidad;
    private Vector3 Aceleracion = new Vector3(0f,-10f,0f);
    private float Size;
    private Color ColorInicial = Color.Yellow;
    private Color ColorIntermedio1 = Color.OrangeRed;
    private Color ColorIntermedio2 = Color.DarkRed;
    private Color ColorFinal = Color.Black;
    private Color ColorInterpolado;
    private float TiempoVida;
    private float TranslucidezInicial = 1f;
    private float TranslucidezFinal = 0f;
    private float TranslucidezInterpolada;
    private float duracionExplosion = 1.5f;
    private float radio = 50;
    private Vector3 PosInicial;
    

    public ParticulasExplosion(Vector3 posicion, Vector3 velocidad, float size, float tiempoVida){
        Posicion = posicion;
        Velocidad = velocidad;
        Size = size;
        TiempoVida = tiempoVida;
        PosInicial = posicion; //las posInicial no cambia, es el punto donde se genera la colision a partir de aqui se alejan las particulas
    }

    public void Update(GameTime gameTime){
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(viva())
        {
            Velocidad += Aceleracion * 0.5f * elapsedTime * elapsedTime;
            Posicion += Velocidad * elapsedTime;
            TiempoVida -= elapsedTime;
            
            float deltaX = Posicion.X - PosInicial.X;
            float deltaY = Posicion.Y - PosInicial.Y;
            float deltaZ = Posicion.Z - PosInicial.Z;
            float distanciaAlInicio = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            //se calcula la distancia entre 2 vectores3

            if(distanciaAlInicio < radio / 3)
                ColorInterpolado = Color.Lerp(ColorInicial,ColorIntermedio1,distanciaAlInicio/(radio/3));
            else if(distanciaAlInicio < 2 * radio / 3)
                ColorInterpolado = Color.Lerp(ColorIntermedio1,ColorIntermedio2,(distanciaAlInicio-(radio/3))/(radio/3));
            else
                ColorInterpolado = Color.Lerp(ColorIntermedio2,ColorFinal,(distanciaAlInicio-(2*radio/3))/(radio/3));
            //interpolar en base a la distancia de cada particula al centro para obtener colores similares a una explosion

            TranslucidezInterpolada = MathHelper.Lerp(TranslucidezFinal,TranslucidezInicial,TiempoVida/duracionExplosion); 
            //generar un efecto de "desaparcicion" de las particulas
            //TiempoVida/duracionExplosion para que los valores vayan de 1 a 0
            

        }
        
    }
    
    public bool viva()
    {
        return this.TiempoVida > 0f;
    }

    public Vector3 posicion(){
        return Posicion;
    }
    public Color color(){
        return ColorInterpolado * TranslucidezInterpolada;
    }
    public float size(){
        return Size;
    }
  }

}
