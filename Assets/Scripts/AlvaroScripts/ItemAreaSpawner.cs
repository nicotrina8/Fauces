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
    }

    void SpreadItem()
    {
        Vector3 randPosition = new Vector3(
            Random.Range(1, dungeonGenerator.mazeSize - 1),
            Random.Range(2,0), // Altura desde donde cae el objeto
            Random.Range(1, dungeonGenerator.mazeSize - 1)) + dungeonGenerator.transform.position;

        GameObject clone = Instantiate(itemToSpread, randPosition, Quaternion.identity);

        // Asegurarse de que el Rigidbody está activado para que la física se aplique
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Asegurarse de que la física esté activada
        }

        // Opcionalmente, desactivar el Rigidbody después de un tiempo para que quede en una posición fija
        StartCoroutine(DisablePhysics(clone, 2.0f)); // Desactivar física después de 2 segundos
    }

    IEnumerator DisablePhysics(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Desactivar la física para que el objeto quede fijo
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
