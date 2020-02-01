using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoFigura { TRIANGULO, CUADRADO, CIRCULO, ESTRELLA, OCTAGONO, SHURINKEN };

public class Figura : MonoBehaviour
{
    public TipoFigura tipo;
    private Vector3 _posicionInicial;

    void Start()
    {
        _posicionInicial = transform.position;
    }

    public void RestablecerPosicionInicial()
    {
        transform.position = _posicionInicial;
        var cuerpo = GetComponent<Rigidbody2D>();
        cuerpo.simulated = true;
        cuerpo.velocity = Vector2.zero;
    }
}