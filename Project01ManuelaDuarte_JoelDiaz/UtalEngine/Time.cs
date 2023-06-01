using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public static class Time
    {
        #region Atributos

        //Variables de tiempo
        static DateTime time1 = DateTime.Now;
        static DateTime time2 = DateTime.Now;

        //Variables delta time
        public static double deltaTimeDouble = -1;
        public static float deltaTime = -1;

        #endregion

        #region Metodos

        //Actualizar delta time en cada iteracion del while loop
        public static void UpdateDeltaTime()
        {
            time2 = DateTime.Now;
            deltaTimeDouble = (time2 - time1).TotalMilliseconds / 1000.0;
            deltaTime = (float)deltaTimeDouble;
            //Console.WriteLine(deltaTime);  // *float* output {0,2493331}
            //Console.WriteLine(time2.Ticks - time1.Ticks); // *int* output {2493331}
            time1 = time2;
        }

        #endregion
    }
}
