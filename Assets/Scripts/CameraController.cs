using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// La lámpara, alias «Directional Light».
    /// Se espera que sea seteado desde unity.
    /// </summary>
    public GameObject Luz;

    /// <summary>
    /// Indica cada que tiempo debe cambiarse la luz
    /// se puede setear desde unity.
    /// </summary>
    public int CambiarLuz = 25;

    /// <summary>
    /// La pelota, el jugador. Setear desde unity.
    /// </summary>
    public GameObject Pelota;


    /// <summary>
    /// La distancia entre la cámara y la pelota.
    /// </summary>
    private Vector3 Distancia {get; set;}

    /// <summary>
    /// Indica que puntuación había cuando cambió la última
    /// vez de día para noche. Además, sirve como semáforo
    /// para saber si hay que cambiar.
    /// </summary>
    private int LastChange = 0;

    // sube el telón
    void Start()
    {
        // la distancia es la diferencia que hay en los ejes
        // de la cámara con respecto a la pelota
        Distancia = transform.position - Pelota.transform.position;
    }

    // corre cuando se halla terminado de mover la pelota
    void LateUpdate()
    {
        // aumenta la posición de la cámara de acorde
        // al desplazamiento y la posicón de la pelota
        transform.position = Pelota.transform.position + Distancia;

        // que puntuación está indicando la pelota
        var puntos = Pelota.GetComponent<PelotaController>().Puntos;

        // cada 25 puntos, cambia la situación de la luz
        if (puntos % CambiarLuz != 0 || puntos ==0 || puntos==LastChange )
            return;

        // graba la puntuación actual
        LastChange = puntos;

        // cambia la iluminación del terreno
        // y alterna la luz de la pelota
        if (Luz.GetComponent<Light>().intensity < 0.8f)
        {
            Luz.GetComponent<Light>().intensity = 0.8f;
            Pelota.GetComponent<Light>().intensity = 0f;
        }
        else
        {
            Luz.GetComponent<Light>().intensity = 0f;
            Pelota.GetComponent<Light>().intensity = 0.5f;
        }
    }
}
