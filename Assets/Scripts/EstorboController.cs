using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// sonar como si callera del cielo, parar cuando
// el colisionador se active y haga contacto con el suelo

// Describe al submarino y demás objetos futuros que se dedican
// a perturbar la tranquilidad y la monotonía del juego.
public class EstorboController : MonoBehaviour
{
    // una propiedad que indica si el jugador lo empujó
    public bool Tocado {get; set;} = false;

    // la posición inicial del submarino
    // la ubicación en la vitrina
    public Vector3 PosicionParqueo {get; set;}

    // al arrancar el script, registra la posición inicial
    private void Start() =>
        PosicionParqueo = this.gameObject.transform.position;

    // al chocar
    private void OnCollisionEnter(Collision col) {
        
        // haz un ruido al chocar con el piso
        if (col.gameObject.CompareTag("suelo")){
            GetComponent<AudioSource>().pitch=1f;
            GetComponent<AudioSource>().Play();
        }

        // indica si chocó con el jugador
        Tocado = col.gameObject.CompareTag("jugador");

        // sonido de contacto con la pelota
        if (Tocado && !GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().pitch=0.4f;
            GetComponent<AudioSource>().Play();
        }

    } // OnCollisionEnter


} // class
