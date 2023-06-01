using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    public class Element
    {
        #region Atributos

        //Identificador unico
        private string id = string.Empty;

        //Tipo de objeto
        private string type = string.Empty;

        //Diccionario con las cantidades de cada tipo de objeto
        public static Dictionary<string, int> amount = new Dictionary<string, int>();

        //Lista de elementos
        public static List<Element> elements = new List<Element>();

        //Boton y casilla asociados
        private Button associatedButton;
        public Box associatedBox;

        #endregion

        #region Metodos

        //Obtener el id
        public string getID()
        {
            return this.id;
        }

        //Obtencion de la cantidad de objetos de un tipo
        public int getObjectAmount(string id)
        {
            return Element.amount[id];
        }

        //Asignacion de casilla asociada
        public void setBox(Box box)
        {
            this.associatedBox = box;
        }

        //Obtener casilla asociada
        public Box getBox()
        {
            return this.associatedBox;
        }

        //Obtener el boton asociado
        public Button getButton()
        {
            return this.associatedButton;
        }

        //Obtener el tipo de objeto
        public string getType()
        {
            return this.type;
        }

        //Establecer el boton asociado
        public void setButton(Button myButton)
        {
            this.associatedButton = myButton;
        }

        #endregion

        #region Constructores

        //Constructor por defecto
        public Element()
        {

        }

        //Asignacion de id
        public Element(string id, string type)
        {
            this.id = id;
            this.type = type;

            if (amount.ContainsKey(id))
            {
                Element.amount[id]++;
            }
            else
            {
                Element.amount.Add(id, 0);
            }
        }

        //Asignacion de id y boton
        public Element(string id, string type, Button myButton)
        {
            this.id = id;
            this.type = type;
            this.associatedButton = myButton;

            if (amount.ContainsKey(type))
            {
                Element.amount[type]++;
            }
            else
            {
                Element.amount.Add(type, 0);
            }
        }

        #endregion
    }
}
