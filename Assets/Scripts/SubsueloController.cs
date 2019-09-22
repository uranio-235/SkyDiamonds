using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Helper;
using System.Linq;
using UnityEngine.SceneManagement;

public class SubsueloController : MonoBehaviour
{
    // la pelota de los cojones
    public GameObject Pelota;

    // el texto de la ui que contiene la puntuación
    public Text GameOverText;

    // el panel que tapa la escena
    public GameObject GameOverPanel;

    // semáforo que indica si hay un estorbo en movimiento
    public bool HayEstorbo {get; set;} = false;

    private void OnTriggerEnter(Collider col)
    {
        // si calló alguna gema, vuélvela a tirar pa arriba
        if (col.gameObject.CompareTag("pickup"))
            Gem.LanzarGema(col.gameObject);

        // si calló un estorbo, desactívalo
        if (col.gameObject.CompareTag("estorbo"))
            RecogerEstorbo(col.gameObject);

        // mira a ver si puedes tirar un estorbo
        if (Pelota.gameObject.GetComponent<PelotaController>().Puntos % 6 ==0)
            TirarEstorbo();

        // si cayó el jugador, termian el juego
        if (col.gameObject.CompareTag("jugador"))
            FinalizarJuego();

    } // OnTriggerEnter

    
    /// <summary>
    /// Recoge un estorbo que halla caído y lo desaparece.
    /// </summary>
    private void RecogerEstorbo(GameObject estorbo) 
    {
        // si el jugador hizo contacto con el estorbo
        // le incrementaremos los puntos
        // if (estorbo.GetComponent<EstorboController>())

        // parquéalo en la vitrina hasta nuevo aviso
        estorbo.transform.position = 
            estorbo.GetComponent<EstorboController>().PosicionParqueo;

        // páralo!
        estorbo.GetComponent<Rigidbody>().Sleep();

        // incrementa los puntos
        GameObject.FindGameObjectWithTag("jugador")
            .GetComponent<PelotaController>()
            .AumentarPuntos(N.Random(9,15));

        // no hay estorbo
        HayEstorbo=false;

    } // RecogerEstorbo() 


    /// <summary>
    /// Tira un objeto que estorba, como el submarino, etc...
    /// </summary>
    public void TirarEstorbo()
    {
        // sin movimiento en caso de haber un estorbo ya
        if (HayEstorbo) return;

        // indica que hay un estorbo en órbita
        HayEstorbo=true;

        // toma todos los estorbos que halla en juego
        GameObject[] estorbos = GameObject.FindGameObjectsWithTag("estorbo");

        // coge uno al azar y déjalo caer del cielo
        estorbos.ElementAt((int)N.Random(0,estorbos.Length-1))
            .gameObject.transform.position = new Vector3(1,9,0);

    } // TirarEstorbo()


    /// <summary>
    /// Termina el Juego, mostrando la pantalla de finalización
    /// </summary>
    private void FinalizarJuego()
    {
        // apágale la música a la pelota
        Pelota.GetComponent<AudioSource>().Stop();

        // cuantos puntos hizo
        // toma los puntos de la pelota desde otro objeto, GENIAL!
        // es que simplemente el componente script tiene eso como public
        int puntos = Pelota.GetComponent<PelotaController>().Puntos;

        // desactiva la pelota
        Pelota.SetActive(false);

        // según la puntuación, el texto
        if (puntos==0)
            GameOverText.text = @"¿podrías al menos «tropezar» con una gema?";
        else if (puntos ==1 )
            GameOverText.text = $"solo agarraste una gema";
        else
            GameOverText.text = $"{puntos} gemas recogidas y tu record es de {PlayerPrefs.GetInt("record")}";

        // muestra el panel de gameover
        GameOverPanel.SetActive(true);

    } // FinalizarJuego 

} // class