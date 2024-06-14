using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class BotonSalir : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
        // Start is called before the first frame update

    public TextMeshProUGUI buttonText;
    public AudioSource audioSource;
    public AudioClip strikeOutSound;
    public AudioClip hoverSound; //Sonido de lapiz rayado del texto
    public GameObject textureObject; //textura de tachado

    void Start()
    {
        //la textura tachado debe estar desactivada hasta que se seleccione el boton
        if (textureObject != null)
        {
            textureObject.SetActive(false);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontStyle = FontStyles.Underline;
            buttonText.fontSize += 3;
        }
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontStyle = FontStyles.Normal;

            buttonText.fontSize -= 3;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (textureObject != null)
        {
            //Quiero un efecto de que la imagen se genera de izuqierda a derecha
            //textureObject.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

}
