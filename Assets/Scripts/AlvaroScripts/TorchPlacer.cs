using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPlacer : MonoBehaviour
{
    public GameObject torchPrefab;
    public Transform wallParent;

    public void PlaceTorches(bool[,] mapData, int mazeSize)
    {
        for (int z = 0; z < mazeSize; z++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if (mapData[z, x] && Random.value < 0.1f && IsWallNextToPath(mapData, x, z, mazeSize))
                {
                    Vector3 torchPosition = new Vector3(x, 2, z); // Ajustar la altura según sea necesario
                    Vector3 wallNormal = GetWallNormal(mapData, x, z, mazeSize);

                    if (wallNormal != Vector3.zero)
                    {
                        bool isSpaceFree = Physics.Raycast(torchPosition + Vector3.up,Vector3.down, 1f); // Comprobar si hay espacio para colocar la antorcha
                        if (!isSpaceFree)
                        {
                            continue;
                        }

                        float randomOffset = Random.Range(-0.2f, 0.2f);
                        torchPosition += wallNormal * (0.5f + randomOffset); // Ajustar la posición de la antorcha para que no esté en el centro de la pared

                        var torch = Instantiate(torchPrefab, torchPosition, Quaternion.identity, wallParent);
                        torch.transform.forward = wallNormal;
                        torch.transform.rotation = Quaternion.LookRotation(wallNormal) * Quaternion.Euler(0, 90, 0);
                    }
                    else
                    {
                        Debug.Log($"No valid wall normal found for torch at: {new Vector3(x, 2, z)}");
                    }
                }
            }
        }
    }

    Vector3 GetWallNormal(bool[,] mapData, int x, int z, int mazeSize)
    {
        if (x > 0 && !mapData[z, x - 1])
            return Vector3.left; // Pared orientada a la izquierda
        if (x < mazeSize - 1 && !mapData[z, x + 1])
            return Vector3.right; // Pared orientada a la derecha
        if (z > 0 && !mapData[z - 1, x])
            return Vector3.back; // Pared orientada hacia atrás
        if (z < mazeSize - 1 && !mapData[z + 1, x])
            return Vector3.forward; // Pared orientada hacia adelante

        return Vector3.zero; // No se encontró una pared válida
    }

    bool IsWallNextToPath(bool[,] mapData, int x, int z, int mazeSize)
    {
        // Verificar si hay un camino (false en mapData) al lado de la pared
        if ((x > 0 && !mapData[z, x - 1]) || // Oeste
            (x < mazeSize - 1 && !mapData[z, x + 1]) || // Este
            (z > 0 && !mapData[z - 1, x]) || // Sur
            (z < mazeSize - 1 && !mapData[z + 1, x])) // Norte
        {
            return true;
        }
        return false;
    }
}
