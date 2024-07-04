using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    public GameObject pauseMenuUI, opcionesMenuUI;
    public TextMeshProUGUI pauseText; //creo no uso

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        GameEvents.Instance.TogglePause();
        pauseMenuUI.SetActive(GameEvents.Instance.isPaused);

    }

    public void ToggleOpciones()
    {
        GameEvents.Instance.ToggleOpciones();
        opcionesMenuUI.SetActive(GameEvents.Instance.inOpciones);

    }


    public void RestartGameButton()
    {
        GameEvents.Instance.RestartScene();
    }

    public void ReturnToMainMenuButton()
    {
        GameEvents.Instance.ReturnToMainMenu();
    }

     //public void SaveGameButton() { GameEvents.Instance.SaveGame(); }

}
