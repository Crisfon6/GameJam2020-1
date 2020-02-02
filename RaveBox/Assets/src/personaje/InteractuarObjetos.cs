using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarObjetos : MonoBehaviour
{
    public Transform posicionObjetoCogido;
    public Transform posicionSoltarObjeto;
    public float esperaSoltandoObjeto;
    private GameObject _objetoCogido;
    private Transform _padreInicialObjeto;
    private bool _soltandoObjeto;
    private bool _botonPresionado;
    private Vector3 _posicionInicial;

    void Start()
    {
        _posicionInicial = transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Figura")
        {
            if(Input.GetAxisRaw("Vertical") < 0 && _objetoCogido == null && !_botonPresionado)
            {
                CogerObjecto(other.gameObject);
                _botonPresionado = true;
            }
        }
    }

    private void CogerObjecto(GameObject objeto)
    {
        _objetoCogido = objeto;
        var col = _objetoCogido.GetComponent<Collider2D>();
        col.enabled = false;
        var cuerpo = _objetoCogido.GetComponent<Rigidbody2D>();
        cuerpo.simulated = false;
        _objetoCogido.transform.position = posicionObjetoCogido.position;
        _padreInicialObjeto = _objetoCogido.transform.parent;
        _objetoCogido.transform.parent = transform;
    }

    void Update()
    {
        var verticalAxis = Input.GetAxisRaw("Vertical");
        if(verticalAxis < 0 && _objetoCogido != null && !_soltandoObjeto && !_botonPresionado)
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
        _objetoCogido.transform.parent = _padreInicialObjeto;
        _objetoCogido.transform.position = posicionSoltarObjeto.position;
        var col = _objetoCogido.GetComponent<Collider2D>();
        col.enabled = true; 
        var cuerpo = _objetoCogido.GetComponent<Rigidbody2D>();
        cuerpo.simulated = true;
        cuerpo.velocity = Vector3.zero;
        _objetoCogido = null;
        _soltandoObjeto = true;
        Invoke("ResetearSoltandoObjeto", esperaSoltandoObjeto);
    }

    private void ResetearSoltandoObjeto()
    {
        _soltandoObjeto = false;
    }

    public void Reiniciar()
    {
        if(_objetoCogido != null)
        {
            var figura = _objetoCogido.GetComponent<Figura>();
            figura.RestablecerPosicionInicial();
            _objetoCogido = null;
        }

        transform.position = _posicionInicial;
        var cuerpo = GetComponent<Rigidbody2D>();
        cuerpo.velocity = Vector3.zero;
        cuerpo.MovePosition(_posicionInicial);
    }
}