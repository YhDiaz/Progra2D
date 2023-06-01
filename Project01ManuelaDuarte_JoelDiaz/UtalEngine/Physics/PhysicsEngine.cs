using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.UtalEngine.Physics
{
    public static class PhysicsEngine
    {
        #region Atributos

        public static List<Rigidbody> allRigidbodies = new List<Rigidbody>();
        public static List<Rigidbody> allDeadRigidbodies = new List<Rigidbody>();

        #endregion

        #region Metodos

        public static void Destroy(Rigidbody rigidbody)
        {
            allDeadRigidbodies.Add(rigidbody);
        }

        public static void Update()
        {
            for (int i = 0; i < allRigidbodies.Count; i++)
            {
                for (int j = i + 1; j < allRigidbodies.Count; j++)
                {
                    if (allRigidbodies[i].CheckCollision(allRigidbodies[j]))
                    {
                        Console.WriteLine("Choque");
                        Console.WriteLine(allRigidbodies[i].transform.position);
                        Console.WriteLine(allRigidbodies[j].transform.position);
                    }
                }
            }
            foreach (Rigidbody rb in allDeadRigidbodies)
            {
                allRigidbodies.Remove(rb);
            }
            allDeadRigidbodies = new List<Rigidbody>();
        }

        #endregion
    }
}
