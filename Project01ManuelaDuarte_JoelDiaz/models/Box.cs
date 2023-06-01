using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Project01.models.Box;

namespace Project01.models
{
    public class Box
    {
        # region Atributos

        public static Board myForm;

        //Posicion de la casilla
        private int x;
        private int y;

        //Contenido de la casilla
        private Character boxCharacter;
        private Wall boxWall;
        private Ground boxGround;
        private Door boxDoor;
        private List<Element> objects = new List<Element>();

        //Casilla final
        public static Box medalBox;

        //Boton asociado a la casilla
        public Button associatedButton;

        //Diccionario de vecinos utilizando enumeracion de sentido
        public enum Direction { UP, DOWN, RIGHT, LEFT }
        public Dictionary<Direction, Box> neighbors = new Dictionary<Direction, Box>();

        //Almacenamiento del id de las casillas actual y anterior sobre las que se hizo click
        public static string lastClick = string.Empty;
        public static string currentClick = string.Empty;

        //Lista de NPCs
        public static List<Box> npcs = new List<Box>();

        #endregion

        #region Metodos

        //Establecer posicion
        public void setPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //Obtener posicion por referencia
        public void getPos(ref int x, ref int y)
        {
            x = this.x;
            y = this.y;
        }

        # region Setters (Sobrecarga de metodos)

        public void setContent(Wall myWall)
        {
            this.boxCharacter = null;
            this.boxWall = myWall;
            this.boxGround = null;
            this.boxDoor = null;
            this.objects = null;
        }

        public void setContent(Ground myGround)
        {
            this.boxCharacter = null;
            this.boxWall = null;
            this.boxGround = myGround;
            this.boxDoor = null;
            this.objects = null;
        }

        public void setContent(Door myDoor)
        {
            this.boxCharacter = null;
            this.boxWall = null;
            this.boxGround = null;
            this.boxDoor = myDoor;
            this.objects = null;
        }

        //Establecer solo el character
        public void setCharacter(Character myCharacter)
        {
            this.boxCharacter = myCharacter;
        }

        //Establecer la lista de objetos
        public void setObjectList(List<Element> elements)
        {
            this.objects = elements;
        }

        #endregion

        #region Getters

        //Obtener el ground
        public Ground getGround()
        {
            return this.boxGround;
        }

        //Obtener el character
        public Character getCharacter()
        {
            return this.boxCharacter;
        }

        //Obtener la puerta
        public Door getDoor()
        {
            return this.boxDoor;
        }

        //Obtener la lista de objetos
        public List<Element> getObjectsList()
        {
            return this.objects;
        }

        #endregion

        //Hacer null la varible para almacenar el personaje
        public void doBoxCharacterNull()
        {
            this.boxCharacter = null;
        }

