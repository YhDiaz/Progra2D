using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    public class Character
    {
        #region Atributos

        protected string id = string.Empty;

        //Casilla asociada al personaje
        protected Box characterBox;

        //Skin del personaje
        protected PictureBox characterSkin;

        //Turno del player
        public static bool turnPlayer = true;

        #endregion

        #region Metodos

        //Establecer casilla
        public void setBox(Box box)
        {
            this.characterBox = box;
        }

        //Establecer ubicacion del picture box
        public void setPBLocation(Button button)
        {
            this.characterSkin.Location = button.Location;
            this.characterSkin.BringToFront();
        }

        //Obtener la casilla asociada
        public Box getBox()
        {
            if(this.characterBox != null)
            {
                return this.characterBox;
            }

            return null;
        }

        //Obtener la skin del personaje
        public PictureBox getSkin()
        {
            if(characterSkin == null)
            {
                return characterSkin;
            }

            return null;
        }

        public static void moveNPC()
        {
            Box controls = new Box();

            controls.turnNPC();
        }

        public string getID()
        {
            return this.id;
        }

        #endregion
    }
}
