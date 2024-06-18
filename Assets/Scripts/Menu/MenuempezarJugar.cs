using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuempezarJugar : MonoBehaviour
{


    public void CambioEscenaVolver()
    {

        //llama a otra funcion cambio de escena, porque ahi metere efecto para cambio de escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BotonJugar()
    {

        //llama a otra funcion cambio de escena, porque ahi metere efecto para cambio de escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
