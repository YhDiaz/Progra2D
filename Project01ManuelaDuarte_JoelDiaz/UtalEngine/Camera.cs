using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public class Camera
    {
        #region Atributos

        //Longitud de la camara
        public int xSize = 700;
        public int ySize = 700;

        //Ubicacion y escala
        public Vector2 Position;
        public float scale = 1;

        #endregion
    }
}