        //Evento click
        public void click(object sender, EventArgs e)
        {
            //Mensajes en el historial
            Multipurpose controls = new Multipurpose();

            //El click actual pasa a ser el ultimo que se hizo, ya que el click actual tendra otro valor
            if (currentClick != string.Empty)
            {
                lastClick = currentClick;
            }

            if (this.boxGround != null)
            {
                currentClick = "GROUND";

                //Si el ultimo click se hizo en el player, entonces el usuario se quiere
                //mover a la casilla que selecciono
                if (lastClick == "PLAYER" && Character.turnPlayer)
                {
                    //El vecino de arriba contiene al player
                    if (this.neighbors[Direction.UP].boxCharacter != null && this.neighbors[Direction.UP].boxCharacter.getID() == "Player")
                    {
                        movePlayer(this, this.neighbors[Direction.UP].boxCharacter, Direction.UP);
                    }
                    //El vecino de abajo contiene al player
                    else if (this.neighbors[Direction.DOWN].boxCharacter != null && this.neighbors[Direction.DOWN].boxCharacter.getID() == "Player")
                    {
                        movePlayer(this, this.neighbors[Direction.DOWN].boxCharacter, Direction.DOWN);
                    }
                    //El vecino de la derecha contiene al player
                    else if (this.neighbors[Direction.RIGHT].boxCharacter != null && this.neighbors[Direction.RIGHT].boxCharacter.getID() == "Player")
                    {
                        movePlayer(this, this.neighbors[Direction.RIGHT].boxCharacter, Direction.RIGHT);
                    }
                    //El vecino de la izquierda contiene al player
                    else if (this.neighbors[Direction.LEFT].boxCharacter != null && this.neighbors[Direction.LEFT].boxCharacter.getID() == "Player")
                    {
                        movePlayer(this, this.neighbors[Direction.LEFT].boxCharacter, Direction.LEFT);
                    }
                    else
                    {
                        controls.historyMessages(null, "La casilla esta muy lejos...");
                    }
                }
                else
                {
                    controls.historyMessages(null, "La casilla esta libre!!");
                }
            }
            else if (this.boxWall != null)
            {
                currentClick = "WALL";

                if (lastClick == "PLAYER")
                {
                    controls.historyMessages(null, "Hay un muro en esa posicion, no puedes moverte ahi (a menos que seas Spider-Man)");
                }
                else
                {
                    controls.historyMessages(null, "Asi es, hay un muro ahi");
                }
            }
            else if (this.boxDoor != null)
            {
                if (this.boxDoor.getDoorStatus())
                {
                    currentClick = "OPENDOOR";

                    if (lastClick == "PLAYER")
                    {
                        //El vecino de arriba contiene al player
                        if (this.neighbors[Direction.UP].boxCharacter != null && this.neighbors[Direction.UP].boxCharacter.getID() == "Player")
                        {
                            if (Door.command)
                            {
                                movePlayer(this, this.neighbors[Direction.UP].boxCharacter, Direction.UP);
                            }
                            else
                            {
                                controls.historyMessages(null, "Presiona J para abrir la puerta");
                            }
                        }
                        //El vecino de abajo contiene al player
                        else if (this.neighbors[Direction.DOWN].boxCharacter != null && this.neighbors[Direction.DOWN].boxCharacter.getID() == "Player")
                        {
                            if (Door.command)
                            {
                                movePlayer(this, this.neighbors[Direction.DOWN].boxCharacter, Direction.DOWN);
                            }
                            else
                            {
                                controls.historyMessages(null, "Presiona J para abrir la puerta");
                            }
                        }
                        //El vecino de la derecha contiene al player
                        else if (this.neighbors[Direction.RIGHT].boxCharacter != null && this.neighbors[Direction.RIGHT].boxCharacter.getID() == "Player")
                        {
                            if (Door.command)
                            {
                                movePlayer(this, this.neighbors[Direction.RIGHT].boxCharacter, Direction.RIGHT);
                            }
                            else
                            {
                                controls.historyMessages(null, "Presiona J para abrir la puerta");
                            }
                        }
                        //El vecino de la izquierda contiene al player
                        else if (this.neighbors[Direction.LEFT].boxCharacter != null && this.neighbors[Direction.LEFT].boxCharacter.getID() == "Player")
                        {
                            if (Door.command)
                            {
                                movePlayer(this, this.neighbors[Direction.LEFT].boxCharacter, Direction.LEFT);
                            }
                            else
                            {
                                controls.historyMessages(null, "Presiona J para abrir la puerta");
                            }
                        }
                        else
                        {
                            controls.historyMessages(null, "La puerta esta sin candado, pero debes acercarte mas para cruzar...");
                            Door.command = false;
                        }
                    }
                    else
                    {
                        controls.historyMessages(null, "La puerta esta abierta...");
                    }
                }
                else
                {
                    currentClick = "CLOSEDOOR";

                    if (lastClick == "PLAYER")
                    {
                        controls.historyMessages(null, "Busca la llave para abrir la puerta");
                    }
                    else
                    {
                        controls.historyMessages(null, "La puerta esta cerrada con candado");
                    }
                }
            }
        }

        //Agregar vecino al diccionario
        public void setNeighbor(Box neighbor, Direction direction)
        {
            this.neighbors[direction] = neighbor;
        }

        //Mover al player
        public void movePlayer(Box neighbor, Character player, Direction direction)
        {
            //Console.WriteLine("Control Tower");
            if (player.getBox() != null)
            {
                player.getBox().doBoxCharacterNull();
            }

            Ground newGround = neighbor.getGround();

            this.setCharacter(player);
            player.setPBLocation(this.associatedButton);
            player.setBox(this);

            //Termino el turno del player
            Character.turnPlayer = false;

            //Mensaje
            Multipurpose controls = new Multipurpose();

            if (this.boxGround != null)
            {
                controls.historyMessages(null, "Te has movido!!");
            }
            else if (this.boxDoor != null)
            {
                controls.historyMessages(null, "Entrando a la siguiente habitacion...");
                Door.command = false;
            }

            if(this == Box.medalBox)
            {
                MessageBox.Show("Has llegado al final del juego");
                //myForm.end();
            }

            //Movimiento de los NPCs
            Character.moveNPC();
        }

