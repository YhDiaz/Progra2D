using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine.Physics
{
    public class CircleCollider : Collider
    {
        #region Atributos

        public float radius;

        #endregion

        #region Constructores

        public CircleCollider(Rigidbody rigidbody, float radius) : base(rigidbody)
        {
            this.radius = radius;
        }

        #endregion

        #region Metodos

        public override bool CheckCollision(Collider other)
        {
            CircleCollider otherC = other as CircleCollider;
            if (otherC != null)
            {
                Vector2 distVector = otherC.rigidbody.transform.position - rigidbody.transform.position;
                float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
                if (squareDist < radius * radius + otherC.radius * otherC.radius)
                {
                    return true;
                }

            }
            return false;
        }

        #endregion
    }
}
