using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    public class Player : Character
    {
        //Inventario
        List<Element> inventory = new List<Element>();
        private int inventoryCapacity = 4;
        public static bool deployedInventory = false;

        #region Metodos

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

        //Agregar objetos al inventario
        public void addObjects(Element obj, Box objAssociatedBox, ref bool objectAdded)
        {
            Multipurpose controls = new Multipurpose();

            //Se comprueba que el inventario aun no haya alcanzado el maximo
            if (this.inventory.Count < this.inventoryCapacity)
            {
                //Se agrega el objeto al inventario
                this.inventory.Add(obj);

                //Se elimina el objeto de la lista de objetos de su casilla
                objAssociatedBox.removeObject(obj);

                objectAdded = true;
                controls.historyMessages(null, "Se agrego el objeto " + obj.getType() + " al inventario");
            }
            else
            {
                controls.historyMessages(null, "Inventario lleno");
                objectAdded = false;
            }
        }

        //Obtener el inventario
        public List<Element> getInventory()
        {
            return this.inventory;
        }

        #endregion

        #region Constructores

        //Constructor por defecto
        public Player()
        {

        }

        //Asignacion de skin y casilla
        public Player(PictureBox skin, Box myBox, string id)
        {
            this.characterSkin = skin;
            this.characterBox = myBox;
            this.id = id;
        }

        #endregion
    }
}
