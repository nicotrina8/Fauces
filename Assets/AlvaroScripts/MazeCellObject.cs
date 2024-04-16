using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour {

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;

    public void Init (bool top, bool botttom, bool right, bool left) {

        topWall.SetActive(top);
        bottomWall.SetActive(botttom);
        rightWall.SetActive(right);
        leftWall.SetActive(left);
    }
}
