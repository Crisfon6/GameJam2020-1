using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trampas : MonoBehaviour
{
    public Text NumeroIntentos;
    private InteractuarObjetos _jugador;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            int n = int.Parse(NumeroIntentos.text);
            n+=1;
            NumeroIntentos.text = (n).ToString();
            _jugador = collision.gameObject.GetComponent<InteractuarObjetos>();
            Invoke("ReiniciarJugador", 1.5f);
        }
    }

    private void ReiniciarJugador()
    {
        _jugador.Reiniciar();
        _jugador = null;
    }
}