using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectorDeFigura : MonoBehaviour
{
    public TipoFigura tipo;
    public Transform posicionFigura;
    public Vector3 rotacionLocal;
    public GameObject mecanismo;
    public string funcionAEjecutar;
    private bool _conectorCompletado;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Figura" && !_conectorCompletado)
        {
            var figura = other.GetComponent<Figura>();

            if(figura.tipo == tipo)
            {
                AceptarFigura(figura);
            }
            else
            {
                RechazarFigura(figura);
            }
        }
    }

    private void AceptarFigura(Figura figura)
    {
        var col = figura.GetComponent<Collider2D>();
        col.enabled = false;
        var objeto = figura.gameObject;
        var cuerpo = objeto.GetComponent<Rigidbody2D>();
        cuerpo.simulated = false;
        objeto.transform.position = posicionFigura.position;
        objeto.transform.parent = transform;
        objeto.transform.localEulerAngles = rotacionLocal;

        if(mecanismo != null)
        {
            mecanismo.SendMessage("funcionAEjecutar", SendMessageOptions.RequireReceiver);
        }
    }

    private void RechazarFigura(Figura figura)
    {
        figura.RestablecerPosicionInicial();
    }
}