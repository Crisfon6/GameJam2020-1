using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float x;
    public float y;

     void OnTriggerEnter2D(Collider2D objetoChoca)
    {
        if(objetoChoca.tag == "Figura")
        {
            objetoChoca.transform.Translate(x, y, 0);
        }
    }
}
