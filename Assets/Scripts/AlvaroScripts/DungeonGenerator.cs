using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation; // Importar el namespace para NavMesh
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject ceilingPrefab;

    public GameObject floorParent; 
    public GameObject wallParent;

    public GameObject Player;

    public bool isRoofNeeded = true;

    private int tilesToRemove = 350;

    public int mazeSize = 30;

    private bool isPlayerPlaced = false;

    private bool[,] mapData; 

    private void Start()
    {
        mapData = GenerateMazeData();

        for (int z = 0; z < mazeSize; z++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if (mapData[z, x])
                {
                    CreateChildPrefabInstance(wallPrefab, wallParent, new Vector3(x, 1, z));
                    CreateChildPrefabInstance(wallPrefab, wallParent, new Vector3(x, 2, z));
                    CreateChildPrefabInstance(wallPrefab, wallParent, new Vector3(x, 3, z));
                }
                else if (!isPlayerPlaced)
                {
                    Player.transform.SetPositionAndRotation(new Vector3(x, 1, z), Quaternion.identity);
                    isPlayerPlaced = true;
                }

                CreateChildPrefabInstance(floorPrefab, floorParent, new Vector3(x, 0, z));

                if (isRoofNeeded)
                {
                    CreateChildPrefabInstance(ceilingPrefab, wallParent, new Vector3(x, 4, z));
                }
            }
        }

        // Llamar al método para colocar antorchas DESPUÉS de generar la mazmorra
        TorchPlacer torchPlacer = GetComponent<TorchPlacer>();
        if (torchPlacer != null)
        {
            torchPlacer.PlaceTorches(mapData, mazeSize);
        }
    }

    bool[,] GenerateMazeData()
    {
        bool[,] data = new bool[mazeSize, mazeSize];

        // Inicializamos todas las paredes a true
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                data[y, x] = true;
            }
        }

        // Limpiar las paredes en la cantidad de tilesToRemove
        int tilesConsumed = 0;
        int mazeX = 0, mazeY = 0;

        // Iterar con nuestro crawler aleatorio y limpiar las paredes necesarias
        while (tilesConsumed < tilesToRemove)
        {
            int xDirection = 0, yDirection = 0;

            if (Random.value < 0.5)
                xDirection = Random.value < 0.5 ? 1 : -1;
            else
                yDirection = Random.value < 0.5 ? 1 : -1;

            int numSpaceMove = (int)(Random.Range(1, mazeSize - 1));

            for (int i = 0; i < numSpaceMove; i++)
            {
                mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
                mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);

                if (data[mazeY, mazeX])
                {
                    data[mazeY, mazeX] = false;
                    tilesConsumed++;
                }
            }
        }
        return data; 
    }

    void CreateChildPrefabInstance(GameObject prefab, GameObject parent, Vector3 spawnPosition)
    {
        var newGameObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        newGameObject.transform.parent = parent.transform;
    }
}