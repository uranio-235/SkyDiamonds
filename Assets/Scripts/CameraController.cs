using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // el jugador se coloca aquí desde unity
    // en este caso, la pelotica de los cojones
    public GameObject Pelota;

    // la distancia entre la cámara y la pelota
    private Vector3 Distancia {get; set;}

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

    }

}
