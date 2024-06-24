using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class BotonMenu : MonoBehaviour
{

    // Start is called before the first frame update

    public AudioSource audioSource;

        // si se ponen en la llamada de la funcion en los botones no hacen falta tenerlas aqui
    public AudioClip strikeOutSound; //sonido de tachado
    public AudioClip hoverSound; //Sonido de lapiz rayado del texto
    public AudioClip changeSceneSound; //sonido de paginas para cambio de escena o nose  




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
    public  void OnExitReduceSizeText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontSize -= 3;
        }
    }

    //poner el texto normal
    public  void OnExitNormalStyleText(TextMeshProUGUI t)
    {
        if (t != null)
        {
            t.fontStyle = FontStyles.Normal;
        }
    }

    public  void OnClick(GameObject g)
    {
        if (g != null)
        {
            g.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

    public  void PlaySoundChangeScene()
    {
        
        audioSource.PlayOneShot(changeSceneSound);
    }

    public void PlaySoundStrikeOut()
    {
        audioSource.PlayOneShot(strikeOutSound);
    }


    public void PlayGame(Image i)
    {
        //cambia escena al en buildsettings, el indice 1, por ahora escena batalla
        Debug.Log("Cambiando a escena Indice 1");

        StartCoroutine(ICorrCambioEscenaJuego(i));
    }

    public float t; //tiempo del strikeOut
    float strikeOutTime ;
    IEnumerator ICorrCambioEscenaJuego(Image i)
    {
        t = 0;
        strikeOutTime = strikeOutSound.length;
        i.color = Color.white;

        PlaySoundStrikeOut();

        while (t < strikeOutTime)
        {
            t += Time.deltaTime * 0.03f;
            float smoothValue = Mathf.Lerp(0f, 1f, t / strikeOutTime);
            i.fillAmount = smoothValue;
        }
        t = 0;
        
        yield return new WaitForSeconds(strikeOutTime+1);


        //llama a otra funcion cambio de escena, porque ahi metere efecto para cambio de escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }

    //Animaciones de los bichos del menu
    public void TriggerAnimation(Animator animator)
    {
        animator.SetTrigger("recibiendoAtaque");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del Juego!");
        Application.Quit();
    }


}
