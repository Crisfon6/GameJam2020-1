using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D objetoChoca)
    {
        Debug.Log(objetoChoca);
        Debug.Log((objetoChoca.transform.position.x));
        objetoChoca.transform.Translate(objetoChoca.transform.position.x+20,0,0);
        Debug.Log("Entro");
        
        
        
    }
/*    private void OnTriggerExit2D(Collider2 other)
    {
       Debug.Log("Entro");
    }*/
}
