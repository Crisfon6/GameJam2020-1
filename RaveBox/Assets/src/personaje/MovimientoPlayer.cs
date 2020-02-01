using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float tolInput;
    public float velCaminado;
    public float poderSalto;
    public float fuerzaEnAire;
    public float resistenciaAire;
    private bool _sobreElPiso;
    private Rigidbody2D _cuerpo;
    private bool _estaSaltando;

    void Start()
    {
        _cuerpo = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D()
    {
        _cuerpo.velocity = Vector2.zero;
    }

    void OnCollisionExit2D()
    {
    }

    void OnTriggerEnter2D()
    {
        _sobreElPiso = true;
        _estaSaltando = false;
    }

    void OnTriggerExit2D()
    {
        _sobreElPiso = false;
    }

    void Update()
    {
        if(_sobreElPiso)    // mientras este en contacto con el suelo.
        {
            if(Input.GetAxis("Vertical") > 0 && !_estaSaltando)
            {
                Debug.Log("Saltar");
                _estaSaltando = true;
                var horizontal = Vector2.right * Input.GetAxis("Horizontal") * velCaminado;
                var vertical = Vector2.up * poderSalto;
                _cuerpo.velocity = vertical + horizontal;
            }
            else if(Mathf.Abs(Input.GetAxis("Horizontal")) > tolInput && !_estaSaltando)
            {
                var deltaX = velCaminado * Vector2.right * Mathf.Sign(Input.GetAxis("Horizontal"));
                _cuerpo.MovePosition( _cuerpo.position + deltaX * Time.deltaTime );
            }
        }
        else    // mientras este en el aire.
        {
            var fuerzaExtraAire = Vector2.zero;
            if(Input.GetAxis("Vertical") > tolInput && _estaSaltando)
            {
                fuerzaExtraAire += VelocidadVerticalExtra();
            }
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > tolInput && _estaSaltando)
            {
                var horizontal = Vector2.right * Input.GetAxis("Horizontal") * fuerzaEnAire;
                fuerzaExtraAire += horizontal;
            }
            _cuerpo.AddForce(fuerzaExtraAire);
            var velHorizontalAire = -_cuerpo.velocity.x * resistenciaAire;
            _cuerpo.velocity += velHorizontalAire * Vector2.right;
        }
    }

    private Vector2 VelocidadVerticalExtra()
    {
        return Vector3.zero;
    }
}