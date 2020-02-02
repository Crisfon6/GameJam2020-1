using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public string axisHorizontal;
    public string axisVertical;
    public float tolInput;
    public float velCaminado;
    public AnimationCurve poderSalto;
    public float fuerzaEnAire;
    public float resistenciaAire;
    private bool _sobreElPiso;
    private Rigidbody2D _cuerpo;
    private bool _estaSaltando;
    private bool _verticalPresionado;
    private float _saltoCount;

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

    void OnTriggerStay2D()
    {
        _sobreElPiso = true;
        _estaSaltando = false;
        _saltoCount = 0;
    }

    void OnTriggerExit2D()
    {
        _sobreElPiso = false;
    }

    void Update()
    {
        var valAxisHorizontal = Input.GetAxis(axisHorizontal);
        var valAxisVertical = Input.GetAxis(axisVertical);

        if(_sobreElPiso)    // mientras este en contacto con el suelo.
        {
            if(valAxisVertical > 0 && !_estaSaltando)
            {
                _estaSaltando = true;
                var horizontal = Vector2.right * valAxisHorizontal * velCaminado * Time.deltaTime;
                var vertical = Vector2.up * PoderSalto();
                _cuerpo.velocity = vertical + horizontal;
            }
            else if(Mathf.Abs(Input.GetAxisRaw(axisHorizontal)) > tolInput && !_estaSaltando)
            {
                var velX = velCaminado * Vector2.right * Mathf.Sign(valAxisHorizontal);
                _cuerpo.velocity = velX * Time.deltaTime;
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0)
            {
                _cuerpo.velocity = new Vector2(0, _cuerpo.velocity.y);
            }
        }
        else    // mientras este en el aire.
        {
            var fuerzaExtraAire = Vector2.zero;
            if(valAxisVertical > tolInput && _estaSaltando)
            {
                _saltoCount += Time.deltaTime;
                fuerzaExtraAire += Vector2.up * PoderSalto();
            }
            if(Mathf.Abs(valAxisHorizontal) > tolInput && _estaSaltando)
            {
                var horizontal = Vector2.right * valAxisHorizontal * fuerzaEnAire;
                fuerzaExtraAire += horizontal;
            }
            _cuerpo.AddForce(fuerzaExtraAire);

            if(valAxisHorizontal == 0)
            {
                var velHorizontalAire = -_cuerpo.velocity.x * resistenciaAire;
                _cuerpo.velocity += velHorizontalAire * Vector2.right;
            }
        }

        CalcularFlip();
    }

    private float PoderSalto()
    {
        var valor = poderSalto.Evaluate(_saltoCount);
        _saltoCount += Time.deltaTime;
        return valor;
    }

    private void CalcularFlip()
    {
        if(_sobreElPiso)
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                var localScale = transform.localScale;
                localScale.x = Mathf.Abs(localScale.x) * Input.GetAxisRaw("Horizontal");
                transform.localScale = localScale;
            }
        }
        else
        {
            var localScale = transform.localScale;
            if(_cuerpo.velocity.x > 0)
            {
                localScale.x = Mathf.Abs(localScale.x);
            }
            else if(_cuerpo.velocity.x < 0)
            {
                localScale.x = -Mathf.Abs(localScale.x);
            }
            transform.localScale = localScale;
        }
    }
}