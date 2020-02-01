using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trampas : MonoBehaviour
{
    public Text NumeroIntentos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            int n = int.Parse(NumeroIntentos.text);
            n+=1;
            NumeroIntentos.text = (n).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
