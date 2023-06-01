using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public abstract class EmptyUpdatable
    {
        #region Metodos

        public EmptyUpdatable()
        {
            GameObjectManager.AllEmptyUpdatables.Add(this);
        }

        public abstract void Update();

        #endregion
    }
}
