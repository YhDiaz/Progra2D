using Project01.Game;
using Project01.UtalEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class PlayerFrame : Frame
    {
        #region Constructores

        public PlayerFrame(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {

        }

        #endregion

        #region Metodos

        public override void OnCollisionEnter(GameObject other)
        {
            //renderer.sprite.Dispose();
            //GameEngine.Destroy(this);
        }

        public override void Update()
        {
            if (GameEngine.KeyPress(System.Windows.Forms.Keys.Oemplus))
            {
                GameEngine.MainCamera.scale += Time.deltaTime;
            }
            if (GameEngine.KeyPress(System.Windows.Forms.Keys.OemMinus))
            {
                GameEngine.MainCamera.scale -= Time.deltaTime;
            }
            timerMove += Time.deltaTime;
            if (timerMove >= 1)
            {
                if (timerMove >= 2)
                {
                    timerMove -= 1;
                }
                bool moved = false;
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.W))
                {
                    transform.position.y -= 50;
                    moved = true;
                }
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.S))
                {
                    transform.position.y += 50;
                    moved = true;
                }
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.A))
                {
                    transform.position.x -= 50;
                    moved = true;
                }
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.D))
                {
                    transform.position.x += 50;
                    moved = true;
                }
                if (moved)
                {
                    myCamera.Position = transform.position;
                    timerMove -= 1;
                }
            }
        }

        #endregion
    }
}
