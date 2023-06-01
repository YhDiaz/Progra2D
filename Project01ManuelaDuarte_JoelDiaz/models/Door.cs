using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    public class Door
    {
        #region Atributos

        private string id = string.Empty;
        private bool openDoor = false;

        public static bool command = false;

        #endregion

        #region Metodos

        //Obtencion del estado de la puerta (true: abierta, false: cerrada)
        public bool getDoorStatus()
        {
            return this.openDoor;
        }

        //Mostrar id
        public void print()
        {
            if (id != string.Empty)
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

        //Se activa el comando para abrir la puerta
        public void open(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 'J')
            {
                Door.command = true;
            }
        }

        #endregion

        #region Constructores

        //Constructor por defecto
        public Door()
        {

        }

        //Asignacion de id
        public Door(string id)
        {
            this.id = id;
        }

        //Asignacion de id y estado de la puerta
        public Door(string id, bool openDoor)
        {
            this.id = id;
            this.openDoor = openDoor;
        }

        #endregion
    }
}
