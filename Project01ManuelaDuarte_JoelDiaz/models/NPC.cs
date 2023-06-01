using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    public class NPC : Character
    {
        #region  Constructores

        //Constructor por defecto
        public NPC()
        {
            
        }


        //Asignacion de id, casilla y skin
        public NPC(string id, Box characterBox, PictureBox skin)
        {
            this.id = id;
            this.characterBox = characterBox;
            this.characterSkin = skin;
        }

        #endregion
    }
}
