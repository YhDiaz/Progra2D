using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using CanvasDrawing.Game;
using Project01.Game;
using Project01.models;
using Project01.UtalEngine;
using static Project01.models.Box;

namespace Project01
{
    public partial class Board : Form
    {
        Image playerImage;
        Frame playerFrame;

        Image npcImage;

        Multipurpose controls = new Multipurpose();

        //Forma del tablero:
        //-1: para indicar el limite (siempre en la ultima fila y en la ultima columna,
        //0: para indicar que no hay casilla
        //1: para indicar que hay una casilla vacia
        //2: para indicar que hay un muro
        //3: para indicar que hay una puerta cerrada
        //4: para indicar que hay una puerta abierta
        //5: para indicar que esta el personaje
        //6: para indicar que hay un NPC
        //7: para indicar que hay objetos
        //8: para indicar el punto de termino del juego
        int[,] board =
        {
            { 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 2, -1 },
            { 2, 5, 7, 2, 2, 2, 2, 2, 1, 1, 1, 6, 2, -1 },
            { 2, 1, 1, 4, 7, 1, 1, 4, 1, 1, 1, 1, 2, -1 },
            { 2, 1, 6, 2, 2, 2, 2, 2, 2, 2, 2, 4, 2, -1 },
            { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
            { 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 4, 2, -1 },
            { 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, -1 },
            { 2, 8, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 2, -1 },
            { 2, 6, 1, 1, 2, 2, 2, 2, 2, 1, 1, 6, 2, -1 },
            { 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, -1 },
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
        };

        //1: Llave; 2: Casco; 3: Coraza; 4: Espada; 5: Hoz
        string[,] objects =
        {
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "1234", "0", "1234", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
            { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        };

        public Box[,] boxes;

        public Board()
        {
            int rows = 0, columns = 0;
            controls.getMatrixLength(board, ref rows, ref columns);

            //Imagen y frame del player
            playerImage = global::Project01.Properties.Resources.Player;
            playerFrame = new Frame(1, playerImage, new Vector2(50, 50));

            //Imagen de los NPCs
            npcImage = global::Project01.Properties.Resources.NPC;

            //Cantidad de NPCs en el tablero
            for (int i = 0; i < controls.getNumOfSameContent(board, rows, columns, 6); i++)
            {
                new Frame(i, npcImage, new Vector2(50, 50), i * 50, 200 + i * 50);
            }

            DoubleBuffered = true;
            InitializeComponent();

            OriginalCode();
            
            GameEngine.InitEngine(this);
            GameEngine.MainCamera.scale = 1f;
            GameEngine.MainCamera.xSize = GameEngine.MainCamera.ySize = 600;
            GameEngine.MainCamera.Position = new Vector2(150, 200);
            new FrameManager();
        }

        public void OriginalCode()
        {
            Box.myForm = this;

            //Mensaje
            controls.historyMessages(this.history, "Creando tablero...");

            //Se crea un dato Box para almacenar los datos del boton pivot
            Box pivot = new Box();
            pivot.setPos(pivotButton.Location.X, pivotButton.Location.Y);

            //Se ocultan los botones
            this.pivotButton.Hide();
            this.player.Hide();

            //Filas y columnas del tablero
            int rows = 0, columns = 0;

            //Se obtienen, por referencia, la cantidad de filas y columnas de la matriz
            controls.getMatrixLength(board, ref rows, ref columns);
            Multipurpose.boardRows = rows;
            Multipurpose.boardColumns = columns;

            //Tablero de casillas
            boxes = new Box[rows, columns];

            //A partir del pivot, se crea el tablero
            createBoard(rows, columns, boxes);

            controls.historyMessages(null, "Tablero creado");
        }

        ////Objeto multiproposito
        //Multipurpose controls = new Multipurpose();

        //Player myPlayer;
        //public static List<Button> buttonsInventory = new List<Button>();
        //public static Button elementButtonSelected;

        ////Forma del tablero:
        ////-1: para indicar el limite (siempre en la ultima fila y en la ultima columna,
        ////0: para indicar que no hay casilla
        ////1: para indicar que hay una casilla vacia
        ////2: para indicar que hay un muro
        ////3: para indicar que hay una puerta cerrada
        ////4: para indicar que hay una puerta abierta
        ////5: para indicar que esta el personaje
        ////6: para indicar que hay un NPC
        ////7: para indicar que hay objetos
        ////8: para indicar el punto de termino del juego
        //int[,] board =
        //{
        //    { 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 2, -1 },
        //    { 2, 5, 7, 2, 2, 2, 2, 2, 1, 1, 1, 6, 2, -1 },
        //    { 2, 1, 1, 4, 7, 1, 1, 4, 1, 1, 1, 1, 2, -1 },
        //    { 2, 1, 6, 2, 2, 2, 2, 2, 2, 2, 2, 4, 2, -1 },
        //    { 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
        //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
        //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, -1 },
        //    { 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 4, 2, -1 },
        //    { 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, -1 },
        //    { 2, 8, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 2, -1 },
        //    { 2, 6, 1, 1, 2, 2, 2, 2, 2, 1, 1, 6, 2, -1 },
        //    { 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, -1 },
        //    { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
        //};

        ////1: Llave; 2: Casco; 3: Coraza; 4: Espada; 5: Hoz
        //string[,] objects =
        //{
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "1234", "0", "1234", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        //};

        //public Box[,] boxes;

        //public Board()
        //{
        //    InitializeComponent();

        //    Box.myForm = this;

        //    //Mensaje
        //    controls.historyMessages(this.history, "Creando tablero...");

        //    //Se crea un dato Box para almacenar los datos del boton pivot
        //    Box pivot = new Box();
        //    pivot.setPos(pivotButton.Location.X, pivotButton.Location.Y);

        //    //Se ocultan los botones
        //    this.pivotButton.Hide();
        //    this.player.Hide();

        //    //Filas y columnas del tablero
        //    int rows = 0, columns = 0;

        //    //Se obtienen, por referencia, la cantidad de filas y columnas de la matriz
        //    controls.getMatrixLength(board, ref rows, ref columns);
        //    Multipurpose.boardRows = rows;
        //    Multipurpose.boardColumns = columns;

        //    //Tablero de casillas
        //    boxes = new Box[rows, columns];

        //    //A partir del pivot, se crea el tablero
        //    createBoard(rows, columns, boxes);

        //    controls.historyMessages(null, "Tablero creado");
        //}

        //Creacion del tablero
        public void createBoard(int numRow, int numCol, Box[,] boxes)
        {
            //Contadores
            int ground = 0, walls = 0, doors = 0, npcs = 0;

            //numCol es la cantidad de columnas de board
            for (int i = 0; i < numCol; i++)
            {
                //numRow es la cantidad de filas de board
                for (int j = 0; j < numRow; j++)
                {
                    //Si en el tablero se indica que hay una casilla en la posicion actual, se ejecuta el bloque
                    if (board[j, i] != 0)
                    {
                        //Se crea el boton y se asignan sus valores
                        Button button = new Button();
                        setButton(ref button, i, j);

                        //Se crea la nueva casilla y se le asigna el boton que le corresponde
                        Box box = new Box(button);

                        //Asignacion de valores a la casilla dependiendo de su contenido
                        switch (board[j, i])
                        {
                            //Suelo
                            case 1:
                                ground++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Ground;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Ground objGround = new Ground("G" + ground);
                                box.setContent(objGround);
                                break;

                            //Muros
                            case 2:
                                walls++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Wall;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Wall objWall = new Wall("W" + walls);
                                box.setContent(objWall);
                                break;

                            //Puertas cerradas
                            case 3:
                                doors++;
                                button.BackgroundImage = global::Project01.Properties.Resources.CloseDoor;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Door objCloseDoor = new Door("D" + doors);
                                box.setContent(objCloseDoor);
                                break;

                            //Puertas abiertas
                            case 4:
                                doors++;
                                button.BackgroundImage = global::Project01.Properties.Resources.OpenDoor;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Door objOpenDoor = new Door("D" + doors, true);
                                box.setContent(objOpenDoor);
                                break;

                            //Player
                            case 5:
                                ground++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Ground;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Ground objGroundPlayer = new Ground("G" + ground);
                                box.setContent(objGroundPlayer);

                                //PictureBox playerPicBox = initializeCharacterPicBox("player", box);
                                //myPlayer = new Player(playerPicBox, box, "Player");
                                //box.setCharacter(myPlayer);

                                break;

                            //NPCs
                            case 6:
                                npcs++;
                                ground++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Ground;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Ground objGroundNPC = new Ground("G" + ground);
                                box.setContent(objGroundNPC);

                                //PictureBox npcPicBox = initializeCharacterPicBox("NPC" + npcs, box);
                                //NPC objNPC = new NPC("NPC" + npcs, box, npcPicBox);

                                //box.setCharacter(objNPC);
                                //Box.npcs.Add(box);

                                break;

                            case 7:
                                ground++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Ground;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Ground objGroundElement = new Ground("G" + ground);
                                box.setContent(objGroundElement);

                                //box.setObjectList(createObjList(objects[j, i], box));

                                break;

                            case 8:
                                ground++;
                                button.BackgroundImage = global::Project01.Properties.Resources.Medal;
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                Ground objGroundMedal = new Ground("G" + ground);
                                box.setContent(objGroundMedal);
                                Box.medalBox = box;

                                break;
                        }

                        //Se agrega la nueva casilla a la matriz de casillas
                        boxes[j, i] = box;

                        //Evento click en el boton
                        button.Click += new System.EventHandler(boxes[j, i].click);

                        //Evento TeclaPresionada en el boton
                        if (board[j, i] == 3 || board[j, i] == 4)
                        {
                            button.KeyUp += new KeyEventHandler(boxes[j, i].getDoor().open);
                        }

                        //Se agrega el boton al form
                        this.Controls.Add(button);
                    }
                    else
                    {
                        boxes[j, i] = null;
                    }
                }
            }

            //Indicar los vecinos de la casilla
            for (int i = 0; i < numCol; i++)
            {
                for (int j = 0; j < numRow; j++)
                {
                    if (board[j, i] != 0)
                    {
                        //La casilla actual tiene al menos una casilla arriba
                        if (j > 0)
                        {
                            boxes[j, i].setNeighbor(boxes[j - 1, i], Box.Direction.UP);
                        }

                        //La casilla actual tiene al menos una casilla abajo
                        if (j < numRow - 1)
                        {
                            boxes[j, i].setNeighbor(boxes[j + 1, i], Box.Direction.DOWN);
                        }

                        //La casilla actual tiene al menos una casilla a la izquierda
                        if (i > 0)
                        {
                            boxes[j, i].setNeighbor(boxes[j, i - 1], Box.Direction.LEFT);
                        }

                        //La casilla actual tiene al menos una a la derecha
                        if (i < numCol - 1)
                        {
                            boxes[j, i].setNeighbor(boxes[j, i + 1], Box.Direction.RIGHT);
                        }
                    }
                }
            }
        }

        //Click en el NPC
        private void npcClick(object sender, EventArgs e)
        {
            controls.historyMessages(null, "Saluda al NPC... Suficiente, hora de destruirlo");
        }

        //Click en el player
        public void playerClick(object sender, EventArgs e)
        {
            controls.historyMessages(null, "Player seleccionado");

            if (Box.currentClick != string.Empty)
            {
                Box.lastClick = Box.currentClick;
            }

            Box.currentClick = "PLAYER";
        }

        //Se establecen los valores del boton
        public void setButton(ref Button button, int i, int j)
        {
            int spaceBtwn = 1;

            button.Location = new System.Drawing.Point(this.pivotButton.Location.X + ((spaceBtwn + this.pivotButton.Size.Width) * i), this.pivotButton.Location.Y + (j * (spaceBtwn + this.pivotButton.Size.Width)));
            button.Size = new System.Drawing.Size(this.pivotButton.Size.Width, this.pivotButton.Size.Width);
            button.Enabled = true;
            button.TextImageRelation = TextImageRelation.ImageAboveText;
        }

        //Inicializacion del picture box de un personaje
        public PictureBox initializeCharacterPicBox(string id, Box box)
        {
            int posX = 0, posY = 0, size = this.pivotButton.Size.Width;

            box.getPos(ref posX, ref posY);

            PictureBox picBox = new PictureBox();
            picBox.Location = new Point(posX, posY);
            picBox.Size = new Size(size, size);
            picBox.TabIndex = 1;
            picBox.TabStop = false;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;

            if (id == "player")
            {
                picBox.Image = global::Project01.Properties.Resources.Player;
                picBox.Name = "Player";
                picBox.Click += new System.EventHandler(this.playerClick);
            }
            else
            {
                picBox.Image = global::Project01.Properties.Resources.NPC;
                picBox.Name = id;
                picBox.Click += new System.EventHandler(this.npcClick);
            }

            this.Controls.Add(picBox);
            return picBox;
        }

        ////Crear lista de objetos
        //public List<Element> createObjList(string elements, Box box)
        //{
        //    List<Element> objectsList = new List<Element>();

        //    for (int i = 0; i < elements.Length; i++)
        //    {
        //        string type = objectType(Convert.ToInt16(elements[i].ToString()));
        //        int amount = Element.elements.Count + 1;

        //        string id = type + "_" + amount;

        //        Button objButton = initializeObjectButton(id, type, box, elements.Length, i);
        //        Element newObject = new Element(id, type, objButton);
        //        newObject.setBox(box);
        //        objectsList.Add(newObject);
        //        Element.elements.Add(newObject);
        //    }

        //    return objectsList;
        //}

        ////Inicializacion del picture box de un objeto
        //public Button initializeObjectButton(string id, string type, Box box, int objectsAmount, int i)
        //{
        //    int posX = 0, posY = 0, size = this.pivotButton.Size.Width;

        //    box.getPos(ref posX, ref posY);

        //    Button objButton = new Button();

        //    if (objectsAmount > 1)
        //    {
        //        objButton.Location = new Point(posX + ((i % 2) * size / 2), posY + ((i / 2) * (size / 2)));
        //        objButton.Size = new Size(size / 2, size / 2);
        //    }
        //    else
        //    {
        //        objButton.Location = new Point(posX, posY);
        //        objButton.Size = new Size(size, size);
        //    }

        //    objButton.TabIndex = 1;
        //    objButton.TabStop = false;
        //    objButton.Name = id;
        //    objButton.Click += new System.EventHandler(this.objectClick);
        //    objButton.Enabled = true;

        //    switch (type)
        //    {
        //        case "key":
        //            objButton.BackgroundImage = global::Project01.Properties.Resources.Key;
        //            break;

        //        case "helmet":
        //            objButton.BackgroundImage = global::Project01.Properties.Resources.Helmet;
        //            break;

        //        case "breastplate":
        //            objButton.BackgroundImage = global::Project01.Properties.Resources.Breastplate;
        //            break;

        //        case "sword":
        //            objButton.BackgroundImage = global::Project01.Properties.Resources.Sword;
        //            break;

        //        case "sickle":
        //            objButton.BackgroundImage = global::Project01.Properties.Resources.Sickle;
        //            break;
        //    }

        //    objButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        //    objButton.BackgroundImageLayout = ImageLayout.Stretch;

        //    this.Controls.Add(objButton);
        //    return objButton;
        //}

        ////Identificador del objeto que hay en la casilla
        //public string objectType(int n)
        //{
        //    switch (n)
        //    {
        //        case 1: return "key";
        //        case 2: return "helmet";
        //        case 3: return "breastplate";
        //        case 4: return "sword";
        //        case 5: return "sickle";
        //        default: return "ERROR";
        //    }
        //}

        ////Click en un objeto
        //private void objectClick(object sender, EventArgs e)
        //{
        //    //Multipurpose controls = new Multipurpose();
        //    Element myNewElement = new Element();
        //    Box objAssociatedBox = new Box();
        //    Button buttonAssociated = new Button();

        //    for (int i = 0; i < Element.elements.Count; i++)
        //    {
        //        if (Element.elements[i] != null && sender == Element.elements[i].getButton())
        //        {
        //            objAssociatedBox = Element.elements[i].getBox();
        //            myNewElement = Element.elements[i];
        //            buttonAssociated = Element.elements[i].getButton();
        //        }
        //    }

        //    bool elementAdded = false, neighborFound = false;

        //    foreach (Box neighbor in myPlayer.getBox().neighbors.Values)
        //    {
        //        if (neighbor == objAssociatedBox)
        //        {
        //            neighborFound = true;

        //            myPlayer.addObjects(myNewElement, objAssociatedBox, ref elementAdded);

        //            if (elementAdded)
        //            {
        //                this.Controls.Remove(buttonAssociated);
        //            }
        //        }
        //    }

        //    if (!elementAdded && !neighborFound)
        //    {
        //        controls.historyMessages(null, "El objeto esta muy lejos, acercate mas para tomarlo");
        //    }
        //}

        ////Click en el boton de inventario
        //private void inventoryButton_Click(object sender, EventArgs e)
        //{
        //    //Si el inventario no esta desplegado, se despliega y vice versa
        //    if (Player.deployedInventory)
        //    {
        //        Player.deployedInventory = false;
        //    }
        //    else
        //    {
        //        Player.deployedInventory = true;
        //    }

        //    this.showInventory();
        //}

        ////Desplegar/contraer inventario
        //public void showInventory()
        //{
        //    //Si hay que desplegar el inventario, se crean los botones
        //    if (Player.deployedInventory)
        //    {
        //        //Posicion del boton de inventario
        //        int x = this.inventoryButton.Location.X, y = this.inventoryButton.Location.Y;

        //        //Medida del boton y distancia entre botones
        //        int size = this.pivotButton.Size.Width, space = 40;

        //        for (int i = 0; i < myPlayer.getInventory().Count; i++)
        //        {
        //            Button elementButton = myPlayer.getInventory()[i].getButton();
        //            elementButton.Location = new Point(x, (y * (i + 1)) + space);
        //            elementButton.Size = new Size(size, size);

        //            //Se elimina el evento click anterior y se asigna uno nuevo
        //            elementButton.Click -= this.objectClick;

        //            Board.buttonsInventory.Add(elementButton);
        //            this.Controls.Add(elementButton);
        //        }
        //    }
        //    //Si hay que contraer el inventario, se eliminan los botones
        //    else
        //    {
        //        for (int i = 0; i < Board.buttonsInventory.Count; i++)
        //        {
        //            this.Controls.Remove(buttonsInventory[i]);
        //        }

        //        Board.buttonsInventory.Clear();
        //    }
        //}

        //Fin del juego
        public void end()
        {
            this.Close();
        }
    }
}