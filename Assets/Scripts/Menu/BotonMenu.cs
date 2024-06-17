using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class BotonMenu : MonoBehaviour
{

    // Start is called before the first frame update

    public AudioSource audioSource;
    public AudioClip strikeOutSound;
    public AudioClip hoverSound; //Sonido de lapiz rayado del texto
    public AudioClip changeSceneSound; //sonido de paginas para cambio de escena o nose  




    public void OnEnterIncreaseSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize += 3;
        }
        
    }

    public void OnEnterUnderlineText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontStyle = FontStyles.Underline;
        }

    }

    
    public void OnEnterPlaySound(AudioClip ac) 
    {
        //por ahora pongo sonido predeterminado, quizas lo cambio a una lsita de sonidos y que coja uno aleatorio
        // quiero que si hay un sonido ya reproduciendo, en vez de no reproducir, que lo reproduzca con menos volumen
        if (audioSource != null && hoverSound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }


    public virtual void OnExitReduceSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize -= 3;
        }
    }

    public virtual void OnExitNormalStyleText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontStyle = FontStyles.Normal;
        }
    }

    public virtual void OnClick(GameObject g)
    {
        if (g != null)
        {
            g.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

    public virtual void PlaySoundChangeScene()
    {
        
        audioSource.PlayOneShot(changeSceneSound);
    }

}
