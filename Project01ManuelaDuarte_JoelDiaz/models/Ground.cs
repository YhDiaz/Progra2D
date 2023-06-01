using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.models
{
    public class Ground
    {
        //Atributos
        private string id = string.Empty;

        #region Metodos

        //Mostrar id
        public void print()
        {
            if(id != string.Empty)
            {
                Console.WriteLine(id);
            }
            else
            {
                Console.WriteLine("ID no asignado");
            }
        }

        //Devolver el id
        public string print(bool returnValue)
        {
            if (returnValue)
            {
                return this.id;
            }
            else
            {
                return "PARAMETER TRUE REQUIRED";
            }
        }

        #endregion

        #region Constructores

        //Constructor por defecto
        public Ground()
        {

        }

        //Asignacion de id
        public Ground(string id)
        {
            this.id = id;
        }

        #endregion
    }
}
