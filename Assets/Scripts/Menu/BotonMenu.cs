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

        // si se ponen en la llamada de la funcion en los botones no hacen falta tenerlas aqui
    public AudioClip strikeOutSound; //sonido de tachado
    public AudioClip hoverSound; //Sonido de lapiz rayado del texto
    public AudioClip changeSceneSound; //sonido de paginas para cambio de escena o nose  



    public void Tachar (Image i)
    {
        StartCoroutine(ICorrTachado(i));
    }

    public float t;
    IEnumerator ICorrTachado (Image i)
    {
        t = 0;
        i.color = Color.white;
        while (t<1 )
        {
           i.fillAmount = t;
            t+= Time.deltaTime*5;

            yield return null;

        }
        t=0;
        yield return null;

    }


    // Aumenta el tamanio del texto para luego reducirlo, dar retroalimentacion de donde esta el raton
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
    public virtual void OnExitReduceSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize -= 3;
        }
    }

    //poner el texto normal
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
