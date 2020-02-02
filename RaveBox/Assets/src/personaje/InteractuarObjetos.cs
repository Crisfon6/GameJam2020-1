using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarObjetos : MonoBehaviour
{
    public Transform posicionObjetoCogido;
    public Transform posicionSoltarObjeto;
    public float esperaSoltandoObjeto;
    private GameObject objetoCogido;
    private Transform _padreInicialObjeto;
    private bool _soltandoObjeto;
    private bool _botonPresionado;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Figura")
        {
            if(Input.GetAxisRaw("Vertical") < 0 && objetoCogido == null && !_botonPresionado)
            {
                CogerObjecto(other.gameObject);
                _botonPresionado = true;
            }
        }
    }

    private void CogerObjecto(GameObject objeto)
    {
        objetoCogido = objeto;
        var col = objetoCogido.GetComponent<Collider2D>();
        col.enabled = false;
        var cuerpo = objetoCogido.GetComponent<Rigidbody2D>();
        cuerpo.simulated = false;
        objetoCogido.transform.position = posicionObjetoCogido.position;
        _padreInicialObjeto = objetoCogido.transform.parent;
        objetoCogido.transform.parent = transform;
    }

    void Update()
    {
        var verticalAxis = Input.GetAxisRaw("Vertical");
        if(verticalAxis < 0 && objetoCogido != null && !_soltandoObjeto && !_botonPresionado)
        {
            SoltarObjetoCogido();
            _botonPresionado = true;
        }
        else if(verticalAxis == 0 && _botonPresionado)
        {
            _botonPresionado = false;
        }
    }

    private void SoltarObjetoCogido()
    {
        objetoCogido.transform.parent = _padreInicialObjeto;
        objetoCogido.transform.position = posicionSoltarObjeto.position;
        var col = objetoCogido.GetComponent<Collider2D>();
        col.enabled = true; 
        var cuerpo = objetoCogido.GetComponent<Rigidbody2D>();
        cuerpo.simulated = true;
        cuerpo.velocity = Vector3.zero;
        objetoCogido = null;
        _soltandoObjeto = true;
        Invoke("ResetearSoltandoObjeto", esperaSoltandoObjeto);
    }

    private void ResetearSoltandoObjeto()
    {
        _soltandoObjeto = false;
    }
}