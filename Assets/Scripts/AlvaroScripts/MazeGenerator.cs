using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    [Range(5, 500)]
    public int mazeWidth = 5, mazeHeight = 5; //Tamaño de la mazmorra
    public int startX, startY; //Posición de inicio del algoritmo
    MazeCell[,] maze; //Array bidimensional de celdas

    Vector2Int currentCell; //Celda actual

    public MazeCell[,] GetMaze() { 
        
        //Inicializa el array de celdas
        maze = new MazeCell[mazeWidth, mazeHeight];

        //Inicializa cada celda en el array
        for (int x = 0; x < mazeWidth; x++) {
            for (int y = 0; y < mazeHeight; y++) {
                maze[x, y] = new MazeCell(x, y); 
            }
        }

        CarvePath(startX, startY);

        return maze;
    }

    List<Direction> directions = new List<Direction> {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right
    };

    List<Direction> GetRandomDirections () {

        //Hacer una copia de la lista de direcciones
        List<Direction> dir = new List<Direction>(directions);

        //Hacer otra lista para las direcciones en orden aleatorio
        List<Direction> rndDir = new List<Direction>();

        while (dir.Count > 0) { //Loop hasta que la lista rndDir esté vacía

            int rnd = Random.Range(0, dir.Count); //Escoge un índice aleatorio de la lista dir
            rndDir.Add(dir[rnd]);   //Añade la dirección en ese índice a la lista rndDir
            dir.RemoveAt(rnd);      //Quita esa dirección para que no se repita
        }

        return rndDir; //Cuando tengamos las cuatro direcciones en un orden aleatorio, devolver la lista
    }

    bool IsCellValid (int x, int y) {

        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited) return false; 
        else return true;

    }

    Vector2Int CheckNeighbours() {

        List<Direction> rndDir = GetRandomDirections(); //Obtener las direcciones en orden aleatorio

        for (int i = 0; i < rndDir.Count; i++) {

            Vector2Int neighbour = currentCell; //Inicializar la celda vecina con la celda actual

            //Modifica las coordinadas de las celdas vecinas basado en las direcciones aleatorias
            switch (rndDir[i]) {

                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;

            }

            //Si el vecino es una celda válida, devolver sus coordenadas, si no lo hacemos otra vez

            if (IsCellValid(neighbour.x, neighbour.y)) return neighbour;
        }


        return currentCell; //Si no hay celdas válidas, devolver la celda actual
    }

    void BreakWalls (Vector2Int primaryCell, Vector2Int secondaryCell) {

        //Solo podemos ir hacia una sola direccion a la vez, y podemos hacer esto mediante if statements
        if (primaryCell.x > secondaryCell.x) { //Primary Cell left wall

            maze[primaryCell.x, primaryCell.y].leftWall = false;

        } else if (primaryCell.x < secondaryCell.x) { //Secondary Cell left wall

            maze[secondaryCell.x, secondaryCell.y].leftWall = false;

        } else if (primaryCell.y < secondaryCell.y) { //Primary Cell top wall

            maze[primaryCell.x, primaryCell.y].topWall = false;

        } else if (primaryCell.y > secondaryCell.y) { //Secondary Cell top wall

            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
    }

    //Empezando en la x, y que se han dado, se crea un camino a través de la mazmorra hasta que encuentre un dead end
    //un dead end es una celda que no tiene vecinos válidos
    void CarvePath (int x, int y) {

        //Lleva a cabo un check rápido para asegurarse de que nuestra posición inicial está dentro de los límites del mapa
        //si no, establecelos a default (usamos 0) y pon un aviso
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1){

            x = y = 0;
            Debug.LogWarning("Start position out of bounds. Resetting to default values.");
        }

        //Establece la celda actual en la posición inicial
        currentCell = new Vector2Int(x, y);

        List<Vector2Int> path = new List<Vector2Int>();

        //Loop hasta que no haya vecinos válidos
        bool deadEnd = false; 
        while (!deadEnd) {

            //Coge la siguiente celda vecina que vamos a probar
            Vector2Int nextCell = CheckNeighbours();

            //Si la celda no tiene vecinos válidos, entonces es un dead end true así que salimos del loop
            if (nextCell == currentCell) {

                //Si la celda no tiene vecinos válidos, entonces es un dead end true así que salimos del loop
                for (int i = path.Count -1; i >= 0; i--) {

                    currentCell = path[i]; //Establece la celda actual al siguiente paso atrás en el camino
                    path.RemoveAt(i); //Elimina este paso del camino
                    nextCell = CheckNeighbours(); //Comprueba esta celda para ver si tiene vecinos válidos

                    //Si encontramos un vecino válido, salir del loop y continuar con el algoritmo
                    if (nextCell != currentCell) break;

                }

                if (nextCell == currentCell)
                    deadEnd = true; //Si no hay vecinos válidos, entonces es un dead end

            } else {


                BreakWalls(currentCell, nextCell); //Si la celda tiene vecinos válidos, rompe las paredes entre las celdas
                maze[currentCell.x, currentCell.y].visited = true; //Marca la celda actual como visitada
                currentCell = nextCell; //Establece la celda actual a la celda vecina
                path.Add(currentCell); //Añade la celda actual al camino

            }

        }
    }

}

public enum Direction {
    Up, 
    Down, 
    Left, 
    Right
}

public class  MazeCell : MonoBehaviour {

    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall; 

    //Devuelve x e y como Vector2Int por conveniencia 

    public Vector2Int position { 
        get {

            return new Vector2Int(x, y);
        }
    
    }

    public MazeCell (int x, int y) {

        //Las coordenadas de la celda en el grid de la mazmorra
        this.x = x;
        this.y = y;

        //Si el algoritmo ha visitado esta celda o no -- empieza con false
        visited = false;

        //Por default, todas las paredes están presentes hasta que el algoritmo las quite
        topWall = leftWall = true;
    }
}