using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class BotonOpciones : BotonMenu
{
    
        
    public override void OnPointerEnter(PointerEventData eventData)
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

   

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (textureObject != null)
        {
            //Quiero un efecto de que la imagen se genera de izuqierda a derecha
            //textureObject.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

}
