using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class BotonJugar : BotonMenu
{


    

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontSize -= 3;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (textureObject != null)
        {
            textureObject.SetActive(true); //No funciona, pilla la textura del textmeshpro, nose
            audioSource.PlayOneShot(strikeOutSound);
        }
    }

    public override void PlaySoundChangeScene()
    {
        
        audioSource.PlayOneShot(strikeOutSound);
    }

}
