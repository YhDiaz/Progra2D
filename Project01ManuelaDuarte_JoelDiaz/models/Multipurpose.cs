using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.models
{
    internal class Multipurpose
    {
        #region Atributos

        //Mensajes
        public static int lines = 0;
        public static Queue<string> messages = new Queue<string>();
        public static Label label = null;

        //Longitud de la matriz de casillas
        public static int boardRows = 0;
        public static int boardColumns = 0;

        #endregion

        #region Metodos

        //Se obtiene la cantidad de filas y columnas de una matriz de enteros, donde -1 indica el limite
        public void getMatrixLength(int[,] matrix, ref int rows, ref int columns)
        {
            int i = 0, j = 0;

            //Se obtiene el numero de filas
            while (matrix[i, 0] != -1)
            {
                i++;
            }

            rows = i;

            //Se obtiene el numero de columnas
            while (matrix[0, j] != -1)
            {
                j++;
            }

            columns = j;
        }

        //Se muestra un historial de mensajes
        public void historyMessages(Label labelDefault, string message)
        {
            //Si no hay una etiqueta de historial, se asigna la que se paso como parametro
            if (label == null && labelDefault != null)
            {
                label = labelDefault;
            }
            //Si hay una etiqueta de historial, no se reemplaza por la que se paso como parametro
            else if (label != null && labelDefault != null)
            {
                Console.WriteLine("Nueva etiqueta rechazada, ya existe un historial");
            }

            //Si hay una etiqueta donde mostrar el historial, se muestra
            if (label != null)
            {
                //El maximo de lineas que se muestran en el historial son 2
                if (lines >= 2)
                {
                    //Se actualiza la lista de mensajes
                    messages.Dequeue();

                    //Se "limpia" el historial de mensajes
                    label.Text = string.Empty;

                    //Se convierte la lista en array y se asigna el primer string
                    //como primera linea del historial
                    label.Text = messages.ToArray()[0];
                }

                //Si no hay mensajes en el historial, se asigna el texto
                if (lines == 0)
                {
                    label.Text = message;
                }
                //Si ya hay mensajes en el historial, se agrega el texto como su continuacion
                else
                {
                    label.Text += "\n" + message;
                }

                //Se agrega el nuevo mensaje a la cola y se incrementa el contador de lineas
                messages.Enqueue(message);
                lines++;
            }
            else
            {
                Console.WriteLine("No hay historial");
            }
        }

        //Obtener la cantidad de contenido de casilla del mismo tipo
        public int getNumOfSameContent(int[,] board, int rows, int columns, int contentToSearch)
        {
            int count = 0;

            for(int i = 0; i < columns; i++)
            {
                for(int j = 0; j < rows; j++)
                {
                    if (board[j, i] == contentToSearch)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        #endregion
    }
}
