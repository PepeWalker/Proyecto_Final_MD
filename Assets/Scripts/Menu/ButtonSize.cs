using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSize : MonoBehaviour
{

    public AudioSource audioSource;

public AudioClip hoverSound; //Sonido de lapiz rayado del texto



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterIncreaseSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize += 3;
        }

    }

    //pone un underline en el texto si la font lo permite se usa, si no, no
    public void OnEnterUnderlineText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontStyle = FontStyles.Underline;
        }

    }

    //reproduce el audio que tenga referenciado el boton, no haria falta tener las variables de audioclip
    public void OnEnterPlaySound(AudioClip ac)
    {
        //por ahora pongo sonido predeterminado, quizas lo cambio a una lsita de sonidos y que coja uno aleatorio
        // quiero que si hay un sonido ya reproduciendo, en vez de no reproducir, que lo reproduzca con menos volumen
        if (audioSource != null && hoverSound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ac);
        }
    }


    //reducir tamanio texto cuando sale el raton 
    public void OnExitReduceSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize -= 3;
        }
    }

    //poner el texto normal
    public void OnExitNormalStyleText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontStyle = FontStyles.Normal;
        }
    }

    


}
