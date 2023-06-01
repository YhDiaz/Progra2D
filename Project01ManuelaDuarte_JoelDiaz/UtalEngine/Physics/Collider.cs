using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine.Physics
{
    public abstract class Collider
    {
        #region Atributos

        public Rigidbody rigidbody;

        #endregion

        #region Constructores

        public Collider(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        #endregion

        #region Metodos

        public abstract bool CheckCollision(Collider other);

        #endregion
    }
}
