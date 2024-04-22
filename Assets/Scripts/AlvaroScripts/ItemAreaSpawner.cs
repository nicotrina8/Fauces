using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAreaSpawner : MonoBehaviour
{

    public GameObject itemToSpread;
    public int numItemsToSpawn = 10; 
    public DungeonGenerator dungeonGenerator; //Referencia a la clase DungeonGenerator

    public float itemXSpread = 10;
    public float itemYSpread = 0;
    public float itemZSpread = 10;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < numItemsToSpawn; i++) //vamos a correr este loop 10 veces
        {
            SpreadItem();
        }

        SpreadItem();
    }

    void SpreadItem()
    {
        Vector3 randPosition = new Vector3 (
            Random.Range(1, dungeonGenerator.mazeSize - 1),
            Random.Range(1, 0), 
            Random.Range(1, dungeonGenerator.mazeSize - 1)) + dungeonGenerator.transform.position;
        GameObject clone = Instantiate(itemToSpread, randPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
