using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float tolInput;
    public float velCaminado;
    private bool _sobreElPiso;
    private Rigidbody2D _cuerpo;

    void Start()
    {
        _cuerpo = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D()
    {
        _sobreElPiso = true;
    }

    void OnCollisionExit2D()
    {
        _sobreElPiso = false;
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") > tolInput && _sobreElPiso)
        {
            _cuerpo.MovePosition( velCaminado * Vector2.right * Time.deltaTime );
        }
        if(Input.GetAxis("Horizontal") < -tolInput && _sobreElPiso)
        {
            _cuerpo.MovePosition( velCaminado * Vector2.left * Time.deltaTime );
        }
    }
}