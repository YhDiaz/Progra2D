using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public static class GameObjectManager
    {
        #region Atributos

        public static List<GameObject> AllGameObjects = new List<GameObject>();
        public static List<GameObject> AllDeadGameObjects = new List<GameObject>();
        public static List<EmptyUpdatable> AllEmptyUpdatables = new List<EmptyUpdatable>();

        #endregion

        #region Metodos

        public static void Update()
        {
            foreach (GameObject go in AllGameObjects)
            {
                go.Update();
            }
            foreach (EmptyUpdatable eu in AllEmptyUpdatables)
            {
                eu.Update();
            }
        }

        #endregion
    }
}
