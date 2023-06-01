using Project01.Game;
using Project01.UtalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.Game
{
    public class FrameManager : EmptyUpdatable
    {
        #region Atributos

        public static List<Frame> AllFrames = new List<Frame>();
        public static int selectedIndex;
        public static FrameManager Instance;

        #endregion

        #region Metodos

        public override void Update()
        {
            if (GameEngine.KeyUp(System.Windows.Forms.Keys.C))
            {
                selectedIndex++;
                Console.WriteLine("Key detected");
                if (AllFrames.Count >= selectedIndex)
                {
                    selectedIndex %= AllFrames.Count;
                }
                GameEngine.MainCamera = AllFrames[selectedIndex].myCamera;
            }
        }

        #endregion
    }
}
