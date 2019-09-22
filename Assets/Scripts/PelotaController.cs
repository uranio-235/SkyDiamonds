using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;
using UnityEngine.SceneManagement;


public class PelotaController : MonoBehaviour
{

    /// <summary>
    /// El suelo dónde rueda la pelota. El campo de juego.
    /// </summary>
    public GameObject Suelo;

    /// <summary>
    /// La velocidad a la que se mueve la pelota. Controlable desde Unity.
    /// </summary>
    public float Velocidad;

    /// <summary>
    /// El control de text que contendrá la puntuación.
    /// </summary>
    public Text TextPuntos;

    /// <summary>
    /// El contador de puntos.
    /// </summary>
    public int Puntos;

    // vamos a necesitar el rigidbody de la bola
    private Rigidbody Cuerpo { get; set; }

    /// <summary>
    /// La mayor cantidad de puntos que ha hecho este jugador.
    /// </summary>
    /// <value></value>
    [SerializeField]
    public int Record { get; set; } = 0;



    /// <summary>
    ///  Lo que sucederá cuando arranque el juego
    /// </summary>
    private void Awake() {

        // si no tiene un record establecido
        // inicialízalo como 0
        if (!PlayerPrefs.HasKey("record"))
            PlayerPrefs.SetInt("record",0);

    } // Awake
        


    /// <summary>
    /// Aumenta los puntos en determinada cantidad.
    /// </summary>
    /// <param name="monto">cantidad a aumentar</param>
    public void AumentarPuntos(int monto)
    {
        // aumenta los puntos
        Puntos += monto;

        // si está rompiendo record, auméntalo
        if (PlayerPrefs.GetInt("record")<Puntos){
            PlayerPrefs.SetInt("record",Puntos);
            PlayerPrefs.Save();
        }

        // muestra la puntuación si hay algo que mostrar
        if (Puntos == 1)
            TextPuntos.text = $"Bien! Veamos cuantas puedes agarrar, sin caerte.";
        else
            if (Puntos>=PlayerPrefs.GetInt("record"))
                TextPuntos.text = $"{Puntos} gemas recogidas, ¡imponiendo record!.";
            else
                TextPuntos.text = $"{Puntos} gemas recogidas, tu record es de {PlayerPrefs.GetInt("record")}";
    }

    /// <summary>
    /// Incrementa un punto a la puntuación total del juego.
    /// </summary>
    public void AumentarPuntos() =>
        AumentarPuntos(1);


    // sube el telón
    private void Start()
    {
        // toma el rigidbody
        Cuerpo = GetComponent<Rigidbody>();

        // coloca cada picker en una posició aleatoria
        // también dale una forma y textura aleatoria
        foreach (var gema in GameObject.FindGameObjectsWithTag("pickup"))
            Gem.LanzarGema(gema);
        
    } // Start

    // cuando se hace un cómputo de física
    private void FixedUpdate()
    {
        // toma la posición que indican los cursores
        float EjeHorizontal = Input.GetAxis("Horizontal");
        float EjeVertical = Input.GetAxis("Vertical");

        // el empujón que le meteremos a la pelota es un Vector de 3 ejes
        // además, incrementamos estos valores por la velocidad dada
        // si salta, no hagas fuerza por los ejes
        Cuerpo.AddForce(new Vector3() { x = EjeHorizontal, y = 0, z = EjeVertical }* Velocidad);

    } // FixedUpdate

    // cuando choques con algo
    private void OnCollisionEnter(Collision chocante)
    {
        // solo interactuamos con las gemas
        if (!chocante.gameObject.CompareTag("pickup"))return;

        // haz un sonido de contacto con la gema
        GetComponent<AudioSource>().Play();

        // la hacer contacto con una gema, simplemnte la volvemos a tirar
        Gem.LanzarGema(chocante.gameObject);

        // dale un ligero toque a la bola, no sea que
        // el piso se mueva y me tire para abajo
        Cuerpo.Sleep();
        Cuerpo.AddForce(new Vector3(0, 100, 0));

        // aumenta los puntos
        AumentarPuntos(1);

        // hagamos esto más divertido...
        // aumenta la velocidad de acorde a los puntos
        Velocidad += Puntos / 100f;

        // y más emocionante!
        // cada 5 puntos, inclina el terreno de acorde a la puntaución
        // pero sólo si tiene más de dies puntos y el terreno está lo
        // bastante elevado. Cada vez que el suelto rota, tiene que bajar.
        if (Puntos % 5 == 0 && Puntos > 10 && Suelo.transform.position.y >= 0)
        {

            // emite un sonido que evoque movimiento brusco
            Suelo.GetComponent<AudioSource>().Play();

            // Haz bajar el piso, ya que si no, al rotar el terreno
            // la bola caería del plano. Esto además causa una sacudida
            // que mueve las gemas y hace rebotar la pelota. Muy emotivo!
            Suelo.transform.position = new Vector3(0, -1.3f, 0);

            // la rotación varía según la cantidad de puntos
            // sin embargo, nunca puede exceder los 22 «grados», sería desagradable.
            // En caso de ser más de 22, pues tomamos un número entre 15 y 22
            // no siempre saldrá 20, ya que sería aburrido, así coge un respiro de vez en cuando
            Func<float> Rotación = () => 
                Puntos / 5 <= 22f ? N.Random(0.1f, Puntos / 5) : N.Random(15, 22);

            // rota el terreno en una posición aleatoria
            // las X siempre rotan en negativo, para que el terreno
            // quede de frente a la cámara
            Suelo.transform.rotation = new Quaternion(0, 0, 0, 0);
            Suelo.transform.Rotate(new Vector3(Rotación() * -1, 0, Rotación()));

        } // if

        // siempre el terreno va subiendo lentamente hasta llegar a 0
        // es una recuperación que además, causa breves sacudidas a la bola
        if (Suelo.transform.position.y < 0)
            Suelo.transform.position =
                new Vector3(0, Suelo.transform.position.y + 0.2f, 0);

    } // OnTriggerEnter

} // class