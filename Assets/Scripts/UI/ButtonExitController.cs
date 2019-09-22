using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ButtonExitController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button BotónSalir, BotónRiniciar;

    void Start()
    {
        BotónRiniciar.onClick.AddListener(Reiniciar);
        BotónSalir.onClick.AddListener(Salir);
    }

    void Salir() =>
        Application.Quit();

    void Reiniciar() =>
         SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex,
            LoadSceneMode.Single);
        


} // class