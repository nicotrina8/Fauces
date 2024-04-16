using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPos : MonoBehaviour
{
    public Transform camaraPos;

    private void Update(){
        transform.position = camaraPos.position;
    }
    
    
    }
