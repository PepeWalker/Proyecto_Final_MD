using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame ()
    {
        //cambia escena al en buildsettings, el indice 1, por ahora escena batalla
        Debug.Log("Cambiando a escena Indice 1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); 

    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del Juego!");
        Application.Quit();
    }


}
