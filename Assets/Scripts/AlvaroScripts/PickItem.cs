using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{

    public GameObject[] itemsToPickFrom; //array de objetos que se pueden recoger. Va a ser nuestra colección de items

    // Start is called before the first frame update
    void Start()
    {
        Pick(); //tenemos que asegurarnos de que nuestra FUNCIÓN "Pick" se ejecute cuando se inicie el juego. El juego se inicia, se llama a la función "Pick", cogerá un número random y hará aparecer un objeto random del array
    }

    void Pick()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length); 
        //esto nos va a devolver un número aleatorio entre dos números.
        //El length del array es el número de items que pongamos en el inspector
        //Básicamente le decimos: "dame un número random entre 0 y el número de items que hay en el array"
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], transform.position, Quaternion.identity);
        //Usamos "Instatiate" para crear una copia de un prefab en nuestro mundo. 
        //esto de arriba va a decir: "coge la cosa que esté en ese hueco del array, lo ponemos en la posición de este objeto (transform.position y lo rotamos a 0 (quaternion.identity)"
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
