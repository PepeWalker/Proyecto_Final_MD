using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class BotonMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
        // Start is called before the first frame update

    public TextMeshProUGUI buttonText;
    public AudioSource audioSource;
    public AudioClip strikeOutSound;
    public AudioClip hoverSound; //Sonido de lapiz rayado del texto
    public AudioClip changeSceneSound; //sonido de paginas para cambio de escena o nose  
    public GameObject textureObject; //textura de tachado

    void Start()
    {
        //la textura tachado debe estar desactivada hasta que se seleccione el boton
        if (textureObject != null)
        {
            textureObject.SetActive(false);
        }
        
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontSize += 3;
        }
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontSize -= 3;
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (textureObject != null)
        {
            textureObject.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

    public virtual void PlaySoundChangeScene()
    {
        
        audioSource.PlayOneShot(changeSceneSound);
    }

}
