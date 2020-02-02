using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
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
public Animator animator;
    float horizontalMove = 0f;
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
    void OnLanding(){
        animator.SetBool("IsJumping",false);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*40f;
        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        if(_sobreElPiso)    // mientras este en contacto con el suelo.
        {
            if(Input.GetAxis("Vertical") > 0 && !_estaSaltando)
            {
                _estaSaltando = true;
                animator.SetBool("IsJumping",true);
                var horizontal = Vector2.right * Input.GetAxis("Horizontal") * velCaminado * Time.deltaTime;
                var vertical = Vector2.up * PoderSalto();
                _cuerpo.velocity = vertical + horizontal;
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > tolInput && !_estaSaltando)
            {
                var velX = velCaminado * Vector2.right * Mathf.Sign(Input.GetAxis("Horizontal"));
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
            if(Input.GetAxis("Vertical") > tolInput && _estaSaltando)
            {
                _saltoCount += Time.deltaTime;
                fuerzaExtraAire += Vector2.up * PoderSalto();
            }
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > tolInput && _estaSaltando)
            {
                var horizontal = Vector2.right * Input.GetAxis("Horizontal") * fuerzaEnAire;
                fuerzaExtraAire += horizontal;
            }
            _cuerpo.AddForce(fuerzaExtraAire);

            if(Input.GetAxis("Horizontal") == 0)
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