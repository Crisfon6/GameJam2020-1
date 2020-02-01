using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
float speed;
float position;
private void Update() {
    if(Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate( 5*Time.deltaTime,0, 0);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(-5*Time.deltaTime,0 , 0);
        }

        if(Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(0, -5*Time.deltaTime, 0);
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(0, -5*Time.deltaTime, 0);
        }
}
private void Start() {
    
}

}