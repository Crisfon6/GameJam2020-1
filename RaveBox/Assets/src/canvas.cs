using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class canvas : MonoBehaviour
{
    public GameObject UINuevoJuego;
    public GameObject UICreditos;
    public GameObject UISalir;
    public void btnNuevoJuego(){
       //SceneManager.LoadScene("Tutorial");
SceneManager.LoadScene("Game");

    }
    public void btnSalir(){
         Application.Quit();
    }
    public void btnCreditos(){
       SceneManager.LoadScene("Creditos");
    }
    public void holaMundo(){
        //SceneManager.LoadScene("Tutorial");

 
    }
    
  
}
