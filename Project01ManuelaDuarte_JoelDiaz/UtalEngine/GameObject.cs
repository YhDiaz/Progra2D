using Project01.UtalEngine;
using Project01.UtalEngine.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine
{
    public class GameObject
    {
        #region Clases

        public class Renderer
        {
            public Image sprite;
            public Vector2 position;
            public Vector2 size;
            public float rotation;
        }

        #endregion

        #region Atributos

        public Rigidbody rigidbody;

        public Transform transform = new Transform();
        public Renderer renderer = new Renderer();

        #endregion

        #region Constructores

        public GameObject(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0)
        {
            transform.position = new Vector2(xPos, yPos); // Posicion

            // Rigidbody
            rigidbody = new Rigidbody();
            rigidbody.transform = transform;
            rigidbody.CreateCircleCollider(newSize.x / 2);
            rigidbody.OnCollision = OnCollisionEnter;
            rigidbody.GetOnCollisionObject = GetOnCollision;

            GameObjectManager.AllGameObjects.Add(this); // Se agrega el GameObject a la lista de objetos del juego

            // Renderer
            renderer.sprite = newSprite;
            renderer.size = newSize;
        }

        #endregion

        #region Metodos

        public void OnCollisionEnter(Object otherO)
        {
            GameObject otherGO = otherO as GameObject;
            OnCollisionEnter(otherGO);
        }

        public Object GetOnCollision()
        {
            return this;
        }

        public virtual void OnCollisionEnter(GameObject other)
        {

        }

        public virtual void Update()
        {

        }

        public void OnDestroy()
        {
            PhysicsEngine.Destroy(rigidbody);
        }

        public void Draw(Graphics graphics, Camera camera)
        {
            int xOffset = camera.xSize / 2;
            int yOffset = camera.ySize / 2;
            if (renderer == null)
            {
                return;
            }
            graphics.DrawImage(renderer.sprite,
                (transform.position.x - camera.Position.x - renderer.size.x / 2) / camera.scale + xOffset,
                (transform.position.y - camera.Position.y - renderer.size.y / 2) / camera.scale + yOffset,
                renderer.size.x / camera.scale,
                renderer.size.y / camera.scale);
        }

        #endregion
    }
}
