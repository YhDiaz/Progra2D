using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public struct Vector2
    {
        #region Atributos

        //Coordenada
        public float x;
        public float y;

        #endregion

        #region Constructores

        //Asignacion de coordenadas
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Operaciones

        //Suma y resta de vectores
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        #endregion

        #region Metodos

        public override string ToString()
        {
            return "x " + x + " y " + y;
        }

        #endregion
    }
}