        //Turno del NPC
        public void turnNPC()
        {
            //Mensaje
            Multipurpose controls = new Multipurpose();
            controls.historyMessages(null, "Tus enemigos se estan moviendo...");

            //Movimiento aleatorio del NPC
            Random rand = new Random();

            //Lista de los nuevos npcs
            List<Box> newNPCs = new List<Box>();

            //Contador
            int count = 1;

            foreach (Box npc in npcs)
            {
                //end indica que se hizo el cambio
                bool end = false;

                do
                {
                    //0 <= move <= 3
                    int move = rand.Next(4);
                    Box neighbor = new Box();
                    Direction direction = new Direction();

                    //Movimiento
                    //  0
                    //3 X 1
                    //  2

                    //Se comprueba que la casilla a la que se quiere mover esta vacia

                    //UP
                    if (move == 0)
                    {
                        if (npc.neighbors[Direction.UP].getGround() != null && npc.neighbors[Direction.UP].getCharacter() == null)
                        {
                            end = true;
                            neighbor = npc.neighbors[Direction.UP];
                            direction = Direction.UP;
                        }
                    }
                    //RIGHT
                    else if (move == 1)
                    {
                        if (npc.neighbors[Direction.RIGHT].getGround() != null && npc.neighbors[Direction.RIGHT].getCharacter() == null)
                        {
                            end = true;
                            neighbor = npc.neighbors[Direction.RIGHT];
                            direction = Direction.RIGHT;
                        }
                    }
                    //DOWN
                    else if (move == 2)
                    {
                        if (npc.neighbors[Direction.DOWN].getGround() != null && npc.neighbors[Direction.DOWN].getCharacter() == null)
                        {
                            end = true;
                            neighbor = npc.neighbors[Direction.DOWN];
                            direction = Direction.DOWN;
                        }
                    }
                    //LEFT
                    else if (move == 3)
                    {
                        if (npc.neighbors[Direction.LEFT].getGround() != null && npc.neighbors[Direction.LEFT].getCharacter() == null)
                        {
                            end = true;
                            neighbor = npc.neighbors[Direction.LEFT];
                            direction = Direction.LEFT;
                        }
                    }

                    //Se hace el cambio de posicion del NPC (movimiento)
                    if (end)
                    {
                        moveNPC(neighbor, npc.getCharacter(), direction, ref newNPCs);
                    }
                }
                while (!end);

                count++;

                //Todos los npcs se movieron, por lo que es el turno del player
                if (count > Box.npcs.Count)
                {
                    Character.turnPlayer = true;
                }
            }

            //Cuando se termine de mover a todos los npcs, la lista se actualiza
            Box.npcs = newNPCs;
        }

        //Movimiento del NPC
        public void moveNPC(Box neighbor, Character npc, Direction direction, ref List<Box> newNPCs)
        {
            //La casilla del NPC se hace null antes de cambiarla por la casilla a la que se movera
            if (npc.getBox() != null)
            {
                npc.getBox().doBoxCharacterNull();
            }

            //Ajuste de valores
            neighbor.setCharacter(npc);
            neighbor.getCharacter().setPBLocation(neighbor.associatedButton);
            npc.setBox(neighbor);

            //Se agrega la nueva casilla donde esta el npc a la lista de npcs
            newNPCs.Add(neighbor);
        }

        //Se elimina un objeto de la casilla
        public void removeObject(Element myObj)
        {
            this.objects.Remove(myObj);
        }

        //Mostrar informacion de la casilla
        public void info()
        {
            if (this.boxGround != null)
            {
                this.boxGround.print();
            }
            else if (this.boxWall != null)
            {
                this.boxWall.print();
            }
            else if (this.boxDoor != null)
            {
                this.boxDoor.print();
            }
        }

        //Devolver la informacion de la casilla
        public string info(bool returnValue)
        {
            if (returnValue)
            {
                if (this.boxGround != null)
                {
                    return this.boxGround.print(true);
                }
                else if (this.boxWall != null)
                {
                    return this.boxWall.print(true);
                }
                else if (this.boxDoor != null)
                {
                    return this.boxDoor.print(true);
                }
                else if (this.boxCharacter != null)
                {
                    return "Character";
                }
                else
                {
                    return "EMPTY BOX";
                }
            }
            else
            {
                return "PARAMETER TRUE REQUIRED";
            }
        }

        #endregion

        #region Constructores

        //Constructor por defecto
        public Box()
        {

        }

        //Boton asociado
        public Box(Button myButton)
        {
            this.associatedButton = myButton;
            this.x = myButton.Location.X;
            this.y = myButton.Location.Y;
        }

        #endregion
    }
}
