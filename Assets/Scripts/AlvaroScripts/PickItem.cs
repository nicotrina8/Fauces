using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{

    public GameObject[] itemsToPickFrom; //array de objetos que se pueden recoger. Va a ser nuestra colecci�n de items

    // Start is called before the first frame update
    void Start()
    {
        Pick(); //tenemos que asegurarnos de que nuestra FUNCI�N "Pick" se ejecute cuando se inicie el juego. El juego se inicia, se llama a la funci�n "Pick", coger� un n�mero random y har� aparecer un objeto random del array
    }

    void Pick()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length); 
        //esto nos va a devolver un n�mero aleatorio entre dos n�meros.
        //El length del array es el n�mero de items que pongamos en el inspector
        //B�sicamente le decimos: "dame un n�mero random entre 0 y el n�mero de items que hay en el array"
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], transform.position, Quaternion.identity);
        //Usamos "Instatiate" para crear una copia de un prefab en nuestro mundo. 
        //esto de arriba va a decir: "coge la cosa que est� en ese hueco del array, lo ponemos en la posici�n de este objeto (transform.position y lo rotamos a 0 (quaternion.identity)"
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
