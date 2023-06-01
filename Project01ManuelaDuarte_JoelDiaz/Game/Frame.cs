using Project01.UtalEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project01.UtalEngine.GameObject;

namespace Project01.Game
{
    public class Frame : GameObject
    {
        #region Atributos

        public static int LASTID = 0;
        public float Speed = 1;
        public float timerMove = 0;
        public int myId = 0;
        static Random rand;
        public Camera myCamera;

        #endregion

        #region Constructores

        public Frame(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(newsprite, newSize, x, y)
        {
            FrameManager.AllFrames.Add(this); // Se agrega el Frame a la lista de frames
            myId = LASTID++; // Asignacion de id
            myCamera = new Camera(); // Asignacion de camara
            this.Speed = Speed; // Velocidad
            renderer.size = new Vector2(50, 50); // Dimension del renderer
            
            // Creacion de un objeto random
            if (rand == null)
            {
                rand = new Random();
            }
        }

        #endregion

        #region Metodos

        public override void OnCollisionEnter(GameObject other)
        {
            //renderer.sprite.Dispose();
            GameEngine.Destroy(this);
        }

        public override void Update()
        {
            //return;
            timerMove += (float)Time.deltaTime;
            if (timerMove >= 1)
            {
                timerMove -= 1 - (float)(rand.NextDouble()) / 100f;

                int moveOption = rand.Next(0, 4);
                switch (moveOption)
                {
                    case 0:
                        transform.position.x += 50;
                        break;
                    case 1:
                        transform.position.y += 50;
                        break;
                    case 2:
                        transform.position.x -= 50;
                        break;
                    case 3:
                        transform.position.y -= 50;
                        break;
                }
                myCamera.Position = transform.position;
            }
        }

        #endregion
    }
}
